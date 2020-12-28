using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NationCreationSequence
{
    
	public static Vector2 chooseWhereToPlaceVillage(){
		string message = "Choose a location for your starting village.  Click Submit once chosen.";
		Dictionary<string, string> buttons = new Dictionary<string, string>();
		buttons["Submit"] = "tile_coordinates";
		InteractableMessageView messageView = new InteractableMessageView(message, buttons);
		return new Vector2(1, 1);
	}

	public static string chooseRuralTile(){
		return null;
	}

}
