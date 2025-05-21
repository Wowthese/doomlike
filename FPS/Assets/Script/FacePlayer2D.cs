using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer2D : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;

        // 只保留水平方向（忽略 Y 轴差）
        direction.y = 0;

        // 若方向为零，跳过防止错误
        if (direction.sqrMagnitude < 0.001f) return;

        // 计算朝向
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // 应用朝向，只绕 Y 轴
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }
}
