using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Dialogs
{
    public static Part CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Part>(jsonString);
    }
    public class Part
    {
        public string Label;
        public string[] Texts;
    }
}
