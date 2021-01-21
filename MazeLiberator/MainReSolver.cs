using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MazeLiberator
{
    /// <summary>
    /// Original by: https://www.dotnetperls.com/maze
    /// </summary>
    internal static class MainReSolver
    {
        static int[][] _moves = {
                                new int[] { -1, 0 },
                                new int[] { 0, -1 },
                                new int[] { 0, 1 },
                                new int[] { 1, 0 }
        };

        public static int[][] GetMazeArrayFromPanel(Panel panelWithButtons, int columns, int rows)
        {
            //panelWithButtons
            int[][] array = new int[columns][];

            for (int i = 0; i < panelWithButtons.Controls.Count - 1; i++)
            {
                var rowNumber = Math.Abs(i % rows);
                int colNumberDec = i / columns;
                var colNumber = colNumberDec;

                //Initialise row array
                if (array[rowNumber] == null)
                {
                    array[rowNumber] = new int[rows];
                }

                int valueToSet = 0;
                var button = (TileButton)panelWithButtons.Controls[i];

                if (button.IsWallTile)
                {
                    valueToSet = -1;
                }
                else if (button.IsInitialTile)
                {
                    valueToSet = 1;
                }
                else if (button.IsFinalTile)
                {
                    valueToSet = -3;
                }

                array[colNumber][rowNumber] = valueToSet;

                Debug.WriteLine(colNumber + " - " + rowNumber + " -> " + valueToSet.ToString());
            }

            return array;
        }

        private static bool IsValidPos(int[][] array, int row, int newRow, int newColumn)
        {
            if (newRow < 0) return false;

            if (newColumn < 0) return false;

            if (newRow >= array.Length) return false;

            if (newColumn >= array[row].Length) return false;
            return true;
        }

        private static int ModifyPath(int[][] array)
        {
            // Loop over rows and then columns.
            for (int rowIndex = 0; rowIndex < array.Length; rowIndex++)
            {
                var row = array[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    // Find a square we have traveled to.
                    int value = array[rowIndex][columnIndex];
                    if (value >= 1)
                    {
                        // Try all possible moves from this square.
                        foreach (var movePair in _moves)
                        {
                            // Move to a valid square.
                            int newRow = rowIndex + movePair[0];
                            int newColumn = columnIndex + movePair[1];
                            if (IsValidPos(array, rowIndex, newRow, newColumn))
                            {
                                int testValue = array[newRow][newColumn];
                                if (testValue == 0)
                                {
                                    // Travel to a new square for the first time and record the count of total moves to it.
                                    array[newRow][newColumn] = value + 1;
                                    // Move has been performed.
                                    return 0;
                                }
                                else if (testValue == -3)
                                {
                                    // We are at our end point.
                                    return 1;
                                }
                            }
                        }
                    }
                }
            }
            // We cannot do anything.
            return -1;
        }

        public static void BadMainSolver(int[][] array)
        {
            int count = 0;

            // Read user input and evaluate maze.
            while (true)
            {
                int result = ModifyPath(array);
                if (result == 1)
                {
                    MessageBox.Show($"The maze can be solved in {count} attempts", "MazeLiberator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
                else if (result == -1)
                {
                    MessageBox.Show($"Sorry but the maze cannot be solved. Non-exit path found in: {count} attempts", "MazeLiberator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
                count++;
            }
        }
    }
}
