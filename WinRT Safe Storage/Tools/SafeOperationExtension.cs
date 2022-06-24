using System;
using System.Threading.Tasks;
using WinRT_Safe_Storage.Tools;

// Don't put in namespace tools for better user experience with IntelliSense
namespace WinRT_Safe_Storage
{
    public static class SafeOperationExtension
    {
        public static async Task<SafeOperation<T>> OnSuccess<T>(this Task<SafeOperation<T>> asyncOperation, Action<T> action)
        {
            var operation = await asyncOperation;

            return operation.OnSuccess(action);
        }

        public static async Task<SafeOperation<T>> OnSuccess<T>(this Task<SafeOperation<T>> asyncOperation, Func<T, Task> action)
        {
            var operation = await asyncOperation;

            return await operation.OnSuccess(action);
        }

        public static async Task<SafeOperation> OnSuccess(this Task<SafeOperation> asyncOperation, Action action)
        {
            var operation = await asyncOperation;

            return operation.OnSuccess(action);
        }

        public static async Task<SafeOperation> OnSuccess(this Task<SafeOperation> asyncOperation, Func<Task> action)
        {
            var operation = await asyncOperation;

            return await operation.OnSuccess(action);
        }

        public static async Task<SafeOperation<T>> OnError<T>(this Task<SafeOperation<T>> asyncOperation, Action<Exception> action)
        {
            var operation = await asyncOperation;

            return operation.OnError(action);
        }

        public static async Task<SafeOperation<T>> OnError<T>(this Task<SafeOperation<T>> asyncOperation, Func<Exception, Task> action)
        {
            var operation = await asyncOperation;

            return await operation.OnError(action);
        }

        public static async Task<SafeOperation> OnError(this Task<SafeOperation> asyncOperation, Action<Exception> action)
        {
            var operation = await asyncOperation;

            return operation.OnError(action);
        }

        public static async Task<SafeOperation> OnError(this Task<SafeOperation> asyncOperation, Func<Exception, Task> action)
        {
            var operation = await asyncOperation;

            return await operation.OnError(action);
        }
    }
}
