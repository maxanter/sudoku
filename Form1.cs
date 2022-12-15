using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku2
{
    public partial class Form1 : Form
    {
        Gra g = new Gra();
        public Form1()
        {
            InitializeComponent();
            blokuj();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            g.Generuj();
            int [,]tab = g.Wypelnij();
            int x = 0;
            int y = 0;
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Enabled = true;
                tb.Text = tab[x, y].ToString();
                if (x == 8)
                {
                    x = 0;
                    y++;
                }
                else x++;
                if (tb.Text == "0") { tb.Text = ""; }
                else
                {
                    tb.Enabled = false;
                }
            }
        }

        public void blokuj()
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Enabled = false;
            }
        }

        public void wyczysc()
        {
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wyczysc();
            blokuj();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            bool sprawdz = true;
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                tb.ForeColor = Color.Black;
                if (tb.Text == "") { }
                else if(tb.Text == g.daj(x,y))
                {
                    tb.Enabled = false;
                }
                else
                {
                    tb.ForeColor = Color.Red;
                }
                if (x == 8)
                {
                    x = 0;
                    y++;
                }
                else x++;
            }
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                if(tb.Text == "")
                {
                    sprawdz = false;
                }
            }
            if(sprawdz == true)
            {
                MessageBox.Show("Wygrałeś!");
                wyczysc();
                blokuj();
            }
        }
    }
    public class Gra
    {
        public static int[,] tab = new int[9, 9];
        public static int[,] tabcopy = new int[9, 9];

        public string daj(int x, int y)
        {
            return tab[x,y].ToString();
        }
        public int[,] Wypelnij()
        {
            Random r = new Random();
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    tabcopy[i, j] = tab[i, j];
                }
            }
            int a = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    a = r.Next(1, 100);
                    if (a < 50)
                    {
                        tabcopy[i, j] = 0;
                    }

                }
            }
            return tabcopy;
        }
        public void Generuj()
        {
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    tab[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
            Update();
        }
        static void Losowanie(int Wart1, int Wart2)
        {
            int x1, y1, x2, y2;
            x1 = y1 = x2 = y2 = 0;
            for (int i = 0; i <= 8; i += 3)
            {
                for (int k = 0; k <= 8; k += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (tab[i + j, k + z] == Wart1)
                            {
                                x1 = i + j;
                                y1 = k + z;

                            }
                            if (tab[i + j, k + z] == Wart2)
                            {
                                x2 = i + j;
                                y2 = k + z;

                            }
                        }
                    }
                    tab[x1, y1] = Wart2;
                    tab[x2, y2] = Wart1;
                }
            }
        }
        public void Update()
        {
            for (int i = 0; i < 10; i++)
            {
                Random r1 = new Random(Guid.NewGuid().GetHashCode());
                Random r2 = new Random(Guid.NewGuid().GetHashCode());
                Losowanie(r1.Next(1, 9), r2.Next(1, 9));
            }
        }
    }
}
