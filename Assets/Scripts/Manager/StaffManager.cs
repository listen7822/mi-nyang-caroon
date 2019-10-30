using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffManager : MonoSingleton<StaffManager>
{
    public GameObject m_Staff = null;

    private List<GameObject> m_StaffList = new List<GameObject>();

    private Dictionary<int, Drink.TYPE> m_OrderedDrinkDic = new Dictionary<int, Drink.TYPE>();

    private Queue<Drink.TYPE> m_ReadyDrinkQue = new Queue<Drink.TYPE>();

    public Staff staff;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject staffobj = GameObject.Instantiate(m_Staff) as GameObject;
        staffobj.transform.localPosition = new Vector2(-830, 0);
        staffobj.transform.SetParent(this.transform, false);
        staff = staffobj.GetComponent<Staff>();
        m_StaffList.Add(staffobj);
    }

}


