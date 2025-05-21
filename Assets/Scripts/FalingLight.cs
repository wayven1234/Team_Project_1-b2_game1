using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalingLight : MonoBehaviour
{
    // ������ ���� ������ ����
    public GameObject FallingLightprefab;

    // ������Ʈ�� ������ �� y���� ���� �������� �󸶳� ����� �����ϴ� ����
    public float spawnHeightOffset = 1.5f;

    
    // ������Ʈ�� �����Ǽ� �������� �ɸ��� �ð�
    public float dropduration = 0.2f;

    // Ʈ���ſ� �������� �� ȣ��Ǵ� �Լ� (���谡 �浹 ����)
    private void OnTriggerEnter2D(Collider2D collision)

    {
        // �浹�� ������Ʈ�� �±װ� "FallingLight"���� Ȯ��
        if (collision.CompareTag("FallingLight"))
        {
            // �ֿܼ� �÷��̾ �������ٰ� �޽��� ���
            Debug.Log("�÷��̾ ���������ϴ�.");

            // �ν��� ������Ʈ�� ��ǥ�� contactPoint�� �ִ´�
            Vector2 contactPoint = collision.transform.position;

            // contactPoint���� spawnHeightOffset���� ���̸� ���� ��ŭ ����
            Vector3 startPosition = new Vector3(contactPoint.x, contactPoint.y + spawnHeightOffset, 0f);

            // contactPoint�� ���� ��ġ
            Vector3 endPosition = new Vector3(contactPoint.x, contactPoint.y , 0f);

            // �ڷ�ƾ�� �����Ͽ� ������Ʈ�� ����߸�
            StartCoroutine(SmoothDrop(startPosition, endPosition, dropduration));

            SoundManager.Instance.Play("���� �ν����� �Ҹ�");
        }
    }

    // ������Ʈ�� �ε巴�� ����߸��� �ڷ�ƾ �Լ�
    private IEnumerator SmoothDrop(Vector3 from, Vector3 to, float duration)
    {
        yield return new WaitForSeconds(0.7f);

        // �������� ���� ��ġ�� ����
        GameObject spawned = Instantiate(FallingLightprefab, from, Quaternion.identity);
        float elapsed = 0f; // 

        // duration(������ �ð�) ���� �ݺ�
        while (elapsed < duration)
        {
            // Lerp�� �̿��� ���� ��ġ�� ����Ͽ� �̵�
            spawned.transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime; // ��� �ð� ����
            yield return null; // ���� �����ӱ��� ���
        }
        spawned.transform.position = to; // ������ ��ġ�� ��Ȯ�ϰ� ��ǥ ������ ����

        Destroy(spawned);
    }
}
