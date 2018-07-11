using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制器
/// </summary>
public class RoleCtrl : MonoBehaviour
{
    #region 成员变量或者属性
    /// <summary>
    /// 目标位置
    /// </summary>
    [HideInInspector]
    public Vector3 TargetPos = Vector3.zero;

    /// <summary>
    /// 移动速度
    /// </summary>
    public float Speed = 10.0f;

    /// <summary>
    /// 角色控制器
    /// </summary>
    [HideInInspector]
    public CharacterController CharacterCtrl;

    /// <summary>
    /// 动画控制器
    /// </summary>
    public Animator AnimaCtrl;

    /// <summary>
    /// 当前角色类型
    /// </summary>
    [HideInInspector]
    public RoleType CurrRoleType = RoleType.None;

    /// <summary>
    /// 当前角色信息
    /// </summary>
    public RoleInfoBase CurrRoleInfo;

    /// <summary>
    /// 当前角色AI
    /// </summary>
    public IRoleAI RoleAI;

    /// <summary>
    /// 角色有限状态机管理类
    /// </summary>
    public RoleFSMMgr RoleFSMMgr;

    /// <summary>
    /// 头顶信息位置
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;

    /// <summary>
    /// 头顶UI条
    /// </summary>
    private GameObject m_HeadBar;

    /// <summary>
    /// 头顶UI控制器
    /// </summary>
    private RoleHeadBarCtr m_HeadBarCtrl;

    /// <summary>
    /// 敌人巡逻范围
    /// </summary>
    public float PatrolRange;

    /// <summary>
    /// 敌人视野范围
    /// </summary>
    public float ViewRange;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange;

    /// <summary>
    /// 出生点
    /// </summary>
    [HideInInspector]
    public Transform BornPos;

    /// <summary>
    /// 锁定敌人
    /// </summary>
    [HideInInspector]
    public RoleCtrl LockEnemy;

    /// <summary>
    /// 伤害监听事件
    /// </summary>
    public Action OnHurt;

    /// <summary>
    /// 死亡事件
    /// </summary>
    public System.Action<RoleCtrl> OnDie;
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="roleinfo">角色信息</param>
    /// <param name="roleAi">角色AI</param>
    public void Init(RoleType roleType, RoleInfoBase roleinfo, IRoleAI roleAi)
    {
        CurrRoleType = roleType;
        CurrRoleInfo = roleinfo;
        RoleAI = roleAi;
    }

