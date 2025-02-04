using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSupplyDrop : MonoBehaviour, IItem
{
    private Rigidbody2D rb;
    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        MoveItem();
    }

    private void MoveItem()
    {
        rb.velocity = Vector3.down * speed;
    }

    public void UseItem(GameObject target)
    {
        var spawner = target.GetComponent<MinionSpawner>();
        spawner?.SpawnMinion();

        Destroy(gameObject);
    }
}
