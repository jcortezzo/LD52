using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField]
    public bool canDestroy { get; private set; }

    private string CORE_SPRITE = "planetCore";

    private Sprite groundSprite; 

    // Start is called before the first frame update
    void Start()
    {
        canDestroy = true;
        groundSprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (groundSprite.ToString().Equals(CORE_SPRITE))
        {
            canDestroy = false;
        }
    }
}
