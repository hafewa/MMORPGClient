using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleHeadBarCtr : MonoBehaviour
{
    /// <summary>
    /// 头顶昵称
    /// </summary>
    [SerializeField]
    private UILabel m_NickName;

    /// <summary>
    /// 主角身上的头顶UI挂点
    /// </summary>
    private Transform m_TargetPos;

    /// <summary>
    /// 飘血字
    /// </summary>
    [SerializeField]
    private HUDText m_HudText;

    /// <summary>
    /// 血条
    /// </summary>
    [SerializeField]
    private GameObject pbHp;

    /// <summary>
    /// 初始化头顶UI
    /// </summary>
    /// <param name="targetPos">位置</param>
    /// <param name="nickName">昵称</param>
    public void Init(Transform targetPos,string nickName,bool isShowHpBar)
    {
        m_TargetPos = targetPos;
        m_NickName.text = nickName;

        NGUITools.SetActive(pbHp, isShowHpBar);
    }

    private void Update()
    {
        if (Camera.main == null || UICamera.mainCamera == null || m_TargetPos == null) return;

        //首先将头顶UI条的位置从世界坐标转换到视口坐标
        Vector3 viewPos = Camera.main.WorldToScreenPoint(m_TargetPos.position);
        //然后再将转换到视口坐标的UI条转换到世界坐标
        Vector3 worldPos = UICamera.mainCamera.ScreenToWorldPoint(viewPos);

        gameObject.transform.position = worldPos;
    }

    /// <summary>
    /// 飘血显示
    /// </summary>
    /// <param name="value"></param>
    public void SetHpAndHUD(int value,float hpRate)
    {
        m_HudText.Add(string.Format("-{0}", value.ToString()), Color.red, 0.2f);
        pbHp.GetComponent<UISlider>().value = hpRate;
    }
}
