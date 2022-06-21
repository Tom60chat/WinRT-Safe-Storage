using System;
using System.Threading.Tasks;
using Windows.Foundation;

namespace WinRT_Safe_Storage.Tools
{
    internal static class SafeExecution
    {
        public static Exception LastException { get; private set; }

        /// <summary>
        /// Execute a synchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        public static bool This(Action execution)
        {
            try
            {
                execution();
                return true;
            }
            catch (Exception ex)
            {
                LastException = ex;
                return false;
            }
        }

        /// <summary>
        /// Execute a synchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        /// <returns>What the methode return</returns>
        public static T This<T>(Action<T> execution)
        {
            try
            {
                return (T)execution.DynamicInvoke();
            }
            catch (Exception ex)
            {
                LastException = ex;
                return default;
            }
        }

        /// <summary>
        /// Execute a asynchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        public static async Task<bool> This(Func<Task> execution)
        {
            try
            {
                await execution();
                return true;
            }
            catch (Exception ex)
            {
                LastException = ex;
                return false;
            }
        }

        /// <summary>
        /// Execute a asynchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        /// <returns>What the methode return</returns>
        public static Task<T> This<T>(Func<Task<T>> execution)
        {
            try
            {
                return execution();
            }
            catch (Exception ex)
            {
                LastException = ex;
                return Task.FromResult((T)default);
            }
        }

        /// <summary>
        /// Execute a asynchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        /// <returns>What the methode return</returns>
        public static IAsyncOperation<TResult> This<TResult>(Func<IAsyncOperation<TResult>> execution)
        {
            try
            {
                return execution();
            }
            catch (Exception ex)
            {
                LastException = ex;
                return default;
            }
        }

        /*/// <summary>
        /// Execute a asynchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        public static async IAsyncOperation<bool> This(Func<IAsyncAction> execution)
        {
            return Windows.Foundation.
            try
            {
                await execution();
                return true;
            }
            catch (Exception ex)
            {
                LastException = ex;
                return false;
            }
        }*/
    }
}
