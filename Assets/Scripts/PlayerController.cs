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
    private InputAction quit;
    private InputAction pause;
    private bool isGamePaused;
    [SerializeField] GameObject pauseIndicator;

    private PawnStatus playerStatus;
    [SerializeField] PawnStatus enemyStatus;

    private bool canAttackLeft;
    private bool canAttackRight;
    private bool canBlockLeft;
    private bool canBlockRight;
    
    private PlayerAnimator playerAnimator;
    
    [SerializeField] EnemyController enemyController;
    // player can recover health by landing four hits without getting hit
    private int hitsBeforeHeal = 4;
    private int subsequentHits;

    void Start()
    {
        playerStatus = GetComponent<PawnStatus>();
        playerAnimator = GetComponent<PlayerAnimator>();
        canAttackLeft = true;
        canAttackRight = true;
        canBlockLeft = true;
        canBlockRight = true;
        subsequentHits = 0;

        isGamePaused = false;
        pauseIndicator.SetActive(isGamePaused);
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

        quit = playerInput.Interface.Quit;
        pause = playerInput.Interface.Pause;


    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // QUIT GAME
        if (quit.WasPressedThisFrame())
        {
            Application.Quit();
        }

        // PAUSE GAME
        if (pause.WasPressedThisFrame() && playerStatus.IsPawnAlive())
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
            pauseIndicator.SetActive(isGamePaused);
        }

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
        if (blockLeft.IsPressed() && canAttackLeft)
        {
            BlockStartLeft();
        }
        else if (blockLeft.WasReleasedThisFrame())
        {
            playerStatus.SetBlockLeft(false);
        }
        if (blockRight.IsPressed() && canAttackRight)
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
            // enemy attempts to block if they aren't already blocking
            if (!enemyStatus.GetBlockLeft())
            {
                enemyController.ReactBlockLeft();
            }
            if (enemyStatus.TryDamage(1, PawnStatus.AttackTarget.Left))
            {
                subsequentHits++;
                if (subsequentHits >= hitsBeforeHeal)
                {
                    // heal player and reset subsequent hits
                    playerStatus.RecoverHealth(1);
                    subsequentHits = 0;
                }
            }
        }
    }

    void AttackRight()
    {
        if (canAttackRight)
        {
            StartCoroutine(DoAttackCooldownRight());
            playerAnimator.AttackRightAnim();
            if (!enemyStatus.GetBlockRight())
            {
                enemyController.ReactBlockRight();
            }
            if (enemyStatus.TryDamage(1, PawnStatus.AttackTarget.Right))
            {
                subsequentHits++;
                if (subsequentHits >= hitsBeforeHeal)
                {
                    // heal player and reset subsequent hits
                    playerStatus.RecoverHealth(1);
                    subsequentHits = 0;
                }
            }
        }
    }

    void BlockStartLeft()
    {
        if (canBlockLeft)
        {
            StartCoroutine(DoBlockCooldownLeft());
            playerStatus.SetBlockLeft(true);
            playerAnimator.BlockLeftAnim();
        }
    }

    void BlockStartRight()
    {
        if (canBlockRight)
        {
            StartCoroutine(DoBlockCooldownRight());
            playerStatus.SetBlockRight(true);
            playerAnimator.BlockRightAnim();
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

    public void ResetSubsequentHits()
    {
        subsequentHits = 0;
    }
}
