using System;
using UnityEngine;

[Serializable]
public abstract class LevelMaper<T> where T : struct
{
    [SerializeField]
    protected TextAsset _levelData = null;

    protected T[] _datas = null;

    protected int _count = 0;

    public abstract void Init();

    public T this[int level]
    {
        get
        {
            if (level <= 0 || level > _count)
            {
                throw new IndexOutOfRangeException(string.Format("level:{0} is not a valid level", level));
            }
            return _datas[level];
        }
    }

    protected string[] GetDatas()
    {
        string[] datas = _levelData.text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        if (datas.Length == 0)
        {
            throw new InvalidOperationException(string.Format("Please make the {0} file has one line data at least", _levelData));
        }
        return datas;
    }

    public int Count()
    {
        return _count;
    }
}

[Serializable]
public class FloatLevelMaper : LevelMaper<float>
{
    public override void Init()
    {
        string[] datas = GetDatas();
        _datas = new float[datas.Length + 1];
        for (int i = 0; i < datas.Length; ++i)
        {
            _datas[i + 1] = float.Parse(datas[i]);
        }
        _count = datas.Length;
    }
}

[Serializable]
public class IntLevelMaper : LevelMaper<int>
{
    public override void Init()
    {
        string[] datas = GetDatas();
        _datas = new int[datas.Length + 1];
        for (int i = 0; i < datas.Length; ++i)
        {
            _datas[i + 1] = int.Parse(datas[i]);
        }
        _count = datas.Length;
    }
}