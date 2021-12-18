using System;
using System.Drawing;
using System.Windows.Forms;

namespace MazeLiberator
{
    public partial class FormMain : Form
    {
        Difficulty selectedDifficulty = Difficulty.Medium;

        int PnlSize;
        int increase;
        int NumButtonsPerRow;
        int TotalNumOfButtons;

        readonly string ButtonPrefix = "btnMaze_";

        //Images
        Bitmap TileBlock;
        Bitmap TileWay;
        Bitmap InitialTile;
        Bitmap EndTile;

        TileButton btnInitial;
        TileButton btnFinal;

        Random randomize;

        public FormMain()
        {
            InitializeComponent();
            LoadImagesForTiles();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            CreateControls();
        }

        /// <summary>
        /// This event fire each time that user click on any button on panelMain
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileButton_Click(object sender, EventArgs e)
        {
            //Get the button that fire the event
            TileButton btnClicked = (TileButton)sender;
            ChangebuttonState(btnClicked);
        }

        #region ToolStrip Events

        private void NewMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMaze();
        }

        private void SolveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveLabyrinth();
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Click on tiles to change his behavior." + Environment.NewLine + Environment.NewLine
                            + "Green Tile: Entrance point." + Environment.NewLine +
                            "Race Flag Tile: Escape point" + Environment.NewLine +
                            "X Gray Tile: Wall" + Environment.NewLine +
                            "Red Tile: Solve attemps";

            MessageBox.Show(caption, "MazeLiberator Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EasylToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedDifficulty = Difficulty.Easy;
            CommonToolStripAction(easylToolStripMenuItem);
        }

