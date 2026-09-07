using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueKnot
{
    public string KnotName;
    [TextArea]
    public List<string> texts;
}

public class DialogueManager : MonoBehaviour
{
    static public DialogueManager instance;

    [Header("Main dialogue used during the game")]
    [SerializeField] public List<DialogueKnot> dialogueTexts;

    [Header("Dialogue UI Link & Setting")]
    public Text uiTextDialogue;
    public Image uiImageDialogue;

    public float textFlowSpeed = 1f;

    #region Property
    private bool bIsDialoguePlaying = false;

    private Coroutine dialogueCoroutine = null; // Used to keep in track the coroutine
    #endregion

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

        //if (uiTextDialogue == null || uiImageDialogue == null)
        //{
        //    Debug.LogError("Dialogue system can't work properly without those object or setting assigned, check if anything is missing before starting the game");
        //    return;
        //}
    }

    public void EnterDialogue(string KnotName)
    {
        if (bIsDialoguePlaying == true) return;

        if (!FindKnot(KnotName))
        {
            Debug.LogWarning($"{KnotName} is not set or miss written");
            return;
        }

        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
        Debug.Log("Dialogue knot find, start playing");
        bIsDialoguePlaying = true;
        dialogueCoroutine = StartCoroutine(CO_StartDialogue(KnotName));
    }

    private bool FindKnot(string KnotName)
    {
        foreach (DialogueKnot knot in dialogueTexts)
        {
            if (string.Equals(KnotName,knot.KnotName))
                return true;
        }
        return false;
    }

    private IEnumerator CO_StartDialogue(string KnotName)
    {
        Debug.Log($"Dialogue started {KnotName}");
        bIsDialoguePlaying = false;
        yield return null;
    }
}
