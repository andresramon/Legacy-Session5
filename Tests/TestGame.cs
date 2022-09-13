using Trivia;
using Xunit;

namespace Tests
{
    public class TestGame
    {
        [Theory]
        [InlineData(2, true )]
        [InlineData(1, false)]
        public void GameReadyToStart(int playersNumber, bool expectedReadyToStart)
        {
            var game = SetupGame(playersNumber);
            Assert.Equal(expectedReadyToStart,game.IsPlayable());
        }

        [Fact]
        public void CheckWrongAnswer()
        {
            var game = SetupGame(2);
            game.WrongAnswer();
            Assert.Equal("Question was incorrectly answered",game.consoleText[4]);
            Assert.Equal("0 was sent to the penalty box",game.consoleText[5]);
        }

        private static TestableGame SetupGame(int numPlayer)
        {
            TestableGame game = new TestableGame();
            for (int iPlayer = 0; iPlayer < numPlayer; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }

            return game;
        }

        [Fact]
        public void CheckWrongAnswerOfLastPlayerGoesBackToFirstPlayer()
        {
            var game = SetupGame(2);
            game.WrongAnswer();
            game.WrongAnswer();
            game.ClearConsoleText();
            game.WrongAnswer();
            Assert.Equal("0 was sent to the penalty box",game.consoleText[1]);
        }

        [Fact]
        public void ValidatePlayerAdd()
        {
            TestableGame game = new TestableGame();
            game.Add("Test");
            Assert.Equal("Test was added",game.consoleText[0]);
            Assert.Equal("They are player number 1",game.consoleText[1]);
        }

        [Fact]
        public void CheckCorrectAnsweredPlayerNotInPenaltyBox()
        {
            var game = SetupGame(2);
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("Answer was corrent!!!!",game.consoleText[0]);
        }
        
        [Fact]
        public void PlayerEarnsACoinAfterCorrectAnswer()
        {
            var game = SetupGame(2);
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("0 now has 1 Gold Coins.",game.consoleText[1]);
        }

        [Fact]
        public void CheckIfPlayerWinOnlyWhenHeHasSixCoins()
        {
            var game = SetupGame(2);

            for (int answer = 0; answer < 10; answer++)
            {
                Assert.True(game.WasCorrectlyAnswered());
            }
            Assert.False(game.WasCorrectlyAnswered());
        }

        [Fact]
        public void CheckCorrectAnswerOfLastPlayerGoesBackToFirstPlayer()
        {
            var game = SetupGame(2);
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("0 now has 2 Gold Coins.",game.consoleText[1]);
        }

        [Fact]
        public void CheckIfPlayerIsInPenaltyBoxOnlyGoToNextPlayer()
        {
            var game = SetupGame(2);
            game.SetPlayerInPenaltyBox(0);
            game.WasCorrectlyAnswered();
            Assert.Equal(1,game.GetCurrentPlayer());
        }

    }
}