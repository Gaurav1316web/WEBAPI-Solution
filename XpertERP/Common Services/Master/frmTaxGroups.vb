'Developed By -  Ajit Singh
'Start Date - 01/05/2011
'Last Modify Date - 23/12/2011 by ---Pankaj Kumar Chaudhary--- [Replaced Old Finder With New Finder, Resolved The Data Retreiving PRoblem]
'Table Used - TSPL_TAX_GROUP_MASTER,TSPL_TAX_GROUP_DETAILS
'' updation by richa agarwal on 18 feb,2015 regarding transfer type transaction in tax finder validation BM00000005586
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports common

Public Class FrmTaxGroups
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dt As DataTable
    Dim con As SqlConnection
    Dim tableName As String = "TSPL_TAX_GROUP_MASTER"
    Dim tableCode As String = "Tax_Group_Code"
    Dim codePrefix As String = "TGC"
   

    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
#End Region

#Region "Finders"
    '''' Code Added By Pankaj Kumar Chaudhary

    Private Sub findTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles findTaxGroup._MYValidating
        Dim qst As String = "select count(*) from TSPL_TAX_GROUP_MASTER where TAX_Group_Code='" + findTaxGroup.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            findTaxGroup.MyReadOnly = False
        Else
            findTaxGroup.MyReadOnly = True
        End If
        If findTaxGroup.MyReadOnly OrElse isButtonClicked Then
            '' add condition in query for transfer type transaction
            Dim qry As String = "SELECT TAX_Group_Code as 'Tax Group',(CASE WHEN Tax_Group_Type='S' THEN 'Sales'  WHEN Tax_Group_Type='T' THEN 'Transfer' ELSE 'Purchase' END) as 'Transaction Type',Tax_Group_Desc as Description,Currency_Code From TSPL_TAX_GROUP_MASTER"
            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("TaxGroupFinder", qry)
            If dr IsNot Nothing Then
                findTaxGroup.Value = dr("Tax group").ToString()
                'Selects The Value of Transaction Type From Finder
                txtCurrencyCode.Value = clsCommon.myCstr(dr("Currency_Code"))
                Dim transType As String = dr("Transaction Type").ToString()
                If transType = "Sales" Then
                    ddlTransType.SelectedIndex = 1
                ElseIf transType = "Purchase" Then
                    ddlTransType.SelectedIndex = 0
                ElseIf transType = "Transfer" Then
                    ddlTransType.SelectedIndex = 2
                End If
                txtDesc.Text = dr("Description")
                'This IS Added For Handling Navigation-------by ---Pankaj Kumar
                txtRowNo.Text = clsDBFuncationality.getSingleValue("Select Row from (SELECT ROW_NUMBER() OVER (ORDER BY Tax_Group_Code) AS Row, Tax_Group_Code, Tax_Group_Desc FROM TSPL_TAX_GROUP_MASTER) xxx Where Tax_Group_Code='" + findTaxGroup.Value + "' And Tax_Group_Desc='" + txtDesc.Text + "'")
                'Code Ends Here
                findTaxGroup.MyReadOnly = True

                fillControls()
                'If userCode <> "ADMIN" Then
                '    If funSetUserAccess() = False Then Exit Sub
                'End If
                If findTaxGroup.Value = String.Empty Then
                    btnAdd.Enabled = False
                    btnDelete.Enabled = False
                End If
                ' Code Ends Here
            End If
        End If
        findTaxGroup.MyReadOnly = True
    End Sub


    Private Sub findTaxGroup__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles findTaxGroup._MYNavigator
        GetData(NavType)
    End Sub

    Public Sub GetData(ByVal NavType As common.NavigatorType)
        '-----------------------------Updated By---Pankaj Kumar---------on---13-04-2012----------
        Dim qry1 As String = "  Select * from (SELECT ROW_NUMBER() OVER (ORDER BY Tax_Group_Code) AS Row, Tax_Group_Code, Tax_Group_Desc, (CASE WHEN Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as [Transaction Type],Currency_Code,Is_Tax_Exempted,Active FROM TSPL_TAX_GROUP_MASTER) xxx Where 1=1"
        Select Case NavType
            Case NavigatorType.First
                qry1 += " And  Row = (Select MIN(Row) as Row from (SELECT ROW_NUMBER() OVER (ORDER BY Tax_Group_Code) AS Row from  TSPL_TAX_GROUP_MASTER ) yyy)"
            Case NavigatorType.Last
                qry1 += " And  Row = (Select MAX(Row) as Row from (SELECT ROW_NUMBER() OVER (ORDER BY Tax_Group_Code) AS Row from  TSPL_TAX_GROUP_MASTER ) yyy)"
            Case NavigatorType.Next
                qry1 += " And  Row = (Select MIN(Row) as Row from (SELECT ROW_NUMBER() OVER (ORDER BY Tax_Group_Code) AS Row from  TSPL_TAX_GROUP_MASTER ) yyy Where Row>" + txtRowNo.Text + ")"
            Case NavigatorType.Previous
                qry1 += " And  Row = (Select MAX(Row) as Row from (SELECT ROW_NUMBER() OVER (ORDER BY Tax_Group_Code) AS Row from  TSPL_TAX_GROUP_MASTER ) yyy Where Row<" + txtRowNo.Text + ")"
            Case NavigatorType.Current
                qry1 += " AND  Row=" + txtRowNo.Text + ""
        End Select
        '-----------------------------------Code Ends Here----------------------------------------
        '---------------------------------Old Code For Navition-----------------------------------
        'Dim qry As String = ""
        'Select Case NavType
        '    Case NavigatorType.First
        '        qry += " SELECT MIN(TAX_Group_Code) as 'Tax Group', MIN((CASE WHEN Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END)) as 'Transaction Type', MIN(Tax_Group_Desc) as Description From TSPL_TAX_GROUP_MASTER "
        '    Case NavigatorType.Last
        '        qry += " SELECT MAX(TAX_Group_Code) as 'Tax Group', MAX((CASE WHEN Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END)) as 'Transaction Type', MAX(Tax_Group_Desc) as Description From TSPL_TAX_GROUP_MASTER "
        '    Case NavigatorType.Next
        '        qry += " "
        '    Case NavigatorType.Previous
        '        qry += " "
        '    Case NavigatorType.Current
        '        qry += " SELECT TAX_Group_Code as 'Tax Group',(CASE WHEN Tax_Group_Type='S' THEN 'Sales' ELSE 'Purchase' END) as 'Transaction Type',Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER WHERE TSPL_TAX_GROUP_MASTER.TAX_Group_Code='" + findTaxGroup.Value + "'"
        'End Select
        '-------------------------------Code Ends HEre--------------------------------------------
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            findTaxGroup.Value = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Code"))
            Dim transType As String = clsCommon.myCstr(dt.Rows(0)("Transaction Type"))
            If transType = "Sales" Then
                ddlTransType.SelectedIndex = 1
            ElseIf transType = "Purchase" Then
                ddlTransType.SelectedIndex = 0
            End If
            txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Tax_Group_Desc"))
            txtRowNo.Text = dt.Rows(0)("Row")
            txtCurrencyCode.Value = clsCommon.myCstr(dt.Rows(0)("Currency_Code"))
            chkTaxExempted.Checked = IIf(clsCommon.myCdbl(dt.Rows(0)("Is_Tax_Exempted")) = 1, True, False)
            chkActive.Checked = IIf(clsCommon.myCdbl(dt.Rows(0)("Active")) = 1, True, False)
        End If

        fillControls()
        findTaxGroup.MyReadOnly = True

    End Sub

#End Region

#Region "Methods"
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "TAX-GRP-M"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text, MessageBoxButtons.OK)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnAdd.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btnDelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function

    Private Sub fillTransactionType()
        Dim dataTableTransType As New DataTable()
        dataTableTransType.Columns.Add("TransDesc")
        dataTableTransType.Columns.Add("TransValue")
        dataTableTransType.Rows.Add("Purchase", "P")
        dataTableTransType.Rows.Add("Sales", "S")
        dataTableTransType.Rows.Add("Transfer", "T")
        ddlTransType.DisplayMember = "TransDesc"
        ddlTransType.ValueMember = "TransValue"
        ddlTransType.DataSource = dataTableTransType
        ddlTransType.SelectedValue = "S"
    End Sub

    Private Sub reset()
        txtRowNo.Text = 0
        findTaxGroup.MyReadOnly = False
        findTaxGroup.Focus()
        chkExcisable.ToggleState = ToggleState.Off
        chkSale.ToggleState = ToggleState.Off
        chkVat.ToggleState = ToggleState.Off
        pnlTaxFormula.Text = String.Empty
        findTaxGroup.Value = String.Empty
        ddlTransType.SelectedValue = "S"
        txtDesc.Text = String.Empty
        gvTaxGroups.DataSource = Nothing
        txtCurrencyCode.Value = Nothing

        gvTaxGroups.Rows.Clear()
        btnAdd.Text = "Save"
        Dim i As Integer = 0
        chkTransfer.Checked = False
        chkTaxExempted.Checked = False
        'For i = 0 To gvTaxGroups.Rows.Count
        '    gvTaxGroups.Rows.RemoveAt(i)
        'Next
        chkActive.Checked = False
    End Sub

    'fill tax authority gridview column
    Private Sub fillTaxAuthority()
        Dim CurrCheck As String = ""
        If pnlCurrConv.Visible = True Then
            CurrCheck = " AND CURRENCY_CODE='" & txtCurrencyCode.Value & "'"
        End If
        If chkExcisable.ToggleState = ToggleState.Off Then
            sql = "select Tax_Code as 'Tax Authority',Tax_Code_Desc as Description,Tax_Liability_Account as 'Tax Liability Account'," & _
            "(Case when Tax_Recoverable='Y' then 'Yes' ELSE 'No' END) as Tax_Recoverable,Tax_Recoverable_Account as 'Tax Recoverable Account' " & _
            " from TSPL_TAX_MASTER WHERE (Tax_Code IN (SELECT Tax_Code  FROM TSPL_TAX_RATES  where Tax_Type='" & ddlTransType.SelectedValue & "' )) " & CurrCheck

        ElseIf chkExcisable.ToggleState = ToggleState.On And chkSale.ToggleState = ToggleState.Off And chkVat.ToggleState = ToggleState.Off Then
            sql = "select Tax_Code as 'Tax Authority',Tax_Code_Desc as Description,Tax_Liability_Account as 'Tax Liability Account'," & _
           "(Case when Tax_Recoverable='Y' then 'Yes' ELSE 'No' END) as Tax_Recoverable,Tax_Recoverable_Account as 'Tax Recoverable Account' " & _
           " from TSPL_TAX_MASTER WHERE (Tax_Code IN (SELECT Tax_Code  FROM TSPL_TAX_RATES where Tax_Type='" & ddlTransType.SelectedValue & "'))  " & CurrCheck

        ElseIf chkExcisable.Checked = True Or chkSale.Checked = True Or chkVat.Checked = True Then
            sql = "select Tax_Code as 'Tax Authority',Tax_Code_Desc as Description,Tax_Liability_Account as 'Tax Liability Account'," & _
           "(Case when Tax_Recoverable='Y' then 'Yes' ELSE 'No' END) as Tax_Recoverable,Tax_Recoverable_Account as 'Tax Recoverable Account' " & _
           " from TSPL_TAX_MASTER WHERE (Tax_Code IN (SELECT Tax_Code  FROM TSPL_TAX_RATES where Tax_Type='" & ddlTransType.SelectedValue & "')) " & CurrCheck
        Else
            sql = "select Tax_Code as 'Tax Authority',Tax_Code_Desc as Description,Tax_Liability_Account as 'Tax Liability Account'," & _
                       "(Case when Tax_Recoverable='Y' then 'Yes' ELSE 'No' END) as Tax_Recoverable,Tax_Recoverable_Account as 'Tax Recoverable Account' " & _
                       " from TSPL_TAX_MASTER WHERE (Tax_Code IN (SELECT Tax_Code  FROM TSPL_TAX_RATES where Tax_Type='" & ddlTransType.SelectedValue & "')) " & CurrCheck
        End If

        'If objCommonVar.GSTApplicable And objCommonVar.GSTActiveTaxGroup Then
        '    sql = sql + " and GSTActive=1"
        'End If

        ds = connectSql.RunSQLReturnDS(sql)
        Dim gvMultiComboColum As GridViewMultiComboBoxColumn = TryCast(gvTaxGroups.Columns(0), GridViewMultiComboBoxColumn)
        gvMultiComboColum.ValueMember = "Tax Authority"
        gvMultiComboColum.DisplayMember = "Tax Authority"
        gvMultiComboColum.DataSource = ds.Tables(0)
        Dim gvMultiComboColum1 As GridViewMultiComboBoxColumn = TryCast(gvTaxGroups.Columns(4), GridViewMultiComboBoxColumn)
        gvMultiComboColum1.ValueMember = "Tax Authority"
        gvMultiComboColum1.DisplayMember = "Tax Authority"
        gvMultiComboColum1.DataSource = ds.Tables(0)
    End Sub

    Private Function validateRecords() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso findTaxGroup.Value = String.Empty Then
            myMessages.blankValue("Tax Group")
            findTaxGroup.Focus()
            Return False
        ElseIf (txtDesc.Text = String.Empty) Then
            myMessages.blankValue("Description")
            txtDesc.Focus()
            Return False
        ElseIf (gvTaxGroups.RowCount = 0) Then
            myMessages.zeroGridRow()
            Return False
        Else
            For ii As Integer = 0 To gvTaxGroups.Rows.Count - 1
                For jj As Integer = ii + 1 To gvTaxGroups.Rows.Count - 1
                    If clsCommon.myLen(gvTaxGroups.Rows(ii).Cells(0).Value) > 0 Then
                        If clsCommon.CompairString(gvTaxGroups.Rows(ii).Cells(0).Value, gvTaxGroups.Rows(jj).Cells(0).Value) = CompairStringResult.Equal Then
                            common.clsCommon.MyMessageBoxShow("Duplicate tax authorities are not allowed.")
                            Return False
                        End If
                    End If
                Next
            Next
            For Each grow As GridViewRowInfo In gvTaxGroups.Rows
                If grow.Cells(3).Value = "Yes" And grow.Cells(4).Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Surcharge can not be left blank for " + grow.Cells(0).Value)
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Function validateTaxGroup() As Boolean
        Try
            Dim TaxType As String
            Dim arrTaxType As New List(Of String)
            Dim arrTax As New List(Of String)
            For ii As Integer = 0 To gvTaxGroups.Rows.Count - 1
                Dim strTaxCode As String = clsCommon.myCstr(gvTaxGroups.Rows(ii).Cells("taxAuthority").Value)
                If clsCommon.myLen(strTaxCode) > 0 Then
                    Dim strSurTaxCode As String = clsCommon.myCstr(gvTaxGroups.Rows(ii).Cells("surtaxonAuthority").Value)
                    If arrTax.Contains(strTaxCode) Then
                        Throw New Exception("Duplicate Tax Authority not allowed.")
                    Else
                        arrTax.Add(strTaxCode)
                    End If
                    If clsCommon.CompairString(clsCommon.myCstr(gvTaxGroups.Rows(ii).Cells("surtax").Value), "Yes") = CompairStringResult.Equal Then
                        If Not arrTax.Contains(strSurTaxCode) Then
                            Throw New Exception("Surtax Authority must appear as Tax Authority earlier in the group.")
                        End If
                    End If

                    TaxType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select type from TSPL_TAX_MASTER where Tax_Code='" & strTaxCode & "'"))
                    If TaxType <> "" Then
                        arrTaxType.Add(TaxType)
                    End If

                End If
            Next



            If objCommonVar.GSTApplicable Then
                If arrTaxType.Contains("IGST") AndAlso (arrTaxType.Contains("CGST") Or arrTaxType.Contains("SGST") Or arrTaxType.Contains("UGST")) Then
                    Throw New Exception("A group having IGST cannot have CGST/SGST/UGST.")
                ElseIf (arrTaxType.Contains("CGST") And arrTaxType.Contains("SGST")) AndAlso (arrTaxType.Contains("IGST") Or arrTaxType.Contains("UGST")) Then
                    Throw New Exception("A group having CGST and SGST cannot have IGST or UGST.")
                ElseIf (arrTaxType.Contains("CGST") And arrTaxType.Contains("UGST")) AndAlso (arrTaxType.Contains("SGST") Or arrTaxType.Contains("IGST")) Then
                    Throw New Exception("A group having CGST and UGST cannot have SGST or IGST.")
                ElseIf (arrTaxType.Contains("UGST") Or arrTaxType.Contains("SGST")) AndAlso (Not arrTaxType.Contains("CGST")) Then
                    Throw New Exception("A group having UGST or SGST must have CGST.")
                ElseIf arrTaxType.Contains("CGST") AndAlso (Not (arrTaxType.Contains("UGST") Or arrTaxType.Contains("SGST"))) Then
                    Throw New Exception("A group having CGST must have UGST or SGST.")
                End If
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

        Return True
    End Function

    'Private Function validateTaxGroup() As Boolean
    '    For Each grow As GridViewRowInfo In gvTaxGroups.Rows
    '        Dim growSurtax As String
    '        If grow.Cells(4).Value Is Nothing Then
    '            growSurtax = String.Empty
    '        Else
    '            growSurtax = grow.Cells(4).Value.ToString()
    '        End If

    '        Dim growTax As String = grow.Cells(0).Value
    '        If growTax Is Nothing Then
    '            common.clsCommon.MyMessageBoxShow("Tax Authority can not be left blank.")
    '            Return False
    '        End If
    '        If (growTax.Equals(growSurtax)) Then
    '            common.clsCommon.MyMessageBoxShow("Surtax Authority must appear as Tax Authority earlier in the group.")
    '            Return False
    '        End If
    '        Dim arr As New ArrayList()

    '        For Each grow1 As GridViewRowInfo In gvTaxGroups.Rows
    '            Dim grow1Surtax As String
    '            If grow1.Cells(4).Value Is Nothing Then
    '                grow1Surtax = String.Empty
    '            Else
    '                grow1Surtax = grow1.Cells(4).Value.ToString()
    '            End If
    '            Dim grow1Tax As String = grow1.Cells(0).Value
    '            If grow.Index <> grow1.Index Then
    '                If growTax.Equals(grow1Tax) Then
    '                    common.clsCommon.MyMessageBoxShow("Duplicate Tax Authority not allowed.")
    '                    Return False
    '                ElseIf (grow1.Index < grow.Index) Then
    '                    arr.Add(grow1Tax)
    '                End If
    '            End If
    '            If Not growSurtax = String.Empty AndAlso Not arr.Contains(growSurtax) AndAlso arr.Count > 0 Then
    '                common.clsCommon.MyMessageBoxShow("Surtax Authority must appear as Tax Authority earlier in the group.")
    '                Return False
    '            End If
    '        Next
    '    Next
    '    Return True
    'End Function



    Private Sub fillControls()
        gvTaxGroups.DataSource = Nothing
        gvTaxGroups.Rows.Clear()
        sql = "Select (Case Excisable when 'Y' then 'True' else 'False' end),(Case Vat when 'Y' then 'True' else 'False' end)," & _
        "(Case STax when 'Y' then 'True' else 'False' end),Is_Transfer,Is_Tax_Exempted,Active from TSPL_Tax_Group_Master where Tax_Group_Code='" + findTaxGroup.Value + "'"
        dt = clsDBFuncationality.GetDataTable(sql)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            For Each dr As DataRow In dt.Rows
                chkExcisable.Checked = dr(0).ToString()
                chkVat.Checked = dr(1).ToString()
                chkSale.Checked = dr(2).ToString()
                chkTransfer.Checked = IIf(clsCommon.myCdbl(dr("Is_Transfer")) = 1, True, False)
                chkTaxExempted.Checked = IIf(clsCommon.myCdbl(dr("Is_Tax_Exempted")) = 1, True, False)
                chkActive.Checked = IIf(clsCommon.myCdbl(dr("Active")) = 1, True, False)
            Next
        End If
        sql = "SELECT  TSPL_TAX_GROUP_DETAILS.Tax_Code, TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc, " & _
              "(CASE WHEN TSPL_TAX_GROUP_DETAILS.Taxable = 'Y' THEN 'Yes' ELSE 'No' END) AS Taxable, " & _
              "(CASE WHEN TSPL_TAX_GROUP_DETAILS.Surtax = 'Y' THEN 'Yes' ELSE 'No' END) AS Surtax, TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code, " & _
              "TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code_Desc,TSPL_TAX_GROUP_DETAILS.Currency_Code,TSPL_TAX_GROUP_DETAILS.ConvRate,TSPL_TAX_GROUP_DETAILS.ApplicableFrom  " & _
              " FROM TSPL_TAX_GROUP_DETAILS INNER JOIN TSPL_Tax_Group_Master ON TSPL_TAX_GROUP_DETAILS.Tax_Group_Code = TSPL_Tax_Group_Master.Tax_Group_Code AND TSPL_TAX_GROUP_DETAILS.Tax_Group_Type = TSPL_TAX_GROUP_MASTER.Tax_Group_Type" & _
              " WHERE TSPL_Tax_Group_Master.Tax_Group_Code='" + findTaxGroup.Value + "' and TSPL_Tax_Group_Master.Tax_Group_Type='" + ddlTransType.SelectedValue + "' Order by TSPL_TAX_GROUP_DETAILS.trans_Code"
        dt = clsDBFuncationality.GetDataTable(sql)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 0
            For Each dr1 As DataRow In dt.Rows
                Dim viewInfo As New GridViewInfo(gvTaxGroups.MasterTemplate)
                Dim dataRowInfo As New GridViewDataRowInfo(viewInfo)
                dataRowInfo.Cells(0).Value = dr1(0).ToString()
                dataRowInfo.Cells(1).Value = dr1(1).ToString()
                dataRowInfo.Cells(2).Value = dr1(2).ToString()
                dataRowInfo.Cells(3).Value = dr1(3).ToString()
                dataRowInfo.Cells(4).Value = dr1(4).ToString()
                dataRowInfo.Cells(5).Value = dr1(5).ToString()

                '' MultiCurrency
                dataRowInfo.Cells("CurrencyCode").Value = clsCommon.myCstr(dr1("Currency_Code").ToString())
                dataRowInfo.Cells("ConvRate").Value = clsCommon.myCdbl(dr1("ConvRate").ToString())
                If clsCommon.myLen(dr1("ApplicableFrom").ToString) > 0 Then
                    dataRowInfo.Cells("ApplicableFrom").Value = clsCommon.GetPrintDate(dr1("ApplicableFrom"), "dd/MMM/yyyy")
                Else
                    dataRowInfo.Cells("ApplicableFrom").Value = ""
                End If



                'End MultiCurrency'
                gvTaxGroups.Rows.Insert(i, dataRowInfo)
                i = i + 1
            Next
            ' transportSql.FillGridView(sql, gvTaxGroups)
            createFormula()
            btnAdd.Text = "Update"
            btnAdd.Enabled = True
            btnDelete.Enabled = True
        Else
            txtDesc.Text = String.Empty
            btnAdd.Text = "Save"
            btnAdd.Enabled = True
            btnDelete.Enabled = False
            'If userCode <> "ADMIN" Then
            '    If funSetUserAccess() = False Then Exit Sub
            'End If
        End If
    End Sub
#End Region

#Region "events"
    Private Sub SetUserMgmtNew()
        '' Anubhooti 30-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.taxGroup)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnAdd.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 03/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnAdd.Visible = True Then
            menuImport.Enabled = True
            menuExport.Enabled = True
        Else
            menuImport.Enabled = False
            menuExport.Enabled = False
        End If
        '--------------------------------------------------
        '         btnclose.Visible = MyBase.isDeleteFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        findTaxGroup.MyMaxLength = 12
        txtDesc.MaxLength = 50
    End Sub
    Private Sub FrmTaxGroups_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        findTaxGroup.Focus()
        txtRowNo.Text = 0
        txtRowNo.Visible = False
        SetDataBaseGrid()
        RadPageView1.SelectedPage = RadPageViewPage1

        fillTransactionType()
        fillTaxAuthority()
        findTaxGroup.MyMaxLength = 12
        findTaxGroup.MyReadOnly = True
        ButtonToolTip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

        '' MultiCurrency
        SetMultiCurrencyVisibility()
        '' End of MultiCurrency
        If objCommonVar.GSTApplicable Then
            gbActive.Enabled = True
        Else
            gbActive.Enabled = False
        End If
    End Sub

    Sub SetMultiCurrencyVisibility()
        Dim strq As String = ""
        If clsModuleCurrencyMapping.CheckMultiCurrency(Me.Module_Code) = True Then
            pnlCurrConv.Visible = True
            gvTaxGroups.Columns("CurrencyCode").IsVisible = True
            gvTaxGroups.Columns("ConvRate").IsVisible = True
            gvTaxGroups.Columns("ApplicableFrom").IsVisible = True
        Else
            RadGroupBox1.Height = RadGroupBox1.Height - pnlCurrConv.Height
            pnlCurrConv.Height = 0
            pnlCurrConv.Visible = False

            gvTaxGroups.Columns("CurrencyCode").IsVisible = False
            gvTaxGroups.Columns("ConvRate").IsVisible = False
            gvTaxGroups.Columns("ApplicableFrom").IsVisible = False
        End If
        gvTaxGroups.Columns("CurrencyCode").ReadOnly = True
        gvTaxGroups.Columns("ConvRate").ReadOnly = True
        gvTaxGroups.Columns("ApplicableFrom").ReadOnly = True
    End Sub
   
    Private Sub keyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Public Sub savedata()
        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.taxGroup, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        'If clsCommon.myLen(findTaxGroup.Value) <= 0 Then
        '    common.clsCommon.MyMessageBoxShow("Tax Group can not be left blank")
        '    findTaxGroup.Focus()
        '    findTaxGroup.MyReadOnly = False
        '    Return
        'End If
        If pnlCurrConv.Visible Then
            If clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Currency first.")
                txtCurrencyCode.Focus()
                Return
            End If
        End If
        gvTaxGroups.EndEdit()
        If validateRecords() Then
            If validateTaxGroup() Then
                Dim strTaxable As String = "N"
                Dim strSurtax As String = "N"
                Dim strExcise As String = "N"
                Dim strVat As String = "N"
                Dim strSTax As String = "N"
                If chkExcisable.ToggleState = ToggleState.On Then
                    strExcise = "Y"
                End If
                If chkVat.ToggleState = ToggleState.On Then
                    strVat = "Y"
                End If
                If chkSale.ToggleState = ToggleState.On Then
                    strSTax = "Y"
                End If
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Dim arrDB As List(Of String) = GetReplecateCompaniesDataBase()
                Try
                    Dim isSaved As Boolean = True
                    If btnAdd.Text = "Save" Then
                        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, trans)) = "1", True, False)) Then
                            Dim ChkNewEntry As String = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_TAX_GROUP_MASTER where Tax_Group_Code='" & findTaxGroup.Value & "'", trans)
                            If ChkNewEntry = 0 Then
                                findTaxGroup.Value = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"), clsDocType.TaxGroup, "", "")
                                If clsCommon.myLen(findTaxGroup.Value) <= 0 Then
                                    Throw New Exception("Error in Code Generation")
                                End If
                            End If
                        End If
                        isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_MASTER_INSERT", New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Desc", txtDesc.Text), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue), New SqlParameter("@Excise", strExcise), New SqlParameter("@Vat", strVat), New SqlParameter("@STax", strSTax), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                        Dim col As New Hashtable()
                        clsCommon.AddColumnsForChange(col, "CURRENCY_CODE", txtCurrencyCode.Value, True)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + findTaxGroup.Value + "'", trans)

                        'sanjay
                        Dim col1 As New Hashtable()
                        clsCommon.AddColumnsForChange(col1, "Active", clsCommon.myCstr(IIf(chkActive.Checked, 1, 0)), True)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col1, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + findTaxGroup.Value + "'", trans)
                        'sanjay

                        For Each grow As GridViewRowInfo In gvTaxGroups.Rows
                            If grow.Cells(2).Value = "Yes" Then
                                strTaxable = "Y"
                            Else
                                strTaxable = "N"
                            End If
                            If grow.Cells(3).Value = "Yes" Then
                                strSurtax = "Y"
                            Else
                                strSurtax = "N"
                            End If



                            If (grow.Cells(4).Value Is Nothing) Then
                                isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_DETAIL_INSERT", New SqlParameter("@Trans_Code", grow.Index + 1), New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue), New SqlParameter("@Tax_Code", grow.Cells(0).Value), New SqlParameter("@Tax_Code_Desc", grow.Cells(1).Value), New SqlParameter("@Taxable", strTaxable), New SqlParameter("@Surtax", strSurtax), New SqlParameter("@Surtax_Tax_Code", ""), New SqlParameter("@Surtax_Tax_Code_Desc", ""), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                            Else
                                isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_DETAIL_INSERT", New SqlParameter("@Trans_Code", grow.Index + 1), New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue), New SqlParameter("@Tax_Code", grow.Cells(0).Value), New SqlParameter("@Tax_Code_Desc", grow.Cells(1).Value), New SqlParameter("@Taxable", strTaxable), New SqlParameter("@Surtax", strSurtax), New SqlParameter("@Surtax_Tax_Code", grow.Cells(4).Value), New SqlParameter("@Surtax_Tax_Code_Desc", grow.Cells(5).Value), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                            End If

                            '' multicurrency
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", grow.Cells("CurrencyCode").Value, True)
                            clsCommon.AddColumnsForChange(coll, "ConvRate", clsCommon.myCdbl(grow.Cells("ConvRate").Value))
                            If clsCommon.myLen(grow.Cells("ApplicableFrom").Value) > 0 Then
                                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(grow.Cells("ApplicableFrom").Value, "dd/MMM/yyyy"), True)
                            End If
                            isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_GROUP_DETAILS", OMInsertOrUpdate.Update, " Tax_Group_Code='" & findTaxGroup.Value & "' and  Tax_Code='" + clsCommon.myCstr(grow.Cells(0).Value) + "'", trans)
                            '' end of multicurrency
                        Next
                        UpdateTransferCheckBox(arrDB, trans)
                        If isSaved Then
                            trans.Commit()
                            myMessages.insert()
                            btnAdd.Text = "Update"
                            btnDelete.Enabled = True
                        Else
                            trans.Rollback()
                        End If
                    ElseIf btnAdd.Text = "Update" Then
                        isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_MASTER_UPDATE", New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Desc", txtDesc.Text), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue), New SqlParameter("@Excisable", strExcise), New SqlParameter("@Vat", strVat), New SqlParameter("@STax", strSTax), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))

                        'sanjay
                        Dim col1 As New Hashtable()
                        clsCommon.AddColumnsForChange(col1, "Active", clsCommon.myCstr(IIf(chkActive.Checked, 1, 0)), True)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col1, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "TSPL_TAX_GROUP_MASTER.Tax_Group_Code='" + findTaxGroup.Value + "'", trans)
                        'sanjay

                        isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_DETAIL_DELETE", New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue))
                        For Each grow As GridViewRowInfo In gvTaxGroups.Rows
                            If grow.Cells(2).Value = "Yes" Then
                                strTaxable = "Y"
                            Else
                                strTaxable = "N"
                            End If
                            If grow.Cells(3).Value = "Yes" Then
                                strSurtax = "Y"
                            Else
                                strSurtax = "N"
                            End If

                            If (grow.Cells(4).Value Is Nothing) Then
                                isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_DETAIL_INSERT", New SqlParameter("@Trans_Code", grow.Index + 1), New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue), New SqlParameter("@Tax_Code", grow.Cells(0).Value), New SqlParameter("@Tax_Code_Desc", grow.Cells(1).Value), New SqlParameter("@Taxable", strTaxable), New SqlParameter("@Surtax", strSurtax), New SqlParameter("@Surtax_Tax_Code", DBNull.Value), New SqlParameter("@Surtax_Tax_Code_Desc", DBNull.Value), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                            Else
                                isSaved = isSaved AndAlso clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDB, "sp_TSPL_TAX_GROUP_DETAIL_INSERT", New SqlParameter("@Trans_Code", grow.Index + 1), New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue), New SqlParameter("@Tax_Code", grow.Cells(0).Value), New SqlParameter("@Tax_Code_Desc", grow.Cells(1).Value), New SqlParameter("@Taxable", strTaxable), New SqlParameter("@Surtax", strSurtax), New SqlParameter("@Surtax_Tax_Code", grow.Cells(4).Value), New SqlParameter("@Surtax_Tax_Code_Desc", grow.Cells(5).Value), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                            End If

                            '' multicurrency
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "CURRENCY_CODE", grow.Cells("CurrencyCode").Value, True)
                            clsCommon.AddColumnsForChange(coll, "ConvRate", clsCommon.myCdbl(grow.Cells("ConvRate").Value))
                            If clsCommon.myLen(grow.Cells("ApplicableFrom").Value) > 0 Then
                                clsCommon.AddColumnsForChange(coll, "ApplicableFrom", clsCommon.GetPrintDate(grow.Cells("ApplicableFrom").Value, "dd/MMM/yyyy"), True)
                            End If
                            isSaved = isSaved And clsCommonFunctionality.UpdateDataTable(coll, "TSPL_TAX_GROUP_DETAILS", OMInsertOrUpdate.Update, " Tax_Group_Code='" & findTaxGroup.Value & "' and  Tax_Code='" + clsCommon.myCstr(grow.Cells(0).Value) + "'", trans)
                            '' end of multicurrency
                        Next
                        UpdateTransferCheckBox(arrDB, trans)
                        If isSaved Then
                            trans.Commit()
                            myMessages.update()
                            btnAdd.Text = "Update"
                        Else
                            trans.Rollback()
                        End If
                    End If
                Catch ex As Exception
                    trans.Rollback()
                    myMessages.myExceptions(ex)
                    Exit Sub
                End Try
            End If

        End If
    End Sub
    Sub UpdateTransferCheckBox(ByVal Arr As List(Of String), ByVal trans As SqlTransaction)
        Dim qry As String = " update " + clsCommon.ReplicateDBString + " TSPL_TAX_GROUP_MASTER set Is_Transfer=" + clsCommon.myCstr(IIf(chkTransfer.Checked, 1, 0)) + " ,Is_Tax_Exempted=" + clsCommon.myCstr(IIf(chkTaxExempted.Checked, 1, 0)) + "  where Tax_Group_Code= '" + findTaxGroup.Value + "' "
        clsDBFuncationality.ExecuteNonQueryInSelectedDatabase(qry, Arr, trans)
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        savedata()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        deletedata()
    End Sub
    Public Sub deletedata()

        If clsCommon.myLen(findTaxGroup.Value) <= 0 Then
            findTaxGroup.Focus()
            Return
        End If
        If myMessages.deleteConfirm() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim qry = "select count(*) from tspl_transfer_head where Tax_Group='" + findTaxGroup.Value + "'"
                Dim count As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                'trans.Commit()
                If count = 0 Then
                    clsDBFuncationality.UpdateInSelectedDatabase(trans, GetReplecateCompaniesDataBase(), "sp_TSPL_TAX_GROUP_MASTER_DELETE", New SqlParameter("@Tax_Group_Code", findTaxGroup.Value), New SqlParameter("@Tax_Group_Type", ddlTransType.SelectedValue))
                    trans.Commit()
                    myMessages.delete()
                    btnAdd.Text = "Save"
                Else
                    common.clsCommon.MyMessageBoxShow("This Record Cannot be deleted.It is used by another process")
                    trans.Rollback()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()
            End Try
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub gvTaxGroups_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTaxGroups.CellValueChanged
        Try

            Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
            Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
            If column.Index = 0 Then
                If pnlCurrConv.Visible And findTaxGroup.MyReadOnly = False Then
                    If clsCommon.myLen(txtCurrencyCode.Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Select Currency first.")
                        txtCurrencyCode.Focus()
                        Exit Sub
                    End If
                End If

                Dim taxdesc As String
                sql = "select Tax_Code_Desc,Currency_Code,ConvRate,ApplicableFrom from TSPL_TAX_MASTER where Tax_Code='" + grow.Cells(0).Value + "'"
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable(sql)
                If dt.Rows.Count > 0 Then
                    taxdesc = dt.Rows(0).Item("Tax_Code_Desc").ToString
                    grow.Cells(1).Value = taxdesc

                    '' multicurrency
                    grow.Cells("CurrencyCode").Value = dt.Rows(0).Item("Currency_Code").ToString
                    grow.Cells("ConvRate").Value = clsCommon.myCdbl(dt.Rows(0).Item("ConvRate").ToString)
                    If clsCommon.myLen(dt.Rows(0).Item("ApplicableFrom").ToString) > 0 Then
                        grow.Cells("ApplicableFrom").Value = clsCommon.GetPrintDate(dt.Rows(0).Item("ApplicableFrom"), "dd/MMM/yyyy")
                    Else
                        grow.Cells("ApplicableFrom").Value = ""
                    End If
                    '' end multicurrency
                Else

                End If

                'taxdesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
                'If clsCommon.myLen(taxdesc) > 0 Then
                '    grow.Cells(1).Value = taxdesc
                'End If
            ElseIf column.Index = 4 Then
                Dim taxcdedesc As String
                sql = "select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + grow.Cells(4).Value + "'"
                taxcdedesc = clsCommon.myCstr(clsDBFuncationality.getSingleValue(sql))
                If clsCommon.myLen(taxcdedesc) > 0 Then
                    grow.Cells(5).Value = taxcdedesc
                End If
            ElseIf column.Index = 3 Then
                If grow.Cells(3).Value = "No" Then
                    RemoveHandler gvTaxGroups.ValueChanged, AddressOf gvTaxGroups_ValueChanged
                    grow.Cells(4).Value = ""
                    grow.Cells(5).Value = ""
                    AddHandler gvTaxGroups.ValueChanged, AddressOf gvTaxGroups_ValueChanged
                End If

            End If
            createFormula()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub ddlTransType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlTransType.SelectedIndexChanged
        Try
            fillControls()
            fillTaxAuthority()

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub gvTaxGroups_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gvTaxGroups.CellBeginEdit
        If TypeOf Me.gvTaxGroups.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gvTaxGroups.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            If gvTaxGroups.CurrentColumn.Name = "surtaxonAuthority" Then
                If gvTaxGroups.CurrentRow.Cells(3).Value = "No" Then
                    e.Cancel = True
                Else
                    e.Cancel = False
                End If
            End If
        End If
    End Sub

    Private Sub gvTaxGroups_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTaxGroups.CellEditorInitialized
        If TypeOf Me.gvTaxGroups.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gvTaxGroups.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor(editor.DisplayMember, FilterOperator.Contains, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub

    Private Sub gvTaxGroups_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvTaxGroups.UserAddedRow
        Try
            createFormula()

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub createFormula()
        Try
            pnlTaxFormula.Text = ""
            Dim taxableTax As String = ""
            If chkExcisable.ToggleState = ToggleState.Off Then
                For Each grow As GridViewRowInfo In gvTaxGroups.Rows
                    taxableTax = ""
                    For Each gro As GridViewRowInfo In gvTaxGroups.Rows
                        If gro.Index < grow.Index AndAlso gro.Cells(2).Value = "Yes" Then
                            taxableTax = taxableTax + " + (Tax Amount for <b>" + gro.Cells(0).Value + "</b>)"
                        End If
                    Next
                    If grow.Cells(3).Value = "No" Then
                        pnlTaxFormula.Text = pnlTaxFormula.Text + "<html><b>" + grow.Cells(0).Value + "-</b> (Base Amount) " + taxableTax + " * (Tax Rate for <b>" + grow.Cells(0).Value + "</b>)/100<br></html>"
                    Else
                        For Each gro As GridViewRowInfo In gvTaxGroups.Rows
                            If gro.Cells(0).Value = grow.Cells(4).Value Then
                                pnlTaxFormula.Text = pnlTaxFormula.Text + "<html><b>" + grow.Cells(0).Value + "-</b> (Tax Amount for <b>" + gro.Cells(0).Value + "</b>) * (Tax Rate for <b>" + grow.Cells(0).Value + "</b>)/100<br></html>"
                                Exit For
                            End If
                        Next

                    End If
                Next
            Else
                For Each grow As GridViewRowInfo In gvTaxGroups.Rows
                    taxableTax = ""
                    For Each gro As GridViewRowInfo In gvTaxGroups.Rows
                        If gro.Index < grow.Index AndAlso gro.Cells(2).Value = "Yes" Then
                            taxableTax = taxableTax + " + (Tax Amount for <b>" + gro.Cells(0).Value + "</b>)"
                        End If
                    Next
                    If grow.Index = 0 Then
                        pnlTaxFormula.Text = pnlTaxFormula.Text + "<html><b>" + grow.Cells(0).Value + "-</b> (MRP * Abatement/100) * (Tax Rate for <b>" + grow.Cells(0).Value + "</b>)/100<br></html>"
                    ElseIf grow.Cells(3).Value = "No" Then
                        Dim lastFormula As String = "<html><b>" + grow.Cells(0).Value + "-</b> (Basic Amount) " + taxableTax + " * (Tax Rate for <b>" + grow.Cells(0).Value + "</b>)/100<br></html>"
                        If lastFormula.Length > 150 Then
                            Dim i As Integer = lastFormula.Length - 141
                            Dim str As String = lastFormula.Substring(0, 140)
                            Dim str1 As String = lastFormula.Substring(141, i)
                            lastFormula = str + "<br>" + str1
                        End If
                        pnlTaxFormula.Text = pnlTaxFormula.Text + lastFormula
                    Else
                        For Each gro As GridViewRowInfo In gvTaxGroups.Rows
                            If gro.Cells(0).Value = grow.Cells(4).Value Then
                                pnlTaxFormula.Text = pnlTaxFormula.Text + "<html><b>" + grow.Cells(0).Value + "-</b> (Tax Amount for <b>" + gro.Cells(0).Value + "</b>) * (Tax Rate for <b>" + grow.Cells(0).Value + "</b>)/100<br></html>"
                                Exit For
                            End If
                        Next

                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub gvTaxGroups_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gvTaxGroups.ValueChanged
        If TypeOf gvTaxGroups.CurrentColumn Is GridViewCheckBoxColumn Then
            Dim editor As RadCheckBoxEditor = DirectCast(sender, RadCheckBoxEditor)
            If Not editor Is Nothing Then
                gvTaxGroups.EndEdit()
            End If
        End If

    End Sub

    Private Sub chkExcisable_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkExcisable.ToggleStateChanged
        gvTaxGroups.DataSource = Nothing
        gvTaxGroups.Rows.Clear()
        pnlTaxFormula.Text = ""
        fillTaxAuthority()
    End Sub

    Private Sub chkVat_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVat.ToggleStateChanged
        If chkVat.ToggleState = ToggleState.On Then
            chkSale.ToggleState = ToggleState.Off
            fillTaxAuthority()

        End If
    End Sub

    Private Sub chkSale_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSale.ToggleStateChanged
        If chkSale.ToggleState = ToggleState.On Then
            chkVat.ToggleState = ToggleState.Off
        End If
    End Sub

    Private Function GetReplecateCompaniesDataBase() As List(Of String)
        Dim arrDBName As New List(Of String)
        arrDBName.Add(objCommonVar.CurrDatabase)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function

    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
    End Sub

    Private Sub MasterTemplate_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTaxGroups.CellDoubleClick
        If e.Column.Name = "taxable" Then
            If e.Row.Cells("taxable").Value = "No" Then
                e.Row.Cells("taxable").Value = "Yes"
                gvTaxGroups.EndEdit()
            Else
                e.Row.Cells("taxable").Value = "No"
            End If
        ElseIf e.Column.Name = "surtax" Then
            If e.Row.Cells("surtax").Value = "No" Then
                e.Row.Cells("surtax").Value = "Yes"
                gvTaxGroups.EndEdit()
            Else
                e.Row.Cells("surtax").Value = "No"
            End If
        End If
    End Sub

    Private Sub MasterTemplate_DefaultValuesNeeded(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvTaxGroups.DefaultValuesNeeded
        e.Row.Cells("taxable").Value = "No"
        e.Row.Cells("surtax").Value = "No"
    End Sub

    Private Sub findTaxGroup_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles findTaxGroup.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        ElseIf (e.KeyChar = Chr(9)) Or (e.KeyChar = Chr(13)) Then
            ddlTransType.Focus()
        End If

    End Sub
#End Region

#Region "Menu Events"

#Region "Import/Export"

    Private Sub menuExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuExport.Click
        sql = "SELECT TSPL_Tax_Group_Master.Tax_Group_Code AS 'Tax Group Code',TSPL_Tax_Group_Master.Tax_Group_Desc AS 'Description', " & _
              "(CASE WHEN TSPL_Tax_Group_Master.Tax_Group_Type = 'S' THEN 'Sales' ELSE 'Purchase' END) AS 'Tax Group Type',TSPL_TAX_GROUP_MASTER.Excisable ,TSPL_TAX_GROUP_MASTER .VAT ,TSPL_TAX_GROUP_MASTER .STax ," & _
              " TSPL_TAX_GROUP_DETAILS.Tax_Code AS 'Tax Authority', TSPL_TAX_GROUP_DETAILS.Tax_Code_Desc AS 'Authority Description'," & _
              " (CASE WHEN TSPL_TAX_GROUP_DETAILS.Taxable = 'N' THEN 'No' ELSE 'Yes' END) AS Taxable, " & _
              " (CASE WHEN TSPL_TAX_GROUP_DETAILS.Surtax = 'Y' THEN 'Yes' ELSE 'No' END) AS Surtax, " & _
              " TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code AS 'Surtax Code', TSPL_TAX_GROUP_DETAILS.Surtax_Tax_Code_Desc AS 'Surtax Description'"

        If objCommonVar.GSTApplicable Then
            sql += " ,TSPL_Tax_Group_Master.Active as 'Active'"
        End If

        sql += " FROM TSPL_Tax_Group_Master INNER JOIN TSPL_TAX_GROUP_DETAILS ON TSPL_Tax_Group_Master.Tax_Group_Code = TSPL_TAX_GROUP_DETAILS.Tax_Group_Code AND " & _
              "  TSPL_Tax_Group_Master.Tax_Group_Type = TSPL_TAX_GROUP_DETAILS.Tax_Group_Type"
        transportSql.ExporttoExcel(sql, Me)

    End Sub

    Private Sub menuImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuImport.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim input() As String = {}
        If objCommonVar.GSTApplicable Then
            input = {"Tax Group Code", "Description", "Tax Group Type", "Excisable", "VAT", "STax", "Tax Authority", "Authority Description", "Taxable", "Surtax", "Surtax Code", "Surtax Description", "Active"}
        Else
            input = {"Tax Group Code", "Description", "Tax Group Type", "Excisable", "VAT", "STax", "Tax Authority", "Authority Description", "Taxable", "Surtax", "Surtax Code", "Surtax Description"}
        End If
        Dim inputlist As List(Of String) = New List(Of String)(input)
        If transportSql.importExcel(gv, inputlist.ToArray()) Then
            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                Dim transCode As Integer
                Dim TaxGroupCode As String = ""
                Dim TaxGroupType As String = ""
                Dim Active As Integer = 0
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strTaxGrCode As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strDesc As String = clsCommon.myCstr(grow.Cells(1).Value)
                    Dim strTaxGrType As String = clsCommon.myCstr(grow.Cells(2).Value)

                    Dim strexcisable As String = clsCommon.myCstr(grow.Cells(3).Value)
                    Dim strvat As String = clsCommon.myCstr(grow.Cells(4).Value)
                    Dim strstax As String = clsCommon.myCstr(grow.Cells(5).Value)

                    Dim strTaxCode As String = clsCommon.myCstr(grow.Cells(6).Value)
                    Dim strTaxCodeDesc As String = clsCommon.myCstr(grow.Cells(7).Value)
                    Dim strTaxable As String = clsCommon.myCstr(grow.Cells(8).Value)
                    Dim strSurtax As String = clsCommon.myCstr(grow.Cells(9).Value)
                    Dim strSurtaxCode As String = clsCommon.myCstr(grow.Cells(10).Value)
                    Dim strSurtaxCodeDesc As String = clsCommon.myCstr(grow.Cells(11).Value)

                    If objCommonVar.GSTApplicable Then

                        Active = clsCommon.myCdbl(grow.Cells(12).Value)
                        If Active <> 0 And Active <> 1 Then
                            common.clsCommon.MyMessageBoxShow("Active can't be left blank")
                            trans.Rollback()
                            Me.Controls.Remove(gv)
                            Exit Sub
                        End If
                    End If

                    If String.IsNullOrEmpty(strexcisable) Or strexcisable.Length <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Excisable cannont be left blank")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strvat) Or strvat.Length <= 0 Then
                        common.clsCommon.MyMessageBoxShow("Tax cannont be left blank")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strstax) Or strstax.Length <= 0 Then
                        common.clsCommon.MyMessageBoxShow("STax cannont be left blank")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strTaxGrCode) Or strTaxGrCode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Tax Authority has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strDesc) Or strDesc.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Description has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If (strTaxGrType = "Sales") Then
                        strTaxGrType = "S"
                    ElseIf (strTaxGrType = "Purchase") Then
                        strTaxGrType = "P"
                    Else
                        common.clsCommon.MyMessageBoxShow("Taxable has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strTaxCode) Or strTaxCode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Tax Authority has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(strTaxCodeDesc) Or strTaxCodeDesc.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Authority Description has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If (strTaxable = "Yes" Or strTaxable = "True") Then
                        strTaxable = "Y"
                    ElseIf (strTaxable = "No" Or strTaxable = "False") Then
                        strTaxable = "N"
                    Else
                        common.clsCommon.MyMessageBoxShow("Taxable has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If (strSurtax = "Yes" Or strSurtax = "True") Then
                        strSurtax = "Y"
                    ElseIf (strSurtax = "No" Or strSurtax = "False") Then
                        strSurtax = "N"
                    Else
                        common.clsCommon.MyMessageBoxShow("Surtax has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strSurtaxCode.Length > 12 Then
                        common.clsCommon.MyMessageBoxShow("Surtax Code has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    If strSurtaxCodeDesc.Length > 50 Then
                        common.clsCommon.MyMessageBoxShow("Surtax Description has some incorrect values")
                        trans.Rollback()
                        Me.Controls.Remove(gv)
                        Exit Sub
                    End If
                    Dim qry As String
                    Try
                        If Not (clsCommon.CompairString(TaxGroupCode, strTaxGrCode) = CompairStringResult.Equal And clsCommon.CompairString(TaxGroupType, strTaxGrType) = CompairStringResult.Equal) Then
                            transCode = 1
                            qry = "delete from  TSPL_TAX_GROUP_DETAILS where Tax_Group_Code = '" & strTaxGrCode & "' AND Tax_Group_Type='" + strTaxGrType + "'"
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            qry = " Select COUNT(*) From TSPL_TAX_GROUP_MASTER Where Tax_Group_Code='" + strTaxGrCode + "' AND Tax_Group_Type='" + strTaxGrType + "' "
                            Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, trans))
                            If Count <= 0 Then
                                connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_GROUP_MASTER_INSERT", New SqlParameter("@Tax_Group_Code", strTaxGrCode), New SqlParameter("@Tax_Group_Desc", strDesc), New SqlParameter("@Tax_Group_Type", strTaxGrType), New SqlParameter("@Excise", strexcisable), New SqlParameter("@Vat", strvat), New SqlParameter("@STax", strstax), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                                'sanjay
                                Dim col1 As New Hashtable()
                                clsCommon.AddColumnsForChange(col1, "Active", Active)
                                clsCommonFunctionality.UpdateDataTable(col1, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "Tax_Group_Code='" + strTaxGrCode + "' AND Tax_Group_Type='" + strTaxGrType + "'", trans)
                                'sanjay
                            Else
                                connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_GROUP_MASTER_UPDATE", New SqlParameter("@Tax_Group_Code", strTaxGrCode), New SqlParameter("@Tax_Group_Desc", strDesc), New SqlParameter("@Excisable", strexcisable), New SqlParameter("@Tax_Group_Type", strTaxGrType), New SqlParameter("@Vat", strvat), New SqlParameter("@STax", strstax), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                                'sanjay
                                Dim col1 As New Hashtable()
                                clsCommon.AddColumnsForChange(col1, "Active", Active)
                                clsCommonFunctionality.UpdateDataTable(col1, "TSPL_TAX_GROUP_MASTER", OMInsertOrUpdate.Update, "Tax_Group_Code='" + strTaxGrCode + "' AND Tax_Group_Type='" + strTaxGrType + "'", trans)
                                'sanjay
                            End If
                        End If

                        connectSql.RunSpTransaction(trans, "sp_TSPL_TAX_GROUP_DETAIL_INSERT", New SqlParameter("@Trans_Code", transCode), New SqlParameter("@Tax_Group_Code", strTaxGrCode), New SqlParameter("@Tax_Group_Type", strTaxGrType), New SqlParameter("@Tax_Code", strTaxCode), New SqlParameter("@Tax_Code_Desc", strTaxCodeDesc), New SqlParameter("@Taxable", strTaxable), New SqlParameter("@Surtax", strSurtax), New SqlParameter("@Surtax_Tax_Code", strSurtaxCode), New SqlParameter("@Surtax_Tax_Code_Desc", strSurtaxCodeDesc), New SqlParameter("@Created_By", userCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modify_By", userCode), New SqlParameter("@Modify_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", companyCode))
                        transCode = transCode + 1
                        TaxGroupCode = strTaxGrCode
                        TaxGroupType = strTaxGrType
                    Catch ex As Exception
                        clsCommon.MyMessageBoxShow(ex.Message)
                    End Try

                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
                trans.Rollback()

            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

#End Region

    Private Sub menuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles menuClose.Click
        Me.Close()
    End Sub

#End Region

    Private Sub FrmTaxGroups_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            deletedata()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub

    Private Sub gvDB_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvDB.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtCurrencyCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCurrencyCode._MYValidating
        'Dim qry As String = "select CURRENCY_CODE AS Code, CURRENCY_NAME as Name from TSPL_CURRENCY_MASTER"
        'clsCurrencyMaster.getFinder
        txtCurrencyCode.Value = clsCurrencyMaster.getFinder("", txtCurrencyCode.Value, isButtonClicked) 'clsCommon.ShowSelectForm("CURRENCY_MASTER", qry, "Code", "", txtCurrencyCode.Value, "CURRENCY_CODE", isButtonClicked)
        fillTaxAuthority()

    End Sub
End Class
