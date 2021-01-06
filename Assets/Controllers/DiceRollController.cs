using System;
using UnityEngine;
using UnityEngine.UI;

public class DiceRollController : MonoBehaviour
{

    private System.Random randy = new System.Random();
    
    public int rollTheDice()
    {
        int rollResult = diceRoll();
        string specialEvent = "Resources Collected";
        if (rollResult == 7)
        {
            specialEvent = randomSpecialEvent();
        }
        updateUI(rollResult, specialEvent);
        // trigger all the world actions related to the dice roll
        // resource gathering & skill checks

        return rollResult;
    }

    private void updateUI(int rollResult, string specialEvent)
    {
        GameObject.Find("DiceRollNumber").GetComponent<Text>().text = rollResult.ToString();
        GameObject.Find("SpecialEventText").GetComponent<Text>().text = specialEvent;
    }

    private int diceRoll()
    {
        return (randy.Next(1, 7) + randy.Next(1, 7));
    }

    private string randomSpecialEvent()
    {
        int num = randy.Next(1, 21);
        switch(num)
        {
            case 20:
                return "Spawn Immigrant Party";

            case int n when (n < 20 && n >= 17):
                return "Caravan Enters Map";

            case int n when (n < 17 && n >= 12):
                return "Spawn Single Immigrant";

            case int n when (n < 12 && n >= 5):
                return "Move Monster Parties";

            default:
                return "Spawn Monster Party";

        }
    }

}
