
//===================================================
//作    者: 王栋 
//创建时间：2018-07-16 15:56:13
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// RoleJob数据管理
/// </summary>
public partial class RoleJobDBModel : AbstractDBModel<RoleJobDBModel, RoleJobEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "RoleJob.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override RoleJobEntity MakeEntity(GameDataTableParser parse)
    {
        RoleJobEntity entity = new RoleJobEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.JobPic = parse.GetFieldValue("JobPic");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.Dec = parse.GetFieldValue("Dec");
        return entity;
    }
}
