using UnityEngine;
using UnityEngine.Networking;

public class NetworkComponentDisable : NetworkBehaviour
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

            transform.GetChild(1).GetChild(0).GetComponent<Camera>().enabled = false;
        }
    }


}
