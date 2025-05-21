using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimeLine : MonoBehaviour
{
    // Ÿ�Ӷ��� ���� �� �� ��ȯ���� �ɸ��� �ð�
    public float delayBeforeTransition = 0.5f;

    // �� ��ȯ �� Fade ȿ�� ��� ����
    public bool useFadeEffect = true;

    // Fade ȿ�� ���� �ð�
    public float fadeDuration = 1.0f;

    private PlayableDirector director;

    // �� ��ȯ�� �̹� ���۵Ǿ����� Ȯ��
    private bool transitionStarted = false;

    void Start()
    {
        // PlayableDirector ������Ʈ ��������
        director = GetComponent<PlayableDirector>();

        if (director == null)
        {
            return;
        }

        director.stopped += OnTimelineStopped;
    }

    private void OnDestroy()
    {
        if (director != null)
        {
            director.stopped -= OnTimelineStopped;
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (transitionStarted) return;

        transitionStarted = true;

        StartCoroutine(TransitionToNextScene());
    }

    private IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(delayBeforeTransition);

        if (useFadeEffect)
        {
            yield return StartCoroutine(FadeOut());
        }

        SceneManager.LoadScene("Room_1");
    }

    private IEnumerator FadeOut()
    {
        // Fade ȿ���� ���� ĵ������ �̹����� �����ϰڴ�
        GameObject fadeObject = new GameObject("FadeCanvas");
        Canvas fadeCanvas = fadeObject.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = 999; // �� ���� ǥ��

        UnityEngine.UI.Image fadeImage = fadeObject.AddComponent<UnityEngine.UI.Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // ������ ����ȭ

        // ����Ƽ UI �� ��ġ�� �ٲ� �� RectTransform�� ����ؾ� �Ѵ�
        RectTransform rectTransform = fadeImage.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero; // ��ü ȭ�� ũ��

        // Fade Out ȿ��
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
    }
}
