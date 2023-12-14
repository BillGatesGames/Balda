using Zenject;

namespace Balda
{
    public class SingalBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<StateData>();
            Container.DeclareSignal<int>();
        }
    }
}
