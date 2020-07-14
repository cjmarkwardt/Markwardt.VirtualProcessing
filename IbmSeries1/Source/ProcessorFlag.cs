namespace Markwardt.VirtualProcessing.IbmSeries1
{
    public enum ProcessorFlag
    {
        SpecificationCheck = 0,
        InvalidStorageAddress = 1,
        PrivilegeViolation = 2,
        ProtectionCheck = 3,
        InvalidFunction = 4,
        FloatingPoint = 5,
        StackException = 6,

        StorageParityCheck = 8,

        ProcessorControlCheck = 10,
        InputOutputCheck = 11,
        SequenceIndicator = 12,
        AutoLoad = 13,
        TranslatorEnabled = 14,
        PowerThermalWarning = 15
    }
}