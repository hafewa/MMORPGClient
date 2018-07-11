using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景UI管理类
/// </summary>
public class SceneUIMgr : Singleton<SceneUIMgr>
{
    /// <summary>
    /// 当前场景的场景UI
    /// </summary>
    public UISceneBase currentUIScene;

    /// <summary>
    /// 场景UI加载
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject LoadSceneUI(SceneUIType type)
    {
        GameObject go = null;
        switch(type)
        {
            case SceneUIType.LogOn:
                go = ResourcesMgr.Instance.Load(ResourcesType.UIScene, "UIRoot_LogOnScene");
                currentUIScene = go.GetComponent<SceneLogOnCtrl>();
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.MainCity:
                go = ResourcesMgr.Instance.Load(ResourcesType.UIScene, "UIRoot_MainCity");
                currentUIScene = go.GetComponent<SceneMainCityCtrl>();
                break;
        }

        return go;
    }
}
