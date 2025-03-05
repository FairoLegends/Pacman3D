using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _player;

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
    }

    void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (pickable.pickableType == PickableType.PowerUP)
        {
            _player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            Debug.Log("Menang");
        }
    }
}