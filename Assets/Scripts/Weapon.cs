using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour
{
    public Transform tr;
    public int maxAmmo;
    public int power;
    public int currentAmmo;
    public int distance;
    public int rebound;
    public int bullSpeed;
    public float yunsa;
    bool charged;

    public enum Type
    {
        Rifle=1, Pistol=2
    }
    public Type type;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer) this.enabled = false;
        tr = GetComponent<Transform>();
        charged = false;

    }
    

}
