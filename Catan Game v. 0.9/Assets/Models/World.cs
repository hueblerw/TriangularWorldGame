using System;
using System.Collections.Generic;
using UnityEngine;

public class World {

    private const int ROUND_TO = 1;
    private const double MAX_LAYER_NUMBER = 12.0;
    private const double LAYER_TRANSITION = 2.0;

    public TriangularTile[,] triangularGrid;
    public int[,] hexagonalNumbers;
    public static int worldX;
    public static int worldZ;
    public List<Person> people;

    private LayerGenerator layerGenerator;
    private System.Random randy = new System.Random();
    private Dictionary<string, int> resourceTotals;

    public World(int x, int z)
    {
        worldX = x;
        worldZ = z;
        generateWorld();
        while (isBadWorld())
        {
            Debug.Log("Invalid world, generating a new one");
            generateWorld();
        }

        this.people = createSettlementPeople();
    }

    public void generateWorld()
    {
        while (!isAcceptableWorld(resourceTotals))
        {
            layerGenerator = new LayerGenerator(worldX, worldZ, ROUND_TO);
            hexagonalNumbers = generateHexNumbers();
            triangularGrid = generateWorldGrid();
            Debug.Log(calculateWorldStats());
            Debug.Log(isAcceptableWorld(resourceTotals));
        }
    }

    public string calculateWorldStats()
    {
        string stats = "Ocean %: " + getOceanPercentage();
        stats += "\nHill %: " + getHillPercentage();
        stats += "\nMountain %: " + getMountainPercentage();
        resourceTotals = getResourceTotals();
        stats += "\nNumber of Each Resource: \n" + Utility.printHash(resourceTotals);
        return stats;
    }

    public string getTileInfo(Vector2 coors)
    {
        string info = "Tile (" + coors.x + ", " + coors.y + ")\n";
        info += triangularGrid[(int) coors.x, (int) coors.y] + "\n";
        info += "Claimed by Nation: " + "none";
        // info += getSettlementInfo(x, z);
        return info;
    }

    private Vector2 triangularToHexCoordinates(int x, int z)
    {
        if (x % 6 > 2)
        {
            return new Vector2((int)x / 3, (int)(z + 1) / 2);
        } else
        {
            return new Vector2((int)x / 3, (int) z / 2);
        }
    }

    private bool isBadWorld()
    {
        return getOceanPercentage() > 0.4 || (!resourceTotals.ContainsKey("wood") || resourceTotals["wood"] < 20);
    }

    private double getOceanPercentage()
    {
        double totalTiles = getNumOfTriangles();
        int oceanSum = 0;
        for(int x = 0; x < worldX; x++)
        {
            for (int z = 0; z < worldZ; z++)
            {
                if(triangularGrid[x, z].terrain.Equals("ocean"))
                {
                    oceanSum++;
                }
            }
        }

        return Math.Round(oceanSum / totalTiles, 2);
    }

    private double getHillPercentage()
    {
        double totalTiles = getNumOfTriangles();
        int hillSum = 0;
        for (int x = 0; x <worldX; x++)
        {
            for (int z = 0; z < worldZ; z++)
            {
                if (triangularGrid[x, z].terrain.Equals("hills"))
                {
                    hillSum++;
                }
            }
        }

        return Math.Round(hillSum / totalTiles, 2);
    }

    private double getMountainPercentage()
    {
        double totalTiles = getNumOfTriangles();
        int mountSum = 0;
        for (int x = 0; x < worldX; x++)
        {
            for (int z = 0; z < worldZ; z++)
            {
                if (triangularGrid[x, z].terrain.Equals("mountains"))
                {
                    mountSum++;
                }
            }
        }

        return Math.Round(mountSum / totalTiles, 2);
    }

    private Dictionary<string, int> getResourceTotals()
    {
        resourceTotals = new Dictionary<string, int>();
        for (int x = 0; x < worldX; x++)
        {
            for (int z = 0; z < worldZ; z++)
            {
                Resource current = triangularGrid[x, z].resource;
                if (current != null)
                {
                    if(!resourceTotals.ContainsKey(current.name))
                    {
                        resourceTotals[current.name] = 1;
                    } else
                    {
                        resourceTotals[current.name]++;
                    }
                } else
                {
                    if(!resourceTotals.ContainsKey("empty"))
                    {
                        resourceTotals["empty"] = 1;
                    } else
                    {
                        resourceTotals["empty"]++;
                    }
                }
            }
        }

        return resourceTotals;
    }

    private int getNumOfTriangles()
    {
        return worldX * worldZ;
    }