        private void MediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedDifficulty = Difficulty.Medium;
            CommonToolStripAction(mediumToolStripMenuItem);
        }

        private void HardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedDifficulty = Difficulty.Hard;
            CommonToolStripAction(hardToolStripMenuItem);
        }

        private void CommonToolStripAction(ToolStripMenuItem toolStripMenuItemToMark)
        {
            UnselectAllDifficultyToolStrip();
            toolStripMenuItemToMark.Checked = true;
            btnInitial = null;
            btnFinal = null;
            CreateControls();
        }

        #endregion

        /// <summary>
        /// Handle the 4 types of tile (Empty, Wall, Initial and Final)
        /// </summary>
        /// <param name="btnClicked"></param>
        private void ChangebuttonState(TileButton btnClicked)
        {
            //If empty can set as Wall
            if (btnClicked.IsEmptyTitle())
            {
                //Set as wall
                btnClicked.BackgroundImage = TileBlock;
                btnClicked.SetAsWallTitle();
            }
            else if (btnClicked.IsWallTile && btnInitial == null)
            {
                //Check if exist a previous Initial Tile
                if (btnInitial != null)
                {
                    btnInitial.BackgroundImage = null;
                    btnInitial.SetAsEmptyTitle();
                }

                //Set as Initial Tile
                btnClicked.BackgroundImage = InitialTile;
                btnClicked.SetAsInitialTitle();
                btnInitial = btnClicked;
            }
            else if (btnClicked.IsInitialTile)
            {
                btnClicked.SetAsEmptyTitle();
                btnInitial = null;
            }
            else if (btnClicked.IsFinalTile)
            {
                btnClicked.SetAsEmptyTitle();
                btnFinal = null;
            }
            else if (btnClicked.IsWallTile && btnFinal == null)
            {
                //Check if exist a previous Final Tile
                if (btnFinal != null)
                {
                    btnFinal.BackgroundImage = null;
                    btnFinal.SetAsEmptyTitle();
                }

                btnClicked.BackgroundImage = EndTile;
                btnClicked.SetAsFinalTitle();
                btnFinal = btnClicked;
            }
            else
            {
                btnClicked.SetAsEmptyTitle();
            }
        }

        /// <summary>
        /// Create maze
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateMaze()
        {
            CleanPanel();
            randomize = new Random(DateTime.Now.Millisecond);

            //Generat Start and End points
            GenerateInitialAndFinalButton();

            //Generate walls
            GenerateWalls();
        }

        /// <summary>
        /// Generate initial and final tiles
        /// </summary>
        private void GenerateInitialAndFinalButton()
        {
            //Generate initial button
            int initialButtonIndex = randomize.Next(0, TotalNumOfButtons);
            btnInitial = (TileButton)mainPanel.Controls[initialButtonIndex];
            btnInitial.BackgroundImage = InitialTile;
            btnInitial.SetAsInitialTitle();

            //Generate final button
            int finalButtonIndex;
            do
            {
                finalButtonIndex = randomize.Next(0, TotalNumOfButtons);
            }
            // We need check that we don't use the same button and also that we have a minimun difference
            while (finalButtonIndex == initialButtonIndex || Math.Abs(initialButtonIndex - finalButtonIndex) < 45);

            btnFinal = (TileButton)mainPanel.Controls[finalButtonIndex];
            btnFinal.BackgroundImage = EndTile;
            btnFinal.SetAsFinalTitle();
        }

        /// <summary>
        /// Generate walls randomly, not checks if the path can be solved
        /// </summary>
        private void GenerateWalls()
        {
            int numOfWalls = TotalNumOfButtons / 3;
            int wallCount = 0;
            do
            {
                int wallIndex = randomize.Next(0, TotalNumOfButtons);
                TileButton btnWall = (TileButton)mainPanel.Controls[wallIndex];
                if (btnWall.IsEmptyTitle())
                {
                    btnWall.BackgroundImage = TileBlock;
                    btnWall.SetAsWallTitle();
                    wallCount++;
                }
            }
            while (wallCount < numOfWalls);
        }

        /// <summary>
        /// Clean controls in panel
        /// </summary>
        private void CleanPanel()
        {
            foreach (Control item in mainPanel.Controls)
            {
                if (item.GetType() == typeof(TileButton))
                {
                    item.BackgroundImage = null;
                }
            }

            btnInitial = null;
            btnFinal = null;
        }

        /// <summary>
        /// According to the difficulty We change the size of button (by the way, the number of button generated)
        /// </summary>
        private void SetSetupByDifficulty()
        {
            switch (selectedDifficulty)
            {
                case Difficulty.Easy:
                    increase = 50;
                    break;

                case Difficulty.Medium:
                    increase = 25;
                    break;

                case Difficulty.Hard:
                    increase = 20;
                    break;
            }

            //Set number of button per row
            PnlSize = mainPanel.Height;
            NumButtonsPerRow = (PnlSize / increase);
        }

        /// <summary>
        /// Set styles and images to controls
        /// </summary>
        private void LoadImagesForTiles()
        {
            string sImagePath = "./Images/Style1.png";

            int w = 48 / 3;
            int h = 16;

            try
            {
                Bitmap styleOriginal = new Bitmap(sImagePath);

                Rectangle srcRectBlock = new Rectangle(w, 0, w, h);
                Rectangle srcRectTileWay = new Rectangle(w + w, 0, w, h);

                TileBlock = (Bitmap)styleOriginal.Clone(srcRectBlock, styleOriginal.PixelFormat);
                TileWay = (Bitmap)styleOriginal.Clone(srcRectTileWay, styleOriginal.PixelFormat);

                sImagePath = "./Images/Style2.png";

                //Initial and End Tile
                styleOriginal = new Bitmap(sImagePath);

                //srcRectNone = new System.Drawing.Rectangle(0, 0, w, h);
                srcRectBlock = new Rectangle(w, 0, w, h);
                srcRectTileWay = new Rectangle(w + w, 0, w, h);

                InitialTile = (Bitmap)styleOriginal.Clone(srcRectBlock, styleOriginal.PixelFormat);
                EndTile = (Bitmap)styleOriginal.Clone(srcRectTileWay, styleOriginal.PixelFormat);
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while loading images", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Create and asign controls to main form
        /// </summary>
        private void CreateControls()
        {
            //Get setup for selected difficulty
            SetSetupByDifficulty();

            //Clean panel and set enabled true
            mainPanel.Controls.Clear();
            mainPanel.Enabled = true;

            int iCountParcial = 0;
            int iLocationA = 0;
            int iLocationB = 0;
            int sideSizeForButton = increase;

            //Get number of button to generate
            TotalNumOfButtons = (PnlSize / increase) * NumButtonsPerRow;

            for (int i = 0; i < TotalNumOfButtons; i++)
            {
                TileButton btnToAsign = new TileButton
                {
                    Name = ButtonPrefix + i,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                //Add events to button
                btnToAsign.Click += new EventHandler(TileButton_Click);

                btnToAsign.Location = new Point(iLocationA, iLocationB);
                btnToAsign.Text = string.Empty;
                btnToAsign.FlatStyle = FlatStyle.Flat;

                //Set Size
                btnToAsign.Size = new Size(sideSizeForButton, sideSizeForButton);

                //Add to the panel
                mainPanel.Controls.Add(btnToAsign);

                //Increase count of button set in panel
                iCountParcial++;

                //Increase referencial location point
                if (iCountParcial < NumButtonsPerRow)
                {
                    iLocationA += increase;
                }
                else
                {
                    iCountParcial = 0;
                    iLocationA = 0;
                    iLocationB += increase;
                }
            }
        }

        /// <summary>
        /// Launch the solver for current maze
        /// </summary>
        private void SolveLabyrinth()
        {
            if (IsMazeRight() == null)
            {
                //Transform buttons to array
                var arr = MainReSolver.GetMazeArrayFromPanel(panelWithButtons: mainPanel, NumButtonsPerRow, NumButtonsPerRow);
                arr = MainReSolver.BadMainSolver(arr);

                //DrawSolved
                DrawSolvedArray(arr);
            }
        }

        private void DrawSolvedArray(int[][] arr)
        {
            int counter;

            for (int row = 0; row < NumButtonsPerRow; row++)
            {
                for (int column = 0; column < NumButtonsPerRow; column++)
                {
                    if (row == 0)
                    {
                        counter = row + column;
                    }
                    else
                    {
                        counter = (row * 10) + column;
                    }

                    //Get current button
                    var button = (TileButton)mainPanel.Controls[counter];
                    var valueInArray = arr[row][column];
                    if (valueInArray > 1)
                    {
                        button.BackgroundImage = TileWay;
                    }
                }
            }
        }

        /// <summary>
        /// Check if we have start and end tiles
        /// </summary>
        /// <returns></returns>
        private string IsMazeRight()
        {
            string warningMessage = null;

            if (btnInitial == null || btnInitial.BackgroundImage != InitialTile)
            {
                warningMessage = "A starting point is required (Green Tile)" + Environment.NewLine;
            }

            if (btnFinal == null || btnFinal.BackgroundImage != EndTile)
            {
                warningMessage += "End point required(Race Flag Tile)";
            }

            if (warningMessage != null)
            {
                MessageBox.Show(warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return warningMessage;
        }

        /// <summary>
        /// Unchecked all toolStrip related to difficulty
        /// </summary>
        private void UnselectAllDifficultyToolStrip()
        {
            easylToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            hardToolStripMenuItem.Checked = false;
        }
    }
}
