using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.IO;

namespace POO_TIC_TAC
{
    class grille
    {
        private string chemin = @"C:\Users\DELL\Desktop\VAR.txt";

        private string element = "DEFAULT"; // TYPE_ICONE
        public grille(int L, Point start)
        {
            // Start(50,100)
            grid = new Rect[3,3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grid[i, j] = new Rect(50+j*L, 50+i*L, 100); 
            // attention matrice de rectangle : création de NxN rectangle
            // N c'est 3
 

        }

        public bool SeachRect( Point p, out  int ix , out  int jy)
        {
            ix = -1;
            jy = -1;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i, j].PointInRect(p)) { ix = i; jy = j; return true; } ;

            return false ;
        }



        public bool PlayerB(ref Graphics g, Point p, string name_player)
        {
            int i = -1, j = -1;
            bool r = SeachRect(p, out i, out j);
            if (!r) { validatePlayerEntry(); return false; }
            if (r) 
            { 
                grid[i, j].croix(ref g, element);
                string appendText = name_player + " : [" + i + "," + j + "]" + Environment.NewLine;
                File.AppendAllText(chemin, appendText);
                return true;
            }
            return true;
        }
        public bool PlayerA( ref Graphics g, Point p,string name_player)
        {
            int i=-1, j=-1;
            bool r = SeachRect(p, out i,  out j);
            if (!r) { validatePlayerEntry(); return false; }
            if (r) 
            { 
                grid[i, j].rond(ref g,element); 
                string appendText = name_player + " : [" + i +"," + j + "]" + Environment.NewLine;
                File.AppendAllText(chemin, appendText);
                return true; 
            }
            return true;
        }
        public void dessiner(ref Graphics g)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grid[i, j].desiner(ref g);

        }

        public void ResetGame( Form1 f)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    grid[i, j].reset();
            string appendText = "---------------------"+ Environment.NewLine +"NEW GAME :" + Environment.NewLine;
            File.AppendAllText(@"C:\Users\DELL\Desktop\VAR.txt", appendText);
            f.Refresh(); // redessiner

        }

        public void change_world(string s) { element = s; }

        

            // is winner
        public    int IsWinner()
        {

            // player A
            if (
                (grid[0, 0].PlayerA()) &&
                (grid[1, 0].PlayerA()) &&
                (grid[2, 0].PlayerA())
                )
                return 1;

            if (
                (grid[0, 1].PlayerA()) &&
                (grid[1, 1].PlayerA()) &&
                (grid[2, 1].PlayerA())
                )
                return 1;

            if (
                (grid[0, 2].PlayerA()) &&
                (grid[1, 2].PlayerA()) &&
                (grid[2, 2].PlayerA())
                )
                return 1;

            if (
                (grid[0, 0].PlayerA()) &&
                (grid[0, 1].PlayerA()) &&
                (grid[0, 2].PlayerA())
               )
                return 1;

            if (
                (grid[1, 0].PlayerA()) &&
                (grid[1, 1].PlayerA()) &&
                (grid[1, 2].PlayerA())
                )
                return 1;

            if (
                (grid[2, 0].PlayerA()) &&
                (grid[2, 1].PlayerA()) &&
                (grid[2, 2].PlayerA())
                )
                return 1;


            // player B

            if (
                (grid[0, 0].PlayerB()) &&
                (grid[1, 0].PlayerB()) &&
                (grid[2, 0].PlayerB())
                )
                return -1;

            if (
                (grid[0, 1].PlayerB()) &&
                (grid[1, 1].PlayerB()) &&
                (grid[2, 1].PlayerB())
                )
                return -1;

            if (
                (grid[0, 2].PlayerB()) &&
                (grid[1, 2].PlayerB()) &&
                (grid[2, 2].PlayerB())
                )
                return -1;

            if (
                (grid[0, 0].PlayerB()) &&
                (grid[0, 1].PlayerB()) &&
                (grid[0, 2].PlayerB())
                )
                return -1;

            if (
                (grid[1, 0].PlayerB()) &&
                (grid[1, 1].PlayerB()) &&
                (grid[1, 2].PlayerB())
                )
                return -1;

            if (
                (grid[2, 0].PlayerB()) &&
                (grid[2, 1].PlayerB()) &&
                (grid[2, 2].PlayerB())
                )
                return -1;






            // player A
            if (
                (grid[0, 0].PlayerA()) &&
                (grid[1, 1].PlayerA()) &&
                (grid[2, 2].PlayerA())
                )
                return 1;

            // player A
            if (
                (grid[0, 2].PlayerA()) &&
                (grid[1, 1].PlayerA()) &&
                (grid[2, 0].PlayerA())
                )
                return 1;


            // player B
            if (
                (grid[0, 2].PlayerB()) &&
                (grid[1, 1].PlayerB()) &&
                (grid[2, 0].PlayerB())
                )
                return -1;

            // player B
            if (
                (grid[0, 0].PlayerB()) &&
                (grid[1, 1].PlayerB()) &&
                (grid[2, 2].PlayerB())
                )
                return -1;


            return 0;
}


private void validatePlayerEntry()
        {


            string message = "You did not click on the grid. Cancel this operation?";
            string caption = "Error Detected in Input given by "+"Player";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

            
        }

//

    public bool CheckForGameOver( Form1 f,string winner)
        {
            string messageA = "Player 2 : " + winner + " Wins !";
            string messageB = "Player 1 : " + winner + " Wins !";
            string messageC = "No winner";
            string caption = "Ending Game bye !";
            string appendText_win = winner + " : GAME OVER" + Environment.NewLine;
            string appendText_noWin =  "GAME OVER" + Environment.NewLine;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
    
            int nWin = this.IsWinner();
            if (nWin == 1)
            {
                f.playSimpleSound(@"C:\Users\DELL\Downloads\Winnner.wav");
                MessageBox.Show(messageA, caption, buttons); // APPEL Partie.Gameover()
                f.increment_cpt_player2();
                File.AppendAllText(chemin, appendText_win);
                this.ResetGame(f);
                return true;
            }
            else if (nWin == -1) 
            {
                f.playSimpleSound(@"C:\Users\DELL\Downloads\Winnner.wav");
                MessageBox.Show(messageB, caption, buttons); 
                    f.increment_cpt_player1(); 
                    File.AppendAllText(chemin, appendText_win);
                    this.ResetGame(f);
                    return true; 
            }
            // Cas d'égalité ajouté 
            else if (nWin == 0 && f.end_click()) 
            { 
                f.playSimpleSound(@"C:\Users\DELL\Downloads\No_winner.wav");
                MessageBox.Show(messageC, caption, buttons);
                File.AppendAllText(chemin, appendText_noWin);
                this.ResetGame(f);
                return true; 
            }
            return false;
        }
        private Rect[,] grid; //=new Rect [3,3];
        // matrice double de rectangle
        

    }


}
