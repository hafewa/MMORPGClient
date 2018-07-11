using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击状态
/// </summary>
public class RoleStateAttack : RoleStateAbstract
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="roleFsmMgr"></param>
    public RoleStateAttack(RoleFSMMgr roleFsmMgr) : base(roleFsmMgr)
    {

    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void OnEnter()
    {
        //切换攻击动画
        CurrAinmator.SetInteger(ToAnimatorCondition.ToPhyAttack.ToString(), 1);
    }

    /// <summary>
    /// 执行状态(每帧执行)
    /// </summary>
    public override void OnUpdate()
    {
        CurrAnimatorStateInfo = CurrAinmator.GetCurrentAnimatorStateInfo(0);

        //给每个动画一个设置一个状态CurState,因为在动画控制器里使用的是AnyState进行动画的切换,所以需要判断当前动画是否就是要切换的动画,如果不是才切换,是的话就不切换了,否则AnyState会不停的自己切换到自己
        if (CurrAnimatorStateInfo.IsName(RoleAnimatorName.PhyAttack1.ToString()))
        {
            CurrAinmator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleStateType.Attack);

            //如果完成了攻击就切换回待机状态
            if(CurrAnimatorStateInfo.normalizedTime > 1)
            {
                RoleFSMMgr.RoleCtrl.ToIdle();
            }
        }
    }

    /// <summary>
    /// 离开状态
    /// </summary>
    public override void OnLeave()
    {
        CurrAinmator.SetInteger(ToAnimatorCondition.ToPhyAttack.ToString(), 0);
    }
}
