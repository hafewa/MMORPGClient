using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowReigsterCtrl : UIWindowBase
{
    /// <summary>
    /// 注册账号
    /// </summary>
    [SerializeField]
    private UIInput InputNickName;

    /// <summary>
    /// 注册密码
    /// </summary>
    [SerializeField]
    private UIInput InputPassWord;

    /// <summary>
    /// 确认密码
    /// </summary>
    [SerializeField]
    private UIInput InputRePassWord;

    /// <summary>
    /// 错误提示
    /// </summary>
    [SerializeField]
    private UILabel notice;

    /// <summary>
    /// 所有按钮的点击事件
    /// </summary>
    /// <param name="go"></param>
    protected override void OnClickEvent(GameObject go)
    {
        Debug.Log("click:" + go.name);

        switch (go.name)
        {
            case "Register":
                OnRegister();
                break;
            case "BackLogin":
                OnLogin();
                break;
        }
    }

    private void OnRegister()
    {
        string nickname = InputNickName.value.Trim();

        string pwd = InputPassWord.value.Trim();

        string repwd = InputRePassWord.value.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            notice.text = "请输入正确的昵称";
            return;
        }

        if (string.IsNullOrEmpty(pwd))
        {
            notice.text = "请输入正确密码";
            return;
        }

        if(!repwd.Equals(pwd))
        {
            notice.text = "两次密码不一致";
            return;
        }

        PlayerPrefs.SetString(GlobalInit.MMO_NICKNAME, nickname);

        PlayerPrefs.SetString(GlobalInit.MMO_PWD, pwd);

        GlobalInit.Instance.MainRoleNickName = nickname;

        SceneMgr.Instance.LoadCityScene();
    }

    private void OnLogin()
    {
        #region 关闭当前窗口并打开新的窗口
        //下一个要打开的窗口时登陆窗口
        NextOpenWindow = WindowUIType.LogOn;
        //关闭当前窗口,关闭窗口的时候会执行基类中BeforeDestroy方法,在这个方法里会打开新的窗口
        Close();
        #endregion

        //不关闭当前窗口并打开新的窗口
        //WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
