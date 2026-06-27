using UnityEngine;
using UnityEngine.InputSystem;

public class followmouse : MonoBehaviour
{
    public Camera maincamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //maincamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = maincamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
}
