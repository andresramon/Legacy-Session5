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
            Game game = new Game();
            for (int iPlayer = 0; iPlayer < playersNumber; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }
            Assert.Equal(expectedReadyToStart,game.IsPlayable());
        }

        [Fact]
        public void CheckWrongAnswer()
        {
            TestableGame game = new TestableGame();
            for (int iPlayer = 0; iPlayer < 2; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }
            game.WrongAnswer();
            Assert.Equal("Question was incorrectly answered",game.consoleText[4]);
            Assert.Equal("0 was sent to the penalty box",game.consoleText[5]);
        }

        [Fact]
        public void CheckWrongAnswerOfLastPlayerGoesBackToFirstPlayer()
        {
            TestableGame game = new TestableGame();
            for (int iPlayer = 0; iPlayer < 2; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }
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
            TestableGame game = new TestableGame();
            for (int iPlayer = 0; iPlayer < 2; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("Answer was corrent!!!!",game.consoleText[0]);
        }
        
        [Fact]
        public void PlayerEarnsACoinAfterCorrectAnswer()
        {
            TestableGame game = new TestableGame();
            for (int iPlayer = 0; iPlayer < 2; iPlayer++)
            {
                game.Add(iPlayer.ToString());
            }
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("0 now has 1 Gold Coins.",game.consoleText[1]);
        }

    }
}