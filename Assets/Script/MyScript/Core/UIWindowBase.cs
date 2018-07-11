using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口UI的基类
/// </summary>
public class UIWindowBase : UIBase
{
    /// <summary>
    /// 挂点类型(该窗口挂在场景UI的哪个九宫格挂点上)
    /// </summary>
    public ContainerType containerType;

    /// <summary>
    /// 窗口打开效果
    /// </summary>
    public WindowShowStyle windowShowStyle;

    /// <summary>
    /// 如果不是正常打开窗口的效果,这里设置窗口动画播放时长
    /// </summary>
    public float duration = 3.0f;

    /// <summary>
    /// 窗口的类型
    /// </summary>
    [HideInInspector]
    public WindowUIType curWindowType;

    /// <summary>
    /// 下一个要打开的窗口类型
    /// </summary>
    protected WindowUIType NextOpenWindow = WindowUIType.None;

    /// <summary>
    /// 关闭当前窗口
    /// </summary>
    protected void Close()
    {
        WindowUIMgr.Instance.CloseWindow(curWindowType);
    }

    /// <summary>
    /// 销毁方法,销毁当前窗口的同时再打开另一个需要打开的窗口,这样就不会出现打开新窗口和关闭当前窗口同时进行的情况,表现效果更好
    /// </summary>
    protected override void BeforeDestroy()
    {
        //每次关闭一个窗口的时候都检查一下窗口是不是都关闭了,如果都关闭了就将重置窗口层级
        LayerUIMgr.Instance.CheckWindow();
        if (NextOpenWindow == WindowUIType.None) return;
        WindowUIMgr.Instance.OpenWindow(NextOpenWindow);
    }
}
