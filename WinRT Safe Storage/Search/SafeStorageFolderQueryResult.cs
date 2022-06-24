using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Data.Text;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Search;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage.Search
{
    public class SafeStorageFolderQueryResult
    {
        #region Constructors
        public SafeStorageFolderQueryResult(StorageFolderQueryResult storageFolderQueryResult)
        {
            UnsafeFolderQueryResult = storageFolderQueryResult;
            Folder = new SafeStorageFolder(storageFolderQueryResult.Folder);

            storageFolderQueryResult.ContentsChanged += (o, e) =>
                { if (ContentsChanged != null) ContentsChanged(o, e); };
            storageFolderQueryResult.OptionsChanged += (o, e) =>
                { if (OptionsChanged != null) OptionsChanged(o, e); };
        }
        #endregion

        #region Variables
        public readonly StorageFolderQueryResult UnsafeFolderQueryResult;

        public readonly SafeStorageFolder Folder;
        public event TypedEventHandler<IStorageQueryResultBase, object> ContentsChanged;
        public event TypedEventHandler<IStorageQueryResultBase, object> OptionsChanged;
        #endregion

        #region Methods
        public async Task<SafeOperation<IReadOnlyList<SafeStorageFolder>>> TryGetFoldersAsync([In] uint startIndex, [In] uint maxNumberOfItems)
        {
            var operation = await SafeExecution.Try(async () =>
                await UnsafeFolderQueryResult.GetFoldersAsync(startIndex, maxNumberOfItems));

            return operation.IsSuccess ?
                SafeOperation<IReadOnlyList<SafeStorageFolder>>.Success(StorageConverter.ToSafe(operation.Value)) :
                SafeOperation<IReadOnlyList<SafeStorageFolder>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFolder>>> TryGetFoldersAsync()
        {
            var operation = await SafeExecution.Try(async () =>
                await UnsafeFolderQueryResult.GetFoldersAsync());

            return operation.IsSuccess ?
                SafeOperation<IReadOnlyList<SafeStorageFolder>>.Success(StorageConverter.ToSafe(operation.Value)) :
                SafeOperation<IReadOnlyList<SafeStorageFolder>>.Error(operation.Exception);
        }

        public IAsyncOperation<uint> GetItemCountAsync() =>
            UnsafeFolderQueryResult.GetItemCountAsync();

        public IAsyncOperation<uint> FindStartIndexAsync([In][Variant] object value) =>
            UnsafeFolderQueryResult.FindStartIndexAsync(value);

        public QueryOptions GetCurrentQueryOptions() =>
            UnsafeFolderQueryResult.GetCurrentQueryOptions();

        public void ApplyNewQueryOptions([In] QueryOptions newQueryOptions) =>
            UnsafeFolderQueryResult.ApplyNewQueryOptions(newQueryOptions);
        #endregion
    }
}
