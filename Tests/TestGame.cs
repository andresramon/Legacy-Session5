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
            var game = TestableGame.SetupGame(playersNumber);
            Assert.Equal(expectedReadyToStart,game.IsPlayable());
        }

        [Fact]
        public void CheckWrongAnswer()
        {
            var game = TestableGame.SetupGame(2);
            game.WrongAnswer();
            Assert.Equal("Question was incorrectly answered",game.consoleText[4]);
            Assert.Equal("0 was sent to the penalty box",game.consoleText[5]);
        }

        [Fact]
        public void CheckWrongAnswerOfLastPlayerGoesBackToFirstPlayer()
        {
            var game = TestableGame.SetupGame(2);
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
            var game = TestableGame.SetupGame(2);
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("Answer was correct!!!!",game.consoleText[0]);
        }
        
        [Fact]
        public void PlayerIfNotInPenaltyBoxEarnsACoinAfterCorrectAnswer()
        {
            var game = TestableGame.SetupGame(2);
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("0 now has 1 Gold Coins.",game.consoleText[1]);
        }

        [Fact]
        public void CheckIfPlayerNotInPenaltyBoxWinOnlyWhenHeHasSixCoins()
        {
            var game = TestableGame.SetupGame(2);

            for (int answer = 0; answer < 10; answer++)
            {
                Assert.True(game.WasCorrectlyAnswered());
            }
            Assert.False(game.WasCorrectlyAnswered());
        }

        [Fact]
        public void CheckCorrectAnswerOfLastPlayerGoesBackToFirstPlayer()
        {
            var game = TestableGame.SetupGame(2);
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("0 now has 2 Gold Coins.",game.consoleText[1]);
        }

        [Fact]
        public void CheckIfPlayerIsInPenaltyBoxOnlyGoToNextPlayer()
        {
            var game = TestableGame.SetupGame(2);
            game.SetPlayerInPenaltyBox(0);
            game.WasCorrectlyAnswered();
            Assert.Equal(1,game.GetCurrentPlayer());
        }

        [Fact]
        public void CheckIfLastPlayerIsInPenaltyBoxGoToFirstPlayer()
        {
            var game = TestableGame.SetupGame(2, true, false);
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            Assert.Equal(0, game.GetCurrentPlayer());
        }

        [Fact]
        public void CheckIfPlayerGettingOutOfPenaltyBoxAnswerIsCorrect()
        {
            var game = TestableGame.SetupGame(2, true, true);
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("Answer was correct!!!!", game.consoleText[0]);
        }

        [Fact]
        public void PlayerGettingOutOfPenaltyBoxEarnsACoinAfterCorrectAnswer()
        {
            var game = TestableGame.SetupGame(2, true, true);
            game.ClearConsoleText();
            game.WasCorrectlyAnswered();
            Assert.Equal("0 now has 1 Gold Coins.", game.consoleText[1]);
        }

        [Fact]
        public void PlayerGettingOutOfPenaltyBoxWinOnlyWhenHeHasSixCoins()
        {
            var game = TestableGame.SetupGame(2, true, true);

            for (int answer = 0; answer < 10; answer++)
            {
                Assert.True(game.WasCorrectlyAnswered());
            }

            Assert.False(game.WasCorrectlyAnswered());
        }

        [Fact]
        public void CheckIfLastPlayerGettingOutOfPenaltyBoxGoToFirstPlayer()
        {
            var game = TestableGame.SetupGame(2, true, true);
            game.WasCorrectlyAnswered();
            game.WasCorrectlyAnswered();
            Assert.Equal(0, game.GetCurrentPlayer());
        }

        [Theory]
        [InlineData(1, 1 )]
        [InlineData(12, 0)]
        [InlineData(14, 2)]
        public void RollMovesPlayerToNewPosition(int roll, int expectedLocation)
        {
            var game = TestableGame.SetupGame(2);
            game.ClearConsoleText();
            game.Roll(roll);
            Assert.Equal("0's new location is "+expectedLocation, game.consoleText[2]);
        }

        [Fact]
        public void ValidateRollAndPlayerWhenRolling()
        {
            var game = TestableGame.SetupGame(2);
            game.ClearConsoleText();
            game.Roll(1);
            Assert.Equal("0 is the current player", game.consoleText[0]);
            Assert.Equal("They have rolled a 1", game.consoleText[1]);
        }

        [Fact]
        public void RollWhenPlayerIsInPenaltyBoxAndRollEven()
        {
            var game = TestableGame.SetupGame(2,true,false);
            game.ClearConsoleText();
            game.Roll(2);
            Assert.Equal("0 is not getting out of the penalty box",game.consoleText[2]);
            Assert.False(game.GetIsGettingOutOfPenaltyBox());
        }
        
        [Fact]
        public void CheckPlayerInPenaltyBoxRollOddAndIsGettingOutOfPenaltyBox()
        {
            var game = TestableGame.SetupGame(2,true,false);
            game.ClearConsoleText();
            game.Roll(3);
            Assert.Equal("0 is getting out of the penalty box",game.consoleText[2]);
            Assert.True(game.GetIsGettingOutOfPenaltyBox());
        }
        
        [Theory]
        [InlineData(1, 1 )]
        [InlineData(13, 1)]
        public void RollMovesPlayerInPenaltyBoxToNewPositionWhenRollIsOdd(int roll, int expectedLocation)
        {
            var game = TestableGame.SetupGame(2,true,false);
            game.ClearConsoleText();
            game.Roll(roll);
            Assert.Equal("0's new location is "+expectedLocation, game.consoleText[3]);
        }

        [Theory]
        [InlineData(true, 1,"Science")]
        [InlineData(true, 5,"Science")]
        [InlineData(true, 9,"Science")]
        [InlineData(true, 3,"Rock")]
        [InlineData(true, 7,"Rock")]
        [InlineData(true, 11,"Rock")]
        [InlineData(false, 0,"Pop")]
        [InlineData(false, 4,"Pop")]
        [InlineData(false, 8,"Pop")]
        [InlineData(false, 12,"Pop")]
        [InlineData(false, 1,"Science")]
        [InlineData(false, 5,"Science")]
        [InlineData(false, 9,"Science")]
        [InlineData(false, 2,"Sports")]
        [InlineData(false, 6,"Sports")]
        [InlineData(false, 10,"Sports")]
        [InlineData(false, 3,"Rock")]
        [InlineData(false, 7,"Rock")]
        [InlineData(false, 11,"Rock")]
        
        public void CheckCategoryDependingPlayerLocation(bool isInPenaltyBox, int roll, string expectedCategory)
        {
            var game = TestableGame.SetupGame(2,isInPenaltyBox,false);
            game.ClearConsoleText();
            game.Roll(roll);
            if (isInPenaltyBox)
            {
                Assert.Equal("The category is " + expectedCategory, game.consoleText[4]);
            }
            else
            {
                Assert.Equal("The category is " + expectedCategory, game.consoleText[3]);
            }
        }
        
        [Theory]
        [InlineData(true, 1, "Science Question 0")]
        [InlineData(true, 3,"Rock Question 0")]
        [InlineData(false, 0,"Pop Question 0")]
        [InlineData(false, 2,"Sports Question 0")]
        public void CheckCategoryQuestionDependingOnPlayerLocation(bool isInPenaltyBox, int roll, string expectedCategoryQuestion)
        {
            var game = TestableGame.SetupGame(2,isInPenaltyBox,false);
            game.ClearConsoleText();
            game.Roll(roll);
            if (isInPenaltyBox)
            {
                
                Assert.Equal(expectedCategoryQuestion, game.consoleText[5]);
            }
            else
            {
                Assert.Equal(expectedCategoryQuestion, game.consoleText[4]);
            }
        }
        
        [Theory]
        [InlineData(false, 1, 4, "Science Question 1")]
        [InlineData(false, 3, 4,"Rock Question 1")]
        [InlineData(false, 4, 4,"Pop Question 1")]
        [InlineData(false, 2, 4,"Sports Question 1")]
        public void CheckCategoryQuestionRemovalAfterRoll(bool isInPenaltyBox, int roll, int roll2, string expectedCategoryQuestion)
        {
            var game = TestableGame.SetupGame(2,isInPenaltyBox,false);
            game.Roll(roll);
            game.ClearConsoleText();
            
            game.Roll(roll2);
            Assert.Equal(expectedCategoryQuestion, game.consoleText[4]);
           
        }
    }
}