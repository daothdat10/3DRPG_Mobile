using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    //xp player
    public float xpPlayer;
    private float xpMaxPlayer=5f;
    public LevelSystem xpLevel;
    public TextMeshProUGUI xpText;

    
    public string characterName;
    private Damage x2dame;
    [SerializeField] GameObject hitVFX;
    [SerializeField] ParticleSystem dieVFX;

    //Player o trang thai binh thuong
    public IgemsType currentIgems = IgemsType.None;

    //Dau Hieu Tang Suc Manh
    public GameObject igemsIndicator;


    public bool hasIgems = false; // Kiem Tra xem player So huu  igem k
    private bool hasImmorta = false; //Kiem Tra bat tu
    private bool hasx2dame=false;

    //hp player
    public int health  , healthMax = 10;
    public FloatingHealthbar healthbar;
    Animator animator;

    //trang thai bat tu
    public GameObject immotal;

    private Coroutine igemCountdown;
    

    
    public string CharacterName
    {
        get { return characterName; }
        set { characterName = value; }
    }
    
    

    void Awake()
    {
        xpPlayer = 0;
        xpLevel = GameObject.Find("XPSlider").GetComponent<LevelSystem>();
        health = healthMax;
        healthbar = GetComponentInChildren<FloatingHealthbar>();
        xpLevel = GetComponentInChildren<LevelSystem>();
        animator = GetComponent<Animator>();
        
        
    }

    
    public virtual void Update()
    {
        igemsIndicator.transform.position = transform.position + new Vector3(0, 2f, 0);
        if (Input.GetKeyDown(KeyCode.Q) && xpPlayer<=xpMaxPlayer)
        {
            xpdieEnemy();
        }
        
        
    }


    public virtual void TakeDamage(int damageAmount)
    {
           
        StartCoroutine(TakeDamageCorouter(damageAmount));
        if (health <= 0)
        {
            dieVFX.Play();
            Die();
            
        }
    }


    public void xpdieEnemy()
    {
        xpPlayer ++;
        xpText.text = xpPlayer.ToString();
        
        xpLevel.xpLevelUp(xpPlayer, xpMaxPlayer);
    }

    public virtual void Die()
    {
        Debug.Log(characterName + " has died.");
       
        Destroy(this.gameObject);
        
    }

    private IEnumerator TakeDamageCorouter(int damageAmount)
    {
        
        if (hasIgems)
        {   
            hasImmorta=true;
            health = healthMax;
            damageAmount = 0;
            healthbar.UpdateHealthbar(health, healthMax);
            hasImmorta = false;
            StartCoroutine(IgemsCoroutine());
            yield return new WaitForSeconds(7);

        }

        else
        {
            hasImmorta =false;
            health -= damageAmount;
            healthbar.UpdateHealthbar(health, healthMax);
            animator.SetTrigger("damage");


            

            hasImmorta = false;
            
            yield return null;
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Gem1") || other.gameObject.CompareTag("GemBlue"))
        {

            hasIgems = true;
            currentIgems = other.gameObject.GetComponent<Igems>().igemsType;

            Destroy(other.gameObject);
            igemsIndicator.gameObject.SetActive(true);                                                                                                                                                                                                              

            if(igemCountdown != null)
            {
                StopCoroutine(igemCountdown);
            }
            igemCountdown = StartCoroutine((IEnumerator)IgemsCoroutine());
        }

        
    }

   
     IEnumerator IgemsCoroutine()
    {
            
            yield return new WaitForSeconds(10);
            hasIgems = false;
            igemsIndicator.gameObject.SetActive(false);
            currentIgems = IgemsType.None;
        
  
    }

    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX,hitPosition,Quaternion.identity);
        Destroy(hit,3f);
    }

    

    
}
