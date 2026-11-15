using System.Collections;
using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
    private protected PawnStatus pawnStatus;
    private protected bool isAttackingLeft;
    private protected bool isAttackingRight;
    private protected bool canStartBlockLeft;
    private protected bool canStartBlockRight;
    [SerializeField] protected Animator pawnAnimatorLeft;
    [SerializeField] protected Animator pawnAnimatorRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pawnStatus = GetComponent<PawnStatus>();
        isAttackingLeft = false;
        isAttackingRight = false;
        pawnAnimatorLeft.SetFloat("PunchValue", 0);
        pawnAnimatorRight.SetFloat("PunchValue", 0);
        pawnAnimatorLeft.SetFloat("BlockValue", 0);
        pawnAnimatorLeft.SetBool("BoolBlocking", false);
        pawnAnimatorRight.SetFloat("BlockValue", 0);
        pawnAnimatorRight.SetBool("BoolBlocking", false);
        canStartBlockLeft = true;
        canStartBlockRight = true;
    }

    public virtual void AttackLeftAnim()
    {
        if (!isAttackingLeft)
        {
            StartCoroutine(PlayLeftAttackAnim());
        }
    }

    public virtual void AttackRightAnim()
    {
        if (!isAttackingRight)
        {
            StartCoroutine(PlayRightAttackAnim());
        }
    }

    public virtual void BlockLeftAnim()
    {
        if (canStartBlockLeft)
        {
            Debug.Log("Block left");
            StartCoroutine(PlayLeftBlockAnim());
        }
    }

    public virtual void BlockRightAnim()
    {
        if (canStartBlockRight)
        {
            Debug.Log("Block right");
            StartCoroutine(PlayRightBlockAnim());
        }
    }

    public virtual void TakeDamageAnim()
    {
        // play take damage animation
    }

    private IEnumerator PlayLeftAttackAnim()
    {
        // play animation
        isAttackingLeft = true;
        float punchTime = 0;
        // DIFFERENT GLOVE OBJECTS EACH WITH ITS OWN INSTANCE OF THE ANIMATOR
        pawnAnimatorLeft.SetTrigger("TrPunch");
        while (true)
        {
            pawnAnimatorLeft.SetFloat("PunchValue", punchTime);
            punchTime += 0.25f;
            if (punchTime > 1)
            {
                break;
            }
            yield return new WaitForSeconds(0.06f);
        }
        pawnAnimatorLeft.SetFloat("PunchValue", 1);
        yield return new WaitForSeconds(0.1f);
        pawnAnimatorLeft.SetFloat("PunchValue", 0);
        isAttackingLeft = false;
    }

    private IEnumerator PlayRightAttackAnim()
    {
        // play animation
        isAttackingRight = true;
        float punchTime = 0;
        // DIFFERENT GLOVE OBJECTS EACH WITH ITS OWN INSTANCE OF THE ANIMATOR
        pawnAnimatorRight.SetTrigger("TrPunch");
        while (true)
        {
            pawnAnimatorRight.SetFloat("PunchValue", punchTime);
            punchTime += 0.25f;
            if (punchTime > 1)
            {
                break;
            }
            yield return new WaitForSeconds(0.06f);
        }
        pawnAnimatorRight.SetFloat("PunchValue", 1);
        yield return new WaitForSeconds(0.1f);
        pawnAnimatorRight.SetFloat("PunchValue", 0);
        isAttackingRight = false;
    }

    private IEnumerator PlayLeftBlockAnim()
    {
        canStartBlockLeft = false;
        float blockTime = 0;
        pawnAnimatorLeft.SetBool("BoolBlocking", true);
        while (pawnStatus.GetBlockLeft())
        {
            pawnAnimatorLeft.SetFloat("BlockValue", blockTime);
            blockTime += 0.5f;
            if (blockTime > 0.5f)
            {
                blockTime = 0.5f;
            }
            yield return new WaitForSeconds(0.06f);
        }
        pawnAnimatorLeft.SetFloat("BlockValue", 0.5f);
        yield return new WaitForSeconds(0.1f);
        pawnAnimatorLeft.SetFloat("BlockValue", 0);
        pawnAnimatorLeft.SetBool("BoolBlocking", false);
        canStartBlockLeft = true;
    }

    private IEnumerator PlayRightBlockAnim()
    {
        canStartBlockRight = false;
        float blockTime = 0;
        pawnAnimatorRight.SetBool("BoolBlocking", true);
        while (pawnStatus.GetBlockRight())
        {
            pawnAnimatorRight.SetFloat("BlockValue", blockTime);
            blockTime += 0.5f;
            if (blockTime > 0.5f)
            {
                blockTime = 0.5f;
            }
            yield return new WaitForSeconds(0.06f);
            Debug.Log("Blocking anim");
        }
        pawnAnimatorRight.SetFloat("BlockValue", 0.5f);
        yield return new WaitForSeconds(0.1f);
        pawnAnimatorRight.SetFloat("BlockValue", 0);
        pawnAnimatorRight.SetBool("BoolBlocking", false);
        canStartBlockRight = true;
    }
}
