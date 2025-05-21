using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    public string weaponName;
    public bool useMagazine;       // 是否使用弹匣
    public int maxAmmo;            // 备弹最大数量
    public int currentAmmo;        // 当前备弹数量
    public int magazineSize;       // 弹匣容量
    public int currentMagazine;    // 当前弹匣内子弹数
    public float fireRate;
    public float reloadTime;
}
public class WeaponManager : MonoBehaviour
{
    public WeaponData[] weapons;
    public Animator[] weaponAnimators; // 每把武器的动画控制器
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
                    Debug.Log("弹匣空了，按R换弹");
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
                    Debug.Log("没有子弹了");
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
