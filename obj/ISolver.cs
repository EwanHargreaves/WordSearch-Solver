using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch_Solver
{
    interface ISolver
    {
        abstract void solve();

        abstract void writeResults(string loadTime, string solveTime, string filepath);
    }
}
