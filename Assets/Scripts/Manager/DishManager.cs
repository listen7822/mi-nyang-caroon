using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoSingleton<DishManager>
{
    const int DISH_COUNT = 3;

    private Callback m_Callback = null;
    private List<GameObject> m_Dishes = new List<GameObject>();

    public GameObject m_Dish;
    public delegate void Callback(Ingredient.FOOD_TYPE food, int dishIndex);

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
            dish.GetComponent<Dish>().SetIndex(i);
            m_Dishes.Add(dish);
            ++nIndexX;
        }

        IngredientsManager.Instance().SetCallback(OnSetIngrediant);
        StaffManager.Instance().SetOnClearDishCallback(OnClear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject GetActiveDish()
    {
        foreach (GameObject dish in m_Dishes)
        {
            // 활성화된 접시를 일단 모두 끈다.
            if (Dish.DISH_STATE.ACTIVE == dish.GetComponent<Dish>().GetState())
            {
                return dish;
            }
        }

        return null;
    }

    public void SetCallback(Callback func)
    {
        m_Callback += func;
    }

    public void OnSetIngrediant(Ingredient.FOOD_TYPE foodType, Ingredient.INGREDIENT_TYPE ingrediantType)
    {
        GameObject dish = GetActiveDish();

        if(null == dish)
        {
            Debug.Log("활성화된 접시가 없습니다.");
            return;
        }

        if(true == dish.GetComponent<Dish>().SetIngrediant(foodType, ingrediantType))
        {
            m_Callback(foodType, dish.GetComponent<Dish>().GetIndex());
        }
    }

    public void SelectedDish(int dishIndex)
    {
        foreach(GameObject dish in m_Dishes)
        {
            // 활성화된 접시를 일단 모두 끈다.
            if(Dish.DISH_STATE.ACTIVE == dish.GetComponent<Dish>().GetState())
            {
                if(dishIndex == dish.GetComponent<Dish>().GetIndex())
                {
                    // 이미 활성화된 접시가 또 선택됨. 무시.
                    break;
                }

                dish.GetComponent<Dish>().ChangeToInActiveState();
            }

            // 선택된 접시를 찾아 활성화 시킨다.
            if(dishIndex == dish.GetComponent<Dish>().GetIndex())
            {
                dish.GetComponent<Dish>().ChangeToActiveState();
                break;
            }
        }
    }

    public void OnClear(Ingredient.FOOD_TYPE foodType, int dishIndex)
    {
        foreach(GameObject dish in m_Dishes)
        {
            if(dishIndex == dish.GetComponent<Dish>().GetIndex())
            {
                dish.GetComponent<Dish>().Clear(foodType);
            }
        }
    }
}
