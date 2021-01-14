using System.Windows.Forms;

namespace MazeLiberator
{
    public class TileButton : Button
    {
        public bool IsWallTile { get; private set; }
        public bool IsInitialTile { get; private set; }
        public bool IsFinalTile { get; private set; }

        public void SetAsWallTitle()
        {
            IsWallTile = true;
            IsInitialTile = false;
            IsFinalTile = false;
        }

        public void SetAsInitialTitle()
        {
            IsWallTile = false;
            IsInitialTile = true;
            IsFinalTile = false;
        }

        public void SetAsFinalTitle()
        {
            IsWallTile = false;
            IsInitialTile = false;
            IsFinalTile = true;
        }

        public void SetAsEmptyTitle()
        {
            IsWallTile = false;
            IsInitialTile = false;
            IsFinalTile = false;
            this.BackgroundImage = null;
        }

        public bool IsEmptyTitle()
        {
            return !IsWallTile && !IsInitialTile && !IsFinalTile;
        }
    }
}
