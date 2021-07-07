namespace TurtleGames.Framework.Runtime.FPS
{
    public interface IInteractable
    {

        bool IsActive();

        void OnSight();

        void OutOfSight();

        void Interact();

        void Activate();

        void Deactivate();

        void DeInteract();

    }
}
