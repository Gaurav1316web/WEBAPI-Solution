'-----------shivani
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Public Class FrmExpiryDate
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Dim isnewentry As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmExpiryDate)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub LoadScreenName()
        isInsideLoadData = True
        Dim qry As String = "select Program_Code as [Code] ,Program_Name as [Name]   from TSPL_PROGRAM_MASTER where  Program_Code in ('BOOK-PS', 'DEL-ORD-PS','SALE-ORD-PS','SHIPMENT-PS','PO-ODR')"
        cmbScreenName.DataSource = clsDBFuncationality.GetDataTable(qry)
        cmbScreenName.ValueMember = "Code"
        cmbScreenName.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub
    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(lblExpiry.Text) > 0 Then
                If lblExpiry.Text >= txtNewExpiryDate.Value Then
                    clsCommon.MyMessageBoxShow("Expiry Date should not be greater or equal to new expiry date")
                    Return False
                End If
            End If
            If clsCommon.myLen(LblDocDate.Text) > 0 Then
                If LblDocDate.Text > txtNewExpiryDate.Value Then
                    clsCommon.MyMessageBoxShow("New expiry date should be greater to document date")
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub FrmExpiryDate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadScreenName()
        AddNew()
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Adding New ")
        AddNew()
    End Sub
    Sub SaveData()
        Try
            If (AllowToSave() = True) Then

                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmExpiryDate, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If

                Dim obj As New ClsExpiryDateHistory()
                obj.Screen_Name = cmbScreenName.Text
                obj.Program_Code = cmbScreenName.SelectedValue
                obj.Document_No = FndDocumnetNo.Value
                'obj.Doc_Date = txtDocDate.Text
                'obj.Expiry_Date = txtExpiryDate.Text
                obj.Doc_Date = LblDocDate.Text
                If clsCommon.myLen(lblExpiry.Text) > 0 Then
                    obj.Expiry_Date = lblExpiry.Text
                Else
                    obj.Expiry_Date = Nothing
                End If

                obj.New_Expiry_Date = txtNewExpiryDate.Text
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Document_No) from TSPL_EXPIRY_DATE where Document_No='" + obj.Document_No + "'")
                If (qry = 0) Then
                    isnewentry = True
                Else
                    isnewentry = False
                End If
                If (ClsExpiryDateHistory.SaveData(obj, isnewentry)) Then

                    clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                Else
                    btnsave.Text = "Save"
                    btndelete.Enabled = False

                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
    Sub LoadDocCode(ByVal strcode As String)
        Try
            Dim obj As ClsExpiryDateHistory = ClsExpiryDateHistory.getdata(strcode)
            If obj IsNot Nothing Then

                cmbScreenName.Text = obj.Screen_Name
                cmbScreenName.SelectedValue = obj.Program_Code
                FndDocumnetNo.Value = obj.Document_No
                'txtDocDate.Text = obj.Doc_Date
                'txtExpiryDate.Text = obj.Expiry_Date
                LblDocDate.Text = obj.Doc_Date
                If clsCommon.myLen(obj.Expiry_Date) > 0 Then
                    lblExpiry.Text = obj.Expiry_Date
                Else
                    lblExpiry.Text = ""
                End If

                txtNewExpiryDate.Text = obj.New_Expiry_Date
                btnsave.Text = "update"
            End If
            btndelete.Enabled = True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub DeleteData()
        Try
            If clsCommon.myLen(cmbScreenName.Text) <= 0 Then
                Throw New Exception("Code not found to delete")
            End If
            If clsCommon.MyMessageBoxShow("are you sure? do you want to delete this Code ('" + cmbScreenName.Text + "')", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then

                Dim qry As String = "DELETE FROM TSPL_EXPIRY_DATE WHERE Screen_Name='" + cmbScreenName.Text + "'"
                clsDBFuncationality.ExecuteNonQuery(qry)
                clsCommon.MyMessageBoxShow("Deleted Successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            If (clsCommon.CompairString(clsCommon.myCstr(ex.Message), "Code not found to delete") <> CompairStringResult.Equal) Then
                clsCommon.MyMessageBoxShow("Current Code is in use")
            Else
                clsCommon.MyMessageBoxShow(ex.Message)
            End If
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
    Sub AddNew()
        cmbScreenName.Text = ""
        FndDocumnetNo.Value = ""
        lblExpiry.Text = ""
        LblDocDate.Text = ""
        btnsave.Text = "Save"
        LoadScreenName()
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        AddNew()
    End Sub

    Private Sub cmbScreenName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbScreenName.SelectedIndexChanged
        Dim strcode As String = cmbScreenName.Text
        If isInsideLoadData = False Then
            'AddNew()
            LoadDocCode(strcode)
        End If

    End Sub

    Private Sub txtDocumnetNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndDocumnetNo._MYValidating
        Dim ScreenCode As String = ""
        Dim TableName As String = ""
        Dim DocNo As String = ""
        If clsCommon.CompairString(cmbScreenName.SelectedValue, "PO-ODR") = CompairStringResult.Equal Then
            ScreenCode = "PO-ODR"
            TableName = "TSPL_PURCHASE_ORDER_HEAD"
            DocNo = "PurchaseOrder_No"
        ElseIf clsCommon.CompairString(cmbScreenName.SelectedValue, clsUserMgtCode.frmSalesOrderProductSale) = CompairStringResult.Equal Then
            ScreenCode = clsUserMgtCode.frmSalesOrderProductSale
            TableName = "TSPL_SD_SALES_ORDER_HEAD"
            DocNo = "Document_Code"
        ElseIf clsCommon.CompairString(cmbScreenName.SelectedValue, clsUserMgtCode.frmDeliveryPrderProductSale) = CompairStringResult.Equal Then
            ScreenCode = clsUserMgtCode.frmDeliveryPrderProductSale
            TableName = "TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE"
            DocNo = "Document_Code"
        ElseIf clsCommon.CompairString(cmbScreenName.SelectedValue, clsUserMgtCode.frmBookingProductSale) = CompairStringResult.Equal Then
            ScreenCode = clsUserMgtCode.frmBookingProductSale
            TableName = "TSPL_BOOKING_MASTER_PRODUCTSALE"
            DocNo = "Document_Code"
        End If

        Dim qry As String = "select " & DocNo & "  from " & TableName & " "
        FndDocumnetNo.Value = clsCommon.ShowSelectForm("screen", qry, DocNo, "", FndDocumnetNo.Value, DocNo, isButtonClicked)
        If clsCommon.CompairString(cmbScreenName.SelectedValue, "PO-ODR") = CompairStringResult.Equal Then
            If clsCommon.myLen(FndDocumnetNo.Value) > 0 Then
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
                lblExpiry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,Expiry_Date,103) AS Expiry_Date FROM  " & TableName & " Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
                LblDocDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,PurchaseOrder_Date,103) AS PurchaseOrder_Date  FROM  " & TableName & "  Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
                txtNewExpiryDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT CASE WHEN isnull(convert(varchar,New_Expiry_Date,103),'')='' THEN '" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy ") & "' ELSE convert(varchar,New_Expiry_Date,103) END AS New_Expiry_Date  FROM  TSPL_EXPIRY_DATE WHERE Document_No='" + FndDocumnetNo.Value + "'"))
            Else
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
            End If
        ElseIf clsCommon.CompairString(cmbScreenName.SelectedValue, clsUserMgtCode.frmSalesOrderProductSale) = CompairStringResult.Equal Then
            If clsCommon.myLen(FndDocumnetNo.Value) > 0 Then
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
                lblExpiry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,Delivery_date,103) AS Expiry_Date FROM  " & TableName & " Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
                LblDocDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,Document_Date,103) AS Document_Date  FROM  " & TableName & "  Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
            Else
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
            End If
        ElseIf clsCommon.CompairString(cmbScreenName.SelectedValue, clsUserMgtCode.frmDeliveryPrderProductSale) = CompairStringResult.Equal Then
            If clsCommon.myLen(FndDocumnetNo.Value) > 0 Then
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
                lblExpiry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,Delivery_date,103) AS Expiry_Date FROM  " & TableName & " Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
                LblDocDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,Document_Date,103) AS Document_Date  FROM  " & TableName & "  Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
            Else
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
            End If
        ElseIf clsCommon.CompairString(cmbScreenName.SelectedValue, clsUserMgtCode.frmBookingProductSale) = CompairStringResult.Equal Then
            If clsCommon.myLen(FndDocumnetNo.Value) > 0 Then
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
                lblExpiry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,bookvalidity_date,103) AS Expiry_Date FROM  " & TableName & " Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
                LblDocDate.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT convert(varchar,Document_Date,103) AS Document_Date  FROM  " & TableName & "  Where " & DocNo & "='" + FndDocumnetNo.Value + "'"))
            Else
                lblExpiry.Text = ""
                LblDocDate.Text = ""
                txtNewExpiryDate.Text = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy ")
            End If
        End If
    End Sub

    Private Sub FrmExpiryDate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            AddNew()
        End If
    End Sub
End Class
