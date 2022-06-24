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
    public class SafeStorageFileQueryResult
    {
        #region Constructors
        public SafeStorageFileQueryResult(StorageFileQueryResult storageFileQueryResult)
        {
            UnsafeFileQueryResult = storageFileQueryResult;
            Folder = new SafeStorageFolder(storageFileQueryResult.Folder);

            storageFileQueryResult.ContentsChanged += (o, e) =>
                { if (ContentsChanged != null) ContentsChanged(o, e); };
            storageFileQueryResult.OptionsChanged += (o, e) =>
                { if (OptionsChanged != null) OptionsChanged(o, e); };
        }
        #endregion

        #region Variables
        public readonly StorageFileQueryResult UnsafeFileQueryResult;

        public readonly SafeStorageFolder Folder;
        public event TypedEventHandler<IStorageQueryResultBase, object> ContentsChanged;
        public event TypedEventHandler<IStorageQueryResultBase, object> OptionsChanged;
        #endregion

        #region Methods
        public async Task<SafeOperation<IReadOnlyList<SafeStorageFile>>> TryGetFilesAsync([In] uint startIndex, [In] uint maxNumberOfItems)
        {
            var operation = await SafeExecution.Try(async () =>
                await UnsafeFileQueryResult.GetFilesAsync(startIndex, maxNumberOfItems));

            return operation.IsSuccess ?
                SafeOperation<IReadOnlyList<SafeStorageFile>>.Success(StorageConverter.ToSafe(operation.Value)) :
                SafeOperation<IReadOnlyList<SafeStorageFile>>.Error(operation.Exception);
        }

        public async Task<SafeOperation<IReadOnlyList<SafeStorageFile>>> TryGetFilesAsync()
        {
            var operation = await SafeExecution.Try(async () =>
                await UnsafeFileQueryResult.GetFilesAsync());

            return operation.IsSuccess ?
                SafeOperation<IReadOnlyList<SafeStorageFile>>.Success(StorageConverter.ToSafe(operation.Value)) :
                SafeOperation<IReadOnlyList<SafeStorageFile>>.Error(operation.Exception);
        }

        public IAsyncOperation<uint> GetItemCountAsync() =>
            UnsafeFileQueryResult.GetItemCountAsync();

        public IAsyncOperation<uint> FindStartIndexAsync([In][Variant] object value) =>
            UnsafeFileQueryResult.FindStartIndexAsync(value);

        public QueryOptions GetCurrentQueryOptions() =>
            UnsafeFileQueryResult.GetCurrentQueryOptions();

        public void ApplyNewQueryOptions([In] QueryOptions newQueryOptions) =>
            UnsafeFileQueryResult.ApplyNewQueryOptions(newQueryOptions);

        public IDictionary<string, IReadOnlyList<TextSegment>> GetMatchingPropertiesWithRanges([In] SafeStorageFile file) =>
            UnsafeFileQueryResult.GetMatchingPropertiesWithRanges(file.UnsafeFile);
        #endregion
    }
}