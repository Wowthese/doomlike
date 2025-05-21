using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AmmoCollector;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AmmoCollector collector = other.GetComponent<AmmoCollector>();
            if (collector != null)
            {
                collector.PickupAmmo(ammoAmount);
                Destroy(gameObject);
            }
        }
    }
}
