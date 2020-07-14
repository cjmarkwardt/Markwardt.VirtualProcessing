using System.Collections.Generic;
using System.Linq;

namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public class JumpOnNotConditionInstruction : ConditionalJumpInstruction
    {
        public JumpOnNotConditionInstruction(IEnumerable<Word> words)
            : base(words) { }

        public JumpOnNotConditionInstruction(ConditionCode condition, byte wordDisplacement)
            : base(condition, wordDisplacement) { }

        public override byte[] Export()
            => new byte[] { (byte)(((int)InstructionId.AddByteImmediate << 3) | (int)Condition), (byte)Jump.Unsigned };

        protected override IReadOnlyDictionary<ConditionCode, Test> Conditions { get; } = new Dictionary<ConditionCode, Test>()
        {
            { ConditionCode.Zero, l => !l.GetStatusFlag(LevelFlag.Zero) },
            { ConditionCode.Positive, l => l.GetStatusFlag(LevelFlag.Zero) || l.GetStatusFlag(LevelFlag.Negative) },
            { ConditionCode.Negative, l => !l.GetStatusFlag(LevelFlag.Negative) },
            { ConditionCode.Even, l => !l.GetStatusFlag(LevelFlag.Even) },
            { ConditionCode.ArithmeticallyLess, l => (l.GetStatusFlag(LevelFlag.Overflow) && l.GetStatusFlag(LevelFlag.Negative)) || (!l.GetStatusFlag(LevelFlag.Overflow) && !l.GetStatusFlag(LevelFlag.Negative)) },
            { ConditionCode.ArithmeticallyLessOrEqual, l => (l.GetStatusFlag(LevelFlag.Overflow) && l.GetStatusFlag(LevelFlag.Negative) && !l.GetStatusFlag(LevelFlag.Zero)) || (!l.GetStatusFlag(LevelFlag.Overflow) && !l.GetStatusFlag(LevelFlag.Negative) && !l.GetStatusFlag(LevelFlag.Zero)) },
            { ConditionCode.LogicallyLessOrEqual, l => !l.GetStatusFlag(LevelFlag.Carry) && !l.GetStatusFlag(LevelFlag.Zero) },
            { ConditionCode.Carry, l => !l.GetStatusFlag(LevelFlag.Carry) }
        };

        public class Reader : InstructionReader
        {
            public override InstructionId Id => InstructionId.JumpOnNotCondition;

            public override (IInstruction Instruction, int AdditionalWords) ReadWords(IEnumerable<Word> words)
                => ReadStaticSize(words, Size, w => new JumpOnNotConditionInstruction(w));
        }
    }
}