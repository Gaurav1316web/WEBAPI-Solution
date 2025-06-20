Imports common
Imports System.Data.SqlClient
Public Class frmDCSTransportationCharges
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Const ColSNo As String = "SNo"
    Const ColDCSUploaderCode As String = "DCS Uploader Code"
    Const colDCSCode As String = "DCS Code"
    Const ColDCSName As String = "DCS Name"
    Const colTransportationRate As String = "Transportation Rate"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Public isInActive As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        '        btnReverse.Visible = False
        If MyBase.isExport = True Then
            rmiImport.Enabled = True
            rmiExport.Enabled = True
        Else
            rmiImport.Enabled = False
            rmiExport.Enabled = False
        End If
    End Sub
    Private Sub frmDCSTransportationCharges_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()
        AddNew()
    End Sub
    'Private Sub frmDCSTransportationCharges_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '    If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
    '        If MyBase.isReverse Then
    '            Dim frm As New FrmPWD(Nothing)
    '            frm.strType = clsFixedParameterType.SIR
    '            frm.strCode = clsFixedParameterCode.SIReversAndCreate
    '            frm.ShowDialog()
    '            If frm.isPasswordCorrect Then
    '                'btnReverse.Visible = True
    '            End If
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
    '        End If
    '    End If
    'End Sub
    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = ColSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        Dim TxtBoxCol As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Uploader Code"
        TxtBoxCol.Name = ColDCSUploaderCode
        TxtBoxCol.IsVisible = True
        TxtBoxCol.Width = 100
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Code"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = colDCSCode
        TxtBoxCol.IsVisible = True
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Name"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = ColDCSName
        TxtBoxCol.Width = 200
        TxtBoxCol.IsVisible = True
        TxtBoxCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        Dim repoTransportationRate = New GridViewDecimalColumn()
        repoTransportationRate.FormatString = "{0:n2}"
        repoTransportationRate.HeaderText = "Transportation Rate"
        repoTransportationRate.Name = colTransportationRate
        repoTransportationRate.Width = 150
        repoTransportationRate.Minimum = 0
        repoTransportationRate.Width = 100
        repoTransportationRate.ShowUpDownButtons = False
        repoTransportationRate.DecimalPlaces = 2
        repoTransportationRate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTransportationRate)

        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = True
        gv1.BestFitColumns()
        ' gv1.Rows.AddNew()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Public Sub AddNew()
        isNewEntry = True
        lblStatus.Status = ERPTransactionStatus.Pending
        txtDocumentNo.Value = ""
        txtEndDate.Checked = False
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtItems.arrValueMember = Nothing
        txtItems.arrDispalyMember = Nothing
        txtRemarks.Text = ""
        txtComment.Text = ""
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnGo.Enabled = True
        btnDelete.Enabled = False
        chkInActive.Checked = False
        txtStartDate.Value = clsCommon.GETSERVERDATE()
        txtEndDate.Value = clsCommon.GETSERVERDATE()
        chkInActive.Enabled = False
        LoadBlankGrid()
        btnSave.Text = "Save"
    End Sub
    Private Sub txtItems__My_Click(sender As Object, e As EventArgs) Handles txtItems._My_Click
        Try
            Dim qry As String = ""
            qry = "select * from (
