using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public GameObject m_Dish;
    const int DISH_COUNT = 3;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 dishTableSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 spaceThatOneObjectCanUse = new Vector2(dishTableSize.x / DISH_COUNT, 0);
        int nIndexX = 0;

        for (int i = 0; i < DISH_COUNT; ++i)
        {
            GameObject dish = GameObject.Instantiate(m_Dish) as GameObject;
            dish.transform.localPosition = new Vector2(nIndexX * spaceThatOneObjectCanUse.x, 0);
            dish.transform.SetParent(this.transform, false);
            ++nIndexX;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
