using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 未设置状态
/// </summary>
public class RoleStateNone : RoleStateAbstract
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="roleFsmMgr"></param>
    public RoleStateNone(RoleFSMMgr roleFsmMgr) : base(roleFsmMgr)
    {

    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void OnEnter()
    {

    }

    /// <summary>
    /// 执行状态(每帧执行)
    /// </summary>
    public override void OnUpdate()
    {

    }

    /// <summary>
    /// 离开状态
    /// </summary>
    public override void OnLeave()
    {

    }
}
