''created by Monika'15/10/2015 [BM00000008148]
Imports common
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmApprovalAlertSumm
    Inherits FrmMainTranScreen


#Region "variables"
    Dim ButtonToolTip As New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChanged As Boolean = False

    Const colSno As String = "Sno"
    Const colDetailbutton As String = "detailbutn"
    Const colScreenCode As String = "ScreenCode"
    Const colScreenName As String = "ScreenName"
    Const colSubject As String = "Subject"
    Const colDate As String = "Date"
    Const colAppCount As String = "AppCount"
    Const colPendingCount As String = "PendingCount"
    Const colRejCount As String = "RejCount"

    Const colDSno As String = "DSno"
    Const colDDetailbutton As String = "Ddetailbutn"
    Const colDScreenCode As String = "DScreenCode"
    Const colDScreenName As String = "DScreenName"
    Const colDDocNo As String = "DocNo"
    Const colDDocDate As String = "DocDate"
    Const colDDesc As String = "Description"
    Const colDFromUser As String = "FromUser"
    Const colDPostStatus As String = "PostStatus"
    Const colReadStatus As String = "ReadStatus"
#End Region

    Private Sub LoadType()
        isInsideLoadData = True

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow

        dr = dt.NewRow()
        dr("Code") = "Pending"
        dr("Name") = "Pending"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Approved"
        dr("Name") = "Approved"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Rejected"
        dr("Name") = "Rejected"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.DisplayMember = "Name"
        cboType.ValueMember = "Code"

        cboType.SelectedValue = "Pending"
        isInsideLoadData = False
    End Sub

    Private Function SetUserMgmtNew() As Boolean
        'MyBase.SetUserMgmt(clsUserMgtCode.frmApprovalAlertSumm)
        If Not (MyBase.isReadFlag) Then
            Return False
            'clsCommon.MyMessageBoxShow("Permission Denied")
        End If

        btnExport.Visible = MyBase.isExport
        btnExport_Doc.Visible = MyBase.isExport
        Return True
    End Function

    Private Sub FrmApprovalAlertSumm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.B AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                btnBack.PerformClick()
            ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso RadPageViewPage1.Item.Visibility = ElementVisibility.Visible Then
                btnRefresh.PerformClick()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmApprovalAlertSumm_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadType()
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, Nothing) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            Throw New Exception("Unauthorized access of Work flow of Approval.")
            Me.Close()
        End If


        If Not SetUserMgmtNew() Then
            Me.Close()
        Else
            LoadBlankGv1()
            LoadBlankGv1_Doc()
            FunReset()

            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
            ButtonToolTip.SetToolTip(btnCloseAlert, "Press Alt+C for close window.")
            ButtonToolTip.SetToolTip(btnBack, "Press Alt+B for alert window.")
            ButtonToolTip.SetToolTip(btnRefresh, "Press Alt+N for refresh window.")

            RadPageViewPage1.Text = ""
            RadPageViewPage2.Text = ""

            RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
            RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed

            btnPDF.Visibility = ElementVisibility.Collapsed
            btnPDF_Doc.Visibility = ElementVisibility.Collapsed

            Load_Alert_Data()
            RadPageView1.SelectedPage = RadPageViewPage1

        End If

        btnRefresh.Text = "Refresh"
    End Sub

    Private Sub FunReset()
        gv_Alert.Rows.Clear()
        gv_Doc.Rows.Clear()

        ReStoreGridLayout()

        gv_Alert.Focus()
        gv_Alert.Select()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub LoadBlankGv1()
        gv_Alert.Rows.Clear()
        gv_Alert.Columns.Clear()

        Dim repoTextbox As New GridViewTextBoxColumn
        Dim repodate As New GridViewDateTimeColumn()
        Dim repoimage As New GridViewImageColumn()

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 60
        repoTextbox.HeaderText = "S.No."
        repoTextbox.Name = colSno
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 100
        repoTextbox.HeaderText = "Screen Code"
        repoTextbox.Name = colScreenCode
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = False
        repoTextbox.WrapText = True
        repoTextbox.VisibleInColumnChooser = False
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        repoimage = New GridViewImageColumn()
        repoimage.FormatString = ""
        repoimage.Width = 16
        repoimage.HeaderText = ""
        repoimage.Name = colDetailbutton
        repoimage.ReadOnly = True
        repoimage.ImageLayout = ImageLayout.Zoom
        repoimage.IsVisible = True
        repoimage.WrapText = True
        gv_Alert.MasterTemplate.Columns.Add(repoimage)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 200
        repoTextbox.HeaderText = "Screen Name"
        repoTextbox.Name = colScreenName
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 250
        repoTextbox.HeaderText = "Subject"
        repoTextbox.Name = colSubject
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        repodate = New GridViewDateTimeColumn()
        repodate.FormatString = "{0:d}"
        repodate.CustomFormat = "dd/MM/yyyy"
        repodate.Format = DateTimePickerFormat.Custom
        repodate.HeaderText = "Date"
        repodate.Name = colDate
        repodate.ReadOnly = True
        repodate.Width = 80
        gv_Alert.MasterTemplate.Columns.Add(repodate)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 90
        repoTextbox.HeaderText = "Pending for App."
        repoTextbox.Name = colPendingCount
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 90
        repoTextbox.HeaderText = "Approved Pt."
        repoTextbox.Name = colAppCount
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 90
        repoTextbox.HeaderText = "Rejected Pt."
        repoTextbox.Name = colRejCount
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Alert.MasterTemplate.Columns.Add(repoTextbox)

        gv_Alert.AllowDeleteRow = False
        gv_Alert.AllowAddNewRow = False
        gv_Alert.ShowGroupPanel = False
        gv_Alert.AllowColumnReorder = True
        gv_Alert.AllowRowReorder = False
        gv_Alert.EnableSorting = False
        gv_Alert.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_Alert.ShowFilteringRow = True
        gv_Alert.EnableFiltering = True
        gv_Alert.MasterTemplate.ShowRowHeaderColumn = False

        repoTextbox = Nothing
        repodate = Nothing


        ReStoreGridLayout()
    End Sub

    Private Sub LoadBlankGv1_Doc()
        gv_Doc.Rows.Clear()
        gv_Doc.Columns.Clear()

        Dim repoTextbox As New GridViewTextBoxColumn
        Dim repodate As New GridViewDateTimeColumn()
        Dim repoimage As New GridViewImageColumn()

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 60
        repoTextbox.HeaderText = "S.No."
        repoTextbox.Name = colDSno
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 100
        repoTextbox.HeaderText = "Screen Code"
        repoTextbox.Name = colDScreenCode
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = False
        repoTextbox.WrapText = True
        repoTextbox.VisibleInColumnChooser = False
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 200
        repoTextbox.HeaderText = "Screen Name"
        repoTextbox.Name = colDScreenName
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repoimage = New GridViewImageColumn()
        repoimage.FormatString = ""
        repoimage.Width = 16
        repoimage.HeaderText = ""
        repoimage.Name = colDDetailbutton
        repoimage.ReadOnly = True
        repoimage.IsVisible = True
        repoimage.WrapText = True
        repoimage.ImageLayout = ImageLayout.Zoom
        gv_Doc.MasterTemplate.Columns.Add(repoimage)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 120
        repoTextbox.HeaderText = "Document Code"
        repoTextbox.Name = colDDocNo
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repodate = New GridViewDateTimeColumn()
        repodate.FormatString = "{0:d}"
        repodate.CustomFormat = "dd/MM/yyyy"
        repodate.Format = DateTimePickerFormat.Custom
        repodate.HeaderText = "Date"
        repodate.Name = colDDocDate
        repodate.ReadOnly = True
        repodate.Width = 80
        gv_Doc.MasterTemplate.Columns.Add(repodate)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 250
        repoTextbox.HeaderText = "Subject"
        repoTextbox.Name = colDDesc
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 100
        repoTextbox.HeaderText = "From User"
        repoTextbox.Name = colDFromUser
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repoTextbox = New GridViewTextBoxColumn()
        repoTextbox.FormatString = ""
        repoTextbox.Width = 100
        repoTextbox.HeaderText = "Post Status"
        repoTextbox.Name = colDPostStatus
        repoTextbox.ReadOnly = True
        repoTextbox.IsVisible = True
        repoTextbox.WrapText = True
        gv_Doc.MasterTemplate.Columns.Add(repoTextbox)

        repoimage = New GridViewImageColumn()
        repoimage.FormatString = ""
        repoimage.Width = 80
        repoimage.HeaderText = "Status"
        repoimage.Name = colReadStatus
        repoimage.HeaderImage = Global.ERP.My.Resources.Resources._new
        repoimage.TextImageRelation = TextImageRelation.ImageBeforeText
        repoimage.ReadOnly = True
        repoimage.IsVisible = True
        repoimage.WrapText = True
        'repoimage.ImageLayout = ImageLayout.Zoom
        gv_Doc.MasterTemplate.Columns.Add(repoimage)

        gv_Doc.AllowDeleteRow = False
        gv_Doc.AllowAddNewRow = False
        gv_Doc.ShowGroupPanel = False
        gv_Doc.AllowColumnReorder = True
        gv_Doc.AllowRowReorder = False
        gv_Doc.EnableSorting = False
        gv_Doc.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_Doc.ShowFilteringRow = True
        gv_Doc.EnableFiltering = True
        gv_Doc.MasterTemplate.ShowRowHeaderColumn = False

        repoTextbox = Nothing
        repodate = Nothing

        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage1.Item.Visibility = ElementVisibility.Visible Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv_Alert.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv_Alert.Columns.Count - 1 Step ii + 1
                        gv_Alert.Columns(ii).IsVisible = False
                        gv_Alert.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv_Alert.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If

            If clsCommon.myLen(MyBase.Form_ID + "DOC") > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID + "DOC", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv_Doc.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv_Doc.Columns.Count - 1 Step ii + 1
                        gv_Doc.Columns(ii).IsVisible = False
                        gv_Doc.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv_Doc.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnCloseAlert_Click(sender As Object, e As EventArgs) Handles btnCloseAlert.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click ''save layout
        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage1.Item.Visibility = ElementVisibility.Visible Then
            gv_Alert.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv_Alert.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv_Alert.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If

        If clsCommon.myLen(MyBase.Form_ID + "DOC") > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            gv_Doc.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID + "DOC"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv_Doc.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv_Doc.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click ''delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage1.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
        If clsGridLayout.DeleteData(MyBase.Form_ID + "DOC", objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed

        isInsideLoadData = True
        cboType.SelectedValue = "Pending"
        isInsideLoadData = False

        Load_Alert_Data()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExcel_Doc_Click(sender As Object, e As EventArgs) Handles btnExcel_Doc.Click
        Try
            If gv_Doc.Rows.Count >= 0 Then
                clsCommon.MyExportToExcelGrid("Document for Approval", gv_Doc, Nothing, "Document for Approval")
            Else
                Throw New Exception("no record found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPDF_Doc_Click(sender As Object, e As EventArgs) Handles btnPDF_Doc.Click
        Try
            If gv_Doc.Rows.Count >= 0 Then
                clsCommon.MyExportToPDF("Document for Approval", gv_Doc, Nothing, "Document for Approval")
            Else
                Throw New Exception("no record found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv_Alert.Rows.Count >= 0 Then
                clsCommon.MyExportToExcelGrid("Messages/Alerts Overview", gv_Alert, Nothing, "Messages/Alerts")
            Else
                Throw New Exception("no record found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            If gv_Alert.Rows.Count >= 0 Then
                clsCommon.MyExportToPDF("Messages/Alerts Overview", gv_Alert, Nothing, "Messages/Alerts")
            Else
                Throw New Exception("no record found.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Load_Alert_Data()
    End Sub

    Private Sub Load_Alert_Data()
        Dim dt As New DataTable()
        Try
            'GKD/21/05/18-000134 by balwidner on 25/05/2018
            Dim qry As String = "select ROW_NUMBER() over(order by fin.TRANS_Code) as sno,fin.TRANS_Code,fin.screenname,fin.Subject,max(fin.date) as [date],sum(pending) as pending,sum(app) as app,sum(rej) as rej from ( " & _
                                "select TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name as screenname,'Request for Document Approval.' as Subject,max(document_date) as [date],0 as pending,count(*) as App,0 as Rej from tspl_approval_level_transaction_detail left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code where user_code='" + objCommonVar.CurrentUserCode + "' and (status='Approved' or (status='Approved' and Is_Posted=1)) and is_reverse=0 group by TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name " & _
                                " union all " & _
                                "select TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name as screenname,'Request for Document Approval.' as Subject,max(document_date) as [date],0 as pending,0 as App,count(*) as Rej from tspl_approval_level_transaction_detail left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code where user_code='" + objCommonVar.CurrentUserCode + "' and (status='Rejected' or (status='Rejected' and Is_Posted=1)) and is_reverse=0 group by TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name " & _
                                " union all " & _
                                "select TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name as screenname,'Request for Document Approval.' as Subject,max(document_date) as [date],count(*) as pending,0 as App,0 as Rej from tspl_approval_level_transaction_detail left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL.TRANS_Code where user_code='" + objCommonVar.CurrentUserCode + "' and ((isnull(status,'')='' or isnull(status,'')='Pending') and isnull(Is_Posted,'')=0) and is_reverse=0 " + Environment.NewLine + _
                                " and 2=(case when isnull(All_Level_Approval,0)=0 or tspl_approval_level_transaction_detail.No_Of_Level=1 then 2 else (case when exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level-1  and inn.Status='Approved' ) then 2 else 3 end ) end)" + Environment.NewLine + _
                                "  and   not exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level and inn.Status<>'') " + Environment.NewLine + _
                                " group by TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name " & _
                                " )Fin group by fin.TRANS_Code,fin.screenname,fin.Subject"
            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            gv_Alert.Rows.Clear()

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv_Alert.Rows.AddNew()
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(dr("sno"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colDetailbutton).Value = My.Resources.Resources.Detail
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colScreenCode).Value = clsCommon.myCstr(dr("TRANS_Code"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colScreenName).Value = clsCommon.myCstr(dr("screenname"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colSubject).Value = clsCommon.myCstr(dr("Subject"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colDate).Value = clsCommon.myCstr(dr("date"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colPendingCount).Value = clsCommon.myCstr(dr("pending"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colAppCount).Value = clsCommon.myCstr(dr("app"))
                    gv_Alert.Rows(gv_Alert.Rows.Count - 1).Cells(colRejCount).Value = clsCommon.myCstr(dr("rej"))
                Next
            Else
                gv_Alert.Rows.Clear()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadDocData(ByVal scrnCode As String)
        Dim dt As New DataTable
        Try
            Dim qry As String = "select row_number() over(order by trans_code,document_code) as sno,TRANS_Code,TSPL_PROGRAM_MASTER.Program_Name,Document_Code,Document_Date,case when len(isnull(description,''))>0 and len(isnull(Remarks,''))>0 then Description+char(10)+'Remark: '+Remarks when len(isnull(description,''))>0 and len(isnull(Remarks,''))<=0 then Description when len(isnull(description,''))<=0 and len(isnull(Remarks,''))>0 then Remarks else TSPL_PROGRAM_MASTER.Program_Name+' based on doc no. '+document_code+ ' is pending for approval.' end as subject,tspl_approval_level_transaction_detail.Created_By,Status,Is_Read,case when Is_Posted=0 then 'Unposted' else 'Posted' end as 'PostStatus' " & _
                                        "from tspl_approval_level_transaction_detail left outer join TSPL_PROGRAM_MASTER on TSPL_PROGRAM_MASTER.Program_Code=tspl_approval_level_transaction_detail.TRANS_Code " & _
                                        " where user_code='" + objCommonVar.CurrentUserCode + "' and trans_code='" + scrnCode + "' and is_reverse=0 " 'and Is_Posted=0 and (isnull(status,'')='' or isnull(status,'')='Pending')
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Pending") = CompairStringResult.Equal Then
                qry += " and (isnull(status,'')='' or isnull(status,'')='Pending') " + Environment.NewLine +
                    " and 2=(case when isnull(All_Level_Approval,0)=0 or tspl_approval_level_transaction_detail.No_Of_Level=1 then 2 else (case when exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level-1  and inn.Status='Approved' ) then 2 else 3 end ) end)" + Environment.NewLine +
                    " and not exists(select 1 from tspl_approval_level_transaction_detail as inn where inn.TRANS_Code=tspl_approval_level_transaction_detail.TRANS_Code and inn.Document_Code=tspl_approval_level_transaction_detail.Document_Code and inn.Comp_Code=tspl_approval_level_transaction_detail.Comp_Code and inn.No_Of_Level=tspl_approval_level_transaction_detail.No_Of_Level and inn.Status<>'' and inn.Status<>'Amend') "
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Approved") = CompairStringResult.Equal Then
                qry += " and (isnull(status,'')='Approved')"
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Rejected") = CompairStringResult.Equal Then
                qry += " and (isnull(status,'')='Rejected')"
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            gv_Doc.Rows.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv_Doc.Rows.AddNew()
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDSno).Value = clsCommon.myCstr(dr("sno"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDDetailbutton).Value = Global.ERP.My.Resources.Resources.Detail
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDScreenCode).Value = clsCommon.myCstr(dr("TRANS_Code"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDScreenName).Value = clsCommon.myCstr(dr("Program_Name"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDDocNo).Value = clsCommon.myCstr(dr("Document_Code"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDDocDate).Value = clsCommon.myCstr(dr("Document_Date"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDDesc).Value = clsCommon.myCstr(dr("subject"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDPostStatus).Value = clsCommon.myCstr(dr("PostStatus"))
                    gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colDFromUser).Value = clsCommon.myCstr(dr("Created_By"))
                    If clsCommon.myCdbl(clsCommon.myCstr(dr("Is_Read"))) <= 0 Then ''pending pt seen
                        gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colReadStatus).Value = My.Resources.CloseBox
                    Else 'unseen
                        gv_Doc.Rows(gv_Doc.Rows.Count - 1).Cells(colReadStatus).Value = My.Resources.OpenBox
                    End If
                Next
                gv_Doc.Focus()
                gv_Doc.Select()
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                clsCommon.MyMessageBoxShow("No " & cboType.SelectedValue & " record found")
                RadPageViewPage1.Item.Visibility = ElementVisibility.Collapsed
                RadPageViewPage2.Item.Visibility = ElementVisibility.Visible
                RadPageView1.SelectedPage = RadPageViewPage2
                'cboType.SelectedValue = "Pending"
            End If ''end dt
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            dt = Nothing
        End Try
    End Sub

    Private Sub gv_Alert_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gv_Alert.CellClick
        Dim dt As New DataTable
        Try
            If e.Column Is gv_Alert.Columns(colDetailbutton) Then
                RadPageViewPage1.Item.Visibility = ElementVisibility.Visible
                RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
                RadPageView1.SelectedPage = RadPageViewPage1

                Dim scrnCode As String = clsCommon.myCstr(gv_Alert.CurrentRow.Cells(colScreenCode).Value)

                If clsCommon.myLen(scrnCode) > 0 Then
                    LoadDocData(scrnCode)
                End If ''end scrn cond
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv_Doc_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gv_Doc.CellClick
        Try
            If gv_Doc.Rows.Count >= 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible AndAlso e.Column Is gv_Doc.Columns(colDDetailbutton) Then
                Dim screenCode As String = clsCommon.myCstr(gv_Doc.CurrentRow.Cells(colDScreenCode).Value)
                Dim doc_code As String = clsCommon.myCstr(gv_Doc.CurrentRow.Cells(colDDocNo).Value)

                Dim frm As New FrmApprovalAlert_Child()
                frm.ScreenCode = screenCode
                frm.DocumentCode = doc_code
                frm.WindowState = FormWindowState.Normal
                frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
                'frm.StartPosition = FormStartPosition.CenterParent
                'frm.BringToFront()
                'Me.SendToBack()
                frm.ShowDialog()

                Dim qry As String = "update TSPL_APPROVAL_LEVEL_TRANSACTION_DETAIL set Is_Read='1' where trans_code='" + screenCode + "' and document_code='" + doc_code + "' and user_code='" + objCommonVar.CurrentUserCode + "' and is_reverse=0"
                If clsDBFuncationality.ExecuteNonQuery(qry) Then
                    gv_Doc.CurrentRow.Cells(colReadStatus).Value = My.Resources.OpenBox
                Else
                    gv_Doc.CurrentRow.Cells(colReadStatus).Value = My.Resources.CloseBox
                End If

                LoadDocData(screenCode)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    
    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        If Not isInsideLoadData Then
            Dim screenCode As String = clsCommon.myCstr(gv_Alert.CurrentRow.Cells(colScreenCode).Value)
            LoadDocData(screenCode)
        End If
    End Sub
End Class
