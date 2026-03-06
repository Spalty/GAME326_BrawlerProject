using UnityEngine;

public class PlayerAirDashState : PlayerBaseState
{
    public PlayerAirDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = false; 
    }

    private Rigidbody2D PlayerRB => Context.PlayerRB;
    private FighterData FighterData => Context.FightData;
    private InputHandler InputHandler => Context.InputHandler;

    public override void EnterState()
    {
        Context.CurrentSubSubState = SubSubStates.AirDash;
        //Context.AnimController.SetAirDashingBool(true);
        HandleAirDash();
    }

    public override void InitializeSubState() { }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
    public override void CheckSwitchState()
    {
        
    }

    public override void ExitState() 
    {
        //Context.AnimController.SetAirDashingBool(false);
        InputHandler.WasDashPressed = false; // Reset the input flag

    }

    private void HandleAirDash()
    {
        float dashDirection = InputHandler.HorizontalInput != 0 ? Mathf.Sign(InputHandler.HorizontalInput) : Mathf.Sign(Context.transform.localScale.x);
        PlayerRB.linearVelocity = new Vector2(dashDirection * FighterData.AirDashSpeed, 0f);
    }
}
