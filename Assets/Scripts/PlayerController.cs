using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾� �̵� �ӵ�
    public float moveSpeed = 5f;

    //���� �̵� �ӵ��� ������ ����
    private float originalMoveSpeed;

    // kidController ����
    public KidController kidController;

    // ���� ��� �̵��� ���� Rigifbody2D
    private Rigidbody2D rb;

    // �ִϸ����� ������Ʈ ����
    private Animator animator;

    private Camera mainCamera;

    // ���� �÷��̾� ���¸� ��Ÿ���� ����
    private string currentState = "Player_front";   // �⺻ ���¸� �������� ����

    // �ִϸ��̼� ���� �Լ�
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


    // ������ �̵� �ӵ�
    public float slowedSpeed = 2.5f;
    // �������� ���� �ð�
    public float slowDuration = 3f;

    void Start()
    {
        // Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();

        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        mainCamera = Camera.main;

        // ���� �̵� �ӵ� ����
        originalMoveSpeed = moveSpeed;

        // ���� Rigidbody2D�� ���ٸ� ��� �޽��� ���
        if(rb == null)
        {
            Debug.LogError("Player ������Ʈ�� Rigidbody2D ������Ʈ�� �����ϴ�!");
        }

        if(mainCamera == null)
        {
            Debug.LogError("���� ���� ī�޶� �����ϴ�!");
        }

        // KIdController�� ����Ǿ� ���� �ʴٸ� ã��
        if(kidController == null)
        {
            // "Kid" �±׸� ���� ������Ʈ ã��
            GameObject kidObject = GameObject.FindGameObjectWithTag("Kid");
            if(kidObject != null)
            {
                kidController = kidObject.GetComponent<KidController>();
            }
        }
    }

    void Update()
    {
        // �������� ���� ���� �ʱ�ȭ
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // ȭ��ǥ Ű ��� ���� Ű�� Ȯ�� (WASD�θ� ������)
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

            // �̵� ���� ����
            Vector3 movement = new Vector3 (horizontalInput, verticalInput, 0f);

        // �̵� Vector ����ȭ (�밢�� �̵��ӵ� ����)
        if (movement.magnitude > 0)
        {
            movement.Normalize();
        }

        // �÷��̾� �̵�
        transform.Translate(movement * moveSpeed * Time.deltaTime);
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

    //Ʈ���� �浹 ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Room_1_OutDoor �±׸� ���� ������Ʈ�� �浹���� ��
        if (other.CompareTag("Room_1_OutDoor"))
        {
            // �÷��̾� ��ġ �̵�
            transform.position = room2PlayerPosition;

            // ī�޶� ��ġ �̵�
            if (mainCamera != null)
            {
                mainCamera.transform.position = room2CameraPosition;
            }
        }
        // Room_1_InDoor �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Room_1_InDoor"))
        {
            // �÷��̾� ��ġ �̵�
            transform.position = room1PlayerPosition;

            // ī�޶� ��ġ �̵�
            if (mainCamera != null)
            {
                mainCamera.transform.position = room1CameraPosition;
            }
        }
        // Room_2_OutDoor �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Room_2_OutDoor"))
        {
            //�÷��̾� ��ġ �̵�
            transform.position = room3_2PlayerPosition;

            if (mainCamera != null)
            {
                mainCamera.transform.position = room3_2CameraPosition;
            }
        }
        // Room_2_InDoor �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Room_2_InDoor"))
        {
            // �÷��̾� ��ġ �̵�
            transform.position = room2_3PlayerPosition;

            // ī�޶� ��ġ �̵�
            if (mainCamera != null) 
            {
                mainCamera.transform.position = room2_3CameraPosition;
            }
        }



        // Room_3_OutDoor �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Room_3_OutDoor"))
        {
            // �÷��̾� ��ġ �̵�
            transform.position = room3_4PlayerPosition;

            // ī�޶� ��ġ �̵�
            if (mainCamera != null)
            {
                mainCamera.transform.position = room3_4CameraPosition;
            }
        }

        // Room_3_InDoor �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Room_3_InDoor"))
        {
            // �÷��̾� ��ġ �̵�
            transform.position = room3PlayerPosition;

            // ī�޶� ��ġ �̵�
            if (mainCamera != null)
            {
                mainCamera.transform.position = room3CameraPosition;
            }
        }    

        // Scissor �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Scissor"))
        {
            // ���� ������ ����
            Destroy(other.gameObject);

            // �̵� �ӵ� ����
            SlowPlayerDown();
        }
        // "Warning" �±׸� ���� ������Ʈ�� �浹���� ��
        else if (other.CompareTag("Warning"))
        {
            if (kidController != null)
            {
                kidController.EnableChasing();
            }
        }
    }

    // �÷��̾� �ӵ��� ���ҽ�Ű�� �Լ�
    private void SlowPlayerDown()
    {
        // �̹� ���� ���� �ڷ�ƾ�� �ִٸ� ����
        StopAllCoroutines();

        // �̵� �ӵ� ����
        moveSpeed = slowedSpeed;

        StartCoroutine(RestoreSpeed());
    }
    // ���� �ð� �Ŀ� �ӵ��� ������� �����ϴ� �ڷ�ƾ
    private IEnumerator RestoreSpeed()
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(slowDuration);

        // �̵� �ӵ� ����
        moveSpeed = originalMoveSpeed;
    }
}
