using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GunManager : MonoBehaviour
{
    public GameObject[] weapons;
    public int currentWeaponIndex;
    public GameObject gunShopPanel;
    public TMP_Text priceText;
    private int shop_Price;

    GunNpc gunNpc;
    PlayerManager playerManager;
    RoundSystem roundSystem;


    void Start()
    {
        gunNpc = GameObject.Find("PrimaryWeapon").GetComponent<GunNpc>();
        playerManager = GameObject.Find("FPS_Controller").GetComponent<PlayerManager>();
        roundSystem = GameObject.Find("FPS_Controller").GetComponent<RoundSystem>();
    }

    private void Update()
    {
        currentWeaponIndex = gunNpc.currentGunIndex; // INDEXLERİN AYNI DEĞERİ TUTMASINI SAĞLAR BU ŞEKİLDE SİLAH SEÇİMİ KOLAYLAŞIR
        shop_Price = weapons[currentWeaponIndex].GetComponent<GunScript>().shopPrices; // GETTING PRICE FROM GUN ATTRIBUTES

        priceText.text = "" + shop_Price;

        if(shop_Price == 0) priceText.text = "Available";

        if (Input.GetKeyDown(KeyCode.Escape) || roundSystem.shopActive == false)
        {
            gunShopPanel.SetActive(false);
            playerManager.isInteraction = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    public void buySystem()
    {    
       if (!weapons[currentWeaponIndex].activeSelf)
       {
          if (playerManager.currentMoney >= shop_Price)
          {
                playerManager.isInteraction = false;
                foreach (GameObject gun in weapons)
                gun.SetActive(false);

               weapons[currentWeaponIndex].SetActive(true);
               gunShopPanel.SetActive(false);
               playerManager.currentMoney -= shop_Price;

               Cursor.visible = false;
               Cursor.lockState = CursorLockMode.Locked;

               weapons[currentWeaponIndex].GetComponent<GunScript>().shopPrices = 0;
          }
       }                  
    }
}
