using System;
using System.Threading;
using System.Web;

namespace TestDemo.FunctionTest.Help
{
    public class HttpCounterResult : IHttpAsyncHandler
    {
        static int count = 0;
        static readonly object locker = new object();

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
}
