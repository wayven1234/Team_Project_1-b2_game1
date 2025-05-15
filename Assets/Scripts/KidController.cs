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
    public float moveSpeed = 3f;

    // 게임 오버 패널
    public GameObject gameoverPanel;

    public GameObject helloPanel;

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

    // 이동 방향 계산
    private Vector3 previousPosition;

    // 경로 업데이트 타이머
    private float pathUpdateTimer = 0;

    // 추격 활성화 상태
    private bool isChasing = false;

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
        agent.autoBraking = false;
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

        // 회전값 초기화 (회전 방지)
        transform.rotation = Quaternion.identity;

        // 처음에는 추격 비활성화 (Player가 점에 트리거되기 전까지)
        DisableChasing();
    }

    void Update()
    {
        if (!isChasing || player == null)
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
       
        // "Player" 태그를 지닌 오브젝트와 충동했는지 확인
        if (other.CompareTag("Player"))
        {
            Hello(); // 인사 패널 표시 함수 호출

            // 게임 오버 패널이 할당되어 있다면
            if (gameoverPanel != null)
            {
                gameoverPanel.SetActive(true); // 게임 오버 패널 활성화
            }
        }
    }

    // 인사 패널을 2초간 보여주는 함수
    public void Hello()
    {
        helloPanel.SetActive(true); // 인사 패널 활성화
        Invoke("HidePanel", 2f); // 2초 후 HidePanel 함수 호출
    }

    // 인사 패널을 숨기는 함수
    public void HidePanel()
    {
        helloPanel.SetActive(false); // 인사 패널 비활성화
    }

    // 추격 활성화 함수

    public void EnableChasing()
    {
        isChasing = true; // 추격 상태로 전환
        agent.isStopped = false; // 에이전트 이동 재개
    }

    // 추격 비활성화 함수
    public void DisableChasing()
    {
        isChasing = false; // 추격 상태 해제

        // agent가 null이 아니고 활성화되어 있으면
        if (agent != null && agent.isActiveAndEnabled) 
        {
            agent.isStopped = true; // 에이전트 이동 정지
        }
    }
}
