Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.Data
Imports System.Text.RegularExpressions
Imports common
Imports Microsoft.Office.Interop
'Imports Microsoft.Office.Interop.Excel
Imports System.Runtime.InteropServices.Marshal
 
Imports System.IO
Imports System.Configuration
Imports System

Imports System.Collections.Generic
Imports System.ComponentModel

Imports Excel = Microsoft.Office.Interop.Excel





'' CREATED BY : SURAJ
''Start Date: 10-05-2011
'' End Date:10-05-2011
' update by Vipin on 07/06/2012 for Inserting the data in selected database
'Added BY abhishek as on 19/10/2012 4:52 Pm For Delete Row Event If Unit_Code Exist in Tspl_Shipment_Detail,Tspl_transfer_Detail then it should not delete.
' update by priti on 14/05/2013 to add a new column weight in unit grid

Public Class frmItemMaster
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim dr As SqlDataReader
    Dim ds As New DataSet()
    Dim btntooltip As ToolTip = New ToolTip()
    Dim QrySheet As String
    Dim QrySpcl As String
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Const colitemno As String = "itemCode"
    Dim FlagCheckUOM As Int32
    
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.itemMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave1.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave1.Visible = True Then
            mnimport.Enabled = True
            mnexport.Enabled = True
        Else
            mnimport.Enabled = False
            mnexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btndelete1.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        frmitemcode1.MyMaxLength = 50
        txtdesc1.MaxLength = 100
        txttech.MaxLength = 60

    End Sub
    Private Sub frmItemMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'globalFunc.mandatoryText(frmitemcode1.txtValue, fndstructurecode1.Value, fndpurchaseaccountset1.Value, fndsaleaccountset1.txtValue, fndunitofmeasure.txtValue, fndcategory.txtValue, txtpackseq, txtflavourseq, txtskuseq)
        ddlitemtype.Text = "Finished Goods"
        ds = connectSql.RunSQLReturnDS("select Unit_Code,Unit_Desc  from TSPL_UNIT_MASTER")
        Dim chkuomcode As GridViewMultiComboBoxColumn = TryCast(dgvuomdetail.Columns(0), GridViewMultiComboBoxColumn)
        chkuomcode.DataSource = ds.Tables(0)
        chkuomcode.DisplayMember = "Unit_Code"
        chkuomcode.ValueMember = "Unit_Code"
        Dim cmbstock As GridViewComboBoxColumn = TryCast(dgvuomdetail.Columns(3), GridViewComboBoxColumn)
        cmbstock.DataSource = New String() {"Yes", "No"}

        'Dim arr As New ArrayList()
        'arr.Add("Yes")
        'arr.Add("No")
        'cmbstock.DataSource = arr
        fungridclasscode()
        fndpurchaseaccountset1.MyReadOnly = True
        fndsaleaccountset1.MyReadOnly = True
        ' frmitemcode1.txtValue.ReadOnly = True
        'fndstructurecode1.txtValue.ReadOnly = True
        fndstructurecode1.BackColor = Color.White
        frmitemcode1.BackColor = Color.White
        fndsaleaccountset1.BackColor = Color.White
        fndpurchaseaccountset1.BackColor = Color.White
        fndunitofmeasure.MyReadOnly = True
        fndunitofmeasure.BackColor = Color.White
        AddHandler fndstructurecode1.TextChanged, AddressOf Text_Changed
        btndelete1.Enabled = False
        AddHandler frmitemcode1.TextChanged, AddressOf texchanged

        AddHandler fndcategory.TextChanged, AddressOf fndcategory_text_changed
        AddHandler fndcategory.KeyPress, AddressOf fndcategory_key_press
        AddHandler fndcategory.Leave, AddressOf fndcategory_leave
        'fndcategory.txtValue.CharacterCasing = CharacterCasing.Upper
        'fndcategory.txtValue.MaxLength = 12
        If fndstructurecode1.Value = "" Then
            dgvclass1.ReadOnly = True
        Else
            dgvclass1.ReadOnly = False
        End If
        fndchapterhead1.MyReadOnly = True
        funretrivestructurecode()
        txttolerence.Text = 0
        txtcost.Text = 0
        txtpackseq.Text = 0
        txtflavourseq.Text = 0
        txtskuseq.Text = 0
        ' Edited by Abhishek
        btntooltip.SetToolTip(btnsave1, "Press Alt+S for Save/Update Trasnaction")
        btntooltip.SetToolTip(btndelete1, "Press Alt+D Delete Trasnaction")
        btntooltip.SetToolTip(btnclose1, "Press Esc Close the Window")
        btntooltip.SetToolTip(btnreset1, "Press Alt+N Adding New Transaction")
        drpboxType.Items.Add("A")
        drpboxType.Items.Add("B")
        drpboxType.Items.Add("C")
        SetDataBaseGrid()
        'funreset()
    End Sub

    Public Sub fndcategory_text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            '? Dim strquery As String = "select category_code as [Item Category],category_name as [Description] from tspl_Item_category where category_code ='" + fndcategory.Value + "'"
            Dim strquery As String = "select category_code  from tspl_Item_category where category_code ='" + fndcategory.Value + "'"
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
            'Dim dr1 As SqlDataReader
            'Dim strvalue As String
            'dr1 = connectSql.RunSqlReturnDR(strquery)
            'While dr1.Read()
            '    strvalue = dr1(0).ToString()
            'End While
            If strvalue <> "" Then
                funfillcategory()
            Else
                txtcatdesc.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    'It will fill the  controls if value exist in database according to fndbranch
    Public Sub funfillcategory()
        Try

            'Dim strquery As String = "select category_code as [Item Category],category_name as [Description] from tspl_Item_category where category_code ='" + fndcategory.Value + "'"
            Dim strquery As String = "select category_name  from tspl_Item_category where category_code ='" + fndcategory.Value + "'"

            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
            txtcatdesc.Text = strvalue
            fndItemSubCategory.Value = ""
            txtSubcatdesc.Text = ""



            'Dim dr2 As SqlDataReader
            'Dim strvalue As String
            'dr2 = connectSql.RunSqlReturnDR(strquery)
            'While dr2.Read()
            '    txtcatdesc.Text = dr2(1).ToString()
            '    fndItemSubCategory.Value = ""
            '    txtSubcatdesc.Text = ""
            'End While
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    'Keypress validation on finder and converting lower case to upper case
    Public Sub fndcategory_key_press(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub
    'This will check the existence of value in databse on leave property of finder,if it exist then it will fill, otherwise it will blank the fields
    Public Sub fndcategory_leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fndcategory.Value = "" Then
        Else
            Try
                Dim strquery As String = "select category_code  from tspl_Item_category  where category_code ='" + fndcategory.Value + "'"
                Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)
                If strvalue <> "" Then
                Else : strquery = ""
                    txtcatdesc.Text = ""
                    common.clsCommon.MyMessageBoxShow(Me, "This Categoy does not exist in Master Table", Me.Text)
                    fndcategory.Value = ""
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub
    ''To Authorised the user 
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITEM-MASTER"
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
    '            'rdbtnSave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    '' To Retrieve default structure code in structure code finder
    Private Sub funretrivestructurecode()
        Dim strdefault As String = connectSql.RunScalar("select structure_code from TSPL_STRUCTURE_MASTER where default_struct='Y' ")
        If Not String.IsNullOrEmpty(strdefault) Then
            fndstructurecode1.Value = strdefault
        End If
    End Sub
    '' To fill all the value according to the Item Code
    Private Sub texchanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim str As String = "select item_code from tspl_item_master where item_code= '" + frmitemcode1.Value + "'"
        '  dr = connectSql.RunSqlReturnDR(str)
        Dim stritemcode As String = clsDBFuncationality.getSingleValue(str)
        ' While dr.Read()
        'stritemcode = dr(0).ToString()
        ' End While
        If stritemcode <> "" Then
            funfill()
            ' fndstructurecode1.Enabled = False
            dgvclass1.ReadOnly = True
        End If
    End Sub

    '' To fill class code according to the class name in datagridview 
    Private Sub fungridclasscode()
        Try
            If fndstructurecode1.Value <> "" Then
                Dim str As String = "select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name  = '" + dgvclass1.CurrentRow.Cells(0).Value + "'"
                Dim dt As GridViewComboBoxColumn = TryCast(dgvclass1.Columns(1), GridViewComboBoxColumn)
                dt.DataSource = clsDBFuncationality.GetDataTable(str)
                dt.ValueMember = "inv_class_code"
                dt.DisplayMember = "inv_class_code"
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString())
        End Try
    End Sub
    ''To insert The data into the table(TSPL_ITEM_MASTER) and table(TSPL_ITEM_DETAILS)
    Private Sub funinsert()
        Dim str As String = ""
        For i As Integer = 0 To dgvclass1.Rows.Count - 1
            str = str + dgvclass1.Rows(i).Cells(1).Value
        Next
        Dim STR2 As String = "SELECT INV_CLASS_NAME FROM TSPL_INV_CLASS WHERE Class_Type = 'Pack Type'"
        'dr = connectSql.RunSqlReturnDR(STR2)
        Dim strpacktype As String = clsDBFuncationality.getSingleValue(STR2)
        Dim strpackcode As String = ""
        Dim i2 As Integer
        'If dr.HasRows Then
        '    While dr.Read()
        '        strpacktype = dr(0).ToString()
        '    End While
        'End If

        For i As Integer = 0 To dgvclass1.Rows.Count - 1
            If strpacktype = dgvclass1.Rows(i).Cells(0).Value Then
                strpackcode = dgvclass1.Rows(i).Cells(1).Value
                i2 = i
                Exit For
            End If
        Next
        Dim strfathermothercode As String = "select Finished_Goods, mother_code, father_code from TSPL_PACKTYPE_MASTER where Class_Type = '" + strpacktype + "' and Finished_Goods = '" + strpackcode + "'"
        'dr = connectSql.RunSqlReturnDR(strfathermothercode)
        'Dim strfathercode As String
        'Dim strmothercode As String
        'Dim strfinishedgood As String
        'If dr.HasRows Then
        '    While dr.Read()
        '        strfinishedgood = dr(0).ToString()
        '        strfathercode = dr(2).ToString()
        '        strmothercode = dr(1).ToString()
        '    End While
        'End If
        Dim dt As DataTable
        Dim strfathercode As String = ""
        Dim strmothercode As String = ""
        Dim strfinishedgood As String = ""
        
        dt = clsDBFuncationality.GetDataTable(strfathermothercode)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                strfinishedgood = dt.Rows(i)("Finished_Goods").ToString()
                strfathercode = dt.Rows(i)("father_code")
                strmothercode = dt.Rows(i)("mother_code")
            Next
        End If





        Dim strgeneratefathercode As String = ""
        Dim strgeneratemothercode As String = ""
        'If strfathercode = "NIL" Then
        '    strfathercode = ""
        'End If
        If strfathercode <> "" And strfathercode <> "NIL" And strfinishedgood <> "" Then
            Dim value As String

            For i As Integer = 0 To dgvclass1.Rows.Count - 1
                If dgvclass1.Rows(i).Cells(1).Value = strfinishedgood Then
                    value = strfathercode
                Else
                    value = dgvclass1.Rows(i).Cells(1).Value
                End If
                ' strgeneratefathercode = strgeneratefathercode + dgvclass1.Rows(i).Cells(1).Value
                strgeneratefathercode = strgeneratefathercode + value

            Next
        Else
            strgeneratefathercode = "NIL"
        End If
        If strmothercode <> "NIL" And strmothercode <> "" And strfinishedgood <> "" Then

            For i As Integer = 0 To dgvclass1.Rows.Count - 1
                Dim value As String
                If dgvclass1.Rows(i).Cells(1).Value = strfinishedgood Then
                    value = strmothercode
                Else
                    value = dgvclass1.Rows(i).Cells(1).Value
                End If
                strgeneratemothercode = strgeneratemothercode + value
                'strgeneratemothercode = ""
            Next
            'dgvclass1.Rows(i2).Cells(1).Value = strfinishedgood
        Else
            strgeneratemothercode = "NIL"


        End If
        Dim trans As SqlTransaction = Nothing
        Try
            'connectSql.OpenConnection()
            trans = clsDBFuncationality.GetTransactin()

            If strgeneratefathercode = "" Then
                strgeneratefathercode = "NIL"
            End If
            If strgeneratemothercode = "" Then
                strgeneratemothercode = "NIL"
            End If
            Dim strp2 As String
            Dim strp3 As String
            Dim createddate As Date = Date.Today
            Dim modifydate As Date = Date.Today
            If chkp2count.Checked = True Then
                strp2 = "Y"
            Else
                strp2 = "N"
            End If
            If chkp2count.Checked = True Then
                strp3 = "Y"
            Else
                strp3 = "N"
            End If
            If txtfatherqty1.Text = "" Then
                txtfatherqty1.Text = "0"
            End If
            If txtmotherqty1.Text = "" Then
                txtmotherqty1.Text = "0"
            End If
            'If txtopnbalance.Text = "" Then
            '    txtopnbalance.Text = "0"
            'End If
            'If txtdefautltprice1.Text = "" Then
            '    txtdefautltprice1.Text = "0"
            'End If
            Dim stritemtype As String = ""
            If ddlitemtype.Text = "Finished Goods" Then
                stritemtype = "F"
            ElseIf ddlitemtype.Text = "Promotional Item" Then
                stritemtype = "P"
            ElseIf ddlitemtype.Text = "Raw Material" Then
                stritemtype = "R"
            ElseIf ddlitemtype.Text = "Other" Then

                stritemtype = "0"
            ElseIf ddlitemtype.Text = "Trading Item" Then
                stritemtype = "T"

            End If
            Dim strshow As String
            If chkshow.Checked = True Then
                strshow = "Y"
            Else
                strshow = "N"
            End If

            Dim strNoMRP As String
            If chkNoMRP.Checked = True Then
                strNoMRP = "1"
            Else
                strNoMRP = "0"
            End If

            Dim Morning As Integer
            If chkMorning.Checked = True Then
                Morning = 1
            Else
                Morning = 0
            End If

            Dim arrDBName As New List(Of String)
            arrDBName.Add(objCommonVar.CurrDatabase)
            For ii As Integer = 0 To gddatabase.Rows.Count - 1
                If (clsCommon.myCBool(gddatabase.Rows(ii).Cells(colSelect).Value)) Then
                    arrDBName.Add(clsCommon.myCstr(gddatabase.Rows(ii).Cells(colDataBaseName).Value))
                End If
            Next



            'connectSql.RunSpTransaction(trans, "sp_TSPL_ITEM_MASTER_insert", New SqlParameter("@itemcode", str), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.txtValue.Text), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.txtValue.Text), New SqlParameter("@salesclasscode", fndsaleaccountset1.txtValue.Text), New SqlParameter("@unitcode", fndunitofmeasure.txtValue.Text), New SqlParameter("@defaultprice", txtdefautltprice1.Text), New SqlParameter("@fathercode", strgeneratefathercode), New SqlParameter("@fatherqty", txtfatherqty1.Text), New SqlParameter("@chapterhead", fndchapterhead1.txtValue.Text), New SqlParameter("@mothercode", strgeneratemothercode), New SqlParameter("@motherqty", txtmotherqty1.Text), New SqlParameter("@openingbal", txtopnbalance.Text), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
            'clsDBFuncationality.UpdateInAllDatabase(trans, "sp_TSPL_ITEM_MASTER_insert", New SqlParameter("@itemcode", str), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", "0"), New SqlParameter("@fathercode", strgeneratefathercode), New SqlParameter("@fatherqty", txtfatherqty1.Text), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", strgeneratemothercode), New SqlParameter("@motherqty", txtmotherqty1.Text), New SqlParameter("@openingbal", "0"), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning))

            If clsCommon.myLen(fndProdItemCategory.Value) > 0 Then
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_MASTER_insert", New SqlParameter("@itemcode", str), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", "0"), New SqlParameter("@fathercode", strgeneratefathercode), New SqlParameter("@fatherqty", txtfatherqty1.Text), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", strgeneratemothercode), New SqlParameter("@motherqty", txtmotherqty1.Text), New SqlParameter("@openingbal", "0"), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning), New SqlParameter("@Prod_item_category", fndProdItemCategory.Value))
            Else
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_MASTER_insert", New SqlParameter("@itemcode", str), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", "0"), New SqlParameter("@fathercode", strgeneratefathercode), New SqlParameter("@fatherqty", txtfatherqty1.Text), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", strgeneratemothercode), New SqlParameter("@motherqty", txtmotherqty1.Text), New SqlParameter("@openingbal", "0"), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning))
            End If









            'Dim qry As String = "update tspl_item_master set item_category='" + fndcategory.txtValue.Text + "'"
            'connectSql.RunSql(qry)
            For j As Integer = 0 To dgvclass1.Rows.Count - 1
                'connectSql.RunSpTransaction(trans, "sp_TSPL_ITEM_DETAILS_insert", New SqlParameter("itemcode", str), New SqlParameter("@classcode", dgvclass1.Rows(j).Cells(1).Value), New SqlParameter("@classname", dgvclass1.Rows(j).Cells(0).Value), New SqlParameter("@classdesc", dgvclass1.Rows(j).Cells(2).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))
                'clsDBFuncationality.UpdateInAllDatabase(trans, "sp_TSPL_ITEM_DETAILS_insert", New SqlParameter("itemcode", str), New SqlParameter("@classcode", dgvclass1.Rows(j).Cells(1).Value), New SqlParameter("@classname", dgvclass1.Rows(j).Cells(0).Value), New SqlParameter("@classdesc", dgvclass1.Rows(j).Cells(2).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode))

                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_DETAILS_insert", New SqlParameter("itemcode", str), New SqlParameter("@classcode", dgvclass1.Rows(j).Cells(1).Value), New SqlParameter("@classname", dgvclass1.Rows(j).Cells(0).Value), New SqlParameter("@classdesc", dgvclass1.Rows(j).Cells(2).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))


            Next
            For k As Integer = 0 To dgvuomdetail.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvuomdetail.Rows(k).Cells(0).Value) Then
                    Dim strstockunit As String = ""
                    If Not String.IsNullOrEmpty(dgvuomdetail.Rows(k).Cells(3).Value) Then
                        If dgvuomdetail.Rows(k).Cells(3).Value = "Yes" Then
                            strstockunit = "Y"
                        Else
                            strstockunit = "N"
                        End If
                    End If
                    'clsDBFuncationality.UpdateInAllDatabase(trans, "SP_TSPL_ITEM_UOM_DETAIL_INSERT", New SqlParameter("@itemcode", str), New SqlParameter("@uomcode", dgvuomdetail.Rows(k).Cells(0).Value), New SqlParameter("@uomdesc", dgvuomdetail.Rows(k).Cells(1).Value), New SqlParameter("@conversionfactor", dgvuomdetail.Rows(k).Cells(2).Value), New SqlParameter("@stockingunit", strstockunit))
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_ITEM_UOM_DETAIL_INSERT", New SqlParameter("@itemcode", str), New SqlParameter("@uomcode", dgvuomdetail.Rows(k).Cells(0).Value), New SqlParameter("@uomdesc", dgvuomdetail.Rows(k).Cells(1).Value), New SqlParameter("@conversionfactor", dgvuomdetail.Rows(k).Cells(2).Value), New SqlParameter("@stockingunit", strstockunit), New SqlParameter("@Weight", dgvuomdetail.Rows(k).Cells(4).Value))


                End If
            Next

            txtfathercode1.Text = strgeneratefathercode
            txtmothercode1.Text = strgeneratemothercode
            trans.Commit()
            frmitemcode1.Value = str
            myMessages.insert()

        Catch ex As Exception
            If (ex.Message.Contains("Violation of PRIMARY KEY constraint")) Then
                common.clsCommon.MyMessageBoxShow(Me, "Record already Exist", Me.Text)
                myMessages.myExceptions(ex)
                trans.Rollback()

                funreset()
            Else
                myMessages.myExceptions(ex)
                trans.Rollback()

                funreset()
            End If


        End Try

    End Sub


    Sub SetDataBaseGrid()
        gddatabase.Rows.Clear()
        gddatabase.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gddatabase.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gddatabase.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gddatabase.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gddatabase.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 and Comp_Code not in ('" + objCommonVar.CurrentCompanyCode + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gddatabase.Rows.AddNew()
                gddatabase.Rows(gddatabase.Rows.Count - 1).Cells(colSelect).Value = True
                gddatabase.Rows(gddatabase.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gddatabase.Rows(gddatabase.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gddatabase.Rows(gddatabase.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If




    End Sub
    '' To delte the data from the table(TSPL_ITEM_MASTER) and table(TSPL_ITEM_DETAILS)
    Private Sub fundelete()
        Dim trans As SqlTransaction = Nothing
        Try
           
            trans = clsDBFuncationality.GetTransactin()
            Dim qst As String
            Dim dpt As String

            qst = "select Item_Code from TSPL_ADJUSTMENT_DETAIL where Item_Code='" + frmitemcode1.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                trans.Rollback()
                Return
            End If
            qst = "select Item_Code from TSPL_SCRAPINVOICE_DETAIL where Item_Code='" + frmitemcode1.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                trans.Rollback()
                Return
            End If
            qst = "select Item_Code from TSPL_SRN_DETAIL where Item_Code='" + frmitemcode1.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                trans.Rollback()
                Return
            End If
            qst = "select Item_Code from TSPL_PR_DETAIL where Item_Code='" + frmitemcode1.Value + "'"
            dpt = clsDBFuncationality.getSingleValue(qst, trans)
            If clsCommon.myLen(dpt) > 0 AndAlso dpt IsNot Nothing Then
                common.clsCommon.MyMessageBoxShow(Me, "This Record Cannot be deleted." + Environment.NewLine + "This is already in Process")
                trans.Rollback()
                Return
            End If
            'connectSql.RunSp("SP_TSPL_ITEM_DETAILS_DELETE", New SqlParameter("@itemcode", frmitemcode1.txtValue.Text))
            'connectSql.RunSp("sp_TSPL_ITEM_MASTER_delete", New SqlParameter("@itemcode", frmitemcode1.txtValue.Text))

            ''clsDBFuncationality.UpdateInAllDatabase(trans, "SP_TSPL_ITEM_DETAILS_DELETE", New SqlParameter("@itemcode", frmitemcode1.Value))
            ''clsDBFuncationality.UpdateInAllDatabase(trans, "SP_TSPL_ITEM_UOM_DETAIL_DELETE", New SqlParameter("@itemcode", frmitemcode1.Value))
            ''clsDBFuncationality.UpdateInAllDatabase(trans, "sp_TSPL_ITEM_MASTER_delete", New SqlParameter("@itemcode", frmitemcode1.Value))


            Dim arrDBName As New List(Of String)
            arrDBName.Add(objCommonVar.CurrDatabase)
            For ii As Integer = 0 To gddatabase.Rows.Count - 1
                If (clsCommon.myCBool(gddatabase.Rows(ii).Cells(colSelect).Value)) Then
                    arrDBName.Add(clsCommon.myCstr(gddatabase.Rows(ii).Cells(colDataBaseName).Value))
                End If
            Next



            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_ITEM_DETAILS_DELETE", New SqlParameter("@itemcode", frmitemcode1.Value))
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_ITEM_UOM_DETAIL_DELETE", New SqlParameter("@itemcode", frmitemcode1.Value))
            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_MASTER_delete", New SqlParameter("@itemcode", frmitemcode1.Value))


            trans.Commit()
            myMessages.delete()

            
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()

        End Try
         
    End Sub
    '' To update the data from the table (TSPL_ITEM_MASTER) and table(TSPL_ITEM_DETAILS) according the item 
    Private Sub funupdate()

        Dim trans As SqlTransaction = Nothing
        Try
            Dim strp2 As String
            Dim strp3 As String
            Dim createddate As Date = Date.Today
            Dim modifydate As Date = Date.Today
            If chkp2count.Checked = True Then
                strp2 = "Y"
            Else
                strp2 = "N"
            End If
            If chkp3count.Checked = True Then
                strp3 = "Y"
            Else
                strp3 = "N"
            End If
            Dim stritemtype As String = ""
            If ddlitemtype.Text = "Finished Goods" Then
                stritemtype = "F"
            ElseIf ddlitemtype.Text = "Promotional Item" Then
                stritemtype = "P"
            ElseIf ddlitemtype.Text = "Raw Material" Then
                stritemtype = "R"
            ElseIf ddlitemtype.Text = "Other" Then
                stritemtype = "O"
            ElseIf ddlitemtype.Text = "Trading Item" Then
                stritemtype = "T"


            End If
            Dim strshow As String
            If chkshow.Checked = True Then
                strshow = "Y"
            Else
                strshow = "N"
            End If

            Dim strNoMRP As String
            If chkNoMRP.Checked = True Then
                strNoMRP = "1"
            Else
                strNoMRP = "0"
            End If
            ''''Added By Pankaj Kumar---on--12/03/2012
            Dim Morning As Integer
            If chkMorning.Checked = True Then
                Morning = 1
            Else
                Morning = 0
            End If
            ''''Code Ends Here

            'connectSql.OpenConnection()
            Dim decFtherQty As Decimal
            Dim decMtherQty As Decimal
            If Me.txtfatherqty1.Text = 0.0 Then
                decFtherQty = 0
            Else
                decFtherQty = Me.txtfatherqty1.Text
            End If
            If Me.txtmotherqty1.Text = 0.0 Then
                decMtherQty = 0
            Else
                decMtherQty = Me.txtmotherqty.Text
            End If

            trans = clsDBFuncationality.GetTransactin() ' connectSql.OpenConnection.BeginTransaction()
            'connectSql.RunSpTransaction(trans, "sp_TSPL_ITEM_MASTER_update", New SqlParameter("@itemcode", frmitemcode1.txtValue.Text), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.txtValue.Text), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.txtValue.Text), New SqlParameter("@salesclasscode", fndsaleaccountset1.txtValue.Text), New SqlParameter("@unitcode", fndunitofmeasure.txtValue.Text), New SqlParameter("@defaultprice", txtdefautltprice1.Text), New SqlParameter("@fathercode", txtfathercode1.Text), New SqlParameter("@fatherqty", txtfatherqty1.Text), New SqlParameter("@chapterhead", fndchapterhead1.txtValue.Text), New SqlParameter("@mothercode", txtmothercode1.Text), New SqlParameter("@motherqty", txtmotherqty1.Text), New SqlParameter("@openingbal", txtopnbalance.Text), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", createddate), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", modifydate), New SqlParameter("@compcode", companyCode))
            'connectSql.RunSpTransaction(trans, "sp_TSPL_ITEM_DETAILS_delete", New SqlParameter("@itemcode", frmitemcode1.txtValue.Text))
            'For i As Integer = 0 To dgvclass1.Rows.Count - 1
            '    connectSql.RunSpTransaction(trans, "sp_TSPL_ITEM_DETAILS_insert", New SqlParameter("itemcode", frmitemcode1.txtValue.Text), New SqlParameter("@classcode", dgvclass1.Rows(i).Cells(1).Value), New SqlParameter("@classname", dgvclass1.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclass1.Rows(i).Cells(2).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", createddate), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", modifydate), New SqlParameter("@compcode", companyCode))
            'Next

            '--------------By Vipin For Selected database update on 06/06/2012---------------------------------------------------------

            ''clsDBFuncationality.UpdateInAllDatabase(trans, "sp_TSPL_ITEM_MASTER_update", New SqlParameter("@itemcode", frmitemcode1.Value), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", 0), New SqlParameter("@fathercode", txtfathercode1.Text), New SqlParameter("@fatherqty", decFtherQty), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", txtmothercode1.Text), New SqlParameter("@motherqty", decMtherQty), New SqlParameter("@openingbal", 0), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate()), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate()), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning))
            ''clsDBFuncationality.UpdateInAllDatabase(trans, "sp_TSPL_ITEM_DETAILS_delete", New SqlParameter("@itemcode", frmitemcode1.Value))

            Dim arrDBName As New List(Of String)
            arrDBName.Add(objCommonVar.CurrDatabase)
            For ii As Integer = 0 To gddatabase.Rows.Count - 1
                If (clsCommon.myCBool(gddatabase.Rows(ii).Cells(colSelect).Value)) Then
                    arrDBName.Add(clsCommon.myCstr(gddatabase.Rows(ii).Cells(colDataBaseName).Value))
                End If
            Next


            'clsDBFuncationality.SaveAStorePorcedure(trans, arrDBName, "sp_TSPL_ITEM_MASTER_update", New SqlParameter("@itemcode", frmitemcode1.Value), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", 0), New SqlParameter("@fathercode", txtfathercode1.Text), New SqlParameter("@fatherqty", decFtherQty), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", txtmothercode1.Text), New SqlParameter("@motherqty", decMtherQty), New SqlParameter("@openingbal", 0), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning))
            If clsCommon.myLen(fndProdItemCategory.Value) > 0 Then
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_MASTER_update", New SqlParameter("@itemcode", frmitemcode1.Value), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", 0), New SqlParameter("@fathercode", txtfathercode1.Text), New SqlParameter("@fatherqty", decFtherQty), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", txtmothercode1.Text), New SqlParameter("@motherqty", decMtherQty), New SqlParameter("@openingbal", 0), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning), New SqlParameter("@Prod_item_category", fndProdItemCategory.Value))
            Else
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_MASTER_update", New SqlParameter("@itemcode", frmitemcode1.Value), New SqlParameter("@itemdesc", txtdesc1.Text), New SqlParameter("@structurecode", fndstructurecode1.Value), New SqlParameter("@structuredesc", txtstrcturedesc.Text), New SqlParameter("@purchaseclasscode", fndpurchaseaccountset1.Value), New SqlParameter("@salesclasscode", fndsaleaccountset1.Value), New SqlParameter("@unitcode", fndunitofmeasure.Value), New SqlParameter("@defaultprice", 0), New SqlParameter("@fathercode", txtfathercode1.Text), New SqlParameter("@fatherqty", decFtherQty), New SqlParameter("@chapterhead", fndchapterhead1.Value), New SqlParameter("@mothercode", txtmothercode1.Text), New SqlParameter("@motherqty", decMtherQty), New SqlParameter("@openingbal", 0), New SqlParameter("@itemtype", stritemtype), New SqlParameter("@twocountstatus", strp2), New SqlParameter("@threecountstatus", strp3), New SqlParameter("@servetype", ddlservertype.Text), New SqlParameter("@flavourseq", txtflavourseq.Text), New SqlParameter("@packseq", txtpackseq.Text), New SqlParameter("@skuseq", txtskuseq.Text), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode), New SqlParameter("@show", strshow), New SqlParameter("@item_category", fndcategory.Value), New SqlParameter("@tolerence", txttolerence.Text), New SqlParameter("@tech_shelf_life", txttech.Text), New SqlParameter("@cost", txtcost.Text), New SqlParameter("@Sub_item_category", fndItemSubCategory.Value), New SqlParameter("@typeofitm", drpboxType.Text), New SqlParameter("@NOMRP", strNoMRP), New SqlParameter("@Morning", Morning))
            End If

            clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_DETAILS_delete", New SqlParameter("@itemcode", frmitemcode1.Value))



            For i As Integer = 0 To dgvclass1.Rows.Count - 1
                'clsDBFuncationality.UpdateInAllDatabase(trans, "sp_TSPL_ITEM_DETAILS_insert", New SqlParameter("itemcode", frmitemcode1.Value), New SqlParameter("@classcode", dgvclass1.Rows(i).Cells(1).Value), New SqlParameter("@classname", dgvclass1.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclass1.Rows(i).Cells(2).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", createddate), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", modifydate), New SqlParameter("@compcode", companyCode))
                clsDBFuncationality.SaveAStorePorcedure(trans, "sp_TSPL_ITEM_DETAILS_insert", New SqlParameter("itemcode", frmitemcode1.Value), New SqlParameter("@classcode", dgvclass1.Rows(i).Cells(1).Value), New SqlParameter("@classname", dgvclass1.Rows(i).Cells(0).Value), New SqlParameter("@classdesc", dgvclass1.Rows(i).Cells(2).Value), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", createddate), New SqlParameter("@modifyby", userCode), New SqlParameter("@modifydate", modifydate), New SqlParameter("@compcode", companyCode))

            Next
            'clsDBFuncationality.UpdateInAllDatabase(trans, "SP_TSPL_ITEM_UOM_DETAIL_DELETE", New SqlParameter("@itemcode", frmitemcode1.Value))
            clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_ITEM_UOM_DETAIL_DELETE", New SqlParameter("@itemcode", frmitemcode1.Value))

            For k As Integer = 0 To dgvuomdetail.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvuomdetail.Rows(k).Cells(0).Value) Then
                    Dim strstockunit As String = ""
                    If Not String.IsNullOrEmpty(dgvuomdetail.Rows(k).Cells(3).Value.ToString()) Then
                        If dgvuomdetail.Rows(k).Cells(3).Value = "Yes" Then
                            strstockunit = "Y"
                        Else
                            strstockunit = "N"
                        End If
                    End If
                    'clsDBFuncationality.UpdateInAllDatabase(trans, "SP_TSPL_ITEM_UOM_DETAIL_INSERT", New SqlParameter("@itemcode", frmitemcode1.Value), New SqlParameter("@uomcode", dgvuomdetail.Rows(k).Cells(0).Value), New SqlParameter("@uomdesc", dgvuomdetail.Rows(k).Cells(1).Value), New SqlParameter("@conversionfactor", dgvuomdetail.Rows(k).Cells(2).Value), New SqlParameter("@stockingunit", strstockunit))
                    clsDBFuncationality.SaveAStorePorcedure(trans, "SP_TSPL_ITEM_UOM_DETAIL_INSERT", New SqlParameter("@itemcode", frmitemcode1.Value), New SqlParameter("@uomcode", dgvuomdetail.Rows(k).Cells(0).Value), New SqlParameter("@uomdesc", dgvuomdetail.Rows(k).Cells(1).Value), New SqlParameter("@conversionfactor", dgvuomdetail.Rows(k).Cells(2).Value), New SqlParameter("@stockingunit", strstockunit), New SqlParameter("@Weight", dgvuomdetail.Rows(k).Cells(4).Value))

                End If
            Next
            trans.Commit()
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
            trans.Rollback()
        End Try
    End Sub
    ''To fill all the field according to the item
    Private Sub funfill()
        Try
            Dim strTSPL_ITEM_MASTER As String = "select item_desc, Structure_Code, Structure_Desc, Purchase_Class_Code, Sale_Class_Code , Unit_Code,Deafult_Price, Father_Code, Father_QTy, Cheapter_Heads, Mother_Code, Mother_Qty , Opening_Balance, Two_Count_Status, Three_Count_Status, Server_Type, item_type,  convert(numeric,flavour_seq ) as  flavour_seq,  convert(numeric,pack_seq) as pack_seq,  sku_seq, show,item_category,tolerence,tech_shelf_life,cost, Sub_item_category ,typeofitm,NOMRP, Morning ,T1.PROD_ITEM_CATEGORY_CODE,T2.PROD_ITEM_CATEGORY_NAME from TSPL_ITEM_MASTER T1 LEFT JOIN TSPL_MF_PRODUCTION_ITEM_CATEGORY T2 ON T1.PROD_ITEM_CATEGORY_CODE=T2.PROD_ITEM_CATEGORY_CODE where Item_Code ='" + frmitemcode1.Value + "' and item_type <> 'R'and item_type <> 'O'"


            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strTSPL_ITEM_MASTER)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    txtdesc1.Text = dt.Rows(i)("item_desc").ToString()
                    fndstructurecode1.Value = dt.Rows(i)("Structure_Code").ToString()
                    txtstrcturedesc.Text = dt.Rows(i)("Structure_Desc").ToString()
                    fndpurchaseaccountset1.Value = dt.Rows(i)("Purchase_Class_Code").ToString()
                    fndsaleaccountset1.Value = dt.Rows(i)("Sale_Class_Code").ToString()
                    fndunitofmeasure.Value = dt.Rows(i)("Unit_Code").ToString()
                    ''txtdefautltprice1.Text = dr(6).ToString()
                    txtfathercode1.Text = dt.Rows(i)("Father_Code").ToString()
                    txtfatherqty1.Text = dt.Rows(i)("Father_QTy").ToString()
                    fndchapterhead1.Value = dt.Rows(i)("Cheapter_Heads").ToString()
                    txtmothercode1.Text = dt.Rows(i)("Mother_Code").ToString()
                    txtmotherqty1.Text = dt.Rows(i)("Mother_Qty").ToString()
                    ''  txtopnbalance.Text = dr(12).ToString()
                    Dim strtwo As String = dt.Rows(i)("Two_Count_Status").ToString().Trim()
                    If strtwo = "Y" Then
                        chkp2count.Checked = True
                    Else : strtwo = "N"
                        chkp2count.Checked = False
                    End If
                    Dim strthree As String = dt.Rows(i)("Three_Count_Status").ToString().Trim()
                    If strthree = "Y" Then
                        chkp3count.Checked = True
                    Else : strthree = "N"
                        chkp3count.Checked = False
                    End If
                    ddlservertype.Text = dt.Rows(i)("Server_Type").ToString()
                    Dim stritemtype As String = dt.Rows(i)("item_type").ToString().Trim()
                    If stritemtype = "F" Then
                        ddlitemtype.Text = "Finished Goods"
                    ElseIf stritemtype = "P" Then
                        ddlitemtype.Text = "Promotional Item"
                        'ElseIf stritemtype = "R" Then
                        '    ddlitemtype.Text = "Raw Material"
                        'ElseIf stritemtype = "O" Then
                        '    ddlitemtype.Text = "Other"
                    ElseIf stritemtype = "T" Then
                        ddlitemtype.Text = "Trading Item"
                    End If
                    txtflavourseq.Text = dt.Rows(i)("flavour_seq").ToString()
                    txtpackseq.Text = dt.Rows(i)("pack_seq").ToString()
                    txtskuseq.Text = dt.Rows(i)("sku_seq").ToString()
                    Dim strshow As String
                    If Not String.IsNullOrEmpty(dt.Rows(i)("show").ToString()) Then
                        strshow = dt.Rows(i)("show")
                        If strshow.Trim() = "Y" Then
                            chkshow.Checked = True
                        Else
                            chkshow.Checked = False
                        End If
                    Else
                        chkshow.Checked = False
                    End If
                    fndcategory.Value = dt.Rows(i)("item_category").ToString()
                    txttolerence.Text = dt.Rows(i)("tolerence").ToString()
                    txttech.Text = dt.Rows(i)("tech_shelf_life").ToString()
                    txtcost.Text = dt.Rows(i)("cost").ToString()
                    fndItemSubCategory.Value = dt.Rows(i)("Sub_item_category").ToString()
                    fndProdItemCategory.Value = dt.Rows(i)("PROD_ITEM_CATEGORY_CODE").ToString()
                    txtProdItemCategoryName.Text = dt.Rows(i)("PROD_ITEM_CATEGORY_NAME").ToString()

                    drpboxType.Text = ""
                    drpboxType.Text = dt.Rows(i)("typeofitm").ToString()
                    Dim strNoMRP As String = dt.Rows(i)("NOMRP").ToString()
                    If strNoMRP = "1" Then
                        chkNoMRP.Checked = True
                    Else : strNoMRP = "0"
                        chkNoMRP.Checked = False
                    End If
                    ''''Added by---Pankaj Kumar----on--12/03/2012
                    If dt.Rows(i)("Morning").ToString() = "1" Then
                        chkMorning.Checked = True
                    Else
                        chkMorning.Checked = False
                    End If
                    ''''Code Ends Here
                    funfillcategory()
                    fndItemSubCategory.Value = dt.Rows(i)("Sub_item_category").ToString()

                    fillSub_Item_Category_Desc() ''''Fills The Description of 'Sub_Item_Category'


                Next
            End If





            ''dr = connectSql.RunSqlReturnDR(strTSPL_ITEM_MASTER)
            ''If dr.HasRows Then
            ''    While dr.Read()
            ''        txtdesc1.Text = dr(0).ToString()
            ''        fndstructurecode1.Value = dr(1).ToString()
            ''        txtstrcturedesc.Text = dr(2).ToString()
            ''        fndpurchaseaccountset1.Value = dr(3).ToString()
            ''        fndsaleaccountset1.Value = dr(4).ToString()
            ''        fndunitofmeasure.Value = dr(5).ToString()
            ''        ''txtdefautltprice1.Text = dr(6).ToString()
            ''        txtfathercode1.Text = dr(7).ToString()
            ''        txtfatherqty1.Text = dr(8).ToString()
            ''        fndchapterhead1.Value = dr(9).ToString()
            ''        txtmothercode1.Text = dr(10).ToString()
            ''        txtmotherqty1.Text = dr(11).ToString()
            ''        ''  txtopnbalance.Text = dr(12).ToString()
            ''        Dim strtwo As String = dr(13).ToString().Trim()
            ''        If strtwo = "Y" Then
            ''            chkp2count.Checked = True
            ''        Else : strtwo = "N"
            ''            chkp2count.Checked = False
            ''        End If
            ''        Dim strthree As String = dr(14).ToString().Trim()
            ''        If strthree = "Y" Then
            ''            chkp3count.Checked = True
            ''        Else : strthree = "N"
            ''            chkp3count.Checked = False
            ''        End If
            ''        ddlservertype.Text = dr(15).ToString()
            ''        Dim stritemtype As String = dr(16).ToString().Trim()
            ''        If stritemtype = "F" Then
            ''            ddlitemtype.Text = "Finished Goods"
            ''        ElseIf stritemtype = "P" Then
            ''            ddlitemtype.Text = "Promotional Item"
            ''            'ElseIf stritemtype = "R" Then
            ''            '    ddlitemtype.Text = "Raw Material"
            ''            'ElseIf stritemtype = "O" Then
            ''            '    ddlitemtype.Text = "Other"
            ''        ElseIf stritemtype = "T" Then
            ''            ddlitemtype.Text = "Trading Item"
            ''        End If
            ''        txtflavourseq.Text = dr(17).ToString()
            ''        txtpackseq.Text = dr(18).ToString()
            ''        txtskuseq.Text = dr(19).ToString()
            ''        Dim strshow As String
            ''        If Not String.IsNullOrEmpty(dr(20).ToString()) Then
            ''            strshow = dr(20)
            ''            If strshow.Trim() = "Y" Then
            ''                chkshow.Checked = True
            ''            Else
            ''                chkshow.Checked = False
            ''            End If
            ''        Else
            ''            chkshow.Checked = False
            ''        End If
            ''        fndcategory.Value = dr(21).ToString()
            ''        txttolerence.Text = dr(22).ToString()
            ''        txttech.Text = dr(23).ToString()
            ''        txtcost.Text = dr(24).ToString()
            ''        fndItemSubCategory.Value = dr(25).ToString()
            ''        drpboxType.Text = ""
            ''        drpboxType.Text = dr("typeofitm").ToString()
            ''        Dim strNoMRP As String = dr("NOMRP").ToString()
            ''        If strNoMRP = "1" Then
            ''            chkNoMRP.Checked = True
            ''        Else : strNoMRP = "0"
            ''            chkNoMRP.Checked = False
            ''        End If
            ''        ''''Added by---Pankaj Kumar----on--12/03/2012
            ''        If dr("Morning").ToString() = "1" Then
            ''            chkMorning.Checked = True
            ''        Else
            ''            chkMorning.Checked = False
            ''        End If
            ''        ''''Code Ends Here

            ''        fillSub_Item_Category_Desc() ''''Fills The Description of 'Sub_Item_Category'
            ''    End While
            ''    dr.Close()
            ''End If
            dgvclass1.AutoGenerateColumns = False
            dgvclass1.DataSource = Nothing
            dgvclass1.Rows.Clear()
            dgvuomdetail.DataSource = Nothing
            dgvuomdetail.Rows.Clear()
            'Dim strTSPL_ITEM_DETAILS As String = "select  class_code, class_name, Class_desc  from TSPL_ITEM_details where item_code = '" + frmitemcode1.Value + "' "
            Dim strTSPL_ITEM_DETAILS As String = "select  class_code, class_name, Class_desc  from TSPL_ITEM_details left outer join TSPL_ITEM_MASTER on  TSPL_ITEM_details.Item_Code =TSPL_ITEM_MASTER.Item_Code  where TSPL_ITEM_details. item_code = '" + frmitemcode1.Value + "' and TSPL_ITEM_MASTER.item_type <> 'R'and TSPL_ITEM_MASTER.item_type <> 'O'"
            'Dim dt As New DataTable()
            'ds = connectSql.RunSQLReturnDS(strTSPL_ITEM_DETAILS)
            'ds = ds.Tables(0).DefaultView.Table(True, "class_code", "class_name", "Class_desc")
            Dim da As New SqlDataAdapter(strTSPL_ITEM_DETAILS, connectSql.SqlCon())
            Dim dt1 As New DataTable()
            da.Fill(dt1)
            dt1 = dt1.DefaultView.ToTable(True, "class_code", "class_name", "Class_desc")
            'transportSql.FillGridView(strTSPL_ITEM_DETAILS, dgvclass1)
            dgvclass1.DataSource = dt1
            dgvclass1.Columns(0).FieldName = "class_name"
            dgvclass1.Columns(1).FieldName = "class_code"
            dgvclass1.Columns(2).FieldName = "Class_desc"
            dgvuomdetail.AutoGenerateColumns = False
            Dim struomdetail As String = "select UOM_Code  , UOM_Description , Conversion_Factor , (case Stocking_Unit when 'Y' then 'Yes'  ELSE 'No' END) AS [Stocking Unit],Weight  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + frmitemcode1.Value + "'"
            transportSql.FillGridView(struomdetail, dgvuomdetail)
            dgvuomdetail.Columns(0).FieldName = "UOM_Code"
            dgvuomdetail.Columns(1).FieldName = "UOM_Description"
            dgvuomdetail.Columns(2).FieldName = "Conversion_Factor"
            dgvuomdetail.Columns(3).FieldName = "Stocking Unit"
            dgvuomdetail.Columns(4).FieldName = "Weight"

            btnsave1.Text = "Update"
            btndelete1.Enabled = True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString())
        End Try
    End Sub
    '' To Reset all the field on the screen
    Private Sub funreset()
        frmitemcode1.MyReadOnly = False
        drpboxType.Text = "Select"
        frmitemcode1.Value = ""
        fndpurchaseaccountset1.Value = ""
        fndsaleaccountset1.Value = ""
        fndstructurecode1.Value = ""
        dgvclass1.DataSource = Nothing
        fndchapterhead1.Value = ""
        ''//txtdefautltprice1.Text = ""
        txtdesc1.Text = ""
        txtfathercode1.Text = ""
        txtfatherqty1.Text = ""
        txtmothercode1.Text = ""
        txtmotherqty1.Text = ""
        ' //txtopnbalance.Text = ""
        txtstrcturedesc.Text = ""
        fndunitofmeasure.Value = ""
        chkp2count.Checked = False
        chkp3count.Checked = False
        btndelete1.Enabled = False
        btnsave1.Text = "Save"
        ddlservertype.Text = "select"
        fndstructurecode1.Enabled = True
        ddlitemtype.Text = "Finished Goods"
        txtpackseq.Text = ""
        txtskuseq.Text = ""
        txtflavourseq.Text = ""
        dgvuomdetail.DataSource = Nothing
        dgvuomdetail.Rows.Clear()
        chkshow.Checked = False
        fndcategory.Value = ""
        txtcatdesc.Text = ""
        fndItemSubCategory.Value = ""
        txtSubcatdesc.Text = ""
        txttolerence.Text = 0
        txttech.Text = ""
        txtpackseq.Text = 0
        txtflavourseq.Text = 0
        txtskuseq.Text = 0
        txtcost.Text = 0
        chkNoMRP.Checked = False
        SetDataBaseGrid()
    End Sub
    '' To Avoid the enter of character in numeric textbox
    Private Sub txtfatherqty1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfatherqty1.KeyPress
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub
    '' To Avoid the enter of character in numeric textbox
    Private Sub txtmotherqty1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmotherqty1.KeyPress
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub
    '' To Avoid the enter of character in numeric textbox
    Private Sub txtdefautltprice1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
    End Sub
    '' To Avoid the enter of character in numeric textbox
    Private Sub txtopnbalance_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub
    ''To Fill the grid according to the structure code
    Private Sub fungridfill()
        Try
            Dim str As String = "select structure_descq from TSPL_STRUCTURE_MASTER where Structure_Code = '" + fndstructurecode1.Value + "'"
            Dim da As New SqlDataAdapter(str, connectSql.SqlCon)
            Dim dt As New DataTable()
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                txtstrcturedesc.Text = row(0).ToString()
            End If
            dgvclass1.AutoGenerateColumns = False
            Dim strgrid As String = "select inv_class from TSPL_STRUCTURE_DETAIL where Structure_Code = '" + fndstructurecode1.Value + "'"
            transportSql.FillGridView(strgrid, dgvclass1)
            dgvclass1.Columns(0).FieldName = "inv_class"

            dgvclass1.AllowAddNewRow = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message.ToString())
        End Try
    End Sub
    ''To call fungridfill() to retrieve the class
    Private Sub Text_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fungridfill()
        If fndstructurecode1.Value = "" Then
            dgvclass1.ReadOnly = True
        Else
            dgvclass1.ReadOnly = False
        End If
    End Sub
    Private Sub txtchapterhead1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (Microsoft.VisualBasic.Asc(e.KeyChar) < 65) _
Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 90) _
And (Microsoft.VisualBasic.Asc(e.KeyChar) < 97) _
Or (Microsoft.VisualBasic.Asc(e.KeyChar) > 122) Then
            'Allowed space
            If (Microsoft.VisualBasic.Asc(e.KeyChar) <> 32) Then
                e.Handled = True
            End If
        End If
        ' Allowed backspace
        If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub
    'Private Sub fndstructurecode1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndstructurecode1.ConnectionString = connectSql.SqlCon()
    '    fndstructurecode1.Query = "select Structure_Code as [Strucutre Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER"
    '    fndstructurecode1.ValueToSelect = "Strucutre Code"
    '    fndstructurecode1.Caption = "Structure Detail"
    '    fndstructurecode1.ValueToSelect1 = "Description"
    'End Sub
    'Private Sub fndpurchaseaccountset1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndpurchaseaccountset1.Query = "select Purchase_Class_Code as [Purchase Account Set], Purchase_Class_Desc as [Description] from dbo.TSPL_PURCHASE_ACCOUNTS"
    '    fndpurchaseaccountset1.ConnectionString = connectSql.SqlCon()
    '    fndpurchaseaccountset1.ValueToSelect = "Purchase Account Set"
    '    fndpurchaseaccountset1.Caption = "Purchase Account Detail"
    '    fndpurchaseaccountset1.ValueToSelect1 = "Description"
    'End Sub
    'Private Sub fndsaleaccountset1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndsaleaccountset1.ConnectionString = connectSql.SqlCon()
    '    fndsaleaccountset1.Query = " select Sales_Class_Code as [Sales Account Set], Sales_Class_Desc as [Description] from TSPL_SALES_ACCOUNTS"
    '    fndsaleaccountset1.ValueToSelect = "Sales Account Set"
    '    fndsaleaccountset1.Caption = "Sale Account Detail"
    '    fndsaleaccountset1.ValueToSelect1 = "Description"
    'End Sub
    'Private Sub frmitemcode1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    frmitemcode1.ConnectionString = connectSql.SqlCon()
    '    frmitemcode1.Query = "select item_code as [Item Code], item_desc as [Description] from tspl_item_master"
    '    frmitemcode1.ValueToSelect = "Item Code"
    '    frmitemcode1.Caption = "Item Details"
    '    frmitemcode1.ValueToSelect1 = "Description"
    'End Sub
    'Private Sub fndunitofmeasure_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndunitofmeasure.ConnectionString = connectSql.SqlCon()
    '    fndunitofmeasure.Query = "select unit_code as [Unit Code], unit_desc as [Description] from tspl_unit_master"
    '    fndunitofmeasure.ValueToSelect = "Unit Code"
    '    fndunitofmeasure.Caption = "Unit Detail"
    '    fndunitofmeasure.ValueToSelect1 = "Description"
    'End Sub
    ''To Close the window screen
    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        closeform()
    End Sub
    Public Sub closeform()
        Me.Close()
    End Sub
    ''To Call the delete function 
    Private Sub btndelete1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete1.Click
        deletedata()
    End Sub
    Public Sub deletedata()
        If myMessages.deleteConfirm() Then
            fundelete()
            funreset()
        End If
    End Sub
    Private Sub btnsave1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave1.Click
        savedata()
    End Sub
    Public Sub savedata()
        Dim checkyes As Decimal
        For ki As Integer = 0 To dgvuomdetail.Rows.Count - 1
            If Not String.IsNullOrEmpty(dgvuomdetail.Rows(ki).Cells(3).Value.ToString()) Then
                If dgvuomdetail.Rows(ki).Cells(3).Value = "Yes" Then
                    checkyes = checkyes + 1
                End If
            End If
        Next
        If checkyes > 1 Then
            common.clsCommon.MyMessageBoxShow(Me, "Stocking Unit Yes use only one time")
        Else
            Dim check As String = connectSql.RunScalar("select Inv_Class_Name  from TSPL_INV_CLASS  where Class_Type ='Pack Type' ")
            Dim classcode As String = ""
            For j As Integer = 0 To dgvclass1.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvclass1.Rows(j).Cells(0).Value) Then
                    If dgvclass1.Rows(j).Cells(0).Value = check Then
                        classcode = dgvclass1.Rows(j).Cells(1).Value
                        Exit For
                    End If
                End If
            Next
            Dim strpack As String = "select Parent, Child  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name = '" + check + "' and Inv_Class_Code = '" + classcode + "'"
            ' dr = connectSql.RunSqlReturnDR(strpack)
            Dim father As String = ""
            Dim child As String = ""
            Dim fathercode As String = Nothing
            Dim mothercode As String = Nothing
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strpack)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    father = dt.Rows(i)("Parent").ToString()
                    child = dt.Rows(i)("Child").ToString()
                Next
            End If


            'If dr.HasRows Then
            '    While dr.Read()
            '        father = dr(0).ToString()
            '        child = dr(1).ToString()
            '    End While
            'End If
            If father = "N" And child = "N" Then
                funbtninsertupdate()


            ElseIf father = "N" And child = "Y" Then
                mothercode = connectSql.RunScalar("select Mother_Code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "'")
                If String.IsNullOrEmpty(mothercode) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please make before child code")
                Else
                    funbtninsertupdate()
                End If
            ElseIf father = "Y" And child = "N" Then
                fathercode = connectSql.RunScalar("select father_code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "'")
                If String.IsNullOrEmpty(fathercode) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please make before parent code")
                Else
                    funbtninsertupdate()
                End If

            ElseIf father = "Y" And child = "Y" Then

                Dim sqlMC As String = "select Mother_Code , Father_Code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "' "
                'dr = connectSql.RunSqlReturnDR(sqlMC)


                Dim dtmc As DataTable
                dtmc = clsDBFuncationality.GetDataTable(sqlMC)
                If dtmc.Rows.Count > 0 Then
                    For i As Integer = 0 To dtmc.Rows.Count - 1
                        fathercode = dtmc.Rows(i)("Father_Code")
                        mothercode = dtmc.Rows(i)("Mother_Code")

                    Next
                End If


                'If dr.HasRows Then
                '    dr.Read()
                '    fathercode = dr(1)
                '    mothercode = dr(0)
                'End If
                If String.IsNullOrEmpty(fathercode) Or String.IsNullOrEmpty(mothercode) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Parent and child code for this pack type is not defined")
                Else
                    funbtninsertupdate()
                End If
                Else

                    funbtninsertupdate()


                End If

            End If
    End Sub
    ''To save  and update the data on the button click event
    Private Sub funbtninsertupdate()

        If txttolerence.Text = "" Then
            txttolerence.Text = 0
        End If
        If txtcost.Text = "" Then
            txtcost.Text = 0
        End If
        Dim check As String = ""
        If fndstructurecode1.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select the structure code")
            fndstructurecode1.Focus()
        ElseIf fndpurchaseaccountset1.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select the Purchase Account Set")
            fndpurchaseaccountset1.Focus()
        ElseIf fndsaleaccountset1.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select the Sale Account set")
            fndsaleaccountset1.Focus()
        ElseIf fndunitofmeasure.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select the Unit Of Measure")
            fndunitofmeasure.Focus()
            'ElseIf txtflavourseq.Text = "" And ddlitemtype.Text <> "Finished Goods" Then
            '    myMessages.blankValue("Flavour Sequence")
            '    txtflavourseq.Focus()
            'ElseIf txtpackseq.Text = "" And ddlitemtype.Text <> "Finished Goods" Then
            '    myMessages.blankValue("Pack Sequence")
            '    txtpackseq.Focus()
            'ElseIf txtskuseq.Text = "" And ddlitemtype.Text <> "Finished Goods" Then
            '    myMessages.blankValue("SKU Sequence")
            '    txtskuseq.Focus()
        ElseIf fndcategory.Value = "" Then
            myMessages.blankValue("Item Category")
            fndcategory.Focus()
        ElseIf fndItemSubCategory.Value = "" Then
            myMessages.blankValue("Sub Item Category")
            fndItemSubCategory.Focus()
            'ElseIf txttolerence.Text = "" Then
            '    txttolerence.Text = 0
        Else
            If btnsave1.Text = "Save" Then
                For i As Integer = 0 To dgvclass1.Rows.Count - 1
                    If String.IsNullOrEmpty(dgvclass1.Rows(i).Cells(1).Value) Then
                        check = i
                    End If
                Next
                If Not String.IsNullOrEmpty(check) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select all the class Code")
                Else
                    funinsert()
                    funfill()
                End If

            Else : btnsave1.Text = "Update"
                For i As Integer = 0 To dgvclass1.Rows.Count - 1
                    If String.IsNullOrEmpty(dgvclass1.Rows(i).Cells(1).Value) Then
                        check = i
                    End If
                Next
                If Not String.IsNullOrEmpty(check) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select all the class Code")
                Else

                    funupdate()
                End If

            End If
        End If
    End Sub
    ''To Display class code according to the class name 
    Private Sub dgvclass1_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles dgvclass1.EditorRequired
        Dim dt As GridViewComboBoxColumn = TryCast(dgvclass1.Columns(1), GridViewComboBoxColumn)
        Dim str As String = "select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name = '" + dgvclass1.CurrentRow.Cells(0).Value + "'"
        ds = connectSql.RunSQLReturnDS(str)
        dt.DataSource = ds.Tables(0)
        dt.ValueMember = "inv_class_code"
    End Sub
    Private Sub txtdesc1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub
    ''To Call the reset function
    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset1.Click
        funreset()
    End Sub
    'Private Sub mnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnexport.Click
    '    clsCommon.ProgressBarShow()
    '    Dim x As Integer
    '    ' x = dataGrd.Rows.Count - 1
    '    Dim y As Integer
    '    ' y = dataGrd.Columns.Count
    '    Dim i As Integer
    '    Dim j As Integer
    '    x = 2
    '    y = 2
    '    i = 0
    '    j = 0
    '    Dim intRow As Integer
    '    Dim intColumn As Integer

    '    Dim xlApp As Excel.Application
    '    Dim xlWorkBook As Excel.Workbook
    '    Dim xlWorkSheet As Excel.Worksheet
    '    Dim xlWorkSheetBiLL As Excel.Worksheet
    '    Dim xlWorkSheetBiLLS As Excel.Worksheet
    '    Dim misValue As Object = System.Reflection.Missing.Value

    '    xlApp = New Excel.ApplicationClass
    '    xlWorkBook = xlApp.Workbooks.Add(misValue)



    '    xlWorkSheet = xlWorkBook.Sheets("sheet1")
    '    xlWorkSheetBiLL = xlWorkBook.Sheets("sheet2")
    '    xlWorkSheetBiLLS = xlWorkBook.Sheets("sheet3")


    '    Dim myDataTable1 As DataTable
    '    Dim myDataTable2 As DataTable
    '    Dim myDataTable3 As DataTable

    '    Dim strQ1 As String
    '    Dim strQ2 As String
    '    Dim strQ3 As String
    '    strQ1 = "select * from TSPL_ITEM_MASTER"

    '    myDataTable1 = clsDBFuncationality.GetDataTable(strQ1)


    '    If myDataTable1.Rows.Count > 0 Then
    '        ' xlWorkSheet.Cells(1, 6) = "Item Master"
    '        xlWorkSheet.Cells(2, 1) = "Item Code"
    '        xlWorkSheet.Cells(2, 2) = "Item Description"
    '        xlWorkSheet.Cells(2, 3) = "Structure Code"
    '        ' xlWorkSheetBiLL.Cells.EntireColumn(4).Hidden = True
    '        xlWorkSheet.Cells(2, 4) = "Structure Description"
    '        xlWorkSheet.Cells(2, 5) = "Purchase Class Code"
    '        xlWorkSheet.Cells(2, 6) = "Sales Class Code"
    '        xlWorkSheet.Cells(2, 7) = "Unit Code"
    '        xlWorkSheet.Cells(2, 8) = "Default Price"
    '        xlWorkSheet.Cells(2, 9) = "Father Code"
    '        xlWorkSheet.Cells(2, 10) = "Father Quantity"
    '        xlWorkSheet.Cells(2, 11) = "Cheapter Heads"
    '        xlWorkSheet.Cells(2, 12) = "Mother Code"
    '        xlWorkSheet.Cells(2, 13) = "Mother Quantity"
    '        xlWorkSheet.Cells(2, 14) = "Opening Balance"
    '        xlWorkSheet.Cells(2, 15) = "Two Count Status"
    '        xlWorkSheet.Cells(2, 16) = "Three Count Status"
    '        xlWorkSheet.Cells(2, 17) = "Server Type"
    '        xlWorkSheet.Cells(2, 18) = "Mfg Date"
    '        xlWorkSheet.Cells(2, 19) = "Batch Number"
    '        xlWorkSheet.Cells(2, 20) = "Best Before Use Date"
    '        xlWorkSheet.Cells(2, 21) = "Item Type"
    '        xlWorkSheet.Cells(2, 22) = "Created By"
    '        xlWorkSheet.Cells(2, 23) = "Created Date"
    '        xlWorkSheet.Cells(2, 24) = "Mofify By"
    '        xlWorkSheet.Cells(2, 25) = "Modify Date"
    '        xlWorkSheet.Cells(2, 26) = "Comp Code"
    '        xlWorkSheet.Cells(2, 27) = "Flavor Seq"
    '        xlWorkSheet.Cells(2, 28) = "Pack Seq"
    '        xlWorkSheet.Cells(2, 29) = "Sku Seq"
    '        xlWorkSheet.Cells(2, 30) = "Show"
    '        xlWorkSheet.Cells(2, 31) = "Item Category"
    '        xlWorkSheet.Cells(2, 32) = "Tolerance"
    '        xlWorkSheet.Cells(2, 33) = "Shelf Life"
    '        xlWorkSheet.Cells(2, 34) = "Cost"
    '        xlWorkSheet.Cells(2, 35) = "Sub Item Category"
    '        xlWorkSheet.Cells(2, 36) = "Type Of Item"
    '        xlWorkSheet.Cells(2, 37) = "No MRP"
    '        xlWorkSheet.Cells(2, 38) = "Morning"

    '        For intRow = 0 To myDataTable1.Rows.Count - 1
    '            x = x + 1
    '            j = 0
    '            y = 0
    '            For intColumn = 0 To myDataTable1.Columns.Count - 1
    '                y = y + 1
    '                Dim test As String = myDataTable1.Rows(i).Item(j).ToString()
    '                xlWorkSheet.Cells(x, y) = test

    '                j += 1
    '            Next
    '            i = i + 1
    '        Next
    '    Else
    '        Exit Sub
    '    End If

    '    'xlWorkSheet.Cells(1, 1) = dataGrd.Item(1, 0).Value 

    '    ' strQ2 = "select * from TSPL_ITEM_DETAILS"
    '    strQ2 = "select Item_Code,CAST (class_code as varchar(100)) as class_code,Class_Name ,Class_Desc ,Created_By ,Created_Date ,Modify_By ,Modify_Date ,Comp_Code   from TSPL_ITEM_DETAILS"

    '    myDataTable2 = clsDBFuncationality.GetDataTable(strQ2)

    '    If myDataTable2.Rows.Count > 0 Then
    '        xlWorkSheetBiLL.Range("B:B").NumberFormat = "@"
    '        ' xlWorkSheetBiLL.Cells(1, 6) = "Item Details"
    '        xlWorkSheetBiLL.Cells(2, 1) = "Item Code"
    '        xlWorkSheetBiLL.Cells(2, 2) = "Class Code"
    '        xlWorkSheetBiLL.Cells(2, 2).ToString()
    '        xlWorkSheetBiLL.Cells(2, 3) = "Class Name"
    '        'xlWorkSheetBiLL.Cells.EntireColumn(4).Hidden = True
    '        xlWorkSheetBiLL.Cells(2, 4) = "Class Description"
    '        xlWorkSheetBiLL.Cells(2, 5) = "Created By"
    '        xlWorkSheetBiLL.Cells(2, 6) = "Created Date"
    '        xlWorkSheetBiLL.Cells(2, 7) = "Modify By"
    '        xlWorkSheetBiLL.Cells(2, 8) = "Modify Date"
    '        xlWorkSheetBiLL.Cells(2, 9) = "Comp Code"

    '        x = 2
    '        y = 2
    '        i = 0
    '        j = 0

    '        For intRow = 0 To myDataTable2.Rows.Count - 1
    '            x = x + 1
    '            j = 0
    '            y = 0
    '            For intColumn = 0 To myDataTable2.Columns.Count - 1
    '                y = y + 1
    '                'If y = 4 Then
    '                '    y = y + 1
    '                'End If
    '                Dim test As String = myDataTable2.Rows(i).Item(j).ToString()
    '                xlWorkSheetBiLL.Cells(x, y) = test
    '                j += 1
    '            Next
    '            i = i + 1
    '        Next

    '        xlWorkSheetBiLL.Range("B:B").NumberFormat = "@"
    '        ' xlWorkSheetBiLL("Sheet2").Range("A1").Cells.NumberFormat = "General")
    '        '            Dim misValue1 = System.Reflection.Missing.Value
    '        'Dim  range  As  Excel.Range = (Excel.Range)xlWorkSheetBiLL.Columns["B", misValue1] = "@"
    '        '            Rang()
    '        ' Range("A1:A100").NumberFormat = "@"
    '        'xlWorkSheetBiLL("Sheet2").Range("A1").Cells.NumberFormat = "@"




    '    End If


    '    strQ3 = "select * from tspl_item_uom_detail"
    '    myDataTable3 = clsDBFuncationality.GetDataTable(strQ3)

    '    If myDataTable3.Rows.Count > 0 Then
    '        ' xlWorkSheetBiLLS.Cells(1, 6) = "Item UOM Details"
    '        xlWorkSheetBiLLS.Cells(2, 1) = "Item Code"
    '        xlWorkSheetBiLLS.Cells(2, 2) = "UOM Code"
    '        xlWorkSheetBiLLS.Cells(2, 3) = "UOM_Description"
    '        '  xlWorkSheetBiLLS.Cells.EntireColumn(4).Hidden = True
    '        xlWorkSheetBiLLS.Cells(2, 4) = "Conversion_Factor"
    '        xlWorkSheetBiLLS.Cells(2, 5) = "Stocking_Unit"
    '        x = 2
    '        y = 2
    '        i = 0
    '        j = 0
    '        For intRow = 0 To myDataTable3.Rows.Count - 1
    '            x = x + 1
    '            j = 0
    '            y = 0
    '            For intColumn = 0 To myDataTable3.Columns.Count - 1
    '                y = y + 1
    '                'If y = 4 Then
    '                '    y = y + 1
    '                'End If
    '                Dim test As String = myDataTable3.Rows(i).Item(j).ToString()
    '                xlWorkSheetBiLLS.Cells(x, y) = test
    '                j += 1
    '            Next
    '            i = i + 1
    '        Next

    '    End If
    '    'xlWorkBook.Sheets("Sheet1").Range("a1").Value = "Item Master"
    '    'xlWorkBook.Sheets("Sheet2").Range("a1").Value = "Item Details"
    '    'xlWorkBook.Sheets("Sheet3").Range("a1").Value = "Item UOM Details"

    '    Dim excelWorkSheet As Excel.Worksheet

    '    xlWorkSheet = xlWorkBook.Sheets(1)
    '    xlWorkSheet.Name = "ItemMaster"

    '    xlWorkSheetBiLL = xlWorkBook.Sheets(2)
    '    xlWorkSheetBiLL.Name = "ItemDetails"

    '    xlWorkSheetBiLLS = xlWorkBook.Sheets(3)
    '    xlWorkSheetBiLLS.Name = "ItemUOMDetails"

    '    'xlWorkSheet.Name = "Item Master"
    '    'xlWorkSheetBiLL.Name = "Item Details"
    '    'xlWorkSheetBiLLS.Name = "Item UOM Details"




    '    Dim sfd As SaveFileDialog = New SaveFileDialog()
    '    Dim path As String
    '    sfd.FileName = Me.Text
    '    Dim strFileName As String
    '    sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
    '    clsCommon.ProgressBarHide()
    '    If sfd.ShowDialog() = System.System.Windows.Forms.DialogResult.OK Then
    '        strFileName = sfd.FileName
    '        clsCommon.ProgressBarShow()
    '        xlWorkSheet.SaveAs("" + strFileName + "")

    '        xlWorkBook.Close()
    '        xlApp.Quit()
    '        releaseObject(xlApp)
    '        releaseObject(xlWorkBook)
    '        releaseObject(xlWorkSheet)
    '        releaseObject(xlWorkSheetBiLL)
    '        releaseObject(xlWorkSheetBiLLS)

    '        clsCommon.ProgressBarHide()
    '        common.clsCommon.MyMessageBoxShow("Data Exported Successfully ...")
    '    Else
    '        MsgBox("No Record")
    '    End If







    '    'Dim ds As New DataSet()
    '    'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_MASTER")
    '    'Dim dtInsert1 As New DataTable("1")
    '    'dtInsert1 = dt.Copy()
    '    'dtInsert1.TableName = "1"
    '    'ds.Tables.Add(dtInsert1)

    '    'dt = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_DETAILS")
    '    'Dim dtInsert2 As New DataTable("2")
    '    'dtInsert2 = dt.Copy()
    '    'dtInsert2.TableName = "2"
    '    'ds.Tables.Add(dtInsert2)

    '    ''dt = clsDBFuncationality.GetDataTable("select * from TSPL_ITEM_UOM_DETAIL")
    '    ''Dim dtInsert3 As New DataTable("3")
    '    ''dtInsert3 = dt.Copy()
    '    ''dtInsert3.TableName = "3"
    '    ''ds.Tables.Add(dtInsert3)
    '    'ExportToExcel(ds, Me)

    '    '-------------------------   Early Coding  --------------------------------
    '    'Dim str As String = "select m.Item_Code as [Item Code], m.Item_Desc as [Item Description], m.Structure_Code as [Strucuture Code], m.Structure_Desc as [Structure Description], m.Purchase_Class_Code as [Purchase Class Code], m.Sale_Class_Code as [Sale Class Code], m.Unit_Code as [Unit Code], m.Deafult_Price as [Default Price], m.Father_Code as [Father Code], m.Father_QTy as [Father Quantity], m.Cheapter_Heads as [Chapter Heads], m.Mother_Code as [Mother Code], m.Mother_Qty as [Mother Quantity],m.Opening_Balance as [Opening Balance], m.Two_Count_Status as [Two Count Status], m.Three_Count_Status as [Three Count Status], m.Server_Type as [Serve Type], m.item_type as [Item Type],  m.Flavour_Seq as [Flavour Sequence] , m.Pack_Seq as [Pack Sequence] , m.Sku_Seq as [SKU Sequence] ,   d.Class_Code as [Class Code], d.Class_Name as [Class Name], d.Class_Desc AS [Class Description],  m.Created_By as [Created By], m.Created_Date  as [Created Date], m.Modify_By as [Modify By], m.Modify_Date as [Modify Date], m.Comp_Code as [Company Code] from TSPL_ITEM_MASTER m join TSPL_ITEM_DETAILS d on m.Item_Code = d.Item_Code where Class_Name='" + classname + "'"
    '    'transportSql.ExporttoExcel(str, Me)

    '    '-------------------------    Ends Here    -------------------------------------------------

    'End Sub

    '--------------------------Added By--Pankaj Kumar----------------On-----04/06/2012---------------------------
    Private Sub RMIExportBlank_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIExportBlank.Click
        QrySheet = " Where 1=2"
        QrySpcl = "Blank"
        ExportItem(QrySheet)
    End Sub

    Private Sub RMIexportRaw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIexportRaw.Click
        QrySheet = " Where Item_Code in (Select Item_Code from TSPL_ITEM_MASTER Where Item_Type='R')"
        ExportItem(QrySheet)
    End Sub

    Private Sub RmiExportFinished_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmiExportFinished.Click
        QrySheet = " Where Item_Code in (Select Item_Code from TSPL_ITEM_MASTER Where Item_Type='F')"
        ExportItem(QrySheet)
    End Sub

    Private Sub RMIExportAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIExportAll.Click
        QrySheet = " "
        ExportItem(QrySheet)
    End Sub

    Private Sub RMIOthers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RMIOthers.Click
        QrySheet = " Where Item_Code in (Select Item_Code from TSPL_ITEM_MASTER Where Item_Type='O')"
        ExportItem(QrySheet)
    End Sub
    '-------------------------------------Code Ends Here-----------------------------------------------------------

    Public Sub ExportItem(ByVal qry1 As String)
        clsCommon.ProgressBarShow()
        Dim x As Integer
        ' x = dataGrd.Rows.Count - 1
        Dim y As Integer
        ' y = dataGrd.Columns.Count
        Dim i As Integer
        Dim j As Integer
        x = 2
        y = 2
        i = 0
        j = 0
        Dim intRow As Integer
        Dim intColumn As Integer

        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        Dim xlWorkSheetBiLL As Excel.Worksheet
        Dim xlWorkSheetBiLLS As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value

        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)



        xlWorkSheet = xlWorkBook.Sheets("sheet1")
        xlWorkSheetBiLL = xlWorkBook.Sheets("sheet2")
        xlWorkSheetBiLLS = xlWorkBook.Sheets("sheet3")


        Dim myDataTable1 As DataTable
        Dim myDataTable2 As DataTable
        Dim myDataTable3 As DataTable

        Dim strQ1 As String
        Dim strQ2 As String
        Dim strQ3 As String
        strQ1 = "select * from TSPL_ITEM_MASTER " + qry1 + ""

        myDataTable1 = clsDBFuncationality.GetDataTable(strQ1)


        If myDataTable1.Rows.Count > 0 Or QrySpcl = "Blank" Then
            ' xlWorkSheet.Cells(1, 6) = "Item Master"
            xlWorkSheet.Cells(2, 1) = "Item Code"
            xlWorkSheet.Cells(2, 2) = "Item Description"
            xlWorkSheet.Cells(2, 3) = "Structure Code"
            ' xlWorkSheetBiLL.Cells.EntireColumn(4).Hidden = True
            xlWorkSheet.Cells(2, 4) = "Structure Description"
            xlWorkSheet.Cells(2, 5) = "Purchase Class Code"
            xlWorkSheet.Cells(2, 6) = "Sales Class Code"
            xlWorkSheet.Cells(2, 7) = "Unit Code"
            xlWorkSheet.Cells(2, 8) = "Default Price"
            xlWorkSheet.Cells(2, 9) = "Father Code"
            xlWorkSheet.Cells(2, 10) = "Father Quantity"
            xlWorkSheet.Cells(2, 11) = "Cheapter Heads"
            xlWorkSheet.Cells(2, 12) = "Mother Code"
            xlWorkSheet.Cells(2, 13) = "Mother Quantity"
            xlWorkSheet.Cells(2, 14) = "Opening Balance"
            xlWorkSheet.Cells(2, 15) = "Two Count Status"
            xlWorkSheet.Cells(2, 16) = "Three Count Status"
            xlWorkSheet.Cells(2, 17) = "Server Type"
            xlWorkSheet.Cells(2, 18) = "Mfg Date"
            xlWorkSheet.Cells(2, 19) = "Batch Number"
            xlWorkSheet.Cells(2, 20) = "Best Before Use Date"
            xlWorkSheet.Cells(2, 21) = "Item Type"
            xlWorkSheet.Cells(2, 22) = "Created By"
            xlWorkSheet.Cells(2, 23) = "Created Date"
            xlWorkSheet.Cells(2, 24) = "Mofify By"
            xlWorkSheet.Cells(2, 25) = "Modify Date"
            xlWorkSheet.Cells(2, 26) = "Comp Code"
            xlWorkSheet.Cells(2, 27) = "Flavor Seq"
            xlWorkSheet.Cells(2, 28) = "Pack Seq"
            xlWorkSheet.Cells(2, 29) = "Sku Seq"
            xlWorkSheet.Cells(2, 30) = "Show"
            xlWorkSheet.Cells(2, 31) = "Item Category"
            xlWorkSheet.Cells(2, 32) = "Tolerance"
            xlWorkSheet.Cells(2, 33) = "Shelf Life"
            xlWorkSheet.Cells(2, 34) = "Cost"
            xlWorkSheet.Cells(2, 35) = "Sub Item Category"
            xlWorkSheet.Cells(2, 36) = "Type Of Item"
            xlWorkSheet.Cells(2, 37) = "No MRP"
            xlWorkSheet.Cells(2, 38) = "Morning"

            For intRow = 0 To myDataTable1.Rows.Count - 1
                x = x + 1
                j = 0
                y = 0
                For intColumn = 0 To myDataTable1.Columns.Count - 1
                    y = y + 1
                    Dim test As String = myDataTable1.Rows(i).Item(j).ToString()
                    xlWorkSheet.Cells(x, y) = test

                    j += 1
                Next
                i = i + 1
            Next
        Else
            Exit Sub
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found")
        End If

        'xlWorkSheet.Cells(1, 1) = dataGrd.Item(1, 0).Value 

        ' strQ2 = "select * from TSPL_ITEM_DETAILS"
        strQ2 = "select Item_Code,CAST (class_code as varchar(100)) as class_code,Class_Name ,Class_Desc ,Created_By ,Created_Date ,Modify_By ,Modify_Date ,Comp_Code   from TSPL_ITEM_DETAILS " + QrySheet + ""

        myDataTable2 = clsDBFuncationality.GetDataTable(strQ2)

        If myDataTable2.Rows.Count > 0 Or QrySpcl = "Blank" Then
            xlWorkSheetBiLL.Range("B:B").NumberFormat = "@"
            ' xlWorkSheetBiLL.Cells(1, 6) = "Item Details"
            xlWorkSheetBiLL.Cells(2, 1) = "Item Code"
            xlWorkSheetBiLL.Cells(2, 2) = "Class Code"
            xlWorkSheetBiLL.Cells(2, 2).ToString()
            xlWorkSheetBiLL.Cells(2, 3) = "Class Name"
            'xlWorkSheetBiLL.Cells.EntireColumn(4).Hidden = True
            xlWorkSheetBiLL.Cells(2, 4) = "Class Description"
            xlWorkSheetBiLL.Cells(2, 5) = "Created By"
            xlWorkSheetBiLL.Cells(2, 6) = "Created Date"
            xlWorkSheetBiLL.Cells(2, 7) = "Modify By"
            xlWorkSheetBiLL.Cells(2, 8) = "Modify Date"
            xlWorkSheetBiLL.Cells(2, 9) = "Comp Code"

            x = 2
            y = 2
            i = 0
            j = 0

            For intRow = 0 To myDataTable2.Rows.Count - 1
                x = x + 1
                j = 0
                y = 0
                For intColumn = 0 To myDataTable2.Columns.Count - 1
                    y = y + 1
                    'If y = 4 Then
                    '    y = y + 1
                    'End If
                    Dim test As String = myDataTable2.Rows(i).Item(j).ToString()
                    xlWorkSheetBiLL.Cells(x, y) = test
                    j += 1
                Next
                i = i + 1
            Next

            xlWorkSheetBiLL.Range("B:B").NumberFormat = "@"
            ' xlWorkSheetBiLL("Sheet2").Range("A1").Cells.NumberFormat = "General")
            '            Dim misValue1 = System.Reflection.Missing.Value
            'Dim  range  As  Excel.Range = (Excel.Range)xlWorkSheetBiLL.Columns["B", misValue1] = "@"
            '            Rang()
            ' Range("A1:A100").NumberFormat = "@"
            'xlWorkSheetBiLL("Sheet2").Range("A1").Cells.NumberFormat = "@"




        End If


        strQ3 = "select * from tspl_item_uom_detail " + QrySheet + ""
        myDataTable3 = clsDBFuncationality.GetDataTable(strQ3)

        If myDataTable3.Rows.Count > 0 Or QrySpcl = "Blank" Then
            ' xlWorkSheetBiLLS.Cells(1, 6) = "Item UOM Details"
            xlWorkSheetBiLLS.Cells(2, 1) = "Item Code"
            xlWorkSheetBiLLS.Cells(2, 2) = "UOM Code"
            xlWorkSheetBiLLS.Cells(2, 3) = "UOM_Description"
            '  xlWorkSheetBiLLS.Cells.EntireColumn(4).Hidden = True
            xlWorkSheetBiLLS.Cells(2, 4) = "Conversion_Factor"
            xlWorkSheetBiLLS.Cells(2, 5) = "Stocking_Unit"
            x = 2
            y = 2
            i = 0
            j = 0
            For intRow = 0 To myDataTable3.Rows.Count - 1
                x = x + 1
                j = 0
                y = 0
                For intColumn = 0 To myDataTable3.Columns.Count - 1
                    y = y + 1
                    'If y = 4 Then
                    '    y = y + 1
                    'End If
                    Dim test As String = myDataTable3.Rows(i).Item(j).ToString()
                    xlWorkSheetBiLLS.Cells(x, y) = test
                    j += 1
                Next
                i = i + 1
            Next

        End If
        'xlWorkBook.Sheets("Sheet1").Range("a1").Value = "Item Master"
        'xlWorkBook.Sheets("Sheet2").Range("a1").Value = "Item Details"
        'xlWorkBook.Sheets("Sheet3").Range("a1").Value = "Item UOM Details"

        ' Dim excelWorkSheet As Excel.Worksheet

        xlWorkSheet = xlWorkBook.Sheets(1)
        xlWorkSheet.Name = "ItemMaster"

        xlWorkSheetBiLL = xlWorkBook.Sheets(2)
        xlWorkSheetBiLL.Name = "ItemDetails"

        xlWorkSheetBiLLS = xlWorkBook.Sheets(3)
        xlWorkSheetBiLLS.Name = "ItemUOMDetails"

        'xlWorkSheet.Name = "Item Master"
        'xlWorkSheetBiLL.Name = "Item Details"
        'xlWorkSheetBiLLS.Name = "Item UOM Details"




        Dim sfd As SaveFileDialog = New SaveFileDialog()
        ' Dim path As String
        sfd.FileName = Me.Text
        Dim strFileName As String
        sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        clsCommon.ProgressBarHide()
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strFileName = sfd.FileName
            clsCommon.ProgressBarShow()
            xlWorkSheet.SaveAs("" + strFileName + "")

            xlWorkBook.Close()
            xlApp.Quit()
            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
            releaseObject(xlWorkSheetBiLL)
            releaseObject(xlWorkSheetBiLLS)

            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, "Data Exported Successfully ...")
        Else
            MsgBox("No Record")
        End If
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
    Public Sub ExportToExcel(ByVal DS_MyDataset As DataSet, ByVal frm As RadForm)
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        ' Dim path As String
        sfd.FileName = frm.Text
        Dim strFileName As String = ""
        sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strFileName = sfd.FileName
        Else

        End If
        'The full path where the excel file will be stored


        'Dim strFileName As String = _
        '    AppDomain.CurrentDomain.BaseDirectory.Replace("/", "\")
        'strFileName = strFileName & "\MyExcelFile1"
        Dim objCells As Excel.Range = Nothing
        Dim objExcel As Excel.Application = Nothing
        Dim objBooks As Excel.Workbooks = Nothing
        Dim objBook As Excel.Workbook = Nothing
        Dim objSheets As Excel.Sheets = Nothing
        Dim objSheet As Excel.Worksheet = Nothing
        Dim objRange As Excel.Range = Nothing

        Try
            'Creating a new object of the Excel application object
            objExcel = New Excel.Application
            'Hiding the Excel application
            objExcel.Visible = False
            'Hiding all the alert messages occurring during the process
            objExcel.DisplayAlerts = False

            'Adding a collection of Workbooks to the Excel object
            objBook = CType(objExcel.Workbooks.Add(), Excel.Workbook)

            'Saving the Workbook as a normal workbook format.
            objBook.SaveAs(strFileName, Excel.XlFileFormat.xlWorkbookNormal)

            'Getting the collection of workbooks in an object
            objBooks = objExcel.Workbooks

            'Get the reference to the first sheet
            'in the workbook collection in a variable
            objSheet = CType(objBooks(1).objSheets.Item(1), Excel.Worksheet)
            'Optionally name the worksheet
            objSheet.Name = "First Sheet"
            'You can even set the font attributes of a range of cells
            'in the sheet. Here we have set the fonts to bold.
            objSheet.Range("A1", "Z1").Font.Bold = True

            'Get the cells collection of the sheet in a variable, to write the data.
            objRange = objSheet.Cells

            'Calling the function to write the dataset data in the cells of the first sheet.
            WriteData(DS_MyDataset.Tables(0), objCells)

            'Setting the width of the specified range of cells
            'so as to absolutely fit the written data.
            objSheet.Range("A1", "Z1").EntireColumn.AutoFit()
            'Saving the worksheet.
            objSheet.SaveAs(strFileName)

            objBook = objBooks.Item(1)
            objSheets = objBook.Worksheets
            objSheet = CType(objSheets.Item(2), Excel.Worksheet)
            objSheet.Name = "Second Sheet"
            'Setting the color of the specified range of cells
            'to Red (ColorIndex 3 denoted Red color)
            objSheet.Range("A1", "Z1").Font.ColorIndex = 3

            objRange = objSheet.Cells
            WriteData(DS_MyDataset.Tables(1), objCells)
            objSheet.Range("A1", "Z1").EntireColumn.AutoFit()

            objSheet.SaveAs(strFileName)

        Catch ex As Exception
            myMessages.myExceptions(ex)
        Finally
            'Close the Excel application
            objExcel.Quit()

            'Release all the COM objects so as to free the memory
            ReleaseComObject(objRange)
            ReleaseComObject(objSheet)
            ReleaseComObject(objSheets)
            ReleaseComObject(objBook)
            ReleaseComObject(objBooks)
            ReleaseComObject(objExcel)

            'Set the all the objects for the Garbage collector to collect them.
            objExcel = Nothing
            objBooks = Nothing
            objBook = Nothing
            objSheets = Nothing
            objSheet = Nothing
            objRange = Nothing

            'Specifically call the garbage collector.
            System.GC.Collect()
        End Try
    End Sub

    Private Sub WriteData(ByVal DT_DataTable As DataTable, ByVal objCells As Excel.Range)
        Dim iRow As Integer, iCol As Integer

        'Traverse through the DataTable columns to write the
        'headers on the first row of the excel sheet.
        For iCol = 0 To DT_DataTable.Columns.Count - 1
            objCells(1, iCol + 1) = DT_DataTable.Columns(iCol).ToString
        Next

        'Traverse through the rows and columns
        'of the datatable to write the data in the sheet.
        For iRow = 0 To DT_DataTable.Rows.Count - 1
            For iCol = 0 To DT_DataTable.Columns.Count - 1
                objCells(iRow + 2, iCol + 1) = DT_DataTable.Rows(iRow)(iCol)
            Next
        Next
    End Sub



    Private Sub mnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnclose.Click
        Me.Close()
    End Sub

    Private Sub mnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnimport.Click

        Dim gv As New RadGridView()
        Dim gv1 As New RadGridView()
        Dim gv2 As New RadGridView()
        Dim isSaved As Boolean = True
        Me.Controls.Add(gv)
        Me.Controls.Add(gv1)
        Me.Controls.Add(gv2)

        Dim ofd As OpenFileDialog = New OpenFileDialog()
        Dim filePath As String
        ofd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            filePath = ofd.FileName
        Else
            Return
        End If
        'clsCommon.ProgressBarShow()
        clsCommon.ProgressBarPercentShow()
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = Nothing
        Dim ii As Integer = 0
        Try

            trans = clsDBFuncationality.GetTransactin()
            ''-Insertig for ITem MAster
            Dim COUNTER As Integer = 0
            If transportSql.importExcelForItemMaster("ItemMaster$", gv, gv1, gv2, filePath, "Item Code", "Item Description", "Structure Code", "Structure Description", "Purchase Class Code", "Sales Class Code", "Unit Code", "Default Price", "Father Code", "Father Quantity", "Cheapter Heads", "Mother Code", "Mother Quantity", "Opening Balance", "Two Count Status", "Three Count Status", "Server Type", "Mfg Date", "Batch Number", "Best Before Use Date", "Item Type", "Flavor Seq", "Pack Seq", "Sku Seq", "Show", "Item Category", "Tolerance", "Shelf Life", "Cost", "Sub Item Category", "Type Of Item", "No MRP") Then
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    COUNTER += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / gv.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv.RowCount - 1))
                    Dim stritemcode As String = clsCommon.myCstr(grow.Cells(0).Value).Trim()
                    Dim stritemdesc As String = clsCommon.myCstr(grow.Cells(1).Value).Trim()
                    Dim strstructcode As String = clsCommon.myCstr(grow.Cells(2).Value).Trim()
                    Dim strstructdesc As String = clsCommon.myCstr(grow.Cells(3).Value).Trim()
                    Dim strpurchaseclasscode As String = clsCommon.myCstr(grow.Cells(4).Value).Trim()
                    Dim strsaleclasscode As String = clsCommon.myCstr(grow.Cells(5).Value).Trim()
                    Dim strunitcode As String = clsCommon.myCstr(grow.Cells(6).Value).Trim()
                    Dim strdefaultprice As String = clsCommon.myCstr(grow.Cells(7).Value).Trim()
                    Dim strfathercode As String = clsCommon.myCstr(grow.Cells(8).Value).Trim()
                    Dim strfatherqty As String = clsCommon.myCstr(grow.Cells(9).Value).Trim()
                    Dim strchapterhead As String = clsCommon.myCstr(grow.Cells(10).Value).Trim()
                    Dim strmothercode As String = clsCommon.myCstr(grow.Cells(11).Value).Trim()
                    Dim strmotherqty As String = clsCommon.myCstr(grow.Cells(12).Value).Trim()
                    Dim stropnbal As String = clsCommon.myCstr(grow.Cells(13).Value).Trim()
                    Dim strtwocount As String = clsCommon.myCstr(grow.Cells(14).Value).Trim()
                    Dim strthreecount As String = clsCommon.myCstr(grow.Cells(15).Value).Trim()
                    Dim strservetype As String = clsCommon.myCstr(grow.Cells(16).Value).Trim()
                    Dim strmfg_date As String = clsCommon.myCstr(grow.Cells(17).Value).Trim()
                    Dim strbatch_number As String = clsCommon.myCstr(grow.Cells(18).Value).Trim()
                    Dim strbest_before_usedate As String = clsCommon.myCstr(grow.Cells(19).Value).Trim()
                    Dim stritemtype As String = clsCommon.myCstr(grow.Cells(20).Value).Trim()
                    Dim strflavourseq As String = clsCommon.myCstr(grow.Cells(21).Value).Trim()
                    Dim strpackseq As String = clsCommon.myCstr(grow.Cells(22).Value).Trim()
                    Dim strskuseq As String = clsCommon.myCstr(grow.Cells(23).Value).Trim()
                    Dim strshow As String = clsCommon.myCstr(grow.Cells(24).Value.ToString()).Trim()
                    Dim strItemCategory As String = clsCommon.myCstr(grow.Cells(25).Value.ToString()).Trim()
                    Dim strTolerance As String = clsCommon.myCstr(grow.Cells(26).Value).Trim()
                    Dim strshelf As Double = clsCommon.myCdbl(grow.Cells(27).Value)
                    Dim strcost As Double = clsCommon.myCdbl(grow.Cells(28).Value)
                    Dim strSubItemCategory As String = clsCommon.myCstr(grow.Cells(29).Value.ToString()).Trim()
                    Dim stritemoftype As String = clsCommon.myCstr(grow.Cells(30).Value.ToString()).Trim()
                    Dim strnomrp As String = clsCommon.myCstr(grow.Cells(31).Value.ToString()).Trim()
                    Dim Morning As Integer = grow.Cells(37).Value

                    If clsCommon.myLen(strflavourseq) <= 0 Then
                        strflavourseq = 0
                    End If
                    If clsCommon.myLen(strpackseq) <= 0 Then
                        strpackseq = 0
                    End If
                    If clsCommon.myLen(strskuseq) <= 0 Then
                        strskuseq = 0
                    End If
                    If clsCommon.myLen(stritemcode) = 0 OrElse clsCommon.myLen(stritemcode) > 50 Then

                        Throw New Exception("Item Code has some incorrect values at Row -- " + clsCommon.myCstr(COUNTER + 2) + " ")
                    End If
                    If clsCommon.myLen(stritemdesc) <= 0 OrElse clsCommon.myLen(stritemdesc) > 100 Then
                        Throw New Exception("Item Description has some incorrect values")
                    End If
                    If clsCommon.myLen(strstructcode) <= 0 OrElse clsCommon.myLen(strstructcode) > 12 Then
                        Throw New Exception("Structure Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strstructdesc) <= 0 OrElse clsCommon.myLen(strstructdesc) > 50 Then
                        Throw New Exception("Structure Description has some incorrect values")
                    End If
                    If clsCommon.myLen(strpurchaseclasscode) <= 0 OrElse clsCommon.myLen(strpurchaseclasscode) > 6 Then
                        Throw New Exception("Purchase Class Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strsaleclasscode) <= 0 OrElse clsCommon.myLen(strsaleclasscode) > 50 Then
                        Throw New Exception("Sales Class Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strunitcode) <= 0 OrElse clsCommon.myLen(strunitcode) > 12 Then
                        Throw New Exception("Unit Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strdefaultprice) > 18 Then
                        Throw New Exception("Default Price has some incorrect values")
                    End If
                    If clsCommon.myLen(strfathercode) > 50 Then
                        Throw New Exception("Father code has some incorrect values")
                    End If
                    If clsCommon.myLen(strfatherqty) > 18 Then
                        Throw New Exception("Father Quantity has some incorrect values")
                    End If
                    If clsCommon.myLen(strchapterhead) > 50 Then
                        Throw New Exception("Chapter Head has some incorrect values")
                    End If
                    If clsCommon.myLen(strmothercode) > 50 Then
                        Throw New Exception("Mother Code has some incorrect values")
                    End If
                    If clsCommon.myLen(strmotherqty) > 18 Then
                        Throw New Exception("Mother  Quantity has some incorrect values")
                    End If
                    If clsCommon.myLen(stropnbal) > 18 Then
                        Throw New Exception("Opening Balance has some incorrect values")
                    End If
                    If clsCommon.myLen(strtwocount) > 1 Then
                        Throw New Exception("Two Count Status has some incorrect values")
                    End If
                    If clsCommon.myLen(strthreecount) > 1 Then
                        Throw New Exception("Three Count Status has some incorrect values")
                    End If
                    If clsCommon.myLen(stritemtype) <= 0 Then
                        Throw New Exception("Item  Type has some incorrect values")
                    End If
                    If clsCommon.myLen(strflavourseq) <= 0 OrElse clsCommon.myLen(strflavourseq) > 16 Then
                        Throw New Exception("Flavour Sequence has some incorrect values")
                    End If
                    If clsCommon.myLen(strpackseq) <= 0 OrElse clsCommon.myLen(strpackseq) > 16 Then
                        Throw New Exception("Pack Sequence has some incorrect values")
                    End If
                    If clsCommon.myLen(strskuseq) <= 0 OrElse clsCommon.myLen(strskuseq) > 16 Then
                        Throw New Exception("SKU Sequence has some incorrect values")
                    End If
                    If clsCommon.myLen(companyCode) <= 0 Then
                        Throw New Exception("Please Enter Company Code")
                    End If

                    If clsCommon.myLen(stritemoftype) > 2 Then
                        Throw New Exception("Type of Item have incorrect value")
                    End If
                    If clsCommon.myLen(strnomrp) > 1 Then
                        Throw New Exception("No MRP have incorrect value")
                    End If
                    If clsCommon.myLen(Morning) > 1 Then
                        Throw New Exception("Please Insert Morning as Integer (1 or 0)")
                    End If


                    Dim qry As String = "select 1 from TSPL_ITEM_MASTER where Item_Code ='" + stritemcode + "' "
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    Dim isNewEntry As Boolean = True
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        isNewEntry = False
                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", stritemdesc)
                    clsCommon.AddColumnsForChange(coll, "Structure_Code", strstructcode)
                    clsCommon.AddColumnsForChange(coll, "Structure_Desc", strstructdesc)
                    clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", strpurchaseclasscode)
                    clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", strsaleclasscode)
                    clsCommon.AddColumnsForChange(coll, "Unit_Code", strunitcode)
                    clsCommon.AddColumnsForChange(coll, "Deafult_Price", strdefaultprice)
                    clsCommon.AddColumnsForChange(coll, "Father_Code", strfathercode)
                    clsCommon.AddColumnsForChange(coll, "Father_QTy", strfatherqty)
                    clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", strchapterhead)
                    clsCommon.AddColumnsForChange(coll, "Mother_Code", strmothercode)
                    clsCommon.AddColumnsForChange(coll, "Mother_Qty", strmotherqty)
                    clsCommon.AddColumnsForChange(coll, "Opening_Balance", stropnbal)
                    clsCommon.AddColumnsForChange(coll, "Two_Count_Status", strtwocount)
                    clsCommon.AddColumnsForChange(coll, "Three_Count_Status", strthreecount)
                    clsCommon.AddColumnsForChange(coll, "Server_Type", strservetype)
                    clsCommon.AddColumnsForChange(coll, "Mfg_date", strmfg_date)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", strbatch_number)
                    clsCommon.AddColumnsForChange(coll, "Best_Befor_UseDate", strbest_before_usedate)
                    clsCommon.AddColumnsForChange(coll, "item_type", stritemtype)
                    clsCommon.AddColumnsForChange(coll, "Created_By", userCode)
                    clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Modify_By", userCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", companyCode)
                    clsCommon.AddColumnsForChange(coll, "Flavour_Seq", strflavourseq)
                    clsCommon.AddColumnsForChange(coll, "Pack_Seq", strpackseq)
                    clsCommon.AddColumnsForChange(coll, "Sku_Seq", strskuseq)
                    clsCommon.AddColumnsForChange(coll, "show", strshow)
                    clsCommon.AddColumnsForChange(coll, "item_category", strItemCategory)
                    clsCommon.AddColumnsForChange(coll, "tolerence", clsCommon.myCdbl(strTolerance))
                    clsCommon.AddColumnsForChange(coll, "tech_shelf_life", strshelf)
                    clsCommon.AddColumnsForChange(coll, "Cost", strcost)
                    clsCommon.AddColumnsForChange(coll, "Sub_item_category", strSubItemCategory)
                    clsCommon.AddColumnsForChange(coll, "TypeOfItm", stritemoftype)
                    clsCommon.AddColumnsForChange(coll, "NOMRP", strnomrp)
                    clsCommon.AddColumnsForChange(coll, "Morning", Morning)

                    If isNewEntry Then

                        clsCommon.AddColumnsForChange(coll, "Item_Code", stritemcode)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "Item_Code='" + stritemcode + "'", trans)
                    End If
                Next
            End If

            ''-Insertig for ITem Details
            If transportSql.importExcelForItemMaster("ItemDetails$", gv, gv1, gv2, filePath, "Item Code", "Class Code", "Class Name", "Class Description", "Created By", "Created Date", "Modify By", "Modify Date", "Comp Code") Then
                Dim counter1 As Integer = 0
                For Each grow1 As GridViewRowInfo In gv1.Rows
                    counter1 += 1
                    Dim strIDItemCode As String = clsCommon.myCstr(grow1.Cells(0).Value)
                    Dim strIDClassCode As String = clsCommon.myCstr(grow1.Cells(1).Value)
                    Dim strIDClassName As String = clsCommon.myCstr(grow1.Cells(2).Value)
                    Dim strIDClassDesc As String = clsCommon.myCstr(grow1.Cells(3).Value)

                    Dim sql2 As String = "select 1 from TSPL_ITEM_DETAILS where Item_Code = '" + strIDItemCode + "' and class_code='" + strIDClassCode + "'and class_name='" + strIDClassName + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql2, trans)
                    Dim isNewEntry As Boolean = True
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        isNewEntry = False
                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "class_code", strIDClassCode)
                    clsCommon.AddColumnsForChange(coll, "class_name", strIDClassName)
                    clsCommon.AddColumnsForChange(coll, "class_desc", strIDClassDesc)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", strIDItemCode)
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy"))
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "Item_Code = '" + strIDItemCode + "'and class_code='" + strIDClassCode + "'and class_name='" + strIDClassName + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_DETAILS", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
            End If

            ''-Insertig for ITem UOM Details
            If transportSql.importExcelForItemMaster("ItemUOMDetails$", gv, gv1, gv2, filePath, "Item Code", "UOM Code", "UOM_Description", "Conversion_Factor", "Stocking_Unit") Then
                Dim counter2 As Integer = 0
                For Each grow2 As GridViewRowInfo In gv2.Rows
                    counter2 += 1
                    Dim strIUDIemCode As String = clsCommon.myCstr(grow2.Cells(0).Value)
                    Dim strIUDUomCode As String = clsCommon.myCstr(grow2.Cells(1).Value)
                    Dim strIUDUomDesc As String = clsCommon.myCstr(grow2.Cells(2).Value)
                    Dim strIUDConvFact As String = clsCommon.myCstr(grow2.Cells(3).Value)
                    Dim strIUDStockingUnit As String = clsCommon.myCstr(grow2.Cells(4).Value)

                    Dim sql3 As String = "select 1 from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + strIUDIemCode + "' and uom_code='" + strIUDUomCode + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(sql3, trans)
                    Dim isNewEntry As Boolean = True
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        isNewEntry = False
                    End If
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "UOM_Description", strIUDUomDesc)
                    clsCommon.AddColumnsForChange(coll, "Conversion_Factor", strIUDConvFact)
                    clsCommon.AddColumnsForChange(coll, "Stocking_Unit", strIUDStockingUnit)

                    If isNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", strIUDIemCode)
                        clsCommon.AddColumnsForChange(coll, "UOM_Code", strIUDUomCode)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "Item_Code = '" + strIUDIemCode + "' and uom_code='" + strIUDUomCode + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
            End If

            If isSaved Then
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Imported Successfully ...")
            Else
                trans.Rollback()
                Throw New Exception("Error in Import")
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarHide()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            Me.Controls.Remove(gv)
            Me.Controls.Remove(gv1)
            Me.Controls.Remove(gv2)
            clsCommon.ProgressBarHide()
        End Try
    End Sub

    'Private Sub fndunitofmeasure_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndunitofmeasure.ConnectionString = connectSql.SqlCon()
    '    fndunitofmeasure.Query = "select unit_code as [Unit Of Measure], unit_Desc as [Description]from TSPL_UNIT_MASTER"
    '    fndunitofmeasure.ValueToSelect = "Unit Of Measure"
    '    fndunitofmeasure.Caption = "Detail Of Unit"
    '    fndunitofmeasure.ValueToSelect1 = "Description"
    'End Sub

    Private Sub MasterTemplate_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvclass1.CellValueChanged
        If e.ColumnIndex = 1 Or e.RowIndex >= 0 Then
            Dim strclassdescription As String = connectSql.RunScalar("select inv_class_desc from TSPL_INV_CLASS_DETAILS where inv_class_code = '" + dgvclass1.CurrentRow.Cells(1).Value + "'")
            dgvclass1.CurrentRow.Cells(2).Value = strclassdescription
        End If
        If e.ColumnIndex = 2 Then
            Dim totaldescription As String = ""
            For i As Integer = 0 To dgvclass1.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvclass1.Rows(i).Cells(2).Value) Then
                    totaldescription = totaldescription + " " + dgvclass1.Rows(i).Cells(2).Value
                End If
            Next
            txtdesc1.Text = totaldescription
        End If
        If e.ColumnIndex = 1 Then
            Dim check As String = connectSql.RunScalar("select Inv_Class_Name  from TSPL_INV_CLASS  where Class_Type ='Pack Type' ")
            Dim classcode As String = ""
            For j As Integer = 0 To dgvclass1.Rows.Count - 1
                If Not String.IsNullOrEmpty(dgvclass1.Rows(j).Cells(0).Value) Then
                    If dgvclass1.Rows(j).Cells(0).Value = check Then
                        classcode = dgvclass1.Rows(j).Cells(1).Value
                        Exit For
                    End If
                End If
            Next
            Dim strpack As String = "select Parent, Child  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name = '" + check + "' and Inv_Class_Code = '" + classcode + "'"

            Dim father As String = ""
            Dim child As String = ""
            Dim fathercode As String = Nothing
            Dim mothercode As String = Nothing
            Dim dt As DataTable
            dt = clsDBFuncationality.GetDataTable(strpack)
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    father = dt.Rows(i)("Parent").ToString
                    child = dt.Rows(i)("Child").ToString
                Next
            End If






            'dr = connectSql.RunSqlReturnDR(strpack)
            'Dim father As String
            'Dim child As String
            'Dim fathercode As String
            'Dim mothercode As String
            'If dr.HasRows Then
            '    While dr.Read()
            '        father = dr(0).ToString()
            '        child = dr(1).ToString()
            '    End While
            'End If
            If father = "N" And child = "N" Then
                Dim strchekc As String = "adf"
            ElseIf father = "N" And child = "Y" Then
                mothercode = connectSql.RunScalar("select Mother_Code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "'")
                If String.IsNullOrEmpty(mothercode) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please make before child code")
                End If
            ElseIf father = "Y" And child = "N" Then
                fathercode = connectSql.RunScalar("select father_code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "'")
                If String.IsNullOrEmpty(fathercode) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please make before parent code")
                End If
            ElseIf father = "Y" And child = "Y" Then


                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable("select Mother_Code , Father_Code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "' ")
                If dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        mothercode = dt1.Rows(i)("Mother_Code")
                        fathercode = dt1.Rows(i)("Father_Code")
                    Next

                End If


                'dr = connectSql.RunSqlReturnDR("select Mother_Code , Father_Code  from TSPL_PACKTYPE_MASTER  where Class_Type = '" + check + "' and Finished_Goods = '" + classcode + "' ")
                'If dr.HasRows Then
                '    dr.Read()
                '    mothercode = dr(0)
                '    fathercode = dr(1)
                'End If
                If String.IsNullOrEmpty(fathercode) Or String.IsNullOrEmpty(mothercode) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Parent and child code for this pack type is not defined")
                End If
                Else


                End If
        End If
    End Sub

    Private Sub RadGroupBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox1.Click

    End Sub

    Private Sub RadGroupBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadGroupBox2.Click

    End Sub

    Private Sub chkp2count_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkp2count.ToggleStateChanged

    End Sub

    'Private Sub Finder1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndchapterhead1.ConnectionString = connectSql.SqlCon()
    '    fndchapterhead1.Query = "select chapter_head_code as [Chapter Head Code], Description from tspl_chapter_head"
    '    fndchapterhead1.ValueToSelect = "Chapter Head Code"
    '    fndchapterhead1.ValueToSelect1 = "Description"
    '    fndchapterhead1.Caption = "Chapter Details"
    'End Sub

    Private Sub txtfathercode1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lblskuseq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblskuseq.Click

    End Sub

    Private Sub txtpackseq_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpackseq.KeyPress
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub

    Private Sub txtflavourseq_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtflavourseq.KeyPress
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else

        End If
    End Sub

    Private Sub txtskuseq_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtskuseq.KeyPress
        If e.KeyChar >= Chr(47) And e.KeyChar <= Chr(58) Then
        Else
            e.Handled = True
        End If
        If e.KeyChar = Chr(8) Then
            e.Handled = False
        End If
        If e.KeyChar = Chr(39) Then
            e.Handled = True
        Else


        End If
        If e.KeyChar = Chr(46) Then
            e.Handled = False
        End If
    End Sub

    Private Sub txtfathercode1_TextChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dgvuomdetail_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvuomdetail.CellValueChanged
        If e.ColumnIndex = 0 Then
            Dim str As String = "select Unit_Desc,Conv_Factor  from TSPL_UNIT_MASTER where Unit_Code = '" + dgvuomdetail.CurrentRow.Cells(0).Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)
            Dim strdes As String = "NULL"
            Dim CONV As Decimal = 0
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    strdes = dt.Rows(i)("Unit_Desc").ToString()
                    If Not IsDBNull(dt.Rows(i)("Conv_Factor")) Then
                        CONV = dt.Rows(i)("Conv_Factor")
                    End If
                    dgvuomdetail.CurrentRow.Cells(1).Value = strdes
                    dgvuomdetail.CurrentRow.Cells(2).Value = CONV
                    dgvuomdetail.CurrentRow.Cells(4).Value = 0
                Next
            End If




            'dr = connectSql.RunSqlReturnDR(str)
            'Dim strdes As String = "NULL"
            'Dim CONV As Decimal = 0
            'If dr.HasRows Then
            '    dr.Read()
            '    strdes = dr(0).ToString()
            '    If Not IsDBNull(dr(1)) Then
            '        CONV = dr(1)
            '    End If
            '    dgvuomdetail.CurrentRow.Cells(1).Value = strdes
            '    dgvuomdetail.CurrentRow.Cells(2).Value = CONV
            'End If
        End If
        If e.ColumnIndex = 3 Or e.ColumnIndex = 0 Then
            For i As Integer = 0 To dgvuomdetail.Rows.Count - 1

            Next
            If IsDBNull(dgvuomdetail.CurrentRow.Cells(3).Value) Or Not IsDBNull(dgvuomdetail.CurrentRow.Cells(0).Value) And dgvuomdetail.CurrentRow.Cells(3).Value <> "Yes" Then
                dgvuomdetail.CurrentRow.Cells(3).Value = "No"
            End If
        End If

    End Sub

    Private Sub dgvuomdetail_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles dgvuomdetail.CellEditorInitialized
        If TypeOf Me.dgvuomdetail.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.dgvuomdetail.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Unit_Desc", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub
    Private Sub MasterTemplate_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles dgvuomdetail.CellValidating
        Try

            'If e.ColumnIndex = 4 Then
            '    If Not IsNumeric(e.Value) Then
            '        RadMessageBox.Show("Please enter Numeric value for Weight")
            '        e.Cancel = True
            '    End If
            'End If
            Dim column As GridViewDataColumn = e.Column
            If TypeOf e.Row Is GridViewRowInfo Then
                If column.HeaderText = "UOM Code" Then
                    For Each grow As GridViewDataRowInfo In dgvuomdetail.Rows
                        If e.RowIndex <> grow.Index Then
                            If e.Value = grow.Cells(0).Value Then
                                e.Cancel = True
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    'Private Sub fndcategory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndcategory.Query = "select category_code as [Item Category],category_name as [Description] from tspl_Item_category"
    '    fndcategory.ConnectionString = connectSql.SqlCon()
    '    fndcategory.ValueToSelect = "Item Category"
    '    fndcategory.Caption = "Item Category Details"
    '    fndcategory.ValueToSelect1 = "Description"
    'End Sub

    Private Sub fndItemSubCategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndItemSubCategory._MYValidating
        If clsCommon.myLen(fndcategory.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select 'Item Category' ")
        Else
            Dim Qry As String = "select Sub_Category_Code as [Code], Description  from TSPL_ITEM_SUB_CATEGORY "
            fndItemSubCategory.Value = clsCommon.ShowSelectForm("SubCategorySel", Qry, "Code", "Category_Code='" + fndcategory.Value + "'", fndItemSubCategory.Value, "Code", isButtonClicked)
            fillSub_Item_Category_Desc()
        End If
    End Sub

    Public Sub fillSub_Item_Category_Desc()
        If clsCommon.myLen(fndItemSubCategory.Value) > 0 Then
            txtSubcatdesc.Text = clsDBFuncationality.getSingleValue("Select Description from TSPL_ITEM_SUB_CATEGORY Where Sub_Category_Code='" + fndItemSubCategory.Value + "' ")
        Else
            txtSubcatdesc.Text = ""
        End If
    End Sub

    Private Sub txttolerence_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttolerence.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(46)) Then
        Else
            e.Handled = True
        End If
    End Sub



    Private Sub txttolerence_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttolerence.Leave
        If txttolerence.Text <> "" Then
            Dim num As Decimal = CDec(txttolerence.Text)
            If num > 100 Then
                common.clsCommon.MyMessageBoxShow(Me, "Percentage can not be greater then 100")

                txttolerence.Focus()
                'txttolerence.Text = ""
            End If
        End If
    End Sub
    Private Sub frmItemMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso MyBase.isModifyFlag AndAlso btnreset1.Enabled Then
            funreset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave1.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete1.Enabled Then
            deletedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            closeform()
        End If
    End Sub


    Private Sub txttech_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txttech.KeyPress
        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(46)) Then
        Else
            e.Handled = True
        End If
    End Sub



    Private Sub fndstructurecode1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndstructurecode1._MYValidating
        Dim qry As String = "select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER"
        fndstructurecode1.Value = clsCommon.ShowSelectForm("Item_Masterscod", qry, "Code", "", fndstructurecode1.Value, "", isButtonClicked)
        fungridfill()
        If fndstructurecode1.Value = "" Then
            dgvclass1.ReadOnly = True
        Else
            dgvclass1.ReadOnly = False
        End If
    End Sub

    Private Sub fndpurchaseaccountset1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndpurchaseaccountset1._MYValidating
        Dim qry As String = "select Purchase_Class_Code as [Code], Purchase_Class_Desc as [Description] from dbo.TSPL_PURCHASE_ACCOUNTS"
        fndpurchaseaccountset1.Value = clsCommon.ShowSelectForm("Item_Maasterpac", qry, "Code", "", fndpurchaseaccountset1.Value, "", isButtonClicked)

    End Sub

    Private Sub fndunitofmeasure__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndunitofmeasure._MYValidating
        Dim qry As String = "select unit_code as [Code], unit_desc as [Description] from tspl_unit_master"
        fndunitofmeasure.Value = clsCommon.ShowSelectForm("Item_masteruni", qry, "Code", "", fndunitofmeasure.Value, "", isButtonClicked)
    End Sub

    Private Sub fndsaleaccountset1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndsaleaccountset1._MYValidating
        Dim qry As String = " select Sales_Class_Code as [SalesAccountSet], Sales_Class_Desc as [Description] from TSPL_SALES_ACCOUNTS"
        fndsaleaccountset1.Value = clsCommon.ShowSelectForm("Item_Masteracc", qry, "SalesAccountSet", "", fndsaleaccountset1.Value, "", isButtonClicked)
    End Sub

    Private Sub fndchapterhead1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndchapterhead1._MYValidating
        Dim qry As String = "select chapter_head_code as [ChapterHeadCode], Description from tspl_chapter_head"
        fndchapterhead1.Value = clsCommon.ShowSelectForm("Item_Masterchap", qry, "ChapterHeadCode", "", fndchapterhead1.Value, "", isButtonClicked)
    End Sub

    Private Sub fndcategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcategory._MYValidating
        Dim qry As String = "select category_code as [ItemCategory],category_name as [Description] from tspl_Item_category"
        fndcategory.Value = clsCommon.ShowSelectForm("Item_Mastercat", qry, "ItemCategory", "", fndcategory.Value, "", isButtonClicked)
        LoadCategoryData()
    End Sub
    Public Sub LoadCategoryData()
        Try
            Dim strquery As String = "select category_code as [Item Category],category_name as [Description] from tspl_Item_category where category_code ='" + fndcategory.Value + "'"
            'Dim dr1 As SqlDataReader
            Dim strvalue As String = clsDBFuncationality.getSingleValue(strquery)


            'Dim dr1 As SqlDataReader
            'Dim strvalue As String
            'dr1 = connectSql.RunSqlReturnDR(strquery)
            'While dr1.Read()
            '    strvalue = dr1(0).ToString()
            'End While
            If strvalue <> "" Then
                funfillcategory()
            Else
                txtcatdesc.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub frmitemcode1__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles frmitemcode1._MYValidating
        Dim str As String = "select count(*) from tspl_item_master where item_code ='" + frmitemcode1.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            frmitemcode1.MyReadOnly = False
        Else
            frmitemcode1.MyReadOnly = True
        End If
        If frmitemcode1.MyReadOnly OrElse isButtonClicked Then

            Dim qry As String = "select item_code as [Code], item_desc as [Description] from tspl_item_master   "
            frmitemcode1.Value = clsCommon.ShowSelectForm("Item_MasterFinder", qry, "Code", " Item_Type in ('F','P','T')", frmitemcode1.Value, "", isButtonClicked)
            'frmitemcode1.Value = clsItemMaster.getFinder(" Item_Type in ('F','P','T')", frmitemcode1.Value, isButtonClicked)
            LoadData()
            frmitemcode1.MyReadOnly = True
        End If
    End Sub
    Public Sub LoadData()
        Dim str As String = "select item_code from tspl_item_master where item_code= '" + frmitemcode1.Value + "'"
        '  dr = connectSql.RunSqlReturnDR(str)
        Dim stritemcode As String = connectSql.RunScalar(str)
        ' While dr.Read()
        'stritemcode = dr(0).ToString()
        ' End While
        If stritemcode <> "" Then
            funfill()
            ' fndstructurecode1.Enabled = False
            dgvclass1.ReadOnly = True

        End If
    End Sub

    Private Sub frmitemcode1__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles frmitemcode1._MYNavigator
        Dim qst As String = "select item_code as [Code], item_desc as [Description] from tspl_item_master where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and Item_Type in ('F','P','T') and  tspl_item_master .item_code in ('" + frmitemcode1.Value + "') "
            Case NavigatorType.Next
                qst += " and tspl_item_master .item_code in (select min(item_code ) from tspl_item_master where item_code  >'" + frmitemcode1.Value + "' and Item_Type in ('F','P','T')) "
            Case NavigatorType.First
                qst += " and tspl_item_master .item_code in (select MIN(item_code ) from tspl_item_master  where Item_Type in ('F','P','T')) "

            Case NavigatorType.Last
                qst += " and tspl_item_master .item_code in (select Max(item_code ) from tspl_item_master where Item_Type in ('F','P','T') )"
            Case NavigatorType.Previous
                qst += " and tspl_item_master .item_code in (select Max(item_code ) from tspl_item_master where item_code  <'" + frmitemcode1.Value + "' and Item_Type in ('F','P','T') )"


        End Select
        ' qst += " and item_type <> 'R'and item_type <> 'O'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            frmitemcode1.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtdesc1.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadData()
        frmitemcode1.MyReadOnly = True
    End Sub
   
    Private Sub dgvuomdetail_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles dgvuomdetail.UserDeletingRow
        Dim UomCode As String = dgvuomdetail.CurrentRow.Cells(0).Value
        Dim ShipUomCode As String = clsDBFuncationality.getSingleValue("select Unit_code  from TSPL_SHIPMENT_DETAILS where Unit_code ='" & UomCode & "'")
        Dim TransUomCode As String = clsDBFuncationality.getSingleValue("select Uom  from TSPL_TRANSFER_DETAIL where  Uom  ='" & UomCode & "'")
        If clsCommon.myLen(ShipUomCode) > 0 Or clsCommon.myLen(TransUomCode) > 0 Then
            common.clsCommon.MyMessageBoxShow(Me, " This record can't be deleted.It is used in another process.")
            funfill()
            dgvclass1.ReadOnly = True
            Exit Sub
        Else

        End If
    End Sub

    Private Sub rmiExportItemdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExportItemdetails.Click
        Try
            Dim strCmd As String = " SELECT '' as [Structure Code], '' as [Flavour], '' as  [Size], '' as [Category], '' as [Pack], '' as [Purchase A/C Set], '' as [Sale A/C Set], '' as [UOM], '' as [Default Price], '' as [Parent], '' as [Parent Qty], '' as [Child], '' as [Child Qty], '' as [Chapter Heads], '' as [Opng balance], '' as [Two Count], '' as [Three Count], '' as [Serve Type], '' as [Mfg Date], '' as [Batch No], '' as [Exp Date], '' as [Item Type], '' as [Flavour Seq], '' as [Pack Seq], '' as [SKU Seq], '' as [Show], '' as [Item Category], '' as [Item Sub Category], '' as [Tolerence], '' as [Tech Self Life], '' as [Cost], '' as [Type Of Item], '' as [NoMrp], '' as[Morning]"

            transportSql.ExporttoExcel(strCmd, Me)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Item")
        End Try
    End Sub

    Private Sub rmiExportNewUomDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiExportNewUomDetails.Click
        Try
            Dim strCmd As String = " SELECT ''as [Item Code], '' as [UOM], 0 as [Conversion Factor], 'Y/N' as [Stocking Unit], 0 as Weight "

            transportSql.ExporttoExcel(strCmd, Me)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "UOM")
        End Try
    End Sub

    Private Sub rmiImportItemDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImportItemDetails.Click
        Dim gv1 As New RadGridView()
        Dim isSaved As Boolean = True
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim ii As Integer = 0
        Try
            ''-Insertig for ITem MAster
            ' as [Structure Code], '' as [Flavour], '' as  [Size], '' as [Category], '' as [Pack], '' as [Purchase A/C Set], '' as [Sale A/C Set], '' as [UOM], '' as [' as [Parent Qty], [Child], [Child Qty], [Chapter Heads], [Opng balance], [Two Count], [Three Count], [Serve Type], [Mfg Date], [Batch No], [Exp Date], [Item Type], [Flavour Seq], [Pack Seq], [SKU Seq], [Show], [Item_Category], [Item Sub Category], [Tolerence], [Tech Self Life], [Cost], [Type Of Item], [NoMrp], [Morning]"
            If transportSql.importExcel(gv1, "Structure Code", "Flavour", "Size", "Category", "Pack", "Purchase A/C Set", "Sale A/C Set", "UOM", "Default Price", "Parent", "Parent Qty", "Child", "Child Qty", "Chapter Heads", "Opng balance", "Two Count", "Three Count", "Serve Type", "Mfg Date", "Batch No", "Exp Date", "Item Type", "Flavour Seq", "Pack Seq", "SKU Seq", "Show", "Item Category", "Item Sub Category", "Tolerence", "Tech Self Life", "Cost", "Type Of Item", "NoMrp", "Morning") Then

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / gv1.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
                    Dim coll As New Hashtable()

                    Dim strStructureCode As String = ""                                                  '----Structure Code------
                    Dim StructureCode As String = clsCommon.myCstr(grow.Cells("Structure Code").Value)
                    If clsCommon.myLen(StructureCode) > 0 Then
                        strStructureCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Structure_Code from TSPL_STRUCTURE_MASTER Where Structure_Code='" + StructureCode + "'", trans))
                        If clsCommon.CompairString(strStructureCode, StructureCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Structure_Code", strStructureCode)
                        Else
                            Throw New Exception("The Structure Code '" + StructureCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Structure Code at Line No '" + LineNo + "'")
                    End If

                    Dim strStuctDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Structure_Descq  from TSPL_STRUCTURE_MASTER Where Structure_Code='" + strStructureCode + "'", trans))  '------Structure description-------------
                    clsCommon.AddColumnsForChange(coll, "Structure_Desc", strStuctDesc)

                    '----------------item Code---------------------------------------
                    Dim strFlavour As String
                    Dim Flavour As String = clsCommon.myCstr(grow.Cells("Flavour").Value)
                    If clsCommon.myLen(Flavour) > 0 Then
                        strFlavour = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Flavour' AND inv_class_code='" + Flavour + "'", trans))
                        If Not clsCommon.CompairString(Flavour, strFlavour) = CompairStringResult.Equal Then
                            Throw New Exception("The flavour '" + Flavour + "' at Line No '" + LineNo + "' does not exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Flavour At Line No '" + LineNo + "'")
                    End If

                    Dim strSize As String
                    Dim Size As String = clsCommon.myCstr(grow.Cells("Size").Value)
                    If clsCommon.myLen(Size) > 0 Then
                        strSize = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Size' AND Inv_Class_Code='" + Size + "'", trans))
                        If clsCommon.myLen(strSize) <= 0 Then
                            Throw New Exception("The Size '" + Size + "' at Line No '" + LineNo + "' does not exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Size At Line No '" + LineNo + "'")
                    End If

                    Dim strcategory As String
                    Dim Category As String = clsCommon.myCstr(grow.Cells("Category").Value)
                    If clsCommon.myLen(Category) > 0 Then
                        strcategory = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Category' AND inv_class_code='" + Category + "'", trans))
                        If Not clsCommon.CompairString(Category, strcategory) = CompairStringResult.Equal Then
                            Throw New Exception("The Category '" + Category + "' at Line No '" + LineNo + "' does not exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Category At Line No '" + LineNo + "'")
                    End If

                    Dim strPack As String
                    Dim Pack As String = clsCommon.myCstr(grow.Cells("Pack").Value)
                    If clsCommon.myLen(Pack) > 0 Then
                        strPack = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select inv_class_code from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Pack' AND inv_class_code='" + Pack + "'", trans))
                        If Not clsCommon.CompairString(Pack, strPack) = CompairStringResult.Equal Then
                            Throw New Exception("The Pack '" + Pack + "' at Line No '" + LineNo + "' does not exist")
                        End If
                    Else
                        Throw New Exception("Please Insert Pack At Line No '" + LineNo + "'")
                    End If

                    Dim ItemCode As String = strFlavour + strSize + strcategory + strPack   '-------Generated Item Code-----------------------
                    clsCommon.AddColumnsForChange(coll, "Item_Code", ItemCode)
                    '----------------------------------------------------------------------------

                    '---------------Item Description-------------------------
                    Dim FlavourDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Class_Desc  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Flavour' AND inv_class_code='" + strFlavour + "'", trans))
                    Dim SizeDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Class_Desc  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Size' AND inv_class_code='" + strSize + "'", trans))
                    Dim CategoryDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Class_Desc  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Category' AND inv_class_code='" + strcategory + "'", trans))
                    Dim packDesc As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Inv_Class_Desc  from TSPL_INV_CLASS_DETAILS where Inv_Class_Name='Pack' AND inv_class_code='" + strPack + "'", trans))
                    Dim ItemDesc As String = FlavourDesc + " " + SizeDesc + " " + CategoryDesc + " " + packDesc
                    clsCommon.AddColumnsForChange(coll, "Item_Desc", ItemDesc)
                    '--------------------------------------------------------

                    Dim P_ACSet As String = clsCommon.myCstr(grow.Cells("Purchase A/C Set").Value)      '---purchase A/c Code
                    If clsCommon.myLen(P_ACSet) > 0 Then
                        Dim strP_ACSet As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Purchase_Class_Code  from TSPL_PURCHASE_ACCOUNTS Where Purchase_Class_Code='" + P_ACSet + "'", trans))
                        If clsCommon.CompairString(P_ACSet, strP_ACSet) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Purchase_Class_Code", strP_ACSet)
                        Else
                            Throw New Exception("The Purchse A/C Set '" + P_ACSet + "' at Line No '" + LineNo + "' does not exist ")
                        End If
                    Else
                        Throw New Exception("Please Insert Purchase A/C Set in Line No '" + LineNo + "'")
                    End If

                    Dim S_ACSet As String = clsCommon.myCstr(grow.Cells("Sale A/C Set").Value)          '-----Sales A/c Code
                    If clsCommon.myLen(S_ACSet) > 0 Then
                        Dim strS_ACSet As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Sales_Class_Code   from TSPL_SALES_ACCOUNTS Where Sales_Class_Code='" + S_ACSet + "'", trans))
                        If clsCommon.CompairString(S_ACSet, strS_ACSet) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Sale_Class_Code", strS_ACSet)
                        Else
                            Throw New Exception("The Sale A/C Set '" + S_ACSet + "' at Line No '" + LineNo + "' does not exist ")
                        End If
                    Else
                        Throw New Exception("Please Insert Sale A/C Set in Line No '" + LineNo + "'")
                    End If

                    Dim strUOM As String                                                                '-------Unit Code------
                    Dim UnitCode As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(UnitCode) > 0 Then
                        strUOM = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans)
                        If clsCommon.CompairString(strUOM, UnitCode) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Unit_Code", strUOM)
                        Else
                            Throw New Exception("The UOM '" + UnitCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert UOM at Line No '" + LineNo + "' ")
                    End If
                    '-----------------------Default Price--------------------------------
                    Dim DfltPrice As Double = clsCommon.myCdbl(grow.Cells("Default Price").Value)
                    clsCommon.AddColumnsForChange(coll, "Deafult_Price", DfltPrice)
                    '----------------------------------------------------------------------

                    '---------------Generation Of father and Mother COde----------------Parent------
                    Dim strfathercode As String = clsCommon.myCstr(grow.Cells("Parent").Value)
                    clsCommon.AddColumnsForChange(coll, "Father_Code", strfathercode)           '-----father Code

                    Dim strmothercode As String = clsCommon.myCstr(grow.Cells("Child").Value)
                    clsCommon.AddColumnsForChange(coll, "Mother_Code", strmothercode)           '--MOther Code-------
                    '-------------------------------------------------------------------------------

                    Dim ParentQty As Double = clsCommon.myCdbl(grow.Cells("Parent Qty").Value)
                    clsCommon.AddColumnsForChange(coll, "Father_Qty", ParentQty)                '-----father Qty

                    Dim ChildQty As Double = clsCommon.myCdbl(grow.Cells("Child Qty").Value)
                    clsCommon.AddColumnsForChange(coll, "Mother_Qty", ChildQty)                 '-----Mother Qty

                    '--------------------------Chapter Head-----------------------------------------
                    Dim strChapterHead As String = clsCommon.myCstr(grow.Cells("Chapter Heads").Value)
                    Dim ChapterHead As String
                    If clsCommon.myLen(strChapterHead) > 0 Then
                        ChapterHead = clsDBFuncationality.getSingleValue("Select Chapter_Head_Code from TSPL_CHAPTER_HEAD WHERE Chapter_Head_Code='" + strChapterHead + "'", trans)
                        If clsCommon.CompairString(ChapterHead, strChapterHead) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", ChapterHead)
                        Else
                            Throw New Exception("The Chapter Head '" + UnitCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Cheapter_Heads", "")
                    End If
                    '-------------------------------------------------------------------------------
                    '-------------------------Openning Balance--------------------------------------
                    Dim Opngbal As Double = clsCommon.myCdbl(grow.Cells("Opng balance").Value)
                    clsCommon.AddColumnsForChange(coll, "Opening_Balance", Opngbal)
                    '-------------------------------------------------------------------------------
                    '------------------------two Count Status---------------------------------------
                    Dim TwoCountStatus As String = clsCommon.myCstr(grow.Cells("Two Count").Value)  '------Two Count Status
                    If clsCommon.CompairString(TwoCountStatus, "Y") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Two_Count_Status", "Y")
                    ElseIf clsCommon.CompairString(TwoCountStatus, "N") = CompairStringResult.Equal Or clsCommon.CompairString(TwoCountStatus, " ") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Two_Count_Status", "N")
                    Else
                        Throw New Exception("Please Enter Two Count Status as 'Y' Or 'N' Or Leave Blank at Line No '" + LineNo + "'")
                    End If
                    '-------------------------------------------------------------------------------
                    '-------------------------Three Count Status------------------------------------
                    Dim ThreeCountStatus As String = clsCommon.myCstr(grow.Cells("Three Count").Value)  '---Three Count Status
                    If clsCommon.CompairString(ThreeCountStatus, "Y") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Three_Count_Status", "Y")
                    ElseIf clsCommon.CompairString(ThreeCountStatus, "N") = CompairStringResult.Equal Or clsCommon.CompairString(ThreeCountStatus, " ") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Three_Count_Status", "N")
                    Else
                        Throw New Exception("Please Enter Three Count Status as 'Y' Or 'N' Or Leave Blank at Line No '" + LineNo + "'")
                    End If
                    '-------------------------------------------------------------------------------
                    '--------------------------Server Type------------------------------------------
                    Dim StrServeType As String = clsCommon.myCstr(grow.Cells("Server Type"))
                    If clsCommon.myLen(StrServeType) > 50 Then
                        Throw New Exception("Please Check the length of Serve type At Line No '" + LineNo + "'")
                    End If
                    '-------------------------------------------------------------------------------
                    '---------------------------Mfg date--------------------------------------------
                    Dim MfgDate As String
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Mfg Date").Value)) >= 10 Then
                        MfgDate = clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("Mfg Date").Value), "dd/MM/yyyy")
                    Else
                        Throw New Exception("Please Insert Mfg Date In Format(dd/MM/yyyy) At Line No '" + LineNo + "'")
                    End If
                    clsCommon.AddColumnsForChange(coll, "Mfg_Date", clsCommon.GetPrintDate(MfgDate, "dd/MMM/yyyy"))
                    '-------------------------------------------------------------------------------

                    Dim strbatchNo As String = clsCommon.myCstr(grow.Cells("Batch No").Value)
                    clsCommon.AddColumnsForChange(coll, "Batch_No", strbatchNo)                         '----Batch No------

                    '---------------------------Best Before Use--------------------------------------------
                    'If clsCommon.myLen(clsCommon.myCstr(grow.Cells("Exp Date").Value)) >= 10 Then
                    '    Dim Expdate As String = clsCommon.GetPrintDate(clsCommon.myCstr(grow.Cells("Exp Date").Value), "dd/MM/yyyy")
                    'Else
                    '    Throw New Exception("Please Insert Exp  Date In Format(dd/MM/yyyy) At Line No '" + LineNo + "'")
                    'End If
                    '-------------------------------------------------------------------------------

                    clsCommon.AddColumnsForChange(coll, "Item_Type", "F")                               '-----Item Type='F'--Default

                    Dim flavourSeq As Double = clsCommon.myCdbl(grow.Cells("Flavour Seq").Value)
                    clsCommon.AddColumnsForChange(coll, "Flavour_Seq", flavourSeq)                     '------Flavour sequence----

                    Dim packSeq As Double = clsCommon.myCdbl(grow.Cells("Pack Seq"))
                    clsCommon.AddColumnsForChange(coll, "pack_Seq", packSeq)                            '---------pack Sequence----

                    Dim SKUSeq As Double = clsCommon.myCdbl(grow.Cells("SKU Seq").Value)
                    clsCommon.AddColumnsForChange(coll, "SKU_Seq", SKUSeq)                              '--------SKU Sequence------
                    '---------------------------------------Show-----------------------------------------
                    Dim strShow As String = clsCommon.myCstr(grow.Cells("Show").Value)
                    If clsCommon.CompairString(strShow, "Y") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Show", "Y")
                    ElseIf clsCommon.CompairString(strShow, "N") = CompairStringResult.Equal Or clsCommon.CompairString(strShow, "") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Show", "N")
                    Else
                        Throw New Exception("Please Enter Show as 'Y' Or 'N' Or Leave Blank at Line No '" + LineNo + "'")
                    End If
                    '------------------------------------------------------------------------------------
                    '--------------------------------------NO Mrp----------------------------------------
                    Dim strNoMrp As String = clsCommon.myCstr(grow.Cells("NoMrp").Value)
                    If clsCommon.CompairString(strNoMrp, "Y") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "NoMrp", "1")
                    ElseIf clsCommon.CompairString(strNoMrp, "N") = CompairStringResult.Equal Or clsCommon.CompairString(strNoMrp, "") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "NoMrp", "0")
                    Else
                        Throw New Exception("Please Enter NoMrp as 'Y' Or 'N' Or Leave Blank at Line No '" + LineNo + "'")
                    End If
                    '------------------------------------------------------------------------------------
                    '--------------------------------------morning---------------------------------------
                    Dim strMorning As String = clsCommon.myCstr(grow.Cells("Morning").Value)
                    If clsCommon.CompairString(strMorning, "Y") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Morning", "1")
                    ElseIf clsCommon.CompairString(strMorning, "N") = CompairStringResult.Equal Or clsCommon.CompairString(strMorning, "") = CompairStringResult.Equal Then
                        clsCommon.AddColumnsForChange(coll, "Morning", "0")
                    Else
                        Throw New Exception("Please Enter Morning as 'Y' Or 'N' Or Leave Blank at Line No '" + LineNo + "'")
                    End If
                    '------------------------------------------------------------------------------------
                    '-------------------------------------Item category----------------------------------
                    Dim strItemCategory As String
                    Dim Itemcategory As String = clsCommon.myCstr(grow.Cells("Item Category").Value)
                    If clsCommon.myLen(Itemcategory) > 0 Then
                        strItemCategory = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Category_Code  from TSPL_Item_Category Where Category_Code='" + Itemcategory + "'", trans))
                        If clsCommon.CompairString(strItemCategory, Itemcategory) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Item_Category", strItemCategory)
                        Else
                            Throw New Exception("The Item category '" + Itemcategory + "' Does Not Exist at Line No " + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please Insert Item category at Line No '" + LineNo + "'")
                    End If
                    '--------------------------------------------------------------------------------------
                    '------------------------------Item Sub Category---------------------------------------
                    Dim strItemSubCategory As String
                    Dim ItemSubcategory As String = clsCommon.myCstr(grow.Cells("Item Sub Category").Value)
                    If clsCommon.myLen(ItemSubcategory) > 0 Then
                        strItemSubCategory = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Sub_Category_Code  from TSPL_ITEM_SUB_CATEGORY Where Category_Code='" + strItemCategory + "' and Sub_Category_Code='" + ItemSubcategory + "'", trans))
                        If clsCommon.CompairString(strItemSubCategory, ItemSubcategory) = CompairStringResult.Equal Then
                            clsCommon.AddColumnsForChange(coll, "Sub_Item_Category", strItemSubCategory)
                        Else
                            Throw New Exception("The Item Sub category '" + ItemSubcategory + "' Does Not Exist at Line No " + LineNo + "'")
                        End If
                    Else
                        Throw New Exception("Please Insert Item Sub category at Line No '" + LineNo + "'")
                    End If
                    '----------------------------------------------------------------------------------------

                    Dim tollerence As Double = clsCommon.myCdbl(grow.Cells("Tolerence").Value)
                    clsCommon.AddColumnsForChange(coll, "tolerence", tollerence)                             '----Tolerence------

                    Dim techSelfLife As String = clsCommon.myCstr(grow.Cells("tech Self Life").Value)
                    clsCommon.AddColumnsForChange(coll, "tech_shelf_life", techSelfLife)                           '----Tech Self Life------

                    Dim Cost As Double = clsCommon.myCdbl(grow.Cells("Cost").Value)
                    clsCommon.AddColumnsForChange(coll, "Cost", Cost)                                             '----Cost------

                    '----------------------------------type Of Item-------------------------------------------
                    Dim typeOfItem As String = clsCommon.myCstr(grow.Cells("Type Of Item").Value)
                    If clsCommon.CompairString(typeOfItem, "A") = CompairStringResult.Equal Then
                        typeOfItem = "A"
                    ElseIf clsCommon.CompairString(typeOfItem, "B") = CompairStringResult.Equal Then
                        typeOfItem = "B"
                    ElseIf clsCommon.CompairString(typeOfItem, "C") = CompairStringResult.Equal Then
                        typeOfItem = "C"
                    Else
                        typeOfItem = " "
                    End If
                    clsCommon.AddColumnsForChange(coll, "TypeOfItm", typeOfItem)

                    '------------------------------------------------------------------------------------------
                    Dim strModifyBy As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modify_By", strModifyBy)
                    Dim strmodifyDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                    clsCommon.AddColumnsForChange(coll, "Modify_Date", strmodifyDate)
                    Dim CompCode As String = clsCommon.myCstr(objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", CompCode)

                    Dim strCreatedBy As String = clsCommon.myCstr(objCommonVar.CurrentUserCode)
                    Dim strCreatedDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")

                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_ITEM_MASTER Where Item_Code='" + ItemCode + "'", trans))
                    If Count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", strCreatedBy)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", strCreatedDate)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_MASTER", OMInsertOrUpdate.Update, "Item_Code='" + ItemCode + "'", trans)
                    End If
                    '---------------------Insert data In TSPL_ITEM_DETAIL-----------------------
                    Dim dtItemDetail As DataTable = clsDBFuncationality.GetDataTable("Select Inv_Class from TSPL_STRUCTURE_DETAIL Where Structure_Code='" + StructureCode + "' ", trans)
                    If dtItemDetail.Rows.Count > 0 Then
                        For Each dr As DataRow In dtItemDetail.Rows
                            Dim Classname As String = clsCommon.myCstr(dr("Inv_Class"))
                            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_ITEM_DETAILS Where Item_Code='" + ItemCode + "' AND Class_Name='" + Classname + "'", trans)
                            Dim col1 As New Hashtable
                            clsCommon.AddColumnsForChange(col1, "Item_Code", ItemCode)
                            Dim ClassCode As String = ""
                            Dim ClassDesc As String = ""
                            If clsCommon.CompairString(Classname, "Flavour") = CompairStringResult.Equal Then
                                ClassCode = strFlavour
                                ClassDesc = FlavourDesc
                            ElseIf clsCommon.CompairString(Classname, "Size") = CompairStringResult.Equal Then
                                ClassCode = strSize
                                ClassDesc = SizeDesc
                            ElseIf clsCommon.CompairString(Classname, "Category") = CompairStringResult.Equal Then
                                ClassCode = strcategory
                                ClassDesc = CategoryDesc
                            ElseIf clsCommon.CompairString(Classname, "Pack") = CompairStringResult.Equal Then
                                ClassCode = strPack
                                ClassDesc = packDesc
                            End If
                            clsCommon.AddColumnsForChange(col1, "Class_Code", ClassCode)
                            clsCommon.AddColumnsForChange(col1, "Class_Name", Classname)
                            clsCommon.AddColumnsForChange(col1, "Class_Desc", ClassDesc)
                            clsCommon.AddColumnsForChange(col1, "Created_By", strCreatedBy)
                            clsCommon.AddColumnsForChange(col1, "Created_Date", strCreatedDate)
                            clsCommon.AddColumnsForChange(col1, "Modify_By", strModifyBy)
                            clsCommon.AddColumnsForChange(col1, "Modify_date", strmodifyDate)
                            clsCommon.AddColumnsForChange(col1, "Comp_Code", CompCode)
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(col1, "TSPL_ITEM_DETAILS", OMInsertOrUpdate.Insert, "", trans)
                        Next
                    End If
                    '---------------------------------------------------------------------------
                Next
                If isSaved Then
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    RadMessageBox.Show("Data Imported Successfully ...")
                Else
                    Throw New Exception("Error in Import")
                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()

            RadMessageBox.Show(ex.Message)
        Finally
            Me.Controls.Remove(gv1)
            'clsCommon.ProgressBarHide()
        End Try

    End Sub


    Private Sub rmiImportItemUOMDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiImportItemUOMDetails.Click
        Dim gv1 As New RadGridView()
        Dim isSaved As Boolean = True
        Me.Controls.Add(gv1)
        Dim currentdate As Date = Date.Today
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim ii As Integer = 0
        Try
            ''-Insertig for ITem MAster
            If transportSql.importExcel(gv1, "Item Code", "UOM", "Conversion Factor", "Stocking Unit", "Weight") Then
                clsCommon.ProgressBarPercentShow()
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim LineNo As String = clsCommon.myCstr(grow.Index) + 2
                    ii += 1
                    clsCommon.ProgressBarPercentUpdate((ii * 100) / gv1.RowCount - 1, "Importing " + clsCommon.myCstr(ii) + "/" + clsCommon.myCstr(gv1.RowCount - 1))
                    Dim coll As New Hashtable()

                    Dim strItemCode As String                                                           '-----Item COde-------
                    Dim itemCode As String = clsCommon.myCstr(grow.Cells("Item Code").Value)
                    If clsCommon.myLen(itemCode) >= 0 Then
                        strItemCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Item_Code from TSPL_ITEM_MASTER Where Item_Code='" + itemCode + "'", trans))
                        If clsCommon.myLen(strItemCode) <= 0 Then
                            Throw New Exception("The Item '" + itemCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        Else
                            'clsCommon.AddColumnsForChange(coll, "Item_Code", strItemCode)
                        End If
                    Else
                        Throw New Exception("Please Insert Item Code at Line No '" + LineNo + "' ")
                    End If

                    Dim strUOM As String                                                                '-------Unit Code------
                    Dim UnitCode As String = clsCommon.myCstr(grow.Cells("UOM").Value)
                    If clsCommon.myLen(UnitCode) > 0 Then
                        strUOM = clsDBFuncationality.getSingleValue("Select Unit_Code from TSPL_UNIT_MASTER Where Unit_Code='" + UnitCode + "'", trans)
                        If clsCommon.CompairString(strUOM, UnitCode) = CompairStringResult.Equal Then
                            'clsCommon.AddColumnsForChange(coll, "UOM_Code", strUOM)
                        Else
                            Throw New Exception("The UOM '" + UnitCode + "' at Line No '" + LineNo + "' Does Not Exist")
                        End If
                    Else
                        Throw New Exception("Please Insert UOM at Line No '" + LineNo + "' ")
                    End If

                    '------------UOM DESCRIPTION--------------------
                    Dim UOM_Description = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Unit_Desc from TSPL_UNIT_MASTER Where Unit_Code='" + strUOM + "'", trans))
                    clsCommon.AddColumnsForChange(coll, "UOM_Description", UOM_Description)
                    '----------------------------------------------

                    If clsCommon.myCstr(grow.Cells("Stocking Unit").Value) = "Y" Then
                        clsCommon.AddColumnsForChange(coll, "Stocking_Unit", "Y")
                    Else
                        clsCommon.AddColumnsForChange(coll, "Stocking_Unit", "N")
                    End If

                    '  Dim strconFactor As String                                                                '-------Unit Code------
                    Dim ConversionFactor As Double = clsCommon.myCdbl(grow.Cells("Conversion Factor").Value)
                    If ConversionFactor > 0 Then
                        clsCommon.AddColumnsForChange(coll, "COnversion_Factor", ConversionFactor)
                    Else
                        Throw New Exception("Please Insert Convrsion Factor at Line No '" + LineNo + "' ")
                    End If

                    If clsCommon.myLen(grow.Cells("Weight").Value) > 0 Then
                        If Not IsNumeric(grow.Cells("Weight").Value) Then
                            Throw New Exception("Please insert decimal data in Weight at Line No '" + LineNo + "' ")
                        Else
                            clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight").Value))
                        End If
                    Else
                        clsCommon.AddColumnsForChange(coll, "Weight", clsCommon.myCdbl(grow.Cells("Weight").Value))
                    End If

                    Dim Count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Count(*) from TSPL_ITEM_UOM_DETAIL Where Item_Code='" + itemCode + "' AND UOM_Code='" + strUOM + "'", trans))
                    If Count <= 0 Then
                        clsCommon.AddColumnsForChange(coll, "Item_Code", itemCode)
                        clsCommon.AddColumnsForChange(coll, "UOM_Code", strUOM)
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        Dim whrClas As String = "Item_Code = '" + itemCode + "' and uom_code='" + strUOM + "'"
                        isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_ITEM_UOM_DETAIL", OMInsertOrUpdate.Update, whrClas, trans)
                    End If
                Next
                If isSaved Then
                    trans.Commit()
                    clsCommon.ProgressBarPercentHide()
                    RadMessageBox.Show("Data Imported Successfully ...")
                Else
                    Throw New Exception("Error in Import")
                End If
            End If

        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            RadMessageBox.Show(ex.Message)
        Finally
            Me.Controls.Remove(gv1)
            'clsCommon.ProgressBarHide()
        End Try
    End Sub

    Private Sub fndProdItemCategory__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndProdItemCategory._MYValidating
        Dim qry As String = "select PROD_ITEM_CATEGORY_CODE as [prodItemCategory],PROD_ITEM_CATEGORY_NAME as [Description] from TSPL_MF_PRODUCTION_ITEM_CATEGORY"
        fndProdItemCategory.Value = clsCommon.ShowSelectForm("TSPL_MF_PRODUCTION_ITEM_CATEGORY", qry, "prodItemCategory", "", fndProdItemCategory.Value, "", isButtonClicked)
        txtProdItemCategoryName.Text = clsDBFuncationality.getSingleValue("select PROD_ITEM_CATEGORY_NAME as [Description] from TSPL_MF_PRODUCTION_ITEM_CATEGORY where PROD_ITEM_CATEGORY_CODE='" & fndProdItemCategory.Value & "'")

    End Sub
End Class
