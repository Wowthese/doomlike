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

        // ֻ����ˮƽ���򣨺��� Y ��
        direction.y = 0;

        // ������Ϊ�㣬������ֹ����
        if (direction.sqrMagnitude < 0.001f) return;

        // ���㳯��
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        // Ӧ�ó���ֻ�� Y ��
        transform.rotation = Quaternion.Euler(0, lookRotation.eulerAngles.y, 0);
    }
}
