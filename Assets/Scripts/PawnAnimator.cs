using System.Collections;
using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
    private PawnStatus pawnStatus;
    [SerializeField] bool isEnemy;
    private bool isAttackingLeft;
    private bool isAttackingRight;
    private bool canStartBlockLeft;
    private bool canStartBlockRight;
    [SerializeField] Animator pawnAnimatorLeft;
    [SerializeField] Animator pawnAnimatorRight;
    [SerializeField] Animator pawnAnimatorBody;

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
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 1);
            StartCoroutine(AnimateBody());
        }
    }

    private IEnumerator AnimateBody()
    {
        float bodyPosition = pawnAnimatorBody.GetFloat("BodyPosition");
        while (bodyPosition <= 3)
        {
            bodyPosition += 0.1f;
            pawnAnimatorBody.SetFloat("BodyPosition", bodyPosition);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void AttackLeftAnim()
    {
        if (!isAttackingLeft)
        {
            StartCoroutine(PlayLeftAttackAnim());
        }
    }

    public void AttackRightAnim()
    {
        if (!isAttackingRight)
        {
            StartCoroutine(PlayRightAttackAnim());
        }
    }

    public void BlockLeftAnim()
    {
        if (canStartBlockLeft)
        {
            StartCoroutine(PlayLeftBlockAnim());
        }
    }

    public void BlockRightAnim()
    {
        if (canStartBlockRight)
        {
            StartCoroutine(PlayRightBlockAnim());
        }
    }

    public void TakeDamageAnim()
    {
        // play take damage animation
    }

    private IEnumerator PlayLeftAttackAnim()
    {
        // play animation
        isAttackingLeft = true;
        float punchTime = 0;
        // DIFFERENT GLOVE OBJECTS EACH WITH ITS OWN INSTANCE OF THE ANIMATOR
        // stop moving body if doing an action
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 0);
            StopCoroutine(AnimateBody());
            pawnAnimatorBody.SetFloat("BodyPosition", 0);
        }
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
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 1);
            StartCoroutine(AnimateBody());
        }
        isAttackingLeft = false;
    }

    private IEnumerator PlayRightAttackAnim()
    {
        // play animation
        isAttackingRight = true;
        float punchTime = 0;
        // DIFFERENT GLOVE OBJECTS EACH WITH ITS OWN INSTANCE OF THE ANIMATOR
        // stop moving body if doing an action
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 0);
            StopCoroutine(AnimateBody());
            pawnAnimatorBody.SetFloat("BodyPosition", 0);
        }
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
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 1);
            StartCoroutine(AnimateBody());
        }
        isAttackingRight = false;
    }

    private IEnumerator PlayLeftBlockAnim()
    {
        canStartBlockLeft = false;
        float blockTime = 0;
        // stop moving body if doing an action
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 0);
            StopCoroutine(AnimateBody());
            pawnAnimatorBody.SetFloat("BodyPosition", 0);
        }
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
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 1);
            StartCoroutine(AnimateBody());
        }
        canStartBlockLeft = true;
    }

    private IEnumerator PlayRightBlockAnim()
    {
        canStartBlockRight = false;
        float blockTime = 0;
        // stop moving body if doing an action
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 0);
            StopCoroutine(AnimateBody());
            pawnAnimatorBody.SetFloat("BodyPosition", 0);
        }
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
        if (isEnemy)
        {
            pawnAnimatorBody.SetFloat("BoolNotMoving", 1);
            StartCoroutine(AnimateBody());
        }
        canStartBlockRight = true;
    }
}
