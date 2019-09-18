using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    const int OFFSET = 10;
    const int MAX_COUNT = 7;
    public GameObject m_Ingredient;

    public void SetIngrediant()
    {
        Vector2 traySize;
        Vector2 ingredientSize = m_Ingredient.GetComponent<RectTransform>().sizeDelta;

        traySize = GetComponent<RectTransform>().sizeDelta;
        float spaceThatOneObjectCanUse = traySize.x / MAX_COUNT;
        float spaceBetweenIngrediant = (spaceThatOneObjectCanUse - ingredientSize.x) / 2;
        int centerOffset = Mathf.FloorToInt((traySize.y - ingredientSize.y) / 2);
        int index = 0;
        for (int i = 0; i < MAX_COUNT; ++i)
        {
            GameObject ingredient = GameObject.Instantiate(m_Ingredient) as GameObject;
            ingredient.transform.localPosition = new Vector2((index * spaceThatOneObjectCanUse) + spaceBetweenIngrediant, -centerOffset);
            ingredient.transform.SetParent(this.transform, false);
            ++index;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
