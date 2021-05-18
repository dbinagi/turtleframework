namespace TurtleGames.Framework.Runtime.FPS
{
    public interface IInteractable
    {

        public bool IsActive();

        public void OnSight();

        public void OutOfSight();

        public void Interact();

        public void Activate();

        public void Deactivate();

    }
}
