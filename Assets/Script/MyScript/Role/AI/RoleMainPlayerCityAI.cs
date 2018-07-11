using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角主城的AI
/// </summary>
public class RoleMainPlayerCityAI : IRoleAI
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    public RoleCtrl roleCtrl
    {
        get;
        set;
    }

    public void DoAI()
    {

    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="rolectrl"></param>
    public RoleMainPlayerCityAI(RoleCtrl rolectrl)
    {
        roleCtrl = rolectrl;
    }
}
