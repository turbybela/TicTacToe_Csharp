using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Label[] labels = new Label[9];

        bool prev = true; // true: X / false: O

        int clickNum = 0;

        public Form1()
        {
            InitializeComponent();

            labels = new Label[9] {
                label_11, label_12, label_13,
                label_21, label_22, label_23,
                label_31, label_32, label_33
            };

            foreach (var l in labels)
            {
                l.Click += new EventHandler(Clicked);
            }

            reset();
        }

        private Label GetLabel(int pos)
        {
            int pY = (pos - (pos % 10)) / 10;
            int pX = pos % 10;
            switch (pY)
            {
                case 1:
                    switch (pX)
                    {
                        case 1:
                            return labels[0];
                        case 2:
                            return labels[1];
                        case 3:
                            return labels[2];
                        default:
                            return null;
                    }
                case 2:
                    switch (pX)
                    {
                        case 1:
                            return labels[3];
                        case 2:
                            return labels[4];
                        case 3:
                            return labels[5];
                        default:
                            return null;
                    }
                case 3:
                    switch (pX)
                    {
                        case 1:
                            return labels[6];
                        case 2:
                            return labels[7];
                        case 3:
                            return labels[8];
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        void reset()
        {
            clickNum = 0;
            foreach (var l in labels)
            {
                l.Text = "-";
            }
        }


        private void Win(string winner)
        {
            string message = "Nil";
            if (winner == "Tie")
            {
                message = "Its a Tie!";
            }
            else
            {
                message = "The winner is: " + winner + "!";
            }

            MessageBox.Show(message, "Game Over!");
            reset();
        }


        private KeyValuePair<bool, string> Check()
        {
            #region help

            /* Possible wins:
             * 
                XXX * 3
             *
                X
                X
                X * 3
             *
                X
                 X
                  X
             *
                  X
                 X
                X
             */// new KeyValuePair<bool, string>(false, null);

            #endregion

            for (int t = 0; t < 2; t++)
            {
                string target = (t == 0 ? "X" : "O");
                for (int i = 1; i <= 3; i++) // horizontal
                {
                    int pY = i * 10;

                    if (GetLabel(pY + 1).Text == target &&
                        GetLabel(pY + 2).Text == target &&
                        GetLabel(pY + 3).Text == target) 
                        {
                            return new KeyValuePair<bool, string>(true, target);
                    }
                }
                for (int i = 1; i <= 3; i++) // vertical
                {
                    int pX = i;

                    if (GetLabel(pX + 10).Text == target &&
                        GetLabel(pX + 20).Text == target &&
                        GetLabel(pX + 30).Text == target)
                    {
                        return new KeyValuePair<bool, string>(true, target);
                    }
                }

                if (GetLabel(11).Text == target && // Diag 1
                    GetLabel(22).Text == target &&
                    GetLabel(33).Text == target)
                    {
                        return new KeyValuePair<bool, string>(true, target);
                    }

                if (GetLabel(13).Text == target && // Diag 2
                    GetLabel(22).Text == target &&
                    GetLabel(31).Text == target)
                    {
                        return new KeyValuePair<bool, string>(true, target);
                    }
            }

            if (clickNum == 9) return new KeyValuePair<bool, string>(true, "Tie");

            return new KeyValuePair<bool, string>(false, null); // no winner
        }

        private void Clicked(object sender, EventArgs e)
        {

            Label s = (Label)sender;

            int pos = int.Parse(s.Name.Split('_')[1]);

            if (s.Text != "-") return;

            s.Text = (prev?"X":"O");
            prev = !prev;
            clickNum++;

            KeyValuePair<bool, string> c = Check();
            if (c.Key) Win(c.Value);
        }




        private void btnNew_Click(object sender, EventArgs e)
        { 
            reset();
        }
    }
}
