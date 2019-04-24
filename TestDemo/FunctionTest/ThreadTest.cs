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

            //Console.WriteLine($"ThreadTest end\n");
        }

        #region Thread constructor

        void ConstructorTest()
        {
            Thread thread = new Thread(new ThreadStart(ThreadMethod));
            Thread threadWithPara = new Thread(new ParameterizedThreadStart(ThreadMethodWithPara));
            thread.Start();
            threadWithPara.Start("This is a string parameter");
        }

        void ThreadMethod()
        {
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");
        }

        void ThreadMethodWithPara(object obj)
        {
            Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}, parameter: {obj?.ToString()}");
        }

        #endregion Thread constructor

        #region Thread join

        void ThreadInherit()
        {
            Thread.CurrentThread.Name = "level 0 yo";
            Console.WriteLine($"Level 0 thread name: {Thread.CurrentThread.Name}");
            new Thread(() => Console.WriteLine($"Level 1_1 thread name: {Thread.CurrentThread.Name}")) { Name = "level 1_1 yo" }.Start();
            new Thread(new ThreadStart(() =>
            {
                Console.WriteLine($"Level 1_2 thread name: {Thread.CurrentThread.Name}");
                new Thread(() => Console.WriteLine($"Level 2 thread name: {Thread.CurrentThread.Name}")) { Name = "level 2 yoyo" }.Start();
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
                    Console.WriteLine($"Sub thread {Thread.CurrentThread.Name} is running now, {j % 10 * 10}");
                }
                Console.WriteLine($"Sub thread {Thread.CurrentThread.Name} is done");
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
            printWithTime($"Back to main thread, thread {thread.Name} state is {thread.ThreadState}\n");
        }

        void Abort()
        {
            try
            {
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                printWithTime($"Thread {Thread.CurrentThread.Name} receive abort signal, thread state: {Thread.CurrentThread.ThreadState.ToString()}, in catch block. Exception: {ex.Message}");
            }
            finally
            {
                printWithTime($"Thread {Thread.CurrentThread.Name} receive abort signal, thread state: {Thread.CurrentThread.ThreadState.ToString()}, in finally block");
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
            printWithTime($"Back to main thread, thread {thread.Name} state is {thread.ThreadState}\n");
        }

        void Interrupt()
        {
            try
            {
                Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                printWithTime($"Thread {Thread.CurrentThread.Name} receive interrupt signal, thread state: {Thread.CurrentThread.ThreadState.ToString()}, in catch block. Exception: {ex.Message}");
            }
            finally
            {
                printWithTime($"Thread {Thread.CurrentThread.Name} receive interrupt signal, thread state: {Thread.CurrentThread.ThreadState.ToString()}, in finally block");
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
            printWithTime($"Thread {Thread.CurrentThread.Name} has been suspended");
            Thread.CurrentThread.Suspend();
            printWithTime($"Thread {Thread.CurrentThread.Name} has been resumed");
        }

        #endregion Thread suspend and resume

        #region Func and action

        Action<Thread> startThenJoinAction = thread =>
        {
            thread.Start();
            thread.Join();
        };

        Action<string> printWithTime = text => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {text}");

        #endregion Func and action
    }
}
