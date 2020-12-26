using System;
using System.Collections.Generic;

public class Utility
{

    public static string printHash<T1, T2>(Dictionary<T1, T2> hash)
    {
        string text = "";
        foreach (KeyValuePair<T1, T2> entry in hash)
        {
            text += entry.Key + ": " + entry.Value + "\n";
        }
        return text;
    }

    public static string printArray(string[] array)
    {
        string output = "";
        for (int i = 0; i < array.Length; i++)
        {
            output += array[i] + ", ";
        }
        return output;
    }

    public static int roll2Dice(Random randy)
    {
        return randy.Next(1, 7) + randy.Next(1, 7);
    }

}
