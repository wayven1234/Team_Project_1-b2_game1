using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;      // 계속하기 버튼 연결


    void Start()
    {
        if (continueButton == null)
    {
        continueButton = GetComponent<Button>();
    }
    continueButton.onClick.AddListener(OnContinueButtonClick);    
    }

    public void OnContinueButtonClick()
    {
        Debug.Log("Continue button clicked");
        LoadNextScene2(); // 다음 씬 로드
    }

    private void LoadNextScene2()
    {
        // 다음 씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("Room_1");
    }
}
