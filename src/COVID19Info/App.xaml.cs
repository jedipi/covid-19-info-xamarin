using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using COVID19Info.Services;
using COVID19Info.Views;

namespace COVID19Info
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<HtmlData>();
            MainPage = new AppShell();
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
