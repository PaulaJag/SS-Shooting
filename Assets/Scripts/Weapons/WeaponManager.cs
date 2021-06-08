using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponHandler[] weapons;

    private int currectWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Set the first active weapon
        currectWeaponIndex = 0;
        weapons[currectWeaponIndex].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        // If the current weapon is the same as selected
        if (currectWeaponIndex == weaponIndex)
        {
            // If -> Break out of the function
            return;
        }
        // Turning off the current weapon
        weapons[currectWeaponIndex].gameObject.SetActive(false);

        // Turning on the selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);

        // Store the current selected weapon index
        currectWeaponIndex = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        // Get weapon handler from the currently selected weapon
        return weapons[currectWeaponIndex];
    }
}
