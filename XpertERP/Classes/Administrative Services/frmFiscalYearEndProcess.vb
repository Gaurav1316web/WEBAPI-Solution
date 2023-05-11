Imports common
Imports System
Imports System.Data.SqlClient

Public Class FrmFiscalYearEndProcess
    Inherits FrmMainTranScreen


    Private Sub FrmFiscalYearEndProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        lblNextFiscalYear.Text = ""
        MyLabel6.Text = objCommonVar.CurrFiscalYear
        btnSave.Enabled = IIf(clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select is_End_Year_Proceed from TSPL_Fiscal_Year_Master where Fiscal_Code='" + objCommonVar.CurrFiscalYear + "'")) = 1, False, True)
    End Sub

    Private Sub txtFinancialYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtNxtFinancialYear._MYValidating
        Dim qry As String = "select fiscal_code as Code,fiscal_name as Description,convert(varchar, Start_Date,103) as [Start Date],convert(varchar,End_Date,103) as [End Date] from TSPL_Fiscal_Year_Master"
        Dim WhrCls As String = "is_end_year_proceed=0 and fiscal_code not in ('" + objCommonVar.CurrFiscalYear + "')"

        txtNxtFinancialYear.Value = clsCommon.ShowSelectForm("FisYEarEbd", qry, "Code", WhrCls, txtNxtFinancialYear.Value, "Code", isButtonClicked)
        lblNextFiscalYear.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select fiscal_name as Description  from TSPL_Fiscal_Year_Master where fiscal_code ='" + txtNxtFinancialYear.Value + "'"))
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FisaclYearEndProcess)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            If clsCommon.myLen(txtNxtFinancialYear.Value) <= 0 Then
                txtNxtFinancialYear.Focus()
                Throw New Exception("Please select financial year")
            End If
            If clsCommon.MyMessageBoxShow("Start the finanacial year end process" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    ' transportSql.CreateJEForEndYear("", , trans)
                    ''richa agarwal changes done against ticket no.BM00000009404 on 4Aug,2016
                    Dim strcurrentfisyearenddate As DateTime? = Nothing
                    strcurrentfisyearenddate = clsDBFuncationality.getSingleValue("select End_Date from TSPL_Fiscal_Year_Master where Fiscal_Code='" + objCommonVar.CurrFiscalYear + "' ", trans)
                    transportSql.CreateJEForEndYear("", strcurrentfisyearenddate, trans)
                    ''-------------------------

                    Dim qry As String = "Update TSPL_Fiscal_Year_Master set is_End_Year_Proceed=1 where Fiscal_Code='" + objCommonVar.CurrFiscalYear + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "Update TSPL_Fiscal_Year_Master set Is_Current_Year=0 "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    qry = "Update TSPL_Fiscal_Year_Master set Is_Current_Year=1 where Fiscal_Code='" + txtNxtFinancialYear.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)

                    trans.Commit()
                    objCommonVar.RefreshCommonVar()
                    clsCommon.MyMessageBoxShow("Successfully complete the process")
                    clsERPFuncationality.closeForm(Me)
                Catch ex As Exception
                    trans.Rollback()
                    Throw New Exception(ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

     

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub FrmFiscalYearEndProcess_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            If pnlYearEndRollback.Visible Then
                pnlYearEndRollback.Visible = False
            Else
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIRC
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    pnlYearEndRollback.Visible = Not pnlYearEndRollback.Visible
                End If
            End If
        End If
    End Sub

    Private Sub TxtFinder1__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtFinder1._MYValidating
        Try
            Dim qry As String = "select fiscal_code as Code,fiscal_name as Description,convert(varchar, Start_Date,103) as [Start Date],convert(varchar,End_Date,103) as [End Date] from TSPL_Fiscal_Year_Master"
            Dim WhrCls As String = "is_end_year_proceed=1"
            TxtFinder1.Value = clsCommon.ShowSelectForm("FisYEarEbdT", qry, "Code", WhrCls, TxtFinder1.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If clsCommon.myLen(TxtFinder1.Value) <= 0 Then
                TxtFinder1.Focus()
                Throw New Exception("Please select the Rollback fiscal year")
            End If
            If clsCommon.MyMessageBoxShow("Rollback Year End Process of Fiscal Year -" + TxtFinder1.Value + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim qry As String = "delete from TSPL_JOURNAL_DETAILS where Voucher_No in ( select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='GL-JE' and Transaction_Type='X' and convert(date,Source_Doc_Date,103)=(select convert(date, End_Date,103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + TxtFinder1.Value + "') and Voucher_Desc like 'Fiscal Year End for%')"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "delete from TSPL_JOURNAL_MASTER where Source_Code='GL-JE' and Transaction_Type='X' and convert(date,Source_Doc_Date,103)=(select convert(date, End_Date,103) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + TxtFinder1.Value + "') and Voucher_Desc like 'Fiscal Year End for%'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update tspl_fiscal_year_master set Is_Current_Year=0"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                qry = "update tspl_fiscal_year_master set Is_Current_Year=1,is_End_Year_Proceed=0 where Fiscal_Code='" + TxtFinder1.Value + "'"
                clsDBFuncationality.ExecuteNonQuery(qry, trans)

                trans.Commit()
                TxtFinder1.Value = ""
                clsCommon.MyMessageBoxShow("Task Completed", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
