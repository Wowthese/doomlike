using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [Header("引用")]
    public selectGun weaponSelector;            // 引用切枪脚本
    public TextMeshProUGUI ammoText;            // 显示弹药的文本组件

    void Update()
    {
        // 获取当前激活的武器对象
        GameObject currentWeaponObj = weaponSelector.GetCurrentWeapon();
        if (currentWeaponObj == null || ammoText == null) return;

        // 获取当前武器上的 SimpleWeaponController 脚本
        SimpleWeaponController weapon = currentWeaponObj.GetComponent<SimpleWeaponController>();
        if (weapon != null)
        {
            ammoText.text = $"{weapon.GetCurrentAmmo()} / {weapon.maxAmmo}";
        }
        else
        {
            ammoText.text = "N/A";
        }
    }
}
