'Added by Anand
'Date-07/March/2014
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Public Class FrmAssetDetailAttachments
    Public strCode As String = ""

    Private Sub FrmAssetDetailAttachments_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Load_Data()
    End Sub
    Private Sub Load_Data()
        Try
            Dim qry As String = " select FILENAME,'Download' as [Download]  from TSPL_Asset_Details where Asset_Code='" + strCode + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Attachment found", Me.Text)
                Me.Close()
                Exit Sub
            End If
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.AllowAddNewRow = False
            gv1.ShowGroupPanel = False
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Data Found to Display")
            End If
            gv1.DataSource = dt
            SetGridFormationOFGV1()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormationOFGV1()
        gv1.TableElement.TableHeaderHeight = 25
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 1 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
        Next
        'gv1.Columns("Select").Width = 50
        gv1.Columns("FileName").Width = 300
        gv1.Columns("Download").Width = 200
        gv1.Columns("Download").HeaderText = " "
        
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CellDoubleClick_1(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.myLen(strCode) > 0 Then
                If clsCommon.CompairString(e.Column.Name, "Download") = CompairStringResult.Equal Then
                    SaveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
                    SaveFileDialog1.FilterIndex = 2
                    SaveFileDialog1.RestoreDirectory = True
                    Dim strSql As String = " select  FILENAME ,ATTACHMENT  from TSPL_Asset_Details where Asset_Code='" + strCode + "'  "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strSql)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim FileName As String = clsCommon.myCstr(dt.Rows(0)("FILENAME"))
                        Dim data As Byte() = DirectCast(dt.Rows(0)("ATTACHMENT"), Byte())
                        SaveFileDialog1.FileName = "C:\ERPTempFolder\" + FileName
                        System.IO.File.WriteAllBytes(SaveFileDialog1.FileName.ToString(), data)
                        System.Diagnostics.Process.Start(SaveFileDialog1.FileName.ToString())
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
