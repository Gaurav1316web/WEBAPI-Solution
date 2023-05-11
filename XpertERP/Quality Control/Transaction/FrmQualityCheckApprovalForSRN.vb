'created by Monika
Imports common
Imports System.Data.SqlClient


Public Class FrmQualityCheckApprovalForSRN
    Inherits FrmMainTranScreen

#Region "vriables"
    Dim FORMTYPE As String = Nothing
    Dim QC_Type As String = Nothing
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim ButtonToolTip As New ToolTip()

    Const colSelect As String = "Select"
    Const colLineno As String = "Lineno"
    Const colQCNo As String = "QCNo"
    Const colQCDate As String = "QCDate"
    Const colQCDesc As String = "QCDesc"
    Const colVendorCode As String = "Vcode"
    Const colVendorName As String = "Vname"
    Const colBillToLoction As String = "BillLocation"
    Const colBillToLoctionName As String = "BillLocationName"
    Const colQCStatus As String = "QCStatus"
    Const colSRNType As String = "SRNType"
    Const colItemType As String = "ItemType"

    Dim VendorCode As String = ""
    Dim SRNType As String = ""
#End Region

#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String, ByVal QCType As String)
        InitializeComponent()
        FORMTYPE = formid
        QC_Type = QCType
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub Funreset()
        LoadData()
    End Sub

    Private Sub FrmQualityCheckApprovalForSRN_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
                btnsave.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                clsERPFuncationality.closeForm(Me)
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
                Funreset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isCellValueChanged = False
        End Try
    End Sub

    Private Sub FrmQualityCheckApprovalForSRN_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        Funreset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+S for reset window.")

        If clsCommon.CompairString(QC_Type, "Incoming") = CompairStringResult.Equal Then
            MyLabel4.Text = "INCOMING QUALITY CHECK APPROVAL"
        ElseIf clsCommon.CompairString(QC_Type, "Outgoing") = CompairStringResult.Equal Then
            MyLabel4.Text = "OUTGOING QUALITY CHECK APPROVAL"
        ElseIf clsCommon.CompairString(QC_Type, "InProcess") = CompairStringResult.Equal Then
            MyLabel4.Text = "IN-PROCESS QUALITY CHECK APPROVAL"
        End If
    End Sub

    Private Sub LoadBlankGrid()
        gv.Columns.Clear()
        gv.Rows.Clear()

        Dim repoStr As New GridViewTextBoxColumn()
        Dim repochk As New GridViewCheckBoxColumn()

        repochk = New GridViewCheckBoxColumn()
        repochk.FormatString = ""
        repochk.HeaderText = "Select"
        repochk.Name = colSelect
        repochk.Width = 70
        repochk.ThreeState = False
        gv.MasterTemplate.Columns.Add(repochk)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "S.No."
        repoStr.Name = colLineno
        repoStr.Width = 70
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "QC No."
        repoStr.Name = colQCNo
        repoStr.Width = 130
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "QC Date"
        repoStr.Name = colQCDate
        repoStr.Width = 110
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Description"
        repoStr.Name = colQCDesc
        repoStr.Width = 230
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Vendor Code"
        repoStr.Name = colVendorCode
        repoStr.Width = 110
        repoStr.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Vendor Name"
        repoStr.Name = colVendorName
        repoStr.Width = 230
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Bill To Location"
        repoStr.Name = colBillToLoction
        repoStr.Width = 100
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Location Name"
        repoStr.Name = colBillToLoctionName
        repoStr.Width = 180
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "QC Status"
        repoStr.Name = colQCStatus
        repoStr.Width = 130
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Document Type"
        repoStr.Name = colSRNType
        repoStr.Width = 130
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        repoStr = New GridViewTextBoxColumn()
        repoStr.FormatString = ""
        repoStr.HeaderText = "Item Type"
        repoStr.Name = colItemType
        repoStr.Width = 130
        repoStr.ReadOnly = True
        repoStr.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoStr)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40

        repoStr = Nothing
    End Sub

    Private Sub LoadData()
        Dim obj As New clsQualityCheckApprovalForSRN()
        Try
            gv.Rows.Clear()

            VendorCode = ""
            SRNType = ""

            obj = clsQualityCheckApprovalForSRN.GetData()
            isInsideLoadData = False

            If obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                isInsideLoadData = True
                For Each objtr As clsQualityCheckApprovalForSRN In obj.Arr
                    gv.Rows.AddNew()

                    gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = False
                    gv.Rows(gv.Rows.Count - 1).Cells(colLineno).Value = gv.Rows.Count
                    gv.Rows(gv.Rows.Count - 1).Cells(colQCNo).Value = objtr.Document_Code
                    gv.Rows(gv.Rows.Count - 1).Cells(colQCDate).Value = objtr.Document_Date
                    gv.Rows(gv.Rows.Count - 1).Cells(colQCDesc).Value = objtr.Description
                    gv.Rows(gv.Rows.Count - 1).Cells(colQCStatus).Value = objtr.QC_Status
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorCode).Value = clsCommon.myCstr(objtr.Vendor_Code)
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorName).Value = objtr.Vendor_Name
                    gv.Rows(gv.Rows.Count - 1).Cells(colSRNType).Value = clsQualityCheckForSRNHead.FullNameOfPurchaseOrderType(objtr.SRN_Type)
                    gv.Rows(gv.Rows.Count - 1).Cells(colBillToLoction).Value = objtr.Bill_To_Location
                    gv.Rows(gv.Rows.Count - 1).Cells(colBillToLoctionName).Value = clsLocation.GetName(objtr.Bill_To_Location, Nothing)
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = clsQualityCheckForSRNHead.FullNameOfItemType(objtr.Item_Type)
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            obj = Nothing
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            Dim VCode As String = ""
            Dim SRNType As String = ""
            Dim itemtype As String = ""
            Dim OldVCode As String = ""
            Dim OldSRNType As String = ""
            Dim Olditemtype As String = ""
            Dim counter As Integer = 0

            For Each grow As GridViewRowInfo In gv.Rows
                VCode = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                SRNType = clsCommon.myCstr(grow.Cells(colSRNType).Value)
                itemtype = clsCommon.myCstr(grow.Cells(colItemType).Value)

                If clsCommon.myLen(VCode) > 0 AndAlso clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                    counter += 1

                    For ii As Integer = grow.Index + 1 To gv.Rows.Count - 1
                        OldVCode = clsCommon.myCstr(gv.Rows(ii).Cells(colVendorCode).Value)
                        OldSRNType = clsCommon.myCstr(gv.Rows(ii).Cells(colSRNType).Value)
                        Olditemtype = clsCommon.myCstr(gv.Rows(ii).Cells(colItemType).Value)

                        If clsCommon.myLen(VCode) > 0 AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colSelect).Value) = True AndAlso clsCommon.CompairString(VCode, OldVCode) <> CompairStringResult.Equal Then
                            gv.CurrentRow = gv.Rows(ii)
                            Throw New Exception("Selected document is not of same vendor i.e (" + clsCommon.myCstr(grow.Cells(colVendorName).Value) + ").")
                        End If
                        If clsCommon.myLen(SRNType) > 0 AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colSelect).Value) = True AndAlso clsCommon.CompairString(SRNType, OldSRNType) <> CompairStringResult.Equal Then
                            gv.CurrentRow = gv.Rows(ii)
                            Throw New Exception("Selected document is not of same type i.e (" + SRNType + ").")
                        End If
                        If clsCommon.myLen(itemtype) > 0 AndAlso clsCommon.myCBool(gv.Rows(ii).Cells(colSelect).Value) = True AndAlso clsCommon.CompairString(itemtype, Olditemtype) <> CompairStringResult.Equal Then
                            gv.CurrentRow = gv.Rows(ii)
                            Throw New Exception("Selected document is not of same item type i.e (" + itemtype + ").")
                        End If
                    Next
                End If
            Next

            If counter <= 0 Then
                gv.CurrentRow = gv.Rows(0)
                Throw New Exception("Select atleast one document for approval.")
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub SaveData()
        Dim obj As New clsQualityCheckApprovalForSRN()
        Dim objtr As New clsQualityCheckApprovalForSRN()
        Try
            If AllowToSave() Then
                obj = New clsQualityCheckApprovalForSRN()
                obj.Arr = New List(Of clsQualityCheckApprovalForSRN)

                For Each grow As GridViewRowInfo In gv.Rows
                    objtr = New clsQualityCheckApprovalForSRN()

                    objtr.Line_No = CInt(clsCommon.myCdbl(grow.Cells(colLineno).Value))
                    objtr.Document_Code = clsCommon.myCstr(grow.Cells(colQCNo).Value)
                    objtr.Document_Date = clsCommon.myCDate(grow.Cells(colQCDate).Value)
                    objtr.Description = clsCommon.myCstr(grow.Cells(colQCDesc).Value).Replace("'", "`")
                    objtr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                    objtr.Bill_To_Location = clsCommon.myCstr(grow.Cells(colBillToLoction).Value)
                    objtr.QC_Status = clsCommon.myCstr(grow.Cells(colQCStatus).Value)
                    objtr.SRN_Type = clsQualityCheckForSRNHead.CodeOfPurchaseOrderType(clsCommon.myCstr(grow.Cells(colSRNType).Value))
                    objtr.Item_Type = clsQualityCheckForSRNHead.CodeOfItemType(clsCommon.myCstr(grow.Cells(colItemType).Value))

                    If clsCommon.myLen(objtr.Document_Code) > 0 AndAlso clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then ''if selected for approval then go to save
                        obj.Arr.Add(objtr)
                    End If
                Next

                If obj.Arr Is Nothing OrElse obj.Arr.Count <= 0 Then
                    Throw New Exception("Select atleast one record for approval.")
                End If


                If clsQualityCheckApprovalForSRN.SaveData(obj) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully.")

                    LoadData()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            objtr = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Funreset()
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try
            If e.Column IsNot Nothing Then
                If clsCommon.myLen(gv.CurrentRow.Cells(colQCNo).Value) > 0 Then
                    Dim frm As New FrmQualityCheckForSRN()
                    frm.SetUserMgmt(clsUserMgtCode.frmQualityCheckForSRN)
                    frm.strDocumentCode = clsCommon.myCstr(gv.CurrentRow.Cells(colQCNo).Value)
                    frm.FORMTYPE = clsUserMgtCode.frmQualityCheckForSRN
                    frm.QC_Type = QC_Type
                    frm.ShowDialog()
                End If
                
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_ValueChanging(sender As Object, e As ValueChangingEventArgs) Handles gv.ValueChanging
        Try
            If gv.CurrentRow.Index < 0 Then
                Exit Sub
            End If
            If isInsideLoadData Then
                Exit Sub
            End If
            If e.NewValue Then
                If gv.CurrentRow.Index >= 0 Then
                    Dim vcode As String = clsCommon.myCstr(gv.CurrentRow.Cells(colVendorCode).Value)
                    Dim srn_type As String = clsCommon.myCstr(gv.CurrentRow.Cells(colSRNType).Value)

                    If clsCommon.myLen(VendorCode) > 0 AndAlso clsCommon.CompairString(VendorCode, vcode) <> CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Selected document is not for vendor " + VendorCode + "")
                        gv.CurrentRow.Cells(colSelect).Value = False
                        Exit Sub
                    End If
                    If clsCommon.myLen(SRNType) > 0 AndAlso clsCommon.CompairString(SRNType, srn_type) <> CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Selected document is not for type " + SRNType + "")
                        gv.CurrentRow.Cells(colSelect).Value = False
                        Exit Sub
                    End If

                    VendorCode = vcode
                    SRNType = srn_type
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
