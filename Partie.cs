using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace POO_TIC_TAC
{
    class Partie
    {
        int  nbrPartie;
        int cpt_partie = 1;
        Form2 f2 ;
        public Partie(int nPartie)
        {
            nbrPartie = nPartie;
        }

        public void PartiePP(Form1 f)
        {
            cpt_partie++;
            f.modifier_cpt_partie(cpt_partie);
        }

        public void GameOver(Form1 f)
        {
            if (cpt_partie == nbrPartie)
            {
                f2 = new Form2(f.Name_BestScore());
               
                    f.Hide();
                    f2.Show();
                    cpt_partie = 1;
                    f.modifier_cpt_partie(cpt_partie);
            }
            else PartiePP(f);
        }

        public void ResetPartie(Form1 f)
        {
            cpt_partie = 1;
            f.modifier_cpt_partie(cpt_partie);
        }
    }
}
