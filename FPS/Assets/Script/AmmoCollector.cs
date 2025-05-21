using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    public SimpleWeaponController[] weapons;  // �����������ű��Ͻ�����

    public void PickupAmmo(int amount)
    {
        foreach (var weapon in weapons)
        {
            if (weapon != null)
                weapon.Reload(amount);
        }
    }
}
