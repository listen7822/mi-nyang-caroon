using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltManager : MonoBehaviour
{
    private Transform m_Child;
    private float m_velocity = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_Child = gameObject.transform.Find("ConveyorBelt");
    }

    // Update is called once per frame
    void Update()
    {
        m_Child.Translate(Vector2.left * m_velocity * Time.deltaTime, Space.Self);
    }
}
