namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Enumerations;
    using Poker.Models;

    public partial class PokerGameForm : Form
    {
        #region Variables

        // Not used
        // ProgressBar asd = new ProgressBar();
        // public int Nm;
        int call = 500;

        int foldedPlayers = 5;

        private Dictionary<Players, Player> players;

        double type;

        double rounds = 0;

        double Raise = 0;

        bool intsadded;

        bool changed;

        int height;

        int width;

        int winners = 0;

        int maxLeft = 6;

        int last = 123;

        int raisedTurn = 1;

        List<Type> Win = new List<Type>();

        List<string> CheckWinners = new List<string>();

        List<int> ints = new List<int>();

        bool restart = false;

        bool raising = false;

        Type sorted;

        string[] allGameCardsImagesWithLocationsCollection = Directory.GetFiles(@"..\..\..\Poker\Resources\Assets\Cards\", "*.png", SearchOption.TopDirectoryOnly);

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
        int[] initialCardsCollection = new int[17];

        Image[] Deck = new Image[52];

        PictureBox[] gameCardsImagesCollection = new PictureBox[52];

        Timer timer = new Timer();

        Timer Updates = new Timer();

        int time = 60;

        int index;

        int bigBlind = 500;

        int smallBlind = 250;

        int up = 10000000;

        int turnCount = 0;

        #endregion

        public PokerGameForm()
        {
            this.players = new Dictionary<Players, Player>();

            foreach (Players player in Enum.GetValues(typeof(Players)))
            {
                this.players.Add(player, new Player(new Panel()));
            }

            this.players[Players.Player].Turn = true;

            this.call = this.bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Updates.Start();
            this.InitializeComponent();
            this.width = this.Width;
            this.height = this.Height;
            this.Shuffle();
            this.textBoxPot.Enabled = false;
            this.textBoxChips.Enabled = false;
            this.textBoxBotChips1.Enabled = false;
            this.textBoxBotChips2.Enabled = false;
            this.textBoxBotChips3.Enabled = false;
            this.textBoxBotChips4.Enabled = false;
            this.textBoxBotChips5.Enabled = false;
            this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
            this.textBoxBotChips1.Text = "Chips : " + this.players[Players.Bot1].CurrentChips.ToString();
            this.textBoxBotChips2.Text = "Chips : " + this.players[Players.Bot2].CurrentChips.ToString();
            this.textBoxBotChips3.Text = "Chips : " + this.players[Players.Bot3].CurrentChips.ToString();
            this.textBoxBotChips4.Text = "Chips : " + this.players[Players.Bot4].CurrentChips.ToString();
            this.textBoxBotChips5.Text = "Chips : " + this.players[Players.Bot5].CurrentChips.ToString();
            this.timer.Interval = 1 * 1 * 1000;
            this.timer.Tick += this.TimerTick;
            this.Updates.Interval = 1 * 1 * 100;
            this.Updates.Tick += this.UpdateTick;
            this.textBoxBigBlind.Visible = true;
            this.textBoxSmallBlind.Visible = true;
            this.buttonBigBlind.Visible = true;
            this.buttoneSmallBlind.Visible = true;
            this.textBoxBigBlind.Visible = true;
            this.textBoxSmallBlind.Visible = true;
            this.buttonBigBlind.Visible = true;
            this.buttoneSmallBlind.Visible = true;
            this.textBoxBigBlind.Visible = false;
            this.textBoxSmallBlind.Visible = false;
            this.buttonBigBlind.Visible = false;
            this.buttoneSmallBlind.Visible = false;
            this.textBoxRaise.Text = (this.bigBlind * 2).ToString();
        }

        public async Task Shuffle()
        {
            this.buttonCall.Enabled = false;
            this.buttonRaise.Enabled = false;
            this.buttonFold.Enabled = false;
            this.bCheck.Enabled = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bool isSecondReseivedCard = false;
            // Image for the backside of all cards
            Bitmap backImage = new Bitmap(@"..\..\..\Poker\Resources\Assets\Back\Back.png");
            //size for picture box with initial card
            int horizontal = 580;
            //size for picture box with initial card
            int vertical = 480;

            Random random = new Random();

            // Shuffles all cards in allGameCardsLocationsCollection
            for (this.index = this.allGameCardsImagesWithLocationsCollection.Length; this.index > 0; this.index--)
            {
                int randomCardIndex = random.Next(this.index);
                var gameCard = this.allGameCardsImagesWithLocationsCollection[randomCardIndex];
                this.allGameCardsImagesWithLocationsCollection[randomCardIndex] = this.allGameCardsImagesWithLocationsCollection[this.index - 1];
                this.allGameCardsImagesWithLocationsCollection[this.index - 1] = gameCard;
            }

            const int initialNumberOfCardsOnTable = 17;
            for (this.index = 0; this.index < initialNumberOfCardsOnTable; this.index++)
            {
                // Saves all card's names with their location in list of images
                this.Deck[this.index] = Image.FromFile(this.allGameCardsImagesWithLocationsCollection[this.index]);
                var charsToRemove = new[] { @"..\..\..\Poker\Resources\Assets\Cards\", ".png" };

                //Removes information for card's location and leaves only card's number
                foreach (var c in charsToRemove)
                {
                    this.allGameCardsImagesWithLocationsCollection[this.index] = this.allGameCardsImagesWithLocationsCollection[this.index].Replace(c, string.Empty);
                }

                // Saves ínitial card's number
                this.initialCardsCollection[this.index] = int.Parse(this.allGameCardsImagesWithLocationsCollection[this.index]) - 1;

                // Filling collection, which contains images of all cards as separate picturebox
                this.gameCardsImagesCollection[this.index] = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Height = 130,
                    Width = 80
                };

                // Adding picture box to winforms controls
                this.Controls.Add(this.gameCardsImagesCollection[this.index]);

                //Seting name for each picture box 
                this.gameCardsImagesCollection[this.index].Name = "pb" + this.index.ToString();
                await Task.Delay(200);

                //this is for initial two players cards. Fist two cards are always for the player
                if (this.index < 2)
                {
                    //Checks wheater first image in gameCardsImageCollection has a tag. If it has a tag than it sets tag on second element in gameCardsImagesCollection 
                    if (this.gameCardsImagesCollection[0].Tag != null)
                    {
                        this.gameCardsImagesCollection[1].Tag = this.initialCardsCollection[1];
                    }

                    //Sets tag to first image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                    this.gameCardsImagesCollection[0].Tag = this.initialCardsCollection[0];
                    //Saves in picture box image for current card from the array with card images
                    this.gameCardsImagesCollection[this.index].Image = this.Deck[this.index];
                    //Anchors picture box for bottom of the screen
                    this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.Bottom;

                    // gameCardsImagesCollection[index].Dock = DockStyle.Top;

                    //Sets location for the picture box 580: 480
                    this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                    //increases horizontal size with width (80) of current card so the next card can be till current card
                    horizontal += this.gameCardsImagesCollection[this.index].Width;
                    //Adding panel to winform controls
                    this.Controls.Add(this.players[Players.Player].Panel);
                    //Sets location for upper panel 
                    this.players[Players.Player].SetupPanel(
                        new Point(this.gameCardsImagesCollection[0].Left - 10, this.gameCardsImagesCollection[0].Top - 10));
                }

                if (this.players[Players.Bot1].CurrentChips > 0)
                {
                    //decrease number of folded player. It is 5.
                    this.foldedPlayers--;//foldedPlayers 4
                    //this is for initial two cards of first bot. Second two cards are always for the first bot.
                    if (this.index >= 2 && this.index < 4)
                    {
                        //Checks wheater third image in gameCardsImageCollection has a tag. If it has a tag than it sets tag on 
                        //fourth element in gameCardsImagesCollection 
                        if (this.gameCardsImagesCollection[2].Tag != null)
                        {
                            this.gameCardsImagesCollection[3].Tag = this.initialCardsCollection[3];
                        }

                        //Sets tag to third image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                        this.gameCardsImagesCollection[2].Tag = this.initialCardsCollection[2];
                        //sets coordinates for third card position
                        if (!isSecondReseivedCard)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        isSecondReseivedCard = true;
                        //Anchors picture box for bottom left side of the screen
                        this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                        //Saves in picture box image for current card. This image is back side of the card
                        this.gameCardsImagesCollection[this.index].Image = backImage;

                        // gameCardsImagesCollection[index].Image = Deck[index];

                        //Sets location for the picture box 15: 420
                        this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                        //increases horizontal size with width (80) of current card so the next card can be till current card
                        horizontal += this.gameCardsImagesCollection[this.index].Width;
                        //makes picturebox visible
                        this.gameCardsImagesCollection[this.index].Visible = true;

                        //Adds panel to winforms control
                        this.Controls.Add(this.players[Players.Bot1].Panel);
                        this.players[Players.Bot1].SetupPanel(
                            new Point(this.gameCardsImagesCollection[2].Left - 10, this.gameCardsImagesCollection[2].Top - 10));

                        if (this.index == 3)
                        {
                            isSecondReseivedCard = false;
                        }
                    }
                }

                if (this.players[Players.Bot2].CurrentChips > 0)
                {
                    this.foldedPlayers--;
                    //this is for initial two cards of second bot. Third two cards are always for the second bot.
                    if (this.index >= 4 && this.index < 6)
                    {
                        //Checks wheater fifth image in gameCardsImageCollection has a tag. If it has a tag than it sets tag 
                        //on sixth element in gameCardsImagesCollection 
                        if (this.gameCardsImagesCollection[4].Tag != null)
                        {
                            this.gameCardsImagesCollection[5].Tag = this.initialCardsCollection[5];
                        }
                        //Sets tag to fifth image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                        this.gameCardsImagesCollection[4].Tag = this.initialCardsCollection[4];
                        //sets coordinates for fifth card position
                        if (!isSecondReseivedCard)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        isSecondReseivedCard = true;
                        //Anchors picture box for top left side of the screen
                        this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        //Saves in picture box image for current card. This image is back side of the card
                        this.gameCardsImagesCollection[this.index].Image = backImage;

                        // gameCardsImagesCollection[index].Image = Deck[index];
                        //Sets location for the picture box 75: 65
                        this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                        //increases horizontal size with width (80) of current card so the next card can be till current card
                        horizontal += this.gameCardsImagesCollection[this.index].Width;
                        //makes picturebox visible
                        this.gameCardsImagesCollection[this.index].Visible = true;

                        //Adds panel to winforms control
                        this.Controls.Add(this.players[Players.Bot2].Panel);
                        this.players[Players.Bot2].SetupPanel(
                            new Point(this.gameCardsImagesCollection[4].Left - 10, this.gameCardsImagesCollection[4].Top - 10));

                        //sets isSecondReseivedCard to false so the next bot can set it's coordinates for it's cards
                        if (this.index == 5)
                        {
                            isSecondReseivedCard = false;
                        }
                    }
                }

                if (this.players[Players.Bot3].CurrentChips > 0)
                {
                    this.foldedPlayers--;
                    //this is for initial two cards of third bot. Fourth two cards are always for the third bot.
                    if (this.index >= 6 && this.index < 8)
                    {
                        if (this.gameCardsImagesCollection[6].Tag != null)
                        {
                            this.gameCardsImagesCollection[7].Tag = this.initialCardsCollection[7];
                        }
                        //Sets tag to seventh image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                        this.gameCardsImagesCollection[6].Tag = this.initialCardsCollection[6];
                        //sets coordinates for seventh card position
                        if (!isSecondReseivedCard)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        isSecondReseivedCard = true;
                        //Anchors picture box for top side of the screen
                        this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.Top;
                        //Saves in picture box image for current card. This image is back side of the card
                        this.gameCardsImagesCollection[this.index].Image = backImage;

                        // gameCardsImagesCollection[index].Image = Deck[index];

                        //Sets location for the picture box 590: 25
                        this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                        //increases horizontal size with width (80) of current card so the next card can be till current card
                        horizontal += this.gameCardsImagesCollection[this.index].Width;
                        //makes picturebox visible
                        this.gameCardsImagesCollection[this.index].Visible = true;
                        //Adds panel to winforms control
                        this.Controls.Add(this.players[Players.Bot3].Panel);
                        this.players[Players.Bot3].SetupPanel(
                            new Point(this.gameCardsImagesCollection[6].Left - 10, this.gameCardsImagesCollection[6].Top - 10));
                        //sets isSecondReseivedCard to false so the next bot can set it's coordinates for it's cards
                        if (this.index == 7)
                        {
                            isSecondReseivedCard = false;
                        }
                    }
                }

                if (this.players[Players.Bot4].CurrentChips > 0)
                {
                    this.foldedPlayers--;
                    //this is for initial two cards of fourth bot. Fivth two cards are always for the fourth bot.
                    if (this.index >= 8 && this.index < 10)
                    {
                        if (this.gameCardsImagesCollection[8].Tag != null)
                        {
                            this.gameCardsImagesCollection[9].Tag = this.initialCardsCollection[9];
                        }

                        //Sets tag to ninth image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                        this.gameCardsImagesCollection[8].Tag = this.initialCardsCollection[8];
                        //sets coordinates for ninth card position
                        if (!isSecondReseivedCard)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        isSecondReseivedCard = true;
                        //Anchors picture box for top right side of the screen
                        this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        //Saves in picture box image for current card. This image is back side of the card
                        this.gameCardsImagesCollection[this.index].Image = backImage;

                        // gameCardsImagesCollection[index].Image = Deck[index];
                        //Sets location for the picture box 1115: 65
                        this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                        //increases horizontal size with width (80) of current card so the next card can be till current card
                        horizontal += this.gameCardsImagesCollection[this.index].Width;
                        //makes picturebox visible
                        this.gameCardsImagesCollection[this.index].Visible = true;
                        //Adds panel to winforms control
                        this.Controls.Add(this.players[Players.Bot4].Panel);
                        this.players[Players.Bot4].SetupPanel(
                            new Point(this.gameCardsImagesCollection[8].Left - 10, this.gameCardsImagesCollection[8].Top - 10));

                        //sets isSecondReseivedCard to false so the next bot can set it's coordinates for it's cards
                        if (this.index == 9)
                        {
                            isSecondReseivedCard = false;
                        }
                    }
                }

                if (this.players[Players.Bot5].CurrentChips > 0)
                {
                    this.foldedPlayers--;
                    //this is for initial two cards of fifth bot. Sixth two cards are always for the fifth bot.
                    if (this.index >= 10 && this.index < 12)
                    {
                        if (this.gameCardsImagesCollection[10].Tag != null)
                        {
                            this.gameCardsImagesCollection[11].Tag = this.initialCardsCollection[11];
                        }
                        //Sets tag to eleventh image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                        this.gameCardsImagesCollection[10].Tag = this.initialCardsCollection[10];
                        //sets coordinates for eleventh card position
                        if (!isSecondReseivedCard)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        isSecondReseivedCard = true;
                        //Anchors picture box for bottom right side of the screen
                        this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                        //Saves in picture box image for current card. This image is back side of the card
                        this.gameCardsImagesCollection[this.index].Image = backImage;

                        // gameCardsImagesCollection[index].Image = Deck[index];

                        //Sets location for the picture box 1160: 420
                        this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                        //increases horizontal size with width (80) of current card so the next card can be till current card
                        horizontal += this.gameCardsImagesCollection[this.index].Width;
                        //makes picturebox visible
                        this.gameCardsImagesCollection[this.index].Visible = true;

                        //Adds panel to winforms control. It wraps two picture boxes of fifth bot
                        this.Controls.Add(this.players[Players.Bot5].Panel);
                        this.players[Players.Bot5].SetupPanel(
                            new Point(this.gameCardsImagesCollection[10].Left - 10, this.gameCardsImagesCollection[10].Top - 10));

                        //sets isSecondReseivedCard to false so the next bot can set it's coordinates for it's cards
                        if (this.index == 11)
                        {
                            isSecondReseivedCard = false;
                        }
                    }
                }

                //sets last five card on the table
                if (this.index >= 12)
                {
                    //Sets tag to thirdteenth image in gameCardsImagesCollection. Tag stores additional information about the image in our case the number of card
                    this.gameCardsImagesCollection[12].Tag = this.initialCardsCollection[12];
                    //set tags for the rest of cards
                    if (this.index > 12)
                    {
                        this.gameCardsImagesCollection[13].Tag = this.initialCardsCollection[13];
                    }

                    if (this.index > 13)
                    {
                        this.gameCardsImagesCollection[14].Tag = this.initialCardsCollection[14];
                    }

                    if (this.index > 14)
                    {
                        this.gameCardsImagesCollection[15].Tag = this.initialCardsCollection[15];
                    }

                    if (this.index > 15)
                    {
                        this.gameCardsImagesCollection[16].Tag = this.initialCardsCollection[16];
                    }
                    //sets coordinates for thirdteenth card position
                    if (!isSecondReseivedCard)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    isSecondReseivedCard = true;
                    if (this.gameCardsImagesCollection[this.index] != null)
                    {
                        //Sets no anchor
                        this.gameCardsImagesCollection[this.index].Anchor = AnchorStyles.None;
                        //Saves in picture box image for current card. This image is back side of the card
                        this.gameCardsImagesCollection[this.index].Image = backImage;

                        // gameCardsImagesCollection[index].Image = Deck[index];
                        //Sets location for the picture box 410: 265
                        this.gameCardsImagesCollection[this.index].Location = new Point(horizontal, vertical);
                        horizontal += 110;
                    }
                }

                if (this.players[Players.Bot1].CurrentChips <= 0)
                {
                    this.players[Players.Bot1].FoldedTurn = true;
                    this.gameCardsImagesCollection[2].Visible = false;
                    this.gameCardsImagesCollection[3].Visible = false;
                }
                else
                {
                    this.players[Players.Bot1].FoldedTurn = false;
                    if (this.index == 3)
                    {
                        if (this.gameCardsImagesCollection[3] != null)
                        {
                            this.gameCardsImagesCollection[2].Visible = true;
                            this.gameCardsImagesCollection[3].Visible = true;
                        }
                    }
                }

                if (this.players[Players.Bot2].CurrentChips <= 0)
                {
                    this.players[Players.Bot2].FoldedTurn = true;
                    this.gameCardsImagesCollection[4].Visible = false;
                    this.gameCardsImagesCollection[5].Visible = false;
                }
                else
                {
                    this.players[Players.Bot2].FoldedTurn = false;
                    if (this.index == 5)
                    {
                        if (this.gameCardsImagesCollection[5] != null)
                        {
                            this.gameCardsImagesCollection[4].Visible = true;
                            this.gameCardsImagesCollection[5].Visible = true;
                        }
                    }
                }

                if (this.players[Players.Bot3].CurrentChips <= 0)
                {
                    this.players[Players.Bot3].FoldedTurn = true;
                    this.gameCardsImagesCollection[6].Visible = false;
                    this.gameCardsImagesCollection[7].Visible = false;
                }
                else
                {
                    this.players[Players.Bot3].FoldedTurn = false;
                    if (this.index == 7)
                    {
                        if (this.gameCardsImagesCollection[7] != null)
                        {
                            this.gameCardsImagesCollection[6].Visible = true;
                            this.gameCardsImagesCollection[7].Visible = true;
                        }
                    }
                }

                if (this.players[Players.Bot4].CurrentChips <= 0)
                {
                    this.players[Players.Bot4].FoldedTurn = true;
                    this.gameCardsImagesCollection[8].Visible = false;
                    this.gameCardsImagesCollection[9].Visible = false;
                }
                else
                {
                    this.players[Players.Bot4].FoldedTurn = false;
                    if (this.index == 9)
                    {
                        if (this.gameCardsImagesCollection[9] != null)
                        {
                            this.gameCardsImagesCollection[8].Visible = true;
                            this.gameCardsImagesCollection[9].Visible = true;
                        }
                    }
                }

                if (this.players[Players.Bot5].CurrentChips <= 0)
                {
                    this.players[Players.Bot5].FoldedTurn = true;
                    this.gameCardsImagesCollection[10].Visible = false;
                    this.gameCardsImagesCollection[11].Visible = false;
                }
                else
                {
                    this.players[Players.Bot5].FoldedTurn = false;
                    if (this.index == 11)
                    {
                        if (this.gameCardsImagesCollection[11] != null)
                        {
                            this.gameCardsImagesCollection[10].Visible = true;
                            this.gameCardsImagesCollection[11].Visible = true;
                        }
                    }
                }

                if (this.index == 16)
                {
                    if (!this.restart)
                    {
                        this.MaximizeBox = true;
                        this.MinimizeBox = true;
                    }

                    this.timer.Start();
                }
            }

            if (this.foldedPlayers == 5)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Would You Like To Play Again ?",
                    "You Won , Congratulations ! ",
                    MessageBoxButtons.YesNo);
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
                this.foldedPlayers = 5;
            }

            if (this.index == 17)
            {
                this.buttonRaise.Enabled = true;
                this.buttonCall.Enabled = true;
                this.buttonRaise.Enabled = true;
                this.buttonRaise.Enabled = true;
                this.buttonFold.Enabled = true;
            }
        }

        async Task Turns()
        {
            if (!this.players[Players.Player].FoldedTurn)
            {
                if (this.players[Players.Player].Turn)
                {
                    this.FixCall(this.playerStatus, this.players[Players.Player], 1);

                    // MessageBox.Show("Player's Turn");
                    this.progressBarTimer.Visible = true;
                    this.progressBarTimer.Value = 1000;
                    this.time = 60;
                    this.up = 10000000;
                    this.timer.Start();
                    this.buttonRaise.Enabled = true;
                    this.buttonCall.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.turnCount++;
                    this.FixCall(this.playerStatus, this.players[Players.Player], 2);
                }
            }

            if (this.players[Players.Player].FoldedTurn || !this.players[Players.Player].Turn)
            {
                await this.AllIn();
                if (this.players[Players.Player].FoldedTurn && !this.players[Players.Player].Folded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        this.maxLeft--;
                        this.players[Players.Player].Folded = true;
                    }
                }

                await this.CheckRaise(0, 0);
                this.progressBarTimer.Visible = false;
                this.buttonRaise.Enabled = false;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.timer.Stop();

                this.players[Players.Bot1].Turn = true;
                if (!this.players[Players.Bot1].FoldedTurn)
                {
                    if (this.players[Players.Bot1].Turn)
                    {
                        this.FixCall(this.bot1Status, this.players[Players.Bot1], 1);
                        this.FixCall(this.bot1Status, this.players[Players.Bot1], 2);

                        this.Rules(2, 3, "Bot 1", this.players[Players.Bot1]);
                        MessageBox.Show("Bot 1's Turn");
                        this.AI(2, 3, this.bot1Status, 0, this.players[Players.Bot1]);
                        this.turnCount++;
                        this.last = 1;
                        this.players[Players.Bot1].Turn = false;
                        this.players[Players.Bot2].Turn = true;
                    }
                }

                if (this.players[Players.Bot1].FoldedTurn && !this.players[Players.Bot1].Folded)
                {
                    this.maxLeft--;
                    this.players[Players.Bot1].Folded = true;
                }

                if (this.players[Players.Bot1].FoldedTurn || !this.players[Players.Bot1].Turn)
                {
                    await this.CheckRaise(1, 1);
                    this.players[Players.Bot2].Turn = true;
                }

                if (!this.players[Players.Bot2].FoldedTurn)
                {
                    if (this.players[Players.Bot2].Turn)
                    {
                        this.FixCall(this.bot2Status, this.players[Players.Bot2], 1);
                        this.FixCall(this.bot2Status, this.players[Players.Bot2], 2);
                        this.Rules(4, 5, "Bot 2", this.players[Players.Bot2]);
                        MessageBox.Show("Bot 2's Turn");
                        this.AI(4, 5, this.bot2Status, 1, this.players[Players.Bot2]);
                        this.turnCount++;
                        this.last = 2;
                        this.players[Players.Bot2].Turn = false;
                        this.players[Players.Bot3].Turn = true;
                    }
                }

                if (this.players[Players.Bot2].FoldedTurn && !this.players[Players.Bot2].Folded)
                {
                    this.maxLeft--;
                    this.players[Players.Bot2].Folded = true;
                }

                if (this.players[Players.Bot2].FoldedTurn || !this.players[Players.Bot2].Turn)
                {
                    await this.CheckRaise(2, 2);
                    this.players[Players.Bot3].Turn = true;
                }

                if (!this.players[Players.Bot3].FoldedTurn)
                {
                    if (this.players[Players.Bot3].Turn)
                    {
                        this.FixCall(this.bot3Status, this.players[Players.Bot3], 1);
                        this.FixCall(this.bot3Status, this.players[Players.Bot3], 2);
                        this.Rules(6, 7, "Bot 3", this.players[Players.Bot3]);
                        MessageBox.Show("Bot 3's Turn");
                        this.AI(6, 7, this.bot3Status, 2, this.players[Players.Bot3]);
                        this.turnCount++;
                        this.last = 3;
                        this.players[Players.Bot3].Turn = false;
                        this.players[Players.Bot4].Turn = true;
                    }
                }

                if (this.players[Players.Bot3].FoldedTurn && !this.players[Players.Bot3].Folded)
                {
                    this.maxLeft--;
                    this.players[Players.Bot3].Folded = true;
                }

                if (this.players[Players.Bot3].FoldedTurn || !this.players[Players.Bot3].Turn)
                {
                    await this.CheckRaise(3, 3);
                    this.players[Players.Bot4].Turn = true;
                }

                if (!this.players[Players.Bot4].FoldedTurn)
                {
                    if (this.players[Players.Bot4].Turn)
                    {
                        this.FixCall(this.bot4Status, this.players[Players.Bot4], 1);
                        this.FixCall(this.bot4Status, this.players[Players.Bot4], 2);
                        this.Rules(8, 9, "Bot 4", this.players[Players.Bot4]);
                        MessageBox.Show("Bot 4's Turn");
                        this.AI(8, 9, this.bot4Status, 3, this.players[Players.Bot4]);
                        this.turnCount++;
                        this.last = 4;
                        this.players[Players.Bot4].Turn = false;
                        this.players[Players.Bot5].Turn = true;
                    }
                }

                if (this.players[Players.Bot4].FoldedTurn && !this.players[Players.Bot4].Folded)
                {
                    this.maxLeft--;
                    this.players[Players.Bot4].Folded = true;
                }

                if (this.players[Players.Bot4].FoldedTurn || !this.players[Players.Bot4].Turn)
                {
                    await this.CheckRaise(4, 4);
                    this.players[Players.Bot5].Turn = true;
                }

                if (!this.players[Players.Bot5].FoldedTurn)
                {
                    if (this.players[Players.Bot5].Turn)
                    {
                        this.FixCall(this.bot5Status, this.players[Players.Bot5], 1);
                        this.FixCall(this.bot5Status, this.players[Players.Bot5], 2);
                        this.Rules(10, 11, "Bot 5", this.players[Players.Bot5]);
                        MessageBox.Show("Bot 5's Turn");
                        this.AI(10, 11, this.bot5Status, 4, this.players[Players.Bot5]);
                        this.turnCount++;
                        this.last = 5;
                        this.players[Players.Bot5].Turn = false;
                    }
                }

                if (this.players[Players.Bot5].FoldedTurn && !this.players[Players.Bot5].Folded)
                {
                    this.maxLeft--;
                    this.players[Players.Bot5].Folded = true;
                }

                if (this.players[Players.Bot5].FoldedTurn || !this.players[Players.Bot5].Turn)
                {
                    await this.CheckRaise(5, 5);
                    this.players[Players.Player].Turn = true;
                }

                if (this.players[Players.Player].FoldedTurn && !this.players[Players.Player].Folded)
                {
                    if (this.buttonCall.Text.Contains("All in") == false || this.buttonRaise.Text.Contains("All in") == false)
                    {
                        this.maxLeft--;
                        this.players[Players.Player].Folded = true;
                    }
                }

                await this.AllIn();
                if (!this.restart)
                {
                    await this.Turns();
                }

                this.restart = false;
            }
        }

        private void Rules(int firstCard, int secondCard, string currentText, Player player)
        {
            if (!player.FoldedTurn ||
                firstCard == 0 &&
                secondCard == 1 &&
                this.playerStatus.Text.Contains("Fold") == false)
            {
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = this.initialCardsCollection[firstCard];
                Straight[1] = this.initialCardsCollection[secondCard];
                Straight1[0] = Straight[2] = this.initialCardsCollection[12];
                Straight1[1] = Straight[3] = this.initialCardsCollection[13];
                Straight1[2] = Straight[4] = this.initialCardsCollection[14];
                Straight1[3] = Straight[5] = this.initialCardsCollection[15];
                Straight1[4] = Straight[6] = this.initialCardsCollection[16];

                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();

                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();

                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);

                for (this.index = 0; this.index < 16; this.index++)
                {
                    if (this.initialCardsCollection[this.index] == int.Parse(this.gameCardsImagesCollection[firstCard].Tag.ToString())
                        && this.initialCardsCollection[this.index + 1] == int.Parse(this.gameCardsImagesCollection[secondCard].Tag.ToString()))
                    {
                        // Pair from Hand current = 1
                        this.PairFromHandRule(player);

                        #region Pair or Two Pair from Table current = 2 || 0

                        this.PairTwoPairRule(player);

                        #endregion

                        #region Two Pair current = 2

                        this.TwoPairRule(player);

                        #endregion

                        #region Three of a kind current = 3

                        this.ThreeOfaKindRule(player, Straight);

                        #endregion

                        #region Straight current = 4

                        this.StraightRule(player, Straight);

                        #endregion

                        #region Flush current = 5 || 5.5

                        this.FlushRule(player, ref vf, Straight1);

                        #endregion

                        #region Full House current = 6

                        this.FullHouseRule(player, ref done, Straight);

                        #endregion

                        #region Four of a Kind current = 7

                        this.FourOfAKindRule(player, Straight);

                        #endregion

                        #region Straight Flush current = 8 || 9

                        this.StraightFlushRule(player, st1, st2, st3, st4);

                        #endregion

                        #region High Card current = -1

                        this.HighCardRule(player);

                        #endregion
                    }
                }
            }
        }

        private void StraightFlushRule(Player player, int[] st1, int[] st2, int[] st3, int[] st4)
        {
            if (player.Type >= -1)
            {
                if (st1.Length >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        player.Type = 8;
                        player.PowerHand = st1.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 8));

                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st1[0] == 0 &&
                        st1[1] == 9 &&
                        st1[2] == 10 &&
                        st1[3] == 11 &&
                        st1[0] + 12 == st1[4])
                    {
                        player.Type = 9;
                        player.PowerHand = st1.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 9));
                        this.sorted = this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (st2.Length >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        player.Type = 8;
                        player.PowerHand = st2.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 8));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (st2[0] == 0 && st2[1] == 9 && st2[2] == 10 && st2[3] == 11 && st2[0] + 12 == st2[4])
                    {
                        player.Type = 9;
                        player.PowerHand = st2.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 9));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (st3.Length >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        player.Type = 8;
                        player.PowerHand = st3.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 8));
                        this.sorted =
                            this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st3[0] == 0 && st3[1] == 9 && st3[2] == 10 && st3[3] == 11 && st3[0] + 12 == st3[4])
                    {
                        player.Type = 9;
                        player.PowerHand = st3.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 9));
                        this.sorted =
                            this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (st4.Length >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        player.Type = 8;
                        player.PowerHand = st4.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 8));
                        this.sorted =
                            this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st4[0] == 0 && st4[1] == 9 && st4[2] == 10 && st4[3] == 11 && st4[0] + 12 == st4[4])
                    {
                        player.Type = 9;
                        player.PowerHand = st4.Max() / 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 9));
                        this.sorted =
                            this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void FourOfAKindRule(Player player, int[] Straight)
        {
            if (player.Type >= -1)
            {
                for (int j = 0; j <= 3; j++)
                {
                    if (Straight[j] / 4 == Straight[j + 1] / 4 && Straight[j] / 4 == Straight[j + 2] / 4
                        && Straight[j] / 4 == Straight[j + 3] / 4)
                    {
                        player.Type = 7;
                        player.PowerHand = (Straight[j] / 4) * 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 7));
                        this.sorted =
                            this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (Straight[j] / 4 == 0 && Straight[j + 1] / 4 == 0 && Straight[j + 2] / 4 == 0
                        && Straight[j + 3] / 4 == 0)
                    {
                        player.Type = 7;
                        player.PowerHand = 13 * 4 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 7));
                        this.sorted =
                            this.Win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void FullHouseRule(Player player, ref bool done, int[] Straight)
        {
            if (player.Type >= -1)
            {
                this.type = player.PowerHand;
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.Type = 6;
                                player.PowerHand = 13 * 2 + player.Type * 100;
                                this.Win.Add(new Type(player.PowerHand, 6));
                                this.sorted =
                                    this.Win.OrderByDescending(op1 => op1.Current)
                                        .ThenByDescending(op1 => op1.Power)
                                        .First();
                                break;
                            }

                            if (fh.Max() / 4 > 0)
                            {
                                player.Type = 6;
                                player.PowerHand = fh.Max() / 4 * 2 + player.Type * 100;
                                this.Win.Add(new Type(player.PowerHand, 6));
                                this.sorted =
                                    this.Win.OrderByDescending(op1 => op1.Current)
                                        .ThenByDescending(op1 => op1.Power)
                                        .First();
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max() / 4 == 0)
                            {
                                player.PowerHand = 13;
                                done = true;
                                j = -1;
                            }
                            else
                            {
                                player.PowerHand = fh.Max() / 4;
                                done = true;
                                j = -1;
                            }
                        }
                    }
                }

                if (player.Type != 6)
                {
                    player.PowerHand = this.type;
                }
            }
        }

        private void FlushRule(Player player, ref bool vf, int[] Straight1)
        {
            if (player.Type >= -1)
            {
                var f1 = Straight1.Where(o => o % 4 == 0).ToArray();
                var f2 = Straight1.Where(o => o % 4 == 1).ToArray();
                var f3 = Straight1.Where(o => o % 4 == 2).ToArray();
                var f4 = Straight1.Where(o => o % 4 == 3).ToArray();
                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (this.initialCardsCollection[this.index] % 4 == this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f1[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }

                        if (this.initialCardsCollection[this.index + 1] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (this.initialCardsCollection[this.index] / 4 < f1.Max() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = f1.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 4)
                {
                    // different cards in hand
                    if (this.initialCardsCollection[this.index] % 4 != this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f1[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f1.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 != this.initialCardsCollection[this.index] % 4
                        && this.initialCardsCollection[this.index + 1] % 4 == f1[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index + 1] / 4 > f1.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f1.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f1.Length == 5)
                {
                    if (this.initialCardsCollection[this.index] % 4 == f1[0] % 4 && this.initialCardsCollection[this.index] / 4 > f1.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 == f1[0] % 4 && this.initialCardsCollection[this.index + 1] / 4 > f1.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.initialCardsCollection[this.index] / 4 < f1.Min() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f1.Min())
                    {
                        player.Type = 5;
                        player.PowerHand = f1.Max() + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (this.initialCardsCollection[this.index] % 4 == this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f2[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }

                        if (this.initialCardsCollection[this.index + 1] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (this.initialCardsCollection[this.index] / 4 < f2.Max() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = f2.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 4)
                {
                    // different cards in hand
                    if (this.initialCardsCollection[this.index] % 4 != this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f2[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f2.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 != this.initialCardsCollection[this.index] % 4
                        && this.initialCardsCollection[this.index + 1] % 4 == f2[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index + 1] / 4 > f2.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f2.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f2.Length == 5)
                {
                    if (this.initialCardsCollection[this.index] % 4 == f2[0] % 4 && this.initialCardsCollection[this.index] / 4 > f2.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 == f2[0] % 4 && this.initialCardsCollection[this.index + 1] / 4 > f2.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.initialCardsCollection[this.index] / 4 < f2.Min() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f2.Min())
                    {
                        player.Type = 5;
                        player.PowerHand = f2.Max() + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (this.initialCardsCollection[this.index] % 4 == this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f3[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }

                        if (this.initialCardsCollection[this.index + 1] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (this.initialCardsCollection[this.index] / 4 < f3.Max() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = f3.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 4)
                {
                    // different cards in hand
                    if (this.initialCardsCollection[this.index] % 4 != this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f3[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f3.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 != this.initialCardsCollection[this.index] % 4
                        && this.initialCardsCollection[this.index + 1] % 4 == f3[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index + 1] / 4 > f3.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f3.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f3.Length == 5)
                {
                    if (this.initialCardsCollection[this.index] % 4 == f3[0] % 4 && this.initialCardsCollection[this.index] / 4 > f3.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 == f3[0] % 4 && this.initialCardsCollection[this.index + 1] / 4 > f3.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.initialCardsCollection[this.index] / 4 < f3.Min() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f3.Min())
                    {
                        player.Type = 5;
                        player.PowerHand = f3.Max() + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (this.initialCardsCollection[this.index] % 4 == this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f4[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }

                        if (this.initialCardsCollection[this.index + 1] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else if (this.initialCardsCollection[this.index] / 4 < f4.Max() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = f4.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 4)
                {
                    // different cards in hand
                    if (this.initialCardsCollection[this.index] % 4 != this.initialCardsCollection[this.index + 1] % 4
                        && this.initialCardsCollection[this.index] % 4 == f4[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f4.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 != this.initialCardsCollection[this.index] % 4
                        && this.initialCardsCollection[this.index + 1] % 4 == f4[0] % 4)
                    {
                        if (this.initialCardsCollection[this.index + 1] / 4 > f4.Max() / 4)
                        {
                            player.Type = 5;
                            player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                        else
                        {
                            player.Type = 5;
                            player.PowerHand = f4.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 5));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                            vf = true;
                        }
                    }
                }

                if (f4.Length == 5)
                {
                    if (this.initialCardsCollection[this.index] % 4 == f4[0] % 4 && this.initialCardsCollection[this.index] / 4 > f4.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }

                    if (this.initialCardsCollection[this.index + 1] % 4 == f4[0] % 4 && this.initialCardsCollection[this.index + 1] / 4 > f4.Min() / 4)
                    {
                        player.Type = 5;
                        player.PowerHand = this.initialCardsCollection[this.index + 1] + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                    else if (this.initialCardsCollection[this.index] / 4 < f4.Min() / 4 && this.initialCardsCollection[this.index + 1] / 4 < f4.Min())
                    {
                        player.Type = 5;
                        player.PowerHand = f4.Max() + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        vf = true;
                    }
                }

                // ace
                if (f1.Length > 0)
                {
                    if (this.initialCardsCollection[this.index] / 4 == 0 && this.initialCardsCollection[this.index] % 4 == f1[0] % 4 && vf && f1.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.initialCardsCollection[this.index + 1] / 4 == 0 && this.initialCardsCollection[this.index + 1] % 4 == f1[0] % 4 && vf
                        && f1.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f2.Length > 0)
                {
                    if (this.initialCardsCollection[this.index] / 4 == 0 && this.initialCardsCollection[this.index] % 4 == f2[0] % 4 && vf && f2.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.initialCardsCollection[this.index + 1] / 4 == 0 && this.initialCardsCollection[this.index + 1] % 4 == f2[0] % 4 && vf
                        && f2.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f3.Length > 0)
                {
                    if (this.initialCardsCollection[this.index] / 4 == 0 && this.initialCardsCollection[this.index] % 4 == f3[0] % 4 && vf && f3.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.initialCardsCollection[this.index + 1] / 4 == 0 && this.initialCardsCollection[this.index + 1] % 4 == f3[0] % 4 && vf
                        && f3.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }

                if (f4.Length > 0)
                {
                    if (this.initialCardsCollection[this.index] / 4 == 0 && this.initialCardsCollection[this.index] % 4 == f4[0] % 4 && vf && f4.Length > 0)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }

                    if (this.initialCardsCollection[this.index + 1] / 4 == 0 && this.initialCardsCollection[this.index + 1] % 4 == f4[0] % 4 && vf)
                    {
                        player.Type = 5.5;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 5.5));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void StraightRule(Player player, int[] Straight)
        {
            if (player.Type >= -1)
            {
                var op = Straight.Select(o => o / 4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            player.Type = 4;
                            player.PowerHand = op.Max() + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 4));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                        }
                        else
                        {
                            player.Type = 4;
                            player.PowerHand = op[j + 4] + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 4));
                            this.sorted =
                                this.Win.OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();
                        }
                    }

                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        player.Type = 4;
                        player.PowerHand = 13 + player.Type * 100;
                        this.Win.Add(new Type(player.PowerHand, 4));
                        this.sorted =
                            this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void ThreeOfaKindRule(Player player, int[] Straight)
        {
            if (player.Type >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o / 4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max() / 4 == 0)
                        {
                            player.Type = 3;
                            player.PowerHand = 13 * 3 + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 3));
                            this.sorted =
                                this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            player.Type = 3;
                            player.PowerHand = fh[0] / 4 + fh[1] / 4 + fh[2] / 4 + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 3));
                            this.sorted =
                                this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        private void TwoPairRule(Player player)
        {
            if (player.Type >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (this.initialCardsCollection[this.index] / 4 != this.initialCardsCollection[this.index + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }

                            if (tc - k >= 12)
                            {
                                if (this.initialCardsCollection[this.index] / 4 == this.initialCardsCollection[tc] / 4
                                    && this.initialCardsCollection[this.index + 1] / 4 == this.initialCardsCollection[tc - k] / 4
                                    || this.initialCardsCollection[this.index + 1] / 4 == this.initialCardsCollection[tc] / 4
                                    && this.initialCardsCollection[this.index] / 4 == this.initialCardsCollection[tc - k] / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.initialCardsCollection[this.index] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = 13 * 4 + (this.initialCardsCollection[this.index + 1] / 4) * 2
                                                           + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.initialCardsCollection[this.index + 1] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = 13 * 4 + (this.initialCardsCollection[this.index] / 4) * 2 + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.initialCardsCollection[this.index + 1] / 4 != 0 && this.initialCardsCollection[this.index] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = (this.initialCardsCollection[this.index] / 4) * 2
                                                           + (this.initialCardsCollection[this.index + 1] / 4) * 2 + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
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

        private void PairTwoPairRule(Player player)
        {
            if (player.Type >= -1)
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
                            if (this.initialCardsCollection[tc] / 4 == this.initialCardsCollection[tc - k] / 4)
                            {
                                if (this.initialCardsCollection[tc] / 4 != this.initialCardsCollection[this.index] / 4
                                    && this.initialCardsCollection[tc] / 4 != this.initialCardsCollection[this.index + 1] / 4 && player.Type == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (this.initialCardsCollection[this.index + 1] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = (this.initialCardsCollection[this.index] / 4) * 2 + 13 * 4 + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.initialCardsCollection[this.index] / 4 == 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = (this.initialCardsCollection[this.index + 1] / 4) * 2 + 13 * 4
                                                           + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.initialCardsCollection[this.index + 1] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = (this.initialCardsCollection[tc] / 4) * 2
                                                           + (this.initialCardsCollection[this.index + 1] / 4) * 2 + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }

                                        if (this.initialCardsCollection[this.index] / 4 != 0)
                                        {
                                            player.Type = 2;
                                            player.PowerHand = (this.initialCardsCollection[tc] / 4) * 2 + (this.initialCardsCollection[this.index] / 4) * 2
                                                           + player.Type * 100;
                                            this.Win.Add(new Type(player.PowerHand, 2));
                                            this.sorted =
                                                this.Win.OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                        }
                                    }

                                    msgbox = true;
                                }

                                if (player.Type == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (this.initialCardsCollection[this.index] / 4 > this.initialCardsCollection[this.index + 1] / 4)
                                        {
                                            if (this.initialCardsCollection[tc] / 4 == 0)
                                            {
                                                player.Type = 0;
                                                player.PowerHand = 13 + this.initialCardsCollection[this.index] / 4 + player.Type * 100;
                                                this.Win.Add(new Type(player.PowerHand, 1));
                                                this.sorted =
                                                    this.Win.OrderByDescending(op => op.Current)
                                                        .ThenByDescending(op => op.Power)
                                                        .First();
                                            }
                                            else
                                            {
                                                player.Type = 0;
                                                player.PowerHand = this.initialCardsCollection[tc] / 4 + this.initialCardsCollection[this.index] / 4
                                                               + player.Type * 100;
                                                this.Win.Add(new Type(player.PowerHand, 1));
                                                this.sorted =
                                                    this.Win.OrderByDescending(op => op.Current)
                                                        .ThenByDescending(op => op.Power)
                                                        .First();
                                            }
                                        }
                                        else
                                        {
                                            if (this.initialCardsCollection[tc] / 4 == 0)
                                            {
                                                player.Type = 0;
                                                player.PowerHand = 13 + this.initialCardsCollection[this.index + 1] + player.Type * 100;
                                                this.Win.Add(new Type(player.PowerHand, 1));
                                                this.sorted =
                                                    this.Win.OrderByDescending(op => op.Current)
                                                        .ThenByDescending(op => op.Power)
                                                        .First();
                                            }
                                            else
                                            {
                                                player.Type = 0;
                                                player.PowerHand = this.initialCardsCollection[tc] / 4 + this.initialCardsCollection[this.index + 1] / 4
                                                               + player.Type * 100;
                                                this.Win.Add(new Type(player.PowerHand, 1));
                                                this.sorted =
                                                    this.Win.OrderByDescending(op => op.Current)
                                                        .ThenByDescending(op => op.Power)
                                                        .First();
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

        private void PairFromHandRule(Player player)
        {
            if (player.Type >= -1)
            {
                bool msgbox = false;
                if (this.initialCardsCollection[this.index] / 4 == this.initialCardsCollection[this.index + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (this.initialCardsCollection[this.index] / 4 == 0)
                        {
                            player.Type = 1;
                            player.PowerHand = 13 * 4 + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 1));
                            this.sorted =
                                this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            player.Type = 1;
                            player.PowerHand = (this.initialCardsCollection[this.index + 1] / 4) * 4 + player.Type * 100;
                            this.Win.Add(new Type(player.PowerHand, 1));
                            this.sorted =
                                this.Win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }

                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (this.initialCardsCollection[this.index + 1] / 4 == this.initialCardsCollection[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.initialCardsCollection[this.index + 1] / 4 == 0)
                            {
                                player.Type = 1;
                                player.PowerHand = 13 * 4 + this.initialCardsCollection[this.index] / 4 + player.Type * 100;
                                this.Win.Add(new Type(player.PowerHand, 1));
                                this.sorted =
                                    this.Win.OrderByDescending(op => op.Current)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }
                            else
                            {
                                player.Type = 1;
                                player.PowerHand = (this.initialCardsCollection[this.index + 1] / 4) * 4 + this.initialCardsCollection[this.index] / 4
                                               + player.Type * 100;
                                this.Win.Add(new Type(player.PowerHand, 1));
                                this.sorted =
                                    this.Win.OrderByDescending(op => op.Current)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }
                        }

                        msgbox = true;
                    }

                    if (this.initialCardsCollection[this.index] / 4 == this.initialCardsCollection[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (this.initialCardsCollection[this.index] / 4 == 0)
                            {
                                player.Type = 1;
                                player.PowerHand = 13 * 4 + this.initialCardsCollection[this.index + 1] / 4 + player.Type * 100;
                                this.Win.Add(new Type(player.PowerHand, 1));
                                this.sorted =
                                    this.Win.OrderByDescending(op => op.Current)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }
                            else
                            {
                                player.Type = 1;
                                player.PowerHand = (this.initialCardsCollection[tc] / 4) * 4 + this.initialCardsCollection[this.index + 1] / 4
                                               + player.Type * 100;
                                this.Win.Add(new Type(player.PowerHand, 1));
                                this.sorted =
                                    this.Win.OrderByDescending(op => op.Current)
                                        .ThenByDescending(op => op.Power)
                                        .First();
                            }
                        }

                        msgbox = true;
                    }
                }
            }
        }

        private void HighCardRule(Player player)
        {
            if (player.Type == -1)
            {
                if (this.initialCardsCollection[this.index] / 4 > this.initialCardsCollection[this.index + 1] / 4)
                {
                    player.Type = -1;
                    player.PowerHand = this.initialCardsCollection[this.index] / 4;
                    this.Win.Add(new Type(player.PowerHand, -1));
                    this.sorted =
                        this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    player.Type = -1;
                    player.PowerHand = this.initialCardsCollection[this.index + 1] / 4;
                    this.Win.Add(new Type(player.PowerHand, -1));
                    this.sorted =
                        this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }

                if (this.initialCardsCollection[this.index] / 4 == 0 || this.initialCardsCollection[this.index + 1] / 4 == 0)
                {
                    player.Type = -1;
                    player.PowerHand = 13;
                    this.Win.Add(new Type(player.PowerHand, -1));
                    this.sorted =
                        this.Win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(Player player, string currentText, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }

            for (int j = 0; j <= 16; j++)
            {
                // await Task.Delay(5);
                if (this.gameCardsImagesCollection[j].Visible)
                {
                    this.gameCardsImagesCollection[j].Image = this.Deck[j];
                }
            }

            if (player.Type == this.sorted.Current)
            {
                if (player.PowerHand == this.sorted.Power)
                {
                    this.winners++;
                    this.CheckWinners.Add(currentText);
                    if (player.Type == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (player.Type == 1 || player.Type == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (player.Type == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (player.Type == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }

                    if (player.Type == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (player.Type == 5 || player.Type == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (player.Type == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (player.Type == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (player.Type == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (player.Type == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }

            if (currentText == lastly)
            {
                // lastfixed
                if (this.winners > 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.players[Players.Player].CurrentChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxChips.Text = this.players[Players.Player].CurrentChips.ToString();

                        // pPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.players[Players.Bot1].CurrentChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotChips1.Text = this.players[Players.Bot1].CurrentChips.ToString();

                        // b1Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.players[Players.Bot2].CurrentChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotChips2.Text = this.players[Players.Bot2].CurrentChips.ToString();

                        // b2Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.players[Players.Bot3].CurrentChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotChips3.Text = this.players[Players.Bot3].CurrentChips.ToString();

                        // b3Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.players[Players.Bot4].CurrentChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotChips4.Text = this.players[Players.Bot4].CurrentChips.ToString();

                        // b4Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.players[Players.Bot5].CurrentChips += int.Parse(this.textBoxPot.Text) / this.winners;
                        this.textBoxBotChips5.Text = this.players[Players.Bot5].CurrentChips.ToString();

                        // b5Panel.Visible = true;
                    }

                    // await Finish(1);
                }

                if (this.winners == 1)
                {
                    if (this.CheckWinners.Contains("Player"))
                    {
                        this.players[Players.Player].CurrentChips += int.Parse(this.textBoxPot.Text);

                        // await Finish(1);
                        // pPanel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 1"))
                    {
                        this.players[Players.Bot1].CurrentChips += int.Parse(this.textBoxPot.Text);

                        // await Finish(1);
                        // b1Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 2"))
                    {
                        this.players[Players.Bot2].CurrentChips += int.Parse(this.textBoxPot.Text);

                        // await Finish(1);
                        // b2Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 3"))
                    {
                        this.players[Players.Bot3].CurrentChips += int.Parse(this.textBoxPot.Text);

                        // await Finish(1);
                        // b3Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 4"))
                    {
                        this.players[Players.Bot4].CurrentChips += int.Parse(this.textBoxPot.Text);

                        // await Finish(1);
                        // b4Panel.Visible = true;
                    }

                    if (this.CheckWinners.Contains("Bot 5"))
                    {
                        this.players[Players.Bot5].CurrentChips += int.Parse(this.textBoxPot.Text);

                        // await Finish(1);
                        // b5Panel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (this.raising)
            {
                this.turnCount = 0;
                this.raising = false;
                this.raisedTurn = currentTurn;
                this.changed = true;
            }
            else
            {
                if (this.turnCount >= this.maxLeft - 1 || !this.changed && this.turnCount == this.maxLeft)
                {
                    if (currentTurn == this.raisedTurn - 1 || !this.changed && this.turnCount == this.maxLeft
                        || this.raisedTurn == 0 && currentTurn == 5)
                    {
                        this.changed = false;
                        this.turnCount = 0;
                        this.Raise = 0;
                        this.call = 0;
                        this.raisedTurn = 123;
                        this.rounds++;
                        if (!this.players[Players.Player].FoldedTurn)
                        {
                            this.playerStatus.Text = string.Empty;
                        }

                        if (!this.players[Players.Bot1].FoldedTurn)
                        {
                            this.bot1Status.Text = string.Empty;
                        }

                        if (!this.players[Players.Bot2].FoldedTurn)
                        {
                            this.bot2Status.Text = string.Empty;
                        }

                        if (!this.players[Players.Bot3].FoldedTurn)
                        {
                            this.bot3Status.Text = string.Empty;
                        }

                        if (!this.players[Players.Bot4].FoldedTurn)
                        {
                            this.bot4Status.Text = string.Empty;
                        }

                        if (!this.players[Players.Bot5].FoldedTurn)
                        {
                            this.bot5Status.Text = string.Empty;
                        }
                    }
                }
            }

            if (this.rounds == (double)TurnParts.Flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (this.gameCardsImagesCollection[j].Image != this.Deck[j])
                    {
                        this.gameCardsImagesCollection[j].Image = this.Deck[j];
                        this.players[Players.Player].Call = 0;
                        this.players[Players.Player].Raise = 0;
                        this.players[Players.Bot1].Call = 0;
                        this.players[Players.Bot1].Raise = 0;
                        this.players[Players.Bot2].Call = 0;
                        this.players[Players.Bot2].Raise = 0;
                        this.players[Players.Bot3].Call = 0;
                        this.players[Players.Bot3].Raise = 0;
                        this.players[Players.Bot4].Call = 0;
                        this.players[Players.Bot4].Raise = 0;
                        this.players[Players.Bot5].Call = 0;
                        this.players[Players.Bot5].Raise = 0;
                    }
                }
            }

            if (this.rounds == (double)TurnParts.Turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (this.gameCardsImagesCollection[j].Image != this.Deck[j])
                    {
                        this.gameCardsImagesCollection[j].Image = this.Deck[j];
                        this.players[Players.Player].Call = 0;
                        this.players[Players.Player].Raise = 0;
                        this.players[Players.Bot1].Call = 0;
                        this.players[Players.Bot1].Raise = 0;
                        this.players[Players.Bot2].Call = 0;
                        this.players[Players.Bot2].Raise = 0;
                        this.players[Players.Bot3].Call = 0;
                        this.players[Players.Bot3].Raise = 0;
                        this.players[Players.Bot4].Call = 0;
                        this.players[Players.Bot4].Raise = 0;
                        this.players[Players.Bot5].Call = 0;
                        this.players[Players.Bot5].Raise = 0;
                    }
                }
            }

            if (this.rounds == (double)TurnParts.River)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (this.gameCardsImagesCollection[j].Image != this.Deck[j])
                    {
                        this.gameCardsImagesCollection[j].Image = this.Deck[j];
                        this.players[Players.Player].Call = 0;
                        this.players[Players.Player].Raise = 0;
                        this.players[Players.Bot1].Call = 0;
                        this.players[Players.Bot1].Raise = 0;
                        this.players[Players.Bot2].Call = 0;
                        this.players[Players.Bot2].Raise = 0;
                        this.players[Players.Bot3].Call = 0;
                        this.players[Players.Bot3].Raise = 0;
                        this.players[Players.Bot4].Call = 0;
                        this.players[Players.Bot4].Raise = 0;
                        this.players[Players.Bot5].Call = 0;
                        this.players[Players.Bot5].Raise = 0;
                    }
                }
            }

            if (this.rounds == (double)TurnParts.End && this.maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!this.playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    this.Rules(0, 1, "Player", this.players[Players.Player]);
                }

                if (!this.bot1Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    this.Rules(2, 3, "Bot 1", this.players[Players.Bot1]);
                }

                if (!this.bot2Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    this.Rules(4, 5, "Bot 2", this.players[Players.Bot2]);
                }

                if (!this.bot3Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    this.Rules(6, 7, "Bot 3", this.players[Players.Bot3]);
                }

                if (!this.bot4Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    this.Rules(8, 9, "Bot 4", this.players[Players.Bot4]);
                }

                if (!this.bot5Status.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    this.Rules(10, 11, "Bot 5", this.players[Players.Bot5]);
                }

                this.Winner(this.players[Players.Player], "Player", fixedLast);
                this.Winner(this.players[Players.Bot1], "Bot 1", fixedLast);
                this.Winner(this.players[Players.Bot2], "Bot 2", fixedLast);
                this.Winner(this.players[Players.Bot3], "Bot 3", fixedLast);
                this.Winner(this.players[Players.Bot4], "Bot 4", fixedLast);
                this.Winner(this.players[Players.Bot5], "Bot 5", fixedLast);

                this.restart = true;

                this.players[Players.Player].Turn = true;

                foreach (Player player in this.players.Values)
                {
                    player.FoldedTurn = false;
                }

                if (this.players[Players.Player].CurrentChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.A != 0)
                    {
                        this.players[Players.Player].CurrentChips = f2.A;
                        this.players[Players.Bot1].CurrentChips += f2.A;
                        this.players[Players.Bot2].CurrentChips += f2.A;
                        this.players[Players.Bot3].CurrentChips += f2.A;
                        this.players[Players.Bot4].CurrentChips += f2.A;
                        this.players[Players.Bot5].CurrentChips += f2.A;
                        this.players[Players.Player].FoldedTurn = false;
                        this.players[Players.Player].Turn = true;
                        this.buttonRaise.Enabled = true;
                        this.buttonFold.Enabled = true;
                        this.bCheck.Enabled = true;
                        this.buttonRaise.Text = "Raise";
                    }
                }

                foreach (Player player in this.players.Values)
                {
                    player.Panel.Visible = true;
                    player.Call = 0;
                    player.Raise = 0;
                    player.PowerHand = 0;
                    player.Type = -1;
                }

                this.last = 0;
                this.call = this.bigBlind;
                this.Raise = 0;
                this.allGameCardsImagesWithLocationsCollection = Directory.GetFiles(@"..\..\..\Poker\Resources\Assets\Cards", "*.png", SearchOption.TopDirectoryOnly);
                this.rounds = 0;
                this.type = 0;
                this.ints.Clear();
                this.CheckWinners.Clear();
                this.winners = 0;
                this.Win.Clear();
                this.sorted.Current = 0;
                this.sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    this.gameCardsImagesCollection[os].Image = null;
                    this.gameCardsImagesCollection[os].Invalidate();
                    this.gameCardsImagesCollection[os].Visible = false;
                }

                this.textBoxPot.Text = "0";
                this.playerStatus.Text = string.Empty;
                await this.Shuffle();
                await this.Turns();
            }
        }

        void FixCall(Label status, Player player, int options)
        {
            if (this.rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        player.Raise = int.Parse(changeRaise);
                    }

                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        player.Call = int.Parse(changeCall);
                    }

                    if (status.Text.Contains("Check"))
                    {
                        player.Raise = 0;
                        player.Call = 0;
                    }
                }

                if (options == 2)
                {
                    if (player.Raise != this.Raise && player.Raise <= this.Raise)
                    {
                        this.call = Convert.ToInt32(this.Raise) - player.Raise;
                    }

                    if (player.Call != this.call || player.Call <= this.call)
                    {
                        this.call = this.call - player.Call;
                    }

                    if (player.Raise == this.Raise && this.Raise > 0)
                    {
                        this.call = 0;
                        this.buttonCall.Enabled = false;
                        this.buttonCall.Text = "Callisfuckedup"; // No shit Batman
                    }
                }
            }
        }

        async Task AllIn()
        {
            if (this.players[Players.Player].CurrentChips <= 0 && !this.intsadded)
            {
                if (this.playerStatus.Text.Contains("Raise"))
                {
                    this.ints.Add(this.players[Players.Player].CurrentChips);
                    this.intsadded = true;
                }

                if (this.playerStatus.Text.Contains("Call"))
                {
                    this.ints.Add(this.players[Players.Player].CurrentChips);
                    this.intsadded = true;
                }
            }

            this.intsadded = false;

            IEnumerable<Player> outOfChipsBots =
                this.players.Where(player => player.Key.ToString().Contains("Bot"))
                    .Where(player => player.Value.CurrentChips <= 0)
                    .Where(player => !player.Value.FoldedTurn)
                    .Select(player => player.Value);

            foreach (Player player in outOfChipsBots)
            {
                if (this.intsadded)
                {
                    this.ints.Add(player.CurrentChips);
                    this.intsadded = true;
                }

                this.intsadded = false;
            }

            if (this.ints.ToArray().Length == this.maxLeft)
            {
                await this.Finish(2);
            }
            else
            {
                this.ints.Clear();
            }

            var playersStillStanding = this.players.Values.Count(x => !x.FoldedTurn);

            #region LastManStanding

            if (playersStillStanding == 1)
            {
                var winner = this.players.FirstOrDefault(p => !p.Value.FoldedTurn);

                winner.Value.CurrentChips += int.Parse(this.textBoxPot.Text);
                this.textBoxChips.Text = winner.Value.CurrentChips.ToString();
                winner.Value.Panel.Visible = true;
                MessageBox.Show(winner.Key + " Wins");

                for (int j = 0; j <= 16; j++)
                {
                    this.gameCardsImagesCollection[j].Visible = false;
                }

                await this.Finish(1);
            }

            this.intsadded = false;

            #endregion

            #region FiveOrLessLeft

            if (playersStillStanding < 6 && playersStillStanding > 1 && this.rounds >= (double)TurnParts.End)
            {
                await this.Finish(2);
            }

            #endregion
        }

        async Task Finish(int n)
        {
            if (n == 2)
            {
                this.FixWinners();
            }

            foreach (Player player in this.players.Values)
            {
                // TODO: Add those to player.SetDefaults() maybe?
                player.Panel.Visible = true;
                player.PowerHand = 0;
                player.Type = -1;
                player.Turn = false;
                player.FoldedTurn = false;
                player.Folded = false;
                player.Call = 0;
                player.Raise = 0;
            }

            this.players[Players.Player].Turn = true;

            this.call = this.bigBlind;
            this.Raise = 0;
            this.foldedPlayers = 5;
            this.type = 0;
            this.rounds = 0;
            this.Raise = 0;
            this.restart = false;
            this.raising = false;
            this.height = 0;
            this.width = 0;
            this.winners = 0;
            this.maxLeft = 6;
            this.last = 123;
            this.raisedTurn = 1;
            this.CheckWinners.Clear();
            this.ints.Clear();
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            this.textBoxPot.Text = "0";
            this.time = 60;
            this.up = 10000000;
            this.turnCount = 0;

            // TODO: Add those to Player?
            this.playerStatus.Text = string.Empty;
            this.bot1Status.Text = string.Empty;
            this.bot2Status.Text = string.Empty;
            this.bot3Status.Text = string.Empty;
            this.bot4Status.Text = string.Empty;
            this.bot5Status.Text = string.Empty;
            if (this.players[Players.Player].CurrentChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.A != 0)
                {
                    this.players[Players.Player].CurrentChips = f2.A;
                    this.players[Players.Bot1].CurrentChips += f2.A;
                    this.players[Players.Bot2].CurrentChips += f2.A;
                    this.players[Players.Bot3].CurrentChips += f2.A;
                    this.players[Players.Bot4].CurrentChips += f2.A;
                    this.players[Players.Bot5].CurrentChips += f2.A;
                    this.players[Players.Player].FoldedTurn = false;
                    this.players[Players.Player].Turn = true;
                    this.buttonRaise.Enabled = true;
                    this.buttonFold.Enabled = true;
                    this.bCheck.Enabled = true;
                    this.buttonRaise.Text = "Raise";
                }
            }

            this.allGameCardsImagesWithLocationsCollection = Directory.GetFiles(@"..\..\..\Poker\Resources\Assets\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                this.gameCardsImagesCollection[os].Image = null;
                this.gameCardsImagesCollection[os].Invalidate();
                this.gameCardsImagesCollection[os].Visible = false;
            }

            await this.Shuffle();

            // await Turns();
        }

        void FixWinners()
        {
            this.Win.Clear();
            this.sorted.Current = 0;
            this.sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!this.playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                this.Rules(0, 1, "Player", this.players[Players.Player]);
            }

            if (!this.bot1Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                this.Rules(2, 3, "Bot 1", this.players[Players.Bot1]);
            }

            if (!this.bot2Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                this.Rules(4, 5, "Bot 2", this.players[Players.Bot2]);
            }

            if (!this.bot3Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                this.Rules(6, 7, "Bot 3", this.players[Players.Bot3]);
            }

            if (!this.bot4Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                this.Rules(8, 9, "Bot 4", this.players[Players.Bot4]);
            }

            if (!this.bot5Status.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                this.Rules(10, 11, "Bot 5", this.players[Players.Bot5]);
            }

            this.Winner(this.players[Players.Player], "Player", fixedLast);
            this.Winner(this.players[Players.Bot1], "Bot 1", fixedLast);
            this.Winner(this.players[Players.Bot2], "Bot 2", fixedLast);
            this.Winner(this.players[Players.Bot3], "Bot 3", fixedLast);
            this.Winner(this.players[Players.Bot4], "Bot 4", fixedLast);
            this.Winner(this.players[Players.Bot5], "Bot 5", fixedLast);
        }

        void AI(int c1, int c2, Label sStatus, int name, Player player)
        {
            if (!player.FoldedTurn)
            {
                if (player.Type == -1)
                {
                    this.HighCard(player, sStatus, player.PowerHand);
                }

                if (player.Type == 0)
                {
                    this.PairTable(player, sStatus, player.PowerHand);
                }

                if (player.Type == 1)
                {
                    this.PairHand(player, sStatus, player.PowerHand);
                }

                if (player.Type == 2)
                {
                    this.TwoPair(player, sStatus, player.PowerHand);
                }

                if (player.Type == 3)
                {
                    this.ThreeOfAKind(player, sStatus, name, player.PowerHand);
                }

                if (player.Type == 4)
                {
                    this.Straight(player, sStatus, name, player.PowerHand);
                }

                if (player.Type == 5 || player.Type == 5.5)
                {
                    this.Flush(player, sStatus, name, player.PowerHand);
                }

                if (player.Type == 6)
                {
                    this.FullHouse(player, sStatus, name, player.PowerHand);
                }

                if (player.Type == 7)
                {
                    this.FourOfAKind(player, sStatus, name, player.PowerHand);
                }

                if (player.Type == 8 || player.Type == 9)
                {
                    this.StraightFlush(player, sStatus, name, player.PowerHand);
                }
            }

            if (player.FoldedTurn)
            {
                this.gameCardsImagesCollection[c1].Visible = false;
                this.gameCardsImagesCollection[c2].Visible = false;
            }
        }

        private void HighCard(Player player, Label sStatus, double botPower)
        {
            this.HP(player, sStatus, botPower, 20, 25);
        }

        private void PairTable(Player player, Label sStatus, double botPower)
        {
            this.HP(player, sStatus, botPower, 16, 25);
        }

        private void PairHand(Player player, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                this.PH(player, sStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                this.PH(player, sStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                this.PH(player, sStatus, rCall, 9, rRaise);
            }
        }

        private void TwoPair(Player player, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                this.PH(player, sStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                this.PH(player, sStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                this.PH(player, sStatus, rCall, 4, rRaise);
            }
        }

        private void ThreeOfAKind(Player player, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                this.Smooth(player, sStatus, name, tCall, tRaise);
            }

            if (botPower <= 327 && botPower >= 321)
            {
                // 10  8
                this.Smooth(player, sStatus, name, tCall, tRaise);
            }

            if (botPower < 321 && botPower >= 303)
            {
                // 7 2
                this.Smooth(player, sStatus, name, tCall, tRaise);
            }
        }

        private void Straight(Player player, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                this.Smooth(player, sStatus, name, sCall, sRaise);
            }

            if (botPower <= 409 && botPower >= 407)
            {
                // 10  8
                this.Smooth(player, sStatus, name, sCall, sRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
                this.Smooth(player, sStatus, name, sCall, sRaise);
            }
        }

        private void Flush(Player player, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            this.Smooth(player, sStatus, name, fCall, fRaise);
        }

        private void FullHouse(Player player, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                this.Smooth(player, sStatus, name, fhCall, fhRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
                this.Smooth(player, sStatus, name, fhCall, fhRaise);
            }
        }

        private void FourOfAKind(Player player, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                this.Smooth(player, sStatus, name, fkCall, fkRaise);
            }
        }

        private void StraightFlush(Player player, Label sStatus, int name, double botPower)
        {
            Random straightFlush = new Random();
            int straightFlushCall = straightFlush.Next(1, 3);
            int straightFlushRaise = straightFlush.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                this.Smooth(player, sStatus, name, straightFlushCall, straightFlushRaise);
            }
        }

        private void Fold(Player player, Label sStatus)
        {
            this.raising = false;
            sStatus.Text = "Fold";
            player.Turn = false;
            player.FoldedTurn = true;
        }

        private void Check(Player player, Label cStatus)
        {
            cStatus.Text = "Check";
            player.Turn = false;
            this.raising = false;
        }

        private void Call(Player player, Label sStatus)
        {
            this.raising = false;
            player.Turn = false;
            player.CurrentChips -= this.call;
            sStatus.Text = "Call " + this.call;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
        }

        private void Raised(Player player, Label sStatus)
        {
            player.CurrentChips -= Convert.ToInt32(this.Raise);
            sStatus.Text = "Raise " + this.Raise;
            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + Convert.ToInt32(this.Raise)).ToString();
            this.call = Convert.ToInt32(this.Raise);
            this.raising = true;
            player.Turn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips / n) / 100d, 0) * 100;
            return a;
        }

        private void HP(Player player, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (this.call <= 0)
            {
                this.Check(player, sStatus);
            }

            if (this.call > 0)
            {
                if (rnd == 1)
                {
                    if (this.call <= RoundN(player.CurrentChips, n))
                    {
                        this.Call(player, sStatus);
                    }
                    else
                    {
                        this.Fold(player, sStatus);
                    }
                }

                if (rnd == 2)
                {
                    if (this.call <= RoundN(player.CurrentChips, n1))
                    {
                        this.Call(player, sStatus);
                    }
                    else
                    {
                        this.Fold(player, sStatus);
                    }
                }
            }

            if (rnd == 3)
            {
                if (this.Raise == 0)
                {
                    this.Raise = this.call * 2;
                    this.Raised(player, sStatus);
                }
                else
                {
                    if (this.Raise <= RoundN(player.CurrentChips, n))
                    {
                        this.Raise = this.call * 2;
                        this.Raised(player, sStatus);
                    }
                    else
                    {
                        this.Fold(player, sStatus);
                    }
                }
            }

            if (player.CurrentChips <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        private void PH(Player player, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (this.rounds < 2)
            {
                if (this.call <= 0)
                {
                    this.Check(player, sStatus);
                }

                if (this.call > 0)
                {
                    if (this.call >= RoundN(player.CurrentChips, n1))
                    {
                        this.Fold(player, sStatus);
                    }

                    if (this.Raise > RoundN(player.CurrentChips, n))
                    {
                        this.Fold(player, sStatus);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (this.call >= RoundN(player.CurrentChips, n) && this.call <= RoundN(player.CurrentChips, n1))
                        {
                            this.Call(player, sStatus);
                        }

                        if (this.Raise <= RoundN(player.CurrentChips, n) && this.Raise >= RoundN(player.CurrentChips, n) / 2)
                        {
                            this.Call(player, sStatus);
                        }

                        if (this.Raise <= RoundN(player.CurrentChips, n) / 2)
                        {
                            if (this.Raise > 0)
                            {
                                this.Raise = RoundN(player.CurrentChips, n);
                                this.Raised(player, sStatus);
                            }
                            else
                            {
                                this.Raise = this.call * 2;
                                this.Raised(player, sStatus);
                            }
                        }
                    }
                }
            }

            if (this.rounds >= 2)
            {
                if (this.call > 0)
                {
                    if (this.call >= RoundN(player.CurrentChips, n1 - rnd))
                    {
                        this.Fold(player, sStatus);
                    }

                    if (this.Raise > RoundN(player.CurrentChips, n - rnd))
                    {
                        this.Fold(player, sStatus);
                    }

                    if (!player.FoldedTurn)
                    {
                        if (this.call >= RoundN(player.CurrentChips, n - rnd) && this.call <= RoundN(player.CurrentChips, n1 - rnd))
                        {
                            this.Call(player, sStatus);
                        }

                        if (this.Raise <= RoundN(player.CurrentChips, n - rnd)
                            && this.Raise >= RoundN(player.CurrentChips, n - rnd) / 2)
                        {
                            this.Call(player, sStatus);
                        }

                        if (this.Raise <= RoundN(player.CurrentChips, n - rnd) / 2)
                        {
                            if (this.Raise > 0)
                            {
                                this.Raise = RoundN(player.CurrentChips, n - rnd);
                                this.Raised(player, sStatus);
                            }
                            else
                            {
                                this.Raise = this.call * 2;
                                this.Raised(player, sStatus);
                            }
                        }
                    }
                }

                if (this.call <= 0)
                {
                    this.Raise = RoundN(player.CurrentChips, r - rnd);
                    this.Raised(player, sStatus);
                }
            }

            if (player.CurrentChips <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        void Smooth(Player player, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (this.call <= 0)
            {
                this.Check(player, botStatus);
            }
            else
            {
                if (this.call >= RoundN(player.CurrentChips, n))
                {
                    if (player.CurrentChips > this.call)
                    {
                        this.Call(player, botStatus);
                    }
                    else if (player.CurrentChips <= this.call)
                    {
                        this.raising = false;
                        player.Turn = false;
                        player.CurrentChips = 0;
                        botStatus.Text = "Call " + player.CurrentChips;
                        this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + player.CurrentChips).ToString();
                    }
                }
                else
                {
                    if (this.Raise > 0)
                    {
                        if (player.CurrentChips >= this.Raise * 2)
                        {
                            this.Raise *= 2;
                            this.Raised(player, botStatus);
                        }
                        else
                        {
                            this.Call(player, botStatus);
                        }
                    }
                    else
                    {
                        this.Raise = this.call * 2;
                        this.Raised(player, botStatus);
                    }
                }
            }

            if (player.CurrentChips <= 0)
            {
                player.FoldedTurn = true;
            }
        }

        #region UI

        private async void TimerTick(object sender, object e)
        {
            if (this.progressBarTimer.Value <= 0)
            {
                this.players[Players.Player].FoldedTurn = true;
                await this.Turns();
            }

            if (this.time > 0)
            {
                this.time--;
                this.progressBarTimer.Value = (this.time / 6) * 100;
            }
        }

        private void UpdateTick(object sender, object e)
        {
            this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
            this.textBoxBotChips1.Text = "Chips : " + this.players[Players.Bot1].CurrentChips.ToString();
            this.textBoxBotChips2.Text = "Chips : " + this.players[Players.Bot2].CurrentChips.ToString();
            this.textBoxBotChips3.Text = "Chips : " + this.players[Players.Bot3].CurrentChips.ToString();
            this.textBoxBotChips4.Text = "Chips : " + this.players[Players.Bot4].CurrentChips.ToString();
            this.textBoxBotChips5.Text = "Chips : " + this.players[Players.Bot5].CurrentChips.ToString();

            if (this.players[Players.Player].CurrentChips <= 0)
            {
                this.players[Players.Player].Turn = false;
                this.players[Players.Player].FoldedTurn = true;
                this.buttonCall.Enabled = false;
                this.buttonRaise.Enabled = false;
                this.buttonFold.Enabled = false;
                this.bCheck.Enabled = false;
            }

            if (this.up > 0)
            {
                this.up--;
            }

            if (this.players[Players.Player].CurrentChips >= this.call)
            {
                this.buttonCall.Text = "Call " + this.call.ToString();
            }
            else
            {
                this.buttonCall.Text = "All in";
                this.buttonRaise.Enabled = false;
            }

            if (this.call > 0)
            {
                this.bCheck.Enabled = false;
            }

            if (this.call <= 0)
            {
                this.bCheck.Enabled = true;
                this.buttonCall.Text = "Call";
                this.buttonCall.Enabled = false;
            }

            if (this.players[Players.Player].CurrentChips <= 0)
            {
                this.buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (this.textBoxRaise.Text != string.Empty && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.players[Players.Player].CurrentChips <= int.Parse(this.textBoxRaise.Text))
                {
                    this.buttonRaise.Text = "All in";
                }
                else
                {
                    this.buttonRaise.Text = "Raise";
                }
            }

            if (this.players[Players.Player].CurrentChips < this.call)
            {
                this.buttonRaise.Enabled = false;
            }
        }

        private async void ButtonFoldClick(object sender, EventArgs e)
        {
            this.playerStatus.Text = "Fold";
            this.players[Players.Player].Turn = false;
            this.players[Players.Player].FoldedTurn = true;
            await this.Turns();
        }

        private async void ButonCheckClick(object sender, EventArgs e)
        {
            if (this.call <= 0)
            {
                this.players[Players.Player].Turn = false;
                this.playerStatus.Text = "Check";
            }
            else
            {
                // playerStatus.Text = "All in " + Chips;
                this.bCheck.Enabled = false;
            }

            await this.Turns();
        }

        private async void ButtonCallClick(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", this.players[Players.Player]);
            if (this.players[Players.Player].CurrentChips >= this.call)
            {
                this.players[Players.Player].CurrentChips -= this.call;
                this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
                if (this.textBoxPot.Text != string.Empty)
                {
                    this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                }
                else
                {
                    this.textBoxPot.Text = this.call.ToString();
                }

                this.players[Players.Player].Turn = false;
                this.playerStatus.Text = "Call " + this.call;
                this.players[Players.Player].Call = this.call;
            }
            else if (this.players[Players.Player].CurrentChips <= this.call && this.call > 0)
            {
                this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.players[Players.Player].CurrentChips).ToString();
                this.playerStatus.Text = "All in " + this.players[Players.Player].CurrentChips;
                this.players[Players.Player].CurrentChips = 0;
                this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
                this.players[Players.Player].Turn = false;
                this.buttonFold.Enabled = false;
                this.players[Players.Player].Call = this.players[Players.Player].CurrentChips;
            }

            await this.Turns();
        }

        private async void ButtonRaiseClick(object sender, EventArgs e)
        {
            this.Rules(0, 1, "Player", this.players[Players.Player]);
            int parsedValue;
            if (this.textBoxRaise.Text != string.Empty && int.TryParse(this.textBoxRaise.Text, out parsedValue))
            {
                if (this.players[Players.Player].CurrentChips > this.call)
                {
                    if (this.Raise * 2 > int.Parse(this.textBoxRaise.Text))
                    {
                        this.textBoxRaise.Text = (this.Raise * 2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (this.players[Players.Player].CurrentChips >= int.Parse(this.textBoxRaise.Text))
                        {
                            this.call = int.Parse(this.textBoxRaise.Text);
                            this.Raise = int.Parse(this.textBoxRaise.Text);
                            this.playerStatus.Text = "Raise " + this.call.ToString();
                            this.textBoxPot.Text = (int.Parse(this.textBoxPot.Text) + this.call).ToString();
                            this.buttonCall.Text = "Call";
                            this.players[Players.Player].CurrentChips -= int.Parse(this.textBoxRaise.Text);
                            this.raising = true;
                            this.last = 0;
                            this.players[Players.Player].Raise = Convert.ToInt32(this.Raise);
                        }
                        else
                        {
                            this.call = this.players[Players.Player].CurrentChips;
                            this.Raise = this.players[Players.Player].CurrentChips;
                            this.textBoxPot.Text =
                                (int.Parse(this.textBoxPot.Text) + this.players[Players.Player].CurrentChips).ToString();
                            this.playerStatus.Text = "Raise " + this.call.ToString();
                            this.players[Players.Player].CurrentChips = 0;
                            this.raising = true;
                            this.last = 0;
                            this.players[Players.Player].Raise = Convert.ToInt32(this.Raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            this.players[Players.Player].Turn = false;
            await this.Turns();
        }

        private void ButtonAddClick(object sender, EventArgs e)
        {
            if (this.tbAdd.Text == string.Empty)
            {
            }
            else
            {
                this.players[Players.Player].CurrentChips += int.Parse(this.tbAdd.Text);
                this.players[Players.Bot1].CurrentChips += int.Parse(this.tbAdd.Text);
                this.players[Players.Bot2].CurrentChips += int.Parse(this.tbAdd.Text);
                this.players[Players.Bot3].CurrentChips += int.Parse(this.tbAdd.Text);
                this.players[Players.Bot4].CurrentChips += int.Parse(this.tbAdd.Text);
                this.players[Players.Bot5].CurrentChips += int.Parse(this.tbAdd.Text);
            }

            this.textBoxChips.Text = "Chips : " + this.players[Players.Player].CurrentChips.ToString();
        }

        private void ButtonOptionsClick(object sender, EventArgs e)
        {
            this.textBoxBigBlind.Text = this.bigBlind.ToString();
            this.textBoxSmallBlind.Text = this.smallBlind.ToString();
            if (this.textBoxBigBlind.Visible == false)
            {
                this.textBoxBigBlind.Visible = true;
                this.textBoxSmallBlind.Visible = true;
                this.buttonBigBlind.Visible = true;
                this.buttoneSmallBlind.Visible = true;
            }
            else
            {
                this.textBoxBigBlind.Visible = false;
                this.textBoxSmallBlind.Visible = false;
                this.buttonBigBlind.Visible = false;
                this.buttoneSmallBlind.Visible = false;
            }
        }

        private void ButtonSmallBlindClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxSmallBlind.Text.Contains(",") || this.textBoxSmallBlind.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                this.textBoxSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = this.smallBlind.ToString();
                return;
            }

            if (int.Parse(this.textBoxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                this.textBoxSmallBlind.Text = this.smallBlind.ToString();
            }

            if (int.Parse(this.textBoxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(this.textBoxSmallBlind.Text) >= 250 && int.Parse(this.textBoxSmallBlind.Text) <= 100000)
            {
                this.smallBlind = int.Parse(this.textBoxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void ButtonBigBlindClick(object sender, EventArgs e)
        {
            int parsedValue;
            if (this.textBoxBigBlind.Text.Contains(",") || this.textBoxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                this.textBoxBigBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (!int.TryParse(this.textBoxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                this.textBoxSmallBlind.Text = this.bigBlind.ToString();
                return;
            }

            if (int.Parse(this.textBoxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                this.textBoxBigBlind.Text = this.bigBlind.ToString();
            }

            if (int.Parse(this.textBoxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(this.textBoxBigBlind.Text) >= 500 && int.Parse(this.textBoxBigBlind.Text) <= 200000)
            {
                this.bigBlind = int.Parse(this.textBoxBigBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void LayoutChange(object sender, LayoutEventArgs e)
        {
            this.width = this.Width;
            this.height = this.Height;
        }

        #endregion

        private void PokerGameForm_Load(object sender, EventArgs e)
        {

        }
    }
}