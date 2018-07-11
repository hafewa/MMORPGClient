using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色信息基类
/// </summary>
public class RoleInfoBase
{
    /// <summary>
    /// 角色服务器id(相同的怪在服务器上的id也是不同的)
    /// </summary>
    public long RoleServerId;

    /// <summary>
    /// 角色id,每种怪对应唯一的Roleid
    /// </summary>
    public int RoleId;

    /// <summary>
    /// 最大血量
    /// </summary>
    public int MaxHp;

    /// <summary>
    /// 当前血量
    /// </summary>
    public int CurrHp;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName;
}
