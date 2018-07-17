using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 账号实体
/// </summary>
public class AccountEntity
{
    /// <summary>
    /// id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Pwd { get; set; }

    /// <summary>
    /// 元宝
    /// </summary>
    public int YuanBao { get; set; }

    /// <summary>
    /// 退出时的服务器id
    /// </summary>
    public int LastServerId { get; set; }

    /// <summary>
    /// 退出时的服务器名称
    /// </summary>
    public string LastServerName { get; set; }

    /// <summary>
    /// 创建角色的时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime UpdateTime { get; set; }
}
