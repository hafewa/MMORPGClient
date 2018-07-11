using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物AI
/// </summary>
public class RoleMonsterAI : IRoleAI
{
    /// <summary>
    /// 下次巡逻间隔时间
    /// </summary>
    private float m_NextPatrolintervalTime;

    /// <summary>
    /// 下次攻击时间
    /// </summary>
    private float m_NextAttackTime;
    /// <summary>
    /// 角色控制器
    /// </summary>
    public RoleCtrl roleCtrl
    {
        get;
        set;
    }

    /// <summary>
    /// 执行AI(每帧执行)
    /// </summary>
    public void DoAI()
    {
        if(roleCtrl != null)
        {
            //如果敌人没有锁定主角就一直巡逻
            if (roleCtrl.LockEnemy == null)
            {
                //敌人巡逻间隔时间达到了
                if (Time.time > m_NextPatrolintervalTime)
                {
                    //每次的巡逻时间都是随机的
                    m_NextPatrolintervalTime = Time.time + UnityEngine.Random.Range(5, 10);

                    //敌人巡逻是以敌人的出生点为中心,自身的巡逻范围为半径的区域
                    Vector3 bornPos = roleCtrl.BornPos.position;

                    Vector3 targetPos = new Vector3(UnityEngine.Random.Range(bornPos.x + roleCtrl.PatrolRange * -1, bornPos.x + roleCtrl.PatrolRange), 0, UnityEngine.Random.Range(bornPos.z + roleCtrl.PatrolRange * -1, bornPos.z + roleCtrl.PatrolRange));

                    roleCtrl.MoveTo(targetPos);
                }

                //如果敌人和主角的距离小于敌人的可视范围就锁定主角
                if (Vector3.Distance(roleCtrl.transform.position, GlobalInit.Instance.MainRoleCtrl.transform.position) <= roleCtrl.ViewRange)
                {
                    roleCtrl.LockEnemy = GlobalInit.Instance.MainRoleCtrl;
                }
            }
            else//已经锁定了主角
            {
                //如果敌人和主角的距离大于敌人的可视范围就解除对主角的锁定
                if (Vector3.Distance(roleCtrl.transform.position, GlobalInit.Instance.MainRoleCtrl.transform.position) > roleCtrl.ViewRange)
                {
                    roleCtrl.LockEnemy = null;
                }
                else//主角在敌人的锁定期间
                {
                    //如果敌人和主角的距离小于敌人的攻击范围敌人就开始攻击主角
                    if (Vector3.Distance(roleCtrl.transform.position, GlobalInit.Instance.MainRoleCtrl.transform.position) <= roleCtrl.AttackRange)
                    {
                        //攻击间隔时间到了并且当前不是攻击状态才进行攻击
                        if(Time.time > m_NextAttackTime && roleCtrl.RoleFSMMgr.RoleStateType != RoleStateType.Attack)
                        {
                            //3-5秒进行一次攻击
                            m_NextAttackTime = Time.time + UnityEngine.Random.Range(3, 5);
                            roleCtrl.ToAttack();
                        }
                    }
                    else//不在攻击范围内就追主角
                    {
                        //只有敌人在待机状态下才能追主角
                        if (roleCtrl.RoleFSMMgr.RoleStateType == RoleStateType.Idle)
                        {
                            //这里不能直接使用主角的坐标,如果这样的话敌人追上主角后就会和主角重合,需要离主角有一点距离,这里使用了敌人的攻击范围做为随机因子来确定目标点
                            Vector3 targetPos = new Vector3(UnityEngine.Random.Range(roleCtrl.LockEnemy.transform.position.x + roleCtrl.AttackRange * -1, roleCtrl.LockEnemy.transform.position.x + roleCtrl.AttackRange), 0, UnityEngine.Random.Range(roleCtrl.LockEnemy.transform.position.z + roleCtrl.AttackRange * -1, roleCtrl.LockEnemy.transform.position.z + roleCtrl.AttackRange));
                            //达不到攻击范围的时候就一直追主角
                            roleCtrl.MoveTo(targetPos);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="RoleCtrl"></param>
    public RoleMonsterAI(RoleCtrl RoleCtrl)
    {
        roleCtrl = RoleCtrl;
    }
}
