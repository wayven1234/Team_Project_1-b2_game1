using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;      // 계속하기 버튼 연결


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
        LoadNextScene3(); // 다음 씬 로드
    }

    private void LoadNextScene3()
    {
        // 다음 씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
