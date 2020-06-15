using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    public static ItemAsset Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    public Sprite swordSprite;
    public Sprite helmetSprite;
    public Sprite potionSprite;
    public Sprite armorSprite;


}
