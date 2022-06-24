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
        /// Execute a synchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        public static SafeOperation Try(Action execution)
        {
            try
            {
                execution();
                return SafeOperation.Success();
            }
            catch (Exception ex)
            {
                return SafeOperation.Error(ex);
            }
        }

        /// <summary>
        /// Execute a synchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        /// <returns>What the methode return</returns>
        public static SafeOperation<T> Try<T>(Func<T> execution)
        {
            try
            {
                var value = (T)execution.DynamicInvoke();
                return SafeOperation<T>.Success(value);
            }
            catch (Exception ex)
            {
                return SafeOperation<T>.Error(ex);
            }
        }

        /// <summary>
        /// Execute a asynchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        public static async Task<SafeOperation> Try(Func<Task> execution)
        {
            try
            {
                await execution();
                return SafeOperation.Success();
            }
            catch (Exception ex)
            {
                return SafeOperation.Error(ex);
            }
        }

        /// <summary>
        /// Execute a asynchronously, and catch any exception that occur
        /// </summary>
        /// <param name="execution">The methode to execute</param>
        /// <param name="warnUser">Show the exeption message to the user</param>
        /// <returns>What the methode return</returns>
        public static async Task<SafeOperation<T>> Try<T>(Func<Task<T>> execution)
        {
            try
            {
                var value = await execution();
                return SafeOperation<T>.Success(value);
            }
            catch (Exception ex)
            {
                return SafeOperation<T>.Error(ex);
            }
        }
    }
}
