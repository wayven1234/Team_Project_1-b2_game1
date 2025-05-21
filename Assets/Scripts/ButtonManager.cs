using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "TimeLine";   // 다음에 로드할 씬
    [SerializeField] private Button startButton;         // 시작 버튼 연결
    [SerializeField] private Button menuButton;         // 메뉴 버튼 연결
    [SerializeField] private Button nextButton;         // 다음 버튼 연결
    [SerializeField] private Button backButton;         // 뒤로가기 버튼 연결
    [SerializeField] private Button xButton;            // X 버튼 연결
    [SerializeField] private GameObject menuPanel;        // 메뉴 패널 연결
    [SerializeField] private GameObject menu2Panel;       // 메뉴2 패널 연결

    [SerializeField] private bool startPanelInactive = true;    // 시작 시 패널 비활성화

    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>();
        }
        startButton.onClick.AddListener(OnStartButtonClick);

        if (nextButton == null)
        {
            nextButton = GetComponent<Button>();
        }
        nextButton.onClick.AddListener(OnNextButtonClick);

        if (backButton == null)
        {
            backButton = GetComponent<Button>();
        }
        backButton.onClick.AddListener(OnBackButtonClick);

        if (xButton == null)
        {
             xButton = GetComponent<Button>();
        }
        xButton.onClick.AddListener(OnXButtonClick);

        // 컴포넌트가 Inspector에 할당되지 않았다면 자동으로 찾기
        if (menuButton == null)
        {
            menuButton = GetComponent<Button>();
        }

        // 시작 시 패널 상태 설정
        if (menuPanel != null && startPanelInactive)
        {
            menuPanel.SetActive(false); // 비활성화
        }

        // 시작 시 패널 상태 설정
        if (menu2Panel != null && startPanelInactive)
        {
            menu2Panel.SetActive(false); // 비활성화
        }
    }

    // 시작 버튼 클릭 시 호출되는 함수
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene(); // 다음 씬 로드
    }

    // 다음 씬을 로드하는 함수
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // 메뉴 버튼 클릭 시 호출되는 함수
    public void OnNextButtonClick()
    {
        Debug.Log("Next button clicked");
        menuPanel.SetActive(false); // 메뉴 패널 비활성화
        menu2Panel.SetActive(true); // 메뉴2 패널 활성화
    }

    // Back 버튼 클릭 시 호출되는 함수
    public void OnBackButtonClick()
    {
        Debug.Log("Back button clicked");
        menuPanel.SetActive(true); // 메뉴 패널 활성화
        menu2Panel.SetActive(false); // 메뉴2 패널 비활성화
    }

    // X 버튼 클릭 시 호출되는 함수
    public void OnXButtonClick()
    {
        Debug.Log("X button clicked");
        menuPanel.SetActive(false); // 메뉴 패널 비활성화
        menu2Panel.SetActive(false); // 메뉴2 패널 비활성화
    }

    // 메뉴 패널을 켜는 함수
    public void MenuPanelOn()
    {
        menuPanel.SetActive(true);
    }

    // 메뉴 패널을 끄는 함수
    public void MenuPanelOff()
    {
        menuPanel.SetActive(false);
    }
}
