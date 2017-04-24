using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace Boxes_walls_and_keys
{
    public partial class Form1 : Form
    {
        GameMain game_main = new GameMain();

        public Form1()
        {

            InitializeComponent();
            //Size = new Size(SO.window_wh.X, SO.window_wh.Y);
            Size screenSize = Screen.PrimaryScreen.WorkingArea.Size;
            Size gameSize = new Size(SO.map_wh.X * SO.sq, SO.map_wh.Y * SO.sq);
            MaximumSize = new Size(Math.Min(gameSize.Width, screenSize.Width), Math.Min(gameSize.Height, screenSize.Height));

            WindowState = FormWindowState.Maximized;
            
            Controls.Add(game_main.game_panel);
            game_main.gaming();
        }
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.W)
                game_main.move_player(SO.move_direction.up);
            if (e.KeyCode == Keys.A)
                game_main.move_player(SO.move_direction.left);
            if (e.KeyCode == Keys.S)
                game_main.move_player(SO.move_direction.down);
            if (e.KeyCode == Keys.D)
                game_main.move_player(SO.move_direction.right);

        }
    }

}
