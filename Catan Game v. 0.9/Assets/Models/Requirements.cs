using System.Collections.Generic;

public class Requirements {

    public List<string> habitats;
    public List<string> terrains;
    public Dictionary<string, int> resources;

    public Requirements(List<string> habitats, List<string> terrains)
    {
        this.habitats = habitats;
        this.terrains = terrains;
    }

    public Requirements(Dictionary<string, int> resources)
    {
        this.resources = resources;
    }

}
