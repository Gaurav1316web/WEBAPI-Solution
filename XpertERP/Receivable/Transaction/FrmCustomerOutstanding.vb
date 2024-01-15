''created by richa agarwal BHA/21/06/18-000077 on 13 July,2018
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO

Public Class FrmCustomerOutstanding
    Inherits FrmMainTranScreen
    Dim userCode, companyCode, sql, strQuery, strType As String
    Dim ArrDBName As ArrayList = Nothing
    Dim strLocation As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim dt As DataTable
    Dim arrLoc As String = Nothing
    Public Shared Alocation As String = Nothing
    Dim dblPenaltPercentage As Double = 0
    '----------grid Varibales-----------
    Const colApply As String = "Apply"
    Const colDocType As String = "DocType"
    Const colLineNo As String = "Line_No"
    Const colDocNo As String = "DocNo"
    Const colDocDate As String = "DocDate"
    Const colDueDate As String = "DueDate"
    Const colAgeingDays As String = "colAgeingDays"
    Const colPending_Balance As String = "Pending_Balance"
    Const colPenaltyPer As String = "PenaltyPer"
    Const colPenaltyAmount As String = "PenaltyAmount"
    Const colCURRENCY_CODE As String = "CURRENCY_CODE"
    Const colConvRateOld As String = "colConvRateOld"
    Private isNewEntry As Boolean = False
   

    Private Sub rptCustomerAgeingDrillDown_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadCurrencyType()
        dtpAgeof.Value = Date.Today
        AddNew()
        ddlAgedRcvbl.Text = "Aged Trial Balance By Due Date"
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        dblPenaltPercentage = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.PenaltyPercentage, clsFixedParameterCode.PenaltyPercentage, Nothing))
        ' ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
    End Sub
    Private Sub LOCATIONRIGTHS()
        Dim obj As New clsMCCCodes()
        Try

            obj = clsMCCCodes.GetData()

            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                'lblLocationCode.Value = obj.Default_LocCode
                'LblLocationName.Text = obj.Default_LocName
            End If
            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
                Alocation = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        MyBase.SetUserMgmt(clsUserMgtCode.FrmCustomerOutstanding)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub print(ByVal Isgrid As Boolean)
        Try
            Dim type As String = Me.ddlAgedRcvbl.Text
            Dim strTtpe As String = ""
            Dim IsFifoBased As String = "N"
            Dim dt As New DataTable
            If chkType.Checked = True Then
                strTtpe = "SMry"
            End If
         
            Dim ArryLst As New ArrayList
            ArryLst.Add("IN")
            ArryLst.Add("DB")
            ArryLst.Add("CR")
            ArryLst.Add("RC")

            ArryLst.Add("UC")
            ArryLst.Add("SR")

            ArryLst.Add("AD")
            ArryLst.Add("RF")

            ArryLst.Add("AV")
            ArryLst.Add("OA")
            ArryLst.Add("VGCL")

            If ArryLst.Count <= 0 Then
                RadMessageBox.Show("Please select at least one Transaction Type")
                Return
            End If


            Dim rptHeading As String
            rptHeading = "Aged Trial Balance Report"

            strQuery = ""
            Dim strEmptyQry As String = ""
            Dim strFilledQry As String = ""
            Dim strUpperQry As String = ""
            Dim strUpperQry1 As String = ""
            Dim strInnerQry As String = ""
            Dim strLowerQry As String = ""
            Dim strLowerQry1 As String = ""

            ''----------------
            Dim isonduedate As String = String.Empty
            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Due Date") = CompairStringResult.Equal Then
                isonduedate = "DueDate"
            End If
            Dim arrcustomer As New ArrayList
            arrcustomer.Add(Fndcustomer.Value)

            Dim arrLocation As New ArrayList
            arrLocation.Add(fndLocation.Value)

            strInnerQry = clsCustomerMasterNew.GetOutStandingQry(dtpAgeof.Value, dtpAgeof.Value, " AND TSPL_CUSTOMER_MASTER.Status='N'", ArryLst, isonduedate, ddlCurrencyType.SelectedValue, arrcustomer, arrLocation, Nothing, ChkISParentCust.Checked, Nothing, IIf(ChkSecurity.Checked, "", "AND ISNULL(TSPL_RECEIPT_HEADER.SecurityDeposit,'')='N'"), True)


            strUpperQry = " select  " & _
            " Query.Comp_Code ,Query.[Customer Id],Query.[Parent Code] ,Query.ParentName ,Query.[Customer Name] ,Query.Cust_Group_Code ,Query.Cust_Group_Desc ,Query.[Document Id] ,Query.[Desc] as [Desc] , "


            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Due Date") = CompairStringResult.Equal Then
                strUpperQry += " case when ( DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )>0 or isnull(Query.[Due Date],'')='' then Case when Query.Document_Type IN ('IN','DB','RF') then CASE WHEN convert(decimal(18,2), Query.[Due Amount] )<0 THEN 0 ELSE  convert(decimal(18,2), Query.[Due Amount] ) END else 0 End else 0 end as [Due Amount],"
            End If

            strUpperQry += " Query.Currency ,Query.CURRENCY_CODE ,Query.ConvRate ,case when Query.Document_Type IN ('IN','DB','RF') then convert(varchar,Query.[Due Date],103) else convert(varchar, Query.[Document Date],103) end as [Due Date], Query.type ,Query.[Document Date] , "

            If clsCommon.CompairString(ddlAgedRcvbl.Text, "Aged Trial Balance By Due Date") = CompairStringResult.Equal Then
                strUpperQry += " case when Query.Document_Type NOT IN ('IN','DB','RF') then  convert(decimal(18,2), Query.[Due Amount]) else case when ( DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1 )<=0 then Case when Query.Document_Type IN ('IN','DB','RF') then convert(decimal(18,2), Query.[Due Amount] ) else 0 End else 0 end End  as [Current], " + Environment.NewLine & _
                " case when Query.Document_Type IN ('IN','DB','RF') and isnull((Select max(TSPL_CUSTOMER_OUTSTANDING_DETAIL .Customer_Outsanding_Date)  from TSPL_CUSTOMER_OUTSTANDING_DETAIL where  TSPL_CUSTOMER_OUTSTANDING_DETAIL .Document_No=Query.[Document Id]),'')<>'' then " & _
