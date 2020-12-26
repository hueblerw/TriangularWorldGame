using System;
using UnityEngine;
using System.Collections.Generic;

public class Culture {

    private static System.Random randy = new System.Random();

    // public Government defaultGovernment;
    public string nationality;
    public Dictionary<string, int> skillBonuses;
    public Vector2 defaultPigmentRange;
    public Vector2 defaultHairRange;
    public Vector2 defaultEyeRange;

    public Dictionary<string, int> raceWeights;
    public Dictionary<string, int> classWeights;

    public Culture(string name)
    {
        assignCulture(name);
    }

    public PersonClass randomClass()
    {
        int index = randy.Next(0, 12);
        int current = 0;
        foreach (KeyValuePair<string, int> entry in classWeights)
        {
            current += entry.Value;
            if (index < current)
            {
                return new PersonClass(entry.Key);
            }
        }
        return new PersonClass("civilian");
    }

    public Race randomRace()
    {
        int index = randy.Next(0, 32);
        int current = 0;
        foreach (KeyValuePair<string, int> entry in raceWeights)
        {
            current += entry.Value;
            if (index < current)
            {
                return new Race(entry.Key);
            }
        }
        return new Race("human");
    }

    public int randomPigment()
    {
        return randy.Next((int)defaultPigmentRange.x, (int)defaultPigmentRange.y);
    }

    public int randomHair()
    {
        return randy.Next((int)defaultHairRange.x, (int)defaultHairRange.y);
    }

    public int randomEyes()
    {
        return randy.Next((int)defaultEyeRange.x, (int)defaultEyeRange.y);
    }

    private void assignCulture(string name)
    {
        switch (name)
        {
            case "Jerkland":
                this.nationality = "Jerkish";
                this.skillBonuses = new Dictionary<string, int>();
                skillBonuses["intimidation"] = 2;
                skillBonuses["labor"] = 1;
                defaultPigmentRange = new Vector2(21, 80);
                defaultHairRange = new Vector2(1, 100);
                defaultEyeRange = new Vector2(1, 100);
                raceWeights = new Dictionary<string, int>();
                raceWeights.Add("elf", 2);
                // raceWeights.Add("dragonborn", 2);
                // raceWeights.Add("goblin", 2);
                // raceWeights.Add("half-orc", 1);
                raceWeights.Add("half-elf", 1);
                classWeights = new Dictionary<string, int>();
                classWeights.Add("fighter", 1);
                classWeights.Add("paladin", 1);
                classWeights.Add("barbarian", 1);
                classWeights.Add("cleric", 1);
                // defaultGovernment = new Government("fuedal");
                break;
            case "Mishegoshes":
                this.nationality = "Mishegoshian";
                this.skillBonuses = new Dictionary<string, int>();
                skillBonuses["trade"] = 2;
                skillBonuses["materials"] = 1;
                defaultPigmentRange = new Vector2(1, 50);
                defaultHairRange = new Vector2(1, 100);
                defaultEyeRange = new Vector2(1, 100);
                raceWeights = new Dictionary<string, int>();
                raceWeights.Add("elf", 3);
                raceWeights.Add("dwarf", 2);
                raceWeights.Add("half-elf", 1);
                classWeights = new Dictionary<string, int>();
                classWeights.Add("fighter", 1);
                classWeights.Add("rogue", 1);
                classWeights.Add("ranger", 1);
                classWeights.Add("monk", 1);
                // defaultGovernment = new Government("democracy");
                break;
            default:
                throw new Exception("Culture not found!");
        }
    }

}
