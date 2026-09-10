using Com.DefaultCompany.LostSoul;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StoryKnot
{
    private int currentIndex = -1;
    public DialogueKnot knot;
    public bool canContinue
    {
        get
        {
            return knot.texts.Count > (currentIndex + 1);
        }
    }

    public string Continue()
    {
        if (canContinue)
        {
            ++currentIndex;
            return knot.texts[currentIndex];
        }
        else
        {
            Debug.LogWarning($"Trying to continue outside of the current knot or story : {knot.KnotName}");
            return string.Empty;
        }
    }
    public void Reset()
    {
        currentIndex = -1;
        knot = null;
    }
}

public class DialogueManager : Manager<DialogueManager>
{
    // Listener for those system that need dialogue start and end info
    public UnityEvent OnDialogueStarted = new();
    public UnityEvent OnDialogueEnded = new();

    [Header("Main dialogue used during the game")]
    [SerializeField] private SO_Story story;

    [Header("Dialogue UI Link & Setting")]
    [SerializeField] private float typingSpeed = 1f;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image uiBackgroudImageDialogue;

    [Header("Dialogue Sound Effect")]
    public FMODUnity.EventReference submitEvent;
    FMOD.Studio.EventInstance submitEventInstance;

    public FMODUnity.EventReference displayLineLetterEvent;
    FMOD.Studio.EventInstance displayLineLetterEventInstance;

    #region Property
    private bool isPlaying = false;
    private bool canContinueToNextLine = false;
    private StoryKnot currentStory = new();
    private PlayerInputHandler playerInputHandler;
    private UIManager uiManager;

    // For dialogue system
    private Coroutine displayLineCoroutine = null; // Used to keep in track the coroutine
    #endregion

    private void Awake()
    {
        if (dialogueText == null || uiBackgroudImageDialogue == null)
        {
            Debug.LogError("Dialogue system can't work properly without those object or setting assigned, check if anything is missing before starting the game");
            return;
        }

        dialogueText.text = string.Empty;
        SetTransparency(uiBackgroudImageDialogue, 0);
    }

    private void Update()
    {
        if (!isPlaying) return;

        if (canContinueToNextLine && playerInputHandler.GetSubmitPressed())
        {
            FMODUnity.RuntimeManager.PlayOneShot(submitEvent);
            ContinueStory();
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        CoreSystem.OnServiceRegistered += SetService;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        CoreSystem.OnServiceRegistered -= SetService;
    }
    private void SetService(Type serviceType)
    {
        if (serviceType == typeof(PlayerInputHandler))
        {
            playerInputHandler = CoreSystem.Instance.GetService<PlayerInputHandler>();
        }
        if (serviceType == typeof(UIManager))
        {
            uiManager = CoreSystem.Instance.GetService<UIManager>();
        }
    }

    public IEnumerator CO_EnterDialogue(InteractableItemInfo collectableItemInfo)
    {
        if (isPlaying) yield break;
        isPlaying = true;

        if ((currentStory.knot = story.FindKnot(collectableItemInfo.knotName)) == null)
        {
            Debug.LogWarning($"{collectableItemInfo.knotName} is not set or miss written");
            yield break;
        }

        Debug.Log($"Dialogue started {collectableItemInfo.knotName}");

        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
        }
        Debug.Log("Dialogue knot find, start playing");

        OnDialogueStarted?.Invoke();

        if (collectableItemInfo.backgroundSprite != null)
        {
            uiBackgroudImageDialogue.sprite = collectableItemInfo.backgroundSprite;
        }
        else
        {
            Debug.LogWarning($"Background sprite is not set for {collectableItemInfo.knotName}");
        }
        SetTransparency(uiBackgroudImageDialogue, 1);
        uiManager.ShowPanel(MenuType.Dialogue);

        ContinueStory();
        yield return null;
    }

    public IEnumerator CO_ExitDialogue()
    {
        if (!isPlaying) yield break;
        isPlaying = false;

        uiManager.ShowPanel(MenuType.InGame);
        SetTransparency(uiBackgroudImageDialogue, 0);
        uiBackgroudImageDialogue.sprite = null; // Reset background sprite
        dialogueText.text = "???"; // Reset dialogue text
        currentStory.Reset();
        OnDialogueEnded?.Invoke(); // Notice player FSM & other to exit dialogue mode
        Debug.Log("Dialogue has ended");
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
        }
        else
        {
            StartCoroutine(CO_ExitDialogue());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        canContinueToNextLine = false;

        int lineLen = line.ToCharArray().Length;
        if (lineLen <= 1)
        {
            ContinueStory();
            yield return null;
        }
        dialogueText.text = line;

        for (int i = 1; i <= lineLen; i++)
        {
            dialogueText.maxVisibleCharacters = i;
            if (playerInputHandler.GetSubmitPressed() && i > 3)
            {
                dialogueText.maxVisibleCharacters = lineLen;
                break;
            }
            yield return new WaitForSeconds(typingSpeed);
        }

        canContinueToNextLine = true;
    }

    public void SetTransparency(UnityEngine.UI.Image image, float transparency)
    {
        if (image != null)
        {
            UnityEngine.Color alpha = image.color;
            alpha.a = transparency;
            image.color = alpha;
        }
    }
}
