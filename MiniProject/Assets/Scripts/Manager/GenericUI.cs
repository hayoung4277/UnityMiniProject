using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GenericUI : MonoBehaviour
{
    protected virtual void Awake()
    {
        Close();
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
