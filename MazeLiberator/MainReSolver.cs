using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MazeLiberator
{
    class MainReSolver
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
                decimal colNumberDec = i / columns;
                var colNumber = (int)Math.Floor(colNumberDec);

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

        static bool IsValidPos(int[][] array, int row, int newRow, int newColumn)
        {
            if (newRow < 0) return false;

            if (newColumn < 0) return false;

            if (newRow >= array.Length) return false;

            if (newColumn >= array[row].Length) return false;
            return true;
        }

        static int Move(int[][] arrayTemp, int rowIndex, int columnIndex, int count, ref int lowest)
        {
            // Copy map so we can modify it and then abandon it.
            int[][] array = new int[arrayTemp.Length][];
            for (int i = 0; i < arrayTemp.Length; i++)
            {
                var row = arrayTemp[i];
                array[i] = new int[row.Length];
                for (int x = 0; x < row.Length; x++)
                {
                    array[i][x] = row[x];
                }
            }

            int value = array[rowIndex][columnIndex];
            if (value >= 1)
            {
                // Try all moves.
                foreach (var movePair in _moves)
                {
                    int newRow = rowIndex + movePair[0];
                    int newColumn = columnIndex + movePair[1];
                    if (IsValidPos(array, rowIndex, newRow, newColumn))
                    {
                        int testValue = array[newRow][newColumn];
                        if (testValue == 0)
                        {
                            array[newRow][newColumn] = value + 1;
                            // Try another move.
                            Move(array, newRow, newColumn, count + 1, ref lowest);
                        }
                        else if (testValue == -3)
                        {
                            // End point.
                            // ... Could print the optimal maze solution here.
                            if (count + 1 < lowest)
                            {
                                lowest = count + 1;
                            }
                            return 1;
                        }
                    }
                }
            }
            return -1;
        }

        public static void MainSolver(int[][] array)
        {
            // Get start position.
            for (int i = 0; i < array.Length; i++)
            {
                var row = array[i];
                for (int x = 0; x < row.Length; x++)
                {
                    Debug.WriteLine(i + " - " + x + " -> " + row[x].ToString());
                    // Start square is here.
                    if (row[x] == 1)
                    {
                        int lowest = int.MaxValue;
                        Move(array, i, x, 0, ref lowest);
                        MessageBox.Show($"Optimal moves: {lowest}");
                    }
                }
            }
        }







        static int ModifyPath(int[][] array)
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
                                    // Travel to a new square for the first time.
                                    // ... Record the count of total moves to it.
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
                    MessageBox.Show($"The maze can be solved in {count} moves");
                    break;
                }
                else if (result == -1)
                {
                    MessageBox.Show($"Sorry but the maze cannot be solved. Non-exit path found in: {count} moves");
                    break;
                }
                count++;
            }
        }
    }
}
