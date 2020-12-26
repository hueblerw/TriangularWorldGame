using System.Collections.Generic;
using System;

public class Person {

    public string name;
    public string gender;
    public int currentHp;
    public string clan;
    public string father;
    public string mother;
    public Culture culture;
    private BaseStats baseStats;
    public Race race;
    public PersonClass personClass;
    public Genes genes;
    public int age;
    public List<string> proficiencies;
    public Dictionary<string, int> skillBonuses;

    public Person(string name, string gender, string clan, string father, string mother, Race race, Culture culture, PersonClass personClass, Genes genes, int age, List<string> proficiencies, Dictionary<string, int> skillBonuses)
    {
        this.name = name;
        this.gender = gender;
        this.clan = clan;
        this.father = father;
        this.mother = mother;
        this.race = race;
        this.culture = culture;
        this.baseStats = new BaseStats();
        this.personClass = personClass;
        this.age = age;
        this.proficiencies = proficiencies;
        this.skillBonuses = skillBonuses;
    }

    public static Person newBorn(string name, Dictionary<string, Person> parents)
    {
        Race race = racialInheritance(parents);
        string gender = randomGender();
        Culture culture = culturalInheritance(parents);
        Genes genes = geneticInheritance(parents);
        PersonClass personClass = randomClass(culture);
        List<string> proficiencies = naturalProficiencies(race, personClass);
        Dictionary<string, int> skills = naturalSkills(race, personClass, culture);
        return new Person(name, gender, parents["father"].clan, parents["father"].name, parents["mother"].name, race, culture, personClass, genes, 1, proficiencies, skills);
    }

    public static Person newImmigrant(string name, string clan, Dictionary<string, Person> parents, Culture culture)
    {
        Race race = randomRace(culture);
        string gender = randomGender();
        string father;
        string mother;
        if(parents.ContainsKey("father"))
        {
            father = parents["father"].name;
        } else {
            father = "unknown";
        }
        if(parents.ContainsKey("mother"))
        {
            mother = parents["mother"].name;
        } else {
            mother = "unknown";
        }
        PersonClass personClass = randomClass(culture);
        Genes genes;
        if (parents.Count == 2)
        {
            genes = geneticInheritance(parents);        
        } else if (parents.Count == 1)
        {
            if(father == "unknown")
            {
                genes = new Genes(parents["mother"], culture);
            } else {
                genes = new Genes(parents["father"], culture);
            }
        } else 
        {
            genes = new Genes(culture);
        }
        List<string> proficiencies = naturalProficiencies(race, personClass);
        Dictionary<string, int> skills = naturalSkills(race, personClass, culture);
        int age = race.randomAdultAge();
        return new Person(name, gender, clan, father, mother, race, culture, personClass, genes, age, proficiencies, skills);
    }

    public int getStat(string name)
    {
        switch (name)
        {
            case "strength":
                return baseStats.strength + race.baseStatsBonuses["strength"] + race.baseStatsBonuses["all"];
            case "dexterity":
                return baseStats.dexterity + race.baseStatsBonuses["dexterity"] + race.baseStatsBonuses["all"];
            case "constitution":
                return baseStats.constitution + race.baseStatsBonuses["constitution"] + race.baseStatsBonuses["all"];
            case "intelligence":
                return baseStats.intelligence + race.baseStatsBonuses["intelligence"] + race.baseStatsBonuses["all"];
            case "wisdom":
                return baseStats.wisdom + race.baseStatsBonuses["wisdom"] + race.baseStatsBonuses["all"];
            case "charisma":
                return baseStats.charisma + race.baseStatsBonuses["charisma"] + race.baseStatsBonuses["all"];
        }
        throw new Exception("Stat not found!");
    }

    public int getSkill(string name)
    {
        return 0;
    }

    // PRIVATES

    private static string randomGender()
    {
        Random randy = new Random();
        double num = randy.NextDouble();
        if (num > .5)
        {
            return "male";
        } else 
        {
            return "female";
        }
    }

    private static Dictionary<string, int> naturalSkills(Race race, PersonClass personClass, Culture culture)
    {
        Dictionary<string, int> skills = new Dictionary<string, int>();
        skills = updateSkills(skills, race.skillBonuses);
        skills = updateSkills(skills, culture.skillBonuses);
        skills = updateSkills(skills, personClass.chooseRandomTrainedSkills());
        return skills;
    }

    private static Dictionary<string, int> updateSkills(Dictionary<string, int> skills, Dictionary<string, int> newSkillBonuses)
    {
        foreach (string key in new List<string>(newSkillBonuses.Keys)){
            if(skills.ContainsKey(key))
            {
                skills[key] += newSkillBonuses[key];
            } else 
            {
                skills[key] = newSkillBonuses[key];
            }
        }

        return skills;
    }

    private static List<string> naturalProficiencies(Race race, PersonClass personClass)
    {
        List<string> proficiencies = new List<string>();
        proficiencies.AddRange(race.proficiencies);
        proficiencies.AddRange(personClass.classProficiencies);
        return proficiencies;
    }

    private static PersonClass randomClass(Culture culture)
    {
        return culture.randomClass();
    }

    private static Race randomRace(Culture culture)
    {
        return culture.randomRace();
    }

    private static Genes randomGenes(Culture culture)
    {
        return new Genes(culture);
    }

    private static Culture culturalInheritance(Dictionary<string, Person> parents)
    {
        throw new NotImplementedException();
    }

    private static Genes geneticInheritance(Dictionary<string, Person> parents)
    {
        return new Genes(parents["father"], parents["mother"]);
    }

    private static Race racialInheritance(Dictionary<string, Person> parents)
    {
        if(parents["father"].race.name == parents["mother"].race.name)
        {
            return new Race(parents["father"].race.name);
        } else
        {
            return new Race(parents["father"] + "-" + parents["mother"]);
        }
    }

    private string printDictionary(Dictionary<string, int> dict)
    {
        string output = "";
        foreach (string key in dict.Keys){
            output += key + ": +" + dict[key] + "\t"; 
        }
        return output;
    }

    public override string ToString()
    {
        string output = name + " " + clan + " - " + age + " year old " + " " + gender + " " + culture.nationality + " " +  race.name + " " + personClass.className;
        output += "\n" + baseStats;
        output += "\nSkill Bonuses:\t" + printDictionary(skillBonuses);
        return output;
    }

}
