using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using WinRT_Safe_Storage.Tools;

namespace WinRT_Safe_Storage.FileProperties
{
    public interface ISafeStorageItemExtraProperties
    {
        Task<SafeOperation<IDictionary<string, object>>> TryRetrievePropertiesAsync([In] IEnumerable<string> propertiesToRetrieve);
        Task<SafeOperation> TrySavePropertiesAsync([In][HasVariant] IEnumerable<KeyValuePair<string, object>> propertiesToSave);
        Task<SafeOperation> TrySavePropertiesAsync();
    }
}