Imports System.Data.SqlClient
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPHRandPayroll
Public Class rptListofCowDCS
    Private Sub rptListofCowDCS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Sub FormatGrid()
        If Gv1 Is Nothing Then
            Return
        End If

        Gv1.TableElement.TableHeaderHeight = 25
        Gv1.MasterTemplate.ShowRowHeaderColumn = True

        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = True
            'Gv1.Columns(ii).FormatString = "{0:n2}"
        Next

        Dim sNoColumn As GridViewDataColumn = Gv1.Columns("SNo")
        If sNoColumn IsNot Nothing Then
            sNoColumn.IsVisible = True
            sNoColumn.Width = 100
            sNoColumn.HeaderText = "SNo"
        End If

        Dim dcsCodeColumn As GridViewDataColumn = Gv1.Columns("DCS Code")
        If dcsCodeColumn IsNot Nothing Then
            dcsCodeColumn.IsVisible = True
            dcsCodeColumn.Width = 100
            dcsCodeColumn.HeaderText = "DCS Code"
        End If

        Dim dcsNameColumn As GridViewDataColumn = Gv1.Columns("DCS Name")
        If dcsNameColumn IsNot Nothing Then
            dcsNameColumn.IsVisible = True
            dcsNameColumn.Width = 100
            dcsNameColumn.HeaderText = "DCS Name"
        End If

        Dim applyCowPriceColumn As GridViewDataColumn = Gv1.Columns("Apply Cow Price")
        If applyCowPriceColumn IsNot Nothing Then
            applyCowPriceColumn.IsVisible = True
            applyCowPriceColumn.Width = 100
            applyCowPriceColumn.HeaderText = "Apply Cow Price"
        End If

        Dim startDateColumn As GridViewDateTimeColumn = Gv1.Columns("Start Date")
        If startDateColumn IsNot Nothing Then
            startDateColumn.IsVisible = True
            startDateColumn.Width = 100
            startDateColumn.HeaderText = "Start Date"
        End If

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
    End Sub
    Private Sub txtMultiMCC_My_Click(sender As Object, e As EventArgs) Handles txtMultiMCC._My_Click
        Dim qry As String = "select MCC_Code as Code  from TSPL_MCC_MASTER "
        txtMultiMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("MCC@VMPIFSC", qry, "Code", "", txtMultiMCC.arrValueMember, txtMultiMCC.arrDispalyMember)
        RefreshDCS()
    End Sub
    Sub RefreshDCS()
        If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
            Dim qry As String = "select MCC_Code from TSPL_MCC_MASTER where MCC_Code in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ")  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            txtMultiMCC.arrValueMember = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim arr As New ArrayList
                For Each dr As DataRow In dt.Rows
                    arr.Add(clsCommon.myCstr(dr("MCC_Code")))
                Next
                txtMultiMCC.arrValueMember = arr
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message.ToString(), Me.Text)
        End Try
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strHeading As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptListofCowDCS & "'"))

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Report Name : " + strHeading)

            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            If exporter = EnumExportTo.Excel Then

                transportSql.exportdata(Gv1, "", Me.Text, , arrHeader, False, True)
            Else
                clsCommon.MyExportToPDF(strHeading, Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub
    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = clsCommon.myCstr(MyBase.Form_ID)
        TemplateGridview = Gv1
        Print(False)
    End Sub
    Sub Print(ByVal isPrint As Boolean, Optional ByVal isPrerint As Boolean = False)
        Try
            Dim qry As String = ""
            Dim dt As New DataTable

            qry = "select row_number() over(order by(select 1)) as SNo,VLC_Code_VLC_Uploader as DCS  ,VLC_Name as [DCS Name],case when Apply_Cow_Price=1 then 'Y' else 'N' end [Apply Cow Price],cast (Convert(date,TSPL_VLC_MASTER_HEAD.ApplyCowPriceDate,103) as varchar) as [Apply Cow Price Date]from TSPL_VLC_MASTER_HEAD
                    LEFT JOIN TSPL_MCC_MASTER ON TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC "

            If txtMultiMCC.arrValueMember IsNot Nothing AndAlso txtMultiMCC.arrValueMember.Count > 0 Then
                qry += "  where TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtMultiMCC.arrValueMember) + ")"
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()
            FormatGrid()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                'FormatGrid()
                Gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found ", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class