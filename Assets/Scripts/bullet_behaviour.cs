using Unity.VisualScripting;
using UnityEngine;

public class bullet_behaviour : MonoBehaviour
{
    public float damage =1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            if (GameObject.FindWithTag("powerupmanager").GetComponent<powerupmanager>().piercingobtained) return;
            Debug.Log(collision.gameObject);
            GetComponent<Animator>().SetTrigger("burst");
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    public void destroybullet()
    {
        Destroy(gameObject);
    }
}
