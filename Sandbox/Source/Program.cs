using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Markwardt.VirtualProcessing.IbmSeries1;

namespace Markwardt.VirtualProcessing
{
    public class Program
    {
        public static void Main(string[] args)
            => Start().Wait();

        private static async Task Start()
        {
            byte[] program = CreateInstructions().BuildExecutable().ToArray();

            var processor = new IbmSeries1Processor(new MemoryBlock(ushort.MaxValue), new MemoryBlock.Allocator());
            processor.Load(program);
            Console.WriteLine(processor);
            await processor.StepContinuously();
            Console.WriteLine(processor);
        }

        private static IEnumerable<Instruction> CreateInstructions()
        {
            var instructions = new List<Instruction>()
            {
                new MoveByteImmediateInstruction(0, 50),
                new StopInstruction()
            };

            return instructions;
        }
    }
}