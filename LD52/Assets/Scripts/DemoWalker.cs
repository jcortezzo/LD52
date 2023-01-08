using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWalker : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject PLAYER_PREFAB;

    [SerializeField]
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DemoWalker newWalker = Instantiate(
                PLAYER_PREFAB, 
                new Vector3(transform.position.x + 10, transform.position.y, 0), 
                Quaternion.identity
            ).GetComponent<DemoWalker>();
            newWalker.PLAYER_PREFAB = PLAYER_PREFAB;
            newWalker.canMove = false;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical).normalized;

        rb.velocity = movement * speed;
    }
}
