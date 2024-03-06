Imports common
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Public Class rptLoanstatement
    Inherits FrmMainTranScreen
    Private Sub rptLoanstatement_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub findPayperiod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles findPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS Code,(DATEDIFF(DAY,date_from,date_to)+1) as Totaldays, " _
       & " PAY_PERIOD_NAME as Name FROM TSPL_PAYPERIOD_MASTER"
        'Dim qry As String = "select PAY_PERIOD_CODE as Code , PAY_PERIOD_NAME as Name, DATE_FROM as 'From Date', DATE_TO AS 'To Date', DESCRIPTION as Description  from TSPL_PAYPERIOD_MASTER"
        findPayperiod.Value = clsCommon.ShowSelectForm("PAYPERIOD_Master", qry, "Code", "POSTED=1 and FREEZED=0", findPayperiod.Value, "PAY_PERIOD_CODE", isButtonClicked)
        If clsCommon.myLen(findPayperiod.Value) > 0 Then
            Dim clspp As clsPayPeriodMaster
            clspp = clsPayPeriodMaster.GetData(findPayperiod.Value, NavigatorType.Current)
            lblPayPeriodName.Text = clspp.Name
            findPayperiod.Value = clspp.Code
        Else
            lblPayPeriodName.Text = ""

        End If
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim whrcls As String = Nothing
        Dim LocCode As String = Nothing
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
            If clsCommon.myLen(LocCode) > 0 Then
                whrcls = " Location_Type='Physical' And LOCATION_CODE='" + LocCode + "'"
            Else
                whrcls = " Location_Type='Physical' "
            End If
        End If
        fndLocation.Value = clsLocation.getFinder(whrcls, Me.fndLocation.Value, isButtonClicked)
        lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btngo_Click(sender As Object, e As EventArgs) Handles btngo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        TemplateGridview = gv1
        Griddetail()
    End Sub
    Sub Griddetail()
        Try
            Dim qry As String = ""
            Dim dt As New DataTable
            If clsCommon.myLen(findPayperiod.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Pay Period Code ")
                Exit Sub
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location")
                Exit Sub
            End If
            If clsCommon.myLen(fndBankCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "please select Bank Code")
            End If
            qry = "SELECT row_number() over(order by(select 1)) as SNo, LA.EMP_CODE,EMP.EMP_NAME,EMP.FATHERS_NAME,emp.Designation,LA.LOAN_CODE,LA.LOAN_DATE,LA.EMI_NO,LA.EMI_AMOUNT, COALESCE(ADJ.ADJUSTMENT_PLUS,0) AS ADJUSTMENT_PLUS,COALESCE(ADJ.ADJUSTMENT_MINUS,0) AS ADJUSTMENT_MINUS  ,(LA.EMI_AMOUNT+COALESCE(ADJ.ADJUSTMENT_PLUS,0)-COALESCE(ADJ.ADJUSTMENT_MINUS,0)) AS NET_EMI FROM (  select T1.EMP_CODE,T1.LOAN_CODE,T1.LOAN_DATE,MIN(T2.EMI_NO) AS EMI_NO,T2.EMI_AMOUNT  from TSPL_LOAN_APPLICATION T1 JOIN TSPL_LOANEMI_DETAIL T2 ON T1.LOAN_CODE=T2.LOAN_CODE  LEFT JOIN (SELECT TT1.LOAN_GENERATION_CODE,TT2.LOAN_CODE,TT2.EMP_CODE,TT2.EMI_NO  FROM TSPL_LOAN_GENERATION TT1 JOIN TSPL_LOANGENERATION_DETAIL TT2  ON TT1.LOAN_GENERATION_CODE=TT2.LOAN_GENERATION_CODE WHERE TT1.PAY_PERIOD_CODE!='" + findPayperiod.Value + "') AS T3  ON T2.LOAN_CODE=T3.LOAN_CODE AND T2.EMI_NO=T3.EMI_NO WHERE T3.EMI_NO IS NULL and T1.PAID=1 and T1.POSTED =1 and t1.LOAN_DATE <=(select convert(date,DATE_TO,103) from TSPL_PAYPERIOD_MASTER where PAy_Period_Code='" + findPayperiod.Value + "')   GROUP BY T1.LOAN_CODE,T1.LOAN_DATE,T1.EMP_CODE,T2.EMI_AMOUNT) AS LA  LEFT JOIN (select  ADJ.EMP_CODE,ADJ.LOAN_CODE,SUM(ADJ.ADJUSTMENT_PLUS) AS ADJUSTMENT_PLUS, SUM(ADJ.ADJUSTMENT_MINUS) AS ADJUSTMENT_MINUS  from TSPL_LOAN_ADJUSTMENT ADJ WHERE ADJ.PAY_PERIOD_CODE='" + findPayperiod.Value + "' AND GENERATED=0  GROUP BY ADJ.EMP_CODE,ADJ.LOAN_CODE) ADJ ON LA.EMP_CODE=ADJ.EMP_CODE AND LA.LOAN_CODE=ADJ.LOAN_CODE  
                        LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON LA.EMP_CODE=EMP.EMP_CODE  
                        left join tspl_location_master on tspl_location_master.Location_Code =EMP.LOCATION_CODE left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =emp.DEVISION_CODE  where 2=2  and emp.LOCATION_CODE ='" + fndLocation.Value + "' ORDER BY  LA.EMP_CODE,LA.LOAN_CODE "
            dt = clsDBFuncationality.GetDataTable(qry)
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.MasterView.Refresh()
            FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found ", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBankCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBankCode._MYValidating
        Try
            Dim qry As String = "select Bank_Code as Code,Bank_Name as Name,City_Code from tspl_vendor_bank_master"
            fndBankCode.Value = clsCommon.ShowSelectForm("bnkcode", qry, "Code", "", fndBankCode.Value, "Code", isButtonClicked)
            lblBankCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Bank_Name from tspl_vendor_bank_master where Bank_Code='" & fndBankCode.Value & "'"))
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text, MessageBoxButtons.OK)
        End Try
    End Sub
    Sub FormatGrid()
        If gv1 Is Nothing Then
            Return
        End If

        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = True

        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = True
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next

        Dim sNoColumn As GridViewDataColumn = gv1.Columns("SNo")
        If sNoColumn IsNot Nothing Then
            sNoColumn.IsVisible = True
            sNoColumn.Width = 100
            sNoColumn.HeaderText = "SNo"
        End If

        Dim dcsCodeColumn As GridViewDataColumn = gv1.Columns("EMP_CODE")
        If dcsCodeColumn IsNot Nothing Then
            dcsCodeColumn.IsVisible = True
            dcsCodeColumn.Width = 100
            dcsCodeColumn.HeaderText = "Employee CODE"
        End If

        Dim dcsNameColumn As GridViewDataColumn = gv1.Columns("EMP_NAME")
        If dcsNameColumn IsNot Nothing Then
            dcsNameColumn.IsVisible = True
            dcsNameColumn.Width = 100
            dcsNameColumn.HeaderText = "Employee NAME"
        End If

        Dim applyCowPriceColumn As GridViewDataColumn = gv1.Columns("Designation")
        If applyCowPriceColumn IsNot Nothing Then
            applyCowPriceColumn.IsVisible = True
            applyCowPriceColumn.Width = 100
            applyCowPriceColumn.HeaderText = "Designation"
        End If

        Dim startDateColumn As GridViewDateTimeColumn = gv1.Columns("FATHERS_NAME")
        If startDateColumn IsNot Nothing Then
            startDateColumn.IsVisible = True
            startDateColumn.Width = 100
            startDateColumn.HeaderText = "Father's Name"
        End If

        Dim netemi As GridViewDateTimeColumn = gv1.Columns("NET_EMI")
        If netemi IsNot Nothing Then
            netemi.IsVisible = True
            netemi.Width = 100
            netemi.HeaderText = "Net EMI"
        End If

        gv1.ShowGroupPanel = False
        gv1.MasterTemplate.AutoExpandGroups = True
    End Sub

End Class