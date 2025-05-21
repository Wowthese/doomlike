using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;//������
    public Transform playerBody;//
    public float yRotation = 0f;//��ֵ�ۼ�
    
    // Start is called before the first frame update
    void Start()
    {
        // ������굽��Ļ����
        Cursor.lockState = CursorLockMode.Locked;

        // ���������
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -80f, 80f);//�����ӽǶ���
        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);//��ֱ�ӽ�
        playerBody.Rotate(Vector3.up * mouseX);//ˮƽ�ӽ�
    }
}
