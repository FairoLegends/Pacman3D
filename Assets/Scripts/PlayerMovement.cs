using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    //Mengatur kecepatan gerak player
    public float speed;
    public Camera camera;
    public float powerUpDuration;

    private Rigidbody rigidbody;
    private Coroutine powerUpCoroutine;

    public void PickPowerUp()
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        
        yield return new WaitForSeconds(powerUpDuration);

        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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

        Vector3 horizontalDirection = horizontal * camera.transform.right;
        Vector3 verticalDirection = vertical * camera.transform.forward ;
        verticalDirection.y = 0;
        horizontalDirection.y = 0;
        
        Vector3 movementDirection = horizontalDirection + verticalDirection;

        rigidbody.velocity = movementDirection * speed * Time.fixedDeltaTime;
    }
}
