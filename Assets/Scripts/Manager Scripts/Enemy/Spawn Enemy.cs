using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyList;
    [SerializeField] GameObject Coins;

    [SerializeField] float maxX;
    [SerializeField] float maxZ;
    [SerializeField] float minX;
    [SerializeField] float minZ;
    [SerializeField] float timeBetweenspawn;
    [SerializeField] Transform spawnParent;

    [Header("Boss")]
    [SerializeField] GameObject spawnBoss;
    [SerializeField] int Boss;
    public int enemyCount;
    public int round = 1;
    bool ground = true;

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
            if (round % Boss == 0)
            {
                ground = true;
                spawm(round);
            }
        }
    }

    void spawm(int roundBoss)
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        GameObject toSpawn;
        GameObject spawnpoint;
        if (round == 1)
        {
            Debug.Log("The Enemy has appeared");
            StartCoroutine(SpawnCoin());

            for (int i = 0; i < 2; i++)
            {
                toSpawn = enemyList[i];
                spawnpoint = Instantiate(toSpawn, transform.position + new Vector3(randomX, 4f, randomZ), transform.rotation);
                spawnpoint.transform.parent = spawnParent;
            }
        }
        else if (round == 2)
        {
            Debug.Log("The boss has appeared");
            toSpawn = spawnBoss;
            StartCoroutine(SpawmEnemy());
            StartCoroutine(SpawnCoin());
            spawnpoint =Instantiate(toSpawn, transform.position + new Vector3(randomX, 4f, randomZ), transform.rotation);
            spawnpoint.transform.parent = spawnParent;
        }
        else
        {
            return;
        }
    }

    IEnumerator SpawnCoin()
    {
        Instantiate(Coins, transform.position, transform.rotation);
        yield return new WaitForSeconds(2);
    }

    IEnumerator SpawmEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Instantiate(enemyList[i], transform.position, transform.rotation);
        }

        yield return new WaitForSeconds(1f);
    }
}
