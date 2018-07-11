using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登陆场景启动脚本
/// </summary>
public class LogOnSceneStart : MonoBehaviour
{
    private void Awake()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIType.LogOn);
    }
}
