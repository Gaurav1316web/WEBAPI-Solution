Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports System.Data.SqlClient

Public Class frmChangeSerialNumber
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrLocation As ArrayList
    Public arrItem As ArrayList
    Public arrCategory As ArrayList
    Public arrSubCategory As ArrayList
#End Region

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value
        SetUserMgmtNew()
        LoadType()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S Save/Update Data")

    End Sub

    Sub LoadType()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable("select distinct Document_Type  from TSPL_SERIAL_ITEM order by Document_Type")
        dt.Columns.Add("Code", GetType(String))
        cboDocumentType.DataSource = dt
        cboDocumentType.ValueMember = "Document_Type"
        cboDocumentType.DisplayMember = "Document_Type"
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ChangeItemSerialNumber)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefersh.Click
        Try
            gv1.EnableFiltering = True
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadData()
        Dim qry As String = "select Document_Type,Document_Code,Document_Date,TSPL_SERIAL_ITEM.Item_Code, TSPL_ITEM_MASTER.Item_Desc,Auto_Sr_No,'' as NewSerialNumber"
        qry += " from TSPL_SERIAL_ITEM "
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_SERIAL_ITEM.Item_Code"
        qry += " where Against_Inv_Movement_Trans_Id is not null "
        If clsCommon.myLen(cboDocumentType.SelectedValue) > 0 Then
            qry += " and Document_Type='" + clsCommon.myCstr(cboDocumentType.SelectedValue) + "' "
        End If
        qry += " and Document_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' "
        qry += " order by Parent_Line_No,Line_No "

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.ShowFilteringRow = True
        gv1.ShowGroupPanel = False
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
        End If

        gv1.DataSource = dt
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next

        gv1.Columns("Document_Type").Width = 100
        gv1.Columns("Document_Type").IsVisible = True
        gv1.Columns("Document_Type").HeaderText = "Document Type"

        gv1.Columns("Document_Code").IsVisible = True
        gv1.Columns("Document_Code").Width = 150
        gv1.Columns("Document_Code").HeaderText = "Document No"

        gv1.Columns("Document_Date").IsVisible = True
        gv1.Columns("Document_Date").Width = 150
        gv1.Columns("Document_Date").HeaderText = "Document Date"

        gv1.Columns("Item_Code").IsVisible = True
        gv1.Columns("Item_Code").Width = 100
        gv1.Columns("Item_Code").HeaderText = "Item Code"

        gv1.Columns("Item_Desc").IsVisible = True
        gv1.Columns("Item_Desc").Width = 150
        gv1.Columns("Item_Desc").HeaderText = "Item Description"

        gv1.Columns("Auto_Sr_No").IsVisible = True
        gv1.Columns("Auto_Sr_No").Width = 150
        gv1.Columns("Auto_Sr_No").HeaderText = "Serial Number"

        gv1.Columns("NewSerialNumber").IsVisible = True
        gv1.Columns("NewSerialNumber").Width = 150
        gv1.Columns("NewSerialNumber").HeaderText = "New Serial Number"
        gv1.Columns("NewSerialNumber").ReadOnly = False
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Dim strInvColumns As String = clsERPFuncationality.GetTableColumnNameForQry("TSPL_SERIAL_ITEM", Nothing)
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim isFound As Boolean = False
            For ii As Integer = 0 To gv1.RowCount - 1
                Dim strNewSrNumber As String = clsCommon.myCstr(gv1.Rows(ii).Cells("NewSerialNumber").Value)
                If clsCommon.myLen(strNewSrNumber) > 0 Then
                    isFound = True
                    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Item_Code").Value)
                    Dim strSrNumber As String = clsCommon.myCstr(gv1.Rows(ii).Cells("Auto_Sr_No").Value)
                    Dim qry As String = "select Code from TSPL_SERIAL_ITEM where Item_Code='" + strICode + "' and Auto_Sr_No='" + strSrNumber + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Dim strCode As String = clsCommon.myCstr(dr("Code"))
                            qry = "INSERT INTO TSPL_SERIAL_ITEM_HIST(" + strInvColumns + ",Hist_By,Hist_Date) SELECT " + strInvColumns + ",'" + objCommonVar.CurrentUserCode + "','" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy hh:mm tt") + "' FROM TSPL_SERIAL_ITEM WHERE Code='" + strCode + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)

                            qry = "Update TSPL_SERIAL_ITEM set Auto_Sr_No='" + strNewSrNumber + "' where Code='" + strCode + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                        Next
                    End If
                End If
            Next
            If Not isFound Then
                Throw New Exception("No Data found to save")
            Else
                trans.Commit()
                clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                LoadData()
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
