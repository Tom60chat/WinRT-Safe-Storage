using System;
using System.Threading.Tasks;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage.FileProperties
{
    public class Safe
    {
        public Exception LastException { get; private set; }

        protected bool Try(Action execution)
        {
            if (SafeExecution.This(execution))
                return true;
            else
            {
                LastException = SafeExecution.LastException;
                return false;
            }
        }

        protected T Try<T>(Action<T> execution)
        {
            var value = SafeExecution.This(execution);
            if (value != null)
                return value;
            else
            {
                LastException = SafeExecution.LastException;
                return value;
            }
        }

        protected async Task<bool> Try(Func<Task> execution)
        {
            if (await SafeExecution.This(execution))
                return true;
            else
            {
                LastException = SafeExecution.LastException;
                return false;
            }
        }

        protected async Task<T> Try<T>(Func<Task<T>> execution)
        {
            var value = await SafeExecution.This(execution);
            if (value != null)
                return value;
            else
            {
                LastException = SafeExecution.LastException;
                return value;
            }
        }
    }
}