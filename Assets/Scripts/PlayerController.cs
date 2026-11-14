using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction attackLeft;
    private InputAction attackRight;
    private InputAction blockLeft;
    private InputAction blockRight;

    private PawnStatus playerStatus;
    [SerializeField] PawnStatus enemyStatus;

    private bool canAttackLeft;
    private bool canAttackRight;
    private bool canBlockLeft;
    private bool canBlockRight;
    
    private PawnAnimator playerAnimator;
    
    [SerializeField] EnemyController enemyController;

    void Start()
    {
        playerStatus = GetComponent<PawnStatus>();
        playerAnimator = GetComponent<PawnAnimator>();
        canAttackLeft = true;
        canAttackRight = true;
        canBlockLeft = true;
        canBlockRight = true;
    }

    void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    void OnEnable()
    {
        attackLeft = playerInput.Gameplay.AttackLeft;

        attackRight = playerInput.Gameplay.AttackRight;

        blockLeft = playerInput.Gameplay.BlockLeft;

        blockRight = playerInput.Gameplay.BlockRight;
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // if player is not blocking they can attack
        if (attackLeft.WasPressedThisFrame() && playerStatus.GetBlockLeft() != true)
        {
            AttackLeft();
        }
        if (attackRight.WasPressedThisFrame() && playerStatus.GetBlockRight() != true)
        {
            AttackRight();
        }
        // if player has the ability to attack that means they have the ability to block
        if (blockLeft.WasPressedThisFrame() && canAttackLeft)
        {
            BlockStartLeft();
        }
        else if (blockLeft.WasReleasedThisFrame())
        {
            playerStatus.SetBlockLeft(false);
        }
        if (blockRight.WasPressedThisFrame() && canAttackRight)
        {
            BlockStartRight();
        }
        else if (blockRight.WasReleasedThisFrame())
        {
            playerStatus.SetBlockRight(false);
        }
    }

    void AttackLeft()
    {
        if (canAttackLeft)
        {
            StartCoroutine(DoAttackCooldownLeft());
            playerAnimator.AttackLeftAnim();
            enemyController.AttemptBlockLeft();
            enemyStatus.TryDamage(1, PawnStatus.AttackTarget.Left);
        }
    }

    void AttackRight()
    {
        if (canAttackRight)
        {
            StartCoroutine(DoAttackCooldownRight());
            playerAnimator.AttackRightAnim();
            enemyController.AttemptBlockRight();
            enemyStatus.TryDamage(1, PawnStatus.AttackTarget.Right);
        }
    }

    void BlockStartLeft()
    {
        if (canBlockLeft)
        {
            StartCoroutine(DoBlockCooldownLeft());
            playerAnimator.BlockLeftAnim();
            playerStatus.SetBlockLeft(true);
        }
    }

    void BlockStartRight()
    {
        if (canBlockRight)
        {
            StartCoroutine(DoBlockCooldownRight());
            playerAnimator.BlockRightAnim();
            playerStatus.SetBlockRight(true);
        }
    }

    IEnumerator DoAttackCooldownLeft()
    {
        canAttackLeft = false;
        yield return new WaitForSeconds(0.24f);
        canAttackLeft = true;
    }

    IEnumerator DoAttackCooldownRight()
    {
        canAttackRight = false;
        yield return new WaitForSeconds(0.24f);
        canAttackRight = true;
    }

    IEnumerator DoBlockCooldownLeft()
    {
        canBlockLeft = false;
        yield return new WaitForSeconds(0.24f);
        canBlockLeft = true;
    }

    IEnumerator DoBlockCooldownRight()
    {
        canBlockRight = false;
        yield return new WaitForSeconds(0.24f);
        canBlockRight = true;
    }
}
