using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어 이동 속도
    public float moveSpeed = 5f;

    //원래 이동 속도를 저장할 변수
    private float originalMoveSpeed;

    // kidController 참조
    public KidController kidController;

    // 물리 기반 이동을 위한 Rigifbody2D
    private Rigidbody2D rb;

    // 애니메이터 컴포넌트 참조
    private Animator animator;

    private Camera mainCamera;

    // 현재 플레이어 상태를 나타내는 변수
    private string currentState = "Player_front";   // 기본 상태를 정면으로 설정

    // 애니메이션 상태 함수
    private const string PLAYER_FRONT = "Player_front";
    private const string PLAYER_BACK = "Player_back";
    private const string PLAYER_LEFT = "Player_left";
    private const string PLAYER_RIGHT = "Player_right";

    private Vector3 room2PlayerPosition = new Vector3(4.07f, -7.55f, 0f);
    private Vector3 room2CameraPosition = new Vector3(8.83f, -10.18f, -10f);

    private Vector3 room1PlayerPosition = new Vector3(4.07f, -4.65f, 0f);
    private Vector3 room1CameraPosition = new Vector3(0f, 0f, -10f);

    private Vector3 room3_2PlayerPosition = new Vector3(12.9f, -17.6f, 0f);
    private Vector3 room3_2CameraPosition = new Vector3(7.3f, -20.28f, -10f);

    private Vector3 room2_3PlayerPosition = new Vector3(12.9f, -14.8f, 0f);
    private Vector3 room2_3CameraPosition = new Vector3(8.83f, -10.18f, -10f);

    private Vector3 room3PlayerPosition = new Vector3(15.18f, -23.62f, 0f);
    private Vector3 room3CameraPosition = new Vector3(7.3f, -20.2847f, -10f);

    private Vector3 room3_4PlayerPosition = new Vector3(17.35f, -23.79f, 0f);
    private Vector3 room3_4CameraPosition = new Vector3(25.1f, -23.2096f, -10f);


    // 느려질 이동 속도
    public float slowedSpeed = 2.5f;
    // 느려지는 지속 시간
    public float slowDuration = 3f;

    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();

        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;

        // 원래 이동 속도 저장
        originalMoveSpeed = moveSpeed;

        // 만약 Rigidbody2D가 없다면 경고 메시지 출력
        if(rb == null)
        {
            Debug.LogError("Player 오브젝트에 Rigidbody2D 컴포넌트가 없습니다!");
        }

        if(mainCamera == null)
        {
            Debug.LogError("씬에 메인 카메라가 없습니다!");
        }

        // KIdController가 연결되어 있지 않다면 찾기
        if(kidController == null)
        {
            // "Kid" 태그를 지닌 오브젝트 찾기
            GameObject kidObject = GameObject.FindGameObjectWithTag("Kid");
            if(kidObject != null)
            {
                kidController = kidObject.GetComponent<KidController>();
            }
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
    }

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

    //트리거 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Room_1_OutDoor 태그를 가진 오브젝트와 충돌했을 때
        if (other.CompareTag("Room_1_OutDoor"))
        {
            // 플레이어 위치 이동
            transform.position = room2PlayerPosition;

            // 카메라 위치 이동
            if (mainCamera != null)
            {
                mainCamera.transform.position = room2CameraPosition;
            }
        }
        // Room_1_InDoor 태그를 가진 오브젝트와 충돌했을 때
        else if (other.CompareTag("Room_1_InDoor"))
        {
            // 플레이어 위치 이동
            transform.position = room1PlayerPosition;

            // 카메라 위치 이동
            if (mainCamera != null)
            {
                mainCamera.transform.position = room1CameraPosition;
            }
        }
        // Room_2_OutDoor 태그를 가진 오브젝트와 충돌했을 때
        else if (other.CompareTag("Room_2_OutDoor"))
        {
            //플레이어 위치 이동
            transform.position = room3_2PlayerPosition;

            if (mainCamera != null)
            {
                mainCamera.transform.position = room3_2CameraPosition;
            }
        }
        // Room_2_InDoor 태그를 가진 오브젝트와 충돌했을 때
        else if (other.CompareTag("Room_2_InDoor"))
        {
            // 플레이어 위치 이동
            transform.position = room2_3PlayerPosition;

            // 카메라 위치 이동
            if (mainCamera != null) 
            {
                mainCamera.transform.position = room2_3CameraPosition;
            }
        }



        // Room_3_OutDoor 태그를 가진 오브젝트와 충돌했을 때
        else if (other.CompareTag("Room_3_OutDoor"))
        {
            // 플레이어 위치 이동
            transform.position = room3_4PlayerPosition;

            // 카메라 위치 이동
            if (mainCamera != null)
            {
                mainCamera.transform.position = room3_4CameraPosition;
            }
        }

        // Room_3_InDoor 태그를 가진 오브젝트와 충돌했을 때
        else if (other.CompareTag("Room_3_InDoor"))
        {
            // 플레이어 위치 이동
            transform.position = room3PlayerPosition;

            // 카메라 위치 이동
            if (mainCamera != null)
            {
                mainCamera.transform.position = room3CameraPosition;
            }
        }    

        // Scissor 태그를 가진 오프젝트와 충돌했을 때
        else if (other.CompareTag("Scissor"))
        {
            // 가위 아이템 제거
            Destroy(other.gameObject);

            // 이동 속도 감소
            SlowPlayerDown();
        }
        // "Warning" 태그를 지닌 오브젝트와 충돌했을 때
        else if (other.CompareTag("Warning"))
        {
            if (kidController != null)
            {
                kidController.EnableChasing();
            }
        }
    }

    // 플레이어 속도를 감소시키는 함수
    private void SlowPlayerDown()
    {
        // 이미 실행 중인 코루틴이 있다면 중지
        StopAllCoroutines();

        // 이동 속도 감소
        moveSpeed = slowedSpeed;

        StartCoroutine(RestoreSpeed());
    }
    // 일정 시간 후에 속도를 원래대로 복구하는 코루틴
    private IEnumerator RestoreSpeed()
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(slowDuration);

        // 이동 속도 복구
        moveSpeed = originalMoveSpeed;
    }
}
