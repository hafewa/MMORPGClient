using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 有限状态机抽象基类
/// </summary>
public abstract class RoleStateAbstract
{
    /// <summary>
    /// 有限状态机管理器
    /// </summary>
    public RoleFSMMgr RoleFSMMgr { get; private set; }

    /// <summary>
    /// 角色当前动画信息
    /// </summary>
    public AnimatorStateInfo CurrAnimatorStateInfo { get; set; }

    /// <summary>
    /// 角色当前动画控制器
    /// </summary>
    public Animator CurrAinmator;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="roleFsmMgr"></param>
    public RoleStateAbstract(RoleFSMMgr roleFsmMgr)
    {
        RoleFSMMgr = roleFsmMgr;

        CurrAinmator = RoleFSMMgr.RoleCtrl.AnimaCtrl;
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 执行状态
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// 离开状态
    /// </summary>
    public virtual void OnLeave() { }
}
