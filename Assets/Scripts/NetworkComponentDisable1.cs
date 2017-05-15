using UnityEngine;
using UnityEngine.Networking;

public class NetworkComponentDisable1 : NetworkBehaviour
{

    [SerializeField]
    Behaviour[] componentToDisable;

    Camera cam;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentToDisable.Length; i++)
            {
                componentToDisable[i].enabled = false;
            }
        }
    }


}
