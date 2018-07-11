using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerUIMgr : Singleton<LayerUIMgr>
{
    /// <summary>
    /// 初始层级
    /// </summary>
    private int layer = 50;

    /// <summary>
    /// 重置窗口层级,当所有窗口都关闭的时候就重置一下窗口的层级
    /// </summary>
    public void Reset()
    {
        layer = 50;
    }

    /// <summary>
    /// 当所有窗口都关闭的时候就重置一下窗口的层级
    /// </summary>
    public void CheckWindow()
    {
        if (WindowUIMgr.Instance.OpenWindowCount == 0)
            Reset();
    }

    /// <summary>
    /// 设置层级
    /// </summary>
    public void SetLayer(GameObject go)
    {
        layer += 1;
        UIPanel[] panArray = go.GetComponentsInChildren<UIPanel>();

        for(int i=0; i < panArray.Length; i++)
        {
            UIPanel curPanel = panArray[i];
            curPanel.depth += layer;
        }
    }
}
