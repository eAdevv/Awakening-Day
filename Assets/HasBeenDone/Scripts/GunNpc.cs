using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunNpc : MonoBehaviour
{
    public GameObject[] gunModels;
    public int currentGunIndex;

    void Start()
    {
        foreach (GameObject gun in gunModels)   // SHOP ÜSTÜNDEKİ BÜTÜN SİLAH RESİMLERİ GİZLENİR
            gun.SetActive(false);  

        gunModels[currentGunIndex].SetActive(true);  // İNDİSTEKİ SİLAHIN GÖRÜNÜMÜ AÇILIR
    }

    public void nextButton() 
    {
        gunModels[currentGunIndex].SetActive(false);
        currentGunIndex++;

        if (currentGunIndex == gunModels.Length) 
            currentGunIndex = 0;

        gunModels[currentGunIndex].SetActive(true);
    }

    public void previousButton()
    {
        gunModels[currentGunIndex].SetActive(false);
        currentGunIndex--;

        if (currentGunIndex == -1)
            currentGunIndex = gunModels.Length-1;

        gunModels[currentGunIndex].SetActive(true);
    }

}
