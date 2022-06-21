using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Storage.FileProperties;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage.FileProperties
{
    public sealed class SafeBasicProperties : Safe, ISafeBasicProperties, ISafeStorageItemExtraProperties
    {
        #region Constructors
        /// <summary>
        /// Create a new instance of <see cref="SafeBasicProperties"/>
        /// </summary>
        /// <param name="storageFolder">Storage folder</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SafeBasicProperties(BasicProperties basicProperties)
        {
            this.basicProperties = basicProperties ?? throw new ArgumentNullException(nameof(basicProperties));
        }
        #endregion

        #region Variables
        private readonly BasicProperties basicProperties;
        #endregion

        #region Properties
        /// <summary> Gets the timestamp of the last modification of the file. </summary>
        public DateTimeOffset DateModified { get; }

        /// <summary> Gets the most appropriate date for the item. </summary>
        public DateTimeOffset ItemDate { get; }

        /// <summary> Gets the size of the file, in bytes. </summary>
        public ulong Size { get; }
        #endregion

        #region Methods
        /// <summary> </summary>
        /// <param name="propertiesToRetrieve"></param>
        /// <returns>
        ///     Returns the IDictionary, if this method executes correctly;
        ///     otherwise <see langword="null"/>.
        /// </returns>
        public Task<IDictionary<string, object>> TryRetrievePropertiesAsync([In] IEnumerable<string> propertiesToRetrieve) =>
            Try(async () =>
                await basicProperties.RetrievePropertiesAsync(propertiesToRetrieve)
            );

        /// <summary> </summary>
        /// <param name="propertiesToRetrieve"></param>
        /// <returns>
        ///     Returns <see langword="true"/> if the properties was successfully saved;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public Task<bool> TrySavePropertiesAsync([HasVariant, In] IEnumerable<KeyValuePair<string, object>> propertiesToSave) =>
            Try(async () =>
                await basicProperties.SavePropertiesAsync(propertiesToSave)
            );

        /// <summary> </summary>
        /// <param name="propertiesToRetrieve"></param>
        /// <returns>
        ///     Returns <see langword="true"/> if the properties was successfully saved;
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public Task<bool> TrySavePropertiesAsync() =>
            Try(async () =>
                await basicProperties.SavePropertiesAsync()
            );
        #endregion
    }
}