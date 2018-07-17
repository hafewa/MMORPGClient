using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class TestMemoryData : MonoBehaviour
{
	void Start ()
    {
        //Get请求(从Web获取数据)
        //if (!NetWorkHttp.Instance.IsBusy)
        //{
        //    NetWorkHttp.Instance.SendData(GlobalInit.WebServerUrl + "api/account?id=5", GetCallBack);
        //}

        JsonData json = new JsonData();
        json["UserName"] = "哇哈哈";
        json["Pwd"] = "wd8622133";
        json["Type"] = 0; //0注册 1登陆

        //Post请求(向Web服务器发送数据)
        if (!NetWorkHttp.Instance.IsBusy)
        {
            NetWorkHttp.Instance.SendData(GlobalInit.WebServerUrl + "api/account", PostCallBack,true, json.ToJson());
        }
    }

    private void PostCallBack(CallBackArgs obj)
    {
        if (obj.IsError)
        {
            Debug.Log(obj.Error);
        }
        else
        {
            RetValue ret = JsonMapper.ToObject<RetValue>(obj.jsonData);

            if (ret.isError)
            {
                Debug.LogError(ret.Error);
            }
            else
            {
                Debug.Log("新注册的用户编号为:" + ret.JsonData);
            }
        }
    }

    private void GetCallBack(CallBackArgs obj)
    {
        if (obj.IsError)
        {
            Debug.Log(obj.Error);
        }
        else
        {
            AccountEntity entity = JsonMapper.ToObject<AccountEntity>(obj.jsonData);

            Debug.Log(entity.UserName);
        }
    }
}
