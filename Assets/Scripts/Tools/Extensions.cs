using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Extensions
{   
    public static string ToString(this Vector3 vector) {
        return "[" + vector.x + " " + vector.y + " " + vector.z + "]";
    }
}
