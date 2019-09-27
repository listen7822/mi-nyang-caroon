using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Drink
{
    protected override void Start()
    {
        base.Start();
        base.m_Type = TYPE.COFFEE;
        gameObject.name = "Coffee";
    }
}
