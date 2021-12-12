using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colour {
    red,
    green,
    clear,
    white,
}

public class Tools
{   
    public static T GetEnum<T>(string s) where T : struct {
        Enum.TryParse(s, out T returner);
        return returner;
    }

    public static bool ParseBool(string s) {
        return s == "TRUE";
    }
}
