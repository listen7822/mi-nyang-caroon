using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacaroonIngrediant : Ingredient 
{
    protected override void Start()
    {
        base.Start();
        m_FoodType = FOOD_TYPE.MACARRON;
    }
}
