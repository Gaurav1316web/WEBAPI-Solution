'' work done agaist ticket no. BHA/16/07/18-000175 by parteek
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.Net.Mail
Imports System.Net
Imports System.IO
Imports XpertERPEngine

Public Class FrmRFQ
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Const colLineNo As String = "SNO"
    Const colVCode As String = "ICODE"
    Const colVName As String = "INAME"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
#End Region

    Private Sub FrmRFQ_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        funreset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D to Delete Transaction")
        ButtonToolTip.SetToolTip(btnSave, "Press Ctrl+S for Save")
        ButtonToolTip.SetToolTip(btnPost, "Press Ctrl+P for Post")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 30
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Vendor Code"
        repoTextBox.Name = colVCode
        repoTextBox.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoTextBox.TextImageRelation = TextImageRelation.TextBeforeImage
        repoTextBox.Width = 200
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Vendor Name"
        repoTextBox.Name = colVName
        repoTextBox.Width = 300
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

         

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False

    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colVCode) Then
                        If e.Column Is gv1.Columns(colVCode) Then
                            OpenVendorList(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenVendorList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(alies_name,'') As [Alies Name] from TSPL_VENDOR_MASTER"
        gv1.CurrentRow.Cells(colVCode).Value = clsCommon.ShowSelectForm("PRVendofnd", qry, "Code", " TSPL_VENDOR_MASTER.Status='N' and isnull(form_type,'ALL')='ALL' ", clsCommon.myCstr(gv1.CurrentRow.Cells(colVCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(colVName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_VENDOR_MASTER where Vendor_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colVCode).Value) + "'"))
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
            End If
        End If
    End Sub

    Function AllowToSave() As Boolean
        Try
            '= KUNAL => TICKET : BM00000009580 ===============================================
            If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
                txtDate.Focus()
                Return False
            End If

            If clsCommon.myLen(txtReqNo.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select Requision No.")
                txtReqNo.Focus()
                txtReqNo.Select()
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsRFQ()
                obj.RFQ_No = txtRFQNo.Value
                obj.RFQ_Date = txtDate.Value
                obj.Requisition_Id = txtReqNo.Value
                obj.Arr = New List(Of clsRFQDetails)
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colVCode).Value) > 0 Then
                        Dim objTr As New clsRFQDetails()
                        objTr.Vendor_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colVCode).Value)
                        objTr.arrItem = TryCast(gv1.Rows(ii).Tag, ArrayList)
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow("Please Fill at list one Vendor")
                    Return False
                End If
                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                LoadData(obj.RFQ_No, NavigatorType.Current)
                Return isSaved
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                SavingData(True)
                If (clsRFQ.PostData(txtRFQNo.Value, True)) Then
                    CreateEmailContent(txtRFQNo.Value, Nothing)
                    common.clsCommon.MyMessageBoxShow("Successfully Posted")
                    LoadData(txtRFQNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub CreateEmailContent(ByVal RFQNo As String, ByVal trans As SqlTransaction)

        Dim Form_ID As String = clsUserMgtCode.RFQ
        Dim ItemDesc As String = ""
        Dim ItemCode As String = ""
        'Dim Vendor_Code As String = ""
        'Dim Vendor_Name As String = ""
        Dim Qty As Decimal = 0
        Dim strPath As String = ""


        Dim dtContent As DataTable = clsDBFuncationality.GetDataTable("SELECT SMS_Text,Email_Text,Email_subject from TSPL_ES_Content where Form_ID='" + Form_ID + "'", trans)
        If dtContent IsNot Nothing AndAlso dtContent.Rows.Count > 0 Then
            ',TSPL_RFQ_DETAIL_ITEM.Item_Code,TSPL_ITEM_MASTER.Item_Desc
            Dim qry As String = "select distinct  TSPL_REQUISITION_DETAIL.Requisition_Id,TSPL_VENDOR_MASTER.Email,TSPL_RFQ_DETAIL_ITEM.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name from TSPL_RFQ_HEAD inner join TSPL_RFQ_DETAIL_ITEM on TSPL_RFQ_DETAIL_ITEM.RFQ_NO=TSPL_RFQ_HEAD.RFQ_NO"
            qry += " left outer join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_DETAIL.Requisition_Id=TSPL_RFQ_HEAD.Requisition_Id"
            qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RFQ_DETAIL_ITEM.Item_Code"
            qry += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER .Vendor_Code=TSPL_RFQ_DETAIL_ITEM.Vendor_Code "
            qry += " where 2=2  and TSPL_RFQ_DETAIL_ITEM.RFQ_NO='" + RFQNo + "' "

            Dim dtParty As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            'Vendor_Code = clsCommon.myCstr(dtParty.Rows(0)("Vendor_Code"))
            'Vendor_Name = clsCommon.myCstr(dtParty.Rows(0)("Vendor_Name"))
            Dim frmCRViewer As New frmCrystalReportViewer()
            If clsCommon.myLen(dtContent.Rows(0)("Email_Text")) > 0 Then
                If dtParty IsNot Nothing AndAlso dtParty.Rows.Count > 0 Then

                    For Each dr As DataRow In dtParty.Rows
                        Dim objSMSH As New clsEMailHead()
                        objSMSH.Email_Subject = clsCommon.myCstr(dtContent.Rows(0)("Email_subject"))
                        objSMSH.Email_Text = clsCommon.myCstr(dtContent.Rows(0)("Email_Text"))


                        '' Email Print Qry
                        Dim qry1 As String = "select TSPL_REQUISITION_DETAIL.Item_Cost ,TSPL_REQUISITION_DETAIL.Item_Net_Amt  as Amount ,TSPL_REQUISITION_HEAD.Requisition_Id, convert(varchar,TSPL_RFQ_HEAD.RFQ_Date,103) as Requisition_Date , " & _
                  "convert(varchar,TSPL_REQUISITION_HEAD.Expire_Date,103) as Expire_Date ,convert(varchar,TSPL_REQUISITION_HEAD.Require_Date,103) as Require_Date , " & _
                  "TSPL_REQUISITION_HEAD.Ref_No ,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Request_By , " & _
                  "TSPL_REQUISITION_DETAIL.Item_Code ,TSPL_REQUISITION_DETAIL.Item_Desc,TSPL_REQUISITION_DETAIL.Specification,TSPL_REQUISITION_DETAIL.Capacity,TSPL_REQUISITION_DETAIL.Make,TSPL_REQUISITION_DETAIL.Model,Category = TSPL_REQUISITION_HEAD.Category+case when TSPL_REQUISITION_HEAD.emergency>0 then ' [Emergency]' else '' end,Item_Detail= TSPL_REQUISITION_DETAIL.Item_Desc+ case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ' [Spec:'+TSPL_REQUISITION_DETAIL.Specification else '' END + case when len(TSPL_REQUISITION_DETAIL.Remarks)>0 then ', Remarks:'+TSPL_REQUISITION_DETAIL.Remarks else '' END +case when len(TSPL_REQUISITION_DETAIL.Capacity)>0 then ', Capacity:'+TSPL_REQUISITION_DETAIL.Capacity else '' END +case when len(TSPL_REQUISITION_DETAIL.Make)>0 then ', Make:'+TSPL_REQUISITION_DETAIL.Make else '' END  +case when len(TSPL_REQUISITION_DETAIL.Model)>0 then ', Model:'+TSPL_REQUISITION_DETAIL.Model else '' END + case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ']' else '' end, " & _
                  "ItemDesc_Detail= " & _
                  "TSPL_REQUISITION_DETAIL.Item_Desc+ case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ' [Spec:'+TSPL_REQUISITION_DETAIL.Specification else '' END + case when len(TSPL_REQUISITION_DETAIL.Remarks)>0 then ', Remarks:'+TSPL_REQUISITION_DETAIL.Remarks else '' END +case when len(TSPL_REQUISITION_DETAIL.Capacity)>0 then ', Capacity:'+TSPL_REQUISITION_DETAIL.Capacity else '' END +case when len(TSPL_REQUISITION_DETAIL.Make)>0 then ', Make:'+TSPL_REQUISITION_DETAIL.Make else '' END  +case when len(TSPL_REQUISITION_DETAIL.Model)>0 then ', Model:'+TSPL_REQUISITION_DETAIL.Model else '' END + case when len(TSPL_REQUISITION_DETAIL.Specification)>0 then ']' else '' end, " & _
                  "TSPL_REQUISITION_DETAIL.Remarks as DRemarks ,TSPL_REQUISITION_DETAIL.Unit_Code ,TSPL_REQUISITION_DETAIL.Requisition_Qty, " & _
                  "isnull((select SUM( case when InOut='I' then Qty else  -1* Qty end )from TSPL_INVENTORY_MOVEMENT where Item_Code=TSPL_REQUISITION_DETAIL.Item_Code and TSPL_INVENTORY_MOVEMENT.Location_Code=TSPL_REQUISITION_HEAD.Location),0) as AvaiQty  , " & _
                  "TSPL_VENDOR_MASTER.Vendor_Name,TSPL_REQUISITION_HEAD.Comments ,TSPL_COMPANY_MASTER.Comp_Name ,TSPL_COMPANY_MASTER.Logo_Img , " & _
                  "TSPL_COMPANY_MASTER.Logo_Img2,user1.User_Name as CreatedBy,case when TSPL_REQUISITION_HEAD.status=1 then TSPL_REQUISITION_HEAD.modify_by else '' end as AuthorizeBy,case when TSPL_REQUISITION_HEAD.status=1 then convert(varchar,TSPL_REQUISITION_HEAD.Posting_Date,103) else '' end as AuthorizeDate ,TSPL_REQUISITION_HEAD.Request_By, " & _
                  "TSPL_REQUISITION_HEAD.Require_Date,TSPL_REQUISITION_HEAD.Dept_Desc,TSPL_REQUISITION_HEAD.Location , " & _
                  "TSPL_COMPANY_MASTER.Add1,case when  is_internal ='Y' then 'MATERIAL REQUISITION' else 'PURCHASE INDENT' END AS Heading ,isnull(TSPL_ITEM_MASTER.HSN_Code,'') as HSN_Code " & _
                  "from TSPL_REQUISITION_HEAD join TSPL_REQUISITION_DETAIL on TSPL_REQUISITION_HEAD.Requisition_Id =TSPL_REQUISITION_DETAIL.Requisition_Id left outer join TSPL_COMPANY_MASTER on  TSPL_REQUISITION_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer join TSPL_USER_MASTER as user1 on TSPL_REQUISITION_HEAD.Created_By=user1.User_Code left outer join TSPL_USER_MASTER as user2 on TSPL_REQUISITION_HEAD.Modify_By=user2.User_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_REQUISITION_DETAIL.Item_Code left outer join TSPL_RFQ_HEAD on TSPL_RFQ_HEAD.Requisition_Id=TSPL_REQUISITION_HEAD.Requisition_Id " & _
                  " left join TSPL_RFQ_DETAIL_ITEM on TSPL_RFQ_DETAIL_ITEM.Item_Code=TSPL_REQUISITION_DETAIL.Item_Code and TSPL_RFQ_DETAIL_ITEM.RFQ_NO=TSPL_RFQ_HEAD.RFQ_NO " & _
                  " left outer join TSPL_VENDOR_MASTER on TSPL_RFQ_DETAIL_ITEM.Vendor_Code =TSPL_VENDOR_MASTER.Vendor_Code " & _
                  " where(2 = 2)"

                        If txtReqNo.Value <> "" Then
                            qry1 += " and  TSPL_REQUISITION_HEAD.Requisition_Id='" + clsCommon.myCstr(dtParty.Rows(0)("Requisition_Id")) + "'"
                        End If
                        qry1 += " and  TSPL_RFQ_DETAIL_ITEM.Vendor_Code='" + clsCommon.myCstr(dr("Vendor_Code")) + "'"

                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry1)


                        objSMSH.Attachment_1_Path = frmCRViewer.EmailAttachment(CrystalReportFolder.PurchaseOrder, dt, "PurchaseRequisition", "Purchase Requisition")

                       

                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Code, clsCommon.myCstr(dr("Vendor_Code")))
                        objSMSH.Email_Text = objSMSH.Email_Text.Replace(XpertERPEngine.frmEMailAndSMSSetting.Vendor_Name, clsCommon.myCstr(dr("Vendor_Name")))

                        objSMSH.arrEMail = New List(Of String)()
                        objSMSH.arrEMail.Add(clsCommon.myCstr(dr("Email")))
                        objSMSH.SaveData(Form_ID, objSMSH, trans)
                        objSMSH = Nothing
                    Next
                End If
            End If
        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            Dim obj As New clsRFQ()
            obj = clsRFQ.GetData(strCode, NavTyep)
            funreset()
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.RFQ_No) > 0) Then
                isNewEntry = False
                If obj.Is_Post = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    btnSave.Enabled = True
                    btnSave.Text = "Update"
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If

                txtDate.Value = obj.RFQ_Date
                txtReqNo.Value = obj.Requisition_Id
                txtRFQNo.Value = obj.RFQ_No
                funRequisition(txtReqNo.Value)
                For Each objtr As clsRFQDetails In obj.Arr
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVCode).Value = objtr.Vendor_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVName).Value = objtr.Vendor_Name
                    gv1.Rows(gv1.Rows.Count - 1).Tag = objtr.arrItem
                    gv1.Rows.AddNew()
                Next
            End If
        Catch ex As Exception
            isNewEntry = True
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
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
                If (clsRFQ.DeleteData(txtRFQNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funreset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtRFQNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtRFQNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtRFQNo._MYNavigator
        LoadData(txtRFQNo.Value, NavType)
    End Sub

    Private Sub txtRFQNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRFQNo._MYValidating
        Dim qry As String = "select TSPL_RFQ_HEAD.RFQ_NO as [Code],TSPL_RFQ_HEAD.RFQ_Date as [Date],  TSPL_REQUISITION_HEAD.Requisition_Id as [Requisition Id], TSPL_REQUISITION_HEAD.Requisition_Date as [Requisition Date]," & _
                            "TSPL_REQUISITION_HEAD.Description as [Description], TSPL_REQUISITION_HEAD.Remarks as [Remarks],TSPL_REQUISITION_HEAD.Comments as [Comments]," & _
                            "TSPL_REQUISITION_HEAD.Item_Type as [Indent Type],TSPL_REQUISITION_HEAD.Requisition_Type as [Type],TSPL_REQUISITION_HEAD.Request_By as [Request By]," & _
                            "TSPL_REQUISITION_HEAD.Expire_Date as [Expire Date],TSPL_REQUISITION_HEAD.Require_Date as [Date Required],TSPL_REQUISITION_HEAD.Cust_OrderNo as [Customer Order No]," & _
                            "TSPL_REQUISITION_HEAD.Mode_Of_Transport as [Mode of Transport],TSPL_REQUISITION_HEAD.Location as [Location],TSPL_REQUISITION_HEAD.Dept as [Department Id],TSPL_REQUISITION_HEAD.Dept_Desc as [Department Description]," & _
                            "TSPL_REQUISITION_HEAD.PROJECT_ID as [Project Id],TSPL_REQUISITION_HEAD.Ref_No as [Reference No]" & _
                            "from TSPL_REQUISITION_HEAD   inner join TSPL_RFQ_HEAD on TSPL_RFQ_HEAD.Requisition_Id = TSPL_REQUISITION_HEAD.Requisition_Id"
        Dim whr As String = ""
        LoadData(clsCommon.ShowSelectForm("RFQ", qry, "Code", "", txtReqNo.Value, "", isButtonClicked), NavigatorType.Current)
    End Sub

    Private Sub txtReqNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtReqNo._MYValidating
        Dim qry As String = "select TSPL_REQUISITION_HEAD.Requisition_Id as [Code], TSPL_REQUISITION_HEAD.Requisition_Date as [Requisition Date]," & _
                            " TSPL_REQUISITION_HEAD.Description as [Description], TSPL_REQUISITION_HEAD.Remarks as [Remarks],TSPL_REQUISITION_HEAD.Comments as [Comments]," & _
                            " TSPL_REQUISITION_HEAD.Item_Type as [Indent Type],TSPL_REQUISITION_HEAD.Requisition_Type as [Type],TSPL_REQUISITION_HEAD.Request_By as [Request By]," & _
                            " TSPL_REQUISITION_HEAD.Expire_Date as [Expire Date],TSPL_REQUISITION_HEAD.Require_Date as [Date Required],TSPL_REQUISITION_HEAD.Cust_OrderNo as [Customer Order No]," & _
                            " TSPL_REQUISITION_HEAD.Mode_Of_Transport as [Mode of Transport],TSPL_REQUISITION_HEAD.Location as [Location],TSPL_REQUISITION_HEAD.Dept as [Department Id],TSPL_REQUISITION_HEAD.Dept_Desc as [Department Description]," & _
                            " TSPL_REQUISITION_HEAD.PROJECT_ID as [Project Id],TSPL_REQUISITION_HEAD.Ref_No as [Reference No]," & _
                            " case when TSPL_REQUISITION_HEAD.Level5_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level5_Approval_By else case when TSPL_REQUISITION_HEAD.Level4_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level4_Approval_By else case when TSPL_REQUISITION_HEAD.Level3_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level3_Approval_By else case when TSPL_REQUISITION_HEAD.Level2_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level2_Approval_By else case when TSPL_REQUISITION_HEAD.Level1_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level1_Approval_By else '' end  end end end end as [Approval By]," & _
                            " case when TSPL_REQUISITION_HEAD.Level5_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level5_Approval_On else case when TSPL_REQUISITION_HEAD.Level4_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level4_Approval_On else case when TSPL_REQUISITION_HEAD.Level3_Approval_On=1 then TSPL_REQUISITION_HEAD.Level3_Approval_By else case when TSPL_REQUISITION_HEAD.Level2_Approval_On=1 then TSPL_REQUISITION_HEAD.Level2_Approval_By else case when TSPL_REQUISITION_HEAD.Level1_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level1_Approval_On else null end  end end end end as [Approval Date]," & _
                            " TSPL_REQUISITION_HEAD.Total_RQ_Amt as [Requisition Amount] from TSPL_REQUISITION_HEAD  "
        Dim whrcals As String = "Requisition_Id  not in (select Requisition_Id from TSPL_RFQ_HEAD union all select distinct isnull(TSPL_PURCHASE_ORDER_DETAIL.Requisition_Id,'') from TSPL_PURCHASE_ORDER_DETAIL LEFT OUTER JOIN TSPL_PURCHASE_ORDER_HEAD ON TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No = TSPL_PURCHASE_ORDER_DETAIL.PurchaseOrder_No where isnull(TSPL_PURCHASE_ORDER_HEAD.IsCancel,0)<>1 ) and Status=1"
        txtReqNo.Value = clsCommon.ShowSelectForm("REQ", qry, "Code", whrcals, txtReqNo.Value, "", isButtonClicked)
        LoadBlankGrid()
        If (clsCommon.myLen(txtReqNo.Value) > 0) Then
            Dim dt As New DataTable()
            dt = clsDBFuncationality.GetDataTable("select Vendor_Code, Item_Code from TSPL_REQUISITION_DETAIL where TSPL_REQUISITION_DETAIL.Requisition_Id='" + clsCommon.myCstr(txtReqNo.Value) + "' and len(isnull( vendor_code,''))>0 ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVCode).Value = clsCommon.myCstr(dr("Vendor_Code"))
                    Dim arrlst As New ArrayList
                    arrlst.Add(clsCommon.myCstr(dr("Item_Code")))
                    gv1.Rows(gv1.Rows.Count - 1).Tag = arrlst
                Next
            End If
            funRequisition(txtReqNo.Value)
        End If
        gv1.Rows.AddNew()
    End Sub

    Sub funRequisition(ByVal Requisition_ID As String)
        Dim qry As String = "select Requisition_Date ,case when TSPL_REQUISITION_HEAD.Level5_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level5_Approval_By else case when TSPL_REQUISITION_HEAD.Level4_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level4_Approval_By else case when TSPL_REQUISITION_HEAD.Level3_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level3_Approval_By else case when TSPL_REQUISITION_HEAD.Level2_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level2_Approval_By else case when TSPL_REQUISITION_HEAD.Level1_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level1_Approval_By else '' end  end end end end as Approval_by,"
        qry += " case when TSPL_REQUISITION_HEAD.Level5_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level5_Approval_On else case when TSPL_REQUISITION_HEAD.Level4_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level4_Approval_On else case when TSPL_REQUISITION_HEAD.Level3_Approval_On=1 then TSPL_REQUISITION_HEAD.Level3_Approval_By else case when TSPL_REQUISITION_HEAD.Level2_Approval_On=1 then TSPL_REQUISITION_HEAD.Level2_Approval_By else case when TSPL_REQUISITION_HEAD.Level1_Approval_Status=1 then TSPL_REQUISITION_HEAD.Level1_Approval_On else null end  end end end end as Approval_Date,Total_RQ_Amt,TSPL_REQUISITION_HEAD.Description,TSPL_REQUISITION_HEAD.Remarks,TSPL_REQUISITION_HEAD.Comments from TSPL_REQUISITION_HEAD where Requisition_Id ='" + Requisition_ID + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtreqDate.Value = clsCommon.myCstr(dt.Rows(0)("Requisition_Date"))
            txtLastAppBy.Text = clsCommon.myCstr(dt.Rows(0)("Approval_by"))
            If clsCommon.myLen(dt.Rows(0)("Approval_Date")) > 0 Then
                txtLastAppDate.Value = clsCommon.myCstr(dt.Rows(0)("Approval_Date"))
            Else
                txtLastAppDate.Value = Nothing
            End If
            txtAmount.Text = clsCommon.myCstr(dt.Rows(0)("Total_RQ_Amt"))

            txtDesc.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
            txtRmks.Text = clsCommon.myCstr(dt.Rows(0)("Remarks"))
            txtComment.Text = clsCommon.myCstr(dt.Rows(0)("Comments"))
        Else
            txtAmount.Text = "0.00"
            txtLastAppBy.Text = ""
            txtLastAppDate.Value = clsCommon.GETSERVERDATE()
            txtreqDate.Value = clsCommon.GETSERVERDATE()
            txtDesc.Text = ""
            txtRmks.Text = ""
            txtComment.Text = ""
        End If
    End Sub

    Sub funreset()
        txtreqDate.Value = Date.Today
        txtDate.Value = Date.Today
        txtLastAppDate.Value = Date.Today
        txtReqNo.Value = ""
        txtRFQNo.Value = ""
        txtAmount.Text = "0.00"
        txtLastAppBy.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False
        txtDesc.Text = ""
        txtRmks.Text = ""
        txtComment.Text = ""
        RadReqDetails.Enabled = False
        isNewEntry = True
        UsLock1.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub

    Private Sub FrmRFQ_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
            'Add Tool tip Task No- TEC/22/05/18-000245
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                         "TSPL_RFQ_HEAD " + Environment.NewLine + _
                                         "TSPL_RFQ_DETAIL ")
            'Add Tool tip Task No- TEC/22/05/18-000245
        End If
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        funreset()
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            If clsCommon.myLen(txtReqNo.Value) > 0 Then
                Dim arr As ArrayList = TryCast(gv1.CurrentRow.Tag, ArrayList)
                Dim qry As String = "select Item_Code,Item_Desc,Requisition_Qty,Unit_Code,Item_Cost from TSPL_REQUISITION_DETAIL where Requisition_Id='" + txtReqNo.Value + "'"
                gv1.CurrentRow.Tag = clsCommon.ShowMultipleSelectForm("RFGItem", qry, "Item_Code", "", arr, Nothing)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
End Class
