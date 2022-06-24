using System.Collections.Generic;
using Windows.Storage;

namespace WinRT_Safe_Storage
{
    public class StorageConverter
    {
        #region Safe
        public static ISafeStorageItem ToSafe(IStorageItem unsafeItem)
        {
            if (unsafeItem is StorageFile file)
                return new SafeStorageFile(file);
            else if (unsafeItem is StorageFolder folder)
                return new SafeStorageFolder(folder);
            else
                return null;
        }

        public static IReadOnlyList<ISafeStorageItem> ToSafe(IEnumerable<IStorageItem> unsafeItems)
        {
            var safeItems = new List<ISafeStorageItem>();

            if (unsafeItems != null)
            {
                foreach (var unsafeItem in unsafeItems)
                    safeItems.Add(ToSafe(unsafeItem));
            }

            return safeItems.AsReadOnly();
        }

        public static IReadOnlyList<SafeStorageFolder> ToSafe(IEnumerable<StorageFolder> unsafeFolders)
        {
            var safeFolders = new List<SafeStorageFolder>();

            if (unsafeFolders != null)
            {
                foreach (var unsafeFolder in unsafeFolders)
                    safeFolders.Add(new SafeStorageFolder(unsafeFolder));
            }

            return safeFolders.AsReadOnly();
        }

        public static IReadOnlyList<SafeStorageFile> ToSafe(IEnumerable<StorageFile> unsafeFiles)
        {
            var safeFiles = new List<SafeStorageFile>();

            if (unsafeFiles != null)
            {
                foreach (var unsafeFile in unsafeFiles)
                    safeFiles.Add(new SafeStorageFile(unsafeFile));
            }

            return safeFiles.AsReadOnly();
        }

        public static IReadOnlyList<ISafeStorageItem> ToSafe(IReadOnlyList<IStorageItem> unsafeItems) =>
            ToSafe((IEnumerable<IStorageItem>)unsafeItems);

        public static IReadOnlyList<SafeStorageFolder> ToSafe(IReadOnlyList<StorageFolder> unsafeFolders) =>
            ToSafe((IEnumerable<StorageFolder>)unsafeFolders);

        public static IReadOnlyList<SafeStorageFile> ToSafe(IReadOnlyList<StorageFile> unsafeFiles) =>
            ToSafe((IEnumerable<StorageFile>)unsafeFiles);
        #endregion

        #region Unsafe
        public static IStorageItem ToUnsafe(ISafeStorageItem safeItem)
        {
            if (safeItem is SafeStorageFile file)
                return file.UnsafeFile;
            else if (safeItem is SafeStorageFolder folder)
                return folder.UnsafeFolder;
            else
                return null;
        }

        public static IReadOnlyList<IStorageItem> ToUnsafe(IEnumerable<ISafeStorageItem> safeItems)
        {
            var unsafeItems = new List<IStorageItem>();

            if (safeItems != null)
            {
                foreach (var safeItem in safeItems)
                    unsafeItems.Add(ToUnsafe(safeItem));
            }

            return unsafeItems.AsReadOnly();
        }

        public static IReadOnlyList<StorageFolder> ToUnsafe(IEnumerable<SafeStorageFolder> safeFolders)
        {
            var unsafeFolders = new List<StorageFolder>();

            if (safeFolders != null)
            {
                foreach (var safeFolder in safeFolders)
                    unsafeFolders.Add(safeFolder.UnsafeFolder);
            }

            return unsafeFolders.AsReadOnly();
        }

        public static IReadOnlyList<StorageFile> ToUnsafe(IEnumerable<SafeStorageFile> safeFiles)
        {
            var unsafeFiles = new List<StorageFile>();

            if (safeFiles != null)
            {
                foreach (var safeFile in safeFiles)
                    unsafeFiles.Add(safeFile.UnsafeFile);
            }

            return unsafeFiles.AsReadOnly();
        }

        public static IReadOnlyList<IStorageItem> ToUnsafe(IReadOnlyList<ISafeStorageItem> safeItems) =>
            ToUnsafe((IEnumerable<ISafeStorageItem>)safeItems);

        public static IReadOnlyList<StorageFolder> ToUnsafe(IReadOnlyList<SafeStorageFolder> safeFolders) =>
            ToUnsafe((IEnumerable<SafeStorageFolder>)safeFolders);

        public static IReadOnlyList<StorageFile> ToUnsafe(IReadOnlyList<SafeStorageFile> safeFiles) =>
            ToUnsafe((IEnumerable<SafeStorageFile>)safeFiles);
        #endregion
    }
}
