using Com.DefaultCompany.LostSoul;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// Following dirty was made by me bcs lazy to make a proper UI manager with a proper state machine and stuff, but this is just a simple project so I made it dirty and fast. :)
/// </summary>

public enum MenuType
{
    None = 0,
    Main = 1,
    InGame = 2,
    Pause = 3,
    Settings = 4,
    Dialogue = 5,
    Inventory = 6
}

public class UIManager : Manager<UIManager>
{
    public Action OnSceneChange;

    [Header("Intro video")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip introVideoClip;
    [SerializeField] private bool skipIntroVideo;

    [Header("Scene Id")]
    [SerializeField] private int mainMenuSceneId = 1;
    [SerializeField] private int inGameSceneId = 2;

    [Header("Fade Panel")]
    [SerializeField] private float defaultFadeDuration = 1f;
    [SerializeField] private Image fadePanel;

    [Header("Menu Panels")]
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject InGameUi;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject inventoryUI;

    [Header("UI sound")]
    [SerializeField] private FMODUnity.EventReference UIClickSound;


    public MenuType DefaultMenu;

    private MenuType currentMenuType = MenuType.None;

    private MenuType lastMenuType = MenuType.None;

    public bool IsFadingProcess { get; private set; } = false;

    private PlayerInputHandler playerInputHandler;
    private PlayerInput playerInput;
    private Settings settings;

    private void Awake()
    {
        if (fadePanel == null
            || MainMenu == null
            || InGameUi == null
            || pauseMenu == null
            || settingsMenu == null
            || dialogueUI == null
            || inventoryUI == null
            || UIClickSound.IsNull)
        {
            Debug.LogError("UIManager: One or more UI elements are not assigned in the inspector.");
        }

        // Initialize all panels to be inactive at the start
        MainMenu.SetActive(false);
        InGameUi.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        dialogueUI.SetActive(false);
        inventoryUI.SetActive(false);
    }

    private void Start()
    {
        
        if (skipIntroVideo)
        {
            fadePanel.gameObject.SetActive(true);
            videoPlayer.gameObject.SetActive(false);
            FadeOut();
            ShowPanel(DefaultMenu);
            return;
        }
        PlayVideo(introVideoClip);
    }
    private void OnVideoFinished(VideoPlayer player)
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
        fadePanel.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(false);
        FadeOut();
        ShowPanel(DefaultMenu);
    }

    public void PlayVideo(VideoClip videoClip)
    {
        fadePanel.gameObject.SetActive(false);
        videoPlayer.clip = videoClip;
        videoPlayer.loopPointReached += OnVideoFinished;
    }
    private void Update()
    {
        if (playerInputHandler.GetEscapePressed())
        {
            OnBackButton();
        }
        if (playerInputHandler.GetInventoryPressed())
        {
            if (currentMenuType == MenuType.Inventory)
            {
                OnBackButton();
                return;
            }
            OnInventoryButton();
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
            playerInput = playerInputHandler.GetComponent<PlayerInput>();
        }
        if (serviceType == typeof(Settings))
        {
            settings = CoreSystem.Instance.GetService<Settings>();
        }
    }

