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

    /// <summary>
    /// 下次攻击时间
    /// </summary>
    private float m_NextAttackTime = 0;

    /// <summary>
    /// 执行AI
    /// </summary>
    public void DoAI()
    {
        //如果主角有了锁定敌人
        if(roleCtrl.LockEnemy != null)
        {
            //敌人死了就不攻击了并且将锁定敌人清空
            if(roleCtrl.LockEnemy.CurrRoleInfo.CurrHp <=0)
            {
                roleCtrl.LockEnemy = null;
                m_NextAttackTime = 0;
                return;
            }

            if (Time.time > m_NextAttackTime && roleCtrl.RoleFSMMgr.RoleStateType != RoleStateType.Attack)
            {
                //1秒进行一次攻击
                m_NextAttackTime = Time.time + 1f;
                roleCtrl.ToAttack();
            }
        }
        else
        {
            m_NextAttackTime = 0;
        }
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
