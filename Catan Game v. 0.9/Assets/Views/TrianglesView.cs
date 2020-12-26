using System;
using System.Collections.Generic;
using UnityEngine;

public class TrianglesView
{

    private float horizConst = 1.5f;
    private float verticalConst = 1.5f * (float) Math.Sqrt(3);

    public GameObject triangleBoard;
    private World world;
    private Dictionary<string, Texture2D> triangularTileImages;
    private Dictionary<string, Texture2D> terrainImages;
    private Dictionary<string, Texture2D> resourceTileImages;

    public TrianglesView(World world, Texture2D[] triangularTileImages, Texture2D[] terrainImages, Texture2D[] resourceImages)
    {
        this.world = world;
        this.triangularTileImages = habitatToHash(triangularTileImages);
        this.terrainImages = terrainToHash(terrainImages);
        this.resourceTileImages = resourceToHash(resourceImages);
        triangleBoard = new GameObject("TriangleBoard");
        buildTriangleMap();
    }

    public void buildTriangleMap()
    {
        for (int x = 0; x < World.worldX; x++)
        {
            for (int z = 0; z < World.worldZ; z++)
            {
                string habitatName = world.triangularGrid[x, z].habitat;
                string terrainName = world.triangularGrid[x, z].terrain;
                GameObject triangleTile = addHabitatTriangle(x, z, habitatName);
                makeTriangleClickable(triangleTile);
                if (!terrainName.Equals("flat") && !terrainName.Equals("ocean"))
                {
                    addHillsAndMountains(x, z, terrainName, triangleTile);
                }
                if (world.triangularGrid[x, z].resource != null)
                {
                    string resourceName = world.triangularGrid[x, z].resource.name;
                    addResource(x, z, resourceName, triangleTile);
                }
            }
        }
    }

    public void destroyWorld()
    {
        UnityEngine.Object.Destroy(triangleBoard);
    }

    private void addResource(int x, int z, string resourceName, GameObject parentTile)
    {
        GameObject currentTile = new GameObject(x + ", " + z + " - " + resourceName);
        currentTile.transform.parent = parentTile.transform;
        SpriteRenderer sr = currentTile.AddComponent<SpriteRenderer>();
        sr.sprite = GetResourceSprite(x, z, resourceName);
        sr.sortingOrder = 2;
        setResourcePosition(currentTile, x, z);
    }

    private void addHillsAndMountains(int x, int z, string terrainName, GameObject parentTile)
    {
        GameObject currentTile = new GameObject(x + ", " + z + " - " + terrainName);
        currentTile.transform.parent = parentTile.transform;
        SpriteRenderer sr = currentTile.AddComponent<SpriteRenderer>();
        sr.sprite = GetTerrainSprite(x, z, terrainName);
        sr.sortingOrder = 1;
        setHabitatPosition(currentTile, x, z, terrainName);
    }

    private GameObject addHabitatTriangle(int x, int z, string habitatName)
    {
        GameObject currentTile = new GameObject(x + ", " + z + " - " + world.triangularGrid[x, z]);
        currentTile.transform.parent = triangleBoard.transform;
        SpriteRenderer sr = currentTile.AddComponent<SpriteRenderer>();
        sr.sprite = GetTriangleSprite(x, z, habitatName);
        setTrianglePosition(currentTile, x, z);
        return currentTile;
    }

    private Sprite GetTerrainSprite(int x, int z, string terrain)
    {
        return Sprite.Create(terrainImages[terrain], new Rect(0, 0, terrainImages[terrain].width, terrainImages[terrain].height), new Vector2(.5f, .5f));
    }

    private Sprite GetResourceSprite(int x, int z, string resource)
    {
        return Sprite.Create(resourceTileImages[resource], new Rect(0, 0, resourceTileImages[resource].width, resourceTileImages[resource].height), new Vector2(.5f, .5f));
    }

