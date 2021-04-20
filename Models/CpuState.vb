Public class CpuState
    Public Property Flags as CpuFlags

    Public Property A as Byte
    Public Property B as Byte
    Public Property C as Byte
    Public Property D as Byte
    Public Property E as Byte
    Public Property H as Byte
    Public Property L as Byte

    Public Property StackPointer as UShort

    Public Property ProgramCounter as UShort

    Public Property Cycles as Ulong
End Class