using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button yesBtn;
    public Button noBtn;

    private Vector3 yesOriginalSize;  //yes 버튼의 원래 크기
    private Vector3 noOriginalSize;  //no 버튼의 원래 크기
    private Vector3 yesEnlargedSize;  //yes 버튼의 커질 크기
    private Vector3 noEnlargedSize;    //no 버튼의 커질 크기

    private float scaleFactor = 1.2f;   // 버튼이 커지는 정도를 조절하는

    [SerializeField] private string nextSceneName = "Room_1";
    [SerializeField] private string nextSceneName2 = "MainScene";
    [SerializeField] private GameObject gameOverPanel;        // 게임 오버 패널 연결
    [SerializeField] private GameObject helloPanel;             // 놀래키는 패널 연결

    [SerializeField] private Image surpriseImage;           // 놀래키는 이미지 연결
    private Vector3 surpriseImageOriginalSize;              // 원래 크기
    private Vector3 surpriseImageEnlargedSize;              // 확대된 크기
    private float surpriseImageScaleFactor = 5.0f;          // 확대 배율
    private float gameoverDelay = 1f;                       // 게임 오버 패널이 활성화 되기 까지의 지연 시간
    private float surpriseDelay = 0.2f;                     // 이미지가 커지기 까지의 지연 시간


    void Start()
    {
       
        if (yesBtn == null)
        {
            yesBtn = GetComponent<Button>();
        }
        yesBtn.onClick.AddListener(OnyesButtonClick);

        if (noBtn == null)
        {
            noBtn = GetComponent<Button>();
        }
        noBtn.onClick.AddListener(OnnoButtonClick);

        // 시작 시 패널 상태 설정
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // 비활성화
        
        // 시작 시 패널 상태 설정
        if (helloPanel != null)
            helloPanel.SetActive(false); // 비활성화


        // 버튼들의 원래 크기를 저장
        yesOriginalSize = yesBtn.transform.localScale;
        noOriginalSize = yesBtn.transform.localScale;

        // 최대로 커질 버튼 크기 계산
        yesEnlargedSize = yesOriginalSize *  scaleFactor;
        noEnlargedSize = yesOriginalSize * scaleFactor;

        // 이벤트 트리거 컴포넌트 추가 및 이벤트 등록
        AddButtonEvents(yesBtn, noBtn);
        AddButtonEvents(noBtn, yesBtn);

        // 놀래키는 이미지 크기 조정
        if (surpriseImage != null)
        {
            surpriseImageOriginalSize = surpriseImage.transform.localScale;
            surpriseImageEnlargedSize = surpriseImageOriginalSize * surpriseImageScaleFactor;
        }

    }
    public void OnyesButtonClick()
    {
        Debug.Log("yes button clicked");
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    public void OnnoButtonClick()
    {
        Debug.Log("no button clicked");
        LoadNextScene2();
    }
    public void LoadNextScene2()
    {
        SceneManager.LoadScene(nextSceneName2);
    }

    void AddButtonEvents(Button targetButton, Button otherButton)
    {
        // 이벤트 트리거 컴포넌트 가져오기 (없으면 자동으로 추가)
        EventTrigger trigger = targetButton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = targetButton.gameObject.AddComponent<EventTrigger>();

        // 포인터 진입 이벤트 (마우스가 올려져 있을때)
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) =>
        {
            // 타겟 버튼 확대
            if (targetButton == yesBtn)
                targetButton.transform.localScale = yesEnlargedSize;
            else
                targetButton.transform.localScale = noEnlargedSize;

            // 다른 버튼은 축소 (원래 크기로)
            if (otherButton == yesBtn)
                otherButton.transform.localScale = yesOriginalSize;
            else
                otherButton.transform.localScale = noOriginalSize;
        });
        trigger.triggers.Add(entryEnter);
        
        EventTrigger.Entry entryExit = new EventTrigger.Entry();  
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) =>
        {
            // 두 버튼 중 어느 버튼에도 마우스가 올려져 있지 않을때
        });
        trigger.triggers.Add(entryExit);
    }

    public void ShowGameOverPanel()
    {
        // 놀래키는 이미지가 있으면 처음엔 작게 시작(기본)
        if (surpriseImage != null)
        {
            surpriseImage.transform.localScale = surpriseImageOriginalSize;

            // 일정 시간 후 이미지 크기를 갑자기 확대
            StartCoroutine(EnlargeSurpriseImage());
        }
    }

    // 이미지를 갑자기 확대하는 코루틴
    private IEnumerator EnlargeSurpriseImage()
    {
        // 지정된 지연 시간만큼 대기
        yield return new WaitForSeconds(surpriseDelay);

        // 이미지 크기를 갑자기 확 키움
        surpriseImage.transform.localScale = surpriseImageEnlargedSize;

        // 약간의 흔들림 효과 추가
        float snakeDuration = 0.5f;
        float elapsedTime = 0f;

        Vector3 originalPosition = surpriseImage.transform.localPosition;

        while (elapsedTime < snakeDuration)
        {
            float offsetX = Random.Range(-10f, 10f);
            float offsetY = Random.Range(-10f, 10f);

            surpriseImage.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 원래 위치로 복원
        surpriseImage.transform.localPosition = originalPosition;

        // 지정된 지연 시간만큼 대기 후 게임 오버 패널 표시
        yield return new WaitForSeconds(gameoverDelay);

        // 게임 오버 패널 활성화
        if (gameOverPanel != null)
        {
            SoundManager.Instance.Pause("배경음");
            gameOverPanel.SetActive(true);
        }

        // 놀래키는 패널 비활성화
        helloPanel.SetActive(false);

    }
}
