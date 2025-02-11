using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : State
{
    float gravityValue;
    float playerSpeed;
    bool grounded;
    bool cbattack;

    Vector3 currentVelocity;
    Vector3 cVelocity;
    public attack(Character _character, StateMachine _stateMachine):base(_character, _stateMachine)
    {
        character = _character;

        stateMachine = _stateMachine;
    }
    public override void Enter()
    {
        base.Enter();

        cbattack = false;
        character.aniamtor.applyRootMotion = true;
        

        gravityVelocity.y = 0;

        input = Vector2.zero;
        currentVelocity = Vector3.zero;

        character.aniamtor.SetTrigger("attack");
        character.aniamtor.SetFloat("speed", 0);

        velocity = character.playerVelocity;
        grounded = character.controller.isGrounded;
        playerSpeed = character.playerSpeed;
        gravityValue = character.gravityValue;

    }


    public override void HandleInput()
    {
        base.HandleInput();
        if (attackAction.triggered)
        {
            cbattack=true;
        }

        input = moveAction.ReadValue<Vector2>();
        velocity = new Vector3(input.x,0,input.y);
        velocity = velocity.x*character.cameraTransform.right.normalized+velocity.z*character.cameraTransform.forward.normalized;
        velocity.y = 0;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;

        if (grounded && gravityVelocity.y < 0f)
        {

            gravityVelocity.y = 0f;
        }

        currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref cVelocity, character.velocityDampTime);
        character.controller.Move(currentVelocity * Time.deltaTime * playerSpeed + gravityVelocity * Time.deltaTime);

        //Xoay Nguoi choi
        if (velocity.sqrMagnitude > 0f)
        {
            character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(velocity), character.rotationDampTime);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        character.aniamtor.SetFloat("speed",input.magnitude,character.speedDampTime,Time.deltaTime);

        if (cbattack)
        {
            character.aniamtor.SetTrigger("attack");
            stateMachine.ChangeState(character.standing);
        }
    }
    public override void Exit()
    {
        base.Exit();

        gravityVelocity.y=0f;
        character.playerVelocity = new Vector3(input.x,0,input.y);
        character.aniamtor.SetTrigger("move");
    }
}
