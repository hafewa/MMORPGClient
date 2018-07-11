using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 场景类型
public enum SceneType
{
    LogOn,
    City,
}
#endregion

#region 资源类型
/// <summary>
/// 资源类型
/// </summary>
public enum ResourcesType
{
    /// <summary>
    /// 场景UI
    /// </summary>
    UIScene,
    /// <summary>
    /// 窗口UI
    /// </summary>
    UIWindow,
    /// <summary>
    /// 角色
    /// </summary>
    Role,
    /// <summary>
    /// 特效
    /// </summary>
    Effect,

    /// <summary>
    /// 其他UI预设
    /// </summary>
    UIOther,
}
#endregion

#region SceneUIType 场景UI类型
/// <summary>
/// 场景UI类型
/// </summary>
public enum SceneUIType
{
    /// <summary>
    /// 登陆场景UI
    /// </summary>
    LogOn,
    /// <summary>
    /// 加载场景UI
    /// </summary>
    Loading,
    /// <summary>
    /// 主城场景
    /// </summary>
    MainCity,
}
#endregion


#region WindowUIType 窗口类型
/// <summary>
/// 场景UI类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 登陆窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Register,

    /// <summary>
    /// 角色信息窗口
    /// </summary>
    RoleInfo,
}
#endregion

#region ContainerType 挂点类型
/// <summary>
/// 挂点类型
/// </summary>
public enum ContainerType
{
    /// <summary>
    /// 左上挂点
    /// </summary>
    LeftTop,
    /// <summary>
    /// 右上挂点
    /// </summary>
    RightTop,
    /// <summary>
    /// 左下挂点
    /// </summary>
    LeftBottom,
    /// <summary>
    /// 右下挂点
    /// </summary>
    RightBottom,
    /// <summary>
    /// 中间挂点
    /// </summary>
    Center,
}
#endregion

#region 窗口打开效果的类型
public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中心慢慢变大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上到下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下到上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左到右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右到左
    /// </summary>
    FromRight,
}
#endregion

#region 角色类型
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 主角
    /// </summary>
    MainPlayer,
    /// <summary>
    /// 怪
    /// </summary>
    Monster,
}
#endregion

#region 角色状态类型
public enum RoleStateType
{
    None,
    Idle,
    Run,
    Hurt,
    Die,
    Attack,
    Skill,
    Select,
    XiuXian,
}
#endregion

#region 角色动画名称
public enum RoleAnimatorName
{
    Idle_Normal,
    Idle_Fight,
    Run,
    Hurt,
    Die,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3,
    Skill1,
    Skill2,
    Skill3,
    Skill4,
    Skill5,
    Skill6,
    Select,
    XiuXian,
}
#endregion

#region 角色动画切换条件
public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDie,
    ToPhyAttack,
    ToSkill,
    ToSelect,
    ToXiuXian,
    CurState,
}
#endregion
