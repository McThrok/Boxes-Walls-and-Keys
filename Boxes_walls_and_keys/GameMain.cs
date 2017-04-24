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
    public class GameMain
    {

        public Panel game_panel = new Panel();

        Player player = new Player();


        public GameMain()
        {
            game_panel.Location = new Point(0, 0);
            game_panel.Size = new Size(SO.map_wh.X * SO.sq, SO.map_wh.Y * SO.sq);
            game_panel.Padding = new Padding(0, 0, 0, 0);
            game_panel.Margin = new Padding(0, 0, 0, 0);
        }
        public void gaming()
        {
            initialize_map();
            next_level();
        }

        //player interface----------------------------------------------------------------------
        public void set_location_player(int x = 0, int y = 0)
        {
            player.set_location(x, y);
            refresh_player_location();
        }
        public void move_player(SO.move_direction dir)
        {
            player.move(dir);
            check_events(dir);
            refresh_player_location();
            player.current_level.check_vision(player.location_current);
        }
        public void refresh_player_location()
        {
            player.pb_player.Location = new Point(player.location_current.X * SO.sq, player.location_current.Y * SO.sq);
        }

        //map interface
        public void initialize_map()
        {

            game_panel.Controls.Clear();
            for (int i = 0; i < SO.map_wh.X; i++)
                for (int j = 0; j < SO.map_wh.Y; j++)
                {
                    PictureBox picture = player.current_level.Squares[i, j].picture_box;
                    game_panel.Controls.Add(picture);
                }
            game_panel.Controls.Add(player.pb_player);
            player.pb_player.BringToFront();

        }
        public void check_events(SO.move_direction direction)
        {
            switch (player.current_level.Squares[player.location_current.X, player.location_current.Y].event_number)
            {
                case 0:
                    //None
                    break;
                case 1:
                    door_opened(direction);
                    break;
                case 2:
                    door_closed(direction);
                    break;
                case 3:
                    take_key();
                    break;
                default:
                    MessageBox.Show("event_number problem");
                    break;
            }
        }
        public void next_level()
        {
            if(player.level_number==SO.level_number)
            {
                MessageBox.Show("You finished the game.","The End");
                Environment.Exit(1);
            }
            player.current_level.convert_file2map(SO.level_filenames[player.level_number]);
            
            player.current_level.cover_map();

            player.level_number++;
            player.reset4lvl();

            set_location_player(player.current_level.start_position.X, player.current_level.start_position.Y);
            player.current_level.check_vision(player.location_current, true);


        }


        //events
        public void door_opened(SO.move_direction direction)
        {
            DialogResult result = MessageBox.Show("Leave this area and move to the next one.", "To door or not to door", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                next_level();
            else
                player.move_back(direction);
        }
        public void door_closed(SO.move_direction direction)
        {
            MessageBox.Show("You have only " + player.taken_keys.ToString() + "/" + SO.keys_per_level.ToString() + " keys. Collect all of them.", "Collect keys");

            player.move_back(direction);
        }
        public void take_key()
        {
            player.current_level.convert_type(ref player.current_level.Squares[player.location_current.X, player.location_current.Y], 1);
            player.taken_keys++;
            if (player.taken_keys == SO.keys_per_level)
                for (int i = 0; i < SO.map_wh.X; i++)
                    for (int j = 0; j < SO.map_wh.Y; j++)
                        if (player.current_level.Squares[i, j].square_id > 9 && player.current_level.Squares[i, j].square_id < 14)
                        {
                            int current_vision = player.current_level.Squares[i, j].vision;
                            player.current_level.convert_type(ref player.current_level.Squares[i, j], player.current_level.Squares[i, j].square_id + 4);
                            player.current_level.Squares[i, j].set_vision(current_vision);
                            player.current_level.set_shadow(); //to cover the door
                        }
        }

    }
}

