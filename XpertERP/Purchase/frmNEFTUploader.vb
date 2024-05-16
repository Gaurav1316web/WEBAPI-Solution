Imports common
Imports System.Data.SqlClient
Imports XpertERPFarmerPayment

Public Class FrmNEFTUploader
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim FORMTYPE As String = Nothing
    Public Const colSlno As String = "colSlno"
    Public Const colCorporateID As String = "colCorporateID"
    Public Const ColComp As String = "ColComp"
    Public Const colDebitAccount As String = "colDebitAccount"
    Public Const colType As String = "colType"
    Public Const colBenifName As String = "colBenifName"
    Public Const colRemarks As String = "colRemarks"
    Public Const ColDOCUMENTREF As String = "ColDOCUMENTREF"
    Public Const ColComp1 As String = "ColComp1"
    Public Const ColAMOUNT As String = "ColAMOUNT"
    Public Const ColAcNo As String = "ColAcNo"
    Public Const ColTransferType As String = "ColTransferType"
    Public Const ColIFSCCODE As String = "ColIFSCCODE"
    Public Const ColBankCODEMP As String = "ColBankCODEMP"
    Public Const ColPAYEENAME As String = "ColPAYEENAME"
    Public Const Column1 As String = "Column1"
    Public Const Column2 As String = "Column2"
    Public Const Column3 As String = "Column3"
    Public Const Column4 As String = "Column4"
    Public Const Column5 As String = "Column5"
    Public Const Column6 As String = "Column6"
    Public Const ColPAYEEACNO As String = "ColPAYEEACNO"
    Public Const Column7 As String = "Column7"
    Public Const Column8 As String = "Column8"
    Public Const Column9 As String = "Column9"
    Public Const Column10 As String = "Column10"
    Public Const Column11 As String = "Column11"
    Public Const Column12 As String = "Column12"
    Public Const Column13 As String = "Column13"
    Public Const Column14 As String = "Column14"
    Public Const Column15 As String = "Column15"
    Public Const Column16 As String = "Column16"
    Public Const Column17 As String = "Column17"
    Public Const ColDOCUMENTREF1 As String = "ColDOCUMENTREF1"
    Public Const ColPAYEENAME1 As String = "ColPAYEENAME1"
    Dim colTextBox As GridViewTextBoxColumn = Nothing
    Dim colDecimal As GridViewDecimalColumn = Nothing
    Dim ButtonToolTip As ToolTip = New ToolTip()
    '=====sanjeet (NEW UPLOADER -UDL 21/12/16)=======
    Dim New_NeftUploader As Double
    Dim DebitBankSelectWithNewFormateInNFTUploader As Double
    Dim CreateNeftuploaderPlantWise As Double = 0
    Public Const colmccCode As String = "colmccCode"
    Public Const colmccName As String = "colmccName"
    Public Const colvspcode As String = "colvspcode"
    Public Const colNeftAmt As String = "colNeftAmt"
    Public Const colNeftValueDate As String = "colNeftValueDate"
    Public Const colSenderIfsc As String = "colSenderIfsc"
    Public Const colSCACNO As String = "colSCACNO"
    Public Const colSCAName As String = "colSCAName"
    Public Const colBenifIFSC As String = "colBenifIFSC"
    Public Const colBeniACNO As String = "colBeniACNO"
    Public Const colBeniACName As String = "colBeniACName"
    Public Const colBeniBankName As String = "colBeniBankName"
    Public Const colBeniBankBranch As String = "colBeniBankBranch"
    Public Const colVlcUpladerCode As String = "colVlcUpladerCode"
    Public Const colVendorCode As String = "colVendorCode"
    Public FixRTGSAmt As Decimal = 0
    '==================
    '=====Preeti (NEW UPLOADER -UDL 21/12/16)=======
    Dim New_NeftUploader_For_MP As Double
    Public Const colMPcode_For_MP As String = "colMPcode_For_MP"
    Public Const colNeftAmt_For_MP As String = "colNeftAmt_For_MP"
    Public Const colNeftValueDate_For_MP As String = "colNeftValueDate_For_MP"
    Public Const colSenderIfsc_For_MP As String = "colSenderIfsc_For_MP"
    Public Const colSCACNO_For_MP As String = "colSCACNO_For_MP"
    Public Const colSCAName_For_MP As String = "colSCAName_For_MP"
    Public Const colBenifIFSC_For_MP As String = "colBenifIFSC_For_MP"
    Public Const colBeniACNO_For_MP As String = "colBeniACNO_For_MP"
    Public Const colBeniACName_For_MP As String = "colBeniACName_For_MP"
    Public Const colBeniBankName_For_MP As String = "colBeniBankName_For_MP"
    Public Const colBeniBankBranch_For_MP As String = "colBeniBankBranch_For_MP"
    Public Const colVlcUpladerCode_For_MP As String = "colVlcUpladerCode_For_MP"
    Public Const colVendorCode_For_MP As String = "colVendorCode_For_MP"
    Public FixRTGSAmt_For_MP As Decimal = 0
    Public Const ColACVSPCode As String = "ColACVSPCode"
    Public Const ColADVLCCode As String = "ColADVLCCode"
    Dim AllowDateChanged As Boolean = False
    Dim Is_Load As Boolean = False
    '==================
    Dim dr As DataRow
