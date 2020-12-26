using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public class Nation
{
    
	public string name;
	public string color;
	public Culture primaryCulture;
	// public Government government;
	public List<Vector2> ownedCoor;

	public Nation(string name, string primaryCulture)
	{
		this.name = name;
		this.primaryCulture = new Culture(primaryCulture);
		ownedCoor = new List<Vector2>();
		this.color = "green";
	}

	public void assignTile(int x, int y)
	{
		ownedCoor.Add(new Vector2(x, y));
	}

}
