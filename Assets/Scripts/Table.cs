using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public enum STATE
    {
        NONE = 0,
        EMPTY = 1,
        USED = 2
    };

    private int m_Score = 0;
    private TableManager m_TableManger = null;
    private int m_index = 0;
    private STATE m_State = STATE.NONE;
    // Start is called before the first frame update
    void Start()
    {
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

    public STATE GetState()
    {
        return m_State;
    }

    public void SetState(STATE state)
    {
        m_State = state;
    }

    public int GetIndex()
    {
        return m_index;
    }

    public void SetIndex(int index)
    {
        m_index = index;
    }

    public bool SetFoodOnTable(Ingredient.FOOD_TYPE foodType)
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

    // Private region.
    private bool CheckCorrectFood(List<Ingredient> food)
    {
        if(3 != food.Count)
        {
            return false;
        }

        if(food[0].GetType() != food[1].GetType())
        {
            return false;
        }

        if(food[0].GetType() != food[2].GetType())
        {
            return false;
        }

        return true;
    }

    public void ClearTable()
    {
        //m_Score = 0;
        //GameObject foods = this.transform.Find("Foods").gameObject;
        //foreach(Transform childFood in foods.transform)
        //{
        //    childFood.gameObject.SetActive(false);
        //}
        m_State = STATE.EMPTY;
    }

}
