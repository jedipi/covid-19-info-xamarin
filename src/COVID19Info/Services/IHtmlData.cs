using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using COVID19Info.Models;
using HtmlAgilityPack;

namespace COVID19Info.Services
{
    public interface IHtmlData
    {
        List<int> Total { get; set; }
        ObservableCollection<Country> Countries { get; set; }
        Task GetData();
        void GetTotal();
        void GetCountries();
    }
}
