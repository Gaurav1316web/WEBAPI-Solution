Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Management
Imports System.ComponentModel
Module ProcessHelpers
    ' Win32 API for User SID (from previous)
    <DllImport("advapi32.dll", SetLastError:=True)> Private Function OpenProcessToken(hProcess As IntPtr, DesiredAccess As Integer, ByRef TokenHandle As IntPtr) As Boolean
    End Function
    <DllImport("advapi32.dll", SetLastError:=True)> Private Function GetTokenInformation(TokenHandle As IntPtr, TokenInformationClass As Integer, TokenInformation As IntPtr, TokenInformationLength As UInteger, ByRef ReturnLength As UInteger) As Boolean
    End Function
    <DllImport("kernel32.dll")> Private Function OpenProcess(dwDesiredAccess As UInteger, bInheritHandle As Boolean, dwProcessId As UInteger) As IntPtr
    End Function
    <DllImport("advapi32.dll", CharSet:=CharSet.Unicode, SetLastError:=True)> Private Function ConvertSidToStringSidW(pSid As IntPtr, ByRef pStringSid As String) As Boolean
    End Function

    Public Function GetProcessUser(procId As Integer) As String
        Try
            Dim hProcess As IntPtr = OpenProcess(&H1000, False, CUInt(procId))
            If hProcess = IntPtr.Zero Then Return "Access Denied"

            Dim hToken As IntPtr = IntPtr.Zero
            OpenProcessToken(hProcess, 8, hToken)

            Dim size As UInteger = 0
            GetTokenInformation(hToken, 1, IntPtr.Zero, 0, size) ' TokenUser=1
            Dim buf As IntPtr = Marshal.AllocHGlobal(CInt(size))
            GetTokenInformation(hToken, 1, buf, size, size)

            Dim tokUser As TOKEN_USER = DirectCast(Marshal.PtrToStructure(buf, GetType(TOKEN_USER)), TOKEN_USER)
            Dim sidStr As String = Nothing
            ConvertSidToStringSidW(tokUser.User.Sid, sidStr)
            Marshal.FreeHGlobal(buf)

            Return New SecurityIdentifier(sidStr).Translate(GetType(NTAccount)).Value
        Catch
            Return "N/A"
        End Try
    End Function

    Public Function GetParentPid(procId As Integer) As Integer
        Try
            Dim q As New ObjectQuery($"SELECT ParentProcessId FROM Win32_Process WHERE ProcessId={procId}")
            Using searcher As New ManagementObjectSearcher(q)
                For Each mo As ManagementObject In searcher.Get()
                    Return CInt(mo("ParentProcessId"))
                Next
            End Using
        Catch
        End Try
        Return 0
    End Function

    <StructLayout(LayoutKind.Sequential)> Private Structure TOKEN_USER
        Public User As SID_AND_ATTRIBUTES
    End Structure
    <StructLayout(LayoutKind.Sequential)> Private Structure SID_AND_ATTRIBUTES
        Public Sid As IntPtr
        Public Attributes As Integer
    End Structure
End Module
