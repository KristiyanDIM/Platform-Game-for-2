using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platform_Game_Tutorial_MOO_ICT
{
    public partial class Form1 : Form
    {

        bool goLeft, goRight, jumping, isGameOver;
        bool goLeft2, goRight2, jumping2;

        int jumpSpeed2;
        int force2;

        int jumpSpeed;
        int force;

        int score = 0;
        int score2 = 0;

        int playerSpeed = 7;

        int horizontalSpeed = 5;
        int verticalSpeed = 3;

        int enemyOneSpeed = 5;
        int enemyTwoSpeed = 3;



        public Form1()
        {
            InitializeComponent();
        }

        private void MainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Plaeyr 1 Score: " + score;
            txtScore2.Text = "Player 2 Score: " + score2;


            player.Top += jumpSpeed;
            if (goLeft) player.Left -= playerSpeed;
            if (goRight) player.Left += playerSpeed;
            if (jumping && force < 0) jumping = false;
            if (jumping)
            {
                jumpSpeed = -8;
                force -= 1;
            }
            else
            {
                jumpSpeed = 10;
            }

            // player 2
            player2.Top += jumpSpeed2;
            if (goLeft2) player2.Left -= playerSpeed;
            if (goRight2) player2.Left += playerSpeed;
            if (jumping2 && force2 < 0) jumping2 = false;
            if (jumping2)
            {
                jumpSpeed2 = -8;
                force2 -= 1;
            }
            else
            {
                jumpSpeed2 = 10;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            force = 8;
                            player.Top = x.Top - player.Height;
                            if ((string)x.Name == "horizontalPlatform" && (goLeft || goRight))
                            {
                                player.Left -= horizontalSpeed;
                            }
                        }
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                        {
                            x.Visible = false;
                            score++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!!";
                            txtScore2.Text = "Player 2 Score: " + score2;

                        }
                    }
                }
            }

            // player 2 
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "platform")
                    {
                        if (player2.Bounds.IntersectsWith(x.Bounds))
                        {
                            force2 = 8;
                            player2.Top = x.Top - player2.Height;
                            if ((string)x.Name == "horizontalPlatform" && (goLeft2 || goRight2))
                            {
                                player2.Left -= horizontalSpeed;
                            }
                        }
                    }
                    if ((string)x.Tag == "coin")
                    {
                        if (player2.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                        {
                            x.Visible = false;
                            score2++;
                        }
                    }
                    if ((string)x.Tag == "enemy")
                    {
                        if (player2.Bounds.IntersectsWith(x.Bounds))
                        {
                            gameTimer.Stop();
                            isGameOver = true;
                            txtScore2.Text = "Score: " + score + Environment.NewLine + "You were killed in your journey!!";
                            txtScore2.Text = "Player 2 Score: " + score2;

                        }
                    }
                }
            }


            horizontalPlatform.Left -= horizontalSpeed;
            if (horizontalPlatform.Left < 0 || horizontalPlatform.Left + horizontalPlatform.Width > this.ClientSize.Width)
            {
                horizontalSpeed = -horizontalSpeed;
            }

            verticalPlatform.Top += verticalSpeed;
            if (verticalPlatform.Top < 195 || verticalPlatform.Top > 581)
            {
                verticalSpeed = -verticalSpeed;
            }

            enemyOne.Left -= enemyOneSpeed;
            if (enemyOne.Left < pictureBox5.Left || enemyOne.Left + enemyOne.Width > pictureBox5.Left + pictureBox5.Width)
            {
                enemyOneSpeed = -enemyOneSpeed;
            }

            enemyTwo.Left += enemyTwoSpeed;
            if (enemyTwo.Left < pictureBox2.Left || enemyTwo.Left + enemyTwo.Width > pictureBox2.Left + pictureBox2.Width)
            {
                enemyTwoSpeed = -enemyTwoSpeed;
            }

            if (player.Top + player.Height > this.ClientSize.Height + 50 || player2.Top + player2.Height > this.ClientSize.Height + 50)
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Player 1 Score: " + score + Environment.NewLine + "You fell to your death!";
                txtScore2.Text = "Player 2 Score: " + score2 + Environment.NewLine + "You fell to your death!";
            }

            if (player.Bounds.IntersectsWith(door.Bounds) || player2.Bounds.IntersectsWith(door.Bounds))
            {
                gameTimer.Stop();
                isGameOver = true;
                txtScore.Text = "Резултат на Играч 1: " + score + Environment.NewLine + "Вашето приключение е завършено!";
                txtScore2.Text = "Резултат на Играч 2: " + score2 + Environment.NewLine + "Вашето приключение е завършено!";
            }
            else
            {
                txtScore.Text = "Резултат на Играч 1: " + score;
                txtScore2.Text = "Резултат на Играч 2: " + score2 + Environment.NewLine + "Съберете всички монети";
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            //player1
            if (e.KeyCode == Keys.Left) goLeft = true;
            if (e.KeyCode == Keys.Right) goRight = true;
            if (e.KeyCode == Keys.Up && !jumping) jumping = true;

            //player2
            if (e.KeyCode == Keys.A) goLeft2 = true;
            if (e.KeyCode == Keys.D) goRight2 = true;
            if (e.KeyCode == Keys.W && !jumping2) jumping2 = true;
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            //player1
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Up) jumping = false;

            //player2
            if (e.KeyCode == Keys.A) goLeft2 = false;
            if (e.KeyCode == Keys.D) goRight2 = false;
            if (jumping2) jumping2 = false;

            if (e.KeyCode == Keys.Enter && isGameOver) RestartGame();

        }

        private void RestartGame()
        {

            jumping = false;
            jumping2 = false;

            goLeft = false;
            goRight = false;

            goLeft2 = false;
            goRight2 = false;

            isGameOver = false;
            score = 0;
            score2 = 0;

            txtScore.Text = "Player 1 Score: " + score;
            txtScore2.Text = "Player 2 Score: " + score2;


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Visible == false)
                {
                    x.Visible = true;
                }
            }


            // reset the position of player, platform and enemies

            player.Left = 72;
            player.Top = 656;

            player2.Left = 100;
            player2.Top = 656;

            enemyOne.Left = 471;
            enemyTwo.Left = 360;

            horizontalPlatform.Left = 275;
            verticalPlatform.Top = 581;

            gameTimer.Start();

        }
    }
}
