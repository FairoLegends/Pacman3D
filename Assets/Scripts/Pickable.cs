using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [SerializeField]
    public PickableType pickableType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        Debug.Log("Pick Up " + pickableType);
        Destroy(gameObject);
        }

    }

    
}
