using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DialogueKnot
{
    public string KnotName;
    [TextArea]
    public List<string> texts;
}

[CreateAssetMenu(fileName = "SO_Story", menuName = "Scriptable Objects/Story")]
public class SO_Story : ScriptableObject
{
    public List<DialogueKnot> dialogueTexts;

    public DialogueKnot FindKnot(string KnotName)
    {
        foreach (DialogueKnot knot in dialogueTexts)
        {
            if (string.Equals(KnotName, knot.KnotName))
                return knot;
        }
        return null;
    }
}
