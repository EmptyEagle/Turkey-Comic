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

    void Start()
    {
        enemyStatus = GetComponent<PawnStatus>();
        canAttackLeft = true;
        canAttackRight = true;
        canBlockLeft = true;
        canBlockRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TryMove()
    {
        
    }

    private void DoAttackLeft()
    {

    }

    private void DoAttackRight()
    {

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
            StartCoroutine(BlockLeft());
        }
        else if (rand > 25 && canBlockLeft && canBlockRight)
        {
            // block incorrect side
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
            StartCoroutine(BlockRight());
        }
        else if (rand > 25 && canBlockRight && canBlockLeft)
        {
            // block incorrect side
            StartCoroutine(BlockLeft());
        }
    }

    IEnumerator BlockLeft()
    {
        canBlockLeft = false;
        float startTime = Time.time;
        float elapsedTime = startTime;
        while (elapsedTime < startTime + 0.5f)
        {
            enemyStatus.BlockLeft();
            elapsedTime += 0.001f;
        }
        yield return new WaitForSeconds(0.01f);
        canBlockLeft = true;
    }

    IEnumerator BlockRight()
    {
        canBlockRight = false;
        float startTime = Time.time;
        float elapsedTime = startTime;
        while (elapsedTime < startTime + 0.5f)
        {
            enemyStatus.BlockRight();
            elapsedTime += 0.001f;
        }
        yield return new WaitForSeconds(0.01f);
        canBlockRight = true;
    }
}
