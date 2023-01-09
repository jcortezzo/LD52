using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    // TODO: Set planetGenerator based on distance from/force on player
    [SerializeField]
    private GameObject planetGenerator;

    private Rigidbody2D rb;

    private Animator animator;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject laserPrefab;

    [Header("Player Settings")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float laserDuration;
    [SerializeField]
    private float laserLength;

    [Header("Animation Settings")]
    [SerializeField]
    private float animationSpeed;

    public Vector2 CoreToPlayer {
        get {
            return (this.transform.position - planetGenerator.transform.position).normalized;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Default playback speed is 1, which is too fast for the idle animation
        animator.speed = animationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space)) {
            //Jump();
            ShootLaser();
        }
    }

    private void Move() {
        this.transform.up = CoreToPlayer * this.transform.up.magnitude;
        float dir = Input.GetAxisRaw("Horizontal");

        // Keep original Y velocity
        Vector2 currentVelocityWorldSpace = rb.velocity;
        Vector2 currentVelocityLocalSpace = transform.InverseTransformDirection(currentVelocityWorldSpace);

        // Take suggested X velocity
        Vector2 suggestedMovementWorldSpace = this.transform.right * speed * dir;
        Vector2 suggestedMovementLocalSpace = transform.InverseTransformDirection(suggestedMovementWorldSpace);

        suggestedMovementLocalSpace = new Vector2(suggestedMovementLocalSpace.x, currentVelocityLocalSpace.y);
        Vector2 finalMovementWorldSpace = transform.TransformDirection(suggestedMovementLocalSpace);
        rb.velocity = finalMovementWorldSpace;
    }

    private void Jump()
    {
        rb.AddForce(CoreToPlayer * jumpForce, ForceMode2D.Impulse);
    }

    private void ShootLaser()
    {
        Laser l = Instantiate(laserPrefab, this.transform.position, this.transform.rotation).GetComponent<Laser>();
        l.Shoot(laserDuration, laserLength, -CoreToPlayer);
        Jump();
    }
}
