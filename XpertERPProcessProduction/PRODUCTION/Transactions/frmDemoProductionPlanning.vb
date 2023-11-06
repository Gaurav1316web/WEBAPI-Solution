Imports common
Imports System.Data
Imports System.Data.SqlClient
Public Class frmDemoProductionPlanning
    Inherits FrmMainTranScreen
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Qry As String
    Const colLineNo As String = "COLLNO"
    Const colLabbourStrenth As String = "colLabbourStrenth"
    Const colProcess As String = "colProcess"
    Const colItem As String = "colItem"
    Const colCapacity As String = "colCapacity"
    Const colReqd As String = "colReqd"
    Const colRequirement As String = "colRequirement"
    Const colLabourCapacity As String = "colLabourCapacity"
    Const colSecPiece As String = "colSecPiece"
    Const colReqdTime As String = "colReqdTime"


    Private Sub FrmProductionPlanning1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        AddNew()
    End Sub
    Sub AddNew()

        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        gv1.Rows.AddNew()
    End Sub
    Sub BlankAllControls()
        txtDocNo.Value = ""
        fndCust.Value = ""
        lblCust.Text = ""
        txtItem.Value = ""
        txtReference.Text = ""
        txtDesc.Text = ""
        txtDocNo.Value = ""
        lblTotSec.Text = ""
        lblWithTP.Text = ""
        lblHrsReqd.Text = ""
        lblDaysReqd.Text = ""
        lblLabour.Text = ""
        lblLabourPerHr.Text = ""
        lblLabourPerSec.Text = ""
        lblLabourPart.Text = ""
        txtTotalQty.Text = 0
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt("frmDemoProductionPlanning")
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoLabStrenth As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLabStrenth.FormatString = "{0:n2}"
        repoLabStrenth.DecimalPlaces = 0
        repoLabStrenth.HeaderText = "Labour Strength"
        repoLabStrenth.Name = colLabbourStrenth
        repoLabStrenth.Width = 100
        repoLabStrenth.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoLabStrenth)

        Dim repoProcess As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProcess.FormatString = ""
        repoProcess.HeaderText = "Process"
        repoProcess.Name = colProcess
        repoProcess.Width = 150
        repoProcess.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoProcess.TextImageRelation = TextImageRelation.TextBeforeImage
        repoProcess.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoProcess)

        Dim repoItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItem.FormatString = ""
        repoItem.HeaderText = "Item"
        repoItem.Name = colItem
        repoItem.Width = 150
        repoItem.HeaderImage = Global.XpertERPProcessProduction.My.Resources.Resources.search4
        repoItem.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItem.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoItem)

        Dim repoCapacity As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCapacity = New GridViewTextBoxColumn()
        repoCapacity.FormatString = ""
        repoCapacity.HeaderText = "Capacity"
        repoCapacity.Name = colCapacity
        repoCapacity.Width = 150
        repoCapacity.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapacity)

        Dim repoReqd As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoReqd.FormatString = "{0:n2}"
        repoReqd.DecimalPlaces = 0
        repoReqd.HeaderText = "Required"
        repoReqd.Name = colReqd
        repoReqd.Width = 100
        repoReqd.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoReqd)

        Dim repoRequirement As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRequirement.FormatString = "{0:n2}"
        repoRequirement.DecimalPlaces = 0
        repoRequirement.HeaderText = "Requirement"
        repoRequirement.Name = colRequirement
        repoRequirement.Width = 100
        repoRequirement.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRequirement)

        Dim repoLabCapacity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLabCapacity.FormatString = "{0:n2}"
        repoLabCapacity.DecimalPlaces = 0
        repoLabCapacity.HeaderText = "Labour Capacity"
        repoLabCapacity.Name = colLabourCapacity
        repoLabCapacity.Width = 100
        repoLabCapacity.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLabCapacity)

        Dim repoSecPiece As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLabCapacity.FormatString = "{0:n2}"
        repoSecPiece.HeaderText = "Second/Piece"
        repoSecPiece.Name = colSecPiece
        repoSecPiece.Width = 80
        repoSecPiece.Minimum = 0
        repoSecPiece.ReadOnly = True
        repoSecPiece.ShowUpDownButtons = False
        repoSecPiece.Step = 0
        repoSecPiece.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoSecPiece)

        Dim repoReqdtime As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLabCapacity.FormatString = "{0:n2}"
        repoReqdtime.HeaderText = "Reqd/time"
        repoReqdtime.Name = colReqdTime
        repoReqdtime.Width = 80
        repoReqdtime.Minimum = 0
        repoReqdtime.ReadOnly = True
        repoReqdtime.ShowUpDownButtons = False
        repoReqdtime.Step = 0
        repoReqdtime.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoReqdtime)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If clsCommon.myLen(txtTotalQty.Text) <= 0 OrElse clsCommon.myCdbl(txtTotalQty.Text) = 0 Then
                        txtTotalQty.Focus()
                        Throw New Exception("Please enter Total Qty.")
                    End If
                    If e.Column Is gv1.Columns(colProcess) OrElse e.Column Is gv1.Columns(colItem) OrElse e.Column Is gv1.Columns(colReqd) Then
                        Dim dbltotalQty As Integer = txtTotalQty.Text
                        If e.Column Is gv1.Columns(colProcess) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colLabbourStrenth).Value) = 0 OrElse gv1.CurrentRow.Cells(colLabbourStrenth).Value = 0 Then
                                Throw New Exception("Please enter Labour Strength")
                            End If
                            OpenProcessCodeList(False)
                        ElseIf e.Column Is gv1.Columns(colItem) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colProcess).Value) > 0 Then
                                OpenItemCodeList(False, gv1.CurrentRow.Cells(colProcess).Value)
                            End If
                        ElseIf e.Column Is gv1.Columns(colReqd) Then
                            If clsCommon.myLen(gv1.CurrentRow.Cells(colReqd).Value) > 0 AndAlso gv1.CurrentRow.Cells(colReqd).Value > 0 Then

                                Dim intReqd As Integer = gv1.CurrentRow.Cells(colReqd).Value
                                Dim intLabourStr As Integer = gv1.CurrentRow.Cells(colLabbourStrenth).Value
                                Dim intCapacity As Integer = clsCommon.myCdbl(gv1.CurrentRow.Cells(colCapacity).Value)
                                Dim intLabourCapacity As Double = 0
                                Dim intSecPiece As Double = 0
                                Dim intReqdtime As Double = 0

                                intLabourCapacity = intCapacity * intLabourStr
                                intSecPiece = 3600 / intLabourCapacity
                                intReqdtime = (intReqd * dbltotalQty) * intSecPiece

                                gv1.CurrentRow.Cells(colRequirement).Value = intReqd * dbltotalQty
                                gv1.CurrentRow.Cells(colLabourCapacity).Value = Math.Round(intLabourCapacity, 2)
                                gv1.CurrentRow.Cells(colSecPiece).Value = Math.Round(intSecPiece, 1)
                                gv1.CurrentRow.Cells(colReqdTime).Value = Math.Round(intReqdtime, 2)

                                UpdateTotal()
                            End If
                        End If
                    End If


                End If
                isCellValueChangedOpen = False
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub UpdateTotal()
        Dim dbltotalQty As Integer = txtTotalQty.Text
        Dim intReqd As Integer = 0
        Dim intLabourStr As Integer = 0
        Dim intCapacity As Integer=0
        Dim intLabourCapacity As Double = 0
        Dim intSecPiece As Double = 0
        Dim intReqdtime As Double = 0

       
        Dim TotSec As Decimal = 0.0
        Dim withTP, HrsReqd, DayReqd, labourPart, dblLabour As Double
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(colProcess).Value) > 0 Then
                intReqd = grow.Cells(colReqd).Value
                intLabourStr = grow.Cells(colLabbourStrenth).Value
                intCapacity = clsCommon.myCdbl(grow.Cells(colCapacity).Value)

                intLabourCapacity = intCapacity * intLabourStr
                intSecPiece = 3600 / intLabourCapacity
                intReqdtime = (intReqd * dbltotalQty) * intSecPiece

                grow.Cells(colRequirement).Value = intReqd * dbltotalQty
                grow.Cells(colLabourCapacity).Value = Math.Round(intLabourCapacity, 2)
                grow.Cells(colSecPiece).Value = Math.Round(intSecPiece, 1)
                grow.Cells(colReqdTime).Value = Math.Round(intReqdtime, 2)
                TotSec = TotSec + clsCommon.myCdbl(grow.Cells(colReqdTime).Value)
            End If

        Next
        lblTotSec.Text = Math.Round(TotSec, 2)
        dblLabour = clsCommon.myCdbl(lbllabour.Text)
        withTP = TotSec * 1.1
        HrsReqd = withTP / 3600
        DayReqd = HrsReqd / 7.5
        labourPart = withTP * 0.01
        lblWithTP.Text = withTP
        lblHrsReqd.Text = HrsReqd
        lblDaysReqd.Text = DayReqd

        lblLabourPerHr.Text = Math.Round(dblLabour / 9, 2)
        lblLabourPerSec.Text = Math.Round(clsCommon.myCdbl(lblLabourPerHr.Text) / 3600, 2)
        lblLabourPart.Text = Math.Round(withTP * clsCommon.myCdbl(lblLabourPerSec.Text), 2)
    End Sub
    Sub OpenProcessCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select distinct Process_Code as Code,Process_Desc as [DESCRIPTION] from tspl_process_master"
        Dim ProcessCode As String = clsCommon.myCstr(clsCommon.ShowSelectForm("ProcessCode", qry, "Code", "", gv1.CurrentRow.Cells(colProcess).Value, "", False))
        If clsCommon.myLen(ProcessCode) > 0 Then
            gv1.CurrentRow.Cells(colProcess).Value = ProcessCode
        Else
            SetBlankOfItemColumns()
        End If

    End Sub
    Sub OpenItemCodeList(ByVal isButtonClick As Boolean, ByVal strProcessCode As String)
        Dim qry As String = "select Item_Code as Code,Capacaity from tspl_process_master  "
        Dim ItemCode As String = clsCommon.myCstr(clsCommon.ShowSelectForm("ExpenseCode", qry, "Code", "Process_Code='" & strProcessCode & "'", gv1.CurrentRow.Cells(colItem).Value, "", False))
        If clsCommon.myLen(ItemCode) > 0 Then
            qry = "select Item_Code,Capacaity from tspl_process_master where Process_Code='" & strProcessCode & "' and Item_Code='" & ItemCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colItem).Value = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                gv1.CurrentRow.Cells(colCapacity).Value = clsCommon.myCstr(dt.Rows(0)("Capacaity"))        
            Else
                SetBlankOfItemColumns()
            End If
        End If

    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colProcess).Value = ""
        gv1.CurrentRow.Cells(colItem).Value = ""
        gv1.CurrentRow.Cells(colCapacity).Value = ""
        gv1.CurrentRow.Cells(colReqd).Value = 0
        gv1.CurrentRow.Cells(colRequirement).Value = 0
        gv1.CurrentRow.Cells(colLabourCapacity).Value = 0
        gv1.CurrentRow.Cells(colSecPiece).Value = 0
        gv1.CurrentRow.Cells(colReqdTime).Value = 0
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            UpdateTotal()
        End If
    End Sub

    Private Sub fndCust__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndCust._MYValidating
        Dim qry As String = "select Cust_Code as Code,Customer_Name from TSPL_CUSTOMER_MASTER"
        fndCust.Value = clsCommon.ShowSelectForm("Cust Code", qry, "Code", "", fndCust.Value, "", isButtonClicked)
        lblCust.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + fndCust.Value + "'")
    End Sub
    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then

                Dim obj As New clsDemoProdPlan()

                obj.Document_No = txtDocNo.Value
                obj.Cust_Code = fndCust.Value
                obj.Item_Code = txtItem.Value
                obj.TotalSec = lblTotSec.Text
                obj.With_TP = lblWithTP.Text
                obj.Hours_Reqd = lblHrsReqd.Text
                obj.Days_Reqd = lblDaysReqd.Text
                obj.Labour = lbllabour.Text
                obj.Labour_hour = lblLabourPerHr.Text
                obj.Labour_Sec = lblLabourPerSec.Text
                obj.Labour_Part = lblLabourPart.Text
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                obj.TotalQty = txtTotalQty.Text


                obj.Arr = New List(Of ClsDemoProdPlanDetail)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsDemoProdPlanDetail()
                    objTr.Document_Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)

                    objTr.Labour_Strenth = clsCommon.myCdbl(grow.Cells(colLabbourStrenth).Value)
                    objTr.Process_Code = clsCommon.myCstr(grow.Cells(colProcess).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItem).Value)
                    objTr.Capacity = clsCommon.myCstr(grow.Cells(colCapacity).Value)
                    objTr.Reqd = clsCommon.myCdbl(grow.Cells(colReqd).Value)
                    objTr.Requirement = clsCommon.myCdbl(grow.Cells(colRequirement).Value)
                    objTr.Labour_Capacity = clsCommon.myCdbl(grow.Cells(colLabourCapacity).Value)
                    objTr.Second_Piece = clsCommon.myCdbl(grow.Cells(colSecPiece).Value)
                    objTr.Required_Time = clsCommon.myCdbl(grow.Cells(colReqdTime).Value)

                    If isFirstTime Then
                        isFirstTime = False
                    End If


                    If (clsCommon.myLen(objTr.Process_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)

                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
                Return isSaved
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New clsDemoProdPlan()
            obj = clsDemoProdPlan.GetData(strCode, NavTyep, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
              

                txtDocNo.Value = obj.Document_No
                fndCust.Value = obj.Cust_Code
                txtItem.Value = obj.Item_Code
                lblTotSec.Text = obj.TotalSec
                lblWithTP.Text = obj.With_TP
                lblHrsReqd.Text = obj.Hours_Reqd
                lblDaysReqd.Text = obj.Days_Reqd
                lbllabour.Text = obj.Labour
                lblLabourPerHr.Text = obj.Labour_hour
                lblLabourPerSec.Text = obj.Labour_Sec
                lblLabourPart.Text = obj.Labour_Part
                txtReference.Text = obj.Reference
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                txtTotalQty.Text = obj.TotalQty
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsDemoProdPlanDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Document_Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLabbourStrenth).Value = objTr.Labour_Strenth
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProcess).Value = objTr.Process_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItem).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCapacity).Value = objTr.Capacity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqd).Value = objTr.Reqd
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRequirement).Value = objTr.Requirement
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLabourCapacity).Value = objTr.Labour_Capacity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSecPiece).Value = objTr.Second_Piece
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colReqdTime).Value = objTr.Required_Time
                    Next

                End If
              
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
          
                If (clsDemoProdPlan.DeleteData(txtDocNo.Value)) Then

                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(fndCust.Value) <= 0 Then
            fndCust.Focus()
            Throw New Exception("Please select Customer")
        End If
        If clsCommon.myLen(txtItem.Value) <= 0 Then
            txtItem.Focus()
            Throw New Exception("Please select item")
        End If
        If clsCommon.myLen(lbllabour.Text) <= 0 Then
            lbllabour.Focus()
            Throw New Exception("Please select Labour")
        End If
        Dim Counter As Integer = 0
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strPCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colProcess).Value)
            Dim dblCost As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colLabbourStrenth).Value)

            If clsCommon.myLen(strPCode) > 0 Then
                Counter += 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colLabbourStrenth).Value) <= 0 Then
                    Throw New Exception("Please enter labour strength of Process Code " + strPCode + " at Row No " + clsCommon.myCstr(ii + 1))
                ElseIf clsCommon.myLen(gv1.Rows(ii).Cells(colItem).Value) <= 0 Then
                    Throw New Exception("Please enter Item of Process Code " + strPCode + " at Row No " + clsCommon.myCstr(ii + 1))
                ElseIf clsCommon.myLen(gv1.Rows(ii).Cells(colReqd).Value) <= 0 Then
                    Throw New Exception("Please enter Reqd. of Process Code " + strPCode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                UpdateTotal()
            End If

        Next

        If Counter <= 0 Then
            Throw New Exception("Please enter atleast single Process Code")
        End If

        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub txtItem__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtItem._MYValidating
        Dim qry As String = "select Item_Code as Code,Item_Desc as [Item Desc] from TSPL_ITEM_MASTER"
        txtItem.Value = clsCommon.ShowSelectForm("Item Code", qry, "Code", "", txtItem.Value, "", isButtonClicked)
        lblItem.Text = clsDBFuncationality.getSingleValue("select Item_Desc  from TSPL_ITEM_MASTER where Item_Code='" + txtItem.Value + "'")
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_DEMO_PRODPLAN_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT Document_No AS [DocumentNo], Cust_Code as [Customer],Item_Code as [Item] FROM  TSPL_DEMO_PRODPLAN_HEAD  "
        txtDocNo.Value = clsCommon.ShowSelectForm("ProdDoc", qry, "DocumentNo", "", txtDocNo.Value, "DocumentNo", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub txtTotalQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalQty.TextChanged

    End Sub
End Class
