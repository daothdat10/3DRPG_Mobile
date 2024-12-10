using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyList;
    [SerializeField] GameObject Coins;


    [SerializeField] float maxX;
    [SerializeField] float maxZ;
    [SerializeField] float minX;
    [SerializeField] float minZ;
    [SerializeField] float timeBetweenspawn;
   

    [Header("Boss")]
    [SerializeField] GameObject spawnBoss;
    [SerializeField] int Boss;
    public int enemyCount;
    public int round = 1;
    bool ground=true;
    void Start()
    {
        spawm(round);
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            round++;
            if (round %  Boss == 0)
            {
                ground=true;
                spawm(round);
            }
        }

        
        
    }
    void spawm(int roundBoss)
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        GameObject toSpawn;

        
        if(round == 1)
        {
            Debug.Log("The Enemy has appeared");
            StartCoroutine(SpawnCoin());
            toSpawn = enemyList;
        }else if(round == 2)
        {
            Debug.Log("The boss has appeared");
            toSpawn= spawnBoss;
            StartCoroutine(SpawmEnemy());
            StartCoroutine(SpawnCoin());
        }
        else
        {
            return;
        }
        var boss =  Instantiate(toSpawn, transform.position + new Vector3(randomX,4f, randomZ), transform.rotation);
        
    }

    IEnumerator SpawnCoin()
    {
        Instantiate(Coins, transform.position, transform.rotation);
        yield return new WaitForSeconds(2);
    }

    IEnumerator SpawmEnemy()
    {
        for (int i = 1; i <= 3; i++)
        {
            Instantiate(enemyList,transform.position,transform.rotation);
        }
        
        yield return new WaitForSeconds(5f);
    }
}
