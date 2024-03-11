Imports common
Imports System.IO
Imports Telerik.WinControls.UI
Imports System.Data.SqlClient

Public Class frmMilkCollectionLevelsMain
    Inherits FrmMainTranScreen

    Private Const ReportID As String = "MilkColLevelsMain"
    Private Sub FrmBugMasterMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'SetUserMgmtNew()
        LoadData(False)
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadData(True)
    End Sub


    Sub LoadData(ByVal isShowMsg As Boolean)
        Try
            'Dim qry As String = ""
            'Dim dt1 As DataTable = clsMilkCollectionLevels.GetDesignationLevel()
            'For i As Integer = 0 To dt1.Rows.Count - 1
            '    qry += dt1.Rows(i)(0) + " as " + dt1.Rows(i)(0) + ""
            '    If i <> dt1.Rows.Count - 1 Then
            '        qry += ","
            '    End If
            'Next
            ''qry = "Level1_Desg as Level1,Level2_Desg as Level2,Level3_Desg as Level3,Level4_Desg as Level4"
            Dim qry1 As String
            qry1 = "select TSPL_MilkCollectionLevels.LEVEL_CODE,TSPL_MilkCollectionLevels.DESCRIPTION,TSPL_MilkCollectionLevels.Parent_LEVEL_CODE ,TSPL_MilkCollectionLevels_Parent.Description as Parent_Level_Desc "
            qry1 += " from TSPL_MilkCollectionLevels"
            qry1 += " left outer join TSPL_MilkCollectionLevels as TSPL_MilkCollectionLevels_Parent on TSPL_MilkCollectionLevels.Parent_LEVEL_CODE=TSPL_MilkCollectionLevels_Parent.LEVEL_CODE where TSPL_MilkCollectionLevels.comp_code='" & objCommonVar.CurrentCompanyCode & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
            If (dt Is Nothing OrElse dt.Rows.Count <= 0) Then
                If isShowMsg Then
                    clsCommon.MyMessageBoxShow(Me, "No Data found to Display", Me.Text)
                End If

            End If

            gv1.DataSource = dt

            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).Width = 100
            Next
            gv1.Columns("LEVEL_CODE").HeaderText = "Collection Level Code"
            gv1.Columns("DESCRIPTION").HeaderText = "Collection Level Description"
            gv1.Columns("Parent_LEVEL_CODE").HeaderText = "Parent Level Code"
            gv1.Columns("Parent_Level_Desc").HeaderText = "Parent Level Description"


            ReStoreGridLayout()
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            gv1.ShowFilteringRow = True
            gv1.AllowDeleteRow = False
            gv1.EnableAlternatingRowColor = True
            gv1.MasterView.TableFilteringRow.IsCurrent = True
            gv1.Columns(0).IsCurrent = True
            gv1.Focus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
            MessageBox.Show(Me, err.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim frm As New frmMilkCollectionLevels()
        frm.Code = clsCommon.myCstr(gv1.CurrentRow.Cells("LEVEL_CODE").Value)
        frm.Show()
        LoadData(False)
    End Sub

    Private Sub mbtnSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnSaveLayout.Click
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
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mbtnDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnDeleteLayout.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            clsCommon.MyMessageBoxShow(Me, "Layout deleted Successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        Dim frm As New frmMilkCollectionLevels()
        frm.CreateNewTransaction = True
        frm.Show()
        'LoadData(False)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub


    Private Sub mbtnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mbtnExport.Click
        Try
            Dim qry As String = "select TSPL_MilkCollectionLevels.LEVEL_CODE as [Level Code],TSPL_MilkCollectionLevels.Description ,TSPL_MilkCollectionLevels.Parent_LEVEL_CODE as [Parent Level Code] from TSPL_MilkCollectionLevels "
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Milk Collection Levels ")
        End Try
    End Sub

    Private Sub mbtnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim gv As New RadGridView()
        Dim rowNo As Integer = 1
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        Dim qry As String = ""
        Try
            If transportSql.importExcel(gv, "Level Code", "Description", "Parent Level Code") Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    clsCommon.ProgressBarShow()
                    Dim arrVisi As New List(Of String)
                    For Each grow As GridViewRowInfo In gv.Rows
                        Try
                            Dim obj As New clsMilkCollectionLevels()
                            obj.LEVEL_CODE = clsCommon.myCstr(grow.Cells("Level Code").Value)
                            obj.DESCRIPTION = clsCommon.myCstr(grow.Cells("DESCRIPTION").Value)
                            obj.PARENT_LEVEL_CODE = clsCommon.myCstr(grow.Cells("Parent Level Code").Value)

                            If clsCommon.myLen(obj.LEVEL_CODE) <= 0 Then
                                Throw New Exception("Not a valid Level Code: " + obj.LEVEL_CODE)
                            End If
                            If clsCommon.myLen(obj.DESCRIPTION) <= 0 Then
                                Throw New Exception("Not a valid Level Description: " + obj.DESCRIPTION)
                            End If
                            Dim isNewEntry As Boolean = False

                            qry = "select 1 from TSPL_MilkCollectionLevels where LEVEL_CODE='" + obj.LEVEL_CODE + "'	"
                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                                isNewEntry = True
                            Else
                                isNewEntry = False
                            End If

                            clsMilkCollectionLevels.SaveData(obj, isNewEntry, trans)
                            rowNo += 1

                        Catch ex As Exception
                            Throw New Exception("Error at Row no " + clsCommon.myCstr(rowNo) + Environment.NewLine + ex.Message)
                        End Try
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    LoadData(False)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub btnRefresh_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData(False)
    End Sub

    'Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click

    'End Sub
End Class
