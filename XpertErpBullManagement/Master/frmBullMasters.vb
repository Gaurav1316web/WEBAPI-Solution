Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Excel = Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports System.Text.RegularExpressions
Imports common
Imports System.IO


Imports XpertERPEngine
Imports XpertERPEngineFine


Public Class frmBullMasters
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim isLoadData As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim ErrorControl As New clsErrorControl()
    Const ColSNFINKG As String = "ColSNFINKG"
    Const ColFATINKG As String = "ColFATINKG"
    Const ColLactNo As String = "ColLactNo"
    Const ColBestStandardLocationYield As String = "ColBestStandardLocationYield"
    Const ColMUN As String = "ColMUN"
    Const ColSCC As String = "ColSCC"
    Const ColDateOfPedigreeInformationUpdate As String = "ColDateOfPedigreeInformationUpdate"
    Const ColLactose As String = "ColLactose"
    Const ColProtien As String = "ColProtien"
    Const ColSnfPercent As String = "ColSnfPercent"
    Const ColFatPercent As String = "ColFatPercent"
    Const ColFirstStandardLocationYield As String = "ColFirstStandardLocationYield"
    Const ColTaqNo As String = "ColTaqNo"
    Const ColRelation As String = "ColRelation"
    Const ColCode As String = "ColCode"
    'Public Property OpenFileDialog1 As Object
    'Public Property PictureBox1 As Object

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        RadMenuItem1.Enabled = MyBase.isModifyFlag ' For Import
        RadMenuItem2.Enabled = MyBase.isModifyFlag ' For Export
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            fndCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Bull Code can't be blank and Fill 12 Digit Code", Me.Text)
            Exit Function
            Return False
        End If
        If clsCommon.myLen(fndBreed.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Enter Breed Name, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndBreed.Focus()
            ErrorControl.SetError(fndBreed, "Please Enter Breed Name, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndBreed, "")
        End If
        If clsCommon.myLen(fndSpecies.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Species Name, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndSpecies.Focus()
            ErrorControl.SetError(fndSpecies, "Please Species Name, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndSpecies, "")
        End If
        If clsCommon.myLen(TXTExoticBloodPer.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Exotic Blood Percen, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            TXTExoticBloodPer.Focus()
            ErrorControl.SetError(TXTExoticBloodPer, "Please Exotic Blood Percent, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(TXTExoticBloodPer, "")
        End If
        If clsCommon.myLen(txtBullRFID.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Bull RFID, It is Mandatory ", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtBullRFID.Focus()
            ErrorControl.SetError(txtBullRFID, "Please Bull RFID, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtBullRFID, "")
        End If
        If clsCommon.myLen(fndCategory.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Category, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndCategory.Focus()
            ErrorControl.SetError(fndCategory, "Please Category, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndCategory, "")
        End If
        If clsCommon.myLen(txtBullBook.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Category, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtBullBook.Focus()
            ErrorControl.SetError(txtBullBook, "Please Category, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtBullBook, "")
        End If
        If clsCommon.myLen(fndCounty.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select Category, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndCounty.Focus()
            ErrorControl.SetError(fndCounty, "Please Select Category, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndCounty, "")
        End If
        If clsCommon.myLen(fndSubCategory.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select Sub Category, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndSubCategory.Focus()
            ErrorControl.SetError(fndSubCategory, "Please Select Sub Category, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndSubCategory, "")
        End If
        If clsCommon.myLen(TXTSSbull.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select SS bull ID, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            TXTSSbull.Focus()
            ErrorControl.SetError(TXTSSbull, "Please Select SS bull ID, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(TXTSSbull, "")
        End If
        If clsCommon.myLen(txtBullAlias.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select Bull Alias Name, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtBullAlias.Focus()
            ErrorControl.SetError(txtBullAlias, "Please Select Bull Alias Name, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtBullAlias, "")
        End If
        If clsCommon.myLen(fndSSCentre.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select SS Centre, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndSSCentre.Focus()
            ErrorControl.SetError(fndSSCentre, "Please Select SS Centre, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndSSCentre, "")
        End If
        If clsCommon.myLen(fndShedId.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select Shed Id, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndShedId.Focus()
            ErrorControl.SetError(fndShedId, "Please Select Shed Id, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndShedId, "")
        End If
        If clsCommon.myLen(fndPenId.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select Pen Id, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndPenId.Focus()
            ErrorControl.SetError(fndPenId, "Please Select Pen Id, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndPenId, "")
        End If
        If clsCommon.myLen(fndBullRating.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Fill Bull Rating, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndBullRating.Focus()
            ErrorControl.SetError(fndBullRating, "Fill Bull Rating It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndBullRating, "")
        End If
        If clsCommon.myLen(txtDamLocation.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Select Dam Location, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndBullRating.Focus()
            ErrorControl.SetError(txtDamLocation, "Please Select Dam Location, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtDamLocation, "")
        End If
        If clsCommon.myLen(fndBullSourcePainting.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Bull Source Painting , It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndBullSourcePainting.Focus()
            ErrorControl.SetError(fndBullSourcePainting, "Please Fill Bull Source Painting, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndBullSourcePainting, "")
        End If
        If clsCommon.myLen(TXTPrevBull.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Prev Bul , It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            TXTPrevBull.Focus()
            ErrorControl.SetError(TXTPrevBull, "Please Fill Prev Bul, It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(TXTPrevBull, "")
        End If
        If clsCommon.myLen(fndBullStatus.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Bull Status , It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndBullStatus.Focus()
            ErrorControl.SetError(fndBullStatus, "Please Fill Bull Status It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndBullStatus, "")
        End If
        If clsCommon.myLen(fndSubStatus.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Bull Sub Status , It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndBullStatus.Focus()
            ErrorControl.SetError(fndSubStatus, "Please Fill Bull Sub Status It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndSubStatus, "")
        End If
        If clsCommon.myLen(txtRemark.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Remark , It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtRemark.Focus()
            ErrorControl.SetError(txtRemark, "Please Fill Remark It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtRemark, "")
        End If
        If clsCommon.myLen(txtPurchaseNo.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Purchase No, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtPurchaseNo.Focus()
            ErrorControl.SetError(txtPurchaseNo, "Please Fill Purchase No It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtPurchaseNo, "")
        End If
        If clsCommon.myLen(txtSourceName.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Source Name, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtSourceName.Focus()
            ErrorControl.SetError(txtSourceName, "Please Fill Source Name It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtSourceName, "")
        End If
        If clsCommon.myLen(txtOwnerName.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Fill Owner Name, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtOwnerName.Focus()
            ErrorControl.SetError(txtOwnerName, "Please Fill Owner Name It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtOwnerName, "")
        End If
        If clsCommon.myLen(txtSemenPrice.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Semen Price, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtSemenPrice.Focus()
            ErrorControl.SetError(txtSemenPrice, "Please Semen Price It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtSemenPrice, "")
        End If
        If clsCommon.myLen(txtProducedTillDate.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Produced Till Date, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtProducedTillDate.Focus()
            ErrorControl.SetError(txtProducedTillDate, "Please Produced Till Date It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtProducedTillDate, "")
        End If
        If clsCommon.myLen(txtAveregeDoses.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Averege Doses, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtAveregeDoses.Focus()
            ErrorControl.SetError(txtAveregeDoses, "Please Averege Doses It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtAveregeDoses, "")
        End If
        If clsCommon.myLen(txtBreedingValue.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Breeding1, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtBreedingValue.Focus()
            ErrorControl.SetError(txtBreedingValue, "Please Breeding It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtBreedingValue, "")
        End If
        If clsCommon.myLen(fndSourceName.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Source Nam, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            fndSourceName.Focus()
            ErrorControl.SetError(fndSourceName, "Please Source Name It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(fndSourceName, "")
        End If
        If clsCommon.myLen(txtNodaughters.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Entre No Of Daughters , It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtNodaughters.Focus()
            ErrorControl.SetError(txtNodaughters, "lease Entre No Of Daughters It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtNodaughters, "")
        End If
        If clsCommon.myLen(txtlDamOrigin.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Dam Origin, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtlDamOrigin.Focus()
            ErrorControl.SetError(txtlDamOrigin, "Please Dam Origine It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtlDamOrigin, "")
        End If
        If clsCommon.myLen(txtBirthWeight.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Birth Weight, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtBirthWeight.Focus()
            ErrorControl.SetError(txtBirthWeight, "Please Birth Weight It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtBirthWeight, "")
        End If
        If clsCommon.myLen(txtWeightAtEntry.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Weight At Entry, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtWeightAtEntry.Focus()
            ErrorControl.SetError(txtWeightAtEntry, "Please Weight At Entry It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtWeightAtEntry, "")
        End If
        If clsCommon.myLen(txtMilkingDone.Text) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, " Please Milking Done, It is Mandatory", Me.Text)
            'RadPageView1.SelectedPage = lblBreed2
            txtMilkingDone.Focus()
            ErrorControl.SetError(txtMilkingDone, "Please Milking Done It is Mandatory")
            Return False
        Else
            ErrorControl.SetError(txtMilkingDone, "")
        End If
        Return True
    End Function

    Private Sub fndSpecies__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSpecies._MYValidating
        Try

            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SPECIES_MASTER  "
            fndSpecies.Value = clsCommon.ShowSelectForm("Species", qry, "Code", "", fndSpecies.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCategory._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_CATEGORY_MASTER  "
            fndCategory.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndCategory.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCountry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub fndSubCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSubCategory._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SUB_CATEGORY_MASTER  "
            fndSubCategory.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSubCategory.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndSSCentre__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSSCentre._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SS_CENTRE_MASTER  "
            fndSSCentre.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSSCentre.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBreed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBreed._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_BREED_MASTER  "
            fndBreed.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBreed.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndShedId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndShedId._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SHED_MASTER  "
            fndShedId.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndShedId.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndPenId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPenId._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_PEN_ID_MASTER  "
            fndPenId.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndPenId.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBullStatus__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullStatus._MYValidating
        Try
            Dim qry As String = " select STSTUS_Code as Code, Name  as Name from TSPL_BULL_STATUS_MASTER  "
            fndBullStatus.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBullStatus.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndSubStatus__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSubStatus._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SUB_STATUS_MASTER  "
            fndSubStatus.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSubStatus.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            Dim Sqlqry As String = "select * from TSPL_BULL_MASTER where Bull_code='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            If fndCode.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = ""
                Dim qry As String = "select * from TSPL_BULL_MASTER"
                fndCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Bull_Code", whrClas, fndCode.Value, "TSPL_BULL_MASTER.Bull_Code asc", isButtonClicked, Nothing)
                LoadData(fndCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            gv2.DataSource = Nothing
            gv2.Refresh()
            isInsideLoadData = True
            fndCode.MyReadOnly = True
            AddNew()
            Dim obj As clsBullMasters = clsBullMasters.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                'PictureBox1.Image = obj.Picture_Upload
                fndCode.Value = obj.Bull_Code
                fndSpecies.Value = obj.Species_Code
                fndCategory.Value = obj.Category_Code
                fndSubCategory.Value = obj.Sub_Category_Code
                fndSSCentre.Value = obj.SS_Centre_Code
                fndBreed.Value = obj.Breed_Code
                fndShedId.Value = obj.Shed_Code
                fndPenId.Value = obj.Pen_Code
                fndBullStatus.Value = obj.Status_Code
                fndSubStatus.Value = obj.Sub_Status_Code
                fndBullRating.Value = obj.Bull_Rating
                TXTSSbull.Text = obj.SS_Bull_Id
                txtBullAlias.Text = obj.Bull_Alia_Name
                txtDamLocation.Text = obj.Dam_Location_Yeild
                txtWeightAtEntry.Text = obj.Weight_At_Entry
                '--date--
                txtFirstCollectionDate.Value = obj.First_Collection_Date
                txtLastDateBreeding.Value = obj.Last_Updated_Date_For_Breeding_Value
                txtPurchaseRequestDate.Value = obj.Purchase_Request_Date
                txtPurchaseDate.Value = obj.Purchase_Date
                txtRegDate.Value = obj.Registration_Date
                txtStatusDateChanged.Value = obj.Status_Changed_Date
                txtDateOfBirth.Value = obj.Date_Of_Birth
                TXTEndDate.Value = obj.Exit_Date
                '--end date
                txtNoofFemaleCalves.Text = obj.No_of_Female_Calves
                fndBullSourcePainting.Value = obj.Bull_source_Printing_Straws
                txtBullRFID.Text = obj.Bull_RFID
                If obj.is_Semen Then
                    RadioButton2.Checked = True
                Else
                    RadioButton1.Checked = True
                End If
                TXTExoticBloodPer.Text = obj.Exotic_Blood_Per
                txtBullBook.Text = obj.Bull_Book_Value
                fndCounty.Value = obj.Country_Code
                TXTPrevBull.Text = obj.Prev_Bull_Id
                txtDateOfBirth.Value = obj.Date_Of_Birth
                txtRegDate.Value = obj.Registration_Date
                txtStatusDateChanged.Value = obj.Status_Changed_Date
                TXTEndDate.Value = obj.Exit_Date
                txtRemark.Text = obj.Remark
                txtPurchaseNo.Text = obj.Purchase_Request_No
                txtSourceName.Text = obj.Source_Address
                txtOwnerName.Text = obj.Owner_Of_Bull
                txtSemenPrice.Text = obj.Semen_Price_per_Straw
                txtProducedTillDate.Text = obj.Doses_Produce_Till_Date
                txtAveregeDoses.Text = obj.Capacity_Of_Averege_Monthly_Doses
                txtBreedingValue.Text = obj.Breeding_Value
                fndSourceName.Value = obj.Source_Code
                txtNodaughters.Text = obj.NO_OF_Doughters
                txtlDamOrigin.Text = obj.Dam_Origin
                txtBirthWeight.Text = obj.Birth_Weight
                txtMilkingDone.Text = obj.No_Milking_Done
                txtTotalAI.Text = obj.Total_AI
                txtDateofnominatedmatinginitiated.Text = obj.Date_of_nominated_mating_initiated
                txtNoofmalesproduced.Text = obj.No_of_males_produced
                txtTrainingCentre.Text = obj.Training_Centre
                txtTotalHeiferAI.Text = obj.Total_Heifer_AI
                txtNoofmalecalves.Text = obj.No_of_Female_Calves
                txtNoofinseminationcarriedout.Text = obj.No_of_insemination_carried_out
                txtPreQuarantine.Text = obj.Pre_Quarantine
                txtNoundersemencollection.Text = obj.No_under_semen_collection
                txtTotalHeiferConceptions.Text = obj.Total_Heifer_Conceptions
                txtNoofelitefemalescurrentlypregnant.Text = obj.No_of_elite_females_currently_pregnant
                txtQuarntine.Text = obj.Quarntine
                txtTotalConceptions.Text = obj.Total_Conceptions
                txtNoabortions.Text = obj.No_abortions
                txtNomalesborn.Text = obj.No_males_born
                txtREARINGCentre.Text = obj.REARING_Centre
                'PictureBox1.Image = obj.Picture_Upload
                '--check condition added
                chkGeneticDiseaseTeasting.Checked = obj.Genomic_Tested_Bulls '
                chkSonOfProvenSire.Checked = obj.Son_Of_Proven_Sire
                chkGenomicTestedBulls.Checked = obj.Genomic_Tested_Bulls
                chkKaryotyping.Checked = obj.Karyotyping
                chkSibilingTeasted.Checked = obj.SibilingTeasted
                chkShouldbeshowninSireDirectory.Checked = obj.Should_be_shown_in_Sire_Directory
                chkProven.Checked = obj.Proven
                chkUnderProgenyTest.Checked = obj.Under_Progeny_Test
                chkPercentageVerification.Checked = obj.Percentage_Verification
                chkIsIBRBull.Checked = obj.Is_IBR_Bull
                isInsideLoadData = True
                If obj.Arr IsNot Nothing Then
                    For Each objTr As clsBullMastersDetail In obj.Arr
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColRelation).Value = objTr.Relation
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColTaqNo).Value = objTr.Taq_No
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColFirstStandardLocationYield).Value = objTr.First_Standard_Location_Yield
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColFatPercent).Value = objTr.Fat_Percent
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColSnfPercent).Value = objTr.Snf_Percent
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColProtien).Value = objTr.Protien

                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColLactose).Value = objTr.Lactose
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColDateOfPedigreeInformationUpdate).Value = objTr.Date_Of_Pedigree_Information_Update
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColSCC).Value = objTr.SCC
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColMUN).Value = objTr.MUN
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColBestStandardLocationYield).Value = objTr.Best_Standard_Location_Yield
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColLactNo).Value = objTr.Lact_No
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColSNFINKG).Value = objTr.FAT_IN_KG
                        gv2.Rows(gv2.Rows.Count - 1).Cells(ColFATINKG).Value = objTr.SNF_IN_KG

                        gv2.Rows.AddNew()
                    Next

                End If
                'AddNew()
                LoadImage1()
            End If
            isLoadData = True
            'LoadImage1()
            'isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub LoadImage1()

        Try
            PictureBox1.Image = Nothing
            Dim qry As String = "select Picture_Upload from tspl_bull_master where Bull_code='" + fndCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' If Not IsDBNull(dt.Rows(0)("Picture_Upload")) AndAlso clsCommon.myLen(dt.Rows(0)("Picture_Upload")) > 0 Then
                If (clsCommon.myLen(dt.Rows(0)("Picture_Upload")) > 0) Then
                        Dim data As Byte() = DirectCast(dt.Rows(0)("Picture_Upload"), Byte())
                        Dim ms As New MemoryStream(data)
                        PictureBox1.Image = Image.FromStream(ms)
                        ms.Close()
                        ms.Dispose()
                    End If
                End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBullMasters()
                If IsNumeric(fndCode.Value) AndAlso clsCommon.myLen(fndCode.Value) >= 12 Then
                    obj.Bull_Code = clsCommon.myCstr(fndCode.Value)
                Else
                    clsCommon.MyMessageBoxShow("fill 12 Digit Bull id", Me.Text)
                    fndCode.Focus()
                    fndCode.Text = ""
                    Exit Sub
                End If
                obj.Species_Code = fndSpecies.Value
                obj.Category_Code = fndCategory.Value
                obj.Sub_Category_Code = fndSubCategory.Value
                obj.SS_Centre_Code = fndSSCentre.Value
                obj.Breed_Code = fndBreed.Value
                obj.Shed_Code = fndShedId.Value
                obj.Pen_Code = fndPenId.Value
                obj.Status_Code = fndBullStatus.Value
                obj.Sub_Status_Code = fndSubStatus.Value
                If RadioButton1.Checked = True Then
                    obj.is_Semen = False
                Else
                    obj.is_Semen = True
                End If
                If IsNumeric(TXTExoticBloodPer.Text) Then
                    obj.Exotic_Blood_Per = clsCommon.myCdbl(TXTExoticBloodPer.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "Please enter a valid percentage", Me.Text)
                    TXTExoticBloodPer.Focus()
                    TXTExoticBloodPer.Text = ""
                    Exit Sub
                End If
                obj.Bull_Book_Value = txtBullBook.Text
                obj.Country_Code = fndCounty.Value
                obj.Prev_Bull_Id = TXTPrevBull.Text
                obj.Date_Of_Birth = txtDateOfBirth.Value
                obj.Registration_Date = txtRegDate.Value
                obj.Exit_Date = txtEndDate.Value
                obj.Status_Changed_Date = txtStatusDateChanged.Value
                obj.Bull_Alia_Name = txtBullAlias.Text
                obj.Bull_Rating = fndBullRating.Value
                obj.Dam_Location_Yeild = txtDamLocation.Text
                obj.Bull_source_Printing_Straws = fndBullSourcePainting.Value
                obj.Bull_RFID = txtBullRFID.Text
                obj.Remark = txtRemark.Text
                obj.Source_Code = fndSourceName.Value
                obj.Source_Address = txtSourceName.Text
                obj.Owner_Of_Bull = txtOwnerName.Text
                obj.Semen_Price_per_Straw = txtSemenPrice.Text
                obj.Doses_Produce_Till_Date = txtProducedTillDate.Text
                obj.Capacity_Of_Averege_Monthly_Doses = txtAveregeDoses.Text
                obj.Breeding_Value = txtBreedingValue.Text
                'obj.Picture_Upload = PictureBox1.Image

                obj.First_Collection_Date = txtFirstCollectionDate.Value
                obj.Last_Updated_Date_For_Breeding_Value = txtLastDateBreeding.Value
                obj.Purchase_Request_No = clsCommon.myCstr(txtPurchaseNo.Text)
                obj.Purchase_Request_Date = clsCommon.myCDate(txtPurchaseRequestDate.Value)
                obj.Purchase_Date = clsCommon.myCDate(txtPurchaseDate.Value)
                obj.Genetic_Disease_Teasting = chkGeneticDiseaseTeasting.Checked
                obj.Son_Of_Proven_Sire = chkSonOfProvenSire.Checked
                obj.Genomic_Tested_Bulls = chkGenomicTestedBulls.Checked
                obj.Karyotyping = chkKaryotyping.Checked
                obj.SibilingTeasted = chkSibilingTeasted.Checked
                obj.Should_be_shown_in_Sire_Directory = chkShouldbeshowninSireDirectory.Checked
                obj.Proven = chkProven.Checked
                obj.Under_Progeny_Test = chkUnderProgenyTest.Checked
                obj.Percentage_Verification = chkPercentageVerification.Checked
                obj.Is_IBR_Bull = chkIsIBRBull.Checked
                obj.NO_OF_Doughters = txtNodaughters.Text
                obj.No_Milking_Done = txtBirthWeight.Text
                obj.Birth_Weight = txtWeightAtEntry.Text
                obj.Dam_Origin = txtMilkingDone.Text
                obj.SS_Bull_Id = TXTSSbull.Text
                '----progeny detail---
                obj.Total_AI = txtTotalAI.Text
                obj.Date_of_nominated_mating_initiated = txtDateofnominatedmatinginitiated.Text
                obj.No_of_males_produced = txtNoofmalesproduced.Text
                obj.Training_Centre = txtTrainingCentre.Text
                obj.No_of_male_calves = txtNoofmalecalves.Text
                obj.Total_Heifer_AI = txtTotalHeiferAI.Text
                obj.No_of_insemination_carried_out = txtNoofinseminationcarriedout.Text
                obj.Pre_Quarantine = txtPreQuarantine.Text
                obj.No_under_semen_collection = txtNoundersemencollection.Text
                obj.Total_Heifer_Conceptions = txtTotalHeiferConceptions.Text
                obj.No_of_Female_Calves = txtNoofFemaleCalves.Text
                obj.No_of_elite_females_currently_pregnant = txtNoofelitefemalescurrentlypregnant.Text
                obj.Quarntine = txtQuarntine.Text
                obj.Total_Conceptions = txtTotalConceptions.Text
                obj.REARING_Centre = txtREARINGCentre.Text
                obj.No_abortions = txtNoabortions.Text
                obj.No_males_born = txtNomalesborn.Text
                '--END --
                obj.Arr = New List(Of clsBullMastersDetail)
                For Each row As GridViewRowInfo In gv2.Rows
                    Dim objTr As New clsBullMastersDetail()
                    objTr.Code = clsCommon.myCstr(row.Cells(ColCode).Value)

                    objTr.Relation = clsCommon.myCstr(row.Cells(ColRelation).Value)
                    objTr.Taq_No = clsCommon.myCstr(row.Cells(ColTaqNo).Value)
                    objTr.First_Standard_Location_Yield = clsCommon.myCstr(row.Cells(ColFirstStandardLocationYield).Value)
                    objTr.Fat_Percent = clsCommon.myCDecimal(row.Cells(ColFatPercent).Value)
                    objTr.Snf_Percent = clsCommon.myCDecimal(row.Cells(ColSnfPercent).Value)
                    objTr.Protien = clsCommon.myCstr(row.Cells(ColProtien).Value)
                    objTr.Lactose = clsCommon.myCstr(row.Cells(ColLactose).Value)
                    objTr.Date_Of_Pedigree_Information_Update = clsCommon.myCDate(row.Cells(ColDateOfPedigreeInformationUpdate).Value)
                    objTr.SCC = clsCommon.myCstr(row.Cells(ColMUN).Value)
                    objTr.MUN = clsCommon.myCstr(row.Cells(ColSCC).Value)
                    objTr.Best_Standard_Location_Yield = clsCommon.myCstr(row.Cells(ColBestStandardLocationYield).Value)
                    objTr.Lact_No = clsCommon.myCDecimal(row.Cells(ColLactNo).Value)
                    objTr.FAT_IN_KG = clsCommon.myCDecimal(row.Cells(ColFATINKG).Value)
                    objTr.SNF_IN_KG = clsCommon.myCDecimal(row.Cells(ColSNFINKG).Value)
                    If (clsCommon.myLen(objTr.Relation) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                Dim Sqlqry As String = "select count(1) from tspl_bull_master where Bull_code ='" + fndCode.Value + "'"
                Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
                'SaveImage()
                If count = 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                'SaveImage()
                If (obj.SaveData(obj, isNewEntry)) Then
                    SaveImage()
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Bull_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SaveImage()

        Try
            Dim qry As String = ""
            If clsCommon.myLen(fndCode.Value) > 0 Then
                If PictureBox1.Image IsNot Nothing Then
                    'Sanjay Ticket No-MIL/01/04/19-000058
                    'Dim bm As New Bitmap(PictureBox1.Image)

                    Dim ms As New MemoryStream()
                    'bm.Save(ms, System.Drawing.Imaging.ImageFormat.MemoryBmp)
                    PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    clsDBFuncationality.UpdateImage("Picture_Upload", data, "tspl_bull_master", "Bull_Code='" + fndCode.Value + "'")
                    ''richa agarwal regarding memory leakage
                    ms.Close()
                    ms.Dispose()
                Else
                    clsDBFuncationality.ExecuteNonQuery("Update tspl_bull_master set Picture_Upload=null where  Bull_Code='" + fndCode.Value + "'")
                End If


            End If
        Catch ex As Exception
            '  MsgBox(ex.Message.ToString())
        End Try
    End Sub

    Private Sub frmBullMasters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AddNew()
        LoadBlankGridColmns()
    End Sub



    Public Sub AddNew()
        LoadBlankGridColmns()
        isInsideLoadData = False
        fndCode.MyReadOnly = False
        fndCode.Value = Nothing
        fndCode.Value = Nothing
        fndSpecies.Value = Nothing
        fndCategory.Value = Nothing
        fndSubCategory.Value = Nothing
        fndSSCentre.Value = Nothing
        fndBreed.Value = Nothing
        fndShedId.Value = Nothing
        fndPenId.Value = Nothing
        fndBullStatus.Value = Nothing
        fndSubStatus.Value = Nothing
        fndBullRating.Value = Nothing
        fndBullSourcePainting.Value = Nothing
        fndBullRating.Value = Nothing
        fndCounty.Value = Nothing
        txtDamLocation.Text = ""
        TXTSSbull.Text = ""
        TXTExoticBloodPer.Text = ""
        txtBullBook.Text = ""
        'DigitBullId.Text = ""
        TXTPrevBull.Text = ""
        txtDateOfBirth.Value = clsCommon.GETSERVERDATE()
        txtRegDate.Value = clsCommon.GETSERVERDATE()
        txtStatusDateChanged.Value = clsCommon.GETSERVERDATE()
        TXTEndDate.Value = clsCommon.GETSERVERDATE()
        txtBullAlias.Text = ""
        fndCode.Focus()
        txtRemark.Text = ""
        '----progeny detail---
        txtTotalAI.Text = ""
        txtDateofnominatedmatinginitiated.Text = ""
        txtNoofmalesproduced.Text = ""
        txtTrainingCentre.Text = ""
        txtTotalHeiferAI.Text = ""
        txtNoofmalecalves.Text = ""
        txtNoofinseminationcarriedout.Text = ""
        txtPreQuarantine.Text = ""
        txtNoundersemencollection.Text = ""
        txtTotalHeiferConceptions.Text = ""
        txtNoofFemaleCalves.Text = ""
        txtNoofelitefemalescurrentlypregnant.Text = ""
        txtQuarntine.Text = ""
        txtTotalConceptions.Text = ""
        txtNoabortions.Text = ""
        txtNomalesborn.Text = ""
        txtREARINGCentre.Text = ""
        txtBullRFID.Text = ""
        '--end
        '--bull general detail
        txtNodaughters.Text = ""
        txtlDamOrigin.Text = ""
        txtBirthWeight.Text = ""
        txtWeightAtEntry.Text = ""
        txtMilkingDone.Text = ""
        chkGeneticDiseaseTeasting.Checked = False
        chkSonOfProvenSire.Checked = False
        chkGenomicTestedBulls.Checked = False
        chkKaryotyping.Checked = False
        chkSibilingTeasted.Checked = False
        chkShouldbeshowninSireDirectory.Checked = False
        chkProven.Checked = False
        chkUnderProgenyTest.Checked = False
        chkPercentageVerification.Checked = False
        chkIsIBRBull.Checked = False
        '--end
        RadioButton2.Checked = True
        RadioButton1.Checked = False
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = True
        'LoadBlankGridColmns()
        '--Bull Information--
        txtPurchaseNo.Text = ""
        txtSourceName.Text = ""
        txtOwnerName.Text = ""
        txtFirstCollectionDate.Value = clsCommon.GETSERVERDATE()
        txtLastDateBreeding.Value = clsCommon.GETSERVERDATE()
        txtPurchaseRequestDate.Value = clsCommon.GETSERVERDATE()
        txtPurchaseDate.Value = clsCommon.GETSERVERDATE()
        txtSemenPrice.Text = ""
        txtProducedTillDate.Text = ""
        txtAveregeDoses.Text = ""
        txtBreedingValue.Text = ""
        fndSourceName.Value = ""
        PictureBox1.Image = Nothing
        'gv2.Rows.AddNew()
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                fndCode.Focus()
                fndCode.Select()
                ErrorControl.SetError(fndCode, "Code not found to delete.")
                Throw New Exception("Code not found to delete")
            Else
                ErrorControl.ResetError(fndCode)
            End If

            If myMessages.deleteConfirm() Then
                If clsBullMasters.DeleteData(fndCode.Value) Then
                    myMessages.delete()
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv2_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv2.UserDeletedRow
        For ii As Integer = 1 To gv2.Rows.Count
            gv2.Rows(ii - 1).Cells(ColRelation).Value = ii
        Next
    End Sub

    Private Sub gv2_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv2.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select count(*) from TSPL_BULL_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Bull_Code,Species_Code,Category_Code,Sub_Category_Code,SS_Centre_Code,Breed_Code,Shed_Code,Pen_Code,Status_Code,Sub_Status_Code,Exotic_Blood_Per,Bull_Book_Value,Country_Code,Prev_Bull_Id,SS_Bull_Id,Bull_Alia_Name,Bull_Rating,Dam_Location_Yeild,Bull_source_Printing_Straws,Is_Semen,Bull_RFID,Remark,Registration_Date,Date_Of_Birth,Status_Changed_Date,Exit_Date from TSPL_BULL_MASTER"
        Else
            qry = "select '' as Bull_Code"
        End If

        transportSql.OpenExporttoExcel(qry, Me)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Bull_Code", "Name", "Bull_Code", "Species_Code", "Category_Code", "Sub_Category_Code", "SS_Centre_Code", "Breed_Code", "Shed_Code", "Pen_Code", "Status_Code", "Sub_Status_Code", "Exotic_Blood_Per", "Bull_Book_Value", "Country_Code", "Prev_Bull_Id", "SS_Bull_Id", "Bull_Alia_Name", "Bull_Rating", "Dam_Location_Yeild", "Bull_source_Printing_Straws", "Is_Semen", "Bull_RFID", "Remark", "Registration_Date", "Date_Of_Birth", "Status_Changed_Date", "Exit_Date") Then
            Dim obj As New clsBullMasters()

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows
                    'obj = New clsBullBreedMaster()

                    obj.Bull_Code = clsCommon.myCstr(grow.Cells("Bull_Code").Value)
                    If clsCommon.myLen(obj.Bull_Code) > 50 Then
                        Throw New Exception("Code has max. length 50 see at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If

                    'obj.Name = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    'If clsCommon.myLen(obj.Name) <= 0 Then
                    '    Throw New Exception("Fill name at line no. " + clsCommon.myCstr(counter + 1) + "")
                    'End If
                    'If clsCommon.myLen(obj.Name) > 200 Then
                    '    obj.Name = obj.Name.Substring(0, 200)
                    'End If

                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_BULL_MASTER where Bull_Code='" + obj.Bull_Code + "'")
                    isNewEntry = True
                    If qry > 0 Then
                        isNewEntry = False
                    End If

                    If (obj.SaveData(obj, isNewEntry)) Then

                    End If
                    counter += 1
                Next

                clsCommon.ProgressBarHide()

                If counter >= 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to transfer", Me.Text)
                End If


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub

    Private Sub fndBullSourcePainting__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullSourcePainting._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_Source_Printing  "
            fndBullSourcePainting.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBullSourcePainting.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndBullRating__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullRating._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_Rating  "
            fndBullRating.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBullRating.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCounty__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCounty._MYValidating
        Try
            Dim qry As String = " select COUNTRY_CODE as Code, COUNTRY_NAME  as Name from TSPL_COUNTRY_MASTER  "
            fndCounty.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndCounty.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndSourceName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSourceName._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name as Name from TSPL_BULL_SOURCE_NAME  "
            fndSourceName.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSourceName.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadBlankGridColmns()
        Dim qry As String = String.Empty
        gv2.Rows.Clear()
        gv2.Columns.Clear()
        gv2.DataSource = Nothing
        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        'S.NO column'
        Dim gridColCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColCode.FormatString = ""
        gridColCode.HeaderText = "Code"
        gridColCode.Name = ColCode
        gridColCode.Width = 105
        gridColCode.ReadOnly = False

        gridColCode.IsVisible = False


        gv2.MasterTemplate.Columns.Add(gridColCode)


        Dim gridColRelation As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        gridColRelation.FormatString = ""
        gridColRelation.HeaderText = "Relation"
        gridColRelation.Name = ColRelation
        gridColRelation.DataSource = {"Mother", "Father", "Son", "Daughter"}

        gridColRelation.FormatString = ""
        'gridColRelation.DecimalPlaces = 0
        gridColRelation.Width = 52
        gridColRelation.ReadOnly = False

        gv2.MasterTemplate.Columns.Add(gridColRelation)

        'Code column'
        Dim gridColTaqNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColTaqNo.FormatString = ""
        gridColTaqNo.HeaderText = "Taq No"
        gridColTaqNo.Name = ColTaqNo
        gridColTaqNo.Width = 105
        gridColTaqNo.ReadOnly = False

        gv2.MasterTemplate.Columns.Add(gridColTaqNo)

        'Item Name column'
        Dim gridColFirstStandardLocationYield As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColFirstStandardLocationYield.FormatString = ""
        gridColFirstStandardLocationYield.HeaderText = "First Standard Location Yield"
        gridColFirstStandardLocationYield.Name = ColFirstStandardLocationYield
        gridColFirstStandardLocationYield.Width = 105
        gridColFirstStandardLocationYield.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(gridColFirstStandardLocationYield)

        'Unit column'
        Dim gridColFatPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridColFatPercent.FormatString = ""
        gridColFatPercent.HeaderText = "Fat Percent"
        gridColFatPercent.Name = ColFatPercent
        gridColFatPercent.Width = 105
        gridColFatPercent.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(gridColFatPercent)


        'Quantity column'
        Dim gridColSnfPercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridColSnfPercent.FormatString = ""
        gridColSnfPercent.HeaderText = "Snf Percent"
        gridColSnfPercent.Name = ColSnfPercent
        gridColSnfPercent.Width = 105
        gridColSnfPercent.Minimum = 0
        gridColSnfPercent.ReadOnly = False
        gridColSnfPercent.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv2.MasterTemplate.Columns.Add(gridColSnfPercent)

        'Rate column'
        Dim gridColProtien As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColProtien.FormatString = ""
        gridColProtien.HeaderText = "Protien"
        gridColProtien.Name = ColProtien
        gridColProtien.ReadOnly = False
        gridColProtien.Width = 105
        gv2.MasterTemplate.Columns.Add(gridColProtien)

        'Amount column'
        Dim gridColLactose As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColLactose.FormatString = ""
        gridColLactose.HeaderText = "Lactose"
        gridColLactose.Name = ColLactose
        gridColLactose.Width = 105
        gridColLactose.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(gridColLactose)


        Dim gridColDateOfPedigreeInformationUpdate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        gridColDateOfPedigreeInformationUpdate.FormatString = "{0:dd/MM/yyyy}"
        gridColDateOfPedigreeInformationUpdate.HeaderText = "Date Of Pedigree Information Update"
        gridColDateOfPedigreeInformationUpdate.Name = ColDateOfPedigreeInformationUpdate
        gridColDateOfPedigreeInformationUpdate.ReadOnly = False
        gridColDateOfPedigreeInformationUpdate.Width = 90
        gv2.MasterTemplate.Columns.Add(gridColDateOfPedigreeInformationUpdate)

        'Tax after amount column'

        Dim gridColSCC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColSCC.FormatString = ""
        gridColSCC.HeaderText = "SCC"
        gridColSCC.Name = ColSCC
        gridColSCC.ReadOnly = False
        gridColSCC.Width = 105
        gv2.MasterTemplate.Columns.Add(gridColSCC)

        'Amount after tax column'

        Dim gridColMUN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridColMUN.FormatString = ""
        gridColMUN.HeaderText = "MUN"
        gridColMUN.Name = ColMUN
        gridColMUN.ReadOnly = False
        gridColMUN.Width = 135
        gv2.MasterTemplate.Columns.Add(gridColMUN)
        gv2.Rows.AddNew()
        gv2.AllowAddNewRow = False

        'Discount percent
        Dim gridBestStandardLocationYield As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridBestStandardLocationYield.FormatString = ""
        gridBestStandardLocationYield.HeaderText = "Best Standard Location Yield"
        gridBestStandardLocationYield.Name = ColBestStandardLocationYield
        gridBestStandardLocationYield.ReadOnly = False
        gridBestStandardLocationYield.Width = 90
        gv2.MasterTemplate.Columns.Add(gridBestStandardLocationYield)

        'Discount Amount column'

        Dim gridLactNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridLactNo.FormatString = ""
        gridLactNo.HeaderText = "LactNo"
        gridLactNo.Name = ColLactNo
        gridLactNo.ReadOnly = False
        gridLactNo.Width = 125
        gv2.MasterTemplate.Columns.Add(gridLactNo)

        Dim gridFATKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridFATKG.FormatString = ""
        gridFATKG.Name = ColFATINKG
        gridFATKG.HeaderText = "FAT IN KG"
        gridFATKG.ReadOnly = False
        gridFATKG.Width = 135
        gv2.MasterTemplate.Columns.Add(gridFATKG)

        Dim gridSNFKG As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridSNFKG.FormatString = ""
        gridSNFKG.Name = ColSNFINKG
        gridSNFKG.HeaderText = "SNF IN KG"
        gridSNFKG.ReadOnly = False
        gridSNFKG.Width = 135
        gv2.MasterTemplate.Columns.Add(gridSNFKG)


        gv2.AllowAddNewRow = False
        gv2.AllowDeleteRow = True
        gv2.AllowRowReorder = False
        gv2.ShowGroupPanel = False
        gv2.EnableFiltering = False
        gv2.EnableSorting = False
        gv2.EnableGrouping = False
        gv2.AllowColumnChooser = True
        gv2.AllowColumnReorder = True
        'gv2.Rows.AddNew()
        ReStoreGridLayout()
    End Sub

    Private Sub gv2_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv2.CurrentColumnChanged
        If gv2.RowCount > 0 Then
            Dim intCurrRow As Integer = gv2.CurrentRow.Index
            If intCurrRow = gv2.Rows.Count - 1 Then
                gv2.Rows.AddNew()
                gv2.CurrentRow = gv2.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv2.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv2.Columns.Count - 1 Step ii + 1
                        gv2.Columns(ii).IsVisible = False
                        gv2.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv2.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub btnSelectPath1_Click(sender As Object, e As EventArgs) Handles btnSelectPath1.Click
        Try
            OpenFileDialog1.Filter = " image |*.gif;  *.jpg;  *.bmp;  *.png;"
            OpenFileDialog1.ShowDialog()
            If clsCommon.CompairString(clsCommon.myCstr(sender.Name), "btnSelectPath1") = CompairStringResult.Equal Then
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv2_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv2.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocus()
        If gv2.CurrentCell IsNot Nothing AndAlso gv2.Rows.Count > 0 Then
            Dim setnxtRow As Boolean = False
            If gv2.CurrentCell.ColumnInfo.Name = ColRelation Then
                gv2.CurrentColumn = gv2.Columns(ColTaqNo)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColTaqNo Then
                gv2.CurrentColumn = gv2.Columns(ColFirstStandardLocationYield)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColFirstStandardLocationYield Then
                gv2.CurrentColumn = gv2.Columns(ColFatPercent)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColFatPercent Then
                gv2.CurrentColumn = gv2.Columns(ColSnfPercent)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColSnfPercent Then
                gv2.CurrentColumn = gv2.Columns(ColProtien)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColProtien Then
                gv2.CurrentColumn = gv2.Columns(ColLactose)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColLactose Then
                gv2.CurrentColumn = gv2.Columns(ColDateOfPedigreeInformationUpdate)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColDateOfPedigreeInformationUpdate Then
                gv2.CurrentColumn = gv2.Columns(ColSCC)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColSCC Then
                gv2.CurrentColumn = gv2.Columns(ColMUN)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColMUN Then
                gv2.CurrentColumn = gv2.Columns(ColBestStandardLocationYield)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColBestStandardLocationYield Then
                gv2.CurrentColumn = gv2.Columns(ColLactNo)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColLactNo Then
                gv2.CurrentColumn = gv2.Columns(ColFATINKG)
            ElseIf gv2.CurrentCell.ColumnInfo.Name = ColFATINKG Then
                gv2.CurrentColumn = gv2.Columns(ColSNFINKG)

                setnxtRow = True
                gv2.CurrentColumn = gv2.Columns(ColRelation)
            End If

            If setnxtRow Then
                Dim nextRowIndex As Integer = gv2.CurrentRow.Index + 1
                If nextRowIndex < gv2.Rows.Count Then
                    gv2.CurrentRow = gv2.Rows(nextRowIndex)
                    gv2.CurrentColumn = gv2.Columns(ColRelation)
                End If
            End If
        End If
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "select distinct Code, Taq_No,Relation,First_Standard_Location_Yield,Fat_Percent,Snf_Percent,Protien,Lactose,Date_Of_Pedigree_Information_Update,SCC,MUN,Best_Standard_Location_Yield,Lact_No,FAT_IN_KG,SNF_IN_KG FROM tspl_bull_master_DETAIL"
            'Dim WhrCls As String = " TSPL_ITEM_TYPE_MASTER.IsVaccine = 'Y' "
            Dim dt As System.Data.DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'gv2.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, gv2.CurrentRow.Cells(colItemCode).Value, "Code", isButtonClick)
                'If clsCommon.myLen(gv2.CurrentRow.Cells(ColCode).Value) > 0 Then
                'gv2.CurrentRow.Cells(ColRelation).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from tspl_item_master where Item_Code='" + gv2.CurrentRow.Cells(colItemCode).Value + "'"))
                'gv2.CurrentRow.Cells(ColTaqNo).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM = 1 and Item_Code='" + gv2.CurrentRow.Cells(colItemCode).Value + "'"))
                gv2.CurrentRow.Cells(ColCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                gv2.CurrentRow.Cells(ColRelation).Value = clsCommon.myCstr(dt.Rows(0)("Relation"))
                gv2.CurrentRow.Cells(ColTaqNo).Value = clsCommon.myCstr(dt.Rows(0)("Taq_No"))
                gv2.CurrentRow.Cells(ColFirstStandardLocationYield).Value = clsCommon.myCstr(dt.Rows(0)("First_Standard_Location_Yield"))
                gv2.CurrentRow.Cells(ColFatPercent).Value = clsCommon.myCdbl(dt.Rows(0)("Fat_Percent"))
                gv2.CurrentRow.Cells(ColSnfPercent).Value = clsCommon.myCdbl(dt.Rows(0)("Snf_Percent"))
                gv2.CurrentRow.Cells(ColProtien).Value = clsCommon.myCstr(dt.Rows(0)("Protien"))
                gv2.CurrentRow.Cells(ColLactose).Value = clsCommon.myCstr(dt.Rows(0)("Lactose"))
                gv2.CurrentRow.Cells(ColDateOfPedigreeInformationUpdate).Value = clsCommon.myCDate(dt.Rows(0)("Date_Of_Pedigree_Information_Update"))
                gv2.CurrentRow.Cells(ColSCC).Value = clsCommon.myCstr(dt.Rows(0)("SCC"))
                gv2.CurrentRow.Cells(ColMUN).Value = clsCommon.myCstr(dt.Rows(0)("MUN"))
                gv2.CurrentRow.Cells(ColBestStandardLocationYield).Value = clsCommon.myCstr(dt.Rows(0)("Best_Standard_Location_Yield"))
                gv2.CurrentRow.Cells(ColLactNo).Value = clsCommon.myCstr(dt.Rows(0)("Lact_No"))
                gv2.CurrentRow.Cells(ColSNFINKG).Value = clsCommon.myCstr(dt.Rows(0)("FAT_IN_KG"))
                gv2.CurrentRow.Cells(ColFATINKG).Value = clsCommon.myCstr(dt.Rows(0)("SNF_IN_KG"))

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub
    Private Sub gv2_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gv2.Columns(ColCode) Then
                        OpenICodeList(False)
                    End If

                    'If e.Column Is gv1.Columns(colUnitCode) Then
                    '    OpenUOMList(False)
                    'End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_MASTER where Bull_Code='" + fndCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class