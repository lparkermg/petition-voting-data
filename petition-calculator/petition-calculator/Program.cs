using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using petition_calculator.DataObjects;

namespace petition_calculator
{
    class Program
    {
        private static string _petitionJsonUrl = "https://petition.parliament.uk/petitions/131215.json";
        private static Petition _petition;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Petition data calculator. By Luke Parker");
            TagsUsing(args);
            Console.WriteLine("\n Downloading Json Data.");
            
            _petition = GetJsonData();

            Console.WriteLine("Crunching Data.");
            RunCalculations(args);
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static Petition GetJsonData()
        {
            var jsonData = "";

            using (WebClient wb = new WebClient())
            {
                jsonData = wb.DownloadString(_petitionJsonUrl);
            }

            if (!String.IsNullOrWhiteSpace(jsonData))
            {
                var dyn = JsonConvert.DeserializeObject<dynamic>(jsonData);

                var countries = GetCountries(dyn.data.attributes.signatures_by_country);
                var constituencies = GetContituencies(dyn.data.attributes.signatures_by_constituency);

                return new Petition(dyn.data.attributes.action.ToString(),_petitionJsonUrl,DateTime.Parse(dyn.data.attributes.updated_at.ToString()),Convert.ToInt32(dyn.data.attributes.signature_count.ToString()),countries,constituencies);
            }

            return null;
        }

        private static List<Country> GetCountries(dynamic countries)
        {
            var theCountries = new List<Country>();

            foreach (var country in countries)
            {
                theCountries.Add(new Country(country.name.ToString(), country.code.ToString(), Convert.ToInt32(country.signature_count.ToString())));
            }

            return theCountries;
        }

        private static List<Constituency> GetContituencies(dynamic constituencies)
        {
            var theConstituencies = new List<Constituency>();

            foreach (var constituency in constituencies)
            {
                theConstituencies.Add(new Constituency(constituency.name.ToString(),constituency.ons_code.ToString(),constituency.mp.ToString(),Convert.ToInt32(constituency.signature_count.ToString())));
            }

            return theConstituencies;
        }

        private static void TagsUsing(string[] args)
        {
            Console.WriteLine("Arguments Used:");
            foreach (var arg in args)
            {
                if (arg.Contains("-country-top-"))
                {
                    Console.WriteLine("Top Countries.");
                }

                if (arg.Contains("-constit-top-"))
                {
                    Console.WriteLine("Top Constituencies.");
                }
            }
        }

        private static void RunCalculations(string[] args)
        {
            GetTotals("outsideUK");
            Console.WriteLine("");

            GetTotals("insideUK");
            Console.WriteLine("");

            foreach (var arg in args)
            {
                if (arg.Contains("-country-top-"))
                {
                    var number = Convert.ToInt32(arg.Remove(0, 13));
                    Console.WriteLine($"Top {number} Countries:");
                    GetTop("countries",number);
                }
                else if (arg.Contains("-constit-top-"))
                {
                    var number = Convert.ToInt32(arg.Remove(0, 13));
                    Console.WriteLine($"Top {number} Constituencies:");
                    GetTop("constituencies", number);
                }
                    
                Console.WriteLine("");
            }
        }

        private static void GetTop(string ofWhat, int amount)
        {
            if (ofWhat == "countries")
            {
                var sorted = _petition.Countries.OrderByDescending(op => op.VoteCount).Take(amount).ToList();
                var count = 0;
                foreach (var item in sorted)
                {
                    count++;
                    Console.WriteLine($"{count}. {item.Name} with {item.VoteCount} votes placed.");
                }
            }
            else if (ofWhat == "constituencies")
            {
                var sorted = _petition.Constituencies.OrderByDescending(op => op.VoteCount).Take(amount).ToList();

                var count = 0;
                foreach (var item in sorted)
                {
                    count++;
                    Console.WriteLine($"{count}. {item.Name}({item.MP}) with {item.VoteCount} votes placed.");
                }
            }
        }

        private static void GetTotals(string where)
        {
            if (where == "outsideUK")
            {
                Console.Write("Getting total votes outside the UK: ");
                var count = 0;
                foreach (var country in _petition.Countries)
                {
                        if(country.Code != "GB")
                            count += country.VoteCount;

                }

                Console.Write($"{count}.\n");
            }
            else if (where == "insideUK")
            {
                Console.Write("Getting total votes inside the UK: ");
                var count = 0;
                foreach (var constituency in _petition.Constituencies)
                {
                    count += constituency.VoteCount;
                }

                Console.Write($"{count}.\n");
            }
        }
    }
}
