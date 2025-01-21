using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon1 : Monster
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        MonsterSpeed = 3f;
    }

    private void Update()
    {
        MonsterDown(rb);
    }

    public override void MonsterDown(Rigidbody2D rb)
    {
        base.MonsterDown(rb);
    }
}
