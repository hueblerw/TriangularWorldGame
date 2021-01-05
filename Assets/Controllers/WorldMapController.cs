using System;
using UnityEngine;

public class WorldMapController : MonoBehaviour {

    public int worldX;
    public int worldZ;

    public World world;

    public Texture2D[] triangularTileImages;
    public Texture2D[] terrainImages;
    public Texture2D[] hexNumberImages;
    public Texture2D[] resourceImages;

    // Use this for initialization
    void Start () {
        Debug.Log("Hello World!");
        world = getWorld(false);
        // printWorld(world);
        gameObject.GetComponent<ObjectClickedController>().worldMapController = this;
        WorldDateController.updateDate(world.worldDate);
        TrianglesView trianglesView = new TrianglesView(world, triangularTileImages, terrainImages, resourceImages);
        HexagonalView hexagonalView = new HexagonalView(world, hexNumberImages);
	}

    public World getWorld(bool load)
    {
        Debug.Log("Fetching World!");
        if (!load)
        {
            Debug.Log("Building a new world (" + worldX + ", " + worldZ + ")");
            return new World(worldX, worldZ);
        }
        throw new NotImplementedException();
    }

    public static World FetchCurrentControllerWorld()
    {
        WorldMapController worldMapController = GameObject.Find("WorldObject").GetComponent<WorldMapController>();
        return worldMapController.world;
    }

    public void saveWorldToFile()
    {
        // convert the world objects to json file and save them.
    }

    public World loadWorldFromFile()
    {
        // unconvert the world json file back into world objects.
        throw new NotImplementedException();
    }

    private void printWorld(World world)
    {
        string worldString = "";
        for(int x = 0; x < worldX; x++)
        {
            for (int z = 0; z < worldZ; z++)
            {
                worldString += world.triangularGrid[x, z] + "\t";
            }
            worldString += "\n";
        }

        Debug.Log(worldString);
    }

}
