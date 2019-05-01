using System;
using System.Threading;

namespace TestDemo.FunctionTest.Help
{
    public class AsyncCounter : IAsyncResult
    {
        readonly object _param;
        bool _asyncIsComplete;
        readonly AsyncCallback _callback;

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
}
