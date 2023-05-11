Imports common
Imports System.Data.SqlClient

Public Class FrmBackup
    Inherits FrmMainTranScreen
    Private Sub FrmBackup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadCompany()
        LoadServerPath()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.mbtnTakeBackup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnBackup.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        ''btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub LoadServerPath()
        Dim qry As String = "Select * from TSPL_SERVER_PATH"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            txtServerName.Text = clsCommon.myCstr(dt.Rows(0)("SERVER_NAME"))
            txtServerPath.Text = clsCommon.myCstr(dt.Rows(0)("SERVER_PATH"))
            tbDestination.Text = clsCommon.myCstr(dt.Rows(0)("CLIENT_PATH"))
        End If
    End Sub

    Private Sub LoadCompany()
        Dim qry As String = "select Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(DataBase_Name)>0"
        cbgCmp.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCmp.ValueMember = "DataBase_Name"
        cbgCmp.DisplayMember = "Comp_Code"
    End Sub
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackup.Click
        Try
            If clsCommon.myLen(txtServerName.Text) <= 0 Then
                txtServerName.Focus()
                Throw New Exception("Please Enter Server Name")
            End If

            If clsCommon.myLen(txtServerPath.Text) <= 0 Then
                txtServerPath.Focus()
                Throw New Exception("Please Enter Server Path")
            End If

            'If clsCommon.myLen(tbDestination.Text) <= 0 Then
            '    bDestination.Focus()
            '    Throw New Exception("Please select Client Path")
            'End If

            clsCommon.ProgressBarShow()

            If TakeBakup() Then
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Backup taken Sucessfully")
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Function TakeBakup() As Boolean
        Dim arr As ArrayList = cbgCmp.CheckedValue
        If arr Is Nothing OrElse arr.Count <= 0 Then
            Throw New Exception("Plese select at least one Company")
            Return False
        End If

        If Not System.IO.Directory.Exists(tbDestination.Text) Then
            System.IO.Directory.CreateDirectory(tbDestination.Text)
        End If


        Dim qry As String = ""
        Dim strdt As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "yyyy_MM_dd_hh_mm_tt")
        For ii As Integer = 0 To arr.Count - 1
            Dim strFileName As String = "\\" + txtServerName.Text.Trim() + "\" + txtServerPath.Text.Trim() + "\" + clsCommon.myCstr(arr(ii)) + "_" + strdt + ".bak "
            '            Dim qry As String = "BACKUP DATABASE " + clsCommon.myCstr(arr(ii)) + " TO DISK = '" + tbDestination.Text + "\" + clsCommon.myCstr(arr(ii)) + "_" + strdt + ".bak'  "
            Try
                clsCommon.ProgressBarUpdate("Taking Backup...")
                qry = "BACKUP DATABASE " + clsCommon.myCstr(arr(ii)) + " TO DISK = '" + strFileName + "'  "
                clsDBFuncationality.ExecuteNonQuery(qry)
            Catch ex As Exception
                Throw New Exception("Invalid Server Path for BackUp")
                Return False
            End Try

            Dim copyFileName As String = strFileName.Replace(":\", "\")
            Try
                clsCommon.ProgressBarUpdate("Copy to Destination...")
                Dim strSrcFile As String = "\\" + txtServerName.Text.Trim() + "\" + txtServerPath.Text.Trim() + "\\" + copyFileName
                System.IO.File.Copy(copyFileName, tbDestination.Text + "\" + clsCommon.myCstr(arr(ii)) + "_" + strdt + ".bak ")
            Catch ex As Exception
                Throw New Exception("Invalid Server Name or Client Path")
                Return False
            End Try

            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Backup_Code", strFileName)
            clsCommon.AddColumnsForChange(coll, "Backup_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Backup", OMInsertOrUpdate.Insert, "", Nothing)

            'System.IO.File.Delete(copyFileName)
            
        Next
        qry = "delete from TSPL_SERVER_PATH"
        clsDBFuncationality.ExecuteNonQuery(qry)

        Dim coll1 As New Hashtable()
        clsCommon.AddColumnsForChange(coll1, "Server_Name", txtServerName.Text.Trim())
        clsCommon.AddColumnsForChange(coll1, "Server_Path", txtServerPath.Text.Trim())
        clsCommon.AddColumnsForChange(coll1, "Client_Path", tbDestination.Text.Trim())

        Dim result As Boolean = clsCommonFunctionality.UpdateDataTable(coll1, "TSPL_SERVER_PATH", OMInsertOrUpdate.Insert, "")

        
        Return True
    End Function

    Private Sub bDestination_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bDestination.Click
        Try
            If FolderBrowserDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                tbDestination.Text = FolderBrowserDialog1.SelectedPath
            End If
        Catch ex As Exception
            RadMessageBox.Show("Error: " + ex.Message)
        End Try
    End Sub

    Private Sub btnServerPath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If FolderBrowserDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                txtServerPath.Text = FolderBrowserDialog1.SelectedPath
            End If
        Catch ex As Exception
            RadMessageBox.Show("Error: " + ex.Message)
        End Try
    End Sub


End Class

