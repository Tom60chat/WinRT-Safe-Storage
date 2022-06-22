# WinRT Safe Storage

- Enough of InvalidOperationException, COMException or just undocumented exceptions?
- Try Catch each storage method makes your code heavier?
- You want to go back to Win32 because it was easier?
- But you had to finish this one UWP project?

Well **WinRT Safe Storage** is here for that!  
It might not be that clean, but at least every WinRT storage call isn't going to crash your app, and error handling is much easier.

## Examples
Try
```cs
await SafeStorageFolder.TryGetFolderFromPathAsync(UrlBar.Text)
    .OnSuccess(async (folder) =>
        await ShowFolder(folder) )
    .OnError((exception) =>
        WarningMessage.Text = exception.Message );
```
or  
```cs
var getFolderOperation = await SafeStorageFolder.TryGetFolderFromPathAsync(UrlBar.Text);

if (getFolderOperation.IsSuccess)
    await ShowFolder(getFolderOperation.Value);
else
    WarningMessage.Text = getFolderOperation.Exception.Message;
```
##  
Try with return
```cs
var getParentOperation = await CurrentFolder.TryGetParentAsync();

if (getParentOperation.Value != null)
    await GoToAsync(getParentOperation.Value);

return getParentOperation.Value != null; // Is successful ?
```
##  
Single convertion
```cs
await Launcher.LaunchFileAsync(safeFile.UnsafeFile);
```
Multi convertion
```cs
var safeFolders = new List<SafeStorageFolder>();

safeFolders.AddRange(StorageConverter.ToSafe(unsafeFolders));
```

## Download

[Download on NuGet](https://www.nuget.org/packages/WinRT_SafeStorage/)
