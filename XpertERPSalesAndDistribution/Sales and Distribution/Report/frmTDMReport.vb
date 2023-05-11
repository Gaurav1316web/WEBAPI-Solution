'--form Add By- Panch Raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmTDMReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "TDMReport"

#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    'Dim DT_Details As DataTable

#End Region
    Sub LoadData()
        Try


            isInsideLoadData = True
            btnGenrate.Enabled = False
            DT = GetTDMDT(clsCommon.myCstr(fndEmployee.Value), Me.dtptodate.Value)

            SetupMasterForAutoGenerateHierarchy()
            btnGenrate.Enabled = True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            btnGenrate.Enabled = True
        End Try
    End Sub

    Private Sub frmTDMReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        Me.dtptodate.Value = Today
        'ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmTDMReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()

        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub frmTDMReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        LoadData()
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
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
        arr.Add("TDM Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")

        clsCommon.MyExportToExcelGrid("TDM Report", gv1, arr, "TDM Report", False)

    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpoPDF.Click
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("TDM Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("TDM Report", gv1, arr, "TDM Report", False)
    End Sub

#Region "grid operations"

    Private Sub SetupMasterForAutoGenerateHierarchy()
        Using Me.gv1.DeferRefresh()
            Me.gv1.AutoGenerateHierarchy = True
            Me.gv1.MasterTemplate.Reset()
            Me.gv1.TableElement.RowHeight = 20
            Me.gv1.DataSource = DT

            Me.gv1.MasterTemplate.Columns("TARGET_DATE").HeaderText = "Target Date"
            Me.gv1.MasterTemplate.Columns("Employee_Code").HeaderText = "Employee Code"
            Me.gv1.MasterTemplate.Columns("Employee_Name").HeaderText = "Employee Name"
            Me.gv1.MasterTemplate.Columns("targetQty").HeaderText = "Target Given"
            Me.gv1.MasterTemplate.Columns("Order_Qty").HeaderText = "Target Achieved"

            Me.gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill


        End Using
    End Sub

#End Region

    Private Sub fndEmployee__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndEmployee._MYValidating
        Dim qry As String = "select EMP_CODE as [EmployeeCode] ,Emp_Name as [Employee Name]  from TSPL_EMPLOYEE_MASTER"
        Dim whrcls As String = " Emp_Status='Active'"
        'fndEmployee.Value = clsCommon.ShowSelectForm("EMPMasFND", qry, "EmployeeCode", whrcls, fndEmployee.Value, "", isButtonClicked)
        fndEmployee.Value = clsCommon.ShowSelectForm("EMPB", qry, "EmployeeCode", whrcls, fndEmployee.Value, "", isButtonClicked)
        'txtdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + fndEmployee.Value + "'"))


    End Sub
    Function GetTDMDT(ByVal EMP_CODE As String, ByVal toDate As Date) As DataTable
        Dim DT As DataTable
        Dim strq As String
        Dim cond As String = ""

        If EMP_CODE = "" Then
            cond = " WHERE T1.TARGET_DATE='" & clsCommon.GetPrintDate(toDate, "dd/MMM/yyyy") & "'"
        Else
            cond = " WHERE T1.TARGET_DATE='" & clsCommon.GetPrintDate(toDate, "dd/MMM/yyyy") & "' and T1.Employee_Code='" & EMP_CODE & "'"
        End If
        strq = ""

        strq += " SELECT DISTINCT Convert(VARCHAR, T1.TARGET_DATE, 103) AS TARGET_DATE,T1.Employee_Code,T3.EMP_NAME AS EMPLOYEE_NAME,T1.Flavour,T1.Qty AS TargetQty,COALESCE(T2.Order_Qty,0) AS Order_Qty,(T1.TargetQty-COALESCE(T2.Order_Qty,0)) AS Balance "
        strq += " FROM TSPL_TDMWISE_TARGET_DETAIL T1"
        strq += " LEFT JOIN ("
        strq += " SELECT T1.Order_Date,T1.Level3_User_code AS EMPLOYEE_CODE,T2.FLAVOUR,SUM(T2.Order_Qty) AS Order_Qty  FROM TSPL_SALES_ORDER_HEAD T1 INNER JOIN ("
        strq += " SELECT T1.Order_No,T1.Item_Code,T2.Class_Code AS FLAVOUR,Order_Qty FROM TSPL_SALES_ORDER_DETAIL T1 "
        strq += " LEFT JOIN (SELECT * FROM TSPL_ITEM_DETAILS WHERE Class_Name='FLAVOUR') T2 ON T1.Item_Code=T2.Item_Code) AS T2 ON T1.Order_No=T2.Order_No"
        strq += " GROUP BY T1.Order_Date,T1.Level3_User_code,T2.FLAVOUR) AS T2 ON T1.TARGET_DATE=Convert(DATE,T2.Order_Date,103) AND T1.Employee_Code=T2.Employee_Code AND T1.Flavour=T2.FLAVOUR"
        strq += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T1.Employee_Code=T3.EMP_CODE "
        strq += cond & " AND T1.Flavourwise='Y'"

        strq += "UNION ALL"
        strq += " SELECT DISTINCT Convert(VARCHAR, T1.TARGET_DATE, 103) AS TARGET_DATE,T1.Employee_Code,T3.EMP_NAME AS EMPLOYEE_NAME,'' AS Flavour,T1.TargetQty,COALESCE(T2.Order_Qty,0) AS Order_Qty,(T1.TargetQty-COALESCE(T2.Order_Qty,0)) AS Balance "
        strq += " FROM TSPL_TDMWISE_TARGET_DETAIL T1"
        strq += " LEFT JOIN ("
        strq += " SELECT T1.Order_Date,T1.Level3_User_code AS EMPLOYEE_CODE,SUM(T2.Order_Qty) AS Order_Qty  FROM TSPL_SALES_ORDER_HEAD T1 INNER JOIN ("
        strq += " SELECT T1.Order_No,T1.Item_Code,T2.Class_Code AS FLAVOUR,Order_Qty FROM TSPL_SALES_ORDER_DETAIL T1 "
        strq += " LEFT JOIN (SELECT * FROM TSPL_ITEM_DETAILS WHERE Class_Name='FLAVOUR') T2 ON T1.Item_Code=T2.Item_Code) AS T2 ON T1.Order_No=T2.Order_No"
        strq += " GROUP BY T1.Order_Date,T1.Level3_User_code) AS T2 ON T1.TARGET_DATE=Convert(DATE,T2.Order_Date,103) AND T1.Employee_Code=T2.Employee_Code"
        strq += " LEFT JOIN TSPL_EMPLOYEE_MASTER T3 ON T1.Employee_Code=T3.EMP_CODE  "
        strq += cond & " AND T1.Flavourwise='N'"
        'strq += "ORDER BY T1.TARGET_DATE,T1.Employee_Code"

        DT = clsDBFuncationality.GetDataTable(strq)
        Return DT




    End Function
End Class
