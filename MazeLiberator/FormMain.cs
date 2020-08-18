using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MazeLiberator
{
    public partial class FormMain : Form
    {
        enum Difficulty
        {
            Easy = 1,
            Medium = 2,
            Hard = 3
        }

        Difficulty selectedDifficulty = Difficulty.Medium;

        int pnlSize;
        int increase;
        int iBtnPerRow;
        int iTotalButtons;

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
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadImagesForTiles();
            GetSetupByDifficulty();
            CreateControls();
        }

        private void TileButton_Click(object sender, EventArgs e)
        {
            //Get the button that fire the event
            TileButton btnClicked = (TileButton)sender;

            if (btnClicked.BackgroundImage == null)
            {
                //Set as wall
                btnClicked.BackgroundImage = TileBlock;
                btnClicked.SetAsWallTitle();
            }
            else if ((btnClicked.BackgroundImage == TileBlock || btnClicked.BackgroundImage == InitialTile) && btnInitial == null)
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
            else if ((btnClicked.BackgroundImage == InitialTile || btnClicked.BackgroundImage == TileBlock) && btnFinal == null)
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
                btnClicked.BackgroundImage = null;
            }
        }

        private void changebuttonState(Button sender)
        {
            //Get the button that fire the event
            TileButton btnClicked = (TileButton)sender;


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

        #region ToolStrip Events

        private void NewMazeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateMaze();
        }

        private void SolveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolverLabyrinth();
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Click on tiles to change his behavior." + Environment.NewLine + Environment.NewLine + "Tile Green: Entrance point." + Environment.NewLine + "Tile Black: Escape point" + Environment.NewLine + "Tile Blue: Wall";
            MessageBox.Show(caption, "Help MazeLiberator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EasylToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnselectAllDifficultyToolStrip();
            easylToolStripMenuItem.Checked = true;
            selectedDifficulty = Difficulty.Easy;
        }

        private void MediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnselectAllDifficultyToolStrip();
            mediumToolStripMenuItem.Checked = true;
            selectedDifficulty = Difficulty.Medium;
        }

        private void HardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnselectAllDifficultyToolStrip();
            hardToolStripMenuItem.Checked = true;
            selectedDifficulty = Difficulty.Hard;
        }

        #endregion


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

        private void GenerateInitialAndFinalButton()
        {
            //Generate initial button
            int initialButtonIndex = randomize.Next(0, iTotalButtons);
            btnInitial = (TileButton)mainPanel.Controls[initialButtonIndex];
            btnInitial.BackgroundImage = InitialTile;
            btnInitial.SetAsInitialTitle();

            //Generate final button
            int finalButtonIndex;
            do
            {
                finalButtonIndex = randomize.Next(0, iTotalButtons);
            }
            // We need check that we don't use the same button and also that we have a minimun difference
            while (finalButtonIndex == initialButtonIndex || Math.Abs(initialButtonIndex - finalButtonIndex) < 45);

            btnFinal = (TileButton)mainPanel.Controls[finalButtonIndex];
            btnFinal.BackgroundImage = EndTile;
            btnFinal.SetAsFinalTitle();
        }

        private void GenerateWalls()
        {
            int numOfWalls = iTotalButtons / 3;
            int wallCount = 0;
            do
            {
                int wallIndex = randomize.Next(0, iTotalButtons);
                TileButton btnWall = (TileButton)mainPanel.Controls[wallIndex];
                if (btnWall.IsEmptyTitle())
                {
                    btnWall.BackgroundImage = TileBlock;
                    btnWall.SetAsWallTitle();
                    wallCount++;
                }

            } while (wallCount < numOfWalls);
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
        private void GetSetupByDifficulty()
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
                    increase = 10;
                    break;
            }

            //Set number of button per row
            pnlSize = mainPanel.Height;
            iBtnPerRow = (pnlSize / increase);
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
                srcRectBlock = new System.Drawing.Rectangle(w, 0, w, h);
                srcRectTileWay = new System.Drawing.Rectangle(w + w, 0, w, h);

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
        public void CreateControls()
        {
            //Clean panel and set enabled true
            mainPanel.Controls.Clear();
            mainPanel.Enabled = true;

            int iCountParcial = 0;
            int iLocationA = 0;
            int iLocationB = 0;
            int sideSizeForButton = increase;

            iTotalButtons = (pnlSize / increase) * iBtnPerRow;

            for (int i = 0; i <= iTotalButtons; i++)
            {
                TileButton btnToAsign = new TileButton
                {
                    Name = "btnMaze_" + i,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                //Add events to button
                btnToAsign.Click += new EventHandler(TileButton_Click);

                mainPanel.Controls.Add(btnToAsign);
                Point ptoLocation = new Point(iLocationA, iLocationB);
                btnToAsign.Location = ptoLocation;
                btnToAsign.Text = string.Empty;
                btnToAsign.FlatStyle = FlatStyle.Flat;
                
                //Set Size
                btnToAsign.Size = new Size(sideSizeForButton, sideSizeForButton);

                //Increase count of button set in panel
                iCountParcial++;

                //Increase referencial location point
                if (iCountParcial < iBtnPerRow)
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
        private void SolverLabyrinth()
        {
            if (IsMazeRight() == null)
            {
                //Transform buttons to array
                var arr = MainReSolver.GetMazeArrayFromPanel(panelWithButtons: mainPanel, iBtnPerRow, iBtnPerRow);
                MainReSolver.BadMainSolver(arr);
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
                warningMessage = "A starting point is required (Green)" + Environment.NewLine;
            }

            if (btnFinal == null || btnFinal.BackgroundImage != EndTile)
            {
                warningMessage += "End point required(Black)";
            }

            if (warningMessage != null)
            {
                MessageBox.Show(warningMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return warningMessage;
        }


        //Dibuja el camino basandose en una lista genérica
        private void DrawWay(List<TileButton> Camino)
        {
            foreach (Button btnActualCamino in Camino)
            {
                btnActualCamino.BackgroundImage = TileWay;
            }
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