using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/SpriteData")]
public class SpriteDataScriptableObject : ScriptableObject
{
    [SerializeField] private List<Sprite> sprites = new();

    public Sprite GetSprite(string sprite)
    {
        return sprites.Find(x => x.name == sprite);
    }
}
