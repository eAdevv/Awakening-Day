using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoundSystem : MonoBehaviour
{
    private GameObject Enemy,Enemy_V2;
    public GameObject enemyv1,enemyv2,enemyv3,enemyv4,enemyv5;
    public TMP_Text roundText;
    private float xPos,xPos_v2;
    private float zPos,zPos_v2;
    public int enemyCount;
    public int[] Rounds;

    public int round_number;
    public float enemy_numbers;

    public float number_of_enemies_killed;

    private int  count = 0;

    public bool roundActive = true;
    public bool shopActive = true;

    public GameObject upgradeShop, gunShop, armorShop;
    PlayerManager playerManager;

    private Dictionary<int, float> RoundMap = new Dictionary<int, float>()
    {
        {1, 2f},   // Round1
        {2, 4f},   // Round2
        {3, 6f},   // Round3
        {4, 8f},   // Round4
        {5, 10f},  // Round5
        {6, 12f},  // Round6
        {7, 14f},  // Round7
        {8, 16f},  // Round8
        {9, 18f},  // Round9
        {10, 20f}, // Round10
        {11, 22f}, // Round11
        {12, 24f}, // Round12
        {13, 26f}, // Round13
        // TOTAL ROUND NUMBER WILL BE 100.
    };

    private void Start()
    {
        roundActive = true;
        round_number = Rounds[count];
        enemy_numbers = RoundMap[round_number];
        roundText.text = "" + round_number;
        playerManager = this.gameObject.GetComponent<PlayerManager>();
    }
    private void Update()
    {

        if (!roundActive)
        {
            round_number = Rounds[count];
            enemy_numbers = RoundMap[round_number];

            if (number_of_enemies_killed >= enemy_numbers)
            {
                ++count;
                StartCoroutine(roundEnd());
                number_of_enemies_killed = 0;
            }
        }
        if (roundActive)
        {
            StartCoroutine(enemySpawner(enemy_numbers));
            enemyCount = 0;
        }
    }

    IEnumerator enemySpawner(float round_enemy_number)
    {
        roundActive = false;
       

        while (enemyCount < round_enemy_number)
        {
            #region Round Enemy Numbers
            if (round_number <= 3)
                Enemy = enemyv1;
            else if ( round_number <= 5)
                Enemy = enemyv2;
            else if ( round_number <= 8)
                Enemy = enemyv3;
            else if ( round_number <= 10)
                Enemy = enemyv4;
            else if ( round_number <= 12)
                Enemy = enemyv5;
            #endregion

            #region Special Rounds
            if (round_number == 3)
                Enemy_V2 = enemyv2;
            else if (round_number == 5)
                Enemy_V2 = enemyv3;
            else if (round_number == 8)
                Enemy_V2 = enemyv4;
            else if (round_number == 10)
                Enemy_V2 = enemyv5;
            #endregion 
            // If the specified special rounds are reached, 2 different enemies are spawned.

            xPos = Random.Range(-50f, 50f);
            zPos = Random.Range(-50f, 50f);
            Instantiate(Enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);

            if (Enemy_V2 != null) 
            {
                xPos_v2 = Random.Range(-50f, 50f);
                zPos_v2 = Random.Range(-50f, 50f);
                Instantiate(Enemy_V2, new Vector3(xPos_v2, 0, zPos_v2), Quaternion.identity); // A new enemy type is created for special rounds.
            }                             
            yield return new WaitForSeconds(1f);

            if (Enemy_V2 != null)
                enemyCount += 2;
            else 
                enemyCount += 1;
            
        }           
    }

    IEnumerator roundEnd()
    {

        upgradeShop.SetActive(true);
        gunShop.SetActive(true);
        armorShop.SetActive(true);
        shopActive = true;
        playerManager.Health = 100f;
        playerManager.Armor = playerManager.upgradedArmor;
        yield return new WaitForSeconds(5f);
        roundActive = true;
        roundText.text = "" + round_number;

        shopActive = false;
        upgradeShop.SetActive(false);
        gunShop.SetActive(false);
        armorShop.SetActive(false);
    }
    

}
