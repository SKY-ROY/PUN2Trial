using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomCustomPropertyGenerator : MonoBehaviour
{   
    // photon hash table to store custom properties
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    
    [SerializeField]
    private TMP_Text _text;

    public Button customPropertyButton;

    private void Awake()
    {
        customPropertyButton = GetComponent<Button>();
        customPropertyButton.onClick.AddListener(OnClick_CustomPropertyButton);
    }

    /// <summary>
    /// Here we are setting a random number as our custom property so it can be used by specific client in game.
    /// Custom properties are useful for setting values that, won't change rapidly like player icon, player color, etc.
    /// </summary>
    private void SetCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 99);

        _text.text = result.ToString();

        _myCustomProperties["RandomNumber"] = result;

        PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }

    public void OnClick_CustomPropertyButton()
    {
        SetCustomNumber();
    }   
}
