using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    
    private PawnAnimator pawnAnimator;

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

    void LateUpdate()
    {
        isBlockingLeft = false;
        isBlockingRight = false;
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
    }

    public void BlockLeft()
    {
        isBlockingLeft = true;
    }

    public void BlockRight()
    {
        isBlockingRight = true;
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
