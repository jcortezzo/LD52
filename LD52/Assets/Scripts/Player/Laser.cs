using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]

public class Laser : MonoBehaviour
{
	private LineRenderer lr;
    private List<Transform> nodes;
    private List<Vector2> colliderPoints;
    private PolygonCollider2D pc2d;

    public Vector2 StartPosition { get { return nodes[0].position; } }
    public Vector2 EndPosition { 
        get 
        { 
            return nodes[1].position; 
        } 
        set 
        { 
            nodes[1].position = value; 
        } 
    }

    private Coroutine shootCoroutine;

    void Awake()
    {
        GameObject endObject = new GameObject();
        endObject.transform.parent = this.transform;
        this.nodes = new List<Transform>() {
            this.transform,
            endObject.transform,
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        this.lr = GetComponent<LineRenderer>();
        lr.positionCount = nodes.Count;
        pc2d = GetComponent<PolygonCollider2D>();
        pc2d.isTrigger = true;
	}

	// Update is called once per frame
	void Update()
    {
        // Draw Line
        lr.SetPositions(nodes.ConvertAll(n => n.position).ToArray());

        // Make collision
        colliderPoints = CalculateColliderPoints();
        pc2d.SetPath(0, colliderPoints.ConvertAll(p => (Vector2) transform.InverseTransformPoint(p)));
    }

    public void Shoot(float duration, float length, Vector2 direction)
    {
        shootCoroutine = StartCoroutine(ShootCoroutine(duration, length, direction.normalized));
    }

    private IEnumerator ShootCoroutine(float duration, float length, Vector2 direction)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime / duration)
        {
            EndPosition = (length / duration) * t * direction;
            yield return null;
        }
        Destroy(this.gameObject, 0.01f);  // jank
    }

    private List<Vector2> CalculateColliderPoints()
    {
        // Get All positions on the line renderer
        Vector3[] positions = GetPositions();

        // Get the Width of the Line
        float width = lr.startWidth;

        // m = (y2 - y1) / (x2 - x1)
        float m = (positions[1].y - positions[0].y) / (positions[1].x - positions[0].x);
        float deltaX = (width / 2f) * (m / Mathf.Pow(m * m + 1, 0.5f));
        float deltaY = (width / 2f) * (1 / Mathf.Pow(1 + m * m, 0.5f));

        // Calculate the Offset from each point to the collision vertex
        Vector3[] offsets = new Vector3[2];
        offsets[0] = new Vector3(-deltaX, deltaY);
        offsets[1] = new Vector3(deltaX, -deltaY);

        // Generate the Colliders Vertices
        List<Vector2> colliderPositions = new List<Vector2> {
            positions[0] + offsets[0],
            positions[1] + offsets[0],
            positions[1] + offsets[1],
            positions[0] + offsets[1]
        };
        return colliderPositions;
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }
}
