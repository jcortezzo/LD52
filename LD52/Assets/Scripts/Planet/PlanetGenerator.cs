using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject GROUND_TILE_PREFAB;

    [field:SerializeField]
    public float Radius { get; private set; }
    [SerializeField]
    private int NUM_SPOKES;
    [SerializeField]
    private int NUM_LAYERS;

    private Stack<GameObject>[] stackList;
    private float DEGREES_IN_PLANET = 360f;

    [SerializeField]
    private List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        stackList = new Stack<GameObject>[NUM_SPOKES];
        for (int i = 0; i < stackList.Length; i++)
        {
            stackList[i] = new Stack<GameObject>();
        }
        for (int layer = 0; layer < NUM_LAYERS; layer++)
        {
            float currRadius = (Radius / NUM_LAYERS) * layer;
            for (int spoke = 0; spoke < NUM_SPOKES; spoke++)
            {
                float theta = (DEGREES_IN_PLANET / NUM_SPOKES) * spoke;
                theta *= Mathf.Deg2Rad;
                float x = Mathf.Cos(theta) * currRadius;
                float y = Mathf.Sin(theta) * currRadius;

                Vector2 worldPos = new Vector2(x, y);

                Vector2 direction = (worldPos - (Vector2) this.transform.position).normalized; 

                GameObject groundTile = Instantiate(GROUND_TILE_PREFAB, worldPos, Quaternion.identity, this.transform);
                groundTile.transform.up = direction;
                Sprite planetSprite = GetPlanetSprite(layer + 1);
                groundTile.GetComponent<SpriteRenderer>().sprite = planetSprite;
                stackList[spoke].Push(groundTile);
            }
        }
    }

    private Sprite GetPlanetSprite(int currLayer)
    {
        double layerRatio = ((double) currLayer / (double) NUM_LAYERS);
        Debug.Log($"{layerRatio} with layer {currLayer}");
        if (layerRatio == 1.0)
        {
            return sprites[0];
        } 
        else if (layerRatio >= 0.90) 
        {
            return sprites[1];
        } 
        else if (layerRatio >= 0.80)
        {
            return sprites[2];
        } 
        else if (layerRatio >= 0.60)
        {
            return sprites[3];
        } else if (layerRatio >= 0.3)
        {
            return sprites[4];
        } else if (layerRatio >= 0.2)
        {
            return sprites[5];
        }
        else
        {
            return sprites[6];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // y = sqrt(r^2 - x^2)
    // y = sin(theta)*r
    // x = cos(theta)*r
}
