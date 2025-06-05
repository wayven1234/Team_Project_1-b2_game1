using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WinController : MonoBehaviour
{
    [SerializeField] private GameObject winPanel; // 성공 패널 연결

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
            SoundManager.Instance.Stop("배경음");
            SoundManager.Instance.Stop("아이 웃음 소리");
            winPanel.SetActive(true); // 성공 패널 
        }
    }
}
