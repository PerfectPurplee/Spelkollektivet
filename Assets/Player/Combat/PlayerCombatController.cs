using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using Synty.AnimationGoblinLocomotion.Samples;
using UnityEngine;

public partial class PlayerCombatController : MonoBehaviour
{
    public static PlayerCombatController Instance;

    public enum State
    {
        None,
        Walking,
        BasicAttack,
        Block,
        Dash
    }
    public State state;
    public bool shieldInHand = true;

    private float stateEnterTime = -1;

    [SerializeField]
    private float walkingSpeed = 2f;
    [SerializeField]
    private float walkingSpeedWhileBlocking = 1f;
    [SerializeField]
    private float walkingSpeedWhileAttacking = 1.5f;

    [SerializeField]
    private float bufforTimeLimit = 0.1f;
    [SerializeField]
    private float basicAttackDuration = 0.5f;
    [SerializeField]
    private float basicAttackHitBoxDuration = 0.2f;
    [SerializeField]
    private float blockMinDuration = 0.1f;
    [SerializeField]
    private float dashCooldownDuration = 3f;
    [SerializeField]
    private float dashDuration = 1f;
    [SerializeField]
    private float dashAttackHitBoxDuration = 0.5f;
    [SerializeField]
    private float dashPushForce = 1f;
    [SerializeField]
    private AnimationCurve dashRotationSpeed;
    [SerializeField]
    private Cooldown dashCooldown;
    [SerializeField]
    private float defaultRotationSpeed = 10f;
    [SerializeField]
    private float shieldThrowingForwardDuration = 1.5f;
    [SerializeField]
    private float shieldThrowDistance = 3f;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform shield;
    [SerializeField]
    private float distanceToShieldToCatch = 0.3f;
    [SerializeField]
    private float shieldMagnetForce = 1f;
    [SerializeField]
    private Quaternion shieldThrowingRotation;
    private Transform defaultShieldParent;
    private Vector3 defaultShieldPosition;
    private Quaternion defaultShieldRotation;
    private Vector3 currentShieldMovementDirection;
    private float currentShieldSpeed;
    private float shieldAcceleration;
    private float shieldStartSpeed;

    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    private SamplePlayerAnimationController samplePlayerAnimationController;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform targetPoint;
    [SerializeField]
    private Player.Player player;
    [SerializeField]
    private SingleAttackHitBox basicAttackHitBox;
    [SerializeField]
    private SingleAttackHitBox dashAttackHitBox;
    [SerializeField]
    private SingleAttackHitBox throwAttackHitBox;
    [SerializeField]
    private SlampCast slamCast;

    [SerializeField]
    private GameObject blockObject;
    private List<ParticleSystem> blockParticles;

    private float bufforTime;
    private Action bufforAction;

    public event Action OnEmpoweredAttack;
    public bool empoweredAttacks = false;
    public int empoweredAttackEveryX;
    public float empoweredAttackDamageMultiplier;
    private int empoweredAttackCounter = 0;
    [SerializeField]
    private Light empoweredAttackLight;
    [SerializeField]
    private GameObject empoweredAttackVFXPrefab;
    [SerializeField]
    private ParticleSystem basicAttackDeafultParticleSystem;
    private ParticleSystem.MainModule basicAttackDefaultParticleSystemMain;
    [SerializeField]
    private ParticleSystem basicAttackEmpoweredParticleSystem;
    private ParticleSystem.MainModule basicAttackEmpoweredParticleSystemMain;

    private void Awake()
    {
        Instance = this;
        dashAttackHitBox.OnAttackEntity += PushEnemyWhileDashing;
        defaultShieldPosition = shield.localPosition;
        defaultShieldRotation = shield.localRotation;
        defaultShieldParent = shield.parent;
        basicAttackDefaultParticleSystemMain = basicAttackDeafultParticleSystem.main;
        basicAttackEmpoweredParticleSystemMain = basicAttackEmpoweredParticleSystem.main;
        blockParticles = blockObject.GetComponentsInChildren<ParticleSystem>().ToList();
    }

