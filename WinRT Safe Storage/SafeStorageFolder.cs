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
using WinRT_Safe_Storage.Search;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage
{
    public class SafeStorageFolder : ISafeStorageItem
    {
        #region Constructors
        /// <summary>
        /// Create a new instance of <see cref="SafeStorageFolder"/>
        /// </summary>
        /// <param name="storageFolder">Storage folder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SafeStorageFolder(StorageFolder storageFolder)
        {
            UnsafeFolder = storageFolder ?? throw new ArgumentNullException(nameof(storageFolder));
        }
        #endregion

        #region Variables
        public readonly StorageFolder UnsafeFolder;
        #endregion

        #region Properties
        public FileAttributes Attributes => UnsafeFolder.Attributes;
        public DateTimeOffset DateCreated => UnsafeFolder.DateCreated;
        public string Name => UnsafeFolder.Name;
        public string Path => UnsafeFolder.Path;
        public string DisplayName => UnsafeFolder.DisplayName;
        public string DisplayType => UnsafeFolder.DisplayType;
        public string FolderRelativeId => UnsafeFolder.FolderRelativeId;
        public StorageItemContentProperties Properties => UnsafeFolder.Properties;
        public StorageProvider Provider => UnsafeFolder.Provider;
        #endregion

        #region Methods
        public static Task<SafeOperation<SafeStorageFolder>> TryGetFolderFromPathForUserAsync([In] User user, [In] string path) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFolder.GetFolderFromPathForUserAsync(user, path);

                return new SafeStorageFolder(value);
            });

        public static Task<SafeOperation<SafeStorageFolder>> TryGetFolderFromPathAsync([In] string path) =>
            SafeExecution.Try(async () =>
            {
                var value = await StorageFolder.GetFolderFromPathAsync(path);

                return new SafeStorageFolder(value);
            });

        public Task<SafeOperation<SafeStorageFile>> TryCreateFileAsync([In] string desiredName) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.CreateFileAsync(desiredName);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation<SafeStorageFile>> TryCreateFileAsync(string desiredName, CreationCollisionOption options) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.CreateFileAsync(desiredName, options);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation<SafeStorageFolder>> TryCreateFolderAsync([In] string desiredName) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.CreateFolderAsync(desiredName);

                return new SafeStorageFolder(value);
            });

        public Task<SafeOperation<SafeStorageFolder>> TryCreateFolderAsync([In] string desiredName, [In] CreationCollisionOption options) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.CreateFolderAsync(desiredName, options);

                return new SafeStorageFolder(value);
            });

        public Task<SafeOperation<SafeStorageFile>> TryGetFileAsync([In] string name) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.GetFileAsync(name);

                return new SafeStorageFile(value);
            });

        public Task<SafeOperation<SafeStorageFolder>> TryGetFolderAsync([In] string name) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.GetFolderAsync(name);

                return new SafeStorageFolder(value);
            });

        public Task<SafeOperation<ISafeStorageItem>> TryGetItemAsync([In] string name) =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.TryGetItemAsync(name);

                return StorageConverter.ToSafe(value);
            });

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFile>>> TryGetFilesAsync()
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetFilesAsync());

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<SafeStorageFile>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<SafeStorageFile>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFile>>> TryGetFilesAsync([In] CommonFileQuery query)
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetFilesAsync(query));

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<SafeStorageFile>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<SafeStorageFile>>.Error(operation.Exception);
        }


        public async Task<SafeOperation<IReadOnlyList<SafeStorageFile>>> TryGetFilesAsync([In] CommonFileQuery query, [In] uint startIndex, [In] uint maxItemsToRetrieve)
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetFilesAsync(query, startIndex, maxItemsToRetrieve));

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<SafeStorageFile>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<SafeStorageFile>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFolder>>> TryGetFoldersAsync()
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetFoldersAsync());

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<SafeStorageFolder>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<SafeStorageFolder>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFolder>>> TryGetFoldersAsync([In] CommonFolderQuery query)
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetFoldersAsync(query));

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<SafeStorageFolder>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<SafeStorageFolder>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFolder>>> TryGetFoldersAsync([In] CommonFolderQuery query, [In] uint startIndex, [In] uint maxItemsToRetrieve)
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetFoldersAsync(query, startIndex, maxItemsToRetrieve));

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<SafeStorageFolder>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<SafeStorageFolder>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<ISafeStorageItem>>> TryGetItemsAsync()
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetItemsAsync());

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<ISafeStorageItem>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<ISafeStorageItem>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<ISafeStorageItem>>> TryGetItemsAsync([In] uint startIndex, [In] uint maxItemsToRetrieve)
        {
            var operation = await SafeExecution.Try(async () => await UnsafeFolder.GetItemsAsync(startIndex, maxItemsToRetrieve));

            return operation.IsSuccess ?
                 SafeOperation<IReadOnlyList<ISafeStorageItem>>.Success(StorageConverter.ToSafe(operation.Value)) :
                 SafeOperation<IReadOnlyList<ISafeStorageItem>>.Error(operation.Exception);
        }

        public Task<SafeOperation> TryRenameAsync(string desiredName) =>
            SafeExecution.Try(async () =>
                await UnsafeFolder.RenameAsync(desiredName)
            );

        public Task<SafeOperation> TryRenameAsync(string desiredName, NameCollisionOption option) =>
            SafeExecution.Try(async () =>
                await UnsafeFolder.RenameAsync(desiredName, option)
            );

        public Task<SafeOperation> TryDeleteAsync() =>
            SafeExecution.Try(async () =>
                await UnsafeFolder.DeleteAsync()
            );

        public Task<SafeOperation> TryDeleteAsync(StorageDeleteOption option) =>
            SafeExecution.Try(async () =>
                await UnsafeFolder.DeleteAsync()
            );

        Task<SafeOperation<SafeBasicProperties>> ISafeStorageItem.TryGetBasicPropertiesAsync() =>
            SafeExecution.Try(async () =>
            {
                var unsafeBasicProperties = await UnsafeFolder.GetBasicPropertiesAsync();

                return new SafeBasicProperties(unsafeBasicProperties);
            });

        public IAsyncOperation<IndexedState> GetIndexedStateAsync()
            => UnsafeFolder.GetIndexedStateAsync();

        public SafeStorageFileQueryResult CreateFileQuery() =>
            new SafeStorageFileQueryResult(UnsafeFolder.CreateFileQuery());

        public SafeStorageFileQueryResult CreateFileQuery([In] CommonFileQuery query) =>
            new SafeStorageFileQueryResult(UnsafeFolder.CreateFileQuery());

        public SafeStorageFileQueryResult CreateFileQueryWithOptions([In] QueryOptions queryOptions) =>
            new SafeStorageFileQueryResult(UnsafeFolder.CreateFileQueryWithOptions(queryOptions));

        public SafeStorageFolderQueryResult CreateFolderQuery() =>
            new SafeStorageFolderQueryResult(UnsafeFolder.CreateFolderQuery());

        public SafeStorageFolderQueryResult CreateFolderQuery([In] CommonFolderQuery query) =>
            new SafeStorageFolderQueryResult(UnsafeFolder.CreateFolderQuery(query));

        public SafeStorageFolderQueryResult CreateFolderQueryWithOptions([In] QueryOptions queryOptions) =>
            new SafeStorageFolderQueryResult(UnsafeFolder.CreateFolderQueryWithOptions(queryOptions));

        public SafeStorageItemQueryResult CreateItemQuery() =>
            new SafeStorageItemQueryResult(UnsafeFolder.CreateItemQuery());

        public SafeStorageItemQueryResult CreateItemQueryWithOptions([In] QueryOptions queryOptions) =>
            new SafeStorageItemQueryResult(UnsafeFolder.CreateItemQueryWithOptions(queryOptions));

        public bool AreQueryOptionsSupported([In] QueryOptions queryOptions) =>
            UnsafeFolder.AreQueryOptionsSupported(queryOptions);

        public bool IsCommonFolderQuerySupported([In] CommonFolderQuery query) =>
            UnsafeFolder.IsCommonFolderQuerySupported(query);

        public bool IsCommonFileQuerySupported([In] CommonFileQuery query) =>
            UnsafeFolder.IsCommonFileQuerySupported(query);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode) =>
            UnsafeFolder.GetThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            UnsafeFolder.GetThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            UnsafeFolder.GetThumbnailAsync(mode, requestedSize, options);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode) =>
            UnsafeFolder.GetScaledImageAsThumbnailAsync(mode);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize) =>
            UnsafeFolder.GetScaledImageAsThumbnailAsync(mode, requestedSize);

        public IAsyncOperation<StorageItemThumbnail> GetScaledImageAsThumbnailAsync([In] ThumbnailMode mode, [In] uint requestedSize, [In] ThumbnailOptions options) =>
            UnsafeFolder.GetScaledImageAsThumbnailAsync(mode, requestedSize, options);

        public Task<SafeOperation<SafeStorageFolder>> TryGetParentAsync() =>
            SafeExecution.Try(async () =>
            {
                var value = await UnsafeFolder.GetParentAsync();

                return new SafeStorageFolder(value);
            });

        public StorageLibraryChangeTracker TryGetChangeTracker() =>
            UnsafeFolder.TryGetChangeTracker();

        public bool IsOfType(StorageItemTypes type) =>
            UnsafeFolder.IsOfType(type);

        public bool IsEqual(ISafeStorageItem item) =>
            DateCreated.Equals(item.DateCreated) &&
                   Name == item.Name &&
                   Path == item.Path;
        #endregion
    }
}