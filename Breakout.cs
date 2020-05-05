using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YoavBreakoutGame
{
    public partial class Breakout_game : Form
    {
        bool go_Left;
        bool go_Right;
        int speed = 10;

        int ballX = 5;
        int ballY = 5;
        int score = 0;

        private Random rnd = new Random();
        public Breakout_game()
        {
            InitializeComponent();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Block")
                {
                    Color RandomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                    x.BackColor = RandomColor;
                }
            }
        }
        public void gameOver()
        {
            timer1.Enabled = false;
            MessageBox.Show("Game Over!!!!");
            Application.Exit();
        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // if the player pressd the left key AND the player is inside
            // the panel then we set the player laft boolean to true
            if (e.KeyCode == Keys.Left && player.Left > 0)
            {
                go_Left = true;
            }
            // if  player pressed the right key and the player left plus player width 
            // is k less then the panell width then we set the player right boolean to true
            if (e.KeyCode == Keys.Right && player.Left + player.Width < 920)
            {
                go_Right = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // if the LEFT key is up we set the player left boolean to false
            if (e.KeyCode == Keys.Left)
            {
                go_Left = false;
            }
            //  if the RIGHT key is up we set the player right boolean to false
            if (e.KeyCode == Keys.Right)
            {
                go_Right = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Left += ballX;
            ball.Top += ballY;
            lblSumSCORE.Text = score.ToString();
            if (go_Left)
            {
                player.Left -= speed; // move left
            }
            if (go_Right)
            {
                player.Left += speed; // move right
            }
            if (player.Left < 1)
            {
                go_Left = false;      // stop the player from going of scren
            }
            else if (player.Left + player.Width > 920)
            {
                go_Right = false;
            }
            if (ball.Left + ball.Width > ClientSize.Width || ball.Left < 0)
            {
                ballX = -ballX;       // this will bounce the object from the left or right side
            }
            if (ball.Top < 0 || ball.Bounds.IntersectsWith(player.Bounds))
            {
                ballY = -ballY;       // this will bounce the object from the top and bottom border
            }
            if (ball.Top + ball.Height > ClientSize.Height)
            {
                gameOver();           // the player lost game over!!!
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Block")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ballY = -ballY;
                        score++;
                    }
                }
                if (score > 31)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("You win!!!!");  // the player won!!!! 
                    Application.Exit();
                }
            }
        }
    }
}