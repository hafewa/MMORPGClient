using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 刷怪器
/// </summary>
public class MonsterCreateMachine : MonoBehaviour
{
    /// <summary>
    /// 最大刷怪数量
    /// </summary>
    [SerializeField]
    private int m_MaxCount;

    /// <summary>
    /// 当前已经刷怪数量
    /// </summary>
    private int m_CurrCount = 0;

    /// <summary>
    /// 怪物的名称
    /// </summary>
    [SerializeField]
    private string m_MonsterName;

    /// <summary>
    /// 刷怪时间
    /// </summary>
    private float m_CreateTime = 0;

    /// <summary>
    /// 刷怪
    /// </summary>
    void Update ()
    {
		if(m_CurrCount < m_MaxCount)
        {
            if (Time.time > m_CreateTime)
            {
                m_CreateTime = Time.time + UnityEngine.Random.Range(1.5f, 3.5f);
                GameObject moster = RoleMgr.Instance.LoadRole(RoleType.Monster, m_MonsterName);
                moster.transform.parent = transform;
                //在指定区域随机刷处怪物
                moster.transform.position = transform.transform.TransformPoint(new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0, UnityEngine.Random.Range(-0.5f, 0.5f)));
                m_CurrCount++;

                RoleInfoMonster roleInfo = new RoleInfoMonster();
                roleInfo.NickName = "骑兵";
                roleInfo.RoleServerId = DateTime.Now.Ticks;
                roleInfo.MaxHp = roleInfo.CurrHp = 100;
                roleInfo.RoleId = 1;

                RoleCtrl ctrl = moster.GetComponent<RoleCtrl>();
                ctrl.BornPos = transform;

                RoleMonsterAI ai = new RoleMonsterAI(ctrl);

                ctrl.Init(RoleType.Monster, roleInfo, ai);
            }
        }
	}
}
