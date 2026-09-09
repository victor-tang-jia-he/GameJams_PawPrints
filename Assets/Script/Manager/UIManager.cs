using Com.DefaultCompany.LostSoul;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Following dirty was made by me bcs lazy to make a proper UI manager with a proper state machine and stuff, but this is just a simple project so I made it dirty and fast. :)
/// </summary>

public enum MenuType
{
    None = 0,
    Main,
    InGame,
    Pause,
    Settings,
    Dialogue,
    Inventory
}

public class UIManager : Manager<UIManager>
{
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

    public MenuType DefaultMenu;

    private MenuType currentMenuType = MenuType.None;

    public bool IsFadingProcess { get; private set; } = false;

    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        if (fadePanel == null
            || MainMenu == null
            || InGameUi == null
            || pauseMenu == null
            || settingsMenu == null
            || dialogueUI == null
            || inventoryUI == null)
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
        FadeOut();
        ShowPanel(DefaultMenu);
    }

    private void Update()
    {
        if (playerInputHandler.GetEscapePressed())
        {
            if (currentMenuType == MenuType.Pause)
            {
                ShowPanel(MenuType.InGame);
            }
            else if (currentMenuType == MenuType.InGame)
            {
                ShowPanel(MenuType.Pause);
            }
            else if (currentMenuType == MenuType.Settings)
            {
                ShowPanel(MenuType.Main);
            }
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
    }

    #region Button Events
    public void OnPlayButton()
    {
        StartCoroutine(PlayButtonCoroutine());
    }
    // button function with Coroutine to handle fade in and fade out
    private IEnumerator PlayButtonCoroutine()
    {
        yield return FadeIn();
        ShowPanel(MenuType.InGame);
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1f);
        yield return FadeOut();
    }

    public void OnSettingButton()
    {
        ShowPanel(MenuType.Settings);
    }

    public void OnBackSettingToMainMenuButton()
    {
        ShowPanel(MenuType.Main);
    }

    public void OnPauseButton()
    {
        ShowPanel(MenuType.Pause);
    }

    public void OnQuit()
    {
        Application.Quit();
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
                MainMenu.SetActive(true);
                break;
            case MenuType.InGame:
                InGameUi.SetActive(true);
                break;
            case MenuType.Pause:
                pauseMenu.SetActive(true);
                break;
            case MenuType.Settings:
                settingsMenu.SetActive(true);
                break;
            case MenuType.Dialogue:
                dialogueUI.SetActive(true);
                break;
            default:
                break;
        }
        currentMenuType = menuType;
    }

    private void HideCurrentPanel()
    {
        switch (currentMenuType)
        {
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
            default:
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

    public void ChangeScene(int nextSceneId)
    {
        StartCoroutine(StartChangingScene(nextSceneId));
    }

    private IEnumerator StartChangingScene(int nextSceneId)
    {
        yield return FadeIn(1f);
        SceneManager.LoadScene(nextSceneId, LoadSceneMode.Single);
        yield return new WaitForSeconds(1f);
        yield return FadeOut(1f);
    }
}
