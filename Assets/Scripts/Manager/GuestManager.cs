using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoSingleton<GuestManager>
{
    public GameObject m_Guest;
    private const int GUEST_COUNT = 1;
    private List<GameObject> m_Children = new List<GameObject>();
    private OnFinishToEatCallback m_FinishToEatCallback = null;
    private OnGetOrderCallback m_OnGetOrderCallback = null;
    public delegate void OnFinishToEatCallback(int tableIndex);
    public delegate void OnGetOrderCallback(Ingredient.FOOD_TYPE foodType, int tableIndex);
    // Start is called before the first frame update
    void Start()
    {
        // int count = Mathf.FloorToInt(m_Size.x / (prefabSizeOfX + OFFSET));
        for (int i = 0; i < GUEST_COUNT; ++i)
        {
            GameObject child = GameObject.Instantiate(m_Guest) as GameObject;
            child.transform.position = new Vector2(0, 0);
            child.transform.SetParent(this.transform, false);
            child.SetActive(false);
            child.GetComponent<Guest>().SetState(Guest.STATE.WAITING_OUTSIDE);
            m_Children.Add(child);
        }

        TableManager.Instance().SetOnArrivedFoodCallback(OnArrivedFood);
        TableManager.Instance().SetOnAvailableTableCallback(OnAvailableTable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnGetOrderCallback(OnGetOrderCallback func)
    {
        m_OnGetOrderCallback += func;
    }

    public void SetOnFinishToEatCallback(OnFinishToEatCallback func)
    {
        m_FinishToEatCallback += func;
    }

    public void ArrivedTable(Ingredient.FOOD_TYPE food, int tableIndex)
    {
        m_OnGetOrderCallback(food, tableIndex);
    }

    public void OnAvailableTable(int tableIndex)
    {
        Debug.Log("테이블 사용가능!");
        foreach(GameObject guest in m_Children)
        {
            if(Guest.STATE.WAITING_OUTSIDE == guest.GetComponent<Guest>().GetState())
            {
                Debug.Log("손님 입장");
                guest.GetComponent<Guest>().SetState(Guest.STATE.INSIDE);
                guest.SetActive(true);
                guest.GetComponent<Guest>().GoToTable(tableIndex);
                break;
            }
        }
    }

    public void OnArrivedFood(int tableIndex)
    {
        foreach(GameObject guest in m_Children)
        {
            if(false == guest.activeSelf)
            {
                continue;
            }

            if(tableIndex == guest.GetComponent<Guest>().GetTableIndex())
            {
                int score = guest.GetComponent<Guest>().Eat();
                // TableManager에 다 먹었음을 알려준다. OnFinishToEat.
                m_FinishToEatCallback(tableIndex);
                guest.GetComponent<Guest>().Leave();
                guest.GetComponent<Guest>().SetState(Guest.STATE.WAITING_OUTSIDE);
            }
        }
    }
}
