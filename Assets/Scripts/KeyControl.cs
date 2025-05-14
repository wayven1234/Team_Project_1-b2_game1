using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class KeyControl : MonoBehaviour
{
    // 벽 오브젝트(열쇠를 획득하면 비활성화 할 오브젝트)
    public GameObject wallobject;

    // 엔딩 지점 오브젝트(열쇠를 획득하면 활성화할 오브젝트)
    public GameObject EndingSpot;

    // 생성할 열쇠 프리팹
    public GameObject Keyprefab;

    // 열쇠가 생성될 첫 번째 위치
    public Transform Keyspawnpoint1;
    // 열쇠가 생성될 두 번째 위치
    public Transform Keyspawnpoint2;


    // 열쇠를 랜덤 위치에 생성하는 함수
    public void randomkey()
    {
        // 0 또는 1 중 랜덤 인덱스 생성
        int randomindex = Random.Range(0, 2);

        // 랜덤 인덱스에 따라 생성 위치 선택
        Transform chosenPoint = (randomindex == 0) ? Keyspawnpoint1 : Keyspawnpoint2;

        // 선택된 위치에 열쇠 프리팹 생성
        Instantiate(Keyprefab, chosenPoint.position,chosenPoint.rotation);

        Destroy(wallobject);

    }

    // 게임 시작 시 실행되는 함수
    void Start()
    {
        // 엔딩 지점 오브젝트를 비활성화
        EndingSpot.SetActive(false);
    }

    // 트리거에 진입했을 때 호출되는 함수 (열쇠가 충돌 감지)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Key"인지 확인
        if (collision.CompareTag("Key"))
        {
            // 엔딩 지점 오브젝트 활성화
            EndingSpot.SetActive(true);

            // 콘솔에 열쇠 획득 메시지 출력
            Debug.Log("열쇠를 획득했습니다");

           // 열쇠 오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