    #region Button Events
    public void OnPlayButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        StartCoroutine(StartChangingScene(inGameSceneId, MenuType.InGame));
    }
    public void OnQuitPauseButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        StartCoroutine(StartChangingScene(mainMenuSceneId, MenuType.Main));
        CoreSystem.Instance.GetService<Inventory>().ResetInventory();
    }

    public void OnSettingButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        ShowPanel(MenuType.Settings);
    }

    public void OnPauseButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        ShowPanel(MenuType.Pause);
    }
    public void OnQuit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        Application.Quit();
    }

    public void OnInventoryButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        if (currentMenuType == MenuType.InGame)
        {
            ShowPanel(MenuType.Inventory);
            return;
        }
    }

    public void UniversalShowPanel(int menuType)
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        ShowPanel((MenuType)menuType);
    }

    public void OnBackButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        if (lastMenuType == MenuType.None) return;

        if (!(currentMenuType == MenuType.InGame && lastMenuType == MenuType.Main) &&
            !(currentMenuType == MenuType.InGame && lastMenuType == MenuType.Dialogue) &&
            !(currentMenuType == MenuType.Main && lastMenuType == MenuType.Pause) &&
            !(currentMenuType == MenuType.Main && lastMenuType == MenuType.Settings) &&
            !(currentMenuType == MenuType.Pause && lastMenuType == MenuType.Settings) &&
            !(currentMenuType == MenuType.InGame && lastMenuType == MenuType.Inventory))

        {
            ShowPanel(lastMenuType);
            return;
        }
        if (currentMenuType == MenuType.InGame)
        {
            ShowPanel(MenuType.Pause);
        }
        if (currentMenuType == MenuType.Pause)
        {
            ShowPanel(MenuType.InGame);
        }
    }

    public void OnSaveButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot(UIClickSound);
        settings.Save();
    }

    #endregion
    public void ShowPanel(MenuType menuType)
    {
        if (currentMenuType == menuType)
            return;
        HideCurrentPanel();
        switch (menuType)
        {
            case MenuType.Main:
                playerInput.SwitchCurrentActionMap("UI");
                MainMenu.SetActive(true);
                break;
            case MenuType.InGame:
                playerInput.SwitchCurrentActionMap("Gameplay");
                InGameUi.SetActive(true);
                break;
            case MenuType.Pause:
                playerInput.SwitchCurrentActionMap("Pause");
                pauseMenu.SetActive(true);
                break;
            case MenuType.Settings:
                playerInput.SwitchCurrentActionMap("UI");
                settingsMenu.SetActive(true);
                break;
            case MenuType.Dialogue:
                playerInput.SwitchCurrentActionMap("UI");
                dialogueUI.SetActive(true);
                break;
            case MenuType.Inventory:
                playerInput.SwitchCurrentActionMap("UI");
                inventoryUI.SetActive(true);
                break;
            default:
                Debug.LogWarning("UIManager: Attempted to show an unhandled menu type.");
                break;
        }
        lastMenuType = currentMenuType;
        currentMenuType = menuType;
    }

    private void HideCurrentPanel()
    {
        switch (currentMenuType)
        {
            case MenuType.None:
                break;
            case MenuType.Main:
                MainMenu.SetActive(false);
                break;
            case MenuType.InGame:
                InGameUi.SetActive(false);
                break;
            case MenuType.Pause:
                pauseMenu.SetActive(false);
                break;
            case MenuType.Settings:
                settingsMenu.SetActive(false);
                break;
            case MenuType.Dialogue:
                dialogueUI.SetActive(false);
                break;
            case MenuType.Inventory:
                inventoryUI.SetActive(false);
                break;
            default:
                Debug.LogWarning("UIManager: Attempted to hide an unhandled menu type.");
                break;
        }
    }

    #region Features
    public Coroutine FadeOut(float duration = -1f)
    {
        if (duration < 0f)
            duration = defaultFadeDuration;
        return StartCoroutine(Fade(1f, 0f, duration));
    }

    public Coroutine FadeIn(float duration = -1f)
    {
        if (duration < 0f)
            duration = defaultFadeDuration;
        return StartCoroutine(Fade(0f, 1f, duration));
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        IsFadingProcess = true;
        float t = 0f;
        UnityEngine.Color alpha = fadePanel.color;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            alpha.a = Mathf.Lerp(from, to, t);
            fadePanel.color = alpha;
            yield return null;
        }

        IsFadingProcess = false;
    }
    #endregion

    public void ChangeScene(int nextSceneId, MenuType menuType)
    {
        StartCoroutine(StartChangingScene(nextSceneId, menuType));
    }

    private IEnumerator StartChangingScene(int nextSceneId, MenuType menuType)
    {
        OnSceneChange?.Invoke();
        yield return FadeIn(1f);
        SceneManager.LoadScene(nextSceneId, LoadSceneMode.Single);
        ShowPanel(menuType);
        yield return new WaitForSeconds(1f);
        yield return FadeOut(1f);
    }
}
