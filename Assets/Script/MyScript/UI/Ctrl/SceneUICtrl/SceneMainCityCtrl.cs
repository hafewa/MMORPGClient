using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMainCityCtrl : UISceneBase
{
    protected override void OnClickEvent(GameObject go)
    {
        switch(go.name)
        {
            case "BtnHead":
                OpenRoleInfo();
                break;
        }
    }

    void OpenRoleInfo()
    {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.RoleInfo);
    }
}
