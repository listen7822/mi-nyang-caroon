using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    public enum TYPE
    {
        NONE = 0,
        TEA,
        COFFEE,
    };

    [SerializeField]
    protected TYPE m_Type = TYPE.NONE;

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }
}
