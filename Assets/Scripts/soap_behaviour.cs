using UnityEngine;

public class soap_behaviour : MonoBehaviour
{
    public float shieldRotationSpeed;
    void Update()
    {
        transform.position = GameObject.FindWithTag("Player").transform.position;
        transform.Rotate(0, 0, shieldRotationSpeed * Time.deltaTime);
    }
}
