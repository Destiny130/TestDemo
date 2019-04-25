using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestDemo.FunctionTest
{
    public class ThreadTest
    {
        public void Start()
        {
            //ConstructorTest();

            //ThreadInherit();
            //ThreadJoin();
            //ThreadJoinOneByOne();

            //ThreadAbort();

            //ThreadInterrupt();

            //ThreadSuspendAndResume();

            //ThreadPoolTest();

            AsyncResultTest();

            //Console.WriteLine($"ThreadTest end\n");
        }

        #region Thread constructor

        void ConstructorTest()
        {
            Thread thread = new Thread(ThreadMethod);
            Thread threadWithPara = new Thread(ThreadMethodWithPara);
            thread.Start();
            threadWithPara.Start("dummy parameter");
        }

        void ThreadMethod()
        {
            Console.WriteLine($"ThreadId: {currThreadId()}");
        }

        void ThreadMethodWithPara(object obj)
        {
            Console.WriteLine($"ThreadId: {currThreadId()}, parameter: {obj?.ToString()}");
        }

        #endregion Thread constructor

        #region Thread join

        void ThreadInherit()
        {
            Thread.CurrentThread.Name = "level 0 yo";
            Console.WriteLine($"Level 0 thread name: {currThreadName()}");
            new Thread(() => Console.WriteLine($"Level 1_1 thread name: {currThreadName()}")) { Name = "level 1_1 yo" }.Start();
            new Thread(new ThreadStart(() =>
            {
                Console.WriteLine($"Level 1_2 thread name: {Thread.CurrentThread.Name}");
                new Thread(() => Console.WriteLine($"Level 2 thread name: {currThreadName()}")) { Name = "level 2 yoyo" }.Start();
            }))
            { Name = "level 1_2 yo" }.Start();
        }

        void ThreadJoin()
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < 5; ++i)
                {
                    Thread.Sleep(600);
                    Console.WriteLine($"Sub thread is running now, {i % 5 * 20}%");
                }
                Console.WriteLine($"Sub thread is done");
            })
            { Name = "Sub thread" };
            startThenJoinAction(thread);
            Console.WriteLine($"Now is main thread\n");
        }

        void ThreadJoinOneByOne()
        {
            List<Thread> list = Enumerable.Range(0, 3).Select(i => new Thread(() =>
            {
                for (int j = 0; j < 10; ++j)
                {
                    Thread.Sleep(600);
                    Console.WriteLine($"Sub thread {currThreadName()} is running now, {j % 10 * 10}%");
                }
                Console.WriteLine($"Sub thread {currThreadName()} is done");
            })
            { Name = $"SubThread {i.ToString()}" }).ToList();
            list.ForEach(startThenJoinAction);
            Console.WriteLine($"Sub threads are done\n");
        }

        #endregion Thread join

        #region Thread abort

        void ThreadAbort()
        {
            printWithTime(String.Empty);
            Thread thread = new Thread(Abort) { Name = "uselessThread" };
            thread.Start();
            Thread.Sleep(1000);
            thread.Abort();
            thread.Join();
            printWithTime($"Back to main thread, thread {thread.Name} state: {thread.ThreadState}\n");
        }

        void Abort()
        {
            try
            {
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                printWithTime($"Thread {currThreadName()} receive abort signal, thread state: {currThreadState()}, in catch block. Exception: {ex.Message}");
            }
            finally
            {
                printWithTime($"Thread {currThreadName()} receive abort signal, thread state: {currThreadState()}, in finally block");
            }
        }

        #endregion Thread abort

        #region Thread interrupt

        void ThreadInterrupt()
        {
            printWithTime(String.Empty);
            Thread thread = new Thread(Interrupt) { Name = "uselessThread2" };
            thread.Start();
            Thread.Sleep(1000);
            thread.Interrupt();
            thread.Join();
            printWithTime($"Back to main thread, thread {thread.Name} state: {thread.ThreadState}\n");
        }

        void Interrupt()
        {
            try
            {
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                printWithTime($"Thread {currThreadName()} receive interrupt signal, thread state: {currThreadState()}, in catch block. Exception: {ex.Message}");
            }
            finally
            {
                printWithTime($"Thread {currThreadName()} receive interrupt signal, thread state: {currThreadState()}, in finally block");
            }
        }

        #endregion Thread interrupt

        #region Thread suspend and resume

        void ThreadSuspendAndResume()
        {
            printWithTime(String.Empty);
            Thread thread1 = new Thread(Suspend) { Name = "t1" };
            Thread thread2 = new Thread(Suspend) { Name = "t2" };
            thread1.Start();
            thread2.Start();
            Thread.Sleep(3000);
            thread1.Resume();
            thread2.Resume();
            Console.WriteLine();
        }

        void Suspend()
        {
            printWithTime($"Thread {currThreadName()} has been suspended");
            Thread.CurrentThread.Suspend();
            printWithTime($"Thread {currThreadName()} has been resumed");
        }

        #endregion Thread suspend and resume

        #region Thread pool

        void ThreadPoolTest()
        {
            printWithTime($"Main thread, id: {currThreadId()}");
            ManualResetEvent[] resetEventArr = new ManualResetEvent[6];
            for (int i = 0; i < 6; ++i)
            {
                resetEventArr[i] = new ManualResetEvent(false);
                ThreadPool.QueueUserWorkItem(ThreadSingle, new Tuple<string, ManualResetEvent>("dummy parameter", resetEventArr[i]));
            }
            ManualResetEvent.WaitAll(resetEventArr);
            printWithTime($"Back to main thread, id: {currThreadId()}");
        }

        void ThreadSingle(object obj)
        {
            Tuple<string, ManualResetEvent> tuple = obj as Tuple<string, ManualResetEvent>;
            if (tuple == null)
            {
                printWithTime($"Sub thread id: {currThreadId()}, state: {currThreadState()}, parameter parse failed");
            }
            else
            {
                printWithTime($"Sub thread id: {currThreadId()}, state: {currThreadState()}, parameter: {tuple.Item1}");
                //Thread.Sleep(2000);
                tuple.Item2.Set();
            }
        }

        #endregion Thread pool

        #region AsyncResult

        void AsyncResultTest()
        {
            printWithTime($"Main thread id: {currThreadId()}");
            IAsyncResult result = printWithTime.BeginInvoke($"Dummy head, thread id: {currThreadId()}", asyncResult =>
            {
                printWithTime.EndInvoke(asyncResult);
                printWithTime($"{asyncResult.AsyncState.ToString()}, thread id: {currThreadId()}");
            }, "Dummy footer");
            printWithTime($"Back to main thread id: {currThreadId()}");
        }

        #endregion AsyncResult

        #region Delegates

        Action<Thread> startThenJoinAction = thread =>
        {
            thread.Start();
            thread.Join();
        };

        Func<Thread> currThread = () => Thread.CurrentThread;

        Func<int> currThreadId = () => Thread.CurrentThread.ManagedThreadId;

        Func<string> currThreadName = () => Thread.CurrentThread.Name;

        Func<string> currThreadState = () => Thread.CurrentThread.ThreadState.ToString();

        Action<string> printWithTime = text => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {text}");

        #endregion Delegates
    }
}
