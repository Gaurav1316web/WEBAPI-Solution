Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
'Created By preeti Gupta[15/10/2019][]
Public Class CardSale
    Inherits FrmMainTranScreen
    Dim dt As DataTable
    Dim qry As String
    Dim CurrentDate As DateTime = clsCommon.GETSERVERDATE()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = True

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        If btnSave.Visible = True Then
            rmImport.Enabled = True
            rmExport.Enabled = True
        Else
            rmImport.Enabled = False
            rmExport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData(txtCode.Value)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Reset()
    End Sub
    Private Sub CardSale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Dim coll As Dictionary(Of String, String)
            'coll = New Dictionary(Of String, String)()
            'coll.Add("isFirstSpell", "integer not null default 0")
            'coll.Add("isSecondSpell", "integer not null default 0")
            'clsCommonFunctionality.CreateOrAlterTable("Tspl_Card_Sale", coll)

            SetUserMgmtNew()
            Reset()
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
            ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
            ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
            ButtonToolTip.SetToolTip(btnnew, "Press Alt+N For New")
            ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub
    Private Sub Reset()
        BlankAllControl()
        btnSave.Text = "Save"
        lblPending.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        chkFirstSpell.Checked = False
        chkSecondSpell.Checked = False
        chkFirstSpell.Enabled = True
        chkSecondSpell.Enabled = True
        
    End Sub
    Sub BlankAllControl()
        txtCode.Value = ""
        txtCardate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtFromDate.Value.AddMonths(1)
        ''richa  VIJ/22/06/21-001216
        txtToDate.Value = txtToDate.Value.AddDays(-1)
    End Sub
    Sub SaveData(ByVal isPost As Boolean)
        Try
            If (AllowToSave()) Then
                Dim obj As New ClsCardSale()
                obj.Card_No = txtCode.Value
                obj.Card_Date = txtCardate.Value
                obj.FROM_DATE = clsCommon.myCDate(txtFromDate.Value)
                obj.TO_DATE = clsCommon.myCDate(txtToDate.Value)
                obj.No_Of_Days = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT DATEDIFF(day, convert(date,'" & txtFromDate.Value & "',103), convert(date,'" & txtToDate.Value & "',103)) AS DateDiff"))
                obj.Free_Days = 1
                obj.isFirstSpell = chkFirstSpell.Checked
                obj.isSecondSpell = chkSecondSpell.Checked
                If obj.SaveData(obj, isNewEntry) Then
                    If isPost = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                        LoadData(obj.Card_No, NavigatorType.Current)
                        Exit Sub
                    End If
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function AllowToSave() As Boolean
        If chkFirstSpell.Checked = False AndAlso chkSecondSpell.Checked = False Then
            clsCommon.MyMessageBoxShow(Me, "Please Select 1st Spell Or 2nd Spell.", Me.Text)
            Return False
        End If
        Return True
    End Function
    Sub LoadData(ByVal StrCardNo As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            Dim obj As New ClsCardSale
            obj = ClsCardSale.GetData(StrCardNo, NavType)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Card_No) > 0 Then
                isNewEntry = False
                btnSave.Text = "Update"
                txtCode.Value = obj.Card_No
                txtFromDate.Value = obj.FROM_DATE
                txtToDate.Value = obj.TO_DATE
                txtCardate.Value = obj.Card_Date
                lblPending.Status = obj.Status
                chkFirstSpell.Checked = obj.isFirstSpell
                chkSecondSpell.Checked = obj.isSecondSpell
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
                chkFirstSpell.Enabled = False
                chkSecondSpell.Enabled = False
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DeleteData(ByVal strCardNo As String)
        If clsCommon.myLen(strCardNo) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Card No found to delete.", Me.Text)
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If clsCommon.myLen(strCardNo) > 0 Then
                If ClsCardSale.fundelete(strCardNo, trans) Then
                    trans.Commit()
                    clsCommon.MyMessageBoxShow(Me, "Data deleted successfully.", Me.Text)
                    Reset()
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Card No. found to delete.", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub CardSale_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData(txtCode.Value)
        End If
    End Sub
    Sub postData()
        Try
            If (myMessages.postConfirm()) Then
                SaveData(True)
                ClsCardSale.postData(txtCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            qry = "select count(*) from tspl_Card_Sale where Card_No='" + txtCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = ClsCardSale.getFinder("", txtCode.Value, isButtonClicked)
        LoadData(txtCode.Value, NavigatorType.Current)
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDate.ValueChanged
        txtToDate.Value = txtFromDate.Value.AddMonths(1)
        ''richa  VIJ/22/06/21-001216
        txtToDate.Value = txtToDate.Value.AddDays(-1)
    End Sub

    
    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_card_sale"))
        If count = 0 Then
            str = "select '' as [Card No] ,'' as [Card Date] ,'' as [From Date],'' as [To Date], '' as [isFirstSpell], '' as [isSecondSpell] from tspl_card_sale"
        Else
            str = " select Card_No as [Card No] ,Card_Date as [Card Date] ,FROM_DATE as [From Date],TO_DATE as [To Date], isFirstSpell  , isSecondSpell  from tspl_card_sale"
        End If

        ListImpExpColumnsMandatory = New List(Of String)({"Card Date", "From Date", "To Date"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory)
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Try
            Dim currentdate As Date = Date.Today
            Dim linno As Integer = 0

            Dim Strs As List(Of String) = New List(Of String)
            Strs.Add("Card Date")
            Strs.Add("From Date")
            Strs.Add("To Date")
            
            
            
            If transportSql.importExcel(gv, Strs.ToArray()) Then
                Dim trans As SqlTransaction = Nothing
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim obj As New ClsCardSale()
                        linno += 1
                       obj.Card_Date = clsCommon.GetPrintDate(grow.Cells("Card Date").Value, "dd/MMM/yyyy  hh:mm:ss tt ")
                        obj.FROM_DATE = clsCommon.GetPrintDate(grow.Cells("From Date").Value, "dd/MMM/yyyy  hh:mm:ss tt ")
                        obj.TO_DATE = clsCommon.GetPrintDate(grow.Cells("To Date").Value, "dd/MMM/yyyy  hh:mm:ss tt ")
                        
                        If clsCommon.myLen(obj.Card_Date) <= 0 Then
                            Throw New Exception("Card Date can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                            If clsCommon.myLen(obj.FROM_DATE) <= 0 Then
                                Throw New Exception("From Date can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If clsCommon.myLen(obj.TO_DATE) <= 0 Then
                                Throw New Exception("To Date can not be blank at line no. " + clsCommon.myCstr(linno) + ".")
                            End If
                            If obj.FROM_DATE > obj.TO_DATE Then
                                Throw New Exception("From Date can not be greater then To Date at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Free_Days = 1
                        Dim firstSpell As String = clsCommon.myCstr(grow.Cells("isFirstSpell").Value)
                        If Not (clsCommon.CompairString(firstSpell, "1") = CompairStringResult.Equal Or clsCommon.CompairString(firstSpell, "0") = CompairStringResult.Equal Or clsCommon.CompairString(firstSpell, "") = CompairStringResult.Equal) Then
                            Throw New Exception("isFirstSpell can not be other than ('1' or '0', Blank ) at line '" + clsCommon.myCstr(linno) + "'")
                        End If
                        Dim secondSpell As String = clsCommon.myCstr(grow.Cells("isSecondSpell").Value)
                        If Not (clsCommon.CompairString(secondSpell, "1") = CompairStringResult.Equal Or clsCommon.CompairString(secondSpell, "0") = CompairStringResult.Equal Or clsCommon.CompairString(secondSpell, "") = CompairStringResult.Equal) Then
                            Throw New Exception("isSecondSpell can not be other than ('1' or '0', Blank ) at line '" + clsCommon.myCstr(linno) + "'")
                        End If
                        If clsCommon.CompairString(firstSpell, "1") = CompairStringResult.Equal Then
                            obj.isFirstSpell = True
                        Else
                            obj.isFirstSpell = False
                        End If

                        If clsCommon.CompairString(secondSpell, "1") = CompairStringResult.Equal Then
                            obj.isSecondSpell = True
                        Else
                            obj.isSecondSpell = False
                        End If
            If obj.isFirstSpell = True AndAlso obj.isSecondSpell = True Then
                Throw New Exception("Only one Spell Allow at line no. " + clsCommon.myCstr(linno) + ".")
            End If
                        If obj.isFirstSpell = False AndAlso obj.isSecondSpell = False Then
                            Throw New Exception("Select One Spell at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
            obj.No_Of_Days = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT DATEDIFF(day, convert(date,'" & obj.FROM_DATE & "',103), convert(date,'" & obj.TO_DATE & "',103)) AS DateDiff", trans))
            Dim isSaved As Boolean = False
            isSaved = obj.SaveData(obj, True, trans)
            If isSaved = False Then
                clsCommon.ProgressBarHide()
                Throw New Exception("")
            End If
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception("Error at Line No" + clsCommon.myCstr(linno) + Environment.NewLine + ex.Message)
                End Try
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gv)
        End Try
    End Sub

    Private Sub chkFirstSpell_CheckedChanged(sender As Object, e As EventArgs) Handles chkFirstSpell.CheckedChanged
        If chkFirstSpell.Checked = True Then
            chkSecondSpell.Checked = False
        End If
    End Sub

    Private Sub chkSecondSpell_CheckedChanged(sender As Object, e As EventArgs) Handles chkSecondSpell.CheckedChanged
        If chkSecondSpell.Checked = True Then
            chkFirstSpell.Checked = False
        End If
    End Sub

End Class
