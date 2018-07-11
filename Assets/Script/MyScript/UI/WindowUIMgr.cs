using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowUIMgr : Singleton<WindowUIMgr>
{
    /// <summary>
    /// 已经打开的窗口的缓存
    /// </summary>
    private Dictionary<WindowUIType, UIWindowBase> m_WinDic = new Dictionary<WindowUIType, UIWindowBase>();

    public int OpenWindowCount
    {
        get
        {
            return m_WinDic.Count;
        }
    }

    #region OpenWindow 打开窗口
    /// <summary>
    /// 窗口UI加载
    /// </summary>
    /// <param name="type">窗口类型</param>
    /// <returns></returns>
    public GameObject OpenWindow(WindowUIType type)
    {
        if (type == WindowUIType.None) return null;

        GameObject go = null;

        UIWindowBase windowBase = null;

        if (!m_WinDic.ContainsKey(type))
        {
            //这里要去预设的前缀要跟WindowUIType的枚举名称一致
            string prefabName = type.ToString() + "Window";

            go = ResourcesMgr.Instance.Load(ResourcesType.UIWindow, prefabName, true);

            if (go == null) return null;

            //获取该窗口的UIWindowBase
            windowBase = go.GetComponent<UIWindowBase>();

            if (windowBase == null) return null;

            windowBase.curWindowType = type;

            m_WinDic.Add(type, windowBase);

            Transform container = null;

            switch (windowBase.containerType)
            {
                //获取该窗口应该挂在场景UI的哪个九宫格挂点上
                case ContainerType.Center:
                    container = SceneUIMgr.Instance.currentUIScene.m_ContainerCenter;
                    break;
            }

            go.transform.parent = container;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            NGUITools.SetActive(go, false);
        }
        else
        {
            windowBase = m_WinDic[type];
            go = windowBase.gameObject;
        }

        StartWindowShow(windowBase, true);

        //设置层级,最新打开的窗口的层级depth会自动增加层级数量(50为基数),以保证它在视图的最前面
        LayerUIMgr.Instance.SetLayer(go);

        return go;
    }
    #endregion

    #region CloseWindow 关闭窗口
    public void CloseWindow(WindowUIType type)
    {
        if(m_WinDic.ContainsKey(type))
        {
            StartWindowShow(m_WinDic[type], false);
        }
    }
    #endregion

    #region DestroyWindow 销毁窗口
    /// <summary>
    /// 销毁窗口
    /// </summary>
    /// <param name="go"></param>
    private void DestroyWindow(UIWindowBase windowBase)
    {
        if (m_WinDic.ContainsKey(windowBase.curWindowType))
            m_WinDic.Remove(windowBase.curWindowType);

        Object.Destroy(windowBase.gameObject);
    }
    #endregion

    #region 各种窗口打开效果
    /// <summary>
    /// 根据style来打开窗口或者关闭窗口
    /// </summary>
    /// <param name="go">窗口</param>
    /// <param name="style">样式</param>
    /// <param name="isOpen">是否打开还是关闭</param>
    public void StartWindowShow(UIWindowBase windowBase, bool isOpen)
    {
        switch (windowBase.windowShowStyle)
        {
            case WindowShowStyle.Normal:
                ShowWindowWithNormal(windowBase, isOpen);
                break;
            case WindowShowStyle.CenterToBig:
                ShowWindowWithCenterToBig(windowBase, isOpen);
                break;
            case WindowShowStyle.FromTop:
                ShowWindowWithDirection(windowBase, isOpen, 0);
                break;
            case WindowShowStyle.FromDown:
                ShowWindowWithDirection(windowBase, isOpen, 1);
                break;
            case WindowShowStyle.FromLeft:
                ShowWindowWithDirection(windowBase, isOpen, 2);
                break;
            case WindowShowStyle.FromRight:
                ShowWindowWithDirection(windowBase, isOpen, 3);
                break;
        }
    }

    /// <summary>
    /// 从各个方向打开窗口
    /// </summary>
    /// <param name="dir">0 上 1下 2 左 3 右</param>
    private void ShowWindowWithDirection(UIWindowBase windowBase, bool isOpen, int dir)
    {
        //创建或者获取TweenScale
        TweenPosition tp = windowBase.gameObject.GetOrCreateComponent<TweenPosition>();
        tp.animationCurve = GlobalInit.Instance.animationCurve;
        Vector3 from = Vector3.zero;

        switch (dir)
        {
            case 0:
                from = new Vector3(0, 1000, 0);
                break;
            case 1:
                from = new Vector3(0, -1000, 0);
                break;
            case 2:
                from = new Vector3(-1400, 0, 0);
                break;
            case 3:
                from = new Vector3(1400,0 , 0);
                break;
        }
        tp.from = from;
        tp.to = Vector3.one;
        tp.duration = windowBase.duration;
        tp.SetOnFinished(() =>
        {
            if (!isOpen)
                DestroyWindow(windowBase);
        });
        NGUITools.SetActive(windowBase.gameObject, true);
        //如果是关闭窗口就反方向播放动画(从大到小,当动画播放完毕后会自动销毁窗口)
        if (!isOpen)
            tp.Play(isOpen);
    }

    /// <summary>
    /// 正常打开窗口
    /// </summary>
    /// <param name="go"></param>
    /// <param name="isOpen"></param>
    private void ShowWindowWithNormal(UIWindowBase windowBase, bool isOpen)
    {
        if (isOpen)
        {
            NGUITools.SetActive(windowBase.gameObject, true);
        }
        else
        {
            DestroyWindow(windowBase);
        }
    }

    /// <summary>
    /// 从中心慢慢变大的效果
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="isOpen"></param>
    private void ShowWindowWithCenterToBig(UIWindowBase windowBase, bool isOpen)
    {
        //创建或者获取TweenScale
        TweenScale ts = windowBase.gameObject.GetOrCreateComponent<TweenScale>();
        ts.animationCurve = GlobalInit.Instance.animationCurve;
        ts.from = Vector3.zero;
        ts.to = Vector3.one;
        ts.duration = windowBase.duration;
        ts.SetOnFinished(() =>
        {
            if (!isOpen)
                DestroyWindow(windowBase);
        });
        NGUITools.SetActive(windowBase.gameObject, true);
        //如果是关闭窗口就反方向播放动画(从大到小,当动画播放完毕后会自动销毁窗口)
        if (!isOpen)
            ts.Play(isOpen);
    }
    #endregion
}
