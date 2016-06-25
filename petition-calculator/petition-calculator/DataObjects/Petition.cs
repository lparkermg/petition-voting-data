using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petition_calculator.DataObjects
{
    public class Petition
    {
        public string Action { get; private set; }
        public string Url { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public List<Country> Countries { get; private set; }
        public List<Constituency> Constituencies { get; private set; }
        public int TotalVotes { get; private set; }

        public Petition(string action, string url,DateTime lastUpdated,int totalVotes, List<Country> countries, List<Constituency> constituencies)
        {
            Action = action;
            Url = url;
            LastUpdated = lastUpdated;
            TotalVotes = totalVotes;
            Countries = countries;
            Constituencies = constituencies;
        }
    }
}
