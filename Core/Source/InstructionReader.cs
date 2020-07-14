namespace Markwardt.VirtualProcessing
{
    public interface IInstructionReader
    {
        (IInstruction Instruction, int AdditionalBytes) Read(byte[] data, int start = 0, int? length = null);
    }
}