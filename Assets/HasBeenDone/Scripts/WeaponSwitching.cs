using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    public int selectedWeapon = 0;
    void Start()
    {
        WeaponSelect();
    }

    
    void Update()
    {

        int preSelectedWeapon = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;

        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if(preSelectedWeapon != selectedWeapon)
        {
            WeaponSelect();
        }
    }


    public void WeaponSelect()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selectedWeapon)      
                weapon.gameObject.SetActive(true);     
            else          
                weapon.gameObject.SetActive(false);          
            i++;

        }
    }
    
}
