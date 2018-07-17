using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商品实体类(对应商品数据表中的每一行数据)
/// 每一个数据表都应该有这么一个实体类
/// 此代码由ReadExcel工具生成,请勿手动修改
/// </summary>
public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 图片名称
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Dec { get; set; }
}
