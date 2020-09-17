using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yatzy
{
    public partial class frmMain : Form
    {
        PictureBox[] savedDice = new PictureBox[5];
        PictureBox[] rolledDice = new PictureBox[5];
        public frmMain()
        {
            InitializeComponent();
            savedDice[0] = pbSavedDice1;
            savedDice[1] = pbSavedDice2;
            savedDice[2] = pbSavedDice3;
            savedDice[3] = pbSavedDice4;
            savedDice[4] = pbSavedDice5;
            rolledDice[0] = pbSavedDice1;
            rolledDice[1] = pbSavedDice2;
            rolledDice[2] = pbSavedDice3;
            rolledDice[3] = pbSavedDice4;
            rolledDice[4] = pbSavedDice5;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            for (int i = 0; i < 5; i++)
            {
                if (savedDice[i].Image == null)
                {
                    savedDice[i].Image = pb.Image;
                    break;
                }
            }
            pb.Image = null;
        }
    }
}
