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
        int ballXSpeed = 4;
        int ballYSpeed = 5;
        int speed = 2;
        Random rand = new Random();
        bool goDown, goUp;
        int computerSpeedChange = 50;
        int playerScore = 0;
        int computerScore = 0;
        int playerSpeed = 8;


        public Form1()
        {
            InitializeComponent();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

        }

        private void CheckCollision(PictureBox picOne, PictureBox picTwo, int offset) 
        {
            
        } 

        private void gameOver(string message)
        {
            
        }



    }
}
