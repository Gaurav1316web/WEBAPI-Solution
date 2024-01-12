' ----------------- Created By Anubhooti On 04-May-2015 BM00000006299 -------------------- '
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class frmHRTravelReqApproval
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isloading As Boolean = False

    Const colApproved As String = "Approved"
    Const colRejected As String = "Rejected"
    Const colReqCode As String = "Req Code"
    Const colBookedByTravel As String = "Travel Booking By"
    Const colBookedByNameTravel As String = "Travel Booking For"
    Const colTravelType As String = "Travel Type"
    Const colEmployeeCode As String = "Employee Code"
    Const colTravelCategory As String = "Travel Category"
    Const colTotalAppliedAmt As String = "Total Applied Amount"
    Const colStatus As String = "Status"
    Const colAppliedDate As String = "Applied Date"
    Const colHideApproved As String = "Hide Approved"
    Const colHideRejected As String = "Hide Rejected"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmHRTravelReqApproval)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
    End Sub
    ' ----------------- Get Travel Type ------------------------
    Private Function GetTT() As DataTable
        Dim DT_TT As DataTable = New DataTable
        DT_TT.Columns.Add("Code", GetType(String))
        DT_TT.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_TT.NewRow()
        DR = DT_TT.NewRow()
        DR("Name") = "ALL"
        DR("Code") = "A"
        DT_TT.Rows.Add(DR)

        DR = DT_TT.NewRow()
        DR("Name") = "Domestic"
        DR("Code") = "D"
        DT_TT.Rows.Add(DR)

        DR = DT_TT.NewRow()
        DR("Name") = "International"
        DR("Code") = "I"
        DT_TT.Rows.Add(DR)

        DT_TT.AcceptChanges()

        Return DT_TT
    End Function
    ' ----------------- Get Status ------------------------
    Private Function GetS() As DataTable
        Dim DT_S As DataTable = New DataTable
        DT_S.Columns.Add("Code", GetType(String))
        DT_S.Columns.Add("Name", GetType(String))

        Dim DR As DataRow = DT_S.NewRow()
        DR = DT_S.NewRow()
        DR("Name") = "ALL"
        DR("Code") = "ALL"
        DT_S.Rows.Add(DR)

        DR = DT_S.NewRow()
        DR("Name") = "Approved"
        DR("Code") = "A"
        DT_S.Rows.Add(DR)

        DR = DT_S.NewRow()
        DR("Name") = "Rejected"
        DR("Code") = "R"
        DT_S.Rows.Add(DR)

        DT_S.AcceptChanges()

        Return DT_S
    End Function
    Private Function AllowToSave() As Boolean
        btnsave.Focus()
       
        Return True
    End Function
    Public Sub Reset()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        TxtTravelBookingBy.Value = ""
        LblTravelBookingBy.Text = ""
        TxtTravelBookingFor.Value = ""
        LblTravelBookingFor.Text = ""
        TxtTravelCat.Value = ""
        LblTravelCat.Text = ""
        TxtTravelPurpose.Value = ""
        LblTravelPurpose.Text = ""
        dtpFromDate.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
      
        Me.cmbTravelType.DataSource = GetTT()
        Me.cmbTravelType.DisplayMember = "Name"
        Me.cmbTravelType.ValueMember = "Code"

        Me.CmbStatus.DataSource = GetS()
        Me.CmbStatus.DisplayMember = "Name"
        Me.CmbStatus.ValueMember = "Code"

        GrpSearchDate.Enabled = False
        ChkSearchDate.Enabled = True
    End Sub


    Sub LoadData(ByVal WhrCls As String)
        Try
            isloading = True
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()

            Dim strquery As String = ""

            strquery += " SELECT  CAST(ISNULL(Approved,0) as Bit) as [Approved],CAST(ISNULL(Rejected,0) as Bit) as [Rejected],Travel_Req_Code AS [Travel Code],CASE WHEN Is_Domesctic =1 THEN 'Domestic' WHEN Is_International =1 THEN 'International' END AS [Travel Type],Booking_For_Code AS [Booking For],CASE WHEN BOOKED_BY_TRAVEL = 'S' THEN 'Self' ELSE 'Company' END AS [Booked By],Booked_By_Name_Code AS [Booked By Code],Travel_Category_Code AS [Travel Category],Amount AS [Total Applied Amount],CASE WHEN Approved =0 THEN 'Pending' ELSE 'Approved' END AS [Status], Document_Date AS [Applied Date],CAST(ISNULL(Approved,0) as Bit) as [Hide Approved],CAST(ISNULL(Rejected,0) as Bit) as [Hide Rejected] FROM TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY "
            strquery += "" + WhrCls
            gv1.DataSource = clsDBFuncationality.GetDataTable(strquery)
            FormatGrid()

            btnSave.Enabled = True
            btnSave.Text = "Save"


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isloading = False
        End Try
       
    End Sub
    Public Sub Savedata()
        Try
            Dim currentdate As Date = Date.Today
            Dim IsApproved As Integer = 0
            Dim IsRejected As Integer = 0
            Dim qry1 As String = String.Empty
            Dim AR_Date As String = String.Empty
            Dim AR_By As String = String.Empty
            Dim Code As String = String.Empty

            If AllowToSave() Then

                For i As Integer = 0 To gv1.Rows.Count - 1
                    If CBool(gv1.Rows(i).Cells(colApproved).Value) = True Then
                        IsApproved = 1
                        IsRejected = 0
                        AR_Date = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                        AR_By = "'" + objCommonVar.CurrentUserCode + "'"
                        Code = clsCommon.myCstr(gv1.Rows(i).Cells("Travel Code").Value)

                        qry1 = "UPDATE TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY SET Approved =" + clsCommon.myCstr(IsApproved) + ", Approved_Date = " + AR_Date + " ,Approved_By = " + AR_By + " ,Rejected =" + clsCommon.myCstr(IsRejected) + ", Rejected_Date = " + AR_Date + " ,Rejected_By = " + AR_By + " WHERE Travel_Req_Code ='" + Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)

                    ElseIf CBool(gv1.Rows(i).Cells(colRejected).Value) = True Then
                        IsApproved = 0
                        IsRejected = 1
                        AR_Date = "'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy hh:mm tt") + "'"
                        AR_By = "'" + objCommonVar.CurrentUserCode + "'"
                        Code = clsCommon.myCstr(gv1.Rows(i).Cells("Travel Code").Value)

                        qry1 = "UPDATE TSPL_HR_RAISE_TRAVEL_REQUISITION_ENTRY SET Approved =" + clsCommon.myCstr(IsApproved) + ", Approved_Date = " + AR_Date + " ,Approved_By = " + AR_By + " ,Rejected =" + clsCommon.myCstr(IsRejected) + ", Rejected_Date = " + AR_Date + " ,Rejected_By = " + AR_By + " WHERE Travel_Req_Code ='" + Code + "'"
                        clsDBFuncationality.ExecuteNonQuery(qry1)

                    End If
                Next
                myMessages.insert()
                Reset()
                LoadData("")
            End If

            btnsave.Text = "Save"
       
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub FormatGrid()
        If gv1.Rows.Count > 0 Then
            gv1.Columns("Approved").Width = 75
            gv1.Columns("Approved").ReadOnly = False
            gv1.Columns("Rejected").Width = 75
            gv1.Columns("Rejected").ReadOnly = False
            gv1.Columns("Travel Code").Width = 100
            gv1.Columns("Travel Code").ReadOnly = True
            gv1.Columns("Booking For").Width = 100
            gv1.Columns("Booking For").ReadOnly = True
            gv1.Columns("Booked By").Width = 65
            gv1.Columns("Booked By").ReadOnly = True
            gv1.Columns("Booked By Code").Width = 120
            gv1.Columns("Booked By Code").ReadOnly = True
            gv1.Columns("Travel Category").Width = 110
            gv1.Columns("Travel Category").ReadOnly = True
            gv1.Columns("Total Applied Amount").Width = 120
            gv1.Columns("Total Applied Amount").ReadOnly = True
            gv1.Columns("Status").Width = 70
            gv1.Columns("Status").ReadOnly = True
            gv1.Columns("Applied Date").Width = 100
            gv1.Columns("Applied Date").FormatString = "{0:dd/MMM/yyyy}"
            gv1.Columns("Applied Date").ReadOnly = True
            gv1.Columns("Travel Type").Width = 75
            gv1.Columns("Travel Type").ReadOnly = True
            gv1.Columns("Hide Approved").IsVisible = False
            gv1.Columns("Hide Rejected").IsVisible = False
            gv1.AllowDeleteRow = True
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = False
            gv1.AllowRowReorder = False
            gv1.EnableFiltering = True
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
        Else
            gv1.BestFitColumns()
        End If
    End Sub
    Private Sub frmHRTravelReqApproval_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
            GC.Collect()
        End If
    End Sub
    Private Sub frmHRTravelReqApproval_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag))
        Else
            LoadData("")
        End If
    End Sub
    Private Sub gv1_RowFormatting(sender As Object, e As RowFormattingEventArgs) Handles gv1.RowFormatting
        If e.RowElement.RowInfo.Cells(colApproved).Value = True Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightGreen
        ElseIf e.RowElement.RowInfo.Cells(colRejected).Value = True Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LightSalmon
        Else
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.AliceBlue
        End If
    End Sub
    Private Sub gv1_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            Savedata()
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub
    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        If e.Column Is gv1.Columns(colApproved) OrElse e.Column Is gv1.Columns(colRejected) Then
            gv1.CurrentRow.Cells(colApproved).ReadOnly = False
            gv1.CurrentRow.Cells(colRejected).ReadOnly = False

            If CBool(gv1.CurrentRow.Cells(colApproved).Value) = True Then
                If CBool(gv1.CurrentRow.Cells(colHideApproved).Value) = True Then
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                    gv1.CurrentRow.Cells(colRejected).Value = 0
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                    gv1.CurrentRow.Cells(colRejected).Value = 0
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = False
                End If
            ElseIf CBool(gv1.CurrentRow.Cells(colRejected).Value) = True Then
                If CBool(gv1.CurrentRow.Cells(colHideRejected).Value) = True Then
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = True
                    gv1.CurrentRow.Cells(colApproved).Value = 0
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = True
                Else
                    gv1.CurrentRow.Cells(colApproved).ReadOnly = True
                    gv1.CurrentRow.Cells(colApproved).Value = 0
                    gv1.CurrentRow.Cells(colRejected).ReadOnly = False
                End If
                
            End If      
        End If
    End Sub
    Private Sub TxtTravelCat__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTravelCat._MYValidating
        Dim Whr As String = String.Empty

        TxtTravelCat.Value = ClsHRTravelCategoryMaster.GetFinder("", TxtTravelCat.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTravelCat.Value) > 0 Then
            LblTravelCat.Text = clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') AS Description FROM TSPL_HR_TRAVEL_CATEGORY_MASTER WHERE Travel_Category_Code='" + TxtTravelCat.Value + "'")
        Else
            LblTravelCat.Text = ""
        End If
       
    End Sub
    Private Sub TxtTravelPurpose__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTravelPurpose._MYValidating
        TxtTravelPurpose.Value = ClsHRTravelPurposeMaster.GetFinder("", TxtTravelPurpose.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTravelPurpose.Value) > 0 Then
            LblTravelPurpose.Text = clsDBFuncationality.getSingleValue("select Travel_Desp from TSPL_HR_TRAVEL_PURPOSE_MASTER where Travel_Code='" + TxtTravelPurpose.Value + "'")
        Else
            LblTravelPurpose.Text = ""
        End If
    End Sub
    Private Sub TxtTravelBookingBy__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTravelBookingBy._MYValidating
        TxtTravelBookingBy.Value = clsEmployeeMaster.getFinder("", TxtTravelBookingBy.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTravelBookingBy.Value) > 0 Then
            LblTravelBookingBy.Text = clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + TxtTravelBookingBy.Value + "'")
        Else
            LblTravelBookingBy.Text = ""
        End If
    End Sub
    Private Sub TxtTravelBookingFor__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtTravelBookingFor._MYValidating
        TxtTravelBookingFor.Value = clsEmployeeMaster.getFinder("", TxtTravelBookingFor.Value.Replace("'", ""), isButtonClicked)
        If clsCommon.myLen(TxtTravelBookingFor.Value) > 0 Then
            LblTravelBookingFor.Text = clsDBFuncationality.getSingleValue("SELECT Emp_Name FROM TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + TxtTravelBookingFor.Value + "'")
        Else
            LblTravelBookingFor.Text = ""
        End If
    End Sub

    Private Sub BtnShow_Click(sender As Object, e As EventArgs) Handles BtnShow.Click

        Dim WhrCls As String = String.Empty
        Dim TravelPur As String = String.Empty
        Dim TravelCat As String = String.Empty
        Dim BookingFor As String = String.Empty
        Dim BookingBy As String = String.Empty
        Dim TravelType As String = String.Empty


        WhrCls = " WHERE 2=2 "
        If clsCommon.myLen(TxtTravelPurpose.Value) > 0 Then
            WhrCls += " AND Travel_Purpose_Code='" & clsCommon.myCstr(TxtTravelPurpose.Value) & "'"
        End If
        If clsCommon.myLen(TxtTravelCat.Value) > 0 Then
            WhrCls += " AND Travel_Category_Code='" & clsCommon.myCstr(TxtTravelCat.Value) & "'"
        End If
        If clsCommon.myLen(TxtTravelBookingBy.Value) > 0 Then
            WhrCls += " AND Booked_By_Name_Code='" & clsCommon.myCstr(TxtTravelBookingBy.Value) & "'"
        End If
        If clsCommon.myLen(TxtTravelBookingFor.Value) > 0 Then
            WhrCls += " AND Booking_For_Code='" & clsCommon.myCstr(TxtTravelBookingFor.Value) & "'"
        End If
        If clsCommon.myLen(cmbTravelType.SelectedValue) > 0 Then
            If clsCommon.CompairString(cmbTravelType.SelectedValue, "D") = CompairStringResult.Equal Then
                WhrCls += " AND Is_Domesctic =1"
            ElseIf clsCommon.CompairString(cmbTravelType.SelectedValue, "I") = CompairStringResult.Equal Then
                WhrCls += " AND Is_International =1"
            End If
        End If
        If ChkSearchDate.Checked = True Then
            WhrCls += " AND convert(date ,DOCUMENT_DATE,103) >= '" & clsCommon.GetPrintDate(dtpFromDate.Value, "dd/MMM/yyyy") & "' AND convert(date ,DOCUMENT_DATE,103) <= '" & clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") & "'"
        End If

        If clsCommon.myLen(CmbStatus.SelectedValue) > 0 Then
            If clsCommon.CompairString(CmbStatus.SelectedValue, "A") = CompairStringResult.Equal Then
                WhrCls += " AND Approved =1"
            ElseIf clsCommon.CompairString(CmbStatus.SelectedValue, "R") = CompairStringResult.Equal Then
                WhrCls += " AND Rejected =1"
            End If
        End If

        LoadData(WhrCls)
    End Sub

    Private Sub ChkSearchDate_CheckStateChanged(sender As Object, e As EventArgs) Handles ChkSearchDate.CheckStateChanged
        If ChkSearchDate.Checked = True Then
            GrpSearchDate.Enabled = True
        Else
            GrpSearchDate.Enabled = False
            dtpFromDate.Value = clsCommon.GETSERVERDATE()
            dtpToDate.Value = clsCommon.GETSERVERDATE()
        End If
    End Sub
End Class
