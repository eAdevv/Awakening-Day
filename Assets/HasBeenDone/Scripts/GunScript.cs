using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunScript : MonoBehaviour
{

    [Header("Fire Settings")]
    public float damage;
    public float range;
    public float fireRate;
    private float nextTimtetoFire = 0f;

    [Header("Reload Settings")]
    public float maxAmmo;
    public float currentAmmo;
    private bool isReloading = false;
    public float reloadTime;
    public float maxMagBullet;
    public float currentMagBullet;
    private float ammoToAdd;
    public GameObject ammo;

    [Header("Effects-Camera")]
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject hitMarker;
    public GameObject bulletHole;

    [Header("Upgrade Settings")]
    public int damage_Level;
    public int range_Level;
    public int fireRate_Level;
    public int damage_stars = 0;

    public Text ammoText, magAmmoText;
    [Space(20)]
    public int shopPrices;


    Weapon_Recoil_System recoil;
    PlayerManager playerManager;
    RoundSystem roundSystem;
    EnemySystem enemySystem;
    private void Start()
    {
        recoil = this.gameObject.GetComponent<Weapon_Recoil_System>();
        playerManager = GameObject.Find("FPS_Controller").GetComponent<PlayerManager>();
        roundSystem= GameObject.Find("FPS_Controller").GetComponent<RoundSystem>();
        muzzleFlash.Stop();
        currentAmmo = maxAmmo;
        currentMagBullet = maxMagBullet;
        ammoText.text = "" + currentAmmo;
        magAmmoText.text = "" + currentMagBullet;
    }
    
    private void OnEnable()
    {
        isReloading = false;    
    }

    void Update()
    {
        #region Reloding

        if (isReloading)
            return;   

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)) 
        {
            if(currentMagBullet != 0)
            {
                StartCoroutine(Reload());
                return;
            }         
        }
        #endregion

        #region Shooting
        if ( Time.time >= nextTimtetoFire && currentAmmo > 0 && playerManager.isInteraction == false)
        {         
            if (Input.GetButton("Fire1") &&  this.gameObject.tag == "Auto" )
            {
                nextTimtetoFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetButtonDown("Fire1") && this.gameObject.tag == "nonAuto")
            {
                nextTimtetoFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        #endregion

        ammoText.text = "" + currentAmmo;
        magAmmoText.text = "" + currentMagBullet;

    }

    void Shoot()
    {
        recoil.Fire();
        muzzleFlash.Play();
       
       RaycastHit hit;
        if (this.gameObject.tag.Equals("Shotgun"))
        {
            ShotgunSystem();
            currentAmmo -= 5f;
        }
        else
        {
            --currentAmmo;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                Debug.Log("hit");
                GameObject impact_go = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
                Destroy(impact_go.gameObject, 0.2f);

                if (hit.collider.gameObject.tag != "Enemy")
                {
                    GameObject hole_gameobject = Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal)) as GameObject;
                    Destroy(hole_gameobject.gameObject, 2f);
                }
           
                if (hit.collider.gameObject.tag.Equals("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<EnemySystem>().enemyHealth -= damage;
                    hitMarker.SetActive(true);
                    Invoke("markerDeactive", 0.2f);

                    if (hit.collider.gameObject.GetComponent<EnemySystem>().enemyHealth <= 0)
                    {
                        var ammo_drop_chance = Random.Range(1, 11f);
                        if(ammo_drop_chance <= hit.collider.gameObject.GetComponent<EnemySystem>().enemyLevel * 1.5f)
                        {
                            GameObject ammo_gameobject = Instantiate(ammo, hit.collider.gameObject.transform.position, Quaternion.identity) as GameObject;
                            Destroy(ammo_gameobject, 5f);
                        }
                   
                        Destroy(hit.collider.gameObject, 0f);
                        playerManager.currentMoney += hit.collider.gameObject.GetComponent<EnemySystem>().earnedMoney;
                        roundSystem.number_of_enemies_killed += 1;
                    }

                }
                
            }
        }               
    } 
    void ShotgunSystem()
    {
        for(int i = 0; i<5;i++)
        {
            RaycastHit hitShotgun;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward + new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), 0), out hitShotgun, range))
            {
                GameObject go = Instantiate(impactEffect, hitShotgun.point, Quaternion.LookRotation(hitShotgun.normal)) as GameObject;
                Destroy(go.gameObject, 0.4f);
            }
        }
        
    }  
    IEnumerator Reload()
    {
        Debug.Log("Reloading");
        isReloading = true;
        ammoToAdd = maxAmmo - currentAmmo;

        yield return new WaitForSeconds(reloadTime);
        //currentAmmo += maxAmmo;
        if(currentMagBullet >= ammoToAdd)
        {
            currentAmmo += ammoToAdd;
            currentMagBullet -= ammoToAdd;
        }
        else
        {      
            currentAmmo += currentMagBullet;
            currentMagBullet -= currentMagBullet;
        }

            isReloading = false; ;
    }

    void markerDeactive()
    {
        hitMarker.SetActive(false);
    }

}
