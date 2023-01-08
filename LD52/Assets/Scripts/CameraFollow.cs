using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Player player;
    private Vector3 PlayerPos
    {
        get
        {
            return new Vector3(
                player.transform.position.x,
                player.transform.position.y,
                transform.position.z
           );
        }
    }

    private Quaternion PlayerRotation
    {
        get
        {
            return player.transform.rotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void LateUpdate()
    {
        transform.position = PlayerPos;
        transform.rotation = PlayerRotation;
    }
}
