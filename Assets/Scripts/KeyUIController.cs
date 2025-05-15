using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIController : MonoBehaviour
{
        // ĵ������ �ִ� �̹������� inspector���� �Ҵ�
    public Image[] images;

    // �ٲ� �÷� �̹��� ��������Ʈ
    public Sprite[] colorSprites;

    // ���� ������ ���� ��
    private int collectedKeys = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // "Key" �±׸� ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        if (other.CompareTag("Key"))
        {
            // �浹�� ������Ʈ �ı�
            Destroy(other.gameObject);
            // �浹 ������ ���� �ش� �̹����� �÷��� ����
            if (collectedKeys < images.Length)
            {
                // �̹��� ��������Ʈ�� �÷� �������� ��ü
                images[collectedKeys].sprite = colorSprites[collectedKeys];
                // ������ ���� �� ����
                collectedKeys++;
                // ������� ������ ���� ������ ��ü �̹��� ������ �α׷� ���
                Debug.Log("���� ����: " + collectedKeys + "/" + images.Length);
                if (collectedKeys >= 1)
                {
                    // ���� �Ϸ� �α� ���
                    Debug.Log("���� ���� �Ϸ�");
                }
            }
        }
    }
}
