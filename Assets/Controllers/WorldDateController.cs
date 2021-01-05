using UnityEngine;
using UnityEngine.UI;

public class WorldDateController : MonoBehaviour
{

    public void advanceADay()
    {
        WorldMapController worldMapController = GameObject.Find("WorldObject").GetComponent<WorldMapController>();
        WorldDate worldDate = worldMapController.world.worldDate;
        worldDate.advanceADay();
        updateDate(worldDate);
    }

    public static void updateDate(WorldDate worldDate)
    {
        string dateString = printDateString(worldDate);
        GameObject dateObject = GameObject.Find("DateText");
        dateObject.GetComponent<Text>().text = dateString;
    }

    private static string printDateString(WorldDate worldDate)
    {
        return "Year: " + worldDate.year + "\t\tDay: " + worldDate.day;
    }

}
