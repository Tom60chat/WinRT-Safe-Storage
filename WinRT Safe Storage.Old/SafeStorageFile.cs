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
    public class SafeStorageFile : Safe, ISafeStorageItem
    {
        #region Constructors
        public SafeStorageFile(StorageFile storageFile)
        {
            this.storageFile = storageFile ?? throw new ArgumentNullException(nameof(storageFile));
        }
        #endregion

        #region Variables
        private readonly StorageFile storageFile;
        #endregion

        #region Properties
        public string ContentType => storageFile.ContentType;
        public string FileType => storageFile.FileType;
        public bool IsAvailable => storageFile.IsAvailable;
        public FileAttributes Attributes => storageFile.Attributes;
        public DateTimeOffset DateCreated => storageFile.DateCreated;
        public string Name => storageFile.Name;
        public string Path => storageFile.Path;
        public string DisplayName => storageFile.DisplayName;
        public string DisplayType => storageFile.DisplayType;
        public string FolderRelativeId => storageFile.FolderRelativeId;
        public StorageItemContentProperties Properties => storageFile.Properties;
        public StorageProvider Provider => storageFile.Provider;
        #endregion

        #region Methods
        public static Task<SafeStorageFile> TryGetFileFromPathForUserAsync([In] User user, [In] string path) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFile.GetFileFromPathForUserAsync(user, path);

                return new SafeStorageFile(value);
            });

        public static Task<SafeStorageFile> TryGetFileFromPathAsync([In] string path) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFile.GetFileFromPathAsync(path);

                return new SafeStorageFile(value);
            });

        public static Task<SafeStorageFile> TryGetFileFromApplicationUriAsync([In] Uri uri) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFile.GetFileFromApplicationUriAsync(uri);

                return new SafeStorageFile(value);
            });

        public static Task<SafeStorageFile> TryCreateStreamedFileAsync([In] string displayNameWithExtension, [In] StreamedFileDataRequestedHandler dataRequested, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFile.CreateStreamedFileAsync(displayNameWithExtension, dataRequested, thumbnail);

                return new SafeStorageFile(value);
            });

        public static Task<SafeStorageFile> TryReplaceWithStreamedFileAsync([In] IStorageFile fileToReplace, [In] StreamedFileDataRequestedHandler dataRequested, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFile.ReplaceWithStreamedFileAsync(fileToReplace, dataRequested, thumbnail);

                return new SafeStorageFile(value);
            });

        public static Task<SafeStorageFile> TryCreateStreamedFileFromUriAsync([In] string displayNameWithExtension, [In] Uri uri, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFile.CreateStreamedFileFromUriAsync(displayNameWithExtension, uri, thumbnail);

                return new SafeStorageFile(value);
            });

        public static Task<SafeStorageFile> TryReplaceWithStreamedFileFromUriAsync([In] IStorageFile fileToReplace, [In] Uri uri, [In] IRandomAccessStreamReference thumbnail) =>
            SafeExecution.This(async () => {
                var value = await StorageFile.ReplaceWithStreamedFileFromUriAsync(fileToReplace, uri, thumbnail);

                return new SafeStorageFile(value);
            });

        public Task<IRandomAccessStream> TryOpenAsync([In] FileAccessMode accessMode) =>
            Try(async () =>
                await storageFile.OpenAsync(accessMode)
            );

        public Task<IRandomAccessStream> TryOpenAsync([In] FileAccessMode accessMode, [In] StorageOpenOptions options) =>
            Try(async () =>
                await storageFile.OpenAsync(accessMode, options)
            );

        public Task<StorageStreamTransaction> TryOpenTransactedWriteAsync() =>
            Try(async () =>
                await storageFile.OpenTransactedWriteAsync()
            );

        public Task<SafeStorageFile> TryCopyAsync([In] IStorageFolder destinationFolder) =>
            Try(async () => {
                var value = await storageFile.CopyAsync(destinationFolder);

                return new SafeStorageFile(value);
            });

        public Task<SafeStorageFile> TryCopyAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName) =>
            Try(async () => {
                var value = await storageFile.CopyAsync(destinationFolder, desiredNewName);

                return new SafeStorageFile(value);
            });

        public Task<SafeStorageFile> TryCopyAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName, [In] NameCollisionOption option) =>
            Try(async () => {
                var value = await storageFile.CopyAsync(destinationFolder, desiredNewName, option);

                return new SafeStorageFile(value);
            });

        public Task<bool> TryCopyAndReplaceAsync([In] IStorageFile fileToReplace) =>
            Try(async () =>
                await storageFile.CopyAndReplaceAsync(fileToReplace)
            );

        public Task<bool> TryMoveAsync([In] IStorageFolder destinationFolder) =>
            Try(async () => 
                await storageFile.MoveAsync(destinationFolder)
            );

        public Task<bool> TryMoveAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName) =>
            Try(async () => 
                await storageFile.MoveAsync(destinationFolder, desiredNewName)
            );

        public Task<bool> TryMoveAsync([In] IStorageFolder destinationFolder, [In] string desiredNewName, [In] NameCollisionOption option) =>
            Try(async () => 
                await storageFile.MoveAsync(destinationFolder, desiredNewName, option)
            );

        public Task<bool> TryMoveAndReplaceAsync([In] IStorageFile fileToReplace) =>
            Try(async () =>
                await storageFile.MoveAndReplaceAsync(fileToReplace)
            );

        public Task<bool> TryRenameAsync(string desiredName) =>
            Try(async () =>
                await storageFile.RenameAsync(desiredName)
            );

        public Task<bool> TryRenameAsync(string desiredName, NameCollisionOption option) =>
            Try(async () =>
                await storageFile.RenameAsync(desiredName, option)
            );

        public Task<bool> TryDeleteAsync() =>
            Try(async () =>
                await storageFile.DeleteAsync()
            );

        public Task<bool> TryDeleteAsync(StorageDeleteOption option) =>
            Try(async () =>
                await storageFile.DeleteAsync(option)
            );

        public Task<SafeBasicProperties> TryGetBasicPropertiesAsync() =>
            Try(async () =>
            {
                var value = await storageFile.GetBasicPropertiesAsync();

                return new SafeBasicProperties(value);
            });

        public bool IsOfType(StorageItemTypes type) =>
            storageFile.IsOfType(type);

        public Task<IRandomAccessStreamWithContentType> TryOpenReadAsync() =>
            Try(async () =>
                await storageFile.OpenReadAsync()
            );

        public Task<IInputStream> TryOpenSequentialReadAsync() =>
            Try(async () =>
                await storageFile.OpenSequentialReadAsync()
            );

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode) =>
            storageFile.GetThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            storageFile.GetThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            storageFile.GetThumbnailAsync(mode, requestedSize, options);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode) =>
            storageFile.GetScaledImageAsThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            storageFile.GetScaledImageAsThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            storageFile.GetScaledImageAsThumbnailAsync(mode, requestedSize, options);

        public Task<SafeStorageFolder> TryGetParentAsync() =>
            Try(async () =>
            {
                var value = await storageFile.GetParentAsync();

                return new SafeStorageFolder(value);
            });

        public bool IsEqual(ISafeStorageItem item) =>
            DateCreated.Equals(item.DateCreated) &&
                   Name == item.Name &&
                   Path == item.Path;
        #endregion
    }
}