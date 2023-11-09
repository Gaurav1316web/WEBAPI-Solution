'Developed By -BibhuPrasad Parida
'Database - TSPLERP
'Table - tspl_unit_master
'Start Date -
'End Date -
' Added By abhishek as on 19/10/2012 6:20 pm if CLear the perticular Cell then
'Added BY abhishek as on 19/10/2012 2:52 Pm For Delete Row Event If Unit_Code Exist in Tspl_Uom_Details then it should not delete.
'-06/03/2013:04:04PM--Updated By---Pankaj Kumar Chaudhary--Length of Description In Grid Was not Proper------------By====Anuj
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports System.Text.RegularExpressions
Imports common

Public Class frmUnitOfCode
    Inherits FrmMainTranScreen

#Region "Variable"
    Dim userCode, companyCode As String
    Dim dr As DataTable
    Dim ds As New DataSet()
    Dim sql As String
    Const colCategoryType As String = "CategoryType"
    Const colGSTUnit As String = "colGSTUnit"
    Dim isLoadData As Boolean = True
#End Region

#Region "Constructor"
    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        userCode = user
        companyCode = company

    End Sub
#End Region

#Region "KeyPress Event"
    Private Sub keyPress1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        '  e.Handled = globalFunc.TrapKey(Asc(e.KeyChar))
        If MasterTemplate.CurrentColumn.Index = 2 Then

            If (Microsoft.VisualBasic.Asc(e.KeyChar) >= 48) And (Microsoft.VisualBasic.Asc(e.KeyChar) <= 57) Then
                e.Handled = False
            ElseIf (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
                e.Handled = False
            Else

                e.Handled = True
            End If

        End If
    End Sub
#End Region
#Region "Page Load"
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.unitMaster)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnsave.Visible = True Then
            rdmenuimport.Enabled = True
            rdmenuexport.Enabled = True
        Else
            rdmenuimport.Enabled = False
            rdmenuexport.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        'rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmUnitOfCode_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag Then
            'DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub
    Private Sub frmUnitOfCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Ticket No : BHA/31/08/18-000500 By Prabhakar Add Category and Packet Type column
        Dim repoStockUnit As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoStockUnit.FormatString = ""
        repoStockUnit.HeaderText = "Is Category Type"
        repoStockUnit.Name = colCategoryType
        repoStockUnit.Width = 100
        repoStockUnit.ReadOnly = False
        repoStockUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoStockUnit.DataSource = GetCategoryType()
        repoStockUnit.ValueMember = "Code"
        repoStockUnit.DisplayMember = "Name"
        MasterTemplate.MasterTemplate.Columns.Add(repoStockUnit)


        'Dim repoText As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repoText.FormatString = ""
        'repoText.HeaderText = "RM Item Code"
        'repoText.Name = colRMCode
        'repoText.HeaderImage = Global.XpertERPJobWorkOutward.My.Resources.Resources.search4
        'repoText.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoText.Width = 100
        'gv.MasterTemplate.Columns.Add(repoText)

        Dim repoGSTUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGSTUnit.FormatString = ""
        repoGSTUnit.HeaderText = "GST Unit"
        repoGSTUnit.Name = colGSTUnit
        repoGSTUnit.Width = 100
        repoGSTUnit.ReadOnly = False
        repoGSTUnit.HeaderImage = My.Resources.Resources.search4
        repoGSTUnit.TextImageRelation = TextImageRelation.TextBeforeImage
        'repoGSTUnit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'repoGSTUnit.DataSource = GetGSTCode()
        'repoGSTUnit.ValueMember = "Code"
        'repoGSTUnit.DisplayMember = "Name"
        MasterTemplate.MasterTemplate.Columns.Add(repoGSTUnit)

        SetUserMgmtNew()
        ds = RunSQLReturnDS("select unit_code [Unit of Measure],unit_desc [Description],Floor(Conv_Factor) as  [Default Conversion Factor], (case empty when 'Y' THEN 'True' else 'False' end) as empty, (case create_Price when 'Y' THEN 'True' else 'False' end) as price, Cast(Case When Weight_Type='Y' Then 1 Else 0 End as Bit) as Weight_Type, Cast(Case When Crate_Type='Y' Then 1 Else 0 End as Bit) as Crate_Type,Cast(Case When RT_Rate='Y' Then 1 Else 0 End as Bit) as RT_Rate,Cast(Case When Crate_Type='Y' Then 1 Else 0 End as Bit) as Crate_Type,Cast(Case When Ltr_Type='Y' Then 1 Else 0 End as Bit) as Ltr_Type,Cast(Case When Box_Type='Y' Then 1 Else 0 End as Bit) as Box_Type,Cast(Case When Can_Type='Y' Then 1 Else 0 End as Bit) as Can_Type, Cast(Case When Packet_Type='Y' Then 1 Else 0 End as Bit) as Packet_Type,Cast(IsDefault AS Bit) as IsDefault,Case When Item_Category='K' Then 'KG' when Item_Category='L' then 'LTR'   Else '' End  as Item_Category,isnull (GST_UNIT_CODE,'') as GST_UNIT_CODE  from tspl_unit_master")

        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            '  check = Convert.ToString(ds.Tables(0).Rows(i)("empty"))
            Dim row As GridViewRowInfo = MasterTemplate.Rows.AddNew()
            row.Cells("uom").Value = Convert.ToString(ds.Tables(0).Rows(i)("Unit of Measure"))
            row.Cells("desc").Value = Convert.ToString(ds.Tables(0).Rows(i)("Description"))
            row.Cells("conversion").Value = Convert.ToString(ds.Tables(0).Rows(i)("Default Conversion Factor"))
            row.Cells("empty").Value = Convert.ToString(ds.Tables(0).Rows(i)("empty"))
            row.Cells("createprice").Value = Convert.ToString(ds.Tables(0).Rows(i)("price"))
            row.Cells("colIsWeightType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Weight_Type"))
            row.Cells("ColIsCrateType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Crate_Type"))
            row.Cells("colRTrate").Value = Convert.ToString(ds.Tables(0).Rows(i)("RT_Rate"))
            row.Cells("colIsLtrType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Ltr_Type"))
            row.Cells("ColBoxType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Box_Type"))
            row.Cells("colIsCanType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Can_Type"))
            row.Cells("colPacketType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Packet_Type"))
            row.Cells("colIsDefault").Value = Convert.ToString(ds.Tables(0).Rows(i)("IsDefault"))
            If String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(i)("Item_Category"))) = False Then
                row.Cells(colCategoryType).Value = Convert.ToString(ds.Tables(0).Rows(i)("Item_Category"))
            End If
            If String.IsNullOrEmpty(Convert.ToString(ds.Tables(0).Rows(i)("GST_UNIT_CODE"))) = False Then
                row.Cells(colGSTUnit).Value = Convert.ToString(ds.Tables(0).Rows(i)("GST_UNIT_CODE"))
            End If
            ' MasterTemplate.Rows(i).Cells(UOMColStockUnit).Value()
        Next
        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select unit_code,unit_desc,Conv_Factor from tspl_unit_master"))
        If str = "" Then
            btnsave.Text = "Save"
        Else
            btnsave.Text = "Update"
        End If

        'If userCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        isLoadData = False
    End Sub
