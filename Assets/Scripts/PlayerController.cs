using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Vector2 dir;
    public float angle;
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
        //Vector2 dir = tolookat.position - transform.position;
        //transform.up = dir;

        dir = tolookat.position - transform.position;
        angle = MathF.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,angle - 90f));
    }
}
