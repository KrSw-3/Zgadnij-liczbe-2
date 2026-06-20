namespace Zgadnij_liczbę_2;

public class GameManager
{
	public Settings settings = new();
	private HallOfFame hallOfFame = new();
	private UI ui = new();

	public void run()
	{
        settings.load();
        hallOfFame.load();
        showWelcomeScreen();
	}

	private void showWelcomeScreen()
	{
        bool loop = true;
        while (loop)
        {
            Console.Clear();
            if (settings.currentLanguage == "Polish")
                ui.displayMessage(ui.msg["plSelectOption"]);
            else
                ui.displayMessage(ui.msg["enSelectOption"]);
            int opt = ui.readInt("> ");

            switch (opt)
            {
                case 1:
                    startNewGame();
                    break;
                case 2:
                    int diff = 1;
                    if (settings.currentLanguage == "Polish")
                    {
                        diff = ui.readInt(ui.msg["plDifficulty"]);
                    }
                    else
                    {
                        diff = ui.readInt(ui.msg["enDifficulty"]);
                    }
                    List<PlayerRecord> top = hallOfFame.getTop5(diff);
                    ui.showTop5(top,settings.currentLanguage);
                    break;
                case 3:
                    showSettings();
                    break;
                case 4:
                    loop = false;
                    break;
                default:
                    if (settings.currentLanguage == "Polish")
                        ui.displayMessage(ui.msg["plNonexistant"]);
                    else
                        ui.displayMessage(ui.msg["enNonexistant"]);
                    break;

            }
        }
    }

	private void showSettings()
	{
        bool loop = true;
        while (loop)
        {
            Console.Clear();

            if (settings.currentLanguage == "Polish")
            {
                ui.displayMessage($"Aktualny język: {settings.currentLanguage} Pytaj o tryb zakładu: {settings.askForBetMode}\n");
                ui.displayMessage( ui.msg["plListSet"]);
            }
            else
            {
                ui.displayMessage($"Current language: {settings.currentLanguage} Ask for bet mode: {settings.askForBetMode}\n");
                ui.displayMessage(ui.msg["enListSet"]);
            }
            int setOpt = ui.readInt("> ");
            switch (setOpt)
            {
                case 1:
                    string lang = ui.readString(ui.msg["LangOptions"]);
                    if (lang == "Polish" || lang == "English")
                    {
                        settings.currentLanguage = lang;
                        settings.save();
                    }
                    else
                    {
                        if (settings.currentLanguage == "Polish")
                            ui.displayMessage(ui.msg["plNonexistant"]);
                        else
                            ui.displayMessage(ui.msg["enNonexistant"]);
                    }
                    break;
                case 2:
                    if (settings.currentLanguage == "Polish")
                        settings.askForBetMode = ui.readInt(ui.msg["plYN"]) == 1;
                    else
                        settings.askForBetMode = ui.readInt(ui.msg["enYN"]) == 1;
                    settings.save();
                    break;
                case 3:
                    if(settings.currentLanguage == "Polish")
                    {
                        ui.displayMessage(ui.msg["plClearRecords"]);
                    }
                    else
                    {
                        ui.displayMessage(ui.msg["enClearRecords"]);
                    }
					if(ui.readString("> ")=="Y")
					{
                    hallOfFame.clearRecords();
                    hallOfFame.save();
					}
					break;
                case 4:
                    loop = false;
                    break;
                default:
                    if (settings.currentLanguage == "Polish")
                        ui.displayMessage(ui.msg["plNonexistant"]);
                    else
                        ui.displayMessage(ui.msg["enNonexistant"]);
                    break;
            }
        }
	}  

	private void startNewGame()
	{
        bool loop = true;
        while (loop)
        {
            int diff = 1;
            Console.Clear();
            if (settings.currentLanguage == "Polish")
                ui.displayMessage(ui.msg["plStartNewGame"]);
            else
                ui.displayMessage(ui.msg["enStartNewGame"]);
            int gameOpt = ui.readInt("> ");
            if (settings.currentLanguage == "Polish" && gameOpt != 3)
            {
                ui.displayMessage(ui.msg["plDifficultyNewGame"]);
                diff = ui.readInt("> ");
            }
            else if (settings.currentLanguage == "English" && gameOpt != 3)
            {
                ui.displayMessage(ui.msg["enDifficultyNewGame"]);
                diff = ui.readInt("> ");
            }
            switch (gameOpt)
            {
                case 1:
                    int maxAttempts = 0;
                    if (settings.currentLanguage == "Polish" && settings.askForBetMode == true)
                    {
                        ui.displayMessage(ui.msg["plMaxWage"]);
                        maxAttempts = ui.readInt("> ");
                    }
                    else if (settings.currentLanguage == "English" && settings.askForBetMode == true)
                    {
                        ui.displayMessage(ui.msg["enMaxWage"]);
                        maxAttempts = ui.readInt("> ");
                    }
                    StandardGame standardgame = new(diff, maxAttempts, ui,settings.currentLanguage);
                    hallOfFame.addRecord(standardgame.play());
                    hallOfFame.save();
                    break;
                case 2:
                    NewGamePlus newgameplus = new(diff, ui,settings.currentLanguage);
                    hallOfFame.addRecord(newgameplus.play());
                    hallOfFame.save();
                    break;
                case 3:
                    loop = false;
                    break;
                default:
                    if (settings.currentLanguage == "Polish")
                        ui.displayMessage(ui.msg["plNonexistant"]);
                    else
                        ui.displayMessage(ui.msg["enNonexistant"]);
                    break;

            }
        }


    }
}
