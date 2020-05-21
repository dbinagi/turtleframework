namespace TurtleGames.Framework.Runtime.StateMachine
{
	public interface IState
	{
	    void OnEnter(CContext context);
	    void OnExit(CContext context);
	    void OnUpdate(CContext context);
	}
}