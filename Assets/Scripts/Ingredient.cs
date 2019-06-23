using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public delegate void Callback(Ingredient ingredient);
    private Callback m_Callback = null;
    public string NAME = "Macaroon";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log("Click");
        m_Callback(this);
    }

    public void SetCallback(Callback func)
    {
        m_Callback += func;
    }
}
