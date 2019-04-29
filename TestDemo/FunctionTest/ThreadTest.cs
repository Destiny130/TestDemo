using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Web;

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

            //AsyncResultTest();

            //SimpleThreadPool();

            //VolatileTest();

            //InterlockedTest();

            //DeadLock();

            //ReaderWriterLockTest();

            //MonitorTest();  //??

            AutoResetEventTest();

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

        #region Thread suspend and resume, comment out now

        //void ThreadSuspendAndResume()
        //{
        //    printWithTime(String.Empty);
        //    Thread thread1 = new Thread(Suspend) { Name = "t1" };
        //    Thread thread2 = new Thread(Suspend) { Name = "t2" };
        //    thread1.Start();
        //    thread2.Start();
        //    Thread.Sleep(3000);
        //    thread1.Resume();
        //    thread2.Resume();
        //    Console.WriteLine();
        //}

        //void Suspend()
        //{
        //    printWithTime($"Thread {currThreadName()} has been suspended");
        //    Thread.CurrentThread.Suspend();
        //    printWithTime($"Thread {currThreadName()} has been resumed");
        //}

        #endregion Thread suspend and resume, comment out now

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

        #region Simple thread pool

        void SimpleThreadPool()
        {
            TimeSpan sleepTime = TimeSpan.FromSeconds(2);
            ThreadStart[] threadArray = new ThreadStart[]
            {
                () => printWithTimeAndSleep($"First\t current thread id: {currThreadId()}, state: {currThreadState()}", sleepTime),
                () => printWithTime($"Second\t current thread id: {currThreadId()}, state: {currThreadState()}"),
                () => printWithTimeAndSleep($"Thrid\t current thread id: {currThreadId()}, state: {currThreadState()}", sleepTime),
                () => printWithTimeAndSleep($"Fourth\t current thread id: {currThreadId()}, state: {currThreadState()}", sleepTime),
                () => printWithTimeAndSleep($"Fivth\t current thread id: {currThreadId()}, state: {currThreadState()}", sleepTime),
                () => printWithTimeAndSleep($"Sixth\t current thread id: {currThreadId()}, state: {currThreadState()}", sleepTime),
            };
            SetMaxThreadNum(4);
            QueueThread(threadArray);
        }

        #region Could be a class

        static object poolLocker = new object();
        static Queue<ThreadStart> threadQueue = new Queue<ThreadStart>();
        static HashSet<ThreadStart> threadSet = new HashSet<ThreadStart>();
        static int maxThreadNum = 1;
        static int minThreadNum = 0;

        public static void SetMaxThreadNum(int max)
        {
            maxThreadNum = Math.Max(minThreadNum, max);
        }

        public static void SetMinThreadNum(int min)
        {
            minThreadNum = Math.Min(maxThreadNum, min);
        }

        public static void QueueThread(ThreadStart[] threadArray)
        {
            AddThreadsToPool(threadArray);
            ExecuteThread();
        }

        static void ThreadEnqueue(ThreadStart thread)
        {
            lock (poolLocker)
            {
                threadQueue.Enqueue(thread);
            }
        }

        static void AddThreadsToPool(ThreadStart[] threadArray)
        {
            foreach (ThreadStart thread in threadArray)
            {
                ThreadEnqueue(thread);
            }
        }

        static void ExecuteThread()
        {
            while (threadQueue.Count != 0)
            {
                if (threadSet.Count < maxThreadNum)
                {
                    ExecuteThreadInQueue();
                }
            }
        }

        static void ExecuteThreadInQueue()
        {
            lock (poolLocker)
            {
                ExecuteThreadSingly(threadQueue.Dequeue());
            }
        }

        static void ExecuteThreadSingly(ThreadStart thread)
        {
            threadSet.Add(thread);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (sender, e) => thread.Invoke();
            worker.RunWorkerCompleted += (sender, e) => threadSet.Remove(thread);
            worker.RunWorkerAsync();
        }

        #endregion Could be a class

        #endregion Simple thread pool

        #region MyRegion



        #endregion MyRegion

        #region Volatile

        //int volatileCount;
        //int volatileValue;

        volatile int volatileCount;
        int volatileValue;

        void VolatileTest()
        {
            new Thread(VolatileReadTest).Start();
            for (int i = 0; i < 10; ++i)
            {
                Thread.Sleep(20);
                new Thread(VolatileWriteTest).Start();
            }
        }

        //void VolatileWriteTest()
        //{
        //    int temp = 0;
        //    for (int i = 0; i < 10000000; ++i)
        //    {
        //        temp += 1;
        //    }
        //    volatileValue += temp;
        //    Thread.VolatileWrite(ref volatileCount, 1);
        //}

        //void VolatileReadTest()
        //{
        //    while (true)
        //    {
        //        if (Thread.VolatileRead(ref volatileCount) > 0)
        //        {
        //            printWithTime($"Thread {currThreadId()} count: {volatileValue}");
        //            volatileCount = 0;
        //        }
        //    }
        //}

        void VolatileWriteTest()
        {
            int temp = 0;
            for (int i = 0; i < 10000000; ++i)
            {
                temp += 1;
            }
            volatileValue += temp;
            volatileCount = 1;
        }

        void VolatileReadTest()
        {
            while (true)
            {
                if (volatileCount == 1)
                {
                    printWithTime($"Thread {currThreadId()} count: {volatileValue}");
                    volatileCount = 0;
                }
            }
        }

        #endregion Volatile

        #region Interlocked

        long interlockedCount = 0;

        void InterlockedTest()
        {
            for (int i = 0; i < 20; ++i)
            {
                new Thread(InterlockedIncr).Start();
                //Thread.Sleep(20);
                new Thread(InterlockedDecr).Start();
                //Thread.Sleep(20);
            }

            Thread.Sleep(100);
            Interlocked.Add(ref interlockedCount, 125);
            printWithTime($"After add {125}, count: {interlockedCount}");
            Interlocked.Exchange(ref interlockedCount, 5874);
            printWithTime($"After exchange to {5874}, count: {interlockedCount}");
        }

        void InterlockedIncr()
        {
            //if (Interlocked.Read(ref interlockedCount) == 0)
            //{
            printWithTime($"Thread {currThreadId()} enter increment area");
            Interlocked.Increment(ref interlockedCount);
            printWithTime($"Count is: {Interlocked.Read(ref interlockedCount)}");
            //}
        }

        void InterlockedDecr()
        {
            //if (Interlocked.Read(ref interlockedCount) == 1)
            //{
            printWithTime($"Thread {currThreadId()} enter decrement area");
            Interlocked.Decrement(ref interlockedCount);
            printWithTime($"Count is: {Interlocked.Read(ref interlockedCount)}\n");
            //}
        }

        #endregion Interlocked

        #region Dead lock

        void DeadLock()
        {
            LockAnotherClass lockAnother = new LockAnotherClass();
        }

        #endregion Dead lock

        #region ReaderWriterLock

        ReaderWriterLock rwLock = new ReaderWriterLock();
        List<string> strList;

        void ReaderWriterLockTest()
        {
            strList = new List<string>() { "zhao", "qian" };
            for (int i = 0; i < 5; ++i)
            {
                if (i < 2)
                {
                    new Thread(AcquireWriter).Start($"sun{i.ToString()}");
                    //new Thread(AcquireWriter).Start();
                }
                else
                {
                    new Thread(AcquireReader).Start();
                }
                //Thread.Sleep(20);
            }
        }

        void AcquireWriter(object obj)
        {
            if (!(obj is string))
                return;
            try
            {
                Thread.Sleep(20);
                rwLock.AcquireWriterLock(Timeout.Infinite);
                strList.Add(obj.ToString());
                printWithTime($"Write thread {currThreadId()}, wrote: {obj.ToString()}");
            }
            catch (Exception ex)
            {
                printWithTime($"{nameof(AcquireWriter)} {nameof(Exception)}: {ex.Message}");
            }
            finally
            {
                rwLock.ReleaseWriterLock();
            }
        }

        void AcquireReader()
        {
            try
            {
                rwLock.AcquireReaderLock(Timeout.Infinite);
                //printWithTime($"Read thread {currThreadId()}, list count: {strList.Count},last names: {String.Join(", ", strList)}");
                strList.ForEach(str =>
                {
                    printWithTime($"Read thread {currThreadId()}, list count: {strList.Count},last name: {str}");
                    Thread.Sleep(20);
                });
            }
            catch (Exception ex)
            {
                printWithTime($"{nameof(AcquireReader)} {nameof(Exception)}: {ex.Message}");
            }
            finally
            {
                rwLock.ReleaseReaderLock();
            }
        }

        #endregion ReaderWriterLock

        #region Monitor

        object monitorLocker = new object();

        void MonitorTest()
        {
            new Thread(MonitorWriter).Start();
            new Thread(MonitorWriter).Start();
            new Thread(MonitorReader).Start();
            new Thread(MonitorReader).Start();
        }

        void MonitorWriter()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Monitor.Enter(monitorLocker);
                printWithTime($"Thread {currThreadId()} change data");
                Thread.Sleep(1000);
                Monitor.Pulse(monitorLocker);
                Monitor.Exit(monitorLocker);
            }
        }

        void MonitorReader()
        {
            while (true)
            {
                Monitor.Enter(monitorLocker);
                Monitor.Wait(monitorLocker, 4 * 1000);
                printWithTime($"Thread {currThreadId()} read data");
                Thread.Sleep(1000);
                Monitor.Exit(monitorLocker);
            }
        }

        #endregion Monitor

        #region AutoResetEvent

        void AutoResetEventTest()
        {
            string str = "ABCDEFGHIJKLMNOPQ";
            List<Action> list = new List<Action>();
            foreach (char c in str)
            {
                list.Add(() => printWithTime($"Thread {currThreadId()} {c}"));
            }
            InvokeActionList(list);
        }

        void InvokeActionList(List<Action> actionList)
        {
            if (actionList == null || actionList.Count == 0)
                return;
            WaitHandle[] waitHandleArray = Enumerable.Range(0, actionList.Count).Select(i =>
            {
                WaitHandle waitHandle = new AutoResetEvent(false);
                ThreadPool.QueueUserWorkItem(handle =>
                {
                    actionList[i]();
                    Thread.Sleep(new Random().Next(1000, 10 * 1000));
                    //Thread.Sleep(30 * 1000);
                    (waitHandle as AutoResetEvent).Set();
                }, waitHandle);
                return waitHandle;
            }).ToArray();
            WaitHandle.WaitAll(waitHandleArray);
            printWithTime($"Back to main thread {currThreadId()}");
        }

        #endregion AutoResetEvent

        #region Delegates

        public Action<Thread> startThenJoinAction = thread =>
        {
            thread.Start();
            thread.Join();
        };

        public Func<Thread> currThread = () => Thread.CurrentThread;

        public Func<int> currThreadId = () => Thread.CurrentThread.ManagedThreadId;

        public Func<string> currThreadName = () => Thread.CurrentThread.Name;

        public Func<string> currThreadState = () => Thread.CurrentThread.ThreadState.ToString();

        public Action<string> printWithTime = text => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {text}");

        public Action<string, TimeSpan> printWithTimeAndSleep = (text, ts) =>
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {text}");
            Thread.Sleep(ts);
        };

        #endregion Delegates
    }

    public class HttpCounterResult : IHttpAsyncHandler
    {
        static int count = 0;
        static object locker = new object();

        public void ProcessRequest(HttpContext context)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {nameof(ProcessRequest)}, thread id: {Thread.CurrentThread.ManagedThreadId}, state: {Thread.CurrentThread.ThreadState.ToString()}");
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object param)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {nameof(BeginProcessRequest)}, thread id: {Thread.CurrentThread.ManagedThreadId}, state: {Thread.CurrentThread.ThreadState.ToString()}");
            lock (locker)
            {
                ++count;
            }
            AsyncCounter result = new AsyncCounter(param, callback);
            result.Display();
            return result;
        }

        public void EndProcessRequest(IAsyncResult result)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {nameof(EndProcessRequest)}, thread id: {Thread.CurrentThread.ManagedThreadId}, state: {Thread.CurrentThread.ThreadState.ToString()}");
        }
    }

    public class AsyncCounter : IAsyncResult
    {
        object _param;
        bool _asyncIsComplete;
        AsyncCallback _callback;

        public AsyncCounter(object param, AsyncCallback callback)
        {
            _param = param;
            _callback = callback;
        }

        public object AsyncState
        {
            get { return _param; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return null; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return _asyncIsComplete; }
        }

        public void Display()
        {
            lock (this)
            {
                _asyncIsComplete = true;
                _callback(this);
            }
        }
    }

    public class LockMyself
    {
        public LockMyself()
        {
            new Thread(() =>
            {
                while (true)
                {
                    lock (this)
                    {
                        Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} enter {nameof(LockMyself)} sharing area");
                        Thread.Sleep(3 * 1000);
                    }
                }
            }).Start();
        }
    }

    public class LockAnotherClass
    {
        public LockAnotherClass()
        {
            LockMyself lockMyself = new LockMyself();
            //Thread.Sleep(100);
            lock (lockMyself)
            {
                Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} Now I locked LockMyself");
                Timer timer = new Timer((obj) => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}"), this, 0, 1000);
                Thread.Sleep(10 * 1000);
            }
        }
    }
}
