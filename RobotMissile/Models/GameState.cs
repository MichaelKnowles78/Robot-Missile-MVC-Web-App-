using System;

namespace RobotMissile.Models
{
    public class GameState
    {

        public Guid GameStateId { get; set; }
        public char Code { get; set; }
        public int Guesses { get; set; }
        public string StatusMessage { get; set; }
        public bool GameEnded {get; set; }

        public GameState()
        {
            GameStateId = Guid.NewGuid();
            Code = Convert.ToChar(new Random().Next(65, 90));
            Guesses = 4;

            StatusMessage = "Type the correct code\nletter (A-Z) to\ndefuse the missle.\nYou have 4 chances\n";
        }

        public void ProcessGuess(char guess)
        {
            if (!GameEnded)
            {
                bool defused = false;

                if (char.ToUpper(guess) == Code)
                {
                    defused = true;
                }
                else
                {
                    if (char.ToUpper(guess) < Code)
                    {
                        StatusMessage = "Later";
                    }
                    else
                    {
                        StatusMessage = "Earlier";
                    }
                    StatusMessage += " than " + char.ToUpper(guess);
                }

                if (defused)
                {
                    StatusMessage = "TICK...FZZZZ...CLICK...\nYou did it!";
                    GameEnded = true;
                }
                else
                {
                    if (--Guesses == 0)
                    {
                        StatusMessage = "BOOOOOOOOMMM...\nYou blew it!\nThe correct code was " + Code;
                        GameEnded = true;
                    }
                }
            }
        }
    }
}
