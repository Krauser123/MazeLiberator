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
            Hard= 3
        }

        #region Propiedades
        Difficulty selectedDifficulty = Difficulty.Medium;

        int pnlSize;
        int iTmA;
        int increase;
        int iBtnPerRow;
        int iTotalButtons;

        //Images
        Bitmap TileNone;
        Bitmap TileBlock;
        Bitmap TileWay;

        Bitmap InitialTile;
        Bitmap EndTile;


        bool initialTileHasSet = false;
        bool finalTitleHasSet = false;

        Button btnInitial;
        Button btnFinal;

        #endregion

        public FormMain()
        {
            InitializeComponent();
        }

        #region Eventos
        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            pnlDebug.Visible = true;
#endif

            StyleImage();
            SetDifficulty();
            CreateControls();
        }

        private void TileButton_Click(object sender, System.EventArgs e)
        {
            //Get the button that fire the event
            Button button = (Button)sender;

            if (button.BackgroundImage == null)
            {
                //Disconect for use other format in empty blocks
                button.BackgroundImage = TileBlock;
                button.Tag = "x";
            }
            else if (button.BackgroundImage == TileBlock)
            {
                button.BackgroundImage = InitialTile;
                button.Tag = "1";

                //Set Initial tile
                initialTileHasSet = true;
                btnInitial = button;
            }
            else if (button.BackgroundImage == InitialTile)
            {
                button.BackgroundImage = EndTile;
                button.Tag = "2";

                //Set end tile
                finalTitleHasSet = true;
                btnFinal = button;

            }
            else
            {
                button.BackgroundImage = null;
            }
        }

        private void CleanPanel()
        {
            foreach (Control item in mainPanel.Controls)
            {
                if (item.GetType() == typeof(Button))
                {

                    item.BackgroundImage = null;
                }
            }

            initialTileHasSet = false;
            finalTitleHasSet = false;
        }


        private void SolveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolverLabyrinth();
        }

        //Create labyrinth
        private void LabyrinthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CleanPanel();

            Random randomize = new Random();

            //Refer initial button
            int initialButtonIndex = randomize.Next(0, iTotalButtons);
            btnInitial = (Button)mainPanel.Controls[initialButtonIndex];
            btnInitial.BackgroundImage = InitialTile;
            btnInitial.Tag = "1";
            initialTileHasSet = true;

            #if DEBUG
            lblDebug.Text = "Initial Index " + initialButtonIndex.ToString();
            #endif

            //Refer final button
            int finalButtonIndex;
            do
            {
                finalButtonIndex = randomize.Next(0, iTotalButtons);
            }
            // We need check that we don't use the same button and also that we have a minimun difference
            while (finalButtonIndex == initialButtonIndex || Math.Abs(initialButtonIndex - finalButtonIndex) < 45);

            btnFinal = (Button)mainPanel.Controls[finalButtonIndex];
            btnFinal.BackgroundImage = EndTile;
            btnFinal.Tag = "2";
            finalTitleHasSet = true;

            #if DEBUG
            lblDebug.Text += "Final Index " + finalButtonIndex.ToString();
#endif

            //Generate walls
            int numOfWalls = 120;
            int wallCount = 0;
            do
            {
                int wallIndex = randomize.Next(0, iTotalButtons);
                Button btnWall = (Button)mainPanel.Controls[wallIndex];
                btnWall.BackgroundImage = TileBlock;
                btnWall.Tag = "x";

                wallCount++;
            } while (wallCount < numOfWalls);
        }

        #endregion Events

        private void SetDifficulty()
        {
            switch (selectedDifficulty)
            {
                case Difficulty.Easy:
                    pnlSize = mainPanel.Height;
                    //button size
                    increase = 50;
                    iTmA = increase;
                    //Set number of button per row
                    iBtnPerRow = (pnlSize / increase);
                    break;

                case Difficulty.Medium:

                    pnlSize = mainPanel.Height;
                    //button size
                    increase = 25;
                    iTmA = increase;
                    //Set number of button per row
                    iBtnPerRow = (pnlSize / increase);
                    break;

                case Difficulty.Hard:
                    pnlSize = mainPanel.Height;
                    //button size
                    increase = 10;
                    iTmA = increase;
                    //Set number of button per row
                    iBtnPerRow = (pnlSize / increase);
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// Set styles and images to controls
        /// </summary>
        private void StyleImage()
        {
            string sImagePath = "./Images/Style1.png";

            int w = 48 / 3;
            int h = 16;

            try
            {
                Bitmap styleOriginal = new Bitmap(sImagePath);

                Rectangle srcRectNone = new Rectangle(0, 0, w, h);
                Rectangle srcRectBlock = new Rectangle(w, 0, w, h);
                Rectangle srcRectTileWay = new Rectangle(w + w, 0, w, h);

                TileNone = (Bitmap)styleOriginal.Clone(srcRectNone, styleOriginal.PixelFormat);
                TileBlock = (Bitmap)styleOriginal.Clone(srcRectBlock, styleOriginal.PixelFormat);
                TileWay = (Bitmap)styleOriginal.Clone(srcRectTileWay, styleOriginal.PixelFormat);

                sImagePath = "./Images/Style2.png";

                //Initial and End Tile
                styleOriginal = new Bitmap(sImagePath);

                srcRectNone = new System.Drawing.Rectangle(0, 0, w, h);
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
            //Activamos el panel
            mainPanel.Enabled = true;

            int iCountParcial = 0;

            int iLocationA = 0;
            int iLocationB = 0;

            iTotalButtons = (pnlSize / increase) * iBtnPerRow;

            for (int i = 0; i <= iTotalButtons; i++)
            {
                Button buttonToAsign = new Button
                {
                    Name = "btnMaze_" + i,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                //Add events to button
                buttonToAsign.Click += new EventHandler(TileButton_Click);

                mainPanel.Controls.Add(buttonToAsign);
                Point ptoLocation = new Point(iLocationA, iLocationB);
                buttonToAsign.Location = ptoLocation;
                buttonToAsign.Text = string.Empty;
                buttonToAsign.FlatStyle = FlatStyle.Flat;
                buttonToAsign.Tag = "";

                //Set Size
                buttonToAsign.Size = new Size(iTmA, iTmA);

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


        public void SolverLabyrinth()
        {
            if (IsMazeRight() == null)
            {
                //Transform buttons to array
                var arr = MainReSolver.GetMazeArrayFromPanel(panelWithButtons: mainPanel, iBtnPerRow, iBtnPerRow);
                //MainReSolver.MainSolver(arr);
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

            if (initialTileHasSet == false || btnInitial.BackgroundImage != InitialTile)
            {
                warningMessage = "A starting point is required (Green)" + Environment.NewLine;
            }

            if (finalTitleHasSet == false || btnFinal.BackgroundImage != EndTile)
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
        public void DrawWay(List<Button> Camino)
        {
            foreach (Button btnActualCamino in Camino)
            {
                btnActualCamino.BackgroundImage = TileWay;
            }
        }

        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string caption = "Tile Green: Entrance point." + Environment.NewLine + "Tile Black: Escape point" + Environment.NewLine + "Tile Blue: Wall";
            MessageBox.Show(caption, "Help MazeLiberator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
