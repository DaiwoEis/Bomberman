using UnityEngine;

public class Role : MonoBehaviour
{
    [SerializeField]
    private int _speedLevel = 1;

    public int speedLevel { get { return _speedLevel; } set { _speedLevel = value; } }

    [SerializeField]
    private int _bombNumberLevel = 1;

    public int bombNumberLevel { get { return _bombNumberLevel; } set { _bombNumberLevel = value; } }

    private int _bombPowerLevel = 1;

    public int bombPowerLevel { get { return _bombPowerLevel; } set { _bombPowerLevel = value; } }

    private void Start () 
	{
	
	}
	
	private void Update () 
	{
	
	}
}
