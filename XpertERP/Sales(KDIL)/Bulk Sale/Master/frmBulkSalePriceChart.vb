
'--------Created By Richa 18/07/2014 Against Ticket No BM00000003245,BM00000005219

Imports common
Imports System.Data.SqlClient

Public Class FrmBulkSalePriceChart
    Inherits FrmMainTranScreen
#Region "Variables"


    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim ApplyTSPriceAtBulkSale As Boolean = False
#End Region


#Region "User Defined Functions and Subroutines"

    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub FrmBulkSalePriceChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ApplyTSPriceAtBulkSale = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyTSPriceAtBulkSale, clsFixedParameterCode.ApplyTSPriceAtBulkSale, Nothing)) = 1, True, False))
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D for Delete Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N for New Transaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for New Transaction")
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBulkSalePriceChart)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        If btnsave.Visible = True Then
            RmExport.Enabled = True
            RmImport.Enabled = True
        Else
            RmExport.Enabled = False
            RmImport.Enabled = False
        End If

        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub Reset()

        fndcode.Value = ""
        txtfatRatio.Value = 0
        TxtFatWeightage.Value = 0
        TxtTSRate.Value = 0
        txtsnfRatio.Value = 0
        TxtSNFWeightage.Value = 0
        TxtUOM.Value = ""
        txtStanadardrate.Value = 0
        ''richa against Ticket no BM00000003849 on 10/09/2014
        fndLocation.Value = ""
        lblLocation.Text = ""
        TxtToleranceinMinus.Value = 0
        TxtToleranceInPlus.Value = 0
        TxtFatRate.Value = 0
        TxtSnfRate.Value = 0
        btnsave.Enabled = True
        ''=====================================
        'RmImport.Visibility = ElementVisibility.Hidden
        'RmExport.Visibility = ElementVisibility.Hidden
        txtPricedate.Value = clsCommon.GETSERVERDATE()
        TxtValidTill.Checked = False
        TxtValidTill.Value = clsCommon.GETSERVERDATE()
        fndcode.MyReadOnly = False
        btnsave.Text = "&Save"

        btndelete.Enabled = False
        isNewEntry = True
        chkUseInCanSale.Checked = False

        ''richa agarwal 12 Sep, 2016
        If clsCommon.CompairString(clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where code= '" & clsFixedParameterCode.showPostrequiredforBulkSale & "' and Type ='" & clsFixedParameterType.showPostrequiredforBulkSale & "'")), "1") = CompairStringResult.Equal Then
            btnPost.Enabled = True
            btnPost.Visible = True
        Else
            btnPost.Enabled = False
            btnPost.Visible = False

        End If
        ''richa against Ticket no ERO/10/01/19-000463 on 14 Jan,2019
        If ApplyTSPriceAtBulkSale = True Then
            txtfatRatio.Enabled = False
            TxtFatWeightage.Enabled = False
            TxtTSRate.Enabled = False
            txtsnfRatio.Enabled = False
            TxtSNFWeightage.Enabled = False
            txtStanadardrate.Enabled = False
            TxtToleranceinMinus.Enabled = False
            TxtToleranceInPlus.Enabled = False
            TxtFatRate.Enabled = False
            TxtSnfRate.Enabled = False
            chkUseInCanSale.Enabled = False
            TxtTSRate.Enabled = True
        Else
            txtfatRatio.Enabled = True
            TxtFatWeightage.Enabled = True
            TxtTSRate.Enabled = True
            txtsnfRatio.Enabled = True
            TxtSNFWeightage.Enabled = True
            txtStanadardrate.Enabled = True
            TxtToleranceinMinus.Enabled = True
            TxtToleranceInPlus.Enabled = True
            TxtFatRate.Enabled = True
            TxtSnfRate.Enabled = True
            chkUseInCanSale.Enabled = True
            TxtTSRate.Enabled = False
        End If
    End Sub

    Private Sub FrmBulkSalePriceChart_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub


    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmBulkSalePriceChart, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        Dim obj As New ClsBulkSalePriceChart()
        Try
            If AllowToSave() Then

                obj.Price_Code = fndcode.Value
                obj.Price_Date = txtPricedate.Value
                obj.Snf_Ratio = clsCommon.myCdbl(txtsnfRatio.Value)
                obj.Fat_Ratio = clsCommon.myCdbl(txtfatRatio.Value)
                obj.Snf_Weightage = clsCommon.myCdbl(TxtSNFWeightage.Value)
                obj.Fat_Weightage = clsCommon.myCdbl(TxtFatWeightage.Value)
                obj.Standard_Rate = clsCommon.myCdbl(txtStanadardrate.Value)
                ''richa against Ticket no BM00000003849 on 10/09/2014
                obj.Location_Code = clsCommon.myCstr(fndLocation.Value)
                obj.TolerancePerPlus = clsCommon.myCdbl(TxtToleranceInPlus.Value)
                obj.TolerancePerMinus = clsCommon.myCdbl(TxtToleranceinMinus.Value)
                obj.FatRate = clsCommon.myCdbl(TxtFatRate.Value)
                obj.SNFRate = clsCommon.myCdbl(TxtSnfRate.Value)
                obj.TSRate = clsCommon.myCdbl(TxtTSRate.Value)
                obj.UOM = clsCommon.myCstr(TxtUOM.Value)
                If chkUseInCanSale.Checked Then
                    obj.isUseInCanSale = True
                Else
                    obj.isUseInCanSale = False
                End If

                If TxtValidTill.Checked Then
                    obj.ValidTill = TxtValidTill.Value
                End If

                ''richa against Ticket no ERO/10/01/19-000463 on 14 Jan,2019
                If ApplyTSPriceAtBulkSale = True Then
                    obj.Snf_Weightage = 0
                    obj.Fat_Weightage = 0
                End If
                ''------------------------

                If isNewEntry Then
                    obj.AbandonmentNo = 0
                End If
                ''=================================================

                'Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Price_Code) from TSPL_BulkSalePrice_MASTER where Price_Code='" + obj.Price_Code + "'", trans)
                'If (qry = 0) Then
                '    isNewEntry = True
                'Else
                '    isNewEntry = False
                'End If
                If (ClsBulkSalePriceChart.SaveData(obj, isNewEntry)) Then
                    If isNewEntry Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.Price_Code, NavigatorType.Current)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                        LoadData(obj.Price_Code, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        ''richa against Ticket no BM00000003849 on 10/09/2014
        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            fndLocation.Focus()
            Throw New Exception("Please Select Location")
        End If
        ''-------------------------------------------------
        ''richa against Ticket no ERO/10/01/19-000463 on 14 Jan,2019
        If ApplyTSPriceAtBulkSale = False Then
            If clsCommon.myCdbl(TxtFatWeightage.Value) = 0 Then
                TxtFatWeightage.Focus()
                Throw New Exception("Please enter Fat Weightage")
            End If
            If clsCommon.myCdbl(TxtFatWeightage.Value) < 0 Then
                TxtFatWeightage.Focus()
                Throw New Exception("Fat Weightage cannot be negative")
            End If
            If clsCommon.myCdbl(TxtSNFWeightage.Value) = 0 Then
                TxtSNFWeightage.Focus()
                Throw New Exception("Please enter SNF Weightage")
            End If
            If clsCommon.myCdbl(TxtSNFWeightage.Value) < 0 Then
                TxtSNFWeightage.Focus()
                Throw New Exception("SNF Weightage cannot be negative")
            End If
            If clsCommon.myCdbl(txtfatRatio.Value) = 0 Then
                txtfatRatio.Focus()
                Throw New Exception("Please enter Fat Ratio")
            End If
            If clsCommon.myCdbl(txtfatRatio.Value) < 0 Then
                txtfatRatio.Focus()
                Throw New Exception("Fat Ratio cannot be negative")
            End If
            If clsCommon.myCdbl(txtsnfRatio.Value) = 0 Then
                txtsnfRatio.Focus()
                Throw New Exception("Please enter SNF Ratio")
            End If
            If clsCommon.myCdbl(txtsnfRatio.Value) < 0 Then
                txtsnfRatio.Focus()
                Throw New Exception("SNF Ratio cannot be negative")
            End If
            If clsCommon.myCdbl(txtStanadardrate.Value) = 0 Then
                txtStanadardrate.Focus()
                Throw New Exception("Please enter Standard Rate")
            End If
            If clsCommon.myCdbl(txtStanadardrate.Value) < 0 Then
                txtStanadardrate.Focus()
                Throw New Exception("Standard Rate cannot be negative")
            End If
            If clsCommon.myCdbl(TxtToleranceInPlus.Value) < 0 Then
                TxtToleranceInPlus.Focus()
                Throw New Exception("Tolerance % (+) cannot be in negative")
            End If
            If clsCommon.myCdbl(TxtToleranceInPlus.Value) > 100 Then
                TxtToleranceInPlus.Focus()
                Throw New Exception("Tolerance % (+) cannot be greater than 100")
            End If
            If clsCommon.myCdbl(TxtToleranceinMinus.Value) < 0 Then
                TxtToleranceinMinus.Focus()
                Throw New Exception("Tolerance % (-) cannot be in negative")
            End If
            If clsCommon.myCdbl(TxtToleranceinMinus.Value) > 100 Then
                TxtToleranceinMinus.Focus()
                Throw New Exception("Tolerance % (-) cannot be greater than 100")
            End If
            ''richa agarwal 10/10/2014
            If (clsCommon.myCdbl(TxtFatWeightage.Value) + clsCommon.myCdbl(TxtSNFWeightage.Value)) > 100 Then
                TxtFatWeightage.Focus()
                Throw New Exception("Sum of fat weightage and snf weightage should be 100")
            End If
            If (clsCommon.myCdbl(TxtFatWeightage.Value) + clsCommon.myCdbl(TxtSNFWeightage.Value)) < 100 Then
                TxtFatWeightage.Focus()
                Throw New Exception("Sum of fat weightage and snf weightage should be 100")
            End If

            If chkUseInCanSale.Checked = True Then
                Dim chkUseInCanSale As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_BulkSalePrice_MASTER where UseInCanSale =1 and location_code='" & clsCommon.myCstr(fndLocation.Value) & "' and UOM ='" & clsCommon.myCstr(TxtUOM.Value) & "'  and Price_Code <> '" + fndcode.Value + "'"))
                Dim priceCodeUseInCanSale As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Price_Code from TSPL_BulkSalePrice_MASTER where UseInCanSale =1 and location_code='" & clsCommon.myCstr(fndLocation.Value) & "' and UOM ='" & clsCommon.myCstr(TxtUOM.Value) & "' "))
                If chkUseInCanSale > 0 Then
                    Throw New Exception(" [Use in Can Sale] already allow  another price Code '" + priceCodeUseInCanSale + "',You can not allow [Use in Can Sale] more then one price code. ")
                End If
            End If
        Else
            If clsCommon.myLen(TxtTSRate.Value) < 0 Then
                TxtTSRate.Focus()
                Throw New Exception("Please enter TS Rate")
            End If
        End If
        If (TxtValidTill.Checked) Then
            If (txtPricedate.Value > TxtValidTill.Value) Then
                TxtValidTill.Focus()
                Throw New Exception("Valid till Date cannot be less than Price date ")
            End If
        End If
        ''==============
        Return True
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()

    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim ReceiptAmount As Double = 0
            Dim AmountLessDiscount As Double = 0
            Dim desc As String = ""
            If (myMessages.postConfirm()) Then

                If (ClsBulkSalePriceChart.PostData(MyBase.Form_ID, fndcode.Value)) Then
                    msg = "Successfully Posted"
                Else

                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndcode.Value, NavigatorType.Current)


            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As ClsBulkSalePriceChart = ClsBulkSalePriceChart.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            fndcode.Value = obj.Price_Code
            txtPricedate.Value = obj.Price_Date
            txtfatRatio.Value = obj.Fat_Ratio
            TxtFatWeightage.Value = obj.Fat_Weightage
            txtsnfRatio.Value = obj.Snf_Ratio
            TxtSNFWeightage.Value = obj.Snf_Weightage
            txtStanadardrate.Value = obj.Standard_Rate
            ''richa against Ticket no BM00000003849 on 10/09/2014
            fndLocation.Value = obj.Location_Code
            lblLocation.Text = clsDBFuncationality.getSingleValue("Select isnull(Location_Desc,'')  from TSPL_LOCATION_MASTER where Location_Code='" + fndLocation.Value + "'")
            TxtToleranceInPlus.Value = obj.TolerancePerPlus
            TxtToleranceinMinus.Value = obj.TolerancePerMinus
            TxtFatRate.Value = obj.FatRate
            TxtSnfRate.Value = obj.SNFRate
            TxtTSRate.Value = obj.TSRate
            chkUseInCanSale.Checked = obj.isUseInCanSale
            TxtUOM.Value = obj.UOM
            If (obj.ValidTill IsNot Nothing) Then
                TxtValidTill.Value = obj.ValidTill
                TxtValidTill.Checked = True

            End If
            ''==============================================
            If (clsCommon.myCdbl(obj.Posted)) = 1 Then
                btnPost.Enabled = False
                btnsave.Enabled = False
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
            End If



            fndcode.MyReadOnly = True

            btnsave.Text = "&Update"



        Else
            Reset()

        End If
        obj = Nothing
    End Sub
    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (ClsBulkSalePriceChart.DeleteData(fndcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        Dim qry As String = String.Empty
        Try
            qry = "select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" + fndcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndcode.MyReadOnly = False
            End If

            LoadData(fndcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            qry = Nothing
        End Try
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim qry As String = "select TSPL_BulkSalePrice_MASTER.Price_Code as Code,Convert(varchar,TSPL_BulkSalePrice_MASTER.Price_Date,103) as Date,TSPL_BulkSalePrice_MASTER.Location_Code as [Location Code],TSPL_LOCATION_MASTER.Location_Desc as [Location Name],TSPL_BulkSalePrice_MASTER.Fat_Weightage as [Fat Weightage],TSPL_BulkSalePrice_MASTER.Snf_Weightage as [SNF Weightage],TSPL_BulkSalePrice_MASTER.Fat_Ratio as [Fat Ratio],TSPL_BulkSalePrice_MASTER.Snf_Ratio as [SNF Ratio],TSPL_BulkSalePrice_MASTER.FatRate as [Fat Rate],TSPL_BulkSalePrice_MASTER.SNFRate as [SNF Rate],TSPL_BulkSalePrice_MASTER.Standard_Rate as [Standard Rate],TSPL_BulkSalePrice_MASTER.TolerancePerPlus as [Tolerance % (+)],TSPL_BulkSalePrice_MASTER.TolerancePerMinus as [Tolerance % (-)],case isnull(TSPL_BulkSalePrice_MASTER.Posted,0) when '0' Then 'Pending' When '1' then 'Approved' else '' end as 'Status' from  TSPL_BulkSalePrice_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BulkSalePrice_MASTER .Location_Code "
        fndcode.Value = clsCommon.ShowSelectForm("BulkSalePriceChart", qry, "Code", "", fndcode.Value, "", isButtonClicked)
        LoadData(fndcode.Value, NavigatorType.Current)
        qry = Nothing
    End Sub

    Private Sub RmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)

        If transportSql.importExcel(gv, "Price Code", "Price Date", "Fat Weightage", "SNF Weightage", "Fat Ratio", "SNF Ratio", "Standard Rate", "TS Rate") Then
            Dim linno As Integer = 1
            Dim obj As New ClsBulkSalePriceChart()
            Try
                trans = clsDBFuncationality.GetTransactin()
                connectSql.OpenConnection()


                For Each grow As GridViewRowInfo In gv.Rows



                    Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Price Date").Value))) Then
                        Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Dim strPriceDate As String = clsCommon.myCDate(grow.Cells("Price Date").Value)
                    Dim DblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                    Dim DblFatRatio As Double = clsCommon.myCdbl(grow.Cells("Fat Ratio").Value)
                    Dim DblSnfWeightage As Double = clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                    Dim DblSnfRatio As Double = clsCommon.myCdbl(grow.Cells("SNF Ratio").Value)
                    Dim DblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                    Dim DblTSRate As Double = clsCommon.myCdbl(grow.Cells("TS Rate").Value)

                    linno += 1

                    If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                        Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Price_Code = strPriceCode

                    If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                        Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Price_Date = strPriceDate

                    If clsCommon.myCdbl(DblFatWeightage) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                        Throw New Exception("Fat Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Fat_Weightage = DblFatWeightage

                    If clsCommon.myCdbl(DblSnfWeightage) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                        Throw New Exception("SNF Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Snf_Weightage = DblSnfWeightage

                    If clsCommon.myCdbl(DblFatRatio) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                        Throw New Exception("Fat Ratio should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Fat_Ratio = DblFatRatio

                    If clsCommon.myCdbl(DblSnfRatio) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                        Throw New Exception("SNF Ratio should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Snf_Ratio = DblSnfRatio

                    If clsCommon.myCdbl(DblStandardRate) <= 0 AndAlso ApplyTSPriceAtBulkSale = False Then
                        Throw New Exception("Standard Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.Standard_Rate = DblStandardRate

                    If clsCommon.myCdbl(DblTSRate) <= 0 AndAlso ApplyTSPriceAtBulkSale = True Then
                        Throw New Exception("TS should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.TSRate = DblTSRate

                    If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_BulkSalePrice_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                        IsNewEntry = False
                    Else
                        IsNewEntry = True

                    End If

                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
                    clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                    clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                    clsCommon.AddColumnsForChange(coll, "Fat_Ratio", obj.Fat_Ratio)
                    clsCommon.AddColumnsForChange(coll, "Snf_Ratio", obj.Snf_Ratio)
                    clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                    clsCommon.AddColumnsForChange(coll, "TSRate", obj.TSRate)
                    clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                    clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                    If IsNewEntry Then
                        clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BulkSalePrice_MASTER", OMInsertOrUpdate.Insert, "", trans)
                    Else
                        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_BulkSalePrice_MASTER", OMInsertOrUpdate.Update, "TSPL_BulkSalePrice_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                    End If

                Next
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                myMessages.myExceptions(ex)
            Finally
                obj = Nothing
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmExport.Click
        Dim str As String
        str = "select Price_Code as [Price Code],Price_Date As [Price Date] ,Fat_Weightage as [Fat Weightage],snf_Weightage as [SNF Weightage],Fat_Ratio as[Fat Ratio],Snf_Ratio as [SNF Ratio],Standard_Rate as [Standard Rate],TSRate as [TS Rate] from TSPL_BulkSalePrice_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub


    Private Sub fndLocation__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder(" Location_Type='Physical' and Is_Sub_Location='N' and Is_Section ='N'", fndLocation.Value, isButtonClicked)

        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lblLocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'"))
        Else
            lblLocation.Text = ""
        End If
    End Sub

    Private Sub txtfatRatio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfatRatio.TextChanged
       CalculateFatandSNFRate()

    End Sub

    'Private Sub TxtFatWeightage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFatWeightage.Leave
    '    'Try
    '    '    If clsCommon.myCdbl(TxtSNFWeightage.Value) > 0 Then
    '    '        If clsCommon.myCdbl(TxtFatWeightage.Value) + clsCommon.myCdbl(TxtSNFWeightage.Value) <> 100 Then
    '    '            Throw New Exception("Sum of fat weightage and snf weightage should be 100")
    '    '        End If
    '    '    End If
    '    'Catch ex As Exception
    '    '    clsCommon.MyMessageBoxShow(ex.Message)
    '    'End Try
    '    Try

    '        If clsCommon.myCdbl(TxtFatWeightage.Value) > 100 Then
    '            TxtFatWeightage.Value = 100
    '        End If
    '        TxtSNFWeightage.Value = (100 - clsCommon.myCdbl(TxtFatWeightage.Value))
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub TxtFatWeightage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFatWeightage.TextChanged
        Try
            If clsCommon.myCdbl(TxtFatWeightage.Value) > 100 Then
                TxtFatWeightage.Value = 100
            End If
            TxtSNFWeightage.Value = (100 - clsCommon.myCdbl(TxtFatWeightage.Value))
            CalculateFatandSNFRate()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        'CalculateFatandSNFRate()
    End Sub

    Private Sub txtStanadardrate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtStanadardrate.TextChanged
       CalculateFatandSNFRate()
    End Sub

    Private Sub txtsnfRatio_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtsnfRatio.TextChanged
      CalculateFatandSNFRate()
    End Sub

    'Private Sub TxtSNFWeightage_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSNFWeightage.Leave
    '    'Try
    '    '    If clsCommon.myCdbl(TxtFatWeightage.Value) > 0 Then
    '    '        If clsCommon.myCdbl(TxtFatWeightage.Value) + clsCommon.myCdbl(TxtSNFWeightage.Value) <> 100 Then
    '    '            ' TxtSNFWeightage.Focus()
    '    '            Throw New Exception("Sum of fat weightage and snf weightage should be 100")
    '    '        End If
    '    '    End If
    '    'Catch ex As Exception
    '    '    clsCommon.MyMessageBoxShow(ex.Message)
    '    'End Try
    '    Try
    '        If clsCommon.myCdbl(TxtSNFWeightage.Value) > 100 Then
    '            TxtSNFWeightage.Value = 100
    '        End If
    '        TxtFatWeightage.Value = (100 - clsCommon.myCdbl(TxtSNFWeightage.Value))
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub TxtSNFWeightage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSNFWeightage.TextChanged
        Try
            If clsCommon.myCdbl(TxtSNFWeightage.Value) > 100 Then
                TxtSNFWeightage.Value = 100
            End If
            TxtFatWeightage.Value = (100 - clsCommon.myCdbl(TxtSNFWeightage.Value))
            CalculateFatandSNFRate()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        'CalculateFatandSNFRate()
    End Sub
    Private Sub CalculateFatandSNFRate()
        If clsCommon.myCdbl(txtStanadardrate.Value) <> 0 And clsCommon.myCdbl(TxtFatWeightage.Value) <> 0 And clsCommon.myCdbl(txtfatRatio.Value) <> 0 Then
            TxtFatRate.Value = Math.Floor(clsCommon.myCdbl(txtStanadardrate.Value) * clsCommon.myCdbl(TxtFatWeightage.Value) / clsCommon.myCdbl(txtfatRatio.Value) * 100) / 100
        End If
        If clsCommon.myCdbl(txtStanadardrate.Value) <> 0 And clsCommon.myCdbl(TxtSNFWeightage.Value) <> 0 And clsCommon.myCdbl(txtsnfRatio.Value) <> 0 Then
            TxtSnfRate.Value = Math.Floor(clsCommon.myCdbl(txtStanadardrate.Value) * clsCommon.myCdbl(TxtSNFWeightage.Value) / clsCommon.myCdbl(txtsnfRatio.Value) * 100) / 100
        End If
    End Sub
    ''richa Against ticket No BM00000003879 on 11/09/2014
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndcode.Value) > 0 Then
                funPrint()
            Else
                Throw New Exception("Please Select Price Code")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub TxtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from tspl_Unit_master"
        TxtUOM.Value = clsCommon.ShowSelectForm("UOMFinder", qry, "Code", "", TxtUOM.Value, "Code", isButtonClicked)
    End Sub

    Sub funPrint()
        Try
            ' KUNAL > KDIL > TICKET : BM00000009117 > REQUEST : KLREQ000563 > BUG OF STUTI > ASSIGENED TO ME ON EXCEL NO TICKET > DATE : 24 NOV 2016
            Dim qry As String = " Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name , CONCAT(TSPL_LOCATION_MASTER.Add1, ',' , TSPL_LOCATION_MASTER.Add2 , ',' , TSPL_LOCATION_MASTER.Add3 ,  TSPL_LOCATION_MASTER.Add4 ) as Loc_Desc, CONCAT(TSPL_LOCATION_MASTER.City_Code, ',' ,SM.STATE_NAME , ',' , TSPL_LOCATION_MASTER.Pin_Code ,',' , TSPL_LOCATION_MASTER.Country ) Regional_Detail, TSPL_COMPANY_MASTER.Add1 as Address1,TSPL_COMPANY_MASTER.Add2 as Address2, " &
            " TSPL_COMPANY_MASTER.Add3 as Address3,TSPL_BulkSalePrice_MASTER.Price_Code,Convert(varchar,TSPL_BulkSalePrice_MASTER.Price_Date,103) as Price_Date, " &
            " TSPL_BulkSalePrice_MASTER.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_BulkSalePrice_MASTER.Fat_Weightage,TSPL_BulkSalePrice_MASTER.Snf_Weightage, " &
            " TSPL_BulkSalePrice_MASTER.Standard_Rate,TSPL_BulkSalePrice_MASTER.Fat_Ratio,TSPL_BulkSalePrice_MASTER.Snf_Ratio,TSPL_BulkSalePrice_MASTER.FatRate, " &
            " TSPL_BulkSalePrice_MASTER.SNFRate,TSPL_BulkSalePrice_MASTER.TolerancePerPlus,TSPL_BulkSalePrice_MASTER.TolerancePerMinus,convert (varchar,TSPL_BulkSalePrice_MASTER.ValidTill,103) as ValidTill, " &
            "TSPL_BulkSalePrice_MASTER.Created_By,convert (varchar,TSPL_BulkSalePrice_MASTER.Created_Date,103) as Created_Date ,case when TSPL_BulkSalePrice_MASTER.Posted=1 then TSPL_BulkSalePrice_MASTER.Modified_By else '' end as Posted_By,case when TSPL_BulkSalePrice_MASTER.Posted=1 then convert (varchar, TSPL_BulkSalePrice_MASTER.Modified_Date,103) else '' end as Posted_Date," &
            " Convert(varchar,TSPL_BulkSalePrice_MASTER_History.AbandonmentDate,103) as AbandonmentDate ,TSPL_BulkSalePrice_MASTER_History.Fat_Weightage as  Fat_WeightageHis, " &
            " TSPL_BulkSalePrice_MASTER_History.Snf_Weightage as Snf_WeightageHis,TSPL_BulkSalePrice_MASTER_History.Fat_Ratio as Fat_RatioHis,TSPL_BulkSalePrice_MASTER_History.Snf_Ratio as Snf_RatioHis, " &
            " TSPL_BulkSalePrice_MASTER_History.Standard_Rate as Standard_RateHis,TSPL_BulkSalePrice_MASTER_History.FatRate as FatRateHis,TSPL_BulkSalePrice_MASTER_History.SNFRate as SNFRateHis, " &
            " TSPL_BulkSalePrice_MASTER_History.TolerancePerPlus as TolerancePerPlusHis,TSPL_BulkSalePrice_MASTER_History.TolerancePerMinus as TolerancePerMinusHis,TSPL_BulkSalePrice_MASTER_History.Modified_By as ModifiedBy from TSPL_BulkSalePrice_MASTER  " &
            " Left outer Join TSPL_BulkSalePrice_MASTER_History on TSPL_BulkSalePrice_MASTER.Price_Code=TSPL_BulkSalePrice_MASTER_History.Price_Code left outer Join TSPL_LOCATION_MASTER  " &
            " on TSPL_LOCATION_MASTER.Location_Code=TSPL_BulkSalePrice_MASTER.Location_Code Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_BulkSalePrice_MASTER.Comp_Code " &
            "  LEFT JOIN TSPL_STATE_MASTER SM ON TSPL_LOCATION_MASTER.State = SM.STATE_CODE  where 1=1 and  TSPL_BulkSalePrice_MASTER.Price_Code='" + fndcode.Value + "'"

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.KwalitySalesReport, dt, "rptBulkSalePriceChart", "Price Chart")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    ''=========================================================
End Class
