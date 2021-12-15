using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX, maxX, minZ, maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 1.0f, Random.Range(minZ, maxZ));

        GameObject temp = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);

        if (temp.GetComponent<PhotonView>().IsMine)
            temp.GetComponent<Move>().SetJoysticks(temp.GetComponentInChildren<Camera>().gameObject); 
    }
}
