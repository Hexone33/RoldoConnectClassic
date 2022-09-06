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
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoldoConnectClassic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Control : ContentPage
    {
        private int clickCounter = 0;
        public Control()
        {
            InitializeComponent();
            Wave.Source = ImageSource.FromResource("RoldoConnectClassic.Images.wave.png", typeof(Control).GetTypeInfo().Assembly);
            Open.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ferme.png", typeof(Control).GetTypeInfo().Assembly);
            Open2.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ferme.png", typeof(Control).GetTypeInfo().Assembly);
            Close.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ouvert.png", typeof(Control).GetTypeInfo().Assembly);
            Close2.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ouvert.png", typeof(Control).GetTypeInfo().Assembly);
            RoldoConnect.Source = ImageSource.FromResource("RoldoConnectClassic.Images.roldoname.png", typeof(Control).GetTypeInfo().Assembly);
            ControlPanel.Source = ImageSource.FromResource("RoldoConnectClassic.Images.control.png", typeof(Control).GetTypeInfo().Assembly);
            //ble = CrossBluetoothLE.Current;
            Transporter.adapter = CrossBluetoothLE.Current.Adapter;
            Transporter.deviceList = new ObservableCollection<IDevice>();
        }

        public async void SendDataToMachine(sbyte[] bArr)
        {
            //On récupère les services de notre device selectionner auparavant puis ses charactéristiques
            var services = await Transporter.device.GetServicesAsync();
            var characteristics = await services[0].GetCharacteristicsAsync();
            bool valid = false;

            foreach(var element in services)
            {
                var name = element.Name;
                if(name == "TI SensorTag Smart Keys")
                {
                    characteristics = await element.GetCharacteristicsAsync();
                    valid = true;
                }
            }

            if (valid == true)
            {
                //On convertit en byte pour l'envoi a cause des valeurs négatives
                byte[] unsigned = (byte[])(Array)bArr;

                //On envoi à la machine
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    characteristics[0].WriteAsync(unsigned);
                });
            }
            else
            {
                await DisplayAlert("Erreur", "Erreur Mauvais services", "Ok");
            }
        }

        private void Open_Clicked(object sender, EventArgs e)
        {
            if (clickCounter == 0)
            {
                //Byte correspondant pour une ouverture/fermeture sur une zone si cliquer une fois
                sbyte[] bArr = { -59, 4, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
                SendDataToMachine(bArr);
                Open.Source = ImageSource.FromResource("RoldoConnectClassic.Images.status_selected.png", typeof(Control).GetTypeInfo().Assembly);

                clickCounter = 1;
            }
            else
            {
                //Byte correspondant pour une ouverture/fermeture sur une zone si cliquer deuxieme fois
                sbyte[] bArr = { -59, 6, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
                SendDataToMachine(bArr);
                Open.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ferme.png", typeof(Control).GetTypeInfo().Assembly);
                clickCounter = 0;
            }
        }
        private void Open2_Pressed(object sender, EventArgs e)
        {
            //Byte correspondant pour une ouverture/fermeture sur une zone si cliquer une fois
            sbyte[] bArr = { -59, 4, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
            Open2.Source = ImageSource.FromResource("RoldoConnectClassic.Images.status_selected.png", typeof(Control).GetTypeInfo().Assembly);
            SendDataToMachine(bArr);
        }
        private void Open2_Released(object sender, EventArgs e)
        {
            sbyte[] bArr = { -59, 6, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
            Open2.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ferme.png", typeof(Control).GetTypeInfo().Assembly);
            SendDataToMachine(bArr);
        }
        private void Close_Pressed(object sender, EventArgs e)
        {
            //Byte correspondant pour une ouverture/fermeture sur une zone sir pressé
            sbyte[] bArr = { -59, 5, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
            Close.Source = ImageSource.FromResource("RoldoConnectClassic.Images.status_selected.png", typeof(Control).GetTypeInfo().Assembly);
            SendDataToMachine(bArr);
        }
        private void Close_Released(object sender, EventArgs e)
        {
            //Byte correspondant pour une ouverture/fermeture sur une zone si relaché
            sbyte[] bArr = { -59, 7, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
            Close.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ouvert.png", typeof(Control).GetTypeInfo().Assembly);
            SendDataToMachine(bArr);
        }
        private void Close2_Clicked(object sender, EventArgs e)
        {
            if (clickCounter == 0)
            {
                //Byte correspondant pour une ouverture/fermeture sur une zone si cliquer une fois
                sbyte[] bArr = { -59, 5, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
                SendDataToMachine(bArr);
                Close2.Source = ImageSource.FromResource("RoldoConnectClassic.Images.status_selected.png", typeof(Control).GetTypeInfo().Assembly);
                clickCounter = 1;
            }
            else
            {
                //Byte correspondant pour une ouverture/fermeture sur une zone si cliquer deuxieme fois
                sbyte[] bArr = { -59, 7, 49, 50, 51, 52, 53, 54, 55, 56, -86 };
                SendDataToMachine(bArr);
                Close2.Source = ImageSource.FromResource("RoldoConnectClassic.Images.p_ouvert.png", typeof(Control).GetTypeInfo().Assembly);
                clickCounter = 0;
            }
        }
        private void SwitchBtn1_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchBtn1.IsToggled == true)
            {
                Open.IsVisible = true;
                Open2.IsVisible = false;
                OpenManu.Text = "Automatique";
            }
            else
            {
                Open.IsVisible = false;
                Open2.IsVisible = true;
                OpenManu.Text = "Manuel";
            }
        }
        private void SwitchBtn2_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchBtn2.IsToggled == true)
            {
                Close.IsVisible = false;
                Close2.IsVisible = true;
                CloseManu.Text = "Automatique (interdit en France)";
            }
            else
            {
                Close.IsVisible = true;
                Close2.IsVisible = false;
                CloseManu.Text = "Manuel";
            }
        }
    }
}