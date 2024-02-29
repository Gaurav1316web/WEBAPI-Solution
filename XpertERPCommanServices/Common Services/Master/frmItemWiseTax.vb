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

Public Class FrmItemWiseTax
    Inherits FrmMainTranScreen
#Region "Variables"
    Const colLineNo As String = "colLineNo"
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
    Dim userCode, companyCode As String
    Private isCellValueChanged As Boolean = False
    Dim isImport As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim chkPostClick As Boolean = False
#End Region
    Private Sub FrmItemWiseTax_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        gv1.Rows.AddNew()
        ButtonToolTip.SetToolTip(btnAdd, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        gv1.Enabled = True
        ' For dril down open 
        If clsCommon.myLen(Me.Tag) > 0 Then
            fndCode.Value = clsCommon.myCstr(Me.Tag)
            LoadData(fndCode.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
        End If
        btnAdd.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
    End Sub

    Sub Reset()
        fillTransactionType()
        fndCode.Value = ""
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        ddlTransType.SelectedValue = "S"
        fndCode.MyReadOnly = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnAdd.Enabled = True
        LoadBlankGrid1()
        isNewEntry = True
        btnAdd.Text = "Save"
        ddlTransType.Enabled = True
        txtDescription.Text = ""
        lblPending.Status = ERPTransactionStatus.Pending
    End Sub
    Sub LoadBlankGrid1()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        '------------------------------
        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colItem_Code
        repoItemCode.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItemCode.Width = 90
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.HeaderText = "Item Description"
        repoItemDesc.Name = colItem_Desc
        repoItemDesc.Width = 120
        repoItemDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemDesc)

        Dim repoHSNCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoHSNCode.FormatString = ""
        repoHSNCode.HeaderText = "HSN Code"
        repoHSNCode.Name = colHSN_Code
        repoHSNCode.Width = 100
        repoHSNCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoHSNCode)


        Dim repoTaxGroup As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroup.FormatString = ""
        repoTaxGroup.HeaderText = "Tax Group Code"
        repoTaxGroup.Name = colTAX_GROUP_CODE
        repoTaxGroup.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTaxGroup.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTaxGroup.Width = 80
        gv1.MasterTemplate.Columns.Add(repoTaxGroup)


        Dim repoTaxGroupDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTaxGroupDesc.FormatString = ""
        repoTaxGroupDesc.HeaderText = "Tax Group Description"
        repoTaxGroupDesc.Name = colTAX_GROUP_Desc
        repoTaxGroupDesc.Width = 120
        repoTaxGroupDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTaxGroupDesc)


        Dim repoTax1_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax1_Code.FormatString = ""
        repoTax1_Code.HeaderText = "Tax1 Code"
        repoTax1_Code.Name = colTAX1_Code
        repoTax1_Code.Width = 80
        'repoTax1_Code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        'repoTax1_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax1_Code.ReadOnly = True
        repoTax1_Code.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax1_Code) '26

        Dim repoTax1_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()  ' Dim repoTax1_Rate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax1_Rate.FormatString = ""
        repoTax1_Rate.HeaderText = "Tax1 Rate"
        repoTax1_Rate.Name = colTAX1_Rate
        repoTax1_Rate.Width = 80
        repoTax1_Rate.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTax1_Rate.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax1_Rate.ReadOnly = False
        repoTax1_Rate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax1_Rate) '27


        Dim repoTax2_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2_Code.FormatString = ""
        repoTax2_Code.HeaderText = "Tax2 Code"
        repoTax2_Code.Name = colTAX2_Code
        'repoTax2_Code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        'repoTax2_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax2_Code.Width = 80
        repoTax2_Code.ReadOnly = True
        repoTax2_Code.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax2_Code) '26

        Dim repoTax2_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax2_Rate.FormatString = ""
        repoTax2_Rate.HeaderText = "Tax2 Rate"
        repoTax2_Rate.Name = colTAX2_Rate
        repoTax2_Rate.Width = 80
        repoTax2_Rate.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTax2_Rate.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax2_Rate.ReadOnly = False
        repoTax2_Rate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax2_Rate) '27


        Dim repoTax3_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3_Code.FormatString = ""
        repoTax3_Code.HeaderText = "Tax3 Code"
        repoTax3_Code.Name = colTAX3_Code
        repoTax3_Code.Width = 80
        'repoTax3_Code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        'repoTax3_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax3_Code.ReadOnly = True
        repoTax3_Code.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax3_Code) '26

        Dim repoTax3_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax3_Rate.FormatString = ""
        repoTax3_Rate.HeaderText = "Tax3 Rate"
        repoTax3_Rate.Name = colTAX3_Rate
        repoTax3_Rate.Width = 80
        repoTax3_Rate.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTax3_Rate.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax3_Rate.ReadOnly = False
        repoTax3_Rate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax3_Rate) '27


        Dim repoTax4_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4_Code.FormatString = ""
        repoTax4_Code.HeaderText = "Tax4 Code"
        repoTax4_Code.Name = colTAX4_Code
        repoTax4_Code.Width = 80
        'repoTax4_Code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        'repoTax4_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax4_Code.ReadOnly = True
        repoTax4_Code.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax4_Code) '26

        Dim repoTax4_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax4_Rate.FormatString = ""
        repoTax4_Rate.HeaderText = "Tax4 Rate"
        repoTax4_Rate.Name = colTAX4_Rate
        repoTax4_Rate.Width = 80
        repoTax4_Rate.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTax4_Rate.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax4_Rate.ReadOnly = False
        repoTax4_Rate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax4_Rate) '27


        Dim repoTax5_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5_Code.FormatString = ""
        repoTax5_Code.HeaderText = "Tax5 Code"
        repoTax5_Code.Name = colTAX5_Code
        repoTax5_Code.Width = 80
        'repoTax5_Code.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        'repoTax5_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax5_Code.ReadOnly = True
        repoTax5_Code.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax5_Code) '26

        Dim repoTax5_Rate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTax5_Rate.FormatString = ""
        repoTax5_Rate.HeaderText = "Tax5 Rate"
        repoTax5_Rate.Name = colTAX5_Rate
        repoTax5_Rate.Width = 80
        repoTax5_Rate.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoTax5_Rate.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTax5_Rate.ReadOnly = False
        repoTax5_Rate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoTax5_Rate) '27

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AutoSizeRows = False
        gv1.AllowRowReorder = True
        ReStoreGridLayout()
    End Sub
    Function AllowToSave() As Boolean
        Try
            'If isNewEntry = True Then
            '    Dim strDate As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_WISE_TAX_HEAD  where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and  convert (varchar,DOC_DATE,103) = convert (varchar,'" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "')"))
            '    If strDate > 0 Then
            '        common.clsCommon.MyMessageBoxShow("Start Date : '" + clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy")) + "'  with Transaction Type : '" + ddlTransType.SelectedValue + "' Already Exist.")
            '        Return False
            '    End If
            'End If
            If gv1.Rows.Count <= 0 Then
                Dim Msg As String = "Please fill Item first... "
                common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                Return False
            End If
            If clsCommon.myLen(gv1.Rows(0).Cells(colItem_Code).Value) <= 0 Then
                Dim Msg As String = "Please fill Item first ... "
                common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                Return False
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strItemCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItem_Code).Value)
                Dim strTaxGroupCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX_GROUP_CODE).Value)
                Dim strSno As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colLineNo).Value)
                If clsCommon.myLen(strItemCode) <= 0 And clsCommon.myLen(strSno) > 0 Then
                    Dim Msg As String = "Item Code Can not be blank at Row No " + clsCommon.myCstr(ii + 1)
                    common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                    Return False
                End If
                If clsCommon.myLen(strItemCode) > 0 And clsCommon.myLen(strSno) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_ITEM_MASTER where Item_Code='" + strItemCode + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Item Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxGroupCode) <= 0 And clsCommon.myLen(strSno) > 0 Then
                    Dim Msg As String = "Tax Group Code Can not be blank at Row No " + clsCommon.myCstr(ii + 1)
                    common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                    Return False
                End If

                Dim strGroupcodeChk As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX_GROUP_CODE).Value)
                If clsCommon.myLen(strGroupcodeChk) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and Tax_Group_Code = '" + strGroupcodeChk + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Tax Group Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                        Return False
                    End If
                End If

                If isNewEntry = True Then
                    If clsCommon.myLen(strItemCode) > 0 Then
                        Dim strCountDupicate As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_ITEM_WISE_TAX_GROUP  inner join  TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX.HCODE where  convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103) = convert(varchar, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103) and TSPL_ITEM_WISE_TAX.Type = '" + ddlTransType.SelectedValue + "' and TSPL_ITEM_WISE_TAX_GROUP.Item_Code = '" + strItemCode + "' and TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code = '" + strTaxGroupCode + "' "))
                        If strCountDupicate > 0 Then
                            Dim Msg As String = " Item already Exist another Document .Duplicate Entry not Possible Same Day  at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                Else
                    If clsCommon.myLen(strItemCode) > 0 Then
                        Dim strDocCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_ITEM_WISE_TAX.HCODE from TSPL_ITEM_WISE_TAX_GROUP  inner join  TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX.HCODE where  convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103) = convert(varchar, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy") + "',103) and TSPL_ITEM_WISE_TAX.Type = '" + ddlTransType.SelectedValue + "' and TSPL_ITEM_WISE_TAX_GROUP.Item_Code = '" + strItemCode + "' and TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code = '" + strTaxGroupCode + "' "))
                        If clsCommon.myLen(strDocCode) > 0 Then
                            If clsCommon.CompairString(strDocCode, fndCode.Value) <> CompairStringResult.Equal Then
                                Dim Msg As String = " Item already Exist another Document : '" + strDocCode + "'.Duplicate Entry not Possible Same Day  at Row No " + clsCommon.myCstr(ii + 1) + " "
                                common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                                Return False
                            End If
                        End If

                    End If

                End If

            Next
            For ii As Integer = 0 To gv1.Rows.Count - 1
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItem_Code).Value)
                Dim strITaxGroupCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX_GROUP_CODE).Value)
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colItem_Code).Value)
                    Dim strInnerTaxGroupCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colTAX_GROUP_CODE).Value)
                    If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strITaxGroupCode, strInnerTaxGroupCode) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Tax Group with Item  Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                        common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                        Return False
                    End If
                Next

            Next

            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(ii).Cells(colItem_Code).Value)) > 0 Then
                    Dim strTaxCode1 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX1_Code).Value)
                    Dim strTaxCode2 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX2_Code).Value)
                    Dim strTaxCode3 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX3_Code).Value)
                    Dim strTaxCode4 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX4_Code).Value)
                    Dim strTaxCode5 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX5_Code).Value)
                    Dim strAllTaxCode As String = ""

                    If clsCommon.myLen(strTaxCode1) > 0 AndAlso clsCommon.myLen(strTaxCode2) > 0 Then
                        If (clsCommon.CompairString(strTaxCode1, strTaxCode2) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If

                    End If
                    If clsCommon.myLen(strTaxCode1) > 0 AndAlso clsCommon.myLen(strTaxCode3) > 0 Then
                        If (clsCommon.CompairString(strTaxCode1, strTaxCode3) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If
                    If clsCommon.myLen(strTaxCode1) > 0 AndAlso clsCommon.myLen(strTaxCode4) > 0 Then
                        If (clsCommon.CompairString(strTaxCode1, strTaxCode4) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If
                    If clsCommon.myLen(strTaxCode1) > 0 AndAlso clsCommon.myLen(strTaxCode5) > 0 Then
                        If (clsCommon.CompairString(strTaxCode1, strTaxCode5) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strTaxCode2) > 0 AndAlso clsCommon.myLen(strTaxCode3) > 0 Then
                        If (clsCommon.CompairString(strTaxCode2, strTaxCode3) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strTaxCode2) > 0 AndAlso clsCommon.myLen(strTaxCode4) > 0 Then
                        If (clsCommon.CompairString(strTaxCode2, strTaxCode3) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strTaxCode2) > 0 AndAlso clsCommon.myLen(strTaxCode5) > 0 Then
                        If (clsCommon.CompairString(strTaxCode2, strTaxCode5) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strTaxCode3) > 0 AndAlso clsCommon.myLen(strTaxCode4) > 0 Then
                        If (clsCommon.CompairString(strTaxCode3, strTaxCode4) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strTaxCode3) > 0 AndAlso clsCommon.myLen(strTaxCode5) > 0 Then
                        If (clsCommon.CompairString(strTaxCode3, strTaxCode5) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                            Return False
                        End If
                    End If

                    If clsCommon.myLen(strTaxCode4) > 0 AndAlso clsCommon.myLen(strTaxCode5) > 0 Then
                        If (clsCommon.CompairString(strTaxCode4, strTaxCode5) = CompairStringResult.Equal) Then
                            Dim Msg As String = "Same Tax code  Exist at Row No " + clsCommon.myCstr(ii + 1) + " "
                            common.clsCommon.MyMessageBoxShow(Msg)
                            Return False
                        End If
                    End If
                End If

                Dim strGroupcodeChk As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX_GROUP_CODE).Value)
                Dim strTaxCodeChk1 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX1_Code).Value)
                Dim strTaxCodeChk2 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX2_Code).Value)
                Dim strTaxCodeChk3 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX3_Code).Value)
                Dim strTaxCodeChk4 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX4_Code).Value)
                Dim strTaxCodeChk5 As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX5_Code).Value)

                Dim strTaxRateChk1 As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX1_Rate).Value)
                Dim strTaxRateChk2 As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX2_Rate).Value)
                Dim strTaxRateChk3 As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX3_Rate).Value)
                Dim strTaxRateChk4 As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX4_Rate).Value)
                Dim strTaxRateChk5 As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX5_Rate).Value)

                If clsCommon.myLen(strTaxCodeChk1) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and Tax_Group_Code = '" + strGroupcodeChk + "' and Tax_Code ='" + strTaxCodeChk1 + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Tax1 Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk1) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_RATES where Tax_Code = '" + strTaxCodeChk1 + "' and Tax_Type = '" + ddlTransType.SelectedValue + "' and Tax_Rate = '" + strTaxRateChk1 + "' "))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid [Tax1 Rate]  at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk2) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and Tax_Group_Code = '" + strGroupcodeChk + "' and Tax_Code ='" + strTaxCodeChk2 + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Tax2 Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk2) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_RATES where Tax_Code = '" + strTaxCodeChk2 + "' and Tax_Type = '" + ddlTransType.SelectedValue + "' and Tax_Rate = '" + strTaxRateChk2 + "' "))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid [Tax2 Rate]  at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk3) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and Tax_Group_Code = '" + strGroupcodeChk + "' and Tax_Code ='" + strTaxCodeChk3 + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Tax3 Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk3) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_RATES where Tax_Code = '" + strTaxCodeChk3 + "'  and Tax_Type = '" + ddlTransType.SelectedValue + "' and Tax_Rate = '" + strTaxRateChk3 + "' "))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid [Tax3 Rate]  at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk4) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and Tax_Group_Code = '" + strGroupcodeChk + "' and Tax_Code ='" + strTaxCodeChk4 + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Tax4 Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk4) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_RATES where Tax_Code = '" + strTaxCodeChk4 + "' and Tax_Type = '" + ddlTransType.SelectedValue + "' and Tax_Rate = '" + strTaxRateChk4 + "' "))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid [Tax4 Rate]  at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk5) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS where Tax_Group_Type = '" + ddlTransType.SelectedValue + "' and Tax_Group_Code = '" + strGroupcodeChk + "' and Tax_Code ='" + strTaxCodeChk5 + "'"))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid Tax4 Code at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If

                If clsCommon.myLen(strTaxCodeChk5) > 0 Then
                    Dim strValue As String = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_RATES where Tax_Code = '" + strTaxCodeChk5 + "' and Tax_Type = '" + ddlTransType.SelectedValue + "' and Tax_Rate = '" + strTaxRateChk5 + "' "))
                    If strValue <= 0 Then
                        Dim Msg As String = " Invalid [Tax5 Rate]  at Row No " + clsCommon.myCstr(ii + 1) + " "
                        common.clsCommon.MyMessageBoxShow(Msg)
                        Return False
                    End If
                End If
                '==================


            Next

            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    'Sub SaveData()
    '    Try
    '        If AllowToSave() Then
    '            Dim obj As New clsItemWiseTax()
    '            Dim objGroup As New clsItemWiseTaxGroup()
    '            If isNewEntry = True Then
    '                If ddlTransType.SelectedValue = "P" Then
    '                    obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemPurchaseTax, "")
    '                ElseIf ddlTransType.SelectedValue = "S" Then
    '                    obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemSaleTax, "")
    '                ElseIf ddlTransType.SelectedValue = "T" Then
    '                    obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemTransferTax, "")
    '                End If
    '                If clsCommon.myLen(obj.HCODE) <= 0 Then
    '                    Throw New Exception("Error in code generation")
    '                End If
    '            Else
    '                obj.HCODE = fndCode.Value
    '            End If
    '            obj.DOC_DATE = dtpToDate.Value
    '            obj.Type = ddlTransType.SelectedValue
    '            obj.Description = txtDescription.Text
    '            obj.ArrGroup = New List(Of clsItemWiseTaxGroup)
    '            obj.ArrAuth = New List(Of clsItemWiseTaxAuthority)
    '            For ii As Integer = 0 To gv1.RowCount - 1
    '                If clsCommon.myLen(gv1.Rows(ii).Cells(colItem_Code).Value) > 0 Then
    '                    Dim objtr As New clsItemWiseTaxGroup
    '                    objtr.HCODE = obj.HCODE
    '                    objtr.SNO = ii + 1
    '                    objtr.DCODE = clsCommon.myCstr(objtr.HCODE) + "-" + clsCommon.myCstr(objtr.SNO)
    '                    objtr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colItem_Code).Value)
    '                    objtr.Tax_Group_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX_GROUP_CODE).Value)
    '                    'Dim objAuth As New clsItemWiseTaxAuthority
    '                    'objAuth.HCODE = fndCode.Value
    '                    'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX1_Code).Value)
    '                    'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX1_Rate).Value)
    '                    'obj.ArrAuth.Add(objAuth)
    '                    'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX2_Code).Value)
    '                    'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX2_Rate).Value)
    '                    'obj.ArrAuth.Add(objAuth)
    '                    'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX3_Code).Value)
    '                    'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX3_Rate).Value)
    '                    'obj.ArrAuth.Add(objAuth)
    '                    'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX4_Code).Value)
    '                    'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX4_Rate).Value)
    '                    'obj.ArrAuth.Add(objAuth)
    '                    'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX5_Code).Value)
    '                    'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX5_Rate).Value)
    '                    'obj.ArrAuth.Add(objAuth)


    '                    'objtr.Tax1_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX1_Code).Value)
    '                    'objtr.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX1_Rate).Value)

    '                    'objtr.Tax2_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX2_Code).Value)
    '                    'objtr.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX2_Rate).Value)

    '                    'objtr.Tax3_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX3_Code).Value)
    '                    'objtr.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX3_Rate).Value)

    '                    'objtr.Tax4_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX4_Code).Value)
    '                    'objtr.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX4_Rate).Value)

    '                    'objtr.Tax5_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX5_Code).Value)
    '                    'objtr.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX5_Rate).Value)
    '                    For iii As Integer = 1 To 5
    '                        Dim cloTax As String = "colTAX" + clsCommon.myCstr(iii) + "_Code"
    '                        Dim cloRat As String = "colTAX" + clsCommon.myCstr(iii) + "_Rate"

    '                        Dim objAuth As New clsItemWiseTaxAuthority
    '                        objAuth.HCODE = obj.HCODE
    '                        objAuth.DCODE = objtr.DCODE
    '                        objAuth.SNO = iii
    '                        objAuth.DDCODE = clsCommon.myCstr(objtr.DCODE) + "-" + clsCommon.myCstr(objAuth.SNO)
    '                        objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(cloTax).Value)
    '                        objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(cloRat).Value)
    '                        If clsCommon.myLen(objAuth.Tax_Authority) > 0 Then
    '                            obj.ArrAuth.Add(objAuth)
    '                        End If
    '                    Next

    '                    obj.ArrGroup.Add(objtr)

    '                End If
    '            Next
    '            'For ii As Integer = 0 To gv1.RowCount - 1
    '            '    If clsCommon.myLen(gv1.Rows(ii).Cells(colItem_Code).Value) > 0 Then

    '            '        For iii As Integer = 1 To 5
    '            '            Dim cloTax As String = "colTAX" + clsCommon.myCstr(iii) + "_Code"
    '            '            Dim cloRat As String = "colTAX" + clsCommon.myCstr(iii) + "_Rate"

    '            '            Dim objAuth As New clsItemWiseTaxAuthority
    '            '            objAuth.HCODE = obj.HCODE
    '            '            objAuth.DCODE =
    '            '            objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(cloTax).Value)
    '            '            objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(cloRat).Value)
    '            '            obj.ArrAuth.Add(objAuth)
    '            '        Next

    '            '    End If
    '            'Next
    '            If obj.SaveData(obj, isNewEntry) Then
    '                If chkPostClick = False Then
    '                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
    '                End If
    '                LoadData(obj.HCODE, NavigatorType.Current)
    '                btnAdd.Text = "Update"
    '            End If
    '        Else
    '            fndCode.MyReadOnly = False
    '            btnDelete.Enabled = False
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Sub OpenICode(ByVal isButtonClick As Boolean)
        gv1.CurrentRow.Cells(colItem_Code).Value = clsItemMaster.getFinder("", gv1.CurrentRow.Cells(colItem_Code).Value, isButtonClick)
    End Sub

    Sub OpenTaxGroupCode(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Tax_Group_Code,Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Active=1"
        isInsideLoadData = True
        gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value = clsTaxGroupMaster.getFinder("", gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value, isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) > 0 Then
            '' visible tax columns
            qry = "select Tax_Code,Tax_Code_Desc from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" & gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            For Each col As GridViewColumn In gv1.Columns
                For Each drCol As DataRow In dt.Rows
                    If col.Tag = clsCommon.myCstr(drCol.Item("Tax_Code")) Then
                        col.IsVisible = True
                    End If
                Next
            Next
        End If
        isInsideLoadData = False
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isImport = True Then
                Return
            End If
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    ' If e.Column Is gv1.Columns(colItem_Code) OrElse e.Column Is gv1.Columns(colTAX_GROUP_CODE) Then
                    If e.Column Is gv1.Columns(colItem_Code) Then
                        OpenICodeList(False)

                    ElseIf e.Column Is gv1.Columns(colTAX_GROUP_CODE) Then
                        OpenTaxGroupList(False)
                        'ElseIf e.Column Is gv1.Columns(colTAX1_Code) Then
                        '    OpenTax1CodeList(False)
                        'ElseIf e.Column Is gv1.Columns(colTAX2_Code) Then
                        '    OpenTax2CodeList(False)
                        'ElseIf e.Column Is gv1.Columns(colTAX3_Code) Then
                        '    OpenTax3CodeList(False)
                        'ElseIf e.Column Is gv1.Columns(colTAX4_Code) Then
                        '    OpenTax4CodeList(False)
                        'ElseIf e.Column Is gv1.Columns(colTAX5_Code) Then
                        '    OpenTax5CodeList(False)
                    ElseIf e.Column Is gv1.Columns(colTAX1_Rate) Then
                        OpenRate1CodeList(False)
                    ElseIf e.Column Is gv1.Columns(colTAX2_Rate) Then
                        OpenRate2CodeList(False)
                    ElseIf e.Column Is gv1.Columns(colTAX3_Rate) Then
                        OpenRate3CodeList(False)
                    ElseIf e.Column Is gv1.Columns(colTAX4_Rate) Then
                        OpenRate4CodeList(False)
                    ElseIf e.Column Is gv1.Columns(colTAX5_Rate) Then
                        OpenRate5CodeList(False)
                    End If

                    'UpdateCurrentRow()
                    'End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub
    Sub ClearForGroupTaxCodeFinder()
        'gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value = ""
        gv1.CurrentRow.Cells(colTAX_GROUP_Desc).Value = ""
        gv1.CurrentRow.Cells(colTAX1_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX1_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX2_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX2_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX3_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX3_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX4_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX4_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX5_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX5_Rate).Value = 0
    End Sub
    Sub ClearAllCurrentRowFinder()
        gv1.CurrentRow.Cells(colItem_Desc).Value = ""
        gv1.CurrentRow.Cells(colHSN_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value = ""
        gv1.CurrentRow.Cells(colTAX_GROUP_Desc).Value = ""
        gv1.CurrentRow.Cells(colTAX1_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX1_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX2_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX2_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX3_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX3_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX4_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX4_Rate).Value = 0
        gv1.CurrentRow.Cells(colTAX5_Code).Value = ""
        gv1.CurrentRow.Cells(colTAX5_Rate).Value = 0
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Try
            ClearAllCurrentRowFinder()
            Dim qry As String = "Select Item_Code as Code, Item_Desc as Descriiption , HSN_code From TSPL_ITEM_MASTER"
            gv1.CurrentRow.Cells(colItem_Code).Value = clsCommon.ShowSelectForm("ItemFnder@Item_Code", qry, "Code", " IsTaxable =1 ", clsCommon.myCstr(gv1.CurrentRow.Cells(colItem_Code).Value), "", isButtonClick)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colItem_Code).Value) > 0 Then
                ADDNewRows()
                gv1.CurrentRow.Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gv1.CurrentRow.Cells(colItem_Code).Value), Nothing)
                gv1.CurrentRow.Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(gv1.CurrentRow.Cells(colItem_Code).Value), Nothing)
            End If

        Catch ex As Exception
            gv1.CurrentRow.Cells(colItem_Code).Value = ""
            gv1.CurrentRow.Cells(colItem_Desc).Value = ""
            gv1.CurrentRow.Cells(colHSN_Code).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub OpenTaxGroupList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(ddlTransType.SelectedValue)) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Transaction Type")
                Exit Sub
            End If
            ClearForGroupTaxCodeFinder()
            Dim qry As String = "select Tax_Group_Code as Code , Tax_Group_Desc as Descriiption from TSPL_TAX_GROUP_MASTER"
            gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value = clsCommon.ShowSelectForm("ItemFnder@GroupCode", qry, "Code", "Tax_Group_Type ='" + ddlTransType.SelectedValue + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value), "", isButtonClick)
            gv1.CurrentRow.Cells(colTAX_GROUP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "'"))

            qry = "select Tax_Code from TSPL_TAX_GROUP_DETAILS where Tax_Group_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and Tax_Group_Type = '" + ddlTransType.SelectedValue + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                Dim count As Integer = dt.Rows.Count
                For i As Integer = 1 To count
                    Dim colTax As String = "colTAX" + clsCommon.myCstr(i) + "_Code"
                    Dim colRat As String = "colTAX" + clsCommon.myCstr(i) + "_Rate"
                    gv1.CurrentRow.Cells(colTax).Value = clsCommon.myCstr(dt.Rows(i - 1)("Tax_Code"))
                    gv1.CurrentRow.Cells(colRat).Value = 0
                Next

            End If

            'qry = "select  TSPL_TAX_GROUP_DETAILS.Tax_Code , max( isnull(TSPL_TAX_RATES.Tax_Rate,0)) as Tax_Rate from TSPL_TAX_GROUP_DETAILS  left outer join TSPL_TAX_RATES  on TSPL_TAX_RATES.Tax_Code = TSPL_TAX_GROUP_DETAILS.Tax_Code and TSPL_TAX_RATES.Tax_Type = TSPL_TAX_GROUP_DETAILS.Tax_Group_Type   where TSPL_TAX_GROUP_DETAILS.Tax_Group_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and TSPL_TAX_GROUP_DETAILS.Tax_Group_Type = '" + ddlTransType.SelectedValue + "' group by TSPL_TAX_GROUP_DETAILS.Tax_Code"
            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'If dt.Rows.Count > 0 Then
            '    Dim count As Integer = dt.Rows.Count
            '    For i As Integer = 1 To count
            '        Dim colTax As String = "colTAX" + clsCommon.myCstr(i) + "_Code"
            '        Dim colRat As String = "colTAX" + clsCommon.myCstr(i) + "_Rate"
            '        gv1.CurrentRow.Cells(colTax).Value = clsCommon.myCstr(dt.Rows(i - 1)("Tax_Code"))
            '        gv1.CurrentRow.Cells(colRat).Value = clsCommon.myCdbl(dt.Rows(i - 1)("Tax_Rate"))
            '    Next

            'End If
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value = ""
            gv1.CurrentRow.Cells(colTAX_GROUP_Desc).Value = ""
            gv1.CurrentRow.Cells(colTAX1_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX1_Rate).Value = 0
            gv1.CurrentRow.Cells(colTAX2_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX2_Rate).Value = 0
            gv1.CurrentRow.Cells(colTAX3_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX3_Rate).Value = 0
            gv1.CurrentRow.Cells(colTAX4_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX4_Rate).Value = 0
            gv1.CurrentRow.Cells(colTAX5_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX5_Rate).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenTax1CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Descriiption from TSPL_TAX_GROUP_DETAILS"
            gv1.CurrentRow.Cells(colTAX1_Code).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Code", " Tax_Group_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and Tax_Group_Type ='" + ddlTransType.SelectedValue + "' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX1_Code).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX1_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX1_Rate).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenRate1CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX1_Code).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Tax1 Code  Not Avilable", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Type as Type,Tax_Rate as Rate  from TSPL_TAX_RATES "
            gv1.CurrentRow.Cells(colTAX1_Rate).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Rate", " Tax_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX1_Code).Value) + "' and Tax_Type ='" + ddlTransType.SelectedValue + "' ", clsCommon.myCdbl(gv1.CurrentRow.Cells(colTAX1_Rate).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX1_Rate).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub OpenRate2CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX2_Code).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Tax2 Code  Not Avilable", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Type as Type,Tax_Rate as Rate  from TSPL_TAX_RATES "
            gv1.CurrentRow.Cells(colTAX2_Rate).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Rate", " Tax_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX2_Code).Value) + "' and Tax_Type ='" + ddlTransType.SelectedValue + "' ", clsCommon.myCdbl(gv1.CurrentRow.Cells(colTAX2_Rate).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX2_Rate).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub OpenRate3CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX3_Code).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Tax3 Code  Not Avilable", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Type as Type,Tax_Rate as Rate  from TSPL_TAX_RATES "
            gv1.CurrentRow.Cells(colTAX3_Rate).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Rate", " Tax_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX3_Code).Value) + "' and Tax_Type ='" + ddlTransType.SelectedValue + "' ", clsCommon.myCdbl(gv1.CurrentRow.Cells(colTAX3_Rate).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX3_Rate).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenRate4CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX4_Code).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Tax4 Code  Not Avilable", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Type as Type,Tax_Rate as Rate  from TSPL_TAX_RATES "
            gv1.CurrentRow.Cells(colTAX4_Rate).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Rate", " Tax_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX4_Code).Value) + "' and Tax_Type ='" + ddlTransType.SelectedValue + "' ", clsCommon.myCdbl(gv1.CurrentRow.Cells(colTAX4_Rate).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX4_Rate).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenRate5CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX4_Code).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Tax5 Code  Not Avilable", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Type as Type,Tax_Rate as Rate  from TSPL_TAX_RATES "
            gv1.CurrentRow.Cells(colTAX5_Rate).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Rate", " Tax_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX5_Code).Value) + "' and Tax_Type ='" + ddlTransType.SelectedValue + "' ", clsCommon.myCdbl(gv1.CurrentRow.Cells(colTAX5_Rate).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX5_Rate).Value = 0
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenTax2CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Descriiption from TSPL_TAX_GROUP_DETAILS"
            gv1.CurrentRow.Cells(colTAX2_Code).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Code", "Tax_Group_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and Tax_Group_Type ='" + ddlTransType.SelectedValue + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX2_Code).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX2_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX2_Rate).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenTax3CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Descriiption from TSPL_TAX_GROUP_DETAILS"
            gv1.CurrentRow.Cells(colTAX3_Code).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Code", "Tax_Group_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and Tax_Group_Type ='" + ddlTransType.SelectedValue + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX3_Code).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX3_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX3_Rate).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenTax4CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Descriiption from TSPL_TAX_GROUP_DETAILS"
            gv1.CurrentRow.Cells(colTAX4_Code).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Code", "Tax_Group_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and Tax_Group_Type ='" + ddlTransType.SelectedValue + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX4_Code).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX4_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX4_Rate).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Sub OpenTax5CodeList(ByVal isButtonClick As Boolean)
        Try
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value)) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select Tax Group Code", Me.Text)
                Exit Sub
            End If
            Dim qry As String = "select Tax_Code as Code,Tax_Code_Desc as Descriiption from TSPL_TAX_GROUP_DETAILS"
            gv1.CurrentRow.Cells(colTAX5_Code).Value = clsCommon.ShowSelectForm("ItemFnder@TaxCode", qry, "Code", "Tax_Group_Code= '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "' and Tax_Group_Type ='" + ddlTransType.SelectedValue + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX5_Code).Value), "", isButtonClick)
        Catch ex As Exception
            gv1.CurrentRow.Cells(colTAX5_Code).Value = ""
            gv1.CurrentRow.Cells(colTAX5_Rate).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
    End Sub
    Sub ADDNewRows()
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
        gv1.Rows.AddNew()
    End Sub

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

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        SaveData()
    End Sub

    Private Sub FrmItemWiseTax_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnReset.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnAdd.Enabled Then
            btnAdd.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btnDelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("Delete the current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsItemWiseTax.DeleteData(fndCode.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            isInsideLoadData = True
            Reset()
            Dim obj As clsItemWiseTax = clsItemWiseTax.GetData(strCode, NavType, Nothing)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.HCODE) > 0 Then
                isNewEntry = False

                btnDelete.Enabled = True

                btnAdd.Enabled = True


                fndCode.Value = obj.HCODE
                dtpToDate.Value = obj.DOC_DATE
                ddlTransType.SelectedValue = obj.Type
                txtDescription.Text = obj.Description
                LoadDetailData(obj.ArrGroup, False)
                gv1.Rows.AddNew()
                fndCode.MyReadOnly = True
                btnAdd.Text = "Update"
                ddlTransType.Enabled = False
                lblPending.Status = obj.Status
                If obj.Status = 1 Then
                    btnAdd.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnAdd.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Sub LoadDetailData(ByVal Arr As List(Of clsItemWiseTaxGroup), ByVal isAddMasterCode As Boolean)
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            For Each objtr As clsItemWiseTaxGroup In Arr
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)

                gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objtr.SNO
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = objtr.Item_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(objtr.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(objtr.Item_Code, Nothing)
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_CODE).Value = objtr.Tax_Group_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code = '" + clsCommon.myCstr(gv1.CurrentRow.Cells(colTAX_GROUP_CODE).Value) + "'"))

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Code).Value = objtr.Tax1_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Rate).Value = objtr.TAX1_Rate

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Code).Value = objtr.Tax2_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Rate).Value = objtr.TAX2_Rate

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Code).Value = objtr.Tax3_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Rate).Value = objtr.TAX3_Rate

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Code).Value = objtr.Tax4_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Rate).Value = objtr.TAX4_Rate

                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Code).Value = objtr.Tax5_Code
                gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Rate).Value = objtr.TAX5_Rate
            Next

        End If
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        LoadData(fndCode.Value, NavType)
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating

        Dim qry As String = " select HCODE as Code,CONVERT(varchar, DOC_DATE,103) as [Start Date], [Type],Case when  Status=0 then 'Pending' else 'Approved' end as Status from TSPL_ITEM_WISE_TAX " '" select doc_code as Code, doc_date as StartDate,Tax_Group_Type as Type, Created_By as CreatedBy,Created_Date as CreatedDate from TSPL_ITEM_WISE_TAX_head "


        Dim whrcls As String = ""
        fndCode.Value = clsCommon.ShowSelectForm("FNDDoc", qry, "Code", whrcls, fndCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(fndCode.Value) > 0 Then
            LoadData(fndCode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub

    Private Sub ddlTransType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles ddlTransType.SelectedIndexChanged
        'LoadBlankGrid1()
        'gv1.Rows.AddNew()

        'If gv1.Rows.Count > 0 Then
        '    ddlTransType.Enabled = False
        'Else
        '    ddlTransType.Enabled = True
        'End If

    End Sub



    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Try
            'Dim query As String = " select  xxxx.TAX_Group_SNo , max (xxxx.HCODE) as HCODE,max(Convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103)) as DOC_DATE, max(TSPL_ITEM_WISE_TAX.Type) as Type , xxxx.Item_Code,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO,convert(varchar,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO)+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx left outer join TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX.HCODE = xxxx.HCODE  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = xxxx.Item_Code   "
            Dim query As String = " select   max (xxxx.HCODE) as Doc_Code,max(Convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103)) as DOC_DATE, (TSPL_ITEM_WISE_TAX.Type) as Type,max(case when TSPL_ITEM_WISE_TAX.Status =1 then 'Y' else 'N' end ) as Posted , xxxx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc,max(TSPL_ITEM_MASTER.HSN_Code) as HSN_Code ,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as Tax_Group_Desc,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO,convert(varchar,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO)+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx left outer join TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX.HCODE = xxxx.HCODE  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = xxxx.Item_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code = xxxx.Tax_Group_Code   "
            'Dim query As String = " select   max (xxxx.HCODE) as Doc_Code,max(Convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103)) as DOC_DATE, (TSPL_ITEM_WISE_TAX.Type) as Type,max(case when TSPL_ITEM_WISE_TAX.Status =1 then 'Y' else 'N' end ) as Posted , xxxx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc,max(TSPL_ITEM_MASTER.HSN_Code) as HSN_Code ,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as Tax_Group_Desc,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,convert (nvarchar, DENSE_RANK() OVER (PARTITION BY TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE ORDER BY TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority)) as SNO,Convert (nvarchar,DENSE_RANK() OVER (PARTITION BY TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE ORDER BY TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority))+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx left outer join TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX.HCODE = xxxx.HCODE  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = xxxx.Item_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code = xxxx.Tax_Group_Code   "
            transportSql.ExporttoExcel(query, "  group by xxxx.Item_Code, Type,xxxx.Tax_Group_Code ", " Doc_Code,Tax_Group_Code asc  ", Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Item Tax", Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItem3.Click
        Dim isError As Boolean = False
        Dim isPosted As String = ""
        gv1.Rows.Clear()
        gv1.Rows.AddNew()
        isImport = True
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        Try
            ' "Doc_Code","DOC_DATE","Type","Item_Code","Item_Desc","HSN_Code","Tax_Group_Code","Tax1_Code","TAX1_Rate","Tax2_Code","TAX2_Rate","Tax3_Code","TAX3_Rate","Tax4_Code","TAX4_Rate","Tax5_Code","TAX5_Rate"
            If transportSql.importExcel(dgv, "Doc_Code", "DOC_DATE", "Type", "Posted", "Item_Code", "Item_Desc", "HSN_Code", "Tax_Group_Code", "Tax_Group_Desc", "Tax1_Code", "TAX1_Rate", "Tax2_Code", "TAX2_Rate", "Tax3_Code", "TAX3_Rate", "Tax4_Code", "TAX4_Rate", "Tax5_Code", "TAX5_Rate") Then

                Try
                    Dim lineNo As Integer = 0
                    Dim chkDiffDocCode As Integer = 0
                    Dim chkDiffDocDate As Integer = 0
                    Dim chkDiffType As Integer = 0
                    Dim prvDocCode As String = ""
                    Dim prvDocDate As String = ""
                    Dim prvType As String = ""
                    Dim prvPosted As String = ""
                    For Each grow As GridViewRowInfo In dgv.Rows
                        lineNo = lineNo + 1
                        Dim Doc_Code As String = ""
                        Dim DOC_DATE As String = ""
                        Dim Type As String = ""
                        Dim Item_Code As String = ""
                        Dim Tax_Group_Code As String = ""
                        Dim Tax1_Code As String = ""
                        Dim TAX1_Rate As Double = 0.0
                        Dim Tax2_Code As String = ""
                        Dim TAX2_Rate As Double = 0.0
                        Dim Tax3_Code As String = ""
                        Dim TAX3_Rate As Double = 0.0
                        Dim Tax4_Code As String = ""
                        Dim TAX4_Rate As Double = 0.0
                        Dim Tax5_Code As String = ""
                        Dim TAX5_Rate As Double = 0.0

                        If clsCommon.myLen(fndCode.Value) > 0 Then
                            Dim isPostedDoc As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_ITEM_WISE_TAX where HCODE = '" + fndCode.Value + "' and Status =1 "))
                            If isPostedDoc > 0 Then
                                common.clsCommon.MyMessageBoxShow(" Screen Document : '" + fndCode.Value + "'  already Posted.You Can't Import excel for this document.")
                                isError = True
                                Exit Sub
                            End If

                        End If

                        If clsCommon.myLen(grow.Cells("Doc_Code").Value) > 0 Then
                            If clsCommon.myLen(fndCode.Value) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Doc_Code").Value), clsCommon.myCstr(fndCode.Value)) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Screen and Import  Document Code should be same at line no '" + clsCommon.myCstr(lineNo) + "' ")
                                    isError = True
                                    Exit Sub
                                End If
                            End If
                        End If
                        If clsCommon.myLen(grow.Cells("DOC_DATE").Value) > 0 Then
                            If clsCommon.myLen(dtpToDate.Value) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("DOC_DATE").Value), clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Screen and Import  Start Date should be same at line no '" + clsCommon.myCstr(lineNo) + "' ")
                                    isError = True
                                    Exit Sub
                                End If
                            End If
                        End If

                        If clsCommon.myLen(grow.Cells("Type").Value) > 0 Then
                            If clsCommon.myLen(ddlTransType.Text) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Type").Value), clsCommon.myCstr(ddlTransType.SelectedValue)) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Screen and Import Type should be same at line no '" + clsCommon.myCstr(lineNo) + "' ")
                                    isError = True
                                    Exit Sub
                                End If
                            End If
                        End If

                        If clsCommon.myLen(prvDocCode) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Doc_Code").Value), clsCommon.myCstr(prvDocCode)) <> CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "All Document No in Excel Should be Same. Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                isError = True
                                Exit Sub
                            End If
                        End If

                        If clsCommon.myLen(prvDocDate) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("DOC_DATE").Value), clsCommon.myCstr(prvDocDate)) <> CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "All Document Date in Excel Should be Same. Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                isError = True
                                Exit Sub
                            End If
                        End If

                        If clsCommon.myLen(prvType) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Type").Value), clsCommon.myCstr(prvType)) <> CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "All Type in Excel Should be Same. Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                isError = True
                                Exit Sub
                            End If
                        End If

                        If clsCommon.myLen(prvPosted) > 0 Then
                            If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Posted").Value), clsCommon.myCstr(prvPosted)) <> CompairStringResult.Equal Then
                                common.clsCommon.MyMessageBoxShow(Me, "All Posted in Excel Should be Same . Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                isError = True
                                Exit Sub
                            End If
                        End If

                        If clsCommon.myLen(grow.Cells("Doc_Code").Value) > 0 Then
                            prvDocCode = grow.Cells("Doc_Code").Value
                        End If
                        If clsCommon.myLen(grow.Cells("DOC_DATE").Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Document Date cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                            isError = True
                            Exit Sub
                        End If
                        If clsCommon.myLen(grow.Cells("DOC_DATE").Value) > 0 Then
                            prvDocDate = grow.Cells("DOC_DATE").Value
                        End If
                        If clsCommon.myLen(grow.Cells("Type").Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Document Type cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "' ")
                            isError = True
                            Exit Sub
                        End If

                        If (clsCommon.myCstr(grow.Cells("Type").Value) = "P" Or clsCommon.myCstr(grow.Cells("Type").Value) = "S" Or clsCommon.myCstr(grow.Cells("Type").Value) = "T") Then
                        Else
                            common.clsCommon.MyMessageBoxShow(Me, "Type Should be 'P' or 'S' or 'T' at line :'" + clsCommon.myCstr(lineNo) + "'")
                            'Throw New Exception("Type Should be 'Y' or 'N' at line :'" + clsCommon.myCstr(lineNo) + "'")
                            isError = True
                        End If
                        If clsCommon.myLen(grow.Cells("Type").Value) > 0 Then
                            prvType = grow.Cells("Type").Value
                        End If
                        If clsCommon.myLen(grow.Cells("Posted").Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Posted Value cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                            isError = True
                            Exit Sub
                        End If
                        If (clsCommon.myCstr(grow.Cells("Posted").Value) = "Y" Or clsCommon.myCstr(grow.Cells("Posted").Value) = "N") Then
                        Else
                            Throw New Exception("Posted Value Should be 'Y' or 'N' at line :'" + clsCommon.myCstr(lineNo) + "'")
                            isError = True
                        End If

                        If clsCommon.myLen(grow.Cells("Posted").Value) > 0 Then
                            prvPosted = grow.Cells("Posted").Value
                            isPosted = grow.Cells("Posted").Value
                        End If
                        '*********************** Comment Part ********************
                        If clsCommon.myLen(grow.Cells("Item_Code").Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Item Code cannot be blank.", Me.Text)
                            isError = True
                            Exit Sub
                        ElseIf clsCommon.myLen(grow.Cells("Item_Code").Value) > 0 Then
                            Dim isValidItemcode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Item_code)  from TSPL_ITEM_MASTER  where Item_Code ='" + grow.Cells("Item_Code").Value.ToString() + "'"))
                            If isValidItemcode <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Invalid Item code.", Me.Text)
                                isError = True
                                Exit Sub
                            End If
                        End If


                        '*********************** End Comment Part ********************
                        Item_Code = grow.Cells("Item_Code").Value.ToString()

                        '***********************  Comment Part ********************
                        If clsCommon.myLen(grow.Cells("Tax_Group_Code").Value) <= 0 Then
                            common.clsCommon.MyMessageBoxShow(Me, "Tax Group Code cannot be blank.", Me.Text)
                            isError = True
                            Exit Sub
                        ElseIf clsCommon.myLen(grow.Cells("Tax_Group_Code").Value) > 0 Then
                            Dim isValidGroupcode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Tax_Group_Code)  from TSPL_TAX_GROUP_MASTER  where Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                            If isValidGroupcode <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Invalid Item code.", Me.Text)
                                isError = True
                                Exit Sub
                            End If
                        End If

                        '*********************** End Comment Part ********************
                        Tax_Group_Code = grow.Cells("Tax_Group_Code").Value.ToString()


                        'If clsCommon.myLen(grow.Cells("Tax1_Code").Value) > 0 Then
                        '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax1_Code").Value.ToString() + "' and Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                        '    If isValidTaxCode <= 0 Then
                        '        common.clsCommon.MyMessageBoxShow("Invalid Tax1 Code.")

                        '        Exit Sub
                        '    End If
                        'End If
                        Tax1_Code = grow.Cells("Tax1_Code").Value.ToString()
                        TAX1_Rate = clsCommon.myCdbl(grow.Cells("TAX1_Rate").Value)

                        ' tax code 2


                        'If clsCommon.myLen(grow.Cells("Tax2_Code").Value) > 0 Then
                        '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax2_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                        '    If isValidTaxCode <= 0 Then
                        '        common.clsCommon.MyMessageBoxShow("Invalid Tax3 Code.")

                        '        Exit Sub
                        '    End If
                        'End If
                        Tax2_Code = grow.Cells("Tax2_Code").Value.ToString()
                        TAX2_Rate = clsCommon.myCdbl(grow.Cells("TAX2_Rate").Value)
                        ' Tax 3


                        'If clsCommon.myLen(grow.Cells("Tax3_Code").Value) > 0 Then
                        '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax3_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                        '    If isValidTaxCode <= 0 Then
                        '        common.clsCommon.MyMessageBoxShow("Invalid Tax3 Code.")

                        '        Exit Sub
                        '    End If

                        'End If
                        Tax3_Code = grow.Cells("Tax3_Code").Value.ToString()
                        TAX3_Rate = clsCommon.myCdbl(grow.Cells("TAX3_Rate").Value)
                        ' Tax 4

                        'If clsCommon.myLen(grow.Cells("Tax4_Code").Value) > 0 Then
                        '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax4_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                        '    If isValidTaxCode <= 0 Then
                        '        common.clsCommon.MyMessageBoxShow("Invalid Tax4 Code.")

                        '        Exit Sub
                        '    End If


                        'End If
                        Tax4_Code = grow.Cells("Tax4_Code").Value.ToString()
                        TAX4_Rate = clsCommon.myCdbl(grow.Cells("TAX4_Rate").Value)
                        ' Tax 5

                        'If clsCommon.myLen(grow.Cells("Tax5_Code").Value) > 0 Then
                        '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax5_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                        '    If isValidTaxCode <= 0 Then
                        '        common.clsCommon.MyMessageBoxShow("Invalid Tax5 Code.")

                        '        Exit Sub
                        '    End If


                        'End If
                        Tax5_Code = grow.Cells("Tax5_Code").Value.ToString()
                        TAX5_Rate = clsCommon.myCdbl(grow.Cells("TAX5_Rate").Value)
                        '**************************************************************************************************************


                        '==========================
                        'gv1.Rows.AddNew()

                        gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = lineNo
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(Item_Code, Nothing)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_CODE).Value = Tax_Group_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code = '" + Tax_Group_Code + "'"))

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Code).Value = Tax1_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Rate).Value = TAX1_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Code).Value = Tax2_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Rate).Value = TAX2_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Code).Value = Tax3_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Rate).Value = TAX3_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Code).Value = Tax4_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Rate).Value = TAX4_Rate

                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Code).Value = Tax5_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Rate).Value = TAX5_Rate

                        gv1.Rows.AddNew()
                        '**************************************************************************************************************

                    Next
                  
                Catch ex As Exception

                End Try
            End If
            isImport = False
            'clsCommon.MyMessageBoxShow("Please Click on Save Button for Save Data.", Me.Text)
            If isError = True Then
                gv1.Rows.Clear()
                gv1.Rows.AddNew()
            Else
                If isPosted = "Y" Then
                    btnAdd.PerformClick()
                    btnPost.PerformClick()
                Else
                    btnAdd.PerformClick()
                    clsCommon.MyMessageBoxShow(Me, "Please Click on Post Button for Post Data.", Me.Text)
                End If


            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(dgv)
            isImport = False
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Try
            If clsCommon.myLen(fndCode.Value) > 0 Then
                Dim query As String = " select   max (xxxx.HCODE) as Doc_Code,max(Convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103)) as DOC_DATE, (TSPL_ITEM_WISE_TAX.Type) as Type ,max(case when TSPL_ITEM_WISE_TAX.Status =1 then 'Y' else 'N' end ) as Posted, xxxx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc,max(TSPL_ITEM_MASTER.HSN_Code) as HSN_Code ,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as Tax_Group_Desc,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO,convert(varchar,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO)+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx left outer join TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX.HCODE = xxxx.HCODE  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = xxxx.Item_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code = xxxx.Tax_Group_Code  "
                ' Dim query As String = " select   max (xxxx.HCODE) as Doc_Code,max(Convert(varchar, TSPL_ITEM_WISE_TAX.DOC_DATE,103)) as DOC_DATE, (TSPL_ITEM_WISE_TAX.Type) as Type ,max(case when TSPL_ITEM_WISE_TAX.Status =1 then 'Y' else 'N' end ) as Posted, xxxx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc,max(TSPL_ITEM_MASTER.HSN_Code) as HSN_Code ,max(xxxx.Tax_Group_Code) as Tax_Group_Code,max (TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as Tax_Group_Desc,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,convert (nvarchar, DENSE_RANK() OVER (PARTITION BY TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE ORDER BY TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority)) as SNO,convert(varchar,Convert (nvarchar,DENSE_RANK() OVER (PARTITION BY TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE ORDER BY TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority))+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx left outer join TSPL_ITEM_WISE_TAX on TSPL_ITEM_WISE_TAX.HCODE = xxxx.HCODE  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = xxxx.Item_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.Tax_Group_Code = xxxx.Tax_Group_Code  "
                ' Dim query As String = " select max (xxxx.HCODE) as HCODE,max(Convert (varchar,TSPL_ITEM_WISE_TAX.Doc_Date,103)) as Doc_Date,max(TSPL_ITEM_WISE_TAX.Type) as Type,max(Case when TSPL_ITEM_WISE_TAX.Status = 1 then 'Y'else 'N' end) as Status, xxxx.Item_Code, max(TSPL_ITEM_MASTER.Item_Desc) as Item_Desc,Max(TSPL_ITEM_MASTER.HSN_Code) as HSN_COde,max(xxxx.Tax_Group_Code) as Tax_Group_Code,Max(TSPL_TAX_GROUP_MASTER.Tax_Group_Desc) as Tax_Group_Desc,max (xxxx.[1]) as Tax1_Code,max (xxxx.[1T]) as TAX1_Rate , max (xxxx.[2]) as Tax2_Code,max (xxxx.[2T]) as TAX2_Rate ,max (xxxx.[3]) as Tax3_Code,max (xxxx.[3T]) as TAX3_Rate ,max (xxxx.[4]) as Tax4_Code,max (xxxx.[4T]) as TAX4_Rate ,max (xxxx.[5]) as Tax5_Code,max (xxxx.[5T]) as TAX5_Rate from ( select * from ( select * from ( select TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE, TSPL_ITEM_WISE_TAX_GROUP.SNO as TAX_Group_SNo,  TSPL_ITEM_WISE_TAX_GROUP.item_code,TSPL_ITEM_WISE_TAX_GROUP.Tax_Group_Code,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO,convert(varchar,TSPL_ITEM_WISE_TAX_AUTHORITY.SNO)+'T' as SNO2, TSPL_ITEM_WISE_TAX_AUTHORITY. DCODE,TSPL_ITEM_WISE_TAX_AUTHORITY.Tax_Authority,TSPL_ITEM_WISE_TAX_AUTHORITY.TAX_Rate  from TSPL_ITEM_WISE_TAX_AUTHORITY  inner join TSPL_ITEM_WISE_TAX_GROUP on TSPL_ITEM_WISE_TAX_GROUP.DCODE=TSPL_ITEM_WISE_TAX_AUTHORITY.DCODE and TSPL_ITEM_WISE_TAX_GROUP.HCODE =TSPL_ITEM_WISE_TAX_AUTHORITY.HCODE  )src pivot ( max(Tax_Authority) for SNO in ([1], [2],[3],[4],[5]) ) piv ) xxx pivot  ( max(TAX_Rate)   for SNO2 in ([1T], [2T],[3T],[4T],[5T])  ) piv2 ) xxxx left outer join TSPL_ITEM_WISE_TAX  on XXXX.HCODE  = TSPL_ITEM_WISE_TAX.HCODE left outer Join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code = XXXX.ITem_Code left outer join TSPL_TAX_GROUP_MASTER on TSPL_TAX_GROUP_MASTER.TAX_Group_Code =XXXX.Tax_Group_Code "
                ListImpExpColumnsMandatory = New List(Of String)({"Doc_Code", "DOC_DATE", "Type", "Posted", "Item_Code", "Tax_Group_Code"})
                ListImpExpColumnsSuperMandatory = New List(Of String)({"Doc_Code"})
                transportSql.ExporttoExcel(query, " and xxxx.HCODE = '" + fndCode.Value + "'  group by xxxx.Item_Code, Type,xxxx.Tax_Group_Code ", " Doc_Code,Tax_Group_Code asc  ", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
            Else
                clsCommon.MyMessageBoxShow("Fist Select Document", "Item Tax")
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Item Tax")
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Function SaveData() As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New clsItemWiseTax()
                Dim objGroup As New clsItemWiseTaxGroup()
                If isNewEntry = True Then
                    If ddlTransType.SelectedValue = "P" Then
                        obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemPurchaseTax, "")
                    ElseIf ddlTransType.SelectedValue = "S" Then
                        obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemSaleTax, "")
                    ElseIf ddlTransType.SelectedValue = "T" Then
                        obj.HCODE = clsERPFuncationality.GetNextCode(Nothing, clsCommon.GETSERVERDATE(), clsDocType.ItemWiseTax, clsDocTransactionType.ItemTransferTax, "")
                    End If
                    If clsCommon.myLen(obj.HCODE) <= 0 Then
                        Throw New Exception("Error in code generation")
                    End If
                Else
                    obj.HCODE = fndCode.Value
                End If
                obj.DOC_DATE = dtpToDate.Value
                obj.Type = ddlTransType.SelectedValue
                obj.Description = txtDescription.Text
                obj.ArrGroup = New List(Of clsItemWiseTaxGroup)
                obj.ArrAuth = New List(Of clsItemWiseTaxAuthority)
                For ii As Integer = 0 To gv1.RowCount - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colItem_Code).Value) > 0 Then
                        Dim objtr As New clsItemWiseTaxGroup
                        objtr.HCODE = obj.HCODE
                        objtr.SNO = ii + 1
                        objtr.DCODE = clsCommon.myCstr(objtr.HCODE) + "-" + clsCommon.myCstr(objtr.SNO)
                        objtr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colItem_Code).Value)
                        objtr.Tax_Group_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX_GROUP_CODE).Value)
                        'Dim objAuth As New clsItemWiseTaxAuthority
                        'objAuth.HCODE = fndCode.Value
                        'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX1_Code).Value)
                        'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX1_Rate).Value)
                        'obj.ArrAuth.Add(objAuth)
                        'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX2_Code).Value)
                        'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX2_Rate).Value)
                        'obj.ArrAuth.Add(objAuth)
                        'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX3_Code).Value)
                        'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX3_Rate).Value)
                        'obj.ArrAuth.Add(objAuth)
                        'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX4_Code).Value)
                        'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX4_Rate).Value)
                        'obj.ArrAuth.Add(objAuth)
                        'objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX5_Code).Value)
                        'objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX5_Rate).Value)
                        'obj.ArrAuth.Add(objAuth)


                        'objtr.Tax1_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX1_Code).Value)
                        'objtr.TAX1_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX1_Rate).Value)

                        'objtr.Tax2_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX2_Code).Value)
                        'objtr.TAX2_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX2_Rate).Value)

                        'objtr.Tax3_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX3_Code).Value)
                        'objtr.TAX3_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX3_Rate).Value)

                        'objtr.Tax4_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX4_Code).Value)
                        'objtr.TAX4_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX4_Rate).Value)

                        'objtr.Tax5_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colTAX5_Code).Value)
                        'objtr.TAX5_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(colTAX5_Rate).Value)
                        For iii As Integer = 1 To 5
                            Dim cloTax As String = "colTAX" + clsCommon.myCstr(iii) + "_Code"
                            Dim cloRat As String = "colTAX" + clsCommon.myCstr(iii) + "_Rate"

                            Dim objAuth As New clsItemWiseTaxAuthority
                            objAuth.HCODE = obj.HCODE
                            objAuth.DCODE = objtr.DCODE
                            objAuth.SNO = iii
                            objAuth.DDCODE = clsCommon.myCstr(objtr.DCODE) + "-" + clsCommon.myCstr(objAuth.SNO)
                            objAuth.Tax_Authority = clsCommon.myCstr(gv1.Rows(ii).Cells(cloTax).Value)
                            objAuth.TAX_Rate = clsCommon.myCdbl(gv1.Rows(ii).Cells(cloRat).Value)
                            If clsCommon.myLen(objAuth.Tax_Authority) > 0 Then
                                obj.ArrAuth.Add(objAuth)
                            End If
                        Next

                        obj.ArrGroup.Add(objtr)

                    End If
                Next
           
                If obj.SaveData(obj, isNewEntry) Then
                    If chkPostClick = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.HCODE, NavigatorType.Current)
                    btnAdd.Text = "Update"
                    Return True
                End If
            Else
                fndCode.MyReadOnly = False
                btnDelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return False
    End Function
    Sub PostData()
        Try
            chkPostClick = True
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("Code not found to post")
            End If
            If (myMessages.postConfirm()) Then
                If SaveData() Then
                    If clsItemWiseTax.PostData(fndCode.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                        LoadData(fndCode.Value, NavigatorType.Current)
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text, MessageBoxButtons.OK, RadMessageIcon.Info)
        Finally
            chkPostClick = False
        End Try
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click

        Try
            If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If

                If clsItemWiseTax.ReverseAndUnpost(fndCode.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate")
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(fndCode.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception

            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.fndCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, Nothing)
    End Function

    Private Sub RadMenuItem5_Click(sender As Object, e As EventArgs) Handles RadMenuItem5.Click
        Try
            Dim isError As Boolean = False
            Dim isPosted As String = ""
            gv1.Rows.Clear()
            gv1.Rows.AddNew()
            isImport = True
            Dim dgv As New RadGridView
            Me.Controls.Add(dgv)
            Try
                ' "Doc_Code","DOC_DATE","Type","Item_Code","Item_Desc","HSN_Code","Tax_Group_Code","Tax1_Code","TAX1_Rate","Tax2_Code","TAX2_Rate","Tax3_Code","TAX3_Rate","Tax4_Code","TAX4_Rate","Tax5_Code","TAX5_Rate"
                If transportSql.importExcel(dgv, "Doc_Code", "DOC_DATE", "Type", "Posted", "HSN_Code", "Tax_Group_Code", "Tax_Group_Desc", "Tax1_Code", "TAX1_Rate", "Tax2_Code", "TAX2_Rate", "Tax3_Code", "TAX3_Rate", "Tax4_Code", "TAX4_Rate", "Tax5_Code", "TAX5_Rate") Then

                    Try
                        Dim lineNo As Integer = 0
                        Dim chkDiffDocCode As Integer = 0
                        Dim chkDiffDocDate As Integer = 0
                        Dim chkDiffType As Integer = 0
                        Dim prvDocCode As String = ""
                        Dim prvDocDate As String = ""
                        Dim prvType As String = ""
                        Dim prvPosted As String = ""
                        For Each grow As GridViewRowInfo In dgv.Rows
                            'lineNo = lineNo + 1
                            Dim Doc_Code As String = ""
                            Dim DOC_DATE As String = ""
                            Dim Type As String = ""
                            Dim Item_Code As String = ""
                            Dim Item_Desc As String = ""
                            Dim HSN_Code As String = ""
                            Dim Tax_Group_Code As String = ""
                            Dim Tax1_Code As String = ""
                            Dim TAX1_Rate As Double = 0.0
                            Dim Tax2_Code As String = ""
                            Dim TAX2_Rate As Double = 0.0
                            Dim Tax3_Code As String = ""
                            Dim TAX3_Rate As Double = 0.0
                            Dim Tax4_Code As String = ""
                            Dim TAX4_Rate As Double = 0.0
                            Dim Tax5_Code As String = ""
                            Dim TAX5_Rate As Double = 0.0

                            If clsCommon.myLen(fndCode.Value) > 0 Then
                                Dim isPostedDoc As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_ITEM_WISE_TAX where HCODE = '" + fndCode.Value + "' and Status =1 "))
                                If isPostedDoc > 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, " Screen Document : '" + fndCode.Value + "'  already Posted.You Can't Import excel for this document.")
                                    isError = True
                                    Exit Sub
                                End If

                            End If

                            If clsCommon.myLen(grow.Cells("Doc_Code").Value) > 0 Then
                                If clsCommon.myLen(fndCode.Value) > 0 Then
                                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Doc_Code").Value), clsCommon.myCstr(fndCode.Value)) <> CompairStringResult.Equal Then
                                        common.clsCommon.MyMessageBoxShow(Me, "Screen and Import  Document Code should be same at line no '" + clsCommon.myCstr(lineNo) + "' ")
                                        isError = True
                                        Exit Sub
                                    End If
                                End If
                            End If
                            If clsCommon.myLen(grow.Cells("DOC_DATE").Value) > 0 Then
                                If clsCommon.myLen(dtpToDate.Value) > 0 Then
                                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("DOC_DATE").Value), clsCommon.myCstr(clsCommon.GetPrintDate(dtpToDate.Value, "dd/MM/yyyy"))) <> CompairStringResult.Equal Then
                                        common.clsCommon.MyMessageBoxShow(Me, "Screen and Import  Start Date should be same at line no '" + clsCommon.myCstr(lineNo) + "' ")
                                        isError = True
                                        Exit Sub
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(grow.Cells("Type").Value) > 0 Then
                                If clsCommon.myLen(ddlTransType.Text) > 0 Then
                                    If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Type").Value), clsCommon.myCstr(ddlTransType.SelectedValue)) <> CompairStringResult.Equal Then
                                        common.clsCommon.MyMessageBoxShow(Me, "Screen and Import Type should be same at line no '" + clsCommon.myCstr(lineNo) + "' ")
                                        isError = True
                                        Exit Sub
                                    End If
                                End If
                            End If

                            If clsCommon.myLen(prvDocCode) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Doc_Code").Value), clsCommon.myCstr(prvDocCode)) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "All Document No in Excel Should be Same. Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                    isError = True
                                    Exit Sub
                                End If
                            End If

                            If clsCommon.myLen(prvDocDate) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("DOC_DATE").Value), clsCommon.myCstr(prvDocDate)) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "All Document Date in Excel Should be Same. Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                    isError = True
                                    Exit Sub
                                End If
                            End If

                            If clsCommon.myLen(prvType) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Type").Value), clsCommon.myCstr(prvType)) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "All Type in Excel Should be Same. Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                    isError = True
                                    Exit Sub
                                End If
                            End If

                            If clsCommon.myLen(prvPosted) > 0 Then
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Posted").Value), clsCommon.myCstr(prvPosted)) <> CompairStringResult.Equal Then
                                    common.clsCommon.MyMessageBoxShow(Me, "All Posted in Excel Should be Same . Check Line no : at line :'" + clsCommon.myCstr(lineNo) + "' and at line :'" + clsCommon.myCstr(lineNo - 1) + "'")
                                    isError = True
                                    Exit Sub
                                End If
                            End If

                            If clsCommon.myLen(grow.Cells("Doc_Code").Value) > 0 Then
                                prvDocCode = grow.Cells("Doc_Code").Value
                            End If
                            If clsCommon.myLen(grow.Cells("DOC_DATE").Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Document Date cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                                isError = True
                                Exit Sub
                            End If
                            If clsCommon.myLen(grow.Cells("DOC_DATE").Value) > 0 Then
                                prvDocDate = grow.Cells("DOC_DATE").Value
                            End If
                            If clsCommon.myLen(grow.Cells("Type").Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Document Type cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "' ")
                                isError = True
                                Exit Sub
                            End If

                            If (clsCommon.myCstr(grow.Cells("Type").Value) = "P" Or clsCommon.myCstr(grow.Cells("Type").Value) = "S" Or clsCommon.myCstr(grow.Cells("Type").Value) = "T") Then
                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Type Should be 'P' or 'S' or 'T' at line :'" + clsCommon.myCstr(lineNo) + "'")
                                'Throw New Exception("Type Should be 'Y' or 'N' at line :'" + clsCommon.myCstr(lineNo) + "'")
                                isError = True
                            End If
                            If clsCommon.myLen(grow.Cells("Type").Value) > 0 Then
                                prvType = grow.Cells("Type").Value
                            End If
                            If clsCommon.myLen(grow.Cells("Posted").Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Posted Value cannot be blank at line :'" + clsCommon.myCstr(lineNo) + "'")
                                isError = True
                                Exit Sub
                            End If
                            If (clsCommon.myCstr(grow.Cells("Posted").Value) = "Y" Or clsCommon.myCstr(grow.Cells("Posted").Value) = "N") Then
                            Else
                                Throw New Exception("Posted Value Should be 'Y' or 'N' at line :'" + clsCommon.myCstr(lineNo) + "'")
                                isError = True
                            End If

                            If clsCommon.myLen(grow.Cells("Posted").Value) > 0 Then
                                prvPosted = grow.Cells("Posted").Value
                                isPosted = grow.Cells("Posted").Value
                            End If
                            '*********************** Comment Part ********************
                            If clsCommon.myLen(grow.Cells("HSN_Code").Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "HSN Code cannot be blank.")
                                isError = True
                                Exit Sub
                            ElseIf clsCommon.myLen(grow.Cells("HSN_Code").Value) > 0 Then
                                Dim isValidItemcode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Code)  from TSPL_HSN_MASTER  where Code ='" + grow.Cells("HSN_Code").Value.ToString() + "'"))
                                If isValidItemcode <= 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Invalid HSN code.", Me.Text)
                                    isError = True
                                    Exit Sub
                                End If
                            End If


                            '*********************** End Comment Part ********************
                            HSN_Code = grow.Cells("HSN_Code").Value.ToString()

                            '***********************  Comment Part ********************
                            If clsCommon.myLen(grow.Cells("Tax_Group_Code").Value) <= 0 Then
                                common.clsCommon.MyMessageBoxShow(Me, "Tax Group Code cannot be blank.", Me.Text)
                                isError = True
                                Exit Sub
                            ElseIf clsCommon.myLen(grow.Cells("Tax_Group_Code").Value) > 0 Then
                                Dim isValidGroupcode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Tax_Group_Code)  from TSPL_TAX_GROUP_MASTER  where Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                                If isValidGroupcode <= 0 Then
                                    common.clsCommon.MyMessageBoxShow(Me, "Invalid Group code.", Me.Text)
                                    isError = True
                                    Exit Sub
                                End If
                            End If

                            '*********************** End Comment Part ********************
                            Tax_Group_Code = grow.Cells("Tax_Group_Code").Value.ToString()


                            'If clsCommon.myLen(grow.Cells("Tax1_Code").Value) > 0 Then
                            '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax1_Code").Value.ToString() + "' and Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                            '    If isValidTaxCode <= 0 Then
                            '        common.clsCommon.MyMessageBoxShow("Invalid Tax1 Code.")

                            '        Exit Sub
                            '    End If
                            'End If
                            Tax1_Code = grow.Cells("Tax1_Code").Value.ToString()
                            TAX1_Rate = clsCommon.myCdbl(grow.Cells("TAX1_Rate").Value)

                            ' tax code 2


                            'If clsCommon.myLen(grow.Cells("Tax2_Code").Value) > 0 Then
                            '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax2_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                            '    If isValidTaxCode <= 0 Then
                            '        common.clsCommon.MyMessageBoxShow("Invalid Tax3 Code.")

                            '        Exit Sub
                            '    End If
                            'End If
                            Tax2_Code = grow.Cells("Tax2_Code").Value.ToString()
                            TAX2_Rate = clsCommon.myCdbl(grow.Cells("TAX2_Rate").Value)
                            ' Tax 3


                            'If clsCommon.myLen(grow.Cells("Tax3_Code").Value) > 0 Then
                            '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax3_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                            '    If isValidTaxCode <= 0 Then
                            '        common.clsCommon.MyMessageBoxShow("Invalid Tax3 Code.")

                            '        Exit Sub
                            '    End If

                            'End If
                            Tax3_Code = grow.Cells("Tax3_Code").Value.ToString()
                            TAX3_Rate = clsCommon.myCdbl(grow.Cells("TAX3_Rate").Value)
                            ' Tax 4

                            'If clsCommon.myLen(grow.Cells("Tax4_Code").Value) > 0 Then
                            '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax4_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                            '    If isValidTaxCode <= 0 Then
                            '        common.clsCommon.MyMessageBoxShow("Invalid Tax4 Code.")

                            '        Exit Sub
                            '    End If


                            'End If
                            Tax4_Code = grow.Cells("Tax4_Code").Value.ToString()
                            TAX4_Rate = clsCommon.myCdbl(grow.Cells("TAX4_Rate").Value)
                            ' Tax 5

                            'If clsCommon.myLen(grow.Cells("Tax5_Code").Value) > 0 Then
                            '    Dim isValidTaxCode As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*)  from TSPL_TAX_GROUP_DETAILS  where Tax_Code = '" + grow.Cells("Tax5_Code").Value.ToString() + "' and  Tax_Group_Code ='" + grow.Cells("Tax_Group_Code").Value.ToString() + "'"))
                            '    If isValidTaxCode <= 0 Then
                            '        common.clsCommon.MyMessageBoxShow("Invalid Tax5 Code.")

                            '        Exit Sub
                            '    End If


                            'End If
                            Tax5_Code = grow.Cells("Tax5_Code").Value.ToString()
                            TAX5_Rate = clsCommon.myCdbl(grow.Cells("TAX5_Rate").Value)
                            '**************************************************************************************************************
                            Dim dtItem As DataTable = clsDBFuncationality.GetDataTable("select Item_Code , Item_Desc from TSPL_ITEM_MASTER where HSN_Code = '" + HSN_Code + "'")
                            If (dtItem IsNot Nothing AndAlso dtItem.Rows.Count > 0) Then
                                For Each row As DataRow In dtItem.Rows
                                    lineNo = lineNo + 1
                                    gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = lineNo
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = row.Item("Item_Code")
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = row.Item("Item_Desc")
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colHSN_Code).Value = HSN_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_CODE).Value = Tax_Group_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code = '" + Tax_Group_Code + "'"))

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Code).Value = Tax1_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Rate).Value = TAX1_Rate

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Code).Value = Tax2_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Rate).Value = TAX2_Rate

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Code).Value = Tax3_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Rate).Value = TAX3_Rate

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Code).Value = Tax4_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Rate).Value = TAX4_Rate

                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Code).Value = Tax5_Code
                                    gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Rate).Value = TAX5_Rate

                                    gv1.Rows.AddNew()

                                Next
                            Else
                                common.clsCommon.MyMessageBoxShow(Me, "Item Code not found with HSN Code " + HSN_Code + " .")
                                isError = True
                                Exit Sub
                            End If
                           

                            '==========================


                            'gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = lineNo
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Code).Value = Item_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(Item_Code, Nothing)
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(Item_Code, Nothing)
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_CODE).Value = Tax_Group_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX_GROUP_Desc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Tax_Group_Desc from TSPL_TAX_GROUP_MASTER where Tax_Group_Code = '" + Tax_Group_Code + "'"))

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Code).Value = Tax1_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX1_Rate).Value = TAX1_Rate

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Code).Value = Tax2_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX2_Rate).Value = TAX2_Rate

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Code).Value = Tax3_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX3_Rate).Value = TAX3_Rate

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Code).Value = Tax4_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX4_Rate).Value = TAX4_Rate

                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Code).Value = Tax5_Code
                            'gv1.Rows(gv1.Rows.Count - 1).Cells(colTAX5_Rate).Value = TAX5_Rate

                            'gv1.Rows.AddNew()
                            '**************************************************************************************************************

                        Next

                    Catch ex As Exception

                    End Try
                End If
                isImport = False
                'clsCommon.MyMessageBoxShow("Please Click on Save Button for Save Data.", Me.Text)
                If isError = True Then
                    gv1.Rows.Clear()
                    gv1.Rows.AddNew()
                Else
                    If isPosted = "Y" Then
                        btnAdd.PerformClick()
                        btnPost.PerformClick()
                    Else
                        btnAdd.PerformClick()
                        clsCommon.MyMessageBoxShow(Me, "Please Click on Post Button for Post Data.", Me.Text)
                    End If


                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Finally
                Me.Controls.Remove(dgv)
                isImport = False
            End Try
        Catch ex As Exception

        End Try
    End Sub
    ' Ticket No : TEC/02/08/19-000978 By Prabhakar
    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        Try
            Dim query As String = " Select  '' as Doc_Code, '' as	DOC_DATE, '' as	Type, '' as	Posted, '' as	HSN_Code, '' as	Tax_Group_Code, '' as	Tax_Group_Desc, '' as	Tax1_Code, '' as	TAX1_Rate, '' as	Tax2_Code, '' as	TAX2_Rate, '' as	Tax3_Code, '' as	TAX3_Rate, '' as	Tax4_Code, '' as	TAX4_Rate, '' as	Tax5_Code, '' as	TAX5_Rate   "
            ListImpExpColumnsMandatory = New List(Of String)({"DOC_DATE", "Type", "Posted", "HSN_Code", "Tax_Group_Code"})
            ListImpExpColumnsSuperMandatory = New List(Of String)({"Doc_Code"})
            transportSql.ExporttoExcel(query, " ", " ", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID + "HSN")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Item Tax", Me.Text)
        End Try
    End Sub

   
End Class
