using System;

namespace WinRT_Safe_Storage.FileProperties
{
    public interface ISafeBasicProperties
    {
        DateTimeOffset DateModified { get; }
        DateTimeOffset ItemDate { get; }
        ulong Size { get; }
    }
}