    private void Start()
    {
        ChangeState(State.Walking);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetBufforAction(CastBasicAttack);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetBufforAction(StartBlock);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown.IsUsable)
        {
            SetBufforAction(Dash);
        }
        if (Input.GetMouseButton(1) && bufforAction == null && state != State.Block)
        {
            SetBufforAction(StartBlock);
        }
        if (Input.GetMouseButton(2))
        {
            SetBufforAction(ThrowShield);
        }
        if (Time.time - bufforTime > bufforTimeLimit)
        {
            //Debug.Log(bufforTime);
            //Debug.Log("time limit");
            bufforAction = null;
        }
    }

    private void FixedUpdate()
    {
        if (!shieldInHand)
        {
            UpdateThrowingShield();
        }

        switch (state)
        {
            case State.None:
                break;
            case State.Walking:
                if (bufforAction != null && (shieldInHand || bufforAction == Dash))
                {
                    InvokeBufforAction();
                }
                break;
            case State.BasicAttack:
                if (Time.time - stateEnterTime > basicAttackDuration)
                {
                    ChangeToAnyState();
                }
                if (basicAttackHitBox.attackActive && Time.time - stateEnterTime > basicAttackHitBoxDuration)
                {
                    if (empoweredAttacks)
                    {
                        if (empoweredAttackCounter + 1 == empoweredAttackEveryX)
                        {
                            empoweredAttackLight.enabled = true;
                        }
                        else
                        {
                            empoweredAttackLight.enabled = false;
                        }
                    }
                    basicAttackHitBox.FinishAttack();
                }
                break;
            case State.Block:
                player.shieldDirection = transform.forward;
                if (Time.time - stateEnterTime > blockMinDuration)
                {
                    if (bufforAction != null)
                    {
                        InvokeBufforAction();
                    }
                    else if (!Input.GetMouseButton(1))
                    {
                        ChangeState(State.Walking);
                    }
                }
                break;
            case State.Dash:
                samplePlayerAnimationController.rotationSmoothing = dashRotationSpeed.Evaluate(Time.time - stateEnterTime);
                if (Time.time - stateEnterTime > dashDuration)
                {
                    if (shieldInHand)
                    {
                        ChangeToAnyState();
                    }
                    else
                    {
                        ChangeState(State.Walking);
                    }
                }
                if (dashAttackHitBox.attackActive)
                {
                    if (Time.time - stateEnterTime > dashAttackHitBoxDuration)
                    {
                        dashAttackHitBox.FinishAttack();
                    }
                }
                break;
        }
    }

    private void UpdateThrowingShield()
    {
        float lastFrameSpeed = currentShieldSpeed;
        currentShieldSpeed -= shieldAcceleration * Time.fixedDeltaTime;
        if (lastFrameSpeed > 0 && currentShieldSpeed <= 0)
        {
            throwAttackHitBox.FinishAttack();
            throwAttackHitBox.StartAttack();
        }
        shield.transform.position += currentShieldMovementDirection * currentShieldSpeed * Time.fixedDeltaTime;
        shield.transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
        if (currentShieldSpeed <= 0)
        {
            if (Vector2.Distance(ToVectorXZ(transform.position), ToVectorXZ(shield.position)) <= distanceToShieldToCatch)
            {
                shield.parent = defaultShieldParent;
                shield.SetLocalPositionAndRotation(defaultShieldPosition, defaultShieldRotation);
                throwAttackHitBox.FinishAttack();
                shieldInHand = true;
                if (state == State.Dash)
                {
                    OnShieldCatchWhileDashing?.Invoke();
                }
            }
            else
            {
                Vector3 movementChangeVector = transform.position - shield.position;
                movementChangeVector.y = 0;
                movementChangeVector *= -1;
                movementChangeVector.Normalize();
                movementChangeVector *= Time.fixedDeltaTime * (-currentShieldSpeed) * shieldMagnetForce / (5 + Vector3.Distance(transform.position, shield.position));
                currentShieldMovementDirection += movementChangeVector;
                currentShieldMovementDirection.Normalize();
            }
        }
    }

    private void ThrowShield()
    {
        animator.SetTrigger("Throw");
        Vector3 direction = targetPoint.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        currentShieldMovementDirection = direction.normalized;
        shield.SetParent(null);
        shield.rotation = shieldThrowingRotation;
        shieldStartSpeed = shieldThrowDistance / shieldThrowingForwardDuration * 2;
        currentShieldSpeed = shieldStartSpeed;
        shieldAcceleration = shieldStartSpeed / shieldThrowingForwardDuration;
        throwAttackHitBox.StartAttack();
        shieldInHand = false;
    }

    private void Dash()
    {
        ChangeState(State.Dash);
        animator.SetTrigger("Dash");
        Vector3 direction = targetPoint.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        dashAttackHitBox.StartAttack();
        dashCooldown.Use();
    }

    private void PushEnemyWhileDashing(IDamageable damageable)
    {
        Debug.Log("PUSH");
        if (damageable is MonoBehaviour && ((MonoBehaviour)damageable).TryGetComponent(out Rigidbody enemy))
        {
            Debug.Log("yup");
            if (
                Vector3.Distance(player.transform.position + player.transform.right, enemy.position) 
                > Vector3.Distance(player.transform.position - player.transform.right, enemy.position))
            {
                Debug.Log("left");
                enemy.AddForce(-player.transform.right * dashPushForce);
            }
            else
            {
                Debug.Log("right");
                enemy.AddForce(player.transform.right * dashPushForce);
            }
        }
    }

    private void CastBasicAttack()
    {
        ChangeState(State.BasicAttack);
        animator.SetTrigger("Basic Attack");

        if (empoweredAttacks)
        {
            empoweredAttackCounter++;
            if (empoweredAttackCounter >= empoweredAttackEveryX)
            {
                basicAttackHitBox.multiplier = empoweredAttackDamageMultiplier;
                empoweredAttackCounter = 0;
                OnEmpoweredAttack?.Invoke();
                basicAttackEmpoweredParticleSystem.Play();
                //Instantiate(empoweredAttackVFXPrefab, transform.position + transform.forward * 1.5f + transform.up, Quaternion.identity);
            }
            else
            {
                basicAttackHitBox.multiplier = 1;
                empoweredAttackLight.enabled = false;
                basicAttackDeafultParticleSystem.Play();
                //basicAttackParticleSystemMain.startColor = defaultBasicAttackVFXColor;
            }
        }
        else
        {
            basicAttackDeafultParticleSystem.Play();
        }
        basicAttackHitBox.StartAttack();
    }

    private void StartBlock()
    {
        blockObject.SetActive(true);
        shield.transform.SetParent(blockObject.transform);
        shield.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        animator.SetBool("Block", true);
        ChangeState(State.Block);
    }

    private void SetBufforAction(Action action)
    {
        bufforAction = action;
        bufforTime = Time.time;
    }

    private void InvokeBufforAction()
    {
        bufforAction.Invoke();
        bufforAction = null;
    }

    private void ChangeToAnyState()
    {
        if (bufforAction == null)
        {
            ChangeState(State.Walking);
        }
        else
        {
            bufforAction.Invoke();
        }
    }

    private void ChangeState(State newState)
    {
        player.shielding = false;

        if (state == State.Block && newState != State.Block)
        {
            shield.parent = defaultShieldParent;
            shield.SetLocalPositionAndRotation(defaultShieldPosition, defaultShieldRotation);
            blockObject.SetActive(false);
            animator.SetBool("Block", false);
        }

        float speed = 0;
        switch (newState)
        {
            case State.None:
                break;
            case State.Walking:
                speed = walkingSpeed;
                break;
            case State.BasicAttack:
                speed = walkingSpeedWhileAttacking;
                break;
            case State.Block:
                speed = walkingSpeedWhileBlocking;
                player.shielding = true;
                break;
            case State.Dash:
                speed = 0;
                break;
        }
        samplePlayerAnimationController.rotationSmoothing = newState == State.Dash ? dashRotationSpeed.Evaluate(0) : defaultRotationSpeed;
        samplePlayerAnimationController.targetMaxSpeed = speed;

        stateEnterTime = Time.time;
        state = newState;
    }

    private Vector2 ToVectorXZ(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    private void OnDrawGizmos()
    {
        //Color color = Color.white;
        //switch (state)
        //{
        //    case State.None:
        //        break;
        //    case State.Walking:
        //        color = Color.yellow;
        //        break;
        //    case State.BasicAttack:
        //        color = Color.red;
        //        break;
        //    case State.Block:
        //        color = Color.blue;
        //        break;
        //    case State.Dash:
        //        color = Color.green;
        //        break;
        //}
        //Gizmos.color = color;
        //Gizmos.DrawWireSphere(transform.position, 2);
    }
}
