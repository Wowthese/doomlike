using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectGun : MonoBehaviour
{
   
    public GameObject[] weaponUIObjects; // ËùÓÐ UI ÎäÆ÷ GameObject
    private int currentIndex = 0;

    void Start()
    {
        SelectWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectWeapon(2);
    }

    void SelectWeapon(int index)
    {
        currentIndex = index;
        for (int i = 0; i < weaponUIObjects.Length; i++)
        {
            weaponUIObjects[i].SetActive(i == index);
        }
       
    }

    public Animator GetCurrentAnimator()
    {
        return weaponUIObjects[currentIndex].GetComponent<Animator>();
    }
    public GameObject GetCurrentWeapon()
    {
        return weaponUIObjects[currentIndex];
    }
}
