using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wordsearch_Solver
{
    interface ISolver
    {
        abstract void Solve();

        abstract void WriteResults(string loadTime, string solveTime, string filepath);
    }
}
