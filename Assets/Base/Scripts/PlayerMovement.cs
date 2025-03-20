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
    public float rotationTime = 0.1f;


    private Rigidbody rigidbody;
    private Coroutine powerUpCoroutine;
    private bool isPowerUpActive = false;
    private float rotationVelocity;

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
            SceneManager.LoadScene("LoseScene");
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

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);

        if (movementDirection.magnitude >= 0.1)
        {
            float rotationAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref rotationVelocity, rotationTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            movementDirection = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;
        }

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
