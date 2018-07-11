using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跑步状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    /// <summary>
    /// 转身的速度
    /// </summary>
    private float m_RotateSpeed = 0;

    /// <summary>
    /// 目标转身四元数
    /// </summary>
    private Quaternion m_TargetQuaterion;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="roleFsmMgr"></param>
    public RoleStateRun(RoleFSMMgr roleFsmMgr) : base(roleFsmMgr)
    {

    }

    /// <summary>
    /// 进入状态
    /// </summary>
    public override void OnEnter()
    {
        //切换跑的状态
        CurrAinmator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);

        //重置转身速度
        m_RotateSpeed = 0;
    }

    /// <summary>
    /// 执行状态(每帧执行)
    /// </summary>
    public override void OnUpdate()
    {
        CurrAnimatorStateInfo = CurrAinmator.GetCurrentAnimatorStateInfo(0);

        //给每个动画一个设置一个状态CurState,因为在动画控制器里使用的是AnyState进行动画的切换,所以需要判断当前动画是否就是要切换的动画,如果不是才切换,是的话就不切换了,否则AnyState会不停的自己切换到自己
        if (CurrAnimatorStateInfo.IsName(RoleAnimatorName.Run.ToString()))
        {
            CurrAinmator.SetInteger(ToAnimatorCondition.CurState.ToString(), (int)RoleStateType.Run);
        }
        else
        {
            CurrAinmator.SetInteger(ToAnimatorCondition.CurState.ToString(), 0);
        }

        //当前位置(忽略Y轴)
        Vector3 temPos1 = new Vector3(RoleFSMMgr.RoleCtrl.transform.position.x, 0, RoleFSMMgr.RoleCtrl.transform.position.z);

        //目标位置(忽略Y轴)
        Vector3 temPos2 = new Vector3(RoleFSMMgr.RoleCtrl.TargetPos.x, 0, RoleFSMMgr.RoleCtrl.TargetPos.z);

        if (Vector3.Distance(temPos1, temPos2) > 0.1f)
       {
           Vector3 direction = RoleFSMMgr.RoleCtrl.TargetPos - RoleFSMMgr.RoleCtrl.transform.position;
           direction = direction.normalized;
           direction = direction * Time.deltaTime * RoleFSMMgr.RoleCtrl.Speed;
           //我们只在XZ轴上移动
           direction.y = 0;
           //使得主角移动的时候朝向时朝着鼠标点击的方向,这里不能直接使用m_TargetPos,因为m_TargetPos.y是向地里面去的,所以直接使用m_TargetPos人物会不停的向下栽跟头
           //transform.LookAt(new Vector3(m_TargetPos.x,transform.position.y,m_TargetPos.z));

           //上面的操作虽然实现了转身,但是转身时瞬间的,下面时缓慢转身的实现

            if(m_RotateSpeed <= 1)
            {
                //速度递增
                m_RotateSpeed += 5.0f * Time.deltaTime;

                //目标转身四元数
                m_TargetQuaterion = Quaternion.LookRotation(direction);
                //进行插值转身操作,实现缓慢转身
                RoleFSMMgr.RoleCtrl.transform.rotation = Quaternion.Lerp(RoleFSMMgr.RoleCtrl.transform.rotation, m_TargetQuaterion, m_RotateSpeed);

                if (Quaternion.Angle(RoleFSMMgr.RoleCtrl.transform.rotation, m_TargetQuaterion) < 1)
                {
                    m_RotateSpeed = 0;
                }
            }

           RoleFSMMgr.RoleCtrl.CharacterCtrl.Move(direction);
       }
       else
       {
           //走到目的地就停下来
           RoleFSMMgr.RoleCtrl.ToIdle();
       }
    }

    /// <summary>
    /// 离开状态
    /// </summary>
    public override void OnLeave()
    {
        CurrAinmator.SetBool(ToAnimatorCondition.ToRun.ToString(), false);
    }
}
