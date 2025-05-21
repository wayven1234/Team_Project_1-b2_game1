using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimeLine : MonoBehaviour
{
    // 타임라인 끝난 후 찐 전환까지 걸리는 시간
    public float delayBeforeTransition = 0.5f;

    // 씬 전환 시 Fade 효과 사용 여부
    public bool useFadeEffect = true;

    // Fade 효과 지속 시간
    public float fadeDuration = 1.0f;

    private PlayableDirector director;

    // 씬 전환이 이미 시작되었는지 확인
    private bool transitionStarted = false;

    void Start()
    {
        // PlayableDirector 컴포넌트 가져오기
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
        // Fade 효과를 위한 캔버스와 이미지를 생성하겠다
        GameObject fadeObject = new GameObject("FadeCanvas");
        Canvas fadeCanvas = fadeObject.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = 999; // 맨 위에 표시

        UnityEngine.UI.Image fadeImage = fadeObject.AddComponent<UnityEngine.UI.Image>();
        fadeImage.color = new Color(0, 0, 0, 0); // 검정색 투명화

        // 유니티 UI 는 위치를 바꿀 때 RectTransform을 사용해야 한다
        RectTransform rectTransform = fadeImage.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero; // 전체 화면 크기

        // Fade Out 효과
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
