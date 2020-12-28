using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nation
{
    
	public string name;
	public string color;
	public Culture primaryCulture;
	public int stateSilverPieces;
	// public Government government;
	public List<Vector2> ownedCoor;

	public Nation(string name, string primaryCulture)
	{
		this.name = name;
		this.primaryCulture = new Culture(primaryCulture);
		ownedCoor = new List<Vector2>();
		this.color = "green";
	}

	public void assignTile(Vector2 coors)
	{
		ownedCoor.Add(coors);
	}

	public int spend(int amount)
	{
		stateSilverPieces -= amount;
		return stateSilverPieces;
	}

	public int earn(int amount)
	{
		stateSilverPieces += stateSilverPieces;
		return stateSilverPieces;
	}

	public override string ToString()
	{
		string output = "Nation: " + name + " (" + color + ")\n";
		output += "Culture: " + primaryCulture.nationality;
		output += "Cash: " + stateSilverPieces;
		return output;
	}

}
