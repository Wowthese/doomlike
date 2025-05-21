using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWeaponController : MonoBehaviour
{
    [Header("动画控制")]
    public Animator weaponAnimator;

    [Header("射击参数")]
    public float fireRate = 0.1f;
    private float nextFireTime = 0f;

    [Header("子弹系统")]
    public int maxAmmo = 30;
    private int currentAmmo;

    [Header("射线检测")]
    public Camera cam;
    public float range = 100f;
    public LayerMask hitMask;

    [Header("击中特效")]
    public GameObject hitEffect;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            nextFireTime = Time.time + fireRate;
            currentAmmo--;

            // 播放动画
            if (weaponAnimator != null)
            {
                weaponAnimator.ResetTrigger("Shoot");
                weaponAnimator.SetTrigger("Shoot");
            }

            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 0.5f);

        if (Physics.Raycast(ray, out RaycastHit hit, range, hitMask))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
                enemy.TakeDamage(10);

            if (hitEffect != null)
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    // 公开接口（用于UI显示或换弹）
    public int GetCurrentAmmo() => currentAmmo;

    public void Reload(int amount)
    {
        currentAmmo = Mathf.Min(currentAmmo + amount, maxAmmo);
    }
}

