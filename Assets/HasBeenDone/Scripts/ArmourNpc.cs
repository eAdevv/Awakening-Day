using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmourNpc : MonoBehaviour
{
    [Header("ARMOR NPC")]
    public int upgradeCount;
    private int totalUpgrade = 5;
    public int numOfBars;
    public Image[] armorBars;
    public Sprite fullArmor;
    public Sprite emptyArmor;

    

    PlayerManager playerManager;
    GunScript gunScript;
    RoundSystem roundSystem;
    private GameObject Guns;
    private void Start()
    {
        Guns = GameObject.FindGameObjectWithTag("Gun");
        gunScript = Guns.transform.GetComponentInChildren<GunScript>();
        playerManager = GameObject.Find("FPS_Controller").GetComponent<PlayerManager>();
        roundSystem = GameObject.Find("FPS_Controller").GetComponent<RoundSystem>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || roundSystem.shopActive == false)
        {
            this.gameObject.SetActive(false);
            playerManager.isInteraction = false;
            Cursor.visible = false;  
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (upgradeCount > numOfBars)
        {
            upgradeCount = numOfBars;
        }

        for(int i = 0;i<armorBars.Length;i++)
        {
            if(i< upgradeCount)
            {
                armorBars[i].sprite = fullArmor;
            }
            else
            {
                armorBars[i].sprite = emptyArmor;
            }

            if(i<numOfBars)
            {
                armorBars[i].enabled = true;
            }
            else
            {
                armorBars[i].enabled = false;
            }
        }
        
    }
    public void upgradeButton()
    {
        if(upgradeCount < totalUpgrade)
        {
            if (playerManager.currentMoney >= 200f)
            {
                playerManager.upgradedArmor += 25;
                playerManager.Armor = playerManager.upgradedArmor;
                upgradeCount += 1;
                playerManager.currentMoney -= 200f;
            }
        }
    }  
}
