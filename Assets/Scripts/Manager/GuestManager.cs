using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoSingleton<GuestManager>
{
    public GameObject m_Guest;
    private const int GUEST_COUNT = 1;
    private List<GameObject> m_Guests = new List<GameObject>();
    private FinishToEatCallback OnFinishToEatCallback = null;
    private GetOrderCallback OnGetOrderCallback = null;
    public delegate void FinishToEatCallback(int tableIndex);
    public delegate void GetOrderCallback(Ingredient.FOOD_TYPE foodType, int tableIndex);
    // Start is called before the first frame update
    void Start()
    {
        // int count = Mathf.FloorToInt(m_Size.x / (prefabSizeOfX + OFFSET));
        for (int i = 0; i < GUEST_COUNT; ++i)
        {
            GameObject guest = GameObject.Instantiate(m_Guest) as GameObject;
            guest.transform.position = new Vector2(0, 0);
            guest.transform.SetParent(this.transform, false);
            guest.SetActive(false);
            guest.GetComponent<Guest>().SetState(Guest.STATE.WAITING_OUTSIDE);
            guest.GetComponent<Guest>().SetOrderFoodCallback(OnOrederFood);
            m_Guests.Add(guest);
        }

        TableManager.Instance().SetOnArrivedFoodCallback(OnArrivedFood);
        TableManager.Instance().SetOnAvailableTableCallback(OnAvailableTable);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnGetOrderCallback(GetOrderCallback func)
    {
        OnGetOrderCallback += func;
    }

    public void SetFinishToEatCallback(FinishToEatCallback func)
    {
        OnFinishToEatCallback += func;
    }

    public void OnOrederFood(Ingredient.FOOD_TYPE food, int tableIndex)
    {
        OnGetOrderCallback(food, tableIndex);
    }

    public void OnAvailableTable(int tableIndex)
    {
        foreach(GameObject guest in m_Guests)
        {
            if(Guest.STATE.WAITING_OUTSIDE == guest.GetComponent<Guest>().GetState())
            {
                Debug.Log("손님 입장");
                Debug.Log("테이블 사용가능! Index : " + tableIndex);
                guest.GetComponent<Guest>().SetState(Guest.STATE.INSIDE);
                guest.SetActive(true);
                guest.GetComponent<Guest>().GoToTable(tableIndex);
                break;
            }
        }
    }

    public void OnArrivedFood(int tableIndex)
    {
        foreach(GameObject guest in m_Guests)
        {
            if(false == guest.activeSelf)
            {
                continue;
            }

            if(tableIndex == guest.GetComponent<Guest>().GetTableIndex())
            {
                int score = guest.GetComponent<Guest>().Eat();
                // TableManager에 다 먹었음을 알려준다. OnFinishToEat.
                OnFinishToEatCallback(tableIndex);
                guest.GetComponent<Guest>().Leave();
            }
        }
    }
}
