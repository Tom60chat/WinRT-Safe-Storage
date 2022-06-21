using System.Collections.Generic;
using Windows.Storage;

namespace WinRT_Safe_Storage
{
    internal class UnsafeConverter
    {
        public static ISafeStorageItem ToSafe(IStorageItem unsafeItem)
        {
            if (unsafeItem is StorageFile file)
                return new SafeStorageFile(file);
            else if (unsafeItem is StorageFolder folder)
                return new SafeStorageFolder(folder);
            else
                return null;
        }

        public static IReadOnlyList<ISafeStorageItem> ToSafe(IReadOnlyList<IStorageItem> unsafeItems)
        {
            var safeItems = new List<ISafeStorageItem>();

            if (unsafeItems != null)
            {
                foreach (var unsafeItem in unsafeItems)
                {
                    if (unsafeItem is StorageFile unsafeFile)
                        safeItems.Add(new SafeStorageFile(unsafeFile));
                    else if (unsafeItem is StorageFolder unsafeFolder)
                        safeItems.Add(new SafeStorageFolder(unsafeFolder));
                }
            }

            return safeItems.AsReadOnly();
        }

        public static IReadOnlyList<SafeStorageFolder> ToSafe(IReadOnlyList<StorageFolder> unsafeFolders)
        {
            var safeFolders = new List<SafeStorageFolder>();

            if (unsafeFolders != null)
            {
                foreach (var unsafeFolder in unsafeFolders)
                    safeFolders.Add(new SafeStorageFolder(unsafeFolder));
            }

            return safeFolders.AsReadOnly();
        }

        public static IReadOnlyList<SafeStorageFile> ToSafe(IReadOnlyList<StorageFile> unsafeFiles)
        {
            var safeFiles = new List<SafeStorageFile>();

            if (unsafeFiles != null)
            {
                foreach (var unsafeFile in unsafeFiles)
                    safeFiles.Add(new SafeStorageFile(unsafeFile));
            }

            return safeFiles.AsReadOnly();
        }
    }
}
