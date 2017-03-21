using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{
    [SerializeField]
    private Text _bombPowerText = null;

    [SerializeField]
    private Text _bombNumberText = null;

    [SerializeField]
    private Text _speedText = null;

    private Character _character = null;

    private void Awake()
    {
        _character = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    private void Start() { }

    private void Update()
    {
        _bombPowerText.text = _character.bombPowerLevel.ToString();
        _bombNumberText.text = _character.bombNumberLevel.ToString();
        _speedText.text = _character.speedLevel.ToString();
    }
}
