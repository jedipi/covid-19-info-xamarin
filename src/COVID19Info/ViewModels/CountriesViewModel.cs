using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COVID19Info.Models;
using COVID19Info.Services;
using Xamarin.Forms;

namespace COVID19Info.ViewModels
{
    public class CountriesViewModel : BaseViewModel
    {
        private ObservableCollection<Country> _unfilteredItems;
        
        public IHtmlData HtmlContent => DependencyService.Get<IHtmlData>();
        public bool IsRefreshing { get; set; }
        public ObservableCollection<Country> Countries { get; set; }
        public Country CountryTotal { get; set; }
        public Command RefreshViewCommand { get; set; }
        public Command<string> SearchTextCmd { get; set; }

        public CountriesViewModel()
        {
            Title = "COVID-19 Info by Country";
            RefreshViewCommand = new Command(async()=> await RefreshData());
            SearchTextCmd = new Command<string>((p)=>FilterCmd(p));
        }

        private void FilterCmd(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                Countries = _unfilteredItems;
            }
            else
            {
                Countries = new ObservableCollection<Country>(_unfilteredItems.Where(x => x.Name.ToLower().Contains(filter)));
            }
        }

        public async Task RefreshData()
        {
            this.IsRefreshing = true;

            // load the html page
            await HtmlContent.GetData();

            // parse the html to get a list of countries
            HtmlContent.GetCountries();

            if (HtmlContent.Countries.Count > 0)
            {
                CountryTotal = HtmlContent.CountryTotal;
                Countries = _unfilteredItems = new ObservableCollection<Country>(HtmlContent.Countries.OrderByDescending(x=>x.TotalCases));
            }

            this.IsRefreshing = false;
        }
    }
}
