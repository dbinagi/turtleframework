using UnityEngine;
using TurtleGames.Framework.Runtime.StateMachine;

namespace TurtleGames.Framework.Runtime.UI
{
	class CStateCanvasGroup : CContext
	{

	    private CanvasGroup canvasGroup;
	    private float duration;

	    public float Duration { get => duration; set => duration = value; }
	    public CanvasGroup CanvasGroup { get => canvasGroup; set => canvasGroup = value; }

	}

}