using UnityEngine;

public class HitVisual : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    [SerializeField] Sprite[] hitSprites;

    void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        int rand = Random.Range(0, hitSprites.Length);
        spriteRend.sprite = hitSprites[rand];
    }
}
