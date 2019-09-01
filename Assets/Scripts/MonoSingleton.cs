﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T instance = null;
    public static T Instance()
    {
        instance = FindObjectOfType(typeof(T)) as T;
        if (instance == null)
        {
            instance = new GameObject("@" + typeof(T).ToString(),
                                       typeof(T)).AddComponent<T>();
            DontDestroyOnLoad(instance);
        }

        return instance;
    }
}
