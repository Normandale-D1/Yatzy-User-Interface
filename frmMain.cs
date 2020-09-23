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
using System.Reflection;

namespace Yatzy
{
    public partial class frmMain : Form
    {
        PictureBox[] savedDice = new PictureBox[5];
        PictureBox[] rolledDice = new PictureBox[5];
        PictureBox draggedPicture = new PictureBox();
        Image[] diceImages = new Image[6];
        public frmMain()
        {
            InitializeComponent();
            savedDice[0] = pbSavedDice1;
            savedDice[1] = pbSavedDice2;
            savedDice[2] = pbSavedDice3;
            savedDice[3] = pbSavedDice4;
            savedDice[4] = pbSavedDice5;
            rolledDice[0] = pbRolledDice1;
            rolledDice[1] = pbRolledDice2;
            rolledDice[2] = pbRolledDice3;
            rolledDice[3] = pbRolledDice4;
            rolledDice[4] = pbRolledDice5;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void moveDiceToSaved(object sender, EventArgs e)
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
        private void moveDiceToRolled(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            for (int i = 0; i < 5; i++)
            {
                if (rolledDice[i].Image == null)
                {
                    rolledDice[i].Image = pb.Image;
                    break;
                }
            }
            pb.Image = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DiceMouseDown(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;

            // Start the drag if it's the right mouse button.
            if (e.Button == MouseButtons.Right && p.Image != null)
            {
                draggedPicture = p;
                p.DoDragDrop(p.Image,
                    DragDropEffects.Copy);
            }
        }
        private void DiceDragDrop(object sender, DragEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Image == null)
            {
                p.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
            }
            draggedPicture.Image = null;
        }
        private void DiceDragEnter(object sender, DragEventArgs e)
        {
               // See if this is a copy and the data includes an image.
                if (e.Data.GetDataPresent(DataFormats.Bitmap) &&
                (e.AllowedEffect & DragDropEffects.Copy) != 0)
                {
                    // Allow this.
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    // Don't allow any other drop.
                    e.Effect = DragDropEffects.None;
                }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/41750462/dragn-drop-tell-the-sender-when-drop-occurs
            //http://csharphelper.com/blog/2015/02/drag-and-drop-images-in-c/ 

            foreach (PictureBox pb in savedDice)
            {
                pb.AllowDrop = true;
                pb.MouseDown += DiceMouseDown;
                pb.DragEnter += DiceDragEnter;
                pb.DragDrop += DiceDragDrop;
            }
            foreach (PictureBox pb in rolledDice)
            {
                pb.AllowDrop = true;
                pb.MouseDown += DiceMouseDown;
                pb.DragEnter += DiceDragEnter;
                pb.DragDrop += DiceDragDrop;
            }

            Assembly myAssembly = Assembly.GetExecutingAssembly();

            Stream myStream = myAssembly.GetManifestResourceStream("Yatzy.Properties.Dice1.jpg");
            Bitmap bmp = new Bitmap(myStream);
            diceImages[0] = bmp;

            myStream = myAssembly.GetManifestResourceStream("Yatzy.Properties.Dice2.jpg");
            bmp = new Bitmap(myStream);
            diceImages[1] = bmp;

            myStream = myAssembly.GetManifestResourceStream("Yatzy.Properties.Dice3.jpg");
            bmp = new Bitmap(myStream);
            diceImages[2] = bmp;

            myStream = myAssembly.GetManifestResourceStream("Yatzy.Properties.Dice4.jpg");
            bmp = new Bitmap(myStream);
            diceImages[3] = bmp;

            myStream = myAssembly.GetManifestResourceStream("Yatzy.Properties.Dice5.jpg");
            bmp = new Bitmap(myStream);
            diceImages[4] = bmp;

            myStream = myAssembly.GetManifestResourceStream("Yatzy.Properties.Dice6.jpg");
            bmp = new Bitmap(myStream);
            diceImages[5] = bmp;

        }

    }
}

