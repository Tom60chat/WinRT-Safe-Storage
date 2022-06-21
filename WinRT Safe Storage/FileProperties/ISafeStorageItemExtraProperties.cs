using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace WinRT_Safe_Storage.FileProperties
{
    public interface ISafeStorageItemExtraProperties
    {
        Task<IDictionary<string, object>> TryRetrievePropertiesAsync([In] IEnumerable<string> propertiesToRetrieve);
        Task<bool> TrySavePropertiesAsync([In][HasVariant] IEnumerable<KeyValuePair<string, object>> propertiesToSave);
        Task<bool> TrySavePropertiesAsync();
    }
}