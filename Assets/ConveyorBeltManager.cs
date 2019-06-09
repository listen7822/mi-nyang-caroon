using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltManager : MonoBehaviour
{
    private GameObject[] m_ConveyorBelts = new GameObject[3];
    private float m_velocity = 100.0f;

    public GameObject m_ConveyorBelt;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < m_ConveyorBelts.Length; ++i)
        {
            m_ConveyorBelts[i] = GameObject.Instantiate(m_ConveyorBelt) as GameObject;
            m_ConveyorBelts[i].transform.position = new Vector2(
                GetComponent<RectTransform>().rect.width * (i + 1)
                , 0
                );
            Debug.Log(GetComponent<RectTransform>().localScale.x * (i + 1));
            m_ConveyorBelts[i].transform.SetParent(this.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < m_ConveyorBelts.Length; ++i)
        {
            // 점점 벌어지는 문제...?
            m_ConveyorBelts[i].transform.Translate(Vector2.left * m_velocity * Time.deltaTime, Space.Self);
            Debug.Log(m_ConveyorBelts[i].transform.localPosition.x);
            if(-1920 >= m_ConveyorBelts[i].transform.localPosition.x)
            {
                m_ConveyorBelts[i].transform.localPosition = new Vector2(
                    GetComponent<RectTransform>().rect.width * 2
                    , 0
                    );
            }
        }
    }
}