select  a.DESCRIPTION,a.cat_value, TSPL_ITEM_MASTER.item_code as Item,TSPL_ITEM_MASTER.item_desc as [ItemDesc],TSPL_ITEM_MASTER.Short_Description, 
TSPL_ITEM_MASTER.Unit_Code as Unit , TSPL_ITEM_MASTER.Rate as BasicRate,TSPL_ITEM_MASTER.rate as MRP, Weight_Value as [Weight Value] ,TSPL_ITEM_MASTER.Deduction,TSPL_DEDUCTION_MASTER.Description as Deduction_Name
from TSPL_ITEM_MASTER    
left outer join TSPL_DEDUCTION_MASTER On TSPL_DEDUCTION_MASTER.Code=TSPL_ITEM_MASTER.Deduction
left outer join (select TSPL_ITEM_MASTER_CATEGORY.Item_code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code,
TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as cat_value  from TSPL_ITEM_MASTER_CATEGORY left outer join TSPL_ITEM_CATEGORY_LEVEL on TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE= TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and ISNULL(TSPL_ITEM_CATEGORY_LEVEL.Form_Type,'item')='item' 
left outer join  TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values  and ISNULL(TSPL_ITEM_CATEGORY_LEVEL_VALUES.Form_Type,'item')='item')a on a.Item_code=TSPL_ITEM_MASTER.Item_Code and TSPL_ITEM_MASTER.item_code=a.item_code 
          where  TSPL_ITEM_MASTER.Active = 1 And Is_FreshItem = 0 And coalesce(Product_Type,'') not in ('MI') and Item_Type not in ('A') and coalesce(Item_used_as,'')='S' ) as s pivot(max(cat_value) for description in ([BRAND],[ITEM TYPE1],[MAIN GROUP],[PACKING],[PRODUCT CATEGORY],[SKU],[SUB GROUP]))t  "

            txtItems.arrValueMember = clsCommon.ShowMultipleSelectForm("ITEMSMUL", qry, "Item", "ItemDesc", txtItems.arrValueMember, txtItems.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            LoadBlankGrid()
            Dim dt As DataTable
            Dim obj As New clsDCSTransportationCharges
            Dim qry As String = "select ROW_NUMBER() over (order by VLC_Code_VLC_Uploader) as SNO,VLC_Code_VLC_Uploader as [DCS Uploader Code],VLC_Code as [DCS Code],VLC_Name as [DCS Name],isnull(TPT.Transportation_Rate,0) as [Transportation Rate] from TSPL_VLC_MASTER_HEAD left join (select DCS_Code,Transportation_Rate from TSPL_DCS_Transportation_Charges_Detail where Document_No='" + txtDocumentNo.Value + "' )TPT on TPT.DCS_Code= TSPL_VLC_MASTER_HEAD.VLC_Code  where 2=2 "
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            gv1.GroupDescriptors.Clear()
            gv1.EnableFiltering = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv1.DataSource = dt
                gv1.BestFitColumns()
                gv1.TableElement.TableHeaderHeight = 40
                gv1.MasterTemplate.ShowRowHeaderColumn = True
                For ii As Integer = 0 To gv1.Columns.Count - 2
                    gv1.Columns(ii).ReadOnly = True
                Next
                gv1.Columns("SNO").Width = 40
                gv1.Columns("DCS Uploader Code").Width = 100
                gv1.Columns("DCS Code").Width = 120
                gv1.Columns("DCS Name").Width = 120
                gv1.Columns("Transportation Rate").Width = 150
                gv1.Columns("Transportation Rate").FormatString = "{0:n2}"

                gv1.MasterTemplate.AutoExpandGroups = True
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "Not Found!", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub rmiImport_Click(sender As Object, e As EventArgs) Handles rmiImport.Click
        FunImport()
    End Sub
    Public Sub FunImport()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 AndAlso gv1.Rows.Count > 0 Then
                Dim gv As New UserControls.MyRadGridView
                Me.Controls.Add(gv)
                Dim currentdate As Date = Date.Today
                If transportSql.importExcel(gv, "DCS Uploader Code", "DCS Code", "DCS Name", "Transportation Rate") Then
                    Dim linno As Integer = 0
                    Dim obj As New clsDCSTransportationChargesDetail
                    Dim arr As New List(Of clsDCSTransportationChargesDetail)
                    Dim strCode As String = ""
                    Dim strName As String = ""
                    Dim strUploader_No As String = ""
                    Dim strZone As String = ""
                    Dim duplicateUploader As String = Nothing
                    Dim dtError As New DataTable
                    Dim ii As Integer = 0
                    dtError.Columns.Add("RowNo", GetType(Integer))
                    dtError.Columns.Add("Error", GetType(String))
                    isNewEntry = False
                    Try
                        If gv IsNot Nothing AndAlso gv.Rows.Count > 0 Then
                            clsCommon.ProgressBarPercentShow()
                            For Each grow As GridViewRowInfo In gv.Rows
                                Try
                                    linno += 1
                                    clsCommon.ProgressBarPercentUpdate(linno, gv.Rows.Count, "Validating Data...")
                                    obj = New clsDCSTransportationChargesDetail()
                                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("DCS Code").Value)) <= 0 Then
                                        Throw New Exception("DCS Code can't be blank !")
                                    Else
                                        Dim isDCSCodeExist As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) from TSPL_VLC_MASTER_HEAD where VLC_Code='" + clsCommon.myCstr(grow.Cells("DCS Code").Value) + "'") > 0)
                                        If Not isDCSCodeExist Then
                                            Throw New Exception("[" + clsCommon.myCstr(grow.Cells("DCS Code").Value) + "] DCS Code is not exist in DCS Master.")
                                        End If
                                    End If

                                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value)) <= 0 Then
                                        Throw New Exception("DCS Uploader Code can't be blank !")
                                    Else
                                        Dim isDCSCodeExist As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(1) from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader='" + clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value) + "'") > 0)
                                        If Not isDCSCodeExist Then
                                            Throw New Exception("[" + clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value) + "] DCS Uploader Code is not exist in DCS Master.")
                                        End If
                                    End If

                                    obj.SNO = linno
                                    obj.VLC_Code_VLC_Uploader = clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value)
                                    obj.DCS_Code = clsCommon.myCstr(grow.Cells("DCS Code").Value)
                                    obj.VLC_Name = clsCommon.myCstr(grow.Cells("DCS Uploader Code").Value)
                                    obj.Transportation_Rate = clsCommon.myCdbl(grow.Cells("Transportation Rate").Value)
                                    arr.Add(obj)
                                Catch ex As Exception
                                    Dim dr As DataRow = dtError.NewRow()
                                    dr("RowNo") = linno
                                    dr("Error") = ex.Message
                                    dtError.Rows.Add(dr)
                                End Try
                            Next
                            clsCommon.ProgressBarPercentHide()
                        Else
                            Throw New Exception("No Rows Found to Import")
                        End If
                        Try
                            If dtError.Rows.Count > 0 Then
                                Dim ff As New FrmFreeGrid
                                ff.ReportID = MyBase.Form_ID
                                ff.Text = "DCS Transportation Charges"
                                ff.dt = dtError
                                ff.ShowDialog()
                            ElseIf arr IsNot Nothing AndAlso arr.Count > 0 Then
                                Dim qry As String = "Valid Row [" + clsCommon.myCstr(arr.Count) + "] Do You want to Proceed"
                                If clsCommon.MyMessageBoxShow(Me, qry, Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                                    clsCommon.ProgressBarPercentShow()
                                    ii = 0
                                    Try
                                        gv1.Rows.Clear()
                                        For Each obj1 As clsDCSTransportationChargesDetail In arr
                                            ii += 1
                                            clsCommon.ProgressBarPercentUpdate(ii, arr.Count, "Loading Details..." & clsCommon.myCstr(ii) & "/" & clsCommon.myCstr(arr.Count) & "")
                                            gv1.Rows.AddNew()
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = ii
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSUploaderCode).Value = obj1.VLC_Code_VLC_Uploader
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = obj1.DCS_Code
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSName).Value = obj1.VLC_Name
                                            gv1.Rows(gv1.Rows.Count - 1).Cells(colTransportationRate).Value = obj1.Transportation_Rate

                                        Next
                                    Catch ex As Exception
                                        Throw New Exception(ex.Message)
                                    Finally
                                        clsCommon.ProgressBarPercentHide()
                                    End Try
                                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                                End If
                            Else
                                Throw New Exception("No Valid Rows Found to Load")
                            End If
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    End Try
                End If
            Else
                Throw New Exception("No Data Found to Import")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub rmiExport_Click(sender As Object, e As EventArgs) Handles rmiExport.Click
        Export()
    End Sub
    Public Sub Export()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 AndAlso gv1.Rows.Count > 0 Then
                Dim str As String = "select SNO,VLC_Code_VLC_Uploader as [DCS Uploader Code],DCS_Code as [DCS Code],VLC_Name as [DCS Name],Transportation_Rate as [Transportation Rate] from TSPL_DCS_Transportation_Charges_Detail left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code= TSPL_DCS_Transportation_Charges_Detail.DCS_Code"
                Dim whrCls As String = " and TSPL_DCS_Transportation_Charges_Detail.Document_No='" + txtDocumentNo.Value + "'"
                ListImpExpColumnsMandatory = New List(Of String)({"DCS Uploader Code", "DCS Code", "DCS Name", "Transportation Rate"})
                transportSql.ExporttoExcel(str, whrCls, Me)
            Else
                Throw New Exception("No Data Found to Export")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub
    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
    '    If gv1.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gv1.CurrentRow.Index
    '        gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
    '        If intCurrRow = gv1.Rows.Count - 1 Then
    '            'gv1.Rows.AddNew()
    '            gv1.CurrentRow = gv1.Rows(intCurrRow)
    '        End If
    '    End If
    'End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub LoadData(ByVal strDocNo As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsDCSTransportationCharges()
            obj = clsDCSTransportationCharges.GetData(strDocNo, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                LoadBlankGrid()
                AddNew()
                isNewEntry = False
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                    chkInActive.Enabled = Not obj.In_Active
                    btnGo.Enabled = False
                    btnImport.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    btnDelete.Enabled = True
                    lblStatus.Status = ERPTransactionStatus.Pending
                    chkInActive.Enabled = False
                    btnImport.Enabled = True
                End If
                isInActive = obj.In_Active

                txtDocumentNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtStartDate.Value = obj.Start_Date
                If clsCommon.myLen(obj.End_Date) > 0 Then
                    txtEndDate.Value = obj.End_Date
                    txtEndDate.Checked = True
                Else
                    txtEndDate.Checked = False
                End If
                txtRemarks.Text = obj.Remarks
                txtComment.Text = obj.Comments
                txtItems.arrValueMember = obj.Items
                If obj.In_Active Then
                    chkInActive.Checked = True
                End If
                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDCSTransportationChargesDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = sl
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSUploaderCode).Value = objTr.VLC_Code_VLC_Uploader
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = clsCommon.myCstr(objTr.DCS_Code)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSName).Value = objTr.VLC_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTransportationRate).Value = objTr.Transportation_Rate
                        sl += 1
                    Next
                End If
            Else
                btnSave.Text = "Save"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.GetPrintDate(txtStartDate.Value, "dd/MMM/yyyy") < clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") Then
            txtStartDate.Focus()
            clsCommon.MyMessageBoxShow(Me, "Start Date cannot be lesser than Document Date", Me.Text)
            Return False
        End If
        If txtItems.arrValueMember Is Nothing Then
            txtItems.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please select at Least one Item", Me.Text)
            Return False
        End If
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Public Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsDCSTransportationCharges()
                obj.Document_No = txtDocumentNo.Value
                obj.Document_Date = txtDate.Value
                If txtItems.arrValueMember IsNot Nothing Then
                    obj.Items = txtItems.arrValueMember
                End If
                obj.Start_Date = txtStartDate.Value
                If txtEndDate.Checked Then
                    obj.End_Date = txtEndDate.Value
                Else
                    obj.End_Date = Nothing
                End If

                obj.Remarks = txtRemarks.Text
                obj.Comments = txtComment.Text
                If chkInActive.Checked Then
                    obj.In_Active = True
                End If
                obj.Arr = New List(Of clsDCSTransportationChargesDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsDCSTransportationChargesDetail()
                    ' objTr.SNO = clsCommon.myCdbl(grow.Cells(ColSNo).Value)
                    objTr.DCS_Code = clsCommon.myCstr(grow.Cells(colDCSCode).Value)
                    objTr.Transportation_Rate = clsCommon.myCdbl(grow.Cells(colTransportationRate).Value)
                    If (objTr.Transportation_Rate > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If obj.Arr.Count <= 0 Then
                    Throw New Exception("Please fill at least one DCS Transportation Rate.")
                End If
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Public Sub PostData()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsDCSTransportationCharges.PostData(clsCommon.myCstr(txtDocumentNo.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        Try
            Dim whrClas As String = "2=2"
            Dim qry As String = "select Document_No as DocumentNo,Document_Date as DocumentDate,Start_Date as [Start Date],End_Date as [End Date],case when isnull(In_Active,0)=1 then 'Yes' else 'No' end as [In Active],Remarks,Comments,case when isnull(Status,0)=1 then 'Approved' else 'Pending' end as Status,Posted_Date as [Posted Date] from TSPL_DCS_Transportation_Charges_Head"
            LoadData(clsCommon.myCstr(clsCommon.ShowSelectForm("DCSTPTCH", qry, "DocumentNo", whrClas, txtDocumentNo.Value, "DocumentNo", isButtonClicked, "Document_Date")), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DCS_Transportation_Charges_Head where Document_No='" + txtDocumentNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnReverse_Click(sender As Object, e As EventArgs)
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsDCSTransportationCharges.ReverseAndUnpost(txtDocumentNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Document No not found to Delete", Me.Text)
                Exit Sub
            End If
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsDCSTransportationCharges.DeleteData(txtDocumentNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub chkInActive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInActive.ToggleStateChanged
        Try
            If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
                If Not isInActive Then
                    If chkInActive.Checked Then
                        If clsCommon.MyMessageBoxShow("Are you sure to In Active this Document", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                            If clsDCSTransportationCharges.InActiveDocument(txtDocumentNo.Value) Then
                                clsCommon.MyMessageBoxShow(Me, "Tansaction in Active succesffuly", Me.Text)
                                LoadData(txtDocumentNo.Value, NavigatorType.Current)
                            End If
                        End If
                    End If
                End If
                isInActive = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class