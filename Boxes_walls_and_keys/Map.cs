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

    public class Map
    {
        public Square[,] Squares = new Square[SO.map_wh.X, SO.map_wh.Y];//[colum,row]
        public Point start_position;

        public Map()
        {
            create_map();
        }
        public void create_map()
        {
            for (int i = 0; i < SO.map_wh.X; i++)
                for (int j = 0; j < SO.map_wh.Y; j++)
                {
                    Squares[i, j] = new Square(Image.FromFile("..\\Images\\None_image.png"));
                    Squares[i, j].picture_box.Location = new Point(i * 64, j * 64);
                }
        }
        public void cover_map()
        {
            for (int i = 0; i < SO.map_wh.X; i++)
                for (int j = 0; j < SO.map_wh.Y; j++)
                    Squares[i, j].set_vision();
        }
        public void convert_file2map(string filename)
        {
            StreamReader level_file = new StreamReader(filename);
            for (int j = 0; j < SO.map_wh.Y; j++)
            {
                string[] line = level_file.ReadLine().Split();
                for (int i = 0; i < SO.map_wh.X; i++)
                    convert_type(ref Squares[i, j], Convert.ToInt32(line[i]));
            }
            {
                string[] line = level_file.ReadLine().Split();
                start_position = new Point(Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
            }
            for (int j = 0; j < SO.map_wh.Y; j++)
            {
                string[] line = level_file.ReadLine().Split();
                for (int i = 0; i < SO.map_wh.X; i++)
                    Squares[i, j].room_number = Convert.ToInt32(line[i]);
            }
            level_file.Close();
        }
        public void convert_type(ref Square Sq, int type, string room = "None")
        {
            string temp_room = Sq.room;
            Point location = Sq.picture_box.Location;

            Sq.copy(SO.square_types[type]);
            Sq.picture_box.Location = new Point(location.X, location.Y);

            if (room == "None")
                Sq.room = temp_room;
        }
        public void check_vision(Point location, bool force2check = false)
        {
            if (Squares[location.X, location.Y].room_number == 1 || force2check)
            {
                set_vision(location);
                set_shadow();
            }
        }
        public void set_vision(Point location)
        {
            {
                Squares[location.X, location.Y].set_vision(15);//center
                set_vision_room(new Point(location.X, location.Y - 1));
                set_vision_room(new Point(location.X + 1, location.Y));
                set_vision_room(new Point(location.X, location.Y + 1));
                set_vision_room(new Point(location.X - 1, location.Y));
                set_vision_edges();
            }

        }
        public void set_vision_room(Point location)
        {
            int room = Squares[location.X, location.Y].room_number;
            int vision = 15;

            if (room != 0 && room != 1)
                for (int i = 0; i < SO.map_wh.X; i++)
                    for (int j = 0; j < SO.map_wh.Y; j++)
                        if (Squares[i, j].room_number == room)
                            Squares[i, j].set_vision(vision);//center

        }
        public void set_vision_edges()
        {
            for (int i = 0; i < SO.map_wh.X; i++)
                for (int j = 0; j < SO.map_wh.Y; j++)
                    set_vision_edges_point(i, j);
        }
        public bool check_in_map(int x, int y)
        {
            return x >= 0 && y >= 0 && x < SO.map_wh.X && y < SO.map_wh.Y;
        }
        public void set_vision_edges_point(int x, int y)//worst function ever
        {
            bool[] corners = new bool[4] { false, false, false, false };//clockwise from top roght

            if (Squares[x, y].vision != 15)
            {
                //set corners
                if (check_in_map(x - 1, y - 1) && Squares[x - 1, y - 1].vision == 15)
                    corners[3] = true;
                if (check_in_map(x - 1, y) && Squares[x - 1, y].vision == 15)
                {
                    corners[3] = true;
                    corners[2] = true;
                }
                if (check_in_map(x - 1, y + 1) && Squares[x - 1, y + 1].vision == 15)
                    corners[2] = true;
                if (check_in_map(x, y - 1) && Squares[x, y - 1].vision == 15)
                {
                    corners[3] = true;
                    corners[0] = true;
                }
                if (check_in_map(x, y + 1) && Squares[x, y + 1].vision == 15)
                {
                    corners[2] = true;
                    corners[1] = true;
                }
                if (check_in_map(x + 1, y - 1) && Squares[x + 1, y - 1].vision == 15)
                    corners[0] = true;
                if (check_in_map(x + 1, y) && Squares[x + 1, y].vision == 15)
                {
                    corners[0] = true;
                    corners[1] = true;
                }
                if (check_in_map(x + 1, y + 1) && Squares[x + 1, y + 1].vision == 15)
                    corners[1] = true;


                //set vision
                if (corners[0] == false && corners[1] == false && corners[2] == false && corners[3] == false)
                    Squares[x, y].vision = 0;

                else if (corners[0] == false && corners[1] == false && corners[2] == true && corners[3] == false)
                    Squares[x, y].vision = 1;
                else if (corners[0] == false && corners[1] == false && corners[2] == false && corners[3] == true)
                    Squares[x, y].vision = 2;
                else if (corners[0] == true && corners[1] == false && corners[2] == false && corners[3] == false)
                    Squares[x, y].vision = 3;
                else if (corners[0] == false && corners[1] == true && corners[2] == false && corners[3] == false)
                    Squares[x, y].vision = 4;

                else if (corners[0] == false && corners[1] == true && corners[2] == true && corners[3] == false)
                    Squares[x, y].vision = 5;
                else if (corners[0] == false && corners[1] == false && corners[2] == true && corners[3] == true)
                    Squares[x, y].vision = 6;
                else if (corners[0] == true && corners[1] == false && corners[2] == false && corners[3] == true)
                    Squares[x, y].vision = 7;
                else if (corners[0] == true && corners[1] == true && corners[2] == false && corners[3] == false)
                    Squares[x, y].vision = 8;
                else if (corners[0] == true && corners[1] == false && corners[2] == true && corners[3] == false)
                    Squares[x, y].vision = 9;
                else if (corners[0] == false && corners[1] == true && corners[2] == false && corners[3] == true)
                    Squares[x, y].vision = 10;

                else if (corners[0] == true && corners[1] == true && corners[2] == false && corners[3] == true)
                    Squares[x, y].vision = 11;
                else if (corners[0] == true && corners[1] == true && corners[2] == true && corners[3] == false)
                    Squares[x, y].vision = 12;
                else if (corners[0] == false && corners[1] == true && corners[2] == true && corners[3] == true)
                    Squares[x, y].vision = 13;
                else if (corners[0] == true && corners[1] == false && corners[2] == true && corners[3] == true)
                    Squares[x, y].vision = 14;
                else
                    Squares[x, y].vision = 15;
            }
        }
        public void set_shadow()
        {
            for (int i = 0; i < SO.map_wh.X; i++)
                for (int j = 0; j < SO.map_wh.Y; j++)
                    Squares[i, j].set_shadow(Squares[i, j].vision);
        }
    }
}
//public void check_vision(Point location)
//{
//    if (Squares[location.X, location.Y].room_number == 1)
//    {
//        shadow_check_edges(location.X, location.Y, 13);
//        check_vision_room(new Point(location.X, location.Y - 1));
//        check_vision_room(new Point(location.X + 1, location.Y));
//        check_vision_room(new Point(location.X, location.Y + 1));
//        check_vision_room(new Point(location.X - 1, location.Y));
//    }
//}
//public void check_vision_room(Point location)
//{
//    if (Squares[location.X, location.Y].room_number != 0 && Squares[location.X, location.Y].room_number != 1)
//        shadow_room(Squares[location.X, location.Y].room_number, 13);
//}
//public void shadow_room(int room, int cover = 0)
//{
//    for (int i = 0; i < SO.map_wh.X; i++)
//        for (int j = 0; j < SO.map_wh.Y; j++)
//            if (Squares[i, j].room_number == room)
//            {
//                Squares[i, j].shadow(cover);//center
//                shadow_check_edges(i, j, cover);
//            }
//}
//public void shadow_check_edges(int x, int y, int cover = 0)//edges and center
//{
//    if (cover == 0)
//        for (int i = -1; i < 2; i++)
//            for (int j = -1; j < 2; j++)
//                if (x + i > 0 && y + j >= 0 && Squares[x + i, y + 1].vision > 0)
//                    Squares[x + i, y + 1].shadow(cover, true);
//    if (cover == 13)
//        for (int i = -1; i < 2; i++)
//            for (int j = -1; j < 2; j++)
//                if (x + i >= 0 && y + j >= 0 && x + i < SO.map_wh.X && y + j < SO.map_wh.Y && Squares[x + i, y + j].vision < 13)
//                    Squares[x + i, y + j].shadow(cover, true);
//}
//public void shadow_check_edges_images(int x, int y, int cover = 0)//edges and center
//{


