using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Markwardt.VirtualProcessing.IbmSeries1;

namespace Markwardt.VirtualProcessing
{
    public class IbmSeries1Processor : Processor
    {
        public static int LevelCount => 4;

        public IbmSeries1Processor(IMemoryBlock memory, IMemoryAllocator registers)
        {
            Memory = memory;
            Writer = new MemoryWriter(Memory);
            Reader = new MemoryReader(Memory);

            var levelBlocks = new List<LevelBlock>();
            for (int level = 0; level < LevelCount; level++)
            {
                levelBlocks.Add(new LevelBlock(level));
            }

            Levels = levelBlocks;
        }

        public override IInstructionSet InstructionSet => new InstructionSet();

        public override IMemoryBlock Memory { get; }
        public override IMemoryReader Reader { get; }
        public override IMemoryWriter Writer { get; }
        
        public IReadOnlyList<LevelBlock> Levels { get; }

        public ProcessorState State { get; set; } = ProcessorState.Stop;
        public int LevelId { get; set; } = 0;

        public LevelBlock Level => Levels[LevelId];

        public Word Status { get; set; }

        public bool GetStatusFlag(ProcessorFlag flag)
            => Status[(int)flag];

        public void SetStatusFlag(ProcessorFlag flag, bool value)
            => Status = Status.SetBit((int)flag, value);

        public Word InterruptMask { get; set; }

        public bool GetInterruptEnable(int level)
            => InterruptMask[level];

        public void SetInterruptEnable(int level, bool value)
            => InterruptMask = InterruptMask.SetBit(level, value);

        private DateTime clockStart = DateTime.Now;
        private DoubleWord clockBase = DoubleWord.Zero;
        public DoubleWord Clock
        {
            get
            {
                unchecked
                {
                    return (uint)(clockBase.Unsigned + (DateTime.Now - clockStart).TotalMilliseconds);
                }
            }
            set
            {
                clockStart = DateTime.Now;
                clockBase = value;
            }
        }

        public DoubleWord ClockComparator { get; set; }

        public void Move(int words)
            => Level.NextInstruction = (ushort)(Level.NextInstruction.Unsigned + (words * Word.ByteCount));

        public override void Load()
        {
            ClearRegisters();
            State = ProcessorState.Run;
        }

        public override void Stop()
        {
            State = ProcessorState.Stop;
        }

        public override bool Step()
        {
            if (State == ProcessorState.Stop || State == ProcessorState.Wait)
            {
                return false;
            }

            this.Execute(Level.NextInstruction.Unsigned);
            return true;
        }

        public void ClearRegisters()
        {
            LevelId = 0;
            Status = Word.Zero;
            InterruptMask = Word.Zero;

            foreach (LevelBlock level in Levels)
            {
                level.Clear();
            }
        }

        public override void Load(IEnumerable<byte> program)
        {
            Memory.Write(0, program);
            Load();
        }

        public override string ToString()
            => ToString(true);

        public string ToString(bool titled, int indentLevel = 0)
        {
            StringBuilder builder = new StringBuilder();

            string indent = string.Empty;
            for (int i = 0; i < indentLevel; i++)
            {
                indent += '\t';
            }

            if (titled)
            {
                builder.Append($"{indent}IBM Series 1 Processor");
                indentLevel++;
                indent += '\t';
            }

            builder.Append($"{(titled ? "\n" : string.Empty)}{indent}Status = {Status.ToString(false)}");
            builder.Append($"\n{indent}Interrupt Mask = {InterruptMask.ToString(false)}");
            builder.Append($"\n{indent}Clock = {Clock.ToString(false)}");
            builder.Append($"\n{indent}Clock Comparator = {ClockComparator.ToString(false)}");

            foreach (LevelBlock level in Levels)
            {
                builder.Append($"\n{indent}Level {level.Level}{(level == Level ? " (Current)" : string.Empty)}\n{level.ToString(false, indentLevel + 1)}");
            }

            return builder.ToString();
        }

        public class LevelBlock
        {
            public static int GeneralCount => 8;
            
            public LevelBlock(int level)
            {
                Level = level;
            }

            public int Level { get; }

            public Word Status { get; set; }

            public bool GetStatusFlag(LevelFlag flag)
                => Status[(int)flag];

            public void SetStatusFlag(LevelFlag flag, bool value)
                => Status = Status.SetBit((int)flag, value);

            public Word AddressKey { get; set; }

            public Word NextInstruction { get; set; }

            public Word[] Generals { get; } = new Word[GeneralCount];

            public Word this[int generalIndex]
            {
                get => Generals[generalIndex];
                set => Generals[generalIndex] = value;
            }

            public Word this[Word generalIndex]
            {
                get => Generals[generalIndex.Unsigned];
                set => Generals[generalIndex.Unsigned] = value;
            }

            public void Clear()
            {
                Status = Word.Zero;
                AddressKey = Word.Zero;
                NextInstruction = Word.Zero;

                for (int i = 0; i < Generals.Length; i++)
                {
                    Generals[i] = Word.Zero;
                }
            }

            public override string ToString()
                => ToString(true);

            public string ToString(bool titled, int indentLevel = 0)
            {
                StringBuilder builder = new StringBuilder();

                string indent = string.Empty;
                for (int i = 0; i < indentLevel; i++)
                {
                    indent += '\t';
                }

                if (titled)
                {
                    builder.Append($"{indent}IBM Series 1 Level Block ({Level})");
                    indentLevel++;
                    indent += '\t';
                }

                builder.Append($"{(titled ? "\n" : string.Empty)}{indent}Status = {Status.ToString(false)}");
                builder.Append($"\n{indent}Address Key = {AddressKey.ToString(false)}");
                builder.Append($"\n{indent}Next Instruction = {NextInstruction.ToString(false)}");

                for (int i = 0; i < Generals.Length; i++)
                {
                    builder.Append($"\n{indent}General {i} = {Generals[i].ToString(false)}");
                }

                return builder.ToString();
            }
        }
    }
}