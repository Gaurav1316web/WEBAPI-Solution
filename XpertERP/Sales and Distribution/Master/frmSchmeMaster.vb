

' update by priti on 14/05/2013 to add a new field for mrp in bottle and conv rate
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmSchmeMaster
    Inherits FrmMainTranScreen
    Dim PageMode As String
    Dim change As Boolean = True
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Const colitemno As String = "itemCode"

    Private isCellValueChangedOpen As Boolean = False
    Dim userCode, companyCode As String
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SchemeMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        fndScheme.MyMaxLength = 12
        txtDesc.MaxLength = 100
        txtComment.MaxLength = 200
        txtMDesc.MaxLength = 100
        txtcuscategory.MaxLength = 100

    End Sub
    Private Sub FrmSchmeMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        SetUserMgmtNew()
        lblitem.Visible = True
        ' fndScheme.Value.MaxLength = 12
        PageMode = "New"
        Me.Text = "Scheme Master"
        AddHandler fndScheme.TextChanged, AddressOf fndScheme_TextChanged
        'AddHandler fndScheme.txtValue.KeyPress, AddressOf fndScheme_KeyPress
        AddHandler fndMainItem.TextChanged, AddressOf fndMainItem_TextChanged
        AddHandler fndMainItem.TextChanged, AddressOf fndMainItem_TextChanged
        AddHandler fndUnit.TextChanged, AddressOf fndUnit_TextChanged
        PrepareGrid()
        ResetScreen()
        SetDataBaseGrid()
        dtpEnd.Value = Date.MinValue
        RadTreeView1.Visible = True
        fndScheme.MyMaxLength = 12
        fndScheme.MyCharacterCasing = CharacterCasing.Upper
        txtmrpbottle.ReadOnly = True
        txtconvrate.ReadOnly = True
        ' grdScheme.Enabled = True
        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub
    Private Sub fndUnit_TextChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        fillMRP()
    End Sub
    Private Sub PrepareGrid()
        FillSchemeDetail("")
        'Dim d As GridViewMultiComboBoxColumn = TryCast(grdScheme.Columns(0), GridViewMultiComboBoxColumn)
        'd.DataSource = (New BAL.BALScheme).GetItemMaster()
        'd.ValueMember = "Item Code"

    End Sub

    Private Sub RadLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadLabel3.Click

    End Sub

    'Private Sub Finder2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndMainItem.ConnectionString = connectSql.SqlCon()
    '    fndMainItem.Query = "select item_code as [Item Code] ,item_desc as [Description] from TSPL_ITEM_MASTER where item_type='F' "
    '    fndMainItem.ValueToSelect = "Item Code"
    '    fndMainItem.ValueToSelect1 = "Item Code"
    '    fndMainItem.Caption = "Item Detail"

    'End Sub

    'Private Sub fndUnit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndUnit.ConnectionString = connectSql.SqlCon()
    '    fndUnit.Query = "SELECT distinct UM.Unit_Code as 'Unit Code', UM.Unit_Desc as Description, UM.Conv_Factor as 'Conversion Factor' " & _
    '    " FROM TSPL_ITEM_PRICE_MASTER AS PM INNER JOIN  TSPL_UNIT_MASTER AS UM ON PM.UOM = UM.Unit_Code WHERE PM.Item_Code='" + fndMainItem.Value + "'"
    '    fndUnit.ValueToSelect = "Unit Code"
    '    fndUnit.ValueToSelect1 = "Unit Code"
    '    fndUnit.Caption = "Unit of Measures"
    'End Sub

    'Private Sub fndScheme_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndScheme.ConnectionString = connectSql.SqlCon()
    '    fndScheme.Query = "SELECT [Scheme_Code] as [Scheme Code]" & _
    '     "     ,[Scheme_Desc] as [Description]" & _
    '      "    ,[Start_Date] as [Start Date]" & _
    '       "   ,[End_Date] as [End Date]" & _
    '        "  ,[Scheme_Type] as [Scheme Type]" & _
    '         " ,[Main_Item_Code] as [Main Item]" & _
    '         " ,[Main_Item_desc] as [Main Item Description]" & _
    '         "  ,[Cust_Cate] as [Customer Category]" & _
    '         "  ,[Cust_Cate_desc] as [Customer Category Description]" & _
    '         " ,[Main_Item_Qty]  as [Main Item Quantity]   " & _
    '            " FROM [dbo].[TSPL_SCHEME_MASTER]"
    '    fndScheme.ValueToSelect = "Scheme Code"
    '    fndScheme.ValueToSelect1 = "Scheme Code"
    '    fndScheme.Caption = "Scheme Detail"
    'End Sub
    'Private Sub fndScheme_KeyPress(ByVal sender As System.Object, ByVal e As KeyPressEventArgs)


    '    change = False

    'End Sub
    Private Sub fndScheme_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If change = True Then
        '    If fndScheme.txtValue.Text.Trim <> "" Then
        '        funfill(fndScheme.txtValue.Text)

        '        ''FillSchemeMaster(fndScheme.txtValue.Text)
        '        ''FillSchemeDetail(fndScheme.txtValue.Text)


        '    End If

        'End If
        'change = True

        Dim strqry As String = "select count(*) from TSPL_SCHEME_MASTER where Scheme_Code='" + fndScheme.Value + "'"
        Dim chk As Integer = CInt(connectSql.RunScalar(strqry))

        If chk <> 0 Then
            funfill(fndScheme.Value)
        Else

            PrepareGrid()
            ddlmrp.Text = ""
            dtpStart.Format = DateTimePickerFormat.Short
            dtpEnd.Format = DateTimePickerFormat.Short
            dtpEnd.Value = Date.Now
            dtpStart.Value = Date.Now
            'fndMainItem.txtValue.Enabled = False
            txtAmount.Enabled = False
            txtAmount.Text = "0.00"
            fndUnit.Value = ""
            txtDesc.MaxLength = 100
            txtMDesc.MaxLength = 100
            txtComment.MaxLength = 100
            ddlType.SelectedItem = ddlType.Items(1)

            RadCheckBox1.Checked = False
            RadCheckBox1.Enabled = False
            fndMainItem.Value = ""
            txtDesc.Text = ""
            txtQuatity.Text = "0"
            txtComment.Text = ""
            txtMDesc.Text = ""
            fndcuscategory.Value = ""
            txtcuscategory.Text = ""
            PageMode = "New"
            ddlType.Enabled = True
            grdScheme.DataSource = Nothing
            grdScheme.Rows.Clear()
            grdScheme.Columns("priceDate").DataSourceNullValue = ""
            dtpEnd.Value = dtpEnd.MinDate
            btnSave.Text = "Save"
            RadTreeView1.Nodes.Clear()
            SetDataBaseGrid()
            RadTreeView1.Visible = True
            lblitem.Visible = True
            ddlBasicPrice.Text = "0"

        End If



    End Sub
    Private Sub fillMRP()
        Try
            Dim sql As String = "select distinct Item_Basic_Net  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + fndMainItem.Value + "' AND Uom='" + fndUnit.Value + "'"
            Dim ds As New DataSet()
            ds = connectSql.RunSQLReturnDS(sql)
            ddlmrp.DataSource = ds.Tables(0)
            ddlmrp.ValueMember = "Item_Basic_Net"
            ddlmrp.DisplayMember = "Item_Basic_Net"
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Private Sub fndMainItem_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim strqry As String = "select count(*) from TSPL_ITEM_MASTER where item_code='" + fndMainItem.Value + "'"
        Dim chk As Integer = CInt(connectSql.RunScalar(strqry))

        If (chk <> 0) Then
            If fndMainItem.Value.Trim <> "" Then
                txtMDesc.Text = (New BAL.BALScheme).FillItemDesc(fndMainItem.Value)
            End If
            'fndUnit.ConnectionString = connectSql.SqlCon()
            'fndUnit.Query = "SELECT DISTINCT UM.Unit_Code as 'Unit Code', UM.Unit_Desc as Description, UM.Conv_Factor as 'Conversion Factor' " & _
            '" FROM TSPL_ITEM_PRICE_MASTER AS PM INNER JOIN  TSPL_UNIT_MASTER AS UM ON PM.UOM = UM.Unit_Code WHERE PM.Item_Code='" + fndMainItem.Value + "'"
            'fndUnit.ValueToSelect = "Unit Code"
            'fndUnit.ValueToSelect1 = "Unit Code"
            'fndUnit.Caption = "Unit of Measures"
            fillMRP()
            binditem()
        Else
            RadTreeView1.Nodes.Clear()
        End If
    End Sub
    Public Sub funfill(ByVal code As String)
        grdScheme.DataSource = Nothing
        grdScheme.Rows.Clear()
        FillSchemeDetail(code)
        FillSchemeMaster(code)
        fndScheme.MyReadOnly = True

        btnSave.Text = "Update"
        PageMode = "Edit"
        SetDataBaseGrid()
        If btnSave.Text = "Save" Then
            RadTreeView1.Visible = True
        ElseIf btnSave.Text = "Update" Then
            RadTreeView1.Visible = False
        End If

        Dim decConvF, decConvMrp As Decimal
        If fndUnit.Value = "FC" Then
            decConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndMainItem.Value & "' and UOM_Code='FB'"))
            decConvMrp = clsCommon.myCdbl(ddlmrp.Text) / decConvF
            txtmrpbottle.Text = decConvMrp
            txtconvrate.Text = decConvF
            lblmrp2.Text = "MRP in Bottle"
        ElseIf fndUnit.Value = "FB" Then
            decConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndMainItem.Value & "' and UOM_Code='FC'"))
            decConvMrp = clsCommon.myCdbl(ddlmrp.Text) / decConvF
            txtmrpbottle.Text = decConvMrp
            txtconvrate.Text = decConvF
            lblmrp2.Text = "MRP in Case"
        End If
    End Sub
    Private Sub FillSchemeMaster(ByVal Code As String)
        Dim dtMaster As DataTable
        Dim sql As String = "SELECT [Scheme_Code]  ,[Scheme_Desc] as [Desc],[Start_Date] as [Start],[End_Date] as [End] ,[Scheme_Type] as [Type]  " & _
      ",[Main_Item_Code] as [MainItem]  ,[Main_Item_desc]as [MDesc]  ,[Main_Item_Qty] as [Quantity]  ,[Amount] as [Amount]  " & _
      ",[Comments] as [Comment],[Main_Item_UOM] as [UOM],[MRP],Item_Basic_Price,Cust_Cate as [Customer Category],Cust_Cate_Desc as [Cust Cate Desc] FROM [TSPL_SCHEME_MASTER] where [Scheme_Code] = '" + Code + "' "
        Dim ds As DataSet = connectSql.RunSQLReturnDS(sql)
        dtMaster = ds.Tables(0)
        Dim schemeType As String = dtMaster.Rows(0)("Type").ToString()
        fndMainItem.Enabled = True
        txtQuatity.Enabled = True
        txtAmount.Enabled = True
        RadCheckBox1.Enabled = True
        If schemeType = "S" Then
            ddlType.Text = "Sampling"
            txtAmount.Enabled = False
            RadCheckBox1.Enabled = False
        ElseIf schemeType = "Q" Then
            ddlType.Text = "Quantitative Discount"
            txtAmount.Enabled = False
            RadCheckBox1.Enabled = False
        ElseIf schemeType = "C" Then
            ddlType.Text = "Cash Discount"
        ElseIf schemeType = "P" Then
            ddlType.Text = "Promotional"
            txtAmount.Enabled = False
            RadCheckBox1.Enabled = False
        End If
        ddlType.Enabled = False
        txtMDesc.Enabled = True
        txtDesc.Text = dtMaster.Rows(0)("Desc").ToString()
        txtMDesc.Text = dtMaster.Rows(0)("MDesc").ToString()
        txtQuatity.Text = dtMaster.Rows(0)("Quantity").ToString()
        dtpStart.Value = Convert.ToDateTime(dtMaster.Rows(0)("Start"))
        If Not IsDBNull(dtMaster.Rows(0)("End")) Then
            dtpEnd.Value = Convert.ToDateTime(dtMaster.Rows(0)("End"))
            dtpEnd.Checked = True
        Else
            dtpEnd.Value = clsCommon.GETSERVERDATE()
            dtpEnd.Checked = False
        End If

        If Convert.ToDecimal(dtMaster.Rows(0)("Amount")) < 0 Then
            txtAmount.Text = Math.Abs(Convert.ToDecimal(dtMaster.Rows(0)("Amount"))).ToString()
            RadCheckBox1.Checked = True
        Else
            txtAmount.Text = dtMaster.Rows(0)("Amount").ToString()
        End If
        txtComment.Text = dtMaster.Rows(0)("Comment").ToString()
        fndMainItem.Value = dtMaster.Rows(0)("MainItem").ToString()
        fndUnit.Value = dtMaster.Rows(0)("UOM").ToString()
        ddlmrp.Text = dtMaster.Rows(0)("MRP").ToString()
        ddlBasicPrice.Text = dtMaster.Rows(0)("Item_Basic_Price").ToString()
        fndcuscategory.Value = dtMaster.Rows(0)("Customer Category").ToString()
        txtcuscategory.Text = dtMaster.Rows(0)("Cust Cate Desc").ToString()
    End Sub

    Private Sub FillSchemeDetail(ByVal Code As String)
        ''  Dim Sql1 As String = "SELECT [Scheme_Item_Code] as itemCode ,[Scheme_Item_Desc] as description  " & _
        ''",[Qty] as qty ,Price_Date as priceDate,[UOM] as unitCode,MRP,Item_Basic_Price,[Remarks]  FROM [TSPL_SCHEME_DETAILS] WHERE Scheme_Code = '" + Code + "' "
        ''  Dim DS As DataSet = connectSql.RunSQLReturnDS(Sql1)
        ''  For Each drow As DataRow In DS.Tables(0).Rows
        ''      grdScheme.Rows.Add(drow(0).ToString(), drow(1).ToString(), drow(2).ToString(), drow(3).ToString(), drow(4).ToString(), drow(5).ToString(), drow(6).ToString(), drow(7).ToString())
        ''  Next
        ''  ' grdScheme.DataSource = DS.Tables(0)
        ''  grdScheme.Enabled = True


        '---------------Removed Basic Price by vipin---------------------------
        Dim Sql1 As String = "SELECT [Scheme_Item_Code] as itemCode ,[Scheme_Item_Desc] as description  " & _
    ",[Qty] as qty ,Price_Date as priceDate,[UOM] as unitCode,MRP,[Remarks]  FROM [TSPL_SCHEME_DETAILS] WHERE Scheme_Code = '" + Code + "' "
        Dim DS As DataSet = connectSql.RunSQLReturnDS(Sql1)
        For Each drow As DataRow In DS.Tables(0).Rows
            grdScheme.Rows.Add(drow(0).ToString(), drow(1).ToString(), drow(2).ToString(), drow(3).ToString(), drow(4).ToString(), drow(5).ToString(), drow(6).ToString())
        Next
        ' grdScheme.DataSource = DS.Tables(0)
        grdScheme.Enabled = True

    End Sub

    Private Sub ddlType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlType.SelectedIndexChanged
        If ddlType.Text = "Cash Discount" Then
            grdScheme.Enabled = False
            PrepareGrid()
            fndMainItem.Enabled = True
            txtMDesc.Enabled = True
            txtAmount.Enabled = True
            txtQuatity.Enabled = True
            RadCheckBox1.Enabled = True
        ElseIf ddlType.Text = "Quantitative Discount" Then
            grdScheme.Enabled = True
            txtAmount.Text = "0.00"
            fndMainItem.Enabled = True
            txtMDesc.Enabled = True
            txtAmount.Enabled = False
            txtQuatity.Enabled = True
            RadCheckBox1.Enabled = False
            'Dim d As New GridViewMultiComboBoxColumn("Item Code")
            'd.DataSource = (New BAL.BALScheme).GetItemMaster("F")
            'grdScheme.Columns(0) = d
            'grdScheme.Columns(0).Width = 200
        ElseIf ddlType.Text = "Sampling" Then
            'fndMainItem.Enabled = False
            fndMainItem.Enabled = True
            txtQuatity.Text = "0"
            txtAmount.Text = "0.00"
            txtMDesc.Enabled = False
            txtAmount.Enabled = False
            txtQuatity.Enabled = False
            txtAmount.Enabled = False
            grdScheme.Enabled = True
            RadCheckBox1.Enabled = False
            'Dim d As New GridViewMultiComboBoxColumn("Item Code")
            'd.DataSource = (New BAL.BALScheme).GetItemMaster("F")
            'grdScheme.Columns(0) = d
        ElseIf ddlType.Text = "Promotional" Then
            grdScheme.Enabled = True
            txtAmount.Text = "0.00"
            fndMainItem.Enabled = True
            txtMDesc.Enabled = True
            txtAmount.Enabled = False
            txtQuatity.Enabled = True
            RadCheckBox1.Enabled = False
            'Dim d As New GridViewMultiComboBoxColumn("Item Code")
            'd.DataSource = (New BAL.BALScheme).GetItemMaster("P")
            'grdScheme.Columns(0) = d

        Else
            'fndMainItem.Enabled = False
            txtQuatity.Text = "0"
            txtAmount.Text = "0.00"
            txtMDesc.Enabled = False
            txtAmount.Enabled = False
            txtQuatity.Enabled = False
            txtAmount.Enabled = False
            grdScheme.Enabled = False
            RadCheckBox1.Enabled = False
        End If


    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
       
        SaveData1()

    End Sub
    Sub SaveData1()
        Try

            If ddlType.Text = "Sampling" Then
                savedata()
            Else
                If ddlType.Text = "Select" Or ddlType.Text = "" Then
                    common.clsCommon.MyMessageBoxShow("Type can not be Blank")
                ElseIf fndMainItem.Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Main Item can not be Blank")
                    fndMainItem.Focus()
                ElseIf fndUnit.Value = "" Then
                    myMessages.blankValue("UOM")
                    fndUnit.Focus()

                ElseIf fndcuscategory.Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Customer Category can not be Blank")
                    fndcuscategory.Focus()
                    Return
                    'ElseIf dtpStart.Value = "" Then
                    '    common.clsCommon.MyMessageBoxShow("Check the value of Start Date")
                    'ElseIf dtpEnd.Value = "" Then
                    common.clsCommon.MyMessageBoxShow("Check the End of Start Date")
                ElseIf ddlmrp.Text = "Select" Or ddlmrp.Text = "" Then
                    common.clsCommon.MyMessageBoxShow("MRP can not be Blank")
                    ddlmrp.Focus()
                    'ElseIf ddlBasicPrice.Text = "Select" Or ddlBasicPrice.Text = "" Then
                    '    common.clsCommon.MyMessageBoxShow("Basic Price can not be Blank")
                ElseIf dtpStart.Value > dtpEnd.Value Then
                    common.clsCommon.MyMessageBoxShow("Start Date can not be greater than End Date")
                    ddlmrp.Focus()
                Else
                    If btnSave.Text = "Save" Then
                        Dim schemeType As String = ""
                        If ddlType.Text = "Sampling" Then
                            schemeType = "S"
                        ElseIf ddlType.Text = "Quantitative Discount" Then
                            schemeType = "Q"
                        ElseIf ddlType.Text = "Cash Discount" Then
                            schemeType = "C"
                        ElseIf ddlType.Text = "Promotional" Then
                            schemeType = "P"
                        End If
                        'Dim sql As String = "select COUNT(*) from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.txtValue.Text + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.txtValue.Text + "'  and  Item_Basic_Price='" + ddlBasicPrice.Text + "'"
                        Dim sql As String = "select COUNT(*) from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.Value + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.Value + "'  "
                        Dim i As Integer = CInt(connectSql.RunScalar(sql))
                        If i = 0 Then
                            savedata()
                        Else
                            'Dim query As String = "select Scheme_Code,start_date ,end_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.txtValue.Text + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.txtValue.Text + "'  and  Item_Basic_Price='" + ddlBasicPrice.Text + "'"
                            Dim query As String = "select Scheme_Code,start_date ,end_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.Value + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.Value + "' "
                            Dim con As New SqlConnection(connectSql.SqlCon())
                            Dim da As New SqlDataAdapter(query, con)
                            Dim dt1 As New DataTable
                            da.Fill(dt1)

                            ' Dim array As ArrayList = New ArrayList(100)
                            Dim array As New ArrayList()

                            Dim status As Integer = 0
                            Dim status1 As Integer = 0

                            For j As Integer = 0 To dt1.Rows.Count - 1
                                array.Add(dt1.Rows(j).Item("Scheme_Code"))

                            Next

                            Dim length As Integer = array.Count

                            For k As Integer = 0 To length - 1
                                Dim ddlstart As String = CDate(dtpStart.Value).ToString("dd/MM/yyyy")
                                'Dim str As String = "select end_date,start_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.txtValue.Text + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.txtValue.Text + "'  and  Item_Basic_Price='" + ddlBasicPrice.Text + "' and Scheme_Code='" + array(k) + "'"
                                Dim str As String = "select end_date,start_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.Value + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.Value + "'   and Scheme_Code='" + array(k) + "'"
                                Dim dr As DataTable
                                dr = clsDBFuncationality.GetDataTable(str)
                                Dim enddate As String = ""
                                Dim startdate As String
                                For Each row As DataRow In dr.Rows
                                    enddate = row(0).ToString()
                                    startdate = row(1).ToString()
                                Next
                                If enddate = "" Then

                                    If myMessages.SchemeCloseCheck() Then

                                        For Each code As String In array

                                            'clsCommon.myCDate(dtpStart.Value).AddDays(-1)
                                            Dim qq As String = "update TSPL_SCHEME_MASTER set End_Date=convert(date,'" + clsCommon.myCDate(dtpStart.Value).AddDays(-1) + "',103) where Scheme_Code='" + code + "' "


                                            'Dim qq As String = "update TSPL_SCHEME_MASTER set End_Date=convert(date,'" + dtpStart.Value + "',103) where Scheme_Code='" + code + "' "
                                            clsDBFuncationality.ExecuteNonQuery(qq)
                                        Next



                                    Else

                                        'common.clsCommon.MyMessageBoxShow("This Scheme is not allowed because same scheme does not have end date")
                                        Exit Sub
                                    End If

                                Else
                                    'If ddlstart >= startdate And ddlstart >= enddate Then
                                    '    status = 1
                                    'End If

                                    If CDate(ddlstart).ToString("yyyy/MM/dd") < CDate(enddate).ToString("yyyy/MM/dd") Then
                                        status = 1
                                    End If
                                End If
                            Next

                            If status = 1 Then
                                common.clsCommon.MyMessageBoxShow("Scheme of this type have End date greater then Start date,check the Start Date")

                            Else

                                savedata()

                            End If
                        End If

                    Else
                        Dim dtend As String = CDate(dtpEnd.Value).ToString("dd/MM/yyyy")
                        Dim dtstart As String = CDate(dtpStart.Value).ToString("dd/MM/yyyy")
                        Dim schemeType As String = ""
                        If ddlType.Text = "Sampling" Then
                            schemeType = "S"
                        ElseIf ddlType.Text = "Quantitative Discount" Then
                            schemeType = "Q"
                        ElseIf ddlType.Text = "Cash Discount" Then
                            schemeType = "C"
                        ElseIf ddlType.Text = "Promotional" Then
                            schemeType = "P"
                        End If


                        Dim query As String = "select Scheme_Code,start_date ,end_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.Value + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.Value + "' "
                        Dim con As New SqlConnection(connectSql.SqlCon())
                        Dim da As New SqlDataAdapter(query, con)
                        Dim dt1 As New DataTable
                        da.Fill(dt1)

                        Dim array As New ArrayList()

                        Dim status As Integer = 0
                        Dim status1 As Integer = 0

                        For j As Integer = 0 To dt1.Rows.Count - 1

                            array.Add(dt1.Rows(j).Item("Scheme_Code"))

                        Next

                        Dim length As Integer = array.Count

                        For k As Integer = 0 To length - 1

                            If dtpEnd.Checked Then

                                Dim ddlstart As String = CDate(dtpStart.Value).ToString("dd/MM/yyyy")

                                Dim str As String = "select end_date,start_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.Value + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.Value + "'  and Scheme_Code='" + array(k) + "'"
                                Dim dr As DataTable
                                dr = clsDBFuncationality.GetDataTable(str)
                                Dim enddate As String = clsCommon.GetPrintDate(dtpEnd.Value)
                                Dim startdate As String
                                For Each row As DataRow In dr.Rows
                                    startdate = row(1).ToString()
                                Next

                               
                                If array(k) <> fndScheme.Value Then

                                    'If CDate(dtend).ToString("yyyy/MM/dd") < startdate And CDate(dtend).ToString("yyyy/MM/dd") < enddate Then
                                    '    status = 1
                                    'End If

                                End If

                            End If
                        Next

                        If status = 1 Then
                            common.clsCommon.MyMessageBoxShow("This Scheme is not allowed ,check  Date")

                        Else

                            If dtpEnd.Value = "01/01/1753" Then
                                savedata()
                            Else
                                If dtpEnd.Value >= dtpStart.Value Then
                                    savedata()
                                Else
                                    common.clsCommon.MyMessageBoxShow("Check the  End Date ")
                                End If
                            End If


                        End If
                    End If
                End If

            End If




            'Dim ddlstart As String = CDate(dtpStart.Value).ToString("dd/MM/yyyy")
            ''Dim str As String = "select end_date,start_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.txtValue.Text + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.txtValue.Text + "' and Start_Date='" + CDate(dtpStart.Value).ToString("yyyy-MM-dd") + "' and  Item_Basic_Price='" + ddlBasicPrice.Text + "'"
            'Dim str As String = "select end_date,start_date from TSPL_SCHEME_MASTER where Scheme_Type='" + schemeType + "' and Main_Item_Code='" + fndMainItem.txtValue.Text + "' and MRP='" + ddlmrp.Text + "' and Cust_Cate='" + fndcuscategory.Value + "' and Main_Item_UOM='" + fndUnit.txtValue.Text + "'  and  Item_Basic_Price='" + ddlBasicPrice.Text + "'"
            'Dim dr As SqlDataReader
            'dr = connectSql.RunSqlReturnDR(str)
            'Dim enddate As String
            'Dim startdate As String
            'While dr.Read()
            '    'enddate = CDate(dr(0)).ToString("yyyy-MM-dd")
            '    'startdate = CDate(dr(1)).ToString("yyyy-MM-dd")

            '    enddate = dr(0).ToString()
            '    startdate = dr(1).ToString()
            'End While


            'If enddate = "" Then
            '    common.clsCommon.MyMessageBoxShow("This Scheme is not allowed")
            'Else
            '    If ddlstart >= startdate And ddlstart <= enddate Then
            '        common.clsCommon.MyMessageBoxShow("This Scheme is not allowed")
            '        Exit Sub
            '    Else
            '        If ddlstart > startdate And ddlstart > enddate Then
            '            savedata()

            '        Else
            '            common.clsCommon.MyMessageBoxShow("This Scheme is not allowed")
            '            Exit Sub
            '        End If

            '    End If
            'End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Public Sub savedata()
        Dim expdate As Date? = Nothing
        Dim isSaved As Boolean = False
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        If dtpEnd.Checked Then
            expdate = dtpEnd.Value.Date
        End If
        Try
            If ValidateSave() Then
                Dim schemeType As String = ""
                If ddlType.Text = "Sampling" Then
                    schemeType = "S"
                ElseIf ddlType.Text = "Quantitative Discount" Then
                    schemeType = "Q"
                ElseIf ddlType.Text = "Cash Discount" Then
                    schemeType = "C"
                ElseIf ddlType.Text = "Promotional" Then
                    schemeType = "P"
                End If
                If PageMode = "New" Then
                    Dim Ins As New BAL.BALScheme
                    Dim cashDiscount As Decimal = 0
                    If RadCheckBox1.Checked Then
                        cashDiscount = 0 - Convert.ToDecimal(txtAmount.Text)
                    Else
                        cashDiscount = Convert.ToDecimal(txtAmount.Text)
                    End If
                    'Dim mainbasicprice As String = ddlBasicPrice.Text

                    Dim mainbasicprice As String = 0
                    Dim mainmrp As String = ddlmrp.Text
                    If schemeType = "S" Then
                        mainbasicprice = 0
                        mainmrp = 0
                    End If
                    'Dim isSaved As Boolean = False
                    Dim arrDBName As New List(Of String)
                    arrDBName.Add(objCommonVar.CurrDatabase)
                    For ii As Integer = 0 To gvDB.Rows.Count - 1
                        If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                            arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
                        End If
                    Next
                    isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "InsertSchemeMaster", New SqlParameter("@Code", fndScheme.Value), New SqlParameter("@Type", schemeType), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@Start", dtpStart.Value), New SqlParameter("@EndDate", expdate), New SqlParameter("@MainItem", fndMainItem.Value), New SqlParameter("@Quantity", txtQuatity.Text), New SqlParameter("@MainItemdesc", txtMDesc.Text), New SqlParameter("@Comment", txtComment.Text), New SqlParameter("@Amount", cashDiscount), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", fndUnit.Value), New SqlParameter("@MRP", mainmrp), New SqlParameter("@Item_Basic_Price", mainbasicprice), New SqlParameter("@Cust_Cate", fndcuscategory.Value), New SqlParameter("@Cust_Cate_desc", txtcuscategory.Text))

                    For j As Integer = 0 To grdScheme.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(grdScheme.Rows(j).Cells(0).Value)) <> 0 Then
                            Dim ItemCode As String = grdScheme.Rows(j).Cells(0).Value.ToString()
                            Dim ItemDesc As String = grdScheme.Rows(j).Cells("description").Value.ToString()
                            Dim UOM As String = grdScheme.Rows(j).Cells("unitCode").Value.ToString()
                            Dim Qty As Decimal = Convert.ToDecimal(grdScheme.Rows(j).Cells("qty").Value)
                            Dim Remarks As String = Convert.ToString(grdScheme.Rows(j).Cells("Remarks").Value)
                            Dim mrp As String = grdScheme.Rows(j).Cells("mrp").Value.ToString()
                            Dim priceDate As Date = CDate(grdScheme.Rows(j).Cells("priceDate").Value)
                            'Dim basicPrice As Decimal = grdScheme.Rows(j).Cells("basicPrice").Value.ToString()
                            Dim basicPrice As Decimal = 0
                            isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "InsertSchemeDetail", New SqlParameter("@Code", fndScheme.Value), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@MainItem ", ItemCode), New SqlParameter("@Quantity", Qty), New SqlParameter("@MainItemDesc", ItemDesc), New SqlParameter("@Comment", Remarks), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", UOM), New SqlParameter("@MRP", mrp), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Item_Basic_price", basicPrice))
                        End If
                    Next





                    '-----------------------------------------For replication of items in all location----------------------

                    Dim arr As New ArrayList()
                    Dim chk As Char
                    Dim code As String = fndScheme.Value
                    Dim value As String
                    Dim isSaved1 As Boolean = False
                    For Each node As RadTreeNode In RadTreeView1.Nodes
                        For Each nd As RadTreeNode In node.Nodes
                            If nd.Checked = True Then
                                arr.Add(nd.Name)
                            End If
                        Next
                    Next
                    If arr.Count <> 0 Then

                        If myMessages.ItemchkConfirm Then
                            chk = "T"
                        Else

                            chk = "F"
                        End If

                        For i As Integer = 0 To arr.Count - 1
                            value = code + Convert.ToString(i + 1)
                            'Dim dr1 As DataTable
                            Dim desc As String
                            Dim str As String = "select item_desc from tspl_item_master where Item_Code='" + arr.Item(i) + "'"
                            desc = clsDBFuncationality.getSingleValue(str, trans)
                           
                            ''isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "InsertSchemeMaster", New SqlParameter("@Code", value), New SqlParameter("@Type", schemeType), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@Start", dtpStart.Value), New SqlParameter("@EndDate", expdate), New SqlParameter("@MainItem", arr.Item(i).ToString), New SqlParameter("@Quantity", txtQuatity.Text), New SqlParameter("@MainItemdesc", txtMDesc.Text), New SqlParameter("@Comment", txtComment.Text), New SqlParameter("@Amount", cashDiscount), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", fndUnit.Value), New SqlParameter("@MRP", mainmrp), New SqlParameter("@Item_Basic_Price", mainbasicprice), New SqlParameter("@Cust_Cate", fndcuscategory.Value), New SqlParameter("@Cust_Cate_desc", txtcuscategory.Text))

                            isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "InsertSchemeMaster", New SqlParameter("@Code", value), New SqlParameter("@Type", schemeType), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@Start", dtpStart.Value), New SqlParameter("@EndDate", expdate), New SqlParameter("@MainItem", arr.Item(i).ToString), New SqlParameter("@Quantity", txtQuatity.Text), New SqlParameter("@MainItemdesc", desc), New SqlParameter("@Comment", txtComment.Text), New SqlParameter("@Amount", cashDiscount), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", fndUnit.Value), New SqlParameter("@MRP", mainmrp), New SqlParameter("@Item_Basic_Price", mainbasicprice), New SqlParameter("@Cust_Cate", fndcuscategory.Value), New SqlParameter("@Cust_Cate_desc", txtcuscategory.Text))

                            Dim ItemCode As String
                            Dim ItemDesc As String
                            For ii As Integer = 0 To grdScheme.Rows.Count - 1
                                If clsCommon.myLen(clsCommon.myCstr(grdScheme.Rows(ii).Cells(0).Value)) <> 0 Then
                                    'Dim ItemCode As String = grdScheme.Rows(ii).Cells(0).Value.ToString()
                                    'Dim ItemDesc As String = grdScheme.Rows(ii).Cells("description").Value.ToString()

                                    If chk = "T" Then
                                        ItemCode = grdScheme.Rows(ii).Cells(0).Value.ToString()
                                        ItemDesc = grdScheme.Rows(ii).Cells("description").Value.ToString()

                                    Else
                                        ItemCode = funcreateitem(arr.Item(i), grdScheme.Rows(ii).Cells(0).Value.ToString(), trans)
                                        'ItemDesc = connectSql.RunScalar("select item_desc from tspl_item_master where item_code='" + ItemCode + "'")
                                        ItemDesc = clsDBFuncationality.getSingleValue("select item_desc from tspl_item_master where item_code='" + ItemCode + "'", trans)
                                        If clsCommon.myLen(ItemDesc) = 0 Then
                                            Throw New Exception("'" + ItemCode + "'   Item does not found for    '" + arr.Item(i) + "'")
                                        End If


                                        'Dim ItemDesc As String = grdScheme.Rows(ii).Cells("description").Value.ToString()
                                    End If


                                    Dim UOM As String = grdScheme.Rows(ii).Cells("unitCode").Value.ToString()
                                    Dim Qty As Decimal = Convert.ToDecimal(grdScheme.Rows(ii).Cells("qty").Value)
                                    Dim Remarks As String = Convert.ToString(grdScheme.Rows(ii).Cells("Remarks").Value)
                                    Dim mrp As String = grdScheme.Rows(ii).Cells("mrp").Value.ToString()
                                    Dim priceDate As Date = CDate(grdScheme.Rows(ii).Cells("priceDate").Value)
                                    ' Dim basicPrice As Decimal = grdScheme.Rows(ii).Cells("basicPrice").Value.ToString()
                                    Dim basicPrice As Decimal = 0
                                    'isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans1, arrDBName, "InsertSchemeDetail", New SqlParameter("@Code", value), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@MainItem ", clsCommon.myCstr(arr(i))), New SqlParameter("@Quantity", Qty), New SqlParameter("@MainItemDesc", ItemDesc), New SqlParameter("@Comment", Remarks), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", UOM), New SqlParameter("@MRP", mrp), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Item_Basic_price", basicPrice))
                                    isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "InsertSchemeDetail", New SqlParameter("@Code", value), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@MainItem ", ItemCode), New SqlParameter("@Quantity", Qty), New SqlParameter("@MainItemDesc", ItemDesc), New SqlParameter("@Comment", Remarks), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", UOM), New SqlParameter("@MRP", mrp), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Item_Basic_price", basicPrice))
                                End If
                            Next
                        Next
                    End If


                    If isSaved Then
                        trans.Commit()
                        myMessages.insert()
                        funfill(fndScheme.Value)
                    Else
                        trans.Rollback()
                    End If

                    ' ''-----------------------------------------For replication of items in all location----------------------
                    ''Dim trans1 As SqlTransaction = clsDBFuncationality.GetTransactin()
                    ''Try
                    ''    Dim arr As New ArrayList()
                    ''    'arr.Add(fndMainItem.txtValue.Text)
                    ''    Dim code As String = fndScheme.txtValue.Text
                    ''    Dim value As String
                    ''    Dim isSaved1 As Boolean = False
                    ''    'Dim arrDBName1 As New List(Of String)
                    ''    'arrDBName1.Add(objCommonVar.CurrDatabase)
                    ''    'For ii As Integer = 0 To gvDB.Rows.Count - 1
                    ''    '    If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                    ''    '        arrDBName1.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
                    ''    '    End If
                    ''    'Next
                    ''    For Each node As RadTreeNode In RadTreeView1.Nodes
                    ''        For Each nd As RadTreeNode In node.Nodes
                    ''            If nd.Checked = True Then
                    ''                arr.Add(nd.Name)
                    ''            End If
                    ''        Next
                    ''    Next
                    ''    If arr.Count <> 0 Then
                    ''        For i As Integer = 0 To arr.Count - 1
                    ''            value = code + Convert.ToString(i + 1)
                    ''            Dim dr1 As SqlDataReader
                    ''            Dim desc As String
                    ''            Dim str As String = "select item_desc from tspl_item_master where Item_Code='" + arr.Item(i) + "'"
                    ''            dr1 = connectSql.RunSqlReturnDR(str)
                    ''            While dr1.Read
                    ''                desc = dr1(0).ToString
                    ''            End While

                    ''            'Ins.InsertSchemeMaster(value, schemeType, txtDesc.Text, dtpStart.Value, expdate, arr.Item(i).ToString, txtQuatity.Text, desc, txtComment.Text, cashDiscount, userCode, companyCode, fndUnit.txtValue.Text, mainmrp, mainbasicprice, fndcuscategory.Value, txtcuscategory.Text)
                    ''            isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans1, arrDBName, "InsertSchemeMaster", New SqlParameter("@Code", value), New SqlParameter("@Type", schemeType), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@Start", dtpStart.Value), New SqlParameter("@EndDate", expdate), New SqlParameter("@MainItem", arr.Item(i).ToString), New SqlParameter("@Quantity", txtQuatity.Text), New SqlParameter("@MainItemdesc", txtMDesc.Text), New SqlParameter("@Comment", txtComment.Text), New SqlParameter("@Amount", cashDiscount), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", fndUnit.txtValue.Text), New SqlParameter("@MRP", mainmrp), New SqlParameter("@Item_Basic_Price", mainbasicprice), New SqlParameter("@Cust_Cate", fndcuscategory.Value), New SqlParameter("@Cust_Cate_desc", txtcuscategory.Text))




                    ''            For ii As Integer = 0 To grdScheme.Rows.Count - 1
                    ''                If clsCommon.myLen(clsCommon.myCstr(grdScheme.Rows(ii).Cells(0).Value)) <> 0 Then
                    ''                    'Dim ItemCode As String = grdScheme.Rows(ii).Cells(0).Value.ToString()
                    ''                    'Dim ItemDesc As String = grdScheme.Rows(ii).Cells("description").Value.ToString()

                    ''                    Dim ItemCode As String = funcreateitem(arr.Item(i), grdScheme.Rows(ii).Cells(0).Value.ToString())

                    ''                    Dim ItemDesc As String = grdScheme.Rows(ii).Cells("description").Value.ToString()
                    ''                    Dim UOM As String = grdScheme.Rows(ii).Cells("unitCode").Value.ToString()
                    ''                    Dim Qty As Decimal = Convert.ToDecimal(grdScheme.Rows(ii).Cells("qty").Value)
                    ''                    Dim Remarks As String = Convert.ToString(grdScheme.Rows(ii).Cells("Remarks").Value)
                    ''                    Dim mrp As String = grdScheme.Rows(ii).Cells("mrp").Value.ToString()
                    ''                    Dim priceDate As Date = CDate(grdScheme.Rows(ii).Cells("priceDate").Value)
                    ''                    Dim basicPrice As Decimal = grdScheme.Rows(ii).Cells("basicPrice").Value.ToString()
                    ''                    'isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans1, arrDBName, "InsertSchemeDetail", New SqlParameter("@Code", value), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@MainItem ", clsCommon.myCstr(arr(i))), New SqlParameter("@Quantity", Qty), New SqlParameter("@MainItemDesc", ItemDesc), New SqlParameter("@Comment", Remarks), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", UOM), New SqlParameter("@MRP", mrp), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Item_Basic_price", basicPrice))
                    ''                    isSaved1 = clsDBFuncationality.UpdateInSelectedDatabase(trans1, arrDBName, "InsertSchemeDetail", New SqlParameter("@Code", value), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@MainItem ", ItemCode), New SqlParameter("@Quantity", Qty), New SqlParameter("@MainItemDesc", ItemDesc), New SqlParameter("@Comment", Remarks), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", UOM), New SqlParameter("@MRP", mrp), New SqlParameter("@Price_Date", priceDate), New SqlParameter("@Item_Basic_price", basicPrice))
                    ''                End If
                    ''            Next
                    ''        Next
                    ''    End If
                    ''    If isSaved1 Then
                    ''        trans1.Commit()

                    ''    Else
                    ''        trans1.Rollback()
                    ''    End If
                    ''Catch ex As Exception
                    ''    trans1.Rollback()
                    ''    myMessages.myExceptions(ex)
                    ''End Try


                Else
                    Dim Ins As New BAL.BALScheme
                    Dim cashDiscount As Decimal = 0
                    If RadCheckBox1.Checked Then
                        cashDiscount = 0 - Convert.ToDecimal(txtAmount.Text)
                    Else
                        cashDiscount = Convert.ToDecimal(txtAmount.Text)
                    End If
                    ' Dim mainbasicprice As String = ddlBasicPrice.Text
                    Dim mainbasicprice As String = 0
                    Dim mainmrp As String = ddlmrp.Text
                    If schemeType = "S" Then
                        mainbasicprice = 0
                        mainmrp = 0
                    End If

                    'Dim isSaved As Boolean = False
                    'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()

                    Dim arrDBName As New List(Of String)
                    arrDBName.Add(objCommonVar.CurrDatabase)
                    For ii As Integer = 0 To gvDB.Rows.Count - 1
                        If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                            arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
                        End If
                    Next


                    'Ins.UpdateSchemeMaster(fndScheme.txtValue.Text, schemeType, txtDesc.Text, Convert.ToDateTime(dtpStart.Value.Date), expdate, fndMainItem.txtValue.Text, txtQuatity.Text, txtMDesc.Text, txtComment.Text, cashDiscount, userCode, companyCode, fndUnit.txtValue.Text, mainmrp, mainbasicprice, fndcuscategory.Value, txtcuscategory.Text)
                    isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "UpdateSchemeMaster", New SqlParameter("@Code", fndScheme.Value), New SqlParameter("@Type", schemeType), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@Start", dtpStart.Value), New SqlParameter("@EndDate", expdate), New SqlParameter("@MainItem", fndMainItem.Value), New SqlParameter("@Quantity", txtQuatity.Text), New SqlParameter("@MainItemdesc", txtMDesc.Text), New SqlParameter("@Comment", txtComment.Text), New SqlParameter("@Amount", cashDiscount), New SqlParameter("@UpdatedBy", userCode), New SqlParameter("@UpdatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", fndUnit.Value), New SqlParameter("@MRP", mainmrp), New SqlParameter("@Item_Basic_Price", mainbasicprice), New SqlParameter("@Cust_Cate", fndcuscategory.Value), New SqlParameter("@Cust_Cate_desc", txtcuscategory.Text))
                    isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "DeleteSchemeDetail", New SqlParameter("@Code", fndScheme.Value))


                    For i As Integer = 0 To grdScheme.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(grdScheme.Rows(i).Cells(0).Value)) <> 0 Then
                            Dim ItemCode As String = grdScheme.Rows(i).Cells(0).Value.ToString()
                            Dim ItemDesc As String = grdScheme.Rows(i).Cells("description").Value.ToString()
                            Dim UOM As String = grdScheme.Rows(i).Cells("unitCode").Value.ToString()
                            Dim Qty As Decimal = Convert.ToDecimal(grdScheme.Rows(i).Cells("qty").Value)
                            Dim Remarks As String = Convert.ToString(grdScheme.Rows(i).Cells("remarks").Value)
                            Dim mrp As String = grdScheme.Rows(i).Cells("mrp").Value.ToString()
                            Dim pricedate As Date = CDate(grdScheme.Rows(i).Cells("priceDate").Value.ToString())
                            'Dim basicPrice As Decimal = grdScheme.Rows(i).Cells("basicprice").Value
                            Dim basicPrice As Decimal = 0
                            isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "InsertSchemeDetail", New SqlParameter("@Code", fndScheme.Value), New SqlParameter("@Desc", txtDesc.Text), New SqlParameter("@MainItem ", ItemCode), New SqlParameter("@Quantity", Qty), New SqlParameter("@MainItemDesc", ItemDesc), New SqlParameter("@Comment", Remarks), New SqlParameter("@CreatedBy", userCode), New SqlParameter("@CreatedDate", Date.Now), New SqlParameter("@Company", companyCode), New SqlParameter("@UOM", UOM), New SqlParameter("@MRP", mrp), New SqlParameter("@Price_Date", pricedate), New SqlParameter("@Item_Basic_price", basicPrice))
                        End If
                    Next
                    If isSaved Then
                        myMessages.update()
                        trans.Commit()
                        funfill(fndScheme.Value)
                    Else
                        trans.Rollback()
                    End If

                End If
            End If

            If isSaved Then
                
            Else
                trans.Rollback()
            End If

        Catch ex As Exception
            trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Public Function funcreateitem(ByVal mainitem As String, ByVal schitem As String, ByVal trans As SqlTransaction) As String

        Dim createitem As String = ""
        Try
            Dim classcode As String = ""
            Dim dr As DataTable
            Dim strmain As String = "select Class_Code from TSPL_ITEM_DETAILS where Item_Code ='" + mainitem + "' and class_name ='flavour'"
            'classcode = connectSql.RunScalar(strmain)
            classcode = clsDBFuncationality.getSingleValue(strmain, trans)

            Dim flv As String = ""
            Dim size As String = ""
            Dim cate As String = ""
            Dim pack As String = ""

            Dim strscheme As String = " select a.Item_Code as item ,b.Class_Code  as Flavour ,a.Class_Code as Size ,d.Class_Code as category , c.Class_Code  as Pack  from TSPL_ITEM_DETAILS  a left outer join TSPL_ITEM_DETAILS  b  on a.Item_Code=b.item_code  left outer join TSPL_ITEM_DETAILS c on a.Item_Code=c.Item_Code left outer join TSPL_ITEM_DETAILS d on a.Item_Code=d.Item_Code where  a.Item_Code='" + schitem + "' and a.Class_Name='size' and b.Class_Name='flavour' and c.Class_Name='pack' and d.Class_Name='category'"
            dr = clsDBFuncationality.GetDataTable(strscheme, trans)
            For Each row As DataRow In dr.Rows
                size = row(2).ToString()
                cate = row(3).ToString()
                pack = row(4).ToString()
            Next

            createitem = classcode + size + cate + pack

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return createitem
    End Function

    Public Sub nodecheck()
        Try
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ShowMsg(ByVal Msg As String)
        common.clsCommon.MyMessageBoxShow(Msg, "Scheme Master")
    End Sub
    Private Sub ResetScreen()
        PrepareGrid()
        ddlmrp.Text = ""
        dtpEnd.Value = clsCommon.GETSERVERDATE()
        dtpEnd.Checked = False
        dtpStart.Value = clsCommon.GETSERVERDATE()
        'fndMainItem.txtValue.Enabled = False
        txtAmount.Enabled = False
        txtAmount.Text = "0.00"
        fndUnit.Value = ""
        txtDesc.MaxLength = 100
        txtMDesc.MaxLength = 100
        txtComment.MaxLength = 100
        ddlType.SelectedItem = ddlType.Items(1)
        fndScheme.Value = ""
        RadCheckBox1.Checked = False
        RadCheckBox1.Enabled = False
        fndMainItem.Value = ""
        txtDesc.Text = ""
        txtQuatity.Text = "0"
        txtComment.Text = ""
        txtMDesc.Text = ""
        fndcuscategory.Value = ""
        txtcuscategory.Text = ""
        PageMode = "New"
        ddlType.Enabled = True
        grdScheme.DataSource = Nothing
        grdScheme.Rows.Clear()
        grdScheme.Columns("priceDate").DataSourceNullValue = ""
        btnSave.Text = "Save"
        RadTreeView1.Nodes.Clear()
        SetDataBaseGrid()
        RadTreeView1.Visible = True
        lblitem.Visible = True
        ddlBasicPrice.Text = "0"
        fndScheme.MyReadOnly = False
        ' grdScheme.DataSource = Nothing
        ' grdScheme.Rows.Clear()
    End Sub
    Private Function ValidateSave() As Boolean
        If dtpEnd.Value.Date = dtpEnd.MinDate Then
        Else
            If dtpStart.Value > dtpEnd.Value Then
                ShowMsg("Scheme end date should be greater than or equal to start date")
                Return False
            End If
        End If
        If ddlType.Text = "Sampling" Then
        Else
            If fndUnit.Value = "" Then
                myMessages.blankValue("UOM")
                fndUnit.Focus()
                Return False
            End If
        End If
        If fndScheme.Value.Trim() = "" Then
            ShowMsg("Scheme code can not be blank")
            Return False
        ElseIf ddlType.Text = "Select" Or ddlType.Text = "select" Then
            ShowMsg("Select at least one scheme type")
            Return False
        ElseIf (ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 4) And Convert.ToDecimal(txtQuatity.Text) = 0 Then
            ShowMsg("Quantity can not be 0")
            Return False
        ElseIf ddlType.SelectedIndex = 2 And Convert.ToDecimal(txtQuatity.Text) = 0 Then
            ShowMsg("Quantity can not be 0")
            Return False
        ElseIf ddlType.SelectedIndex = 2 And Convert.ToDecimal(txtAmount.Text) = 0 Then
            ShowMsg("Amount can not be 0")
            Return False
        ElseIf ddlType.SelectedIndex = 2 And Convert.ToDecimal(txtAmount.Text) = 0 Then
            ShowMsg("Amount can not be 0")
            Return False
        ElseIf (ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 4) And fndMainItem.Value = "" Then
            ShowMsg("Main Item should not be blank")

            Return False
        ElseIf (ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 4) And fndUnit.Value.Trim() = "" Then
            myMessages.blankValue("Unit")
            Return False
        ElseIf (ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 4) And (ddlmrp.Text = "Selct" Or ddlmrp.Text = "") Then
            myMessages.blankValue("MRP")
            Return False

            'ElseIf (ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 4) And (ddlBasicPrice.Text = "Selct" Or ddlBasicPrice.Text = "") Then
            '    myMessages.blankValue("Basic Price")
            '    Return False
        ElseIf ddlType.SelectedIndex = 2 And fndMainItem.Value = "" Then
            ShowMsg("Main Item should not be blank")
            Return False

        ElseIf grdScheme.Rows.Count = 0 And ddlType.Text <> "Cash Discount" Then
            ShowMsg("Insert at least one scheme detail")
            Return False
        ElseIf (New BAL.BALScheme).GetSchemeDetail(fndScheme.Value).Rows.Count > 0 And PageMode = "New" Then
            ShowMsg("This scheme name is already in use")
            Return False
        ElseIf ddlType.SelectedIndex = 2 And RadCheckBox1.Checked And Convert.ToDecimal(txtAmount.Text) > 100 Then
            ShowMsg("Cash Discount Percent can not exceed 100")
            Return False
        ElseIf ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 2 Or ddlType.SelectedIndex = 4 Then
            If ddlType.SelectedIndex = 1 Or ddlType.SelectedIndex = 2 Or ddlType.SelectedIndex = 4 Then


                Dim dr As Telerik.WinControls.UI.GridViewRowInfo
                For Each dr In grdScheme.Rows
                    If dr.Index < grdScheme.Rows.Count Then
                        Dim drNext As Telerik.WinControls.UI.GridViewRowInfo
                        For Each drNext In grdScheme.Rows
                            If Convert.ToString(dr.Cells(0).Value) = String.Empty Then
                                ShowMsg("Item Code can not be blank at row positon " & dr.Index + 1 & "")
                                Return False
                            ElseIf dr.Cells("qty").Value.ToString() = "" Then
                                ShowMsg("Item quantity can not be blank of item code " & dr.Cells(0).Value & "")
                                Return False

                            ElseIf dr.Cells("qty").Value = 0 Then
                                ShowMsg("Scheme Item quantity can not be 0 of item code " & dr.Cells(0).Value & "")
                                Return False
                                'ElseIf dr.Cells("basicPrice").Value.ToString() = "" Then
                                '    ShowMsg("Scheme Item basic price can not be blank of item code " & dr.Cells(0).Value & "")
                                '    Return False
                            End If
                            If drNext.Index <> dr.Index Then
                                If drNext.Cells(0).Value = dr.Cells(0).Value Then
                                    ShowMsg("Scheme Detail contains duplicate items on rows " & dr.Index + 1 & " and " & drNext.Index + 1 & "")
                                    Return False
                                End If

                            End If
                        Next

                    End If
                Next
            End If
        Else
            Return True

        End If

        Return True

    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ''If fndScheme.txtValue.Text <> "" And PageMode = "Edit" Then
        ''    If myMessages.deleteConfirm() Then
        ''        Dim Del As New BAL.BALScheme
        ''        Del.DeleteSchemeDetail(fndScheme.txtValue.Text)
        ''        Del.DeleteSchemeMaster(fndScheme.txtValue.Text)
        ''        ShowMsg("Record Deleted Sucessfully")
        ''        ResetScreen()
        ''    End If
        ''End If

        funDelete()
    End Sub

    Public Sub funDelete()
        If fndScheme.Value = "" Then
            Return
        ElseIf myMessages.deleteConfirm Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Dim arrDBName As New List(Of String)
            Dim isSaved As Boolean = False
            arrDBName.Add(objCommonVar.CurrDatabase)
            For ii As Integer = 0 To gvDB.Rows.Count - 1
                If (clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) Then
                    arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
                End If
            Next
            Try

                isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "DeleteSchemeDetail", New SqlParameter("@Code", Me.fndScheme.Value))
                isSaved = clsDBFuncationality.UpdateInSelectedDatabase(trans, arrDBName, "DeleteSchemeMaster", New SqlParameter("@Code", Me.fndScheme.Value))
                If isSaved Then
                    trans.Commit()
                    myMessages.delete()
                    'funNew()
                    ResetScreen()
                    'btnSave.Text = "Save"
                    'btnDelete.Enabled = False
                Else
                    trans.Rollback()
                End If
            Catch ex As Exception
                trans.Rollback()
                myMessages.myExceptions(ex)
            End Try
        Else
            Exit Sub
        End If

    End Sub


    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetScreen()
    End Sub

    Private Sub grdScheme_CellClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdScheme.CellClick
        'If e.ColumnIndex = 0 Then
        '    grdScheme.BeginEdit(True)
        '    Dim x As Int32
        '    Dim height As Int32
        '    Dim width As Int32
        '    Dim txtB As New TextBox



        '    grdfndSch.Location = New Point(x, 0)
        '    grdfndSch.Size = New Size(width, height)
        '    grdfndSch.Cursor = Cursors.Arrow
        '    grdfndSch.ConnectionString = objFunctions.getConnectionStringPO()
        '    grdfndSch.Query = "SELECT ITEMNO,[DESC],STOCKUNIT,INACTIVE FROM ICITEM"
        '    grdfndSch.ValueToSelect = "ITEMNO"
        '    e.
        '    GrdFndOrder.Visible = True

        'End If
    End Sub

    Private Sub grdScheme_CellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles grdScheme.CellFormatting
        Try
            If e.ColumnIndex = 0 Then
                'e.CellElement.Children.Add(grdfndSch)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub txtQuatity_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQuatity.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If Not ((KeyAscii >= System.Windows.Forms.Keys.D0 And KeyAscii <= System.Windows.Forms.Keys.D9) Or (KeyAscii = System.Windows.Forms.Keys.Back) Or Chr(KeyAscii) = "." Or (Chr(KeyAscii) Like "[ ]")) Then
            KeyAscii = 0
            txtQuatity.Focus()
        End If
        If KeyAscii = 0 Then
            e.Handled = True
        End If

        If txtQuatity.Text.IndexOf(".") >= 0 And e.KeyChar = "." Then
            e.Handled = True
        End If

        If txtQuatity.Text.IndexOf(".") > 0 Then
            If txtQuatity.SelectionStart > txtQuatity.Text.IndexOf(".") Then
                If txtQuatity.Text.Length - txtQuatity.Text.IndexOf(".") = 3 Then
                    e.Handled = True
                End If
            End If
        End If

    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If Not ((KeyAscii >= System.Windows.Forms.Keys.D0 And KeyAscii <= System.Windows.Forms.Keys.D9) Or (KeyAscii = System.Windows.Forms.Keys.Back) Or Chr(KeyAscii) = "." Or (Chr(KeyAscii) Like "[ ]")) Then
            KeyAscii = 0
            txtAmount.Focus()
        End If
        If KeyAscii = 0 Then
            e.Handled = True
        End If

        If txtAmount.Text.IndexOf(".") >= 0 And e.KeyChar = "." Then
            e.Handled = True
        End If

        If txtAmount.Text.IndexOf(".") > 0 Then
            If txtAmount.SelectionStart > txtAmount.Text.IndexOf(".") Then
                If txtAmount.Text.Length - txtAmount.Text.IndexOf(".") = 3 Then
                    e.Handled = True
                End If
            End If
        End If

    End Sub

    Private Sub grdScheme_CellValidating(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles grdScheme.CellValidating
        Try

            If e.Column.FieldName = "Quantity" Then
                Dim value = 0
                Try
                    Convert.ToInt16(e.Value)
                Catch ex As Exception
                    e.Cancel = True
                End Try

            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub grdScheme_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles grdScheme.CellBeginEdit
        Try
            If TypeOf Me.grdScheme.CurrentColumn Is GridViewMultiComboBoxColumn Then
                Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.grdScheme.ActiveEditor, RadMultiColumnComboBoxElement)
                editor.AutoSizeDropDownToBestFit = True
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
        'If TypeOf Me.grdScheme.CurrentColumn Is GridViewComboBoxColumn Then
        '    If grdScheme.CurrentColumn.Name = "unitCode" Or grdScheme.CurrentColumn.Name = "mrp" Or grdScheme.CurrentColumn.Name = "basicPrice" Then
        '        If grdScheme.CurrentRow.Cells("priceDate").Value = "" Then
        '            e.Cancel = True
        '        End If
        '    End If
        'End If
    End Sub

    Private Sub grdScheme_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdScheme.CellEditorInitialized
        If TypeOf Me.grdScheme.CurrentColumn Is GridViewMultiComboBoxColumn Then

            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.grdScheme.ActiveEditor, RadMultiColumnComboBoxElement)

            editor.AutoSizeDropDownToBestFit = True

            editor.EditorControl.MasterTemplate.BestFitColumns()

            editor.DropDownStyle = RadDropDownStyle.DropDown

            editor.AutoFilter = True

            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then

                Dim autoFilter As FilterDescriptor = New FilterDescriptor(editor.ValueMember, FilterOperator.StartsWith, "")

                autoFilter.IsFilterEditor = True

                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If


        End If


    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub grdScheme_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles grdScheme.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is grdScheme.Columns(colitemno) Then
                    OpenICodeList(False)
                    'ElseIf e.Column Is gvItems.Columns(coluom) Then
                    '    openuom(False)
                End If
                isCellValueChangedOpen = False
            End If
            If e.ColumnIndex = 0 Then
                grdScheme.CurrentRow.Cells(1).Value = connectSql.RunScalar("select Item_Desc  from TSPL_ITEM_MASTER  where Item_Code = '" + grdScheme.CurrentRow.Cells(0).Value + "'")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster
        If ddlType.Text = "Cash Discount" Then
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(grdScheme.CurrentRow.Cells(colitemno).Value), "", isButtonClick)
        ElseIf ddlType.Text = "Quantitative Discount" Then
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(grdScheme.CurrentRow.Cells(colitemno).Value), "F", isButtonClick)
        ElseIf ddlType.Text = "Sampling" Then
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(grdScheme.CurrentRow.Cells(colitemno).Value), "F", isButtonClick)
        ElseIf ddlType.Text = "Promotional" Then
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(grdScheme.CurrentRow.Cells(colitemno).Value), "P", isButtonClick)
        Else
            obj = clsItemMaster.FinderForItem(clsCommon.myCstr(grdScheme.CurrentRow.Cells(colitemno).Value), "", isButtonClick)
        End If



        ' Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(grdScheme.CurrentRow.Cells(colitemno).Value), "F", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            grdScheme.CurrentRow.Cells(colitemno).Value = obj.Item_Code
            'grdScheme.CurrentRow.Cells(coldesc).Value = obj.Item_Desc
            'gvItems.CurrentRow.Cells(coluom).Value = obj.Unit_Code
        Else
            grdScheme.CurrentRow.Cells(colitemno).Value = ""
            ' grdScheme.CurrentRow.Cells(coldesc).Value = ""
            'gvItems.CurrentRow.Cells(coldesc).Value = ""
        End If

    End Sub





    Private Sub lblUnit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblUnit.Click

    End Sub

    Private Sub grdScheme_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles grdScheme.EditorRequired
        Try

            Dim mrp As GridViewComboBoxColumn = TryCast(grdScheme.Columns("mrp"), GridViewComboBoxColumn)
            Dim unit As GridViewComboBoxColumn = TryCast(grdScheme.Columns("unitCode"), GridViewComboBoxColumn)
            Dim priceDate As GridViewComboBoxColumn = TryCast(grdScheme.Columns("priceDate"), GridViewComboBoxColumn)
            Dim basicPrice As GridViewComboBoxColumn = TryCast(grdScheme.Columns("basicPrice"), GridViewComboBoxColumn)
            Dim sql As String
            Dim ds As New DataSet()
            If grdScheme.CurrentColumn.Name = "priceDate" Then
                sql = "select distinct convert(varchar(10), Start_date,103) as Start_Date  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + grdScheme.CurrentRow.Cells(0).Value + "'"
                ds = connectSql.RunSQLReturnDS(sql)
                priceDate.DataSource = ds.Tables(0)
                priceDate.ValueMember = "Start_Date"
            ElseIf grdScheme.CurrentColumn.Name = "unitCode" Then

                If clsCommon.myLen(grdScheme.CurrentRow.Cells(0).Value) = 0 Then
                    priceDate.DataSource = Nothing
                    unit.DataSource = Nothing
                    mrp.DataSource = Nothing
                    basicPrice.DataSource = Nothing
                Else


                    If grdScheme.CurrentRow.Cells("priceDate").Value Is Nothing Then
                        common.clsCommon.MyMessageBoxShow("Please select price date.")
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(grdScheme.CurrentRow.Cells("priceDate").Value.ToString()) Then
                        common.clsCommon.MyMessageBoxShow("Please select price date.")
                        Exit Sub
                    Else
                        sql = "select distinct Uom  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + grdScheme.CurrentRow.Cells(0).Value + "' AND Start_Date='" + Format(CDate(grdScheme.CurrentRow.Cells("priceDate").Value), "MM/dd/yyyy") + "'"
                        ds = connectSql.RunSQLReturnDS(sql)
                        unit.DataSource = ds.Tables(0)
                        unit.ValueMember = "Uom"
                    End If
                End If

            ElseIf grdScheme.CurrentColumn.Name = "mrp" Then
                If clsCommon.myLen(grdScheme.CurrentRow.Cells(0).Value) = 0 Then

                    priceDate.DataSource = Nothing
                    unit.DataSource = Nothing
                    mrp.DataSource = Nothing
                    basicPrice.DataSource = Nothing
                Else

                    If grdScheme.CurrentRow.Cells("priceDate").Value Is Nothing Then
                        common.clsCommon.MyMessageBoxShow("Please select price date.")
                        Exit Sub
                    End If
                    If String.IsNullOrEmpty(grdScheme.CurrentRow.Cells("priceDate").Value.ToString()) Then
                        common.clsCommon.MyMessageBoxShow("Please select price date.")
                        Exit Sub
                    Else
                        sql = "select distinct Item_Basic_Net  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + grdScheme.CurrentRow.Cells(0).Value + "' AND Start_Date='" + Format(CDate(grdScheme.CurrentRow.Cells("priceDate").Value), "MM/dd/yyyy") + "' AND Uom='" + grdScheme.CurrentRow.Cells("unitCode").Value + "'"
                        ds = connectSql.RunSQLReturnDS(sql)
                        mrp.DataSource = ds.Tables(0)
                        mrp.ValueMember = "Item_Basic_Net"
                    End If
                End If
                'ElseIf grdScheme.CurrentColumn.Name = "basicPrice" Then

                '    If clsCommon.myLen(grdScheme.CurrentRow.Cells(0).Value) = 0 Then

                '        priceDate.DataSource = Nothing
                '        unit.DataSource = Nothing
                '        mrp.DataSource = Nothing
                '        basicPrice.DataSource = Nothing
                '    Else

                '        If grdScheme.CurrentRow.Cells("priceDate").Value Is Nothing Then
                '            common.clsCommon.MyMessageBoxShow("Please select price date.")
                '            Exit Sub
                '        End If
                '        If String.IsNullOrEmpty(grdScheme.CurrentRow.Cells("priceDate").Value.ToString()) Then
                '            common.clsCommon.MyMessageBoxShow("Please select price date.")
                '            Exit Sub
                '        Else
                '            basicPrice.DataSource = Nothing
                '            sql = "select distinct Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + grdScheme.CurrentRow.Cells(0).Value + "' AND Start_Date='" + Format(CDate(grdScheme.CurrentRow.Cells("priceDate").Value), "MM/dd/yyyy") + "' AND Uom='" + grdScheme.CurrentRow.Cells("unitCode").Value + "' AND Item_Basic_Net='" + Convert.ToDecimal(grdScheme.CurrentRow.Cells("mrp").Value).ToString() + "'"
                '            ds = connectSql.RunSQLReturnDS(sql)
                '            basicPrice.DataSource = ds.Tables(0)
                '            basicPrice.ValueMember = "Item_Basic_Price"

                '            'grdScheme.CurrentRow.Cells("unitCode").Value = 0
                '        End If
                '    End If
            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try



    End Sub
    Private Sub exportmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles exportmenu.Click
        Dim query As String = "SELECT TSPL_SCHEME_MASTER.Scheme_Code as 'Scheme Code', TSPL_SCHEME_MASTER.Scheme_Desc as 'Scheme Description', TSPL_SCHEME_MASTER.Start_Date as 'Start Date',TSPL_SCHEME_MASTER.End_Date as 'End date', TSPL_SCHEME_MASTER.Scheme_Type as 'Scheme Type', TSPL_SCHEME_MASTER.Main_Item_Code as 'Main Itme Code', TSPL_SCHEME_MASTER.Main_Item_desc as 'Main Item Description', TSPL_SCHEME_MASTER.Main_Item_Qty as 'Main Item Qty', TSPL_SCHEME_MASTER.Amount as 'Amount',TSPL_SCHEME_MASTER.Comments as 'Comments', TSPL_SCHEME_MASTER.Main_Item_UOM as 'Main Item Uom', TSPL_SCHEME_MASTER.MRP as 'MRP' ,TSPL_SCHEME_DETAILS.Scheme_Code_Auto as 'Scheme Code Auto',TSPL_SCHEME_DETAILS.Scheme_Item_Code as 'Scheme Item Code', TSPL_SCHEME_DETAILS.Scheme_Item_Desc as 'Scheme Item Description', TSPL_SCHEME_DETAILS.Qty as 'Qty',TSPL_SCHEME_DETAILS.Remarks as 'Remarks', TSPL_SCHEME_DETAILS.UOM as 'UOM', TSPL_SCHEME_DETAILS.MRP AS Mrp1,TSPL_SCHEME_DETAILS.Price_Date as 'Price date'FROM  TSPL_SCHEME_MASTER INNER JOIN TSPL_SCHEME_DETAILS ON TSPL_SCHEME_MASTER.Scheme_Code = TSPL_SCHEME_DETAILS.Scheme_Code"
        transportSql.ExporttoExcel(query, Me)
    End Sub

    Private Sub importmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importmenu.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Scheme Code", "Scheme Description", "Start Date", "End date", "Scheme Type", "Main Itme Code", "Main Item Description", "Main Item Qty", "Amount", "Comments", "Main Item Uom", "MRP", "Scheme Code Auto", "Scheme Item Code", "Scheme Item Description", "Qty", "Remarks", "UOM", "Mrp1", "Price date") Then
            Dim trans As SqlTransaction = Nothing
            Dim linno As Integer = 0
            Dim Count As String = ""
            Try
                ''connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                'clsCommon.ProgressBarShow()
                clsCommon.ProgressBarPercentShow()
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    Count = clsCommon.myCstr(dgrv.Index + 2)
                    linno += 1
                    clsCommon.ProgressBarPercentUpdate((linno * 100) / dgv.RowCount - 1, "Importing " + clsCommon.myCstr(linno) + "/" + clsCommon.myCstr(dgv.RowCount - 1))
                    Dim strscheme_code As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    If clsCommon.myLen(strscheme_code) > 12 Or String.IsNullOrEmpty(strscheme_code) Then
                        Throw New Exception("Check the length of scheme Code/Blank")

                    End If
                    Dim strscheme_description As String = clsCommon.myCstr(dgrv.Cells(1).Value)
                    If clsCommon.myLen(strscheme_description) > 100 Then
                        Throw New Exception("Check the length of Scheme Description / Blank")

                    End If
                    Dim strscheme_type As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    If clsCommon.myLen(strscheme_type) > 10 Or strscheme_type = "" Then
                        Throw New Exception("Check the Length of Scheme type/Blank ")

                    End If
                    Dim strmain_item_code As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    If clsCommon.myLen(strmain_item_code) > 50 Then
                        Throw New Exception("Check the Length of main item code/Blank")

                    End If
                    Dim strmain_itemcode_desc As String = clsCommon.myCstr(dgrv.Cells(6).Value)
                    If clsCommon.myLen(strmain_itemcode_desc) > 100 Then
                        Throw New Exception("Check the length of main item code description/blank")

                    End If
                    Dim strmain_item_qty As Decimal = CDec(dgrv.Cells(7).Value.ToString())
                    If strmain_item_qty < 0 Then
                        Throw New Exception("main item qty should not be blank")

                    End If
                    Dim stramount As Decimal = CDec(dgrv.Cells(8).Value.ToString())
                    If stramount < 0 Then
                        Throw New Exception("Amount should not be blank")

                    End If
                    Dim strcomments As String = clsCommon.myCstr(dgrv.Cells(9).Value)
                    If clsCommon.myLen(strcomments) > 200 Then
                        Throw New Exception("Check the length of Comments")

                    End If
                    Dim strmainitem_uom As String = clsCommon.myCstr(dgrv.Cells(10).Value)
                    If clsCommon.myLen(strmainitem_uom) > 12 Then
                        Throw New Exception("Check the length of Main item Uom")

                    End If
                    Dim strschemeitem_code As String = clsCommon.myCstr(dgrv.Cells(13).Value)
                    If clsCommon.myLen(strschemeitem_code) > 12 Or String.IsNullOrEmpty(strschemeitem_code) Then
                        Throw New Exception("Check the length of Scheme Item Code/blank")

                    End If
                    Dim strschemeitem_desc As String = clsCommon.myCstr(dgrv.Cells(14).Value)
                    If clsCommon.myLen(strschemeitem_desc) > 100 Then
                        Throw New Exception("Check the length of scheme itme descriiption")

                    End If
                    Dim strremarks As String = clsCommon.myCstr(dgrv.Cells(16).Value)
                    If clsCommon.myLen(strremarks) > 200 Then
                        Throw New Exception("Check the length of Remarks")

                    End If
                    Dim struom As String = clsCommon.myCstr(dgrv.Cells(17).Value)
                    If clsCommon.myLen(struom) > 12 Then
                        Throw New Exception("Check the length of Uom")

                    End If
                    Dim mrp1 As Double = clsCommon.myCdbl(dgrv.Cells(18).Value)
                    Dim start_date As String = Format(dgrv.Cells(2).Value, "dd/MMM/yyyy")
                    Dim end_date As String = Nothing
                    If clsCommon.myLen(dgrv.Cells(3).Value) > 0 Then
                        end_date = Format(dgrv.Cells(3).Value, "dd/MMM/yyyy")
                    End If


                    Dim mrp As Double = clsCommon.myCdbl(dgrv.Cells(11).Value)
                    Dim schemecode_auto As Integer = CInt(dgrv.Cells(12).Value.ToString)
                    Dim qty As Integer = CInt(dgrv.Cells(15).Value.ToString)


                    Dim coll As New Hashtable()
                    Try
                        clsCommon.AddColumnsForChange(coll, "scheme_code", strscheme_code)
                        clsCommon.AddColumnsForChange(coll, "scheme_desc", strscheme_description)
                        clsCommon.AddColumnsForChange(coll, "start_date", start_date)
                        clsCommon.AddColumnsForChange(coll, "end_date", end_date, True)
                        clsCommon.AddColumnsForChange(coll, "scheme_type", strscheme_type)
                        clsCommon.AddColumnsForChange(coll, "main_item_code", strmain_item_code)
                        clsCommon.AddColumnsForChange(coll, "main_item_desc", strmain_itemcode_desc)
                        clsCommon.AddColumnsForChange(coll, "main_item_qty", strmain_item_qty)
                        clsCommon.AddColumnsForChange(coll, "amount", stramount)
                        clsCommon.AddColumnsForChange(coll, "comments", strcomments)
                        clsCommon.AddColumnsForChange(coll, "created_by", userCode)
                        clsCommon.AddColumnsForChange(coll, "created_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))
                        clsCommon.AddColumnsForChange(coll, "modify_by", userCode)
                        clsCommon.AddColumnsForChange(coll, "modify_date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans)))
                        clsCommon.AddColumnsForChange(coll, "comp_code", companyCode)
                        clsCommon.AddColumnsForChange(coll, "main_item_uom", strmainitem_uom)
                        clsCommon.AddColumnsForChange(coll, "mrp", mrp)
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        'Dim query As String = "insert into TSPL_SCHEME_MASTER (scheme_code,scheme_desc,start_date,end_date,scheme_type,main_item_code,main_item_desc,main_item_qty,amount,comments,created_by,created_date,modify_by,modify_date,comp_code,main_item_uom,mrp)values('" + clsCommon.myCstr(strscheme_code) + "','" + clsCommon.myCstr(strscheme_description) + "','" + start_date + "','" + end_date + "','" + clsCommon.myCstr(strscheme_type.ToString()) + "','" + clsCommon.myCstr(strmain_item_code) + "','" + clsCommon.myCstr(strmain_itemcode_desc) + "','" + strmain_item_qty + "','" + stramount + "','" + strcomments.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "','" + clsCommon.myCstr(strmainitem_uom) + "','" + clsCommon.myCstr(mrp) + "')"
                        ''connectSql.RunSqlTransaction(trans, query)
                    Catch ex As Exception
                        Dim s1 As String = ex.ToString().Substring(0, 46)
                        If ex.ToString().Substring(0, 46) = "System.Data.SqlClient.SqlException: Violation " OrElse ex.Message.ToString().Substring(0, 35) = "Violation of PRIMARY KEY constraint" Then
                            'Dim query1 As String = "update TSPL_SCHEME_MASTER set scheme_desc='" + strscheme_description + "',start_date='" + start_date.ToString() + "',End_date='" + end_date.ToString() + "',scheme_type='" + strscheme_type.ToString() + "',main_item_code='" + strmain_item_code.ToString() + "',main_item_desc='" + strmain_itemcode_desc.ToString() + "',main_item_qty='" + strmain_item_qty.ToString() + "',Amount='" + stramount.ToString() + "',commnets='" + strcomments.ToString() + "',main_item_uom='" + strmainitem_uom.ToString() + "',mrp='" + mrp.ToString() + "'where scheme_code='" + strscheme_code.ToString() + "'"
                            'connectSql.RunSqlTransaction(trans, query1)
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SCHEME_MASTER", OMInsertOrUpdate.Update, "scheme_code='" + strscheme_code + "'", trans)
                        End If
                    End Try
                    'Dim col1 As New Hashtable
                    'Try
                    '    'clsCommon.AddColumnsForChange(col1, "")
                    '    ''    Dim query2 As String = "insert into tspl_scheme_details values('" + schemecode_auto.ToString() + "','" + strscheme_code.ToString() + "','" + strscheme_description.ToString() + "','" + strschemeitem_code.ToString() + "','" + strschemeitem_desc.ToString() + "','" + qty.ToString() + "','" + strremarks.ToString() + "','" + userCode + "','" + connectSql.serverDate() + "','" + userCode + "','" + connectSql.serverDate() + "','" + companyCode + "','" + struom.ToString() + "','" + mrp1.ToString() + "','" + price_date1 + "'"
                    '    ''    connectSql.RunSqlTransaction(trans, query2)
                    'Catch ex As Exception
                    '    ''    Dim s1 As String = ex.ToString().Substring(0, 46)
                    '    ''    If ex.ToString().Substring(0, 46) = "System.Data.SqlClient.SqlException: Violation " Then
                    '    ''      
                    '    ''    End If
                    'End Try
                Next
                trans.Commit()
                'clsCommon.ProgressBarHide()
                clsCommon.ProgressBarPercentHide()
                common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                'clsCommon.ProgressBarHide()
                clsCommon.ProgressBarPercentHide()
                myMessages.myExceptions(ex)


            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Private Sub ddlmrp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlmrp.SelectedIndexChanged
        Try
            If Not (ddlmrp.Text = "Select" Or ddlmrp.Text = "") Then
                Dim sql As String = "select distinct Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + fndMainItem.Value + "' AND Uom='" + fndUnit.Value + "' AND Item_Basic_Net='" + ddlmrp.Text + "'"
                Dim ds1 As New DataSet()
                ds1 = connectSql.RunSQLReturnDS(sql)
                ddlBasicPrice.DataSource = ds1.Tables(0)
                ddlBasicPrice.ValueMember = "Item_Basic_Price"
                ddlBasicPrice.DisplayMember = "Item_Basic_Price"
            End If

            ' Dim strSql As String
            Dim decConvF, decConvMrp As Decimal
            If fndUnit.Value = "FC" Then
                decConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndMainItem.Value & "' and UOM_Code='FB'"))
                decConvMrp = clsCommon.myCdbl(ddlmrp.Text) / decConvF
                txtmrpbottle.Text = decConvMrp
                txtconvrate.Text = decConvF
                lblmrp2.Text = "MRP in Bottle"
            ElseIf fndUnit.Value = "FB" Then
                decConvF = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & fndMainItem.Value & "' and UOM_Code='FC'"))
                decConvMrp = clsCommon.myCdbl(ddlmrp.Text) / decConvF
                txtmrpbottle.Text = decConvMrp
                txtconvrate.Text = decConvF
                lblmrp2.Text = "MRP in Case"
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "SCHEME-M"
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
    'End Function

    ''To bind item according to the pack size
    Private Sub binditem()
        Dim ds As DataSet
        Dim Sql As String
        Dim dt As New DataTable()
        If fndMainItem.Value.Trim().Length = 8 Then
            RadTreeView1.Nodes.Clear()
            Dim value As String = fndMainItem.Value.Substring(2, 6) 'ddlpacksize.Text
            value = "%" + value + "%"
            'Sql = "select Item_Code  from tspl_item_master where item_code like '" + value + "' "
            Sql = "select Distinct(Item_Code)  from TSPL_ITEM_PRICE_MASTER where item_code like '" + value + "' "
            ds = connectSql.RunSQLReturnDS(Sql)
            dt = ds.Tables(0)
            Dim node As New RadTreeNode()
            node.Text = "Item"
            ' node.BackColor = Color.Blue
            RadTreeView1.Nodes.Add(node)
            For i As Integer = 0 To dt.Rows.Count - 1
                If Not dt.Rows(i)(0).ToString() = fndMainItem.Value Then
                    node.Nodes.Add(dt.Rows(i)(0).ToString())
                End If

            Next
        End If
    End Sub
    Private Sub fndcuscategory__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcuscategory._MYValidating
        Dim qry As String = "select CUST_CATEGORY_CODE as Code,CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER"
        fndcuscategory.Value = clsCommon.ShowSelectForm("POShpToLocFltrFND", qry, "Code", "", fndcuscategory.Value, "Code", isButtonClicked)
        txtcuscategory.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CUST_CATEGORY_DESC from TSPL_CUSTOMER_CATEGORY_MASTER where CUST_CATEGORY_CODE='" + fndcuscategory.Value + "'"))
    End Sub

    Private Sub RadTreeView1_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.RadTreeViewEventArgs) Handles RadTreeView1.NodeCheckedChanged
        Dim node As RadTreeNode = e.Node
        If node.Checked = True Then
            For Each nd1 As RadTreeNode In node.Nodes
                nd1.Checked = True
            Next
        Else
            For Each nd1 As RadTreeNode In node.Nodes
                nd1.Checked = False
            Next
        End If

    End Sub

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




    Private Sub FrmSchmeMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso grdScheme.CurrentCell IsNot Nothing Then

            isCellValueChangedOpen = True
            If grdScheme.CurrentColumn Is grdScheme.Columns(colitemno) Then
                grdScheme.CurrentColumn = grdScheme.Columns("description")
                OpenICodeList(True)
                grdScheme.CurrentColumn = grdScheme.Columns(colitemno)
            End If

            isCellValueChangedOpen = False

        End If
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData1()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If

    End Sub

    Private Sub ddlBasicPrice_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddlBasicPrice.KeyPress

        If ((e.KeyChar >= Chr(48) And e.KeyChar <= Chr(57)) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(48) Or e.KeyChar = Chr(46)) Then
        Else
            e.Handled = True
        End If

    End Sub

  

    Private Sub fndMainItem__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndMainItem._MYValidating

        Dim qry As String = "select item_code as [Code] ,item_desc as [Description] from TSPL_ITEM_MASTER  "
        fndMainItem.Value = clsCommon.ShowSelectForm("SchmeMasterManFND", qry, "Code", "item_type='F'", fndMainItem.Value, "", isButtonClicked)
        LoadMainItemData()

    End Sub
    Public Sub LoadMainItemData()
        Dim strqry As String = "select count(*) from TSPL_ITEM_MASTER where item_code='" + fndMainItem.Value + "'"
        Dim chk As Integer = CInt(connectSql.RunScalar(strqry))

        If (chk <> 0) Then
            If fndMainItem.Value.Trim <> "" Then
                txtMDesc.Text = (New BAL.BALScheme).FillItemDesc(fndMainItem.Value)
            End If
            'fndUnit.ConnectionString = connectSql.SqlCon()
            'fndUnit.Query = "SELECT DISTINCT UM.Unit_Code as 'Unit Code', UM.Unit_Desc as Description, UM.Conv_Factor as 'Conversion Factor' " & _
            '" FROM TSPL_ITEM_PRICE_MASTER AS PM INNER JOIN  TSPL_UNIT_MASTER AS UM ON PM.UOM = UM.Unit_Code WHERE PM.Item_Code='" + fndMainItem.Value + "'"
            'fndUnit.ValueToSelect = "Unit Code"
            'fndUnit.ValueToSelect1 = "Unit Code"
            'fndUnit.Caption = "Unit of Measures"
            fillMRP()
            binditem()
        Else
            RadTreeView1.Nodes.Clear()
        End If
    End Sub

    
    Private Sub fndUnit__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndUnit._MYValidating

        Dim qry As String = "SELECT distinct UM.Unit_Code as 'Code', UM.Unit_Desc as Description, UM.Conv_Factor as 'Conversion Factor' "
        qry += " FROM TSPL_ITEM_PRICE_MASTER AS PM INNER JOIN  TSPL_UNIT_MASTER AS UM ON PM.UOM = UM.Unit_Code "
        fndUnit.Value = clsCommon.ShowSelectForm("SchmMasterUntFND", qry, "Code", "PM.Item_Code='" + fndMainItem.Value + "'", fndUnit.Value, "", isButtonClicked)
        fillMRP()

    End Sub

    Private Sub fndScheme__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndScheme._MYValidating

        Dim str As String = "select count(*) from TSPL_SCHEME_MASTER where Scheme_Code ='" + fndScheme.Value + "' "

        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))

        If no = 0 Then
            fndScheme.MyReadOnly = False
        Else
            fndScheme.MyReadOnly = True
        End If

        If fndScheme.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "SELECT [Scheme_Code] as [Code]" & _
         "     ,[Scheme_Desc] as [Description]" & _
          "    ,[Start_Date] as [Start Date]" & _
           "   ,[End_Date] as [End Date]" & _
            "  ,[Scheme_Type] as [Scheme Type]" & _
             " ,[Main_Item_Code] as [Main Item]" & _
             " ,[Main_Item_desc] as [Main Item Description]" & _
             "  ,[Cust_Cate] as [Customer Category]" & _
             "  ,[Cust_Cate_desc] as [Customer Category Description]" & _
             " ,[Main_Item_Qty]  as [Main Item Quantity]   " & _
                " FROM [dbo].[TSPL_SCHEME_MASTER]"
            fndScheme.Value = clsCommon.ShowSelectForm("Schme Master", qry, "Code", "", fndScheme.Value, "", isButtonClicked)
            LoadSchemeData()
        End If
    End Sub
    Public Sub LoadSchemeData()
        Dim strqry As String = "select count(*) from TSPL_SCHEME_MASTER where Scheme_Code='" + fndScheme.Value + "'"
        Dim chk As Integer = CInt(connectSql.RunScalar(strqry))

        If chk <> 0 Then
            funfill(fndScheme.Value)
        Else

            PrepareGrid()
            ddlmrp.Text = ""
            dtpStart.Format = DateTimePickerFormat.Short
            dtpEnd.Format = DateTimePickerFormat.Short
            dtpEnd.Value = Date.Now
            dtpStart.Value = Date.Now
            'fndMainItem.txtValue.Enabled = False
            txtAmount.Enabled = False
            txtAmount.Text = "0.00"
            fndUnit.Value = ""
            txtDesc.MaxLength = 100
            txtMDesc.MaxLength = 100
            txtComment.MaxLength = 100
            ddlType.SelectedItem = ddlType.Items(1)

            RadCheckBox1.Checked = False
            RadCheckBox1.Enabled = False
            fndMainItem.Value = ""
            txtDesc.Text = ""
            txtQuatity.Text = "0"
            txtComment.Text = ""
            txtMDesc.Text = ""
            fndcuscategory.Value = ""
            txtcuscategory.Text = ""
            PageMode = "New"
            ddlType.Enabled = True
            grdScheme.DataSource = Nothing
            grdScheme.Rows.Clear()
            grdScheme.Columns("priceDate").DataSourceNullValue = ""
            dtpEnd.Value = dtpEnd.MinDate
            btnSave.Text = "Save"
            RadTreeView1.Nodes.Clear()
            SetDataBaseGrid()
            RadTreeView1.Visible = True
            lblitem.Visible = True
            ddlBasicPrice.Text = "0"

        End If
    End Sub

    Private Sub fndScheme__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndScheme._MYNavigator
        Dim qst As String = "SELECT [Scheme_Code] as [Scheme Code]" & _
         "     ,[Scheme_Desc] as [Description]" & _
          "    ,[Start_Date] as [Start Date]" & _
           "   ,[End_Date] as [End Date]" & _
            "  ,[Scheme_Type] as [Scheme Type]" & _
             " ,[Main_Item_Code] as [Main Item]" & _
             " ,[Main_Item_desc] as [Main Item Description]" & _
             "  ,[Cust_Cate] as [Customer Category]" & _
             "  ,[Cust_Cate_desc] as [Customer Category Description]" & _
             " ,[Main_Item_Qty]  as [Main Item Quantity]   " & _
                " FROM [dbo].[TSPL_SCHEME_MASTER] where 2=2 "
        Select Case NavType
            Case NavigatorType.Current
                qst += " and [dbo].[TSPL_SCHEME_MASTER] .Scheme_Code in ('" + fndScheme.Value + "')"
            Case NavigatorType.Next
                qst += " and [dbo].[TSPL_SCHEME_MASTER] .Scheme_Code in (select min(Scheme_Code ) from [dbo].[TSPL_SCHEME_MASTER] where Scheme_Code  >'" + fndScheme.Value + "')"
            Case NavigatorType.First
                qst += " and [dbo].[TSPL_SCHEME_MASTER] .Scheme_Code in (select MIN(Scheme_Code ) from [dbo].[TSPL_SCHEME_MASTER])"

            Case NavigatorType.Last
                qst += " and [dbo].[TSPL_SCHEME_MASTER] .Scheme_Code in (select Max(Scheme_Code ) from [dbo].[TSPL_SCHEME_MASTER])"
            Case NavigatorType.Previous
                qst += " and [dbo].[TSPL_SCHEME_MASTER] .Scheme_Code in (select Max(Scheme_Code ) from [dbo].[TSPL_SCHEME_MASTER] where Scheme_Code  <'" + fndScheme.Value + "')"
        End Select
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndScheme.Value = clsCommon.myCstr(dt.Rows(0)("Scheme Code"))
            txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        LoadSchemeData()
    End Sub

    Private Sub fndScheme_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndScheme.Leave

    End Sub

    Private Sub ddlmrp_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlmrp.TextChanged

    End Sub
End Class