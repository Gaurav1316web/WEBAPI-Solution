'' Created By richa Ticket no BM00000003570 on 21/08/2014
Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPCommanServices

Public Class FrmCatalogMaster
    Inherits FrmMainTranScreen

    Const colLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colitemDesc As String = "ItemDesc"
    Const colspecification As String = "Specification"
    Const colfeature As String = "Feature"
    Const colImage As String = "Image"
    Const colImage123 As String = "Image 123"
    Dim Qry As String = ""
    Dim userCode, companyCode As String
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim isCellValueChangedOpen As Boolean
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Reset()
    End Sub

    Private Sub CloseForm()
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub FrmCatalogMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub FrmCatalogMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N New Trasnaction")
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmCatalogMaster)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        txtCode.Value = ""
        txtBom.Value = ""
        txtDescription.Text = ""
        lblmainItem.Text = ""
        lblmainItemDesc.Text = ""
        lblBomDesc.Text = ""
        TxtSpecification.Text = ""
        Txtfeature.Text = ""
        PictureBox1.Image = Nothing
        txtcatlogdate.Text = clsCommon.GETSERVERDATE()
        txtCode.MyReadOnly = False
        btnsave.Text = "Save"
        btndelete.Enabled = False
        btnsave.Enabled = True
        LoadGridColumns()

        isNewEntry = True
    End Sub
    Sub LoadGridColumns()
        gvBOM.Rows.Clear()
        gvBOM.Columns.Clear()
        gvBOM.DataSource = Nothing

        Dim LineNo As New GridViewTextBoxColumn
        Dim ItemCode As New GridViewTextBoxColumn
        Dim itemDesc As New GridViewTextBoxColumn
        Dim Specification As New GridViewTextBoxColumn
        Dim feature As New GridViewTextBoxColumn
        Dim image As New GridViewCommandColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 60
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(LineNo)

        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 80
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvBOM.Columns.Add(ItemCode)

        itemDesc.FormatString = ""
        itemDesc.HeaderText = "Item Description"
        itemDesc.Name = colitemDesc
        itemDesc.Width = 120
        itemDesc.ReadOnly = True
        itemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(itemDesc)

        Specification.FormatString = ""
        Specification.HeaderText = "Specification"
        Specification.Name = colspecification
        Specification.Width = 350
        Specification.MaxLength = 500
        Specification.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(Specification)

        feature.FormatString = ""
        feature.HeaderText = "Feature"
        feature.Name = colfeature
        feature.Width = 350
        feature.MaxLength = 500
        feature.ReadOnly = False
        feature.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvBOM.Columns.Add(feature)

        image.FormatString = ""
        image.HeaderText = "Image"
        image.Name = colImage
        image.Width = 100
        image.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvBOM.Columns.Add(image)



        gvBOM.Rows.AddNew()
        gvBOM.AllowAddNewRow = False
        gvBOM.AllowDeleteRow = False
        gvBOM.AllowRowReorder = False
        gvBOM.ShowGroupPanel = False
        gvBOM.EnableFiltering = False
        gvBOM.EnableSorting = False
        gvBOM.EnableGrouping = False
        gvBOM.AllowColumnReorder = True
        gvBOM.ShowGroupPanel = False
        gvBOM.MasterTemplate.ShowRowHeaderColumn = False
        gvBOM.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()
    End Sub

    Private Sub txtBom__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBom._MYValidating

        Dim dt As DataTable
        Qry = "select BOM_CODE as Code,DESCRIPTION as Name,BOM_DATE,REVISION_NO,PROD_ITEM_CODE AS ITEM_CODE,PROD_QUANTITY AS BUILD_QTY,START_DATE,END_DATE,STATUS,IS_DEFAULT  from TSPL_MF_BOM_HEAD "
        txtBom.Value = clsCommon.ShowSelectForm("Selector", Qry, "Code", "", txtBom.Value, "", isButtonClicked)
        If clsCommon.myLen(txtBom.Value) > 0 Then
            dt = clsDBFuncationality.GetDataTable("SELECT TSPL_MF_BOM_HEAD.DESCRIPTION,TSPL_MF_BOM_HEAD.PROD_ITEM_CODE as Item_Code,tspl_item_master.Item_Desc  FROM TSPL_MF_BOM_HEAD Left Outer Join tspl_item_master on tspl_item_master.Item_Code=TSPL_MF_BOM_HEAD.PROD_ITEM_CODE  where TSPL_MF_BOM_HEAD.BOM_CODE='" + txtBom.Value + "' ")
            If dt.Rows.Count > 0 Then
                lblBomDesc.Text = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                lblmainItem.Text = clsCommon.myCstr(dt.Rows(0)("Item_Code"))
                lblmainItemDesc.Text = clsCommon.myCstr(dt.Rows(0)("Item_Desc"))
                Try
                    Dim Filename As Byte() = clsDBFuncationality.getSingleValue("select Item_image from tspl_item_master where item_code='" + lblmainItem.Text + "'")
                    Using ms As New IO.MemoryStream(CType(Filename, Byte()))
                        Dim img As Image = Image.FromStream(ms)
                        PictureBox1.Image = img
                    End Using
                Catch ex As Exception

                End Try

                GetTableValue()
            End If
        Else
            txtBom.Value = ""
            lblmainItem.Text = ""
            lblmainItemDesc.Text = ""
            lblBomDesc.Text = ""
            PictureBox1.Image = Nothing
            LoadGridColumns()
        End If

    End Sub
    Private Sub GetTableValue()
        Dim dt As DataTable
        LoadGridColumns()
        Qry = "Select LINE_NO ,CONSM_ITEM_CODE ,ITEM_DESCRIPTION  from TSPL_MF_BOM_DETAIL where BOM_CODE='" + txtBom.Value + "'"
        dt = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            For i As Integer = 0 To dt.Rows.Count - 1
                gvBOM.Rows(i).Cells(colLineNo).Value = clsCommon.myCstr(dt.Rows(i)("LINE_NO"))
                gvBOM.Rows(i).Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(i)("CONSM_ITEM_CODE"))
                gvBOM.Rows(i).Cells(colitemDesc).Value = clsCommon.myCstr(dt.Rows(i)("ITEM_DESCRIPTION"))
                gvBOM.Rows(i).Cells(colImage).Value = "Show"
                gvBOM.Rows.AddNew()
            Next
            gvBOM.Rows.RemoveAt(gvBOM.Rows.Count - 1)
        End If
    End Sub

    Private Sub gvBOM_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvBOM.CellClick
        'If e.Column Is gvBOM.Columns(colImage) Then
        Try
            If e.Column Is gvBOM.Columns(colImage) Then
                If clsCommon.myLen(gvBOM.CurrentRow.Cells(colItemCode).Value.ToString) > 0 Then
                    Dim objImage As New frmPicture
                    If objImage.GetImage("tspl_Item_master", "Item_Image", "Item_Code", gvBOM.CurrentRow.Cells(colItemCode).Value.ToString) Then
                        objImage.ShowDialog()
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
        'End If
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        CloseForm()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnNew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtBom.Value) <= 0 Then
            txtBom.Focus()
            Throw New Exception("BOM cannot be left blank")
        End If

        Return True
    End Function
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsCatalogMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data deleted successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsCatalogMaster = ClsCatalogMaster.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtCode.Value = obj.Catalog_Code
            txtcatlogdate.Text = obj.Catalog_Date
            txtDescription.Text = obj.Catalog_Desc
            txtBom.Value = obj.Bom_Code
            lblBomDesc.Text = obj.Bom_Desc
            lblmainItem.Text = obj.Item_Code
            lblmainItemDesc.Text = obj.Item_Desc
            TxtSpecification.Text = obj.Specification
            Txtfeature.Text = obj.Feature
            Try
                Dim Filename As Byte() = clsDBFuncationality.getSingleValue("select Item_image from TSPL_CATALOG_MASTER where Catalog_Code='" + obj.Catalog_Code + "'")

                Using ms As New IO.MemoryStream(CType(Filename, Byte()))
                    Dim img As Image = Image.FromStream(ms)
                    PictureBox1.Image = img
                End Using
            Catch ex As Exception

            End Try
            LoadGridColumns()
            If obj.arrCatalogDetail IsNot Nothing AndAlso obj.arrCatalogDetail.Count > 0 Then

                For Each objTr As ClsCatalogDetail In obj.arrCatalogDetail
                    'gvBOM.Rows.AddNew()
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colitemDesc).Value = objTr.Item_Desc
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colspecification).Value = objTr.Specification
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colfeature).Value = objTr.Feature
                    gvBOM.Rows(gvBOM.Rows.Count - 1).Cells(colImage).Value = "Show"


                    gvBOM.Rows.AddNew()
                Next
                gvBOM.Rows.RemoveAt(gvBOM.Rows.Count - 1)
            Else
                'gvBOM.DataSource = Nothing
            End If


            txtCode.MyReadOnly = True
            btnsave.Text = "Update"
            btndelete.Enabled = True

        Else
            Reset()

        End If
    End Sub

    Sub SaveData()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New ClsCatalogMaster()
                obj.Catalog_Code = txtCode.Value
                obj.Catalog_Date = txtcatlogdate.Text
                obj.Catalog_Desc = txtDescription.Text
                obj.Bom_Code = txtBom.Value
                obj.Bom_Desc = lblBomDesc.Text
                obj.Item_Code = lblmainItem.Text
                obj.Item_Desc = lblmainItemDesc.Text
                obj.Specification = TxtSpecification.Text
                obj.Feature = Txtfeature.Text


                Dim objTr As New ClsCatalogDetail
                obj.arrCatalogDetail = New List(Of ClsCatalogDetail)
                For Each grow As GridViewRowInfo In gvBOM.Rows
                    objTr = New ClsCatalogDetail()
                    objTr.Catalog_Code = clsCommon.myCstr(obj.Catalog_Code)
                    objTr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                    objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colitemDesc).Value)
                    objTr.Specification = clsCommon.myCstr(grow.Cells(colspecification).Value)
                    objTr.Feature = clsCommon.myCstr(grow.Cells(colfeature).Value)
                    obj.arrCatalogDetail.Add(objTr)
                Next


                If (ClsCatalogMaster.SaveData(obj, isNewEntry)) Then
                    '======================== Code to save Image==================================
                    Try


                        Dim Filename As Byte() = clsDBFuncationality.getSingleValue("select Item_image from tspl_item_master where item_code='" + lblmainItem.Text + "'")

                        If clsCommon.myLen(Filename) > 0 Then

                            Dim Str As String = " UPDATE TSPL_CATALOG_MASTER set Item_Image = @BLOBData where Catalog_Code='" + obj.Catalog_Code + "'"
                            Dim cmd As SqlCommand = New SqlCommand(Str, clsDBFuncationality.GetConnnection)
                            Dim prm As New SqlParameter("@BLOBData", Filename)
                            cmd.Parameters.Add(prm)
                            cmd.ExecuteNonQuery()
                        End If
                    Catch ex As Exception

                    End Try
                    '=============================================

                    clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                    LoadData(obj.Catalog_Code, NavigatorType.Current)

                End If


            End If
        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RDDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RDDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvBOM", objCommonVar.CurrentUserCode)
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub RDSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RDSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvBOM.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gvBOM"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvBOM.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvBOM.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gvBOM.MasterTemplate.FilterDescriptors.Clear()

                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvBOM", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvBOM.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvBOM.Columns.Count - 1 Step ii + 1
                        gvBOM.Columns(ii).IsVisible = False
                        gvBOM.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvBOM.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CATALOG_MASTER where Catalog_Code='" + txtCode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtCode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtCode.MyReadOnly = False
            End If

            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtCode._MYValidating
        txtCode.Value = ClsCatalogMaster.getFinder("", "", isButtonClicked)
        LoadData(txtCode.Value, NavigatorType.Current)
    End Sub


    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If txtCode.Value = "" Then
            myMessages.blankValue("Catalog Code")
        Else
            funPrint()
        End If
    End Sub
    Private Sub funPrint()
        Try
            Dim qry As String = " Select  '" & objCommonVar.CurrentCompanyName & "' as Company_Name , TSPL_CATALOG_MASTER.Catalog_Code as CatalogNo,Convert(varchar,TSPL_CATALOG_MASTER.Catalog_Date,103) as catalogDate ,TSPL_CATALOG_MASTER.Catalog_Desc as CatalogDesc,TSPL_CATALOG_MASTER.Bom_Code as BOMCode,TSPL_CATALOG_MASTER.Specification as MainSpecification,TSPL_CATALOG_MASTER.Feature  as MainFeature,TSPL_CATALOG_MASTER.Item_Code as MainItemCode,TSPL_ITEM_MASTER.Item_Desc as mainItemdesc,"
            qry += " TSPL_CATALOG_MASTER.Created_By as CreatedBy,TSPL_CATALOG_MASTER.Modified_By as ModifiedBy,TSPL_CATALOG_DETAIL.Line_No as SL_No, "
            qry += " TSPL_CATALOG_DETAIL.Item_Code as ItemCode,TSPL_CATALOG_DETAIL.Item_Desc as ItemDesc,TSPL_CATALOG_DETAIL.Specification as Specification,TSPL_CATALOG_DETAIL.Feature as Feature, TSPL_ITEM_MASTER.Item_Image as IMG, dtlItem.Item_Image as IMGdtl from TSPL_CATALOG_MASTER inner join TSPL_CATALOG_DETAIL on  TSPL_CATALOG_MASTER.Catalog_Code=TSPL_CATALOG_DETAIL.Catalog_Code"
            qry += " Inner Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_CATALOG_MASTER.Item_Code LEFT OUTER JOIN TSPL_ITEM_MASTER dtlItem on dtlItem.Item_Code=TSPL_CATALOG_DETAIL.Item_Code "
            qry += " where 2=2"

            If txtCode.Value <> "" Then
                qry += " and  TSPL_CATALOG_MASTER.Catalog_Code='" & txtCode.Value & "' "
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "rptCatalogReport", "Catalog Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
