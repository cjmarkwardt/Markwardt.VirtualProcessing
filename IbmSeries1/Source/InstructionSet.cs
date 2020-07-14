using System;
using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class InstructionSet : IInstructionSet
    {
        public static int MinInstructionSize => 2;
        public static int MaxInstructionSize => 6;

        public static InstructionId GetInstructionId(byte[] data, int start = 0)
        {
            int code = data[start] >> 3;
            if (!Enum.IsDefined(typeof(InstructionId), code))
            {
                throw new InvalidOperationException($"Unknown instruction code {code}.");
            }

            return (InstructionId)code;
        }

        int IInstructionSet.MinInstructionSize => MinInstructionSize;
        int IInstructionSet.MaxInstructionSize => MaxInstructionSize;

        private IDictionary<InstructionId, InstructionReader> instructions = new List<InstructionReader>()
        {
            new AddByteImmediateInstruction.Reader(),
            new MoveByteImmediateInstruction.Reader(),
            new JumpOnConditionInstruction.Reader(),
            new JumpOnNotConditionInstruction.Reader(),

            new ManagementInstructionFamily()
        }.ToDictionary(r => r.Id);

        public (IInstruction Instruction, int AdditionalBytes) Read(byte[] data, int start = 0, int? length = null)
        {
            length = RangeUtils.Calculate(data.Length, start, length);

            if (length < 0)
            {
                throw new ArgumentException();
            }
            else if (length < 2)
            {
                return (null, 2 - length.Value);
            }
            else
            {
                InstructionId id = GetInstructionId(data, start);
                if (instructions.TryGetValue(id, out InstructionReader reader))
                {
                    return reader.Read(data, start, length);
                }

                throw new InvalidOperationException($"No reader registered for {id}.");
            }
        }
    }
}