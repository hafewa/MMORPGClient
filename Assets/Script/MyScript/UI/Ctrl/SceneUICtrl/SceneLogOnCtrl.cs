using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登陆场景的场景UI控制器(UIRoot上)
/// </summary>
public class SceneLogOnCtrl : UISceneBase
{
    private void Start()
    {
        StartCoroutine(OpenLogOnWindow());
    }

    IEnumerator OpenLogOnWindow()
    {
        yield return new WaitForSeconds(0.2f);
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
