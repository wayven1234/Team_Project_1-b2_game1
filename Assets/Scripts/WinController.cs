using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WinController : MonoBehaviour
{
    [SerializeField] private GameObject winPanel; // ���� �г� ����

    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WinPanelOn"))
        {
            SoundManager.Instance.Pause("�����");
            SoundManager.Instance.Pause("���� ���� �Ҹ�");
            winPanel.SetActive(true); // ���� �г� 
        }
    }
}
