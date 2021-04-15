using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_Text _text;

    public Player Player { get; private set; }

    public void SetPlayerInfo(Player player)
    {
        Player = player;
        SetPlayerText(player);
    }

    // PUN callback method for update in player custom properties
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    
        if(targetPlayer != null && targetPlayer == Player)
        {
            // changedProps is the hashtable that contains the updated properties
            if (changedProps.ContainsKey("RandomNumber"))
                SetPlayerText(targetPlayer);
        }
    }

    // changes the text for the player listing item with corresponding values
    private void SetPlayerText(Player player)
    {
        int result = -1;

        // receiving the custom property through the photon hash table for each client if it exists
        if (player.CustomProperties.ContainsKey("RandomNumber"))
        {
            result = (int)player.CustomProperties["RandomNumber"];
        }

        _text.text = player.NickName + ", " + result.ToString();
    }
}
