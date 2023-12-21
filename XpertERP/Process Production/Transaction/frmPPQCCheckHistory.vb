Imports common
Imports System.Data.SqlClient


Public Class FrmPPQCCheckHistory
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isInsideLoaddata As Boolean = False
    Dim isCellvaluechanged As Boolean = False
    Dim ButtonToolTip As New ToolTip()
    Public Item_Code As String = Nothing

    Const colSelect As String = "Select"
    Const colDoctype As String = "Doctype"
    Const coldocno As String = "docno"
    Const coldate As String = "date"
    Const colitemcode As String = "itmcode"
    Const coldesc As String = "desc"
    Const colremarks As String = "remarks"
#End Region

    Private Sub FrmPPQCCheckHistory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F5 Then
                btnOk.PerformClick()
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            isCellvaluechanged = False
        End Try
    End Sub

    Private Sub FrmPPQCCheckHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadBlankGrid()

        ButtonToolTip.SetToolTip(btnOk, "Press F5 for select record.")
        ButtonToolTip.SetToolTip(btnclose, "Press Esc for close window.")

        btnOk.Visible = False
    End Sub

    Private Sub LoadBlankGrid()
        Try
            isInsideLoaddata = True
            Dim qry As String = "select cast(0 as bit) as [Select],axa.doc_type as [Document Type],axa.QC_No as [Document No],axa.QC_In_Date_Time as [Date],axa.Item_Code as [Item Code],tspl_item_master.item_desc as [Description],Remarks from ("
            qry += "select ROW_NUMBER() over (partition by Doc_Type order by QC_In_Date_Time desc) as sno,* from ("
            qry += "select distinct 'MCC' as Doc_Type,QC_No,QC_In_Date_Time,Item_Code,Remarks from tspl_quality_check where Item_Code='" + Item_Code + "'"
            qry += " union all "
            qry += "select distinct 'Standardization' as Doc_Type,TSPL_PP_STD_QC_DETAIL.Standardization_Code as QC_No,TSPL_PP_STANDARDIZATION_HEAD.Standardization_Date as QC_In_Date_Time,TSPL_PP_STD_QC_DETAIL.Item_Code,Remarks from TSPL_PP_STD_QC_DETAIL left outer join TSPL_PP_STANDARDIZATION_HEAD on TSPL_PP_STANDARDIZATION_HEAD.Standardization_Code=TSPL_PP_STD_QC_DETAIL.Standardization_Code where TSPL_PP_STD_QC_DETAIL.Item_Code='" + Item_Code + "'"
            qry += " union all "
            qry += "select distinct 'Stage Process' as Doc_Type,TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.STAGE_PROCESS_CODE as QC_No,TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_DATE as QC_In_Date_Time,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code,TSPL_PP_SP_ISSUE_ITEM_DETAIL.Remarks from TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET left outer join TSPL_PP_STAGE_PROCESS_HEAD on TSPL_PP_STAGE_PROCESS_HEAD.STAGE_PROCESS_CODE=TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.STAGE_PROCESS_CODE left outer join TSPL_PP_SP_ISSUE_ITEM_DETAIL on TSPL_PP_SP_ISSUE_ITEM_DETAIL.STAGE_PROCESS_CODE=TSPL_PP_STAGE_PROCESS_QC_LOG_SHEET.STAGE_PROCESS_CODE where TSPL_PP_SP_ISSUE_ITEM_DETAIL.Item_Code='" + Item_Code + "'"
            qry += ")ax)axa left outer join tspl_item_master on tspl_item_master.item_code=axa.item_code where axa.sno<=10 and axa.item_code='" + Item_Code + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            LoadGrid()
            gv.Rows.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv.Rows.AddNew()

                    gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = clsCommon.myCBool(dr("Select"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colDoctype).Value = clsCommon.myCstr(dr("Document Type"))
                    gv.Rows(gv.Rows.Count - 1).Cells(coldocno).Value = clsCommon.myCstr(dr("Document No"))
                    gv.Rows(gv.Rows.Count - 1).Cells(coldate).Value = clsCommon.myCstr(dr("Date"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colitemcode).Value = clsCommon.myCstr(dr("Item Code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(coldesc).Value = clsCommon.myCstr(dr("Description"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colremarks).Value = clsCommon.myCstr(dr("Remarks"))
                Next
            Else
                Throw New Exception("No record found.")
            End If
            isInsideLoaddata = False
        Catch ex As Exception
            isInsideLoaddata = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadGrid()
        gv.Columns.Clear()
        gv.Rows.Clear()

        Dim reposelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        reposelect.HeaderText = "Select"
        reposelect.Width = 60
        reposelect.IsVisible = False
        reposelect.Name = colSelect
        gv.MasterTemplate.Columns.Add(reposelect)

        Dim repodoctype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repodoctype.HeaderText = "Document Type"
        repodoctype.Width = 120
        repodoctype.ReadOnly = True
        repodoctype.Name = colDoctype
        repodoctype.FormatString = ""
        gv.MasterTemplate.Columns.Add(repodoctype)

        repodoctype = New GridViewTextBoxColumn()
        repodoctype.HeaderText = "Document No"
        repodoctype.Width = 100
        repodoctype.ReadOnly = True
        repodoctype.Name = coldocno
        repodoctype.FormatString = ""
        gv.MasterTemplate.Columns.Add(repodoctype)

        repodoctype = New GridViewTextBoxColumn()
        repodoctype.HeaderText = "Date"
        repodoctype.Width = 80
        repodoctype.ReadOnly = True
        repodoctype.Name = coldate
        repodoctype.FormatString = ""
        gv.MasterTemplate.Columns.Add(repodoctype)

        repodoctype = New GridViewTextBoxColumn()
        repodoctype.HeaderText = "Item Code"
        repodoctype.Width = 130
        repodoctype.ReadOnly = True
        repodoctype.Name = colitemcode
        repodoctype.FormatString = ""
        gv.MasterTemplate.Columns.Add(repodoctype)

        repodoctype = New GridViewTextBoxColumn()
        repodoctype.HeaderText = "Description"
        repodoctype.Width = 260
        repodoctype.ReadOnly = True
        repodoctype.Name = coldesc
        repodoctype.FormatString = ""
        gv.MasterTemplate.Columns.Add(repodoctype)

        repodoctype = New GridViewTextBoxColumn()
        repodoctype.HeaderText = "Remarks"
        repodoctype.Width = 100
        repodoctype.ReadOnly = True
        repodoctype.Name = colremarks
        repodoctype.FormatString = ""
        gv.MasterTemplate.Columns.Add(repodoctype)

        gv.AllowDeleteRow = False
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub btnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOk.Click

    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv.DoubleClick
        Try
            'If gv.DataSource IsNot Nothing AndAlso gv.Rows.Count > 0 Then

            Dim doc_type As String = clsCommon.myCstr(gv.CurrentRow.Cells(colDoctype).Value)

            If clsCommon.CompairString(doc_type, "MCC") = CompairStringResult.Equal Then
                Dim frm As New FrmQualityCheck()
                frm.SetUserMgmt(clsUserMgtCode.frmQualityCheck)
                frm.strDocCode = clsCommon.myCstr(gv.CurrentRow.Cells(coldocno).Value)
                frm.strDocType = ""
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
            ElseIf clsCommon.CompairString(doc_type, "Standardization") = CompairStringResult.Equal Then
                Dim frm As New frmProcessProductionStandardization()
                frm.SetUserMgmt(clsUserMgtCode.frmProcessProductionStandardization)
                frm.strDocumentCode = clsCommon.myCstr(gv.CurrentRow.Cells(coldocno).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
            ElseIf clsCommon.CompairString(doc_type, "Stage Process") = CompairStringResult.Equal Then
                Dim frm As New frmProcessProductionStageProcess()
                frm.SetUserMgmt(clsUserMgtCode.frmProcessProductionStageProcess)
                frm.strDocumentCode = clsCommon.myCstr(gv.CurrentRow.Cells(coldocno).Value)
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
            End If

            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
