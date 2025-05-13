using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceController : MonoBehaviour
{
    // ĵ������ �ִ� �̹������� inspertor���� �Ҵ�
    public Image[] images;

    // �ٲ� �÷� �̹��� ��������Ʈ
    public Sprite[] colorSprites;

    // ���� ������ ���� ��
    private int collectedPieces = 0;

    public KeyControl Kc;
    public GameObject Box;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // "Piece" �±׸� ���� ������Ʈ�� �浿�ߴ��� Ȯ��
        if (other.CompareTag("Piece"))
        {
            //�浹�� ������Ʈ �ı�
            Destroy(other.gameObject);

            // �浹 ������ ���� �ش� �̹����� �÷��� ����
            if (collectedPieces < images.Length)
            {
                // �̹��� ��������Ʈ�� �÷� �������� ��ü
                images[collectedPieces].sprite = colorSprites[collectedPieces];

                // ������ ���� �� ����
                collectedPieces++;

                Debug.Log("���� ����: " + collectedPieces + "/" + images.Length);
                if (collectedPieces >= 4)
                {
                    // 
                    Debug.Log("���� �Ϸ�");
                    Kc.randomkey();
                    Box.SetActive(false);
                }
            }
        }
    }
}
