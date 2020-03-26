using Xamarin.Forms;
using COVID19Info.ViewModels;

namespace COVID19Info.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class GlobalPage : ContentPage
    {
        private bool _shown;
        private GlobalViewModel _vm;
        public GlobalPage()
        {
            InitializeComponent();

            BindingContext = _vm = new GlobalViewModel();
            
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_shown)
                return;

            _shown = true;

            await _vm.RefreshData();
        }


    }
}