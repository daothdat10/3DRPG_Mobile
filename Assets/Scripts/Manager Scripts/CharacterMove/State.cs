using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class State 
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction attackAction;
  
    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;

        moveAction = character.playerInput.actions["move"];
        attackAction = character.playerInput.actions["attack"];
        lookAction = character.playerInput.actions["look"];
        
    }

    public virtual void Enter()
    {
        Debug.Log("Enter state"+ this.ToString());

    }
    public virtual void HandleInput() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void Exit() { }


}
