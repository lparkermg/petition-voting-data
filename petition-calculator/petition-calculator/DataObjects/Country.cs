using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petition_calculator.DataObjects
{
    public class Country
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public int VoteCount { get; private set; }

        public Country(string name, string code, int votes)
        {
            Name = name;
            Code = code;
            VoteCount = votes;
        }
    }
}
