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

    public void ReactBlockLeftAnim()
    {
        StartCoroutine(PlayReactLeftBlockAnim());
    }

    public override void BlockRightAnim()
    {
        if (canStartBlockRight)
        {
            StartCoroutine(PlayRightBlockAnim());
        }
    }

    public void ReactBlockRightAnim()
    {
        StartCoroutine(PlayReactRightBlockAnim());
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
        yield return new WaitForSeconds(0.5f);
        isAttackingLeft = false;
    }

    private IEnumerator PlayRightAttackAnim()
    {
        // play animation
        isAttackingRight = true;
        frameTimePos = 0;
        pawnAnimatorRight.SetTrigger("TrPunch");
        yield return new WaitForSeconds(0.5f);
        isAttackingRight = false;
    }

    protected override IEnumerator PlayLeftBlockAnim()
    {
        canStartBlockLeft = false;
        frameTimePos = 0;
        pawnAnimatorLeft.SetTrigger("TrBlock");
        yield return new WaitForSeconds(0.5f);
        canStartBlockLeft = true;
    }

    private IEnumerator PlayReactLeftBlockAnim()
    {
        canStartBlockLeft = false;
        frameTimePos = 0.9f;
        pawnAnimatorLeft.SetTrigger("TrBlock");
        yield return new WaitForSeconds(0.5f);
        canStartBlockLeft = true;
    }

    protected override IEnumerator PlayRightBlockAnim()
    {
        canStartBlockRight = false;
        frameTimePos = 0;
        pawnAnimatorRight.SetTrigger("TrBlock");
        yield return new WaitForSeconds(0.5f);
        canStartBlockRight = true;
    }

    private IEnumerator PlayReactRightBlockAnim()
    {
        frameTimePos = 0.9f;
        pawnAnimatorRight.SetTrigger("TrBlock");
        yield return new WaitForSeconds(0.5f);
        canStartBlockRight = true;
    }
}
