using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI接口
/// </summary>
public interface IRoleAI
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    RoleCtrl roleCtrl
    {
        get;
        set;
    }
    /// <summary>
    /// 执行AI方法
    /// </summary>
    void DoAI();
}