#End Region
#Region "Button Click"

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        'Ticket No-TEC/04/11/19-001046,Only one LTR Type
        Try
            Dim counter_rt As Integer = 0
            Dim counter_LTR As Integer = 0
            Dim counter_IsDefault As Integer = 0
            For Each grow As GridViewRowInfo In MasterTemplate.Rows
                If clsCommon.myCBool(grow.Cells("colRTrate").Value) = True Then
                    counter_rt += 1
                End If
                If clsCommon.myCBool(grow.Cells("colIsLtrType").Value) = True Then
                    counter_LTR += 1
                End If
                If clsCommon.myCBool(grow.Cells("colIsDefault").Value) = True Then
                    counter_IsDefault += 1
                End If
            Next
            If counter_rt > 1 Then
                Throw New Exception("Set any one as RT Rate")
            End If
            If counter_LTR > 1 Then
                Throw New Exception("Set only one as Ltr Type")
            End If
            If counter_IsDefault > 1 Then
                Throw New Exception("Set only one as Default")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Function
    Sub SaveData()
        'If btnsave.Text = "Save" Then

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.unitMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        funInsert()
        'ElseIf btnsave.Text = "Update" Then
        'funUpdate()
        'End If
    End Sub
    Private Sub rdbtnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

#End Region
#Region "Methods"
    ''modify by suraj 
    ''date:25/08/2011

    Private Sub funInsert()
        Dim check As String = String.Empty
        Dim count As Integer = 0
        Dim Weight_Type As Char = "N"
        Dim Crate_Type As Char = "N"
        Dim tran As SqlTransaction = Nothing
        Dim createprice As String = ""
        Dim rt_rate As Char = "N"
        Dim Ltr_type As Char = "N"
        Dim Box_type As Char = "N"
        Dim CAN_type As Char = "N"
        Dim Packet_type As Char = "N"
        Dim Category_Type As Char = ""
        Dim GST_Unit_Code As String = ""
        Dim IsDefault As Integer = 0
        Try
            'connectSql.OpenConnection()
            tran = clsDBFuncationality.GetTransactin()

            For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                If MasterTemplate.Rows(i).Cells("empty").Value = True Then
                    check = "Y"
                Else
                    check = "N"
                End If
                If MasterTemplate.Rows(i).Cells("createprice").Value = True Then
                    createprice = "Y"
                Else
                    createprice = "N"
                End If
                If MasterTemplate.Rows(i).Cells("colIsWeightType").Value = True Then
                    Weight_Type = "Y"
                Else
                    Weight_Type = "N"
                End If
                If MasterTemplate.Rows(i).Cells("colIsCrateType").Value = True Then
                    Crate_Type = "Y"
                Else
                    Crate_Type = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colRTrate").Value = True Then
                    rt_rate = "Y"
                Else
                    rt_rate = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colIsLtrType").Value = True Then
                    Ltr_Type = "Y"
                Else
                    Ltr_Type = "N"
                End If

                If MasterTemplate.Rows(i).Cells("ColBoxType").Value = True Then
                    Box_type = "Y"
                Else
                    Box_type = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colIsCanType").Value = True Then
                    CAN_type = "Y"
                Else
                    CAN_type = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colPacketType").Value = True Then
                    Packet_type = "Y"
                Else
                    Packet_type = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colIsDefault").Value = True Then
                    IsDefault = 1
                Else
                    IsDefault = 0
                End If

                If MasterTemplate.Rows(i).Cells(colCategoryType).Value = "KG" AndAlso MasterTemplate.Rows(i).Cells("colIsWeightType").Value = True Then
                    Category_Type = "K"
                ElseIf MasterTemplate.Rows(i).Cells(colCategoryType).Value = "LTR" AndAlso MasterTemplate.Rows(i).Cells("colIsWeightType").Value = True Then
                    Category_Type = "L"
                Else
                    Category_Type = ""
                End If
                Dim strGST_Unit_Code As String = ""
                GST_Unit_Code = clsCommon.myCstr(MasterTemplate.Rows(i).Cells(colGSTUnit).Value)
                If clsCommon.myLen(GST_Unit_Code) > 0 Then
                    strGST_Unit_Code = " , GST_UNIT_CODE = '" + GST_Unit_Code + "' "
                End If
                Dim UOmCOde As String = Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value)
                If String.IsNullOrEmpty(UOmCOde) Then
                    Throw New Exception("UOM Code can not be left blank.")

                    Exit For
                End If

                Dim sql1 As String = "select count(*) from TSPL_UNIT_MASTER where Unit_Code='" + UOmCOde + "'"
                count = CInt(connectSql.RunScalar(tran, sql1))
                If (count = 0) Then
                    connectSql.RunSpTransaction(tran, "sp_tspl_unit_master_insert", New SqlParameter("@unitcode", Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value)), New SqlParameter("@createprice", createprice), New SqlParameter("@empty", check), New SqlParameter("@desc", Convert.ToString(MasterTemplate.Rows(i).Cells(1).Value)), New SqlParameter("@ConvFact", Convert.ToString(MasterTemplate.Rows(i).Cells(2).Value)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(tran)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(tran)), New SqlParameter("@compcode", companyCode))
                    clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "',RT_Rate='" & rt_rate & "',Ltr_Type='" & Ltr_type & "',Box_Type='" & Box_type & "',CAN_Type='" & CAN_type & "' , Item_Category = '" & Category_Type & "' , Packet_Type = '" + Packet_type + "' , IsDefault = '" + clsCommon.myCstr(IsDefault) + "'" + strGST_Unit_Code + "  WHERE Unit_Code='" & Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value) & "'", tran)
                Else
                    connectSql.RunSpTransaction(tran, "sp_tspl_unit_master_update", New SqlParameter("@unitcode", Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value)), New SqlParameter("@Create_Price", createprice), New SqlParameter("@empty", check), New SqlParameter("@desc", Convert.ToString(MasterTemplate.Rows(i).Cells(1).Value)), New SqlParameter("@ConvFact", Convert.ToString(MasterTemplate.Rows(i).Cells(2).Value)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(tran)), New SqlParameter("@compcode", companyCode))
                    clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "',RT_Rate='" & rt_rate & "',Ltr_Type='" & Ltr_type & "',Box_Type='" & Box_type & "',CAN_Type='" & CAN_type & "' , Item_Category = '" & Category_Type & "' , Packet_Type = '" + Packet_type + "' , IsDefault = '" + clsCommon.myCstr(IsDefault) + "'" + strGST_Unit_Code + "   WHERE Unit_Code='" & Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value) & "'", tran)
                End If
            Next
            tran.Commit()
            myMessages.insert()
            btnsave.Text = "&Update"
        Catch ex As Exception
            tran.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub
    ''modify by suraj 
    ''date:25/08/2011
    Private Sub funUpdate()
        Dim check As String = String.Empty
        Dim count As Integer = 0
        Dim createprice As String = ""
        Dim Weight_Type As Char = "N"
        Dim Crate_Type As Char = "N"
        Dim tran As SqlTransaction = Nothing
        Dim rt_rate As Char = "N"
        Dim GST_Unit_Code As String = ""
        Try

            tran = clsDBFuncationality.GetTransactin()
            'connectSql.RunSpTransaction(tran, "sp_tspl_unit_master_delete")
            For i As Integer = 0 To MasterTemplate.Rows.Count - 1
                If MasterTemplate.Rows(i).Cells("empty").Value = True Then
                    check = "Y"
                Else
                    check = "N"
                End If
                If MasterTemplate.Rows(i).Cells("createprice").Value = True Then
                    createprice = "Y"
                Else
                    createprice = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colIsWeightType").Value = True Then
                    Weight_Type = "Y"
                Else
                    Weight_Type = "N"
                End If

                If MasterTemplate.Rows(i).Cells("colIsCrateType").Value = True Then
                    Crate_Type = "Y"
                Else
                    Crate_Type = "N"
                End If
                Dim strGST_Unit_Code As String = ""
                GST_Unit_Code = clsCommon.myCstr(MasterTemplate.Rows(i).Cells(colGSTUnit).Value)
                If clsCommon.myLen(GST_Unit_Code) > 0 Then
                    strGST_Unit_Code = " , GST_UNIT_CODE = '" + GST_Unit_Code + "' "
                End If

                ' Added By abhishek as on 19/10/2012 6:20 pm if CLear the perticular Cell then
                ' Code Start
                Dim UOmCOde As String = Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value)
                If String.IsNullOrEmpty(UOmCOde) Then
                    Throw New Exception("Please Don't Clear the Perticular Cell")

                    Exit For
                End If
                ' Code End
                connectSql.RunSpTransaction(tran, "sp_tspl_unit_master_update", New SqlParameter("@unitcode", Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value)), New SqlParameter("@Create_Price", createprice), New SqlParameter("@empty", check), New SqlParameter("@desc", Convert.ToString(MasterTemplate.Rows(i).Cells(1).Value)), New SqlParameter("@ConvFact", Convert.ToString(MasterTemplate.Rows(i).Cells(2).Value)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(tran)), New SqlParameter("@compcode", companyCode))
                clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "',RT_Rate='" + rt_rate + "' " + strGST_Unit_Code + " WHERE Unit_Code='" & Convert.ToString(MasterTemplate.Rows(i).Cells(0).Value) & "'", tran)

            Next
            tran.Commit()
            myMessages.update()
        Catch ex As Exception
            tran.Rollback()
            myMessages.myExceptions(ex)
            GridData()
        End Try
    End Sub
