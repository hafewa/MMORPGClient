using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色有限状态机管理类
/// </summary>
public class RoleFSMMgr
{
    /// <summary>
    /// 角色控制器,只能在本类中set,可以在外部获取
    /// </summary>
    public RoleCtrl RoleCtrl { get; private set; }

    /// <summary>
    /// 角色当前状态枚举
    /// </summary>
    public RoleStateType RoleStateType { get; set; }

    /// <summary>
    /// 当前角色当前状态机
    /// </summary>
    private RoleStateAbstract m_RoleState;

    /// <summary>
    /// 角色状态枚举和状态机对应的字典
    /// </summary>
    private Dictionary<RoleStateType, RoleStateAbstract> m_RoleStateDic;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="rolectrl"></param>
    public RoleFSMMgr(RoleCtrl rolectrl)
    {
        RoleCtrl = rolectrl;

        RoleStateType = RoleStateType.None;

        m_RoleStateDic = new Dictionary<RoleStateType, RoleStateAbstract>();
        //初始化角色状态机字典
        m_RoleStateDic.Add(RoleStateType.None, new RoleStateNone(this));
        m_RoleStateDic.Add(RoleStateType.Idle, new RoleStateIdle(this));
        m_RoleStateDic.Add(RoleStateType.Run, new RoleStateRun(this));
        m_RoleStateDic.Add(RoleStateType.Hurt, new RoleStateHurt(this));
        m_RoleStateDic.Add(RoleStateType.Die, new RoleStateDie(this));
        m_RoleStateDic.Add(RoleStateType.Attack, new RoleStateAttack(this));

        if(m_RoleStateDic.ContainsKey(RoleStateType))
        {
            m_RoleState = m_RoleStateDic[RoleStateType];
        }
    }

    /// <summary>
    /// 每帧执行
    /// </summary>
    public void OnUpdate()
    {
        if (m_RoleState != null)
            m_RoleState.OnUpdate();
    }

    /// <summary>
    ///  切换状态
    /// </summary>
    /// <param name="newState">新的状态</param>
    public void ChangeState(RoleStateType newState)
    {
        if (newState == RoleStateType) return;

        if (m_RoleState == null) return;
        //离开之前的状态
        m_RoleState.OnLeave();

        //将当前状态更新为新的状态
        RoleStateType = newState;

        m_RoleState = m_RoleStateDic[newState];

        //调用新状态的进入方法
        m_RoleState.OnEnter();
    }
}
