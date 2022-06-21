using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.System;
using WinRT_Safe_Storage.FileProperties;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage
{
    public class SafeStorageFolder : Safe, ISafeStorageItem
    {
        #region Constructors
        /// <summary>
        /// Create a new instance of <see cref="SafeStorageFolder"/>
        /// </summary>
        /// <param name="storageFolder">Storage folder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SafeStorageFolder(StorageFolder storageFolder)
        {
            this.storageFolder = storageFolder ?? throw new ArgumentNullException(nameof(storageFolder));
        }
        #endregion

        #region Variables
        private readonly StorageFolder storageFolder;
        #endregion

        #region Properties
        public FileAttributes Attributes => storageFolder.Attributes;
        public DateTimeOffset DateCreated => storageFolder.DateCreated;
        public string Name => storageFolder.Name;
        public string Path => storageFolder.Path;
        public string DisplayName => storageFolder.DisplayName;
        public string DisplayType => storageFolder.DisplayType;
        public string FolderRelativeId => storageFolder.FolderRelativeId;
        public StorageItemContentProperties Properties => storageFolder.Properties;
        public StorageProvider Provider => storageFolder.Provider;
        #endregion

        #region Methods
        public static Task<SafeStorageFolder> TryGetFolderFromPathForUserAsync([In] User user, [In] string path) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFolder.GetFolderFromPathForUserAsync(user, path);

                return new SafeStorageFolder(value);
            });

        public static Task<SafeStorageFolder> TryGetFolderFromPathAsync([In] string path) =>
            SafeExecution.This(async () =>
            {
                var value = await StorageFolder.GetFolderFromPathAsync(path);

                return new SafeStorageFolder(value);
            });

        public Task<SafeStorageFile> TryCreateFileAsync([In] string desiredName) =>
            Try(async () =>
            {
                var value = await storageFolder.CreateFileAsync(desiredName);

                return new SafeStorageFile(value);
            });

        public Task<SafeStorageFile> TryCreateFileAsync(string desiredName, CreationCollisionOption options) =>
            Try(async () =>
            {
                var value = await storageFolder.CreateFileAsync(desiredName, options);

                return new SafeStorageFile(value);
            });

        public Task<SafeStorageFolder> TryCreateFolderAsync([In] string desiredName) =>
            Try(async () =>
            {
                var value = await storageFolder.CreateFolderAsync(desiredName);

                return new SafeStorageFolder(value);
            });

        public Task<SafeStorageFolder> TryCreateFolderAsync([In] string desiredName, [In] CreationCollisionOption options) =>
            Try(async () =>
            {
                var value = await storageFolder.CreateFolderAsync(desiredName, options);

                return new SafeStorageFolder(value);
            });

        public Task<SafeStorageFile> TryGetFileAsync([In] string name) =>
            Try(async () =>
            {
                var value = await storageFolder.GetFileAsync(name);

                return new SafeStorageFile(value);
            });

        public Task<SafeStorageFolder> TryGetFolderAsync([In] string name) =>
            Try(async () =>
            {
                var value = await storageFolder.GetFolderAsync(name);

                return new SafeStorageFolder(value);
            });

        public Task<ISafeStorageItem> TryGetItemAsync([In] string name) =>
            Try(async () =>
            {
                var value = await storageFolder.TryGetItemAsync(name);

                return UnsafeConverter.ToSafe(value);
            });

        public async Task<IReadOnlyList<SafeStorageFile>> TryGetFilesAsync()
        {
            var files = await Try(async () =>
                            await storageFolder.GetFilesAsync());

            return UnsafeConverter.ToSafe(files);
        }

        public async Task<IReadOnlyList<SafeStorageFile>> TryGetFilesAsync([In] CommonFileQuery query)
        {
            var files = await Try(async () =>
                            await storageFolder.GetFilesAsync(query));

            return UnsafeConverter.ToSafe(files);
        }


        public async Task<IReadOnlyList<SafeStorageFile>> TryGetFilesAsync([In] CommonFileQuery query, [In] uint startIndex, [In] uint maxItemsToRetrieve)
        {
            var files = await Try(async () =>
                            await storageFolder.GetFilesAsync(query, startIndex, maxItemsToRetrieve));

            return UnsafeConverter.ToSafe(files);
        }

        public async Task<IReadOnlyList<SafeStorageFolder>> TryGetFoldersAsync()
        {
            var folders = await Try(async () =>
                            await storageFolder.GetFoldersAsync());

            return UnsafeConverter.ToSafe(folders);
        }

        public async Task<IReadOnlyList<SafeStorageFolder>> TryGetFoldersAsync([In] CommonFolderQuery query)
        {
            var folders = await Try(async () =>
                            await storageFolder.GetFoldersAsync(query));

            return UnsafeConverter.ToSafe(folders);
        }

        public async Task<IReadOnlyList<SafeStorageFolder>> TryGetFoldersAsync([In] CommonFolderQuery query, [In] uint startIndex, [In] uint maxItemsToRetrieve)
        {
            var folders = await Try(async () =>
                            await storageFolder.GetFoldersAsync(query, startIndex, maxItemsToRetrieve));

            return UnsafeConverter.ToSafe(folders);
        }

        public async Task<IReadOnlyList<ISafeStorageItem>> TryGetItemsAsync()
        {
            var items = await Try(async () =>
                            await storageFolder.GetItemsAsync());

            return UnsafeConverter.ToSafe(items);
        }

        public async Task<IReadOnlyList<ISafeStorageItem>> TryGetItemsAsync([In] uint startIndex, [In] uint maxItemsToRetrieve)
        {
            var items = await Try(async () =>
                            await storageFolder.GetItemsAsync(startIndex, maxItemsToRetrieve));

            return UnsafeConverter.ToSafe(items);
        }

        public Task<bool> TryRenameAsync(string desiredName) =>
            Try(async () =>
                await storageFolder.RenameAsync(desiredName)
            );

        public Task<bool> TryRenameAsync(string desiredName, NameCollisionOption option) =>
            Try(async () =>
                await storageFolder.RenameAsync(desiredName, option)
            );

        public Task<bool> TryDeleteAsync() =>
            Try(async () =>
                await storageFolder.DeleteAsync()
            );

        public Task<bool> TryDeleteAsync(StorageDeleteOption option) =>
            Try(async () =>
                await storageFolder.DeleteAsync()
            );

        Task<SafeBasicProperties> ISafeStorageItem.TryGetBasicPropertiesAsync() =>
            Try(async () =>
            {
                var unsafeBasicProperties = await storageFolder.GetBasicPropertiesAsync();

                return new SafeBasicProperties(unsafeBasicProperties);
            });

        public IAsyncOperation<IndexedState> GetIndexedStateAsync()
            => storageFolder.GetIndexedStateAsync();

        public StorageFileQueryResult CreateFileQuery() =>
            storageFolder.CreateFileQuery();

        public StorageFileQueryResult CreateFileQuery([In] CommonFileQuery query) =>
            storageFolder.CreateFileQuery();

        public StorageFileQueryResult CreateFileQueryWithOptions([In] QueryOptions queryOptions) =>
            storageFolder.CreateFileQueryWithOptions(queryOptions);

        public StorageFolderQueryResult CreateFolderQuery() =>
            storageFolder.CreateFolderQuery();

        public StorageFolderQueryResult CreateFolderQuery([In] CommonFolderQuery query) =>
            storageFolder.CreateFolderQuery(query);

        public StorageFolderQueryResult CreateFolderQueryWithOptions([In] QueryOptions queryOptions) =>
            storageFolder.CreateFolderQueryWithOptions(queryOptions);

        public StorageItemQueryResult CreateItemQuery() =>
            storageFolder.CreateItemQuery();

        public StorageItemQueryResult CreateItemQueryWithOptions([In] QueryOptions queryOptions) =>
            storageFolder.CreateItemQueryWithOptions(queryOptions);

        public bool AreQueryOptionsSupported([In] QueryOptions queryOptions) =>
            storageFolder.AreQueryOptionsSupported(queryOptions);

        public bool IsCommonFolderQuerySupported([In] CommonFolderQuery query) =>
            storageFolder.IsCommonFolderQuerySupported(query);

        public bool IsCommonFileQuerySupported([In] CommonFileQuery query) =>
            storageFolder.IsCommonFileQuerySupported(query);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode) =>
            storageFolder.GetThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            storageFolder.GetThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            storageFolder.GetThumbnailAsync(mode, requestedSize, options);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode) =>
            storageFolder.GetScaledImageAsThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            storageFolder.GetScaledImageAsThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            storageFolder.GetScaledImageAsThumbnailAsync(mode, requestedSize, options);

        public Task<SafeStorageFolder> TryGetParentAsync() =>
            Try(async () =>
            {
                var value = await storageFolder.GetParentAsync();

                return new SafeStorageFolder(value);
            });

        public StorageLibraryChangeTracker TryGetChangeTracker() =>
            storageFolder.TryGetChangeTracker();

        public bool IsOfType(StorageItemTypes type) =>
            storageFolder.IsOfType(type);

        public bool IsEqual(ISafeStorageItem item) =>
            DateCreated.Equals(item.DateCreated) &&
                   Name == item.Name &&
                   Path == item.Path;
        #endregion
    }
}