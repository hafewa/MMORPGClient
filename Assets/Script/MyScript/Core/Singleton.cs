using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例父类
/// </summary>
/// <typeparam name="T"></typeparam>
public class  Singleton<T>:IDisposable where T : new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = new T();

            return instance;
        }
    }

    /// <summary>
    /// 释放接口,可以被子类重写
    /// </summary>
    public virtual void Dispose()
    {
 
    }
}
