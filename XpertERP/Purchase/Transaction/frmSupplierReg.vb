' ----------------- Created By Anubhooti On 15-Jan-2015 Against -------------------- '
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
Imports System.IO

Public Class FrmSupplierReg
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim IsComboLoad As Boolean = False
#Region "Man Power Status"
    Const ColRows As String = "Deptt."
    Const ColExec As String = "Executive"
    Const ColSkill As String = "Skilled"
    Const ColUnSkill As String = "Un skilled"
    Const ColTotal As String = "Total"
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSupplierReg)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Function AllowToSave() As Boolean
        Try
            UcAttachment1.AllowToSave()
            btnsave.Focus()
            If clsCommon.myLen(TxtSupplierName.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtSupplierName.Focus()
                Throw New Exception("Supplier name can not be left blank")
            End If
            
            If clsCommon.myLen(TxtProduct.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtProduct.Focus()
                Throw New Exception("Product can not be left blank")
            End If
            If clsCommon.myLen(TxtCategory.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtCategory.Focus()
                Throw New Exception("Category can not be left blank")
            End If
            If clsCommon.myLen(TxtEmail.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtEmail.Focus()
                Throw New Exception("Email can not be left blank")
            End If
            If clsCommon.CompairString(CmbCertification.SelectedValue, "Any Other") = CompairStringResult.Equal AndAlso clsCommon.myLen(TxtCerti.Text) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage1
                TxtCerti.Focus()
                Throw New Exception("Please specify certification.")
            End If
            If clsCommon.myLen(fndTrmsCode.Value) <= 0 Then
                Me.RadPageView1.SelectedPage = RadPageViewPage2
                fndTrmsCode.Focus()
                Throw New Exception("Terms code can not be left blank")
            End If
            If ChkToxicMat.Checked = True AndAlso UcAttachment1.gv1.Rows.Count <= 0 Then
                Me.RadPageView1.SelectedPage = Attachments
                Throw New Exception("Please make atleast one attachment")
            End If
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function
   
    Sub LoadGrid()
        gvManPS.Rows.Clear()
        gvManPS.RowCount = 5
        gvManPS.Columns.Clear()

        Dim repoRow As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRow = New GridViewTextBoxColumn()
        repoRow.FormatString = ""
        repoRow.HeaderText = "Deptt."
        repoRow.Name = ColRows
        repoRow.Width = 150
        repoRow.ReadOnly = True
        repoRow.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvManPS.MasterTemplate.Columns.Add(repoRow) '0

        Dim repoExec As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoExec = New GridViewDecimalColumn()
        repoExec.FormatString = ""
        repoExec.HeaderText = "Executive"
        repoExec.Name = ColExec
        repoExec.Width = 150
        repoExec.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvManPS.MasterTemplate.Columns.Add(repoExec) '0

        Dim repoSkill As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSkill.FormatString = ""
        repoSkill.HeaderText = "Skilled"
        repoSkill.Name = ColSkill
        repoSkill.Width = 150
        gvManPS.MasterTemplate.Columns.Add(repoSkill) '1

        Dim repoUnSkill As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnSkill.FormatString = ""
        repoUnSkill.HeaderText = "Un Skilled"
        repoUnSkill.Name = ColUnSkill
        repoUnSkill.ReadOnly = False
        repoUnSkill.Width = 150
        gvManPS.MasterTemplate.Columns.Add(repoUnSkill) '2

        Dim repoTotal As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTotal.FormatString = ""
        repoTotal.HeaderText = "Total"
        repoTotal.Name = ColTotal
        repoTotal.ReadOnly = True
        repoTotal.Width = 150
        gvManPS.MasterTemplate.Columns.Add(repoTotal) '2

        gvManPS.Rows(0).Cells(ColRows).Value = "Production" '1 Row
        gvManPS.Rows(1).Cells(ColRows).Value = "Quality Control/Assr." '2 Row
        gvManPS.Rows(2).Cells(ColRows).Value = "Total Room" '3 Row
        gvManPS.Rows(3).Cells(ColRows).Value = "Design & Engg." '4 Row
        gvManPS.Rows(4).Cells(ColRows).Value = "Others" '5 Row

        gvManPS.AllowDeleteRow = True
        gvManPS.AllowAddNewRow = False
        'gvQualification.ShowGroupPanel = False
        gvManPS.AllowColumnReorder = False
        gvManPS.AllowRowReorder = False
        gvManPS.EnableSorting = False
        gvManPS.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvManPS.MasterTemplate.ShowRowHeaderColumn = False
        'gvQualification.TableElement.TableHeaderHeight = 40
        ReStoreGridLayout()


    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvManPS.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvManPS.Columns.Count - 1 Step ii + 1
                        gvManPS.Columns(ii).IsVisible = False
                        gvManPS.Columns(ii).VisibleInColumnChooser = True
                    Next
                    'gvQualification.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
   
    Sub GirdCalculation()
        If gvManPS.RowCount > 0 Then
            gvManPS.Rows(0).Cells(ColTotal).Value = clsCommon.myCdbl(gvManPS.Rows(0).Cells(ColExec).Value + gvManPS.Rows(0).Cells(ColSkill).Value + gvManPS.Rows(0).Cells(ColUnSkill).Value)
            gvManPS.Rows(1).Cells(ColTotal).Value = clsCommon.myCdbl(gvManPS.Rows(1).Cells(ColExec).Value + gvManPS.Rows(1).Cells(ColSkill).Value + gvManPS.Rows(1).Cells(ColUnSkill).Value)
            gvManPS.Rows(2).Cells(ColTotal).Value = clsCommon.myCdbl(gvManPS.Rows(2).Cells(ColExec).Value + gvManPS.Rows(2).Cells(ColSkill).Value + gvManPS.Rows(2).Cells(ColUnSkill).Value)
            gvManPS.Rows(3).Cells(ColTotal).Value = clsCommon.myCdbl(gvManPS.Rows(3).Cells(ColExec).Value + gvManPS.Rows(3).Cells(ColSkill).Value + gvManPS.Rows(3).Cells(ColUnSkill).Value)
            gvManPS.Rows(4).Cells(ColTotal).Value = clsCommon.myCdbl(gvManPS.Rows(4).Cells(ColExec).Value + gvManPS.Rows(4).Cells(ColSkill).Value + gvManPS.Rows(4).Cells(ColUnSkill).Value)
        End If
    End Sub


    Sub funReset()
        isNewEntry = True
        txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.Focus()
        IsComboLoad = False
        LoadGrid()
        Me.RadPageView1.SelectedPage = RadPageViewPage1
        '' Personal Detail
        dtpDate.Text = clsCommon.GETSERVERDATE()
        dtpCentraExcise.Text = clsCommon.GETSERVERDATE()
        dtpSSIRegn.Text = clsCommon.GETSERVERDATE()
        dtpStateSales.Text = clsCommon.GETSERVERDATE()
        TxtSupplierName.Text = ""
        TxtSupplierAdd.Text = ""
        txtSuppAdd.Text = ""
        TxtProduct.Text = ""
        TxtCategory.Text = ""
        TxtEmail.Text = ""
        Me.txtPhoneW.Text = "(+__)__________"
        Me.TxtPhoneHO.Text = "(+__)__________"
        Me.TxtPhoneDWH.Text = "(+__)__________"
        Me.TxtPhoneAFH.Text = "(+__)__________"
        txtfaxWork.Text = ""
        TxtWorkingHWork.Text = ""
        TxtWeeklyHolidayWork.Text = ""
        TxtPhoneHO.Text = ""
        TxtFaxHO.Text = ""
        TxtWorkingHHO.Text = ""
        TxtWeeklyHolidayHO.Text = ""
        TxtNameDWH.Text = ""
        TxtDesgDWH.Text = ""
        TxtPhoneDWH.Text = ""
        TxtNameAWH.Text = ""
        TxtDesgAWH.Text = ""
        TxtPhoneAFH.Text = ""
        TxtYrofEst.Text = ""
        TxtTurnOver.Text = ""
        TxtStateSale.Text = ""
        TxtCentraExcise.Text = ""
        TxtECCNo.Text = ""
        TxtSSIRegn.Text = ""
        ChkCertified.Checked = False
        ChkToxicMat.Checked = False
        TxtCerti.Text = ""
        TxtCust1.Text = ""
        TxtCust2.Text = ""
        TxtCust3.Text = ""
        TxtCust4.Text = ""
        TxtInstalledPC.Text = ""
        TxtOwnPGC.Text = ""
        fndTrmsCode.Value = ""
        lblTermsCode.Text = ""
        TxtInspFac.Text = ""
        TxtMatrTestFacilities.Text = ""
        TxtCapacity.Text = ""
        TxtProcessCap.Text = ""
        TxtMinBatch.Text = ""
        RbtNo.IsChecked = True
        RbtYes.IsChecked = False
        TxtCustComp.Text = ""
        TxtInternalRej.Text = ""
        txtComment.Text = ""
        Me.CmbCertification.DataSource = ClsSupplierRegistration.GetSal
        Me.CmbCertification.DisplayMember = "Name"
        Me.CmbCertification.ValueMember = "Code"
        Me.CmbNatureInd.DataSource = ClsSupplierRegistration.GetNatureOfIndustry
        Me.CmbNatureInd.DisplayMember = "Name"
        Me.CmbNatureInd.ValueMember = "Code"
        'Me.cmbGender.DataSource = ClsSupplierRegistration.GetGender
        'Me.cmbGender.DisplayMember = "Name"
        'Me.cmbGender.ValueMember = "Code"
        'Me.cmbSalutation.DataSource = ClsSupplierRegistration.GetSal
        'Me.cmbSalutation.DisplayMember = "Name"
        'Me.cmbSalutation.ValueMember = "Code"
        'Me.CmbMarStatus.DataSource = ClsSupplierRegistration.GetMS
        'Me.CmbMarStatus.DisplayMember = "Name"
        'Me.CmbMarStatus.ValueMember = "Code"
        TxtCerti.Visible = False
        UsLock1.Status = ERPTransactionStatus.Pending

        '' Blank Grid
        'Me.gvQualification.Rows.Clear()
        'Me.gvQualification.Rows.AddNew()
        'Me.gvQualification.CurrentRow.Cells(ColCourseType).Value = "F"
        'Me.gvDoc.DataSource = Nothing
        'Me.gvDoc.Rows.Clear()
        'Me.gvDoc.Rows.AddNew()
        'Me.gvEmpHis.Rows.Clear()
        'Me.gvEmpHis.Rows.AddNew()
        'Me.gvFamily.Rows.Clear()
        'Me.gvFamily.Rows.AddNew()
        UcAttachment1.BlankAllControls()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        IsComboLoad = True
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsSupplierRegistration.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub



    Private Function Save(Optional ByVal isPosted As Boolean = False) As Boolean

        If AllowToSave() Then

            Dim arr As New List(Of ClsSupplierRegistration)
            Dim obj As New ClsSupplierRegistration()
            obj.Registration_No = clsCommon.myCstr(txtcode.Value)
            obj.Supplier_Name = clsCommon.myCstr(TxtSupplierName.Text)
            obj.Registration_Date = dtpDate.Text
            obj.Supplier_Address = clsCommon.myCstr(TxtSupplierAdd.Text)
            obj.Supplier_Address2 = clsCommon.myCstr(txtSuppAdd.Text)
            obj.Product = clsCommon.myCstr(TxtProduct.Text)
            obj.Category = clsCommon.myCstr(TxtCategory.Text)
            obj.Email = clsCommon.myCstr(TxtEmail.Text)
            obj.Phone_No_Work = clsCommon.myCstr(txtPhoneW.Text)
            obj.Fax_No_Work = clsCommon.myCstr(txtfaxWork.Text)
            obj.Working_Hrs_Work = clsCommon.myCstr(TxtWorkingHWork.Text)
            obj.Weekly_Holiday_Work = clsCommon.myCstr(TxtWeeklyHolidayWork.Text)
            obj.Phone_No_HO = clsCommon.myCstr(TxtPhoneHO.Text)
            obj.Fax_No_HO = clsCommon.myCstr(TxtFaxHO.Text)
            obj.Working_Hrs_HO = clsCommon.myCstr(TxtWorkingHHO.Text)
            obj.Weekly_Holiday_HO = clsCommon.myCstr(TxtWeeklyHolidayHO.Text)
            obj.Name_DWH = clsCommon.myCstr(TxtNameDWH.Text)
            obj.Desgination_DWH = clsCommon.myCstr(TxtDesgDWH.Text)
            obj.Phone_No_DWH = clsCommon.myCstr(TxtPhoneDWH.Text)
            obj.Name_AWH = clsCommon.myCstr(TxtNameAWH.Text)
            obj.Designation_AWH = clsCommon.myCstr(TxtDesgAWH.Text)
            obj.Phone_No_AWH = clsCommon.myCstr(TxtPhoneAFH.Text)
            obj.Nature_Of_Industry = clsCommon.myCstr(CmbNatureInd.SelectedValue)
            obj.Year_Of_Establishment = clsCommon.myCstr(TxtYrofEst.Text)
            obj.Turn_Over = clsCommon.myCstr(TxtTurnOver.Text)
            obj.State_Sales_Tax_No = clsCommon.myCstr(TxtStateSale.Text)
            obj.State_Sales_Tax_Date = dtpStateSales.Text
            obj.Centra_Excise_Regn_No = clsCommon.myCstr(TxtCentraExcise.Text)
            obj.Centra_Excise_Regn_Date = dtpCentraExcise.Text
            obj.ECC_No = clsCommon.myCstr(TxtECCNo.Text)
            obj.SSI_Regn_No = clsCommon.myCstr(TxtSSIRegn.Text)
            obj.SSI_Regn_No_Date = dtpSSIRegn.Text

            If ChkCertified.Checked = True Then
                obj.Is_Certified = "1"
            Else
                obj.Is_Certified = "0"
            End If
            GirdCalculation()
            obj.System_Certification = clsCommon.myCstr(CmbCertification.SelectedValue)
            obj.Other_Certification = clsCommon.myCstr(TxtCerti.Text)
            obj.Production_Exec = clsCommon.myCstr(gvManPS.Rows(0).Cells(ColExec).Value)
            obj.Production_Skilled = clsCommon.myCstr(gvManPS.Rows(0).Cells(ColSkill).Value)
            obj.Production_UnSkilled = clsCommon.myCstr(gvManPS.Rows(0).Cells(ColUnSkill).Value)
            obj.Production_Total = clsCommon.myCstr(gvManPS.Rows(0).Cells(ColTotal).Value)
            obj.QC_Exec = clsCommon.myCstr(gvManPS.Rows(1).Cells(ColExec).Value)
            obj.QC_Skilled = clsCommon.myCstr(gvManPS.Rows(1).Cells(ColSkill).Value)
            obj.QC_UnSkilled = clsCommon.myCstr(gvManPS.Rows(1).Cells(ColUnSkill).Value)
            obj.QC_Total = clsCommon.myCstr(gvManPS.Rows(1).Cells(ColTotal).Value)
            obj.Total_Room_Exec = clsCommon.myCstr(gvManPS.Rows(2).Cells(ColExec).Value)
            obj.Total_Room_Skilled = clsCommon.myCstr(gvManPS.Rows(2).Cells(ColSkill).Value)
            obj.Total_Room_UnSkilled = clsCommon.myCstr(gvManPS.Rows(2).Cells(ColUnSkill).Value)
            obj.Total_Room_Total = clsCommon.myCstr(gvManPS.Rows(2).Cells(ColTotal).Value)
            obj.DE_Exec = clsCommon.myCstr(gvManPS.Rows(3).Cells(ColExec).Value)
            obj.DE_Skilled = clsCommon.myCstr(gvManPS.Rows(3).Cells(ColSkill).Value)
            obj.DE_UnSkilled = clsCommon.myCstr(gvManPS.Rows(3).Cells(ColUnSkill).Value)
            obj.DE_Total = clsCommon.myCstr(gvManPS.Rows(3).Cells(ColTotal).Value)
            obj.Others_Exec = clsCommon.myCstr(gvManPS.Rows(4).Cells(ColExec).Value)
            obj.Others_Skilled = clsCommon.myCstr(gvManPS.Rows(4).Cells(ColSkill).Value)
            obj.Others_UnSkilled = clsCommon.myCstr(gvManPS.Rows(4).Cells(ColUnSkill).Value)
            obj.Others_Total = clsCommon.myCstr(gvManPS.Rows(4).Cells(ColTotal).Value)
            obj.Customer_1 = clsCommon.myCstr(TxtCust1.Text)
            obj.Customer_2 = clsCommon.myCstr(TxtCust2.Text)
            obj.Customer_3 = clsCommon.myCstr(TxtCust3.Text)
            obj.Customer_4 = clsCommon.myCstr(TxtCust4.Text)
            obj.Installed_Power_Capacity = clsCommon.myCstr(TxtInstalledPC.Text)
            obj.Own_Power_Generation_Capacity = clsCommon.myCstr(TxtOwnPGC.Text)

            obj.Terms_Code = clsCommon.myCstr(fndTrmsCode.Value)
            obj.Inspection_Facilities = clsCommon.myCstr(TxtInspFac.Text)
            obj.Material_Testing_Facilities = clsCommon.myCstr(TxtMatrTestFacilities.Text)
            obj.Capacity = clsCommon.myCstr(TxtCapacity.Text)
            obj.Process_Capability = clsCommon.myCstr(TxtProcessCap.Text)
            obj.Minimum_Batch_Size = clsCommon.myCstr(TxtMinBatch.Text)
            obj.Customer_Complaint = clsCommon.myCstr(TxtCustComp.Text)
            obj.Internal_Rejection = clsCommon.myCstr(TxtInternalRej.Text)
            If ChkToxicMat.Checked = True Then
                obj.Is_MSDS = "1"
            Else
                obj.Is_MSDS = "0"
            End If
            If RbtYes.CheckState = CheckState.Checked Then
                obj.Customer_Objection = "1"
            ElseIf RbtNo.CheckState = CheckState.Checked Then
                obj.Customer_Objection = "0"
            End If
            obj.Comments = clsCommon.myCstr(txtComment.Text)
            'obj.Approval_For_Pilot = clsCommon.myCstr(Txt.Text)
            'UcAttachment1.SaveData(obj.Registration_No)

            arr.Add(obj)
            ''''''Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (ClsSupplierRegistration.SaveData(arr)) Then
                If isPosted = False Then
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                End If
                UcAttachment1.SaveData(obj.Registration_No)
                LoadData(obj.Registration_No, NavigatorType.Current)
                btnsave.Text = "Update"
                btndelete.Enabled = True
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
                'End If
            End If
            Return True
        End If
    End Function

    Private Function PostData() As Boolean
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Registration_No As String = ""
            ' isFlag = True

            If clsCommon.myLen(txtcode.Value) > 0 Then
                Registration_No = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Registration_No from TSPL_SUPPLIER_REGISTRATION where Registration_No='" + txtcode.Value + "'"))
                If Registration_No > 0 Then
                    If (myMessages.postConfirm()) Then
                        If Save(True) = False Then
                            Return False
                        End If
                        If (ClsSupplierRegistration.PostData(MyBase.Form_ID, txtcode.Value)) Then
                            msg = "Successfully Posted"
                            Common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(txtcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering Registration No.")
                End If

            Else
                Throw New Exception("Registration No not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            '   isFlag = False
        End Try
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            isNewEntry = False

            ' Dim Course_Name As String
            ' Dim Chk_Description As String
            'Dim Relation_Name As String
            'Dim Qualification_Name As String
            'Dim Occupation_Name As String
            'Dim Designation_Desc As String

            Dim obj As New ClsSupplierRegistration()
            obj = ClsSupplierRegistration.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Registration_No) > 0) Then
                funReset()
                isNewEntry = False
                btnsave.Text = "Update"
                btndelete.Enabled = True
                txtcode.Value = obj.Registration_No

                dtpDate.Text = obj.Registration_Date
                TxtSupplierAdd.Text = obj.Supplier_Address
                txtSuppAdd.Text = clsCommon.myCstr(obj.Supplier_Address)
                TxtSupplierName.Text = clsCommon.myCstr(obj.Supplier_Name)
                TxtProduct.Text = clsCommon.myCstr(obj.Product)
                TxtCategory.Text = clsCommon.myCstr(obj.Category)
                TxtEmail.Text = clsCommon.myCstr(obj.Email)
                txtPhoneW.Text = clsCommon.myCstr(obj.Phone_No_Work)
                txtfaxWork.Text = clsCommon.myCstr(obj.Fax_No_Work)
                TxtWorkingHWork.Text = clsCommon.myCstr(obj.Working_Hrs_Work)
                TxtWeeklyHolidayWork.Text = clsCommon.myCstr(obj.Weekly_Holiday_Work)
                TxtPhoneHO.Text = clsCommon.myCstr(obj.Phone_No_HO)
                TxtFaxHO.Text = clsCommon.myCstr(obj.Fax_No_HO)
                TxtWorkingHHO.Text = clsCommon.myCstr(obj.Working_Hrs_HO)
                TxtWeeklyHolidayHO.Text = clsCommon.myCstr(obj.Weekly_Holiday_HO)
                TxtNameDWH.Text = clsCommon.myCstr(obj.Name_DWH)
                TxtDesgDWH.Text = clsCommon.myCstr(obj.Desgination_DWH)
                TxtPhoneDWH.Text = clsCommon.myCstr(obj.Phone_No_DWH)
                TxtNameAWH.Text = clsCommon.myCstr(obj.Name_AWH)
                TxtDesgAWH.Text = clsCommon.myCstr(obj.Designation_AWH)
                TxtPhoneAFH.Text = clsCommon.myCstr(obj.Phone_No_AWH)
                CmbNatureInd.SelectedValue = clsCommon.myCstr(obj.Nature_Of_Industry)
                TxtYrofEst.Text = clsCommon.myCstr(obj.Year_Of_Establishment)
                TxtTurnOver.Text = clsCommon.myCstr(obj.Turn_Over)
                TxtStateSale.Text = clsCommon.myCstr(obj.State_Sales_Tax_No)
                dtpStateSales.Text = obj.State_Sales_Tax_Date
                TxtCentraExcise.Text = clsCommon.myCstr(obj.Centra_Excise_Regn_No)
                dtpCentraExcise.Text = obj.Centra_Excise_Regn_Date
                TxtECCNo.Text = clsCommon.myCstr(obj.ECC_No)
                TxtSSIRegn.Text = clsCommon.myCstr(obj.SSI_Regn_No)
                dtpSSIRegn.Text = obj.SSI_Regn_No_Date
                If clsCommon.CompairString(obj.Is_Certified, "1") = CompairStringResult.Equal Then
                    ChkCertified.Checked = True
                Else
                    ChkCertified.Checked = False
                End If
                CmbCertification.SelectedValue = clsCommon.myCstr(obj.System_Certification)
                If clsCommon.CompairString(obj.System_Certification, "Any Other") = CompairStringResult.Equal Then
                    TxtCerti.Visible = True
                    TxtCerti.Text = clsCommon.myCstr(obj.Other_Certification)
                Else
                    TxtCerti.Visible = False
                    TxtCerti.Text = ""
                End If
                gvManPS.Rows(0).Cells(ColExec).Value = clsCommon.myCstr(obj.Production_Exec)
                gvManPS.Rows(0).Cells(ColSkill).Value = clsCommon.myCstr(obj.Production_Skilled)
                gvManPS.Rows(0).Cells(ColUnSkill).Value = clsCommon.myCstr(obj.Production_UnSkilled)
                gvManPS.Rows(0).Cells(ColTotal).Value = clsCommon.myCstr(obj.Production_Total)
                gvManPS.Rows(1).Cells(ColExec).Value = clsCommon.myCstr(obj.QC_Exec)
                gvManPS.Rows(1).Cells(ColSkill).Value = clsCommon.myCstr(obj.QC_Skilled)
                gvManPS.Rows(1).Cells(ColUnSkill).Value = clsCommon.myCstr(obj.QC_UnSkilled)
                gvManPS.Rows(1).Cells(ColTotal).Value = clsCommon.myCstr(obj.QC_Total)
                gvManPS.Rows(2).Cells(ColExec).Value = clsCommon.myCstr(obj.Total_Room_Exec)
                gvManPS.Rows(2).Cells(ColSkill).Value = clsCommon.myCstr(obj.Total_Room_Skilled)
                gvManPS.Rows(2).Cells(ColUnSkill).Value = clsCommon.myCstr(obj.Total_Room_UnSkilled)
                gvManPS.Rows(2).Cells(ColTotal).Value = clsCommon.myCstr(obj.Total_Room_Total)
                gvManPS.Rows(3).Cells(ColExec).Value = clsCommon.myCstr(obj.DE_Exec)
                gvManPS.Rows(3).Cells(ColSkill).Value = clsCommon.myCstr(obj.DE_Skilled)
                gvManPS.Rows(3).Cells(ColUnSkill).Value = clsCommon.myCstr(obj.DE_UnSkilled)
                gvManPS.Rows(3).Cells(ColTotal).Value = clsCommon.myCstr(obj.DE_Total)
                gvManPS.Rows(4).Cells(ColExec).Value = clsCommon.myCstr(obj.Others_Exec)
                gvManPS.Rows(4).Cells(ColSkill).Value = clsCommon.myCstr(obj.Others_Skilled)
                gvManPS.Rows(4).Cells(ColUnSkill).Value = clsCommon.myCstr(obj.Others_UnSkilled)
                gvManPS.Rows(4).Cells(ColTotal).Value = clsCommon.myCstr(obj.Others_Total)
                TxtCust1.Text = clsCommon.myCstr(obj.Customer_1)
                TxtCust2.Text = clsCommon.myCstr(obj.Customer_2)
                TxtCust3.Text = clsCommon.myCstr(obj.Customer_3)
                TxtCust4.Text = clsCommon.myCstr(obj.Customer_4)
                TxtInstalledPC.Text = clsCommon.myCstr(obj.Installed_Power_Capacity)
                TxtOwnPGC.Text = clsCommon.myCstr(obj.Own_Power_Generation_Capacity)
                fndTrmsCode.Value = clsCommon.myCstr(obj.Terms_Code)
                TxtInspFac.Text = clsCommon.myCstr(obj.Inspection_Facilities)
                TxtMatrTestFacilities.Text = clsCommon.myCstr(obj.Material_Testing_Facilities)
                TxtCapacity.Text = clsCommon.myCstr(obj.Capacity)
                TxtProcessCap.Text = clsCommon.myCstr(obj.Process_Capability)
                TxtMinBatch.Text = clsCommon.myCstr(obj.Minimum_Batch_Size)
                TxtCustComp.Text = clsCommon.myCstr(obj.Customer_Complaint)
                TxtInternalRej.Text = clsCommon.myCstr(obj.Internal_Rejection)
                txtComment.Text = clsCommon.myCstr(obj.Comments)
                If clsCommon.CompairString(obj.Is_MSDS, "1") = CompairStringResult.Equal Then
                    ChkToxicMat.Checked = True
                Else
                    ChkToxicMat.Checked = False
                End If
                If clsCommon.CompairString(obj.Customer_Objection, "1") = CompairStringResult.Equal Then
                    RbtYes.CheckState = CheckState.Checked
                Else
                    RbtNo.CheckState = CheckState.Checked
                End If
                If clsCommon.CompairString(obj.Posted, "P") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.Posted, "1") = CompairStringResult.Equal Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnsave.Enabled = False
                    BtnPost.Enabled = False
                    btndelete.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    BtnPost.Enabled = True
                End If
                UsLock1.Status = obj.Posted

                txtcode.MyReadOnly = True

                'isInsideLoadData = False
                UcAttachment1.LoadData(obj.Registration_No)
            Else
                isNewEntry = True
                funReset()
                UsLock1.Status = ERPTransactionStatus.Pending
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub FrmSupplierReg_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.P Then
            PostData()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                              "TSPL_SUPPLIER_REGISTRATION")
        End If
    End Sub

    Private Sub FrmSupplierReg_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(BtnPost, "Press Alt+P Post Transaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New Transaction")
        isNewEntry = True

        funReset()
        LoadGrid()
        UcAttachment1.Form_ID = MyBase.Form_ID
        RadPageView1.Pages("Attachments").Item.Visibility = ElementVisibility.Visible


        If clsCommon.myLen(Me.Tag) > 0 Then
            txtcode.Value = clsCommon.myCstr(Me.Tag)
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btndelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub




    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As Common.NavigatorType) Handles txtcode._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_SUPPLIER_REGISTRATION where REGISTRATION_NO='" + txtcode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If
            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        'Dim str As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE ='" + txtcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtcode.MyReadOnly = False
        'Else
        '    txtcode.MyReadOnly = True
        'End If
        ''isInsideLoadData = True
        'If txtcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = ""
        '    qry = "Select APPLICANT_CODE As [Code],Applicant_Description As [Applicant Description] from TSPL_HR_APPLICANT_ENTRY"
        '    txtcode.Value = clsCommon.ShowSelectForm("TSPL_HR_APPLICANT_ENTRY", qry, "Code", "", txtcode.Value, "TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE", isButtonClicked)
        '    If clsCommon.myLen(txtcode.Value) > 0 Then
        '        Dim objOT As ClsSupplierRegistration
        '        objOT = ClsSupplierRegistration.GetData(txtcode.Value, NavigatorType.Current)
        '        If Not objOT Is Nothing Then

        '            LoadData(txtcode.Value, NavigatorType.Current)

        '        End If
        '    Else
        '        funReset()
        '    End If
        'End If


        '''' After Auto Genrated Code
        Dim qry As String = "Select Registration_No As Code,Convert(varchar,Registration_Date,103) AS [Registration Date],Supplier_Name AS [Supplier Name] ,Supplier_Address + ' ' + Supplier_Address2 AS [Supplier Address],Phone_No_Work As [Phone No Work],Fax_No_Work AS [Fax No Work],Working_Hrs_Work AS [Working Hrs Work],Weekly_Holiday_Work As [Weekly Holiday Work],Phone_No_HO AS [Phone No HO],Fax_No_HO As [Fax No HO],Working_Hrs_HO AS [Working Hrs HO],Name_DWH AS [Name DWH],Desgination_DWH AS [Desgination DWH],Phone_No_DWH As [Phone No DWH],Name_AWH AS [Name AWH],Designation_AWH AS [Designation AWH],Phone_No_AWH As [Phone No AWH] ,Case When ISNULL(Nature_Of_Industry,'') ='SS'  Then 'Small Sacle' When ISNULL(Nature_Of_Industry,'') ='PvtL' Then 'Pvt. Limited'  When ISNULL(Nature_Of_Industry,'') ='PS' Then 'Prop. Ship' When ISNULL(Nature_Of_Industry,'') ='PubL' Then 'Public Limited' End  [Nature Of Industry],Year_Of_Establishment As [Year Of Establishment],Turn_Over As [Turn Over],State_Sales_Tax_No AS [State Sales Tax No], Convert(varchar,State_Sales_Tax_Date,103) As [State Sales Tax Date],Centra_Excise_Regn_No AS [Centra Excise Regn No] ,Convert(varchar,Centra_Excise_Regn_Date ,103) As [Centra Excise Regn Date] , ECC_No AS [ECC No],SSI_Regn_No AS [SSI Regn No],Convert(varchar,SSI_Regn_No_Date  ,103) As [SSI Regn No Date],Is_Certified AS [Is Certified],System_Certification As [System Certification],Other_Certification As [Other Certification],Installed_Power_Capacity As [Installed Power Capacity],Own_Power_Generation_Capacity AS [Own Power Generation Capacity],Terms_Code As [Terms Code],Inspection_Facilities As [Inspection Facilities],Material_Testing_Facilities As [Material Testing Facilities],Capacity,Process_Capability As [Process Capability],Minimum_Batch_Size As [Minimum Batch Size],Internal_Rejection AS [Internal Rejection],Posted ,Approved From TSPL_SUPPLIER_REGISTRATION "
        txtcode.Value = clsCommon.ShowSelectForm("SuppFnd", qry, "Code", "", txtcode.Value, "CODE", isButtonClicked, "Registration_Date")
        If clsCommon.myLen(txtcode.Value) > 0 Then
            Dim objOT As ClsSupplierRegistration
            objOT = ClsSupplierRegistration.GetData(txtcode.Value, NavigatorType.Current)
            If Not objOT Is Nothing Then
                LoadData(txtcode.Value, NavigatorType.Current)
            End If
        Else
            funReset()
        End If
    End Sub

    Private Sub fndTrmsCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTrmsCode._MYValidating
        Dim Qry As String = "select Terms_Code as Code,Terms_Desc as Description,No_Days as [No Of Days] from TSPL_TERMS_MASTER"
        fndTrmsCode.Value = clsCommon.ShowSelectForm("TermCodeFNDD", Qry, "Code", "", fndTrmsCode.Value, "Code", isButtonClicked)
        fndTrmsCode_TxtChanged()
    End Sub


    Private Sub gvManPS_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvManPS.CellValueChanged

    End Sub
    Private Sub fndTrmsCode_TxtChanged()

        If Not isInsideLoadData Then
            Dim qry As String = "select Terms_Code,Terms_Desc,No_Days from TSPL_TERMS_MASTER where Terms_Code='" + clsCommon.myCstr(fndTrmsCode.Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                fndTrmsCode.Value = clsCommon.myCstr(dt.Rows(0)("Terms_Code"))
                lblTermsCode.Text = clsCommon.myCstr(dt.Rows(0)("Terms_Desc"))
                'txtDueDate.Value = txtDate.Value.AddDays(clsCommon.myCdbl(dt.Rows(0)("No_Days")))
            Else
                lblTermsCode.Text = ""
            End If
        End If
    End Sub

    Private Sub gvManPS_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvManPS.CurrentColumnChanged
       GirdCalculation()
    End Sub

    Private Sub BtnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnPost.Click
        If clsCommon.myLen(txtcode.Value) > 0 Then
            PostData()
        Else
            clsCommon.MyMessageBoxShow("code not found to post", Me.Text)
        End If
    End Sub

    Private Sub CmbCertification_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CmbCertification.SelectedIndexChanged
        If IsComboLoad = True Then
            If clsCommon.CompairString(CmbCertification.SelectedValue, "Any Other") = CompairStringResult.Equal Then
                TxtCerti.Visible = True
            Else
                TxtCerti.Visible = False
                TxtCerti.Text = ""
            End If
        End If
    End Sub

    Private Sub MyLabel32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyLabel32.Click

    End Sub

    Private Sub TxtEmail_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtEmail.Leave
        If TxtEmail.Text = "" Then
        Else
            Dim check As Match = Regex.Match(TxtEmail.Text, "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
            If check.Success Then
            Else
                common.clsCommon.MyMessageBoxShow("Please Enter the proper format of e-mail address", Me.Text)
                TxtEmail.Text = ""
                TxtEmail.Focus()
            End If
        End If
    End Sub
End Class
