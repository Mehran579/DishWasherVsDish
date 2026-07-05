using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class powerupmanager : MonoBehaviour
{
    public GameObject[] powerups;
    public bool p1;
    public bool p2;
    public bool p3;
    public bool p4;
    public bool p5;

    private void Update()
    {
        if (!p1 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 1)
        {
            p1 = true;
            StartCoroutine(PowerUp());
        }
        else if(!p2 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 3)
        {
            p2 = true;
            StartCoroutine(PowerUp());
        }
        else if(!p3 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 7)
        {
            p3 = true;
            StartCoroutine(PowerUp());
        }
        else if(!p4 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 12) 
        {
            p4 = true;
            StartCoroutine(PowerUp());
        }
        else if(!p5 && GameObject.FindWithTag("Player").GetComponent<PlayerController>().points == 18)
        {
            p5 = true;
            StartCoroutine(PowerUp());
        }
    }

    IEnumerator PowerUp()
    {
        Time.timeScale = 0f;
        List<GameObject> powerupstoshow = new List<GameObject>();
        while(powerupstoshow.Count < 3)
        {
            int randomindex = UnityEngine.Random.Range(0, powerups.Length);
            if (!powerupstoshow.Contains(powerups[randomindex]))
            {
                powerupstoshow.Add(powerups[randomindex]);
            }
        }
        powerupstoshow[0].transform.localPosition = new Vector3(-231, -15, 0);
        powerupstoshow[1].transform.localPosition = new Vector3(7.2f, -15, 0);
        powerupstoshow[2].transform.localPosition = new Vector3(239f, -15, 0);
        foreach(GameObject a in powerupstoshow)
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
}
