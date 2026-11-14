using System.Collections;
using UnityEngine;

public class PawnAnimator : MonoBehaviour
{
    private PawnStatus pawnStatus;
    private bool isAttackingLeft;
    private bool isAttackingRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAttackingLeft = false;
        isAttackingRight = false;
    }

    public void AttackLeftAnim()
    {
        if (!isAttackingLeft)
        {
            StartCoroutine(PlayAttackAnim(false));
        }
    }

    public void AttackRightAnim()
    {
        if (!isAttackingRight)
        {
            StartCoroutine(PlayAttackAnim(true));
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

    private IEnumerator PlayAttackAnim(bool isRight)
    {
        // play animation
        if (isRight)
        {
            // do right attack animation
            yield return new WaitForSeconds(0.5f);
            isAttackingRight = false;
        }
        else
        {
            // do left attack animation
            yield return new WaitForSeconds(0.5f);
            isAttackingLeft = false;
        }
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
