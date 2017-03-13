using System;
using UnityEngine;

public class LevelMaper<T> where T : struct
{
    protected T[] _datas = null;

    public T GetData(int level)
    {
        if (level <= 0 || level >= _datas.Length)
        {
            throw new IndexOutOfRangeException(string.Format("level:{0} is not a valid level", level));
        }
        return _datas[level];
    }

    public int Count()
    {
        int count = _datas.Length - 1;
        if (count < 0) count = 0;
        return count;
    }
}

public class FloatLevelMaper : LevelMaper<float>
{
    public FloatLevelMaper(TextAsset dataText)
    {
        string[] datas = dataText.text.Split(new[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
        _datas = new float[datas.Length + 1];
        for (int i = 0; i < datas.Length; ++i)
        {
            _datas[i + 1] = float.Parse(datas[i]);            
        }
    }
}

public class IntLevelMaper : LevelMaper<int>
{
    public IntLevelMaper(TextAsset text)
    {
        string[] datas = text.text.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        _datas = new int[datas.Length + 1];
        for (int i = 0; i < datas.Length; ++i)
        {
            _datas[i + 1] = int.Parse(datas[i]);
        }
    }
}