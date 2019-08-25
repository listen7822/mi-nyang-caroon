using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public GameObject m_Dish;

    private List<GameObject> m_Dishes = new List<GameObject>();
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
            m_Dishes.Add(dish);
            ++nIndexX;
        }

        GameObject dishManager = GameObject.Find("DishManager") as GameObject;
        foreach (Transform dish in dishManager.transform)
        {
            dish.GetComponent<Dish>().SetCallbackDishManager(OnChangeDishState);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectedDish(Dish selecedDish)
    {
        foreach(GameObject dish in m_Dishes)
        {
            if(Dish.DISH_STATE.ACTIVE == dish.GetComponent<Dish>().GetState())
            {
                dish.GetComponent<Dish>().ChangeToInActiveState();
            }
        }

        selecedDish.ChangeToActiveState();
    }

    public void OnSetIngrediant(Ingredient ingredient)
    {
        foreach(GameObject dish in m_Dishes)
        {
            if (Dish.DISH_STATE.ACTIVE == dish.GetComponent<Dish>().GetState())
            {
                Food food = dish.GetComponent<Dish>().SetIngrediant(ingredient);
                if(null != food)
                {
                    int index = m_Dishes.IndexOf(dish);
                    // StaffManager에 OnReadyFood 함수로 완성된 접시 index와 food를 넘긴다.
                }
            }
        }
    }

    void OnChangeDishState(Dish dish)
    {
    }
}
