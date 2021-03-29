using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Manager/GameSettings")]

public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion; } }

    [SerializeField]
    private string _nickName = "askyr";
    public string NickName 
    { 
        get 
        {
            int val = Random.Range(0, 9999);
            return _nickName + val.ToString(); 
        } 
    }
}
