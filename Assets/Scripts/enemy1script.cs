using UnityEngine;

public class enemy1script : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody2D enemyrb;
    public float enemyspeed = 5f;
    public int enemyhealth = 7;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemyrb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = Vector2.MoveTowards(transform.position , Player.transform.position,enemyspeed * Time.fixedDeltaTime);
        if(enemyhealth <= 0)
        {
            Destroy(gameObject);
        }
        //enemyrb.MovePosition(Player.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyhealth -= 1;
        }
    }
    
}
