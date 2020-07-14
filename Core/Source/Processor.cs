using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Markwardt.VirtualProcessing
{
    public interface IReadOnlyProcessor
    {
        IInstructionSet InstructionSet { get; }

        IReadOnlyMemoryBlock Memory { get; }
        IMemoryReader Reader { get; }

        IInstruction LastInstruction { get; }
    }

    public interface IRunOnlyProcessor : IReadOnlyProcessor
    {
        void Load();
        void Load(IEnumerable<byte> program);
        void Stop();

        void Execute(IInstruction instruction);
        bool Step();
    }

    public interface IProcessor : IRunOnlyProcessor
    {
        new IMemoryBlock Memory { get; }
        IMemoryWriter Writer { get; }
    }

    public abstract class Processor : IProcessor
    {
        public abstract IInstructionSet InstructionSet { get; }
        
        public abstract IMemoryBlock Memory { get; }
        public abstract IMemoryReader Reader { get; }
        public abstract IMemoryWriter Writer { get; }

        public IInstruction LastInstruction { get; private set; }

        IReadOnlyMemoryBlock IReadOnlyProcessor.Memory => Memory;

        public void Execute(IInstruction instruction)
        {
            LastInstruction = instruction;
            instruction.Execute(this);
        }

        public abstract void Load();
        public abstract void Load(IEnumerable<byte> program);
        public abstract void Stop();
        public abstract bool Step();
    }

    public static class ProcessorUtils
    {
        public static void Execute(this IRunOnlyProcessor processor, long address)
            => processor.Execute(processor.InstructionSet.Read(processor.Memory, address, out int size));

        public static void Reload(this IRunOnlyProcessor processor)
        {
            processor.Stop();
            processor.Load();
        }

        public static async Task StepContinuously(this IRunOnlyProcessor processor, float? rate = null, CancellationToken cancellation = default(CancellationToken))
        {
            int delay = rate == null ? -1 : (int)(1000 / rate.Value);
            while (!cancellation.IsCancellationRequested)
            {
                if (!processor.Step())
                {
                    break;
                }

                if (delay != -1)
                {
                    await Task.Delay(delay);
                }
            }
        }

        public static async Task StepForever(this IRunOnlyProcessor processor, float? rate = null, CancellationToken cancellation = default(CancellationToken))
        {
            while (!cancellation.IsCancellationRequested)
            {
                await processor.StepContinuously(rate, cancellation);
            }
        }
    }
}