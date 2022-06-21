using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.Search;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage.Search
{
    public class SafeStorageItemQueryResult
    {
        #region Constructors
        public SafeStorageItemQueryResult(StorageItemQueryResult storageItemQueryResult)
        {
            this.storageItemQueryResult = storageItemQueryResult;
            Folder = new SafeStorageFolder(storageItemQueryResult.Folder);

            storageItemQueryResult.ContentsChanged += (o, e) =>
                { if (ContentsChanged != null) ContentsChanged(o, e); };
            storageItemQueryResult.OptionsChanged += (o, e) =>
                { if (OptionsChanged != null) OptionsChanged(o, e); };
        }
        #endregion

        #region Variables
        public readonly StorageItemQueryResult storageItemQueryResult;

        public SafeStorageFolder Folder { get; private set; }
        public event TypedEventHandler<IStorageQueryResultBase, object> ContentsChanged;
        public event TypedEventHandler<IStorageQueryResultBase, object> OptionsChanged;
        #endregion

        #region Methods
        public async Task<SafeOperation<IReadOnlyList<ISafeStorageItem>>> TryGetItemsAsync([In] uint startIndex, [In] uint maxNumberOfItems)
        {
            var operation = await SafeExecution.Try(async () =>
                await storageItemQueryResult.GetItemsAsync(startIndex, maxNumberOfItems));

            return operation.IsSuccess ?
                SafeOperation<IReadOnlyList<ISafeStorageItem>>.Success(UnsafeConverter.ToSafe(operation.Value)) :
                SafeOperation<IReadOnlyList<ISafeStorageItem>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<ISafeStorageItem>>> TryGetItemsAsync()
        {
            var operation = await SafeExecution.Try(async () =>
                await storageItemQueryResult.GetItemsAsync());

            return operation.IsSuccess ?
                SafeOperation<IReadOnlyList<ISafeStorageItem>>.Success(UnsafeConverter.ToSafe(operation.Value)) :
                SafeOperation<IReadOnlyList<ISafeStorageItem>>.Error(operation.Exception);
        }

        public IAsyncOperation<uint> GetItemCountAsync() =>
            storageItemQueryResult.GetItemCountAsync();

        public IAsyncOperation<uint> FindStartIndexAsync([In][Variant] object value) =>
            storageItemQueryResult.FindStartIndexAsync(value);

        public QueryOptions GetCurrentQueryOptions() =>
            storageItemQueryResult.GetCurrentQueryOptions();

        public void ApplyNewQueryOptions([In] QueryOptions newQueryOptions) =>
            storageItemQueryResult.ApplyNewQueryOptions(newQueryOptions);
        #endregion
    }
}
