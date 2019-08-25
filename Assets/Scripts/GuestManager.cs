using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public GameObject m_Guest;
    private const int GUEST_COUNT = 4;
    private List<GameObject> m_Children = new List<GameObject>();
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
            m_Children.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAvailableTable(int tableIndex)
    {
        foreach(GameObject guest in m_Children)
        {
            if(Guest.STATE.WAITING_OUTSIDE == guest.GetComponent<Guest>().GetState())
            {
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
                if(true == guest.GetComponent<Guest>().Leave())
                {
                    guest.GetComponent<Guest>().Clear();
                }
            }
        }
    }
}
