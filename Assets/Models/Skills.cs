using System.Collections.Generic;

public class Skills {

    public static List<string> magicRestrictedSkills()
    {
        string[] array = { "arcana", "potions", "wand lore" };
        return new List<string>(array);
    }

    public static List<string> civilianRestrictedSkills()
    {
        string[] array = { "construction", "machinery", "smithing" };
        return new List<string>(array);
    }

    public static List<string> allSkills(bool allowMagic, bool allowCivilian)
    {
        List<string> allKeys = new List<string>(skillStatMapping().Keys);
        if (!allowMagic){
            allKeys.RemoveAll(i => magicRestrictedSkills().Contains(i));
        }
        if (!allowCivilian){
            allKeys.RemoveAll(i => civilianRestrictedSkills().Contains(i));
        }
        return allKeys;
    }

    public static Dictionary<string, string> skillStatMapping()
    {
        Dictionary<string, string> mapping = new Dictionary<string, string>();
        mapping["acrobatics"] = "dexterity";
        mapping["animals"] = "wisdom";
        mapping["arcana"] = "intelligence"; // magic users only
        mapping["agriculture"] = "strength";
        mapping["athletics"] = "strength";
        mapping["construction"] = "strength"; // civilians only
        mapping["cooking"] = "wisdom";
        mapping["deception"] = "charisma";
        mapping["history"] = "intelligence";
        mapping["insight"] = "wisdom";
        mapping["intimidate"] = "charisma";
        mapping["labor"] = "constitution";
        mapping["materials"] = "strength";
        mapping["machinery"] = "intelligence"; // civilians only
        mapping["medicine"] = "wisdom";
        mapping["mining"] = "constitution";
        mapping["perception"] = "wisdom";
        mapping["persausion"] = "charisma";
        mapping["potions"] = "wisdom"; // magic users only
        mapping["smithing"] = "strength"; // civilians
        mapping["survival"] = "wisdom";
        mapping["stealth"] = "dexterity";
        mapping["trade"] = "charisma";
        mapping["thievery"] = "dexterity";
        mapping["wand lore"] = "intelligence"; // magic users only

        return mapping;
    }

}
