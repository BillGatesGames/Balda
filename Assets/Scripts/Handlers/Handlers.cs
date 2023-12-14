namespace Balda
{
    public interface IStateHandler
    {
        void SwitchToState(StateData data);
    }

    public interface ISceneLoadHandler
    {
        void Load(int index);
    }
}

