using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 所有UI的基类
/// </summary>
public class UIBase : MonoBehaviour
{
    void Awake()
    {
        //给子类调用
        OnAwake();
    }

    /// <summary>
    /// 这里会调用具体哪一个子类的点击事件
    /// </summary>
    /// <param name="go"></param>
    private void Click(GameObject go)
    {
        OnClickEvent(go);
    }

    private void Start()
    {
        //寻找界面上的所有按钮并给其注册事件
        UIButton[] bttonArrays = GetComponentsInChildren<UIButton>(true);

        //给登陆窗口界面的所有按钮注册事件
        for (int i = 0; i < bttonArrays.Length; i++)
        {
            UIEventListener.Get(bttonArrays[i].gameObject).onClick = Click;
        }

        OnStart();
    }

    private void OnDestroy()
    {
        BeforeDestroy();
    }

    /// <summary>
    /// 给子类使用的Start方法
    /// </summary>
    protected virtual void OnStart() { }

    /// <summary>
    /// 子类使用的点击事件
    /// </summary>
    /// <param name="go"></param>
    protected virtual void OnClickEvent(GameObject go) { }

    /// <summary>
    /// 子类的Awake方法
    /// </summary>
    protected virtual void OnAwake() { }

    /// <summary>
    /// 子类使用的销毁方法
    /// </summary>
    protected virtual void BeforeDestroy() { }
}
