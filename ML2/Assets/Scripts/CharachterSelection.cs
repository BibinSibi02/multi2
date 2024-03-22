using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class CharachterSelection : NetworkBehaviour
{
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private List<GameObject> paddles;  //list of paddle characters /  prefabs
    [SerializeField] private GameObject cPanel;

    public override void OnStartClient() //runs on the start of a client
    {
        base.OnStartClient();
        if (!base.IsOwner)
        {
            canvasObject.SetActive(false);

        }


   
    }

    public void spawnLeft() //spawns left paddle 
    {
        canvasObject.SetActive(false);
        Spawn(0, LocalConnection);

    }

    public void spawnRight()//spawns right paddle
    {
        canvasObject.SetActive(false);

        Spawn(1, LocalConnection);
    }


    [ServerRpc(RequireOwnership = false)]
    void Spawn(int spawnIndex, NetworkConnection conn)
    {
        GameObject player = Instantiate(paddles[spawnIndex]); //Vector3.zero, Quaternion.identity); //SPAWNS DIFFERENT TYPES OF CHARACTERS

        Spawn(player, conn);    
    }

}
