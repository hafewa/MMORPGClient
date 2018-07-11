using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 登陆窗口的控制器
/// </summary>
public class WindowLogOnCtrl : UIWindowBase
{
    /// <summary>
    /// 登陆账号
    /// </summary>
    [SerializeField]
    private UIInput InputNickName;

    /// <summary>
    /// 登陆密码
    /// </summary>
    [SerializeField]
    private UIInput InputPassWord;

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
            case "LogOn":
                OnLogOn();
                break;
            case "BackRegister":
                OnReigester();
                break;
        }
    }

    /// <summary>
    /// 登陆
    /// </summary>
    private void OnLogOn()
    {
        string nickname = InputNickName.value.Trim();

        string pwd = InputPassWord.value.Trim();

        if(string.IsNullOrEmpty(nickname))
        {
            notice.text = "请输入正确的昵称";
            return;
        }

        if (string.IsNullOrEmpty(pwd))
        {
            notice.text = "请输入正确密码";
            return;
        }

        if(PlayerPrefs.GetString(GlobalInit.MMO_NICKNAME) != nickname || PlayerPrefs.GetString(GlobalInit.MMO_PWD) != pwd)
        {
            notice.text = "账号密码不匹配";
            return;
        }

        GlobalInit.Instance.MainRoleNickName = nickname;

        SceneMgr.Instance.LoadCityScene();
    }

    /// <summary>
    /// 打开注册窗口
    /// </summary>
    void OnReigester()
    {
        #region 关闭当前窗口并打开新的窗口
        //下一个要打开的窗口时注册窗口
        NextOpenWindow = WindowUIType.Register;
        //关闭当前窗口,关闭窗口的时候会执行基类中BeforeDestroy方法,在这个方法里会打开新的窗口
        Close();
        #endregion

        //不关闭当前窗口并打开新的窗口
        //WindowUIMgr.Instance.OpenWindow(WindowUIType.Register);
    }
}
