using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    [SerializeField]
    private Transform m_BoxCreateArea;

    private GameObject m_BoxPrefab;

    private int m_CurBoxNum = 0;

    private int m_MaxBoxNum = 10;

    private float m_CreateBoxTime = 0;

    [SerializeField]
    private Transform m_BoxParent;

    string m_BoxKey = "boxKey";

    private int allBoxNum = 0;

    private void Start()
    {
        m_BoxPrefab = Resources.Load("Item/xiangzi") as GameObject;

        allBoxNum = PlayerPrefs.GetInt(m_BoxKey);
    }

    private void Update()
    {
        if(m_CurBoxNum < m_MaxBoxNum)
        {
            if(Time.time > m_CreateBoxTime)
            {
                m_CreateBoxTime = Time.time + 0.5f;

                GameObject box = Instantiate(m_BoxPrefab, m_BoxParent);
                //在指定区域随机刷出箱子
                box.transform.position = m_BoxCreateArea.transform.TransformPoint(new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f)));
                //给箱子注册点击事件
                box.GetComponent<BoxCtrl>().onHit = OnBoxHit;
                m_CurBoxNum++;
            }
        }
    }

    /// <summary>
    /// 箱子拾取事件
    /// </summary>
    /// <param name="go"></param>
    private void OnBoxHit(GameObject go)
    {
        m_CurBoxNum--;
        allBoxNum++;
        PlayerPrefs.SetInt(m_BoxKey, allBoxNum);
        Debug.Log("累计拾取箱子的数量为:" + allBoxNum);
        Destroy(go);
    }
}
