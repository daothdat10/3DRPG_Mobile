using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : State
{
    float gravityValue;

    Vector3 currentVelocity;
    bool grounded;
    float playerSpeed;
    bool attack;

    

    Vector3 cVelocity;
    public StandingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        

        input = Vector2.zero;
        velocity = Vector3.zero;
        currentVelocity = Vector3.zero;
        gravityVelocity.y = 0f;


        playerSpeed = character.playerSpeed;
        grounded = character.controller.isGrounded;
        gravityValue = character.gravityValue;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x, 0, input.y);
        velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
        velocity.y = 0f;
        if(attackAction.triggered)
        {
            attack = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
        

        if (grounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = 0f;
        }

        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        if (velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.aniamtor.SetFloat("speed", input.magnitude, character.speedDampTime, Time.deltaTime);
        if(attack)
        {
            character.aniamtor.SetTrigger("attack");
            stateMachine.ChangeState(character.combatAttack);
        }
       
        

    }
    public override void Exit()
    {
        base.Exit();    
        gravityVelocity.y=0f;
        character.playerVelocity = new Vector3(input.x,0,input.y);

        if(velocity.sqrMagnitude > 0)
        {
            character.transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

}
