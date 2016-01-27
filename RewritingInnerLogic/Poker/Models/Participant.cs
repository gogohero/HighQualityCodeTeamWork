namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Constants;
    using Poker.Enumerations;
    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    /// <summary>
    /// Class Participant.
    /// </summary>
    public abstract class Participant : IParticipant
    {
        /// <summary>
        /// The starting chips
        /// </summary>
        private int startingChips = GlobalConstants.StartingChips;

        /// <summary>
        /// The chips placed.
        /// </summary>
        private int chipsPlaced;

        /// <summary>
        /// The previously called times.
        /// </summary>
        private int previouslyCalled;

        /// <summary>
        /// Initializes a new instance of the <see cref="Participant"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="placeOnBoard">The place on board.</param>
        protected Participant(string name, Point placeOnBoard)
        {
            this.Chips = this.startingChips;
            this.Name = name;
            this.Controls = new Dictionary<string, Control>();
            this.Hand = new Hand();

            this.PlaceOnBoard = placeOnBoard;
        }

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>The controls.</value>
        public Dictionary<string, Control> Controls { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the chips.
        /// </summary>
        /// <value>The chips.</value>
        public int Chips { get; set; }

        /// <summary>
        /// Gets or sets the previously called.
        /// </summary>
        /// <value>The previously called.</value>
        /// <exception cref="System.ArgumentException">Previously called value cannot be negative</exception>
        public int PreviouslyCalled
        {
            get
            {
                return this.previouslyCalled;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Previously called value cannot be negative");
                }

                this.previouslyCalled = value;
            }
        }

        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>The hand.</value>
        public IHand Hand { get; set; }

        /// <summary>
        /// Gets or sets the place on board.
        /// </summary>
        /// <value>The place on board.</value>
        public Point PlaceOnBoard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is all in.
        /// </summary>
        /// <value><c>true</c> if this instance is all in; otherwise, <c>false</c>.</value>
        public bool IsAllIn { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has acted.
        /// </summary>
        /// <value><c>true</c> if this instance has acted; otherwise, <c>false</c>.</value>
        public bool HasActed
        {
            get
            {
                return this.HasCalled || this.HasChecked || this.HasRaised || this.HasFolded || this.IsAllIn;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has folded.
        /// </summary>
        /// <value><c>true</c> if this instance has folded; otherwise, <c>false</c>.</value>
        public bool HasFolded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has checked.
        /// </summary>
        /// <value><c>true</c> if this instance has checked; otherwise, <c>false</c>.</value>
        public bool HasChecked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has raised.
        /// </summary>
        /// <value><c>true</c> if this instance has raised; otherwise, <c>false</c>.</value>
        public bool HasRaised { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has called.
        /// </summary>
        /// <value><c>true</c> if this instance has called; otherwise, <c>false</c>.</value>
        public bool HasCalled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [wins round].
        /// </summary>
        /// <value><c>true</c> if [wins round]; otherwise, <c>false</c>.</value>
        public bool WinsRound { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is in game.
        /// </summary>
        /// <value><c>true</c> if this instance is in game; otherwise, <c>false</c>.</value>
        public bool IsInGame
        {
            get
            {
                return this.Chips > 0;
            }
        }

        /// <summary>
        /// Gets or sets the chips placed.
        /// </summary>
        /// <value>The chips placed.</value>
        /// <exception cref="System.ArgumentException">Chips placed value cannot be negative</exception>
        public int ChipsPlaced
        {
            get
            {
                return this.chipsPlaced;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Chips placed value cannot be negative");
                }

                this.chipsPlaced = value;
            }
        }

        /// <summary>
        /// Calls the specified current highest bet.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        public virtual void Call(ref int currentHighestBet)
        {
            if (this.Chips > currentHighestBet)
            {
                this.ResetFlags();
                this.Chips -= currentHighestBet;
                this.ChipsPlaced += currentHighestBet;
                this.HasCalled = true;
                this.Controls["StatusBox"].Text = GlobalConstants.CallText + currentHighestBet;
                this.Controls["ChipsBox"].Text = $"{this.Name} Chips: {this.Chips}";
                this.previouslyCalled = currentHighestBet;
            }
            else
            {
                this.ResetFlags();
                this.ChipsPlaced += this.Chips;
                this.Chips = 0;
                this.IsAllIn = true;
                this.Controls["StatusBox"].Text = GlobalConstants.AllInText;
                this.Controls["ChipsBox"].Text = $"{this.Name} Chips: {this.Chips}";
            }
        }

        /// <summary>
        /// Raises the specified raise amount.
        /// </summary>
        /// <param name="raiseAmount">The raise amount.</param>
        /// <param name="currentHighestBet">The current highest bet.</param>
        public virtual void Raise(int raiseAmount, ref int currentHighestBet)
        {
            if (raiseAmount > currentHighestBet)
            {
                raiseAmount -= currentHighestBet;
                currentHighestBet += raiseAmount;

                if (this.Chips > currentHighestBet)
                {
                    this.ResetFlags();
                    this.Chips -= currentHighestBet;
                    this.ChipsPlaced += currentHighestBet;
                    this.HasRaised = true;
                    this.Controls["StatusBox"].Text = GlobalConstants.RaiseText + raiseAmount;
                    this.Controls["ChipsBox"].Text = $"{this.Name} Chips: {this.Chips}";
                }
                else
                {
                    this.ResetFlags();
                    this.ChipsPlaced += this.Chips;
                    this.Chips = 0;
                    this.IsAllIn = true;
                    this.Controls["StatusBox"].Text = GlobalConstants.AllInText;
                    this.Controls["ChipsBox"].Text = $"{this.Name} Chips: {this.Chips}";
                }
            }
        }

        /// <summary>
        /// Folds this instance.
        /// </summary>
        public void Fold()
        {
            this.ResetFlags();
            this.HasFolded = true;
            this.Hand.CurrentCards[0].PictureBox.Visible = false;
            this.Hand.CurrentCards[0].PictureBox.Update();
            this.Hand.CurrentCards[1].PictureBox.Visible = false;
            this.Hand.CurrentCards[1].PictureBox.Update();
            this.Controls["StatusBox"].Text = GlobalConstants.FoldText;
        }

        /// <summary>
        /// All the in.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        public virtual void AllIn(ref int currentHighestBet)
        {
            if (this.Chips > currentHighestBet)
            {
                currentHighestBet = this.Chips;
            }

            this.Controls["StatusBox"].Text = GlobalConstants.AllInText;
            this.IsAllIn = true;
            this.ChipsPlaced += this.Chips;
            this.Chips = 0;
            this.Controls["ChipsBox"].Text = this.Chips.ToString();
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        public virtual void Check()
        {
            this.ResetFlags();
            this.HasChecked = true;
            this.Controls["StatusBox"].Text = GlobalConstants.CheckText;
        }

        /// <summary>
        /// Resets the flags.
        /// </summary>
        public virtual void ResetFlags()
        {
            this.HasCalled = false;
            this.HasChecked = false;
            this.HasRaised = false;
        }

        /// <summary>
        /// Plays the turn.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        /// <param name="playersNotFolded">The players not folded.</param>
        /// <param name="canCheck">if set to <c>true</c> [can check].</param>
        /// <param name="currentPartOfTurn">The current part of turn.</param>
        /// <param name="randomBehavior">The random behavior.</param>
        public abstract void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior);
 
        /// <summary>
        /// Sets the flags for new turn.
        /// </summary>
        public virtual void SetFlagsForNewTurn()
        {
            this.ResetFlags();
            this.ChipsPlaced = 0;
            this.HasFolded = false;
            this.WinsRound = false;
            this.IsAllIn = false;
        }
    }
}
