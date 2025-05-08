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
    [SerializeField] private GameObject gameOverPanel;        // 게임 오버 패널 연결


    void Start()
    {
       
        if (yesBtn == null)
        {
            yesBtn = GetComponent<Button>();
        }
        yesBtn.onClick.AddListener(OnyesButtonClick);

        // 시작 시 패널 상태 설정
        gameOverPanel.SetActive(false); // 비활성화


        // 버튼들의 원래 크기를 저장
        yesOriginalSize = yesBtn.transform.localScale;
        noOriginalSize = yesBtn.transform.localScale;

        // 최대로 커질 버튼 크기 계산
        yesEnlargedSize = yesOriginalSize *  scaleFactor;
        noEnlargedSize = yesOriginalSize * scaleFactor;

        // 이벤트 트리거 컴포넌트 추가 및 이벤트 등록
        AddButtonEvents(yesBtn, noBtn);
        AddButtonEvents(noBtn, yesBtn);

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
}
