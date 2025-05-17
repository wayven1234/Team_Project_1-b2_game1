using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;      // ����ϱ� ��ư ����


    void Start()
    {
        if (exitButton == null)
        {
            exitButton = GetComponent<Button>();
        }
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public void OnExitButtonClick()
    {
        Debug.Log("Exit button clicked");
        LoadNextScene3(); // ���� �� �ε�
    }

    private void LoadNextScene3()
    {
        // ���� �� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
