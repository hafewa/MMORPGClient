using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 字符串转换工具类
/// </summary>
public static class StringUtil
{
    /// <summary>
    /// 字符串转int
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str)
    {
        int temp = 0;

        int.TryParse(str, out temp);

        return temp;
    }

    /// <summary>
    /// 字符串转float
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float temp = 0;

        float.TryParse(str, out temp);

        return temp;
    }
}
