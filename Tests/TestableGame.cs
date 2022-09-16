using System.Collections;
using System.Collections.Generic;
using Trivia;

namespace Tests
{

    public class TestableGame : Game
    {
        public TestableOutputInfoGame outputDevice;
        public new QuestionsPackage questions;

        public TestableGame()
        {
            outputDevice = new TestableOutputInfoGame();
            questions = new QuestionsPackage(outputDevice);
        }

        protected override void DisplayLine(string text)
        {
            outputDevice.DisplayLine(text);
        }
        
        public void ClearConsoleText()
        {
            outputDevice.ClearConsoleText();
        }

        public void SetIsGettingOutOfPenaltyBox(bool value)
        {
            _isGettingOutOfPenaltyBox = value;
        }

        public int GetCurrentPlayer()
        {
            return _currentPlayer;
        }
        public bool GetIsGettingOutOfPenaltyBox()
        {
            return _isGettingOutOfPenaltyBox;
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

        public new void SetPlayerInPenaltyBox(int numPlayer)
        {
            base.SetPlayerInPenaltyBox(numPlayer);
        }
    }
}