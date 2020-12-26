using System;
using System.Collections.Generic;

public class PersonClass {

    private const int TRAINING_BONUS = 2;

    public static string[] CLASS_OPTIONS = { "fighter", "rogue", "ranger", "monk", "civilian" };

    public string className;
    public int hpBase;
    public int hpPerLevel;
    public List<string> classProficiencies;
    public int numOptionalSkills;
    public int numTrainableSkills;
    public List<string> trainableSkills;
    // public List<Power> powers;
    // public Dictionary<string, int> spellsPerLevel; - ???
    private bool allowMagic;
    private bool allowCivilian;

    public PersonClass(string name)
    {
        assignClass(name);
    }

    public Dictionary<string, int> chooseRandomTrainedSkills()
    {
        Random randy = new Random();
        Dictionary<string, int> trainedSkills = new Dictionary<string, int>();
        for(int i = 0; i < numTrainableSkills; i++)
        {
            int randomIndex = randy.Next(0, trainableSkills.Count);
            string key = trainableSkills[randomIndex];
            if(trainedSkills.ContainsKey(key))
            {
                trainedSkills[key] += TRAINING_BONUS;
            } else 
            {
                trainedSkills[key] = TRAINING_BONUS;
            }
        }

        for(int i = 0; i < numOptionalSkills; i++)
        {
            List<string> keys = Skills.allSkills(allowMagic, allowCivilian);
            int randomIndex = randy.Next(0, keys.Count);
            string key = keys[randomIndex];
            if(trainedSkills.ContainsKey(key))
            {
                trainedSkills[key] += TRAINING_BONUS;
            } else 
            {
                trainedSkills[key] = TRAINING_BONUS;
            }
        }

        return trainedSkills;
    }

    private void assignClass(string name)
    {
        switch (name)
        {
            case "fighter":
                className = name;
                hpBase = 10;
                hpPerLevel = 6;
                classProficiencies = new List<string>();
                // all armor and shields
                // simple and martial weapons
                numOptionalSkills = 2;
                numTrainableSkills = 2;
                trainableSkills = new List<string>();
                trainableSkills.Add("acrobatics");
                trainableSkills.Add("animals");
                trainableSkills.Add("athletics");
                trainableSkills.Add("history");
                trainableSkills.Add("insight");
                trainableSkills.Add("intimidation");
                trainableSkills.Add("perception");
                trainableSkills.Add("survival");
                allowCivilian = false;
                allowMagic = false;
                break;
            case "rogue":
                className = name;
                hpBase = 8;
                hpPerLevel = 5;
                classProficiencies = new List<string>();
                // light armor
                // simple weapons, hand crossbows, longswords, rapiers, shortswords
                numOptionalSkills = 0;
                numTrainableSkills = 4;
                trainableSkills = new List<string>();
                trainableSkills.Add("acrobatics");
                trainableSkills.Add("athletics");
                trainableSkills.Add("deception");
                trainableSkills.Add("insight");
                trainableSkills.Add("intimidation");
                trainableSkills.Add("perception");
                trainableSkills.Add("persuasion");
                trainableSkills.Add("stealth");
                trainableSkills.Add("theivery");
                allowCivilian = false;
                allowMagic = false;
                break;
            case "ranger":
                className = name;
                hpBase = 10;
                hpPerLevel = 6;
                classProficiencies = new List<string>();
                // Light armor, medium armor, all shields
                // Simple weapons, martial weapons
                numOptionalSkills = 1;
                numTrainableSkills = 3;
                trainableSkills = new List<string>();
                trainableSkills.Add("agriculture");
                trainableSkills.Add("animals");
                trainableSkills.Add("athletics");
                trainableSkills.Add("insight");
                trainableSkills.Add("perception");
                trainableSkills.Add("stealth");
                trainableSkills.Add("survival");
                allowCivilian = false;
                allowMagic = false;
                break;
            case "monk":
                className = name;
                hpBase = 8;
                hpPerLevel = 5;
                classProficiencies = new List<string>();
                // cloth armor
                // Simple weapons, shortswords
                numOptionalSkills = 2;
                numTrainableSkills = 2;
                trainableSkills = new List<string>();
                trainableSkills.Add("acrobatics");
                trainableSkills.Add("athletics");
                trainableSkills.Add("history");
                trainableSkills.Add("insight");
                trainableSkills.Add("religion");
                trainableSkills.Add("stealth");
                allowCivilian = false;
                allowMagic = false;
                break;
            default:
                this.className = "civilian";
                hpBase = 6;
                hpPerLevel = 4;
                classProficiencies = new List<string>();
                // cloth armor
                // Simple weapons
                numOptionalSkills = 3; // depends on education level
                numTrainableSkills = 2;
                trainableSkills = new List<string>();
                trainableSkills.Add("animals");
                trainableSkills.Add("agriculture");
                trainableSkills.Add("construction");
                trainableSkills.Add("cooking");
                trainableSkills.Add("history");
                trainableSkills.Add("labor");
                trainableSkills.Add("materials");
                trainableSkills.Add("machinery");
                trainableSkills.Add("medicine");
                trainableSkills.Add("mining");
                trainableSkills.Add("smithing");
                trainableSkills.Add("trade");
                allowCivilian = true;
                allowMagic = false;
                break;

        }
    }

}
