using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAmmo : MonoBehaviour
{
    private GameObject Guns;
    WeaponSwitching weaponswitching;
    GunScript gunScript;

    private float ammoAddMag;
    private void Start()
    {
        Guns = GameObject.FindGameObjectWithTag("Gun");
        //Pistols = GameObject.FindGameObjectWithTag("Pistol");
        weaponswitching = GameObject.Find("WeaponHolder").GetComponent<WeaponSwitching>();
        gunScript = Guns.transform.GetComponentInChildren<GunScript>();
       // Pistols_GunScript = Pistols.transform.GetComponentInChildren<GunScript>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Guns = GameObject.FindGameObjectWithTag("Gun");
        gunScript = Guns.transform.GetComponentInChildren<GunScript>();

        if (collider.gameObject.tag.Equals("Ammo"))
        {
           if(weaponswitching.selectedWeapon == 0 && gunScript != null && gunScript.currentMagBullet < gunScript.maxMagBullet)
            {
                addAmmoToMag(gunScript);
                Destroy(collider.gameObject);
            }
          /* else if(weaponswitching.selectedWeapon == 1)
            {
                addAmmoToMag(Pistols, Pistols_GunScript);
                Destroy(collider.gameObject);
            }*/
        }
    }
    
    private void addAmmoToMag(GunScript gunscript)
    {
        ammoAddMag = gunscript.maxMagBullet - gunscript.currentMagBullet;

        if (ammoAddMag > gunscript.maxAmmo / 2f)
            gunscript.currentMagBullet += (int)(gunscript.maxAmmo / 2f);
        else
            gunscript.currentMagBullet += (int)ammoAddMag;
    }
    

}
