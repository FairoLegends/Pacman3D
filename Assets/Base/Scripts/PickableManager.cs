using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _player;
    [SerializeField]
    private ScoreManager scoreManager;

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

        scoreManager.SetMaxScore(_pickableList.Count);
    }

    void OnPickablePicked(Pickable pickable)
    {
        _pickableList.Remove(pickable);
        if (scoreManager != null)
        {
            scoreManager.AddScore(1);
        }

        if (pickable.pickableType == PickableType.PowerUP)
        {
            _player?.PickPowerUp();
        }

        if (_pickableList.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}