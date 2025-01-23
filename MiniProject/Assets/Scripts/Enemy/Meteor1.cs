using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meteor1 : UnBreakable
{
    private static readonly string dataId = "050001";
    private Rigidbody2D rb;
    public Image image;
    private float imageToggleTime = 0.6f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Data = DataTableManager.UnBreakableTable.Get(dataId);
        image.enabled = false;

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
        StartCoroutine(WarningToggle());
        StartCoroutine(MoveCoroutine());
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
        yield return new WaitForSeconds(SpawnWarningTime);

        image.enabled = false;

        MoveUnBreakable(rb);
    }

    private IEnumerator WarningToggle()
    {
        image.enabled = true;
        
        yield return new WaitForSeconds(imageToggleTime);

        image.enabled = false;

        yield return new WaitForSeconds(imageToggleTime);

        image.enabled = true;
    }
}