    /// <summary>
    /// 是否在摄像机可见范围内的判断
    /// </summary>
    /// <param name="worldPos"></param>
    /// <returns></returns>
    public bool IsInView(Vector3 worldPos)
    {
        Transform camTransform = Camera.main.transform;
        Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector3 dir = (worldPos - camTransform.position).normalized;
        float dot = Vector3.Dot(camTransform.forward, dir);     //判断物体是否在相机前面

        if (dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

    void Start ()
    {
        CharacterCtrl = GetComponent<CharacterController>();

        //只有主角才需要摄像机跟随
        if (CurrRoleType == RoleType.MainPlayer)
        {
            if (CameraCtrl.Instance != null)
            {
                CameraCtrl.Instance.Init();
            }
        }

        //初始化角色有限状态机管理类
        RoleFSMMgr = new RoleFSMMgr(this);
        //主角初始状态为待机
        ToIdle();
        //初始化头顶UI条
        HeadBarInit();
    }

    #region 角色控制
    /// <summary>
    /// 切换到待机
    /// </summary>
    public void ToIdle()
    {
        RoleFSMMgr.ChangeState(RoleStateType.Idle);
    }
    /// <summary>
    /// 移动到目标位置
    /// </summary>
    public void MoveTo(Vector3 target)
    {
        if (target == Vector3.zero) return;

        TargetPos = target;
        RoleFSMMgr.ChangeState(RoleStateType.Run);
    }
    /// <summary>
    /// 切换到攻击
    /// </summary>
    public void ToAttack()
    {
        //如果没有锁定目标就不能攻击
        if (LockEnemy == null) return;
        RoleFSMMgr.ChangeState(RoleStateType.Attack);

        //锁定敌人受伤
        LockEnemy.ToHurt(100, 0.5f);
    }
    /// <summary>
    /// 切换到死亡
    /// </summary>
    public void ToDie()
    {
        RoleFSMMgr.ChangeState(RoleStateType.Die);
    }


    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="attackValue">攻击力</param>
    /// <param name="delayTime">伤害显示延迟时间</param>
    public void ToHurt(int attackValue, float delayTime)
    {
        StartCoroutine(ToHurtDelay(attackValue, delayTime));
    }

    /// <summary>
    /// 伤害协程
    /// </summary>
    /// <param name="attackValue">攻击力</param>
    /// <param name="delayTime">延迟多长时间后才进行受伤,大招时会有特效表现完成后才会有伤害显示</param>
    /// <returns></returns>
    private IEnumerator ToHurtDelay(int attackValue,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        //这里仅仅是测试(伤害计算公式)
        int hurt = (int)(attackValue * UnityEngine.Random.Range(0.7f, 1f));

        CurrRoleInfo.CurrHp -= hurt;

        m_HeadBarCtrl.SetHpAndHUD(hurt,(float)CurrRoleInfo.CurrHp / CurrRoleInfo.MaxHp);

        //监听伤害事件
        if (OnHurt != null)
            OnHurt();

        if (CurrRoleInfo.CurrHp <= 0)
        {
            ToDie();
        }
        else
        {
            RoleFSMMgr.ChangeState(RoleStateType.Hurt);
        }
    }
    #endregion

    /// <summary>
    /// 初始化头顶信息
    /// </summary>
    void HeadBarInit()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (m_HeadBarPos == null) return;
        if (CurrRoleInfo == null) return;

        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesType.UIOther, "RoleHeadBar");

        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;
        m_HeadBar.transform.localPosition = Vector3.zero;
        m_HeadBar.transform.localScale = Vector3.one;

        m_HeadBarCtrl = m_HeadBar.GetComponent<RoleHeadBarCtr>();
        m_HeadBarCtrl.Init(m_HeadBarPos, CurrRoleInfo.NickName, CurrRoleType == RoleType.Monster);
    }

    void Update()
    {
        //如果没有AI就不执行任何行为
        if (RoleAI == null) return;

        //执行AI行为
        RoleAI.DoAI();

        Vector2 vec2 = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);

        //判断角色是否再主相机内,如果不在就将其头顶UI隐藏,反之打开
        if (IsInView(transform.position))
        {
            if (m_HeadBar != null)
            {
                if (!m_HeadBar.activeSelf)
                {
                    m_HeadBar.SetActive(true);
                }
            }
        }
        else
        {
            if (m_HeadBar != null)
            {
                if(m_HeadBar.activeSelf)
                {
                    m_HeadBar.SetActive(false);
                }
            }
        }

        //每帧执行有限状态机的执行状态
        if (RoleFSMMgr != null)
            RoleFSMMgr.OnUpdate();

        if (CharacterCtrl == null) return;

        #region 主角移动
        //如果角色没有着地,就迅速让其着地
        if (!CharacterCtrl.isGrounded)
        {
            CharacterCtrl.Move(transform.position + new Vector3(0, -1000, 0) - transform.position);
        }
        #endregion

        //摄像机只跟随主角
        if (CurrRoleType == RoleType.MainPlayer)
        {
            #region 摄像机的跟随和旋转
            CameraAutoFllow();
            #endregion
        }
    }

    #region 摄像机的跟随和旋转
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    public void CameraAutoFllow()
    {
        if (CameraCtrl.Instance == null)
            return;

        CameraCtrl.Instance.transform.position = transform.position;

        CameraCtrl.Instance.AutoLooAt(transform.position);
    }
    #endregion

    #region 销毁
    private void OnDestroy()
    {
        Destroy(m_HeadBar);
        m_HeadBar = null;
    }
    #endregion
}
