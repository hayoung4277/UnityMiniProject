using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoof : MonoBehaviour
{
    private float height = 15.36f;

    private void Update()
    {
        if (transform.position.y < -height)
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        var pos = transform.position;
        pos.y += height * 2f;

        transform.position = pos;
    }
}
