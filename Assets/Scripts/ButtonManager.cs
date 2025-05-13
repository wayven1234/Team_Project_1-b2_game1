using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "Room_1";   // ������ �ε��� ��
    [SerializeField] private Button startButton;         // ���� ��ư ����
    [SerializeField] private Button menuButton;         // �޴� ��ư ����
    [SerializeField] private GameObject menuPanel;        // �޴� �г� ����

    [SerializeField] private bool startPanelInactive = true;    // ���� �� �г� ��Ȱ��ȭ

    private void Start()
    {
        if (startButton == null)
        {
            startButton = GetComponent<Button>();
        }
        startButton.onClick.AddListener(OnStartButtonClick);

        // ������Ʈ�� Inspector�� �Ҵ���� �ʾҴٸ� �ڵ����� ã��
        if (menuButton == null)
        {
            menuButton = GetComponent<Button>();
        }

        // ���� �� �г� ���� ����
        if (menuButton != null && startPanelInactive)
        {
            menuPanel.SetActive(false); // ��Ȱ��ȭ
        }
    }

    // ���� ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnStartButtonClick()
    {
        Debug.Log("Start button clicked");
        LoadNextScene();
    }

    // ���� ���� �ε��ϴ� �Լ�
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
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
