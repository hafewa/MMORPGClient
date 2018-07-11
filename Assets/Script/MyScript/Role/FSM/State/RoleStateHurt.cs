using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受伤状态
/// </summary>
public class RoleStateHurt : RoleStateAbstract
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="roleFsmMgr"></param>
    public RoleStateHurt(RoleFSMMgr roleFsmMgr) : base(roleFsmMgr)
    {

    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void OnEnter()
    {
        //切换受伤状态
        CurrAinmator.SetBool(ToAnimatorCondition.ToHurt.ToString(), true);
    }

    /// <summary>
    /// 执行状态(每帧执行)
    /// </summary>
    public override void OnUpdate()
    {
        CurrAnimatorStateInfo = CurrAinmator.GetCurrentAnimatorStateInfo(0);

        //给每个动画一个设置一个状态CurState,因为在动画控制器里使用的是AnyState进行动画的切换,所以需要判断当前动画是否就是要切换的动画,如果不是才切换,是的话就不切换了,否则AnyState会不停的自己切换到自己
        if (CurrAnimatorStateInfo.IsName(RoleAnimatorName.Hurt.ToString()))
        {
            CurrAinmator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleStateType.Hurt);

            //如果完成了受伤就切换回待机状态
            if (CurrAnimatorStateInfo.normalizedTime > 1)
            {
                RoleFSMMgr.RoleCtrl.ToIdle();
            }
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
        CurrAinmator.SetBool(ToAnimatorCondition.ToHurt.ToString(), false);
    }
}
