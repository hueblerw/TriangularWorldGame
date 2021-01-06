using UnityEngine;
using UnityEngine.UI;

public class WorldDateController : MonoBehaviour
{

    public void advanceADay()
    {
        WorldMapController worldMapController = GameObject.Find("WorldObject").GetComponent<WorldMapController>();
        WorldDate worldDate = worldMapController.world.worldDate;
        if (worldDate.day == WorldDate.DAYS_PER_YEAR)
        {
            performYearlyTasks();
            performWeeklyTasks();
        }
        else
        {
            if ((worldDate.day) % 5 == 0)
            {
                performWeeklyTasks();
            }
        }

        performDailyTasks(worldDate);
    }

    private void performDailyTasks(WorldDate worldDate)
    {
        // perform daily tasks
        DiceRollController diceRollController = GameObject.Find("DiceRollNumber").GetComponent<DiceRollController>();
        diceRollController.rollTheDice();
        worldDate.advanceADay();
        updateDate(worldDate);
    }

    private void performWeeklyTasks()
    {

    }

    private void performYearlyTasks()
    {

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
