using System.Diagnostics;

namespace Zgadnij_liczbę_2;

public class NewGamePlus : GameSession
{

    private string lang;
    public NewGamePlus(int difficulty, UI ui, string lang)
    {
        this.difficulty = difficulty;
        this.ui = ui;
        this.lang = lang;
    }
    public override PlayerRecord play()
    {
        generateNumber();
        bool loop = true;
        this.currentAttempt = 0;
        Console.Clear();
        Stopwatch time = Stopwatch.StartNew();
        while (loop)
        {
            reshuffleNumber();
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
        }
        Console.Clear();
        time.Stop();
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
            playerRecord1.isNewGamePlus = true;
            return playerRecord1;

    }

    private void reshuffleNumber()
    {
        if(this.currentAttempt != 0 && this.currentAttempt % 7 == 0)
        {
            generateNumber();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            if (lang == "Polish")
                ui.displayMessage("!!!Przelosowano liczbę!!!");
            else
                ui.displayMessage("!!!Reloaded number!!!");
            Console.ResetColor();
        }
    }
}
