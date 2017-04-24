using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Boxes_walls_and_keys
{
    public class Player
    {
        public Point location_current = new Point(0, 0);
        public Map current_level = new Map();
        public PictureBox pb_player = new PictureBox();
        public int taken_keys = 0;
        public int level_number = 0;

        public Player()
        {
            pb_player.Image = Image.FromFile("..\\Images\\player_floor.png");
            pb_player.Size = new Size(SO.sq, SO.sq);
        }
        
        public void set_location(int x = 0, int y = 0)
        {
            location_current = new Point(x, y);
        }
        public void move(SO.move_direction direction)
        {
            switch (Convert.ToInt32(direction))
            {
                case 0:
                    if (current_level.Squares[location_current.X, location_current.Y - 1].stand)
                        location_current.Y--;
                    break;
                case 1:
                    if (current_level.Squares[location_current.X + 1, location_current.Y].stand)
                        location_current.X++;
                    break;
                case 2:
                    if (current_level.Squares[location_current.X, location_current.Y + 1].stand)
                        location_current.Y++;
                    break;
                case 3:
                    if (current_level.Squares[location_current.X - 1, location_current.Y].stand)
                        location_current.X--;
                    break;
            }
        }
        public void move_back(SO.move_direction direction)
        {
            move((SO.move_direction)(((Convert.ToInt32(direction) + 2) % 4)));//move in the oposite direction
        }
        public void reset4lvl()
        {
            taken_keys = 0;
        }
    }
}