#End Region

    Private Sub FrmNEFTUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        LoadType()
        GetFixAmount()
        LoadTypePaymnetFarmer()
        gvTemp.DataSource = Nothing


        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.Panel2Collapsed = False
            pnlrtgsamt.Visible = True
            txtInvoiceDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
            txtToDate.Value = clsCommon.GETSERVERDATE()
            lblInvDate.Visible = False
            txtInvoiceDate.Visible = False
            lblToDate.Visible = False
            txtToDate.Visible = False
            lblVendorCode.Visible = False
            txtVendorCode.Visible = False
            btnGo.Visible = False
            BtnSameBranch.Visible = True
            btnOtherBranch.Visible = True
            lblFarmerType.Visible = False
            CboTypePaymentFarmer.Visible = False

        Else
            SplitContainer3.Panel1Collapsed = True
            SplitContainer3.Panel2Collapsed = False
            SplitContainer2.SplitterDistance = 40
            BtnSameBranch.Visible = False
            btnOtherBranch.Visible = False
            lblFarmerType.Visible = True
            CboTypePaymentFarmer.Visible = True

        End If
        '===Sanjeet====
        New_NeftUploader = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowToGenerate_NEFTUPLOADER, clsFixedParameterCode.AllowToGenerate_NEFTUPLOADER, Nothing))
        DebitBankSelectWithNewFormateInNFTUploader = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.DebitBankSelectWithNewFormateInNFTUploader, clsFixedParameterCode.DebitBankSelectWithNewFormateInNFTUploader, Nothing))
        '=========

        If clsCommon.CompairString(clsUserMgtCode.frmNEFTUploaderFarmer, FORMTYPE) = CompairStringResult.Equal Then
            CboTypePaymentFarmer.Text = "Payment Farmer"

        End If
        fndBank.Visible = False
        lblBankCaption.Visible = False
        txtBankCode.Visible = False
        radbtnBulkExp.Visible = True

        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsUserMgtCode.frmNEFTUploaderFarmer, FORMTYPE) <> CompairStringResult.Equal Then
            If New_NeftUploader.Equals(1) Then
                If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal AndAlso DebitBankSelectWithNewFormateInNFTUploader = 1 Then
                    fndBank.Visible = True
                    lblBankCaption.Visible = True
                    txtBankCode.Visible = True
                    lblFarmerType.Visible = False
                    CboTypePaymentFarmer.Visible = False
                    radbtnBulkExp.Visible = True
                    SplitContainer2.SplitterDistance = 100
                    Load_Blank_Grid_NEW_BholeBaba()
                End If
            End If
        End If
        pnlCreateNeftuploaderPlantWise.Visible = False
        If clsCommon.CompairString(clsUserMgtCode.frmNEFTUploader, FORMTYPE) = CompairStringResult.Equal Then
            CreateNeftuploaderPlantWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.CreateNeftuploaderPlantWise, clsFixedParameterCode.CreateNeftuploaderPlantWise, Nothing))
            If CreateNeftuploaderPlantWise = 1 Then
                pnlCreateNeftuploaderPlantWise.Visible = True
                SplitContainer2.SplitterDistance = 120
            Else
                pnlCreateNeftuploaderPlantWise.Visible = False
            End If
        End If
        'objCommonVar.CurrentCompanyCode = "RCDF"
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UCDF") = CompairStringResult.Equal OrElse clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "RCDF") = CompairStringResult.Equal Then
            If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then
                radbtnBulkExp.Items.Remove(RadMenuItemMP)
            Else
                radbtnBulkExp.Items.Remove(RadMenuItemVSP)
            End If
        Else
            radbtnBulkExp.Items.Remove(RadMenuItemVSP)
            radbtnBulkExp.Items.Remove(RadMenuItemMP)
            radbtnBulkExp.Items.Remove(RadMenuItemBank)
        End If
        txtMonth.Value = clsCommon.GETSERVERDATE()
        Is_Load = False
        AllowDateChanged = True
    End Sub

    Sub loadblankGrid(Optional ByVal IsFarmer As Boolean = False)

        gv.Rows.Clear()

        gv.Columns.Clear()


        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "SL. No."
        'colTextBox.Name = colSlno
        'colTextBox.Width = 80
        'colTextBox.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Company"
        colTextBox.Name = ColComp
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "DOCUMENT REF"
        colTextBox.Name = ColDOCUMENTREF
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Company"
        colTextBox.Name = ColComp1
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        ''richa agarwal 08-jan-2015 format for excel sheet cell format  ticket no BM00000008659
        'colTextBox = New GridViewTextBoxColumn()
        'colTextBox.FormatString = ""
        'colTextBox.HeaderText = "Amount"
        'colTextBox.Name = ColAMOUNT
        'colTextBox.Width = 80
        'colTextBox.ReadOnly = True
        'gv.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = "{0:n2}"
        colDecimal.HeaderText = "Amount"
        colDecimal.Name = ColAMOUNT
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Company A/C No"
        colTextBox.Name = ColAcNo
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Transfer Type(I/M)"
        colTextBox.Name = ColTransferType
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "IFSC Code"
        colTextBox.Name = ColIFSCCODE
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        If clsCommon.CompairString(clsUserMgtCode.frmNEFTUploaderFarmer, FORMTYPE) = CompairStringResult.Equal Then
            colTextBox = New GridViewTextBoxColumn()
            colTextBox.FormatString = ""
            colTextBox.HeaderText = "Bank Code"
            colTextBox.Name = ColBankCODEMP
            colTextBox.Width = 80
            colTextBox.ReadOnly = True
            gv.MasterTemplate.Columns.Add(colTextBox)
        End If

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee Name"
        colTextBox.Name = ColPAYEENAME
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column1
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column2
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column3
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column4
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column5
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column6
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee A/C No."
        colTextBox.Name = ColPAYEEACNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column7
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column8
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column9
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column10
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column11
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column12
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column13
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column14
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column15
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column16
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = ""
        colTextBox.Name = Column17
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)



        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Document Ref"
        colTextBox.Name = ColDOCUMENTREF1
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Payee Name"
        colTextBox.Name = ColPAYEENAME1
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = If(IsFarmer = True, "VSP Code", "AC")
        colTextBox.Name = ColACVSPCode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = If(IsFarmer = True, "MP Code", "AD")
        colTextBox.Name = ColADVLCCode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.BestFitColumns(BestFitColumnMode.AllCells)


    End Sub

    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        fndBank.Value = ""
        txtBankCode.Text = ""
        Dim Whrcls As String = Nothing
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            If clsCommon.CompairString(cboType.Text, "Payment Process") = CompairStringResult.Equal Then

                fndDocNo.Value = clsPaymentProcessHead.getFinder("isPosted=1", fndDocNo.Value, isButtonClicked)
                'fndDocNo.Value = clsPaymentProcessHead.getFinder("", fndDocNo.Value, isButtonClicked)
                Dim rValue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DocRefNoForUploader from TSPL_PAYMENT_PROCESS_HEAD where doc_no='" & fndDocNo.Value & "'"))
                txtNEFTUploaderREFNo.Text = rValue
                If clsCommon.myLen(fndDocNo.Value) > 0 Then
                    '=====sanjeet(Check For New NEFT UPLOADER Generate)==========
                    If New_NeftUploader.Equals(1) Then
                        If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then
                            Load_New_Uploader_Data(fndDocNo.Value)
                        Else
                            Load_New_Uploader_Data_For_Mp(fndDocNo.Value)
                        End If

                    Else
                        If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then

                            LoadData(fndDocNo.Value)
                        Else

                            LoadDataFarmer(fndDocNo.Value)
                        End If

                    End If
                    '==========================
                Else
                    reset()
                End If
            End If
        Else

            If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then
                If clsCommon.CompairString(clsUserMgtCode.frmNEFTUploaderFarmer, FORMTYPE) = CompairStringResult.Equal Then
                    Whrcls = " isPosted=1 and  FarmType='PPF' "

                End If
                fndDocNo.Value = clsPaymentProcessHead.getFinder(Whrcls, fndDocNo.Value, isButtonClicked)
            Else
                Whrcls = " isPosted=1 and  FarmType='PPF' "
                fndDocNo.Value = clsPaymentProcessHead.getFinder(Whrcls, fndDocNo.Value, isButtonClicked)
            End If

            'fndDocNo.Value = clsPaymentProcessHead.getFinder("", fndDocNo.Value, isButtonClicked)
            Dim rValue As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DocRefNoForUploader from TSPL_PAYMENT_PROCESS_HEAD where doc_no='" & fndDocNo.Value & "'"))
            txtNEFTUploaderREFNo.Text = rValue
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                '=====sanjeet(Check For New NEFT UPLOADER Generate)==========
                If New_NeftUploader.Equals(1) Then
                    If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then
                        If DebitBankSelectWithNewFormateInNFTUploader = 1 Then
                            Load_New_Uploader_Data_BholeBaba(fndDocNo.Value)
                        Else
                            Load_New_Uploader_Data(fndDocNo.Value)
                        End If
                    Else
                        Load_New_Uploader_Data_For_Mp(fndDocNo.Value)
                    End If

                Else
                    If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then

                        LoadData(fndDocNo.Value)
                    Else

                        LoadDataFarmer(fndDocNo.Value)
                    End If
                End If
                '==========================
            Else
                reset()
            End If
        End If


    End Sub

    Sub reset()
        txtInvoiceDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        fndDocNo.Value = ""
        fndBank.Value = ""
        txtBankCode.Text = ""
        loadblankGrid()
        txtNEFTUploaderREFNo.Text = ""
        txtVendorCode.arrValueMember = Nothing
        fndPlantCode.Value = ""
        pnlCreateNeftuploaderPlantWise.Enabled = True
        GetFixAmount()
        If clsCommon.CompairString(clsUserMgtCode.frmNEFTUploaderFarmer, FORMTYPE) = CompairStringResult.Equal Then
            CboTypePaymentFarmer.Text = "Payment Farmer"
        End If

    End Sub
    Function getAccountNo(bankCode As String) As String
        Dim rValue As String = String.Empty
        Try
            Dim qry As String = "select BANKACCNUMBER  from TSPL_BANK_MASTER where BANK_CODE='" & bankCode & "'"
            rValue = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Function isPaymentModeNEFT(Code As String) As Boolean
        Dim rValue As Boolean = False
        Try
            Dim qry As String = " select Payment_Type  from TSPL_PAYMENT_CODE where Payment_Code='" & Code & "'"
            If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry), "NEFT") = CompairStringResult.Equal Then
                rValue = True
            Else
                rValue = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function isPaymentModeRTGS(Code As String) As Boolean
        Dim rValue As Boolean = False
        Try
            Dim qry As String = " select Payment_Type  from TSPL_PAYMENT_CODE where Payment_Code='" & Code & "'"
            If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry), "RTGS") = CompairStringResult.Equal Then
                rValue = True
            Else
                rValue = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Function isPaymentModeTransfer(Code As String) As Boolean
        Dim rValue As Boolean = False
        Try
            Dim qry As String = " select Payment_Type  from TSPL_PAYMENT_CODE where Payment_Code='" & Code & "'"
            If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry), "Transfer") = CompairStringResult.Equal Then
                rValue = True
            Else
                rValue = False
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function
    Sub Load_A_Class_Data()
        Try
            Load_Blank_Grid_ForVendor()

            Dim Query As String = Nothing
            Dim valdate As DateTime = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE())
            Query = "select MAX(COALESCE(Tspl_bulk_milk_purchase_invoice_head.Total_AMT,0)) AS [INVOICE AMOUNT (Rs)],convert(varchar(15),'" + valdate + "',103) as [Value Date] ," &
                " MAX(tspl_bank_Branch_master.IFSC_Code) as [SENDER IFSC],CAST(MAX(TSPL_BANK_MASTER.BANKACCNUMBER) AS nvarchar(30)) as [SENDING CUSTOMER A/C NO],MAX(TSPL_COMPANY_MASTER.Comp_Name) AS [SENDING CUSTOMER A/C NAME]," &
                " MAX(TSPL_VENDOR_MASTER.IFSC_Code) AS [BENEFICIARY IFSC], " &
                " MAX(TSPL_VENDOR_MASTER.Account_No) AS [BENEFICIARY A/C NO],MAX(TSPL_VENDOR_MASTER.Vendor_Name) AS [BENEFICIARY A/C NAME]," &
                " MAX(TSPL_VENDOR_MASTER.Bank_Name) AS [BENEFICIARY BANK NAME],MAX(TSPL_VENDOR_MASTER.Branch_Name) AS [BENEFICIARY BANK BRANCH]," &
                " MAX(TSPL_VENDOR_MASTER.Vendor_Code) as [VENDOR CODE] " &
                 " from Tspl_bulk_milk_purchase_invoice_head " &
                " LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=Tspl_bulk_milk_purchase_invoice_head.vendor_code " &
                 " LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=Tspl_bulk_milk_purchase_invoice_head.Comp_Code " &
                " LEFT JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code " &
                " LEFT JOIN tspl_bank_Branch_master ON tspl_bank_Branch_master.BRANCH_CODE=TSPL_BANK_MASTER.Branch_Code " &
                 " where 1=1  and "
            If txtVendorCode.arrValueMember IsNot Nothing AndAlso txtVendorCode.arrValueMember.Count > 0 Then

                Query += " Tspl_bulk_milk_purchase_invoice_head.vendor_code in (" + clsCommon.GetMulcallString(txtVendorCode.arrValueMember) + ") and "
            End If
            Query += " Tspl_bulk_milk_purchase_invoice_head.DOC_DATE >=convert(date,'" & clsCommon.myCDate(txtInvoiceDate.Value) & "',103) " &
                 " and Tspl_bulk_milk_purchase_invoice_head.DOC_DATE<=convert(date,'" & clsCommon.myCDate(txtToDate.Value) & "',103) GROUP BY Tspl_bulk_milk_purchase_invoice_head.vendor_code,Tspl_bulk_milk_purchase_invoice_head.DOC_DATE "

            Dim dtNU As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtNU.Rows.Count > 0 Then

                Dim j As Integer = 0
                For i As Integer = 0 To dtNU.Rows.Count - 1

                    gv.Rows.AddNew()
                    gv.Rows(j).Cells(colNeftAmt).Value = dtNU.Rows(i)("INVOICE AMOUNT (Rs)").ToString()
                    gv.Rows(j).Cells(colNeftValueDate).Value = dtNU.Rows(i)("Value Date").ToString()
                    gv.Rows(j).Cells(colSenderIfsc).Value = dtNU.Rows(i)("SENDER IFSC").ToString()
                    gv.Rows(j).Cells(colSCACNO).Value = dtNU.Rows(i)("SENDING CUSTOMER A/C NO").ToString()
                    gv.Rows(j).Cells(colSCAName).Value = dtNU.Rows(i)("SENDING CUSTOMER A/C NAME").ToString()
                    gv.Rows(j).Cells(colBenifIFSC).Value = dtNU.Rows(i)("BENEFICIARY IFSC").ToString()

                    gv.Rows(j).Cells(colBeniACNO).Value = dtNU.Rows(i)("BENEFICIARY A/C NO").ToString()
                    gv.Rows(j).Cells(colBeniACName).Value = dtNU.Rows(i)("BENEFICIARY A/C NAME").ToString()

                    gv.Rows(j).Cells(colBeniBankName).Value = dtNU.Rows(i)("BENEFICIARY BANK NAME").ToString()
                    gv.Rows(j).Cells(colBeniBankBranch).Value = dtNU.Rows(i)("BENEFICIARY BANK BRANCH").ToString()

                    gv.Rows(j).Cells(colVendorCode).Value = dtNU.Rows(i)("VENDOR CODE").ToString()
                    j = j + 1
                Next

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Load_New_Uploader_Data(DocNo As String)
        Try
            Load_Blank_Grid_NEW()
            Dim Query As String = Nothing


            Query = "select  TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected as [MCC CODE], TSPL_MCC_MASTER.MCC_NAME as [MCC NAME], TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE as [VSP CODE],TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount AS [Amount (Rs)],convert(varchar(15),TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Value Date],tspl_bank_Branch_master.IFSC_Code as [SENDER IFSC],CAST(TSPL_BANK_MASTER.BANKACCNUMBER AS nvarchar(30)) as [SENDING CUSTOMER A/C NO],TSPL_COMPANY_MASTER.Comp_Name AS [SENDING CUSTOMER A/C NAME],TSPL_VENDOR_MASTER.IFSC_Code AS [BENEFICIARY IFSC]," &
              " TSPL_VENDOR_MASTER.Account_No AS [BENEFICIARY A/C NO],TSPL_VENDOR_MASTER.Vendor_Name AS [BENEFICIARY A/C NAME]," &
              " TSPL_VENDOR_MASTER.Bank_Name AS [BENEFICIARY BANK NAME],TSPL_VENDOR_MASTER.Branch_Name AS [BENEFICIARY BANK BRANCH],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC UPLOADER CODE] " &
              " from TSPL_PAYMENT_PROCESS_HEAD LEFT JOIN TSPL_PAYMENT_PROCESS_DETAIL ON TSPL_PAYMENT_PROCESS_DETAIL.Doc_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No " &
              " LEFT JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Bank_Code " &
              " LEFT JOIN tspl_bank_Branch_master ON tspl_bank_Branch_master.BRANCH_CODE=TSPL_BANK_MASTER.Branch_Code " &
              " LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE " &
              " LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_PROCESS_HEAD.Comp_Code " &
              " left outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected " &
              " where 1=1  and TSPL_PAYMENT_PROCESS_DETAIL.is_Hold_Payment_Process=0   and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount >  " + clsCommon.myCstr(txtAmt.Value) + " "
            If CreateNeftuploaderPlantWise = 1 Then
                Query = Query + "  and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in (" + DocNo + ") "
            Else
                Query = Query + "  and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + DocNo + "') "
            End If



            Dim dtNU As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtNU.Rows.Count > 0 Then
                Dim j As Integer = 0
                For i As Integer = 0 To dtNU.Rows.Count - 1

                    gv.Rows.AddNew()
                    gv.Rows(j).Cells(colmccCode).Value = dtNU.Rows(i)("MCC CODE").ToString()
                    gv.Rows(j).Cells(colmccName).Value = dtNU.Rows(i)("MCC NAME").ToString()
                    gv.Rows(j).Cells(colvspcode).Value = dtNU.Rows(i)("VSP CODE").ToString()
                    gv.Rows(j).Cells(colNeftAmt).Value = dtNU.Rows(i)("Amount (Rs)").ToString()
                    gv.Rows(j).Cells(colNeftValueDate).Value = dtNU.Rows(i)("Value Date").ToString()
                    gv.Rows(j).Cells(colSenderIfsc).Value = dtNU.Rows(i)("SENDER IFSC").ToString()
                    gv.Rows(j).Cells(colSCACNO).Value = dtNU.Rows(i)("SENDING CUSTOMER A/C NO").ToString()
                    gv.Rows(j).Cells(colSCAName).Value = dtNU.Rows(i)("SENDING CUSTOMER A/C NAME").ToString()
                    gv.Rows(j).Cells(colBenifIFSC).Value = dtNU.Rows(i)("BENEFICIARY IFSC").ToString()

                    gv.Rows(j).Cells(colBeniACNO).Value = dtNU.Rows(i)("BENEFICIARY A/C NO").ToString()
                    gv.Rows(j).Cells(colBeniACName).Value = dtNU.Rows(i)("BENEFICIARY A/C NAME").ToString()

                    gv.Rows(j).Cells(colBeniBankName).Value = dtNU.Rows(i)("BENEFICIARY BANK NAME").ToString()
                    gv.Rows(j).Cells(colBeniBankBranch).Value = dtNU.Rows(i)("BENEFICIARY BANK BRANCH").ToString()

                    gv.Rows(j).Cells(colVlcUpladerCode).Value = dtNU.Rows(i)("VLC UPLOADER CODE").ToString()
                    j = j + 1
                Next
                pnlCreateNeftuploaderPlantWise.Enabled = False
            End If
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            gv.SummaryRowsBottom.Clear()

            For iii As Integer = 0 To gv.Columns.Count - 1
                If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                    summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                End If
            Next

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Load_New_Uploader_Data_BholeBaba(DocNo As String)
        Try
            Load_Blank_Grid_NEW_BholeBaba()
            Dim Query As String = Nothing


            Query = " select ROW_NUMBER() OVER(ORDER BY TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE asc ) as SNO ,TSPL_COMPANY_MASTER.Ward_No as [Corporate ID],TSPL_COMPANY_MASTER.Comp_Name as [Debit Account Holder Name],'' as [Debit Account Holder Bank],'' as [Debit Account],case when substring (TSPL_VENDOR_MASTER.IFSC_Code,1,3) = 'SBI' And  substring (TSPL_VENDOR_MASTER.IFSC_Code,1,6) <> 'SBIN0R'  then 'DCR' when  TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount <  200000 then 'NEFT' when  TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount >= 200000 then  'RTGS' else '' end
                      as [Type] , TSPL_VENDOR_MASTER.Vendor_Name +'-' + TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader  as [Beneficiary Name],TSPL_VENDOR_MASTER.IFSC_Code as [Beneficiary IFS Code],TSPL_VENDOR_MASTER.Account_No as [Beneficiery Account No.],TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount as Amount,TSPL_VENDOR_MASTER.Vendor_Name +'-' + TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [Remark]  from TSPL_PAYMENT_PROCESS_HEAD LEFT JOIN TSPL_PAYMENT_PROCESS_DETAIL ON TSPL_PAYMENT_PROCESS_DETAIL.Doc_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No  LEFT JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Bank_Code  LEFT JOIN tspl_bank_Branch_master ON tspl_bank_Branch_master.BRANCH_CODE=TSPL_BANK_MASTER.Branch_Code  LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE  LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_PROCESS_HEAD.Comp_Code  where 1=1  and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + DocNo + "') and TSPL_PAYMENT_PROCESS_DETAIL.is_Hold_Payment_Process=0   
                     "


            Dim dtNU As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtNU.Rows.Count > 0 Then
                Dim j As Integer = 0
                For i As Integer = 0 To dtNU.Rows.Count - 1
                    ' colSlno, colCorporateID, ColComp,colDebitAccount,colType, colBenifName, colBenifIFSC, colBeniACNO, colNeftAmt,colRemarks
                    gv.Rows.AddNew()
                    gv.Rows(j).Cells(colSlno).Value = dtNU.Rows(i)("SNO").ToString()
                    gv.Rows(j).Cells(colCorporateID).Value = dtNU.Rows(i)("Corporate ID").ToString()
                    gv.Rows(j).Cells(ColComp).Value = dtNU.Rows(i)("Debit Account Holder Name").ToString()
                    gv.Rows(j).Cells(colSCAName).Value = dtNU.Rows(i)("Debit Account Holder Bank").ToString()
                    gv.Rows(j).Cells(colDebitAccount).Value = dtNU.Rows(i)("Debit Account").ToString()
                    gv.Rows(j).Cells(colType).Value = dtNU.Rows(i)("Type").ToString()
                    gv.Rows(j).Cells(colBenifName).Value = dtNU.Rows(i)("Beneficiary Name").ToString()
                    gv.Rows(j).Cells(colBenifIFSC).Value = dtNU.Rows(i)("Beneficiary IFS Code").ToString()
                    gv.Rows(j).Cells(colBeniACNO).Value = dtNU.Rows(i)("Beneficiery Account No.").ToString()
                    gv.Rows(j).Cells(colNeftAmt).Value = dtNU.Rows(i)("Amount").ToString()
                    gv.Rows(j).Cells(colRemarks).Value = dtNU.Rows(i)("Remark").ToString()
                    j = j + 1
                Next

            End If
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            gv.SummaryRowsBottom.Clear()

            For iii As Integer = 0 To gv.Columns.Count - 1
                If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                    summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                End If
            Next

            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Load_Blank_Grid_NEW()
        gv.Rows.Clear()

        gv.Columns.Clear()
        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "MCC CODE"
        colTextBox.Name = colmccCode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "MCC NAME"
        colTextBox.Name = colmccName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VSP CODE"
        colTextBox.Name = colvspcode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = "{0:n2}"
        colDecimal.HeaderText = "Amount (Rs)"
        colDecimal.Name = colNeftAmt
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Value Date"
        colTextBox.Name = colNeftValueDate
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDER IFSC"
        colTextBox.Name = colSenderIfsc
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDING CUSTOMER A/C NO"
        colTextBox.Name = colSCACNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDING CUSTOMER A/C NAME"
        colTextBox.Name = colSCAName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY IFSC"
        colTextBox.Name = colBenifIFSC
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY A/C NO"
        colTextBox.Name = colBeniACNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY A/C NAME"
        colTextBox.Name = colBeniACName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY BANK NAME"
        colTextBox.Name = colBeniBankName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY BANK BRANCH"
        colTextBox.Name = colBeniBankBranch
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC UPLOADER CODE"
        colTextBox.Name = colVlcUpladerCode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.BestFitColumns(BestFitColumnMode.AllCells)


    End Sub

    Sub Load_Blank_Grid_NEW_BholeBaba()
        gv.Rows.Clear()
        ' colSlno, colCorporateID, ColComp,colDebitAccount,colType, colBenifName, colBenifIFSC, colBeniACNO, colNeftAmt,colRemarks
        gv.Columns.Clear()

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "S.No."
        colTextBox.Name = colSlno
        colTextBox.Width = 50
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Corporate ID"
        colTextBox.Name = colCorporateID
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Debit Account Holder Name"
        colTextBox.Name = ColComp
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        ' colSCAName

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Debit Account Holder Bank"
        colTextBox.Name = colSCAName
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        colTextBox.IsVisible = False
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Debit Account"
        colTextBox.Name = colDebitAccount
        colTextBox.Width = 100
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Type"
        colTextBox.Name = colType
        colTextBox.Width = 50
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        'colBenifName, colBenifIFSC, colBeniACNO, colNeftAmt,colRemarks


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Beneficiary Name"
        colTextBox.Name = colBenifName
        colTextBox.Width = 120
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Beneficiary IFS Code"
        colTextBox.Name = colBenifIFSC
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Beneficiery Account No."
        colTextBox.Name = colBeniACNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = "{0:n2}"
        colDecimal.HeaderText = "Amount (Rs)"
        colDecimal.Name = colNeftAmt
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Remark"
        colTextBox.Name = colRemarks
        colTextBox.Width = 150
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.BestFitColumns(BestFitColumnMode.AllCells)


    End Sub
    Sub Load_Blank_Grid_NEW_For_MP()
        gv.Rows.Clear()

        gv.Columns.Clear()
        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "MP CODE"
        colTextBox.Name = colMPcode_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = "{0:n2}"
        colDecimal.HeaderText = "Amount (Rs)"
        colDecimal.Name = colNeftAmt_For_MP
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Value Date"
        colTextBox.Name = colNeftValueDate_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDER IFSC"
        colTextBox.Name = colSenderIfsc_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDING CUSTOMER A/C NO"
        colTextBox.Name = colSCACNO_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDING CUSTOMER A/C NAME"
        colTextBox.Name = colSCAName_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY IFSC"
        colTextBox.Name = colBenifIFSC_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY A/C NO"
        colTextBox.Name = colBeniACNO_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY A/C NAME"
        colTextBox.Name = colBeniACName_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY BANK NAME"
        colTextBox.Name = colBeniBankName_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY BANK BRANCH"
        colTextBox.Name = colBeniBankBranch_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VLC UPLOADER CODE"
        colTextBox.Name = colVlcUpladerCode_For_MP
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.BestFitColumns(BestFitColumnMode.AllCells)


    End Sub

    Sub Load_New_Uploader_Data_For_Mp(DocNo As String)
        Try
            Load_Blank_Grid_NEW_For_MP()
            Dim Query As String = Nothing


            Query = "select TSPL_MP_PAY_PROCESS_DETAIL.farmer_code,TSPL_MP_PAY_PROCESS_DETAIL.farmer_Name,TSPL_MP_PAY_PROCESS_DETAIL.payable_Amount AS [Amount (Rs)]," &
                   " convert(varchar(15),TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Value Date],tspl_bank_Branch_master.IFSC_Code as [SENDER IFSC],CAST(TSPL_BANK_MASTER.BANKACCNUMBER AS nvarchar(30)) as [SENDING CUSTOMER A/C NO]," &
                    "  TSPL_COMPANY_MASTER.Comp_Name AS [SENDING CUSTOMER A/C NAME],TSPL_VENDOR_MASTER.IFSC_Code AS [BENEFICIARY IFSC], TSPL_VENDOR_MASTER.Account_No AS [BENEFICIARY A/C NO]," &
                   "  TSPL_VENDOR_MASTER.Vendor_Name AS [BENEFICIARY A/C NAME], TSPL_VENDOR_MASTER.Bank_Name AS [BENEFICIARY BANK NAME]," &
                   " TSPL_VENDOR_MASTER.Branch_Name AS [BENEFICIARY BANK BRANCH],TSPL_MP_PAY_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC UPLOADER CODE] from TSPL_PAYMENT_PROCESS_HEAD " &
                   " left join TSPL_MP_PAY_PROCESS_DETAIL on TSPL_MP_PAY_PROCESS_DETAIL.Doc_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No " &
                   " LEFT JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_MP_PAY_PROCESS_DETAIL.Bank_Code" &
                   " LEFT JOIN tspl_bank_Branch_master ON tspl_bank_Branch_master.BRANCH_CODE=TSPL_BANK_MASTER.Branch_Code " &
                    "  LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_MP_PAY_PROCESS_DETAIL.VSP_CODE  LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_PROCESS_HEAD.Comp_Code " &
                    " where 1=1  and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + DocNo + "') and TSPL_MP_PAY_PROCESS_DETAIL.payable_Amount >  0  "



            Dim dtNU As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtNU.Rows.Count > 0 Then
                Dim j As Integer = 0
                For i As Integer = 0 To dtNU.Rows.Count - 1

                    gv.Rows.AddNew()
                    gv.Rows(j).Cells(colMPcode_For_MP).Value = dtNU.Rows(i)("farmer_code").ToString()
                    gv.Rows(j).Cells(colNeftAmt_For_MP).Value = dtNU.Rows(i)("Amount (Rs)").ToString()
                    gv.Rows(j).Cells(colNeftValueDate_For_MP).Value = dtNU.Rows(i)("Value Date").ToString()
                    gv.Rows(j).Cells(colSenderIfsc_For_MP).Value = dtNU.Rows(i)("SENDER IFSC").ToString()
                    gv.Rows(j).Cells(colSCACNO_For_MP).Value = dtNU.Rows(i)("SENDING CUSTOMER A/C NO").ToString()
                    gv.Rows(j).Cells(colSCAName_For_MP).Value = dtNU.Rows(i)("SENDING CUSTOMER A/C NAME").ToString()
                    gv.Rows(j).Cells(colBenifIFSC_For_MP).Value = dtNU.Rows(i)("BENEFICIARY IFSC").ToString()

                    gv.Rows(j).Cells(colBeniACNO_For_MP).Value = dtNU.Rows(i)("BENEFICIARY A/C NO").ToString()
                    gv.Rows(j).Cells(colBeniACName_For_MP).Value = dtNU.Rows(i)("BENEFICIARY A/C NAME").ToString()

                    gv.Rows(j).Cells(colBeniBankName_For_MP).Value = dtNU.Rows(i)("BENEFICIARY BANK NAME").ToString()
                    gv.Rows(j).Cells(colBeniBankBranch_For_MP).Value = dtNU.Rows(i)("BENEFICIARY BANK BRANCH").ToString()

                    gv.Rows(j).Cells(colVlcUpladerCode_For_MP).Value = dtNU.Rows(i)("VLC UPLOADER CODE").ToString()
                    j = j + 1
                Next

            End If
            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            gv.SummaryRowsBottom.Clear()

            For iii As Integer = 0 To gv.Columns.Count - 1
                If TypeOf (gv.Columns(iii)) Is GridViewDecimalColumn Then
                    summaryRowItem.Add(New GridViewSummaryItem(gv.Columns(iii).Name, "{0:F2}", GridAggregateFunction.Sum))
                End If
            Next
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
            gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Load_Blank_Grid_ForVendor()
        gv.Rows.Clear()

        gv.Columns.Clear()

        colDecimal = New GridViewDecimalColumn()
        colDecimal.FormatString = "{0:n2}"
        colDecimal.HeaderText = "INVOICE AMOUNT (Rs)"
        colDecimal.Name = colNeftAmt
        colDecimal.Width = 80
        colDecimal.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colDecimal)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "Value Date"
        colTextBox.Name = colNeftValueDate
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDER IFSC"
        colTextBox.Name = colSenderIfsc
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDING CUSTOMER A/C NO"
        colTextBox.Name = colSCACNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "SENDING CUSTOMER A/C NAME"
        colTextBox.Name = colSCAName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY IFSC"
        colTextBox.Name = colBenifIFSC
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY A/C NO"
        colTextBox.Name = colBeniACNO
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)


        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY A/C NAME"
        colTextBox.Name = colBeniACName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY BANK NAME"
        colTextBox.Name = colBeniBankName
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "BENEFICIARY BANK BRANCH"
        colTextBox.Name = colBeniBankBranch
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        colTextBox = New GridViewTextBoxColumn()
        colTextBox.FormatString = ""
        colTextBox.HeaderText = "VENDOR CODE"
        colTextBox.Name = colVendorCode
        colTextBox.Width = 80
        colTextBox.ReadOnly = True
        gv.MasterTemplate.Columns.Add(colTextBox)

        gv.AllowAddNewRow = False
        gv.AllowDeleteRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.BestFitColumns(BestFitColumnMode.AllCells)


    End Sub
    Sub LoadData(DocNo As String)
        Try
            loadblankGrid()
            Dim frm As New FrmPaymentProcess
            frm.PayProcessDocNo = fndDocNo.Value
            frm.LoadDataFromOtherForm()
            Dim j As Integer = 0
            If frm.gv IsNot Nothing AndAlso frm.gv.Rows.Count > 0 Then
                For i As Integer = 0 To frm.gv.Rows.Count - 1
                    If isPaymentModeNEFT(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayMode).Value) OrElse isPaymentModeTransfer(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayMode).Value) OrElse isPaymentModeRTGS(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayMode).Value) Then
                        '' done by Panch Raj on 21-05-2017
                        If clsCommon.myCdbl(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPaybleAmt).Value) <= 0 Then
                            Continue For
                        End If

                        gv.Rows.AddNew()
                        '                gv.Rows(j).Cells(colSlno).Value = (j + 1)
                        gv.Rows(j).Cells(ColComp).Value = If(clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal, "KWALITY", objCommonVar.CurrentCompanyName)
                        gv.Rows(j).Cells(ColComp1).Value = If(clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal, "KWALITY", objCommonVar.CurrentCompanyName)
                        gv.Rows(j).Cells(ColDOCUMENTREF).Value = txtNEFTUploaderREFNo.Text
                        gv.Rows(j).Cells(ColDOCUMENTREF1).Value = txtNEFTUploaderREFNo.Text
                        gv.Rows(j).Cells(ColAMOUNT).Value = clsCommon.myFormat(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPaybleAmt).Value)
                        gv.Rows(j).Cells(ColAcNo).Value = getAccountNo(frm.gv.Rows(i).Cells(FrmPaymentProcess.colBankCode).Value)
                        If isPaymentModeTransfer(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayMode).Value) Then
                            gv.Rows(j).Cells(ColTransferType).Value = "M"
                            gv.Rows(j).Cells(ColIFSCCODE).Value = "998" 'frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayeeJointIFSC).Value
                        ElseIf isPaymentModeNEFT(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayMode).Value) OrElse isPaymentModeRTGS(frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayMode).Value) Then
                            gv.Rows(j).Cells(ColTransferType).Value = "I"
                            gv.Rows(j).Cells(ColIFSCCODE).Value = frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayeeJointIFSC).Value
                        End If
                        gv.Rows(j).Cells(ColPAYEENAME).Value = frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayeeJointName).Value
                        gv.Rows(j).Cells(ColPAYEENAME1).Value = frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayeeJointName).Value
                        gv.Rows(j).Cells(ColPAYEEACNO).Value = frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayeeJointAcNo).Value

                        gv.Rows(j).Cells(ColACVSPCode).Value = frm.gv.Rows(i).Cells(FrmPaymentProcess.colVendorCode).Value

                        If clsCommon.myLen(frm.gv.Rows(i).Cells(FrmPaymentProcess.colVLCUploaderCode).Value) > 0 Then
                            'Dim VLCCode As String = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader ='" & frm.gv.Rows(i).Cells(FrmPaymentProcess.colVLCUploaderCode).Value & "' and TSPL_VLC_MASTER_HEAD.VSP_Code='" & gv.Rows(j).Cells(ColACVSPCode).Value & "'")
                            gv.Rows(j).Cells(ColADVLCCode).Value = frm.gv.Rows(i).Cells(FrmPaymentProcess.colVLCUploaderCode).Value 'VLCCode
                        End If

                        j = j + 1
                    End If
                Next
            End If
            frm = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    '=====================Added by preeti gupta====================
    Sub LoadDataFarmer(DocNo As String)
        Try
            loadblankGrid(True)
            Dim frm As New frmPaymentProcessFarmer
            frm.PayProcessDocNo = fndDocNo.Value
            frm.LoadDataFromOtherForm()
            Dim j As Integer = 0
            If frm.gvPaymentToFarmer IsNot Nothing AndAlso frm.gvPaymentToFarmer.Rows.Count > 0 Then
                For i As Integer = 0 To frm.gvPaymentToFarmer.Rows.Count - 1
                    If isPaymentModeNEFT(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayMode).Value) OrElse isPaymentModeTransfer(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayMode).Value) OrElse isPaymentModeRTGS(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayMode).Value) Then
                        If clsCommon.myCdbl(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPaybleAmt).Value) <= 0 Then
                            Continue For
                        End If
                        gv.Rows.AddNew()
                        '                gv.Rows(j).Cells(colSlno).Value = (j + 1)
                        '' changed by panch Raj against Ticket No:KDI/09/04/18-000205 for Kwality
                        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "KL") = CompairStringResult.Equal Then
                            gv.Rows(j).Cells(ColComp).Value = "Kwality".ToUpper()
                            gv.Rows(j).Cells(ColComp1).Value = "Kwality".ToUpper()
                        Else
                            gv.Rows(j).Cells(ColComp).Value = objCommonVar.CurrentCompanyName
                            gv.Rows(j).Cells(ColComp1).Value = objCommonVar.CurrentCompanyName
                        End If

                        gv.Rows(j).Cells(ColDOCUMENTREF).Value = txtNEFTUploaderREFNo.Text
                        gv.Rows(j).Cells(ColDOCUMENTREF1).Value = txtNEFTUploaderREFNo.Text
                        gv.Rows(j).Cells(ColAMOUNT).Value = clsCommon.myFormat(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPaybleAmt).Value)

                        ''TO SHOW DATA FROM MP MASTER RELATED TO BANK DETAILS
                        Dim q As String = "select *   From tspl_mp_master   where 1=1 and tspl_mp_master.Mp_Code ='" & frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colFarmerCode).Value & "'"
                        Dim dtbl As DataTable = clsDBFuncationality.GetDataTable(q)
                        Dim strIFSCCode As String = String.Empty
                        If dtbl IsNot Nothing AndAlso dtbl.Rows.Count > 0 Then
                            gv.Rows(j).Cells(ColPAYEENAME).Value = clsCommon.myCstr(dtbl.Rows(0)("PayeeName"))
                            gv.Rows(j).Cells(ColPAYEENAME1).Value = clsCommon.myCstr(dtbl.Rows(0)("PayeeName"))
                            gv.Rows(j).Cells(ColPAYEEACNO).Value = clsCommon.myCstr(dtbl.Rows(0)("AccountNO"))
                            gv.Rows(j).Cells(ColBankCODEMP).Value = clsCommon.myCstr(dtbl.Rows(0)("BankName"))
                            strIFSCCode = clsCommon.myCstr(dtbl.Rows(0)("IFCICode"))
                        End If

                        gv.Rows(j).Cells(ColAcNo).Value = getAccountNo(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colBankCode).Value)
                        If isPaymentModeTransfer(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayMode).Value) Then
                            gv.Rows(j).Cells(ColTransferType).Value = "M"
                            gv.Rows(j).Cells(ColIFSCCODE).Value = "998" 'frm.gv.Rows(i).Cells(FrmPaymentProcess.colPayeeJointIFSC).Value
                        ElseIf isPaymentModeNEFT(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayMode).Value) OrElse isPaymentModeRTGS(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayMode).Value) Then
                            gv.Rows(j).Cells(ColTransferType).Value = "I"
                            'gv.Rows(j).Cells(ColIFSCCODE).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayeeJointIFSC).Value
                            gv.Rows(j).Cells(ColIFSCCODE).Value = strIFSCCode
                        End If
                        'gv.Rows(j).Cells(ColPAYEENAME).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayeeJointName).Value
                        'gv.Rows(j).Cells(ColPAYEENAME1).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayeeJointName).Value
                        'gv.Rows(j).Cells(ColPAYEEACNO).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colPayeeJointAcNo).Value

                        gv.Rows(j).Cells(ColACVSPCode).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colVendorCode).Value

                        '' commented and changed by panch raj against ticket-KDI/09/04/18-000205
                        'If clsCommon.myLen(frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colVLCUploaderCode).Value) > 0 Then
                        '    'Dim VLCCode As String = clsDBFuncationality.getSingleValue("select VLC_Code from TSPL_VLC_MASTER_HEAD where VLC_Code_VLC_Uploader ='" & frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colVLCUploaderCode).Value & "' ")
                        '    gv.Rows(j).Cells(ColADVLCCode).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colVLCUploaderCode).Value
                        'End If
                        gv.Rows(j).Cells(ColADVLCCode).Value = frm.gvPaymentToFarmer.Rows(i).Cells(frmPaymentProcessFarmer.colFarmerCode).Value

                        j = j + 1
                    End If
                Next
            End If
            frm = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub

    '===============================================================
    Public Sub Export_Excel()
        If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
            Throw New Exception("No Rows Found In Grid, Please Select one of the Payment Process Document")
        End If
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        Dim filePath As String
        sfd.FileName = Me.Text
        sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            filePath = sfd.FileName
        Else
            Exit Sub
        End If
        If New_NeftUploader.Equals(1) Then
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboType.Text, "A-Class") = CompairStringResult.Equal Then
                clsCommon.MyExportToExcelGrid("", gv, Nothing, "NEFT UPLOADER")
            Else
                transportSql.exportdata(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False, Nothing, False, True, False, True) 'frm.Text)
            End If
        Else

            transportSql.exportdata(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False, Nothing, False, True, False, True) 'frm.Text)
            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            Process.Start(filePath)
        End If
    End Sub


    Private Sub btnUploader_Click(sender As Object, e As EventArgs) Handles btnUploader.Click
        Try
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cboType.Text, "A-Class") = CompairStringResult.Equal Then
                Export_Excel()
            Else
                If clsCommon.myLen(fndDocNo.Value) <= 0 AndAlso CreateNeftuploaderPlantWise = 0 Then
                    Throw New Exception("Please select Payment Process Document")
                End If
                Export_Excel()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
        GC.WaitForPendingFinalizers()
    End Sub

    Private Sub btnUpdateRefNo_Click(sender As Object, e As EventArgs) Handles btnUpdateRefNo.Click
        Try
            If clsCommon.myLen(txtNEFTUploaderREFNo.Text) > 0 Then

                If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                    Throw New Exception("Please select Payment Process Document")
                End If
                If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
                    Throw New Exception("No Rows Found In Grid, Please Select one of the Payment Process Document")
                End If

                clsDBFuncationality.ExecuteNonQuery("Update TSPL_PAYMENT_PROCESS_HEAD set DocRefNoForUploader='" & txtNEFTUploaderREFNo.Text & "' where Doc_no='" & fndDocNo.Value & "'")
                For j As Integer = 0 To gv.Rows.Count - 1
                    gv.Rows(j).Cells(ColDOCUMENTREF).Value = txtNEFTUploaderREFNo.Text
                    gv.Rows(j).Cells(ColDOCUMENTREF1).Value = txtNEFTUploaderREFNo.Text
                Next
                clsCommon.MyMessageBoxShow(Me, "Updated Successfully", Me.Text)
            Else

                clsCommon.MyMessageBoxShow(Me, "Please Enter Document Reference No", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Payment Process"
        dr("Name") = "Payment Process"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "A-Class"
        dr("Name") = "A-Class"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.DisplayMember = "Name"
        cboType.ValueMember = "Code"

    End Sub
    Sub LoadTypePaymnetFarmer()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        dr = dt.NewRow()
        dr("Code") = "Payment VSP"
        dr("Name") = "Payment VSP"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Payment Farmer"
        dr("Name") = "Payment Farmer"
        dt.Rows.Add(dr)

        CboTypePaymentFarmer.DataSource = dt
        CboTypePaymentFarmer.DisplayMember = "Name"
        CboTypePaymentFarmer.ValueMember = "Code"

    End Sub

    Sub GetFixAmount()
        FixRTGSAmt = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyRTGSAmtMoreThanGiven, clsFixedParameterCode.ApplyRTGSAmtMoreThanGiven, Nothing))
        If clsCommon.myCdbl(FixRTGSAmt) > 0 Then
            txtAmt.Text = clsCommon.myCdbl(FixRTGSAmt)
        End If
    End Sub

    '====================Added by preeti gupta[10/05/2017]======================
