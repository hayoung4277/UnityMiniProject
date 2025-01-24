using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteor : UnBreakable
{
    public string dataId = "050001";

    private Rigidbody2D rb;
    //private SpriteRenderer sprite;
    //private SpriteRenderer lineSprite;

    //private float imageToggleTime = 0.6f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.UnBreakableTable.Get(dataId);

        //var findGo = GameObject.FindWithTag("Warning");
        //sprite = findGo.GetComponent<SpriteRenderer>();

        //var findLine = GameObject.FindWithTag("Line");
        //lineSprite = findLine.GetComponent<SpriteRenderer>();

        //sprite.enabled = false;
        //lineSprite.enabled = false;

        IsSpawn = false;

        if (Data != null)
        {
            Initialized(Data);
        }
        else
        {
            Debug.LogError($"UnBreakable data with ID '{dataId}' not found.");
        }
    }

    private void Start()
    {
        //StartCoroutine(WarningToggle());
        StartCoroutine(MoveCoroutine());
    }

    private void Update()
    {
        //if(!IsSpawn)
        //{
        //    MoveToPlayer();
        //}
    }

    public override void MoveUnBreakable(Rigidbody2D rb)
    {
        base.MoveUnBreakable(rb);
    }

    public override void Destroyed()
    {
        base.Destroyed();
    }

    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
    }

    public override void Initialized(UnBreakableData data)
    {
        base.Initialized(data);
    }

    private IEnumerator MoveCoroutine()
    {
        //StartCoroutine(WarningToggle());

        yield return new WaitForSeconds(SpawnWarningTime);

        //sprite.enabled = false;
        //lineSprite.enabled = false;

        MoveUnBreakable(rb);
    }

    //private void MoveToPlayer()
    //{
    //    var currentPos = transform.position;

    //    currentPos.x = player.position.x;
    //    currentPos.y = transform.position.y;
    //}

    //private IEnumerator WarningToggle()
    //{
    //    var spritePos = transform.position;

    //    spritePos.y -= 2f;

    //    sprite.enabled = true;
    //    lineSprite.enabled = true;

    //    yield return new WaitForSeconds(imageToggleTime);

    //    sprite.enabled = false;

    //    yield return new WaitForSeconds(imageToggleTime);

    //    sprite.enabled = true;
    //}
}
