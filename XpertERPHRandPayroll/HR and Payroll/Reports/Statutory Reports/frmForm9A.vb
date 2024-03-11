'--13/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine

Public Class frmForm9A
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "frmForm9A"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    Dim DT_Details As DataTable
#End Region
    Sub LoadData()
        Try
            If clsCommon.myLen(txtCode.Value) > 0 Then
                gv1.DataSource = Nothing
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.Rows.Clear()
                gv1.Columns.Clear()

                Dim Qry As String = ""
                Qry += " SELECT ROW_NUMBER ( ) OVER ( order by [EMP_CODE] ) AS Sno, [EMP_CODE] as 'Employee Code',[PF_NO] AS 'PF No' ,[Emp_Name] as  'Employee Name' ,[FATHERS_NAME] as 'Fathers Name' ,[Birth_date] as 'Date of Birth' ,[SEX] AS 'Gender', [Joining_date] as 'Col5',"
                Qry += " '' as 'Col1',"
                Qry += " '' as 'Col2',"
                Qry += " '' as 'Initials of S.S.',"
                Qry += " [RELIEVING_DATE] as 'Releaving Date',[LEAVING_REASON] as 'Leaving Reason' , '' as 'Col3'  "
                Qry += " FROM [TSPL_EMPLOYEE_MASTER] "
                Qry += " where [EMP_CODE] in ( SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE FROM TSPL_GENERATE_SALARY "
                Qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtCode.Value + "' ) "
                DT = clsDBFuncationality.GetDataTable(Qry)

                Using Me.gv1.DeferRefresh()
                    Me.gv1.DataSource = Nothing
                    Me.gv1.Rows.Clear()
                    Me.gv1.Columns.Clear()

                    Me.gv1.DataSource = DT
                    Me.gv1.BestFitColumns()
                    ReStoreGridLayout()

                    gv1.MasterView.TableHeaderRow.Height = 80
                    gv1.MasterView.TableHeaderRow.AllowResize = True

                    gv1.Columns("Col1").IsVisible = True
                    gv1.Columns("Col1").Width = 200
                    gv1.Columns("Col1").HeaderText = "Total period of previous service " + Environment.NewLine + " ( excluding periods of breaks) as " + Environment.NewLine + " on the date of joining the Fund"

                    gv1.Columns("Col2").IsVisible = True
                    gv1.Columns("Col2").Width = 150
                    gv1.Columns("Col2").HeaderText = "Machine EDP NO. of " + Environment.NewLine + "  Ledger Card opened "

                    gv1.Columns("Col3").IsVisible = True
                    gv1.Columns("Col3").Width = 200
                    gv1.Columns("Col3").HeaderText = "Remarks and initials on settlement" + Environment.NewLine + " D.C., S.S., A.O.," + Environment.NewLine + " E.P.F., E.P.S., D.L.I."

                    gv1.Columns("Col5").IsVisible = True
                    gv1.Columns("Col5").Width = 150
                    gv1.Columns("Col5").HeaderText = "Date of eligibility " + Environment.NewLine + "of membership "

                End Using
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmForm9A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmForm9A)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub
    Private Sub frmForm9A_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        End If
    End Sub

    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoExl.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("CODE NO. : -                                               Folio No. ……………………	")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("FORM - 9 (Revised)", gv1, arr, "Employee Voucher Payment")
        clsCommon.MyExportToExcelGrid("Form 9 (Revised)", gv1, arr, "Form 9 (Revised)", False)
    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("CODE NO. : -                                               Folio No. ……………………	")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Form 9 (Revised)", gv1, arr, "Form 9 (Revised)", False)
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtCode.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtCode.Value, "", isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(txtCode.Value, Nothing)
    End Sub
    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        LoadData()
    End Sub
End Class
