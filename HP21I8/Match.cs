using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP21I8
{
    public record Match(DateTime Date,
                    string Season,
                    string TeamHome,
                    string TeamAway,
                    int SetsHome,
                  int SetsAway);

}
