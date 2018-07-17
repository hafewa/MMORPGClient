using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity
{
    /// <summary>
    /// 编号,硬性要求,每个配置表都必须含有Id字段,并且是int类型
    /// </summary>
    public int Id { get; set; }
}
