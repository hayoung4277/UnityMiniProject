using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSupplyDrop : MonoBehaviour, IItem
{
    public ItemType Type { get; set; }

    private Rigidbody2D rb;
    private float speed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Type = ItemType.Null;
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
        spawner?.SpawnMinion(Type);

        Debug.Log($"{Type}");

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestroyBox")
        {
            Destroy(gameObject);
        }
    }
}
