
''created by richa agarwal 12 july, 2016 against ticket no BM00000009300 
Imports common
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations

Public Class FrmCustomerLocationMapping
    Inherits FrmMainTranScreen

#Region "Variables"

    Dim Is_Load As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Dim IsLoadData As Boolean = False
    Dim isInsideLoadData As Boolean = False
#End Region
#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmCustomerLocationMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        ElseIf e.Alt And e.KeyCode = Keys.S Then
            If allowToSave() Then SaveData()
        End If

    End Sub
    Sub closeForm()
        Me.Close()
    End Sub

    Private Sub FrmCustomerLocationMapping_Load(sender As Object, e As EventArgs) Handles Me.Load
        Is_Load = True
        SetUserMgmtNew()
        funReset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        Is_Load = False
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerLocationMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'If btnSave.Visible = True Then
        '    MenuImport.Enabled = True
        '    MenuExport.Enabled = True
        'Else
        '    MenuImport.Enabled = False
        '    MenuExport.Enabled = False
        'End If
        '--------------------------------------------------
    End Sub

    Sub LoadLocation()
        gvlocation.DataSource = Nothing
        Dim strqry As String = String.Empty
        strqry = "sELECT cast(0 as bit) as Sel,TSPL_LOCATION_MASTER.Location_Code As [Location Code],TSPL_LOCATION_MASTER.Location_Desc as Description FROM TSPL_LOCATION_MASTER where isnull(GIT_Type ,'')<>'Y' "

        gvLocation.DataSource = clsDBFuncationality.GetDataTable(strqry)

        gvLocation.Columns("Sel").HeaderText = " "
        gvLocation.Columns("Sel").Width = 50
        gvLocation.Columns("Sel").ReadOnly = False

        gvLocation.Columns("Location Code").HeaderText = "Location Code"
        gvLocation.Columns("Location Code").Width = 100
        gvLocation.Columns("Location Code").ReadOnly = True

        gvLocation.Columns("Description").HeaderText = "Location Name"
        gvLocation.Columns("Description").Width = 200
        gvLocation.Columns("Description").ReadOnly = True

        gvLocation.AllowAddNewRow = False
        gvLocation.ShowGroupPanel = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = False


    End Sub
    Private Sub funReset()

        LoadLocation()
        LoadCustomer("")
    End Sub
    
  


    Sub LoadCustomer(ByVal LocationCode As String)
        GvCustomer.DataSource = Nothing

        Dim qry As String = "Select Final.* from (sELECT cast(1 as bit) as Sel,TSPL_CUSTOMER_LOCATION_MAPPING.SequenceNo,TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code As [Cust Code],TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Name as Description,TSPL_CUSTOMER_MASTER.Alies_Name as [Alias Name] FROM TSPL_CUSTOMER_LOCATION_MAPPING LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code where TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code ='" & clsCommon.myCstr(LocationCode) & "' " &
        " union all " &
        " SELECT cast(0 as bit) as Sel,0 as SequenceNo,TSPL_CUSTOMER_MASTER.Cust_Code As [Cust Code],TSPL_CUSTOMER_MASTER.Customer_Name as Description,TSPL_CUSTOMER_MASTER.Alies_Name as [Alias Name] FROM TSPL_CUSTOMER_MASTER  where   TSPL_CUSTOMER_MASTER.Cust_Code not in (sELECT TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code FROM TSPL_CUSTOMER_LOCATION_MAPPING where TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code= '" & clsCommon.myCstr(LocationCode) & "')) Final ORDER BY fINAL.[Cust Code] "
        GvCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)

        GvCustomer.Columns("Sel").HeaderText = " "
        GvCustomer.Columns("Sel").Width = 50
        GvCustomer.Columns("Sel").ReadOnly = False

        GvCustomer.Columns("SequenceNo").HeaderText = "Seq No"
        GvCustomer.Columns("SequenceNo").Width = 100
        GvCustomer.Columns("SequenceNo").ReadOnly = False
        GvCustomer.Columns("SequenceNo").FormatString = "{0:n0}"


        GvCustomer.Columns("Cust Code").HeaderText = "Customer Code"
        GvCustomer.Columns("Cust Code").Width = 100
        GvCustomer.Columns("Cust Code").ReadOnly = True

        GvCustomer.Columns("Description").HeaderText = "Customer Name"
        GvCustomer.Columns("Description").Width = 200
        GvCustomer.Columns("Description").ReadOnly = True

        GvCustomer.Columns("Alias Name").HeaderText = "Customer Alias Name"
        GvCustomer.Columns("Alias Name").Width = 200
        GvCustomer.Columns("Alias Name").ReadOnly = True

        GvCustomer.AllowAddNewRow = False
        GvCustomer.ShowGroupPanel = False
        GvCustomer.AllowColumnReorder = False
        GvCustomer.AllowRowReorder = False
        GvCustomer.EnableSorting = False
        GvCustomer.Enabled = True
        GvCustomer.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvCustomer.MasterTemplate.ShowRowHeaderColumn = False
    End Sub


    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        If allowToSave() Then SaveData()

    End Sub

    Function allowToSave() As Boolean

        btnsave.Focus()
        Dim dblsequenceno As Double = 0
        Dim dblsequencenoInter As Double = 0

        Dim strcount As Integer = 0
        For ii As Integer = 0 To gvLocation.Rows.Count - 1
            If clsCommon.myCBool(gvLocation.Rows(ii).Cells("Sel").Value) Then
                strcount = strcount + 1
            End If
        Next
        If strcount > 1 Then
            clsCommon.MyMessageBoxShow("Select only one location at a time.")
            Return False
        End If


        'For i As Integer = 0 To GvCustomer.Rows.Count - 1
        '    dblsequenceno = clsCommon.myCdbl(GvCustomer.Rows(i).Cells("SequenceNo").Value)
        '    If dblsequenceno <> 0 Then
        '        For j As Integer = i + 1 To GvCustomer.Rows.Count - 1
        '            dblsequencenoInter = clsCommon.myCdbl(GvCustomer.Rows(j).Cells("SequenceNo").Value)
        '            If dblsequenceno = dblsequencenoInter Then
        '                clsCommon.MyMessageBoxShow("Sequence no should not be same for two customers.")
        '                Return False
        '            End If
        '        Next
        '    End If
        'Next
        Return True
    End Function

    Sub SaveData()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.CustomerLocationMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim arr As New List(Of clsLocationCustomerMapping)
        Dim obj As clsLocation = New clsLocation()
        Try

            For ii As Integer = 0 To gvLocation.Rows.Count - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("Sel").Value) Then
                    obj.Location_Code = clsCommon.myCstr(gvLocation.Rows(ii).Cells("Location Code").Value)
                    Exit For
                End If
            Next

            For ii As Integer = 0 To GvCustomer.Rows.Count - 1
                If clsCommon.myCBool(GvCustomer.Rows(ii).Cells("Sel").Value) Then
                    Dim objTr As New clsLocationCustomerMapping
                    objTr.Location_Name = clsLocation.GetName(obj.Location_Code, Nothing)
                    objTr.Customer_Code = clsCommon.myCstr(GvCustomer.Rows(ii).Cells("Cust Code").Value)
                    objTr.Customer_Name = clsCommon.myCstr(GvCustomer.Rows(ii).Cells("Description").Value)
                    objTr.SequenceNo = clsCommon.myCdbl(GvCustomer.Rows(ii).Cells("SequenceNo").Value)
                    obj.ArrLocCustMap.Add(objTr)
                End If
            Next


            If clsLocationCustomerMapping.SaveData(obj, True) Then
                myMessages.insert()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        closeForm()
    End Sub

    Private Sub gvLocation_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If e.Column Is gvLocation.Columns("Sel") Then
                    Dim strcount As Integer = 0
                    Dim strLocation As String = String.Empty
                    For ii As Integer = 0 To gvLocation.Rows.Count - 1
                        If clsCommon.myCBool(gvLocation.Rows(ii).Cells("Sel").Value) Then
                            strLocation = clsCommon.myCstr(gvLocation.Rows(ii).Cells("Location Code").Value)
                            strcount = strcount + 1
                        End If
                    Next
                    If strcount > 1 Then
                        isInsideLoadData = True
                        gvLocation.CurrentRow.Cells("Sel").Value = False
                        Throw New Exception("Select only one location at a time.")
                    Else
                        LoadCustomer(strLocation)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
      
    End Sub

    Private Sub chkCustomerAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        Try
            IsLoadData = True
            For Each row As GridViewRowInfo In GvCustomer.Rows
                row.Cells(0).Value = chkCustomerAll.IsChecked
            Next
        Catch ex As Exception
        Finally
            IsLoadData = False
        End Try
    End Sub

   
    Private Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Location Code", "Location Name", "Sequence No", "Customer Code", "Customer Name") Then
            Dim linno As Integer = 1
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()

                Dim strLocationCode As String = String.Empty
                Dim strLocationName As String = String.Empty
                Dim strCustomerCode As String = String.Empty
                Dim strCustomerName As String = String.Empty
                Dim dblSequenceNo As Double = 0

                For Each grow As GridViewRowInfo In gv.Rows

                    strLocationCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    strLocationName = clsCommon.myCstr(grow.Cells("Location Name").Value)
                    dblSequenceNo = clsCommon.myCdbl(grow.Cells("Sequence No").Value)
                    strCustomerCode = clsCommon.myCstr(grow.Cells("Customer Code").Value)
                    strCustomerName = clsCommon.myCstr(grow.Cells("Customer Name").Value)

                    linno += 1

                    If (String.IsNullOrEmpty(strLocationCode)) Or clsCommon.myLen(strLocationCode) > 12 Then
                        Throw New Exception("Length of Location Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If (String.IsNullOrEmpty(strLocationName)) Or clsCommon.myLen(strLocationName) > 50 Then
                        Throw New Exception("Length of Location Name should be max. 50 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code ='" & strLocationCode & "' ", trans)) <= 0 Then
                        Throw New Exception("Location Code " & strLocationCode & " is not exist in Location Master At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code ='" & strLocationCode & "' and isnull(GIT_Type,'N')='Y' ", trans)) > 0 Then
                        Throw New Exception("Cannot import Location Code " & strLocationCode & " is of GIT Type At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_LOCATION_MASTER where Location_Code ='" & strLocationCode & "' and Location_Desc='" & strLocationName & "'", trans)) <= 0 Then
                        Throw New Exception("Location Name is incorrect for Location Code " & strLocationCode & " At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If Not IsNumeric(dblSequenceNo) Then
                        Throw New Exception("Sequence No should be numeric At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(dblSequenceNo) < 0 Then
                        Throw New Exception("Sequence No should not be in (-)ve At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(dblSequenceNo) > 0 Then
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_LOCATION_MAPPING where Location_Code ='" & strLocationCode & "' and SequenceNo=" & dblSequenceNo & " and Customer_Code<>'" & strCustomerCode & "'", trans)) > 0 Then
                            Throw New Exception("Please provide different Sequence No for Customer " & strCustomerCode & " and Location Code " & strLocationCode & " At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If (String.IsNullOrEmpty(strCustomerCode)) Or clsCommon.myLen(strCustomerCode) > 12 Then
                        Throw New Exception("Length of Customer Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If (String.IsNullOrEmpty(strCustomerName)) Or clsCommon.myLen(strCustomerName) > 200 Then
                        Throw New Exception("Length of Customer Name should be max. 200 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & strCustomerCode & "' and Customer_Name='" & strCustomerName & "'", trans)) <= 0 Then
                        Throw New Exception("Customer Code " & strCustomerCode & " is is not exist in Customer Master At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_CUSTOMER_MASTER where Cust_Code ='" & strCustomerCode & "' and Customer_Name='" & strCustomerName & "'", trans)) <= 0 Then
                        Throw New Exception("Customer Name is incorrect for Customer Code " & strCustomerCode & " At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If


                    If clsCommon.myLen(strLocationCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_CUSTOMER_LOCATION_MAPPING where Location_Code ='" & strLocationCode & "' and Customer_Code ='" & strCustomerCode & "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True
                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Location_Code", strLocationCode)
                    clsCommon.AddColumnsForChange(coll, "Location_Name", strLocationName)
                    clsCommon.AddColumnsForChange(coll, "SequenceNo", dblSequenceNo)
                    clsCommon.AddColumnsForChange(coll, "Customer_Code", strCustomerCode)
                    clsCommon.AddColumnsForChange(coll, "Customer_Name", strCustomerName)
                    If IsNewEntry Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_LOCATION_MAPPING", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_CUSTOMER_LOCATION_MAPPING", OMInsertOrUpdate.Update, "TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code='" & strLocationCode & "' and  TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code ='" & strCustomerCode & "' ", trans)
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim str As String = String.Empty
        ' str = "Select Location_Code as [Location Code] ,Location_Name  as [Location Name],SequenceNo as [Sequence No] ,Customer_Code as [Customer Code] ,Customer_Name as [Customer Name]  from TSPL_CUSTOMER_LOCATION_MAPPING"
        str = " Select TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code as [Location Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_CUSTOMER_LOCATION_MAPPING.SequenceNo as [Sequence No] ,TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code as [Customer Code] ,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name]  from TSPL_CUSTOMER_LOCATION_MAPPING Left Outer Join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_CUSTOMER_LOCATION_MAPPING.Location_Code Left Outer Join TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code  =TSPL_CUSTOMER_LOCATION_MAPPING.Customer_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            transportSql.ExporttoExcel(str, Me)
        Else
            transportSql.ExporttoExcel("Select '' as [Location Code] ,''  as [Location Name],0 as [Sequence No] ,'' as [Customer Code] ,'' as [Customer Name]", Me)
        End If
        str = Nothing
    End Sub
End Class