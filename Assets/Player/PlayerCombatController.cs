using System;
using Synty.AnimationGoblinLocomotion.Samples;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public enum State
    {
        None,
        Walking,
        BasicAttack,
        Block,
        Dash
    }
    public State state;
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
    private float blockMinDuration = 0.1f;
    [SerializeField]
    private float dashDuration = 0.2f;
    [SerializeField]
    private AnimationCurve dashRotationSpeed;
    [SerializeField]
    private float defaultRotationSpeed = 10f;

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

    private float bufforTime;
    private Action bufforAction;

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
        if (Input.GetMouseButtonDown(2))
        {
            SetBufforAction(Dash);
        }
        if (Input.GetMouseButton(1) && bufforAction == null)
        {
            SetBufforAction(StartBlock);
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
        switch (state)
        {
            case State.None:
                break;
            case State.Walking:
                if (bufforAction != null)
                {
                    InvokeBufforAction();
                }
                break;
            case State.BasicAttack:
                if (Time.time - stateEnterTime > basicAttackDuration)
                {
                    ChangeToAnyState();
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
                    ChangeToAnyState();
                }
                break;
        }
    }

    private void Dash()
    {
        ChangeState(State.Dash);
        animator.SetTrigger("Dash");
        Vector3 direction = targetPoint.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void CastBasicAttack()
    {
        ChangeState(State.BasicAttack);
        animator.SetTrigger("Basic Attack");
        //attack logic
    }

    private void StartBlock()
    {
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
