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
    private PawnAnimator enemyAnimator;
    private bool canDoMove;
    [SerializeField] PlayerController playerController;

    void Start()
    {
        enemyStatus = GetComponent<PawnStatus>();
        enemyAnimator = GetComponent<PawnAnimator>();
        canAttackLeft = true;
        canAttackRight = true;
        canBlockLeft = true;
        canBlockRight = true;
        canDoMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDoMove && enemyStatus.GetHealth() > 0)
        {
            StartCoroutine(DoMove());
        }

    }

    IEnumerator DoMove()
    {
        canDoMove = false;
        float rand = Random.Range(0.5f, 1.2f);
        yield return new WaitForSeconds(rand);
        ExecuteMove();
        canDoMove = true;
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
        int rand = Random.Range(1, 100);
        if (rand > 70)
        {
            AttemptBlockLeft();
        }
        else if (rand > 40)
        {
            AttemptBlockRight();
        }
        else if (rand > 25)
        {
            AttemptAttackLeft();
        }
        else if (rand > 10)
        {
            AttemptAttackRight();
        }
        else
        {
            AttemptBlockLeft();
            AttemptBlockRight();
        }
    }

    private void AttemptAttackLeft()
    {
        if (canAttackLeft)
        {
            StartCoroutine(AttackLeft());
        }
    }

    private void AttemptAttackRight()
    {
        if (canAttackRight)
        {
            StartCoroutine(AttackRight());
        }
    }

    public void AttemptBlockLeft()
    {
        /*
         * when attacked:
         * 25% chance to not block
         * 50% chance to block the correct side
         * 25% chance to block the incorrect side
         */
        int rand = Random.Range(1, 100);
        if (rand > 50 && canBlockLeft)
        {
            // block correct side
            StopCoroutine(DoMove());
            StartCoroutine(BlockLeft());
        }
        else if (rand > 25 && canBlockLeft && canBlockRight)
        {
            // block incorrect side
            StopCoroutine(DoMove());
            StartCoroutine(BlockRight());
        }
    }

    public void AttemptBlockRight()
    {
        /*
         * when attacked:
         * 25% chance to not block
         * 50% chance to block the correct side
         * 25% chance to block the incorrect side
         */
        int rand = Random.Range(1, 100);
        if (rand > 50 && canBlockRight)
        {
            // block correct side
            StopCoroutine(DoMove());
            StartCoroutine(BlockRight());

        }
        else if (rand > 25 && canBlockRight && canBlockLeft)
        {
            // block incorrect side
            StopCoroutine(DoMove());
            StartCoroutine(BlockLeft());
        }
    }

    IEnumerator AttackLeft()
    {
        canAttackLeft = false;
        Debug.Log("Enemy attacking left");
        enemyAnimator.AttackLeftAnim();
        if (playerStatus.TryDamage(1, PawnStatus.AttackTarget.Left))
        {
            // when landing a successful hit, reset player subsequent hits
            playerController.ResetSubsequentHits();
        }
        yield return new WaitForSeconds(0.24f);
        canAttackLeft = true;
    }

    IEnumerator AttackRight()
    {
        canAttackRight = false;
        Debug.Log("Enemy attacking left");
        enemyAnimator.AttackRightAnim();
        if (playerStatus.TryDamage(1, PawnStatus.AttackTarget.Right))
        {
            // when landing a successful hit, reset player subsequent hits
            playerController.ResetSubsequentHits();
        }
        yield return new WaitForSeconds(0.24f);
        canAttackRight = true;
    }

    IEnumerator BlockLeft()
    {
        canBlockLeft = false;
        enemyStatus.SetBlockLeft(true);
        yield return new WaitForSeconds(0.24f);
        enemyStatus.SetBlockLeft(false);
        canBlockLeft = true;
        canDoMove = true;
    }

    IEnumerator BlockRight()
    {
        canBlockRight = false;
        enemyStatus.SetBlockRight(true);
        yield return new WaitForSeconds(0.24f);
        enemyStatus.SetBlockRight(false);
        canBlockRight = true;
        canDoMove = true;
    }
}
