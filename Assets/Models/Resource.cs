using System;
using System.Collections.Generic;

public class Resource {

    public const int MAX_ABUNDANCE = 80;

    private static List<Resource> allResources;

    public string name;
    public string type;
    public int abundance;
    public Requirements requirements;
    // public ??? tools;
    public List<string> skills;
    public int hoursPerUnit;

	public Resource(string name, string type, int abundance, string habitats, string terrains, string skills)
    {
        this.name = name;
        this.type = type;
        this.abundance = abundance;
        this.requirements = new Requirements(separateByComma(habitats), separateByComma(terrains));
        this.skills = separateByComma(skills);
    }

    public bool isValid(string terrain, string habitat)
    {
        if (terrain.Equals("ocean") && this.requirements.terrains.Contains("ocean"))
        {
            return true;
        } else
        {
            return (requirements.terrains.Contains(terrain) && requirements.habitats.Contains(habitat));
        }
    }

    public static int getMaxAbundance()
    {
        return 80;
    }

    public static List<Resource> getAllResources()
    {
        if (allResources == null)
        {
            allResources = constructAllResources();
        }
        return allResources;
    }

    public static string[] getAllResourceNames()
    {
        List<Resource> resources = getAllResources();
        string[] names = new string[resources.Count];
        int index = 0;
        foreach(Resource resource in resources)
        {
            names[index] = resource.name;
            index++;
        }
        Array.Sort(names, (x, y) => String.Compare(x, y));

        return names;
    }

    private static List<Resource> constructAllResources()
    {
        List<Resource> resources = new List<Resource>();
        // agriculture
        resources.Add(new Resource("fish", "agriculture", 20, "", "ocean", "0 Sailing"));
        resources.Add(new Resource("cattle", "agriculture", 10, "plains", "flat, hills", "0 Domestication"));
        resources.Add(new Resource("sheep", "agriculture", 15, "plains", "hills, mountains", "0 Domestication"));
        resources.Add(new Resource("horse", "agriculture", 5, "plains", "flat, hills", "0 Domestication"));
        resources.Add(new Resource("game", "agriculture", 10, "forest", "flat, hills, mountains", "0 Ranged Bonus"));
        resources.Add(new Resource("grain", "agriculture", 30, "plains", "flat", "0 Farming"));
        resources.Add(new Resource("vegetables", "agriculture", 15, "plains, forest", "flat, hills", "0 Farming"));
        resources.Add(new Resource("wine", "agriculture", 5, "plains", "flat, hills", "5 Farming"));
        resources.Add(new Resource("honey", "agriculture", 5, "plains, forest", "flat, hills", "10 Farming"));
        resources.Add(new Resource("dates", "agriculture", 5, "desert", "flat, hills", "2 Farming"));
        resources.Add(new Resource("dye", "agriculture", 2, "forest, swamp", "ocean, hills, mountains", "5 Farming"));
        resources.Add(new Resource("olives", "agriculture", 3, "plains", "flat, hills", "2 Farming"));
        resources.Add(new Resource("hemp", "agriculture", 5, "swamp", "flat, hills, mountains", "2 Farming"));
        resources.Add(new Resource("silk", "agriculture", 1, "swamp", "flat", "10 Farming"));
        resources.Add(new Resource("spices", "agriculture", 2, "desert, swamp", "flat", "5 Farming"));
        resources.Add(new Resource("papyrus", "agriculture", 1, "swamp", "flat", "10 Farming"));
        // extraction
        resources.Add(new Resource("salt", "extraction", 5, "", "ocean", "2 Mining"));
        resources.Add(new Resource("wood", "extraction", 20, "forest", "flat, hills, mountains", "12 Strength"));
        resources.Add(new Resource("iron ore", "extraction", 5, "desert, plains, forest, swamp", "hills, mountains", "2 Mining"));
        resources.Add(new Resource("precious ore", "extraction", 1, "desert, plains, forest, swamp", "mountains", "5 Mining"));
        resources.Add(new Resource("stone", "extraction", 8, "desert, plains, forest, swamp", "hills, mountains", "0 Mining"));
        resources.Add(new Resource("marble", "extraction", 2, "desert, plains, forest, swamp", "mountains", "5 Mining"));
        resources.Add(new Resource("gemstones", "extraction", 2, "desert, plains, forest, swamp", "mountains", "10 Mining"));
        resources.Add(new Resource("clay", "extraction", 10, "swamp", "flat, hills", "12 Strength"));
        resources.Add(new Resource("sand", "extraction", 10, "desert", "flat, hills", "10 Strength"));
        // other

        return resources;
    }

    private List<string> separateByComma(string list)
    {
        List<string> actualList = new List<string>();
        if (!list.Equals(""))
        {
            string[] array = list.Split(',');
            foreach (string item in array)
            {
                actualList.Add(item.Trim());
            }
        }

        return actualList;
    }

}
