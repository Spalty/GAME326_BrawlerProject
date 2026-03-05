using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }
    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private FighterData FighterData => Context.FightData;
    private InputHandler InputHandler => Context.InputHandler;
    //private int _jumpCount = 0;
    

    public override void EnterState()
    {
        InitializeSubState();
        Context.CurrentRootState = RootStates.Jump;
        

        //HANDLE JUMP LOGIC
        if(Context.JumpCount > 0 && Context.InputHandler.HorizontalInput != 0)
        {
          PlayerRB.linearVelocity = new Vector2(Context.InputHandler.HorizontalInput * FighterData.HorizontalJumpForce, FighterData.VerticalJumpForce);
            Context.JumpCount--;
        }
        else if (Context.JumpCount > 0 && InputHandler.HorizontalInput == 0)
        {
            PlayerRB.linearVelocity = new Vector2(0, FighterData.VerticalJumpForce); // Reset vertical velocity before applying jump force
            Context.JumpCount--;
        }
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (!Context.IsGrounded())
        {
            SwitchState(Factory.Airborne());
        }
    }

    public override void ExitState()
    {
        InputHandler.WasJumpPressed = false; // Reset the input flag
    }
}
