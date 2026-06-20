namespace Zgadnij_liczbę_2;

public class UI
{
    public Dictionary<string, string> msg = new()
    {
        {"plSelectOption","Wybierz opcję:\n1. Rozpocznij grę\n2. Hall of Fame\n3. Ustawienia\n4. Wyjdź" },
        {"enSelectOption","Select option:\n1. New game\n2. Hall of Fame\n3. Settings\n4. Exit" },
        {"plDifficulty","Podaj poziom trudności (1-3)\n> " },
        {"enDifficulty","Choose difficulty level (1-3)\n> " },
        {"plNonexistant","Wybrałeś nieistniejącą opcję, spróbuj ponownie." },
        {"enNonexistant","Selected option doesn't exist, try again." },
        {"plListSet","1. Wybierz język \n2. Pytaj o tryb zakładu \n3. Wyczyść Hall of Fame \n4.Wyjdź" },
        {"enListSet", "1. Select language \n2. Ask for bet mode \n3. Reset Hall of Fame \n4. Exit"},
        {"LangOptions","Polish/English\n> " },
        {"plYN","1. Tak\n 0. Nie\n> " },
        {"enYN","1. Yes\n 0. No\n> " },
        {"plClearRecords","Czy jesteś pewny? Potwierdź wpisując Y" },
        {"enClearRecords","Are you sure? Confirm by typing Y" },
        {"plStartNewGame","Wybierz opcję:\n1. Nowa gra\n2. Nowa gra plus\n3. Wyjdź" },
        {"enStartNewGame","Select option:\n1. New game\n2. New game plus\n3. Exit" },
        {"plDifficultyNewGame","Wybierz poziom trudności:\n1. Niski (1-50) \n2. Średni (1-100)\n3. Wysoki (1-250)" },
        {"enDifficultyNewGame", "Select difficulty level:\n1. Low (1-50)\n2. Medium (1-100)\n3. High (1-250)"},
        {"plMaxWage","Wybierz maksymalną liczbę prób (0 - brak zakładu)" },
        {"enMaxWage", "Choose maximal tries count (0 - no wage)"}
    };


    private string[] pltextsTooSmall = { "Liczba jest za mała.", "Podaj większą liczbę.", "Spróbuj podać większą liczbę.", "Ta liczba jest mniejsza od prawidłowej.", "Wpisz większą liczbę" };
    private string[] entextsTooSmall = { "Number is too small.", "Input a bigger number.", "Try inputing a bigger number.", "This number is smaller than the correct one.", "Type bigger number" };
    private string[] pltextsTooBig = { "Liczba jest za duża.", "Podaj mniejszą liczbę.", "Spróbuj podać mniejszą liczbę.", "Ta liczba jest większa od prawidłowej.", "Wpisz mniejszą liczbę" };
    private string[] entextsTooBig = { "Number is too big.", "Input a smaller number.", "Try inputing a smaller number.", "This number is bigger than the correct one.", "Type smaller number" };
    public void displayMessage(string messageKey)
    {
        Console.WriteLine(messageKey);
    }

    public int readInt(string prompt)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(),out int value))
        {
            return value;
        }
        return 0;
    }

    public string readString(string prompt)
    {
        
        Console.Write(prompt);
        return Console.ReadLine();
     
    }

    public void showTop5(List<PlayerRecord> records,string lang)
    {
        Console.Clear();
        int i = 1;
        foreach (PlayerRecord record in records)
        {
            if (record.isNewGamePlus)
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            if (lang == "Polish")
            {
                displayMessage(
                    $"{i++}. {record.playerName} | " +
                    $"{record.attempts} prób | " +
                    $"{record.timeInSeconds}s | " +
                    $"Nowa gra plus: {record.isNewGamePlus}");
            }
            else
            {
                displayMessage(
                    $"{i++}. {record.playerName} | " +
                    $"{record.attempts} tries | " +
                    $"{record.timeInSeconds}s | " +
                    $"New game plus: {record.isNewGamePlus}");
            }
            Console.ResetColor();
        }
        Console.Read();
    }


    public string getRandomFeedback(bool isTooSmall,string lang)
    {

        if (isTooSmall)
        {
            Random rand = new();
            if (lang=="Polish")
                return pltextsTooSmall[rand.Next(5)];
            else
                return entextsTooSmall[rand.Next(5)];
        }
        else
        {
            Random rand = new();
            if (lang == "Polish")
                return pltextsTooBig[rand.Next(5)];
            else
                return entextsTooBig[rand.Next(5)];
        }
    }


}
