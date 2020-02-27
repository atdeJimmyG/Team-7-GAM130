using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    [SerializeField] private Slider loadBar;
    [SerializeField] GameObject fill;
    [SerializeField] GameObject background;
    Image fillImage;
    Image backgroundImage;

    private void Awake()
    {
        fillImage = fill.GetComponent<Image>();
        backgroundImage = background.GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn ()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            backgroundImage.color = new Color(87f, 87f, 87, 0f);
            fillImage.color = new Color(255f, 255f, 255f, 0f);
            yield return 0;
        }
    }

    public void fadeTo(string scene)
    {
        StartCoroutine(fadeOut(scene));
    }

    public void fadetoindex(int index)
    {
        StartCoroutine(fadeOutindex(index));
    }

    IEnumerator fadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            backgroundImage.color = new Color(87f, 87f, 87f, a);
            fillImage.color = new Color(255f, 255f, 255f, a);
            yield return 0;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);

        loadBar.enabled = true;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadBar.value = progress;
            yield return null;
        }

        if (operation.isDone)
        {
            loadBar.enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

    }

    IEnumerator fadeOutindex(int index)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            backgroundImage.color = new Color(87f, 87f, 87, a);
            fillImage.color = new Color(255f, 255f, 255f, a);
            yield return 0;
        }

        int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + index;

        AsyncOperation operation = SceneManager.LoadSceneAsync(nextBuildIndex);

        loadBar.enabled = true;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadBar.value = progress;
            yield return null;
        }

        if (operation.isDone)
        {
            loadBar.enabled = false;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

    }
}
