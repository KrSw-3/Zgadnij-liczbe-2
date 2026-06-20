
namespace Zgadnij_liczbę_2;

public class HallOfFame 
{

    private List<PlayerRecord> records = new();

    public void addRecord(PlayerRecord record)
    {
        records.Add(record);
        save();
    }

    public void clearRecords()
    {
        records.Clear();
        save();
    }

    public List<PlayerRecord> getTop5(int difficulty)
    {
        return records
       .Where(r => r.difficultyLevel == difficulty && !string.IsNullOrWhiteSpace(r.playerName))
       .OrderBy(r => r.attempts)
       .ThenBy(r => r.timeInSeconds)
       .Take(5)
       .ToList();
    }
    public bool hasAnyRecords()
    {
        return records.Count > 0;
    }
    public void save()
    {
        using StreamWriter writer = new StreamWriter("halloffame.txt");

        foreach (PlayerRecord record in records)
        {
            writer.WriteLine(
                $"{record.playerName};{record.attempts};{record.timeInSeconds};{record.difficultyLevel};{record.isNewGamePlus}");
        }
    }

    public void load()
    {
        records.Clear();

        if (!File.Exists("halloffame.txt"))
            return;

        foreach (string line in File.ReadLines("halloffame.txt"))
        {
            string[] parts = line.Split(';');

            PlayerRecord record = new PlayerRecord();
            record.playerName = parts[0];
            record.attempts = int.Parse(parts[1]);
            record.timeInSeconds = int.Parse(parts[2]);
            record.difficultyLevel = int.Parse(parts[3]);
            record.isNewGamePlus = bool.Parse(parts[4]);

            records.Add(record);
        }
    }
}
