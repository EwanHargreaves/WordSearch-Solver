using System;
using System.Collections.Generic;

namespace Wordsearch_Solver
{
    internal class SimpleSolver : BaseSolver
    {
        private List<string> dictionary; 

        public SimpleSolver(WordsearchData _wordsearch)
        {
            grid = _wordsearch.grid;
            length = _wordsearch.length;
            dictionary = _wordsearch.simpleDictionary;
            method = "Simple";
            notFound.AddRange(dictionary);
        }

        public override void Solve()
        {
            for (int y = 0; y < length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    SearchCell(x, y);
                    gridCellsVisited++;
                }
            }
        }

        void SearchCell(int x, int y)
        {
            char letter = grid[x + (y * length)];

            foreach (string word in dictionary)
            {
                dictionaryEntriesVisited++;
                if (letter == word[0])
                {
                    List<int> dirToCheck = PossibleDirections(x, y, word);

                    if (dirToCheck == new List<int>())
                        continue;

                    foreach (int direction in dirToCheck)
                    {
                        IsWord(x, y, direction, word);
                    }
                }
            }
        }

        void IsWord(int x, int y, int direction, string word)
        {
            for (int depth = 1; depth < word.Length; depth++)
            {
                int[] dir = GetDirection(direction);
                int newX = x + (dir[0] * depth);
                int newY = y + (dir[1] * depth);

                if (!ValidPos(newX, newY))
                    break;

                if (grid[newX + (newY * length)] != word[depth])
                    break;

                if (depth + 1 == word.Length)
                {
                    notFound.Remove(word);
                    string location = $"{x} {y} ";
                    found.Add($"{location}{word}");
                }
            }
        }

        List<int> PossibleDirections(int x, int y, string word)
        {
            List<int> check = new List<int>();

            for (int i = 1; i <= 8; i++)
            {
                int[] direction = GetDirection(i);
                char? next = NextLetterInDirection(direction, x, y, 1);
                if (next == null)
                    continue;

                if (next == word[1])
                    check.Add(i);
            }
            return check;
        }
    }
}
