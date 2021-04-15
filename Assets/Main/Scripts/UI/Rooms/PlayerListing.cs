using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;

        int result = -1;

        // receiving the custom property through the photon hash table for each client if it exists
        // as updating custom properties won't let photon know it automatically, we have to do it manually every time 
        if (player.CustomProperties.ContainsKey("RandomNumber"))
        {
            result = (int)player.CustomProperties["RandomNumber"];
        }
        
        _text.text = player.NickName + ", " + result.ToString();
    }
}
