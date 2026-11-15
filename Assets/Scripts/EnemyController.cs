using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private PawnStatus enemyStatus;
    [SerializeField] PawnStatus playerStatus;

    private bool canAttackLeft;
    private bool canAttackRight;
    private bool canBlockLeft;
    private bool canBlockRight;
    private EnemyAnimator enemyAnimator;
    private bool canDoMove;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        enemyStatus = GetComponent<PawnStatus>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        canAttackLeft = true;
        canAttackRight = true;
        canBlockLeft = true;
        canBlockRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDoMove && enemyStatus.IsPawnAlive())
        {
            // THIS SHOULD ONLY RUN ONCE
            enemyAnimator.SetShouldAnimate(true);
            StartCoroutine(DoMove());
        }
        else if (!enemyStatus.IsPawnAlive())
        {
            enemyAnimator.SetShouldAnimate(false);
            canAttackLeft = true;
            canAttackRight = true;
            canBlockLeft = true;
            canBlockRight = true;
        }
    }

    IEnumerator DoMove()
    {
        float randSpeedMult = Random.Range(0.4f, 0.8f);
        enemyAnimator.SetFrameTimeMult(randSpeedMult);
        canDoMove = false;
        yield return new WaitUntil(() => enemyAnimator.GetFrameTimePosition() >= 0.99);
        ExecuteMove();
    }

    private void ExecuteMove()
    {
        /*
         * when given the opportunity to move:
         * 30% chance to block left
         * 30% chance to block right
         * 15% chance to attack left
         * 15% chance to attack right
         * 10% chance to block both
         */
        bool successfulMove = false;
        while (!successfulMove)
        {
            int rand = Random.Range(1, 101);
            if (rand > 70)
            {
                successfulMove = AttemptBlockLeft();
            }
            else if (rand > 40)
            {
                successfulMove = AttemptBlockRight();
            }
            else if (rand > 25)
            {
                successfulMove = AttemptAttackLeft();
            }
            else if (rand > 10)
            {
                successfulMove = AttemptAttackRight();
            }
            else
            {
                successfulMove = AttemptBlockLeft();
                successfulMove = AttemptBlockRight();
            }
        }
        
    }

    private bool AttemptAttackLeft()
    {
        if (canAttackLeft)
        {
            StartCoroutine(AttackLeft());
            return true;
        }
        return false;
    }

    private bool AttemptAttackRight()
    {
        if (canAttackRight)
        {
            StartCoroutine(AttackRight());
            return true;
        }
        return false;
    }

    private bool AttemptBlockLeft()
    {
        if (canBlockLeft)
        {
            StartCoroutine(BlockLeft(false));
            return true;
        }
        return false;
    }

    public void ReactBlockLeft()
    {
        /*
         * when attacked:
         * 50% chance to block correct side
         */
        int rand = Random.Range(1, 101);
        if (rand > 50 && canBlockLeft)
        {
            // block correct side
            StopCoroutine(DoMove());
            StartCoroutine(BlockLeft(true));
        }
        else
        {
            Debug.Log("UNABLE TO REACT BLOCK");
        }
    }

    private bool AttemptBlockRight()
    {
        if (canBlockRight)
        {
            StartCoroutine(BlockRight(false));
            return true;
        }
        return false;
    }

    public void ReactBlockRight()
    {
        /*
         * when attacked:
         * 50% chance to block the correct side
         */
        int rand = Random.Range(1, 101);
        if (rand > 50 && canBlockRight)
        {
            // block correct side
            StopCoroutine(DoMove());
            StartCoroutine(BlockRight(true));
        }
        else
        {
            Debug.Log("UNABLE TO REACT BLOCK");
        }
    }

    IEnumerator AttackLeft()
    {
        canAttackLeft = false;
        canAttackRight = false;
        canBlockLeft = false;
        enemyAnimator.AttackLeftAnim();
        // give a little delay before dealing damage to give player a chance to block
        yield return new WaitUntil(() => enemyAnimator.GetFrameTimePosition() >= 0.7);
        if (playerStatus.TryDamage(1, PawnStatus.AttackTarget.Left))
        {
            // when landing a successful hit, reset player subsequent hits
            playerController.ResetSubsequentHits();
        }
        yield return new WaitForSeconds(0.1f);
        canAttackLeft = true;
        canAttackRight = true;
        canDoMove = true;
    }

    IEnumerator AttackRight()
    {
        canAttackRight = false;
        canAttackLeft = false;
        canBlockRight = false;
        enemyAnimator.AttackRightAnim();
        // give a little delay before dealing damage to give player a chance to block
        yield return new WaitUntil(() => enemyAnimator.GetFrameTimePosition() >= 0.7);
        if (playerStatus.TryDamage(1, PawnStatus.AttackTarget.Right))
        {
            // when landing a successful hit, reset player subsequent hits
            playerController.ResetSubsequentHits();
        }
        yield return new WaitForSeconds(0.1f);
        canAttackRight = true;
        canAttackLeft = true;
        canDoMove = true;
    }

    IEnumerator BlockLeft(bool isReaction)
    {
        canBlockLeft = false;
        canAttackLeft = false;
        canAttackRight = false;
        enemyStatus.SetBlockLeft(true);
        if (isReaction)
        {
            enemyAnimator.ReactBlockLeftAnim();
        }
        else
        {
            enemyAnimator.BlockLeftAnim();
        }
        // give a little delay before being open to attack again
        yield return new WaitUntil(() => enemyAnimator.GetFrameTimePosition() >= 0.95);
        enemyStatus.SetBlockLeft(false);
        canBlockLeft = true;
        canAttackLeft = true;
        canAttackRight = true;
        canDoMove = true;
    }

    IEnumerator BlockRight(bool isReaction)
    {
        canBlockRight = false;
        canAttackRight = false;
        canAttackLeft = false;
        enemyStatus.SetBlockRight(true);
        if (isReaction)
        {
            enemyAnimator.ReactBlockRightAnim();
        }
        else
        {
            enemyAnimator.BlockRightAnim();
        }
        // give a little delay before being open to attack again
        yield return new WaitUntil(() => enemyAnimator.GetFrameTimePosition() >= 0.95);
        enemyStatus.SetBlockRight(false);
        canBlockRight = true;
        canAttackRight = true;
        canAttackLeft = true;
        canDoMove = true;
    }

    public void SetCanDoMove(bool canMove)
    {
        canDoMove = canMove;
        if (canDoMove)
        {
            canAttackLeft = true;
            canAttackRight = true;
            canBlockLeft = true;
            canBlockRight = true;
        }
    }
}
