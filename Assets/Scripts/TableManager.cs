using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public GameObject m_Table;
    const int X_TABLE_COUNT = 2;
    const int Y_TABLE_COUNT = 2;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 hallSize = GetComponent<RectTransform>().sizeDelta;
        Vector2 spaceThatOneObjectCanUse = new Vector2(hallSize.x / X_TABLE_COUNT, hallSize.y / Y_TABLE_COUNT );
        int nIndexX = 0;
        int nIndexY = 0;

        for (int i = 0; i < Y_TABLE_COUNT; ++i)
        {
            for(int j = 0; j < X_TABLE_COUNT; ++j)
            {
                GameObject table = GameObject.Instantiate(m_Table) as GameObject;
                table.transform.localPosition = new Vector2(nIndexX * spaceThatOneObjectCanUse.x, -nIndexY * spaceThatOneObjectCanUse.y);
                table.transform.SetParent(this.transform, false);
                ++nIndexX;
            }
            nIndexX = 0;
            ++nIndexY;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
