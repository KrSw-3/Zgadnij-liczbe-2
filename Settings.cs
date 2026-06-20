namespace Zgadnij_liczbę_2;

using System.Xml.Serialization;

public class Settings
{
    public string currentLanguage = "Polish";
    public bool askForBetMode = true;

    public void save()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Settings));

        using FileStream stream = new FileStream("settings.xml", FileMode.Create);

        serializer.Serialize(stream, this);
    }

    public void load()
    {
        if (!File.Exists("settings.xml"))
        {
            save();
            return;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(Settings));

        using FileStream stream = new FileStream("settings.xml",FileMode.Open);

        Settings loadedSettings = (Settings)serializer.Deserialize(stream)!;

        currentLanguage = loadedSettings.currentLanguage;
        askForBetMode = loadedSettings.askForBetMode;
       
    }

}
