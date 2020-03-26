using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace COVID19Info.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.worldometers.info/coronavirus/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}