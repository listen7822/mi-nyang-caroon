using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public GameObject m_Staff = null;
    private List<GameObject> m_StaffList = new List<GameObject>();
    private Dictionary<int, Food> m_OrderedDic = new Dictionary<int, Food>();
    private Dictionary<int, Food> m_ReadyDic = new Dictionary<int, Food>();
    // Start is called before the first frame update
    void Start()
    {
        GameObject staff = GameObject.Instantiate(m_Staff) as GameObject;
        staff.transform.localPosition = new Vector2(0, 0);
        staff.transform.SetParent(this.transform, false);
        m_StaffList.Add(staff);
    }

    void Update()
    {
    }

    public void OnGetOrder(int tableIndex, Food food)
    {
        if (m_OrderedDic.ContainsKey(tableIndex))
        {
            m_OrderedDic[tableIndex] = food;
            return;
        }

        m_OrderedDic.Add(tableIndex, food);
    }

    public void OnReadyFood(int dishIndex, Food food)
    {
        if(m_ReadyDic.ContainsKey(dishIndex))
        {
            m_ReadyDic[dishIndex] = food;
            return;
        }

        m_ReadyDic.Add(dishIndex, food);
    }

    void Check()
    {
        foreach(KeyValuePair<int, Food> orderedFood in m_OrderedDic)
        {
            foreach(KeyValuePair<int, Food> readyFood in m_ReadyDic)
            {
                if(orderedFood.Value == readyFood.Value)
                {
                    foreach(GameObject staff in m_StaffList)
                    {
                        if(Staff.STATE.WAITING == staff.GetComponent<Staff>().GetState())
                        {
                            // DishManager에 OnClear 콜백 호출.
                            if(true == staff.GetComponent<Staff>().Serving(orderedFood.Key, orderedFood.Value))
                            {
                                // TableManager에 서빙이 완료되었음을 알려줌.OnServed.
                            }

                            break;
                        }
                    }
                }
            }
        }
    }



}
