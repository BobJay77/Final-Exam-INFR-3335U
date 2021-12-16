using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public GameObject player, cam;

    public float minX, maxX, minZ, maxZ;

    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), 1.0f, Random.Range(minZ, maxZ));

        GameObject temp = PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);

        if (temp.GetComponent<PhotonView>().IsMine)
            temp.GetComponent<Move>().SetJoysticks(Instantiate(cam, randomPosition, Quaternion.identity));
    }
}
