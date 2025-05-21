using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;//灵敏度
    public Transform playerBody;//
    public float yRotation = 0f;//轴值累加
    
    // Start is called before the first frame update
    void Start()
    {
        // 锁定鼠标到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;

        // 隐藏鼠标光标
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);//限制视角度数
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);//垂直视角
        playerBody.Rotate(Vector3.up * mouseX);//水平视角
    }
}
