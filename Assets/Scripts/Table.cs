using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    private List<Ingredient> m_Macaroon = new List<Ingredient>();
    public delegate void Callback(int point);
    private Callback m_Callback = null;
    private int m_Score = 0;
    private TableManager m_TableManger = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject staffManager = GameObject.Find("StaffManager") as GameObject;
        foreach(Transform staff in staffManager.transform)
        {
            staff.GetComponent<Staff>().SetCallbackTable(OnServingToGuest);
        }

        GameObject foods = this.transform.Find("Foods").gameObject;
        foreach(Transform food in foods.transform)
        {
            food.gameObject.SetActive(false);
        }

        GameObject guestManager = GameObject.Find("TableManager") as GameObject;

        m_TableManger = gameObject.transform.parent.GetComponent<TableManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool SetFoodOnTable(Food food)
    {
        // 테이블에 음식에 맞는 이미지를 띄운다.
        return true;
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
                foreach(Ingredient ingredient in food)
                {
                    if (childFood.name.Contains(ingredient.GetType().ToString()))
                    {
                        childFood.gameObject.SetActive(true);
                    }
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
        Debug.LogWarning("food count: " + food.Count);
        if(3 != food.Count)
        {
            return false;
        }

        if(food[0].GetType() != food[1].GetType())
        {
            Debug.LogWarning("1 ");
            return false;
        }

        if(food[0].GetType() != food[2].GetType())
        {
            Debug.LogWarning("2 ");
            return false;
        }

        return true;
    }

    public void Clear()
    {
        //m_Score = 0;
        //GameObject foods = this.transform.Find("Foods").gameObject;
        //foreach(Transform childFood in foods.transform)
        //{
        //    childFood.gameObject.SetActive(false);
        //}
    }
}
