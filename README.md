# WinRT Safe Storage

- Enough of InvalidOperationException, COMException or just undocumented exceptions?
- Try Catch each storage method makes your code heavier?
- You want to go back to Win32 because it was easier?
- But you had to finish this one UWP project?

Well **WinRT Safe Storage** is here for that!  
It might not be that clean, but at least every WinRT storage call isn't going to crash your app, and error handling is much easier.

## Examples

```cs
(await SafeStorageFolder.TryGetFolderFromPathAsync("C:/Users/"))
    .OnSuccess((folder) =>
        ShowFolder(folder) )
    .OnError((exception) =>
        WarningMessage.Text = exception.Message );
```
##   
```cs
var operation = await SafeStorageFolder.TryGetFolderFromPathAsync(UrlBar.Text);

if (operation.IsSuccess)
    ShowFolder(operation.Value);
else
    WarningMessage.Text = operation.Exception.Message;
```
