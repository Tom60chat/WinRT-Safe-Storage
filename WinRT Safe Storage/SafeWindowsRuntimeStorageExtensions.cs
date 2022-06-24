using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage
{
    public static class SafeWindowsRuntimeStorageExtensions
    {
        public static SafeOperation<SafeFileHandle> TryCreateSafeFileHandle(
                this SafeStorageFile windowsRuntimeFile, FileAccess access = FileAccess.ReadWrite, FileShare share = FileShare.Read, FileOptions options = FileOptions.None) =>
            SafeExecution.Try(() => windowsRuntimeFile.UnsafeFile.CreateSafeFileHandle(access, share, options));

        public static SafeOperation<SafeFileHandle> TryCreateSafeFileHandle(
                this SafeStorageFolder rootDirectory, string relativePath, FileMode mode) =>
            SafeExecution.Try(() => rootDirectory.UnsafeFolder.CreateSafeFileHandle(relativePath, mode));

        public static SafeOperation<SafeFileHandle> TryCreateSafeFileHandle(
                this SafeStorageFolder rootDirectory, string relativePath, FileMode mode, FileAccess access, FileShare share = FileShare.Read, FileOptions options = FileOptions.None) =>
            SafeExecution.Try(() => rootDirectory.UnsafeFolder.CreateSafeFileHandle(relativePath, mode, access, share, options));

        public static Task<SafeOperation<Stream>> TryOpenStreamForReadAsync(
                this SafeStorageFile windowsRuntimeFile) =>
            SafeExecution.Try(async () => await windowsRuntimeFile.UnsafeFile.OpenStreamForReadAsync());

        public static Task<SafeOperation<Stream>> TryOpenStreamForReadAsync(
                this SafeStorageFolder rootDirectory, string relativePath) =>
            SafeExecution.Try(async () => await rootDirectory.UnsafeFolder.OpenStreamForReadAsync(relativePath));

        public static Task<SafeOperation<Stream>> TryOpenStreamForWriteAsync(
                this SafeStorageFile windowsRuntimeFile) =>
            SafeExecution.Try(async () => await windowsRuntimeFile.UnsafeFile.OpenStreamForWriteAsync());

        public static Task<SafeOperation<Stream>> TryOpenStreamForWriteAsync(
                this SafeStorageFolder rootDirectory, string relativePath, CreationCollisionOption creationCollisionOption) =>
            SafeExecution.Try(async () => await rootDirectory.UnsafeFolder.OpenStreamForWriteAsync(relativePath, creationCollisionOption));
    }
}
