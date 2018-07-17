using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回调数据
/// </summary>
public class CallBackArgs
{
    public bool IsError;

    public string jsonData;

    public string Error;
}

public class NetWorkHttp : MonoBehaviour
{
    #region 单例
    public static NetWorkHttp Instance;

    static NetWorkHttp()
    {
        GameObject go = new GameObject("NetWorkHttp");
        DontDestroyOnLoad(go);
        Instance = go.AddComponent<NetWorkHttp>();
    }
    #endregion

    #region 数据回调
    private Action<CallBackArgs> m_CallBack;

    private CallBackArgs m_CallBackArgs;
    #endregion

    /// <summary>
    /// 是否繁忙
    /// </summary>
    private bool m_IsBusy = false;

    /// <summary>
    /// 给外部使用
    /// </summary>
    public bool IsBusy
    {
        get { return m_IsBusy; }
    }

    void Start ()
    {
        m_CallBackArgs = new CallBackArgs();
    }


    /// <summary>
    /// 请求数据
    /// </summary>
    /// <param name="url">url</param>
    /// <param name="isPost">是否是Post数据</param>
    /// <param name="json">如果是Post的话就是发给Web服务器的json数据</param>
    /// <param name="callback">数据请求回调</param>
    public void SendData(string url, Action<CallBackArgs> callback, bool isPost = false, string json = "")
    {
        if (m_IsBusy) return;

        m_IsBusy = true;

        m_CallBack = callback;

        if (isPost)
        {
            PostUrl(url, json);
        }
        else
        {
            GetUrl(url);
        }
    }

    /// <summary>
    /// Post请求
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    private void PostUrl(string url,string json)
    {
        WWWForm form = new WWWForm();
        form.AddField("", json);

        StartCoroutine(Requsest(url, form));
    }

    /// <summary>
    /// 向Web服务器Post或者Get数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="form"></param>
    /// <returns></returns>
    private IEnumerator Requsest(string url,WWWForm form = null)
    {
        WWW www;
        if (form == null)
            www = new WWW(url);
        else
            www = new WWW(url, form);

        yield return www;

        m_IsBusy = false;

        if (string.IsNullOrEmpty(www.error))
        {
            if (www.text == "null")
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.IsError = true;
                    m_CallBackArgs.Error = "未请求到数据";
                    m_CallBack(m_CallBackArgs);
                }
                yield break;
            }

            if (m_CallBack != null)
            {
                m_CallBackArgs.IsError = false;
                //这里请求的Web网页数据回自动转换为json数据
                m_CallBackArgs.jsonData = www.text;
                m_CallBack(m_CallBackArgs);
            }
        }
        else
        {
            if (m_CallBack != null)
            {
                m_CallBackArgs.IsError =true;
                m_CallBackArgs.Error = www.error;
                m_CallBack(m_CallBackArgs);
            }
        }
    }

    /// <summary>
    ///Get请求
    /// </summary>
    /// <param name="url"></param>
    private void GetUrl(string url)
    {
        StartCoroutine(Requsest(url));
    }
}
