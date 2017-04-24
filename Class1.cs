using System;

public static class SO //static options
{
    //general
    public const int sq = 64;  //square 64x64
    public const int level_number = 4;
    public static string[] level_filenames = new string[level_number]
    {
            "..\\Maps\\level1.txt",
            "..\\Maps\\level2.txt",
            "..\\Maps\\level3.txt",
            "..\\Maps\\level4.txt"
    };

    //movement
    public enum move_direction { up, right, down, left };
    //public static Point zero_point = new Point(0, 0);//game screen location//not used

    //map
    public static Point map_wh = new Point(25, 15);
    public static int keys_per_level = 3;

    //square types
    public static readonly Square[] square_types = new Square[]
        {
               new Square(Image.FromFile("..\\Images\\None_Image.png"), false, 0), //0 - nothing
               new Square(Image.FromFile("..\\Images\\floor1.png"), true, 1), //1 - floor
               new Square(Image.FromFile("..\\Images\\wall.png"), false, 2), //2- wall
               new Square(Image.FromFile("..\\Images\\player_floor.png"), false, 3), //3 - player
               new Square(Image.FromFile("..\\Images\\key_floor.png"), true, 4, 3), //4 - key
               new Square(Image.FromFile("..\\Images\\box.png"), false, 5), //5 - box
               new Square(Image.FromFile("..\\Images\\books.png"), false, 6), //6 - books
               new Square(Image.FromFile("..\\Images\\None_Image.png"), false), //7 - 
               new Square(Image.FromFile("..\\Images\\None_Image.png"), false), //8 - 
               new Square(Image.FromFile("..\\Images\\None_Image.png"), false), //9 - 
               new Square(Image.FromFile("..\\Images\\door2\\door_up_closed.png"), true, 10, 2), //10 - doorUC
               new Square(Image.FromFile("..\\Images\\door2\\door_right_closed.png"), true, 11, 2), //11 - doorRC
               new Square(Image.FromFile("..\\Images\\door2\\door_down_closed.png"), true, 12, 2), //12 - doorDC
               new Square(Image.FromFile("..\\Images\\door2\\door_left_closed.png"), true, 13, 2), //13 - doorLC
               new Square(Image.FromFile("..\\Images\\door2\\door_up_opened.png"), true, 14, 1), //14 - doorUO
               new Square(Image.FromFile("..\\Images\\door2\\door_right_opened.png"), true, 15, 1), //15 - doorRO
               new Square(Image.FromFile("..\\Images\\door2\\door_down_opened.png"), true, 16, 1), //16 - doorDO
               new Square(Image.FromFile("..\\Images\\door2\\door_left_opened.png"), true, 17, 1), //17 - doorLO
        };
}