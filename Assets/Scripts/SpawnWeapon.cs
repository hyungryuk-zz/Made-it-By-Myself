using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnWeapon : NetworkBehaviour
{
    public GameObject objectToSpawn;
    public int numberOfWeapon;


    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfWeapon; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, Random.Range(-20.0f, 20.0f));
            Quaternion spawnRotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 180.0f), 0);

            GameObject weapon = (GameObject)Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            NetworkServer.Spawn(weapon);
        }
    }

   

}


