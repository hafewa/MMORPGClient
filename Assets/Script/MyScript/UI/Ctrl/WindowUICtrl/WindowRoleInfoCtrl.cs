using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色信息UI控制器
/// </summary>
public class WindowRoleInfoCtrl : UIWindowBase
{
    protected override void OnClickEvent(GameObject go)
    {
        switch(go.name)
        {
            case "btnClose":
                Close();
                break;
        }
    }
}
