/// <summary>
/// Class that represent a button
/// </summary>
namespace TurtleGames.Framework.Runtime.Input
{
    public class CInputGameButton
    {
        public string name;
        public bool pressed = false;
        public bool down = false;
        public bool up = false;
        public float timePressed = 0.0f;
    }
}