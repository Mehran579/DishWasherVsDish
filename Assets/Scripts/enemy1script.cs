using UnityEngine;

public class enemy1script : MonoBehaviour
{
    public GameObject Player;
    Rigidbody2D enemyrb;
    public float enemyspeed;
    public int enemyhealth;
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
        enemyrb.MovePosition(Vector2.MoveTowards(transform.position, Player.transform.position, enemyspeed * 0.01f));
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
/*        Debug.Log(collision.gameObject.tag);
*/        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyhealth -= 1;
        }
    }
    
}
