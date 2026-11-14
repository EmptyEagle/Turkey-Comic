using System.Collections;
using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
    private PawnStatus pawnStatus;
    private bool isAttackingLeft;
    private bool isAttackingRight;
    [SerializeField] Animator pawnAnimatorLeft;
    [SerializeField] Animator pawnAnimatorRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAttackingLeft = false;
        isAttackingRight = false;
        pawnAnimatorLeft.SetFloat("PunchValue", 0);
        pawnAnimatorRight.SetFloat("PunchValue", 0);
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
        StartCoroutine(PlayBlockAnim(false));
    }

    public void BlockRightAnim()
    {
        StartCoroutine(PlayBlockAnim(true));
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

    private IEnumerator PlayBlockAnim(bool isRight)
    {
        if (isRight)
        {
            // do right block animation
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            // do left block animation
            yield return new WaitForSeconds(0.5f);
        }
    }
}
