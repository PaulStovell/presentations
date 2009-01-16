using System;

namespace Common.Threading
{
    public interface IDispatcher
    {
        void Dispatch(Action action);
    }
}