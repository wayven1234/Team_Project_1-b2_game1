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

    private Vector3 yesOriginalSize;  //yes ��ư�� ���� ũ��
    private Vector3 noOriginalSize;  //no ��ư�� ���� ũ��
    private Vector3 yesEnlargedSize;  //yes ��ư�� Ŀ�� ũ��
    private Vector3 noEnlargedSize;    //no ��ư�� Ŀ�� ũ��

    private float scaleFactor = 1.2f;   // ��ư�� Ŀ���� ������ �����ϴ�

    [SerializeField] private string nextSceneName = "Room_1";
    [SerializeField] private GameObject gameOverPanel;        // ���� ���� �г� ����


    void Start()
    {
       
        if (yesBtn == null)
        {
            yesBtn = GetComponent<Button>();
        }
        yesBtn.onClick.AddListener(OnyesButtonClick);

        // ���� �� �г� ���� ����
        gameOverPanel.SetActive(false); // ��Ȱ��ȭ


        // ��ư���� ���� ũ�⸦ ����
        yesOriginalSize = yesBtn.transform.localScale;
        noOriginalSize = yesBtn.transform.localScale;

        // �ִ�� Ŀ�� ��ư ũ�� ���
        yesEnlargedSize = yesOriginalSize *  scaleFactor;
        noEnlargedSize = yesOriginalSize * scaleFactor;

        // �̺�Ʈ Ʈ���� ������Ʈ �߰� �� �̺�Ʈ ���
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
        // �̺�Ʈ Ʈ���� ������Ʈ �������� (������ �ڵ����� �߰�)
        EventTrigger trigger = targetButton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = targetButton.gameObject.AddComponent<EventTrigger>();

        // ������ ���� �̺�Ʈ (���콺�� �÷��� ������)
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) =>
        {
            // Ÿ�� ��ư Ȯ��
            if (targetButton == yesBtn)
                targetButton.transform.localScale = yesEnlargedSize;
            else
                targetButton.transform.localScale = noEnlargedSize;

            // �ٸ� ��ư�� ��� (���� ũ���)
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
            // �� ��ư �� ��� ��ư���� ���콺�� �÷��� ���� ������
        });
        trigger.triggers.Add(entryExit);
    }
}
