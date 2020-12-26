using System;
using System.Collections.Generic;

public class Race {

    public string name;
    public string size;
    public int speed;
    public Dictionary<string, int> baseStatsBonuses;
    public Dictionary<string, int> skillBonuses;
    public int baseLanguages;
    public Dictionary<string, int> ageGroups;
    public int averageDeathAge;
    public List<string> proficiencies;
    public string vision;
    public Dictionary<string, string> breedable;

    private static Random randy = new Random();

    public Race(string name)
    {
        assignRace(name);
    }

    private void assignRace(string name)
    {
        switch (name)
        {
            case "human":
                this.name = name;
                this.size = "M";
                this.speed = 6;
                this.baseStatsBonuses = new Dictionary<string, int>();
                baseStatsBonuses["all"] = 1;
                this.skillBonuses = new Dictionary<string, int>();
                this.proficiencies = new List<string>();
                // this.powers = new Dictionary<string, Power>();
                this.ageGroups = new Dictionary<string, int>();
                ageGroups["child"] = 3;
                ageGroups["teen"] = 10;
                ageGroups["adult"] = 16;
                ageGroups["elder"] = 55;
                this.averageDeathAge = 75;
                this.baseLanguages = 2;
                this.vision = "normal";
                this.breedable = new Dictionary<string, string>(); // what you can breed with and the outcome
                breedable[name] = name;
                breedable["elf"] = "half-elf";
                breedable["half-elf"] = "human";
                breedable["orc"] = "half-orc";
                breedable["half-orc"] = "human";
                break;
            case "elf":
                this.name = name;
                this.size = "M";
                this.speed = 7;
                this.baseStatsBonuses = new Dictionary<string, int>();
                baseStatsBonuses["dexterity"] = 2;
                baseStatsBonuses["wisdom"] = 1;
                this.skillBonuses = new Dictionary<string, int>();
                this.proficiencies = new List<string>();
                proficiencies.Add("perception");
                proficiencies.Add("shortbow");
                proficiencies.Add("longbow");
                proficiencies.Add("shortsword");
                proficiencies.Add("longsword");
                // this.powers = new Dictionary<string, Power>();
                // this.powers["fey origin"] = new Power();
                this.ageGroups = new Dictionary<string, int>();
                ageGroups["child"] = 10;
                ageGroups["teen"] = 40;
                ageGroups["adult"] = 100;
                ageGroups["elder"] = 600;
                this.averageDeathAge = 750;
                this.baseLanguages = 1;
                this.vision = "dark";
                this.breedable = new Dictionary<string, string>(); // what you can breed with and the outcome
                breedable[name] = name;
                breedable["human"] = "half-elf";
                breedable["half-elf"] = "elf";
                break;
            case "half-elf":
            case "human-elf":
            case "elf-human":
                this.name = "half-elf";
                this.size = "M";
                this.speed = 6;
                this.baseStatsBonuses = new Dictionary<string, int>();
                baseStatsBonuses["charisma"] = 2;
                chooseRandomStat(1);
                chooseRandomStat(1);
                this.skillBonuses = new Dictionary<string, int>();
                this.proficiencies = new List<string>();
                chooseRandomProficiency();
                chooseRandomProficiency();
                // this.powers = new Dictionary<string, Power>();
                // this.powers["fey origin"] = new Power();
                this.ageGroups = new Dictionary<string, int>();
                ageGroups["child"] = 4;
                ageGroups["teen"] = 12;
                ageGroups["adult"] = 20;
                ageGroups["elder"] = 140;
                this.averageDeathAge = 180;
                this.baseLanguages = 2;
                this.vision = "dark";
                this.breedable = new Dictionary<string, string>(); // what you can breed with and the outcome
                breedable[name] = name;
                breedable["human"] = "human";
                breedable["elf"] = "elf";
                break;
            case "dwarf":
                this.name = name;
                this.size = "M";
                this.speed = 5;
                this.baseStatsBonuses = new Dictionary<string, int>();
                baseStatsBonuses["constitution"] = 2;
                baseStatsBonuses["wisdom"] = 1;
                this.skillBonuses = new Dictionary<string, int>();
                skillBonuses["mining"] = 2;
                skillBonuses["smithing"] = 2;
                skillBonuses["materials"] = 1;
                skillBonuses["labor"] = 1;
                this.proficiencies = new List<string>();
                proficiencies.Add("handaxe");
                proficiencies.Add("battleaxe");
                proficiencies.Add("light hammer");
                proficiencies.Add("warhammer");
                proficiencies.Add("pick");
                // this.powers = new Dictionary<string, Power>();
                // this.powers["dwarven resilience"] = new Power();
                // this.powers["dwarven toughness"] = new Power();
                this.ageGroups = new Dictionary<string, int>();
                ageGroups["child"] = 5;
                ageGroups["teen"] = 20;
                ageGroups["adult"] = 50;
                ageGroups["elder"] = 275;
                this.averageDeathAge = 350;
                this.baseLanguages = 1;
                this.vision = "dark";
                this.breedable = new Dictionary<string, string>(); // what you can breed with and the outcome
                breedable[name] = name;
                break;
            default:
                throw new Exception("Race not found!");
        }
    }

    public int randomAdultAge()
    {
        return (int) Math.Round(randy.NextDouble() * ((averageDeathAge + ageGroups["elder"]) / 2.0 - ageGroups["adult"]) + ageGroups["adult"], 0);
    }

    public bool isValidBreedable(Race parent1, Race parent2)
    {
        return parent1.breedable.ContainsKey(parent2.name);
    }

    public string getAgeGroup(int age)
    {
        if (age < ageGroups["child"])
        {
            return "infant";
        }
        else if (age >= ageGroups["elder"])
        {
            return "elder";
        } else if (age >= ageGroups["adult"])
        {
            return "adult";
        }
        if (age >= ageGroups["teen"])
        {
            return "teen";
        } else
        {
            return "child";
        }
    }

    private void chooseRandomStat(int bonus)
    {
        Random randy = new Random();
        int index = randy.Next(0, 6);
        string stat = BaseStats.STAT_NAMES[index];
        if(baseStatsBonuses.ContainsKey(stat))
        {
            baseStatsBonuses[stat] += bonus;
        } else
        {
            baseStatsBonuses[stat] = bonus;
        }
    }

    private void chooseRandomProficiency()
    {

    }

}
