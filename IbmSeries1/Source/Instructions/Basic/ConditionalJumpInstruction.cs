using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public abstract class ConditionalJumpInstruction : Instruction
    {
        public static int Size => 1;

        public ConditionalJumpInstruction(IEnumerable<Word> words)
        {
            Word word = words.First();
            Condition = (ConditionCode)word.Extract(5, 3).Unsigned;
            Jump = word.Extract(8, 8);
        }

        public ConditionalJumpInstruction(ConditionCode condition, Word wordDisplacement)
        {
            Condition = condition;
        }

        public ConditionCode Condition { get; }
        public Word Jump { get; }

        protected delegate bool Test(IbmSeries1Processor.LevelBlock level);

        protected abstract IReadOnlyDictionary<ConditionCode, Test> Conditions { get; }

        protected override void ExecuteInstruction(IbmSeries1Processor processor)
        {
            if (Conditions.TryGetValue(Condition, out Test test) && test(processor.Level))
            {
                processor.Level.NextInstruction = Jump;
            }
            else
            {
                processor.Move(Size);
            }
        }
    }
}