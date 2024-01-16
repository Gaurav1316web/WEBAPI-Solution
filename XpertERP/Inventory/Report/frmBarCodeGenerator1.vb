'-Created By {Pankaj Kumar Chaudhary]--Against Ticket No--[]
Imports common
Imports System.Data.SqlClient

Public Class FrmBarCodeGenerator1
    Inherits FrmMainTranScreen
    Dim Qry As String = ""
    Dim IsLoaddata As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colSelect As String = "Select"
    Const colItemCode As String = "ItemCode"

    Private Sub FrmBarCodeGenerator1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        chkShowBarCode.Checked = True
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBarCodeGenerator1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnPrint.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub Reset()
        LoadBlankGrid()
        txtItemCode.Value = ""
        lblIName.Text = ""
    End Sub

    Sub LoadBlankGrid()
        dgvItem.Rows.Clear()
        dgvItem.Columns.Clear()

        Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 60
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvItem.MasterTemplate.Columns.Add(LineNo)

        Dim check As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        check.FormatString = ""
        check.HeaderText = "Select"
        check.Name = colSelect
        check.Width = 100
        check.ReadOnly = False
        check.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        dgvItem.MasterTemplate.Columns.Add(check)

        Dim BarCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BarCode.FormatString = ""
        BarCode.HeaderText = "Serial No"
        BarCode.Name = colItemCode
        BarCode.Width = 300
        BarCode.ReadOnly = True
        BarCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvItem.MasterTemplate.Columns.Add(BarCode)

        dgvItem.AllowDeleteRow = False
        dgvItem.AllowAddNewRow = False
        dgvItem.ShowGroupPanel = False
        dgvItem.AllowColumnReorder = False
        dgvItem.AllowRowReorder = False
        dgvItem.EnableSorting = False
        dgvItem.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvItem.MasterTemplate.ShowRowHeaderColumn = False

        btnSelect.Text = "Select All"
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintBarcode()
    End Sub

    Private Sub PrintBarcode()
        Dim BarCode As String
        Dim dtBarCode As New DataTable
        dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
        dtBarCode.Columns.Add("BarCode", GetType(String))
        dtBarCode.Columns.Add("ItemDesc", GetType(String))
        dtBarCode.Columns.Add("MRP", GetType(Double))
        Dim dr As DataRow
        For Each grow As GridViewRowInfo In dgvItem.Rows
            If grow.Cells(colSelect).Value = True Then
                BarCode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                Dim bytes() As Byte
                Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(BarCode, 1, False).[GetType]())
                bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(BarCode, 1, False), GetType(Byte())), Byte())
                dr = dtBarCode.NewRow()
                dr("BarCodeImage") = bytes
                If chkShowBarCode.Checked = True Then
                    dr("BarCode") = BarCode
                Else
                    dr("BarCode") = ""
                End If
                dr("ItemDesc") = ""
                dr("MRP") = 0.0
                dtBarCode.Rows.Add(dr)
            End If
        Next
        If dtBarCode.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dtBarCode, "crptBarCodeImages", "BarCode")
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub txtItemCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtItemCode._MYValidating
        Try
            Qry = "Select Item_Code as Code, Item_Desc as Description, * from TSPL_ITEM_MASTER"
            txtItemCode.Value = clsCommon.ShowSelectForm("fndItem@BarCode", Qry, "Code", "Item_Type <> 'F'", txtItemCode.Value, "Code", isButtonClicked)
            lblIName.Text = clsItemMaster.GetItemName(txtItemCode.Value, Nothing)
            LoadSerialNo(txtItemCode.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadSerialNo(ByVal strItemCode As String)
        Try
            LoadBlankGrid()
            Dim SNo As Integer = 0
            Qry = "Select DISTINCT Auto_Sr_No from TSPL_SERIAL_ITEM WHERE Item_Code='" + strItemCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            For Each dr As DataRow In dt.Rows
                SNo += 1
                dgvItem.Rows.AddNew()
                dgvItem.CurrentRow.Cells(colLineNo).Value = SNo
                dgvItem.CurrentRow.Cells(colSelect).Value = False
                dgvItem.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(dr("Auto_Sr_No"))
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If btnSelect.Text = "Select All" Then
                For Each grow As GridViewRowInfo In dgvItem.Rows
                    grow.Cells(colSelect).Value = True
                Next
                btnSelect.Text = "UnSelect All"
            Else
                For Each grow As GridViewRowInfo In dgvItem.Rows
                    grow.Cells(colSelect).Value = False
                Next
                btnSelect.Text = "Select All"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

