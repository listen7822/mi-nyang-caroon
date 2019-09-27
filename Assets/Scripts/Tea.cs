using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tea : Drink
{
    protected override void Start()
    {
        base.Start();
        base.m_Type = TYPE.TEA;
        gameObject.name = "Tea";
    }
}
