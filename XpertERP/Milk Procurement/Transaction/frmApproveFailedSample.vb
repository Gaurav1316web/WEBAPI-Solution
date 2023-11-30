Imports common
Imports System.Data.SqlClient

Public Class frmApproveFailedSample
    Inherits FrmMainTranScreen
    ''BHA/09/05/18-000018 by balwinder on 20/06/2018
#Region "Variables"
    Const Modul As String = "Module"
    Const Transaction As String = "Transaction"
    Const ApprovedDoc As String = "No of Approved Doc"
    Const UnApprovedDoc As String = "No of UnApproved Doc"
    Dim ButtonTooltip As New ToolTip()
    Dim DtIncentive As DataTable
    Dim Formcode As String
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False
    Dim isPickPendingMilkSRNinNextPaymentCycle As Boolean = False
    Dim isStopVSPBillIfSomethingWrong As Boolean = False
    Dim IsRoundOffPaiseAmount As Boolean = False
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.MilkVSPIssuePayment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Is_Load = True
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnsave, "Press Alt+S for Refresh the Data")
        SetUserMgmtNew()
        txtMonth.Value = clsCommon.GETSERVERDATE()
        Is_Load = False
        AllowDateChanged = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmMilkVSPPayment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S Then
            SaveData()
        End If
    End Sub

    Private Sub DtpDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonth.ValueChanged
        Try
            AllowDateChanged = False
            txtFromDate.MinDate = "01-Jan-0001"
            txtFromDate.MaxDate = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtFromDate.MinDate = "01-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtToDate.Value = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            AllowDateChanged = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DtpFromDate_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFromDate.ValueChanged
        SetToDate()
    End Sub

    Sub SetToDate()
        Try
            If AllowDateChanged Then
                If Is_Load = False Then
                    If clsCommon.myLen(txtMCC.Value) <= 0 Then
                        txtMCC.Focus()
                        Throw New Exception("Please select Mcc First.")
                    End If
                End If

                Dim sQuery As String = "select Pc_Type as Type,PC_VALUE as Value, case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
              & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & txtMCC.Value & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set payment cycle in Mcc master")
                End If
                lblPaymentType.Text = clsCommon.myCstr(dt.Rows(0)("Type"))
                lblPaymentType.Tag = clsCommon.myCdbl(dt.Rows(0)("Value"))
                If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Type")), "Week") = CompairStringResult.Equal Then
                    AllowDateChanged = False
                    txtMonth.Enabled = False
                    txtFromDate.MinDate = New Date(2000, 1, 1)
                    txtFromDate.MaxDate = New Date(3000, 1, 1).AddDays(-1)
                    Dim today As Date = txtFromDate.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(lblPaymentType.Tag = 1, DayOfWeek.Sunday, IIf(lblPaymentType.Tag = 2, DayOfWeek.Monday, IIf(lblPaymentType.Tag = 3, DayOfWeek.Tuesday, IIf(lblPaymentType.Tag = 4, DayOfWeek.Wednesday, IIf(lblPaymentType.Tag = 5, DayOfWeek.Thursday, IIf(lblPaymentType.Tag = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    txtFromDate.Value = today.AddDays(-dayDiff)
                    txtToDate.Value = txtFromDate.Value.AddDays(6)
                    AllowDateChanged = True
                Else
                    txtMonth.Enabled = True
                    Dim PaymentCycleValue As Integer = dt.Rows(0)("Pc_Value")
                    If txtFromDate.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        AllowDateChanged = False
                        clsCommon.MyMessageBoxShow("Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                        txtFromDate.Value = txtFromDate.MinDate
                        txtFromDate.Text = txtFromDate.MinDate
                        AllowDateChanged = True
                    End If
                    txtToDate.Value = txtFromDate.Value.AddDays(PaymentCycleValue - 1)
                    If txtFromDate.Value.Month <> txtToDate.Value.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    Dim dtNxtPay As DateTime = txtToDate.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                    If txtFromDate.Value.Month <> dtNxtPay.Month Then
                        txtToDate.Value = New Date(txtFromDate.Value.Year, txtFromDate.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                End If
                gv1.DataSource = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on location_Code= mcc_Code " _
        & " and (loc_segment_Code in (" & arrLoc & ") or mcc_Code in (" & arrLoc & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("VSPPMCCFn", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        lblMCC.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"))
        SetToDate()
        gv1.DataSource = Nothing
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        LoadData()
    End Sub

    Sub LoadData()
        Try
            gv1.DataSource = Nothing
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If

            Dim qry As String = "select cast(isnull(TSPL_MILK_SRN_HEAD.Failed_Sample_Status,0) as bit) as Failed_Sample_Status,cast(isnull(TSPL_MILK_SRN_HEAD.Failed_Sample_Status,0) as bit) as Failed_Sample_StatusOLD,TSPL_MILK_SRN_HEAD.DOC_CODE,TSPL_MILK_SRN_HEAD.DOC_DATE,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VENDOR_MASTER.Vendor_Name, TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_MILK_SRN_HEAD.ROUTE_CODE,TSPL_MCC_ROUTE_MASTER.Route_Name,TSPL_MILK_SRN_DETAIL.FAT_PER,TSPL_MILK_SRN_DETAIL.SNF_PER,TSPL_MILK_SRN_DETAIL.NET_AMOUNT,TSPL_MILK_SRN_HEAD.Failed_Sample_Approve_By,TSPL_MILK_SRN_HEAD.Failed_Sample_Approve_Date" + Environment.NewLine + _
            " from TSPL_MILK_SRN_DETAIL" + Environment.NewLine + _
            " left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE" + Environment.NewLine + _
            " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE" + Environment.NewLine + _
            "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_SRN_HEAD.VSP_CODE" + Environment.NewLine + _
            "left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE" + Environment.NewLine + _
            "left outer join TSPL_MCC_ROUTE_MASTER on TSPL_MCC_ROUTE_MASTER.Route_Code=TSPL_MILK_SRN_HEAD.ROUTE_CODE" + Environment.NewLine + _
            " where Against_Reject_No is null and TSPL_MILK_SRN_HEAD.DOC_DATE>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_MILK_SRN_HEAD.DOC_DATE<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'" + Environment.NewLine + _
            " and TSPL_MILK_SRN_HEAD.MCC_CODE='" + txtMCC.Value + "' and TSPL_MCC_MASTER.Failed_Sample_Apply=1 and " + Environment.NewLine + _
            " (TSPL_MILK_SRN_DETAIL.FAT_PER<TSPL_MCC_MASTER.Failed_Sample_FAT or TSPL_MILK_SRN_DETAIL.SNF_PER<TSPL_MCC_MASTER.Failed_Sample_SNF )"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No data found")
            End If
            gv1.DataSource = dt
            gv1.EnableFiltering = True
            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = True
            Next
            gv1.Columns("Failed_Sample_Status").ReadOnly = False
            gv1.Columns("Failed_Sample_Status").Width = 20
            gv1.Columns("Failed_Sample_Status").HeaderText = " "

            gv1.Columns("Failed_Sample_StatusOLD").IsVisible = False
            gv1.Columns("Failed_Sample_StatusOLD").HeaderText = "OLD Status"

            gv1.Columns("DOC_CODE").Width = 100
            gv1.Columns("DOC_CODE").HeaderText = "SRN Code"

            gv1.Columns("DOC_DATE").Width = 100
            gv1.Columns("DOC_DATE").HeaderText = "SRN Date"

            gv1.Columns("VSP_CODE").Width = 100
            gv1.Columns("VSP_CODE").HeaderText = "VSP Code"

            gv1.Columns("Vendor_Name").Width = 100
            gv1.Columns("Vendor_Name").HeaderText = "VSP Name"

            gv1.Columns("VLC_CODE").Width = 100
            gv1.Columns("VLC_CODE").HeaderText = "VLC Code"

            gv1.Columns("VLC_Name").Width = 100
            gv1.Columns("VLC_Name").HeaderText = "VLC Name"

            gv1.Columns("VLC_Code_VLC_Uploader").Width = 100
            gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "VLC Uploader Code"

            gv1.Columns("ROUTE_CODE").Width = 100
            gv1.Columns("ROUTE_CODE").HeaderText = "Route Code"

            gv1.Columns("Route_Name").Width = 100
            gv1.Columns("Route_Name").HeaderText = "Route Name"
           

            gv1.Columns("FAT_PER").Width = 100
            gv1.Columns("FAT_PER").HeaderText = "FAT %"

            gv1.Columns("SNF_PER").Width = 100
            gv1.Columns("SNF_PER").HeaderText = "SRN %"

            gv1.Columns("NET_AMOUNT").Width = 100
            gv1.Columns("NET_AMOUNT").HeaderText = "SRN Amount"

            gv1.Columns("Failed_Sample_Approve_By").Width = 100
            gv1.Columns("Failed_Sample_Approve_By").HeaderText = "Approved By"


            gv1.Columns("Failed_Sample_Approve_Date").Width = 100
            gv1.Columns("Failed_Sample_Approve_Date").HeaderText = "Approved On"

            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.AllowColumnReorder = True
            gv1.AllowRowReorder = False
            gv1.EnableSorting = False
            gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.TableElement.TableHeaderHeight = 40
            gv1.AllowDeleteRow = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData()
        Try
            Dim arrSRNNo As New Dictionary(Of String, Boolean)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myCBool(gv1.Rows(ii).Cells("Failed_Sample_Status").Value) <> clsCommon.myCBool(gv1.Rows(ii).Cells("Failed_Sample_StatusOLD").Value) Then
                    arrSRNNo.Add(clsCommon.myCstr(gv1.Rows(ii).Cells("DOC_CODE").Value), clsCommon.myCBool(gv1.Rows(ii).Cells("Failed_Sample_Status").Value))
                End If
            Next
            clsMilkSRNMCC.SaveFailedSampleApproveData(arrSRNNo, txtFromDate.Value, txtToDate.Value)
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
