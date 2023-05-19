namespace Wordsearch_Solver
{
    interface ISolver
    {
        abstract void Solve();

        abstract void WriteResults(string loadTime, string solveTime, string filepath);
    }
}
