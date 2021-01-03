using System;

public class Clan
{
    
	public string clanName;
	public int silverPieces;

	public Clan(string name)
	{
		this.clanName = name;
		this.silverPieces = 0;
	}

	public int spend(int amount)
	{
		silverPieces -= amount;
		return silverPieces;
	}

	public int earn(int amount)
	{
		silverPieces += amount;
		return silverPieces;
	}

	public override string ToString()
	{
		return "Clan: " + clanName + " - " + silverPieces + " sp";
	}

	public override bool Equals(Object obj)
   {
      if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
      {
        return false;
      }
      else {
      	Clan c = (Clan) obj;
      	return this.clanName == c.clanName;
      }
   }

}
