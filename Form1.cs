using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Media;

namespace POO_TIC_TAC
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        int click = 0;
        int cpt_player1 = 0;
        int cpt_player2 = 0;
        string winner = "";
        private grille game;
        private Partie par;
        bool verrou = true;
        bool ok = true;
        public Form1()
        {
            InitializeComponent();

            Point p = new Point(50, 50);
            game = new grille(100, p);

        }
        // FONCTION DE SON QUI PERMET DE JOUER UN SON A TOUT MOMENT
        public void playSimpleSound(string sound_path)
        {
            SoundPlayer simpleSound = new SoundPlayer(sound_path);
            simpleSound.Play();
        }
        // FONCTION QUI DETERMINE LE JOUEUR AVEC LE PLUS GRAND SCORE
        public string Name_BestScore()
        {
            if (cpt_player1 > cpt_player2)
            {
                winner = label3.Text;
            }
            else if (cpt_player2>cpt_player1)
            {
                winner = label4.Text;
            }
            else
            {
                winner = "DRAW";
            }
            return winner;
        }
        // FONCTION QUI AFFICHE LE LE NOMBRE DE PARTIE JOUEE 
        public void modifier_cpt_partie(int c)
        {
            label5.Text = c.ToString();
        }
        // FONCTION QUI INCREMENTE LE COMPTEUR DE SCORE DU PREMIER JOUEUR
        public void increment_cpt_player1()
        {
            cpt_player1++;
            textBox1.Text = cpt_player1.ToString();
        }
        // FONCTION QUI INCREMENTE LE COMPTEUR DE SCORE DU DEUXIEME JOUEUR
        public void increment_cpt_player2()
        {
            cpt_player2++;
            textBox2.Text = cpt_player2.ToString();
        }
        // FONCTION QUI RETORNE QUI DETERMINE LE NOMBRE MAXIMALE DE CLICK D'UN JEU
        public bool end_click()
        {
            if (click==9)
            {
                return true;
            }
            return false;
        }
        // LE BOUTON RESET : 
        private void button1_Click(object sender, EventArgs e)
        {
            if(this.comboBox1.Text!="DEFAULT") game.change_world(this.comboBox1.Text); // CHANGER LES PERSONNAGES
            game.ResetGame(this); // VIDER LA GRILLE

        }
        // L'EVENEMENT LOAD QUI CORRESPOND AU CHARGEMENT DE LA FENETRE 
        private void Form1_Load_1(object sender, EventArgs e)
        {

            string createText = "NEW GAME :" + Environment.NewLine;
            File.WriteAllText(@"C:\Users\DELL\Desktop\VAR.txt", createText, Encoding.UTF8); // CREER UN FICHIER OU ECRASER SON CONTENU AVEC LA CHAINE CREATETEXT

            comboBox1.Text = "DEFAULT";
            textBox1.Text = cpt_player1.ToString();
            textBox2.Text = cpt_player2.ToString();
            // DEMANDE DES NOMS DU JOUEUR 1 :
            label3.Text = Interaction.InputBox("Name of 1st Player", "SET NAMES");
            if (String.IsNullOrEmpty(label3.Text)) label3.Text = "PLAYER 1";
            // DEMANDE DES NOMS DU JOUEUR 2 :
            label4.Text = Interaction.InputBox("Name of 2nd Player", "SET NAMES");
            if (String.IsNullOrEmpty(label4.Text)) label4.Text = "PLAYER 2";
            // DEMANDE DU NOMBRE DE JEU DE CETTE PARTIE : 
            label6.Text = Interaction.InputBox("BEST OF : ...", "DEFINE NUMBER OF GAMES FOR ONE PARTY");
            while (String.IsNullOrEmpty(label6.Text))
            {
                label6.Text = Interaction.InputBox("BEST OF : ...", "DEFINE NUMBER OF GAMES FOR ONE PARTY");
            }
            // CREATION D'UNE PARTIE AVEC COMME VALEUR LA DERNIERE SAISIE : 
            par = new Partie(Int16.Parse(label6.Text));
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            game.dessiner(ref g);
        }
        // EVENEMENT SUR LE CLICK DE LA SOURIS 
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            Point p = e.Location;
            switch (e.Button)
            {
               
                case MouseButtons.Left:
                    {
                        
                        bool ok = false;
                        if (verrou)
                        {
                            playSimpleSound(@"C:\Users\DELL\Downloads\CLICK_DEFAULT_1.wav"); // JOUER UN SON 
                            ok = this.game.PlayerA(ref g, p, label3.Text);
                            if (ok)
                            {
                                verrou = false;
                                click++;
                            }
                        }
                        bool test = game.CheckForGameOver(this, label3.Text); // TESTER LA FIN DU JEU
                        if (test)
                        {
                            click = 0;
                            verrou = true;
                            par.GameOver(this); // TESTER FIN DE LA PARTIE

                        }
                    }

                    break;

                case MouseButtons.Right:
                    {

                        bool ok = false;
                        if (!verrou)
                        {
                            playSimpleSound(@"C:\Users\DELL\Downloads\CLICK_DEFAULT_2.wav");// JOUER UN SON 
                            ok = this.game.PlayerB(ref g, p,label4.Text);  
                            if (ok)
                            {
                                verrou = true;
                                click++;
                            }
                        }
                        bool test = game.CheckForGameOver(this, label4.Text);// TESTER LA FIN DU JEU
                        if (test)
                        {
                            click = 0;
                            verrou = true;
                            par.GameOver(this);// TESTER FIN DE LA PARTIE
                        }
                        
                    }

                    break;
                default:
                    break;
            }
        }

        // BOUTON QUIT POUR QUITTER L'APPLICATION
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // BOUTTON NEWGAME
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.comboBox1.Text != "DEFAULT") game.change_world(this.comboBox1.Text);// TESTER LE CHANGEMENT DU MONDE
            //RENITIALISATION DES COMPTEURS : 
            cpt_player1 = 0; cpt_player2 = 0;
            textBox1.Text = cpt_player1.ToString();
            textBox2.Text = cpt_player2.ToString();
            // VIDER LA GRILLE :
            game.ResetGame(this);
            // ECRASER LE FICHIER VAR CAR C'EST LE DEBUT D'UNE NOUVELLE PARTIE
            string createText = "NEW GAME :" + Environment.NewLine;
            File.WriteAllText(@"C:\Users\DELL\Desktop\VAR.txt", createText, Encoding.UTF8);
            if (MessageBox.Show("Do you want to keep same names ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                label3.Text = Interaction.InputBox("Name of 1st Player", "SET NAMES");
                if (String.IsNullOrEmpty(label3.Text)) label3.Text = "PLAYER 1";
                label4.Text = Interaction.InputBox("Name of 2nd Player", "SET NAMES");
                if (String.IsNullOrEmpty(label4.Text)) label4.Text = "PLAYER 2";

            }
            if (MessageBox.Show("Do you want to keep same number of partie", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                label6.Text = Interaction.InputBox("BEST OF : ...", "DEFINE NUMBER OF GAMES FOR ONE PARTY");
                while (String.IsNullOrEmpty(label3.Text))
                {
                    label6.Text = Interaction.InputBox("BEST OF : ...", "DEFINE NUMBER OF GAMES FOR ONE PARTY");
                }
            }
            par.ResetPartie(this);// RENITILISER LA PARTIE
        }
    }
}

