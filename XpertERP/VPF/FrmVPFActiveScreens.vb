Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports Microsoft.VisualBasic.PowerPacks
Imports System.Text.RegularExpressions
Imports common

Public Class FrmVPFActiveScreens
    Inherits FrmMainTranScreen

    Private Sub TxtModuleCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtModuleCode._MYValidating
        Dim qry As String = ""
        qry = "Select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name From TSPL_PROGRAM_MASTER"
        TxtModuleCode.Value = clsCommon.ShowSelectForm("VPFModCod", qry, "Code", " Type='M' AND Program_Code <>'MUtility' ", TxtModuleCode.Value, "Code", isButtonClicked)
        If clsCommon.myLen(TxtModuleCode.Value) > 0 Then
            LblModuleName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name END AS Name  From TSPL_PROGRAM_MASTER Where Program_Code='" + TxtModuleCode.Value + "'"))


            If clsCommon.CompairString(TxtModuleCode.Value, "MGenLedger") = CompairStringResult.Equal Then
                LblMaster.Text = "SMGLSetup"
                LblTrans.Text = "SMGLTrans"
                LblRptParent.Text = "SMGLReport"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MCommSer") = CompairStringResult.Equal Then
                LblMaster.Text = "SMCSSetup"
                LblTrans.Text = "SMCSTrans"
                LblRptParent.Text = "SMCSReport"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MPayable") = CompairStringResult.Equal Then
                LblRptParent.Text = "SMPayReport"
                LblMaster.Text = "SMPaySetup"
                LblTrans.Text = "SMPayTrans"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MReceivable") = CompairStringResult.Equal Then
                LblMaster.Text = "SMRecSetup"
                LblTrans.Text = "SMRecTrans"
                LblRptParent.Text = "SMRecReport"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MBulkSale") = CompairStringResult.Equal Then
                LblMaster.Text = "SMBulkSale"
                LblTrans.Text = "SMBULKSTRAN"
                LblRptParent.Text = "SMBULKSRPT"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MFreshSale") = CompairStringResult.Equal Then
                LblMaster.Text = "SMFreshSale"
                LblTrans.Text = "SMFRESHSTRAN"
                LblRptParent.Text = "SMFRESHSRPT"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MProductSale") = CompairStringResult.Equal Then
                LblMaster.Text = "SMProductSle"
                LblTrans.Text = "SMPRODSTRAN"
                LblRptParent.Text = "SMPRODSRPT"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MCSASale") = CompairStringResult.Equal Then
                LblMaster.Text = "SMCSASale"
                LblTrans.Text = "SMCSATRAN"
                LblRptParent.Text = "SMCSARPT"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MMaterial") = CompairStringResult.Equal Then
                LblMaster.Text = "SMMatSetup"
                LblTrans.Text = "SMMatTrans"
                LblRptParent.Text = "SMMatReport"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MPurchase") = CompairStringResult.Equal Then
                LblRptParent.Text = "SMPurReport"
                LblMaster.Text = "SMPurSetup"
                LblTrans.Text = "SMPurtrans"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MMMProc") = CompairStringResult.Equal Then
                LblRptParent.Text = "SMMPROCRPT"
                LblMaster.Text = "SMMPROCSetup"
                LblTrans.Text = "SMCPROCTRAN"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MMBProc") = CompairStringResult.Equal Then
                LblRptParent.Text = "SMBPROCRPT"
                LblMaster.Text = "SMBPROCSetup"
                LblTrans.Text = "SMMPROCTRANS"
            ElseIf clsCommon.CompairString(TxtModuleCode.Value, "MProdDairy") = CompairStringResult.Equal Then
                LblMaster.Text = "SMPDSetupD"
                LblTrans.Text = "SMPDTransD"
                LblRptParent.Text = "SMPDReportD"
           
            End If
            LoadRpt(TxtModuleCode.Value, LblMaster.Text, LblTrans.Text, LblRptParent.Text)
            LoadDataReports()
            LoadDataMasters()
            LoadDataTransactions()
        Else
            LblModuleName.Text = ""
            cbgRpt.DataSource = Nothing
            cbgMasters.DataSource = Nothing
            cbgTrans.DataSource = Nothing
            LblRptParent.Text = ""
            LblTrans.Text = ""
        End If
    End Sub
    Public Sub LoadRpt(ByVal StrModuleCode As String, ByVal StrMasCode As String, ByVal StrTransCode As String, ByVal StrReportCode As String)
        Dim RptQry As String = String.Empty
        Dim MasQry As String = String.Empty
        Dim TransQry As String = String.Empty

        'If clsCommon.CompairString(StrModuleCode, "MPurchase") = CompairStringResult.Equal Then
        '    RptQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMPurReport'  order by SNo "
        '    MasQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMPurSetup'  order by SNo "
        '    TransQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMPurtrans'  order by SNo "

        '    LblRptParent.Text = "SMPurReport"
        '    LblMaster.Text = "SMPurSetup"
        '    LblTrans.Text = "SMPurtrans"
        'ElseIf clsCommon.CompairString(StrModuleCode, "MMMProc") = CompairStringResult.Equal Then
        '    RptQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMMPROCRPT'   order by SNo "
        '    MasQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMMPROCSetup'   order by SNo "
        '    TransQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMMPROCTRANS'   order by SNo "
        '    LblRptParent.Text = "SMMPROCRPT"
        '    LblMaster.Text = "SMMPROCSetup"
        '    LblTrans.Text = "SMMPROCTRANS"
        'ElseIf clsCommon.CompairString(StrModuleCode, "MMBProc") = CompairStringResult.Equal Then
        '    RptQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMBPROCRPT'   order by SNo "
        '    MasQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMBPROCSetup'   order by SNo "
        '    TransQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMMPROCTRANS'   order by SNo "
        '    LblRptParent.Text = "SMBPROCRPT"
        '    LblMaster.Text = "SMBPROCSetup"
        '    LblTrans.Text = "SMMPROCTRANS"
        'ElseIf clsCommon.CompairString(StrModuleCode, "MPayable") = CompairStringResult.Equal Then
        '    RptQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMPayReport'   order by SNo "
        '    MasQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMPaySetup'   order by SNo "
        '    TransQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='SMPayTrans'   order by SNo "
        '    LblRptParent.Text = "SMPayReport"
        '    LblMaster.Text = "SMPaySetup"
        '    LblTrans.Text = "SMPayTrans"
        If clsCommon.myLen(StrModuleCode) > 0 AndAlso clsCommon.myLen(StrMasCode) > 0 AndAlso clsCommon.myLen(StrTransCode) > 0 AndAlso clsCommon.myLen(StrReportCode) > 0 Then
            RptQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='" & StrReportCode & "'   order by SNo "
            MasQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='" & StrMasCode & "'   order by SNo "
            TransQry = " select Program_Code As Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from TSPL_PROGRAM_MASTER where Parent_Code='" & StrTransCode & "'   order by SNo "
            LblRptParent.Text = StrReportCode
            LblMaster.Text = StrMasCode
            LblTrans.Text = StrTransCode
        Else
            cbgRpt.DataSource = Nothing
            cbgMasters.DataSource = Nothing
            cbgTrans.DataSource = Nothing
        End If
        If clsCommon.myLen(RptQry) > 0 Then
            cbgRpt.DataSource = clsDBFuncationality.GetDataTable(RptQry)
            cbgRpt.ValueMember = "Code"
            cbgRpt.DisplayMember = "Description"
        End If
        If clsCommon.myLen(MasQry) > 0 Then
            cbgMasters.DataSource = clsDBFuncationality.GetDataTable(MasQry)
            cbgMasters.ValueMember = "Code"
            cbgMasters.DisplayMember = "Description"
        End If
        If clsCommon.myLen(TransQry) > 0 Then
            cbgTrans.DataSource = clsDBFuncationality.GetDataTable(TransQry)
            cbgTrans.ValueMember = "Code"
            cbgTrans.DisplayMember = "Description"
        End If
    End Sub
    Public Function SaveData(ByVal RptParent_Code As String, ByVal MasParentCode As String, ByVal TransParentCode As String) As Boolean
        Dim StrRptName As String = String.Empty
        Dim StrRptNameTotal As String = String.Empty
        Dim StrMasName As String = String.Empty
        Dim StrMasNameTotal As String = String.Empty
        Dim StrTransName As String = String.Empty
        Dim StrTransNameTotal As String = String.Empty
        Dim Act_Rpt As Integer = 0
        Dim Act_Mas As Integer = 0
        Dim Act_Trans As Integer = 0

        '' Report
        If cbgRpt.DataSource IsNot Nothing Then
            If cbgRpt.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one report")
                Return False
                Exit Function
            End If
        Else
            common.clsCommon.MyMessageBoxShow("No data found")
            Return False
            Exit Function
        End If

        '' Masters
        If cbgMasters.DataSource IsNot Nothing Then
            If cbgMasters.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one master")
                Return False
                Exit Function
            End If
        Else
            common.clsCommon.MyMessageBoxShow("No data found")
            Return False
            Exit Function
        End If

        '' Transaction
        If cbgTrans.DataSource IsNot Nothing Then
            If cbgTrans.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one transaction")
                Return False
                Exit Function
            End If
        Else
            common.clsCommon.MyMessageBoxShow("No data found")
            Return False
            Exit Function
        End If

        clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_PROGRAM_MASTER SET VPF_Active_Report=0 WHERE Parent_Code IN ('" & RptParent_Code & "','" & MasParentCode & "','" & TransParentCode & "')")

        '' Report Section
        For Each grow As String In cbgRpt.CheckedValue
            Act_Rpt = 1
            StrRptName = clsCommon.myCstr(grow)
            If clsCommon.myLen(StrRptName) > 0 Then
                StrRptNameTotal = StrRptNameTotal + "," + "'" + StrRptName + "'"
            End If
        Next
        If StrRptNameTotal.Length > 0 Then
            If StrRptNameTotal.Substring(0, 1) = "," Then
                StrRptNameTotal = StrRptNameTotal.Substring(1, StrRptNameTotal.Length - 1)
            End If
        End If

        '' Masters Section
        For Each grow As String In cbgMasters.CheckedValue
            Act_Mas = 1
            StrMasName = clsCommon.myCstr(grow)
            If clsCommon.myLen(StrMasName) > 0 Then
                StrMasNameTotal = StrMasNameTotal + "," + "'" + StrMasName + "'"
            End If
        Next
        If StrMasNameTotal.Length > 0 Then
            If StrMasNameTotal.Substring(0, 1) = "," Then
                StrMasNameTotal = StrMasNameTotal.Substring(1, StrMasNameTotal.Length - 1)
            End If
        End If

        '' Transaction Section
        For Each grow As String In cbgTrans.CheckedValue
            Act_Trans = 1
            StrTransName = clsCommon.myCstr(grow)
            If clsCommon.myLen(StrTransName) > 0 Then
                StrTransNameTotal = StrTransNameTotal + "," + "'" + StrTransName + "'"
            End If
        Next
        If StrTransNameTotal.Length > 0 Then
            If StrTransNameTotal.Substring(0, 1) = "," Then
                StrTransNameTotal = StrTransNameTotal.Substring(1, StrTransNameTotal.Length - 1)
            End If
        End If

        If cbgRpt.CheckedValue.Count >= 0 Then
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_PROGRAM_MASTER SET VPF_Active_Report=1 WHERE Program_Code IN (" + StrRptNameTotal + ")")
        End If
        If cbgMasters.CheckedValue.Count >= 0 Then
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_PROGRAM_MASTER SET VPF_Active_Report=1 WHERE Program_Code IN (" + StrMasNameTotal + ")")
        End If
        If cbgTrans.CheckedValue.Count >= 0 Then
            clsDBFuncationality.ExecuteNonQuery("UPDATE TSPL_PROGRAM_MASTER SET VPF_Active_Report=1 WHERE Program_Code IN (" + StrTransNameTotal + ")")
        End If
        clsCommon.MyMessageBoxShow("Data saved successfully")
    End Function

    Public Sub LoadDataReports()
        Dim ArrRpt As New ArrayList()
        Dim Qry As String = String.Empty

        Qry = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='" & clsCommon.myCstr(LblRptParent.Text) & "' AND VPF_Active_Report=1 order by SNo "

        Dim DT As DataTable = New DataTable()
        DT = clsDBFuncationality.GetDataTable(Qry)

        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            For Each dr As DataRow In DT.Rows
                ArrRpt.Add(clsCommon.myCstr(dr("Code")))
                ArrRpt.Add(clsCommon.myCstr(dr("Name")))
            Next
        End If
        cbgRpt.CheckedValue = ArrRpt
        chkRptAll.IsChecked = True
    End Sub
    Public Sub LoadDataMasters()
        Dim ArrMas As New ArrayList()
        Dim Qry As String = String.Empty

        Qry = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='" & clsCommon.myCstr(LblMaster.Text) & "' AND VPF_Active_Report=1 order by SNo  "

        Dim DT As DataTable = New DataTable()
        DT = clsDBFuncationality.GetDataTable(Qry)

        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            For Each dr As DataRow In DT.Rows
                ArrMas.Add(clsCommon.myCstr(dr("Code")))
                ArrMas.Add(clsCommon.myCstr(dr("Name")))
            Next
        End If
        cbgMasters.CheckedValue = ArrMas
        ChkMasAll.IsChecked = True
    End Sub
    Public Sub LoadDataTransactions()
        Dim ArrTrans As New ArrayList()
        Dim Qry As String = String.Empty

        Qry = "SELECT Program_Code AS Code,case when LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name  FROM TSPL_PROGRAM_MASTER WHERE Parent_Code='" & clsCommon.myCstr(LblTrans.Text) & "' AND VPF_Active_Report=1 order by SNo "

        Dim DT As DataTable = New DataTable()
        DT = clsDBFuncationality.GetDataTable(Qry)

        If (DT IsNot Nothing AndAlso DT.Rows.Count > 0) Then
            For Each dr As DataRow In DT.Rows
                ArrTrans.Add(clsCommon.myCstr(dr("Code")))
                ArrTrans.Add(clsCommon.myCstr(dr("Name")))
            Next
        End If
        cbgTrans.CheckedValue = ArrTrans
        ChkTransAll.IsChecked = True
    End Sub
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If SaveData(LblRptParent.Text, LblMaster.Text, LblTrans.Text) = True Then
            LoadDataReports()
            LoadDataMasters()
            LoadDataTransactions()
        End If
    End Sub

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub chkRptAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkRptAll.ToggleStateChanged
        cbgRpt.Enabled = Not chkRptAll.IsChecked
    End Sub

    Private Sub chkRptSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkRptSelect.ToggleStateChanged
        cbgRpt.Enabled = chkRptSelect.IsChecked
    End Sub

    Private Sub ChkMasAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkMasAll.ToggleStateChanged
        cbgMasters.Enabled = Not ChkMasAll.IsChecked
    End Sub

    Private Sub ChkMasSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkMasSelect.ToggleStateChanged
        cbgMasters.Enabled = ChkMasSelect.IsChecked
    End Sub

    Private Sub FrmVPFActiveReports_Load(sender As Object, e As EventArgs) Handles Me.Load
        chkRptAll.IsChecked = True
        ChkMasAll.IsChecked = True
        ChkTransAll.IsChecked = True
        'Dim Color2 As New ColorDialog
        'Dim color1 As Integer = clsDBFuncationality.getSingleValue("Select marks From test Where id=1")

        'Dim myColor As Color
        'myColor = Color.FromArgb(color1)
        'LblModuleName.BackColor = myColor
    End Sub

    Private Sub ChkTransAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkTransAll.ToggleStateChanged
        cbgTrans.Enabled = Not ChkTransAll.IsChecked
    End Sub

    Private Sub ChkTransSelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkTransSelect.ToggleStateChanged
        cbgTrans.Enabled = ChkTransSelect.IsChecked
    End Sub

    Private Sub btnPhotoBrowse_Click(sender As Object, e As EventArgs) Handles btnPhotoBrowse.Click
        'Me.ColorDialog1.ShowDialog()
        Me.ColorDialog1.AllowFullOpen = True


        Dim Color As New ColorDialog
        'If the user actually selected a color Then
        If Color.ShowDialog() = DialogResult.OK Then
            'Set the background color of the picture box = color selected from the color dialog
            LblModuleName.BackColor = Color.Color
        End If

        Dim myColor As Color = LblModuleName.BackColor  ' ColorDialog1.Color

        ' Create the ColorConverter.
        Dim converter As System.ComponentModel.TypeConverter = _
        System.ComponentModel.TypeDescriptor.GetConverter(myColor)
        clsDBFuncationality.ExecuteNonQuery("Update test Set marks ='" & myColor.ToArgb() & "'")
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class