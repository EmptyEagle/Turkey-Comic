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
    [SerializeField] GameObject hitVisual;

    [SerializeField] GameObject playerFistLeft;
    [SerializeField] GameObject playerFistRight;

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
        playerFistLeft.transform.position = new Vector3();
        playerFistRight.transform.position = new Vector3();
    }

    void Update()
    {
        healthText.text = health.ToString();
        blockingLeftText.text = isBlockingLeft.ToString();
        blockingRightText.text = isBlockingRight.ToString();
    }

    public bool TryDamage(int damage, AttackTarget attackingSide)
    {
        if (isPawnAlive && ((attackingSide == AttackTarget.Left && !isBlockingLeft) || (attackingSide == AttackTarget.Right && !isBlockingRight)))
        {
            // play hit animation
            if (!isPlayer)
            {
                StopCoroutine(PlayHitVisual());
                hitVisual.SetActive(false);
                StartCoroutine(PlayHitVisual());
            }
            //pawnAnimator.TakeDamageAnim();
            if (isPlayer)
            {
                StopCoroutine(PlayPlayerTakeDamage());
                playerFistLeft.transform.position = new Vector3();
                playerFistRight.transform.position = new Vector3();
                StartCoroutine(PlayPlayerTakeDamage());
            }
            

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

    IEnumerator PlayHitVisual()
    {
        hitVisual.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        hitVisual.SetActive(false);
    }

    IEnumerator PlayPlayerTakeDamage()
    {
        yield return new WaitForSeconds(0.1f);
        playerFistLeft.transform.Translate(playerFistLeft.transform.position.x, -0.3f, playerFistLeft.transform.position.z);
        playerFistRight.transform.Translate(playerFistRight.transform.position.x, -0.3f, playerFistRight.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        playerFistLeft.transform.Translate(playerFistLeft.transform.position.x, -0.1f, playerFistLeft.transform.position.z);
        playerFistRight.transform.Translate(playerFistRight.transform.position.x, -0.1f, playerFistRight.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        playerFistLeft.transform.Translate(playerFistLeft.transform.position.x, 0.4f, playerFistLeft.transform.position.z);
        playerFistRight.transform.Translate(playerFistRight.transform.position.x, 0.4f, playerFistRight.transform.position.z);
    }
}
