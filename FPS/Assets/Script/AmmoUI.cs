using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [Header("����")]
    public selectGun weaponSelector;            // ������ǹ�ű�
    public TextMeshProUGUI ammoText;            // ��ʾ��ҩ���ı����

    void Update()
    {
        // ��ȡ��ǰ�������������
        GameObject currentWeaponObj = weaponSelector.GetCurrentWeapon();
        if (currentWeaponObj == null || ammoText == null) return;

        // ��ȡ��ǰ�����ϵ� SimpleWeaponController �ű�
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
