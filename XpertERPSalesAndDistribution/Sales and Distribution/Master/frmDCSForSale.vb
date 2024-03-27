Imports System.Data.SqlClient
Imports common

Public Class frmDCSforSale
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim str As String
    'Dim Qry As String
    'Public Property import As Object
    'Public Property Export As Object

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'If btnSave.Visible = True Then
        '    import.Enabled = True
        '    Export.Enabled = True
        'Else
        '    import.Enabled = False
        '    Export.Enabled = False
        'End If
        '--------------------------------------------------
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub frmDCSforSale_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        funreset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
        'ToolTipcity.SetToolTip(btnNew, "New")
        btnSave.Enabled = True
        btnDelete.Enabled = True
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            txtLocation.Value = clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Location_Master where Location_Code in(" + clsCommon.myCstr(objCommonVar.strCurrUserLocations) + ")") 'clsCommon.myCstr(objCommonVar.strCurrUserLocations)
            lblLocationDesc.Text = clsLocation.GetName(txtLocation.Value, Nothing)
            txtCustomer.Enabled = True
        End If

    End Sub

    Public Sub SaveData()
        'Dim trans As SqlTransaction = Nothing
        Try
            If AllowToSave() Then

                'If MyBase.isModifyonPasswordFlag Then
                '    If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmProfitCenter, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                '    Else
                '        Return
                '    End If
                'End If
                'trans = clsDBFuncationality.GetTransactin()
                Dim obj As New ClsDCSforSale()
                obj.Code = txtCode.Value
                obj.Name = txtName.Text
                obj.Uploader_No = txtUpload.Text
                obj.Zone = txtZone.Value
                obj.Location = txtLocation.Value
                obj.Customer = txtCustomer.Value
                If chkActive.Checked = True Then
                    obj.Active = 1
                Else
                    obj.Active = 0
                End If


                If (obj.SaveData(obj, isNewEntry)) Then

                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            'trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtName.Text) <= 0 Then
            myMessages.blankValue(Me, "DCS Name", Me.Text)
            txtName.Focus()
            Return False
        ElseIf clsCommon.myLen(txtUpload.Text) <= 0 Then
            myMessages.blankValue(Me, "Upload", Me.Text)
            txtUpload.Focus()
            Return False
        ElseIf clsCommon.myLen(txtZone.Value) <= 0 Then
            myMessages.blankValue(Me, "Zone", Me.Text)
            txtZone.Focus()
            Return False
        ElseIf clsCommon.myLen(txtLocation.Value) <= 0 Then
            myMessages.blankValue(Me, "Location", Me.Text)
            txtLocation.Focus()
            Return False
        ElseIf clsCommon.myLen(txtCustomer.Value) <= 0 Then
            myMessages.blankValue(Me, "Customer", Me.Text)
            txtCustomer.Focus()
            Return False
        End If
        Return True
    End Function

    'Delete funtion call on delete button
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Public Sub DeleteData()
        If btnDelete.Enabled = False Then
            Exit Sub
        End If
        If txtCode.Value = "" Then
            myMessages.blankValue(Me, txtCode.MyLinkLable1.Text, Me.Text)
            txtCode.Focus()
        ElseIf myMessages.deleteConfirm() Then
            If (ClsDCSforSale.DeleteData(txtCode.Value)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                funreset()
            End If
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funreset()
    End Sub
    'It will reset all the controls in screens
    Public Sub funreset()
        txtCode.Value = ""
        txtCode.MyReadOnly = False
        isNewEntry = True
        txtName.Text = ""
        txtUpload.Text = ""
        txtZone.Value = ""
        lblZoneDesc.Text = ""
        btnSave.Text = "Save"
        'txtLocation.Value = ""
        'lblLocationDesc.Text = ""
        txtCustomer.Value = ""
        lblCustomerDesc.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = True
        chkActive.Checked = False
    End Sub


    Public Sub closeform()
        Me.Close()
    End Sub

    Private Sub frmDCSforSale_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                                                             "TSPL_DCS_FOR_SALE")
        End If
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Try

            If txtCode.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = ""
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrClas += "  Location in (" + objCommonVar.strCurrUserLocations + ")"
                End If
                Dim qry As String = " select Code as [Code],Name as [Name],Uploader_No as [Uploader No],Zone as [Zone],Location as [Location],Customer as [Customer],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date] from TSPL_DCS_FOR_SALE  "

                LoadData(clsCommon.ShowSelectForm("RPTBDMST", qry, "Code", whrClas, txtCode.Value, "Code", isButtonClicked), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim whrClas As String = " 2=2"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas += " and Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            Dim qst As String = "select count(*) from TSPL_DCS_FOR_SALE where " + whrClas + " "
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
                LoadData(txtCode.Value, NavType)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            txtCode.MyReadOnly = True
            btnSave.Enabled = True
            btnDelete.Enabled = True
            isNewEntry = False
            Dim obj As New ClsDCSforSale()
            obj = ClsDCSforSale.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                funreset()
                isNewEntry = False
                btnSave.Text = "Update"
                txtCode.Value = obj.Code
                txtName.Text = obj.Name
                txtZone.Value = obj.Zone
                txtUpload.Text = obj.Uploader_No
                txtLocation.Value = obj.Location
                txtCustomer.Value = obj.Customer
                lblLocationDesc.Text = obj.Location_Name
                lblCustomerDesc.Text = obj.Customer_Name
                lblZoneDesc.Text = obj.Zone_Name
                If obj.Active = 1 Then
                    chkActive.Checked = True
                Else
                    chkActive.Checked = False
                End If

            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

#Region "Import/Export"

    Public Sub Import()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Name", "Uploader_No", "Zone", "Location", "Customer") Then
            'Dim trans As SqlTransaction = Nothing

            Dim linno As Integer = 0
            Dim TempNewRecord As Boolean = False
            Try
                'trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim obj As New ClsDCSforSale
                Dim strCode As String = ""
                Dim strName As String = ""
                Dim strUploader_No As String = ""
                Dim strZone As String = ""
                Dim strLocation As String = ""
                Dim strCustomer As String = ""

                For Each grow As GridViewRowInfo In gv.Rows
                    linno += 1


                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Name").Value))) Then
                        Throw New Exception("Name Cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        obj.Name = clsCommon.myCstr(grow.Cells("Name").Value)
                    End If



                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Zone").Value))) Then
                        Throw New Exception("Zone cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Zone_Code as Code from TSPL_ZONE_MASTER where Zone_Code='" + clsCommon.myCstr(grow.Cells("Zone").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Zone").Value)) = CompairStringResult.Equal Then
                            obj.Zone = clsCommon.myCstr(grow.Cells("Zone").Value)

                        Else
                            Throw New Exception("Zone Not Exists." + clsCommon.myCstr(linno) + ".")

                        End If
                        obj.Zone = clsCommon.myCstr(grow.Cells("Zone").Value)
                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Location").Value))) Then
                        Throw New Exception("Location cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_LOCATION_MASTER where Location_Code='" + clsCommon.myCstr(grow.Cells("Location").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Location").Value)) = CompairStringResult.Equal Then
                            obj.Location = clsCommon.myCstr(grow.Cells("Location").Value)

                        Else
                            Throw New Exception("Location Not Exists." + clsCommon.myCstr(linno) + ".")

                        End If
                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Customer").Value))) Then
                        Throw New Exception("Customer cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Code from TSPL_CUSTOMER_LOCATION_MAPPING where Customer_Code='" + clsCommon.myCstr(grow.Cells("Customer").Value) + "' and Location_Code='" + clsCommon.myCstr(grow.Cells("Location").Value) + "'"))
                        If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Customer").Value)) = CompairStringResult.Equal Then
                            obj.Customer = clsCommon.myCstr(grow.Cells("Customer").Value)

                        Else
                            Throw New Exception("Customer Not Exists." + clsCommon.myCstr(linno) + ".")

                        End If

                    End If
                    If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Uploader_No").Value))) Then
                        Throw New Exception("Uploader cannot be empty" + clsCommon.myCstr(linno) + ".")
                    Else
                        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_DCS_FOR_SALE WHERE Uploader_No='" + clsCommon.myCstr(grow.Cells("Uploader_No").Value) + "' and Zone='" + clsCommon.myCstr(grow.Cells("Zone").Value) + "' and Customer='" + clsCommon.myCstr(grow.Cells("Customer").Value) + "'"))
                        If count <= 0 Then
                            obj.Uploader_No = clsCommon.myCstr(grow.Cells("Uploader_No").Value)
                        Else
                            Throw New Exception("Uploader No. already Exists at Line No." + clsCommon.myCstr(linno) + ".")

                        End If

                    End If



                    Dim count1 As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from TSPL_DCS_FOR_SALE WHERE code='" + clsCommon.myCstr(grow.Cells(0).Value) + "'"))
                    If count1 <= 0 Then
                        obj.SaveData(obj, True)
                    Else
                        obj.SaveData(obj, False)
                    End If
                Next


                'trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
                'myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub
    Public Sub Export()
        Try
            Dim str As String = "select TSPL_DCS_FOR_SALE.Name As [Name], TSPL_DCS_FOR_SALE.Uploader_No As [Uploader_No], TSPL_DCS_FOR_SALE.Zone As [Zone], TSPL_DCS_FOR_SALE.Location As [Location], TSPL_DCS_FOR_SALE.Customer As [Customer] from TSPL_DCS_FOR_SALE"
            Dim WhrCls As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += " and Location in (" + objCommonVar.strCurrUserLocations + ")"
            End If

            ListImpExpColumnsMandatory = New List(Of String)({"Name", "Uploader_No", "Zone", "Location", "Customer"})
            transportSql.ExporttoExcel(str, WhrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles rdmenuimport.Click 'RadMenuItem2.Click '
        Import()
    End Sub

    Private Sub RadMenuItem3_Click(sender As Object, e As EventArgs) Handles rdmenuexport.Click 'RadMenuItem3.Click 
        Export()
    End Sub

    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub MyLabel7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub MyLabel6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtZone__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtZone._MYValidating
        Try
            Dim qry As String = "select Zone_Code as Code from TSPL_ZONE_MASTER"

            txtZone.Value = clsCommon.ShowSelectForm("DSZoneFinder", qry, "Code", "", txtZone.Value, "", isButtonClicked)
            lblZoneDesc.Text = clsCommon.myCstr(ClsZoneMaster.GetName(txtZone.Value))


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            txtCustomer.Enabled = False
            txtCustomer.Value = ""
            lblCustomerDesc.Text = ""
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                txtCustomer.Enabled = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomer._MYValidating
        Try
            Dim qry As String = "select Customer_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_LOCATION_MAPPING"

            Dim WhrCls As String = ""
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                WhrCls = " Location_code='" & txtLocation.Value & "' "
            Else
                WhrCls = "  TSPL_CUSTOMER_MASTER.Status='N' "
            End If


            txtCustomer.Value = clsCommon.ShowSelectForm("CustomerFnder1", qry, "Code", WhrCls, txtCustomer.Value, "Code", isButtonClicked)
            lblCustomerDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'"))


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub





    'Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
    '    Dim qry As String = " select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
    '    Dim whrcls As String = " Location_Type='Physical'  "
    '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '        whrcls += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
    '    End If

    '    txtLocation.Value = clsCommon.ShowSelectForm("Location_MasterFD", qry, "Code", whrcls, txtLocation.Value, "Code", isButtonClicked)
    '    lbllocation.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code= '" + txtLocation.Value + "' "))
    'End Sub


#End Region
End Class