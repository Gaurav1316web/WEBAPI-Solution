Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'===========Created by Preeti gupta==================
'==================update by preeti gupta======Ticket no[BM00000007520]
Public Class Rptmemberpaymentslip3
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    Dim arrLoc As String = Nothing
    Dim ItemStructureMandatoryOnWeightConversion As Boolean = False
    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMemberPaymentSlip)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag

    End Sub
    Sub LoadMCC()
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER inner join tspl_mcc_master on tspl_mcc_master.mcc_code=TSPL_LOCATION_MASTER.Location_Code where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If

        cbgMCC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgMCC.ValueMember = "Code"
        cbgMCC.DisplayMember = "Name"
    End Sub


    Sub LoadShiftFrom()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtFromShift.DataSource = dt
        txtFromShift.ValueMember = "Code"
    End Sub
    Sub LoadShiftTo()
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Shift")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "M"
        dr("Shift") = "Morning"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "E"
        dr("Shift") = "Evening"
        dt.Rows.Add(dr)

        txtToShift.DataSource = dt
        txtToShift.ValueMember = "Code"
    End Sub
    '===================================================================

    Sub FormatGrid()
        gv.SummaryRowsBottom.Clear()

        Dim summaryItem As New GridViewSummaryItem()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = True
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True

        Next


        gv.Columns("Account NO").IsVisible = False
        gv.Columns("Bank Branch").IsVisible = False
        gv.Columns("Bank Name").IsVisible = False
        gv.Columns("fromDate").IsVisible = False
        gv.Columns("Todate").IsVisible = False
        gv.Columns("companyADD").IsVisible = False
        gv.Columns("CompName").IsVisible = False
        gv.Columns("CompCode").IsVisible = False
        gv.Columns("HShift").IsVisible = False
        gv.Columns("ShiftMor").IsVisible = False
        gv.Columns("ShiftEve").IsVisible = False
        gv.Columns("Unit").IsVisible = False
        gv.Columns("Doc_No").IsVisible = False



        gv.Columns("Buffalo Milk Qty (KG)").IsVisible = True
        gv.Columns("Buffalo Milk Qty (KG)").Width = 100
        gv.Columns("Buffalo Milk Qty (KG)").HeaderText = " Buffalo Milk Qty"
        gv.Columns("Buffalo Milk Qty (KG)").FormatString = "{0:F3}"

        gv.Columns("Buffalo FAT(%)").IsVisible = True
        gv.Columns("Buffalo FAT(%)").Width = 100
        gv.Columns("Buffalo FAT(%)").HeaderText = "Buffalo FAT(%)"
        gv.Columns("Buffalo FAT(%)").FormatString = "{0:F2}"

        gv.Columns("Buffalo SNF(%)").IsVisible = True
        gv.Columns("Buffalo SNF(%)").Width = 100
        gv.Columns("Buffalo SNF(%)").HeaderText = "Buffalo SNF(%)"
        gv.Columns("Buffalo SNF(%)").FormatString = "{0:F2}"

        gv.Columns("Buffalo FAT (KG)").IsVisible = True
        gv.Columns("Buffalo FAT (KG)").Width = 100
        gv.Columns("Buffalo FAT (KG)").HeaderText = "Total Buffalo FAT"
        gv.Columns("Buffalo FAT (KG)").FormatString = "{0:F3}"

        gv.Columns("Buffalo SNF (KG)").IsVisible = True
        gv.Columns("Buffalo SNF (KG)").Width = 100
        gv.Columns("Buffalo SNF (KG)").HeaderText = "total Buffalo SNF"
        gv.Columns("Buffalo SNF (KG)").FormatString = "{0:F3}"

        gv.Columns("Buffalo Amount").IsVisible = True
        gv.Columns("Buffalo Amount").Width = 100
        gv.Columns("Buffalo Amount").HeaderText = "Buffalo Amount"
        gv.Columns("Buffalo Amount").FormatString = "{0:F3}"


        gv.Columns("Cow Milk Qty (KG)").IsVisible = True
        gv.Columns("Cow Milk Qty (KG)").Width = 100
        gv.Columns("Cow Milk Qty (KG)").HeaderText = "Cow Milk Qty"
        gv.Columns("Cow Milk Qty (KG)").FormatString = "{0:F3}"


        gv.Columns("Cow FAT(%)").IsVisible = True
        gv.Columns("Cow FAT(%)").Width = 100
        gv.Columns("Cow FAT(%)").HeaderText = "Cow FAT(%)"
        gv.Columns("Cow FAT(%)").FormatString = "{0:F2}"

        gv.Columns("Cow SNF(%)").IsVisible = True
        gv.Columns("Cow SNF(%)").Width = 100
        gv.Columns("Cow SNF(%)").HeaderText = "Cow SNF(%)"
        gv.Columns("Cow SNF(%)").FormatString = "{0:F2}"

        gv.Columns("Cow FAT (KG)").IsVisible = True
        gv.Columns("Cow FAT (KG)").Width = 100
        gv.Columns("Cow FAT (KG)").HeaderText = "Total Cow FAT"
        gv.Columns("Cow FAT (KG)").FormatString = "{0:F3}"

        gv.Columns("Cow SNF (KG)").IsVisible = True
        gv.Columns("Cow SNF (KG)").Width = 100
        gv.Columns("Cow SNF (KG)").HeaderText = "Total Cow SNF"
        gv.Columns("Cow SNF (KG)").FormatString = "{0:F3}"

        gv.Columns("Cow Amount").IsVisible = True
        gv.Columns("Cow Amount").Width = 100
        gv.Columns("Cow Amount").HeaderText = "Cow Amount"
        gv.Columns("Cow Amount").FormatString = "{0:F3}"
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item6 As New GridViewSummaryItem("Cow Milk Qty (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("Cow FAT (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)

        Dim item8 As New GridViewSummaryItem("Cow SNF (KG)", "{0:F3}", GridAggregateFunction.Sum)

        summaryRowItem.Add(item8)
        Dim summaryItem3 As New GridViewSummaryItem()
        summaryItem3.FormatString = "{0:F2}"
        summaryItem3.Name = "Cow SNF(%)"
        summaryItem3.AggregateExpression = "sum([Cow SNF (KG)])*100/sum([Cow Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem3)

        Dim summaryItem4 As New GridViewSummaryItem()
        summaryItem4.FormatString = "{0:F2}"
        summaryItem4.Name = "Cow FAT(%)"
        summaryItem4.AggregateExpression = "sum([Cow FAT (KG)])*100/sum([Cow Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem4)

        Dim item9 As New GridViewSummaryItem("Buffalo Milk Qty (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item9)
        Dim item10 As New GridViewSummaryItem("Buffalo FAT (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item10)
        Dim item11 As New GridViewSummaryItem("Buffalo SNF (KG)", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item11)
        Dim summaryItem5 As New GridViewSummaryItem()
        summaryItem5.FormatString = "{0:F2}"
        summaryItem5.Name = "Buffalo FAT(%)"
        summaryItem5.AggregateExpression = "sum([Buffalo FAT (KG)])*100/sum([Buffalo Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem5)

        Dim summaryItem6 As New GridViewSummaryItem()
        summaryItem6.FormatString = "{0:F2}"
        summaryItem6.Name = "Buffalo SNF(%)"
        summaryItem6.AggregateExpression = "sum([Buffalo SNF (KG)])*100/sum([Buffalo Milk Qty (KG)])"
        summaryRowItem.Add(summaryItem6)


        Dim item12 As New GridViewSummaryItem("Cow Amount", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item12)
        Dim item13 As New GridViewSummaryItem("Buffalo Amount", "{0:F3}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item13)
        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        If chkVLCAll.IsChecked Then
            cbgVLC.CheckedAll()
        Else
            cbgVLC.UnCheckedAll()
        End If
        chkMCCAll.CheckState = CheckState.Checked
        chkVLCAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        cboUnit.Text = "Kg"
        'cbtMCCRouteVLCC.Enabled = rbtnMCCRouteVLCCSelect.IsChecked
        EnableDisableControl(True)
    End Sub

    Private Sub EnableDisableControl(ByVal val As Boolean)
        RadGroupBox1.Enabled = val

    End Sub

    '==============Added by preeti against ticket no[BM00000009867]==============
    Private Sub LoadData()
        If clsCommon.myCDate(txtFromDate.Value) > clsCommon.myCDate(txtToDate.Value) Then
            txtFromDate.Focus()
            clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            Exit Sub
        End If

        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        Dim companyADD, CompName, CompCode As String
        Dim qry As String

        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        companyADD = dt1.Rows(0).Item("comp_address")

        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompName = dt2.Rows(0).Item("Comp_Name")


        qry = ""
        qry += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry)
        CompCode = dt5.Rows(0).Item("Comp_Code")
        Dim Shh As String = String.Empty
        Dim fromDate As Date = clsCommon.GetPrintDate(txtFromDate.Value)

        Dim Todate As Date = clsCommon.GetPrintDate(txtToDate.Value)

        Dim DiffDate As Integer = clsCommon.myCDate(Todate).Day - clsCommon.myCDate(fromDate).Day
        If DiffDate > 0 Then
            Shh = "Both"
        ElseIf DiffDate = 0 And txtFromShift.SelectedValue <> txtToShift.SelectedValue Then
            Shh = "Both"
        Else
            Shh = IIf(txtFromShift.SelectedValue = "M", "Morning", "Evening")
        End If
        ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
        Dim QryUploader As String = " select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code,TSPL_VLC_DATA_UPLOADER.Doc_No , TSPL_Mp_MASTER.Mcc_Code_VLC_Uploader,TSPL_VLC_DATA_UPLOADER.MCC_Code,TSPL_VLC_DATA_UPLOADER.VLC_CODE as VLC_uploader_CODE,TSPL_Mp_MASTER.MCC_NAME ,TSPL_MP_MASTER.VLC_Code ,TSPL_MP_MASTER.VLC_Name,TSPL_Mp_MASTER.MP_Code ,TSPL_VLC_DATA_UPLOADER.MP_CODE   as MP_Code_uploader ,TSPL_MP_MASTER.MP_Name ,TSPL_MP_MASTER.AccountNO ,TSPL_MP_MASTER.BankBranch ,TSPL_MP_MASTER. BankName,TSPL_VLC_DATA_UPLOADER.Doc_Date ,TSPL_VLC_DATA_UPLOADER.shift,TSPL_VLC_DATA_UPLOADER.qty as qty,TSPL_VLC_DATA_UPLOADER.fat, TSPL_VLC_DATA_UPLOADER.snf ,TSPL_VLC_DATA_UPLOADER.Amount ,TSPL_Mp_Master.UOM_Code,TSPL_MP_MASTER.Route_Code,RT.Route_Name    from  (select TSPL_Mp_MASTER.MP_Code ,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader ,VLC_Code_VLC_Uploader,MP_Code_VLC_Uploader,Mp_Name,TSPL_MP_MASTER.AccountNO,TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankName,TSPL_VLC_MASTER_HEAD.MCC, MCC_NAME,UOM_Code,TSPL_VLC_MASTER_HEAD.VLC_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name,TSPL_VLC_MASTER_HEAD.Route_Code  " & Environment.NewLine & _
        " from TSPL_VLC_MASTER_HEAD   left join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code =TSPL_VLC_MASTER_HEAD.MCC " & Environment.NewLine & _
        " Left join TSPL_Mcc_UOM_DETAIL on TSPL_Mcc_UOM_DETAIL.MCC_CODE =TSPL_MCC_MASTER.MCC_Code and Stocking_Unit ='Y' Left join TSPL_MP_MASTER on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_MASTER.VLC_Code  left join TSPL_BANK_BRANCH_MASTER on TSPL_BANK_BRANCH_MASTER.BRANCH_CODE =TSPL_MP_MASTER.BankBranch  left join TSPL_BANK_MASTER on TSPL_BANK_MASTER.BANK_CODE =TSPL_MP_MASTER.BankName) TSPL_MP_MASTER left join TSPL_VLC_DATA_UPLOADER " & Environment.NewLine & _
         " on TSPL_MP_MASTER.MP_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.MP_CODE and TSPL_MP_MASTER.VLC_Code_VLC_Uploader=TSPL_VLC_DATA_UPLOADER.VLC_CODE and TSPL_MP_MASTER.MCC =TSPL_VLC_DATA_UPLOADER.MCC_Code " & Environment.NewLine & _
            " left join TSPL_MCC_ROUTE_MASTER RT on TSPL_MP_MASTER.Route_Code=RT.Route_Code  where 2=2"

        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            QryUploader += " and 2=( case when File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and File_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and shift='M' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            QryUploader += " and 2=( case when File_Date >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and File_Date <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and shift='E' then 3 else 2 end  )"
        End If

        If cbgMCC.CheckedValue.Count > 0 Then
            QryUploader += " and TSPL_VLC_DATA_UPLOADER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        If cbgVLC.CheckedValue.Count > 0 AndAlso chkVLCselect.IsChecked Then
            QryUploader += " and TSPL_MP_MASTER.VLC_Code  IN (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ") "
        End If
        If Not txtRoute.arrValueMember Is Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            QryUploader += " and TSPL_MP_MASTER.Route_Code  IN (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
        End If
        QryUploader += "  and convert(date,File_Date,103)>=convert(date,('" + txtFromDate.Value + "'),103) and convert(date,File_Date,103) <=convert(date,('" + txtToDate.Value + "'),103) and qty >0 "

        '' query change by Panch Raj against ticket No:KDI/21/05/18-000323-> Route code must be picked from vlc master
        Dim QryManual As String = "select '" & clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.MCCDefaultMilkItem, clsFixedParameterCode.MilkSetting, Nothing)) & "'  as item_code,VDUM.document_code,TSPL_MCC_MASTER.Mcc_Code_VLC_Uploader, VLCM.MCC,VLCM.vlc_code_vlc_uploader AS VLC_UPLODER_CODE," & Environment.NewLine & _
        " tspl_mcc_master.mcc_name,VDUM.VLC_Code,VLCM.vlc_Name as VLC_Name,TSPL_MP_MASTER.mp_code,TSPL_MP_MASTER.MP_Code_VLC_Uploader,TSPL_MP_MASTER.mp_name,TSPL_MP_MASTER.AccountNO," & Environment.NewLine & _
        " TSPL_MP_MASTER.BankBranch,TSPL_MP_MASTER.BankName,VDUM.Document_Date as Document_Date,(case when VDUM.Shift='MORNING' THEN 'M' ELSE 'E' END) AS Shift," & Environment.NewLine & _
        " VDUD.Qty,VDUD.fatper,VDUD.snfper,VDUD.Amount as VLC_Amount,VDUD.Unit_Code,VLCM.Route_Code," & Environment.NewLine & _
        "  tspl_mcc_route_master.route_name from TSPL_VLC_DATA_UPLOADER_DETAIL VDUD  inner join TSPL_VLC_DATA_UPLOADER_MASTER VDUM on VDUD.Document_Code=VDUM.Document_Code  " & Environment.NewLine & _
        " left join TSPL_VLC_MASTER_HEAD VLCM on VDUM.VLC_Code=VLCM.VLC_Code  left join tspl_mcc_master on tspl_mcc_master.mcc_code=VLCM.MCC " & Environment.NewLine & _
        " left join tspl_mp_master on tspl_mp_master.mp_code=VDUD.farmer_code left join tspl_mcc_route_master on tspl_mcc_route_master.Route_Code=VLCM.Route_Code " & Environment.NewLine & _
        " where 2 = 2  and  convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and  convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "'"

        If clsCommon.CompairString(txtFromShift.Text, "E") = CompairStringResult.Equal Then
            QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='MORNING' then 3 else 2 end  )"
        End If
        If clsCommon.CompairString(txtToShift.Text, "M") = CompairStringResult.Equal Then
            QryManual += " and 2=( case when convert(date, VDUM.Document_Date,103) >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and convert(date, VDUM.Document_Date,103) <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm tt") + "' and SHIFT='EVENING' then 3 else 2 end  )"
        End If
        If cbgMCC.CheckedValue.Count > 0 Then
            QryManual += " and VLCM.MCC  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        If cbgVLC.CheckedValue.Count > 0 Then
            QryManual += " and VLCM.VLC_Code IN (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ") "
        End If
        If Not txtRoute.arrValueMember Is Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
            QryManual += " and VLCM.Route_Code  IN (" + clsCommon.GetMulcallString(txtRoute.arrValueMember) + ") "
        End If



        qry = ""
        qry = "select  '" + txtFromShift.Text + "' as ShiftMor,'" + txtToShift.Text + "' as ShiftEve,'" + cboUnit.Text + "' as Unit, '" & Shh & "'  as HShift ,'" & fromDate & "'  as fromDate ,'" & Todate & "'  as Todate ,'" & companyADD & "'  as companyADD, '" & CompName & "'  as CompName,'" & CompCode & "'  as CompCode," & Environment.NewLine & _
        "  MCC_Code as [MCC Code],(Mcc_Code_VLC_Uploader) as [MCC Uploader] ,(MCC_NAME) as [MCC Name],MP_Code as [MP Code],MP_Code_uploader   as [MP Uploader] ,(MP_Name) as [MP Name],(AccountNO) as [Account NO],(BankBranch) as [Bank Branch],(BankName) as [Bank Name],qq.VLC_CODE as [VLC Code] ,VLC_uploader_CODE   as [VLC Code Uploader],TSPL_VLC_MASTER_HEAD.VLC_Name  as [VLC Name],(qq.Route_Code) as [Route Code],(Route_Name) as [Route Name] ," & Environment.NewLine & _
        " convert(varchar,Doc_Date,103) as [Doc Date] ,shift,([Buffalo Milk Qty (KG)]) as [Buffalo Milk Qty (KG)] ," & Environment.NewLine & _
         " [Buffalo FAT(%)] as [Buffalo FAT(%)], " & Environment.NewLine & _
       " [Buffalo SNF(%)] as [Buffalo SNF(%)] ," & Environment.NewLine & _
        " ([Buffalo FAT (KG)]) as [Buffalo FAT (KG)]," & Environment.NewLine & _
       " ([Buffalo SNF (KG)]) as [Buffalo SNF (KG)] ,([Buffalo Amount] ) as [Buffalo Amount]," & Environment.NewLine & _
        " ([Cow Milk Qty (KG)]) as [Cow Milk Qty (KG)], " & Environment.NewLine & _
        " [Cow FAT(%)] as [Cow FAT(%)]," & Environment.NewLine & _
        " [Cow SNF(%)] as [Cow SNF(%)] , " & Environment.NewLine & _
        " ([cow FAT (KG)])  as [Cow FAT (KG)],([cow SNF (KG)]) as [Cow SNF (KG)] ," & Environment.NewLine & _
         " ([Cow Amount]) as [Cow Amount],doc_no  " & Environment.NewLine & _
        " from (  select     " & Environment.NewLine & _
         " pp.*,Case When pp.FAT <= 5 Then  pp.NewQty   Else 0 End [Cow Milk Qty (KG)],Case When pp.FAT < 5 Then pp.FAT Else 0 End [Cow FAT(%)], " & Environment.NewLine & _
        " Case When pp.FAT < 5 Then pp.SNF Else 0 End [Cow SNF(%)],Case When pp.FAT  <= 5 Then pp.NewFAT_KG  Else 0 End [Cow FAT (KG)]," & Environment.NewLine & _
         "Case When pp.FAT <= 5 Then pp.NewSNF_KG Else 0 End [Cow SNF (KG)], Case When pp.FAT <= 5 Then pp.NewAmount Else 0 End [Cow Amount]," & Environment.NewLine & _
        " Case When pp.FAT > 5 Then pp.NewQty  Else 0 End [Buffalo Milk Qty (KG)],Case When pp.FAT > 5 Then pp.FAT Else 0 End [Buffalo FAT(%)], " & Environment.NewLine & _
         " Case When pp.FAT > 5 Then pp.SNF Else 0 End [Buffalo SNF(%)], Case When pp.FAT > 5 Then pp.NewFAT_KG Else 0 End [Buffalo FAT (KG)], " & Environment.NewLine & _
         " Case When pp.FAT > 5 Then pp.NewSNF_KG Else 0 End [Buffalo SNF (KG)],Case When pp.FAT > 5 Then pp.NewAmount  Else 0 End [Buffalo Amount]" & Environment.NewLine & _
       " from (" & Environment.NewLine & _
         " select xx.*,xx.qty*CF as NewQty,xx.Amount*CF as NewAmount ,  fat_KG *Cf   as NewFAT_KG, snf_KG *cf  as NewSNF_KG from (" & Environment.NewLine & _
        " select *,(qty *fat) /100 as fat_KG,(qty *snf) /100 as snf_KG from (" & Environment.NewLine & _
         " " & QryUploader & " " & Environment.NewLine & _
         " union all " & Environment.NewLine & _
         " " & QryManual & "" & Environment.NewLine & _
         " ) as pp )xx " & Environment.NewLine & _
          " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code =xx.Item_Code "

        ''richa agarwal 24 May,2019  TEC/28/03/19-000462 add item structure on setting based
        If ItemStructureMandatoryOnWeightConversion = True Then
            qry += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF,Structure_code  from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code   and lower(zzz.TOUOM)='" + cboUnit.Text + "' where 2 = 2 AND TSPL_ITEM_MASTER.Structure_Code =zzz.Structure_code ) "
        Else
            qry += " left outer join (Select Distinct yyy.* From (  Select Container_UOM as FromUOM, Contained_UOM as TOUOM, Container_Qty*Contained_Qty as CF from TSPL_WEIGHT_CONVERSION UNION All Select Contained_UOM as FromUOM, Container_UOM as TOUOM, Container_Qty/ nullif (Contained_Qty,0) as CF from TSPL_WEIGHT_CONVERSION UNION All   Select Contained_UOM as FromUOM, Contained_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION UNION All Select Container_UOM as FromUOM, Container_UOM as TOUOM, 1 as CF from TSPL_WEIGHT_CONVERSION  ) yyy) zzz on zzz.FromUOM =xx.UOM_Code and lower(zzz.TOUOM)='" + cboUnit.Text + "' ) "
        End If

        qry += " as pp  ) as qq left join TSPL_VLC_MASTER_HEAD on qq.VLC_Code =TSPL_VLC_MASTER_HEAD.VLC_Code  "


        dt = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()
        gv.DataSource = dt
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        FormatGrid()
        If btnReferesh = False Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinHeader(), "crptMemberPaymentSlip", "Member Payment Slip", "")
            frmCRV = Nothing

        End If
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        End If
        gv.BestFitColumns()

        RadPageView1.SelectedPage = RadPageViewPage2
        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try

            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
                arrHeader.Add(CompName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptMemberPaymentSlip & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                If chkMCCSelect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgMCC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgMCC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("MCC Name: " + strMCCName + " "))
                End If


                If chkVLCselect.IsChecked Then
                    Dim strMCCName As String = ""
                    For Each StrName As String In cbgVLC.CheckedDisplayMember
                        If clsCommon.myLen(strMCCName) > 0 Then
                            strMCCName += ", "
                        End If
                        strMCCName += StrName
                    Next
                    Dim strMCCCode As String = ""
                    For Each StrCode As String In cbgVLC.CheckedValue
                        If clsCommon.myLen(strMCCCode) > 0 Then
                            strMCCCode += ", "
                        End If
                        strMCCCode += StrCode
                    Next
                    arrHeader.Add(("VLC Name: " + strMCCName + " "))
                End If
                If txtRoute.arrValueMember IsNot Nothing AndAlso txtRoute.arrValueMember.Count > 0 Then
                    arrHeader.Add(("Route : " + clsCommon.GetMulcallStringWithComma(txtRoute.arrDispalyMember) + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Member Payment Slip", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub Rptmemberpaymentslip3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ItemStructureMandatoryOnWeightConversion = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ItemStructureMandatoryOnWeightConversion, clsFixedParameterCode.ItemStructureMandatoryOnWeightConversion, Nothing)) = 1, True, False))
        LOCATIONRIGTHS()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        LoadMCC()
        LoadVLC()
        LoadShiftFrom()
        LoadShiftTo()
        Reset()
    End Sub

    Private Sub Rptmemberpaymentslip3_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        LoadData()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub


    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        btnReferesh = False
        LoadData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Sub LoadVLC()
        Dim qry As String = Nothing
        qry = "select TSPL_VLC_MASTER_HEAD.VLC_Code as [Code],TSPL_VLC_MASTER_HEAD.VLC_Name as [Name]  from TSPL_VLC_MASTER_HEAD "
        If cbgMCC.CheckedValue.Count > 0 Then
            qry += " left join TSPL_mcc_ROUTE_MASTER on TSPL_mcc_ROUTE_MASTER.Route_Code = TSPL_VLC_MASTER_HEAD.Route_Code left join tspl_mcc_master on tspl_mcc_master.MCC_Code = TSPL_mcc_ROUTE_MASTER.MCC_Code where TSPL_mcc_ROUTE_MASTER.mcc_code IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "

        End If
        cbgVLC.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVLC.ValueMember = "Code"
        cbgVLC.DisplayMember = "Name"

    End Sub
    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub chkVLCAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkVLCAll.ToggleStateChanged
        cbgVLC.Enabled = Not chkVLCAll.IsChecked
        If chkVLCAll.IsChecked Then
            cbgVLC.CheckedAll()
        Else
            cbgVLC.UnCheckedAll()
        End If
    End Sub

    Private Sub cbgMCC__MyCheckChanged(sender As Object, e As EventArgs) Handles cbgMCC._MyCheckChanged
        LoadVLC()
        If chkVLCAll.IsChecked Then
            cbgVLC.CheckedAll()
        Else
            cbgVLC.UnCheckedAll()
        End If
        chkVLCAll.CheckState = CheckState.Checked
     End Sub

    Private Sub txtRoute__My_Click(sender As Object, e As EventArgs) Handles txtRoute._My_Click
        Dim qry As String = " SELECT  DISTINCT RT.Route_Code as Code,RT.Route_Name as Name FROM TSPL_MCC_ROUTE_MASTER RT LEFT JOIN TSPL_VLC_MASTER_HEAD VLC ON RT.Route_Code=VLC.Route_Code where 2=2 "
        If cbgVLC.CheckedValue.Count > 0 AndAlso chkVLCselect.IsChecked Then
            qry += " and VLC.VLC_Code  IN (" + clsCommon.GetMulcallString(cbgVLC.CheckedValue) + ") "
        End If
        txtRoute.arrValueMember = clsCommon.ShowMultipleSelectForm("RouteMulSel", qry, "Code", "Name", txtRoute.arrValueMember, txtRoute.arrDispalyMember)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
