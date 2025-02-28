using UnityEngine;

public class CreationSceneManager : MonoBehaviour
{
    [Header("Sprite zone")]
    [SerializeField] Sprite[] terrain;

    [Header("Sprite obj")]
    [SerializeField] Sprite[] objScene;

    [Header("Object scene zone")]
    [SerializeField] SpriteRenderer objSceneZone;

    // Start is called before the first frame update
    void Start()
    {
        objSceneZone.sprite = terrain[Random.Range(0, terrain.Length)];
    }
}
