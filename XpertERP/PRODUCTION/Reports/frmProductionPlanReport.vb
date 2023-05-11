'--03/10/2013--form Add By- panch raj ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Public Class frmProductionPlanReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "DatewiseProductionReport"
    Dim formtype As String = Nothing

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        formtype = formid
    End Sub
#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
#End Region
    Sub LoadData()

        If isInsideLoadData Then
            clsCommon.MyMessageBoxShow("Work in Progress Please Wait...")
            Exit Sub
        End If

        btnGenrate.Enabled = True
        isInsideLoadData = True
        btnGenrate.Enabled = False
        
        funPrint()
       
        isInsideLoadData = False
        btnGenrate.Enabled = True
    End Sub
    Sub funPrint()
        Try
            Dim qry As String = ""
            qry += " SELECT  T2.PROD_PLAN_CODE,convert(VARCHAR,T2.PLAN_FOR_DATE,103) AS PLANNING_DATE,  T1.PRODUCTION_LINE_CODE,T4.PRODUCTION_LINE_NAME,T1.ITEM_CODE AS "
            qry += " MAIN_ITEM_CODE,T5.Item_Desc AS MAIN_ITEM_DESC,T6.Class_Code AS PACKAGE,T7.Class_Code AS FLAVOUR, "
            qry += " T3.MIN_QUANTITY AS MIN_QTY,T3.MAX_QUANTITY AS MAX_QTY,CONVERT(varchar(5),T3.START_TIME,108) AS START_TIME,T3.SPEED , "
            qry += " (CONVERT(VARCHAR,T2.PLAN_FOR_DATE ,103) + ' ' + CONVERT(varchar(5),T3.END_TIME,108)) AS STOP_TIME,T3.REASON, ('" & objCommonVar.CurrentCompanyName & "')  AS COMPANY_NAME  FROM TSPL_MF_PROD_PLAN_DETAIL T1 INNER JOIN "
            qry += " TSPL_MF_PRODUCTION_PLAN_HEAD T2 ON T1.PROD_PLAN_CODE=T2.PROD_PLAN_CODE  "
            qry += " LEFT JOIN TSPL_MF_BATCH_PP_DETAIL T3 ON T2.PROD_PLAN_CODE=T3.PROD_PLAN_CODE "
            qry += " LEFT JOIN TSPL_MF_PRODUCTION_LINES T4 ON T1.PRODUCTION_LINE_CODE=T4.PRODUCTION_LINE_CODE "
            qry += " LEFT JOIN TSPL_ITEM_MASTER T5 ON T1.ITEM_CODE=T5.Item_Code "
            qry += " LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details WHERE Class_Name='SIZE') T6 ON T1.ITEM_CODE=T6.Item_Code "
            qry += " LEFT JOIN (SELECT ITEM_CODE,CLASS_CODE,CLASS_DESC FROM TSPL_ITEM_details WHERE Class_Name='FLAVOUR') T7 ON T1.ITEM_CODE=T7.Item_Code "
            qry += " WHERE T2.PLAN_FOR_DATE BETWEEN '" & Format(dtpFrom.Value, "dd/MMM/yyyy") & "' and '" & Format(dtpTo.Value, "dd/MMM/yyyy") & "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.PRODUCTION, dt, "crptProductionPlan", "Production Plan")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmProductionPlanReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        dtpFrom.Value = clsCommon.GETSERVERDATE
        dtpTo.Value = clsCommon.GETSERVERDATE

    End Sub

    Private Sub SetUserMgmtNew()
        If formtype = clsUserMgtCode.frmProductionPlanReportSTD Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionPlanReportSTD)
        ElseIf formtype = clsUserMgtCode.frmProductionPlanReportPepsi Then
            ''MyBase.SetUserMgmt(clsUserMgtCode.frmProductionPlanReportPepsi)
        End If
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
        
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub


    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmProductionPlanReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Sub LoadGridColumns()


    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If clsCommon.myLen(ReportID) > 0 Then

            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()

            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            'obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
   

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
    End Sub

   
   
    'Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
    '    'If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
    '    '    e.CellElement.TextAlignment = ContentAlignment.MiddleRight
    '    '    e.CellElement.Font = New Font(e.CellElement.Font, FontStyle.Bold)

    '    'End If
    'End Sub

End Class
