﻿namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public interface IParticipant
    {
        string Name { get; set; }

        int Chips { get; set; }

        IHand Hand { get; set; }

        Dictionary<string, Control> Controls { get; set; }

        Point PlaceOnBoard { get; set; }

        bool Turn { get; set; }

        bool HasActed { get; }

        bool HasFolded { get; set; }

        bool HasChecked { get; set; }

        bool HasRaised { get; set; }

        bool HasCalled { get; set; }

        bool WinsRound { get; set; }

        bool IsInGame { get; }

        void ResetFlags();

        void PlayTurn();

        void SetFlagsForNewTurn();

        void Call(int callAmount);

        void Raise(int raiseAmount);
    }
}