using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsHandler : MonoBehaviour
{
    public static GameEventsHandler Instance;

    public event EventHandler OnMonumentFound;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void MonumentFound(GameObject monument)
    {
        OnMonumentFound?.Invoke(monument, EventArgs.Empty);
    }
}
