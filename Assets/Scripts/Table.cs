using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private List<Ingredient> m_Macaroon = new List<Ingredient>();
    public delegate void Callback(int point);
    private Callback m_Callback = null;
    private int m_Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject dishManager = GameObject.Find("DishManager") as GameObject;
        foreach(Transform dish in dishManager.transform)
        {
            //dish.GetComponent<Dish>().SetCallback(OnServingToGuest);
        }

        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            food.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnServingToGuest(List<Ingredient> food)
    {
        Debug.Log("OnServingToGuest");

        if (true == CheckCorrectFood(food))
        {
            m_Score = 100;
            GameObject foods = this.transform.Find("Foods").gameObject;
            foreach(Transform childFood in foods.transform)
            {
                if(food[0].NAME == childFood.name)
                {
                    childFood.gameObject.SetActive(true);
                }
            }
        }
    }

    public void SetCallback(Callback func)
    {
        m_Callback += func;
    }

    public void OnClick()
    {
        m_Callback(m_Score);
        Clear();
    }

    // Private region.
    private bool CheckCorrectFood(List<Ingredient> food)
    {
        bool isCorrectFood = false;
        //for(int i = 0; i < m_Macaroon.Count; ++i)
        //{
        //    if(m_Macaroon[i] == food[i])
        //    {
        //        continue;
        //    }

        //    isCorrectFood = false;
        //    break;
        //}
        if(0 >= food.Count)
        {
            return false;
        }

        return true;
    }

    private void Clear()
    {
        m_Score = 0;
        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform childFood in foods.transform)
        {
            childFood.gameObject.SetActive(false);
        }
    }
}
