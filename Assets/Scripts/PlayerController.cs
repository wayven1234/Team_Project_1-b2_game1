using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾� �̵� �ӵ�
    public float moveSpeed = 5f;

    // ���� ��� �̵��� ���� Rigifbody2D
    private Rigidbody2D rb;

    // �ִϸ����� ������Ʈ ����
    private Animator animator;

    // ���� �÷��̾� ���¸� ��Ÿ���� ����
    private string currentState = "Player_front";   // �⺻ ���¸� �������� ����

    // �ִϸ��̼� ���� �Լ�
    private const string PLAYER_FRONT = "Player_front";
    private const string PLAYER_BACK = "Player_back";
    private const string PLAYER_LEFT = "Player_left";
    private const string PLAYER_RIGHT = "Player_right";


    void Start()
    {
        // Rigidbody2D ������Ʈ ��������
        rb = GetComponent<Rigidbody2D>();

        // Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        // ���� Rigidbody2D�� ���ٸ� ��� �޽��� ���
        if(rb == null)
        {
            Debug.LogError("Player ������Ʈ�� Rigidbody2D ������Ʈ�� �����ϴ�!");
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
    }
}
