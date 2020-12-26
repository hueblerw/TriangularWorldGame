using System.Collections.Generic;

public class TriangularTile {

    public string terrain;
    public string habitat;
    public Resource resource;
    public int catanNumber;
    public string developmentLevel;
    // public Dictionary<string, Building> buildings;


    public TriangularTile(string terrain, string habitat, int catanNum, int randomInt)
    {
        this.terrain = terrain;
        this.habitat = habitat;
        this.catanNumber = catanNum;
        this.developmentLevel = "wilderness";
        this.resource = generateRandomResource(terrain, habitat, randomInt);
    }

    private Resource generateRandomResource(string currentTerrain, string currentHabitat, int randomInt)
    {
        List<Resource> validResources = getResourceListForTerrain(currentTerrain, currentHabitat);
        return getResourceForRandomNumber(randomInt, validResources, currentTerrain);
    }

    private Resource getResourceForRandomNumber(int randomInt, List<Resource> validResources, string currentTerrain)
    {
        int sum = 0;
        foreach (Resource resource in validResources)
        {
            if ((resource.name.Equals("iron_ore") || resource.name.Equals("precious_ore") || resource.name.Equals("stone")) && currentTerrain.Equals("mountains"))
            {
                sum += (2 * resource.abundance);
            } else
            {
                sum += resource.abundance;
            }

            if (randomInt < sum)
            {
                return resource;
            }
        }

        return null;
    }

    private string printList(List<Resource> validResources)
    {
        string output = "";
        foreach(Resource resource in validResources)
        {
            output += resource.name + " - ";
        }
        return output;
    }

    private List<Resource> getResourceListForTerrain(string currentTerrain, string currentHabitat)
    {
        List<Resource> allResources = Resource.getAllResources();
        List<Resource> validResources = new List<Resource>();
        foreach(Resource resource in allResources)
        {
            if (resource.isValid(currentTerrain, currentHabitat))
            {
                validResources.Add(resource);
            }
        }

        return validResources;
    }

    public override string ToString()
    {
        string output = "";
        if (habitat != null)
        {
            output += habitat + " ";
        }
        output += terrain;
        if (resource != null)
        {
            output += " - " + resource.name;
        }
        output += " (" + catanNumber + ")";
        output += "\nSettlement Level: " + developmentLevel;

        return output;
    }

}
