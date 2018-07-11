using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : MonoBehaviour
{
    /// <summary>
    /// 点击事件委托
    /// </summary>
    public Action<GameObject> onHit;

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="go"></param>
    public void Hit(GameObject go)
    {
        if (onHit != null)
            onHit(go);
    }
}
