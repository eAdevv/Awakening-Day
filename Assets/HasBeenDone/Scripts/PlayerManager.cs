using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public float Health, Armor,upgradedArmor;
    public float currentMoney;
    [Header("TEXTS")]
    public Text moneyText;
    public TMP_Text healthText, armorText;
    public GameObject interactionText;

   [Header("INTERACTION")]
    public bool isInteraction;
    private float interactionRange = 10f;
    public Camera interactionCamera;
    public GameObject armourPanel, upgradePanel,gunshopPanel;

    public GameObject gotHitScreen;

    private void Start()
    {
        isInteraction = false;
        upgradedArmor = Armor;
    }
    void Update()
    {
        moneyText.text = "$" + currentMoney;
        healthText.text = "" + Health;
        armorText.text = "" + Armor;
        
        

        if (Health <= 0)
        {
            Die();
        }

        #region Interaction
        RaycastHit interaction;
        if (Physics.Raycast(interactionCamera.transform.position, interactionCamera.transform.forward, out interaction, interactionRange))
        {
            if (interaction.collider.gameObject.tag.Equals("NPC"))
            {
                
                if (Input.GetKeyDown(KeyCode.T))
                {
                    isInteraction = true;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    if (interaction.collider.gameObject.name.Equals("ArmorShop")) armourPanel.SetActive(true);
                    else if (interaction.collider.gameObject.name.Equals("UpgradeShop")) upgradePanel.SetActive(true);
                    else if (interaction.collider.gameObject.name.Equals("GunShop")) gunshopPanel.SetActive(true);
                } // Check Interaction
            }          
        }
        #endregion

        if(gotHitScreen != null)
        {
            if(gotHitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = gotHitScreen.GetComponent<Image>().color;
                color.a -= 0.01f;
                gotHitScreen.GetComponent<Image>().color = color;                   
            }
        }
    }

    void Die()
    {
        /// DIE SYSTEM ///
        /// Ölüm ekranı.
        /// 1. güne geri döner.
    }
}
