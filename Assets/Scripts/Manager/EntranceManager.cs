using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceManager : MonoBehaviour
{
    public GameObject m_Guest;
    private const int GUEST_COUNT = 4;
    private List<GameObject> m_Children = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < GUEST_COUNT; ++i)
        {
            GameObject child = GameObject.Instantiate(m_Guest) as GameObject;
            child.transform.position = new Vector2(0, 0);
            child.transform.SetParent(this.transform, false);
            child.SetActive(false);
            m_Children.Add(child);
        }
    }
}
