using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    const int OFFSET = 10;
    const int MAX_COUNT = 7;
    public GameObject m_Prefab;

    private Vector2 m_Size;

    // Start is called before the first frame update
    void Start()
    {
        m_Size = GetComponent<RectTransform>().sizeDelta;
        int prefabSizeOfX = (int)m_Prefab.GetComponent<RectTransform>().sizeDelta.x;
        int tmp = Mathf.FloorToInt(m_Size.x / MAX_COUNT);
        int tmp2 = tmp - prefabSizeOfX;
        // int count = Mathf.FloorToInt(m_Size.x / (prefabSizeOfX + OFFSET));
        for (int i = 0; i < MAX_COUNT; ++i)
        {
            GameObject child = GameObject.Instantiate(m_Prefab) as GameObject;
            child.transform.position = new Vector2((i+1) * (prefabSizeOfX + (tmp2)), 0);
            child.transform.SetParent(this.transform, false);
        }
    } 

    // Update is called once per frame
    void Update()
    {
                
    }
}
