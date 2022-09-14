using System.Collections;
using System.Collections.Generic;
using Trivia;

namespace Tests
{

    public class TestableGame : Game
    {
        public List<string> consoleText = new List<string>();

        protected override void DisplayLine(string text)
        {
            consoleText.Add(text);
        }
        
        public void ClearConsoleText()
        {
            consoleText.Clear();
        }

        public void SetPlayerInPenaltyBox(int numPlayer)
        {
            _inPenaltyBox[numPlayer] = true;
        }

        public void SetIsGettingOutOfPenaltyBox(bool value)
        {
            _isGettingOutOfPenaltyBox = value;
        }

        public int GetCurrentPlayer()
        {
            return _currentPlayer;
        }

        public static TestableGame SetupGame(int numPlayer)
        {
            TestableGame game = new TestableGame();
            for (int iPlayer = 0; iPlayer < numPlayer; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }

            return game;
        }

        public static TestableGame SetupGame(int numPlayer, 
            bool allPlayersInPenaltyBox, bool isGettinOutOfPenaltyBox)
        {
            TestableGame game = SetupGame(numPlayer);

            if (allPlayersInPenaltyBox)
            {
                for (int iPlayer = 0; iPlayer < numPlayer; iPlayer++)
                {
                    game.SetPlayerInPenaltyBox(iPlayer);
                }
            }

            game.SetIsGettingOutOfPenaltyBox(isGettinOutOfPenaltyBox);
            return game;
        }
    }
}