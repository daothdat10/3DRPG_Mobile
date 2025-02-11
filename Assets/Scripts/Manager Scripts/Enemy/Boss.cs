using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject igemsIndicator;
    public GameObject hasBoss;
    public Enemy enemyBoss;
    // Start is called before the first frame update
    void Start()
    {
            igemsIndicator.gameObject.SetActive(false); // Ensure the indicator is initially hidden
            enemyBoss = GetComponent<Enemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBoss != null && enemyBoss != null)
        {
            if (!hasBoss.CompareTag("Boss") || enemyBoss.health <= 0)
            {
                    igemsIndicator.gameObject.SetActive(true);
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            enemyBoss.TakeDamage(1);
        }
    }




}
