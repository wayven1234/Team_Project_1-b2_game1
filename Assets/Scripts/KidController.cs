using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class KidController : MonoBehaviour
{
    // �߰��� ���
    public Transform player;

    // �̵� �ӵ�
    public float moveSpeed = 3f;

    // ���� ���� �г�
    public GameObject gameoverPanel;

    public GameObject helloPanel;

    // ���� ��� �̵��� ���� Rigifbody2D
    private Rigidbody2D rb;

    // ��� ���� ����
    public float pathUpdateInterval = 0.5f;

    [SerializeField] Transform traget;

    private NavMeshAgent agent;

    private Animator animator;

    // ���� �ִϸ��̼� ����
    private string currentState = "Kid_right";   // �⺻ ���¸� ������

    // �ִϸ��̼� ���� �Լ�
    private const string KID_FRONT = "Kid_front";
    private const string KID_BACK = "Kid_back";
    private const string KID_LEFT = "Kid_left";
    private const string KID_RIGHT = "Kid_right";

    // �̵� ���� ���
    private Vector3 previousPosition;

    // ��� ������Ʈ Ÿ�̸�
    private float pathUpdateTimer = 0;

    // �߰� Ȱ��ȭ ����
    private bool isChasing = false;

    void Start()
    {
        // Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();

        // ���� Rigidbody2D�� ���ٸ� ��� �޽��� ���
        if (rb == null)
        {
            Debug.LogError("Kid ������Ʈ�� Rigidbody2D ������Ʈ�� �����ϴ�!");
        }
        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        // �÷��̾� ������ ������ ������ ã��
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�.");
            }
        }

        // NawMeshAgent ������Ʈ ��������
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }

        // NavMeshAgent ����
        agent.speed = moveSpeed;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        agent.stoppingDistance = 1f;
        agent.autoBraking = false;
        agent.updatePosition = true;
        agent.updateRotation = false;

        // 2D ���ӿ��� NavMeshAgent�� ����ϱ� ���� ����
        agent.updateUpAxis = false;

        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("kid ������Ʈ�� Animator ������Ʈ�� �����ϴ�!");
        }

        // ���� ��ġ �ʱ�ȭ
        previousPosition = transform.position;

        // ȸ���� �ʱ�ȭ (ȸ�� ����)
        transform.rotation = Quaternion.identity;

        // ó������ �߰� ��Ȱ��ȭ (Player�� ���� Ʈ���ŵǱ� ������)
        DisableChasing();
    }

    void Update()
    {
        if (!isChasing || player == null)
            return;

        // ��� ������Ʈ Ÿ�̸� ����
        pathUpdateTimer += Time.deltaTime;

        // ���� �������� ��� ����
        if (pathUpdateTimer >= pathUpdateInterval)
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Debug.LogWarning("NavMeshAgent�� NavMesh ���� ���� �ʽ��ϴ�!");
            }
            pathUpdateTimer = 0f;
        }

        // ���� �̵� ���� ��� (���� ��ġ - ���� ��ġ)
        Vector3 movementDirection = transform.position - previousPosition;

        // �̵����� ����� Ŭ ���� ���� ���� �� �ִϸ��̼� ó��
        if (movementDirection.magnitude > 0.01f)
        {
            // �̵� ���⿡ ���� �ִϸ��̼� ���� ����
            UpdateAnimationBasedOnMovement(movementDirection);
        }

        // ���� ��ġ ����
        previousPosition = transform.position;
    }

    // �̵� ���⿡ ���� �ִϸ��̼� ���� ������Ʈ
    void UpdateAnimationBasedOnMovement(Vector3 movementDirection)
    {
        // �̵� ���� ����ȭ
        movementDirection.Normalize();

        // ����, ���� �̵� �� �и�
        float horizontalMovement = movementDirection.x;
        float verticalMovement = movementDirection.y;

        // ���� �̵��� ���� �̵����� ũ�� �¿� �ִϸ��̼�, �׷��� ������ ���� �ִϸ��̼�
        if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
        {
            // �¿� �̵�
            if (horizontalMovement > 0)
            {
                // ������ �̵�
                ChangeAnimationState(KID_RIGHT);
            }
            else
            {
                // ���� �̵�
                ChangeAnimationState(KID_LEFT);
            }
        }
        else
        {
            // ���� �̵�
            if (verticalMovement > 0)
            {
                // ���� �̵�
                ChangeAnimationState(KID_BACK);
            }
            else
            {
                // �Ʒ��� �̵�
                ChangeAnimationState(KID_FRONT);
            }
        }
    }


    // �ִϸ��̼� ���¸� �����ϰ� �����ϴ� �Լ�
    void ChangeAnimationState(string newState)
    {
        // ���� ��� ���� �ִϸ��̼��� �ٽ� ����Ϸ��� �ϸ� ����
        if (currentState == newState) return;

        // ���ο� �ִϸ��̼� ���
        animator.Play(newState);

        // ���� ���� ������Ʈ
        currentState = newState;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        // "Player" �±׸� ���� ������Ʈ�� �浿�ߴ��� Ȯ��
        if (other.CompareTag("Player"))
        {
            Hello(); // �λ� �г� ǥ�� �Լ� ȣ��

            // ���� ���� �г��� �Ҵ�Ǿ� �ִٸ�
            if (gameoverPanel != null)
            {
                gameoverPanel.SetActive(true); // ���� ���� �г� Ȱ��ȭ
            }
        }
    }

    // �λ� �г��� 2�ʰ� �����ִ� �Լ�
    public void Hello()
    {
        helloPanel.SetActive(true); // �λ� �г� Ȱ��ȭ
        Invoke("HidePanel", 2f); // 2�� �� HidePanel �Լ� ȣ��
    }

    // �λ� �г��� ����� �Լ�
    public void HidePanel()
    {
        helloPanel.SetActive(false); // �λ� �г� ��Ȱ��ȭ
    }

    // �߰� Ȱ��ȭ �Լ�

    public void EnableChasing()
    {
        isChasing = true; // �߰� ���·� ��ȯ
        agent.isStopped = false; // ������Ʈ �̵� �簳
    }

    // �߰� ��Ȱ��ȭ �Լ�
    public void DisableChasing()
    {
        isChasing = false; // �߰� ���� ����

        // agent�� null�� �ƴϰ� Ȱ��ȭ�Ǿ� ������
        if (agent != null && agent.isActiveAndEnabled) 
        {
            agent.isStopped = true; // ������Ʈ �̵� ����
        }
    }
}
