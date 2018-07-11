using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景UI基类
/// </summary>
public class UISceneBase: UIBase
{
    /// <summary>
    /// 中心挂点(自适应使用),只有场景UI才有九宫格挂点
    /// </summary>
    public Transform m_ContainerCenter;
}
