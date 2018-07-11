using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaingSceneStart : MonoBehaviour
{
    /// <summary>
    /// loading场景控制器
    /// </summary>
    [SerializeField]
    private SceneLoadingCtrl sceneLoadingCtrl;

    /// <summary>
    /// 切换场景句柄
    /// </summary>
    private AsyncOperation m_Async;

    /// <summary>
    /// 目标进度值
    /// </summary>
    private int toProgress;

    /// <summary>
    /// 当前进度值
    /// </summary>
    private float curProgress;

    void Start()
    {
        //切换场景的时候需要重置一下UI层级,这里还可以写一些释放资源的方法
        LayerUIMgr.Instance.Reset();

        curProgress = 0;
        toProgress = 0;

        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 具体切换到哪个场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadScene()
    {
        string sceneName = string.Empty;
        switch(SceneMgr.Instance.curSceneType)
        {
            case SceneType.City:
                sceneName = "MainCity";
                break;
            case SceneType.LogOn:
                sceneName = "LogOn";
                break;
        }

        m_Async = SceneManager.LoadSceneAsync(sceneName);
        //UI的进度条没有走到头之前不让场景进行切换
        m_Async.allowSceneActivation = false;
        yield return m_Async;
    }

    private void Update()
    {
        if(m_Async.progress < 0.9f)
        {
            toProgress = (int)(m_Async.progress * 100);
        }
        else
        {
            toProgress = 100;
        }

        if(curProgress < toProgress)
        {
            curProgress++;
        }

        sceneLoadingCtrl.SetProgress(curProgress * 0.01f);

        //UI上的进度条走到头之后再进行场景的切换
        if (curProgress == 100)
        {
            m_Async.allowSceneActivation = true;
        }
    }
}
