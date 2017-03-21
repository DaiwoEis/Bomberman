using UnityEngine;

public class Border
{
    private Vector2 _min;

    private Vector2 _max;

    public Vector2 center { get; private set; }

    public Border(Vector2 min, Vector2 max)
    {
        _min = min;
        _max = max;
        center = 0.5f*(min + max);
    }

    public bool WithIn(Vector2 point)
    {
        return point.x >= _min.x && point.x <= _max.x && point.y >= _min.y && point.y <= _max.y;
    }

    public Vector2 Clamp(Vector2 point)
    {
        Vector2 clpPoint;
        clpPoint.x = Mathf.Clamp(point.x, _min.x, _max.x);
        clpPoint.y = Mathf.Clamp(point.y, _min.y, _max.y);
        return clpPoint;
    }

    public override string ToString()
    {
        return "min: " + _min + " " + "max: " + _max;
    }
}