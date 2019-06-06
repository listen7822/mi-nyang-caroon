using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsManager : MonoBehaviour
{
    const int OFFSET = 10;
    public GameObject m_Prefab;

    private Vector2 m_Size;

    // Start is called before the first frame update
    void Start()
    {
        m_Size = GetComponent<RectTransform>().sizeDelta;
        int prefabSizeOfX = (int)m_Prefab.GetComponent<RectTransform>().sizeDelta.x;
        int count = Mathf.FloorToInt(m_Size.x / (prefabSizeOfX + OFFSET));
        for (int i = 0; i < count; ++i)
        {
            GameObject child = GameObject.Instantiate(m_Prefab) as GameObject;
            child.transform.position = new Vector2(i * (prefabSizeOfX + OFFSET), 0);
            child.transform.SetParent(this.transform, false);
        }
    } 

    // Update is called once per frame
    void Update()
    {
                
    }
}
