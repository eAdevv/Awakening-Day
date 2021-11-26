using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Drop : MonoBehaviour
{

    public Transform equipPosition;
    public float distance = 10f;
    public GameObject currentWeapon;
    GameObject wp;
    bool canPick;
    


    // Update is called once per frame
    private void Update()
    {
        CheckWeapons();

        if(canPick)
        {
            if (Input.GetKeyDown(KeyCode.E))
                Drop();

            PickUp();
        }

        if(currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.G))
                Drop();
        }
       
    }

    private void CheckWeapons()
    {
        RaycastHit pick;
        if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out pick, distance))
        {
            if(pick.transform.tag == "Assault" || pick.transform.tag == "Pistol")
            {
                canPick = true;
                wp = pick.transform.gameObject;
            }
        }
    }

    private void PickUp()
    {

        currentWeapon = wp;
        currentWeapon.transform.position = equipPosition.position;
        currentWeapon.transform.parent = equipPosition;
        currentWeapon.transform.localEulerAngles = new Vector3(0,90f,0);
        currentWeapon.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Drop()
    {

        Debug.Log("drop drop drop");
        currentWeapon.transform.parent = null;
        currentWeapon.GetComponent<Rigidbody>().isKinematic = false;
        currentWeapon = null;
    }
}
