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
    
    private PawnAnimator playerAnimator;
    
    [SerializeField] EnemyController enemyController;

    void Start()
    {
        playerStatus = GetComponent<PawnStatus>();
        playerAnimator = GetComponent<PawnAnimator>();
        Debug.Log(playerStatus.GetBlockLeft());
        Debug.Log(playerStatus.GetBlockRight());
        canAttackLeft = true;
        canAttackRight = true;
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
        if (attackLeft.WasPressedThisFrame() && playerStatus.GetBlockLeft() != true)
        {
            Debug.Log("Attack left");
            AttackLeft();
        }
        if (attackRight.WasPressedThisFrame() && playerStatus.GetBlockRight() != true)
        {
            Debug.Log("Attack right");
            AttackRight();
        }
        if (blockLeft.IsPressed())
        {
            playerStatus.BlockLeft();
            Debug.Log(playerStatus.GetBlockLeft());
        }
        if (blockRight.IsPressed())
        {
            playerStatus.BlockRight();
            Debug.Log(playerStatus.GetBlockRight());
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

    IEnumerator DoAttackCooldownLeft()
    {
        canAttackLeft = false;
        yield return new WaitForSeconds(0.5f);
        canAttackLeft = true;
    }

    IEnumerator DoAttackCooldownRight()
    {
        canAttackRight = false;
        yield return new WaitForSeconds(0.5f);
        canAttackRight = true;
    }
}
