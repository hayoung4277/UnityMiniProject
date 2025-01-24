using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    public float Speed { get; private set; }

    private float speedUpTime = 0f;
    private float speedUpInterval = 10f;

    private GameManager gm;

    private void Awake()
    {
        Speed = 5f;
    }

    private void Start()
    {
        var findGo = GameObject.FindWithTag("GameController");
        gm = findGo.GetComponent<GameManager>();

        gm.onStopGame += StopScrolling;
        gm.onSpeedUp += SpeedUp;
    }

    private void Update()
    {
        //speedUpTime += Time.deltaTime;

        //if(speedUpTime >= speedUpInterval)
        //{
        //    SpeedUp();
        //    speedUpTime = 0f;
        //}

        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void StopScrolling()
    {
        Speed = 0f;
    }

    private void SpeedUp()
    {
        Speed = Speed * 2f;
        Debug.Log("Speed Up");
    }
}
