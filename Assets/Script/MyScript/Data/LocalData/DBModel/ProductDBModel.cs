using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店配置表的读取管理类,每一个数据表都应该有一个这样的管理类
///  此代码由ReadExcel工具生成,请勿手动修改
/// </summary>
public class ProductDBModel: AbstractDBModel<ProductDBModel, ProductEntity>
{
    /// <summary>
    /// 子类必须要实现的具体哪个配置表名称
    /// </summary>
    protected override string FileName { get { return "Product.data"; } }

    /// <summary>
    /// 子类必须要返回给父类的实体
    /// </summary>
    protected override ProductEntity MakeEntity(GameDataTableParser parse)
    {
        ProductEntity entity = new ProductEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.PicName = parse.GetFieldValue("PicName");
        entity.Price = parse.GetFieldValue("Price").ToInt();
        entity.Dec = parse.GetFieldValue("Dec");
        return entity;
    }
}
