using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupmanager : MonoBehaviour
{
    public GameObject[] powerups;
    public GameObject player;
    public PlayerController playercontroller;
    public bool piercingobtained;
    bool pointsfollowing;
    public GameObject bullet;
    public GameObject shield;
    //public enemy1script enemy1Script;
    public List<GameObject> powerupstoshow = new List<GameObject>();
    public bool p1;
    public bool p2;
    public bool p3;
    public bool p4;
    public bool p5;
    public bool p6;

    private void Update()
    {
        if (!p1 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 1)
        {
            p1 = true;
            StartCoroutine(PowerUp());
        }
        else if (!p2 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 3)
        {
            p2 = true;
            StartCoroutine(PowerUp());
        }
        else if (!p3 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 7)
        {
            p3 = true;
            StartCoroutine(PowerUp());
        }
        else if (!p4 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 12)
        {
            p4 = true;
            StartCoroutine(PowerUp());
        }
        else if (!p5 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 18)
        {
            p5 = true;
            StartCoroutine(PowerUp());
        }
        else if (!p6 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 24)
        {
            p6 = true;
            StartCoroutine(PowerUp());
        }
    }

     
    IEnumerator PowerUp()
    {
        Time.timeScale = 0f;
        while (powerupstoshow.Count < 3)
        {
            int randomindex = UnityEngine.Random.Range(0, powerups.Length);
            if (!powerupstoshow.Contains(powerups[randomindex]))
            {
                if (piercingobtained && randomindex == 7) continue;
                if (gameObject.activeSelf == true && randomindex == 8) continue;
                if (pointsfollowing&& randomindex == 5) continue;
                if (randomindex == 2) continue;
                powerupstoshow.Add(powerups[randomindex]);
            }
        }
        powerupstoshow[0].transform.localPosition = new Vector3(-231, -15, 0);
        powerupstoshow[1].transform.localPosition = new Vector3(7.2f, -15, 0);
        powerupstoshow[2].transform.localPosition = new Vector3(239f, -15, 0);
        foreach (GameObject a in powerupstoshow)
        {
            a.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(2.5f);
        Time.timeScale = 1f;
        foreach (GameObject a in powerupstoshow)
        {
            a.SetActive(false);
        }
        powerupstoshow.Clear();
    }
    
    void resumegame()
    {
        StopCoroutine(PowerUp());
        Time.timeScale = 1f;
        foreach (GameObject a in powerupstoshow)
        {
            a.SetActive(false);
        }
        powerupstoshow.Clear();
    }
    public void firespeedincrease()
    {
        playercontroller.bulletcooldown *= 0.5f;
        resumegame();
    }
    public void movementincrease()
    {
        playercontroller.speed *= 1.4f;
        resumegame();
    }
    public void damageincrease()
    {
        //GameObject.FindWithTag("bullet").GetComponent<bullet_behaviour>().damage += 0.5f;
        bullet.GetComponent<bullet_behaviour>().damage += 0.5f;
        resumegame();
    }
    public void healthincrease()
    {
        playercontroller.FullHealth += 3;
        playercontroller.Health = playercontroller.FullHealth;
        resumegame();
    }
    public void bulletradiusincrease()
    {
        playercontroller.scalemultiplier *= 1.4f;
        resumegame();
    }
    public void bulletpiercing()
    {
        piercingobtained = true;
        resumegame();
    }
    public void shields()
    {
        shield.SetActive(true);
        resumegame();
    }
    public void followpoint()
    {
        pointsfollowing = true;
        resumegame();
        //StartCoroutine(followplayer());
    }
    //IEnumerator followplayer()
    //{
    //    yield return null;
    //}
    private void FixedUpdate()
    {
        if (pointsfollowing)
        {
            GameObject[] points = GameObject.FindGameObjectsWithTag("point");
            foreach (GameObject p in points)
            {
                p.transform.position = Vector2.MoveTowards(p.transform.position, player.transform.position, 20f * Time.fixedDeltaTime);
            }
        }
    }
}
