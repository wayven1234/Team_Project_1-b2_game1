using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;      // ����ϱ� ��ư ����


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
        LoadNextScene2(); // ���� �� �ε�
    }

    private void LoadNextScene2()
    {
        // ���� �� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("Room_1");
    }
}
