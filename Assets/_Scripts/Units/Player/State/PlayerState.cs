using CarGame.Managers;

namespace CarGame.Units
{
    public abstract class PlayerState
    {
        public abstract void EnterState(PlayerManager manager);
        public abstract void Update(PlayerManager manager);
        public abstract void Interact(PlayerManager manager);
    }
}