    private Sprite GetTriangleSprite(int x, int z, string habitat)
    {
        if (habitat == null)
        {
            return Sprite.Create(triangularTileImages["ocean"], new Rect(0, 0, triangularTileImages["ocean"].width, triangularTileImages["ocean"].height), new Vector2(.5f, .5f));
        }
        return Sprite.Create(triangularTileImages[habitat], new Rect(0, 0, triangularTileImages[habitat].width, triangularTileImages[habitat].height), new Vector2(.5f, .5f));
    }

    private void setTrianglePosition(GameObject currentTile, int x, int z)
    {
        Vector3 position = new Vector3(horizConst * x, z * verticalConst, 0.0f);
        Quaternion rotation = calculateRotation(x, z);
        Vector3 scale = new Vector3(1f, 1f, 1f);
        setPosition(currentTile, position, rotation, scale);
    }

    private void setHabitatPosition(GameObject currentTile, int x, int z, string terrainName)
    {
        Vector3 position = new Vector3(0f, 0f, 0f);
        Quaternion rotation = secondaryRotation(x, z);
        Vector3 scale;
        if (terrainName.Equals("hills"))
        {
            scale = new Vector3(0.66f, 0.66f, 0.66f);
        } else
        {
            scale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        setPosition(currentTile, position, rotation, scale);
    }

    private void setResourcePosition(GameObject currentTile, int x, int z)
    {
        Vector3 position = new Vector3(0, -.75f, 0f);
        Quaternion rotation = secondaryRotation(x, z);
        Vector3 scale = new Vector3(1f, 1f, 1f);
        setPosition(currentTile, position, rotation, scale);
    }

    private void setPosition(GameObject currentTile, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        currentTile.transform.localScale = scale;
        currentTile.transform.localPosition = position;
        currentTile.transform.localRotation = rotation;
    }

    private Quaternion calculateRotation(int x, int z)
    {
        if (x % 2 == 1)
        {
            if (z % 2 == 0)
            {
                return Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                return Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }
        else
        {
            if (z % 2 == 0)
            {
                return Quaternion.Euler(new Vector3(0, 0, 180));
            }
            else
            {
                return Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    private Quaternion secondaryRotation(int x, int z)
    {
        if (x % 2 == 1)
        {
            if (z % 2 == 0)
            {
                return Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                return Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }
        else
        {
            if (z % 2 == 0)
            {
                return Quaternion.Euler(new Vector3(0, 0, 180));
            }
            else
            {
                return Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    private GameObject makeTriangleClickable(GameObject triangularTile)
    {
        triangularTile.AddComponent<PolygonCollider2D>();
        PolygonCollider2D collider = triangularTile.GetComponent<PolygonCollider2D>();
        collider.isTrigger = true;

        return triangularTile;
    }

    private Dictionary<string, Texture2D> habitatToHash(Texture2D[] triangularTileImages)
    {
        Dictionary<string, Texture2D> trianglesTextures = new Dictionary<string, Texture2D>();
        trianglesTextures.Add("desert", triangularTileImages[0]);
        trianglesTextures.Add("plains", triangularTileImages[1]);
        trianglesTextures.Add("forest", triangularTileImages[2]);
        trianglesTextures.Add("swamp", triangularTileImages[3]);
        trianglesTextures.Add("ocean", triangularTileImages[4]);

        return trianglesTextures;
    }

    private Dictionary<string, Texture2D> resourceToHash(Texture2D[] resourceImages)
    {
        Dictionary<string, Texture2D> resourceTextures = new Dictionary<string, Texture2D>();
        string[] resourceNames = Resource.getAllResourceNames();
        for(int i = 0; i < resourceNames.Length; i++)
        {
            resourceTextures.Add(resourceNames[i], resourceImages[i]);
        }

        return resourceTextures;
    }

    private Dictionary<string, Texture2D> terrainToHash(Texture2D[] terrainImages)
    {
        Dictionary<string, Texture2D> terrainTextures = new Dictionary<string, Texture2D>();
        terrainTextures.Add("hills", terrainImages[0]);
        terrainTextures.Add("mountains", terrainImages[1]);

        return terrainTextures;
    }

}






