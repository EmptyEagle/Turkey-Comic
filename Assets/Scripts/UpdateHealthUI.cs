using UnityEngine;

public class UpdateHealthUI : MonoBehaviour
{
    private SpriteRenderer spriteRend;
    [SerializeField] Sprite[] healthStates;
    [SerializeField] PawnStatus pawnStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = healthStates[0];
    }

    void Update()
    {
        if (pawnStatus.GetHealth() >= 0 && pawnStatus.GetHealth() < healthStates.Length)
        {
            spriteRend.sprite = healthStates[pawnStatus.GetHealth()];
        }
    }
}
