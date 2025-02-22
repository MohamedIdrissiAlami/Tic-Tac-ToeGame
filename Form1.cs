using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tick_Tac_Toe_Game.Properties;

namespace Tick_Tac_Toe_Game
{
    public partial class Form1 : Form
    {

        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;
        enum enPlayer
        {
            Player1,
            Player2,

        }
        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ResetGame();
        }
        void ResetButton(Button button)
        {
            button.BackColor = Color.Transparent;
            button.Image = Resources.question_mark_96;
            button.Tag = "?";
        }
        void ResetGame()
        {
            ResetButton(btn1);
            ResetButton(btn2);
            ResetButton(btn3);
            ResetButton(btn4);
            ResetButton(btn5);
            ResetButton(btn6);
            ResetButton(btn7);
            ResetButton(btn8);
            ResetButton(btn9);
            PlayerTurn = enPlayer.Player1;
            GameStatus.Winner = enWinner.GameInProgress;
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            lblTurn.Text = "Player1";
            lblWinner.Text = "InProgress";



        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen Pen = new Pen(Color.White,12);
            Pen.StartCap=System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            
            float GbWidth = groupBox1.Width,GbHeight=groupBox1.Height;
            // draw 2 lines horisontal
            for(byte i=1;i<=2;i++)
            {
                e.Graphics.DrawLine(Pen, 0, i * (GbHeight / 3), GbWidth, i * (GbHeight / 3));
            }
            //draw 2 lines vertical
            for (byte i = 1; i <= 2; i++)
            {
                e.Graphics.DrawLine(Pen,  i * (GbWidth / 3),0, i * (GbWidth / 3),GbHeight);
            }
        }
        bool CheckValues(Button button1, Button button2,Button button3)
        {
           if(button1.Tag.ToString()!="?"&&button1.Tag==button2.Tag &&button1.Tag==button3.Tag)
            {
                GameStatus.Winner=button1.Tag.ToString() == "X" ? enWinner.Player1:enWinner.Player2 ;
                button1.BackColor = Color.GreenYellow;
                button2.BackColor = Color.GreenYellow;
                button3.BackColor = Color.GreenYellow;
                GameStatus.GameOver = true;
                EndGame();
                return true ;
            }
            
           GameStatus.GameOver = false;
           return false ;
        }

        void EndGame()
        {
            lblTurn.Text = "Game Over";
            lblWinner.Text = GameStatus.Winner == enWinner.Player1 ? "Player1" : GameStatus.Winner == enWinner.Player2 ? "Player2":"Draw" ;
            MessageBox.Show("Game Over","Tic-Tac-Toe GAME",MessageBoxButtons.OK,MessageBoxIcon.Information) ;


        }
        void CheckWinner()
        {
            if (CheckValues(btn1, btn2, btn3))
                return;
            else if(CheckValues(btn4, btn5, btn6))
                return;
            if (CheckValues(btn7, btn8, btn9))
                return;
            if (CheckValues(btn1, btn4, btn7))
                return;
            if (CheckValues(btn2, btn5, btn8))
                return;
            if (CheckValues(btn3, btn6, btn9))
                return;
            if (CheckValues(btn1, btn5, btn9))
                return;
            if (CheckValues(btn3, btn5, btn7))
                return;
            

        }
        void ChangeImage(Button button)
        {
            if (button.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        button.Image = Resources.X;
                        button.Tag = "X";
                        PlayerTurn=enPlayer.Player2;
                        lblTurn.Text = PlayerTurn.ToString();
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        button.Image = Resources.O;
                        button.Tag = "O";
                        PlayerTurn=enPlayer.Player1;
                        lblTurn.Text = PlayerTurn.ToString();
                        GameStatus.PlayCount++;
                        CheckWinner();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\a");
                MessageBox.Show("Wrong Choice", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            if(GameStatus.PlayCount>=9)
            {
                GameStatus.Winner = enWinner.Draw;
                GameStatus.GameOver = true;
                EndGame();
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            ChangeImage(btn1);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button) sender);
        }

        private void btnResetGame_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

    }
}
