using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    private Transform m_CameraUpAndDown;

    [SerializeField]
    private Transform m_CameraZoomContainer;

    [SerializeField]
    private Transform m_CameraContainer;

    public static CameraCtrl Instance;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 初始化角度
    /// </summary>
    public void Init()
    {
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 1, 65));
    }

    /// <summary>
    /// 摄像机左右旋转
    /// </summary>
    /// <param name="type">0-左 1-右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(0, 90 * Time.deltaTime * (type == 0 ? -1 : 1), 0);
    }

    /// <summary>
    /// 摄像机上下旋转
    /// </summary>
    /// <param name="type">0-上 1-下</param>
    public void SetCameraUpAndDown(int type)
    {
        m_CameraUpAndDown.transform.Rotate(0, 0, 50 * Time.deltaTime * (type == 0 ? -1 : 1));
        //范围限制
        m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, 1, 65));
    }

    /// <summary>
    /// 始终看着主角
    /// </summary>
    public void AutoLooAt(Vector3 rolePos)
    {
        transform.LookAt(rolePos);
    }

    /// <summary>
    /// 摄像机拉近或者远离
    /// </summary>
    /// <param name="type">0-拉近 1-远离</param>
    public void SetCameraZoom(int type)
    {
        m_CameraContainer.Translate(Vector3.forward * 10 * Time.deltaTime * (type == 1? -1 : 1));
        //范围限制
        m_CameraContainer.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraContainer.localPosition.z, -5, 1));
    }
}
