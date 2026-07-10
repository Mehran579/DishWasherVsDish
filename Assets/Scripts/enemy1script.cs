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
    public GameObject enemybullet;
    bool canshoot = true;
    bool candash = true;
    bool isdashing;
    public bool isinpuddle;
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
    void stoppuddle()
    {
        isinpuddle = false;
    }
    private void FixedUpdate()
    {
        if (isknockedback || isdashing)
        {
            return;
        }
        if(isinpuddle) 
        {
            Invoke(nameof(stoppuddle),0.5f);
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
        if (name == "enemy5")
        {
            if (Vector2.Distance(transform.position, Player.transform.position) <= 8 && Vector2.Distance(transform.position, Player.transform.position) >= 2 && candash)
            {
                StartCoroutine(dash());
            }
        }
        else if (name == "enemy7")
        {
            if (canshoot)
            {
                StartCoroutine(Shootcooldown(3.5f));

            }
        }
        else if (name == "enemy8") 
        {
            if (canshoot)
            {
                StartCoroutine(Shootcooldown(6f));
            }
        }

    }
    IEnumerator Shootcooldown(float cooldown)
    {
        canshoot = false;
        if(name == "enemy7")
        {
            GameObject _bullet = Instantiate(enemybullet, transform.position, Quaternion.identity);
        Rigidbody2D bulletrb = _bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletdir = (Player.transform.position - transform.position).normalized;
        Destroy(_bullet, 3f);
        bulletrb.linearVelocity = bulletdir * 10f;
        }else if(name == "enemy8")
        {
            GameObject _bullet = Instantiate(enemybullet, transform.position + new Vector3(0,1,0), Quaternion.identity);
            Destroy(_bullet, 0.45f);
        }
        yield return new WaitForSeconds(cooldown);
        canshoot = true;
    }
    IEnumerator dash()
    {
        candash = false;
        isdashing = true;
        enemyspeed = 80;
        yield return new WaitForSeconds(0.1f);
        enemyspeed = 8;
        isdashing = false;
        yield return new WaitForSeconds(7f);
        candash = true;
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
        if (collision.CompareTag("puddle")&&name!= "enemy8")
        {
            isinpuddle = true;
            enemyrb.AddForce((transform.position - collision.transform.position).normalized * 5f, ForceMode2D.Impulse);
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
