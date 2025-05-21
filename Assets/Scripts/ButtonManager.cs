using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "TimeLine";   // ������ �ε��� ��
    [SerializeField] private Button startButton;         // ���� ��ư ����
    [SerializeField] private Button menuButton;         // �޴� ��ư ����
    [SerializeField] private Button nextButton;         // ���� ��ư ����
    [SerializeField] private Button backButton;         // �ڷΰ��� ��ư ����
    [SerializeField] private Button xButton;            // X ��ư ����
    [SerializeField] private GameObject menuPanel;        // �޴� �г� ����
    [SerializeField] private GameObject menu2Panel;       // �޴�2 �г� ����

    [SerializeField] private bool startPanelInactive = true;    // ���� �� �г� ��Ȱ��ȭ

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

        // ������Ʈ�� Inspector�� �Ҵ���� �ʾҴٸ� �ڵ����� ã��
        if (menuButton == null)
        {
            menuButton = GetComponent<Button>();
        }

        // ���� �� �г� ���� ����
        if (menuPanel != null && startPanelInactive)
        {
            menuPanel.SetActive(false); // ��Ȱ��ȭ
        }

        // ���� �� �г� ���� ����
        if (menu2Panel != null && startPanelInactive)
        {
            menu2Panel.SetActive(false); // ��Ȱ��ȭ
        }
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene(); // ���� �� �ε�
    }

    // ���� ���� �ε��ϴ� �Լ�
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // �޴� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnNextButtonClick()
    {
        Debug.Log("Next button clicked");
        menuPanel.SetActive(false); // �޴� �г� ��Ȱ��ȭ
        menu2Panel.SetActive(true); // �޴�2 �г� Ȱ��ȭ
    }

    // Back ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnBackButtonClick()
    {
        Debug.Log("Back button clicked");
        menuPanel.SetActive(true); // �޴� �г� Ȱ��ȭ
        menu2Panel.SetActive(false); // �޴�2 �г� ��Ȱ��ȭ
    }

    // X ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnXButtonClick()
    {
        Debug.Log("X button clicked");
        menuPanel.SetActive(false); // �޴� �г� ��Ȱ��ȭ
        menu2Panel.SetActive(false); // �޴�2 �г� ��Ȱ��ȭ
    }

    // �޴� �г��� �Ѵ� �Լ�
    public void MenuPanelOn()
    {
        menuPanel.SetActive(true);
    }

    // �޴� �г��� ���� �Լ�
    public void MenuPanelOff()
    {
        menuPanel.SetActive(false);
    }
}
