using System.Collections;
using System.Collections.Generic;

public class WorldDate 
{

    public const int DAYS_PER_YEAR = 40;
    public const int DAYS_PER_WEEK = 5;
    public const int WEEKS_PER_YEAR = DAYS_PER_YEAR / DAYS_PER_WEEK;

    public int day;
    public int year;

    public WorldDate()
    {
        day = 1;
        year = 1;
    }

    public WorldDate(int day, int year)
    {
        this.day = day;
        this.year = year;
    }

    public void advanceADay()
    {
        if (day == DAYS_PER_YEAR)
        {
            day = 1;
            year++;
        } else
        {
            day++;
        }
    }

}
