
Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmBullMasters
    Inherits FrmMainTranScreen
    Dim isNewEntry As Boolean = True
    Dim ErrorControl As New clsErrorControl()
    'Private txtRegDate As Date
    Private Sub fndSpecies__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSpecies._MYValidating
        Try

            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SPECIES_MASTER  "
            'Emp_type = 'Salesman' and Emp_Status = 'Active'
            fndSpecies.Value = clsCommon.ShowSelectForm("Species", qry, "Code", "", fndSpecies.Value, "Code", isButtonClicked)
            ' txtSupervisorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  Emp_Name   from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + fndSupervisorCode.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCategory._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_CATEGORY_MASTER  "
            fndCategory.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndCategory.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCountry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)

    End Sub

    Private Sub fndSubCategory__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSubCategory._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SUB_CATEGORY_MASTER  "
            fndSubCategory.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSubCategory.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndSSCentre__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSSCentre._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SS_CENTRE_MASTER  "
            fndSSCentre.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSSCentre.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndBreed__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBreed._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_BREED_MASTER  "
            fndBreed.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBreed.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndShedId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndShedId._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SHED_MASTER  "
            fndShedId.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndShedId.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndPenId__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPenId._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_PEN_ID_MASTER  "
            fndPenId.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndPenId.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndBullStatus__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullStatus._MYValidating
        Try
            Dim qry As String = " select STSTUS_Code as Code, Name  as Name from TSPL_BULL_STATUS_MASTER  "
            fndBullStatus.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBullStatus.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndSubStatus__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSubStatus._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_SUB_STATUS_MASTER  "
            fndSubStatus.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSubStatus.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
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
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isNewEntry = True
            btnDelete.Enabled = False
            btnSave.Enabled = True
            btnSave.Text = "Save"
            fndCode.MyReadOnly = False

            Dim obj As clsBullMasters = clsBullMasters.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
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
                fndBullSourcePainting.Value = obj.Bull_source_Printing_Straws
                If obj.is_Semen Then
                    RadioButton2.Checked = True
                Else
                    RadioButton1.Checked = True
                End If
                'If obj.is_Semen Then
                '    RadioButton2.Checked = True
                'Else
                '    RadioButton1.Checked = True
                'End If

                TXTExoticBloodPer.Text = obj.Exotic_Blood_Per
                txtBullBook.Text = obj.Bull_Book_Value
                fndCounty.Value = obj.Country_Code
                'DigitBullId.Text = obj.Digit_Bull_Id
                TXTPrevBull.Text = obj.Prev_Bull_Id
                txtDateOfBirth.Value = obj.Date_Of_Birth
                txtRegDate.Value = obj.Registration_Date
                txtStatusDateChanged.Value = obj.Status_Changed_Date
                txtEndDate.Value = obj.Exit_Date
                txtRemark.Text = obj.Remark
                fndCode.MyReadOnly = True
                'btnSave.Text = "Save"
                btnDelete.Enabled = True
            Else
                'AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(fndCode.Value) <= 0 Then
            fndCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please select Code", Me.Text)
            Return False
        End If
        'If clsCommon.myLen(txtPurchaseNo.Text) <= 0 Then
        '    txtPurchaseRequestDate.Enabled = False
        '    txtPurchaseDate.Enabled = False

        '    txtPurchaseNo.Focus()
        '    clsCommon.MyMessageBoxShow(Me, "Please select Purchase No", Me.Text)
        '    Return False

        'End If
        'If clsCommon.myLen(txtPurchaseNo.Text) > 0 Then
        '    txtPurchaseRequestDate.Enabled = True
        '    txtPurchaseDate.Enabled = True

        '    txtPurchaseNo.Focus()
        'End If
        'If clsCommon.myLen(txtPurchaseNo.Text) > 0 Then
        '    txtPurchaseRequestDate.Enabled = True
        '    txtPurchaseDate.EnableCodedUITests = True
        'Else
        '    txtPurchaseRequestDate.Enabled = False
        '    txtPurchaseDate.EnableCodedUITests = False
        'End If
        'Return True
    End Function
    Private Sub SaveData()
        Try
            'If (AllowToSave()) Then
            Dim obj As New clsBullMasters()
                'obj.Date_Of_Birth = txtDob.Value
                If IsNumeric(fndCode.Value) AndAlso clsCommon.myLen(fndCode.Value) >= 12 Then
                    obj.Bull_Code = clsCommon.myCstr(fndCode.Value)
                Else
                    clsCommon.MyMessageBoxShow("fill 12 Digit Bull id", Me.Text)
                    fndCode.Focus()
                    fndCode.Text = ""
                    Exit Sub
                End If
                'obj.Code = fndCode.Value
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
            'If obj.is_Semen Then
            '    obj.RadioButton2 = True
            'Else
            '    obj.RadioButton2 = True
            'End If
            If IsNumeric(TXTExoticBloodPer.Text) Then
                    obj.Exotic_Blood_Per = clsCommon.myCdbl(TXTExoticBloodPer.Text)
                Else
                    clsCommon.MyMessageBoxShow("Please enter a valid percentage", Me.Text)
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
                'obj.Purchase_Request_No = txtPurchaseNo.Text
                'obj.Purchase_Date = txtPurchaseRequestDate.Value
                'obj.Purchase_Request_Date = txtPurchaseDate.Value
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
            If (obj.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    LoadData(obj.Bull_Code, NavigatorType.Current)
                End If
            'End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmBullMasters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txtPurchaseRequestDate.Enabled = False
        'txtPurchaseDate.Enabled = False
        AddNew()
        'txtStatusDateChanged.Value = clsCommon.GETSERVERDATE()
        'txtDob.Value = clsCommon.GETSERVERDATE()
        'txtRegDate.Value = clsCommon.GETSERVERDATE()
        'txtEndDate.Value = clsCommon.GETSERVERDATE()
        'txtDob1.Value = clsCommon.GETSERVERDATE()
        'txtDob.Value = clsCommon.GETSERVERDATE()
        'txtDateOfBirth.Value = clsCommon.GETSERVERDATE()
    End Sub

    Public Sub AddNew()
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select count(*) from TSPL_BULL_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Bull_Code,Species_Code,Category_Code,Sub_Category_Code,SS_Centre_Code,Breed_Code,Shed_Code,Pen_Code,Status_Code,Sub_Status_Code,Exotic_Blood_Per,Bull_Book_Value,Country_Code,Prev_Bull_Id,SS_Bull_Id,Bull_Alia_Name,Bull_Rating,Dam_Location_Yeild,Bull_source_Printing_Straws,Is_Semen,Bull_RFID,Remark,Registration_Date,Date_Of_Birth,Status_Changed_Date,Exit_Date from TSPL_BULL_MASTER"
        Else
            qry = "select '' as Bull_Code"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Bull_Code", "Name") Then
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
                    clsCommon.MyMessageBoxShow("Data transfer successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("No data found to transfer", Me.Text)
                End If


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(ex.Message)
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
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndBullRating__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndBullRating._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name  as Name from TSPL_BULL_Rating  "
            fndBullRating.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndBullRating.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndCounty__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCounty._MYValidating
        Try
            Dim qry As String = " select COUNTRY_CODE as Code, COUNTRY_NAME  as Name from TSPL_COUNTRY_MASTER  "
            fndCounty.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndCounty.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndSourceName__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSourceName._MYValidating
        Try
            Dim qry As String = " select Code as Code, Name as Name from TSPL_BULL_SOURCE_NAME  "
            fndSourceName.Value = clsCommon.ShowSelectForm("Category", qry, "Code", "", fndSourceName.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub













    'Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Try
    '        isNewEntry = True
    '        btnDelete.Enabled = False
    '        btnSave.Enabled = True
    '        btnSave.Text = "Save"
    '        fndCode.MyReadOnly = False

    '        Dim obj As clsBullMasters = clsBullMasters.GetData(strCode, NavTyep)
    '        If obj IsNot Nothing Then
    '            isNewEntry = False
    '            fndCode.Value = obj.Code
    '            fndCategory.Value = obj.Category_Code
    '            fndSpecies.Value = obj.Species_Code
    '            'txtname.Text = obj.Name
    '            fndCode.MyReadOnly = True
    '            'btnsave.Text = "Update"
    '            btnDelete.Enabled = True
    '        Else
    '            'AddNew()
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub
End Class