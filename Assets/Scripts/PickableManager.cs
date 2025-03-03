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
            pickablesObjects[i].OnPicked += OnPickablePicked;
        }

        Debug.Log("Pickable List: " + _pickableList.Count);
    }

    void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        Debug.Log("Pickable List: " + _pickableList.Count);
        if (_pickableList.Count <= 0)
        {
            Debug.Log("Menang");
        }
    }
}