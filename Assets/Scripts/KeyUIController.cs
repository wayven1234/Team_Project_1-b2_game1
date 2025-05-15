using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIController : MonoBehaviour
{
        // 캔버스에 있는 이미지들을 inspector에서 할당
    public Image[] images;

    // 바꿀 컬러 이미지 스프라이트
    public Sprite[] colorSprites;

    // 현재 수집한 열쇠 수
    private int collectedKeys = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // "Key" 태그를 지닌 오브젝트와 충돌했는지 확인
        if (other.CompareTag("Key"))
        {
            // 충돌한 오브젝트 파괴
            Destroy(other.gameObject);
            // 충돌 순서에 따라 해당 이미지를 컬러로 변경
            if (collectedKeys < images.Length)
            {
                // 이미지 스프라이트를 컬러 버전으로 교체
                images[collectedKeys].sprite = colorSprites[collectedKeys];
                // 수집한 열쇠 수 증가
                collectedKeys++;
                // 현재까지 수집한 열쇠 개수와 전체 이미지 개수를 로그로 출력
                Debug.Log("열쇠 수집: " + collectedKeys + "/" + images.Length);
                if (collectedKeys >= 1)
                {
                    // 수집 완료 로그 출력
                    Debug.Log("열쇠 수집 완료");
                }
            }
        }
    }
}
