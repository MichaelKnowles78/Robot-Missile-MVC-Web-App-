using NUnit.Framework;
using RobotMissile.Models;

namespace RobotMissileTest;

[TestFixture]
[Category("Models")]
class GameStateShould
{
    private GameState state;

    [OneTimeSetUp]
    public void CreateGameState()
    {
        state = new()
        {
            Code = 'A'
        };
    }

    [Test]
    public void EndOnCorrectGuess()
    {
        state.ProcessGuess(state.Code);
        Assert.That(state.GameEnded, Is.True);
    }

    [Test]
    public void EndAfterFourIncorrectGuesses()
    {
        state.ProcessGuess('E');
        Assert.That(state.GameEnded, Is.False);
        state.ProcessGuess('D');
        Assert.That(state.GameEnded, Is.False);
        state.ProcessGuess('C');
        Assert.That(state.GameEnded, Is.False);
        state.ProcessGuess('B');
        Assert.That(state.GameEnded, Is.True);
    }

    [Test]
    public void NotAllowACorrectGuessAfterFourGuessesAreDepleted()
    {
        state.ProcessGuess('E');
        state.ProcessGuess('D');
        state.ProcessGuess('C');
        state.ProcessGuess('B');
        state.ProcessGuess('A');
        Assert.That(state.StatusMessage, Is.Not.EqualTo("TICK...FZZZZ...CLICK...\nYou did it!"));
    }

}
