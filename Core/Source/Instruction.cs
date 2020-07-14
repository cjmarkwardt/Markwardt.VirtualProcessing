using System.Collections.Generic;

namespace Markwardt.VirtualProcessing
{
    public interface IInstruction
    {
        void Execute(IProcessor processor);
    }
}