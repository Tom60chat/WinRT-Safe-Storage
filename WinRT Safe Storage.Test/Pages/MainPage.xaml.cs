using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Search;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRT_Safe_Storage.Models;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WinRT_Safe_Storage.Test
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        public ObservableCollection<StorageItemModel> Items { get; set; } = new ObservableCollection<StorageItemModel>();

        private async void GridItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is StorageItemModel itemModel)
            {
                if (itemModel.Item is SafeStorageFolder folder)
                {
                    await ShowFolder(folder);
                }
                else if (itemModel.Item is SafeStorageFile file)
                {

                }
            }
        }

        private void GridItems_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {

        }

        private async void UrlBar_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                (await SafeStorageFolder.TryGetFolderFromPathAsync(UrlBar.Text))
                    .OnSuccess((folder) =>
                        _ = ShowFolder(folder)
                    )
                    .OnError((exception) =>
                        WarningMessage.Text = exception.Message
                    );
        }
        private async Task ShowFolder(SafeStorageFolder storageFolder)
        {
            if (storageFolder == null) return;

            UrlBar.Text = storageFolder.Path;

            var queryOptions = new QueryOptions
            {
                IndexerOption = IndexerOption.UseIndexerWhenAvailable,
            };
            queryOptions.SetThumbnailPrefetch(ThumbnailMode.SingleItem, 0, ThumbnailOptions.UseCurrentScale);
            var currentFolderQuery = storageFolder.CreateItemQueryWithOptions(queryOptions);

            // Show item
            (await currentFolderQuery.TryGetItemsAsync())
                .OnSuccess((items) =>
                {
                    Items.Clear();

                    foreach (var item in items)
                        if (item.IsOfType(StorageItemTypes.Folder))
                            Items.Add(new StorageItemModel(item));
                    foreach (var item in items)
                        if (item.IsOfType(StorageItemTypes.File))
                            Items.Add(new StorageItemModel(item));
                })
                .OnError((exeception) =>
                    WarningMessage.Text = exeception.Message
                );
        }

        private void LocalFolder_Btn_Click(object sender, RoutedEventArgs e)
        {
            _ = ShowFolder(new SafeStorageFolder(ApplicationData.Current.LocalFolder));
        }
    }
}