#End Region
#Region "CellValueChanged Event"
    Private Sub dgUnitofMasterDetails_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles MasterTemplate.CellValueChanged
        If isLoadData = False Then
            If e.ColumnIndex = 0 Then
                For Each row As GridViewRowInfo In MasterTemplate.Rows
                    If row.Index <> MasterTemplate.CurrentRow.Index Then

                        If (row.Cells(0).Value = MasterTemplate.CurrentRow.Cells(0).Value) Then
                            common.clsCommon.MyMessageBoxShow(Me, " Unit of Measure is already exist.")
                            MasterTemplate.CurrentRow.Cells(0).Value = String.Empty
                            ' dgUnitofMasterDetails.CurrentRow.Cells(1).Value = String.Empty
                            'dgUnitofMasterDetails.CurrentRow.Cells(2).Value = ""
                        Else
                        End If
                    End If

                Next
            ElseIf e.Column Is MasterTemplate.Columns(colGSTUnit) Then
                OpenUnitCodeList(False)
            End If
        End If

    End Sub
    Sub OpenUnitCodeList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(0).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select Code , Description as Name from TSPL_EINVOICE_UOM"
            Dim whrCls As String = " "
            MasterTemplate.CurrentRow.Cells(colGSTUnit).Value = clsCommon.ShowSelectForm("GSTUOMfndnder", qry, "Code", whrCls, clsCommon.myCstr(MasterTemplate.CurrentRow.Cells(colGSTUnit).Value), "Code", isButtonClick)
        End If


        'Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colRMCode).Value), "", True, isButtonClick, "", "", "")
        'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
        '    MasterTemplate.CurrentRow.Cells(colGSTUnit).Value = obj.Item_Code
        'Else
        '    MasterTemplate.CurrentRow.Cells(colGSTUnit).Value = ""
        'End If
    End Sub
