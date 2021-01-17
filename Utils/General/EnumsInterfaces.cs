

namespace AnilTools
{
    public interface ITickable
    {
        void Tick();
        int InstanceId();
    }
    
    public enum UpdateType
    {
        fixedTime = 1, normal = 2 , SlowUpdate = 4
    }
    
    public enum MoveType
    { 
        lerp , towards , fastToSlow , slowToFast
    }
}