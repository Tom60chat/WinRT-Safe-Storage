using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml.Media.Imaging;
using WinRT_Safe_Storage.FileProperties;

namespace WinRT_Safe_Storage.Models
{
    public class StorageItemModel : INotifyPropertyChanged, IDisposable
    {
        #region Constructors
        public StorageItemModel(ISafeStorageItem item)
        {
            Item = item;
            Thumbnail = new BitmapImage();

            _ = LoadIcon();
            _ = SetToolTip();
        }
        #endregion

        #region Variables
        public readonly Guid Id = Guid.NewGuid();
        public event PropertyChangedEventHandler PropertyChanged;
        public readonly ISafeStorageItem Item;

        private string glyph;
        private string subGlyph;
        private string toolTip;
        private IAsyncOperation<StorageItemThumbnail> thumbnailAsync;
        #endregion

        #region Properties
        public string DisplayName => (Item is IStorageItemProperties itemProperties) ? itemProperties.DisplayName : Item.Name;
        public string CreationDate => Item.DateCreated.ToString("G");
        public BitmapImage Thumbnail { get; private set; }
        public string Glyph
        {
            get => glyph;
            private set
            {
                glyph = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Glyph)));
            }
        }
        public string SubGlyph
        {
            get => subGlyph;
            private set
            {
                subGlyph = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubGlyph)));
            }
        }
        public string ToolTip
        {
            get => toolTip;
            private set
            {
                toolTip = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ToolTip)));
            }
        }
        #endregion

        #region Methods

        private async Task LoadIcon()
        {
            LoadGlyph();
            if (await LoadThumbnail())
                Glyph = SubGlyph = string.Empty;
        }

        private async Task<bool> LoadThumbnail()
        {
            StorageItemThumbnail thumbnail = null;

            if (Item is SafeStorageFile file)
                thumbnailAsync = file.GetScaledImageAsThumbnailAsync(ThumbnailMode.SingleItem);
            else if (Item is SafeStorageFolder folder)
                thumbnailAsync = folder.GetScaledImageAsThumbnailAsync(ThumbnailMode.SingleItem);

            try
            { thumbnail = await thumbnailAsync; }
            catch
            { }

            if (thumbnail != null && !(
                    thumbnail.ContentType == "image/jpeg" &&
                    thumbnail.Type == ThumbnailType.Icon &&
                    thumbnail.Size == 1261 &&
                    thumbnail.OriginalWidth == 64 &&
                    thumbnail.OriginalHeight == 64 &&
                    thumbnail.ReturnedSmallerCachedSize == false))
            {
                Thumbnail.SetSource(thumbnail);
                return true;
            }

            return false;
        }

        private void LoadGlyph()
        {
            if (Item is SafeStorageFolder folder)
                Glyph = "\uE838"; // E8B7:Folder F12B:FolderHorizontal E838:FolderOpen
            else if (Item is SafeStorageFile file)
            {
                Glyph = "\uE8A5"; // E8A5:Document E8A6:ProtectedDocument
                // TODO: check pretected file

                switch (file.FileType)
                {
                    // Document
                    case ".txt":
                    case ".doc":
                    case ".docx":
                    case ".odt":
                    case ".rtf":
                        SubGlyph = "\uE8E3";
                        break;

                    // PDF
                    case ".pdf":
                        Glyph = "\uea90";
                        break;

                    // Image
                    case ".png":
                    case ".jpg":
                    case ".gif":
                    case ".tiff":
                    case ".raw":
                        Glyph = "\ueb9f";
                        break;

                    // Audio
                    case ".mp3":
                    case ".ogg":
                    case ".wav":
                    case ".mid":
                        Glyph = "\uE189";
                        break;

                    // Video
                    case ".mp4":
                    case ".mov":
                        Glyph = "\uE8b2";
                        break;

                    // Image Edit File
                    case ".psd":
                        Glyph = "\ue932";
                        break;

                    // Video Edit File
                    case ".prproj":
                    case ".sfk":
                        Glyph = "\ue78a";
                        break;

                    // Music Edit File
                    case ".flp":
                    case ".aup3":
                        Glyph = "\ue90b";
                        break;

                    // SlideShow
                    case ".pp":
                        Glyph = "\uE173";
                        break;

                    // Configuration File
                    case ".inf":
                        SubGlyph = "\uE115";
                        break;

                    // Save File
                    case ".sav":
                        SubGlyph = "\uE105";
                        break;

                    // Email
                    case ".pst":
                    case ".eml":
                        SubGlyph = "\uE119";
                        break;

                    // Contact
                    case ".pab":
                        SubGlyph = "\uE136";
                        break;

                    // Subtitle
                    case ".srt":
                    case ".sbv":
                    case ".sub":
                    case ".mpsub":
                    case ".lrc":
                    case ".cap":
                        SubGlyph = "\uE190";
                        break;

                    // Calendar
                    case ".vcs":
                        SubGlyph = "\uE163";
                        break;

                    // Command File
                    case ".cmd":
                    case ".bat":
                    case ".ps1":
                        SubGlyph = "\ue756";
                        break;

                    // Zip file
                    case ".7z":
                    case ".zip":
                    case ".rar":
                    case ".gzip":
                    case ".tar":
                    case ".cab":
                    case ".gcz":
                    case ".rvz":
                        Glyph = "\uEC50";
                        break;

                    // Iso file
                    case ".iso":
                        SubGlyph = "\uE958";
                        break;

                    // Remote Desktop File
                    case ".rdp":
                        SubGlyph = "\uE8AF"; // E8AF:Remote
                        break;

                    // Cert file
                    case ".crt":
                    case ".pfx":
                        SubGlyph = "\uEB95";
                        break;

                    // Coding file
                    case ".cs":
                    case ".c":
                    case ".cpp":
                    case ".rs":
                    case ".java":
                    case ".json":
                    case ".js":
                    case ".py":
                    case ".kt":
                    case ".hs":
                    case ".rb":
                    case ".go":
                    case ".xaml":
                    case ".xml":
                    case ".yml":
                        SubGlyph = "\uE943";
                        break;
                }
            }
        }

        private async Task SetToolTip()
        {
            var fileProperties_ResourceLoader = ResourceLoader.GetForCurrentView();

            IDictionary<string, object> properties = null;
            SafeBasicProperties basicProperties = null;

            (await Item.TryGetBasicPropertiesAsync())
                .OnSuccess(async (bP) => {
                    basicProperties = bP;
                    (await basicProperties.TryRetrievePropertiesAsync(new string[] { "System.ItemTypeText" }))
                        .OnSuccess((p) => properties = p);
                });

            string toolTips = string.Empty;

            if (Item is SafeStorageFolder folder)
            {
                toolTips = fileProperties_ResourceLoader.GetString("CreationDate") + " " + CreationDate + Environment.NewLine;

                //ToolTip += fileProperties_ResourceLoader.GetString("Size") + " " + BytesConverter.BytesToString(basicProperties.Size) + Environment.NewLine;

                try
                {
                    var foldersQuery = folder.CreateFolderQuery();

                    (await foldersQuery.TryGetFoldersAsync(0, 4))
                        .OnSuccess((folders) =>
                        {
                            string foldersString = string.Empty;

                            foreach (var subFolder in folders)
                                foldersString += subFolder.DisplayName + ", ";

                            if (foldersString.Length > 2)
                                foldersString = foldersString.Remove(foldersString.Length - 2);

                            if (folders.Count > 0)
                                toolTips += fileProperties_ResourceLoader.GetString("Folders") + " " + foldersString + Environment.NewLine;
                        });

                    var filesQuery = folder.CreateFileQuery();

                    (await filesQuery.TryGetFilesAsync(0, 4))
                        .OnSuccess((files) =>
                        {
                            string filesString = string.Empty;

                            foreach (var subFile in files)
                                filesString += subFile.DisplayName + ", ";

                            if (filesString.Length > 2)
                                filesString = filesString.Remove(filesString.Length - 2);

                            if (files.Count > 0)
                                toolTips += fileProperties_ResourceLoader.GetString("Files") + " " + filesString + Environment.NewLine;
                        });

                }
                catch { }
            }
            else if (Item is SafeStorageFile file)
            {
                if (properties != null && properties.TryGetValue("System.ItemTypeText", out var itemTypeText))
                    toolTips = fileProperties_ResourceLoader.GetString("Type") + " " + itemTypeText + Environment.NewLine;

                if (basicProperties != null)
                {
                    toolTips += fileProperties_ResourceLoader.GetString("Size") + " " + basicProperties.Size + Environment.NewLine;

                    toolTips += fileProperties_ResourceLoader.GetString("ModifiedOn") + " " + basicProperties.DateModified.ToString("G") + Environment.NewLine;
                }
            }

            ToolTip = toolTips.Remove(toolTips.Length - Environment.NewLine.Length);
        }

        public void Dispose()
        {
            thumbnailAsync.Cancel();
        }

        public override bool Equals(object obj) =>
            obj is StorageItemModel model && Id.Equals(model.Id);

        public override int GetHashCode() =>
            2108858624 + Id.GetHashCode();

        public override string ToString() =>
            DisplayName;

        #endregion
    }
}
