using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoldoConnectClassic
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CodeLock()) { BarBackgroundColor = Color.Turquoise };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
