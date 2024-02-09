using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToPlatform : MonoBehaviour
{

    public Transform teleportTarget;
    public GameObject playerToMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Demonstration of teleportation to object
    void Update()
    {
        if (false)
        {
            playerToMove.transform.position = teleportTarget.transform.position;
        }
    }
}
