using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.System;
using WinRT_Safe_Storage.FileProperties;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage
{
    public class SafeStorageFile : ISafeStorageItem
    {
        #region Constructors
        public SafeStorageFile(StorageFile storageFile)
        {
            UnsafeFile = storageFile ?? throw new ArgumentNullException(nameof(storageFile));
        }
        #endregion

        #region Variables
        public readonly StorageFile UnsafeFile;
        #endregion

        #region Properties
        public string ContentType => UnsafeFile.ContentType;
        public string FileType => UnsafeFile.FileType;
        public bool IsAvailable => UnsafeFile.IsAvailable;
        public FileAttributes Attributes => UnsafeFile.Attributes;
        public DateTimeOffset DateCreated => UnsafeFile.DateCreated;
        public string Name => UnsafeFile.Name;
        public string Path => UnsafeFile.Path;
        public string DisplayName => UnsafeFile.DisplayName;
        public string DisplayType => UnsafeFile.DisplayType;
        public string FolderRelativeId => UnsafeFile.FolderRelativeId;
        public StorageItemContentProperties Properties => UnsafeFile.Properties;
        public StorageProvider Provider => UnsafeFile.Provider;
        #endregion

        #region Methods
        public static Task<SafeOperation<SafeStorageFile>> TryGetFileFromPathForUserAsync([In] User user, [In] string path) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFile.GetFileFromPathForUserAsync(user, path);

                return new SafeStorageFile(value);
            });

        public static Task<SafeOperation<SafeStorageFile>> TryGetFileFromPathAsync([In] string path) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFile.GetFileFromPathAsync(path);

                return new SafeStorageFile(value);
            });

        public static Task<SafeOperation<SafeStorageFile>> TryGetFileFromApplicationUriAsync([In] Uri uri) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFile.GetFileFromApplicationUriAsync(uri);

                return new SafeStorageFile(value);
            });

        public static Task<SafeOperation<SafeStorageFile>> TryCreateStreamedFileAsync([In] string displayNameWithExtension, [In] StreamedFileDataRequestedHandler dataRequested, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFile.CreateStreamedFileAsync(displayNameWithExtension, dataRequested, thumbnail);

                return new SafeStorageFile(value);
            });

        public static Task<SafeOperation<SafeStorageFile>> TryReplaceWithStreamedFileAsync([In] IStorageFile fileToReplace, [In] StreamedFileDataRequestedHandler dataRequested, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFile.ReplaceWithStreamedFileAsync(fileToReplace, dataRequested, thumbnail);

                return new SafeStorageFile(value);
            });

        public static Task<SafeOperation<SafeStorageFile>> TryCreateStreamedFileFromUriAsync([In] string displayNameWithExtension, [In] Uri uri, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFile.CreateStreamedFileFromUriAsync(displayNameWithExtension, uri, thumbnail);

                return new SafeStorageFile(value);
            });

        public static Task<SafeOperation<SafeStorageFile>> TryReplaceWithStreamedFileFromUriAsync([In] IStorageFile fileToReplace, [In] Uri uri, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.Try(async () => {
                var value = await StorageFile.ReplaceWithStreamedFileFromUriAsync(fileToReplace, uri, thumbnail);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation<IRandomAccessStream>> TryOpenAsync([In] FileAccessMode accessMode) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.OpenAsync(accessMode)
            );

        public Task<SafeOperation<IRandomAccessStream>> TryOpenAsync([In] FileAccessMode accessMode, [In] StorageOpenOptions options) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.OpenAsync(accessMode, options)
            );

        public Task<SafeOperation<StorageStreamTransaction>> TryOpenTransactedWriteAsync() =>
            SafeExecution.Try(async () =>
                await UnsafeFile.OpenTransactedWriteAsync()
            );

        public Task<SafeOperation<SafeStorageFile>> TryCopyAsync([In] IStorageFolder destinationFolder) =>
            SafeExecution.Try(async () => {
                var value = await UnsafeFile.CopyAsync(destinationFolder);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation<SafeStorageFile>> TryCopyAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName) =>
            SafeExecution.Try(async () => {
                var value = await UnsafeFile.CopyAsync(destinationFolder, desiredNewName);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation<SafeStorageFile>> TryCopyAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName, [In] NameCollisionOption option) =>
            SafeExecution.Try(async () => {
                var value = await UnsafeFile.CopyAsync(destinationFolder, desiredNewName, option);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation> TryCopyAndReplaceAsync([In] IStorageFile fileToReplace) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.CopyAndReplaceAsync(fileToReplace)
            );

        public Task<SafeOperation> TryMoveAsync([In] IStorageFolder destinationFolder) =>
            SafeExecution.Try(async () => 
                await UnsafeFile.MoveAsync(destinationFolder)
            );

        public Task<SafeOperation> TryMoveAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName) =>
            SafeExecution.Try(async () => 
                await UnsafeFile.MoveAsync(destinationFolder, desiredNewName)
            );

        public Task<SafeOperation> TryMoveAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName, [In] NameCollisionOption option) =>
            SafeExecution.Try(async () => 
                await UnsafeFile.MoveAsync(destinationFolder, desiredNewName, option)
            );

        public Task<SafeOperation> TryMoveAndReplaceAsync([In] IStorageFile fileToReplace) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.MoveAndReplaceAsync(fileToReplace)
            );

        public Task<SafeOperation> TryRenameAsync(string desiredName) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.RenameAsync(desiredName)
            );

        public Task<SafeOperation> TryRenameAsync(string desiredName, NameCollisionOption option) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.RenameAsync(desiredName, option)
            );

        public Task<SafeOperation> TryDeleteAsync() =>
            SafeExecution.Try(async () =>
                await UnsafeFile.DeleteAsync()
            );

        public Task<SafeOperation> TryDeleteAsync(StorageDeleteOption option) =>
            SafeExecution.Try(async () =>
                await UnsafeFile.DeleteAsync(option)
            );

        public Task<SafeOperation<SafeBasicProperties>> TryGetBasicPropertiesAsync() =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFile.GetBasicPropertiesAsync();

                return new SafeBasicProperties(value);
            });

        public bool IsOfType(StorageItemTypes type) =>
            UnsafeFile.IsOfType(type);

        public Task<SafeOperation<IRandomAccessStreamWithContentType>> TryOpenReadAsync() =>
            SafeExecution.Try(async () =>
                await UnsafeFile.OpenReadAsync()
            );

        public Task<SafeOperation<IInputStream>> TryOpenSequentialReadAsync() =>
            SafeExecution.Try(async () =>
                await UnsafeFile.OpenSequentialReadAsync()
            );

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode) =>
            UnsafeFile.GetThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            UnsafeFile.GetThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            UnsafeFile.GetThumbnailAsync(mode, requestedSize, options);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode) =>
            UnsafeFile.GetScaledImageAsThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            UnsafeFile.GetScaledImageAsThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            UnsafeFile.GetScaledImageAsThumbnailAsync(mode, requestedSize, options);

        public Task<SafeOperation<SafeStorageFolder>> TryGetParentAsync() =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFile.GetParentAsync();

                return new SafeStorageFolder(value);
            });

        public bool IsEqual(ISafeStorageItem item) =>
            DateCreated.Equals(item.DateCreated) &&
                   Name == item.Name &&
                   Path == item.Path;
        #endregion
    }
}