using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public event System.Action onSpeedUp;
    //public event System.Action onGameOver;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            onSpeedUp.Invoke();
        }
    }
}
