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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoldoConnectClassic
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CodeLock : ContentPage
    {
        public CodeLock()
        {
            InitializeComponent();
            RoldoConnect.Source = ImageSource.FromResource("RoldoConnectClassic.Images.roldoname.png", typeof(CodeLock).GetTypeInfo().Assembly);
        }

        private void PinView_Success(object sender, EventArgs e)
        {
            if(Application.Current.Properties["oldpassword"] as string == "0000")
            {
                DisplayAlert("Notification", "Mot de passe définie", "Ok");
            }
            App.Current.MainPage = new NavigationPage(new SelectDevicePage());
        }

        private void PinView_Error(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("password"))
            {
                DisplayAlert("Erreur", "Mauvais mot de passe !", "Ok");
            }
        }
    }
}