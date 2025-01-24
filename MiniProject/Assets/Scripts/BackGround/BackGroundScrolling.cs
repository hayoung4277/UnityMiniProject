using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    public float Speed { get; private set; }

    private GameManager gm;

    private void Awake()
    {
        Speed = 5f;
    }

    private void Start()
    {
        var findGo = GameObject.FindWithTag(GMCT.GM);
        gm = findGo.GetComponent<GameManager>();

        gm.onStopGame += StopScrolling;
        gm.onSpeedUp += SpeedUp;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    private void StopScrolling()
    {
        Speed = 0f;
    }

    private void SpeedUp()
    {
        Speed = Speed * 1.1f;
        Debug.Log("Speed Up");
    }
}
