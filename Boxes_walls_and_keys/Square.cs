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
    public class Square
    {
        //parameters
        public int square_id;
        public bool stand;
        public string room;
        public int event_number;
        public PictureBox picture_box = new PictureBox();

        //vision
        public int room_number;
        public int vision;//0-none, 1:14 - partly, 15-full
        Image real_image;

        public Square(bool stand = true)
        {
            this.stand = stand;//need?
        }
        public Square(Image image, bool stand = true, int square_id = 0, int event_number = 0, string room = "None")
        {
            this.room = room;
            this.stand = stand;
            this.event_number = event_number;
            this.square_id = square_id;

            create_picture(image);
        }

        public void create_picture(Image image)
        {
            //new picturebox destry everyything because it forces to map.controls.add(..)
            picture_box.Size = new Size(SO.sq, SO.sq);
            picture_box.Image = image;
            real_image = image;

        }
        public void copy(Square sq)
        {
            stand = sq.stand;
            room = sq.room;
            event_number = sq.event_number;
            square_id = sq.square_id;
            create_picture(sq.picture_box.Image);
        }
        public void set_vision(int vision = 0, bool edge = false)
        {
            if (!edge)
                this.vision = vision;

        }
        public void set_shadow(int cover = 0)
        {
            if (cover == 0)
                picture_box.Image = SO.full_shadow;
            else if (cover == 15)
                picture_box.Image = real_image;
            else
            {
                if (square_id == 1)
                    picture_box.Image = Image.FromFile("..\\Images\\Shadows\\floor1\\floor1_sh_" + vision.ToString() + ".png");
                else if (square_id == 2)
                    picture_box.Image = Image.FromFile("..\\Images\\Shadows\\wall\\wall_sh_" + vision.ToString() + ".png");
                else if (square_id == 5)
                    picture_box.Image = Image.FromFile("..\\Images\\Shadows\\box_floor\\box_floor_sh_" + vision.ToString() + ".png");
                else if (square_id == 6)
                    picture_box.Image = Image.FromFile("..\\Images\\Shadows\\books\\books_sh_" + vision.ToString() + ".png");
                else
                picture_box.Image = Image.FromFile("..\\Images\\None_Image.png");
            }

        }
    }
}
