using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoSingleton<DishManager>
{
    const int DISH_COUNT = 3;

    private ReadyFood OnReadyFoodCallback = null;
    private List<GameObject> m_Dishes = new List<GameObject>();

    public GameObject m_Dish;
    public delegate void ReadyFood(Ingredient.FOOD_TYPE food, int dishIndex);

    // Start is called before the first frame update
    void Start()
    {
        Vector2 dishTableSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 spaceThatOneObjectCanUse = new Vector2(dishTableSize.x / DISH_COUNT, 0);
        int indexX = 0;
        int dishIndex = 0;

        for (int i = 0; i < DISH_COUNT; ++i)
        {
            GameObject dish = GameObject.Instantiate(m_Dish) as GameObject;
            dish.transform.localPosition = new Vector2(indexX * spaceThatOneObjectCanUse.x, 0);
            dish.transform.SetParent(this.transform, false);
            dish.GetComponent<Dish>().SetIndex(dishIndex);
            dish.GetComponent<Dish>().SetSelectedDishCallback(OnSelectedDish);
            m_Dishes.Add(dish);
            ++indexX;
            ++dishIndex;
        }

        IngredientsManager.Instance().SetIngrediantCallback(OnSetIngrediant);
        StaffManager.Instance().SetClearDishCallback(OnClear);
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

    public void SetReadyFoodCallback(ReadyFood func)
    {
        OnReadyFoodCallback += func;
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
            OnReadyFoodCallback(foodType, dish.GetComponent<Dish>().GetIndex());
        }
    }

    public void OnSelectedDish(int dishIndex)
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
                Debug.Log("Clear Dish.");
                dish.GetComponent<Dish>().Clear(foodType);
            }
        }
    }
}