#End Region
#Region "Import/Export"
    Private Sub rdmenuexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuexport.Click
        sql = "select Unit_Code,Unit_Desc,Conv_Factor, Weight_Type,Crate_Type,Item_Category,Packet_Type, isnull (GST_UNIT_CODE,'') as GST_UNIT_CODE from TSPL_UNIT_MASTER "
        transportSql.ExporttoExcel(sql, Me)
    End Sub

    Private Sub rdmenuimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdmenuimport.Click
        'dgUnitofMasterDetails.AutoGenerateColumns = False
        'If dgUnitofMasterDetails.Rows(0).Cells(0).Value = String.Empty Then
        '    btnsave.Text = "Save"
        'Else
        btnsave.Text = "&Update"
        'End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        If transportSql.importExcel(gv, "Unit_Code", "Unit_Desc", "Conv_Factor", "Weight_Type", "Crate_Type", "Item_Category", "Packet_Type", "GST_UNIT_CODE") Then
            Dim trans As SqlTransaction = Nothing
            Try

                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim strUnitCode As String
                Dim strUnitDesc As String
                Dim strUnitConvFact As Double
                Dim Weight_Type As String = "N"
                Dim Crate_Type As String = "N"

                Dim strPacketType As String = "N"
                Dim strGSTUnitCode As String = ""
                For Each grow As GridViewRowInfo In gv.Rows
                    '   str = grow.Cells(0).Value.ToString()
                    ' str1 = grow.Cells(1).Value.ToString()

                    If clsCommon.myLen(grow.Cells(0).Value) > 0 Then
                        If clsCommon.myLen(clsCommon.myLen(grow.Cells(0).Value)) > 12 Then
                            Throw New Exception("Unit Of Measure cannot be greather than 12 length.")
                        Else
                            strUnitCode = clsCommon.myCstr(grow.Cells(0).Value)
                        End If

                        If clsCommon.myLen(clsCommon.myLen(grow.Cells(1).Value)) > 50 Then
                            Throw New Exception("Description cannot be greather than 50 length.")
                        Else
                            strUnitDesc = clsCommon.myCstr(grow.Cells(1).Value)
                        End If

                        If clsCommon.myLen(grow.Cells(2).Value) > 0 Then
                            strUnitConvFact = clsCommon.myCdbl(grow.Cells(2).Value)
                        ElseIf clsCommon.myLen(grow.Cells(2).Value) > 12 Then
                            Throw New Exception("Conversion Factor cannot be greather than 12 length.")

                            If IsNumeric(grow.Cells(2).Value) Then
                                strUnitConvFact = clsCommon.myCdbl(grow.Cells(2).Value)

                                If clsCommon.myLen(grow.Cells(2).Value) > 12 Then
                                    Throw New Exception("Conversion Factor cannot be greather than 12 length.")
                                Else
                                    strUnitConvFact = clsCommon.myCdbl(grow.Cells(2).Value)
                                End If

                            Else
                                Throw New Exception("Charactor not allow in Conversion Factor.")
                            End If
                        Else
                            strUnitConvFact = 0
                        End If
                        If clsCommon.myLen(grow.Cells("Weight_Type").Value) > 0 Then
                            If clsCommon.CompairString(grow.Cells("Weight_Type").Value, "Y") = CompairStringResult.Equal Then
                                Weight_Type = "Y"
                            ElseIf clsCommon.CompairString(grow.Cells("Weight_Type").Value, "N") = CompairStringResult.Equal Then
                                Weight_Type = "N"
                            Else
                                Throw New Exception("Enter Weight Type As 'Y' Or 'N' Or Left Blank.")
                            End If
                        Else
                            Weight_Type = "N"
                        End If
                        '=====ticket No :  ERO/14/12/18-000444  by Prabhakar   for weight type uom enter by weihgt Uom master screen only
                        Dim WeightUOMValidation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select weight_type from tspl_unit_master where unit_code = '" + strUnitCode + "' ", trans))
                        If clsCommon.CompairString(WeightUOMValidation, "Y") = CompairStringResult.Equal Then
                            If clsCommon.CompairString(Weight_Type, "N") = CompairStringResult.Equal Then
                                Throw New Exception("For UOM Code [" + strUnitCode + "], You can not change Weight type UOM by this scren. You can enter new weight type Uom by [Weight Uom Master] Screen Only.")
                            End If
                        Else
                            If clsCommon.CompairString(Weight_Type, "Y") = CompairStringResult.Equal Then
                                Throw New Exception("For UOM Code [" + strUnitCode + "],You can not enter new Weight type UOM by this scren. You can enter new weight type Uom by [Weight Uom Master] Screen Only.")
                            End If
                        End If
                        '' Anubhooti 10-Sep-2014 BM00000003847
                        If clsCommon.myLen(grow.Cells("Crate_Type").Value) > 0 Then
                            If clsCommon.CompairString(grow.Cells("Crate_Type").Value.ToString().ToUpper().Trim(), "Y") = CompairStringResult.Equal Then
                                Crate_Type = "Y"
                            ElseIf clsCommon.CompairString(grow.Cells("Crate_Type").Value.ToString().ToUpper().Trim(), "N") = CompairStringResult.Equal Then
                                Crate_Type = "N"
                            Else
                                Throw New Exception("Enter Crate Type As 'Y' Or 'N' Or Left Blank.")
                            End If
                        Else
                            Crate_Type = "N"
                        End If

                        '============================================================================
                        If clsCommon.myLen(grow.Cells("Packet_Type").Value) > 0 Then
                            If clsCommon.CompairString(grow.Cells("Packet_Type").Value.ToString().ToUpper().Trim(), "Y") = CompairStringResult.Equal Then
                                strPacketType = "Y"
                            ElseIf clsCommon.CompairString(grow.Cells("Packet_Type").Value.ToString().ToUpper().Trim(), "N") = CompairStringResult.Equal Then
                                strPacketType = "N"
                            Else
                                Throw New Exception("Enter Packet Type As 'Y' Or 'N' Or Left Blank.")
                            End If
                        Else
                            Crate_Type = "N"
                        End If

                        strGSTUnitCode = ""
                        If clsCommon.myLen(clsCommon.myCstr(grow.Cells("GST_UNIT_CODE").Value)) > 0 Then
                            Dim check As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Code  from TSPL_COST_CENTER_UNIT_MASTER where Code = '" + clsCommon.myCstr(grow.Cells("GST_UNIT_CODE").Value) + "' ", trans))
                            If clsCommon.myLen(check) <= 0 Then
                                Throw New Exception("Invalid GST Uom code for '" + strUnitCode + "' .")
                            Else
                                strGSTUnitCode = " , GST_UNIT_CODE = '" + clsCommon.myCstr(grow.Cells("GST_UNIT_CODE").Value) + "' "
                            End If
                        End If
                        ' 'ticket No :  ERO/14/12/18-000444  by Prabhakar for item category can not be change by this screen
                        'Dim strItemCategory As String = ""
                        'If clsCommon.myLen(grow.Cells("Item_Category").Value) > 0 AndAlso clsCommon.CompairString(grow.Cells("Weight_Type").Value, "Y") = CompairStringResult.Equal Then
                        '    If clsCommon.CompairString(grow.Cells("Item_Category").Value.ToString().ToUpper().Trim(), "KG") = CompairStringResult.Equal Then
                        '        strItemCategory = "K"
                        '    ElseIf clsCommon.CompairString(grow.Cells("Item_Category").Value.ToString().ToUpper().Trim(), "K") = CompairStringResult.Equal Then
                        '        strItemCategory = "K"
                        '    ElseIf clsCommon.CompairString(grow.Cells("Item_Category").Value.ToString().ToUpper().Trim(), "LTR") = CompairStringResult.Equal Then
                        '        strItemCategory = "L"
                        '    ElseIf clsCommon.CompairString(grow.Cells("Item_Category").Value.ToString().ToUpper().Trim(), "L") = CompairStringResult.Equal Then
                        '        strItemCategory = "L"
                        '    ElseIf String.IsNullOrEmpty(grow.Cells("Item_Category").Value.ToString()) = True Then
                        '        strItemCategory = ""
                        '    Else
                        '        Throw New Exception("Enter Item Category As 'KG' Or 'K' Or 'LTR' Or 'L' Or Left Blank.")
                        '    End If
                        'End If
                        '=========================================================================
                        ''
                        Dim sql1 As String = "select count(*) from TSPL_UNIT_MASTER where Unit_Code='" + strUnitCode + "'"
                        Dim i As Integer = CInt(connectSql.RunScalar(trans, sql1))
                        If (i = 0) Then
                            connectSql.RunSpTransaction(trans, "sp_tspl_unit_master_insert", New SqlParameter("@unitcode", Convert.ToString(strUnitCode)), New SqlParameter("@createprice", "N"), New SqlParameter("@empty", "N"), New SqlParameter("@desc", Convert.ToString(strUnitDesc)), New SqlParameter("@ConvFact", Convert.ToString(strUnitConvFact)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@createdby", userCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                            clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "'   , Packet_Type = '" + strPacketType + "'  " + strGSTUnitCode + "  WHERE Unit_Code='" & Convert.ToString(grow.Cells(0).Value) & "'", trans)
                        Else
                            connectSql.RunSpTransaction(trans, "sp_tspl_unit_master_update", New SqlParameter("@unitcode", Convert.ToString(strUnitCode)), New SqlParameter("@Create_Price", "N"), New SqlParameter("@empty", "N"), New SqlParameter("@desc", Convert.ToString(strUnitDesc)), New SqlParameter("@ConvFact", Convert.ToString(strUnitConvFact)), New SqlParameter("@Weight_Type", Weight_Type), New SqlParameter("@modifiedby", userCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", companyCode))
                            clsDBFuncationality.ExecuteNonQuery("UPDATE tspl_unit_master SET Crate_Type='" & Crate_Type & "' , Packet_Type = '" + strPacketType + "' " + strGSTUnitCode + "   WHERE Unit_Code='" & Convert.ToString(strUnitCode) & "'", trans) ' , Item_Category = '" & strItemCategory & "'
                        End If
                    End If

                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try


            ds = RunSQLReturnDS("select unit_code [Unit of Measure],unit_desc [Description],Floor(Conv_Factor) as  [Default Conversion Factor], (case empty when 'Y' THEN 'True' else 'False' end) as empty, (case create_Price when 'Y' THEN 'True' else 'False' end) as price, Cast(Case When Weight_Type='Y' Then 1 Else 0 End as Bit) as Weight_Type, Cast(Case When Crate_Type='Y' Then 1 Else 0 End as Bit) as Crate_Type,Cast(Case When Packet_Type='Y' Then 1 Else 0 End as Bit) as Packet_Type,Case When Item_Category='K' Then 'KG' when Item_Category='L' then 'LTR'   Else '' End  as Item_Category from tspl_unit_master")

            MasterTemplate.DataSource = Nothing
            MasterTemplate.Rows.Clear()
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                '  check = Convert.ToString(ds.Tables(0).Rows(i)("empty"))
                Dim row As GridViewRowInfo = MasterTemplate.Rows.AddNew()
                row.Cells("uom").Value = Convert.ToString(ds.Tables(0).Rows(i)("Unit of Measure"))
                row.Cells("desc").Value = Convert.ToString(ds.Tables(0).Rows(i)("Description"))
                row.Cells("conversion").Value = Convert.ToString(ds.Tables(0).Rows(i)("Default Conversion Factor"))
                row.Cells("empty").Value = Convert.ToString(ds.Tables(0).Rows(i)("empty"))
                row.Cells("createprice").Value = Convert.ToString(ds.Tables(0).Rows(i)("price"))
                row.Cells("colIsWeightType").Value = ds.Tables(0).Rows(i)("Weight_Type")
                row.Cells("colIsCrateType").Value = ds.Tables(0).Rows(i)("Crate_Type")
                row.Cells("colPacketType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Packet_Type"))
                row.Cells(colCategoryType).Value = Convert.ToString(ds.Tables(0).Rows(i)("Item_Category"))
            Next

        End If
        Me.Controls.Remove(gv)
    End Sub
#End Region
#Region "CellBeginEdit Event"
    Private Sub dgUnitofMasterDetails_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles MasterTemplate.CellBeginEdit
        If TypeOf Me.MasterTemplate.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.MasterTemplate.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)
            If e.ColumnIndex = 2 Then
                AddHandler editorElement.KeyPress, AddressOf keyPress1

            End If
        End If
    End Sub
#End Region
#Region "UserAddedRow Event"
    Private Sub dgUnitofMasterDetails_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles MasterTemplate.UserAddedRow
        ' If Not IsDBNull(dgUnitofMasterDetails.CurrentRow.Cells(2).Value) Then
        If IsDBNull(MasterTemplate.Rows(e.Row.Index).Cells(0).Value) And Not IsDBNull(MasterTemplate.Rows(e.Row.Index).Cells(2).Value) Then
            Dim drin As GridViewDataRowInfo = TryCast(Me.MasterTemplate.CurrentRow, GridViewDataRowInfo)
            Me.MasterTemplate.Rows.RemoveAt(e.Row.Index)
            ' Me.dgUnitofMasterDetails.CurrentRow = Nothing
            'Me.dgUnitofMasterDetails.SelectedRows

        Else

        End If
    End Sub
#End Region

    Private Sub MasterTemplate_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles MasterTemplate.UserDeletingRow
        Dim UomCode1 As String
        Dim UomCode As String = MasterTemplate.CurrentRow.Cells(0).Value
        UomCode1 = clsDBFuncationality.getSingleValue("select UOM_Code  from TSPL_ITEM_UOM_DETAIL where UOM_Code ='" & UomCode & "'")

        If clsCommon.myLen(UomCode1) > 0 Then
            common.clsCommon.MyMessageBoxShow(Me, " This record can't be deleted.It is used in another process.")
            GridData()
            Exit Sub
        Else
            If myMessages.deleteConfirm() Then
                clsDBFuncationality.ExecuteNonQuery("delete from tspl_unit_master where unit_code='" + clsCommon.myCstr(UomCode) + "'")
                myMessages.delete()
                GridData()
            End If
        End If

    End Sub
    Public Sub GridData()
        '' Anubhooti 10-Sep-2014 BM00000003847
        ds = RunSQLReturnDS("select unit_code [Unit of Measure],unit_desc [Description],Floor(Conv_Factor) as  [Default Conversion Factor], (case empty when 'Y' THEN 'True' else 'False' end) as empty, (case create_Price when 'Y' THEN 'True' else 'False' end) as price, Cast(Case When Weight_Type='Y' Then 1 Else 0 End as Bit) as Weight_Type, Cast(Case When Crate_Type='Y' Then 1 Else 0 End as Bit) as Crate_Type,Cast(Case When RT_Rate='Y' Then 1 Else 0 End as Bit) as RT_Rate,tspl_unit_master.Ltr_Type, Cast(Case When Packet_Type='Y' Then 1 Else 0 End as Bit) as Packet_Type,CAST(IsDefault AS Bit) as IsDefault,Case When Item_Category='K' Then 'KG' when Item_Category='L' then 'LTR'   Else '' End  as Item_Category, isnull (GST_UNIT_CODE,'') as GST_UNIT_CODE  from tspl_unit_master")

        MasterTemplate.DataSource = Nothing
        MasterTemplate.Rows.Clear()
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            '  check = Convert.ToString(ds.Tables(0).Rows(i)("empty"))
            Dim row As GridViewRowInfo = MasterTemplate.Rows.AddNew()
            row.Cells("uom").Value = Convert.ToString(ds.Tables(0).Rows(i)("Unit of Measure"))
            row.Cells("desc").Value = Convert.ToString(ds.Tables(0).Rows(i)("Description"))
            row.Cells("conversion").Value = Convert.ToString(ds.Tables(0).Rows(i)("Default Conversion Factor"))
            row.Cells("empty").Value = Convert.ToString(ds.Tables(0).Rows(i)("empty"))
            row.Cells("createprice").Value = Convert.ToString(ds.Tables(0).Rows(i)("price"))
            row.Cells("colIsWeightType").Value = ds.Tables(0).Rows(i)("Weight_Type")
            '' Anubhooti 10-Sep-2014 BM00000003847
            row.Cells("colIsCrateType").Value = ds.Tables(0).Rows(i)("Crate_Type")
            row.Cells("colRTrate").Value = Convert.ToString(ds.Tables(0).Rows(i)("RT_Rate"))
            row.Cells("colIsLtrType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Ltr_Type"))
            row.Cells("ColBoxType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Box_Type"))
            row.Cells("colIsCanType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Can_Type"))
            row.Cells("colPacketType").Value = Convert.ToString(ds.Tables(0).Rows(i)("Packet_Type"))
            row.Cells("colIsDefault").Value = Convert.ToString(ds.Tables(0).Rows(i)("IsDefault"))
            row.Cells(colCategoryType).Value = Convert.ToString(ds.Tables(0).Rows(i)("Item_Category"))
            'GST_UNIT_CODE
            row.Cells(colGSTUnit).Value = Convert.ToString(ds.Tables(0).Rows(i)("GST_UNIT_CODE"))
        Next
    End Sub
    Function GetCategoryType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "None"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "KG"
        dr("Name") = "KG"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "LTR"
        dr("Name") = "LTR"
        dt.Rows.Add(dr)
        Return dt
    End Function

    Function GetGSTCode() As DataTable
        Dim qry As String = " select Code , Description as Name from TSPL_COST_CENTER_UNIT_MASTER "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'Dim dt As New DataTable()
        'dt.Columns.Add("Code", GetType(String))
        'dt.Columns.Add("Name", GetType(String))
        'Dim dr As DataRow = Nothing

        'dr = dt.NewRow()
        'dr("Code") = ""
        'dr("Name") = "None"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "KG"
        'dr("Name") = "KG"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "LTR"
        'dr("Name") = "LTR"
        'dt.Rows.Add(dr)
        Return dt
    End Function
    Private Sub MasterTemplate_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles MasterTemplate.CellFormatting
        If isLoadData = False Then
            'If e.Column Is MasterTemplate.Columns("colIsWeightType") Then
            '    If clsCommon.CompairString(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("colIsWeightType").Value), "Y") = CompairStringResult.Equal Then
            '        MasterTemplate.CurrentRow.Cells(colCategoryType).ReadOnly = False
            '    Else
            '        MasterTemplate.CurrentRow.Cells(colCategoryType).ReadOnly = True
            '    End If
            'End If
            'ticket No :  ERO/14/12/18-000444  by Prabhakar
            If e.Column Is MasterTemplate.Columns(colCategoryType) Then
                'If clsCommon.CompairString(clsCommon.myCstr(MasterTemplate.CurrentRow.Cells("colIsWeightType").Value), True) = CompairStringResult.Equal Then
                '    MasterTemplate.CurrentRow.Cells(colCategoryType).ReadOnly = False
                'Else
                '    MasterTemplate.CurrentRow.Cells(colCategoryType).ReadOnly = True
                'End If
                MasterTemplate.CurrentRow.Cells(colCategoryType).ReadOnly = True
            ElseIf e.Column Is MasterTemplate.Columns("colIsWeightType") Then
                MasterTemplate.CurrentRow.Cells("colIsWeightType").ReadOnly = True
            End If
        End If
       
    End Sub
End Class
