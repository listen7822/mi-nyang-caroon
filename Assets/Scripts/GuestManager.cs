using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    public GameObject m_Guest;
    private const int GUEST_COUNT = 10;
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
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
