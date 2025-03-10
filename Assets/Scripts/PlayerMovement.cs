using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    //Mengatur kecepatan gerak player
    public float speed;
    public Camera camera;
    public float powerUpDuration;
    public int Health;
    public TMP_Text healthText;
    public Transform respawnPoint;

    private Rigidbody rigidbody;
    private Coroutine powerUpCoroutine;
    private bool isPowerUpActive = false;

    public void Dead()
    {
        Health -= 1;

        if (Health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            Health = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        UpdateUI();
    }
   
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
        isPowerUpActive = true;
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        
        yield return new WaitForSeconds(powerUpDuration);
        isPowerUpActive = false;

        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }
    }

    void Awake()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (isPowerUpActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + Health;
    }
}
