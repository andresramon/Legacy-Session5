using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool notAWinner;

        public static void Main(String[] args)
        {
            Random rand = new Random();
            Run(rand);
        }

        public static void Run(Random rand)
        {
            Game aGame = new Game();

            aGame.AddPlayer("Chet");
            aGame.AddPlayer("Pat");
            aGame.AddPlayer("Sue");

            do
            {

                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    notAWinner = aGame.WasCorrectlyAnswered();
                }



            } while (notAWinner);

        }
    }
}