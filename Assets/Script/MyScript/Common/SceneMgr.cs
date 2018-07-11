using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 场景类型
    /// </summary>
    public SceneType curSceneType
    {
        get;
        private set;
    }

    /// <summary>
    /// 切换登陆场景
    /// </summary>
    public void LoadLogOnScene()
    {
        curSceneType = SceneType.LogOn;
        SceneManager.LoadScene("Loading");
    }

    /// <summary>
    /// 切换主城场景
    /// </summary>
    public void LoadCityScene()
    {
        curSceneType = SceneType.City;
        SceneManager.LoadScene("Loading");
    }
}
