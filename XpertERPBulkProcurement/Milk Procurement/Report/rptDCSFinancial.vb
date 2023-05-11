Imports common
Imports System.ComponentModel
Imports System.IO

'by Sanjay - Create new report 
Public Class rptDCSFinancial
    Inherits FrmMainTranScreen

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Dim StrPermission As String
    Private Sub rptMilkCostReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        StrPermission = clsERPFuncationality.UserWiseAvailableLocationCode()
        txtFiscalYear.Value = objCommonVar.CurrFiscalYear
        LoadType()
        Reset()
    End Sub

    Sub LoadType()

        cboType.DataSource = clsDCSFinancialHead.GetHeadType()
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim qry As String = Nothing
            Dim PrevFiscalCode As String = ""
            If clsCommon.myLen(txtFiscalYear.Value) <= 0 Then
                txtFiscalYear.Focus()
                Throw New Exception("Please Select Fiscal Year First.")
            End If

            If clsCommon.myLen(txtDCS.Value) <= 0 Then
                txtFiscalYear.Focus()
                Throw New Exception("Please Select DCS.")
            End If
            If clsCommon.myLen(cboType.SelectedValue) <= 0 Then
                cboType.Focus()
                Throw New Exception("Please Select Type.")
            End If

            qry = "select top 1 Fiscal_Code from TSPL_Fiscal_Year_Master where Fiscal_Code<'" + txtFiscalYear.Value + "' order by Fiscal_Code desc"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                PrevFiscalCode = clsCommon.myCstr(dt.Rows(0)("Fiscal_Code"))
            End If

            qry = " select TSPL_Fiscal_Year_MasterPrev.Fiscal_Name as Fiscal_NamePrev,TSPL_Fiscal_Year_MasterCurr.Fiscal_Name as Fiscal_NameCurr,(case when xxx.Type='TA' then N'व्यापार खाता' else (case when xxx.Type='P&L' then N'हानि-लाभ खाता' else (case when xxx.Type='BS' then N'संतुलन चित्र' else '' end) end) end) +
N' दिनांक '+convert(Varchar, TSPL_Fiscal_Year_MasterCurr.Start_Date,103) +N' से '+convert(varchar,TSPL_Fiscal_Year_MasterCurr.End_Date,103) as ReportName,xxx.DCS_Code,case when TSPL_VLC_MASTER_HEAD.VLC_Name_Hindi is not null then TSPL_VLC_MASTER_HEAD.VLC_Name_Hindi else  TSPL_VLC_MASTER_HEAD.VLC_Name  end as DCSName,(case when xxx.Type='TA' then N'क्रय' else (case when xxx.Type='P&L' then N'हानि' else (case when xxx.Type='BS' then N'लेनदारी' else '' end) end) end) as Head_1,
xxx.Head_Code1,TSPL_DCS_FINANCIAL_HEAD1.Description as Head_Name1,xxx.Prev_Head_Amount1,xxx.Head_Sub_Amount1,xxx.Head_Amount1
,(case when xxx.Type='TA' then N'विक्रय' else (case when xxx.Type='P&L' then N'लाभ' else (case when xxx.Type='BS' then N'देनदारी' else '' end) end) end) as Head_2,xxx.Head_Code2,TSPL_DCS_FINANCIAL_HEAD2.Description as Head_Name2,xxx.Prev_Head_Amount2,xxx.Head_Sub_Amount2,xxx.Head_Amount2,N' '+xxx.Remarks as Remarks 

