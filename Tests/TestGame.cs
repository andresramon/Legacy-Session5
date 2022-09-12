using Trivia;
using Xunit;

namespace Test
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
            Assert.Equal("Question was incorrectly answered. 0 was sent to the penalty box. ",game.consoleText);
        }
    }
}