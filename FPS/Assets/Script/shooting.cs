using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Transform gunEnd;
    public float range = 100f;
    public GameObject hitEffectPrefab;
    public LineRenderer bulletLinePrefab;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(gunEnd.position, gunEnd.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            ShowHitEffect(hit);
            ShowBulletTrail(hit.point);
        }
        else
        {
            ShowBulletTrail(ray.origin + ray.direction * range);
        }
    }

    void ShowHitEffect(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 1.5f);
    }

    void ShowBulletTrail(Vector3 hitPoint)
    {
        LineRenderer line = Instantiate(bulletLinePrefab);
        line.SetPosition(0, gunEnd.position);
        line.SetPosition(1, hitPoint);
        Destroy(line.gameObject, 0.05f);
    }
}
