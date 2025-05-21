using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 6f;  // �����ٶ�
    public float dodgeSpeed = 10f;  // �����ٶ�
    public float dodgeDuration = 0.3f;  // ���ܳ���ʱ��
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 dodgeDirection;
    private bool isDodging = false;
    private float dodgeTime = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ��ȡ���루�޲�ֵ��ȷ��ÿ�η�Ӧ������
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // ȫ�̱��ܣ������������ٶȣ�
        controller.Move(move.normalized * runSpeed * Time.deltaTime);

        // ����������ܣ���������������
        if (isDodging)
        {
            dodgeTime += Time.deltaTime;
            controller.Move(dodgeDirection * dodgeSpeed * Time.deltaTime);

            // �������ʱ�����������
            if (dodgeTime > dodgeDuration)
            {
                isDodging = false;
                dodgeTime = 0f;
            }

            return;
        }

        // ���� Shift ���ҽ�ɫ�����ƶ�ʱ��������
        if (Input.GetKeyDown(KeyCode.LeftShift) && move.magnitude > 0.1f)
        {
            // ���ݵ�ǰ�ƶ��������������ܷ���
            dodgeDirection = move.normalized;  // ʹ�õ�ǰ�ƶ�����������
            isDodging = true;  // ��ʼ����
        }

        // �Զ�������� & �������
        if (CustomIsGrounded() && velocity.y < 0)
            velocity.y = -2f;

        // ��Ծ���루�ո�
        if (Input.GetButtonDown("Jump") && CustomIsGrounded())
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // Ӧ������
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    
    // �Զ�������ⷽ��
    bool CustomIsGrounded()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(rayOrigin, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));
    }
}
