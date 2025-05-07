using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    public float moveSpeed = 5f;

    // 물리 기반 이동을 위한 Rigifbody2D
    private Rigidbody2D rb;

    // 애니메이터 컴포넌트 참조
    private Animator animator;

    // 현재 플레이어 상태를 나타내는 변수
    private string currentState = "Player_front";   // 기본 상태를 정면으로 설정

    // 애니메이션 상태 함수
    private const string PLAYER_FRONT = "Player_front";
    private const string PLAYER_BACK = "Player_back";
    private const string PLAYER_LEFT = "Player_left";
    private const string PLAYER_RIGHT = "Player_right";


    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();

        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 만약 Rigidbody2D가 없다면 경고 메시지 출력
        if(rb == null)
        {
            Debug.LogError("Player 오브젝트에 Rigidbody2D 컴포넌트가 없습니다!");
        }
    }

    void Update()
    {
        // 움직임을 위한 변수 초기화
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // 화살표 키 대신 직접 키를 확인 (WASD로만 움직임)
        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -1f;
            ChangeAnimationState(PLAYER_LEFT);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 1f;
            ChangeAnimationState(PLAYER_RIGHT);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1f;
            ChangeAnimationState(PLAYER_BACK);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1f;
            ChangeAnimationState(PLAYER_FRONT);
        }

            // 이동 방향 개선
            Vector3 movement = new Vector3 (horizontalInput, verticalInput, 0f);

        // 이동 Vector 정규화 (대각선 이동속도 조절)
        if (movement.magnitude > 0)
        {
            movement.Normalize();
        }

        // 플레이어 이동
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // 애니메이션 상태를 안전하게 변경하는 함수
        void ChangeAnimationState(string newState)
        {
            // 현재 재생 중인 애니메이션을 다시 재생하려고 하면 리턴
            if (currentState == newState) return;

            // 새로운 애니메이션 재생
            animator.Play(newState);

            // 현재 상태 업데이트
            currentState = newState;
        }
    }
}
