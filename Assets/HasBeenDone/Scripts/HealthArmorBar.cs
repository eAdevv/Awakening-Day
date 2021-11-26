using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthArmorBar : MonoBehaviour
{
    public Image healthBar, armorBar;
    private float currentHealth, maxHealth, currentArmor, maxArmor;
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.Find("FPS_Controller").GetComponent<PlayerManager>();
        maxHealth = playerManager.Health;
        maxArmor = playerManager.Armor;
    }

    private void Update()
    {
        maxArmor = playerManager.upgradedArmor;
        currentHealth = playerManager.Health;
        currentArmor = playerManager.Armor;

        healthBar.fillAmount = currentHealth / maxHealth;
        armorBar.fillAmount = currentArmor / maxArmor;

        if (currentHealth < 50f)
            healthBar.color = Color.red;
         
    }



}
