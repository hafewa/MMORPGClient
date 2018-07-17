using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 数据表读取管理类基类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="P"></typeparam>
public abstract class AbstractDBModel<T,P>
    where T:class,new()
    where P:AbstractEntity
{

    protected abstract string FileName { get; }
    /// <summary>
    /// 数据表的数据列表
    /// </summary>
    protected List<P> m_list;

    /// <summary>
    /// id和其实体对应的字典
    /// </summary>
    protected Dictionary<int, P> m_Dic;

    public AbstractDBModel()
    {
        m_list = new List<P>();
        m_Dic = new Dictionary<int, P>();
        LoadData();
    }

    #region 单例
    /// <summary>
    /// 单例
    /// </summary>
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    #endregion

    /// <summary>
    /// 定义成抽象函数,必须让子类实现,因为我们需要在基类知道具体是哪个子类才能存放在m_List和m_Dic中
    /// </summary>
    /// <param name="parser"></param>
    /// <returns>字类的实体</returns>
    protected abstract P MakeEntity(GameDataTableParser parser);

    /// <summary>
    /// 读取配置表数据
    /// </summary>
    private void LoadData()
    {
        //这里只是测试用这个路径,真正在手机平台需要热更新后放在沙盒路径下
        string path = string.Format(Application.streamingAssetsPath + "/Data/{0}",FileName);

        using (GameDataTableParser parser = new GameDataTableParser(path))
        {
            while (!parser.Eof)
            {
                //获取子类实体
                P p = MakeEntity(parser);
                m_Dic.Add(p.Id, p);
                m_list.Add(p);
                //进行下一行解析
                parser.Next();
            }
        }
    }

    /// <summary>
    /// 获取整个数据列表
    /// </summary>
    /// <returns></returns>
    public List<P> GetList()
    {
        return m_list;
    }

    /// <summary>
    /// 根据id获取具体配置表具体哪一行的数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public P GetProduct(int id)
    {
        if (m_Dic.ContainsKey(id))
        {
            return m_Dic[id];
        }
        return null;
    }
}
