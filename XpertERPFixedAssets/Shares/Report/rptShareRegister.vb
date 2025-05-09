Imports XpertERPEngine
Imports common
Imports System.IO
Imports Telerik.WinControls.UI

Public Class rptShareRegister
    Inherits FrmMainTranScreen
    Private Sub rptShareRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub

    Private Sub TxtMultiDCS__My_Click(sender As Object, e As EventArgs) Handles TxtMultiDCS._My_Click
        Try
            Dim qry As String = " Select TSPL_VENDOR_MASTER.Vendor_Code As DCS_Code, TSPL_VENDOR_MASTER.Vendor_Name As DCS_Name,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader As [DCS Uploader Code] from TSPL_VENDOR_MASTER
                                  Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code "
            'Dim qry As String = "select TSPL_VLC_MASTER_HEAD.VLC_Code AS DCS_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as [DCS Uploader Code],TSPL_VLC_MASTER_HEAD.VLC_Name as DCS_Name from TSPL_VLC_MASTER_HEAD "
            'If TxtMultiDCS.arrValueMember IsNot Nothing AndAlso TxtMultiDCS.arrValueMember.Count > 0 Then
            '    qry += " and TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ") "
            'End If

            TxtMultiDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("VLC@VMPIFSC", qry, "DCS_Code", "DCS_Name", TxtMultiDCS.arrValueMember, TxtMultiDCS.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub
    Private Sub LoadData()
        Try
            Dim whrcls As String = ""
            whrcls = "where 2 = 2 and convert(date,TSPL_SHARE_ALLOTMENT.IDate,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_SHARE_ALLOTMENT.IDate,103) <=convert(date,'" + ToDate.Value + "' ,103) "
            If TxtMultiDCS.arrValueMember IsNot Nothing AndAlso TxtMultiDCS.arrValueMember.Count > 0 Then
                whrcls += "  and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ")  "
            End If
            Dim qry As String = ""
            If rbtDCSWise.Checked OrElse rbtSummary.Checked Then
                qry = "SELECT max(XXX.UploaderCode)UploaderCode,xxx.DCSCode,max(xxx.DCSName)DCSName,sum(xxx.OpeningAmount)OpeningAmount,sum(xxx.OpeningSHARE)OpeningSHARE,sum(xxx.CLOSINGSHARE)CLOSINGSHARE,sum(xxx.ClosimgAmt)Closimgamt,sum(xxx.IssueNoShare)IssueNoShare FROM  (SELECT max(TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as UploaderCode, (TSPL_VLC_MASTER_HEAD.VSP_Code) as DCSCode, max( TSPL_VLC_MASTER_HEAD.VLC_Name) as DCSName,
