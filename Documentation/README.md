# How to use the framework

## State Machines

### Create new State

1) Creates a Context for the state machine

```
public class CStatePlayer : CContext
{

}
```

2) Creates the states for that context

```
using TurtleGames.Framework.Runtime.StateMachine;

public class CStatePlayerMoving : IState
{
    public void OnEnter(CContext context) { }

    public void OnExit(CContext context) { }

    public void OnFixedUpdate(CContext context) { }

    public void OnUpdate(CContext context) { }
}
```

3) Add the state machine to the manager

```
state = new CStatePlayer();
state.SetState(new CStatePlayerWaitingTouch());
CStateMachinesManager.Inst.AddStateMachine(state);
```

### Compare States

```
if(state.CurrentState is CStatePlayerMoving)
```
