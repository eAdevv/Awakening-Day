using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeNpc : MonoBehaviour
{

    public int upgradeCount;
    private int totalUpgrade = 5;
    public int numOfBars;
    public Image[] damageStars;
    public Image[] rangeStars;
    public Image[] fireRateStars;
    public Sprite fullStars;
    public Sprite emptyStars;

    PlayerManager playerManager;
    GunScript gunScript;
    RoundSystem roundSystem;

    private GameObject Guns;

    void Start()
    {
        Guns = GameObject.FindGameObjectWithTag("Gun");
        gunScript = Guns.transform.GetComponentInChildren<GunScript>();
        playerManager = GameObject.Find("FPS_Controller").GetComponent<PlayerManager>();
        roundSystem = GameObject.Find("FPS_Controller").GetComponent<RoundSystem>();
    }

    
    void Update()
    {
        Guns = GameObject.FindGameObjectWithTag("Gun");
        gunScript = Guns.transform.GetComponentInChildren<GunScript>();


        if (Input.GetKeyDown(KeyCode.Escape) || roundSystem.shopActive == false)
        {
            this.gameObject.SetActive(false);
            playerManager.isInteraction = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        for (int i = 0; i < rangeStars.Length; i++)
        {
            npcsystem(rangeStars, gunScript.range_Level,i);         
        }

        for (int i = 0; i < damageStars.Length; i++)
        {
            npcsystem(damageStars, gunScript.damage_Level,i);
        }

        for (int i = 0; i < fireRateStars.Length; i++)
        {
            npcsystem(fireRateStars, gunScript.fireRate_Level,i);
        }



    }

    public void npcsystem(Image[] upgradeArray,int level,int i)
    {
        if (level > numOfBars)
        {
            level = numOfBars;
        }   
        
        if (i < level)
        {
            upgradeArray[i].sprite = fullStars;
        }
        else
        {
            upgradeArray[i].sprite = emptyStars;
        }

        /*if (i < numOfBars)
        {
            upgradeArray[i].enabled = true;
        }
        else
        {
            upgradeArray[i].enabled = false;
        }
        */
    }

    public void damage_upgrade()
    {
        if (gunScript.damage_Level < totalUpgrade)
        {
            if (playerManager.currentMoney >= 500f)
            {
                gunScript.damage += gunScript.damage / 2f;
                playerManager.currentMoney -= 500f;
                upgradeCount += 1;
                gunScript.damage_Level += 1;
            }
        }
    }

    public void range_upgrade()
    {     
        if (gunScript.range_Level < totalUpgrade)
        {
            if (playerManager.currentMoney >= 250f)
            {
                gunScript.range += gunScript.range / 5f;
                playerManager.currentMoney -= 250f;
                upgradeCount += 1;
                gunScript.range_Level += 1;
            }
        }
    }

    public void fireRate_upgrade()
    {
        if (gunScript.fireRate_Level < totalUpgrade)
        {
            if (playerManager.currentMoney >= 250f)
            {
                gunScript.fireRate += gunScript.fireRate / 5f;
                playerManager.currentMoney -= 250f;
                upgradeCount += 1;
                gunScript.fireRate_Level += 1;
            }
        }
    }

    

}
