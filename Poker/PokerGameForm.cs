using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Poker
{

    public partial class PokerGameForm : Form
    {
        #region Variables
        readonly ProgressBar progresBar = new ProgressBar();
        // TODO 
        private int nm;
        readonly Panel playerPanel = new Panel();
        readonly Panel firstBotPanel = new Panel();
        readonly Panel secondBotPanel = new Panel();
        readonly Panel thirdBotPanel = new Panel();
        readonly Panel fourthBotPanel = new Panel();
        readonly Panel fifthBotPanel = new Panel();
        // TODO 
        private int call = 500;
        private int foldedPlayers = 5;
        // for now is static Max chips for any in the table
        private static int playerMaxChips = 10000;
        private static int firstBotMaxChips = 10000;
        private static int secondBotMaxChips = 10000;
        private static int thirdBotMaxchips = 10000; 
        private static int fourthBotMaxChips = 10000;
        private static int fifthBotMaxChips = 10000;
        private double type;
        private int rounds = 0;
        private double firstBotPowerHand;
        private double secondBotPowerHand;
        private double thirdBotPowerHand;
        private double fourtBotPowerHand;
        private double fiftBotPowerHand;
        private double playerPowerHand ;
        private int raise = 0;
        private double playerType = -1;
        private double b1Type = -1;
        private double b2Type = -1;
        private double b3Type = -1;
        private double b4Type = -1;
        private double b5Type = -1;
        private bool firstBotMove = false;
        private bool secondBotMove = false;
        private bool thirdBotMove = false;
        private bool fourtBotMove = false;
        private bool fifthBotMove = false;
        private bool b1Fturn = false;
        private bool b2Fturn = false;
        private bool b3Fturn = false;
        private bool b4Fturn = false;
        private bool b5Fturn = false;
        private bool pFolded;
        private bool b1Folded;
        private bool b2Folded;
        private bool b3Folded;
        private bool b4Folded;
        private bool b5Folded;
        private bool intsadded;
        private bool changed;
        private int pCall ;
        private int b1Call ;
        private int b2Call ;
        private int b3Call ;
        private int b4Call ;
        private int b5Call ;
        private int pRaise ;
        private int b1Raise ;
        private int b2Raise ;
        private int b3Raise ;
        private int b4Raise ;
        private int b5Raise ;
        private int height;
        private int width;
        private int winners ;
        private int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int maxLeft = 6;
        protected static int Last = 123;
        private int raisedTurn = 1;
        private readonly List<bool?> bools = new List<bool?>();
        private readonly List<Type> win = new List<Type>();
        private readonly List<string> checkWinners = new List<string>();
        private readonly List<int> ints = new List<int>();
        private bool pFturn = false;
        private bool pturn = true;
        private bool restart = false;
        private bool raising = false;
        private Poker.Type sorted;
        private string[] imgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        private readonly int[] reserve = new int[17];
        private readonly Image[] deck = new Image[52];
        private readonly PictureBox[] holder = new PictureBox[52];
        private readonly Timer timer = new Timer();
        private readonly Timer updates = new Timer();
        private int t = 60;
        private int i;
        private int bb = 500;
        private int sb = 250;
        private int up = 10000000;
        int turnCount = 0;

        #endregion
        public PokerGameForm()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            call = bb;
            MaximizeBox = false;
            MinimizeBox = false;
            updates.Start();
            InitializeComponent();
            width = this.Width;
            height = this.Height;
            Shuffle();
            tbPot.Enabled = false;
            tableChips.Enabled = false;
            tbBotChips1.Enabled = false;
            tbBotChips2.Enabled = false;
            tbBotChips3.Enabled = false;
            tbBotChips4.Enabled = false;
            tbBotChips5.Enabled = false;
            tableChips.Text =  @"Chips : " + playerMaxChips;
            tbBotChips1.Text = @"Chips : " + firstBotMaxChips;
            tbBotChips2.Text = @"Chips : " + secondBotMaxChips;
            tbBotChips3.Text = @"Chips : " + thirdBotMaxchips;
            tbBotChips4.Text = @"Chips : " + fourthBotMaxChips;
            tbBotChips5.Text = @"Chips : " + fifthBotMaxChips;
            timer.Interval = (1 * 1 * 1000);
            timer.Tick += timer_Tick;
            updates.Interval = (1 * 1 * 100);
            updates.Tick += Update_Tick;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = true;
            tbSB.Visible = true;
            bBB.Visible = true;
            bSB.Visible = true;
            tbBB.Visible = false;
            tbSB.Visible = false;
            bBB.Visible = false;
            bSB.Visible = false;
            tbRaise.Text = (bb * 2).ToString();
        }
        async Task Shuffle()
        {
            bools.Add(pFturn); bools.Add(b1Fturn); bools.Add(b2Fturn); bools.Add(b3Fturn); bools.Add(b4Fturn); bools.Add(b5Fturn);
            buttonCall.Enabled = false;
            buttonRaise.Enabled = false;
            buttonFold.Enabled = false;
            buttonCheck.Enabled = false;
            MaximizeBox = false;
            MinimizeBox = false;
            bool check = false;
            Bitmap backImage = new Bitmap("Assets\\Back\\Back.png");
            int horizontal = 580, vertical = 480;
            Random r = new Random();
            for (i = imgLocation.Length; i > 0; i--)
            {
                int j = r.Next(i);
                var k = imgLocation[j];
                imgLocation[j] = imgLocation[i - 1];
                imgLocation[i - 1] = k;
            }
            for (i = 0; i < 17; i++)
            {

                deck[i] = Image.FromFile(imgLocation[i]);
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                foreach (var c in charsToRemove)
                {
                    imgLocation[i] = imgLocation[i].Replace(c, string.Empty);
                }
                reserve[i] = int.Parse(imgLocation[i]) - 1;
                holder[i] = new PictureBox();
                holder[i].SizeMode = PictureBoxSizeMode.StretchImage;
                holder[i].Height = 130;
                holder[i].Width = 80;
                this.Controls.Add(holder[i]);
                holder[i].Name = "pb" + i.ToString();
                await Task.Delay(200);
                #region Throwing Cards
                if (i < 2)
                {
                    if (holder[0].Tag != null)
                    {
                        holder[1].Tag = reserve[1];
                    }
                    holder[0].Tag = reserve[0];
                    holder[i].Image = deck[i];
                    holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    holder[i].Location = new Point(horizontal, vertical);
                    horizontal += holder[i].Width;
                    this.Controls.Add(playerPanel);
                    playerPanel.Location = new Point(holder[0].Left - 10, holder[0].Top - 10);
                    playerPanel.BackColor = Color.DarkBlue;
                    playerPanel.Height = 150;
                    playerPanel.Width = 180;
                    playerPanel.Visible = false;
                }
                if (firstBotMaxChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 2 && i < 4)
                    {
                        if (holder[2].Tag != null)
                        {
                            holder[3].Tag = reserve[3];
                        }
                        holder[2].Tag = reserve[2];
                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }
                        check = true;
                        holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        holder[i].Location = new Point(horizontal, vertical);
                        horizontal += holder[i].Width;
                        holder[i].Visible = true;
                        this.Controls.Add(firstBotPanel);
                        firstBotPanel.Location = new Point(holder[2].Left - 10, holder[2].Top - 10);
                        firstBotPanel.BackColor = Color.DarkBlue;
                        firstBotPanel.Height = 150;
                        firstBotPanel.Width = 180;
                        firstBotPanel.Visible = false;
                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }
                if (secondBotMaxChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 4 && i < 6)
                    {
                        if (holder[4].Tag != null)
                        {
                            holder[5].Tag = reserve[5];
                        }
                        holder[4].Tag = reserve[4];
                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }
                        check = true;
                        holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        holder[i].Location = new Point(horizontal, vertical);
                        horizontal += holder[i].Width;
                        holder[i].Visible = true;
                        this.Controls.Add(secondBotPanel);
                        secondBotPanel.Location = new Point(holder[4].Left - 10, holder[4].Top - 10);
                        secondBotPanel.BackColor = Color.DarkBlue;
                        secondBotPanel.Height = 150;
                        secondBotPanel.Width = 180;
                        secondBotPanel.Visible = false;
                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }
                if (thirdBotMaxchips > 0)
                {
                    foldedPlayers--;
                    if (i >= 6 && i < 8)
                    {
                        if (holder[6].Tag != null)
                        {
                            holder[7].Tag = reserve[7];
                        }
                        holder[6].Tag = reserve[6];
                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }
                        check = true;
                        holder[i].Anchor = (AnchorStyles.Top);
                        holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        holder[i].Location = new Point(horizontal, vertical);
                        horizontal += holder[i].Width;
                        holder[i].Visible = true;
                        this.Controls.Add(thirdBotPanel);
                        thirdBotPanel.Location = new Point(holder[6].Left - 10, holder[6].Top - 10);
                        thirdBotPanel.BackColor = Color.DarkBlue;
                        thirdBotPanel.Height = 150;
                        thirdBotPanel.Width = 180;
                        thirdBotPanel.Visible = false;
                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }
                if (fourthBotMaxChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 8 && i < 10)
                    {
                        if (holder[8].Tag != null)
                        {
                            holder[9].Tag = reserve[9];
                        }
                        holder[8].Tag = reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }
                        check = true;
                        holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        holder[i].Location = new Point(horizontal, vertical);
                        horizontal += holder[i].Width;
                        holder[i].Visible = true;
                        this.Controls.Add(fourthBotPanel);
                        fourthBotPanel.Location = new Point(holder[8].Left - 10, holder[8].Top - 10);
                        fourthBotPanel.BackColor = Color.DarkBlue;
                        fourthBotPanel.Height = 150;
                        fourthBotPanel.Width = 180;
                        fourthBotPanel.Visible = false;
                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }
                if (fifthBotMaxChips > 0)
                {
                    foldedPlayers--;
                    if (i >= 10 && i < 12)
                    {
                        if (holder[10].Tag != null)
                        {
                            holder[11].Tag = reserve[11];
                        }
                        holder[10].Tag = reserve[10];
                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }
                        check = true;
                        holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        holder[i].Location = new Point(horizontal, vertical);
                        horizontal += holder[i].Width;
                        holder[i].Visible = true;
                        this.Controls.Add(fifthBotPanel);
                        fifthBotPanel.Location = new Point(holder[10].Left - 10, holder[10].Top - 10);
                        fifthBotPanel.BackColor = Color.DarkBlue;
                        fifthBotPanel.Height = 150;
                        fifthBotPanel.Width = 180;
                        fifthBotPanel.Visible = false;
                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }
                if (i >= 12)
                {
                    holder[12].Tag = reserve[12];
                    if (i > 12) holder[13].Tag = reserve[13];
                    if (i > 13) holder[14].Tag = reserve[14];
                    if (i > 14) holder[15].Tag = reserve[15];
                    if (i > 15)
                    {
                        holder[16].Tag = reserve[16];

                    }
                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }
                    check = true;
                    if (holder[i] != null)
                    {
                        holder[i].Anchor = AnchorStyles.None;
                        holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        holder[i].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }
                #endregion
                if (firstBotMaxChips <= 0)
                {
                    b1Fturn = true;
                    holder[2].Visible = false;
                    holder[3].Visible = false;
                }
                else
                {
                    b1Fturn = false;
                    if (i == 3)
                    {
                        if (holder[3] != null)
                        {
                            holder[2].Visible = true;
                            holder[3].Visible = true;
                        }
                    }
                }
                if (secondBotMaxChips <= 0)
                {
                    b2Fturn = true;
                    holder[4].Visible = false;
                    holder[5].Visible = false;
                }
                else
                {
                    b2Fturn = false;
                    if (i == 5)
                    {
                        if (holder[5] != null)
                        {
                            holder[4].Visible = true;
                            holder[5].Visible = true;
                        }
                    }
                }
                if (thirdBotMaxchips <= 0)
                {
                    b3Fturn = true;
                    holder[6].Visible = false;
                    holder[7].Visible = false;
                }
                else
                {
                    b3Fturn = false;
                    if (i == 7)
                    {
                        if (holder[7] != null)
                        {
                            holder[6].Visible = true;
                            holder[7].Visible = true;
                        }
                    }
                }
                if (fourthBotMaxChips <= 0)
                {
                    b4Fturn = true;
                    holder[8].Visible = false;
                    holder[9].Visible = false;
                }
                else
                {
                    b4Fturn = false;
                    if (i == 9)
                    {
                        if (holder[9] != null)
                        {
                            holder[8].Visible = true;
                            holder[9].Visible = true;
                        }
                    }
                }
                if (fifthBotMaxChips <= 0)
                {
                    b5Fturn = true;
                    holder[10].Visible = false;
                    holder[11].Visible = false;
                }
                else
                {
                    b5Fturn = false;
                    if (i == 11)
                    {
                        if (holder[11] != null)
                        {
                            holder[10].Visible = true;
                            holder[11].Visible = true;
                        }
                    }
                }
                if (i == 16)
                {
                    if (!restart)
                    {
                        MaximizeBox = true;
                        MinimizeBox = true;
                    }
                    timer.Start();
                }
            }
            if (foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show("Would You Like To Play Again ?", "You Won , Congratulations ! ", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            else
            {
                foldedPlayers = 5;
            }
            if (i == 17)
            {
                buttonRaise.Enabled = true;
                buttonCall.Enabled = true;
                buttonRaise.Enabled = true;
                buttonRaise.Enabled = true;
                buttonFold.Enabled = true;
            }
        }
        async Task Turns()
        {
            #region Rotating
            if (!pFturn)
            {
                if (pturn)
                {
                    FixCall(pStatus, ref pCall, ref pRaise, 1);
                    //MessageBox.Show("Player's Turn");
                    progressBarTimer.Visible = true;
                    progressBarTimer.Value = 1000;
                    t = 60;
                    up = 10000000;
                    timer.Start();
                    buttonRaise.Enabled = true;
                    buttonCall.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    turnCount++;
                    FixCall(pStatus, ref pCall, ref pRaise, 2);
                }
            }
            if (pFturn || !pturn)
            {
                await AllIn();
                if (pFturn && !pFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false || buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
                    }
                }
                await CheckRaise(0, 0);
                progressBarTimer.Visible = false;
                buttonRaise.Enabled = false;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                timer.Stop();
                firstBotMove = true;
                if (!b1Fturn)
                {
                    if (firstBotMove)
                    {
                        FixCall(b1Status, ref b1Call, ref b1Raise, 1);
                        FixCall(b1Status, ref b1Call, ref b1Raise, 2);
                        Rules(2, 3, "Bot 1", ref b1Type, ref firstBotPowerHand, b1Fturn);
                        MessageBox.Show("Bot 1's Turn");
                        Ai(2, 3, ref firstBotMaxChips, ref firstBotMove, ref b1Fturn, b1Status, 0, firstBotPowerHand, b1Type);
                        turnCount++;
                        Last = 1;
                        firstBotMove = false;
                        secondBotMove = true;
                    }
                }
                if (b1Fturn && !b1Folded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);
                    maxLeft--;
                    b1Folded = true;
                }
                if (b1Fturn || !firstBotMove)
                {
                    await CheckRaise(1, 1);
                    secondBotMove = true;
                }
                if (!b2Fturn)
                {
                    if (secondBotMove)
                    {
                        FixCall(b2Status, ref b2Call, ref b2Raise, 1);
                        FixCall(b2Status, ref b2Call, ref b2Raise, 2);
                        Rules(4, 5, "Bot 2", ref b2Type, ref secondBotPowerHand, b2Fturn);
                        MessageBox.Show("Bot 2's Turn");
                        Ai(4, 5, ref secondBotMaxChips, ref secondBotMove, ref b2Fturn, b2Status, 1, secondBotPowerHand, b2Type);
                        turnCount++;
                        Last = 2;
                        secondBotMove = false;
                        thirdBotMove = true;
                    }
                }
                if (b2Fturn && !b2Folded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
                    b2Folded = true;
                }
                if (b2Fturn || !secondBotMove)
                {
                    await CheckRaise(2, 2);
                    thirdBotMove = true;
                }
                if (!b3Fturn)
                {
                    if (thirdBotMove)
                    {
                        FixCall(b3Status, ref b3Call, ref b3Raise, 1);
                        FixCall(b3Status, ref b3Call, ref b3Raise, 2);
                        Rules(6, 7, "Bot 3", ref b3Type, ref thirdBotPowerHand, b3Fturn);
                        MessageBox.Show("Bot 3's Turn");
                        Ai(6, 7, ref thirdBotMaxchips, ref thirdBotMove, ref b3Fturn, b3Status, 2, thirdBotPowerHand, b3Type);
                        turnCount++;
                        Last = 3;
                        thirdBotMove = false;
                        fourtBotMove = true;
                    }
                }
                if (b3Fturn && !b3Folded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
                    b3Folded = true;
                }
                if (b3Fturn || !thirdBotMove)
                {
                    await CheckRaise(3, 3);
                    fourtBotMove = true;
                }
                if (!b4Fturn)
                {
                    if (fourtBotMove)
                    {
                        FixCall(b4Status, ref b4Call, ref b4Raise, 1);
                        FixCall(b4Status, ref b4Call, ref b4Raise, 2);
                        Rules(8, 9, "Bot 4", ref b4Type, ref fourtBotPowerHand, b4Fturn);
                        MessageBox.Show("Bot 4's Turn");
                        Ai(8, 9, ref fourthBotMaxChips, ref fourtBotMove, ref b4Fturn, b4Status, 3, fourtBotPowerHand, b4Type);
                        turnCount++;
                        Last = 4;
                        fourtBotMove = false;
                        fifthBotMove = true;
                    }
                }
                if (b4Fturn && !b4Folded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);
                    maxLeft--;
                    b4Folded = true;
                }
                if (b4Fturn || !fourtBotMove)
                {
                    await CheckRaise(4, 4);
                    fifthBotMove = true;
                }
                if (!b5Fturn)
                {
                    if (fifthBotMove)
                    {
                        FixCall(b5Status, ref b5Call, ref b5Raise, 1);
                        FixCall(b5Status, ref b5Call, ref b5Raise, 2);
                        Rules(10, 11, "Bot 5", ref b5Type, ref fiftBotPowerHand, b5Fturn);
                        MessageBox.Show("Bot 5's Turn");
                        Ai(10, 11, ref fifthBotMaxChips, ref fifthBotMove, ref b5Fturn, b5Status, 4, fiftBotPowerHand, b5Type);
                        turnCount++;
                        Last = 5;
                        fifthBotMove = false;
                    }
                }
                if (b5Fturn && !b5Folded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);
                    maxLeft--;
                    b5Folded = true;
                }
                if (b5Fturn || !fifthBotMove)
                {
                    await CheckRaise(5, 5);
                    pturn = true;
                }
                if (pFturn && !pFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false || buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);
                        maxLeft--;
                        pFolded = true;
                    }
                }
                #endregion
                await AllIn();
                if (!restart)
                {
                    await Turns();
                }
                restart = false;
            }
        }

        void Rules(int c1, int c2, string currentText, ref double current, ref double power, bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }
            if (!foldedTurn || c1 == 0 && c2 == 1 && pStatus.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] straight1 = new int[5];
                int[] straight = new int[7];
                straight[0] = reserve[c1];
                straight[1] = reserve[c2];
                straight1[0] = straight[2] = reserve[12];
                straight1[1] = straight[3] = reserve[13];
                straight1[2] = straight[4] = reserve[14];
                straight1[3] = straight[5] = reserve[15];
                straight1[4] = straight[6] = reserve[16];
                var a = straight.Where(o => o % 4 == 0).ToArray();
                var b = straight.Where(o => o % 4 == 1).ToArray();
                var c = straight.Where(o => o % 4 == 2).ToArray();
                var d = straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
                #endregion
                for (i = 0; i < 16; i++)
                {
                    if (reserve[i] == int.Parse(holder[c1].Tag.ToString()) && reserve[i + 1] == int.Parse(holder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        RPairFromHand(ref current, ref power);

                        #region Pair or Two Pair from Table current = 2 || 0
                        RPairTwoPair(ref current, ref power);
                        #endregion

                        #region Two Pair current = 2
                        RTwoPair(ref current, ref power);
                        #endregion

                        #region Three of a kind current = 3
                        RThreeOfAKind(ref current, ref power, straight);
                        #endregion

                        #region Straight current = 4
                        RStraight(ref current, ref power, straight);
                        #endregion

                        #region Flush current = 5 || 5.5
                        RFlush(ref current, ref power, ref vf, straight1);
                        #endregion

                        #region Full House current = 6
                        RFullHouse(ref current, ref power, ref done, straight);
                        #endregion

                        #region Four of a Kind current = 7
                        RFourOfAKind(ref current, ref power, straight);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        RStraightFlush(ref current, ref power, st1, st2, st3, st4);
                        #endregion

                        #region High Card current = -1
                        RHighCard(ref current, ref power);
                        #endregion
                    }
                }
            }
        }
        private void RStraightFlush(ref double current, ref double power, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (current >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;
                        power = (st1.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 8 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st1[0] == 0 && st1[1] == 9 && st1[2] == 10 && st1[3] == 11 && st1[0] + 12 == st1[4])
                    {
                        current = 9;
                        power = (st1.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 9 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;
                        power = (st2.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 8 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        current = 9;
                        power = (st2.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 9 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;
                        power = (st3.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 8 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        current = 9;
                        power = (st3.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 9 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;
                        power = (st4.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 8 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        current = 9;
                        power = (st4.Max()) / 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 9 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void RFourOfAKind(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (straight[j] / 4 == straight[j + 1] / 4 && straight[j] / 4 == straight[j + 2] / 4 &&
                        straight[j] / 4 == straight[j + 3] / 4)
                    {
                        current = 7;
                        power = (straight[j] / 4) * 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 7 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (straight[j] / 4 == 0 && straight[j + 1] / 4 == 0 && straight[j + 2] / 4 == 0 && straight[j + 3] / 4 == 0)
                    {
                        current = 7;
                        power = 13 * 4 + current * 100;
                        win.Add(new Type() { Power = power, Current = 7 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void RFullHouse(ref double current, ref double power, ref bool done, int[] straight)
        {
            if (current >= -1)
            {
                type = power;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                current = 6;
                                power = 13 * 2 + current * 100;
                                win.Add(new Type() { Power = power, Current = 6 });
                                sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                            if (fh.Max() / 4 > 0)
                            {
                                current = 6;
                                power = fh.Max() / 4 * 2 + current * 100;
                                win.Add(new Type() { Power = power, Current = 6 });
                                sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                                break;
                            }
                        }
                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                power = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                power = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }
                if (current != 6)
                {
                    power = type;
                }
            }
        }
        private void RFlush(ref double current, ref double power, ref bool vf, int[] straight1)
        {
            if (current >= -1)
            {
                var f1 = straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f1[0] % 4)
                    {
                        if (reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f1.Max() / 4 && reserve[i + 1] / 4 < f1.Max() / 4)
                        {
                            current = 5;
                            power = f1.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f1[0] % 4)
                    {
                        if (reserve[i] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f1.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f1[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f1.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f1.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f1.Length == 5)
                {
                    if (reserve[i] % 4 == f1[0] % 4 && reserve[i] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f1[0] % 4 && reserve[i + 1] / 4 > f1.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i + 1] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f1.Min() / 4 && reserve[i + 1] / 4 < f1.Min())
                    {
                        current = 5;
                        power = f1.Max() + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f2[0] % 4)
                    {
                        if (reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f2.Max() / 4 && reserve[i + 1] / 4 < f2.Max() / 4)
                        {
                            current = 5;
                            power = f2.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f2[0] % 4)
                    {
                        if (reserve[i] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f2.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f2[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f2.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f2.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f2.Length == 5)
                {
                    if (reserve[i] % 4 == f2[0] % 4 && reserve[i] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f2[0] % 4 && reserve[i + 1] / 4 > f2.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i + 1] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f2.Min() / 4 && reserve[i + 1] / 4 < f2.Min())
                    {
                        current = 5;
                        power = f2.Max() + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f3[0] % 4)
                    {
                        if (reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f3.Max() / 4 && reserve[i + 1] / 4 < f3.Max() / 4)
                        {
                            current = 5;
                            power = f3.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f3[0] % 4)
                    {
                        if (reserve[i] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f3.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f3[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f3.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f3.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f3.Length == 5)
                {
                    if (reserve[i] % 4 == f3[0] % 4 && reserve[i] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f3[0] % 4 && reserve[i + 1] / 4 > f3.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i + 1] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f3.Min() / 4 && reserve[i + 1] / 4 < f3.Min())
                    {
                        current = 5;
                        power = f3.Max() + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 && reserve[i] % 4 == f4[0] % 4)
                    {
                        if (reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        if (reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else if (reserve[i] / 4 < f4.Max() / 4 && reserve[i + 1] / 4 < f4.Max() / 4)
                        {
                            current = 5;
                            power = f4.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 4)//different cards in hand
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 && reserve[i] % 4 == f4[0] % 4)
                    {
                        if (reserve[i] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f4.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                    if (reserve[i + 1] % 4 != reserve[i] % 4 && reserve[i + 1] % 4 == f4[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > f4.Max() / 4)
                        {
                            current = 5;
                            power = reserve[i + 1] + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                        else
                        {
                            current = 5;
                            power = f4.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 5 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                            vf = true;
                        }
                    }
                }
                if (f4.Length == 5)
                {
                    if (reserve[i] % 4 == f4[0] % 4 && reserve[i] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    if (reserve[i + 1] % 4 == f4[0] % 4 && reserve[i + 1] / 4 > f4.Min() / 4)
                    {
                        current = 5;
                        power = reserve[i + 1] + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (reserve[i] / 4 < f4.Min() / 4 && reserve[i + 1] / 4 < f4.Min())
                    {
                        current = 5;
                        power = f4.Max() + current * 100;
                        win.Add(new Type() { Power = power, Current = 5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f2.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f3.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
                if (f4.Length > 0)
                {
                    if (reserve[i] / 4 == 0 && reserve[i] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                    if (reserve[i + 1] / 4 == 0 && reserve[i + 1] % 4 == f4[0] % 4 && vf)
                    {
                        current = 5.5;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 5.5 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void RStraight(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                var op = straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            power = op.Max() + current * 100;
                            win.Add(new Type() { Power = power, Current = 4 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            power = op[j + 4] + current * 100;
                            win.Add(new Type() { Power = power, Current = 4 });
                            sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        power = 13 + current * 100;
                        win.Add(new Type() { Power = power, Current = 4 });
                        sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }
        private void RThreeOfAKind(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            current = 3;
                            power = 13 * 3 + current * 100;
                            win.Add(new Type() { Power = power, Current = 3 });
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            power = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + current * 100;
                            win.Add(new Type() { Power = power, Current = 3 });
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }
        private void RTwoPair(ref double current, ref double power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (reserve[i] / 4 != reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (reserve[i] / 4 == reserve[tc] / 4 && reserve[i + 1] / 4 == reserve[tc - k] / 4 ||
                                    reserve[i + 1] / 4 == reserve[tc] / 4 && reserve[i] / 4 == reserve[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            power = 13 * 4 + (reserve[i + 1] / 4) * 2 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            power = 13 * 4 + (reserve[i] / 4) * 2 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i + 1] / 4 != 0 && reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (reserve[i] / 4) * 2 + (reserve[i + 1] / 4) * 2 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void RPairTwoPair(ref double current, ref double power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (reserve[tc] / 4 == reserve[tc - k] / 4)
                            {
                                if (reserve[tc] / 4 != reserve[i] / 4 && reserve[tc] / 4 != reserve[i + 1] / 4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;
                                            power = (reserve[i] / 4) * 2 + 13 * 4 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i] / 4 == 0)
                                        {
                                            current = 2;
                                            power = (reserve[i + 1] / 4) * 2 + 13 * 4 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i + 1] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (reserve[tc] / 4) * 2 + (reserve[i + 1] / 4) * 2 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (reserve[i] / 4 != 0)
                                        {
                                            current = 2;
                                            power = (reserve[tc] / 4) * 2 + (reserve[i] / 4) * 2 + current * 100;
                                            win.Add(new Type() { Power = power, Current = 2 });
                                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (reserve[i] / 4 > reserve[i + 1] / 4)
                                        {
                                            if (reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                power = 13 + reserve[i] / 4 + current * 100;
                                                win.Add(new Type() { Power = power, Current = 1 });
                                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                power = reserve[tc] / 4 + reserve[i] / 4 + current * 100;
                                                win.Add(new Type() { Power = power, Current = 1 });
                                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (reserve[tc] / 4 == 0)
                                            {
                                                current = 0;
                                                power = 13 + reserve[i + 1] + current * 100;
                                                win.Add(new Type() { Power = power, Current = 1 });
                                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                power = reserve[tc] / 4 + reserve[i + 1] / 4 + current * 100;
                                                win.Add(new Type() { Power = power, Current = 1 });
                                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void RPairFromHand(ref double current, ref double power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (reserve[i] / 4 == reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (reserve[i] / 4 == 0)
                        {
                            current = 1;
                            power = 13 * 4 + current * 100;
                            win.Add(new Type() { Power = power, Current = 1 });
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            power = (reserve[i + 1] / 4) * 4 + current * 100;
                            win.Add(new Type() { Power = power, Current = 1 });
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (reserve[i + 1] / 4 == reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i + 1] / 4 == 0)
                            {
                                current = 1;
                                power = 13 * 4 + reserve[i] / 4 + current * 100;
                                win.Add(new Type() { Power = power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                power = (reserve[i + 1] / 4) * 4 + reserve[i] / 4 + current * 100;
                                win.Add(new Type() { Power = power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (reserve[i] / 4 == reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i] / 4 == 0)
                            {
                                current = 1;
                                power = 13 * 4 + reserve[i + 1] / 4 + current * 100;
                                win.Add(new Type() { Power = power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                power = (reserve[tc] / 4) * 4 + reserve[i + 1] / 4 + current * 100;
                                win.Add(new Type() { Power = power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }
        private void RHighCard(ref double current, ref double power)
        {
            if (current == -1)
            {
                if (reserve[i] / 4 > reserve[i + 1] / 4)
                {
                    current = -1;
                    power = reserve[i] / 4;
                    win.Add(new Type() { Power = power, Current = -1 });
                    sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    power = reserve[i + 1] / 4;
                    win.Add(new Type() { Power = power, Current = -1 });
                    sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (reserve[i] / 4 == 0 || reserve[i + 1] / 4 == 0)
                {
                    current = -1;
                    power = 13;
                    win.Add(new Type() { Power = power, Current = -1 });
                    sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(double current, double power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (holder[j].Visible)
                    holder[j].Image = deck[j];
            }
            if (current == sorted.Current)
            {
                if (power == sorted.Power)
                {
                    winners++;
                    checkWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }
                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }
                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }
                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }
                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }
                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }
                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }
                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }
                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }
                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            if (currentText == lastly)//lastfixed
            {
                if (winners > 1)
                {
                    if (checkWinners.Contains("Player"))
                    {
                        playerMaxChips += int.Parse(tbPot.Text) / winners;
                        tableChips.Text = playerMaxChips.ToString();
                        //playerPanel.Visible = true;

                    }
                    if (checkWinners.Contains("Bot 1"))
                    {
                        firstBotMaxChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips1.Text = firstBotMaxChips.ToString();
                        //firstBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 2"))
                    {
                        secondBotMaxChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips2.Text = secondBotMaxChips.ToString();
                        //secondBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 3"))
                    {
                        thirdBotMaxchips += int.Parse(tbPot.Text) / winners;
                        tbBotChips3.Text = thirdBotMaxchips.ToString();
                        //thirdBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 4"))
                    {
                        fourthBotMaxChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips4.Text = fourthBotMaxChips.ToString();
                        //fourthBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 5"))
                    {
                        fifthBotMaxChips += int.Parse(tbPot.Text) / winners;
                        tbBotChips5.Text = fifthBotMaxChips.ToString();
                        //fifthBotPanel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (checkWinners.Contains("Player"))
                    {
                        playerMaxChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //playerPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 1"))
                    {
                        firstBotMaxChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //firstBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 2"))
                    {
                        secondBotMaxChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //secondBotPanel.Visible = true;

                    }
                    if (checkWinners.Contains("Bot 3"))
                    {
                        thirdBotMaxchips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //thirdBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 4"))
                    {
                        fourthBotMaxChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //fourthBotPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 5"))
                    {
                        fifthBotMaxChips += int.Parse(tbPot.Text);
                        //await Finish(1);
                        //fifthBotPanel.Visible = true;
                    }
                }
            }
        }
        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!pFturn)
                            pStatus.Text = "";
                        if (!b1Fturn)
                            b1Status.Text = "";
                        if (!b2Fturn)
                            b2Status.Text = "";
                        if (!b3Fturn)
                            b3Status.Text = "";
                        if (!b4Fturn)
                            b4Status.Text = "";
                        if (!b5Fturn)
                            b5Status.Text = "";
                    }
                }
            }
            if (rounds == flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (holder[j].Image != deck[j])
                    {
                        holder[j].Image = deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (holder[j].Image != deck[j])
                    {
                        holder[j].Image = deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (holder[j].Image != deck[j])
                    {
                        holder[j].Image = deck[j];
                        pCall = 0; pRaise = 0;
                        b1Call = 0; b1Raise = 0;
                        b2Call = 0; b2Raise = 0;
                        b3Call = 0; b3Raise = 0;
                        b4Call = 0; b4Raise = 0;
                        b5Call = 0; b5Raise = 0;
                    }
                }
            }
            if (rounds == end && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!pStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref playerType, ref playerPowerHand, pFturn);
                }
                if (!b1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref b1Type, ref firstBotPowerHand, b1Fturn);
                }
                if (!b2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref b2Type, ref secondBotPowerHand, b2Fturn);
                }
                if (!b3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref b3Type, ref thirdBotPowerHand, b3Fturn);
                }
                if (!b4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref b4Type, ref fourtBotPowerHand, b4Fturn);
                }
                if (!b5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref b5Type, ref fiftBotPowerHand, b5Fturn);
                }
                Winner(playerType, playerPowerHand, "Player", playerMaxChips, fixedLast);
                Winner(b1Type, firstBotPowerHand, "Bot 1", firstBotMaxChips, fixedLast);
                Winner(b2Type, secondBotPowerHand, "Bot 2", secondBotMaxChips, fixedLast);
                Winner(b3Type, thirdBotPowerHand, "Bot 3", thirdBotMaxchips, fixedLast);
                Winner(b4Type, fourtBotPowerHand, "Bot 4", fourthBotMaxChips, fixedLast);
                Winner(b5Type, fiftBotPowerHand, "Bot 5", fifthBotMaxChips, fixedLast);
                restart = true;
                pturn = true;
                pFturn = false;
                b1Fturn = false;
                b2Fturn = false;
                b3Fturn = false;
                b4Fturn = false;
                b5Fturn = false;
                if (playerMaxChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.A != 0)
                    {
                        playerMaxChips = f2.A;
                        firstBotMaxChips += f2.A;
                        secondBotMaxChips += f2.A;
                        thirdBotMaxchips += f2.A;
                        fourthBotMaxChips += f2.A;
                        fifthBotMaxChips += f2.A;
                        pFturn = false;
                        pturn = true;
                        buttonRaise.Enabled = true;
                        buttonFold.Enabled = true;
                        buttonCheck.Enabled = true;
                        buttonRaise.Text = "Raise";
                    }
                }
                playerPanel.Visible = false; firstBotPanel.Visible = false; secondBotPanel.Visible = false; thirdBotPanel.Visible = false; fourthBotPanel.Visible = false; fifthBotPanel.Visible = false;
                pCall = 0; pRaise = 0;
                b1Call = 0; b1Raise = 0;
                b2Call = 0; b2Raise = 0;
                b3Call = 0; b3Raise = 0;
                b4Call = 0; b4Raise = 0;
                b5Call = 0; b5Raise = 0;
                Last = 0;
                call = bb;
                raise = 0;
                imgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
                rounds = 0;
                playerPowerHand = 0; playerType = -1;
                type = 0; firstBotPowerHand = 0; secondBotPowerHand = 0; thirdBotPowerHand = 0; fourtBotPowerHand = 0; fiftBotPowerHand = 0;
                b1Type = -1; b2Type = -1; b3Type = -1; b4Type = -1; b5Type = -1;
                ints.Clear();
                checkWinners.Clear();
                winners = 0;
                win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    holder[os].Image = null;
                    holder[os].Invalidate();
                    holder[os].Visible = false;
                }
                tbPot.Text = "0";
                pStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }
        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != raise && cRaise <= raise)
                    {
                        call = Convert.ToInt32(raise) - cRaise;
                    }
                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }
                    if (cRaise == raise && raise > 0)
                    {
                        call = 0;
                        buttonCall.Enabled = false;
                        buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }
        async Task AllIn()
        {
            #region All in
            if (playerMaxChips <= 0 && !intsadded)
            {
                if (pStatus.Text.Contains("Raise"))
                {
                    ints.Add(playerMaxChips);
                    intsadded = true;
                }
                if (pStatus.Text.Contains("Call"))
                {
                    ints.Add(playerMaxChips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (firstBotMaxChips <= 0 && !b1Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(firstBotMaxChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (secondBotMaxChips <= 0 && !b2Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(secondBotMaxChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (thirdBotMaxchips <= 0 && !b3Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(thirdBotMaxchips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (fourthBotMaxChips <= 0 && !b4Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(fourthBotMaxChips);
                    intsadded = true;
                }
                intsadded = false;
            }
            if (fifthBotMaxChips <= 0 && !b5Fturn)
            {
                if (!intsadded)
                {
                    ints.Add(fifthBotMaxChips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }
            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding
            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    playerMaxChips += int.Parse(tbPot.Text);
                    tableChips.Text = playerMaxChips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    firstBotMaxChips += int.Parse(tbPot.Text);
                    tableChips.Text = firstBotMaxChips.ToString();
                    firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    secondBotMaxChips += int.Parse(tbPot.Text);
                    tableChips.Text = secondBotMaxChips.ToString();
                    secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    thirdBotMaxchips += int.Parse(tbPot.Text);
                    tableChips.Text = thirdBotMaxchips.ToString();
                    thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    fourthBotMaxChips += int.Parse(tbPot.Text);
                    tableChips.Text = fourthBotMaxChips.ToString();
                    fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    fifthBotMaxChips += int.Parse(tbPot.Text);
                    tableChips.Text = fifthBotMaxChips.ToString();
                    fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    holder[j].Visible = false;
                }
                await Finish(1);
            }
            intsadded = false;
            #endregion

            #region FiveOrLessLeft
            if (abc < 6 && abc > 1 && rounds >= end)
            {
                await Finish(2);
            }
            #endregion


        }
        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            playerPanel.Visible = false; firstBotPanel.Visible = false; secondBotPanel.Visible = false; thirdBotPanel.Visible = false; fourthBotPanel.Visible = false; fifthBotPanel.Visible = false;
            call = bb; raise = 0;
            foldedPlayers = 5;
            type = 0; rounds = 0; firstBotPowerHand = 0; secondBotPowerHand = 0; thirdBotPowerHand = 0; fourtBotPowerHand = 0; fiftBotPowerHand = 0; playerPowerHand = 0; playerType = -1; raise = 0;
            b1Type = -1; b2Type = -1; b3Type = -1; b4Type = -1; b5Type = -1;
            firstBotMove = false; secondBotMove = false; thirdBotMove = false; fourtBotMove = false; fifthBotMove = false;
            b1Fturn = false; b2Fturn = false; b3Fturn = false; b4Fturn = false; b5Fturn = false;
            pFolded = false; b1Folded = false; b2Folded = false; b3Folded = false; b4Folded = false; b5Folded = false;
            pFturn = false; pturn = true; restart = false; raising = false;
            pCall = 0; b1Call = 0; b2Call = 0; b3Call = 0; b4Call = 0; b5Call = 0; pRaise = 0; b1Raise = 0; b2Raise = 0; b3Raise = 0; b4Raise = 0; b5Raise = 0;
            height = 0; width = 0; winners = 0; flop = 1; turn = 2; river = 3; end = 4; maxLeft = 6;
            Last = 123; raisedTurn = 1;
            bools.Clear();
            checkWinners.Clear();
            ints.Clear();
            win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            tbPot.Text = "0";
            t = 60; up = 10000000; turnCount = 0;
            pStatus.Text = "";
            b1Status.Text = "";
            b2Status.Text = "";
            b3Status.Text = "";
            b4Status.Text = "";
            b5Status.Text = "";
            if (playerMaxChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.A != 0)
                {
                    playerMaxChips = f2.A;
                    firstBotMaxChips += f2.A;
                    secondBotMaxChips += f2.A;
                    thirdBotMaxchips += f2.A;
                    fourthBotMaxChips += f2.A;
                    fifthBotMaxChips += f2.A;
                    pFturn = false;
                    pturn = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    buttonCheck.Enabled = true;
                    buttonRaise.Text = "Raise";
                }
            }
            imgLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                holder[os].Image = null;
                holder[os].Invalidate();
                holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }
        void FixWinners()
        {
            win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!pStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref playerType, ref playerPowerHand, pFturn);
            }
            if (!b1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref b1Type, ref firstBotPowerHand, b1Fturn);
            }
            if (!b2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref b2Type, ref secondBotPowerHand, b2Fturn);
            }
            if (!b3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref b3Type, ref thirdBotPowerHand, b3Fturn);
            }
            if (!b4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref b4Type, ref fourtBotPowerHand, b4Fturn);
            }
            if (!b5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref b5Type, ref fiftBotPowerHand, b5Fturn);
            }
            Winner(playerType, playerPowerHand, "Player", playerMaxChips, fixedLast);
            Winner(b1Type, firstBotPowerHand, "Bot 1", firstBotMaxChips, fixedLast);
            Winner(b2Type, secondBotPowerHand, "Bot 2", secondBotMaxChips, fixedLast);
            Winner(b3Type, thirdBotPowerHand, "Bot 3", thirdBotMaxchips, fixedLast);
            Winner(b4Type, fourtBotPowerHand, "Bot 4", fourthBotMaxChips, fixedLast);
            Winner(b5Type, fiftBotPowerHand, "Bot 5", fifthBotMaxChips, fixedLast);
        }
        void Ai(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 0)
                {
                    PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 1)
                {
                    PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 2)
                {
                    TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }
                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 4)
                {
                    Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 6)
                {
                    FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 7)
                {
                    FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                holder[c1].Visible = false;
                holder[c2].Visible = false;
            }
        }
        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Hp(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }
        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Hp(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }
        private void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                Ph(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise);
            }
            if (botPower <= 139 && botPower >= 128)
            {
                Ph(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise);
            }
            if (botPower < 128 && botPower >= 101)
            {
                Ph(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise);
            }
        }
        private void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                Ph(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise);
            }
            if (botPower <= 244 && botPower >= 234)
            {
                Ph(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
            if (botPower < 234 && botPower >= 201)
            {
                Ph(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
        }
        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower <= 327 && botPower >= 321)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303)//7 2
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }
        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower <= 409 && botPower >= 407)//10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
            if (botPower < 407 && botPower >= 404)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }
        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }
        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
            if (botPower < 620 && botPower >= 602)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }
        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }
        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }

        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }
        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }
        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            raising = false;
            sTurn = false;
            sChips -= call;
            sStatus.Text = "Call " + call;
            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
        }
        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(raise);
            sStatus.Text = "Raise " + raise;
            tbPot.Text = (int.Parse(tbPot.Text) + Convert.ToInt32(raise)).ToString();
            call = Convert.ToInt32(raise);
            raising = true;
            sTurn = false;
        }
        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }
        private void Hp(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (call <= 0)
            {
                Check(ref sTurn, sStatus);
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
                if (rnd == 2)
                {
                    if (call <= RoundN(sChips, n1))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (rnd == 3)
            {
                if (raise == 0)
                {
                    raise = call * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (raise <= RoundN(sChips, n))
                    {
                        raise = call * 2;
                        Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        private void Ph(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (raise <= RoundN(sChips, n) && raise >= (RoundN(sChips, n)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (raise <= (RoundN(sChips, n)) / 2)
                        {
                            if (raise > 0)
                            {
                                raise = (int) RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }

                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (raise <= RoundN(sChips, n - rnd) && raise >= (RoundN(sChips, n - rnd)) / 2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (raise <= (RoundN(sChips, n - rnd)) / 2)
                        {
                            if (raise > 0)
                            {
                                raise = (int) RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                raise = call * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    raise = (int) RoundN(sChips, r - rnd);
                    Raised(ref sChips, ref sTurn, sStatus);
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }
        void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= call)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        tbPot.Text = (int.Parse(tbPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (raise > 0)
                    {
                        if (botChips >= raise * 2)
                        {
                            raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        raise = call * 2;
                        Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }
            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region UI
        private async void timer_Tick(object sender, object e)
        {
            if (progressBarTimer.Value <= 0)
            {
                pFturn = true;
                await Turns();
            }
            if (t > 0)
            {
                t--;
                progressBarTimer.Value = (t / 6) * 100;
            }
        }
        private void Update_Tick(object sender, object e)
        {
            if (playerMaxChips <= 0)
            {
                tableChips.Text = "playerMaxChips : 0";
            }
            if (firstBotMaxChips <= 0)
            {
                tbBotChips1.Text = "playerMaxChips : 0";
            }
            if (secondBotMaxChips <= 0)
            {
                tbBotChips2.Text = "playerMaxChips : 0";
            }
            if (thirdBotMaxchips <= 0)
            {
                tbBotChips3.Text = "playerMaxChips : 0";
            }
            if (fourthBotMaxChips <= 0)
            {
                tbBotChips4.Text = "playerMaxChips : 0";
            }
            if (fifthBotMaxChips <= 0)
            {
                tbBotChips5.Text = "playerMaxChips : 0";
            }
            tableChips.Text = "playerMaxChips : " + playerMaxChips.ToString();
            tbBotChips1.Text = "playerMaxChips : " + firstBotMaxChips.ToString();
            tbBotChips2.Text = "playerMaxChips : " + secondBotMaxChips.ToString();
            tbBotChips3.Text = "playerMaxChips : " + thirdBotMaxchips.ToString();
            tbBotChips4.Text = "playerMaxChips : " + fourthBotMaxChips.ToString();
            tbBotChips5.Text = "playerMaxChips : " + fifthBotMaxChips.ToString();
            if (playerMaxChips <= 0)
            {
                pturn = false;
                pFturn = true;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                buttonCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (playerMaxChips >= call)
            {
                buttonCall.Text = "Call " + call.ToString();
            }
            else
            {
                buttonCall.Text = "All in";
                buttonRaise.Enabled = false;
            }
            if (call > 0)
            {
                buttonCheck.Enabled = false;
            }
            if (call <= 0)
            {
                buttonCheck.Enabled = true;
                buttonCall.Text = "Call";
                buttonCall.Enabled = false;
            }
            if (playerMaxChips <= 0)
            {
                buttonRaise.Enabled = false;
            }
            int parsedValue;

            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (playerMaxChips <= int.Parse(tbRaise.Text))
                {
                    buttonRaise.Text = "All in";
                }
                else
                {
                    buttonRaise.Text = "Raise";
                }
            }
            if (playerMaxChips < call)
            {
                buttonRaise.Enabled = false;
            }
        }
        private async void bFold_Click(object sender, EventArgs e)
        {
            pStatus.Text = "Fold";
            pturn = false;
            pFturn = true;
            await Turns();
        }
        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
            {
                pturn = false;
                pStatus.Text = "Check";
            }
            else
            {
                //pStatus.Text = "All in " + playerMaxChips;

                buttonCheck.Enabled = false;
            }
            await Turns();
        }
        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref playerType, ref playerPowerHand, pFturn);
            if (playerMaxChips >= call)
            {
                playerMaxChips -= call;
                tableChips.Text = "playerMaxChips : " + playerMaxChips.ToString();
                if (tbPot.Text != "")
                {
                    tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                }
                else
                {
                    tbPot.Text = call.ToString();
                }
                pturn = false;
                pStatus.Text = "Call " + call;
                pCall = call;
            }
            else if (playerMaxChips <= call && call > 0)
            {
                tbPot.Text = (int.Parse(tbPot.Text) + playerMaxChips).ToString();
                pStatus.Text = "All in " + playerMaxChips;
                playerMaxChips = 0;
                tableChips.Text = "playerMaxChips : " + playerMaxChips.ToString();
                pturn = false;
                buttonFold.Enabled = false;
                pCall = playerMaxChips;
            }
            await Turns();
        }
        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref playerType, ref playerPowerHand, pFturn);
            int parsedValue;
            if (tbRaise.Text != "" && int.TryParse(tbRaise.Text, out parsedValue))
            {
                if (playerMaxChips > call)
                {
                    if (raise * 2 > int.Parse(tbRaise.Text))
                    {
                        tbRaise.Text = (raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (playerMaxChips >= int.Parse(tbRaise.Text))
                        {
                            call = int.Parse(tbRaise.Text);
                            raise = int.Parse(tbRaise.Text);
                            pStatus.Text = "Raise " + call.ToString();
                            tbPot.Text = (int.Parse(tbPot.Text) + call).ToString();
                            buttonCall.Text = "Call";
                            playerMaxChips -= int.Parse(tbRaise.Text);
                            raising = true;
                            Last = 0;
                            pRaise = Convert.ToInt32(raise);
                        }
                        else
                        {
                            call = playerMaxChips;
                            raise = playerMaxChips;
                            tbPot.Text = (int.Parse(tbPot.Text) + playerMaxChips).ToString();
                            pStatus.Text = "Raise " + call.ToString();
                            playerMaxChips = 0;
                            raising = true;
                            Last = 0;
                            pRaise = Convert.ToInt32(raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            pturn = false;
            await Turns();
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            if (tbAdd.Text == "") { }
            else
            {
                playerMaxChips += int.Parse(tbAdd.Text);
                firstBotMaxChips += int.Parse(tbAdd.Text);
                secondBotMaxChips += int.Parse(tbAdd.Text);
                thirdBotMaxchips += int.Parse(tbAdd.Text);
                fourthBotMaxChips += int.Parse(tbAdd.Text);
                fifthBotMaxChips += int.Parse(tbAdd.Text);
            }
            tableChips.Text = "playerMaxChips : " + playerMaxChips.ToString();
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            tbBB.Text = bb.ToString();
            tbSB.Text = sb.ToString();
            if (tbBB.Visible == false)
            {
                tbBB.Visible = true;
                tbSB.Visible = true;
                bBB.Visible = true;
                bSB.Visible = true;
            }
            else
            {
                tbBB.Visible = false;
                tbSB.Visible = false;
                bBB.Visible = false;
                bSB.Visible = false;
            }
        }
        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbSB.Text.Contains(",") || tbSB.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                tbSB.Text = sb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = sb.ToString();
                return;
            }
            if (int.Parse(tbSB.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                tbSB.Text = sb.ToString();
            }
            if (int.Parse(tbSB.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(tbSB.Text) >= 250 && int.Parse(tbSB.Text) <= 100000)
            {
                sb = int.Parse(tbSB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (tbBB.Text.Contains(",") || tbBB.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                tbBB.Text = bb.ToString();
                return;
            }
            if (!int.TryParse(tbSB.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                tbSB.Text = bb.ToString();
                return;
            }
            if (int.Parse(tbBB.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                tbBB.Text = bb.ToString();
            }
            if (int.Parse(tbBB.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(tbBB.Text) >= 500 && int.Parse(tbBB.Text) <= 200000)
            {
                bb = int.Parse(tbBB.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }
        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }
        #endregion
    }
}