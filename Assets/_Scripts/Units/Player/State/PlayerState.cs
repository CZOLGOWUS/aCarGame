using CarGame.Managers;

namespace CarGame.Units
{
    public abstract class PlayerState
    {
        public abstract float speedMultiplier { get; set; }
        public abstract void EnterState(PlayerManager manager);
        public abstract void Update(PlayerManager manager);
        public abstract void Interact(PlayerManager manager);
    }
}