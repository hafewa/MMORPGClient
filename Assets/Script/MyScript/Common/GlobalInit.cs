using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInit : MonoBehaviour
{
    public static GlobalInit Instance;

    /// <summary>
    /// 单机模式下的账号
    /// </summary>
    public const string MMO_NICKNAME = "mmo_nickname";

    /// <summary>
    /// 单机模式下的密码
    /// </summary>
    public const string MMO_PWD = "mmo_pwd";

    /// <summary>
    /// 角色昵称
    /// </summary>
    [HideInInspector]
    public string MainRoleNickName = "";

    /// <summary>
    /// 主角控制器
    /// </summary>
    [HideInInspector]
    public RoleCtrl MainRoleCtrl;

    /// <summary>
    ///动画曲线,用来做UI动画,可以在编辑器里自由设置
    /// </summary>
    public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
