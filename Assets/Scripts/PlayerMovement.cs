using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Mengatur kecepatan gerak player
    public float speed;
    private Rigidbody rb;
    public Camera cam; 

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Horizontal = A atau Kiri (-) dan D atau Kanan (+)
        float horizontal = Input.GetAxis("Horizontal");
        //Horizontal = W atau Kedepan (+) dan S atau Kebelakang ()
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * cam.transform.right;
        Vector3 verticalDirection = vertical * cam.transform.forward ;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;
        
        Vector3 movementDirection = horizontalDirection + verticalDirection;

        rb.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }
}
