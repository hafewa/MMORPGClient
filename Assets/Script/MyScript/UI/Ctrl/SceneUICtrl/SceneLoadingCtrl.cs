using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loading场景UI控制器(UIRoot上)
/// </summary>
public class SceneLoadingCtrl : UISceneBase
{
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private UIProgressBar m_Progress;

    /// <summary>
    /// 进度百分比
    /// </summary>
    [SerializeField]
    private UILabel m_Percent;

    [SerializeField]
    private Transform sprAnim;

    /// <summary>
    /// 设置进度条
    /// </summary>
    /// <param name="value"></param>
    public void SetProgress(float value)
    {
        m_Progress.value = value;
        m_Percent.text = string.Format("{0}%", (int)(value * 100));

        sprAnim.transform.localPosition = new Vector3(990 * value, 0, 0);
    }
}