CASE WHEN TSPL_SHARE_ALLOTMENT.IDate < convert(date,'" + fromDate.Value + "',103) THEN sum(TSPL_SHARE_ALLOTMENT.Amount) END AS OpeningAmount,
	CASE WHEN TSPL_SHARE_ALLOTMENT.IDate < convert(date,'" + fromDate.Value + "',103) THEN sum( TSPL_SHARE_ALLOTMENT.qty) END AS OpeningSHARE,
		CASE WHEN TSPL_SHARE_ALLOTMENT.IDate <= convert(date,'" + ToDate.Value + "' ,103) THEN sum( TSPL_SHARE_ALLOTMENT.qty) END AS CLOSINGSHARE,
			CASE WHEN TSPL_SHARE_ALLOTMENT.IDate <= convert(date,'" + ToDate.Value + "' ,103)THEN sum( TSPL_SHARE_ALLOTMENT.Amount) END AS ClosimgAmt
               					, sum(TSPL_SHARE_ALLOTMENT.Amount) as IssueNoShare
                      FROM TSPL_SHARE_ALLOTMENT
                      left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SHARE_ALLOTMENT.DCS_Code
                      left outer join TSPL_SHARE_MASTER on TSPL_SHARE_MASTER.code=TSPL_SHARE_ALLOTMENT.Share_Code
                       Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " & whrcls & "	 group by TSPL_VLC_MASTER_HEAD.VSP_Code,TSPL_SHARE_ALLOTMENT.IDate)XXX group by xxx.DCSCode "

                'where 2 = 2 and convert(date,TSPL_SHARE_MASTER.IDate,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_SHARE_MASTER.IDate,103) <=convert(date,'" + ToDate.Value + "' ,103) or  TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ") "

            ElseIf rbtDetail.Checked Then
                qry = "select code as Document_No,convert(varchar,TSPL_SHARE_ALLOTMENT.IDate,103) AS DocumentDate,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader as DCSUploaderCode, TSPL_VLC_MASTER_HEAD.VSP_Code as DCSCode,TSPL_VLC_MASTER_HEAD.VLC_Name as DCSName,TSPL_SHARE_ALLOTMENT.QTY as NoOfShare,TSPL_SHARE_ALLOTMENT.Amount  from TSPL_SHARE_ALLOTMENT
                      left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SHARE_ALLOTMENT.DCS_Code
                       Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code " & whrcls & ""
                'ElseIf rbtSummary.Checked Then
                '    qry = "  select TSPL_SHARE_ALLOTMENT.code,sum(TSPL_SHARE_ALLOTMENT.qty) as NoofShare, sum( tspl_share_master.amount) as openingamount,sum(TSPL_SHARE_ALLOTMENT.Amount) AS Issueamount,sum(TSPL_SHARE_ALLOTMENT.amount) as ClosingAmout from TSPL_SHARE_ALLOTMENT 
                '           left outer join tspl_share_master on TSPL_SHARE_ALLOTMENT.Share_Code=tspl_share_master.code
                '           left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_SHARE_ALLOTMENT.DCS_Code
                '           Left Outer Join TSPL_VLC_MASTER_HEAD ON TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code  
                '           where 2 = 2 and convert(date,TSPL_SHARE_MASTER.IDate,103)>=convert(date,'" + fromDate.Value + "',103) or convert(date,TSPL_SHARE_MASTER.IDate,103) <=convert(date,'" + ToDate.Value + "' ,103) and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(TxtMultiDCS.arrValueMember) + ")  
                '           group by TSPL_SHARE_ALLOTMENT.code"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)


            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                Gv1.DataSource = Nothing
                Gv1.Rows.Clear()
                Gv1.Columns.Clear()
                Gv1.GroupDescriptors.Clear()
                Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                Gv1.MasterView.Refresh()
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                SetGridFormat()
                'SetGridFormationOFGV1Collection()
                ' View()
                Gv1.BestFitColumns()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            If rbtDCSWise.Checked Then
                Gv1.Columns("DCSCode").IsVisible = False
                Gv1.Columns("UploaderCode").IsVisible = True

                Gv1.Columns("UploaderCode").HeaderText = "DCS Uploader Code "
                Gv1.Columns("OpeningAmount").HeaderText = "OP Amount"
                Gv1.Columns("OpeningSHARE").HeaderText = "OP Share"
                Gv1.Columns("CLOSINGSHARE").HeaderText = "Closing Share"
                Gv1.Columns("Closimgamt").HeaderText = "Closing Share Amount"
                Gv1.Columns("IssueNoShare").HeaderText = "Issue No Share"
                Gv1.Columns("DCSName").HeaderText = "DCS Name"


            End If
            If rbtDetail.Checked Then
                Gv1.Columns("DCSCode").IsVisible = False
            End If
            If rbtSummary.Checked Then
                Gv1.Columns("DCSCode").IsVisible = False
                Gv1.Columns("UploaderCode").IsVisible = False
                Gv1.Columns("OpeningSHARE").IsVisible = False
                Gv1.Columns("CLOSINGSHARE").IsVisible = False
                Gv1.Columns("DCSName").IsVisible = False
                Gv1.Columns("UploaderCode").HeaderText = "DCS Uploader Code "

                Gv1.Columns("OpeningAmount").HeaderText = "OP Amount"
                Gv1.Columns("Closimgamt").HeaderText = "Closing Share Amount"
                Gv1.Columns("IssueNoShare").HeaderText = "Issue No Share"

                ' Gv1.Columns("UploaderCode").HeaderText = "DCS Uploader Code "
                'Gv1.Columns("OpeningAmount").HeaderText = "OP Amount"
                'Gv1.Columns("OpeningSHARE").HeaderText = "OP Share"
                'Gv1.Columns("CLOSINGSHARE").HeaderText = "Closing Share"
                'Gv1.Columns("Closimgamt").HeaderText = "Closing Share Amount"
                'Gv1.Columns("IssueNoShare").HeaderText = "Issue No Share"
                'Gv1.Columns("DCSName").HeaderText = "DCS Name"

            End If
            'gv1.Columns("Document_No").HeaderText = "Document No."



        Next

        Gv1.AutoSizeRows = True
        Gv1.BestFitColumns()
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        ToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        fromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy")
        btnGo.Enabled = True
        TxtMultiDCS.arrValueMember = Nothing

    End Sub
End Class