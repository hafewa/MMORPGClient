using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;

/// <summary>
/// 数据转换工具类(byte short int float decimal bool string)
/// </summary>
public class MMO_MemoryStream : MemoryStream
{
    #region 构造函数
    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MMO_MemoryStream() { }

    /// <summary>
    /// 有参构造函数
    /// </summary>
    /// <param name="buffer"></param>
    public MMO_MemoryStream(byte[] buffer) : base(buffer) { }
    #endregion

    #region Short
    /// <summary>
    /// 从流中读取一个short数据
    /// </summary>
    /// <returns></returns>
    public short ReadShort()
    {
        byte[] arr = new byte[2];

        base.Read(arr, 0, 2);

        return BitConverter.ToInt16(arr, 0);
    }

    /// <summary>
    /// 写入一个Short数据到流中
    /// </summary>
    public void WriteShort(short value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region UShort
    /// <summary>
    /// 从流中读取一个UShort数据
    /// </summary>
    /// <returns></returns>
    public ushort ReadUShort()
    {
        byte[] arr = new byte[2];

        base.Read(arr, 0, 2);

        return BitConverter.ToUInt16(arr, 0);
    }

    /// <summary>
    /// 写入一个UShort数据到流中
    /// </summary>
    public void WriteUShort(ushort value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Int
    /// <summary>
    /// 从流中读取一个int数据
    /// </summary>
    /// <returns></returns>
    public int ReadInt()
    {
        byte[] arr = new byte[4];

        base.Read(arr, 0, 4);

        return BitConverter.ToInt32(arr, 0);
    }

    /// <summary>
    /// 写入一个int数据到流中
    /// </summary>
    public void WriteInt(int value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region UInt
    /// <summary>
    /// 从流中读取一个uint数据
    /// </summary>
    /// <returns></returns>
    public uint ReadUInt()
    {
        byte[] arr = new byte[4];

        base.Read(arr, 0, 4);

        return BitConverter.ToUInt32(arr, 0);
    }

    /// <summary>
    /// 写入一个uint数据到流中
    /// </summary>
    public void WriteUInt(uint value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region long
    /// <summary>
    /// 从流中读取一个long数据
    /// </summary>
    /// <returns></returns>
    public long ReadLong()
    {
        byte[] arr = new byte[8];

        base.Read(arr, 0, 8);

        return BitConverter.ToInt64(arr, 0);
    }

    /// <summary>
    /// 写入一个long数据到流中
    /// </summary>
    public void WriteLong(long value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region Ulong
    /// <summary>
    /// 从流中读取一个ulong数据
    /// </summary>
    /// <returns></returns>
    public ulong ReadULong()
    {
        byte[] arr = new byte[8];

        base.Read(arr, 0, 8);

        return BitConverter.ToUInt64(arr, 0);
    }

    /// <summary>
    /// 写入一个ulong数据到流中
    /// </summary>
    public void WriteULong(ulong value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region float
    /// <summary>
    ///  从流中读取一个float数据
    /// </summary>
    /// <returns></returns>
    public float ReadFloat()
    {
        byte[] arr = new byte[4];
        base.Read(arr, 0, 4);
        return BitConverter.ToSingle(arr, 0);
    }

    /// <summary>
    /// 写入一个float数据到流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteFloat(float value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region double
    /// <summary>
    ///  从流中读取一个double数据
    /// </summary>
    /// <returns></returns>
    public double ReadDouble()
    {
        byte[] arr = new byte[8];
        base.Read(arr, 0, 8);
        return BitConverter.ToDouble(arr, 0);
    }

    /// <summary>
    /// 写入一个double数据到流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteDouble(double value)
    {
        byte[] arr = BitConverter.GetBytes(value);

        base.Write(arr, 0, arr.Length);
    }
    #endregion

    #region bool
    /// <summary>
    ///  从流中读取一个bool数据
    /// </summary>
    /// <returns></returns>
    public bool ReadBool()
    {
        return base.ReadByte() == 1;
    }

    /// <summary>
    /// 写入一个bool数据到流中
    /// </summary>
    /// <param name="value"></param>
    public void WriteBool(bool value)
    {
        base.WriteByte((byte)(value == true ? 1 : 0));
    }
    #endregion

    #region UTF8String
    /// <summary>
    /// 从流中读取一个字符串
    /// </summary>
    /// <returns></returns>
    public string ReadUTF8String()
    {
        //字符串的长度(不是2个字节,只是用ushort类型来存储字符串的长度)
        ushort len = ReadUShort();

        byte[] arr = new byte[len];

        base.Read(arr, 0, len);

        //得到字符串
        return Encoding.UTF8.GetString(arr);
    }

    /// <summary>
    /// 写入一个字符串数据到流中
    /// </summary>
    public void WriteUTF8String(string value)
    {
        byte[] arr = Encoding.UTF8.GetBytes(value);

        if(arr.Length > 65535)
        {
            throw new InvalidCastException("超出字符串范围");
        }

        //首先将字符串的长度写入
        WriteUShort((ushort)arr.Length);
        //再把字符串本身写入
        base.Write(arr, 0, arr.Length);
    }
    #endregion
}
