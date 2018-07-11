using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleHeadBarRoot : MonoBehaviour
{
    /// <summary>
    /// 单例
    /// </summary>
    public static RoleHeadBarRoot Instance;

    private void Awake()
    {
        Instance = this;
    }
}
