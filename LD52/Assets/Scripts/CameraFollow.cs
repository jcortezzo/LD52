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
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void LateUpdate()
    {
        SmoothFollow();
        transform.rotation = PlayerRotation;
    }

    void SmoothFollow()
    {
        Vector3 targetPos = player.transform.position + offset;
        Vector3 smoothFollow = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        smoothFollow.z = -10;
        transform.position = smoothFollow;
    }
}
