'=============BM00000003434

Imports common
Imports System.Data.SqlClient

Public Class FrmCFormEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private IsCellFormatting As Boolean = False

    Const colDocType As String = "colDocType"
    Const colLineNo As String = "colLineNo"
    Const ColApply As String = "ColApply"
    Const ColDocNo As String = "ColDocNo"
    Const ColDocDate As String = "ColDocDate"
    Const colParty As String = "colParty"
    Const ColTaxableAmt As String = "ColTaxableAmt"
    Const ColtaxAmt As String = "ColTaxAmt"
    Const ColDocAmt As String = "ColDocAmt"
    Const ColCFormAmt As String = "ColCFormAmt"
    Const ColDiff As String = "ColDiff"
    Const ColFormNo As String = "ColFormNo"
    Const ColRemarks As String = "ColRemarks"
    Const ColIsManual As String = "Is Manual"
    Const ColManualFormNo As String = "Manual Form No"
    Const ColToLoc As String = "To Location"
    Const ColToLocDesc As String = "To Location Desc"
    Dim strQuery As String
    Dim dt As DataTable
#End Region


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmCFormEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnpost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        txtFormNo.MaxLength = 30
        txtDesc.MaxLength = 100
        txtReference.MaxLength = 100

    End Sub

    Private Sub FrmCFormEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetLength()
        SetUserMgmtNew()
        rdbCustomer.IsChecked = True
        AddNew()


    End Sub

    Private Sub fndSrcCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSrcCode._MYValidating
        Dim qry As String = ""
        If rdbCustomer.IsChecked = True Then
            qry = "select Cust_Code as [Code] ,Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
            fndSrcCode.Value = clsCommon.ShowSelectForm("fmSrcCode", qry, "Code", "", fndSrcCode.Value, "Code", isButtonClicked)
            lblSourceCode.Text = clsDBFuncationality.getSingleValue("select Customer_Name  from TSPL_CUSTOMER_MASTER where Cust_Code  ='" & fndSrcCode.Value & "'")

        ElseIf rdbVendor.IsChecked = True Then
            qry = "select Vendor_Code as  Code ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER"
            Dim WhrCls = " TSPL_VENDOR_MASTER.Status='N' "
            fndSrcCode.Value = clsCommon.ShowSelectForm("fmVendorCode", qry, "Code", WhrCls, fndSrcCode.Value, "Code", isButtonClicked)
            lblSourceCode.Text = clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code  ='" & fndSrcCode.Value & "'")

        ElseIf rdbLocation.IsChecked = True Then
            qry = "select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER"
            fndSrcCode.Value = clsCommon.ShowSelectForm("frmLocationCode", qry, "Code", "", fndSrcCode.Value, "Code", isButtonClicked)
            lblSourceCode.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code  ='" & fndSrcCode.Value & "'")
        End If
    End Sub

    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False


        Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 60
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(LineNo)



        Dim gvSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        gvSelect.FormatString = ""
        gvSelect.Name = ColApply
        gvSelect.HeaderText = "Select"
        gvSelect.Width = 50
        gvSelect.ReadOnly = False
        gvSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(gvSelect)

        Dim gvSelect1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gvSelect1.FormatString = ""
        gvSelect1.Name = colDocType
        gvSelect1.HeaderText = "Doc Type"
        gvSelect1.Width = 50
        gvSelect1.IsVisible = False
        gvSelect1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(gvSelect1)

        Dim docNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        docNo.FormatString = ""
        If rdbLocation.IsChecked Then
            docNo.HeaderText = "Transfer No"
        Else
            docNo.HeaderText = "Document No"
        End If
        docNo.Name = ColDocNo
        docNo.Width = 100
        docNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(docNo)

        Dim Docdate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Docdate.FormatString = ""
        If rdbLocation.IsChecked Then
            Docdate.HeaderText = "Transfer Date"
        Else
            Docdate.HeaderText = "Document Date"
        End If
        Docdate.Name = ColDocDate
        Docdate.Width = 70
        Docdate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Docdate)

        Dim RepoParty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RepoParty.FormatString = ""
        RepoParty.HeaderText = "Party"
        RepoParty.Name = colParty
        RepoParty.Width = 0
        RepoParty.IsVisible = False
        RepoParty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RepoParty)

        Dim TaxableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        TaxableAmt = New GridViewDecimalColumn()
        TaxableAmt.FormatString = ""
        TaxableAmt.HeaderText = "Taxable Amount"
        TaxableAmt.Name = ColTaxableAmt
        TaxableAmt.Width = 120
        TaxableAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(TaxableAmt)


        Dim TaxAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        TaxAmt = New GridViewDecimalColumn()
        TaxAmt.FormatString = ""
        TaxAmt.HeaderText = "Tax Amount"
        TaxAmt.Name = ColtaxAmt
        TaxAmt.Width = 120
        TaxAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(TaxAmt)

        Dim InvAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        InvAmt = New GridViewDecimalColumn()
        InvAmt.FormatString = ""
        If rdbLocation.IsChecked Then
            InvAmt.HeaderText = "Transfer Amount"
        Else
            InvAmt.HeaderText = "Document Amount"
        End If

        InvAmt.Name = ColDocAmt
        InvAmt.Width = 120
        InvAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(InvAmt)

        Dim CFormAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        CFormAmt = New GridViewDecimalColumn()
        CFormAmt.FormatString = ""
        CFormAmt.HeaderText = "CForm Amount"
        CFormAmt.Name = ColCFormAmt
        CFormAmt.Width = 120
        CFormAmt.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(CFormAmt)

        Dim Diff As GridViewDecimalColumn = New GridViewDecimalColumn()
        Diff = New GridViewDecimalColumn()
        Diff.FormatString = ""
        Diff.HeaderText = "Diff"
        Diff.Name = ColDiff
        Diff.Width = 120
        Diff.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Diff)

        Dim ToLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ToLoc.FormatString = ""
        ToLoc.HeaderText = "To Location"
        ToLoc.Name = ColToLoc
        ToLoc.Width = 100
        ToLoc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ToLoc)

        Dim ToLocD As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ToLocD.FormatString = ""
        ToLocD.HeaderText = "To Location Desp"
        ToLocD.Name = ColToLocDesc
        ToLocD.Width = 120
        ToLocD.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ToLocD)

        Dim repoManual As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoManual.HeaderText = "Is Manual"
        repoManual.Name = ColIsManual
        repoManual.IsVisible = True
        repoManual.Width = 75
        repoManual.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoManual)

        Dim FormNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        FormNo.FormatString = ""
        FormNo.HeaderText = "Form No"
        FormNo.Name = ColFormNo
        FormNo.Width = 100
        ' FormNo.ReadOnly = False
        FormNo.HeaderImage = Global.ERP.My.Resources.Resources.search4
        FormNo.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(FormNo)

        Dim MFormNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        MFormNo.FormatString = ""
        MFormNo.HeaderText = "Form No"
        MFormNo.Width = 200
        MFormNo.Name = ColManualFormNo
        MFormNo.ReadOnly = False
        MFormNo.IsVisible = True
        gv1.MasterTemplate.Columns.Add(MFormNo)

        Dim Remark As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Remark.FormatString = ""
        Remark.HeaderText = "Remarks"
        Remark.Width = 200
        Remark.Name = ColRemarks
        Remark.ReadOnly = False
        Remark.IsVisible = True
        gv1.MasterTemplate.Columns.Add(Remark)



        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            'btnPost.Enabled = True
            LoadBlankGrid()
            isInsideLoadData = True
            Dim obj As New clsCForm()
            obj = clsCForm.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isNewEntry = False
                'If obj.Post = "Y" Then
                '    btnSave.Enabled = False
                '    btnPost.Enabled = False
                'Else
                '    btnSave.Enabled = True
                '    btnPost.Enabled = True
                'End If
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtFormNo.Text = obj.CForm_No
                txtFormDate.Text = obj.CForm_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                txtFormCode.Value = obj.Form_Code
                lblFormType.Text = clsDBFuncationality.getSingleValue("select Form_Name  from TSPL_Form_Master where Form_Code  ='" & txtFormCode.Value & "'")

                Dim strType As String
                strType = obj.CollectionType
                If clsCommon.CompairString(strType, "C") = CompairStringResult.Equal Then
                    rdbCustomer.IsChecked = True
                ElseIf clsCommon.CompairString(strType, "V") = CompairStringResult.Equal Then
                    rdbVendor.IsChecked = True
                Else
                    rdbLocation.IsChecked = True
                End If
                fndSrcCode.Value = obj.Source_Code
                lblSourceCode.Text = obj.Source_Name
                FromDate.Value = obj.From_Date
                ToDate.Value = obj.To_Date
                'isCellValueChangedOpen = True
                'isInsideLoadData = True
                Dim intLineNo As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsCFormDetails In obj.Arr
                        gv1.Rows.AddNew()

                        If clsCommon.myCstr(objTr.Status) = "1" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColApply).Value = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColApply).Value = False
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(intLineNo)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDocNo).Value = objTr.Invoice_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = objTr.doc_type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDocDate).Value = clsCommon.GetPrintDate(objTr.Invoice_Date, "dd/MMM/yyyy")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColTaxableAmt).Value = objTr.TaxableAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColtaxAmt).Value = objTr.TaxAmount

                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDocAmt).Value = objTr.InvoiceAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCFormAmt).Value = objTr.CFormAmount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDiff).Value = objTr.Diff
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colParty).Value = objTr.party

                        If rdbLocation.IsChecked = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLoc).Value = objTr.To_Location
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLoc).Value = ""
                        End If

                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLoc).Value)) >= 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLocDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc FROM TSPL_LOCATION_MASTER WHERE Location_Code ='" & clsCommon.myCstr(objTr.To_Location) & "'"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLocDesc).Value = ""
                        End If
                        If objTr.Is_Manual = "1" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColIsManual).Value = True
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColManualFormNo).Value = objTr.Form_No
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColIsManual).Value = False
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFormNo).Value = objTr.Form_No
                        End If
                        intLineNo += 1
                    Next
                    isInsideLoadData = False
                End If
            End If
            '  isCellValueChangedOpen = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub
    Private Sub funFillGrid()
        Try
            LoadBlankGrid()
            Dim intLineNo As Integer = 1
            isInsideLoadData = True
            If clsCommon.myLen(fndSrcCode.Value) > 0 Then
                If rdbCustomer.IsChecked Then
                    strQuery = "select DocType,Status,Document_Code,Inv_Date,Total_Amt,party,Taxable_Amount,Tax_Amount,Form_No from ("
                    strQuery += "select 'SALE' as DocType,0 as Status,Document_Code,Inv_Date,Total_Amt,Customer_name  as party,Amount_Less_Discount as Taxable_Amount,Total_Tax_Amt as Tax_Amount,'' as Form_No from TSPL_SD_SALE_INVOICE_HEAD inner join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_SD_SALE_INVOICE_HEAD.customer_code  where  CFormApplied=0  and CFormRecd=0 "
                    If clsCommon.myLen(fndSrcCode.Value) > 0 Then
                        strQuery &= " and Customer_Code='" & fndSrcCode.Value & "' "
                    End If
                    strQuery &= " and Total_Amt > 0 and Document_Code not in (select Invoice_No from TSPL_CForm_DETAIL) " & _
                " and Inv_Date >= '" & clsCommon.GetPrintDate(FromDate.Value, "dd/MMM/yyyy") & "' and Inv_Date < = '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'"

                    '=====================add csa transfer screen======BM00000004333================
                    strQuery += " Union all "
                    strQuery += "select 'CSA_Transfer' as DocType,0 as Status,doc_code as Document_Code,transfer_date as Inv_Date,document_amount as Total_Amt,Customer_name  as party,tax1_base_amt as Taxable_Amount,Total_Tax_Amt as Tax_Amount,'' as Form_No from TSPL_CSA_TRANSFER_HEAD inner join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code=TSPL_CSA_TRANSFER_HEAD.cust_code  where  CFormApplied=0  and CFormRecd=0 "
                    If clsCommon.myLen(fndSrcCode.Value) > 0 Then
                        strQuery += " and TSPL_CSA_TRANSFER_HEAD.cust_code='" & fndSrcCode.Value & "' "
                    End If
                    strQuery += " and document_amount > 0 and against_form='F' and doc_code not in (select Invoice_No from TSPL_CForm_DETAIL) " & _
                " and convert(date,transfer_date,103) >= convert(date,'" & clsCommon.GetPrintDate(FromDate.Value, "dd/MMM/yyyy") & "',103) and convert(date,transfer_date,103) < = convert(date,'" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "',103)"
                    '===========================================================
                    strQuery += ")axa  order by axa.inv_date asc"
                ElseIf rdbVendor.IsChecked Then
                    strQuery = "select 'PURCHASE' as DocType,0 as Status,PI_No as Document_Code,PI_Date as Inv_Date,PI_Total_Amt as Total_Amt,vendor_name as party,Amount_Less_Discount as Taxable_Amount,Total_Tax_Amt as Tax_Amount,'' as Form_No  from TSPL_PI_HEAD  where " & _
                    "  CFormApplied=0 and CFormRecd=0 and PI_Total_Amt > 0 and PI_No not in (select Invoice_No from TSPL_CForm_DETAIL) "
                    If clsCommon.myLen(fndSrcCode.Value) > 0 Then
                        strQuery &= " and Vendor_Code='" & fndSrcCode.Value & "'"
                    End If
                    strQuery &= " and PI_Date >= '" & clsCommon.GetPrintDate(FromDate.Value, "dd/MMM/yyyy") & "' and PI_Date < = '" & clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") & "'  order by PI_Date asc "
                Else
                    'strQuery = "select 'TRANSFER' as DocType,0 as Status,Document_No as Document_Code,Document_Date as Inv_Date,DOC_Total_Amt as Total_Amt,'' as party,Amount_Less_Discount as Taxable_Amount,Total_Tax_Amt as Tax_Amount,'' as Form_No " & _
                    '       "from TSPL_Transfer_ORDER_HEAD " & _
                    '       "where 1 = 1 and Form_Received = 0 and Transfer_Type='O' and Status = 1 and Is_AgainstFormF = 1 and DOC_Total_Amt > 0 " & _
                    '       "and From_Location='" + fndSrcCode.Value + "' and Document_No not in (select Invoice_No from TSPL_CForm_DETAIL) " & _
                    '       "and Document_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate.Value), "dd/MMM/yyyy HH:mm:ss") + "' and Document_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy HH:mm:ss") + "' "

                    strQuery = " SELECT * FROM " & _
                    " ( SELECT 'TRANSFER' as DocType,0 as Status,Document_No as Document_Code,Document_Date as Inv_Date,DOC_Total_Amt as Total_Amt,'' as party,Amount_Less_Discount as Taxable_Amount,Total_Tax_Amt as Tax_Amount,'' as Form_No,TSPL_LOCATION_MASTER.Location_Code AS To_Location,TSPL_LOCATION_MASTER.Location_Desc,Form_Received,Transfer_Type,From_Location AS From_Location from TSPL_Transfer_ORDER_HEAD " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.GIT_Location= TSPL_Transfer_ORDER_HEAD.To_Location " & _
                    " where 1 = 1 and Form_Received = 0 and Transfer_Type='O' and Status = 1 and Is_AgainstFormF = 1 and DOC_Total_Amt > 0 " & _
                                         " UNION ALL " & _
                    " SELECT 'CSA TRANSFER'as DocType,0 as Status,DOC_CODE AS Document_Code,Transfer_Date AS Inv_Date,Document_Amount as Total_Amt,TSPL_CSA_TRANSFER_HEAD.Cust_Code  as Party,Amount_Less_Discount AS Taxable_Amount, " & _
                    " Total_Tax_Amt  as Tax_Amount,'' as Form_No,To_Location_Code as To_Location,TSPL_LOCATION_MASTER.Location_Desc,'' AS Form_Received,'' AS Transfer_Type ,From_Location_Code AS From_Location FROM TSPL_CSA_TRANSFER_HEAD " & _
                    " LEFT OUTER JOIN TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.GIT_Location= TSPL_CSA_TRANSFER_HEAD.To_Location_Code  " & _
                    " WHERE 1=1 AND Status =1 AND Against_Form='F' AND Document_Amount >0 " & _
                    " ) xx " & _
                    " where 1 = 1 " & _
                    " and XX.From_Location='" + fndSrcCode.Value + "' and XX.Document_Code not in (select Invoice_No from TSPL_CForm_DETAIL) " & _
                    " and XX.Inv_Date >='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(FromDate.Value), "dd/MMM/yyyy HH:mm:ss") + "' and XX.Inv_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy HH:mm:ss") + "' "
                End If


                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(strQuery)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows.AddNew()
                        If clsCommon.myCstr(dr("Status")) = "1" Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColApply).Value = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColApply).Value = False
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(intLineNo)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDocType).Value = clsCommon.myCstr(dr("doctype"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDocNo).Value = clsCommon.myCstr(dr("Document_Code"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDocDate).Value = clsCommon.GetPrintDate(dr("Inv_Date"), "dd/MMM/yyyy")
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDocAmt).Value = clsCommon.myCdbl(dr("Total_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCFormAmt).Value = clsCommon.myCdbl(dr("Total_Amt"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDiff).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRemarks).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colParty).Value = clsCommon.myCstr(dr("party"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColTaxableAmt).Value = clsCommon.myCdbl(dr("Taxable_Amount"))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColtaxAmt).Value = clsCommon.myCdbl(dr("Tax_Amount"))
                        If clsCommon.myCdbl(dr("Form_No")) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFormNo).Value = clsCommon.myCdbl(dr("Form_No"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColFormNo).Value = Nothing
                        End If
                        If rdbLocation.IsChecked = True Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLoc).Value = clsCommon.myCstr(dr("To_Location"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColToLocDesc).Value = clsCommon.myCstr(dr("Location_Desc"))
                        End If

                        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColManualFormNo).Value = clsCommon.myCstr(dr("Manual_Form_No"))
                        txtDate.Enabled = False
                        intLineNo += 1
                    Next
                End If
            End If
            isInsideLoadData = False
            IsCellFormatting = True
            gv1.BestFitColumns()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "CForm Entry", MessageBoxButtons.OK)
        End Try
    End Sub

    Sub AddNew()
        'BlankAllControls()
        LoadBlankGrid()
        isInsideLoadData = False
        IsCellFormatting = False
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        txtDate.Value = clsCommon.GETSERVERDATE
        txtFormDate.Value = clsCommon.GETSERVERDATE
        txtDocNo.Value = ""
        fndSrcCode.Value = ""
        txtFormNo.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtDate.Focus()
        gv1.Rows.AddNew()
        FromDate.Value = clsCommon.GETSERVERDATE
        ToDate.Value = clsCommon.GETSERVERDATE
        lblSourceCode.Text = ""
        txtFormCode.Value = ""
        lblFormType.Text = ""
    End Sub


    Private Function AllowToSave() As Boolean

        Dim MFormNo As String = String.Empty
        Dim FormNo As String = String.Empty

        If clsCommon.myLen(fndSrcCode.Value) <= 0 Then
            fndSrcCode.Focus()
            Throw New Exception("Please select Source Code")
        End If
        'If clsCommon.myLen(txtFormNo.Text) <= 0 Then
        '    txtFormNo.Focus()
        '    Throw New Exception("Please enter CForm No")
        'End If
        If clsCommon.myLen(txtFormCode.Value) <= 0 Then
            txtFormNo.Focus()
            Throw New Exception("Please Select Form Type")
        End If
        Dim strDocDate As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(ColDocNo).Value)
            Dim strCFormAmt As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColCFormAmt).Value)
            MFormNo = clsCommon.myCstr(gv1.Rows(ii).Cells(ColManualFormNo).Value)
            FormNo = clsCommon.myCstr(gv1.Rows(ii).Cells(ColFormNo).Value)
            If clsCommon.myCBool(gv1.Rows(ii).Cells(ColApply).Value) Then
                If clsCommon.myLen(strICode) > 0 Then
                    If clsCommon.myLen(strCFormAmt) <= 0 Then
                        Throw New Exception("Please enter CForm amount of invoice " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                    End If
                    If clsCommon.myCBool(gv1.Rows(ii).Cells(ColIsManual).Value) Then
                        If clsCommon.myLen(MFormNo) <= 0 Then
                            Throw New Exception("Please enter Form No. invoice " + strICode + " at row no " + clsCommon.myCstr(ii + 1))
                        End If
                    Else
                        If clsCommon.myLen(FormNo) <= 0 Then
                            Throw New Exception("Please select Form No. invoice " + strICode + " at row no " + clsCommon.myCstr(ii + 1))
                        End If
                    End If
                End If

            End If


        Next
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If (AllowToSave()) Then


                Dim obj As New clsCForm()

                obj.Document_No = txtDocNo.Value
                obj.Document_Date = clsCommon.myCDate(txtDate.Value)
                obj.CForm_No = txtFormNo.Text
                obj.CForm_Date = txtFormDate.Value
                obj.CollectionType = IIf(rdbCustomer.IsChecked, "C", "V")
                If rdbLocation.IsChecked Then
                    obj.CollectionType = "L"
                End If
                obj.Source_Code = fndSrcCode.Value
                obj.Source_Name = lblSourceCode.Text
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                obj.From_Date = FromDate.Value
                obj.To_Date = ToDate.Value
                obj.Form_Code = txtFormCode.Value
                Dim intLineno As Integer = 1
                obj.Arr = New List(Of ClsCFormDetails)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCBool(grow.Cells(ColApply).Value) Then
                        Dim objTr As New ClsCFormDetails()
                        objTr.Document_Line_No = intLineno
                        objTr.Invoice_No = clsCommon.myCstr(grow.Cells(ColDocNo).Value)
                        objTr.doc_type = clsCommon.myCstr(grow.Cells(colDocType).Value)
                        objTr.Invoice_Date = clsCommon.myCstr(grow.Cells(ColDocDate).Value)
                        objTr.InvoiceAmount = clsCommon.myCstr(grow.Cells(ColDocAmt).Value)
                        objTr.CFormAmount = clsCommon.myCstr(grow.Cells(ColCFormAmt).Value)
                        objTr.Diff = clsCommon.myCdbl(grow.Cells(ColDiff).Value)
                        objTr.Remarks = clsCommon.myCstr(grow.Cells(ColRemarks).Value)


                        objTr.TaxableAmount = clsCommon.myCstr(grow.Cells(ColTaxableAmt).Value)
                        objTr.TaxAmount = clsCommon.myCstr(grow.Cells(ColtaxAmt).Value)
                        If CBool(grow.Cells(ColIsManual).Value) = True Then
                            objTr.Form_No = clsCommon.myCstr(grow.Cells(ColManualFormNo).Value)
                            objTr.Is_Manual = "1"
                        Else
                            objTr.Form_No = clsCommon.myCstr(grow.Cells(ColFormNo).Value)
                            objTr.Is_Manual = "0"
                        End If
                        If rdbLocation.IsChecked = True Then
                            objTr.To_Location = clsCommon.myCstr(grow.Cells(ColToLoc).Value)
                        Else
                            objTr.To_Location = ""
                        End If


                        obj.Arr.Add(objTr)
                        intLineno += 1
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Document", Me.Text)
                    Return
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If


            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If e.Column Is gv1.Columns(ColDocNo) Then
                If clsCommon.myLen(gv1.CurrentRow.Cells(ColDocNo).Value) > 0 Then
                    Dim frm As New frmSNSaleInvoice()
                    frm.SetUserMgmt(clsUserMgtCode.frmSNSaleInvoice)
                    frm.DocumentNo = clsCommon.myCstr(gv1.CurrentRow.Cells(ColDocNo).Value)
                    frm.ShowDialog()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles gv1.CellFormatting
        If e.RowIndex >= 0 Then
            If e.Column Is gv1.Columns(ColIsManual) Then
                If CBool(gv1.CurrentRow.Cells(ColIsManual).Value) = True Then
                    gv1.CurrentRow.Cells(ColFormNo).ReadOnly = True
                    gv1.CurrentRow.Cells(ColFormNo).Value = ""
                    gv1.CurrentRow.Cells(ColManualFormNo).ReadOnly = False
                    'gv1.CurrentRow.Cells(ColManualFormNo).Value = ""
                Else
                    gv1.CurrentRow.Cells(ColFormNo).ReadOnly = False
                    ' gv1.CurrentRow.Cells(ColFormNo).Value = ""
                    gv1.CurrentRow.Cells(ColManualFormNo).ReadOnly = True
                    gv1.CurrentRow.Cells(ColManualFormNo).Value = ""
                End If
            End If
        End If

    End Sub


    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(ColFormNo) Then
                        If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(ColFormNo).Value)) > 0 Then
                            If clsCommon.myLen(txtFormCode.Value) > 0 Then
                                gv1.CurrentRow.Cells(ColFormNo).Value = ClsCFormDetails.FinderForm_No(txtFormCode.Value, True)
                            Else
                                clsCommon.MyMessageBoxShow(Me, "Please select form type.", Me.Text)
                                isCellValueChangedOpen = False
                                gv1.CurrentRow.Cells(ColFormNo).Value = ""
                                Exit Sub
                            End If

                        End If
                    ElseIf e.Column Is gv1.Columns(ColApply) OrElse e.Column Is gv1.Columns(ColCFormAmt) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
    '    Try
    '        If (Not isInsideLoadData) Then
    '            If Not isCellValueChangedOpen Then
    '                'isCellValueChangedOpen = True
    '                If e.Column Is gv1.Columns(ColApply) Then
    '                    UpdateCurrentRow(gv1.CurrentRow.Index)
    '                End If
    '                If e.Column Is gv1.Columns(ColCFormAmt) Then
    '                    UpdateCurrentRow(gv1.CurrentRow.Index)
    '                End If
    '                ' isCellValueChangedOpen = False
    '            End If
    '        Else
    '            If e.Column Is gv1.Columns(ColFormNo) Then
    '                If Not isCellValueChangedOpen Then
    '                    isCellValueChangedOpen = True
    '                    If clsCommon.myLen(gv1.CurrentRow.Cells(ColFormNo).Value) > 0 Then
    '                        gv1.CurrentRow.Cells(ColFormNo).Value = ClsCFormDetails.FinderForm_No(txtFormCode.Value, True)
    '                        isCellValueChangedOpen = False
    '                    End If

    '                End If
    '            End If
    '            If Not isCellValueChangedOpen Then
    '                isCellValueChangedOpen = True
    '                If e.Column Is gv1.Columns(ColApply) Then
    '                    UpdateCurrentRow(gv1.CurrentRow.Index)
    '                End If
    '                If e.Column Is gv1.Columns(ColCFormAmt) Then
    '                    UpdateCurrentRow(gv1.CurrentRow.Index)
    '                End If
    '                isCellValueChangedOpen = False
    '            End If
    '            End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim strApply As Boolean = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(ColApply).Value)
        Dim dblCFOrmAmt As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColCFormAmt).Value)
        If strApply = True AndAlso dblCFOrmAmt > 0 Then
            Dim dblInAmt As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(ColDocAmt).Value)
            Dim dblAmt As Double = dblInAmt - dblCFOrmAmt
            gv1.Rows(IntRowNo).Cells(ColDiff).Value = dblAmt
        Else
            gv1.Rows(IntRowNo).Cells(ColDiff).Value = 0
        End If

    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow > 0 Then
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            End If

            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (clsCForm.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
        rdbCustomer.IsChecked = True
        txtDocNo.Value = ""
    End Sub

    Private Sub rdbCustomer_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbCustomer.ToggleStateChanged
        If clsCommon.myLen(txtDocNo.Value) = 0 Then
            AddNew()
        End If
    End Sub

    Private Sub rdbVendor_ToggleStateChanged(ByVal sender As Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbVendor.ToggleStateChanged
        If clsCommon.myLen(txtDocNo.Value) = 0 Then
            AddNew()
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        funFillGrid()
    End Sub

    Private Sub txtDocNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_CForm_HEADER where Document_No='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = " SELECT  Document_No as [DocCode],Document_Date as [Doc Date],CForm_No as [CForm No],CForm_Date as [CForm Date],case when CollectionType='C' then 'Customer' else 'Vendor' end as Type,Source_Code as [Source Code],Reference  from TSPL_CForm_HEADER"

        LoadData(clsCommon.ShowSelectForm("CFormEntry", qry, "DocCode", "", txtDocNo.Value, "DocCode", isButtonClicked), NavigatorType.Current)
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            txtDocNo.MyReadOnly = False
        Else
            txtDocNo.MyReadOnly = True
        End If
    End Sub


    Private Sub txtFormCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtFormCode._MYValidating
        Dim WhrCls As String = String.Empty

        If rdbLocation.CheckState = CheckState.Checked Then
            WhrCls = " Form_Type ='F' "
        Else
            WhrCls = ""
        End If
        Dim qry1 As String = "select Form_Code as [Code] ,Form_Name as [Name], Form_Type as [Type] from TSPL_Form_Master"
        txtFormCode.Value = clsCommon.ShowSelectForm("Formtype", qry1, "Code", WhrCls, txtFormCode.Value, "Code", isButtonClicked)
        lblFormType.Text = clsDBFuncationality.getSingleValue("select Form_Name  from TSPL_Form_Master where Form_Code  ='" & txtFormCode.Value & "'")
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class

