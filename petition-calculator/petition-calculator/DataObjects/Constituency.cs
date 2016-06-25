using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace petition_calculator.DataObjects
{
    public class Constituency
    {
        public string Name { get; private set; }
        public string OnsCode { get; private set; }
        public string MP { get; private set; }
        public int VoteCount { get; private set; }

        public Constituency(string name, string onsCode, string mp, int votes)
        {
            Name = name;
            OnsCode = onsCode;
            MP = mp;
            VoteCount = votes;
        }
    }
}
