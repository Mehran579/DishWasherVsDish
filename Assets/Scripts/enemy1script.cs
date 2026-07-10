using System.Collections;
using UnityEngine;

public class enemy1script : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D enemyrb;
    bool isstun;
    public bool isknockedback;
    public float enemyspeed;
    public float enemyhealth;
    public int dropchance;
    public GameObject drop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position , Player.transform.position,enemyspeed * Time.fixedDeltaTime);
        if(enemyhealth <= 0)
        {
            Destroy(gameObject);
            Debug.Log("enemy killed");
            GameObject.FindGameObjectWithTag("spawner").GetComponent<enemyspawner>().numberofenemies--;
            if (Random.Range(1,11) <= dropchance)
            {
                Debug.Log("spawn point");
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
    }
    private void FixedUpdate()
    {
        if (isknockedback)
        {
            return;
        }
        else
        {
            if (isstun)
            {
                enemyrb.MovePosition(Vector2.MoveTowards(transform.position, Player.transform.position, enemyspeed/2 * 0.01f));
            }
            else
            {
                enemyrb.MovePosition(Vector2.MoveTowards(transform.position, Player.transform.position, enemyspeed * 0.01f));
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if (collision.CompareTag("bullet"))
        {
            enemyhealth -= collision.gameObject.GetComponent<bullet_behaviour>().damage;
            if (GameObject.FindWithTag("powerupmanager").GetComponent<powerupmanager>().stunbullets)
            {
                StartCoroutine(stun());
            }
        }
        if (collision.gameObject.CompareTag("soap shield"))
        {
            enemyhealth -= 1;
            StartCoroutine(knokbackroutine());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("soap shield"))
        {
            StartCoroutine(knokbackroutine());
        }
    }
    IEnumerator knokbackroutine()
    {
        isknockedback = true;
        enemyrb.AddForce((new Vector2(transform.position.x - Player.transform.position.x,transform.position.y - Player.transform.position.y)) * 10f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isknockedback = false;
    }
    IEnumerator stun()
    {
        isstun = true;
        yield return new WaitForSeconds(Random.Range(1, 3));
        isstun = false;
    }
}
