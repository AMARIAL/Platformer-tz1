using System;
using UnityEngine;
using UnityEngine.UI;

public enum ToggleType
{
    Music,
    Sound,
}

public class ToggleController : MonoBehaviour
{
    [SerializeField] private ToggleType _toggleType;
    private Toggle _toggle;
    
    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }
    private void Start()
    {
        switch (_toggleType)
        {
            case ToggleType.Music:
                _toggle.SetIsOnWithoutNotify(Audio.ST.MusicOn);
                break;
            case ToggleType.Sound:
                _toggle.SetIsOnWithoutNotify(Audio.ST.SoundOn);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
