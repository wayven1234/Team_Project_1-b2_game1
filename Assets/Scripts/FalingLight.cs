using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalingLight : MonoBehaviour
{
    // 생성할 조명 프리펩 변수
    public GameObject FallingLightprefab;

    // 오브젝트가 생성될 때 y축의 높이 방향으로 얼마나 띄울지 결정하는 변수
    public float spawnHeightOffset = 1.5f;

    
    // 오브젝트가 생성되서 떨어질때 걸리는 시간
    public float dropduration = 0.2f;

    // 트리거에 진입했을 때 호출되는 함수 (열쇠가 충돌 감지)
    private void OnTriggerEnter2D(Collider2D collision)

    {
        // 충돌한 오브젝트의 태그가 "FallingLight"인지 확인
        if (collision.CompareTag("FallingLight"))
        {
            // 콘솔에 플레이어가 지나갔다고 메시지 출력
            Debug.Log("플레이어가 지나갔습니다.");

            // 인식한 오브젝트의 좌표를 contactPoint에 넣는다
            Vector2 contactPoint = collision.transform.position;

            // contactPoint보다 spawnHeightOffset보다 높이를 더한 만큼 설정
            Vector3 startPosition = new Vector3(contactPoint.x, contactPoint.y + spawnHeightOffset, 0f);

            // contactPoint의 현재 위치
            Vector3 endPosition = new Vector3(contactPoint.x, contactPoint.y , 0f);

            // 코루틴을 실행하여 오브젝트를 떨어뜨림
            StartCoroutine(SmoothDrop(startPosition, endPosition, dropduration));

            SoundManager.Instance.Play("유리 부숴지는 소리");
        }
    }

    // 오브젝트를 부드럽게 떨어뜨리는 코루틴 함수
    private IEnumerator SmoothDrop(Vector3 from, Vector3 to, float duration)
    {
        yield return new WaitForSeconds(0.7f);

        // 프리팹을 시작 위치에 생성
        GameObject spawned = Instantiate(FallingLightprefab, from, Quaternion.identity);
        float elapsed = 0f; // 

        // duration(지정한 시간) 동안 반복
        while (elapsed < duration)
        {
            // Lerp를 이용해 현재 위치를 계산하여 이동
            spawned.transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime; // 경과 시간 누적
            yield return null; // 다음 프레임까지 대기
        }
        spawned.transform.position = to; // 마지막 위치를 정확하게 목표 지점에 맞춤

        Destroy(spawned);
    }
}
