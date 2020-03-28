using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using COVID19Info.Models;
using HtmlAgilityPack;

namespace COVID19Info.Services
{
    public class HtmlData:IHtmlData
    {
        public const string Datasource = @"https://www.worldometers.info/coronavirus/";
        private HtmlDocument _html { get; set; }
        public List<int> Total { get; set; } = new List<int>();
        public List<Country> Countries { get; set; } = new List<Country>();
        public Country CountryTotal { get; set; }
        public string LastUpdate { get; set; }

        /// <summary>
        /// retrieve the html page from https://www.worldometers.info/coronavirus/
        /// </summary>
        /// <returns></returns>
        public async Task GetData()
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                _html = await Task.Run(() => web.Load(Datasource));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            
        }

        /// <summary>
        /// retrieve the global total cases number
        /// </summary>
        public void GetTotal()
        {
            if (_html == null)
                return;

            Total.Clear();
            LastUpdate = "";

            // parse last update
            var tag = _html.DocumentNode.SelectSingleNode("//div[@id='page-top']");
            var sibling = tag.NextSibling;

            while (sibling != null)
            {
                if (sibling.NodeType == HtmlNodeType.Element)
                {
                    LastUpdate = sibling.InnerHtml; 
                    break;
                }

                sibling = sibling.NextSibling;
            }
            

            // get data from <div class="maincounter-number">
            var nodes = _html.DocumentNode.SelectNodes("//div[contains(@class, 'maincounter-number')]");
            if (nodes == null)
                return;

            foreach (var node in nodes)
            {
                int number;
                int.TryParse(node.SelectSingleNode(".//span").InnerText.Trim(), 
                    NumberStyles.AllowThousands, 
                    CultureInfo.InvariantCulture, out number);
                Total.Add(number);
            }
        }

        /// <summary>
        /// retrieve the cases number for all countries
        /// </summary>
        public void GetCountries()
        {
            if (_html == null)
                return;

            Countries.Clear();

            var rows = _html.DocumentNode.SelectNodes("//table[@id='main_table_countries_today']/tbody/tr");
            foreach (var row in rows)
            {
                
                var cells = row.SelectNodes(".//td");
                var country = new Country();

                // the first cell is country name, it may or may not contain a hyper link
                var a = cells.FirstOrDefault(x => x.ChildNodes.Count > 1 );
                if (a != null)
                    country.Name = a.ChildNodes[1].InnerText;
                else
                    country.Name = cells[0].InnerText;

                int temp;
               
                int.TryParse(cells[1].InnerText.Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.TotalCases = temp;

                int.TryParse(cells[2].InnerText.Trim().Trim('+'), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.NewCases = temp;

                int.TryParse(cells[3].InnerText.Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.TotalDeaths = temp;

                int.TryParse(cells[4].InnerText.Trim().Trim('+'), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.NewDeaths = temp;

                int.TryParse(cells[5].InnerText.Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.TotalRecovered = temp;

                int.TryParse(cells[6].InnerText.Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.ActiveCases = temp;

                int.TryParse(cells[7].InnerText.Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.SeriousCritical = temp;

                int.TryParse(cells[7].InnerText.Trim(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out temp);
                country.TotalCasesPer1MPop = temp;

                if (row == rows.Last())
                {
                    CountryTotal = country;
                }
                else
                {
                    Countries.Add(country);
                }
                
            }
        }
    }
}
