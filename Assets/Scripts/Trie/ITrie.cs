using System.Collections.Generic;
using Zenject;

namespace Balda
{
    public interface ITrie : IInitializable
    {
        Node Root { get; }
        IReadOnlyCollection<string> Words { get; }
    }
}