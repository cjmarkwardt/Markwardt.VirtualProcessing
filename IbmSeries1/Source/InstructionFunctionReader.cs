namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public abstract class InstructionFunctionReader : InstructionReader
    {
        public abstract Word FunctionCode { get; }
    }
}