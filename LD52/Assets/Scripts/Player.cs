using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float jumpForce;

    // TODO: Set PLANET_GENERATOR based on distance from/force on player
    [SerializeField]
    private GameObject PLANET_GENERATOR;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 coreToPlayer = (this.transform.position - PLANET_GENERATOR.transform.position).normalized;
        this.transform.up = coreToPlayer;
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = this.transform.right * speed * input;
        if (Input.GetKeyDown(KeyCode.Space)) Jump(coreToPlayer);
    }

    private void Jump(Vector2 coreToPlayer)
    {
        Vector2 forceVector = coreToPlayer * jumpForce;
        rb.AddForce(forceVector, ForceMode2D.Impulse);
        Debug.Log
            ($"Jump force vector {forceVector} applied to rigid body with vector {coreToPlayer}");
    }
}
