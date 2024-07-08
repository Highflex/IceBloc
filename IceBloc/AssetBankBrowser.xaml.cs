using IceBlocLib.Utility;
using IceBlocLib.InternalFormats;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace IceBloc
{
    /// <summary>
    /// Interaction logic for AssetBankBrowser.xaml
    /// </summary>
    public partial class AssetBankBrowser : Window
    {
        public static AssetListItem CurrentAssetItem;
        public static List<AssetBankAnimItem> AnimDataList;

        public static AssetBankBrowser Instance; // We need the instance to statically output messages.

        public AssetBankBrowser(AssetListItem assetListItem)
        {
            InitializeComponent();

            // load all needed information from passed asset list item
            CurrentAssetItem = assetListItem;

            Instance = this;
            this.Title = $"Asset Bank Browser: {CurrentAssetItem.Name}";

            // load the data!
            LoadAnimationsFromAssetBank();
        }

        public static void LoadAnimationsFromAssetBank()
        {
            Console.WriteLine("Loading Animations from Bank!");

            byte[] data = null;
            switch (Settings.CurrentGame)
            {
                case Game.Battlefield3:
                case Game.Warfighter:
                    data = IceBlocLib.Frostbite2.IO.ActiveCatalog.Extract(CurrentAssetItem.MetaData, true, CurrentAssetItem.AssetType); break;
            }

            // make sure its asset bank just as safety...
            if(CurrentAssetItem.Type == "AssetBank")
            {
                using var stream = new MemoryStream(data);
                AnimDataList = IceBlocLib.Frostbite2.Misc.AntPackageAsset.GetAssetBankAnimInfoData(stream);

                // show in data grid
                Instance.AssetsDataGrid.ItemsSource = AnimDataList;
            }
        }
    }
}
