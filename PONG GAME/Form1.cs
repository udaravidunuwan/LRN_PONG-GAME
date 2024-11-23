using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PONG_GAME
{
    public partial class Form1 : Form
    {
        //ball x and y speeds
        int ballXSpeed = 4;
        int ballYSpeed = 5;
        //speed for the computer
        int speed = 2;
        //to select a random speed for ball and computer
        Random rand = new Random();
        //to control when player moves up or down
        bool goDown, goUp;
        //the interval for how ofter the computer speed will change
        int computerSpeedChange = 50;
        //computer and player scores
        int playerScore = 0;
        int computerScore = 0;
        //player speed
        int playerSpeed = 8;
        // this array for assign computer rand speeds
        int[] i = { 5 , 6 , 8 , 9 };
        // this array to assign ball x and y speeds
        int[] j = { 10, 9, 8, 11, 12 };


        public Form1()
        {
            InitializeComponent();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            //assigning the ball speeds horizontally and vertially
            //"-=" so with each interval it will be moving vertical or sideways, positilely or negatively
            //TECHNICALY "-=" IS THERE BECAUSE OF POSITIVE AND NEGATIVE NUMBERS
            ball.Top -= ballYSpeed;
            ball.Left -= ballXSpeed;

            //to update the text on the form on player and computer score
            this.Text = "Player Score: " + playerScore + " -- Computer Score: " + computerScore;

            //check if ball top is < 0 ot ball bottom is point is bigger than form height
            //TO SEE IF THE BALL IS IN THE FORM NOT OUT SIDE 
            if (ball.Top < 0 || ball.Bottom > this.ClientSize.Height)
            {
                //so the ball bounces off to the opposite ditection ad boundary collision
                ballYSpeed = -ballYSpeed;
            }

            //If the ball has reached the left border (computer is on the right side)
            if (ball.Left < -2)
            {
                //reset ball back to 300
                ball.Left = 300;
                //ball move on opposite ditection
                ballXSpeed = -ballXSpeed;
                //computer score will increase
                computerScore++;
            }

            //If ball breaches the right side border (player is on the left side)
            if (ball.Right > this.ClientSize.Width + 2)
            {
                //reset the ball at 300
                ball.Left = 300;
                //ball moves to the opposite direction
                ballXSpeed = -ballXSpeed;
                //player score increases
                playerScore++;
            }

            //if computer picturebox reaches 1 (at the Top side)
            if (computer.Top <= 1)
            {
                //reset it back to 0, so the computer picture box doesn't leave the screen
                computer.Top = 0;
            }
            //for the bottom for the picture box to not to leave the form
            else if (computer.Bottom >= this.ClientSize.Height)
            {
                //computer top will stop at the form height - computer picture box height
                computer.Top = this.ClientSize.Height - computer.Height;
            }
            //computer is folliwing the ball, if the (1/2 of computer picbox height + top border height)
            //is lower and the ball point height AND if ball is in the left half on the form

            //TECHNICALLY IF THE BALL IS IN THE right HALF TOP AREA
            if (ball.Top < computer.Top + (computer.Height / 2) && ball.Left > 300)
            {
                //if thats the case, computer will move up
                computer.Top -= speed;
            }
            //TECHNICALLY IF THE BALL IS IN THE right HALF BOTTOM AREA
            if (ball.Top > computer.Top + (computer.Height / 2) &&  ball.Left > 300)
            {
                //computer will move down to the ball
                computer.Top += speed;
            }

            //pulling down the speed
            computerSpeedChange -= 1;

            //Once the speed is 0, speed is set to a random number
            //SO THAT THE COMPUTE WILL HAVE DIFFERENT SPEEDS AT DEFFERENT TIMES WHEN IT HTIS THE BALL
            if (computerSpeedChange < 0) 
            {
                speed = i[rand.Next(i.Length)]; 
                computerSpeedChange = 50;
            }


            //if the goDown is true, and the player is within the geight of the form, 
            if (goDown  && player.Top + player.Height < this.ClientSize.Height)
            {
                //the lpayer continue to move down
                player.Top += playerSpeed;
            }

            // if the player wants to go up
            if (goUp && player.Top > 0)
            {
                //as long as player is within the form, will go up with reducing speed
                player.Top -= playerSpeed;
            }

            //call the checkcollision function, 
            //passing the ball as picOne, player as picTwo and player.Right +5 as offset
            // this is for both
            CheckCollision(ball, player, player.Right + 5);
            CheckCollision(ball, computer, computer.Left - 35);
            // there is a offset value, so that the ball doesnt hit the picbox and clip to it
            // wich would make a glitch, when tha ball clips to a picture box and moves with it or goes inside the picture box


            //call gameover function to pass, the computer and player scored 
            if (computerScore > 5)
            {
                gameOver("Sorry you lost the Game!");
            }
            else if (playerScore > 5)
            {
                gameOver("Congrats! You Won the Game!");
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            //if the keybaords down key is pressed
            if (e.KeyCode == Keys.Down) 
            {
                goDown = true;
            }
            //if the keybaords up key is pressed
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if(e.KeyCode == Keys.Up)
            {  
                goUp = false;
            }
        }

        private void CheckCollision(PictureBox picOne, PictureBox picTwo, int offset) 
        {
            //picone passed as ball, collides with either cmputer or player
            if (picOne.Bounds.IntersectsWith(picTwo.Bounds)) 
            {
                //to know exactly where the ball needs to move to when it hits computer or player
                picOne.Left = offset;

                //select any ramdon values from j array
                int x = j[rand.Next(j.Length)];
                int y = j[rand.Next(j.Length)];

                //if ball moves to left, the value will be less than 0
                // if the ball moves to right , the  value will be greater than 0
                //so need to assign the value accordingly
                if (ballXSpeed < 0)
                {
                    //at collision event, if the ball was traveling left, now new value will be moving to right
                    ballXSpeed = x; 
                }
                else
                {
                    //at collision event, if the ball was traveling right, now new value will be moving to left
                    ballXSpeed = -x;
                }


                if (ballYSpeed < 0)
                {
                    //at collision event, if the ball was traveling up, the trajectory must stay the same with faster ot slower speed
                    ballYSpeed = -y;
                }
                else
                {
                    //at collision event, if the ball was traveling down, the trajectory must stay the same with faster ot slower speed
                    ballYSpeed = y;
                }
            }
        } 

        private void gameOver(string message)
        {
            //resets everything
            //stop timer
            GameTimer.Stop();
            //message to disply on screen
            MessageBox.Show(message, "Game is : ");
            computerScore = 0;
            playerScore = 0;
            ballXSpeed = ballYSpeed = 4;
            computerSpeedChange = 50;
            //start timer again to play the game
            GameTimer.Start();
        }



    }
}
