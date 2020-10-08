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
using YatzyPlayer;

namespace Yatzy
{
    public partial class frmMain : Form
    {
        private Player player1;
        private PictureBox draggedPicture = new PictureBox();
        private readonly Image[] diceImages = new Image[6];
        private readonly PictureBox[] diceArray = new PictureBox[10];
        private string draggedDice;
        private string droppedDice;

        public frmMain()
        {
            InitializeComponent();

            diceArray[0] = pbRolledDice1;
            diceArray[1] = pbRolledDice2;
            diceArray[2] = pbRolledDice3;
            diceArray[3] = pbRolledDice4;
            diceArray[4] = pbRolledDice5;
            diceArray[5] = pbSavedDice1;
            diceArray[6] = pbSavedDice2;
            diceArray[7] = pbSavedDice3;
            diceArray[8] = pbSavedDice4;
            diceArray[9] = pbSavedDice5;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void MoveDice(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;

            draggedDice = pb.Name;
            int start = 0;
            int end = 5;            

            if (GetDiceArrayElement(draggedDice) < 5)
            {
                start = 5;
                end = 10;
            }

            for (int i = start; i < end; i++)
            {
                if (diceArray[i].Image == null)
                {
                    diceArray[i].Image = pb.Image;
                    droppedDice = diceArray[i].Name;
                    LoadDiceValue();
                    break;
                }
            }
            pb.Image = null;
        }
        
        private void LoadDiceValue()
        {
            int d1 = GetDiceArrayElement(draggedDice);
            int d2 = GetDiceArrayElement(droppedDice);
            player1.MoveDice(d1, d2);
        }
        private void DiceMouseMoved(object sender, MouseEventArgs e)
        {           
            // Start the drag if it's the left mouse button.
            if (e.Button == MouseButtons.Left)
            {
                PictureBox p = (PictureBox)sender;
                if (p.Image != null)
                {
                    draggedPicture = p;
                    p.DoDragDrop(p.Image,
                        DragDropEffects.Copy);
                    draggedDice = p.Name;
                    LoadDiceValue();
                }
            }
        }
        private int GetDiceArrayElement(string ElementToFind)
        {
            int ArrayElement = -1;
            for (int i = 0; i < 10; i++)
            {
                PictureBox p = diceArray[i];
                if (p.Name == ElementToFind)
                {
                    ArrayElement = i;
                    break;
                }
            }
            return ArrayElement;
        }
        private void DiceDragDrop(object sender, DragEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Image == null)
            {
                p.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap, true);
                draggedPicture.Image = null;
            }
        }
        private void DiceDragEnter(object sender, DragEventArgs e)
        {
               // See if this is a copy and the data includes an image.
                if (e.Data.GetDataPresent(DataFormats.Bitmap) && (e.AllowedEffect & DragDropEffects.Copy) != 0)
                {
                PictureBox p = (PictureBox)sender;
                if (p.Image == null)
                {
                    // Allow this.
                    e.Effect = DragDropEffects.Copy;
                    droppedDice = p.Name;
                }
            }
                else
                {
                    // Don't allow any other drop.
                    e.Effect = DragDropEffects.None;
                }
        }
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPlayer1.Text))
            {
                player1 = new Player(tbPlayer1.Text);
                btnRollDice.Enabled = true;
            }
        }
        private void btnRollDice_Click(object sender, EventArgs e)
        {
            player1.RollDice();
            for (int i = 0; i < 5; i++)
            {
                int imageIndex = player1.RolledDice[i]-1;
                diceArray[i].Image = diceImages[imageIndex];
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //https://stackoverflow.com/questions/41750462/dragn-drop-tell-the-sender-when-drop-occurs
            //http://csharphelper.com/blog/2015/02/drag-and-drop-images-in-c/ 

            for (int i = 0; i < 10; i++)
            {
                PictureBox pb = diceArray[i];
                pb.AllowDrop = true;
                pb.DoubleClick += MoveDice;
                pb.MouseMove += DiceMouseMoved;
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

            btnRollDice.Enabled = false;
        }
    }
}

