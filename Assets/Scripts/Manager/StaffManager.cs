using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoSingleton<StaffManager>
{
    public GameObject m_Staff = null;
    public delegate void OnServed(Ingredient.FOOD_TYPE foodType, int tableIndex);
    public delegate void ClearDishCallback(Ingredient.FOOD_TYPE foodType, int dishIndex);

    private OnServed OnServedCallback = null;
    private ClearDishCallback OnClearDishCallback = null;
    private List<GameObject> m_StaffList = new List<GameObject>();
    private Dictionary<int, Ingredient.FOOD_TYPE> m_OrderedDic = new Dictionary<int, Ingredient.FOOD_TYPE>();
    private Dictionary<int, Ingredient.FOOD_TYPE> m_ReadyDic = new Dictionary<int, Ingredient.FOOD_TYPE>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject staff = GameObject.Instantiate(m_Staff) as GameObject;
        staff.transform.localPosition = new Vector2(0, 0);
        staff.transform.SetParent(this.transform, false);
        staff.GetComponent<Staff>().SetState(Staff.STATE.WAITING);
        staff.GetComponent<Staff>().OnSetServingIsDoneCallback(OnServingIsDone);
        m_StaffList.Add(staff);
        GuestManager.Instance().SetOnGetOrderCallback(OnGetOrder);
        DishManager.Instance().SetReadyFoodCallback(OnReadyFood);
        StartCoroutine(OnCheckTimer());
    }

    IEnumerator OnCheckTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            int tableIndex = 0;
            int dishIndex = 0;
            Ingredient.FOOD_TYPE foodType = Ingredient.FOOD_TYPE.NONE;
            if (false == Check(out foodType, out tableIndex, out dishIndex))
            {
                continue;
            }

            foreach(GameObject staff in m_StaffList)
            {
                if(Staff.STATE.SERVING == staff.GetComponent<Staff>().GetState())
                {
                    continue;
                }

                Debug.Log("서빙 시작!");
                staff.GetComponent<Staff>().SetState(Staff.STATE.SERVING);
                staff.GetComponent<Staff>().Serving(foodType, tableIndex);
                OnClearDishCallback(foodType, dishIndex);
            }
        }
    }

    public void OnServingIsDone(Ingredient.FOOD_TYPE foodType, int tableIndex)
    {
        OnServedCallback(foodType, tableIndex);
        foreach(GameObject staff in m_StaffList)
        {
            if(Staff.STATE.WAITING == staff.GetComponent<Staff>().GetState())
            {
                continue;
            }

            staff.GetComponent<Staff>().ReturnToWaitingPos();
        }
    }

    public void SetServedCallback(OnServed func)
    {
        OnServedCallback += func;
    }

    public void SetClearDishCallback(ClearDishCallback func)
    {
        OnClearDishCallback += func;
    }

    public void OnGetOrder(Ingredient.FOOD_TYPE foodType, int tableIndex)
    {
        if (m_OrderedDic.ContainsKey(tableIndex))
        {
            m_OrderedDic[tableIndex] = foodType;
            return;
        }

        Debug.Log("TableIndex : " + tableIndex);
        m_OrderedDic.Add(tableIndex, foodType);
    }

    public void OnReadyFood(Ingredient.FOOD_TYPE foodType, int dishIndex)
    {
        if(m_ReadyDic.ContainsKey(dishIndex))
        {
            m_ReadyDic[dishIndex] = foodType;
            return;
        }

        Debug.Log("DishIndex : " + dishIndex);
        m_ReadyDic.Add(dishIndex, foodType);
    }

    bool Check(out Ingredient.FOOD_TYPE foodType, out int tableIndex, out int dishIndex)
    {
        foodType = Ingredient.FOOD_TYPE.NONE;
        tableIndex = 0;
        dishIndex = 0;
        // foreach 정리가 필요함.
        foreach (KeyValuePair<int, Ingredient.FOOD_TYPE> orderedFood in m_OrderedDic)
        {
            if(Ingredient.FOOD_TYPE.NONE == orderedFood.Value)
            {
                continue;
            }

            foreach(KeyValuePair<int, Ingredient.FOOD_TYPE> readyFood in m_ReadyDic)
            {
                if(Ingredient.FOOD_TYPE.NONE == readyFood.Value)
                {
                    continue;
                }

                if(orderedFood.Value == readyFood.Value)
                {
                    foodType = readyFood.Value;
                    tableIndex = orderedFood.Key;
                    dishIndex = readyFood.Key;
                    m_ReadyDic[readyFood.Key] = Ingredient.FOOD_TYPE.NONE;
                    m_OrderedDic[orderedFood.Key] = Ingredient.FOOD_TYPE.NONE;
                    return true;
                }
            }
        }

        return false;
    }



}