    private TriangularTile[,] generateWorldGrid()
    {
        double startingValue = randy.NextDouble() * MAX_LAYER_NUMBER;
        double[,] habitatSlider = layerGenerator.GenerateWorldLayer(0.0, MAX_LAYER_NUMBER, LAYER_TRANSITION, startingValue, false);
        startingValue = randy.NextDouble() * MAX_LAYER_NUMBER;
        double[,] terrainSlider = layerGenerator.GenerateWorldLayer(0.0, MAX_LAYER_NUMBER, LAYER_TRANSITION, startingValue, false);

        TriangularTile[,] grid = new TriangularTile[worldX, worldZ];

        for (int x = 0; x < worldX; x++)
        {
            for(int z = 0; z < worldZ; z++)
            {
                string habitatName = getHabitatName(habitatSlider[x, z]);
                string terrainName = getTerrainName(terrainSlider[x, z]);
                if (terrainName.Equals("ocean"))
                {
                    habitatName = null;
                }
                int randomInt = randy.Next(Resource.MAX_ABUNDANCE);
                Vector2 hexCoors = triangularToHexCoordinates(x, z);
                int catanNum = hexagonalNumbers[(int)hexCoors.x, (int)hexCoors.y];
                grid[x, z] = new TriangularTile(terrainName, habitatName, catanNum, randomInt);
            }
        }

        return grid;
    }

    private int[,] generateHexNumbers()
    {
        int hexX = worldX / 3;
        int hexZ = worldZ / 2 + 1;
        int[,] hexagonalNumbers = new int[hexX, hexZ];
        for (int i = 0; i < hexX; i++)
        {
            for(int j = 0; j < hexZ; j++)
            {
                hexagonalNumbers[i, j] = randomCatanNumber();
            }
        }
        return hexagonalNumbers;
    }

    private int randomCatanNumber()
    {
        int number = Utility.roll2Dice(randy);
        while (number == 7)
        {
            number = Utility.roll2Dice(randy);
        }
        return number;
    }

    private string getHabitatName(double number)
    {
        if (number < 2.0 && number >= 0.0)
        {
            return "desert";
        } else if (number < 6.0)
        {
            return "plains";
        } else if (number < 10.0)
        {
            return "forest";
        } else if (number <= 12.0)
        {
            return "swamp";
        }
        else
        {
            throw new Exception("Number is not between 0.0 and " + MAX_LAYER_NUMBER);
        }
    }

    private string getTerrainName(double number)
    {
        if (number < 3.0 && number >= 0.0)
        {
            return "ocean";
        }
        else if (number < 9.0)
        {
            return "flat";
        }
        else if (number < 11.0)
        {
            return "hills";
        }
        else if (number <= 12.0)
        {
            return "mountains";
        }
        else
        {
            throw new Exception("Number is not between 0.0 and " + MAX_LAYER_NUMBER);
        }
    }

    private bool isAcceptableWorld(Dictionary<string, int> resourceTotals)
    {
        if (resourceTotals != null)
        {
            if (resourceTotals.ContainsKey("wood") && resourceTotals.ContainsKey("iron ore") && resourceTotals.ContainsKey("stone") && resourceTotals.ContainsKey("clay") && resourceTotals.ContainsKey("sand"))
            {
                return resourceTotals["wood"] > 20 && resourceTotals["iron ore"] > 1 && resourceTotals["stone"] > 4;
            }
        }
        return false;
    }

    private List<Person> createSettlementPeople()
    {
        List<Person> people = new List<Person>();
        int numOfPeople = randy.Next(7, 13);
        int numOfClans = randy.Next(4, 9);
        Debug.Log(numOfPeople + " in " + numOfClans);
        if (numOfPeople < numOfClans)
        {
            numOfClans = numOfPeople;
        }
        // make a clan head for each clan
        for(int i = 0; i < numOfClans; i++)
        {
            Dictionary<string, Person> parents = new Dictionary<string, Person>();
            Culture culture = new Culture("Jerkland");
            string gender = Person.randomGender();
            people.Add(Person.newImmigrant(culture.randomName(gender), gender, culture.randomClan(), parents, culture));
            Debug.Log(people[i]);
        }

        for (int j = numOfClans; j < numOfPeople; j++)
        {
            int clanNum = randy.Next(0, numOfClans);
            Person person = people[clanNum];
            Debug.Log(person.name + " " + person.clan);
            // is this person a parent, sibling or child of the leader?
            int roll = randy.Next(1, 4);
            if (roll == 1 && (person.getAgeGroup() == "teen" || person.getAgeGroup() == "child" || person.getAgeGroup() == "infant"))
            {
                roll++;
            }
            if (roll == 3 && person.getAgeGroup() == "elder")
            {
                roll--;
            }

            string gender = Person.randomGender();
            Dictionary<string, Person> parents = new Dictionary<string, Person>();
            Culture culture = person.culture;

            // need to limit the age of the new character and understand why the genes are null for person in child case???
            switch (roll)
            {
                case 1:
                    // child
                    Debug.Log(person.gender);
                    if (person.gender == "female")
                    {
                        parents["mother"] = person;
                    } else
                    {
                        parents["father"] = person;
                    }
                    people.Add(Person.newImmigrant(culture.randomName(gender), gender, person.clan, parents, culture));
                    break;
                case 2:
                    // sibling
                    people.Add(Person.newImmigrant(culture.randomName(gender), gender, person.clan, parents, culture));
                    break;
                case 3:
                    // parent
                    Person newPerson = Person.newImmigrant(culture.randomName(gender), gender, person.clan, parents, culture);
                    people.Add(newPerson);
                    if (newPerson.gender == "female")
                    {
                        person.mother = newPerson.name;
                    }
                    else
                    {
                        person.father = newPerson.name;
                    }
                    break;
            }

            Debug.Log(people[j]);
        }

        return people;
    }

}
