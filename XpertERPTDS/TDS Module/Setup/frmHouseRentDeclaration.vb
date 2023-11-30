' ------------------------- Created By Preeti Gupta  --------------------'
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports System.Text.RegularExpressions

Public Class FrmHouseRentDeclaration
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
#End Region

#Region "Functions"
    Public Function SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmHouseRentDeclaration)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Return False
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadMenuItem3.Enabled = MyBase.isModifyFlag
        Return True
    End Function
    Function AllowToSave() As Boolean
        Dim FinYr As Double
        Dim PayPer As Double
        Dim EmpCode As Double
        If clsCommon.myLen(txtCode.Value) <= 0 Or clsCommon.myLen(txtCode.Value) > 30 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code can not be left blank", Me.Text)
            txtcode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtFinYear.Value) <= 0 Then
            myMessages.blankValue("Financial year entry code")
            txtFinYear.Focus()
            Return False
        ElseIf clsCommon.myLen(TxtPayPeriod.Value) <= 0 Then
            myMessages.blankValue("Pay period code")
            TxtPayPeriod.Focus()
            Return False
        ElseIf clsCommon.myLen(TxtEmpCode.Value) <= 0 Then
            myMessages.blankValue("Employee master code")
            TxtEmpCode.Focus()
            Return False
        ElseIf clsCommon.myCdbl(txtHouseRentAmt.Text) < 0 Then
            clsCommon.MyMessageBoxShow(Me, "House rent amount can not be negative", Me.Text)
            txtHouseRentAmt.Focus()
            Return False
        End If
        If clsCommon.myLen(txtFinYear.Value) > 0 Then
            FinYr = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_TDS_FINANCIAL_YEAR Where Year_Name='" & clsCommon.myCstr(txtFinYear.Value) & "'"))
            If FinYr = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! Finincial year code (" & clsCommon.myCstr(txtFinYear.Value) & ") does not exists.")
                txtFinYear.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(TxtPayPeriod.Value) > 0 Then
            PayPer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*)  From TSPL_PAYPERIOD_MASTER Where PAY_PERIOD_CODE='" & clsCommon.myCstr(TxtPayPeriod.Value) & "'"))
            If PayPer = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! Pay period code (" & clsCommon.myCstr(TxtPayPeriod.Value) & ") does not exists.")
                TxtPayPeriod.Focus()
                Return False
            End If
        End If
        If clsCommon.myLen(TxtEmpCode.Value) > 0 Then
            EmpCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) From TSPL_EMPLOYEE_MASTER Where EMP_CODE='" & clsCommon.myCstr(TxtEmpCode.Value) & "'"))
            If EmpCode = 0 Then
                common.clsCommon.MyMessageBoxShow("Please check ! Employee code (" & clsCommon.myCstr(TxtEmpCode.Value) & ") does not exists.")
                TxtEmpCode.Focus()
                Return False
            End If
        End If
        UcAttachment1.AllowToSave()
        Return True
    End Function
    Sub funReset()
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        TxtDesp.Text = ""
        txtFinYear.Value = ""
        TxtEmpCode.Value = ""
        TxtPayPeriod.Value = ""
        txtHouseRentAmt.Text = 0
        lblPayName.Text = ""
        lblEmpName.Text = ""

        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        'txtCode.MyReadOnly = True
        btnsave.Enabled = True
        'btndelete.Enabled = True
        'isNewEntry = False
        Dim obj As New ClsHouseRentDeclaration()
        obj = ClsHouseRentDeclaration.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.CODE) > 0) Then
            funReset()
            isNewEntry = False
            btndelete.Enabled = True
            txtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True
            txtCode.Value = obj.CODE
            TxtDesp.Text = obj.Description
            txtFinYear.Value = obj.Financial_Year_Code
            TxtPayPeriod.Value = obj.Pay_Period_Code
            TxtEmpCode.Value = obj.Employee_Code
            txtHouseRentAmt.Text = obj.House_Rent_Amount
            lblEmpName.Text = obj.Emp_Name
            TxtEmpCode.Value = obj.Employee_Code
            lblPayName.Text = obj.PAY_PERIOD_NAME
            UcAttachment1.LoadData(obj.CODE)
        End If
    End Sub
    Public Function Save()
        If AllowToSave() Then
            Dim obj As New ClsHouseRentDeclaration()
            obj.CODE = txtCode.Value
            obj.Description = TxtDesp.Text
            obj.Financial_Year_Code = txtFinYear.Value
            obj.Pay_Period_Code = TxtPayPeriod.Value
            obj.Employee_Code = TxtEmpCode.Value

            If String.IsNullOrEmpty(txtHouseRentAmt.Text) Then
                txtHouseRentAmt.Text = 0
            End If
            obj.House_Rent_Amount = txtHouseRentAmt.Text

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (obj.SaveData(obj, isNewEntry, trans)) Then
                trans.Commit()
                UcAttachment1.SaveData(obj.CODE)
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.CODE, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                trans.Rollback()
            End If
        End If
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Code not found to delete", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsHouseRentDeclaration.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
#End Region

    Private Sub FrmHouseRentDeclaration_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnreset.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub FrmHouseRentDeclaration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New ")
        funReset()
        ''For Attachment
        If objCommonVar.IsDemoERP Then
            UcAttachment1.Form_ID = MyBase.Form_ID
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible
        Else
            RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Collapsed
        End If
        ''End of For Attachmen
    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
        Dim str As String = "select count(*) from TSPL_HOUSE_RENT_DECLARATION where CODE ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = ""
            qry = "Select CODE As [Code],Description As [Description],FINANCIAL_YEAR_CODE As [Financial Year Code],Pay_Period_Code As [Pay Period Code],EMP_CODE As [Emp Code],HOUSE_RENT_AMOUNT As [House Rent Amount] from TSPL_HOUSE_RENT_DECLARATION"
            txtcode.Value = clsCommon.ShowSelectForm("TSPL_HOUSE_RENT_DECLARATION", qry, "Code", "", txtcode.Value, "TSPL_HOUSE_RENT_DECLARATION.CODE", isButtonClicked)
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As ClsHouseRentDeclaration
                objOT = ClsHouseRentDeclaration.GetData(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    LoadData(txtcode.Value, NavigatorType.Current)
                End If
            Else
                funReset()
            End If
        End If

    End Sub


    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        funReset()
    End Sub

    Private Sub txtFinYear__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtFinYear._MYValidating
        Dim qry As String = " Select Year_Name AS CODE  From TSPL_TDS_FINANCIAL_YEAR "
        txtFinYear.Value = clsCommon.ShowSelectForm("fmFinYr", qry, "Code", "", txtFinYear.Value, "CODE", isButtonClicked)
    End Sub

    Private Sub TxtPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtPayPeriod._MYValidating
        Dim qry As String = " Select PAY_PERIOD_CODE AS CODE ,PAY_PERIOD_NAME  From TSPL_PAYPERIOD_MASTER "
        TxtPayPeriod.Value = clsCommon.ShowSelectForm("fmPayPeriod", qry, "Code", "", TxtPayPeriod.Value, "CODE", isButtonClicked)
        lblPayName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select PAY_PERIOD_NAME  From TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + TxtPayPeriod.Value + "'"))
    End Sub

    Private Sub TxtEmpCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtEmpCode._MYValidating
        Dim qry As String = " Select EMP_CODE AS CODE ,Emp_Name  From TSPL_EMPLOYEE_MASTER "
        TxtEmpCode.Value = clsCommon.ShowSelectForm("fmEmpCode", qry, "Code", "", TxtEmpCode.Value, "CODE", isButtonClicked)
        lblEmpName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name  From TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + TxtEmpCode.Value + "'"))
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim FIN_CODE As String = ""
        Dim EMP_CODE As String = ""
        Dim PAY_CODE As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Code", "Description", "Financial Year Code", "Pay Period Code", "Emp Code", "House Rent Amount") Then
            Dim trans As SqlTransaction
            trans = clsDBFuncationality.GetTransactin()
            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New ClsHouseRentDeclaration()
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Code can not be blank or incorrect.")
                    End If
                    obj.CODE = strCode

                    Dim strDesp As String = clsCommon.myCstr(grow.Cells("Description").Value)
                    If strDesp.Length > 100 Then
                        Throw New Exception("Description can should be max. 100 character On Line No " & LineNo & ".")
                    End If
                    obj.Description = strDesp

                    Dim strFinYear As String = clsCommon.myCstr(grow.Cells("Financial Year Code").Value)
                    If strFinYear.Length > 20 Or (String.IsNullOrEmpty(strFinYear)) Then
                        Throw New Exception("Financial Year Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        FIN_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_TDS_FINANCIAL_YEAR Where Year_Name ='" & strFinYear & "'", trans)
                        If FIN_CODE <= 0 Then
                            Throw New Exception("Financial Year Code(" & strFinYear & ") On Line No " & LineNo & " does not exist . Please make it entry first.")
                        End If
                    End If
                    obj.Financial_Year_Code = strFinYear

                    Dim strPay As String = clsCommon.myCstr(grow.Cells("Pay Period Code").Value)
                    If strPay.Length > 30 Or (String.IsNullOrEmpty(strPay)) Then
                        Throw New Exception("Pay Period Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        PAY_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_PAYPERIOD_MASTER Where PAY_PERIOD_CODE ='" & strPay & "'", trans)
                        If PAY_CODE <= 0 Then
                            Throw New Exception("Pay Period Code(" & strPay & ") On Line No " & LineNo & " does not exist.Please make it entry first.")
                        End If
                    End If
                    obj.Pay_Period_Code = strPay

                    Dim strEmpCode As String = clsCommon.myCstr(grow.Cells("Emp Code").Value)
                    If strEmpCode.Length > 12 Or (String.IsNullOrEmpty(strEmpCode)) Then
                        Throw New Exception("Emp Code can not be blank or incorrect On Line No " & LineNo & ".")
                    Else
                        EMP_CODE = clsDBFuncationality.getSingleValue("SELECT COUNT(*) FROM TSPL_EMPLOYEE_MASTER Where EMP_CODE ='" & strEmpCode & "'", trans)
                        If EMP_CODE <= 0 Then
                            Throw New Exception("Emp Code(" & strEmpCode & ") On Line No " & LineNo & " does not exist . Please make it entry first.")
                        End If
                    End If
                    obj.Employee_Code = strEmpCode

                    Dim Amt As String = clsCommon.myCstr(grow.Cells("House Rent Amount").Value)
                    If String.IsNullOrEmpty(Amt) Then
                        Amt = "0"
                    ElseIf Not IsNumeric(grow.Cells("House Rent Amount").Value) Or clsCommon.myCdbl(grow.Cells("House Rent Amount").Value) < 0 Then
                        Throw New Exception("House Rent Amount Should be Numeric or in correct format On Line No " & LineNo & " ")
                    ElseIf Amt.Length > 10 Then
                        Throw New Exception("Please check ! length of house rent amount should be max 10 digits on line no " & LineNo & ".")
                    End If
                    obj.House_Rent_Amount = Amt

                    obj.SaveData(obj, ClsHouseRentDeclaration.CheckNewEntry(obj.CODE, trans), trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub Export_Click(sender As Object, e As EventArgs) Handles Export.Click
        Dim str As String
        str = "Select CODE As [Code],Description As [Description],FINANCIAL_YEAR_CODE As [Financial Year Code],Pay_Period_Code As [Pay Period Code],EMP_CODE As [Emp Code],HOUSE_RENT_AMOUNT As [House Rent Amount] from TSPL_HOUSE_RENT_DECLARATION"
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
