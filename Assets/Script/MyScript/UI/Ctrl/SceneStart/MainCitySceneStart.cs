using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCitySceneStart : MonoBehaviour
{

    [SerializeField]
    private Transform m_PlayerBronPos;

	void Awake ()
    {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIType.MainCity);

        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDragEvent += OnFingerEvent;
            FingerEvent.Instance.OnPlayerClickGround += OnPlayerClickGround;
            FingerEvent.Instance.OnZoom += OnZoom;
        }
    }

    private void Start()
    {
        GameObject mainRole = RoleMgr.Instance.LoadRole(RoleType.MainPlayer,"Role_Main");
        mainRole.transform.position = m_PlayerBronPos.position;
        GlobalInit.Instance.MainRoleCtrl = mainRole.GetComponent<RoleCtrl>();
        //主角控制器初始化
        GlobalInit.Instance.MainRoleCtrl.Init(RoleType.MainPlayer, new RoleInfoBase() { NickName = GlobalInit.Instance.MainRoleNickName , CurrHp = 10000,MaxHp = 10000}, new RoleMainPlayerCityAI(GlobalInit.Instance.MainRoleCtrl));
    }

    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="obj"></param>
    private void OnZoom(ZoomType obj)
    {
        switch (obj)
        {
            case ZoomType.In:
                CameraCtrl.Instance.SetCameraZoom(0);
                break;
            case ZoomType.Out:
                CameraCtrl.Instance.SetCameraZoom(1);
                break;
        }
    }

    /// <summary>
    /// 点击地面主角移动
    /// </summary>
    private void OnPlayerClickGround()
    {
        if (UICamera.currentCamera != null)
        {
            Ray rayUI = UICamera.currentCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayUI, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI")))
            {
                return;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        //获取摄像机的射线碰撞地面点
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.name.Equals("Ground", System.StringComparison.CurrentCultureIgnoreCase))
            {
                if(GlobalInit.Instance.MainRoleCtrl != null)
                {
                    GlobalInit.Instance.MainRoleCtrl.MoveTo(hit.point);
                }
            }
        }
    }

    /// <summary>
    /// 滑动事件
    /// </summary>
    /// <param name="type"></param>
    private void OnFingerEvent(FingerType type)
    {
        switch (type)
        {
            case FingerType.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerType.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
            case FingerType.Up:
                CameraCtrl.Instance.SetCameraUpAndDown(0);
                break;
            case FingerType.Down:
                CameraCtrl.Instance.SetCameraUpAndDown(1);
                break;
        }
    }
}
