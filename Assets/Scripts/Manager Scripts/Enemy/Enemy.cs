using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    //hp enemy
    public float healthMax=3 ;
    public float health;
    protected FloatingHealthbar healthh;

    private HealthSystem _healthSystem;
    
    
    [SerializeField] GameObject hitVFX;
    [SerializeField] ParticleSystem dieVFX;

    [Header("Boss")]
    public bool boss=false;
    private SpawnEnemy spawnManager;
    public float spawnInterval;
    public float nextSpawn;

    


    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    GameObject player;
    NavMeshAgent agent;
    Animator enemyAnimator;
   
    float timePassed;
    float newDestinationCD = 0.5f;
    public virtual void Start()
    {
        _healthSystem = FindObjectOfType<HealthSystem>();
        if (boss)
        {
            spawnManager = GetComponent<SpawnEnemy>();
        }
        health = healthMax;
        healthh = GetComponentInChildren<FloatingHealthbar>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        
        enemyAnimator = GetComponent<Animator>();
    }
    public virtual void Update()
    {
        enemyAnimator.SetFloat("speed", agent.velocity.magnitude / agent.speed);


        

        if (timePassed >= attackCD) {
            if(Vector3.Distance(player.transform.position,transform.position) <= attackRange) {
                enemyAnimator.SetTrigger("attack");
                
                timePassed = 0;
            }
        }

        timePassed += Time.deltaTime;
        if(newDestinationCD<=0 && Vector3.Distance(player.transform.position,transform.position)<= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);    
        }
        newDestinationCD -= Time.deltaTime;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            transform.LookAt(player.transform);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthh.UpdateHealthbar(health, healthMax);
        
        enemyAnimator.SetTrigger("damage");
        
        if (health <= 0)
        {
            Die();
            dieVFX.Play ();
            
        }
    }
    public  void Die()
    {
        
        Destroy(gameObject);
        _healthSystem.xpdieEnemy();
        
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
        



    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
        
        

    }

    public void HitVFX(Vector3 hitPos)
    {
        GameObject hit = Instantiate(hitVFX,hitPos,Quaternion.identity);

        Destroy(hit, 3f);
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(1);
        }

        
    }
}
