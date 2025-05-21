using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollector : MonoBehaviour
{
    public SimpleWeaponController[] weapons;  // 把三个武器脚本拖进这里

    public void PickupAmmo(int amount)
    {
        foreach (var weapon in weapons)
        {
            if (weapon != null)
                weapon.Reload(amount);
        }
    }
}
