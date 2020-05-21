namespace TurtleGames.Framework.Runtime.StateMachine
{
	public interface IContext
	{
	    void SetState(IState state);
	    void Update();
	    void FixedUpdate();
	}
}
