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
using System.Media;
namespace POO_TIC_TAC
{
    public partial class Form2 : Form
    {
        // FONCTION DE SON QUI PERMET DE JOUER UN SON A TOUT MOMENT
        public void playSimpleSound(string sound_path)
        {
            SoundPlayer simpleSound = new SoundPlayer(sound_path);
            simpleSound.Play();
        }
        public Form2(string w)
        {
           
            InitializeComponent();
            label2.Text = w;
        }
        // L'EVENEMENT LOAD QUI CORRESPOND AU CHARGEMENT DE LA FENETRE : ON FAIT APPEL A LA FONCTION DU SON 
        private void Form2_Load(object sender, EventArgs e)
        {
            playSimpleSound(@"C:\Users\DELL\Downloads\VICTORY.wav");
        }
        // BOUTON QUIT POUR QUITTER L'APPLICATION
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // BOUTON BACK QUI INSTANCIE UN OBJET DE TYPE FORM1, CACHE LE FORM ACTUEL ET AFFICHE LE PREMIER POUR REJOUER UNE NOUVELLE FOIS 
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            this.Hide();
            f.Show();           
        }
    }
}
