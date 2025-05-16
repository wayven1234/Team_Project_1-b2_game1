using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceController : MonoBehaviour
{
    // 캔버스에 있는 이미지들을 inspertor에서 할당
    public Image[] images;

    // 바꿀 컬러 이미지 스프라이트
    public Sprite[] colorSprites;

    // 현재 수집한 조각 수
    private int collectedPieces = 0;

    public KeyControl Kc;
    public GameObject Box;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // "Piece" 태그를 지닌 오브젝트와 충동했는지 확인
        if (other.CompareTag("Piece"))
        {
            //충돌한 오브젝트 파괴
            Destroy(other.gameObject);

            // 충돌 순서에 따라 해당 이미지를 컬러로 변경
            if (collectedPieces < images.Length)
            {
                // 이미지 스프라이트를 컬러 버전으로 교체
                images[collectedPieces].sprite = colorSprites[collectedPieces];

                // 수집한 조각 수 증가
                collectedPieces++;

                // 현재까지 수집한 조각 개수와 전체 이미지 개수를 로그로 출력
                Debug.Log("조각 수집: " + collectedPieces + "/" + images.Length);
                if (collectedPieces >= 4)
                {
                    // 수집 완료 로그 출력
                    Debug.Log("수집 완료");

                    // KeyControl 스크립트의 randomkey() 함수 호출 (열쇠를 랜덤 위치에 생성)
                    Kc.randomkey();

                    Destroy(Box);
                }
            }
        }
    }
}