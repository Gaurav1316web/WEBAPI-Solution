Imports Telerik.WinControls.UI
Imports Telerik.Collections
Imports System.Data.SqlClient
Imports System.Data
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports System.Threading
Imports common
Public Class FrmFormCollection
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Dim StrQ As String = Nothing
    Dim Dt As DataTable = Nothing
    Dim Ds As DataSet = Nothing
    Dim Adp As SqlDataAdapter = Nothing
    Dim Dr As DataTable = Nothing
    Dim Cmd As SqlCommand
    Dim Conn As SqlConnection
    Dim userCode, companyCode As String
#End Region
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmFormCollection)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnpost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        fndCollection.MyMaxLength = 30
        txtDescription.MaxLength = 100
        txtSrcName.MaxLength = 100
    End Sub
    Private Sub FrmFormCollection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        FormCodeLoad()
        rdbCustomer.IsChecked = True
        fndCollection.MyReadOnly = True
        dtCollectionDate.Text = Date.Now.ToString("dd / MM / yyyy")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        'AddHandler fndCollection.txtValue.TextChanged, AddressOf Fill_Evnt
        'AddHandler fndSrcCode.txtValue.TextChanged, AddressOf NameEvent
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndCollection.Value = clsCommon.myCstr(Me.Tag)
            Funfill()
        End If
    End Sub

    Sub FormCodeLoad()
        StrQ = "select distinct Form_Code  from TSPL_Form_Master "
        Ds = connectSql.RunSQLReturnDS(StrQ)
        ddlFrmCode.DataSource = Ds.Tables(0)
        ddlFrmCode.DisplayMember = "Form_Code"
        ddlFrmCode.ValueMember = "Form_Code"
        ddlFrmCode.Text = "Select"
    End Sub
    Sub funDelete()
        Try
            StrQ = "delete from tspl_collection_form_header where collection_No='" + fndCollection.Value + "'"
            connectSql.RunSql(StrQ)
            Dim SrtQDtl As String = "delete from tspl_collection_form_detail where collection_No='" + fndCollection.Value + "'"
            connectSql.RunSql(SrtQDtl)
            myMessages.delete()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Collection Form", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub
    Public Sub Fill_Evnt(ByVal sender As Object, ByVal e As EventArgs)
        Funfill()
    End Sub
    Sub Funfill()
        Try
            StrQ = " SELECT TSPL_Collection_Form_Header.Collection_No, TSPL_Collection_Form_Header.Description, TSPL_Collection_Form_Header.Collection_Date, " &
            " TSPL_Collection_Form_Header.Type, TSPL_Collection_Form_Header.Form_Code, TSPL_Collection_Form_Header.Source_Code, " &
            " TSPL_Collection_Form_Header.Source_Name, TSPL_Collection_Form_Header.Form_No, " &
            " TSPL_Collection_Form_Detail.Is_Select, TSPL_Collection_Form_Detail.Document_No, TSPL_Collection_Form_Detail.Document_Date," &
            " TSPL_Collection_Form_Detail.Document_Amount FROM TSPL_Collection_Form_Header INNER JOIN " &
            " TSPL_Collection_Form_Detail ON TSPL_Collection_Form_Header.Collection_No = TSPL_Collection_Form_Detail.Collection_No where TSPL_Collection_Form_Header.collection_No='" + fndCollection.Value + "'"
            Dr = clsDBFuncationality.GetDataTable(StrQ)
            'Dr.Read()
            For Each row As DataRow In Dr.Rows
                txtDescription.Text = row("Description").ToString()
                dtCollectionDate.Value = Convert.ToDateTime(row("Collection_Date").ToString()).Date
                Dim s As String = row("Form_Code").ToString()
                ddlFrmCode.SelectedValue = s.Trim()
                Dim Chk As Char = row("Type").ToString()
                If Chk = "V" Then
                    rdbVendor.IsChecked = True
                ElseIf Chk = "C" Then
                    rdbCustomer.IsChecked = True
                End If
                fndSrcCode.Value = row("Source_Code").ToString()
                txtSrcName.Text = row("Source_Name").ToString()
                txtFormNo.Text = row("Form_No").ToString()
            Next
            Dim StrQue As String = " SELECT Collection_No, Is_Select, Document_No, Document_Date, Document_Amount" &
                                   " FROM TSPL_Collection_Form_Detail where collection_No ='" + fndCollection.Value + "'"
            grdFrmDetails.Rows.Clear()
            grdFrmDetails.AutoGenerateColumns = False
            Ds = connectSql.RunSQLReturnDS(StrQue)
            For i As Integer = 0 To Ds.Tables(0).Rows.Count - 1
                Dim grow As GridViewRowInfo = grdFrmDetails.Rows.AddNew()
                grow.Cells("Check").Value = True
                grow.Cells("Document").Value = Ds.Tables(0).Rows(i)("Document_No").ToString()
                grow.Cells("Document Date").Value = Ds.Tables(0).Rows(i).Item("Document_Date").ToString()
                grow.Cells("Amount").Value = Ds.Tables(0).Rows(i).Item("Document_Amount").ToString()
            Next
            btnSave.Text = "Update"
            btnGo.Enabled = False
            'rdbCustomer.Enabled = False
            'rdbVendor.Enabled = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Collection Form", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub
    Sub funInsert()
        Try
            Dim type As Char
            If rdbCustomer.IsChecked = True Then
                type = "C"
            Else
                type = "V"
            End If
            Dim x As Integer = 0
            For j As Integer = 0 To grdFrmDetails.Rows.Count - 1
                If grdFrmDetails.Rows(j).Cells("Check").Value = True Then
                    x = x + 1
                End If
            Next
            If x = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Atleast One Document!", "Collection Form", MessageBoxButtons.OK)
                Exit Sub
            End If
            Dim EntryNo As String = fnAutoGenerateNo()
            Dim EntryDate As String = clsCommon.GetPrintDate(dtCollectionDate.Value, "dd/MMM/yyyy hh:mm tt")
            StrQ = " insert into TSPL_Collection_Form_Header values ('" + EntryNo + "','" + txtDescription.Text + "','" + EntryDate + "','" + type + "'," & _
                   " '" + Convert.ToString(ddlFrmCode.SelectedItems(0)) + "','" + fndSrcCode.Value + "','" + txtSrcName.Text + "','" + txtFormNo.Text + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "')"
            connectSql.RunSql(StrQ)
            For i As Integer = 0 To grdFrmDetails.RowCount - 1
                Dim Is_Chk As Char
                Dim Doc As String = grdFrmDetails.Rows(i).Cells("Document").Value
                Dim Doc_Date As String = grdFrmDetails.Rows(i).Cells("Document Date").Value
                Dim Amt As String = CDec(grdFrmDetails.Rows(i).Cells("Amount").Value)
                If grdFrmDetails.Rows(i).Cells("Check").Value = True Then
                    Is_Chk = "Y"
                    Dim StrQ1 As String = "insert into TSPL_Collection_Form_Detail values ('" + EntryNo + "','" + Is_Chk + "','" + Doc + "','" + Doc_Date + "','" + Amt + "')"
                    connectSql.RunSql(StrQ1)
                End If
            Next
            myMessages.insert()
            'fndCollection.txtValue.Text = EntryNo
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Collection Form", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub
    Sub GridBind()
        If rdbCustomer.IsChecked = True Then
            StrQ = " select Sale_Invoice_No as [Invoice] ,convert(date,Sale_Invoice_Date,103) as [Date],Total_Invoice_Amt as [Amount]  from TSPL_SALE_INVOICE_HEAD" & _
              " where Sale_Invoice_No not in (select Document_No  from TSPL_Collection_Form_Detail ) and TSPL_SALE_INVOICE_HEAD.Cust_Code='" + fndSrcCode.Value + "' "
        Else
            StrQ = " select PI_No as [Invoice] ,PI_Date as [Date],PI_Total_Amt as [Amount] from TSPL_PI_HEAD where " & _
                   " Vendor_Code ='" + fndSrcCode.Value + "' and PI_No not in (select Document_No  from TSPL_Collection_Form_Detail ) "
        End If
        Dt = clsDBFuncationality.GetDataTable(StrQ)
        grdFrmDetails.AutoGenerateColumns = False
        grdFrmDetails.DataSource = Dt
        grdFrmDetails.Columns("Document").FieldName = "Invoice"
        grdFrmDetails.Columns("Document Date").FieldName = "Date"
        grdFrmDetails.Columns("Amount").FieldName = "Amount"
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub rdbCustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbCustomer.ToggleStateChanged
        'If rdbCustomer.IsChecked = True Then
        fndSrcCode.Value = ""
        txtSrcName.Text = ""
        '    'fndSrcCode.ConnectionString = connectSql.SqlCon()
        '    'fndSrcCode.Query = "select Cust_Code as [Customer Code] ,Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        '    'fndSrcCode.ValueToSelect = "Customer Code"
        '    'fndSrcCode.Caption = "Customer Master"
        '    'fndSrcCode.ValueToSelect1 = "Name"
        '    Dim qry As String = "select Cust_Code as [Code] ,Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        '    fndSrcCode.Value = clsCommon.ShowSelectForm("Code", qry, "Code", "", fndSrcCode.Value, "Code", isButtonClicked)
        '    txtSrcName.Text = clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code  ='" & fndSrcCode.Value & "'")


        '    grdFrmDetails.DataSource = Nothing
        '    grdFrmDetails.Rows.Clear()
        'ElseIf rdbVendor.IsChecked = True Then
        fndSrcCode.Value = ""
        txtSrcName.Text = ""
        '    fndSrcCode.ConnectionString = connectSql.SqlCon()
        '    fndSrcCode.Query = "select Vendor_Code as [Vendor Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER"
        '    fndSrcCode.ValueToSelect = "Vendor Code"
        '    fndSrcCode.Caption = "Vendor Master"
        '    fndSrcCode.ValueToSelect1 = "Name"
        '    grdFrmDetails.DataSource = Nothing
        '    grdFrmDetails.Rows.Clear()
        'End If
    End Sub
    'Function funCusName(ByVal Name As String) As String
    '    Dim StrName As String = Nothing
    '    StrQ = "select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + Name + "' "
    '    StrName = connectSql.RunScalar(StrQ)
    '    Return StrName
    'End Function
    ''Public Sub NameEvent(ByVal sender As Object, ByVal e As System.EventArgs)
    ''    If rdbCustomer.IsChecked = True Then
    ''        txtSrcName.Text = funCusName(fndSrcCode.Value)
    ''    Else
    ''        txtSrcName.Text = funVenName(fndSrcCode.Value)
    ''    End If
    ''End Sub
    'Function funVenName(ByVal Name As String) As String
    '    Dim VName As String
    '    StrQ = "select Vendor_Name  from TSPL_VENDOR_MASTER where Vendor_Code='" + Name + "' "
    '    VName = connectSql.RunScalar(StrQ)
    '    Return VName
    'End Function
    Public Function fnAutoGenerateNo() As String
        Dim Maxvlu As String
        Dim NxtMaxNo As Int32
        Dim strQuery As String = "SELECT MAX(Collection_No) as MaxValue from TSPL_Collection_Form_Header  where Collection_No like '%NUMB%' "
        Ds = connectSql.RunSQLReturnDS(strQuery)
        If Ds.Tables(0).Rows.Count > 0 Then
            If Ds.Tables(0).Rows(0)(0).ToString <> "" Then
                Maxvlu = Ds.Tables(0).Rows(0)(0).ToString()
                Maxvlu = Maxvlu.Remove(0, 4)
                NxtMaxNo = Convert.ToInt32(Maxvlu.ToString())
                NxtMaxNo = NxtMaxNo + 1
                Dim strCount As String
                strCount = NxtMaxNo.ToString()
                If strCount.Length = 1 Then
                    Maxvlu = "NUMB" & "000" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 2 Then
                    Maxvlu = "NUMB" & "00" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 3 Then
                    Maxvlu = "NUMB" & "0" & NxtMaxNo.ToString()
                ElseIf strCount.Length = 4 Then
                    Maxvlu = "NUMB" & NxtMaxNo.ToString()
                End If
                Return Maxvlu
            Else
                Maxvlu = "NUMB0001"
                Return Maxvlu
            End If
        Else
            Maxvlu = "NUMB0001"
            Return Maxvlu
        End If
        Return Maxvlu
    End Function
    Sub funNew()
        fndSrcCode.Value = ""
        fndCollection.Value = ""
        dtCollectionDate.Value = System.DateTime.Now.Date
        txtDescription.Text = ""
        txtFormNo.Text = ""
        txtSrcName.Text = ""
        txtDescription.Text = ""
        rdbCustomer.IsChecked = True
        btnSave.Text = "Save"
        grdFrmDetails.DataSource = Nothing
        grdFrmDetails.Rows.Clear()
        FormCodeLoad()
        btnGo.Enabled = True
        rdbCustomer.Enabled = True
        rdbVendor.Enabled = True
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funNew()
    End Sub
    Sub funUpdate()
        Try
            Dim Code As String = fndCollection.Value
            Dim IsChk As Char
            If rdbCustomer.IsChecked = True Then
                IsChk = "C"
            ElseIf rdbVendor.IsChecked = True Then
                IsChk = "V"
            End If
            Dim x As Integer = 0
            For j As Integer = 0 To grdFrmDetails.Rows.Count - 1
                If grdFrmDetails.Rows(j).Cells("Check").Value = True Then
                    x = +1
                End If
            Next
            If x = 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Select Atleast One Document!", "Collection Form", MessageBoxButtons.OK, Me.Text)
                Exit Sub
            End If
            Dim CollectionDate As String = clsCommon.GetPrintDate(dtCollectionDate.Value, "dd/MMM/yyyy hh:mm tt")
            StrQ = " update TSPL_Collection_Form_Header set [Description]='" + txtDescription.Text + "' ,Type='" + IsChk + "', Collection_Date ='" + CollectionDate + "' , Form_Code ='" + Convert.ToString(ddlFrmCode.SelectedItems(0).Value) + "'," & _
                   " Source_Code ='" + fndSrcCode.Value + "' ,Source_Name ='" + txtSrcName.Text + "' , Form_No ='" + txtFormNo.Text + "' ,Modified_By ='" + userCode + "' ,Modified_Date ='" + connectSql.serverDate() + "' " & _
                   " where Collection_No ='" + fndCollection.Value + "'"
            connectSql.RunSql(StrQ)
            Dim SrtQDtl As String = "delete from tspl_collection_form_detail where collection_No='" + fndCollection.Value + "'"
            connectSql.RunSql(SrtQDtl)
            For i As Integer = 0 To grdFrmDetails.RowCount - 1
                Dim Is_Chk As Char
                Dim Doc As String = grdFrmDetails.Rows(i).Cells("Document").Value
                Dim Doc_Date As String = grdFrmDetails.Rows(i).Cells("Document Date").Value
                Dim Amt As String = CDec(grdFrmDetails.Rows(i).Cells("Amount").Value)
                If grdFrmDetails.Rows(i).Cells("Check").Value = True Then
                    Is_Chk = "Y"
                    Dim StrQ1 As String = "insert into TSPL_Collection_Form_Detail values ('" + fndCollection.Value + "','" + Is_Chk + "','" + Doc + "','" + Doc_Date + "','" + Amt + "')"
                    connectSql.RunSql(StrQ1)
                End If
            Next
            myMessages.update()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Collection Form", MessageBoxButtons.OK, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If fndSrcCode.Value <> "" Then
            GridBind()
        End If
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
       
    End Sub
    Sub SaveData()
        If ddlFrmCode.Text <> "Select" Then
            If fndSrcCode.Value <> "" Then
                If txtFormNo.Text <> "" Then
                    If grdFrmDetails.Rows.Count > 0 Then
                        If btnSave.Text = "Save" Then
                            funInsert()
                            funNew()
                        ElseIf btnSave.Text = "Update" Then
                            funUpdate()
                            funNew()
                        End If
                    End If
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "Fill Up Form NO.!", "Collection Form", MessageBoxButtons.OK, Me.Text)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Select Source Code!", "Collection Form", MessageBoxButtons.OK, Me.Text)
            End If
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Select Form Code!", "Collection Form", MessageBoxButtons.OK, Me.Text)
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndCollection.Value <> "" Then
            If myMessages.deleteConfirm() = True Then
                funDelete()
                funNew()
            Else
                Return
            End If
        End If
    End Sub
    'Private Sub fndCollection_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndCollection.ConnectionString = connectSql.SqlCon()
    '    fndCollection.Query = " SELECT Collection_No as [Collection No], Description , Collection_Date as [Date] FROM TSPL_Collection_Form_Header"
    '    fndCollection.ValueToSelect = "Collection No"
    '    fndCollection.Caption = "Collection Master"
    '    fndCollection.ValueToSelect1 = "Description"
    'End Sub

    Private Sub fndSrcCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndSrcCode._MYValidating
        'Dim qry As String = "select Form_Code as [Code],Form_Name as [Form Name] from TSPL_Form_Master"
        'fndSrcCode.Value = clsCommon.ShowSelectForm("Code", qry, "Code", "", fndSrcCode.Value, "Code", isButtonClicked)
        'txtSrcName.Text = clsDBFuncationality.getSingleValue("select Form_Name from TSPL_Form_Master where Form_Code ='" & fndSrcCode.Value & "'")
        If rdbCustomer.IsChecked = True Then
            Dim qry1 As String = "select Cust_Code as [Code] ,Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
            fndSrcCode.Value = clsCommon.ShowSelectForm("fmSrcCode", qry1, "Code", "", fndSrcCode.Value, "Code", isButtonClicked)
            txtSrcName.Text = clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code  ='" & fndSrcCode.Value & "'")
            grdFrmDetails.DataSource = Nothing
            grdFrmDetails.Rows.Clear()
        ElseIf rdbVendor.IsChecked = True Then
            Dim qry2 As String = "select Vendor_Code as  Code ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER"
            fndSrcCode.Value = clsCommon.ShowSelectForm("fmVendorCode", qry2, "Code", "", fndSrcCode.Value, "Code", isButtonClicked)
            txtSrcName.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code  ='" & fndSrcCode.Value & "'")
            grdFrmDetails.DataSource = Nothing
            grdFrmDetails.Rows.Clear()
        End If
        'If rdbCustomer.IsChecked = True Then
        '    txtSrcName.Text = funCusName(fndSrcCode.Value)
        'Else
        '    txtSrcName.Text = funVenName(fndSrcCode.Value)
        'End If

    End Sub
    Private Sub fndCollection__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCollection._MYValidating
        Dim qry As String = "select [Collection_No ] as [Code],[description] as [Description] from TSPL_Collection_Form_Header"
        fndCollection.Value = clsCommon.ShowSelectForm("fmCollection", qry, "Code", "", fndCollection.Value, "Code", isButtonClicked)
        txtDesc.Text = clsDBFuncationality.getSingleValue("select Description from TSPL_Collection_Form_Header where Collection_No='" & fndCollection.Value & "'")
        Funfill()
    End Sub
    Private Sub fndCollection__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCollection._MYNavigator
        Dim qry As String = "select Collection_No  from TSPL_Collection_Form_Header Where 2=2 "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_Collection_Form_Header.Collection_No=(select MIN(Collection_No) from TSPL_Collection_Form_Header)"
            Case NavigatorType.Last
                qry += " and TSPL_Collection_Form_Header.Collection_No=(select MAX(Collection_No) from TSPL_Collection_Form_Header)"
            Case NavigatorType.Next
                qry += " and TSPL_Collection_Form_Header.Collection_No=(select Min(Collection_No) from TSPL_Collection_Form_Header where Collection_No > '" + fndCollection.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_Collection_Form_Header.Collection_No=(select Max(Collection_No) from TSPL_Collection_Form_Header where Collection_No < '" + fndCollection.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_Collection_Form_Header.Collection_No='" + fndCollection.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            fndCollection.Value = clsCommon.myCstr(dt.Rows(0)("Collection_No"))
            Funfill()
        End If
    End Sub

    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "FRM-COLL"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function

    Private Sub FrmFormCollection_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
       
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funNew()

        End If
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class
