using System;
using TMPro;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public TMP_Text text;
    public Slider slider;
    public float Health = 10f;
    public float FullHealth;
    public Vector2 dir;
    public float angle;
    public float scalemultiplier = 1f;
    public Transform shootpos;
    public Transform tolookat;
    //private Camera maincam;
    public Rigidbody2D playerrb;
    public float speed = 10f;
    public float bulletspeed=13f;
    public float bulletcooldown = 3f;
    Vector2 movement;
    bool ishooting;
    public GameObject bullet;
    public int points;
    public TMP_Text soapcount;
    public bool isvulnerable;
    bool dead;
    public GameObject win;
    public GameObject loose;
    private void Start()
    {
        FullHealth = Health;
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.started && !ishooting)
        {
            StartCoroutine(Shootcooldown());
            GameObject _bullet = Instantiate(bullet,shootpos.position,quaternion.identity);
            Rigidbody2D bulletrb = _bullet.GetComponent<Rigidbody2D>();

            Vector2 bulletdir = new Vector2(MathF.Cos(angle * Mathf.Deg2Rad), MathF.Sin(angle * Mathf.Deg2Rad));
            bulletrb.linearVelocity = bulletdir * bulletspeed;
            _bullet.transform.localScale = Vector3.one * scalemultiplier;
            Destroy(_bullet, 2f);
            
        }
    }

    public void FixedUpdate()
    {
        playerrb.linearVelocity = movement * speed;
    }
  
    IEnumerator Shootcooldown()
    {
        ishooting = true;
        yield return new WaitForSeconds(bulletcooldown);
        ishooting = false;
    }
    private void Update()
    {
        slider.value = Health;
        text.text = "Health : " + Health;
        dir = tolookat.position - transform.position;
        angle = MathF.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle - 90f));
        if(Health == 0&&!dead)
        {
            StartCoroutine(restarter(false));
            dead = true;
        }
    }
    public IEnumerator restarter(bool winorloss)
    {
        if (winorloss)
        {
            Time.timeScale = 0;
            win.SetActive(true);
            yield return new WaitForSecondsRealtime(1.5f);
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        else
        {
            Time.timeScale = 0;
            loose.SetActive(true);
            yield return new WaitForSecondsRealtime(1.5f);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("point"))
        {
            points += 1;
            soapcount.text = "Soap Points: " + points;
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("enemy bullet")||collision.CompareTag("puddle"))
        {
            Health -= 1;
            StartCoroutine(Invincible());
            StartCoroutine(spriteblink());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy") && !isvulnerable)
        {
            Health -= 1;
            StartCoroutine(Invincible());
            StartCoroutine(spriteblink());
        }
    }
    IEnumerator Invincible()
    {
        isvulnerable = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.8f);
        isvulnerable = false;
        GetComponent<Collider2D>().enabled = true;
    }
    IEnumerator spriteblink()
    {
        while (isvulnerable)
        {
            Color temp = spriteRenderer.color;
            temp.a = 0.1f;
            spriteRenderer.color = temp;
            yield return new WaitForSeconds(0.1f);
            Color temp2 = spriteRenderer.color;
            temp2.a = 1f;
            spriteRenderer.color = temp2;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
