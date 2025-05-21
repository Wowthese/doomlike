using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilAmount = 2f;      // �����Ƕ�
    public float recoilSpeed = 10f;      // �����ٶ�
    public float maxRecoil = 10f;        // ���̧ͷ�Ƕ�����

    private float currentRecoil = 0f;
    private float targetRecoil = 0f;

    void Update()
    {
        // ģ���������
        if (Input.GetMouseButtonDown(0))
        {
            ApplyRecoil();
            Debug.Log("1");
        }

        // ��ֵ���ƻ���
        currentRecoil = Mathf.Lerp(currentRecoil, targetRecoil, Time.deltaTime * recoilSpeed);

        // Ӧ�ú�������ת
        transform.localEulerAngles = new Vector3(-currentRecoil, 0f, 0f);
    }

    void ApplyRecoil()
    {
        targetRecoil += recoilAmount;
        targetRecoil = Mathf.Clamp(targetRecoil, 0, maxRecoil);
    }
}
