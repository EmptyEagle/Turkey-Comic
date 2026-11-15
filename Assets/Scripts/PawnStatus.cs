using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Collections;

public class PawnStatus : MonoBehaviour
{
    public int maxHealth;
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
    [SerializeField] GameObject hitVisual;

    [SerializeField] GameObject playerFistLeft;
    [SerializeField] GameObject playerFistRight;

    private bool isInvincible;

    [SerializeField] EnemyController enemyController;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        pawnAnimator = GetComponent<PawnAnimator>();
        if (isPlayer)
        {
            health = maxHealth;
        }
        else
        {
            StartCoroutine(SpawnNextEnemy());
        }
        if (playerFistLeft != null)
        {
            playerFistLeft.transform.position = new Vector3();
        }
        if (playerFistRight != null)
        {
            playerFistRight.transform.position = new Vector3();
        }
    }

    void Update()
    {
        healthText.text = health.ToString();
        blockingLeftText.text = isBlockingLeft.ToString();
        blockingRightText.text = isBlockingRight.ToString();
    }

    public bool TryDamage(int damage, AttackTarget attackingSide)
    {
        if (!isInvincible && IsPawnAlive() && health > 0 && ((attackingSide == AttackTarget.Left && !isBlockingLeft) || (attackingSide == AttackTarget.Right && !isBlockingRight)))
        {
            // play hit animation
            if (!isPlayer)
            {
                StopCoroutine(PlayHitVisual());
                StopCoroutine(PlayEnemyTakeDamage());
                hitVisual.SetActive(false);
                gameObject.transform.position = new Vector3();
                StartCoroutine(PlayHitVisual());
                StartCoroutine(PlayEnemyTakeDamage());
            }
            //pawnAnimator.TakeDamageAnim();
            if (isPlayer)
            {
                StopCoroutine(PlayPlayerTakeDamage());
                playerFistLeft.transform.position = new Vector3();
                playerFistRight.transform.position = new Vector3();
                StartCoroutine(PlayPlayerTakeDamage());
            }
            StartCoroutine(DoInvincibilityTime(0.4f));
            

            // attack hits the target
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            health -= damage;
            if (!IsPawnAlive())
            {
                if (isPlayer)
                {
                    // player is defeated
                    Debug.Log("GAME OVER");
                    // play player defeated animation
                    // game over screen
                    playerController.StopAllCoroutines();
                    Time.timeScale = 0;
                }
                else
                {
                    // enemy is defeated
                    Debug.Log("Enemy defeated");
                    // play enemy defeated animation
                    // spawn next enemy
                    enemyController.StopAllCoroutines();
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
        if (this.health > 10)
        {
            this.health = 10;
        }
    }

    IEnumerator SpawnNextEnemy()
    {
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }

        yield return new WaitForSeconds(3f);
        health = maxHealth;

        // enemy cannot attack for a period after spawn
        StartCoroutine(DoInvincibilityTime(0.8f));

        // FLICKERING SPRITE ON SPAWN
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = true;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = true;
        }
        yield return new WaitForSeconds(0.3f);
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = false;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (SpriteRenderer sprite in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            sprite.enabled = true;
        }
        // also make it so the enemy is invincible during this time
        enemyController.SetCanDoMove(true);
        // play enemy spawn animation
    }

    public bool IsPawnAlive()
    {
        return health > 0;
    }

    IEnumerator PlayHitVisual()
    {
        hitVisual.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        hitVisual.SetActive(false);
    }

    IEnumerator PlayPlayerTakeDamage()
    {
        playerFistLeft.transform.Translate(playerFistLeft.transform.position.x, -0.3f, playerFistLeft.transform.position.z);
        playerFistRight.transform.Translate(playerFistRight.transform.position.x, -0.3f, playerFistRight.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        playerFistLeft.transform.Translate(playerFistLeft.transform.position.x, -0.1f, playerFistLeft.transform.position.z);
        playerFistRight.transform.Translate(playerFistRight.transform.position.x, -0.1f, playerFistRight.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        playerFistLeft.transform.Translate(playerFistLeft.transform.position.x, 0.4f, playerFistLeft.transform.position.z);
        playerFistRight.transform.Translate(playerFistRight.transform.position.x, 0.4f, playerFistRight.transform.position.z);
    }

    IEnumerator PlayEnemyTakeDamage()
    {
        gameObject.transform.Translate(gameObject.transform.position.x, -0.3f, gameObject.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.Translate(gameObject.transform.position.x, -0.1f, gameObject.transform.position.z);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.Translate(gameObject.transform.position.x, 0.4f, gameObject.transform.position.z);
    }

    IEnumerator DoInvincibilityTime(float time)
    {
        isInvincible = true;
        if (!isPlayer) Debug.Log("NOW INVINCIBLE");
        yield return new WaitForSeconds(time);
        isInvincible = false;
        if (!isPlayer) Debug.Log("INVINCIBLITY OVER");
    }
}
