using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections;

public class PawnStatus : MonoBehaviour
{
    private int health;
    private bool isPawnAlive;
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
        isPawnAlive = true;
    }

    void Update()
    {
        healthText.text = health.ToString();
        blockingLeftText.text = isBlockingLeft.ToString();
        blockingRightText.text = isBlockingRight.ToString();
    }

    public bool TryDamage(int damage, AttackTarget attackingSide)
    {
        if ((attackingSide == AttackTarget.Left && !isBlockingLeft) || (attackingSide == AttackTarget.Right && !isBlockingRight))
        {
            // play hit animation
            pawnAnimator.TakeDamageAnim();

            // attack hits the target
            health -= damage;
            if (health <= 0)
            {
                if (isPlayer)
                {
                    // player is defeated
                    Debug.Log("GAME OVER");
                    // play player defeated animation
                    // game over screen
                    isPawnAlive = false;
                    Time.timeScale = 0;
                }
                else
                {
                    // enemy is defeated
                    Debug.Log("Enemy defeated");
                    // play enemy defeated animation
                    // spawn next enemy
                    isPawnAlive = false;
                    StartCoroutine(SpawnNextEnemy());
                }
            }
            if (!isPawnAlive)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    public void SetBlockLeft(bool isBlocking)
    {
        isBlockingLeft = isBlocking;
    }

    public void SetBlockRight(bool isBlocking)
    {
        isBlockingRight = isBlocking;
    }

    public bool GetBlockLeft()
    {
        return isBlockingLeft;
    }

    public bool GetBlockRight()
    {
        return isBlockingRight;
    }

    public int GetHealth()
    {
        return health;
    }

    public void RecoverHealth(int health)
    {
        this.health += health;
        if (health > 10)
        {
            health = 10;
        }
    }

    IEnumerator SpawnNextEnemy()
    {
        yield return new WaitForSeconds(5f);
        health = 4;
        isPawnAlive = true;
    }

    public bool IsPawnAlive()
    {
        return isPawnAlive;
    }
}
