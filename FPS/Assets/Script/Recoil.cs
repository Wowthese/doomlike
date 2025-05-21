using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoilAmount = 2f;      // 后坐角度
    public float recoilSpeed = 10f;      // 回正速度
    public float maxRecoil = 10f;        // 最大抬头角度限制

    private float currentRecoil = 0f;
    private float targetRecoil = 0f;

    void Update()
    {
        // 模拟射击输入
        if (Input.GetMouseButtonDown(0))
        {
            ApplyRecoil();
            Debug.Log("1");
        }

        // 插值控制回正
        currentRecoil = Mathf.Lerp(currentRecoil, targetRecoil, Time.deltaTime * recoilSpeed);

        // 应用后坐力旋转
        transform.localEulerAngles = new Vector3(-currentRecoil, 0f, 0f);
    }

    void ApplyRecoil()
    {
        targetRecoil += recoilAmount;
        targetRecoil = Mathf.Clamp(targetRecoil, 0, maxRecoil);
    }
}
