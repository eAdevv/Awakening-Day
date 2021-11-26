using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    [Header("Attributes")]
    public float enemyHealth;
    public float enemyDamage;
    public float earnedMoney;
    public float attackDelay;
    private float waitForAttack;
    public float enemyLevel;

    [Header("AI")]
    public float lookRadius;
    Transform target;
    NavMeshAgent agent;

    PlayerManager playerManager;


    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        playerManager = GameObject.Find("FPS_Controller").GetComponent<PlayerManager>();
        waitForAttack = attackDelay;
    }


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if(distance <= agent.stoppingDistance)
            {
                enemyAttack();
                FaceTarget();            
            }
        }       
    }
    
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void enemyAttack()
    {
        if (waitForAttack <= 0)
        {
            if(playerManager.Armor > 0)
            {
                playerManager.Armor -= enemyDamage;
                waitForAttack = attackDelay;
                damageScreen();
            }
            else
            {       
                playerManager.Health -= enemyDamage;
                waitForAttack = attackDelay;
                damageScreen();
            }

            if (playerManager.Armor < 0) playerManager.Armor = 0; // To Avoid - values

        }
        if (waitForAttack > 0) waitForAttack -= Time.deltaTime;
    }

    void damageScreen()
    {
        var color = playerManager.gotHitScreen.GetComponent<Image>().color;
        color.a = 0.9f;
        playerManager.gotHitScreen.GetComponent<Image>().color = color;
    }
}
