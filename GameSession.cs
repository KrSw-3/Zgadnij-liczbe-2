namespace Zgadnij_liczbę_2;

public abstract class GameSession
{
    protected int difficulty;
    protected int maxAttempts;
    protected int currentAttempt;
    protected int targetNumber;
    protected DateTime startTime;
    protected UI ui;

    public abstract PlayerRecord play();

    protected void generateNumber()
    {
         Random rand = new();
        if(this.difficulty==1)
        {
            this.targetNumber = rand.Next(1,51);
        }
        else if(this.difficulty==2)
        {
            this.targetNumber = rand.Next(1,101);
        }
        else if (this.difficulty==3) 
        {
            this.targetNumber = rand.Next(1,251);
        }
    }

    protected bool checkGuess(int guess)
    {
        if(guess == this.targetNumber)
            return true;
        else
            return false;
    }
}
