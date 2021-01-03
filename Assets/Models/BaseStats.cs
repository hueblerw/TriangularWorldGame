using System;

public class BaseStats {

    public static string[] STAT_NAMES = { "strength", "dexterity", "constitution", "intelligence", "wisdom", "charisma" };
    private static Random randy = new Random();

    public int strength;
    public int dexterity;
    public int constitution;
    public int intelligence;
    public int wisdom;
    public int charisma;

    public BaseStats()
    {
        this.strength = randomStat();
        this.dexterity = randomStat();
        this.constitution = randomStat();
        this.intelligence = randomStat();
        this.wisdom = randomStat();
        this.charisma = randomStat();
    }

    private int randomStat()
    {
        int dice1 = randy.Next(1, 7);
        int dice2 = randy.Next(1, 7);
        int dice3 = randy.Next(1, 7);
        int dice4 = randy.Next(1, 13);

        // sum of the averages of 2d6 & 1d6 and 1d12
        int num = (int) (dice1 + dice2 + Math.Round((dice3 + dice4) / 2.0, 0));
        return num;
    }

    public override string ToString()
    {
        string output = "STR: " + strength + "\t";
        output += "DEX: " + dexterity + "\t";
        output += "CON: " + constitution + "\n";
        output += "INT: " + intelligence + "\t";
        output += "WIS: " + wisdom + "\t";
        output += "CHA: " + charisma;
        return output;
    }

}
