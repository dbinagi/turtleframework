namespace TurtleGames.Framework.Runtime.StateMachine
{

	public abstract class CContext : IContext
	{
	    private IState currentState;
	    
	    public IState CurrentState { get => currentState; set => currentState = value; }
	  
	    public void SetState(IState state)
	    {
	        if(CurrentState != null)
	            CurrentState.OnExit(this);

	        CurrentState = state;
	        CurrentState.OnEnter(this);
	    }

	    public void Update()
	    {
	        if(CurrentState != null)
	            CurrentState.OnUpdate(this);
	    }
	}

}