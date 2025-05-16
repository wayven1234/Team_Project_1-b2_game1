using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;



public class KeyControl : MonoBehaviour
{
    // �� ������Ʈ(���踦 ȹ���ϸ� ��Ȱ��ȭ �� ������Ʈ)
    public GameObject wallobject;

    // ���� ���� ������Ʈ(���踦 ȹ���ϸ� Ȱ��ȭ�� ������Ʈ)
    public GameObject EndingSpot;

    // ���� ������Ʈ 
    public GameObject FallingLigh;

    // ������ ���� ������
    public GameObject Keyprefab;

    // ���谡 ������ ù ��° ��ġ
    public Transform Keyspawnpoint1;
    // ���谡 ������ �� ��° ��ġ
    public Transform Keyspawnpoint2;
    // ���谡 ������ �� ��° ��ġ
    public Transform Keyspawnpoint3;


    // ���踦 ���� ��ġ�� �����ϴ� �Լ�
    public void randomkey()
    {
        // 0 �Ǵ� 1 �� ���� �ε��� ����
        int randomindex = Random.Range(0, 3);

        // ���� �ε����� ���� ���� ��ġ ����
        Transform chosenPoint;
        if (randomindex == 0)
        {
            chosenPoint = Keyspawnpoint1;
        }
        else if (randomindex == 1)
        {
            chosenPoint = Keyspawnpoint2;
        }
        else
        {
            chosenPoint = Keyspawnpoint3;
        }

        // ���õ� ��ġ�� ���� ������ ����
        Instantiate(Keyprefab, chosenPoint.position,chosenPoint.rotation);
        

        Destroy(wallobject);

    }

    // ���� ���� �� ����Ǵ� �Լ�
    void Start()
    {
        // ���� ���� ������Ʈ�� ��Ȱ��ȭ
        EndingSpot.SetActive(false);
    }

    // Ʈ���ſ� �������� �� ȣ��Ǵ� �Լ� (���谡 �浹 ����)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Key"���� Ȯ��
        if (collision.CompareTag("Key"))
        {
            // ���� ���� ������Ʈ Ȱ��ȭ
            EndingSpot.SetActive(true);

            // ���� ���� Ȱ��ȭ
            FallingLigh.SetActive(true);

            // �ֿܼ� ���� ȹ�� �޽��� ���
            Debug.Log("���踦 ȹ���߽��ϴ�");

           // ���� ������Ʈ ����
            Destroy(collision.gameObject);
        }
    }
}
