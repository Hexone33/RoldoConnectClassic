using Plugin.BLE;
using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoldoConnectClassic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectDevicePage : ContentPage
    {
        ObservableCollection<IDevice> localDeviceList = new ObservableCollection<IDevice>();
        public SelectDevicePage()
        {
            InitializeComponent();
            Scan.Source = ImageSource.FromResource("RoldoConnectClassic.Images.scanbtn.png", typeof(SelectDevicePage).GetTypeInfo().Assembly);
            Wave.Source = ImageSource.FromResource("RoldoConnectClassic.Images.wave.png", typeof(SelectDevicePage).GetTypeInfo().Assembly);

            Transporter.ble = CrossBluetoothLE.Current;
            Transporter.adapter = CrossBluetoothLE.Current.Adapter;

            if (Device.RuntimePlatform == Device.iOS)
            {
                Scan.Scale = 1.5;
                LvBondedDevices.BackgroundColor = Color.Transparent;
            }

            var phoneWidth = Application.Current.MainPage.Width;

        }

        private async void LvBondedDevices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Transporter.device = (Plugin.BLE.Abstractions.Contracts.IDevice)e.SelectedItem;
            await Transporter.adapter.ConnectToDeviceAsync(Transporter.device);
            localDeviceList.Clear();
            App.Current.MainPage = new NavigationPage(new Control()) { BarBackgroundColor = Color.Turquoise };
        }

        private async void Scan_Clicked(object sender, EventArgs e)
        {
            var scanFilterOptions = new ScanFilterOptions();

            //await DisplayAlert("Statut :", ble.State.ToString(), "Close");

            Transporter.adapter.ScanTimeout = 1000;


            localDeviceList.Clear();

            Transporter.adapter.DeviceDiscovered += (s, a) =>
            {
                if (a.Device.Name != null && a.Device.Name.ToLower().Contains("z"))
                {
                    //IDevice renamed = a.Device;
                    //renamed.Name = "Roldo Connect";
                    if(!localDeviceList.Contains(a.Device))
                        localDeviceList.Add(a.Device);
                }
            };

            await Transporter.adapter.StartScanningForDevicesAsync();

            RefreshList();
        }

        public void RefreshList()
        {
            //On stop le scan de force
            Transporter.adapter.StopScanningForDevicesAsync();
            //On remplit notre liste des devices trouvés
            LvBondedDevices.ItemsSource = localDeviceList;
        }

    }
}