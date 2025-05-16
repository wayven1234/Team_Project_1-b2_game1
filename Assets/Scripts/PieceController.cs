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

                // ������� ������ ���� ������ ��ü �̹��� ������ �α׷� ���
                Debug.Log("���� ����: " + collectedPieces + "/" + images.Length);
                if (collectedPieces >= 4)
                {
                    // ���� �Ϸ� �α� ���
                    Debug.Log("���� �Ϸ�");

                    // KeyControl ��ũ��Ʈ�� randomkey() �Լ� ȣ�� (���踦 ���� ��ġ�� ����)
                    Kc.randomkey();

                    Destroy(Box);
                }
            }
        }
    }
}