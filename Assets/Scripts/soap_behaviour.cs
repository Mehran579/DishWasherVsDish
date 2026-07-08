using UnityEngine;

public class soap_behaviour : MonoBehaviour
{
    public float shield_speed;
    void Update()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;
        transform.Rotate(0, 0, shield_speed * Time.deltaTime);
    }
}
