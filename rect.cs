using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Media;
namespace POO_TIC_TAC
{
    class Rect
    {

        public void reset()
        {
            this.state = 0;
        }

        public   bool PlayerA()
                    { return state == 1; }
        public bool PlayerB()
        { return state == -1; }

        public bool PointInRect(Point p)
        {

            bool r = this.geom.Contains(p);
            return  r;
        }
        public Rect(int tx, int ty, int w)
        {
            geom = new Rectangle(tx, ty, w, w);
            state = 0;

           
        }

        public void desiner(ref Graphics g)
        {
            // Graphics est un device context GDI

            //Brush b = new SolidBrush(Color.Black);
            Pen s = new Pen(Color.WhiteSmoke,15);
            //s.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            g.DrawRectangle(s, geom);
            
        }

        public void rond(ref Graphics g,string value)
        {
            // Graphics g = p.Graphics;
            if (state == 0)
            {
                if (value == "DEFAULT")
                {
                    Pen stylo = new Pen(Color.DarkSlateBlue, 15);
                    Brush s = new SolidBrush(Color.Gold);
                    g.FillEllipse(s, geom);
                    g.DrawEllipse(stylo, geom);
                }
                else
                {
                    Image newImage;
                    Point centre_rect;
                    // ICON TOM & JERRY : TOM 
                    if (value == "TOM & JERRY")
                    {
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\TOM_FINAL3.0.png");         
                    }
                    // ICON DORA : DORA
                    else if (value == "DORA")
                    {
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\DORA.png");
                        
                    }
                    // ICON FROZEN : 
                    else if (value == "FROZEN")
                    {
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\ELSA.png");
                    }
                    else
                    {
                        // EN CAS SI JE VEUX AJOUTER UN NOUVEAU MONDE
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\");
                    }
                    centre_rect = new Point((geom.X + geom.Width / 2) - (newImage.Width / 2), (geom.Y + geom.Height / 2) - (newImage.Height / 2));
                    g.DrawImage(newImage, centre_rect);
                    
                }
                state = -1;
            }
        }


        public void croix(ref Graphics g, string value)
        {
            // Graphics g = p.Graphics;
            if (state == 0)
            {
                if (value == "DEFAULT")
                {                
                    Pen s = new Pen(Color.Red, 20);
                    g.DrawLine(s, geom.Left, geom.Top, geom.Right, geom.Bottom);
                    g.DrawLine(s, geom.Left, geom.Bottom, geom.Right, geom.Top);
                }
                else
                {
                    Image newImage; // INITIALISATION
                    Point center;
                    // ICON TOM & JERRY : TOM 
                    if (value == "TOM & JERRY")
                    {
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\JERRY.png");
                    }
                    // ICON DORA : DORA
                    else if (value == "DORA")
                    {
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\BABOUCHE.png");

                    }
                    // ICON FROZEN : 
                    else if (value == "FROZEN")
                    {
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\ANNA.png");
                    }
                    else
                    {
                        // EN CAS SI JE VEUX AJOUTER UN NOUVEAU MONDE
                        newImage = Image.FromFile(@"C:\Users\DELL\Downloads\");       
                    }
                    center = new Point((geom.X + geom.Width / 2) - (newImage.Width / 2), (geom.Y + geom.Height / 2) - (newImage.Height / 2));
                    g.DrawImage(newImage, center);
                }
                state = 1;
            }}




        private Rectangle geom;//forme
        private int state; // 0 1 -1
        private static readonly int s = 100;
    }
}
