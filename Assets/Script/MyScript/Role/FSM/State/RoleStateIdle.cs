using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 待机状态
/// </summary>
public class RoleStateIdle : RoleStateAbstract
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="roleFsmMgr"></param>
    public RoleStateIdle(RoleFSMMgr roleFsmMgr) : base(roleFsmMgr)
    {

    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void OnEnter()
    {
        //切换待机动画
        CurrAinmator.SetBool(ToAnimatorCondition.ToIdleFight.ToString(), true);
    }

    /// <summary>
    /// 执行状态(每帧执行)
    /// </summary>
    public override void OnUpdate()
    {
        CurrAnimatorStateInfo = CurrAinmator.GetCurrentAnimatorStateInfo(0);

        //给每个动画一个设置一个状态CurState,因为在动画控制器里使用的是AnyState进行动画的切换,所以需要判断当前动画是否就是要切换的动画,如果不是才切换,是的话就不切换了,否则AnyState会不停的自己切换到自己
        if (CurrAnimatorStateInfo.IsName(RoleAnimatorName.Idle_Fight.ToString()))
        {
            CurrAinmator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleStateType.Idle);
        }
        else
        {
            CurrAinmator.SetInteger(ToAnimatorCondition.CurState.ToString(), 0);
        }
    }

    /// <summary>
    /// 离开状态
    /// </summary>
    public override void OnLeave()
    {
        //离开待机状态将待机状态的条件设置为false
        CurrAinmator.SetBool(ToAnimatorCondition.ToIdleFight.ToString(), false);
    }
}
