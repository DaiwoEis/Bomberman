using System;
using UnityEngine;

[Serializable]
public class Border
{
    [SerializeField]
    private Vector2 _min;

    [SerializeField]
    private Vector2 _max;

    public Vector2 center { get { return 0.5f*(min + max); }}

    public Vector2 min { get { return _min; } set { _min = value; } }
    public Vector2 max { get { return _max; } set { _max = value; } }

    public bool leftEdgeIsOpen { get; set; }
    public bool upEdgeIsOpen { get; set; }
    public bool rightEdgeIsOpen { get; set; }
    public bool downEdgeIsOpen { get; set; }

    public Border()
    {
        
    }

    public Border(Vector2 min, Vector2 max)
    {
        _min = min;
        _max = max;
    }

    public bool WithIn(Vector2 point)
    {
        return point.x >= _min.x && point.x <= _max.x && point.y >= _min.y && point.y <= _max.y;
    }

    public Vector2 Clamp(Vector2 point)
    {
        if (!leftEdgeIsOpen && point.x < _min.x)
        {
            point.x = _min.x;
        }

        if (!rightEdgeIsOpen && point.x > _max.x)
        {
            point.x = _max.x;
        }

        if (!downEdgeIsOpen && point.y < _min.y)
        {
            point.y = _min.y;
        }

        if (!upEdgeIsOpen && point.y > _max.y)
        {
            point.y = _max.y;
        }

        return point;
    }

    public override string ToString()
    {
        return "min: " + _min + " " + "max: " + _max;
    }
}