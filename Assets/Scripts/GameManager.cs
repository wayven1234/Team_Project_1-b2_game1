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
    [SerializeField] private string nextSceneName2 = "MainScene";
    [SerializeField] private GameObject gameOverPanel;        // ���� ���� �г� ����
    [SerializeField] private GameObject helloPanel;             // �Ű�� �г� ����

    [SerializeField] private Image surpriseImage;           // �Ű�� �̹��� ����
    private Vector3 surpriseImageOriginalSize;              // ���� ũ��
    private Vector3 surpriseImageEnlargedSize;              // Ȯ��� ũ��
    private float surpriseImageScaleFactor = 5.0f;          // Ȯ�� ����
    private float gameoverDelay = 1f;                       // ���� ���� �г��� Ȱ��ȭ �Ǳ� ������ ���� �ð�
    private float surpriseDelay = 0.2f;                     // �̹����� Ŀ���� ������ ���� �ð�


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

        // ���� �� �г� ���� ����
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); // ��Ȱ��ȭ
        
        // ���� �� �г� ���� ����
        if (helloPanel != null)
            helloPanel.SetActive(false); // ��Ȱ��ȭ


        // ��ư���� ���� ũ�⸦ ����
        yesOriginalSize = yesBtn.transform.localScale;
        noOriginalSize = yesBtn.transform.localScale;

        // �ִ�� Ŀ�� ��ư ũ�� ���
        yesEnlargedSize = yesOriginalSize *  scaleFactor;
        noEnlargedSize = yesOriginalSize * scaleFactor;

        // �̺�Ʈ Ʈ���� ������Ʈ �߰� �� �̺�Ʈ ���
        AddButtonEvents(yesBtn, noBtn);
        AddButtonEvents(noBtn, yesBtn);

        // �Ű�� �̹��� ũ�� ����
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

    public void ShowGameOverPanel()
    {
        // �Ű�� �̹����� ������ ó���� �۰� ����(�⺻)
        if (surpriseImage != null)
        {
            surpriseImage.transform.localScale = surpriseImageOriginalSize;

            // ���� �ð� �� �̹��� ũ�⸦ ���ڱ� Ȯ��
            StartCoroutine(EnlargeSurpriseImage());
        }
    }

    // �̹����� ���ڱ� Ȯ���ϴ� �ڷ�ƾ
    private IEnumerator EnlargeSurpriseImage()
    {
        // ������ ���� �ð���ŭ ���
        yield return new WaitForSeconds(surpriseDelay);

        // �̹��� ũ�⸦ ���ڱ� Ȯ Ű��
        surpriseImage.transform.localScale = surpriseImageEnlargedSize;

        // �ణ�� ��鸲 ȿ�� �߰�
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

        // ���� ��ġ�� ����
        surpriseImage.transform.localPosition = originalPosition;

        // ������ ���� �ð���ŭ ��� �� ���� ���� �г� ǥ��
        yield return new WaitForSeconds(gameoverDelay);

        // ���� ���� �г� Ȱ��ȭ
        if (gameOverPanel != null)
        {
            SoundManager.Instance.Pause("�����");
            gameOverPanel.SetActive(true);
        }

        // �Ű�� �г� ��Ȱ��ȭ
        helloPanel.SetActive(false);

    }
}
