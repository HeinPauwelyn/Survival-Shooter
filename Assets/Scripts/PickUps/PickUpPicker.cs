using UnityEngine;
using System.Collections;
using System;

public class PickUpPicker : MonoBehaviour
{
    public float lifeTime = 1000f;
    public Function function = Function.HealtPlayer;
    
    private AudioSource pickUpAudio;
    private GameObject player;
    private GameObject gunBarrelEnd;
    private PlayerShooting playerShooting;
    private PlayerHealth playerHealth;
    private ScoreManager scoreManager;
    private Animator anim;
    private float timer;
    private bool picked = false;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gunBarrelEnd = GameObject.FindGameObjectWithTag("gunBarrelEnd");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerShooting = gunBarrelEnd.GetComponent<PlayerShooting>();
        anim = GetComponent<Animator>();
        pickUpAudio = GetComponent<AudioSource>();
        scoreManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<ScoreManager>();
    }

    void Update()
    {
        timer += 1;

        if (timer >= lifeTime && !picked)
        {
            anim.SetTrigger("TimedOut");
            Destroy(gameObject, 2f);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            pickUpAudio.Play();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            switch (function)
            {
                case Function.HealtPlayer:
                    playerHealth.GetDamage();
                    break;

                case Function.DamageEnemies:

                    foreach (GameObject enemy in enemies)
                    {
                        EnemyHealth enemyHealt = enemy.GetComponent<EnemyHealth>();
                        enemyHealt.currentHealth = 0;
                        enemyHealt.anim.SetTrigger("Dead");

                        ScoreManager.Score += Convert.ToInt32(Math.Round(enemyHealt.scoreValue / 2d, 0));
                    }

                    break;

                case Function.RefillBullets:
                    playerShooting.Refill();
                    break;

                case Function.SlowDownEnemies:

                    foreach (GameObject enemy in enemies)
                    {
                        NavMeshAgent navAgent = enemy.GetComponent<NavMeshAgent>();
                        navAgent.speed = navAgent.speed / 1.5f;
                    }
                    break;

                case Function.Money:
                    scoreManager.Money += UnityEngine.Random.Range(1, 100) * 10;
                    break;

                default:
                    break;
            }
            
            picked = true;
            anim.SetTrigger("Picked");
            Destroy(gameObject, 2f);
        }
    }

    public enum Function
    {
        HealtPlayer,
        DamageEnemies,
        RefillBullets,
        SlowDownEnemies,
        Money
    }
}