#Region "User Defined Functions and Subroutines"
    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        '=========Update By Preeti Gupta===============
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptsaleRegisterReport)
        'MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            If MDI.blnShowAllMenu = False Then
                Throw New Exception("Permission Denied")
            Else
                Throw New Exception("Can't Access in demo version. " + Environment.NewLine + " For any queries/details, contact tecxpert@tecxpert.in. ")

            End If
        End If

    End Sub
#End Region


    Private Sub cboType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cboType.SelectedIndexChanged
        If clsCommon.CompairString(cboType.Text, "Payment Process") = CompairStringResult.Equal Then
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.Panel2Collapsed = False
            lblInvDate.Visible = False
            txtInvoiceDate.Visible = False
            lblToDate.Visible = False
            txtToDate.Visible = False
            lblVendorCode.Visible = False
            txtVendorCode.Visible = False
            btnGo.Visible = False
            txtVendorCode.arrValueMember = Nothing
            SplitContainer2.SplitterDistance = 134
            pnlrtgsamt.Visible = True
            BtnSameBranch.Visible = True
            btnOtherBranch.Visible = True
            reset()
            GetFixAmount()
        Else
            SplitContainer3.Panel1Collapsed = False
            SplitContainer3.Panel2Collapsed = True
            lblInvDate.Visible = True
            txtInvoiceDate.Visible = True
            lblToDate.Visible = True
            txtToDate.Visible = True
            lblVendorCode.Visible = True
            txtVendorCode.Visible = True
            btnGo.Visible = True
            txtVendorCode.arrValueMember = Nothing
            SplitContainer2.SplitterDistance = 100
            pnlrtgsamt.Visible = False
            BtnSameBranch.Visible = False
            btnOtherBranch.Visible = False

            reset()
        End If

    End Sub
    Private Sub txtVendorCode__My_Click(sender As Object, e As EventArgs) Handles txtVendorCode._My_Click
        Try
            If clsCommon.CompairString(cboType.Text, "A-Class") = CompairStringResult.Equal Then

                Dim qry As String = "select Vendor_Code as Code,Vendor_Name as [Vendor Name],ISNULL(Alies_Name,'') As [Alies Name],Add1,Add2,Add3,Closing_Date as [Closing Date],Vendor_Group_Code as [Vendor Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],City_Code as [City Code],City_Code_Desc as [City Description],State,Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person FAX],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Code Description],Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code],Payment_Code_Desc as [Payment Code Description],Bank_Code as [Bank Code],Bank_Code_Desc as [Bank Description],Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Ven_Type_Code as [Vendor Type Code],Ven_Type_Desc as [Vendor Type Description],TAX1,TAX1_Rate as [TAX1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Service_Tax_No as [Service Tax No],Tin_No as [TIN No],Lst_No as [LST No],(select case when Status='N' then 'Active' else 'In Active' end ) as Status,OnHold as [On Hold],Transporter,Remarks1,Remarks2,Additional1,Additional2,Additional3,Credit_Limit as [Credit Limit],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],CST,ECC,Range,Collectorate,PAN,Is_Gross_Receipt as [Is Gross Receipt],Inter_Branch as [Inter Branch],CURRENCY_CODE as [Currency Code],franchise_yn as [Is Franchise] from tspl_vendor_master "
                txtVendorCode.arrValueMember = clsCommon.ShowMultipleSelectForm("NEFTUPLOD", qry, "Code", "", txtVendorCode.arrValueMember, Nothing)
                'fndDocNo.Value = clsVendorMaster.getFinder("", fndDocNo.Value, isButtonClicked)

            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Try
            If clsCommon.myCdbl(txtAmt.Value) > 0 Then
                'Dim Qry As String = "update TSPL_FIXED_PARAMETER set Description=" + clsCommon.myCstr(txtAmt.Text) + " where Code='" + clsFixedParameterCode.ApplyRTGSAmtMoreThanGiven + "' and Type='" + clsFixedParameterType.ApplyRTGSAmtMoreThanGiven + "'"
                'clsDBFuncationality.ExecuteNonQuery(Qry)
                'clsCommon.MyMessageBoxShow("Updated Successfully..")
                If clsCommon.myLen(fndDocNo.Value) > 0 Then
                    If New_NeftUploader.Equals(1) Then
                        If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then
                            Load_New_Uploader_Data(fndDocNo.Value)
                        Else
                            Load_New_Uploader_Data_For_Mp(fndDocNo.Value)
                        End If

                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub BtnSameBranch_Click(sender As Object, e As EventArgs) Handles BtnSameBranch.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) >= 0 Then

                Dim Query As String = Nothing
                Query = "select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE AS [VSP CODE], TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount AS [Amount (Rs)],convert(varchar(15),TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Value Date],tspl_bank_Branch_master.IFSC_Code as [SENDER IFSC],CAST(TSPL_BANK_MASTER.BANKACCNUMBER AS nvarchar(30)) as [SENDING CUSTOMER A/C NO],TSPL_COMPANY_MASTER.Comp_Name AS [SENDING CUSTOMER A/C NAME],TSPL_VENDOR_MASTER.IFSC_Code AS [BENEFICIARY IFSC]," &
                        " TSPL_VENDOR_MASTER.Account_No AS [BENEFICIARY A/C NO],TSPL_VENDOR_MASTER.Vendor_Name AS [BENEFICIARY A/C NAME]," &
                        " TSPL_VENDOR_MASTER.Bank_Name AS [BENEFICIARY BANK NAME],TSPL_VENDOR_MASTER.Branch_Name AS [BENEFICIARY BANK BRANCH],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC UPLOADER CODE] " &
                        " from TSPL_PAYMENT_PROCESS_HEAD LEFT JOIN TSPL_PAYMENT_PROCESS_DETAIL ON TSPL_PAYMENT_PROCESS_DETAIL.Doc_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No " &
                        " LEFT JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Bank_Code " &
                        " LEFT JOIN tspl_bank_Branch_master ON tspl_bank_Branch_master.BRANCH_CODE=TSPL_BANK_MASTER.Branch_Code " &
                        " LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE " &
                        " LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_PROCESS_HEAD.Comp_Code " &
                        " where 1=1  and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + clsCommon.myCstr(fndDocNo.Value) + "') and TSPL_PAYMENT_PROCESS_DETAIL.is_Hold_Payment_Process=0 and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount >  " + clsCommon.myCstr(txtAmt.Value) + " and isnull(SUBSTRING(tspl_bank_Branch_master.IFSC_Code,1,4),'')=isnull(SUBSTRING(TSPL_VENDOR_MASTER.IFSC_Code,1,4),'')  "
                Dim dtNU As DataTable = clsDBFuncationality.GetDataTable(Query)
                If dtNU.Rows.Count <= 0 Then
                    Throw New Exception("No Data found Of same Branch")
                Else

                    Export_Same_other(dtNU, "Same Branch NEFT Uploader")
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub Export_Same_other(ByVal dt As DataTable, ByVal File_Name As String)
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = File_Name
            sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            If New_NeftUploader.Equals(1) Then
                ' Dim GRDV As New RadGridView()
                gvTemp.DataSource = Nothing
                gvTemp.DataSource = dt
                clsCommon.MyExportToExcelGrid("", gvTemp, Nothing, File_Name)
                ' transportSql.exportdata(gvTemp, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False, Nothing, False, True) 'frm.Text)
                'transportSql.ExporttoExcel(Query, Me)

            End If

            common.clsCommon.MyMessageBoxShow(Me, "Exported Successfully.", Me.Text)
            'Process.Start(filePath)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnOtherBranch_Click(sender As Object, e As EventArgs) Handles btnOtherBranch.Click
        If clsCommon.myLen(fndDocNo.Value) >= 0 Then

            Dim Query As String = Nothing
            Query = "select TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE as [VSP CODE],TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount AS [Amount (Rs)],convert(varchar(15),TSPL_PAYMENT_PROCESS_HEAD.Doc_Date,103) as [Value Date],tspl_bank_Branch_master.IFSC_Code as [SENDER IFSC],CAST(TSPL_BANK_MASTER.BANKACCNUMBER AS nvarchar(30)) as [SENDING CUSTOMER A/C NO],TSPL_COMPANY_MASTER.Comp_Name AS [SENDING CUSTOMER A/C NAME],TSPL_VENDOR_MASTER.IFSC_Code AS [BENEFICIARY IFSC]," &
                    " TSPL_VENDOR_MASTER.Account_No AS [BENEFICIARY A/C NO],TSPL_VENDOR_MASTER.Vendor_Name AS [BENEFICIARY A/C NAME]," &
                    " TSPL_VENDOR_MASTER.Bank_Name AS [BENEFICIARY BANK NAME],TSPL_VENDOR_MASTER.Branch_Name AS [BENEFICIARY BANK BRANCH],TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader as [VLC UPLOADER CODE] " &
                    " from TSPL_PAYMENT_PROCESS_HEAD LEFT JOIN TSPL_PAYMENT_PROCESS_DETAIL ON TSPL_PAYMENT_PROCESS_DETAIL.Doc_No=TSPL_PAYMENT_PROCESS_HEAD.Doc_No " &
                    " LEFT JOIN TSPL_BANK_MASTER ON TSPL_BANK_MASTER.BANK_CODE=TSPL_PAYMENT_PROCESS_DETAIL.Bank_Code " &
                    " LEFT JOIN tspl_bank_Branch_master ON tspl_bank_Branch_master.BRANCH_CODE=TSPL_BANK_MASTER.Branch_Code " &
                    " LEFT JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE " &
                    " LEFT JOIN TSPL_COMPANY_MASTER ON TSPL_COMPANY_MASTER.Comp_Code=TSPL_PAYMENT_PROCESS_HEAD.Comp_Code " &
                    " where 1=1  and TSPL_PAYMENT_PROCESS_HEAD.Doc_No in ('" + clsCommon.myCstr(fndDocNo.Value) + "') and TSPL_PAYMENT_PROCESS_DETAIL.is_Hold_Payment_Process=0 and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount >  " + clsCommon.myCstr(txtAmt.Value) + " and isnull(SUBSTRING(tspl_bank_Branch_master.IFSC_Code,1,4),'')<>isnull(SUBSTRING(TSPL_VENDOR_MASTER.IFSC_Code,1,4),'')  "
            Dim dtNU As DataTable = clsDBFuncationality.GetDataTable(Query)
            If dtNU.Rows.Count <= 0 Then
                Throw New Exception("No Rows Found In Grid, Please Select one of the Payment Process Document")
            Else
                Export_Same_other(dtNU, "Other Branch NEFT Uploader")
            End If
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try

            Load_A_Class_Data()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub CboTypePaymentFarmer_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles CboTypePaymentFarmer.SelectedIndexChanged
        If clsCommon.CompairString(cboType.Text, "Payment VSP") = CompairStringResult.Equal Then
            fndDocNo.Value = ""
        Else
            fndDocNo.Value = ""
        End If
    End Sub

    Private Sub fndBank__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBank._MYValidating
        Try
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "First Select Document No.", Me.Text)
                Return
            End If
            Dim qry As String = " select BANK_CODE as Code , DESCRIPTION as Name from TSPL_BANK_MASTER "
            fndBank.Value = clsCommon.ShowSelectForm("NEFTUploder@Bank", qry, "Code", "", fndBank.Value, "Code", isButtonClicked)
            txtBankCode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DESCRIPTION  from TSPL_BANK_MASTER where BANK_CODE = '" + fndBank.Value + "'"))
            Dim SenderBankaccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CAST(TSPL_BANK_MASTER.BANKACCNUMBER AS nvarchar(30)) as BANKACCNUMBER  from TSPL_BANK_MASTER where BANK_CODE = '" + fndBank.Value + "'"))
            If clsCommon.myLen(fndBank.Value) > 0 AndAlso gv.Rows.Count > 0 AndAlso clsCommon.myLen(fndDocNo.Value) > 0 Then
                For ii As Integer = 0 To gv.Rows.Count - 1
                    gv.Rows(ii).Cells(colDebitAccount).Value = SenderBankaccount
                    gv.Rows(ii).Cells(colSCAName).Value = txtBankCode.Text
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmNEFTUploader & "'"))
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("NEFT Uploader", gv, arrHeader, "NEFT Uploader", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Sub SetToDate()
        Try
            If AllowDateChanged Then
                If Is_Load = False Then
                    If clsCommon.myLen(fndPlantCode.Value) <= 0 Then
                        fndPlantCode.Focus()
                        Throw New Exception("Please select Plant Code First.")
                    End If
                End If
                Dim strMccCode As String = clsDBFuncationality.getSingleValue("select top 1 Location_Code from TSPL_Location_MASTER where   Location_Category = 'MCC' and TSPL_Location_MASTER.Rejected_Type = 'N'  and Loc_Segment_Code = '" + fndPlantCode.Value + "'  ")
                Dim sQuery As String = "select Pc_Type as Type,PC_VALUE as Value, case when Pc_Type='Day' then PC_VALUE when PC_Type='Month' then PC_Value * " & Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & " end " _
              & " as Pc_Value from tspl_Mcc_master inner join TSPL_PAYMENT_CYCLE_MASTER  on tspl_Mcc_master.payment_cycle=TSPL_PAYMENT_CYCLE_MASTER.PC_CODE where Mcc_code='" & strMccCode & "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Please set payment cycle in Mcc master")
                End If
                Dim strpaymentType As String = clsCommon.myCstr(dt.Rows(0)("Type"))
                Dim strPaymentTypeTag As String = clsCommon.myCdbl(dt.Rows(0)("Value"))
                'lblPaymentType.Text = clsCommon.myCstr(dt.Rows(0)("Type"))
                'lblPaymentType.Tag = clsCommon.myCdbl(dt.Rows(0)("Value"))
                If clsCommon.CompairString(strpaymentType, "Week") = CompairStringResult.Equal Then
                    AllowDateChanged = False
                    txtMonth.Enabled = False
                    txtFromDatePlant.MinDate = New Date(2000, 1, 1)
                    txtFromDatePlant.MaxDate = New Date(3000, 1, 1).AddDays(-1)
                    Dim today As Date = txtFromDatePlant.Value
                    Dim dayDiff As Integer = today.DayOfWeek - IIf(strPaymentTypeTag = 1, DayOfWeek.Sunday, IIf(strPaymentTypeTag = 2, DayOfWeek.Monday, IIf(strPaymentTypeTag = 3, DayOfWeek.Tuesday, IIf(strPaymentTypeTag = 4, DayOfWeek.Wednesday, IIf(strPaymentTypeTag = 5, DayOfWeek.Thursday, IIf(strPaymentTypeTag = 6, DayOfWeek.Friday, DayOfWeek.Saturday))))))
                    txtFromDatePlant.Value = today.AddDays(-dayDiff)
                    txtToDatePlant.Value = txtFromDatePlant.Value.AddDays(6)
                    AllowDateChanged = True
                Else
                    txtMonth.Enabled = True
                    Dim PaymentCycleValue As Integer = dt.Rows(0)("Pc_Value")
                    If txtFromDatePlant.Value.Day Mod PaymentCycleValue <> 1 And (Not PaymentCycleValue = 1) Then
                        AllowDateChanged = False
                        clsCommon.MyMessageBoxShow("Invalid date.Date should be multiple of " & clsCommon.myCstr(PaymentCycleValue) & " + 1 ")
                        txtFromDatePlant.Value = txtFromDatePlant.MinDate
                        txtFromDatePlant.Text = txtFromDatePlant.MinDate
                        AllowDateChanged = True
                    End If
                    txtToDatePlant.Value = txtFromDatePlant.Value.AddDays(PaymentCycleValue - 1)
                    If txtFromDatePlant.Value.Month <> txtToDatePlant.Value.Month Then
                        txtToDatePlant.Value = New Date(txtFromDatePlant.Value.Year, txtFromDatePlant.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                    Dim dtNxtPay As DateTime = txtToDatePlant.Value.AddDays(Math.Ceiling(PaymentCycleValue / 2.0))
                    If txtFromDatePlant.Value.Month <> dtNxtPay.Month Then
                        txtToDatePlant.Value = New Date(txtFromDatePlant.Value.Year, txtFromDatePlant.Value.Month, 1).AddMonths(1).AddDays(-1)
                    End If
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMonth_ValueChanged(sender As Object, e As EventArgs) Handles txtMonth.ValueChanged
        Try
            AllowDateChanged = False
            txtFromDatePlant.MinDate = "01-Jan-0001"
            txtFromDatePlant.MaxDate = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtFromDatePlant.MinDate = "01-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            txtToDatePlant.Value = Date.DaysInMonth(txtMonth.Value.Year, txtMonth.Value.Month) & "-" & txtMonth.Value.Month & "-" & txtMonth.Value.Year
            AllowDateChanged = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtFromDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtFromDatePlant.Validating
        SetToDate()
    End Sub

    Private Sub txtFromDate_ValueChanged(sender As Object, e As EventArgs) Handles txtFromDatePlant.ValueChanged
        SetToDate()
    End Sub



    Private Sub fndPlantCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPlantCode._MYValidating
        Try
            Dim qry As String = " select distinct Loc_Segment_Code as PlantCode, TSPL_GL_SEGMENT_CODE.description as [PlantName] from TSPL_Location_MASTER inner join TSPL_GL_SEGMENT_CODE on TSPL_Location_MASTER.Loc_Segment_Code = TSPL_GL_SEGMENT_CODE.Segment_code  and TSPL_Location_MASTER.Rejected_Type = 'N' and TSPL_Location_MASTER.Location_Category='MCC'  "
            fndPlantCode.Value = clsCommon.ShowSelectForm("NEFTUPLODER@PLANTCode@Finder221", qry, "PlantCode", "", fndPlantCode.Value, "", isButtonClicked)
            SetToDate()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles btnGoForPlantwise.Click
        Try
            Dim qry As String = "select Count (Doc_No) from TSPL_PAYMENT_PROCESS_HEAD where Loc_Seg_Code = '" + fndPlantCode.Value + "' and Convert(date, From_Date,103) >= CONVERT (date, '" + txtFromDatePlant.Value + "',103) and Convert(date, To_Date,103) <= Convert(date,'" + txtToDatePlant.Value + "',103)"
            Dim qryDocNocode As String = "select Doc_No from TSPL_PAYMENT_PROCESS_HEAD where Loc_Seg_Code = '" + fndPlantCode.Value + "' and Convert(date, From_Date,103) >= CONVERT (date, '" + txtFromDatePlant.Value + "',103) and Convert(date, To_Date,103) <= Convert(date,'" + txtToDatePlant.Value + "',103)"
            Dim isDocumentNoExist As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(qry))
            If isDocumentNoExist = True Then
                Load_New_Uploader_Data(qryDocNocode)
            Else
                clsCommon.MyMessageBoxShow(Me, "Record not Found.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItemVSP.Click
        Try
            ''VSP Wise
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                fndDocNo.Focus()
                Throw New Exception("Please select Document No")
            End If
            If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
                Throw New Exception("No Rows Found In Grid, Please Select one of the Payment Process Document")
            End If

            Dim Qry As String = "select TSPL_COMPANY_MASTER.Comp_Name,
TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as Comp_address,
 TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount 
from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
where TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + fndDocNo.Value + "'  and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount>0 order by TSPL_Vendor_MASTER.Bank_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "NEFTUploaderVLC", "VSP Wise NEFT Uploader")
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles RadMenuItemBank.Click
        Try
            ''Bank Wise
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                fndDocNo.Focus()
                Throw New Exception("Please select Document No")
            End If
            If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
                Throw New Exception("No Rows Found In Grid, Please Select one of the Payment Process Document")
            End If

            Dim Qry As String = "select ROW_NUMBER() over (Partition by max(Doc_No) order by max(Doc_No) desc) as SNO,max(Comp_Name) as Comp_Name,max(Comp_address) as Comp_address,max(Doc_No) as Doc_No,max(Date_Range) as Date_Range,count(VLC_CODE_Uploader) as NoOFVLC,Bank_Code,max(Bank_Code_Desc) as Bank_Code_Desc,sum(Payable_Amount) as Payable_Amount from ("
            If clsCommon.CompairString(CboTypePaymentFarmer.Text, "Payment VSP") = CompairStringResult.Equal Then
                Qry += "select TSPL_COMPANY_MASTER.Comp_Name,
TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as Comp_address,
TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_PAYMENT_PROCESS_DETAIL.VLC_CODE_Uploader,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Name,TSPL_Vendor_MASTER.Bank_Code,TSPL_Vendor_MASTER.Bank_Code as Bank_Code_Desc,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_IFSC_Code,TSPL_PAYMENT_PROCESS_DETAIL.Payee_Joint_Account_No,TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount 
from TSPL_PAYMENT_PROCESS_DETAIL 
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_PAYMENT_PROCESS_DETAIL.Doc_No
left outer join TSPL_Vendor_MASTER on TSPL_Vendor_MASTER.Vendor_Code=TSPL_PAYMENT_PROCESS_DETAIL.VSP_CODE
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
where TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + fndDocNo.Value + "'  and TSPL_PAYMENT_PROCESS_DETAIL.Payable_Amount>0 "
            Else
                Qry += "select TSPL_COMPANY_MASTER.Comp_Name,
TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as Comp_address,
 TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_MP_MASTER.MP_Code, TSPL_MP_MASTER.MP_Code_VLC_Uploader as VLC_CODE_Uploader,TSPL_MP_MASTER.PayeeName as Payee_Joint_Name,TSPL_MP_MASTER.BankName as Bank_Code,TSPL_MP_MASTER.BankName as Bank_Code_Desc,TSPL_MP_MASTER.IFCICode as Payee_Joint_IFSC_Code,TSPL_MP_MASTER.AccountNO as Payee_Joint_Account_No,TSPL_MP_PAY_PROCESS_DETAIL.Payable_Amount 
from TSPL_MP_PAY_PROCESS_DETAIL 
left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_MP_PAY_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
where TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + fndDocNo.Value + "'  and TSPL_MP_PAY_PROCESS_DETAIL.Payable_Amount>0 "
            End If

            Qry += ")x
group by Bank_Code
order by Bank_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "NEFTUploaderBank", "Bank Wise NEFT Uploader")
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItemMP_Click(sender As Object, e As EventArgs) Handles RadMenuItemMP.Click
        Try
            ''MP Wise
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                fndDocNo.Focus()
                Throw New Exception("Please select Document No")
            End If
            If gv Is Nothing OrElse gv.Rows.Count <= 0 Then
                Throw New Exception("No Rows Found In Grid, Please Select one of the Payment Process Document")
            End If

            Dim Qry As String = "select TSPL_COMPANY_MASTER.Comp_Name,
TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end  + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as Comp_address,
 TSPL_PAYMENT_PROCESS_HEAD.Doc_No,convert(varchar, TSPL_PAYMENT_PROCESS_HEAD.From_Date,103) +' To '+ convert(varchar,TSPL_PAYMENT_PROCESS_HEAD.To_Date,103) as Date_Range, TSPL_MP_MASTER.MP_Code, TSPL_MP_MASTER.MP_Code_VLC_Uploader as VLC_CODE_Uploader,TSPL_MP_MASTER.PayeeName as Payee_Joint_Name,TSPL_MP_MASTER.BankName as Bank_Code,TSPL_MP_MASTER.BankName as Bank_Code_Desc,TSPL_MP_MASTER.IFCICode as Payee_Joint_IFSC_Code,TSPL_MP_MASTER.AccountNO as Payee_Joint_Account_No,TSPL_MP_PAY_PROCESS_DETAIL.Payable_Amount 
from TSPL_MP_PAY_PROCESS_DETAIL 
left outer join TSPL_MP_MASTER on TSPL_MP_MASTER.MP_Code=TSPL_MP_PAY_PROCESS_DETAIL.Farmer_Code
left outer join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=TSPL_MP_PAY_PROCESS_DETAIL.Doc_No
left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code='" + objCommonVar.CurrentCompanyCode + "'
where TSPL_PAYMENT_PROCESS_HEAD.Doc_No='" + fndDocNo.Value + "'  and TSPL_MP_PAY_PROCESS_DETAIL.Payable_Amount>0 
order by TSPL_MP_MASTER.BankName"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "NEFTUploaderMP", "MP Wise NEFT Uploader")
            frmCRV = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
