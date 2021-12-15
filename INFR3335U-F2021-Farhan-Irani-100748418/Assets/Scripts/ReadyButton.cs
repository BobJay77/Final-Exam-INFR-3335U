using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ReadyButton : MonoBehaviourPunCallbacks
{
    public GameObject currentPlayer;

    public GameObject unready, ready;

    public bool readyFlag = false;

    public void SetCurrentPlayer(GameObject playerListContent) //*
    {
        Text[] tempPlayerInfoList = playerListContent.GetComponentsInChildren<Text>();
        foreach (Text temp in tempPlayerInfoList)
        {
            Debug.Log("temp text: " + temp.text);
            Debug.Log("Photon Nickname: " + PhotonNetwork.NickName);
            if (temp.text == PhotonNetwork.NickName)
                currentPlayer = temp.gameObject.transform.parent.gameObject;
        }

        unready = currentPlayer.transform.GetChild(1).gameObject;
        ready = currentPlayer.transform.GetChild(2).gameObject;
        
        if (readyFlag)
        {
            unready.SetActive(false);
            ready.SetActive(true);
        }
        else
        {
            unready.SetActive(true);
            ready.SetActive(false);
        }

    }

    public void OnClickReady()
    {
        readyFlag = !readyFlag;

        if (readyFlag)
        {
            unready.SetActive(false);
            ready.SetActive(true);
        }
        else
        {
            unready.SetActive(true);
            ready.SetActive(false);
        }
    }
}