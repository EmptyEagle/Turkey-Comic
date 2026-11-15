using System.Collections;
using UnityEngine;

public class EnemyAnimator : PawnAnimator
{
    [SerializeField] Animator pawnAnimatorBody;

    void Update()
    {
        pawnAnimatorBody.SetFloat("BodyPosition", frameTimePos);
        pawnAnimatorLeft.SetFloat("PunchValue", frameTimePos);
        pawnAnimatorLeft.SetFloat("BlockValue", frameTimePos);
        pawnAnimatorRight.SetFloat("PunchValue", frameTimePos);
        pawnAnimatorRight.SetFloat("BlockValue", frameTimePos);
    }

    public override void AttackLeftAnim()
    {
        if (!isAttackingLeft)
        {
            StartCoroutine(PlayLeftAttackAnim());
        }
    }

    public override void AttackRightAnim()
    {
        if (!isAttackingRight)
        {
            StartCoroutine(PlayRightAttackAnim());
        }
    }

    public override void BlockLeftAnim()
    {
        if (canStartBlockLeft)
        {
            StartCoroutine(PlayLeftBlockAnim());
        }
    }

    public override void BlockRightAnim()
    {
        if (canStartBlockRight)
        {
            StartCoroutine(PlayRightBlockAnim());
        }
    }

    public override void TakeDamageAnim()
    {
        // play take damage animation
    }

    private IEnumerator PlayLeftAttackAnim()
    {
        // play animation
        isAttackingLeft = true;
        frameTimePos = 0;
        pawnAnimatorLeft.SetTrigger("TrPunch");
        yield return new WaitForSeconds(0.25f);
        isAttackingLeft = false;
    }

    private IEnumerator PlayRightAttackAnim()
    {
        // play animation
        isAttackingRight = true;
        frameTimePos = 0;
        pawnAnimatorRight.SetTrigger("TrPunch");
        yield return new WaitForSeconds(0.25f);
        isAttackingRight = false;
    }

    private IEnumerator PlayLeftBlockAnim()
    {
        canStartBlockLeft = false;
        frameTimePos = 0;
        pawnAnimatorLeft.SetBool("BoolBlocking", true);
        yield return new WaitForSeconds(0.25f);
        pawnAnimatorLeft.SetBool("BoolBlocking", false);
        canStartBlockLeft = true;
    }

    private IEnumerator PlayRightBlockAnim()
    {
        canStartBlockRight = false;
        frameTimePos = 0;
        pawnAnimatorRight.SetBool("BoolBlocking", true);
        yield return new WaitForSeconds(0.25f);
        pawnAnimatorRight.SetBool("BoolBlocking", false);
        canStartBlockRight = true;
    }
}
