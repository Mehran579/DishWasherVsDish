using System.Collections;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    public GameObject[] wave1;
    public int wave1creds;
    public GameObject[] wave2;
    public int wave2creds;
    public GameObject[] wave3;
    public int wave3creds;
    public GameObject[] wave4;
    public int wave4creds;
    public int currentwave = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawncooldownwave1());
    }

    // Update is called once per frame
    
    
    
    IEnumerator spawncooldownwave1()
    {
        while (currentwave == 1)
        {
        GameObject enemyprefab = wave1[Random.Range(0, wave1.Length)];
        GameObject enemy = Instantiate(enemyprefab,new Vector2(Random.Range(-52.9f,52.9f),Random.Range(-29.8f,29.8f)),Quaternion.identity);
        enemy.name = enemyprefab.name;
        if (enemy.gameObject.name == "enemy1" )
        {
            wave1creds -= 1;
        }else if(enemy.gameObject.name == "enemy2")
        {
            wave1creds -= 2;
        }
        if(wave1creds <= 0)
            {
                currentwave = 2;
                StartCoroutine(spawncooldownwave2());
            }
        yield return new WaitForSeconds(1);

        }
    }

    IEnumerator spawncooldownwave2()
    {
        while (currentwave == 2)
        {

        GameObject enemyprefab = wave2[Random.Range(0, wave2.Length)];
        GameObject enemy = Instantiate(enemyprefab, new Vector2(Random.Range(-52.9f, 52.9f), Random.Range(-29.8f, 29.8f)), Quaternion.identity);
        enemy.name = enemyprefab.name;
        if (enemy.gameObject.name == "enemy1")
        {
            wave2creds -= 1;
        }
        else if (enemy.gameObject.name == "enemy2")
        {
            wave2creds -= 2;
        }else if(enemy.gameObject.name == "enemy3")
        {
            wave2creds -= 2;
        }else if (enemy.gameObject.name == "enemy4")
        {
            wave2creds -= 3;
        }
        if (wave2creds <= 0)
        {
            currentwave = 3;
            StartCoroutine(spawncooldownwave3());
        }
        yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator spawncooldownwave3()
    {
        while (currentwave == 3)
        {

            GameObject enemyprefab = wave3[Random.Range(0, wave3.Length)];
            GameObject enemy = Instantiate(enemyprefab, new Vector2(Random.Range(-52.9f, 52.9f), Random.Range(-29.8f, 29.8f)), Quaternion.identity);
            enemy.name = enemyprefab.name;
            if (enemy.gameObject.name == "enemy1")
            {
                wave3creds -= 1;
            }
            else if (enemy.gameObject.name == "enemy2")
            {
                wave3creds -= 2;
            }
            else if (enemy.gameObject.name == "enemy3")
            {
                wave3creds -= 2;
            }
            else if (enemy.gameObject.name == "enemy4")
            {
                wave3creds -= 3;
            }else if (enemy.gameObject.name == "enemy5")
            {
                wave3creds -= 3;
            }else if(enemy.gameObject.name == "enemy7")
            {
                wave3creds -= 4;
            }
            if (wave3creds <= 0)
            {
                currentwave = 4;
                StartCoroutine(spawncooldownwave4());
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator spawncooldownwave4()
    {
        while (currentwave == 4) 
        {
            GameObject enemyprefab = wave4[Random.Range(0, wave4.Length)];
            GameObject enemy = Instantiate(enemyprefab, new Vector2(Random.Range(-52.9f, 52.9f), Random.Range(-29.8f, 29.8f)), Quaternion.identity);
            enemy.name = enemyprefab.name;
            if (enemy.gameObject.name == "enemy1")
            {
                wave4creds -= 1;
            }
            else if (enemy.gameObject.name == "enemy2")
            {
                wave4creds -= 2;
            }
            else if (enemy.gameObject.name == "enemy3")
            {
                wave4creds -= 2;
            }
            else if (enemy.gameObject.name == "enemy4")
            {
                wave4creds -= 3;
            }
            else if (enemy.gameObject.name == "enemy5")
            {
                wave4creds -= 3;
            }
            else if (enemy.gameObject.name == "enemy7")
            {
                wave4creds -= 4;
            }
            else if(enemy.gameObject.name == "enemy8")
            {
                wave4creds -= 4;
            }
            if (wave4creds <= 0)
            { 
                currentwave = 5;
                Debug.Log("Game Finished");
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
