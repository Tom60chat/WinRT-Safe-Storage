using System;

namespace WinRT_Safe_Storage.Tools
{
    public class SafeOperation<T>
    {
        #region Constructors
        private SafeOperation()
        {
            state = SafeOperation.OperationState.None;
        }

        private SafeOperation(T value)
        {
            Value = value;
            state = SafeOperation.OperationState.Success;
        }

        private SafeOperation(Exception exception)
        {
            Exception = exception;
            state = SafeOperation.OperationState.Error;
        }
        #endregion

        #region Variables
        private SafeOperation.OperationState state;
        #endregion

        #region Properties
        public T Value { get; private set; }
        public Exception Exception { get; private set; }
        public bool IsSuccess => state == SafeOperation.OperationState.Success;
        #endregion

        #region Methods
        public static SafeOperation<T> Success(T value) => new SafeOperation<T>(value);
        public static SafeOperation<T> Error(Exception exception) => new SafeOperation<T>(exception);

        public SafeOperation<T> OnSuccess(Action<T> action)
        {
            if (state == SafeOperation.OperationState.Success)
                action(Value);

            return this;
        }

        public SafeOperation<T> OnError(Action<Exception> action)
        {
            if (state == SafeOperation.OperationState.Error)
                action(Exception);

            return this;
        }
        #endregion
    }

    public class SafeOperation
    {
        #region Enumerators
        public enum OperationState
        {
            None = -1,
            Success = 0,
            Error = 1,
        }
        #endregion

        #region Constructors
        private SafeOperation()
        {
            state = OperationState.Success;
        }

        private SafeOperation(Exception exception)
        {
            Exception = exception;
            state = OperationState.Error;
        }
        #endregion

        #region Variables
        private OperationState state;
        #endregion

        #region Properties
        public Exception Exception { get; private set; }
        public bool IsSuccess => state == OperationState.Success;
        #endregion

        #region Methods
        public static SafeOperation Success() => new SafeOperation();
        public static SafeOperation Error(Exception exception) => new SafeOperation(exception);

        public SafeOperation OnSuccess(Action action)
        {
            if (state == OperationState.Success)
                action();

            return this;
        }

        public SafeOperation OnError(Action<Exception> action)
        {
            if (state == OperationState.Error)
                action(Exception);

            return this;
        }
        #endregion
    }
}
