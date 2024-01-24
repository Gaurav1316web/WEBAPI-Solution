''created by Preeti Gupta
Imports common
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Public Class RptLocationItemMappingDS
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

    Private Sub RptLocationItemMappingDS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            closeForm()
        ElseIf e.Alt And e.KeyCode = Keys.S Then
            If allowToSave() Then SaveData()
        End If
    End Sub
    Sub closeForm()
        Me.Close()
    End Sub

    Private Sub RptLocationItemMappingDS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Loaditem("")
    End Sub




    Sub LoadItem(ByVal LocationCode As String)
        GvItem.DataSource = Nothing
        Dim qry As String = " Select Final.* from (sELECT cast(1 as bit) as Sel,TSPL_Location_ItemMAPPING.SequenceNo,TSPL_Location_ItemMAPPING.Item_code  As [Item Code],TSPL_Location_ItemMAPPING.Item_Name  as Description FROM TSPL_Location_ItemMAPPING where TSPL_Location_ItemMAPPING.Location_Code='" & clsCommon.myCstr(LocationCode) & "' " & _
        " union all " & _
        "  SELECT cast(0 as bit) as Sel,0 as SequenceNo,TSPL_ITEM_MASTER .Item_Code  As [Item Code],TSPL_ITEM_MASTER.Item_Desc  as Description FROM TSPL_ITEM_MASTER  where   TSPL_ITEM_MASTER.Item_Code  not in (sELECT TSPL_Location_ItemMAPPING.Item_Code  FROM TSPL_Location_ItemMAPPING where TSPL_Location_ItemMAPPING.Location_Code= '" & clsCommon.myCstr(LocationCode) & "'))  Final ORDER BY fINAL.[Item Code]   "
        GvItem.DataSource = clsDBFuncationality.GetDataTable(qry)

        GvItem.Columns("Sel").HeaderText = " "
        GvItem.Columns("Sel").Width = 50
        GvItem.Columns("Sel").ReadOnly = False

        GvItem.Columns("SequenceNo").HeaderText = "Seq No"
        GvItem.Columns("SequenceNo").Width = 100
        GvItem.Columns("SequenceNo").ReadOnly = False
        GvItem.Columns("SequenceNo").FormatString = "{0:n0}"


        GvItem.Columns("Item Code").HeaderText = "Item Code"
        GvItem.Columns("Item Code").Width = 100
        GvItem.Columns("Item Code").ReadOnly = True

        GvItem.Columns("Description").HeaderText = "Item Name"
        GvItem.Columns("Description").Width = 200
        GvItem.Columns("Description").ReadOnly = True

        GvItem.AllowAddNewRow = False
        GvItem.ShowGroupPanel = False
        GvItem.AllowColumnReorder = False
        GvItem.AllowRowReorder = False
        GvItem.EnableSorting = False
        GvItem.Enabled = True
        GvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        GvItem.MasterTemplate.ShowRowHeaderColumn = False
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
            clsCommon.MyMessageBoxShow(Me, "Select only one location at a time.", Me.Text)
            Return False
        End If


        For i As Integer = 0 To GvItem.Rows.Count - 1
            dblsequenceno = clsCommon.myCdbl(GvItem.Rows(i).Cells("SequenceNo").Value)
            If dblsequenceno <> 0 Then
                For j As Integer = i + 1 To GvItem.Rows.Count - 1
                    dblsequencenoInter = clsCommon.myCdbl(GvItem.Rows(j).Cells("SequenceNo").Value)
                    If dblsequenceno = dblsequencenoInter Then
                        clsCommon.MyMessageBoxShow(Me, "Sequence no should not be same for two customers.", Me.Text)
                        Return False
                    End If
                Next
            End If
        Next
        Return True
    End Function

    Sub SaveData()
        Dim arr As New List(Of clsLocationItemMapping)
        Dim obj As clsLocation = New clsLocation()
        Try

            For ii As Integer = 0 To gvLocation.Rows.Count - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("Sel").Value) Then
                    obj.Location_Code = clsCommon.myCstr(gvLocation.Rows(ii).Cells("Location Code").Value)
                    Exit For
                End If
            Next

            For ii As Integer = 0 To GvItem.Rows.Count - 1
                If clsCommon.myCBool(GvItem.Rows(ii).Cells("Sel").Value) Then
                    Dim objTr As New clsLocationItemMapping
                    objTr.Location_Name = clsLocation.GetName(obj.Location_Code, Nothing)
                    objTr.Item_Code = clsCommon.myCstr(GvItem.Rows(ii).Cells("Item Code").Value)
                    objTr.Item_Name = clsCommon.myCstr(GvItem.Rows(ii).Cells("Description").Value)
                    objTr.SequenceNo = clsCommon.myCdbl(GvItem.Rows(ii).Cells("SequenceNo").Value)
                    obj.ArrLocItemMap.Add(objTr)
                End If
            Next


            If clsLocationItemMapping.SaveData(obj, True) Then
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
                        LoadItem(strLocation)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub chkItemAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkItemAll.ToggleStateChanged
        Try
            IsLoadData = True
            For Each row As GridViewRowInfo In GvItem.Rows
                row.Cells(0).Value = chkItemAll.IsChecked
            Next
        Catch ex As Exception
        Finally
            IsLoadData = False
        End Try
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Location Code", "Location Name", "Sequence No", "Item Code", "Item Name") Then
            Dim linno As Integer = 1
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()

                Dim strLocationCode As String = String.Empty
                Dim strLocationName As String = String.Empty
                Dim strItemCode As String = String.Empty
                Dim strItemName As String = String.Empty
                Dim dblSequenceNo As Double = 0

                For Each grow As GridViewRowInfo In gv.Rows

                    strLocationCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    strLocationName = clsCommon.myCstr(grow.Cells("Location Name").Value)
                    dblSequenceNo = clsCommon.myCdbl(grow.Cells("Sequence No").Value)
                    strItemCode = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    strItemName = clsCommon.myCstr(grow.Cells("Item Name").Value)

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
                        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_Location_ItemMAPPING where Location_Code ='" & strLocationCode & "' and SequenceNo=" & dblSequenceNo & " and Item_Code<>'" & strItemCode & "'", trans)) > 0 Then
                            Throw New Exception("Please provide different Sequence No for Item " & strItemCode & " and Location Code " & strLocationCode & " At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    If (String.IsNullOrEmpty(strItemCode)) Or clsCommon.myLen(strItemCode) > 12 Then
                        Throw New Exception("Length of Item Code should be max. 12 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If (String.IsNullOrEmpty(strItemName)) Or clsCommon.myLen(strItemName) > 200 Then
                        Throw New Exception("Length of Customer Name should be max. 200 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from Tspl_Item_Master where Item_Code ='" & strItemCode & "' and Item_Desc='" & strItemName & "'", trans)) <= 0 Then
                        Throw New Exception("Item Code " & strItemCode & " is is not exist in Item Master At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from Tspl_Item_Master where Item_Code ='" & strItemCode & "' and Item_Desc='" & strItemName & "'", trans)) <= 0 Then
                        Throw New Exception("Item Name is incorrect for Item Code " & strItemCode & " At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If


                    If clsCommon.myLen(strLocationCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Location_ItemMAPPING where Location_Code ='" & strLocationCode & "' and Item_Code ='" & strItemCode & "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True
                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Location_Code", strLocationCode)
                    clsCommon.AddColumnsForChange(coll, "Location_Name", strLocationName)
                    clsCommon.AddColumnsForChange(coll, "SequenceNo", dblSequenceNo)
                    clsCommon.AddColumnsForChange(coll, "Item_Code", strItemCode)
                    clsCommon.AddColumnsForChange(coll, "Item_Name", strItemName)
                    If IsNewEntry Then
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Location_ItemMAPPING", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Location_ItemMAPPING", OMInsertOrUpdate.Update, "TSPL_Location_ItemMAPPING.Location_Code='" & strLocationCode & "' and  TSPL_Location_ItemMAPPING.Item_Code ='" & strItemCode & "' ", trans)
                    End If
                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String = String.Empty
        ' str = "Select Location_Code as [Location Code] ,Location_Name  as [Location Name],SequenceNo as [Sequence No] ,Customer_Code as [Customer Code] ,Customer_Name as [Customer Name]  from TSPL_CUSTOMER_LOCATION_MAPPING"
        str = " Select TSPL_Location_ItemMAPPING.Location_Code as [Location Code] ,TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_Location_ItemMAPPING.SequenceNo as [Sequence No] ,TSPL_Location_ItemMAPPING.Item_Code as [Item Code] ,TSPL_Item_master.Item_Desc as [Item Name]  from TSPL_Location_ItemMAPPING Left Outer Join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code =TSPL_Location_ItemMAPPING.Location_Code Left Outer Join TSPL_Item_MASTER ON TSPL_Item_MASTER.Item_Code  =TSPL_Location_ItemMAPPING.Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            transportSql.ExporttoExcel(str, Me)
        Else
            transportSql.ExporttoExcel("Select '' as [Location Code] ,''  as [Location Name],0 as [Sequence No] ,'' as [Customer Code] ,'' as [Customer Name]", Me)
        End If
        str = Nothing
    End Sub
End Class
