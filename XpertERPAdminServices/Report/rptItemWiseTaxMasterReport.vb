'Created By-Sanjay, Ticket No-  BHA/17/10/18-000628,  Client - Bharat 
Imports common
Imports System.IO
Public Class rptItemWiseTaxMasterReport
    Inherits FrmMainTranScreen
    Dim inputs As String() = {}
    Dim ButtonToolTip As New ToolTip()
    Dim strQry As String = ""
    Dim dt As DataTable
    Dim qry As String = ""
    Dim whrcls As String = ""
    Const colLineNo As String = "colLineNo"
    Const colDoc_Code As String = "colDoc_Code"
    Const colDoc_Date As String = "colDoc_Date"
    Const colType As String = "colType"
    Const colPosted As String = "colPosted"
    Const colItem_Code As String = "colItem_Code"
    Const colItem_Desc As String = "colItem_Desc"
    Const colHSN_Code As String = "colHSN_Code"
    Const colTAX_GROUP_CODE As String = "colTAX_GROUP_CODE"
    Const colTAX_GROUP_Desc As String = "colTAX_GROUP_Desc"
    Const colTAX1_Code As String = "colTAX1_Code"
    Const colTAX1_Rate As String = "colTAX1_Rate"
    Const colTAX2_Code As String = "colTAX2_Code"
    Const colTAX2_Rate As String = "colTAX2_Rate"
    Const colTAX3_Code As String = "colTAX3_Code"
    Const colTAX3_Rate As String = "colTAX3_Rate"
    Const colTAX4_Code As String = "colTAX4_Code"
    Const colTAX4_Rate As String = "colTAX4_Rate"
    Const colTAX5_Code As String = "colTAX5_Code"
    Const colTAX5_Rate As String = "colTAX5_Rate"
    Const colModify_By As String = "colModify_By"
    Const colModify_Date As String = "colModify_Date"

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isQuickExportFlag
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        FormatGrid()
        Print()
    End Sub


    Private Sub Print()

        Try
            Dim strWhrClause As String = String.Empty
            Dim strWhrClauseTransType As String = String.Empty
            strWhrClause = " 1=1 "
            strWhrClauseTransType = " 1=1 "
            If txtDoc.arrValueMember IsNot Nothing AndAlso txtDoc.arrValueMember.Count > 0 Then
                strWhrClause += " and TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE in (" + clsCommon.GetMulcallString(txtDoc.arrValueMember) + ")  "
            End If
            If clsCommon.CompairString(ddlTransType.SelectedValue, "P") = CompairStringResult.Equal Then
                strWhrClauseTransType += " and TSPL_ITEM_WISE_TAX.Type='P' "
            ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "S") = CompairStringResult.Equal Then
                strWhrClauseTransType += " and TSPL_ITEM_WISE_TAX.Type='S' "
            ElseIf clsCommon.CompairString(ddlTransType.SelectedValue, "T") = CompairStringResult.Equal Then
                strWhrClauseTransType += " and TSPL_ITEM_WISE_TAX.Type='T' "
            End If

            Dim query As String = "select   xxxx.HCODE as Doc_Code,Convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103) as DOC_DATE, (case when TSPL_ITEM_WISE_TAX.Type='S' THEN 'Sales' " & _
                " when TSPL_ITEM_WISE_TAX.Type='P' THEN 'Purchase' when TSPL_ITEM_WISE_TAX.Type='T' THEN 'Transfer' end) as Type ,case when TSPL_ITEM_WISE_TAX.Status =1 then 'Y' else 'N' end  as Posted, xxxx.Item_Code,TSPL_ITEM_MASTER.Item_Desc as Item_Desc,TSPL_ITEM_MASTER.HSN_Code as HSN_Code ,xxxx.Tax_Group_Code as Tax_Group_Code,xxxx.Tax_Group_Desc as Tax_Group_Desc,xxxx.Tax1_Code as Tax1_Code, xxxx.TAX1_Rate as TAX1_Rate ,xxxx.Tax2_Code as Tax2_Code,xxxx.TAX2_Rate as TAX2_Rate ,xxxx.Tax3_Code as Tax3_Code,xxxx.TAX3_Rate as TAX3_Rate ,xxxx.Tax4_Code as Tax4_Code,xxxx.TAX4_Rate as TAX4_Rate ,xxxx.Tax5_Code as Tax5_Code,xxxx.TAX5_Rate as TAX5_Rate  " & _
                " ,TSPL_ITEM_WISE_TAX.Modify_By as Modify_By,convert(varchar,TSPL_ITEM_WISE_TAX.Modify_Date,103) as Modify_Date from( " & _
                " select xxxx.HCODE as HCODE, xxxx.TAX_Group_SNo, xxxx.Item_Code,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (xxxx.Tax_Group_Desc) as Tax_Group_Desc " & _
                " ,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from (  " & _
                " select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,TSPL_TAX_GROUP_MASTER.Tax_Group_Desc " & _
                " ,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO,convert(varchar,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO)+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  " & _
                " from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE " & _
                " left join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code=TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code " & _
                " where "
            query = query + strWhrClause
            query += " )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv  " & _
                " ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx where 1=1 " & _
                " group by xxxx.HCODE,xxxx.Item_Code, xxxx.TAX_Group_SNo ) xxxx  " & _
                " left outer join TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX.HCODE = xxxx.HCODE " & _
                " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = xxxx.Item_Code where "
            query = query + strWhrClauseTransType + " order by xxxx.HCODE,xxxx.Item_Code"
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()

            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub

            Else

                For Each row As DataRow In dtgv.Rows
                    Gv1.Rows.AddNew()

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCdbl(Gv1.Rows.Count)

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDoc_Code).Value = clsCommon.myCstr(row("Doc_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDoc_Date).Value = clsCommon.myCstr(row("Doc_Date").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colType).Value = clsCommon.myCstr(row("Type").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPosted).Value = clsCommon.myCstr(row("Posted").ToString())

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(row("Item_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(row("Item_Code").ToString()), Nothing)
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(row("Item_Code").ToString()), Nothing)

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX_GROUP_CODE).Value = clsCommon.myCstr(row("Tax_Group_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX_GROUP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code = '" + clsCommon.myCstr(row("Tax_Group_Code").ToString()) + "'"))

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX1_Code).Value = clsCommon.myCstr(row("Tax1_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX1_Rate).Value = clsCommon.myCstr(row("TAX1_Rate").ToString())

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX2_Code).Value = clsCommon.myCstr(row("Tax2_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX2_Rate).Value = clsCommon.myCstr(row("TAX2_Rate").ToString())

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX3_Code).Value = clsCommon.myCstr(row("Tax3_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX3_Rate).Value = clsCommon.myCstr(row("TAX3_Rate").ToString())

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX4_Code).Value = clsCommon.myCstr(row("Tax4_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX4_Rate).Value = clsCommon.myCstr(row("TAX4_Rate").ToString())

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX5_Code).Value = clsCommon.myCstr(row("Tax5_Code").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colTAX5_Rate).Value = clsCommon.myCstr(row("TAX5_Rate").ToString())

                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModify_By).Value = clsCommon.myCstr(row("Modify_By").ToString())
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colModify_Date).Value = clsCommon.myCstr(row("Modify_Date").ToString())
                Next

                    End If

            Gv1.BestFitColumns()
            ReStoreGridLayout()



            RadPageView1.SelectedPage = RadPageViewPage2


            For i As Integer = 0 To Gv1.Columns.Count - 1
                Gv1.Columns(i).BestFit()
            Next

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub fillTransactionType()
        Dim dataTableTransType As New DataTable()
        dataTableTransType.Columns.Add("TransDesc")
        dataTableTransType.Columns.Add("TransValue")
        dataTableTransType.Rows.Add("All", "All")
        dataTableTransType.Rows.Add("Purchase", "P")
        dataTableTransType.Rows.Add("Sales", "S")
        dataTableTransType.Rows.Add("Transfer", "T")
        ddlTransType.DisplayMember = "TransDesc"
        ddlTransType.ValueMember = "TransValue"
        ddlTransType.DataSource = dataTableTransType
        ddlTransType.SelectedValue = "All"
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rptVendorAccountSetReport_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Alt AndAlso e.KeyCode = Keys.R Then
                FunReset()

            ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
                Me.Close()
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rptVendorAccountSetReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window")
    End Sub

    Private Sub FormatGrid()
        Try

          gv1.Rows.Clear()
            Gv1.Columns.Clear()

            Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoLineNo = New GridViewDecimalColumn()
            repoLineNo.FormatString = ""
            repoLineNo.HeaderText = "Line No"
            repoLineNo.Name = colLineNo
            repoLineNo.Width = 50
            repoLineNo.ReadOnly = True
            repoLineNo.IsVisible = True
            repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            Gv1.MasterTemplate.Columns.Add(repoLineNo)

            Dim repoDoc_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDoc_Code.FormatString = ""
            repoDoc_Code.HeaderText = "Doc Code"
            repoDoc_Code.Name = colDoc_Code
            repoDoc_Code.ReadOnly = True
            repoDoc_Code.IsVisible = True
            repoDoc_Code.Width = 90
            Gv1.MasterTemplate.Columns.Add(repoDoc_Code)

            Dim repoDoc_Date As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoDoc_Date.FormatString = ""
            repoDoc_Date.HeaderText = "Doc Date"
            repoDoc_Date.Name = colDoc_Date
            repoDoc_Date.ReadOnly = True
            repoDoc_Date.IsVisible = True
            repoDoc_Date.Width = 90
            Gv1.MasterTemplate.Columns.Add(repoDoc_Date)

            Dim repoType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoType.FormatString = ""
            repoType.HeaderText = "Type"
            repoType.Name = colType
            repoType.ReadOnly = True
            repoType.IsVisible = True
            repoType.Width = 60
            Gv1.MasterTemplate.Columns.Add(repoType)

            Dim repoPosted As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoPosted.FormatString = ""
            repoPosted.HeaderText = "Posted"
            repoPosted.Name = colPosted
            repoPosted.ReadOnly = True
            repoPosted.IsVisible = True
            repoPosted.Width = 40
            Gv1.MasterTemplate.Columns.Add(repoPosted)

          
            Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoItemCode.FormatString = ""
            repoItemCode.HeaderText = "Item Code"
            repoItemCode.Name = colItem_Code
            repoItemCode.ReadOnly = True
            repoItemCode.IsVisible = True
            repoItemCode.Width = 90
            Gv1.MasterTemplate.Columns.Add(repoItemCode)

            Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoItemDesc.FormatString = ""
            repoItemDesc.HeaderText = "Item Description"
            repoItemDesc.Name = colItem_Desc
            repoItemDesc.Width = 120
            repoItemDesc.ReadOnly = True
            repoItemDesc.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoItemDesc)

            Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoHSNCode.FormatString = ""
            repoHSNCode.HeaderText = "HSN Code"
            repoHSNCode.Name = colHSN_Code
            repoHSNCode.Width = 100
            repoHSNCode.ReadOnly = True
            repoHSNCode.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoHSNCode)


            Dim repoTaxGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTaxGroup.FormatString = ""
            repoTaxGroup.HeaderText = "Tax Group Code"
            repoTaxGroup.Name = colTAX_GROUP_CODE
            repoTaxGroup.ReadOnly = True
            repoTaxGroup.IsVisible = True
            repoTaxGroup.Width = 80
            Gv1.MasterTemplate.Columns.Add(repoTaxGroup)


            Dim repoTaxGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTaxGroupDesc.FormatString = ""
            repoTaxGroupDesc.HeaderText = "Tax Group Description"
            repoTaxGroupDesc.Name = colTAX_GROUP_Desc
            repoTaxGroupDesc.Width = 120
            repoTaxGroupDesc.ReadOnly = True
            repoTaxGroupDesc.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTaxGroupDesc)


            Dim repoTax1_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax1_Code.FormatString = ""
            repoTax1_Code.HeaderText = "Tax1 Code"
            repoTax1_Code.Name = colTAX1_Code
            repoTax1_Code.Width = 80
            repoTax1_Code.ReadOnly = True
            repoTax1_Code.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax1_Code)

            Dim repoTax1_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax1_Rate.FormatString = ""
            repoTax1_Rate.HeaderText = "Tax1 Rate"
            repoTax1_Rate.Name = colTAX1_Rate
            repoTax1_Rate.Width = 80
            repoTax1_Rate.ReadOnly = True
            repoTax1_Rate.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax1_Rate)


            Dim repoTax2_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax2_Code.FormatString = ""
            repoTax2_Code.HeaderText = "Tax2 Code"
            repoTax2_Code.Name = colTAX2_Code
            repoTax2_Code.Width = 80
            repoTax2_Code.ReadOnly = True
            repoTax2_Code.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax2_Code)

            Dim repoTax2_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax2_Rate.FormatString = ""
            repoTax2_Rate.HeaderText = "Tax2 Rate"
            repoTax2_Rate.Name = colTAX2_Rate
            repoTax2_Rate.Width = 80
            repoTax2_Rate.ReadOnly = True
            repoTax2_Rate.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax2_Rate)


            Dim repoTax3_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax3_Code.FormatString = ""
            repoTax3_Code.HeaderText = "Tax3 Code"
            repoTax3_Code.Name = colTAX3_Code
            repoTax3_Code.Width = 80
            repoTax3_Code.ReadOnly = True
            repoTax3_Code.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax3_Code)

            Dim repoTax3_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax3_Rate.FormatString = ""
            repoTax3_Rate.HeaderText = "Tax3 Rate"
            repoTax3_Rate.Name = colTAX3_Rate
            repoTax3_Rate.Width = 80
            repoTax3_Rate.ReadOnly = True
            repoTax3_Rate.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax3_Rate)


            Dim repoTax4_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax4_Code.FormatString = ""
            repoTax4_Code.HeaderText = "Tax4 Code"
            repoTax4_Code.Name = colTAX4_Code
            repoTax4_Code.Width = 80
            repoTax4_Code.ReadOnly = True
            repoTax4_Code.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax4_Code)

            Dim repoTax4_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax4_Rate.FormatString = ""
            repoTax4_Rate.HeaderText = "Tax4 Rate"
            repoTax4_Rate.Name = colTAX4_Rate
            repoTax4_Rate.Width = 80
            repoTax4_Rate.ReadOnly = True
            repoTax4_Rate.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax4_Rate)


            Dim repoTax5_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax5_Code.FormatString = ""
            repoTax5_Code.HeaderText = "Tax5 Code"
            repoTax5_Code.Name = colTAX5_Code
            repoTax5_Code.Width = 80
            repoTax5_Code.ReadOnly = True
            repoTax5_Code.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax5_Code)

            Dim repoTax5_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTax5_Rate.FormatString = ""
            repoTax5_Rate.HeaderText = "Tax5 Rate"
            repoTax5_Rate.Name = colTAX5_Rate
            repoTax5_Rate.Width = 80
            repoTax5_Rate.ReadOnly = True
            repoTax5_Rate.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoTax5_Rate)

            Dim repoModify_By As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoModify_By.FormatString = ""
            repoModify_By.HeaderText = "Modify By"
            repoModify_By.Name = colModify_By
            repoModify_By.Width = 80
            repoModify_By.ReadOnly = True
            repoModify_By.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoModify_By)

            Dim repoModify_Date As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoModify_Date.FormatString = ""
            repoModify_Date.HeaderText = "Modify Date"
            repoModify_Date.Name = colModify_Date
            repoModify_Date.Width = 80
            repoModify_Date.ReadOnly = True
            repoModify_Date.IsVisible = True
            Gv1.MasterTemplate.Columns.Add(repoModify_Date)

            Gv1.MasterTemplate.AllowAddNewRow = False

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        FunReset()
    End Sub

    Private Sub FunReset()
        Try
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            'FormatGrid()
            txtDoc.arrValueMember = Nothing
            fillTransactionType()
            ddlTransType.SelectedValue = "All"
            RadPageView1.SelectedPage = RadPageViewPage1
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemWiseTaxMasterReport & "'"))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Item Wise Tax Master Report")
                clsCommon.MyExportToPDF("Item Wise Tax Master Report", Gv1, arrHeader, "Item Wise Tax Master Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RbtnSaveLayout_Click(sender As Object, e As EventArgs) Handles RbtnSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RbtnDeleteLayout_Click(sender As Object, e As EventArgs) Handles RbtnDeleteLayout.Click
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) AndAlso RadPageViewPage2.Item.Visibility = ElementVisibility.Visible Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        End If
    End Sub

  
    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            If e.Column Is Gv1.Columns(colDoc_Code) OrElse e.Column Is Gv1.Columns(colDoc_Date) Then
                Dim doccode As String = ""
                doccode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colDoc_Code).Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemWiseTax, doccode)
            ElseIf e.Column Is Gv1.Columns(colItem_Code) OrElse e.Column Is Gv1.Columns(colItem_Desc) Then
                Dim itemcode As String = ""
                itemcode = clsCommon.myCstr(Gv1.CurrentRow.Cells(colItem_Code).Value)
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDoc__My_Click(sender As Object, e As EventArgs) Handles txtDoc._My_Click
        strQry = "select HCODE as Code,Description as Description from TSPL_ITEM_WISE_TAX"
        txtDoc.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Description", txtDoc.arrValueMember, txtDoc.arrDispalyMember)
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptItemWiseTaxMasterReport & "'"))

            If clsCommon.myLen(ddlTransType.Text) > 0 Then
                arrHeader.Add("Transaction Type : " + ddlTransType.Text)
            End If

            If txtDoc.arrValueMember IsNot Nothing AndAlso txtDoc.arrValueMember.Count > 0 Then
                arrHeader.Add("Document Code : " + clsCommon.GetMulcallStringWithComma(txtDoc.arrValueMember))
            End If
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Item Wise Tax Master Report", Gv1, arrHeader, "Item Wise Tax Master Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
