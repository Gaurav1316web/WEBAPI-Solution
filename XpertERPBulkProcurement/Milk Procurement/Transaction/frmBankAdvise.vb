Imports common
Public Class frmBankAdvise
#Region "Variables"
    Dim IsBankAdviseStartDate As String
    Dim IsSeprateBankForDCSBankAdvice As String
    Dim ApplyPartialBankAdvise As Boolean = False
    Const colDCSCode As String = "colDCSCode"
    Const ColDCSName As String = "ColDCSName"
    Const ColPPDetailNo As String = "ColPPDetailNo"
    Const colBalanceAmt As String = "colBalanceAmt"
    Const colPartialAmt As String = "colPartialAmt"
    Private isCellValueChangedOpen As Boolean = False
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub frmBankAdvise_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            Reset()
            ApplyPartialBankAdvise = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ApplyPartialBankAdvise, clsFixedParameterCode.ApplyPartialBankAdvise, Nothing)) = "1", True, False))
            IsBankAdviseStartDate = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.BankAdviseRequired, clsFixedParameterCode.BankAdviseRequired, Nothing))
            IsSeprateBankForDCSBankAdvice = clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.SeprateBankForDCSBankAdvice, clsFixedParameterCode.SeprateBankForDCSBankAdvice, Nothing))
            If ApplyPartialBankAdvise Then
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Visible
            Else
                RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
            End If
            'createTable()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim TxtBoxCol As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Code"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = colDCSCode
        TxtBoxCol.IsVisible = True
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "DCS Name"
        TxtBoxCol.Width = 120
        TxtBoxCol.Name = ColDCSName
        TxtBoxCol.Width = 200
        TxtBoxCol.IsVisible = True
        TxtBoxCol.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        TxtBoxCol = New GridViewTextBoxColumn()
        TxtBoxCol.FormatString = ""
        TxtBoxCol.HeaderText = "PP Detail No"
        TxtBoxCol.Name = ColPPDetailNo
        TxtBoxCol.IsVisible = False
        TxtBoxCol.Width = 120
        TxtBoxCol.ReadOnly = True
        TxtBoxCol.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(TxtBoxCol)

        Dim repoBalanceAmt = New GridViewDecimalColumn()
        repoBalanceAmt.FormatString = "{0:n2}"
        repoBalanceAmt.HeaderText = "Balance Amt"
        repoBalanceAmt.Name = colBalanceAmt
        repoBalanceAmt.Width = 150
        repoBalanceAmt.Minimum = 0
        repoBalanceAmt.ReadOnly = True
        repoBalanceAmt.Width = 100
        repoBalanceAmt.ShowUpDownButtons = False
        repoBalanceAmt.DecimalPlaces = 2
        repoBalanceAmt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoBalanceAmt)

        Dim repoPartialAmt = New GridViewDecimalColumn()
        repoPartialAmt.FormatString = "{0:n2}"
        repoPartialAmt.HeaderText = "Partial Amt"
        repoPartialAmt.Name = colPartialAmt
        repoPartialAmt.Width = 150
        repoPartialAmt.Minimum = 0
        repoPartialAmt.Width = 100
        repoPartialAmt.ShowUpDownButtons = False
        repoPartialAmt.DecimalPlaces = 2
        repoPartialAmt.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoPartialAmt)

        gv1.EnableFiltering = True
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.BestFitColumns()
    End Sub
    'Sub createTable()
    '    Dim coll As Dictionary(Of String, String)
    '    coll = New Dictionary(Of String, String)()
    '    coll.Add("Document_Code", "varchar(30) references TSPL_BANK_ADVISE(Document_No)")
    '    coll.Add("Bank", "varchar(50) Null")
    '    coll.Add("Bank_Email_ID", "varchar(50) Null")
    '    clsCommonFunctionality.CreateOrAlterTable(False, "TSPL_BANK_ADVISE_SEND_EMAIL", coll, "", True)
    'End Sub


    Private Sub fndDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDocNo._MYValidating
        Try
            Dim Qry As String = "Select Document_No As  Code, Document_Date As [Document Date],Case When Status ='' Then 'Pending' Else 'Approved' End As [Status] from TSPL_BANK_ADVISE"
            fndDocNo.Value = clsCommon.ShowSelectForm("fndDocNo", Qry, "Code", "", fndDocNo.Value, "TSPL_BANK_ADVISE.Document_No", isButtonClicked, "Document_Date")
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                LoadData(fndDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndDocNo._MYNavigator
        Try
            LoadData(fndDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndPaymentProcessNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPaymentProcessNo._MYValidating
        Try
            If ApplyPartialBankAdvise Then
                LoadPPPartialDocumentFinder(fndPaymentProcessNo.Value, isButtonClicked)
            Else
                fndPaymentProcessNo.Value = clsPaymentProcessHead.getFinder("FarmType='PP' And TSPL_PAYMENT_PROCESS_HEAD.isPrePosted=1 And TSPL_PAYMENT_PROCESS_HEAD.Doc_No Not In (Select Payment_Process_Document_No from TSPL_BANK_ADVISE)", fndPaymentProcessNo.Value, isButtonClicked)
            End If
            If clsCommon.myLen(fndPaymentProcessNo.Value) > 0 Then
                LoadDataPaymentProcessDetails(fndPaymentProcessNo.Value, False, "")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadPPPartialDocumentFinder(ByVal curcode As String, ByVal isButtonClicked As Boolean)
        Try
            Dim obj As New clsBankAdvise()
            Dim str As String = obj.GetPaymentProcessDCSWiseDetails("", False, "", False)
            Dim qry As String = ""
            If (clsCommon.myCDecimal(clsFixedParameter.GetData(clsFixedParameterType.ShowMCCFinderInPaymentProcess, clsFixedParameterCode.ShowMCCFinderInPaymentProcess, Nothing)) = 1) = True Then
                qry = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [Plant Code],TSPL_GL_SEGMENT_CODE.description as [Plant Name], isnull (TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected,'') as [MCC Code]  , isnull (TSPL_MCC_MASTER.MCC_NAME,'') as [MCC Name] ,TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code as AreaCode,AreaMaster.Location_Desc as AreaName,TSPL_PAYMENT_PROCESS_HEAD.Location_Code_Prefix as [Location Code Prefix],TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPrePosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount] " &
                "from  (
                 SELECT [Document Code] FROM ( "
                qry += str
                qry += " ) xxx  group by [Document Code] )XXXX Left Outer Join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No=xxxx.[Document Code] "
                qry += " left outer join TSPL_LOCATION_MASTER as AreaMaster on AreaMaster.Location_Code=TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code
left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code 
left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code " &
                " left Outer join TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_PAYMENT_PROCESS_HEAD.MCC_Code_Selected   "
            Else
                qry = " select TSPL_PAYMENT_PROCESS_HEAD.Doc_No as [DocumentNo] ,TSPL_PAYMENT_PROCESS_HEAD.Doc_Date as [Doc Date] ,TSPL_PAYMENT_PROCESS_HEAD.From_Date as [From Date] ,TSPL_PAYMENT_PROCESS_HEAD.To_Date as [To Date] ,TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code as [MCC Code],TSPL_GL_SEGMENT_CODE.description as [MCC Name],TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code as AreaCode,AreaMaster.Location_Desc as AreaName ,TSPL_PAYMENT_PROCESS_HEAD.Created_By as [Created By] ,TSPL_PAYMENT_PROCESS_HEAD.Created_Date as [Created Date] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_By as [Modified By] ,TSPL_PAYMENT_PROCESS_HEAD.Modified_Date as [Modified Date] ,TSPL_PAYMENT_PROCESS_HEAD.Comp_Code as [Comp Code] ,case when isnull(TSPL_PAYMENT_PROCESS_HEAD.isPrePosted,0)=0 then 'NO' else 'YES' end as [Posting Status] ,TSPL_PAYMENT_PROCESS_HEAD.Posting_Date as [Posting Date],TSPL_PAYMENT_PROCESS_HEAD.DocRefNoForUploader as [NEFT Uploader Ref No],PMode.Payment_Mode as [Payment Mode],PMode.Payable_Amount as [Payable Amount] 
From (  
SELECT [Document Code] FROM ( "
                qry += str
                qry += " ) xxx  group by [Document Code] )XXXX Left Outer Join TSPL_PAYMENT_PROCESS_HEAD On TSPL_PAYMENT_PROCESS_HEAD.Doc_No=xxxx.[Document Code] "
                qry += " left outer join TSPL_LOCATION_MASTER as AreaMaster on AreaMaster.Location_Code=TSPL_PAYMENT_PROCESS_HEAD.Area_Location_Code
left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_PAYMENT_PROCESS_HEAD.Loc_Seg_Code 
left join (select Doc_No as PP_Code,Max(Payment_Mode) as Payment_Mode,sum(Payable_Amount) as Payable_Amount  from TSPL_PAYMENT_PROCESS_DETAIL group by Doc_No) PMode on TSPL_PAYMENT_PROCESS_HEAD.Doc_No=PMode.PP_Code  "
            End If
            fndPaymentProcessNo.Value = clsCommon.ShowSelectForm("fndDCSPPBnAdv", qry, "DocumentNo", "", curcode, "DocumentNo", isButtonClicked, "Doc_Date")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtPPMultDCS__My_Click(sender As Object, e As EventArgs) Handles txtPPMultDCS._My_Click
        Try
            Dim Qry As String = clsBankAdvise.paymentProcessDetails(fndPaymentProcessNo.Value)
            clsCommon.ShowMultipleSelectForm(False, "PPBA", Qry, "DCS Code", "", txtPPMultDCS.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub LoadDataPaymentProcessDetails(paymentProcessDocNo As String, ByVal isLoadData As Boolean, ByVal strDocNo As String)
        Try
            Dim dt As DataTable = New DataTable()
            Dim obj As New clsBankAdvise()
            If ApplyPartialBankAdvise Then
                dt = clsDBFuncationality.GetDataTable(obj.GetPaymentProcessDCSWiseDetails(paymentProcessDocNo, isLoadData, strDocNo, False))
            Else
                dt = clsDBFuncationality.GetDataTable(clsBankAdvise.paymentProcessDetails(paymentProcessDocNo))
            End If
            If dt.Rows.Count > 0 Then
                txtPPFromDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("From Date"))
                txtPPToDate.Value = clsCommon.GetPrintDate(dt.Rows(0)("To Date"))
                txtMCC.Text = clsCommon.myCstr(dt.Rows(0)("MCC Code"))
                txtPPArea.Text = clsCommon.myCstr(dt.Rows(0)("Area"))
                Dim arrDCS As ArrayList = New ArrayList()
                LoadBlankGrid()
                For Each row In dt.Rows
                    arrDCS.Add(clsCommon.myCstr(row("DCS Code")))
                    If Not isLoadData Then
                        If ApplyPartialBankAdvise Then
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = clsCommon.myCstr(row("DCS Code"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSName).Value = clsCommon.myCstr(row("DCS Name"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColPPDetailNo).Value = clsCommon.myCstr(row("PP Detail No"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = clsCommon.myCDecimal(row("Balanace Amt"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPartialAmt).Value = (gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value * clsCommon.myCDecimal(txtApplyPer.Value)) / 100
                        End If
                    End If
                Next
                txtPPMultDCS.arrValueMember = arrDCS
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colPartialAmt) Then
                    If gv1.CurrentRow.Index > -1 Then
                        If clsCommon.myCDecimal(gv1.CurrentRow.Cells(colPartialAmt).Value) > clsCommon.myCDecimal(gv1.CurrentRow.Cells(colBalanceAmt).Value) Then
                            gv1.CurrentRow.Cells(colPartialAmt).Value = clsCommon.myCDecimal(gv1.CurrentRow.Cells(colBalanceAmt).Value)
                            Throw New Exception("Partial Amount cannot be greater than Balance Amount")
                        End If
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        lblPending.Status = ERPTransactionStatus.Pending
        If clsCommon.CompairString(lblPending.Status, ERPTransactionStatus.Pending) = CompairStringResult.Equal Then
            btnSendEmail.Enabled = False
        End If
        fndDocNo.Value = Nothing
        txtDocDate.Value = clsCommon.GETSERVERDATE()
        fndPaymentProcessNo.Value = Nothing
        txtMCC.Text = Nothing
        txtPPFromDate.Value = clsCommon.GETSERVERDATE()
        txtPPToDate.Value = clsCommon.GETSERVERDATE()
        txtPPMultDCS.arrValueMember = Nothing
        txtPPArea.Text = Nothing
        txtRemarks.Text = Nothing
        btnSave.Text = "Save"
        btnReverseAndUnpost.Visible = False
        txtApplyPer.Text = 100
        gv1.DataSource = Nothing
        LoadBlankGrid()
        RadPageView1.SelectedPage = RadPageViewPage1
        EnableFeilds()
    End Sub


    Private Sub LoadData(strCode As String, NavType As NavigatorType)
        Try
            Dim obj As clsBankAdvise = clsBankAdvise.GetBankAdviseData(strCode, NavType, Nothing)
            If obj IsNot Nothing Then
                Reset()
                fndDocNo.Value = obj.Document_No
                txtDocDate.Value = obj.Document_Date
                fndPaymentProcessNo.Value = obj.Payment_Process_Document_No
                If clsCommon.myLen(fndPaymentProcessNo.Value) > 0 Then
                    LoadDataPaymentProcessDetails(fndPaymentProcessNo.Value, True, obj.Document_No)
                End If
                If obj.Status > 0 Then
                    lblPending.Status = ERPTransactionStatus.Approved
                    btnPrint.Enabled = True
                    btnSendEmail.Enabled = True
                Else
                    lblPending.Status = ERPTransactionStatus.Pending
                    btnPrint.Enabled = False
                    btnSendEmail.Enabled = False
                End If
                txtRemarks.Text = obj.Remarks
                If clsCommon.CompairString(lblPending.Status, ERPTransactionStatus.Pending) = CompairStringResult.Equal Then
                    btnSave.Text = "Update"
                Else
                    DisableFeilds()
                End If
                If ApplyPartialBankAdvise Then
                    If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                        For Each objTr As clsBankAdviseDetail In obj.Arr
                            gv1.Rows.AddNew()
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = objTr.DCSCode
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDCSName).Value = objTr.DCSName
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColPPDetailNo).Value = objTr.Payment_Process_PP_Detail_No
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = objTr.Balance_Amt
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colPartialAmt).Value = objTr.Partial_Amt
                        Next
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim isNewEntry As Boolean = False
                Dim obj As clsBankAdvise = New clsBankAdvise()
                obj.Document_No = fndDocNo.Value
                obj.Document_Date = txtDocDate.Value
                obj.Payment_Process_Document_No = fndPaymentProcessNo.Value
                obj.Remarks = txtRemarks.Text
                obj.Is_Partial = IIf(ApplyPartialBankAdvise, "1", "0")
                If obj.Document_No IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0 Then
                    isNewEntry = False
                Else
                    isNewEntry = True
                End If
                obj.Arr = New List(Of clsBankAdviseDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objtr As New clsBankAdviseDetail()
                    objtr.Payment_Process_PP_Detail_No = clsCommon.myCstr(grow.Cells(ColPPDetailNo).Value)
                    objtr.Balance_Amt = clsCommon.myCDecimal(grow.Cells(colBalanceAmt).Value)
                    objtr.Partial_Amt = clsCommon.myCDecimal(grow.Cells(colPartialAmt).Value)
                    If (clsCommon.myLen(objtr.Payment_Process_PP_Detail_No) > 0) Then
                        obj.Arr.Add(objtr)
                    End If
                Next
                If clsBankAdvise.SaveData(obj, isNewEntry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_No, Nothing)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Xtra.TransactionValidity(txtDocDate.Value)

        If clsCommon.myLen(fndPaymentProcessNo.Value) < 0 Then
            clsCommon.MyMessageBoxShow(Me, "Payment Process Document No. can't be black.", Me.Text)
                Return False
        End If

        If clsCommon.myLen(txtRemarks) < 0 Then
            clsCommon.MyMessageBoxShow(Me, "Remarks can't be black.", Me.Text)
            Return False
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCDecimal(gv1.Rows(ii).Cells(colPartialAmt).Value) > clsCommon.myCDecimal(gv1.Rows(ii).Cells(colBalanceAmt).Value) Then
                clsCommon.MyMessageBoxShow(Me, "Partial Amount cannot be greater than Balance Amount", Me.Text)
                Return False
            End If
        Next
        If Not ApplyPartialBankAdvise Then
            Dim isDocExist As Boolean = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(Payment_Process_Document_No)DocNo from TSPL_BANK_ADVISE where Payment_Process_Document_No = '" & fndPaymentProcessNo.Value & "' and TSPL_BANK_ADVISE.Document_No not in ('" & fndDocNo.Value & "')")) > 0
            If isDocExist Then
                clsCommon.MyMessageBoxShow(Me, "Payment Process No is already used", Me.Text)
                Return False
            End If
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try

            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If (myMessages.deleteConfirm()) Then
                    If clsBankAdvise.deleteData(fndDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully.", Me.Text)
                        Reset()
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found.", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If (myMessages.postConfirm()) Then
                    Dim qry As String = clsBankAdvise.ChkReturnQry(fndPaymentProcessNo.Value)
                    Dim dtBank As DataTable = clsDBFuncationality.GetDataTable(qry)
                    If dtBank IsNot Nothing AndAlso dtBank.Rows.Count > 0 Then
                        For Each drBank As DataRow In dtBank.Rows
                            ''Note IF You do any changes than change in function frmVendorBankAdvice.Print(ByVal isPrint As Boolean) 
                            If clsCommon.myLen(drBank("Email")) <= 0 Then
                                Throw New Exception("Please Define email ID for bank [" + clsCommon.myCstr(drBank("Company_Bank_Current")) + "]")
                            End If
                        Next
                    End If

                    If clsBankAdvise.postData(fndDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully", Me.Text)
                        LoadData(fndDocNo.Value, Nothing)
                        DisableFeilds()
                    End If
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found to Post.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub EnableFeilds()
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
    End Sub

    Public Sub DisableFeilds()
        btnSave.Enabled = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
    End Sub

    Private Sub btnReverseAndUnpost_Click(sender As Object, e As EventArgs) Handles btnReverseAndUnpost.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    If clsBankAdvise.ReverseAndUnpost(fndDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Reverse and Unposted Successfully", Me.Text)
                        LoadData(fndDocNo.Value, Nothing)
                    End If
                End If
            Else
                    clsCommon.MyMessageBoxShow(Me, "Data Not Found to Reverse and Unpost.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmBankAdvise_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
                If MyBase.isReverse Then
                    Dim frm As New FrmPWD(Nothing)
                    frm.strType = clsFixedParameterType.SIR
                    frm.strCode = clsFixedParameterCode.SIReversAndCreate
                    frm.ShowDialog()
                    If frm.isPasswordCorrect Then
                        btnReverseAndUnpost.Visible = True
                    End If
                Else
                    clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                Dim obj As New frmVendorBankAdvice()
                obj.DocNo = clsCommon.myCstr(fndDocNo.Value)
                obj.MCC = clsCommon.myCstr(txtMCC.Text)
                obj.FormLoad()
                'obj.Print(True, fndDocNo.Value, txtMCC.Text)
            Else
                clsCommon.MyMessageBoxShow(Me, "Document code can't be blank !", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSendEmail_Click(sender As Object, e As EventArgs) Handles btnSendEmail.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) > 0 Then
                Dim obj As New frmVendorBankAdvice()
                obj.DocNo = clsCommon.myCstr(fndDocNo.Value)
                obj.MCC = clsCommon.myCstr(txtMCC.Text)
                obj.isSendMail = True
                obj.FormLoad()
                Dim BankAdviseQry As String = obj.returnBankAdviseQry
                If clsBankAdvise.SendEmail(fndDocNo.Value, BankAdviseQry) Then
                    clsCommon.MyMessageBoxShow(Me, "Email Send Successfully", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found to Send.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndDocNo.Value, "Document_No", "TSPL_BANK_ADVISE")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            If clsCommon.myCDecimal(txtApplyPer.Value) > 0 AndAlso clsCommon.myCDecimal(txtApplyPer.Value) <= 100 Then
                If gv1.Rows.Count > 0 Then
                    For Each grow As GridViewRowInfo In gv1.Rows
                        grow.Cells(colPartialAmt).Value = (grow.Cells(colBalanceAmt).Value * clsCommon.myCDecimal(txtApplyPer.Value)) / 100
                    Next
                End If
            ElseIf clsCommon.myCDecimal(txtApplyPer.Value) <= 0 Then
                Throw New Exception("Please enter Apply Percentage")
            ElseIf clsCommon.myCDecimal(txtApplyPer.Value) > 100 Then
                Throw New Exception("Apply Percentage cannot be more than 100")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class