from(
select xx.DCS_Code,xx.Head_Code1,sum(xx.Head_Amount1 * case when xx.Curr=1 then 0 else 1 end) as Prev_Head_Amount1,sum(xx.Head_Sub_Amount1* case when xx.Curr=1 then 1 else 0 end) as Head_Sub_Amount1,sum(xx.Head_Amount1* case when xx.Curr=1 then 1 else 0 end) as Head_Amount1
,xx.Head_Code2,sum(xx.Head_Amount2 * case when xx.Curr=1 then 0 else 1 end) as Prev_Head_Amount2,sum(xx.Head_Sub_Amount2* case when xx.Curr=1 then 1 else 0 end) as Head_Sub_Amount2,sum(xx.Head_Amount2* case when xx.Curr=1 then 1 else 0 end) as Head_Amount2,max(PK_Id) as PK_Id,max(case when xx.Curr=1 then xx.Remarks else '' end ) as Remarks
,max(case when xx.Curr=1 then xx.Type end) as Type
from (

select TSPL_DCS_FINANCIAL_ENTRY.DCS_Code,TSPL_DCS_FINANCIAL_ENTRY.Fiscal_Code,TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Head_Code1,case when Head_Sub_Amount1=0 then null else Head_Sub_Amount1 end Head_Sub_Amount1,case when Head_Amount1=0 then null else Head_Amount1 end as Head_Amount1,TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Head_Code2 ,case when Head_Sub_Amount2 =0 then null else Head_Sub_Amount2 end Head_Sub_Amount2,case when Head_Amount2=0 then null else Head_Amount2 end as Head_Amount2,PK_Id,1 as Curr,TSPL_DCS_FINANCIAL_ENTRY.Remarks,TSPL_DCS_FINANCIAL_ENTRY.Type
from TSPL_DCS_FINANCIAL_ENTRY_DETAIL
left outer join TSPL_DCS_FINANCIAL_ENTRY on TSPL_DCS_FINANCIAL_ENTRY.Document_Code=TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Document_Code
where TSPL_DCS_FINANCIAL_ENTRY.DCS_Code='" + txtDCS.Value + "' and TSPL_DCS_FINANCIAL_ENTRY.Fiscal_Code='" + txtFiscalYear.Value + "' and TSPL_DCS_FINANCIAL_ENTRY.Type='" + clsCommon.myCstr(cboType.SelectedValue) + "' 

union all

select TSPL_DCS_FINANCIAL_ENTRY.DCS_Code,TSPL_DCS_FINANCIAL_ENTRY.Fiscal_Code,TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Head_Code1,case when Head_Sub_Amount1=0 then null else Head_Sub_Amount1 end Head_Sub_Amount1,case when Head_Amount1=0 then null else Head_Amount1 end as Head_Amount1,TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Head_Code2 ,case when Head_Sub_Amount2 =0 then null else Head_Sub_Amount2 end Head_Sub_Amount2,case when Head_Amount2=0 then null else Head_Amount2 end as Head_Amount2,0 as PK_Id,0 as Curr,TSPL_DCS_FINANCIAL_ENTRY.Remarks,TSPL_DCS_FINANCIAL_ENTRY.Type
from TSPL_DCS_FINANCIAL_ENTRY_DETAIL
left outer join TSPL_DCS_FINANCIAL_ENTRY on TSPL_DCS_FINANCIAL_ENTRY.Document_Code=TSPL_DCS_FINANCIAL_ENTRY_DETAIL.Document_Code
where TSPL_DCS_FINANCIAL_ENTRY.DCS_Code='" + txtDCS.Value + "' and TSPL_DCS_FINANCIAL_ENTRY.Fiscal_Code='" + PrevFiscalCode + "' and TSPL_DCS_FINANCIAL_ENTRY.Type='" + clsCommon.myCstr(cboType.SelectedValue) + "' 

)xx group by xx.DCS_Code,xx.Head_Code1,xx.Head_Code2
)xxx 
left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=xxx.DCS_Code
left outer join TSPL_Fiscal_Year_Master as TSPL_Fiscal_Year_MasterPrev on TSPL_Fiscal_Year_MasterPrev.Fiscal_Code='" + PrevFiscalCode + "'
left outer join TSPL_Fiscal_Year_Master as TSPL_Fiscal_Year_MasterCurr on TSPL_Fiscal_Year_MasterCurr.Fiscal_Code='" + txtFiscalYear.Value + "'
left outer join TSPL_DCS_FINANCIAL_HEAD as  TSPL_DCS_FINANCIAL_HEAD1 on TSPL_DCS_FINANCIAL_HEAD1.Code=xxx.Head_Code1
left outer join TSPL_DCS_FINANCIAL_HEAD as  TSPL_DCS_FINANCIAL_HEAD2 on TSPL_DCS_FINANCIAL_HEAD2.Code=xxx.Head_Code2

order by PK_Id "
            dt = Nothing
            dt = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            Else
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptDCSFinancialEntry", "DCS Finalcial Entry")
                frmCRV = Nothing
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub






    Private Sub txtDCS__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDCS._MYValidating
        Try
            Dim qry As String = "Select TSPL_VLC_MASTER_HEAD.VLC_Code as DCSCode,TSPL_VLC_MASTER_HEAD.VLC_Code_vlc_Uploader as [Uploader Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [DCS Name],TSPL_VLC_MASTER_HEAD.MCC as [MCC Code],TSPL_VLC_MASTER_HEAD.Route_Code as [Route Code]   from TSPL_VLC_MASTER_HEAD "
            txtDCS.Value = clsCommon.ShowSelectForm("DCSFERFY#R", qry, "DCSCode", "", txtDCS.Value, "", isButtonClicked)
            lblDCSName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select VLC_Name from TSPL_VLC_MASTER_HEAD where VLC_Code ='" + txtDCS.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub txtFiscalYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFiscalYear._MYValidating
        Try
            Dim qry As String = "select Fiscal_Code,Fiscal_Name,Start_Date,End_Date from TSPL_FISCAL_YEAR_MASTER"
            txtFiscalYear.Value = clsCommon.ShowSelectForm("DCSFERFY#R", qry, "Fiscal_Code", "", txtFiscalYear.Value, "", isButtonClicked)
            lblFiscalYear.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Fiscal_Name from TSPL_FISCAL_YEAR_MASTER where Fiscal_Code ='" + txtFiscalYear.Value + "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
End Class
