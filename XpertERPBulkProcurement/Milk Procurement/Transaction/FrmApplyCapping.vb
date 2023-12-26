Imports common
Imports System.Data.SqlClient

Public Class FrmApplyCapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonTooltip As New ToolTip()
    Dim Is_Load As Boolean = False
    Dim AllowDateChanged As Boolean = False
#End Region

    Public Sub New(ByVal FormId As String)
        InitializeComponent()
    End Sub

    Private Sub FrmMilkVSPPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Is_Load = True
        ButtonTooltip.SetToolTip(btnClose, "Press Alt+C for Close the Window")
        ButtonTooltip.SetToolTip(btnGenerateBill, "Press Alt+R for Refresh the Data")
        SetUserMgmtNew()

        txtMonth.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = New Date(txtMonth.Value.Year, txtMonth.Value.Month, 1)
        txtToDate.Value = txtFromDate.Value
        Is_Load = False
        AllowDateChanged = True
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Private Sub txtMCC__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMCC._MYValidating
        Dim qry As String = ""
        Dim arrLoc As String = ""
        Dim obj As New clsMCCCodes()
        obj = clsMCCCodes.GetData(True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
            arrLoc = obj.arrLocCodes
        End If

        qry = "select * from ( select Mcc_Code as [Code],MCC_Name as [Name] from tspl_mcc_master inner join tspl_location_master on tspl_location_master.location_Code= tspl_mcc_master.mcc_Code " _
        & " and (tspl_location_master.loc_segment_Code in (" & arrLoc & ") or tspl_mcc_master.mcc_Code in (" & arrLoc & ")))xx "

        txtMCC.Value = clsCommon.ShowSelectForm("VSPPMCCFn", qry, "Code", "", txtMCC.Value, "", isButtonClicked)
        qry = "select Non_Company_VSP_Deduction,Company_VSP_Deduction,MCC_Name from tspl_mcc_master where mcc_Code='" + txtMCC.Value + "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            lblMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC_Name"))
        Else
            lblMCC.Text = ""
        End If
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
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub txtVSP_My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                txtMCC.Focus()
                Throw New Exception("Please select MCC")
            End If
            Dim arrMCC As New ArrayList
            arrMCC.Add(txtMCC.Value)
            Dim qry As String = VSPQry(arrMCC, Nothing)
            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "VSPPVLF", qry, "VLC_CODE", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Function VSPQry(ByVal arrMCC As ArrayList, ByVal arrVSP As ArrayList) As String
        Return clsVSPBillAndIncentiveCalculation.VSPQry(arrMCC, arrVSP, False, txtFromDate.Value, txtToDate.Value, "")
    End Function

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtMCC.Value) <= 0 Then
                Throw New Exception("Please select MCC")
            End If
            If txtVSP.arrValueMember Is Nothing OrElse txtVSP.arrValueMember.Count <= 0 Then
                Throw New Exception("Please select at lease One VSP")
            End If
            Dim qry As String = "select * from (
