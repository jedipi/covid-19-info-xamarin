using COVID19Info.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID19Info.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountriesPage : ContentPage
    {
        private bool _shown;
        private CountriesViewModel _vm;
        public CountriesPage()
        {
            InitializeComponent();
            BindingContext = _vm = new CountriesViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //SearchBarSearch.Focus();

            if (_shown)
                return;

            _shown = true;

            await _vm.RefreshData();

        }
    }
}