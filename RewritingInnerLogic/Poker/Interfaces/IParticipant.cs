namespace Poker.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Poker.Enumerations;

    public interface IParticipant
    {
        string Name { get; set; }

        int Chips { get; set; }

        int ChipsPlaced { get; set; }

        int PreviouslyCalled { get; set; }

        IHand Hand { get; set; }

        Dictionary<string, Control> Controls { get; set; }

        Point PlaceOnBoard { get; set; }

        bool IsAllIn { get; set; }

        bool HasFolded { get; set; }

        bool HasChecked { get; set; }

        bool HasRaised { get; set; }

        bool HasCalled { get; set; }

        bool WinsRound { get; set; }

        bool IsInGame { get; }

        bool HasActed { get; }

        void ResetFlags();

        void PlayTurn(ref int currentHighestBet, int playersNotFolded, bool canCheck, TurnParts currentPartOfTurn, Random randomBehavior);

        void SetFlagsForNewTurn();

        void Check();

        void Call(ref int currentHighestBet);

        void Raise(int raiseAmount, ref int currentHighestBet);

        void Fold();

        void AllIn(ref int currentHighestBet);
    }
}