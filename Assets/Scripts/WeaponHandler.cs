using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class WeaponHandler : NetworkBehaviour
{
    List<Weapon> weaponList;
    public Weapon currentWeapon;
    int index;


    Transform cameraT;
    RaycastHit hit;
    RaycastHit hit2;
    int lm;
    Weapon wp;
    Weapon wp1;


    public GameObject rifle;
    public GameObject bullet;
    public GameObject rifleSound;


    public Vector3 unEquipedRifflePos;
    public Vector3 unEquipedRiffleRot;
    public Quaternion unEquipedRiffleRotQT;
    public Vector3 equipedRifflePos;
    public Vector3 equipedRiffleRot;
    public Vector3 aimingRifflePos;
    public Vector3 aimingRiffleRot;
    public Quaternion equipedRiffleRotQT;
    public Quaternion aimingRiffleRotQT;

    public BoxCollider currCollider;


    Animator ani;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        weaponList = new List<Weapon>();
        unEquipedRifflePos.Set(0.07f, 3.13f, -2.74f);
        unEquipedRiffleRot.Set(61.738f, -77.589f, -184.447f);
        equipedRifflePos.Set(2.59f, -1.22f, 0.51f);
        equipedRiffleRot.Set(-41.378f, -94.612f, -46.782f);
        aimingRifflePos.Set(2.57f, -0.22f, 0.18f);
        aimingRiffleRot.Set(-3.463f, -85.429f, -93.381f);
        equipedRiffleRotQT.eulerAngles = equipedRiffleRot;
        aimingRiffleRotQT.eulerAngles = aimingRiffleRot;
        index = -1;

        cameraT = transform.GetChild(1).GetChild(0).transform;
        lm = 1 << LayerMask.NameToLayer("CrossHair");

        lm = ~lm;
    }

    
    void switchWeapon()
    {
        Debug.Log("Capacity" + weaponList.Capacity);
        if (weaponList.Capacity >= 0)
        {
            if (currentWeapon)
            {
                currentWeapon.transform.SetParent(transform.GetChild(0).GetChild(0).GetChild(2));
                currentWeapon.transform.localPosition = unEquipedRifflePos;
                currentWeapon.transform.localRotation = unEquipedRiffleRotQT;
            }
            currentWeapon = weaponList[++index % weaponList.Capacity];
            currentWeapon.transform.SetParent(transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0));
            currentWeapon.transform.localPosition = equipedRifflePos;
            currentWeapon.transform.localRotation = equipedRiffleRotQT;
            Debug.Log((int)currentWeapon.type);
            ani.SetInteger("WeaponType", (int)currentWeapon.type);


        }
    }

   
    void Aiming()
    {
        Debug.Log("isAiming now!!");
        if (currentWeapon)
        {
            ani.SetBool("Aiming", true);
            currentWeapon.transform.localPosition = aimingRifflePos;
            currentWeapon.transform.localRotation = aimingRiffleRotQT;

        }
    }

    
    void NotAiming()
    {
        if (currentWeapon)
        {
            ani.SetBool("Aiming", false);
            currentWeapon.transform.localPosition = equipedRifflePos;
            currentWeapon.transform.localRotation = equipedRiffleRotQT;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("SwitchWeapon"))
        {
            switchWeapon();
        }

        if (Input.GetButton("Aiming"))
        {
            Aiming();
        }
        else
        {
            NotAiming();
        }
        if (Input.GetButtonDown("Fire"))
        {
            ani.SetBool("isFireing", true);
            CmdFire(currentWeapon.netId);

        }
        else
        {
            ani.SetBool("isFireing", false);
        }

        if (Physics.SphereCast(cameraT.position, 0.1f, cameraT.forward, out hit, 100, lm))
        {

        }
        if (Input.GetButtonDown("F") && Vector3.Distance(hit.transform.position, transform.position) < 15.0f && hit.transform.tag == "Weapon")
        {
            Debug.Log("Get Weapon");
            wp = hit.transform.gameObject.GetComponent<Weapon>();
            Debug.Log("wp.netID : "+ wp.netId);
            CmdaddWeapon(wp.netId);
            
            Invoke("addWeapon", 0.5f);
        }

    }
    
    [Command]
    public void CmdaddWeapon(NetworkInstanceId weaponNetId)
    {
        Debug.Log("In Cmd");
        Debug.Log(weaponNetId);
        GameObject WeaponToAdd = NetworkServer.FindLocalObject(weaponNetId);
        Transform TrOfWeaponToAdd = WeaponToAdd.transform;
        if (WeaponToAdd)
        {
            Debug.Log("a");
            
            GameObject go = (GameObject)Instantiate(rifle, WeaponToAdd.transform.position, WeaponToAdd.transform.rotation);
            if (go)
            {
                Debug.Log("b");
                Destroy(WeaponToAdd);
                Debug.Log("c");
                if(!NetworkServer.SpawnWithClientAuthority(go, transform.gameObject))
                {
                    Debug.Log("Can't Spawn Object");
                }
                Debug.Log("d");
            }
            else
            {
                Debug.Log("Can't make instance from local object");
            }
                
        }
        else
        {
            Debug.Log("Can't find local Object");
        }
        
        
        
    }
    public void addWeapon()
    {
        Physics.SphereCast(cameraT.position, 0.1f, cameraT.forward, out hit2, 100, lm);
        wp1 = hit2.transform.gameObject.GetComponent<Weapon>();
        weaponList.Add(wp1);
        wp1.transform.SetParent(transform.GetChild(0).GetChild(0).GetChild(2));
        wp1.transform.localPosition = unEquipedRifflePos;
        wp1.transform.localRotation = unEquipedRiffleRotQT;
    }

    [Command]
    public void CmdFire(NetworkInstanceId bulletNetId)
    {
        GameObject bulletSpawnPosition = NetworkServer.FindLocalObject(bulletNetId);
        GameObject rifleSoundObject = (GameObject)Instantiate(rifleSound, bulletSpawnPosition.transform.GetChild(4).position, bulletSpawnPosition.transform.GetChild(4).rotation);
        NetworkServer.Spawn(rifleSoundObject);
        Destroy(rifleSoundObject,rifleSoundObject.GetComponent<AudioSource>().clip.length);
        GameObject go = (GameObject)Instantiate(bullet, bulletSpawnPosition.transform.GetChild(4).position, bulletSpawnPosition.transform.GetChild(4).rotation);       
        NetworkServer.Spawn(go);
        
        Destroy(go, 3.0f);
    }
}
