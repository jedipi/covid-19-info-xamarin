using System;
using System.Threading.Tasks;
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
            OpenWebCommand = new Command<string>(async (p) => await Browser.OpenAsync(p));
        }

        
        public ICommand OpenWebCommand { get; set; }
    }
}