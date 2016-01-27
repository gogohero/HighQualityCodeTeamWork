// *******************************************************************************
// Assembly         : Poker
// Created          : 01-24-2016
// *******************************************************************************
// <copyright file="IParticipant.cs" company="Date">Copyright ©  2015</copyright>
// *******************************************************************************
namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Interface IParticipant
    /// </summary>
    public interface IParticipant
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the chips.
        /// </summary>
        /// <value>The chips.</value>
        int Chips { get; set; }

        /// <summary>
        /// Gets or sets the chips placed.
        /// </summary>
        /// <value>The chips placed.</value>
        int ChipsPlaced { get; set; }

        /// <summary>
        /// Gets or sets the previously called.
        /// </summary>
        /// <value>The previously called.</value>
        int PreviouslyCalled { get; set; }

        /// <summary>
        /// Gets or sets the hand.
        /// </summary>
        /// <value>The hand.</value>
        IHand Hand { get; set; }

        /// <summary>
        /// Gets or sets the controls.
        /// </summary>
        /// <value>The controls.</value>
        Dictionary<string, Control> Controls { get; set; }

        /// <summary>
        /// Gets or sets the place on board.
        /// </summary>
        /// <value>The place on board.</value>
        Point PlaceOnBoard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is all in.
        /// </summary>
        /// <value><c>true</c> if this instance is all in; otherwise, <c>false</c>.</value>
        bool IsAllIn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has folded.
        /// </summary>
        /// <value><c>true</c> if this instance has folded; otherwise, <c>false</c>.</value>
        bool HasFolded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has checked.
        /// </summary>
        /// <value><c>true</c> if this instance has checked; otherwise, <c>false</c>.</value>
        bool HasChecked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has raised.
        /// </summary>
        /// <value><c>true</c> if this instance has raised; otherwise, <c>false</c>.</value>
        bool HasRaised { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has called.
        /// </summary>
        /// <value><c>true</c> if this instance has called; otherwise, <c>false</c>.</value>
        bool HasCalled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [wins round].
        /// </summary>
        /// <value><c>true</c> if [wins round]; otherwise, <c>false</c>.</value>
        bool WinsRound { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is in game.
        /// </summary>
        /// <value><c>true</c> if this instance is in game; otherwise, <c>false</c>.</value>
        bool IsInGame { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has acted.
        /// </summary>
        /// <value><c>true</c> if this instance has acted; otherwise, <c>false</c>.</value>
        bool HasActed { get; }

        /// <summary>
        /// Resets the flags.
        /// </summary>
        void ResetFlags();

        /// <summary>
        /// Sets the flags for new turn.
        /// </summary>
        void SetFlagsForNewTurn();

        /// <summary>
        /// Checks this instance.
        /// </summary>
        void Check();

        /// <summary>
        /// Calls the specified current highest bet.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        void Call(int currentHighestBet);

        /// <summary>
        /// Raises the specified raise amount.
        /// </summary>
        /// <param name="raiseAmount">The raise amount.</param>
        /// <param name="currentHighestBet">The current highest bet.</param>
        void Raise(int raiseAmount, ref int currentHighestBet);

        /// <summary>
        /// Folds this instance.
        /// </summary>
        void Fold();

        /// <summary>
        /// All the in.
        /// </summary>
        /// <param name="currentHighestBet">The current highest bet.</param>
        void AllIn(ref int currentHighestBet);
    }
}