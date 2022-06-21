using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using WinRT_Safe_Storage.FileProperties;

namespace WinRT_Safe_Storage
{
    public interface ISafeStorageItem
    {
        #region Properties
        /// <summary> Gets the attributes of a storage element. </summary>
        FileAttributes Attributes { get; }

        /// <summary> Gets the creation date and time of the current item. </summary>
        DateTimeOffset DateCreated { get; }

        /// <summary> Gets the name of the item, including the filename extension, if any. </summary>
        string Name { get; }

        /// <summary> Gets the full file system path of the item, if the item has a path. </summary>
        string Path { get; }
        #endregion

        #region Methods
        /// <summary> Try to gets the parent folder of the current storage item. </summary>
        /// <returns>
        ///     Returns the parent folder as a <see cref="SafeStorageFolder"/> object, if this method executes correctly;
        ///     otherwise <see langword="null"/>.
        /// </returns>
        Task<SafeStorageFolder> TryGetParentAsync();

        /// <summary> Try to renames the current item. </summary>
        /// <param name="desiredName"> Desired new name of the element. </param>
        /// <returns>
        ///     Returns <see langword="true"/> if the current item was successfully renamed;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        Task<bool> TryRenameAsync(string desiredName);

        /// <summary>
        ///     Try to renames the current item.
        ///     This method also specifies what to do if an existing element at the location of the current element has the same name.
        /// </summary>
        /// <param name="desiredName"> Desired new name of the active element. </param>
        /// <param name="option"> Enumeration value that determines how Windows responds if the desiredName is the same as the name of an existing item in the current item location. </param>
        /// <returns>
        ///     Returns <see langword="true"/> if the current item was successfully renamed;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        Task<bool> TryRenameAsync(string desiredName, NameCollisionOption option);

        /// <summary> Deletes the current item. </summary>
        /// <returns>
        ///     Returns <see langword="true"/> if the current item was successfully deleted;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        Task<bool> TryDeleteAsync();

        /// <summary> Deletes the current item, optionally deleting it permanently. </summary>
        /// <param name="option"> A value that indicates whether the item should be permanently deleted. </param>
        /// <returns>
        ///     Returns <see langword="true"/> if the current item was successfully deleted;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        Task<bool> TryDeleteAsync(StorageDeleteOption option);

        /// <summary> Gets the basic properties of the current item (such as a file or folder). </summary>
        /// <returns>
        ///     Returns the basic properties of the current item as a <see cref="SafeBasicProperties"/> object, if this method executes correctly;
        ///     otherwise <see langword="null"/>.
        /// </returns>
        Task<SafeBasicProperties> TryGetBasicPropertiesAsync();

        /// <summary>
        /// Determines whether the current <see cref="ISafeStorageItem"/> object matches the specified <see cref="StorageItemTypes"/> value.
        /// </summary>
        /// <param name="type"> Match value. </param>
        /// <returns>
        ///     <see langword="true"/> if the IStorageItem object matches the specified value;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        bool IsOfType(StorageItemTypes type);

        /// <summary> Indicates whether the current item is identical to the specified item. </summary>
        /// <param name="item"> The ISafeStorageItem object that represents a storage item on which to perform the comparison. </param>
        /// <returns>
        ///     Returns <see langword="true"/> if the current storage element is identical to the specified storage element;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        bool IsEqual(ISafeStorageItem item);
        #endregion
    }
}
