using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class KidController : MonoBehaviour
{
    // 추격할 대상
    public Transform player;

    // 이동 속도
    public float moveSpeed = 3.5f;

    // 물리 기반 이동을 위한 Rigifbody2D
    private Rigidbody2D rb;

    // 경로 재계산 간격
    public float pathUpdateInterval = 0.5f;

    [SerializeField] Transform traget;

    private NavMeshAgent agent;

    private Animator animator;

    // 현재 애니메이션 상태
    private string currentState = "Kid_right";   // 기본 상태를 오른쪽

    // 애니메이션 상태 함수
    private const string KID_FRONT = "Kid_front";
    private const string KID_BACK = "Kid_back";
    private const string KID_LEFT = "Kid_left";
    private const string KID_RIGHT = "Kid_right";

    private Vector3 room1KidPosition = new Vector3(4.07f, -4.65f, 0f);

    private Vector3 room2KidPosition = new Vector3(4.07f, -7.55f, 0f);

    // 이동 방향 계산
    private Vector3 previousPosition;

    // 경로 업데이트 타이머
    private float pathUpdateTimer = 0;

    void Start()
    {
        // Rigidbody2D 컴포넌트 가져오기
        rb = GetComponent<Rigidbody2D>();

        // 만약 Rigidbody2D가 없다면 경고 메시지 출력
        if (rb == null)
        {
            Debug.LogError("Kid 오브젝트에 Rigidbody2D 컴포넌트가 없습니다!");
        }
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        // 플레이어 참조가 없으면 씬에서 찾기
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다.");
            }
        }

        // NawMeshAgent 컴포넌트 가져오기
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        // NavMeshAgent 설정
        agent.speed = moveSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        agent.stoppingDistance = 1f;
        agent.autoBraking = true;
        agent.updatePosition = true;
        agent.updateRotation = false;

        // 2D 게임에서 NavMeshAgent를 사용하기 위한 설정
        agent.updateUpAxis = false;

        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("kid 오브젝트에 Animator 컴포넌트가 없습니다!");
        }

        // 이전 위치 초기화
        previousPosition = transform.position;
    }

    void Update()
    {
        if (player == null)
            return;

        // 경로 업데이트 타이머 증가
        pathUpdateTimer += Time.deltaTime;

        // 일정 간격으로 경로 재계산
        if (pathUpdateTimer >= pathUpdateInterval)
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Debug.LogWarning("NavMeshAgent가 NavMesh 위에 있지 않습니다!");
            }
            pathUpdateTimer = 0f;
        }

        // 현재 이동 방향 계산 (현재 위치 - 이전 위치)
        Vector3 movementDirection = transform.position - previousPosition;

        // 이동량이 충분히 클 때만 방향 변경 및 애니메이션 처리
        if (movementDirection.magnitude > 0.01f)
        {
            // 이동 방향에 따라 애니메이션 상태 변경
            UpdateAnimationBasedOnMovement(movementDirection);
        }

        // 이전 위치 갱신
        previousPosition = transform.position;
    }

    // 이동 방향에 따라 애니메이션 상태 업데이트
    void UpdateAnimationBasedOnMovement(Vector3 movementDirection)
    {
        // 이동 방향 정규화
        movementDirection.Normalize();

        // 수평, 수직 이동 값 분리
        float horizontalMovement = movementDirection.x;
        float verticalMovement = movementDirection.y;

        // 수평 이동이 수직 이동보다 크면 좌우 애니메이션, 그렇지 않으면 상하 애니메이션
        if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
        {
            // 좌우 이동
            if (horizontalMovement > 0)
            {
                // 오른쪽 이동
                ChangeAnimationState(KID_RIGHT);
            }
            else
            {
                // 왼쪽 이동
                ChangeAnimationState(KID_LEFT);
            }
        }
        else
        {
            // 상하 이동
            if (verticalMovement > 0)
            {
                // 위쪽 이동
                ChangeAnimationState(KID_BACK);
            }
            else
            {
                // 아래쪽 이동
                ChangeAnimationState(KID_FRONT);
            }
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Room_1_InDoor 태그를 가진 오브젝트와 충돌했을 때
        if (other.CompareTag("Room_1_OutDoor"))
        {
            // Kid 위치 이동
            transform.position = room2KidPosition;
        }
        // Room_1_OutDoor 태그를 가진 오브젝트와 충돌했을 때
        else if (other.CompareTag("Room_1_InDoor"))
        {
            // Kid 위치 이동
            transform.position = room1KidPosition;
        }
    }
}
