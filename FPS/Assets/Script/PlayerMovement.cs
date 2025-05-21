using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 6f;  // 奔跑速度
    public float dodgeSpeed = 10f;  // 闪避速度
    public float dodgeDuration = 0.3f;  // 闪避持续时间
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
        // 获取输入（无插值，确保每次反应灵敏）
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // 全程奔跑（不再有行走速度）
        controller.Move(move.normalized * runSpeed * Time.deltaTime);

        // 如果正在闪避，不进行其他动作
        if (isDodging)
        {
            dodgeTime += Time.deltaTime;
            controller.Move(dodgeDirection * dodgeSpeed * Time.deltaTime);

            // 如果闪避时间结束，重置
            if (dodgeTime > dodgeDuration)
            {
                isDodging = false;
                dodgeTime = 0f;
            }

            return;
        }

        // 按下 Shift 并且角色正在移动时触发闪避
        if (Input.GetKeyDown(KeyCode.LeftShift) && move.magnitude > 0.1f)
        {
            // 根据当前移动方向来决定闪避方向
            dodgeDirection = move.normalized;  // 使用当前移动方向来闪避
            isDodging = true;  // 开始闪避
        }

        // 自定义地面检测 & 落地贴地
        if (CustomIsGrounded() && velocity.y < 0)
            velocity.y = -2f;

        // 跳跃输入（空格）
        if (Input.GetButtonDown("Jump") && CustomIsGrounded())
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // 应用重力
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    
    // 自定义地面检测方法
    bool CustomIsGrounded()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        return Physics.Raycast(rayOrigin, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));
    }
}
