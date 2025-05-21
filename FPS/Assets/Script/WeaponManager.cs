using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public string weaponName;
    public bool useMagazine;       // �Ƿ�ʹ�õ�ϻ
    public int maxAmmo;            // �����������
    public int currentAmmo;        // ��ǰ��������
    public int magazineSize;       // ��ϻ����
    public int currentMagazine;    // ��ǰ��ϻ���ӵ���
    public float fireRate;
    public float reloadTime;
}
public class WeaponManager : MonoBehaviour
{
    public WeaponData[] weapons;
    public Animator[] weaponAnimators; // ÿ�������Ķ���������
    public int currentWeaponIndex = 0;
    private float nextFireTime = 0f;
    private bool isReloading = false;

    public Camera cam;
    public GameObject hitEffect;
    public LayerMask hitMask;

    void Update()
    {
        HandleSwitch();
        HandleShoot();
        HandleReload();
    }

    void HandleSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
    }

    void SelectWeapon(int index)
    {
        currentWeaponIndex = index;
        for (int i = 0; i < weaponAnimators.Length; i++)
        {
            weaponAnimators[i].gameObject.SetActive(i == index);
        }
    }

    void HandleShoot()
    {
        WeaponData weapon = weapons[currentWeaponIndex];
        Animator anim = weaponAnimators[currentWeaponIndex];

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && !isReloading)
        {
            if (weapon.useMagazine)
            {
                if (weapon.currentMagazine > 0)
                {
                    Shoot(weapon, anim);
                    weapon.currentMagazine--;
                }
                else
                {
                    Debug.Log("��ϻ���ˣ���R����");
                }
            }
            else
            {
                if (weapon.currentAmmo > 0)
                {
                    Shoot(weapon, anim);
                    weapon.currentAmmo--;
                }
                else
                {
                    Debug.Log("û���ӵ���");
                }
            }
        }
    }

    void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            WeaponData weapon = weapons[currentWeaponIndex];
            if (weapon.useMagazine && weapon.currentMagazine < weapon.magazineSize && weapon.currentAmmo > 0)
            {
                StartCoroutine(ReloadCoroutine(weapon, weaponAnimators[currentWeaponIndex]));
            }
        }
    }

    IEnumerator ReloadCoroutine(WeaponData weapon, Animator anim)
    {
        isReloading = true;
        anim.SetTrigger("Reload");

        yield return new WaitForSeconds(weapon.reloadTime);

        int needed = weapon.magazineSize - weapon.currentMagazine;
        int toLoad = Mathf.Min(needed, weapon.currentAmmo);
        weapon.currentMagazine += toLoad;
        weapon.currentAmmo -= toLoad;

        isReloading = false;
    }

    void Shoot(WeaponData weapon, Animator anim)
    {
        nextFireTime = Time.time + weapon.fireRate;
        anim.ResetTrigger("Shoot");
        anim.SetTrigger("Shoot");

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hitMask))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null) enemy.TakeDamage(10);

            if (hitEffect) Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
