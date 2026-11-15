using System.Collections;
using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
    private protected PawnStatus pawnStatus;
    private protected bool isAttackingLeft;
    private protected bool isAttackingRight;
    protected bool canStartBlockLeft;
    protected bool canStartBlockRight;
    [SerializeField] protected Animator pawnAnimatorLeft;
    [SerializeField] protected Animator pawnAnimatorRight;
    private protected float frameTimePos;
    private protected float frameTimeMult;
    private bool shouldAnimate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        pawnStatus = GetComponent<PawnStatus>();
        isAttackingLeft = false;
        isAttackingRight = false;
        pawnAnimatorLeft.SetFloat("PunchValue", 0);
        pawnAnimatorRight.SetFloat("PunchValue", 0);
        pawnAnimatorLeft.SetFloat("BlockValue", 0);
        pawnAnimatorRight.SetFloat("BlockValue", 0);
        canStartBlockLeft = true;
        canStartBlockRight = true;
        frameTimeMult = 1f;
        shouldAnimate = true;
        StartCoroutine(AdvanceFrameTime());
    }

    private IEnumerator AdvanceFrameTime()
    {
        while (true)
        {
            if (shouldAnimate)
            {
                if (frameTimePos > 1)
                {
                    frameTimePos = 0;
                }
                frameTimePos += 0.05f * frameTimeMult;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                frameTimePos = 0;
                yield return new WaitForSeconds(0.05f);
            }
            
        }
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
            StartCoroutine(PlayLeftBlockAnim());
        }
    }

    public virtual void BlockRightAnim()
    {
        if (canStartBlockRight)
        {
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
        // DIFFERENT GLOVE OBJECTS EACH WITH ITS OWN INSTANCE OF THE ANIMATOR
        pawnAnimatorLeft.SetTrigger("TrPunch");
        yield return new WaitForSeconds(0.25f);
        isAttackingLeft = false;
    }

    private IEnumerator PlayRightAttackAnim()
    {
        // play animation
        isAttackingRight = true;
        // DIFFERENT GLOVE OBJECTS EACH WITH ITS OWN INSTANCE OF THE ANIMATOR
        pawnAnimatorRight.SetTrigger("TrPunch");
        yield return new WaitForSeconds(0.25f);
        isAttackingRight = false;
    }

    protected virtual IEnumerator PlayLeftBlockAnim()
    {
        canStartBlockLeft = false;
        pawnAnimatorLeft.SetBool("BoolBlocking", true);
        yield return new WaitForSeconds(0.25f);
        pawnAnimatorLeft.SetBool("BoolBlocking", false);
        canStartBlockLeft = true;
    }

    protected virtual IEnumerator PlayRightBlockAnim()
    {
        canStartBlockRight = false;
        pawnAnimatorRight.SetBool("BoolBlocking", true);
        yield return new WaitForSeconds(0.25f);
        pawnAnimatorRight.SetBool("BoolBlocking", false);
        canStartBlockRight = true;
    }

    public float GetFrameTimePosition()
    {
        return frameTimePos;
    }

    public void SetFrameTimeMult(float mult)
    {
        frameTimeMult = mult;
    }

    public void SetShouldAnimate(bool shouldAnimate)
    {
        this.shouldAnimate = shouldAnimate;
    }
}
