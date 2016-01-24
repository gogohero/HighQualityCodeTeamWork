namespace Poker.Constants
{
    using System.IO;

    using Poker.Interfaces;
    using Poker.TestingAlgorithms;

    public static class GlobalConstants
    {
        private static ICard[] cards = new ICard[52];

        public static IDeck Deck = new Deck(cards);

        public static int StartingChips = 10000;

        public static string[] allGameCardsImagesWithLocationsCollection = 
            Directory.GetFiles(@"..\..\..\Poker\Resources\Assets\Cards\", "*.png", SearchOption.TopDirectoryOnly);

        private static void InitializeCards()
        {

        }
    }
}