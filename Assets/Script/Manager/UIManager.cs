using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager Instance;

    [Header("Fade Panel")]
    [SerializeField] private float defaultFadeDuration = 1f;
    [SerializeField] private Image fadePanel;

    public bool IsFadingProcess { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        FadeOut();
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

    public void ChangeScene(int  scene)
    {
        StartCoroutine(StartChangingScene(scene));
    }

    private IEnumerator StartChangingScene(int nextlevelIndex)
    {
        yield return Instance.FadeIn(1f);
        SceneManager.LoadScene(nextlevelIndex, LoadSceneMode.Single);
        yield return new WaitForSeconds(1f);
        yield return Instance.FadeOut(1f);
    }
}
