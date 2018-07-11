using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInfo : MonoBehaviour
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private UILabel m_NickName;

    /// <summary>
    /// 血量
    /// </summary>
    [SerializeField]
    private UISprite m_HpBar;

    public static UIPlayerInfo Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //监听主角的伤害事件改变血统的显示
        GlobalInit.Instance.MainRoleCtrl.OnHurt += OnMainPlayerHurt;
    }

    private void OnMainPlayerHurt()
    {
        m_HpBar.fillAmount = (float)GlobalInit.Instance.MainRoleCtrl.CurrRoleInfo.CurrHp / GlobalInit.Instance.MainRoleCtrl.CurrRoleInfo.MaxHp;
    }

    public void SetUIPlayerInfo()
    {
        m_NickName.text = GlobalInit.Instance.MainRoleNickName;
    }

    private void OnDestroy()
    {
        GlobalInit.Instance.MainRoleCtrl.OnHurt -= OnMainPlayerHurt;
    }
}
