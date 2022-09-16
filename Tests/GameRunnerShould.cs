using System;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using Trivia;
using Xunit;

namespace Tests;

[UseReporter(typeof(QuietReporter))]
public class GameRunnerShould
{
    private const int NUM_TESTS = 25;

    [Fact]
    public void PlaysTrivia()
    {
        var runGames = () => Enumerable.Range(1, NUM_TESTS). //
            Select(i => 147621 + 13 * i). //
            Select(seed => new Random(seed)). //
            ToList(). //
            ForEach(random => GameRunner.Run(random));


        var random = new Random(1001);
        var output = Capture.ConsoleOutput(runGames);
        Approvals.Verify(output);
    }
}