using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedUnit : MonoBehaviour
{
    public Entity MyEntity;
    public SpriteRenderer UnitSprite;
    public Sprite[] SpriteFrames;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MyEntity != null)
        {
            //animate walking animation if the entity is moving, other wise stand in an idle pose and attack if prompted
            UnitSprite.sprite = SpriteFrames[0];
        }
        
    }
}
