'Created By---> Mayank
'Created Date--->30/jun/2011
'Modified By--> mayank
'Last Modify Date-->30/june/2011
'Tables Used-->TSPL_ITEM_LOCATION_DETAILS
Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Public Class frmItemLocationDetails
    Inherits FrmMainTranScreen
    Dim userCode, companyCode As String
    Dim query As String=""
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.ItemLocationDetails)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        'btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmItemLocationDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub FrmItemLocationDetails_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ' globalFunc.mandatoryText(fnditemCode.Value )
        AddHandler fnditemCode.TextChanged, AddressOf fnditemCode_TextChanged
        AddHandler fnditemCode.Leave, AddressOf fnditemCode_TextLeave
        AddHandler fnditemCode.KeyPress, AddressOf fnditemCode_KeyPress
        fnditemCode.MyCharacterCasing = CharacterCasing.Upper
        grdv.ReadOnly = True

        applyNLevelCategorySetting()

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'Private Sub fnditemCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fnditemCode.ConnectionString = connectSql.SqlCon()
    '    fnditemCode.Query = "select distinct Item_Code as [Item Code],Item_Desc as [Item Desc] from TSPL_ITEM_LOCATION_DETAILS"
    '    fnditemCode.ValueToSelect = "Item Code"
    '    fnditemCode.Caption = "Location Master"
    '    fnditemCode.txtValue.MaxLength = 50
    '    fnditemCode.ValueToSelect1 = "Item Desc"
    'End Sub
    Public Sub fnditemCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim strItem_Code As String = "select Item_Code from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + fnditemCode.Value + "'"
        'Dim dr As SqlDataReader
        Dim strvalue As String
        strvalue = clsDBFuncationality.getSingleValue(strItem_Code)
        'If dr.Read() Then
        '    strvalue = dr(0).ToString()
        'End If
        If (strvalue <> "") Then
            funfill()

        Else
            grdv.DataSource = Nothing
            grdv.Rows.Clear()
            txtDescription.Text = ""
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub funfill()
        Try
            grdv.AutoGenerateColumns = False
            grdv.DataSource = Nothing
            grdv.Rows.Clear()
            Dim strQuery As String = "select Item_Desc, TSPL_LOCATION_MASTER.Location_Code, TSPL_LOCATION_MASTER.Location_Desc,MRP,Batch_No, Item_Qty, Amount from TSPL_ITEM_LOCATION_DETAILS LEFT OUTER JOIN dbo.TSPL_LOCATION_MASTER ON TSPL_ITEM_LOCATION_DETAILS.Location_Code=dbo.TSPL_LOCATION_MASTER.Location_Code WHERE TSPL_LOCATION_MASTER.Location_Type<>'Logical' AND Item_Code='" + fnditemCode.Value + "'"
            Dim da As New SqlDataAdapter(strQuery, connectSql.SqlCon)
            Dim dt As New DataTable()
            da.Fill(dt)
            ' txtDescription.Text =dt.
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim j As GridViewRowInfo = grdv.Rows.AddNew()
                    'Dim total As Decimal
                    Dim qty As Decimal
                    Dim amt As Decimal
                    txtDescription.Text = dt.Rows(i)(0).ToString()
                    j.Cells(0).Value = dt.Rows(i)(1).ToString()
                    j.Cells(1).Value = dt.Rows(i)(2).ToString()

                    'If qty = 0 Then
                    '    total = 0
                    'Else
                    '    total = amt / qty
                    'End If
                    j.Cells(2).Value = dt.Rows(i)(3)
                    j.Cells(3).Value = dt.Rows(i)(4)
                    qty = dt.Rows(i)(5)
                    amt = dt.Rows(i)(6)
                    j.Cells(4).Value = qty
                    j.Cells(6).Value = amt


                    'j.Cells(4).Value = total
                    j.Cells(0).ReadOnly = True
                    j.Cells(1).ReadOnly = True
                    j.Cells(2).ReadOnly = True
                    j.Cells(3).ReadOnly = True
                    j.Cells(4).ReadOnly = True
                    j.Cells(5).ReadOnly = True
                    j.Cells(6).ReadOnly = True
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        grdv.DataSource = Nothing
        grdv.Rows.Clear()
        txtDescription.Text = ""
        fnditemCode.Value = ""
        txtprinciplecode.Value = ""
        txtprincipledesc.Text = ""
        fnditemCode.MyReadOnly = False
    End Sub
    'It Is Used To Check The Value Of Finder(fnditemCode),Is Present In Database Or Not
    Private Sub fnditemCode_TextLeave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If fnditemCode.Value = "" Then
        Else
            Dim strvalue As String
            Dim strUser_Code As String = "select Item_Code from TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + fnditemCode.Value + "'"
            ' Dim dr As SqlDataReader
            strvalue = clsDBFuncationality.getSingleValue(strUser_Code)

            'If dr.Read() Then
            '    strvalue = dr(0).ToString()
            'End If
            If strvalue <> "" Then
            Else : strUser_Code = ""
                common.clsCommon.MyMessageBoxShow(Me, "Item Code does not exist in Master Table", Me.Text)
                grdv.DataSource = Nothing
                grdv.Rows.Clear()
                txtDescription.Text = ""
                fnditemCode.Value = ""
                fnditemCode.Focus()
            End If
        End If
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    'It Validate User To Press The Keys 
    Private Sub fnditemCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub grdv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdv.CellValueChanged
        'If fnditemCode.txtValue.Text <> "" Then
        '    Dim i As Integer
        '    For i = 0 To grdv.Rows.Count - 1
        '        Dim qty As Decimal = grdv.Rows(i).Cells(2).Value
        '        Dim amount As Decimal = grdv.Rows(i).Cells(3).Value
        '        Dim itemcost As Decimal = grdv.Rows(i).Cells(4).Value
        '        If qty = 0 Then
        '            itemcost = 0
        '        Else
        '            itemcost = amount / qty
        '        End If
        '        grdv.Rows(i).Cells(4).Value = itemcost
        '    Next
        'End If
        If e.ColumnIndex = 4 Or e.ColumnIndex = 6 Then
            Dim qty As Decimal = grdv.CurrentRow.Cells(4).Value
            Dim amount As Decimal = grdv.CurrentRow.Cells(6).Value
            Dim itemcost As Decimal
            If qty <> 0 Then
                itemcost = amount / qty
                grdv.CurrentRow.Cells(5).Value = itemcost
            Else
                grdv.CurrentRow.Cells(5).Value = 0
            End If
        End If

    End Sub

    Private Sub rbtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnClose.Click
        Me.Close()
    End Sub

    Private Sub grdv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdv.Click

    End Sub

    'priti added on 01-06-2011 --- To implement the access control
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        'If funCheckLoginStatus() = False Then Exit Function
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "ITEM-LOC"
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
    '            ' rdbtnDelete.Enabled = False
    '        End If
    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function

    Private Sub txtDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDescription.TextChanged

    End Sub
    
    Private Sub fnditemCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fnditemCode._MYValidating
        Dim str As String = "select count(*) from TSPL_ITEM_LOCATION_DETAILS where Item_Code ='" + fnditemCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            fnditemCode.MyReadOnly = False
        Else
            fnditemCode.MyReadOnly = True
        End If

        If fnditemCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Item_Code as Code, Item_Desc AS [Item_Desc] from TSPL_ITEM_MASTER"
            Dim whrcls As String = ""
            '-----------------------------------------------------------------------
            If clsCommon.myLen(txtprinciplecode.Value) > 0 Then
                qry = "select tspl_item_master.Item_Code as Code, Item_Desc AS [Item_Desc] from TSPL_ITEM_MASTER right outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_item_master.item_code and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + txtprinciplecode.Value + "' and TSPL_ITEM_MASTER_CATEGORY.item_category_code='" + MyLabel1.Text + "'"
                whrcls = " TSPL_ITEM_MASTER.Item_Code<>''"
            Else
                
            End If
            '-------------------------------------------------------------------------------

            fnditemCode.Value = clsCommon.ShowSelectForm("ItemLocation", qry, "Code", whrcls, fnditemCode.Value, "", isButtonClicked)
            If txtprinciplecode.Visible Then
                query = "N"
                categoryfill()
            End If
            LoadData()
        End If
    End Sub
    Sub LoadData()
        Dim strvalue As String
        Dim strItem_Code As String = "select Item_Code from TSPL_ITEM_MASTER where Item_Code='" + fnditemCode.Value + "'"
        'Dim dr As SqlDataReader
        strvalue = clsDBFuncationality.getSingleValue(strItem_Code)

        'If dr.Read() Then
        '    strvalue = dr(0).ToString()
        'End If
        If (strvalue <> "") Then
            funfill()

        Else
            grdv.DataSource = Nothing
            grdv.Rows.Clear()
            txtDescription.Text = ""
        End If
    End Sub

    Private Sub fnditemCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fnditemCode._MYNavigator
        Dim qst As String = "select Item_Code as Code,Item_Desc as [Item Desc] from TSPL_ITEM_MASTER where 2=2 "

        If clsCommon.myLen(txtprinciplecode.Value) > 0 Then
            qst = "select tspl_item_master.Item_Code as Code, Item_Desc AS [Item Desc] from TSPL_ITEM_MASTER right outer join TSPL_ITEM_MASTER_CATEGORY on TSPL_ITEM_MASTER_CATEGORY.item_code=tspl_item_master.item_code and TSPL_ITEM_MASTER_CATEGORY.item_cagetory_values='" + txtprinciplecode.Value + "' and TSPL_ITEM_MASTER_CATEGORY.item_category_code='" + MyLabel1.Text + "' where 2=2 "
        End If


        Select Case NavType
            Case NavigatorType.Current
                qst += " and TSPL_ITEM_MASTER .Item_Code in ('" + fnditemCode.Value + "')"
            Case NavigatorType.Next
                qst += " and TSPL_ITEM_MASTER .Item_Code in (select min(Item_Code ) from TSPL_ITEM_MASTER where Item_Code  >'" + fnditemCode.Value + "')"
            Case NavigatorType.First
                qst += " and TSPL_ITEM_MASTER .Item_Code in (select MIN(Item_Code ) from TSPL_ITEM_MASTER)"

            Case NavigatorType.Last
                qst += " and TSPL_ITEM_MASTER .Item_Code in (select Max(Item_Code ) from TSPL_ITEM_MASTER)"
            Case NavigatorType.Previous
                qst += " and TSPL_ITEM_MASTER .Item_Code in (select Max(Item_Code ) from TSPL_ITEM_MASTER where Item_Code  <'" + fnditemCode.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fnditemCode.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtDescription.Text = clsCommon.myCstr(dt.Rows(0)("Item Desc"))
            query = "N"

            If txtprinciplecode.Visible Then
                categoryfill()
            End If
        End If



        LoadData()
    End Sub

    Sub applyNLevelCategorySetting()
        Dim qry As String = "select IsNLevelCatForItem from TSPL_INV_PARAMETERS"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("IsNLevelCatForItem") = 0 Then
                txtprinciplecode.Visible = False
                txtprincipledesc.Visible = False
                MyLabel1.Visible = False
            Else
                txtprinciplecode.Visible = True
                txtprincipledesc.Visible = True
                MyLabel1.Visible = True
            End If
        End If
    End Sub

    Sub categoryfill()
        Try
            Dim qry As String = ""

            If clsCommon.myLen(fnditemCode.Value) > 0 Then
                qry = "select a.ITEM_CATEGORY_CODE as Code,a.pp as Description,b.code as [Sub Code],b.Description as [Sub Category] from TSPL_ITEM_MASTER_CATEGORY left outer join (select TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as pp from TSPL_ITEM_CATEGORY_LEVEL where TSPL_ITEM_CATEGORY_LEVEL.CATEGORY_LEVEL='1')a on a.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join (select TSPL_ITEM_CATEGORY_LEVEL_VALUES.* from TSPL_ITEM_CATEGORY_LEVEL_VALUES)b on b.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and b.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and a.ITEM_CATEGORY_CODE=b.ITEM_CATEGORY_CODE where isnull(a.ITEM_CATEGORY_CODE,'')<>'' and TSPL_ITEM_MASTER_CATEGORY.Item_code='" + fnditemCode.Value + "'"
            Else
                qry = "select a.ITEM_CATEGORY_CODE as Code,a.pp as Description,b.code as [Sub Code],b.Description as [Sub Category] from TSPL_ITEM_MASTER_CATEGORY left outer join (select TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE,TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION as pp from TSPL_ITEM_CATEGORY_LEVEL where TSPL_ITEM_CATEGORY_LEVEL.CATEGORY_LEVEL='1')a on a.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code left outer join (select TSPL_ITEM_CATEGORY_LEVEL_VALUES.* from TSPL_ITEM_CATEGORY_LEVEL_VALUES)b on b.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and b.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values and a.ITEM_CATEGORY_CODE=b.ITEM_CATEGORY_CODE where isnull(a.ITEM_CATEGORY_CODE,'')<>''"
            End If


            If query = "Y" Then
                Dim dr As DataRow = clsCommon.ShowSelectFormForRow("CATFND", qry)
                If dr IsNot Nothing Then
                    MyLabel1.Text = clsCommon.myCstr(dr("code"))
                    txtprinciplecode.Value = clsCommon.myCstr(dr("sub code"))
                    txtprincipledesc.Text = clsCommon.myCstr(dr("sub category"))
                    If clsCommon.myLen(fnditemCode.Value) > 0 Then
                        LoadData()
                    End If
                Else
                    txtprinciplecode.Value = ""
                    txtprincipledesc.Text = ""
                End If
            Else
                If clsCommon.myLen(fnditemCode.Value) > 0 Then
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                    If dt IsNot Nothing And dt.Rows.Count > 0 Then
                        MyLabel1.Text = clsCommon.myCstr(dt.Rows(0)("code"))
                        txtprinciplecode.Value = clsCommon.myCstr(dt.Rows(0)("sub code"))
                        txtprincipledesc.Text = clsCommon.myCstr(dt.Rows(0)("sub category"))

                        LoadData()
                    End If
                End If
            End If

            
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtprinciplecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtprinciplecode._MYValidating
        Try
            query = "Y"
            categoryfill()
            query = "N"
        Catch ex As Exception
            query = "N"
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
