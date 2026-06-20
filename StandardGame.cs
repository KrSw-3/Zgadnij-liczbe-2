using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Zgadnij_liczbę_2;

public class StandardGame : GameSession 
{

    private string lang;
    public StandardGame(int difficulty, int maxAttempts, UI ui, string lang)
    {
        this.difficulty = difficulty;
        this.maxAttempts = maxAttempts;
        this.ui = ui;
        this.lang = lang;
    }
    public override PlayerRecord play()
    {
        generateNumber();
        bool loop = true;
        this.currentAttempt = 0;
        bool wonWager = true;
        Console.Clear();
        Stopwatch time = Stopwatch.StartNew();
        while (loop)
        {
            if (lang == "Polish")
            {
                ui.displayMessage($"Dotychczasowa liczba prób: {this.currentAttempt}");
                ui.displayMessage("Podaj liczbe:\n");
            }
            else
            {
                ui.displayMessage($"Current tries: {this.currentAttempt}");
                ui.displayMessage("Input a number:\n");
            }
            int guess = (ui.readInt("> "));
            if (checkGuess(guess))
            {
                loop = false;
            }
            bool isLower = true;
            if (guess > targetNumber)
            {
                isLower = false;
            }
            if (loop)
                ui.displayMessage(ui.getRandomFeedback(isLower,lang));
            currentAttempt++;
            if (maxAttempts != 0 && maxAttempts <= currentAttempt && loop)
            {
                loop = false;
                wonWager = false;
            }
        }
        
        time.Stop();
        Console.Clear();
        if (wonWager)
        {
            string name = "";
            if (lang == "Polish")
            {
                ui.displayMessage($"Brawo, odgadłeś prawidłową liczbę przy {currentAttempt} próbie.\n");
                name = ui.readString("Podaj swoje imię\n> ");
            }
            else
            {
                ui.displayMessage($"Congratulations, you've guessed the correct number after {currentAttempt} tries.\n");
                name = ui.readString("Submit your name\n> ");
            }
            PlayerRecord playerRecord1 = new();
            playerRecord1.playerName = name;
            playerRecord1.attempts = this.currentAttempt;
            playerRecord1.difficultyLevel = this.difficulty;
            playerRecord1.timeInSeconds = (int)time.Elapsed.TotalSeconds;
            return playerRecord1;
        }
        else
        {
            if (lang == "Polish")
                ui.displayMessage("Nie udało ci się odgadnąć w ustalonej liczbie prób\nNanciśnij ENTER aby przejść dalej");
            else
                ui.displayMessage("You haven't guessed the number in a chosen tries count\nPress ENTER to continue");
            Console.ReadKey();
            PlayerRecord playerRecord1 = new();
            return playerRecord1;
        }
        
    }

  
}
