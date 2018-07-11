using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMgr :Singleton<RoleMgr>
{
    /// <summary>
    /// 加载角色
    /// </summary>
    public GameObject LoadRole(RoleType type, string name)
    {
        string path = string.Empty;

        switch(type)
        {
            case RoleType.Monster:
                path = "Monster";
                break;
            case RoleType.MainPlayer:
                path = "Player";
                break;
        }
        return ResourcesMgr.Instance.Load(ResourcesType.Role, string.Format("{0}/{1}", path,name));
    }

    /// <summary>
    /// 释放
    /// </summary>
    public override void Dispose()
    {

    }
}
