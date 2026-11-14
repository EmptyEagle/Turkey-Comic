using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PawnStatus : MonoBehaviour
{
    private int health;
    public bool isPlayer;
    public enum AttackTarget
    {
        Left,
        Right
    };
    private bool isBlockingLeft;
    private bool isBlockingRight;

    public Text blockingLeftText;
    public Text blockingRightText;
    
    private PawnAnimator pawnAnimator;

    public Text healthText;

    void Start()
    {
        pawnAnimator = GetComponent<PawnAnimator>();
        if (isPlayer)
        {
            health = 10;
        }
        else
        {
            health = 4;
        }
    }

    void Update()
    {
        healthText.text = health.ToString();
    }

    public void TryDamage(int damage, AttackTarget attackingSide)
    {
        if ((attackingSide == AttackTarget.Left && !isBlockingLeft) || (attackingSide == AttackTarget.Right && !isBlockingRight))
        {
            // play hit animation
            pawnAnimator.TakeDamageAnim();

            // attack hits the target
            health -= damage;
            Debug.Log("Dealt damage");
            if (health <= 0)
            {
                if (isPlayer)
                {
                    // player is defeated
                    Debug.Log("GAME OVER");
                    // play player defeated animation
                    // game over screen
                }
                else
                {
                    // enemy is defeated
                    Debug.Log("Enemy defeated");
                    // play enemy defeated animation
                    // spawn next enemy
                }
            }
        }
        Debug.Log("Attack blocked");
    }

    public void SetBlockLeft(bool isBlocking)
    {
        isBlockingLeft = isBlocking;
        blockingLeftText.text = isBlockingLeft.ToString();
        Debug.Log(blockingLeftText.text);
    }

    public void SetBlockRight(bool isBlocking)
    {
        isBlockingRight = isBlocking;
        blockingRightText.text = isBlockingRight.ToString();
    }

    public bool GetBlockLeft()
    {
        return isBlockingLeft;
    }

    public bool GetBlockRight()
    {
        return isBlockingRight;
    }
}