select xxx.*,TSPL_CAPPING.FAT as CappingFAT,TSPL_CAPPING.SNF as CappingSNF
,case when xxx.Farmer_FAT-xxx.FAT_PER>=0 then (case when  xxx.FAT_PER+TSPL_CAPPING.FAT > xxx.Farmer_FAT then xxx.Farmer_FAT else xxx.FAT_PER+TSPL_CAPPING.FAT end ) else xxx.FAT_PER end as NewFAT
,case when xxx.Farmer_SNF-xxx.SNF_PER>=0 then (case when  xxx.SNF_PER+TSPL_CAPPING.SNF > xxx.Farmer_SNF then xxx.Farmer_SNF else xxx.SNF_PER+TSPL_CAPPING.SNF end )else xxx.SNF_PER end as NewSNF
from (
select TSPL_MILK_SRN_HEAD.MCC_CODE,TSPL_MILK_SRN_HEAD.VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as UploaderCode,TSPL_MILK_SRN_HEAD.VSP_CODE,TSPL_VENDOR_MASTER.Vendor_Name as VSPName,TSPL_MILK_SRN_HEAD.Doc_Code, convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) as DOC_DATE,TSPL_MILK_SRN_HEAD.SHIFT,TSPL_MILK_SRN_DETAIL.Qty,FAT_PER,SNF_PER ,TabFarmer.Farmer_FAT,TabFarmer.Farmer_SNF
,(select top 1 case when TSPL_CAPPING.Inactive=0 then TSPL_CAPPING.Code else '' end as  Code
from TSPL_CAPPING 
left outer join TSPL_CAPPING_VSP on TSPL_CAPPING_VSP.Code = TSPL_CAPPING.Code
left outer join TSPL_CAPPING_MCC on TSPL_CAPPING_MCC.Code = TSPL_CAPPING.Code
where TSPL_CAPPING_VSP.VSP_Code=TSPL_MILK_SRN_HEAD.VSP_CODE  and  TSPL_CAPPING_MCC.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE
and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)>=TSPL_CAPPING.Start_Date  
and 2= (case when TSPL_CAPPING.Start_Shift='E' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=TSPL_CAPPING.Start_Date and TSPL_MILK_SRN_HEAD.SHIFT='M' then 3 else 2 end)
and (2= case when TSPL_CAPPING.End_Date is null then 2 else case when ((convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)<= TSPL_CAPPING.End_Date )
and (2= case when TSPL_CAPPING.End_Shift='M' and convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103)=TSPL_CAPPING.End_Date and TSPL_MILK_SRN_HEAD.SHIFT='E' then 3 else 2 end)) then 2 else 3 end end)  
and TSPL_CAPPING.Posted=1 order by TSPL_CAPPING.Start_Date desc,TSPL_CAPPING.Code desc) as CappingCode 
from TSPL_MILK_SRN_DETAIL 
left outer join TSPL_MILK_SRN_HEAD on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MILK_SRN_HEAD.VSP_CODE
left outer join (select mcc_Code,VLC_code, doc_date,shift,Farmer_Qty,Farmer_FAT_KG,Farmer_SNF_KG,case when Farmer_Qty>0 then cast(Farmer_FAT_KG*100/Farmer_Qty as decimal(18,1)) else 0 end as Farmer_FAT,case when Farmer_Qty>0 then cast(Farmer_SNF_KG*100/Farmer_Qty as decimal(18,1)) else 0 end as Farmer_SNF from (
select mcc_Code,VLC_code, doc_date,shift
,cast(sum(Qty * case when RI in (2,3) then 1 else 0 end )as decimal(18,2)) as Farmer_Qty
,cast( sum(FAT_KG * case when RI in (2,3) then 1 else 0 end ) as decimal(18,2))as Farmer_FAT_KG
,cast(sum(SNF_KG * case when RI in (2,3) then 1 else 0 end ) as decimal(18,2)) as Farmer_SNF_KG
from (
Select  TSPL_VLC_DATA_UPLOADER.MCC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Code,convert(date,File_Date,103) as DOC_DATE ,shift,isnull(TSPL_VLC_DATA_UPLOADER.qty,0) as Qty,((TSPL_VLC_DATA_UPLOADER.qty*TSPL_VLC_DATA_UPLOADER.fat/100 )) as FAT_KG,((TSPL_VLC_DATA_UPLOADER.qty*TSPL_VLC_DATA_UPLOADER.snf/100 ))  as SNF_KG , isnull(TSPL_VLC_DATA_UPLOADER.Amount,0) as AMOUNT,0 as Chk,2 as RI   
from TSPL_VLC_DATA_UPLOADER   
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE  and TSPL_VLC_MASTER_HEAD.MCC=TSPL_VLC_DATA_UPLOADER.MCC_Code 
left join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code_VLC_Uploader =TSPL_VLC_DATA_UPLOADER.MP_CODE  and TSPL_MP_MASTER. VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_CODE  
where 2 = 2  and  convert(date, File_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, File_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'  and TSPL_VLC_MASTER_HEAD.VLC_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")
	union all
select TSPL_VLC_MASTER_HEAD.MCC,TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code,convert(date,TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) as Document_Date,(case when TSPL_VLC_DATA_UPLOADER_MASTER.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift,
    TSPL_VLC_DATA_UPLOADER_DETAIL.Qty,(((TSPL_VLC_DATA_UPLOADER_DETAIL.Qty*TSPL_VLC_DATA_UPLOADER_DETAIL.FatPer)/100))  as FAT_KG ,(((TSPL_VLC_DATA_UPLOADER_DETAIL.Qty*TSPL_VLC_DATA_UPLOADER_DETAIL.SNFPer)/100)) as SNF_KG  ,  TSPL_VLC_DATA_UPLOADER_DETAIL.Amount as VLC_Amount,0 as Chk,3 as RI  
from TSPL_VLC_DATA_UPLOADER_DETAIL   
inner join TSPL_VLC_DATA_UPLOADER_MASTER on TSPL_VLC_DATA_UPLOADER_DETAIL.Document_Code=TSPL_VLC_DATA_UPLOADER_MASTER.Document_Code  
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code=TSPL_VLC_MASTER_HEAD.VLC_Code  
where 2 = 2  and  convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) >=  '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and  convert(date, TSPL_VLC_DATA_UPLOADER_MASTER.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_VLC_DATA_UPLOADER_MASTER.VLC_Code  IN (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") 
)x group by mcc_Code,VLC_code, doc_date,shift
)xx) as TabFarmer on TabFarmer.MCC_Code=TSPL_MILK_SRN_HEAD.MCC_CODE and TabFarmer.VLC_Code=TSPL_MILK_SRN_HEAD.VLC_CODE and  TabFarmer.DOC_DATE=convert(date, TSPL_MILK_SRN_HEAD.DOC_DATE,103) and TabFarmer.shift=TSPL_MILK_SRN_HEAD.SHIFT
where isnull(TSPL_MILK_SRN_HEAD.Capping_Apply,0)=0 and  TSPL_MILK_SRN_HEAD.DOC_DATE >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and  TSPL_MILK_SRN_HEAD.DOC_DATE <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_MILK_SRN_HEAD.MCC_CODE ='" + txtMCC.Value + "' and TSPL_MILK_SRN_HEAD.VLC_CODE in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") 
and TabFarmer.Farmer_FAT  is not null and not exists(select 1 from TSPL_MILK_PURCHASE_INVOICE_DETAIL where TSPL_MILK_PURCHASE_INVOICE_DETAIL.SRN_CODE=TSPL_MILK_SRN_HEAD.DOC_CODE)
)xxx 
left outer join TSPL_CAPPING on TSPL_CAPPING.Code=xxx.CappingCode
where xxx.CappingCode is not null and (xxx.Farmer_FAT-xxx.FAT_PER>=0 or xxx.Farmer_SNF-xxx.SNF_PER>=0)
)XXXX where ( FAT_PER<>NewFAT or  SNF_PER<>NewSNF) order by UploaderCode,DOC_DATE,SHIFT desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            gvDetail.DataSource = Nothing
            gvDetail.DataSource = dt
            gvDetail.BestFitColumns()
            gvDetail.ReadOnly = True
            SetGridFormationOFGV1()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gvDetail.ShowGroupPanel = False
        gvDetail.GroupDescriptors.Clear()
        gvDetail.TableElement.TableHeaderHeight = 40
        gvDetail.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gvDetail.Columns.Count - 1
            gvDetail.Columns(ii).ReadOnly = True
            gvDetail.Columns(ii).IsVisible = False
        Next


        gvDetail.Columns("MCC_CODE").HeaderText = "MCC"


        gvDetail.Columns("VLC_CODE").IsVisible = True
        gvDetail.Columns("VLC_CODE").Width = 100
        gvDetail.Columns("VLC_CODE").HeaderText = "VLC"


        gvDetail.Columns("VLC_Name").IsVisible = True
        gvDetail.Columns("VLC_Name").Width = 150
        gvDetail.Columns("VLC_Name").HeaderText = "VLC Name"

        gvDetail.Columns("UploaderCode").IsVisible = True
        gvDetail.Columns("UploaderCode").Width = 80
        gvDetail.Columns("UploaderCode").HeaderText = "Uploader Code"

        gvDetail.Columns("VSP_CODE").IsVisible = True
        gvDetail.Columns("VSP_CODE").Width = 80
        gvDetail.Columns("VSP_CODE").HeaderText = "VSP"

        gvDetail.Columns("VSPName").IsVisible = True
        gvDetail.Columns("VSPName").Width = 150
        gvDetail.Columns("VSPName").HeaderText = "VSP Name"

        gvDetail.Columns("Doc_Code").IsVisible = True
        gvDetail.Columns("Doc_Code").Width = 100
        gvDetail.Columns("Doc_Code").HeaderText = "SRN No"


        gvDetail.Columns("DOC_DATE").IsVisible = True
        gvDetail.Columns("DOC_DATE").Width = 80
        gvDetail.Columns("DOC_DATE").HeaderText = "SRN Date"
        gvDetail.Columns("DOC_DATE").FormatString = "{0:dd/MM/yyyy}"

        gvDetail.Columns("SHIFT").IsVisible = True
        gvDetail.Columns("SHIFT").Width = 50
        gvDetail.Columns("SHIFT").HeaderText = "Shift"


        gvDetail.Columns("Qty").IsVisible = True
        gvDetail.Columns("Qty").Width = 100
        gvDetail.Columns("Qty").HeaderText = "Qty"
        gvDetail.Columns("Qty").FormatString = "{0:n2}"


        gvDetail.Columns("FAT_PER").IsVisible = True
        gvDetail.Columns("FAT_PER").Width = 80
        gvDetail.Columns("FAT_PER").HeaderText = "SRN FAT %"
        gvDetail.Columns("FAT_PER").FormatString = "{0:n2}"


        gvDetail.Columns("SNF_PER").IsVisible = True
        gvDetail.Columns("SNF_PER").Width = 80
        gvDetail.Columns("SNF_PER").HeaderText = "SRN SNF %"
        gvDetail.Columns("SNF_PER").FormatString = "{0:n2}"


        gvDetail.Columns("Farmer_FAT").IsVisible = True
        gvDetail.Columns("Farmer_FAT").Width = 80
        gvDetail.Columns("Farmer_FAT").HeaderText = "Farmer FAT %"
        gvDetail.Columns("Farmer_FAT").FormatString = "{0:n2}"

        gvDetail.Columns("Farmer_SNF").IsVisible = True
        gvDetail.Columns("Farmer_SNF").Width = 80
        gvDetail.Columns("Farmer_SNF").HeaderText = "Farmer SNF %"
        gvDetail.Columns("Farmer_SNF").FormatString = "{0:n2}"

        gvDetail.Columns("CappingCode").IsVisible = True
        gvDetail.Columns("CappingCode").Width = 150
        gvDetail.Columns("CappingCode").HeaderText = "Capping Code"
        gvDetail.Columns("CappingCode").FormatString = "{0:n2}"


        gvDetail.Columns("CappingFAT").IsVisible = True
        gvDetail.Columns("CappingFAT").Width = 80
        gvDetail.Columns("CappingFAT").HeaderText = "Capping FAT %"
        gvDetail.Columns("CappingFAT").FormatString = "{0:n2}"

        gvDetail.Columns("CappingSNF").IsVisible = True
        gvDetail.Columns("CappingSNF").Width = 80
        gvDetail.Columns("CappingSNF").HeaderText = "Capping SNF %"
        gvDetail.Columns("CappingSNF").FormatString = "{0:n2}"

        gvDetail.Columns("NewFAT").IsVisible = True
        gvDetail.Columns("NewFAT").Width = 80
        gvDetail.Columns("NewFAT").HeaderText = "Final FAT %"
        gvDetail.Columns("NewFAT").FormatString = "{0:n2}"

        gvDetail.Columns("NewSNF").IsVisible = True
        gvDetail.Columns("NewSNF").Width = 80
        gvDetail.Columns("NewSNF").HeaderText = "Final SNF %"
        gvDetail.Columns("NewSNF").FormatString = "{0:n2}"
    End Sub

    Private Sub btnGenerateBill_Click(sender As Object, e As EventArgs) Handles btnGenerateBill.Click
        Try
            Dim flag As Boolean = False
            For ii As Integer = 0 To gvDetail.Rows.Count - 1
                If clsCommon.myLen(gvDetail.Rows(ii).Cells("Doc_Code").Value) > 0 Then
                    clsMilkSRNMCC.Correction(clsCommon.myCstr(gvDetail.Rows(ii).Cells("Doc_Code").Value), False, True, False, 0, "", clsCommon.myCdbl(gvDetail.Rows(ii).Cells("NewFAT").Value), clsCommon.myCdbl(gvDetail.Rows(ii).Cells("NewSNF").Value), "", True)
                End If
                flag = True
            Next
            If flag Then
                clsCommon.MyMessageBoxShow(Me, "Capping Process Completetd", Me.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Capping", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDate.Validating
        SetToDate()
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        SetToDate()
    End Sub
End Class
