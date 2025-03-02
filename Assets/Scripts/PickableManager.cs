using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickableList = new List<Pickable>();

    void Start()
    {
        InItPickableList();
    }

    void InItPickableList()
    {
        Pickable[] pickablesObjects = GameObject.FindObjectsOfType<Pickable>();

        for (int i = 0; i < pickablesObjects.Length; i++)
        {
            _pickableList.Add(pickablesObjects[i]);
        }

        Debug.Log("Pickable List: " + _pickableList.Count);
    }
}