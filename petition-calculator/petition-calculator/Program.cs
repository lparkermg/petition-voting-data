using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petition_calculator
{
    class Program
    {
        private string _petitionJsonUrl = "";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Petition data calculator. By Luke Parker");
            TagsUsing(args);
        }

        private static void TagsUsing(string[] args)
        {
            foreach (var arg in args)
            {
                switch (arg)
                {
                    case ("-country-top-5"):
                        Console.WriteLine("Top Five Countries.");
                        break;
                    case ("-constit-top-5"):
                        Console.WriteLine("Top Five Constituencies.");
                        break;
                    default:
                        break;
                }
            }
        }

        private static void RunCalculations(string[] args)
        {
            //TODO: TotalVotes Outside UK
            GetTotals("outsideUK");
            //TODO: TotalVotes Inside UK
            GetTotals("insideUK");

            foreach (var arg in args)
            {
                switch (arg)
                {
                    case ("-country-top-5"):
                        GetTopFive("countries");
                        break;
                    case ("-constit-top-5"):
                        GetTopFive("constituencies");
                        break;
                    default:
                        break;
                }
            }
        }

        private static void GetTopFive(string ofWhat)
        {
            if (ofWhat == "countries")
            {
                
            }
            else if (ofWhat == "constituencies")
            {
                
            }
        }

        private static void GetTotals(string where)
        {
            if (where == "outsideUK")
            {
                
            }
            else if (where == "insideUK")
            {
                
            }
        }
    }
}
