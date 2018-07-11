using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ResourcesMgr : Singleton<ResourcesMgr>
{
    /// <summary>
    /// 缓存资源表,主场景中的UI很多,可以使用这个缓存来优化加载性能
    /// </summary>
    private Hashtable resCaches = new Hashtable();

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">资源子路径</param>
    /// <param name="cache">是否加入缓存</param>
    /// <returns></returns>
    public GameObject Load(ResourcesType type,string prefabName,bool cache = false)
    {
        GameObject go = null;

        if (resCaches.ContainsKey(prefabName))
        {
            go = resCaches[prefabName] as GameObject;
            Debug.Log("从缓存加载资源");
        }
        else
        {
            StringBuilder sb = new StringBuilder();

            switch (type)
            {
                case ResourcesType.UIScene:
                    sb.Append("UIPrefab/UIScene/");
                    break;
                case ResourcesType.UIWindow:
                    sb.Append("UIPrefab/UIWindows/");
                    break;
                case ResourcesType.Role:
                    sb.Append("RolePrefab/");
                    break;
                case ResourcesType.Effect:
                    sb.Append("EffectPrefab/");
                    break;
                case ResourcesType.UIOther:
                    sb.Append("UIPrefab/UIOther/");
                    break;
            }

            sb.Append(prefabName);

            go = Resources.Load(sb.ToString()) as GameObject;

            if(cache)
            {
                resCaches.Add(prefabName, go);
            }
        }

        return GameObject.Instantiate(go);
    }

    /// <summary>
    /// 释放缓存表,缓存中的资源会占用内存,在切换战斗场景时是不需要这些UI缓存资源的,可以在切换战斗场景的时候调用一下这个Dispose方法
    /// </summary>
    public override void Dispose()
    {
        resCaches.Clear();

        Resources.UnloadUnusedAssets();
    }
}