" DATEDIFF (day,convert(date, (Select cast (max(TSPL_CUSTOMER_OUTSTANDING_DETAIL .Customer_Outsanding_Date) as date)  from TSPL_CUSTOMER_OUTSTANDING_DETAIL where  TSPL_CUSTOMER_OUTSTANDING_DETAIL .Document_No=Query.[Document Id]),101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "') " & _
" when Query.Document_Type IN ('IN','DB','RF') and isnull(Query.[Due Date],'')<>'' then  DATEDIFF (day,convert(date, Query.[Due Date],101),'" & clsCommon.GetPrintDate(dtpAgeof.Value, "dd-MMM-yyyy") & "')+1  else 0 end  as [Ageing_Days],"
            End If

            strUpperQry += " Query.Document_Type ,Query.Location  , '' AS From_Vendor, '' AS To_Vendor, " & _
            " '" + Me.ddlAgedRcvbl.Text + "' AS Report_Type,  '" + Me.dtpAgeof.Value + "' AS AgeofDate,'" + strTtpe + "' as [Summary], '" + IsFifoBased + "' as [IsFifoBased]," & _
            " TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.Add1+case  when isnull(TSPL_COMPANY_MASTER.Add2,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add2 +case  when isnull(TSPL_COMPANY_MASTER.Add3,'')='' then '' else ', '+TSPL_COMPANY_MASTER.Add3 end end as comp_address " & _
            " from ( "

            strLowerQry1 = " ) Query " & _
            " LEFT OUTER JOIN TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code = Query .Comp_Code " & _
            " Where 1=1 " & _
            " AND Query.[Document Id] not in (Select  UnApplied_No  from TSPL_RECEIPT_HEADER ForUnapliedEntry_bankReverse_Exclude where ForUnapliedEntry_bankReverse_Exclude.UnApplied_No =Query .[Document Id] and ForUnapliedEntry_bankReverse_Exclude.IsChkReverse ='Y' and isnull(UnApplied_No,'')<>'') " & _
            " AND Query.[Document Id] not in ( Select distinct a.Document_No from TSPL_REVALUATION_DETAIL inner join (Select RefDocNo,Document_No   from TSPL_Customer_Invoice_Head where RefDocType ='REVALUATION ENTRY') a on a.RefDocNo =TSPL_REVALUATION_DETAIL.Document_No inner join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =TSPL_REVALUATION_DETAIL.AR_Invoice_No  where  isnull(TSPL_REVALUATION_DETAIL.AR_Invoice_No ,'')<>'' union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_Sale_Return_No,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_Sale_Return_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_Sale_Return_No,'')<>'' AND ISNULL(Trans_Type,'') ='BSR'  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (Against_MCC_Material_Sale_Return,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select Against_MCC_Material_Sale_Return  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (Against_MCC_Material_Sale_Return,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') )  union all Select Document_No  from TSPL_Customer_Invoice_Head where Document_Type ='C' and (ISNULL (AgainstScrapReturn,'') IN (Select Document_Code from TSPL_SD_SALE_rETURN_HEAD WHERE Document_Code IN (Select AgainstScrapReturn  from TSPL_Customer_Invoice_Head where Document_Type ='C' and ISNULL (AgainstScrapReturn,'')<>'') AND ISNULL(Against_Invoice_No,'')<>'') ))"

            strQuery = " select 'Yes' as Apply,Document_Type ,[Document Id] ,[Document Date],[Due Date],[Ageing_Days],[Due Amount] as BalAmount,0.00 as AppliedAmt,CURRENCY_CODE ,ConvRate " & _
            " from ( " + strUpperQry + strInnerQry + strLowerQry1
            If clsCommon.CompairString(clsCommon.myCstr(ddlCurrencyType.SelectedValue), "1") = CompairStringResult.Equal Then
                strQuery += "  and query.CURRENCY_CODE <>TSPL_COMPANY_MASTER .BaseCurrencyCode "
            End If
            strQuery += " ) xxx where Ageing_Days >0 and [Due Amount]>0" & _
            " order by xxx.[Customer Id],xxx.[Document Date]"

            dt = clsDBFuncationality.GetDataTable(strQuery)
            LoadBlankGrid()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    gv1.Rows.AddNew()
                    gv1.Rows(i).Cells(colApply).Value = clsCommon.myCstr(dt.Rows(i)("Apply"))
                    gv1.Rows(i).Cells(colDocType).Value = clsCommon.myCstr(dt.Rows(i)("Document_Type"))
                    gv1.Rows(i).Cells(colDocNo).Value = clsCommon.myCstr(dt.Rows(i)("Document Id"))
                    gv1.Rows(i).Cells(colDocDate).Value = clsCommon.myCDate(dt.Rows(i)("Document Date"))
                    gv1.Rows(i).Cells(colDueDate).Value = clsCommon.myCDate(dt.Rows(i)("Due Date"))
                    gv1.Rows(i).Cells(colAgeingDays).Value = clsCommon.myCdbl(dt.Rows(i)("Ageing_Days"))
                    gv1.Rows(i).Cells(colPending_Balance).Value = clsCommon.myCdbl(dt.Rows(i)("BalAmount"))
                    gv1.Rows(i).Cells(colPenaltyPer).Value = clsCommon.myCdbl(dblPenaltPercentage)
                    gv1.Rows(i).Cells(colPenaltyAmount).Value = clsCommon.myCdbl(dt.Rows(i)("AppliedAmt"))
                    gv1.Rows(i).Cells(colCURRENCY_CODE).Value = clsCommon.myCstr(dt.Rows(i)("CURRENCY_CODE"))
                    gv1.Rows(i).Cells(colConvRateOld).Value = clsCommon.myCdbl(dt.Rows(i)("ConvRate"))
                Next
            End If

            UpdateAllTotals()
            gv1.BestFitColumns()
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub MasterTemplate_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        GridDouble_Click()
    End Sub
    Private Sub UpdateAllTotals()
        Try
            Dim dblTotAmt As Double = 0
            Dim dblPenaltyAmount As Double = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.myCdbl(gv1.Rows(ii).Cells(colPending_Balance).Value) > 0 AndAlso clsCommon.myCstr(gv1.Rows(ii).Cells(colApply).Value) = "Yes") Then
                    dblTotAmt = clsCommon.myCdbl(gv1.Rows(ii).Cells(colPending_Balance).Value)
                    dblPenaltyAmount = (dblTotAmt * (dblPenaltPercentage / 365) * clsCommon.myCdbl(gv1.Rows(ii).Cells(colAgeingDays).Value)) / 100
                    gv1.Rows(ii).Cells(colPenaltyAmount).Value = Math.Round(clsCommon.myCdbl(dblPenaltyAmount), 2, MidpointRounding.AwayFromZero)
                Else
                    gv1.Rows(ii).Cells(colPenaltyAmount).Value = 0.0
                End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub GridDouble_Click()
        If gv1.Rows.Count > 0 Then
            If clsCommon.myCdbl(gv1.CurrentRow.Index) >= 0 Then
                If gv1.CurrentRow.Cells(colApply).IsCurrent = True Then
                    If gv1.CurrentRow.Cells(colApply).Value = "No" Then
                        gv1.CurrentRow.Cells(colApply).Value = "Yes"
                    ElseIf gv1.CurrentRow.Cells(colApply).Value = "Yes" Then
                        gv1.CurrentRow.Cells(colApply).Value = "No"
                    End If
                    UpdateAllTotals()
                End If
            End If
        End If
    End Sub
    Sub LoadBlankGrid()

        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False

        Dim apply As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        apply.FormatString = ""
        apply.HeaderText = colApply
        apply.Name = colApply
        apply.Width = 50
        apply.ReadOnly = True
        apply.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(apply)

        Dim docType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docType.FormatString = ""
        docType.HeaderText = "Document Type"
        docType.Name = colDocType
        docType.Width = 100
        docType.ReadOnly = True
        gv1.Columns.Add(docType)

        Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        docNo.HeaderText = "Document No"
        docNo.Name = colDocNo
        docNo.Width = 150
        docNo.ReadOnly = True
        docNo.IsVisible = True
        gv1.Columns.Add(docNo)

        Dim docDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docDate.FormatString = ""
        docDate.HeaderText = "Document Date"
        docDate.Name = colDocDate
        docDate.Width = 150
        docDate.ReadOnly = True
        gv1.Columns.Add(docDate)

        Dim duedate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        duedate.FormatString = ""
        duedate.HeaderText = "Due Date"
        duedate.Name = colDueDate
        duedate.Width = 150
        duedate.ReadOnly = True
        gv1.Columns.Add(duedate)

        Dim FilledTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
        FilledTotal.FormatString = ""
        FilledTotal.HeaderText = "Ageing Days"
        FilledTotal.Name = colAgeingDays
        FilledTotal.Width = 70
        FilledTotal.ReadOnly = True
        FilledTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(FilledTotal)

        Dim EmptyTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
        EmptyTotal.FormatString = ""
        EmptyTotal.HeaderText = "Pending Balance"
        EmptyTotal.Name = colPending_Balance
        EmptyTotal.Width = 70
        EmptyTotal.ReadOnly = True
        EmptyTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(EmptyTotal)

        Dim originalInvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        originalInvAmt.FormatString = ""
        originalInvAmt.HeaderText = "Penalty %"
        originalInvAmt.Name = colPenaltyPer
        originalInvAmt.Width = 100
        originalInvAmt.ReadOnly = True
        originalInvAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(originalInvAmt)

        Dim BalAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        BalAmt.FormatString = ""
        BalAmt.DecimalPlaces = 2
        BalAmt.HeaderText = "Penalty Amount"
        BalAmt.Name = colPenaltyAmount
        BalAmt.Width = 100
        BalAmt.ReadOnly = True
        BalAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(BalAmt)

        Dim CURRENCY_CODE As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CURRENCY_CODE.FormatString = ""
        CURRENCY_CODE.HeaderText = "Currency"
        CURRENCY_CODE.Name = colCURRENCY_CODE
        CURRENCY_CODE.Width = 100
        CURRENCY_CODE.ReadOnly = False
        CURRENCY_CODE.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(CURRENCY_CODE)

        Dim ConvRateOld As GridViewDecimalColumn = New GridViewDecimalColumn()
        ConvRateOld.FormatString = ""
        ConvRateOld.DecimalPlaces = 4
        ConvRateOld.HeaderText = "Conv Rate Old"
        ConvRateOld.Name = colConvRateOld
        ConvRateOld.Width = 100
        ConvRateOld.ReadOnly = True
        ConvRateOld.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(ConvRateOld)



        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False


    End Sub
    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(Fndcustomer.Value) <= 0 Then
                Throw New Exception("Please select Customer")
            End If
            gv1.EnableFiltering = True
            print(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btSaveLayout.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Public Function GetReportID() As String

        Return "frmCustomerOutstanding"

    End Function

    Private Sub btnDeleteLayour_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteLayour.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        print(False)
    End Sub
    Private Sub LoadCurrencyType()
        dt = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        dt.Rows.Add("ConvRate", "Functional Currency")
        dt.Rows.Add("1", "Customer Currency")
        ddlCurrencyType.DataSource = dt
        ddlCurrencyType.ValueMember = "Code"
        ddlCurrencyType.DisplayMember = "Name"
    End Sub
    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        fndLocation.Value = clsCommon.ShowSelectForm("LocationOuts", qry, "Code", WhrCls, fndLocation.Value, "Code", isButtonClicked)
        locationname.Text = clsLocation.GetName(fndLocation.Value, Nothing)
    End Sub

    Private Sub Fndcustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles Fndcustomer._MYValidating
        Dim Qry As String = clsERPFuncationality.glCustomerQuery
        Fndcustomer.Value = clsCommon.ShowSelectForm("CustomerSelector", Qry, "Code", "Status ='N' AND OnHold='N'", Fndcustomer.Value, "Code", isButtonClicked)
        CustomerName.Text = clsCustomerMaster.GetName(Fndcustomer.Value, Nothing)
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If savedata() Then
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Function allowToSave() As Boolean
        Try
            Dim count As Integer = 0
            If AllowFutureDateTransaction(dtpAgeof.Value, Nothing) = False Then
                dtpAgeof.Focus()
                Return False
            End If
           
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(Fndcustomer.Value) <= 0 Then
                Throw New Exception("Please select Customer")
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString("Yes", clsCommon.myCstr(gv1.Rows(ii).Cells("Apply").Value)) = CompairStringResult.Equal Then
                    count = count + 1
                End If
            Next

            If count = 0 Then
                Throw New Exception("Please select Atleast one row in grid")
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Public Function savedata(Optional ByVal isPosted As Boolean = False)
        Try
            If (allowToSave()) Then
                Dim obj As New clsCustomerOutstanding()

               
                obj.Customer_Outsanding_No = clsCommon.myCstr(txtDocNo.Value)
                obj.Document_Date = clsCommon.myCDate(dtpAgeof.Value)
                obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
                obj.Cust_Code = clsCommon.myCstr(Fndcustomer.Value)

               
                obj.Arr = New List(Of clsCustomerOutstandingDetail)
                '============================Detail Section==============================
                For i As Integer = 0 To gv1.Rows.Count - 1
                    Dim objTr As New clsCustomerOutstandingDetail()
                    If gv1.Rows(i).Cells(colApply).Value = "Yes" Then
                        objTr.Apply = "Y"
                        objTr.Line_No = clsCommon.myCdbl(i + 1)
                        objTr.Customer_Outsanding_Date = clsCommon.myCDate(dtpAgeof.Value)
                        objTr.Document_Type = clsCommon.myCstr(gv1.Rows(i).Cells(colDocType).Value)
                        objTr.Document_No = clsCommon.myCstr(gv1.Rows(i).Cells(colDocNo).Value)
                        objTr.Document_Date = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDocDate).Value, "yyyy-MM-dd")
                        objTr.Due_Date = clsCommon.GetPrintDate(gv1.Rows(i).Cells(colDueDate).Value, "yyyy-MM-dd")
                        objTr.AgeingDays = clsCommon.myCdbl(gv1.Rows(i).Cells(colAgeingDays).Value)
                        objTr.Pending_Balance = clsCommon.myCdbl(gv1.Rows(i).Cells(colPending_Balance).Value)
                        objTr.PenaltyPer = clsCommon.myCdbl(dblPenaltPercentage)
                        objTr.PenaltyAmount = clsCommon.myCdbl(gv1.Rows(i).Cells(colPenaltyAmount).Value)
                        objTr.CURRENCY_CODE = clsCommon.myCstr(gv1.Rows(i).Cells(colCURRENCY_CODE).Value)
                        objTr.ConvRateOld = clsCommon.myCdbl(gv1.Rows(i).Cells(colConvRateOld).Value)
                        obj.Arr.Add(objTr)
                    End If
                Next

                
                If obj.SaveData(obj, isNewEntry, Form_ID) Then
                    LoadData(obj.Customer_Outsanding_No, NavigatorType.Current)
                End If
               
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True

    End Function
    Public Sub LoadData(ByVal strCode As String, ByVal navType As NavigatorType)
        Try
            AddNew()
            Dim obj As New clsCustomerOutstanding()
            obj = clsCustomerOutstanding.GetData(strCode, arrLoc, navType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Customer_Outsanding_No) > 0) Then
                isNewEntry = False
                fndLocation.Value = obj.Location_Code
                locationname.Text = obj.Location_Name
                Fndcustomer.Value = obj.Cust_Code
                CustomerName.Text = obj.Customer_Name
                txtDocNo.Value = obj.Customer_Outsanding_No
                dtpAgeof.Value = obj.Document_Date
                LoadBlankGrid()
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsCustomerOutstandingDetail In obj.Arr
                        gv1.Rows.AddNew()
                        If objTr.Apply = "Y" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colApply).Value = "Yes"
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = objTr.Document_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocNo).Value = objTr.Document_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocDate).Value = objTr.Document_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDueDate).Value = objTr.Due_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAgeingDays).Value = objTr.AgeingDays
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPending_Balance).Value = objTr.Pending_Balance
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPenaltyPer).Value = objTr.PenaltyPer
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPenaltyAmount).Value = objTr.PenaltyAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCURRENCY_CODE).Value = objTr.CURRENCY_CODE
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colConvRateOld).Value = objTr.ConvRateOld
                    Next
                Else
                    gv1.DataSource = Nothing
                End If

                btnSave.Text = "Update"
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    btnPrint.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    btnPrint.Enabled = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Customer Outstanding", MessageBoxButtons.OK)
        Finally

        End Try
    End Sub
    Sub AddNew()
        isNewEntry = True
        dtpAgeof.Value = clsCommon.GETSERVERDATE
        Dim DateTime As String = clsFixedParameter.GetData(clsFixedParameterType.AllowToSaveTimeWithDocumentDate, clsFixedParameterCode.AllowToSaveTimeWithDocumentDate, Nothing)
        If DateTime = "1" Then
            dtpAgeof.CustomFormat = "dd/MM/yyyy hh:mm tt"
        Else
            dtpAgeof.CustomFormat = "dd/MM/yyyy"
        End If
        txtDocNo.Value = ""
        fndLocation.Value = ""
        CustomerName.Text = ""
        Fndcustomer.Value = ""
        locationname.Text = ""
        LoadBlankGrid()
        UsLock1.Status = ERPTransactionStatus.Pending
        LOCATIONRIGTHS()
        txtDocNo.MyReadOnly = False
        btnsave.Text = "Save"
        btnPost.Enabled = True
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub DeleteData()
        Dim arr As List(Of String) = New List(Of String)
        Try

            If (myMessages.deleteConfirm()) Then
                arr.Add(txtDocNo.Value)
                If (clsCustomerOutstanding.DeleteData(txtDocNo.Value, arrLoc)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_CUSTOMER_OUTSTANDING_HEADER where Customer_Outsanding_No='" + txtDocNo.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If

            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "Select TSPL_CUSTOMER_OUTSTANDING_HEADER.Customer_Outsanding_No as Code,Convert(varchar,TSPL_CUSTOMER_OUTSTANDING_HEADER.Document_Date,103) as [Dispatch Date],TSPL_CUSTOMER_OUTSTANDING_HEADER.Cust_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],ISNULL(TSPL_CUSTOMER_MASTER.Alies_Name,'') As [Alies Name],TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],case when TSPL_CUSTOMER_OUTSTANDING_HEADER.Posted=0 then 'Pending' else 'Approved' end as Status from TSPL_CUSTOMER_OUTSTANDING_HEADER left outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_OUTSTANDING_HEADER.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code Left Outer Join TSPL_LOCATION_MASTER on TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code =TSPL_LOCATION_MASTER.Location_Code"
        txtDocNo.Value = clsCommon.ShowSelectForm("CustomerOutstanding", qry, "Code", " TSPL_CUSTOMER_OUTSTANDING_HEADER.Location_Code in (" + arrLoc + ")", txtDocNo.Value, "", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                savedata()
                If (clsCustomerOutstanding.PostData(MyBase.Form_ID, txtDocNo.Value, arrLoc)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

End Class
