Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'==========================Created by Preeti gupta================
Public Class FrmSalesHierarchyMain
    Inherits FrmMainTranScreen
    Dim isnewentry As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isFlag As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Dim atchqry As String = ""
    Dim dt As DataTable
    Public Code As String
    Dim qryLevel As String
    Public CreateNewTransaction As Boolean = False

    Function allowtosave()
        If clsCommon.myLen(clsCommon.myCstr(txtcode.Value)) <= 0 Then
            myMessages.blankValue(Me, "Code ", Me.Text)
            txtcode.Focus()
            txtcode.Select()
            Errorcontrol.SetError(txtcode, "Code ")
            Return False
        Else
            Errorcontrol.ResetError(fndLevelCode)
        End If

        If clsCommon.myLen(clsCommon.myCstr(fndLevelCode.Value)) <= 0 Then
            myMessages.blankValue(Me, "Level ", Me.Text)
            fndLevelCode.Focus()
            fndLevelCode.Select()
            Errorcontrol.SetError(fndLevelCode, "Level ")
            Return False
        Else
            Errorcontrol.ResetError(fndLevelCode)
        End If
        If clsCommon.CompairString(txtSubType.Text, "O") <> CompairStringResult.Equal Then
            If clsCommon.myLen(fndSource.Value) <= 0 Then
                Errorcontrol.SetError(fndSource, "Source Doc ")
                Return False
            End If
        End If

        'If clsCommon.myLen(clsCommon.myCstr(fndParentStructCode.Value)) <= 0 Then
        '    myMessages.blankValue("Parent Struct Code ")
        '    fndParentStructCode.Focus()
        '    fndParentStructCode.Select()
        '    Errorcontrol.SetError(fndParentStructCode, "Parent Struct Code ")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(fndParentStructCode)
        'End If


        Return True
    End Function
    Sub savedata()
        Try
            If (allowtosave()) Then

                BtnSave.Focus()

                Dim entry As String
                Dim count As Integer = 0
                Dim i As Integer = 0
                Dim qry As String = "select count(*) from TSPL_Sales_Hierarchy_Structure  where Struct_Code ='" + txtcode.Value + "'"
                count = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    isnewentry = True
                Else
                    isnewentry = False

                End If


                Dim SalesHier As New clsSalesHierarchy
                SalesHier.DOC_CODE = clsCommon.myCstr(txtcode.Value)

                SalesHier.Description = clsCommon.myCstr(txtDescription.Text)
                SalesHier.LevelCode = clsCommon.myCstr(fndLevelCode.Value)
                SalesHier.ParentStructCode = clsCommon.myCstr(fndParentStructCode.Value)
                SalesHier.Applicable_From = dtpApplicableFrom.Value
                SalesHier.Source_Doc = fndSource.Value

                If clsSalesHierarchy.savedata(SalesHier, isnewentry) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    entry = SalesHier.DOC_CODE
                    getdata(SalesHier.DOC_CODE, NavigatorType.Current)
                    BtnSave.Text = "Update"
                    btnDelete.Enabled = True
                Else
                    BtnSave.Text = "Save"
                    btnDelete.Enabled = False
                End If
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub


    Sub getdata(ByVal entry As String, ByVal navigatortype As NavigatorType)
        Try
            Dim obj As clsSalesHierarchy = clsSalesHierarchy.getdata(entry, navigatortype)
            If obj IsNot Nothing Then
                txtcode.Value = obj.DOC_CODE
                txtDescription.Text = obj.Description
                fndLevelCode.Value = obj.LevelCode
                txtlevelName.Text = obj.LevelName
                fndParentStructCode.Value = obj.ParentStructCode
                lblParentStructName.Text = obj.ParentStructName
                dtpApplicableFrom.Value = obj.Applicable_From
                txtType.Text = obj.Level_Type
                txtSubType.Text = obj.Sub_Type
                fndSource.Value = obj.Source_Doc
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsSalesHierarchy.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    resetdata()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("code not found to delete")
            Exit Sub
        End If
        funDelete()
    End Sub
    Sub resetdata()
        txtcode.Value = ""
        txtDescription.Text = ""
        fndLevelCode.Value = ""
        txtlevelName.Text = ""
        fndParentStructCode.Value = ""
        lblParentStructName.Text = ""
        txtType.Text = ""
        txtSubType.Text = ""
        fndSource.Value = ""
        txtcode.MyReadOnly = False
        BtnSave.Text = "Save"
        
    End Sub

    Private Sub FrmSalesHierarchyMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If clsCommon.myLen(Code) > 0 Then
            getdata(Code, NavigatorType.Current)
        End If
        If CreateNewTransaction Then
            resetdata()

        End If
    End Sub

    Private Sub FrmSalesHierarchyMain_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub txtcode__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtcode._MYNavigator
        getdata(txtcode.Value, NavType)
    End Sub

    Private Sub txtcode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtcode._MYValidating
       
            Dim str As String = "select count(*) from TSPL_Sales_Hierarchy_Structure where struct_code ='" + txtcode.Value + "' "
            Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
            If no = 0 AndAlso isButtonClicked = False Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If

            If txtcode.MyReadOnly OrElse isButtonClicked Then
                Dim qry As String = ""
                

                qry = "select TSPL_Sales_Hierarchy_Structure.Struct_Code as [Code],TSPL_Sales_Hierarchy_Structure.Description ,TSPL_Sales_Hierarchy_Structure.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Levels.Description as [Level Description],TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code as [Parent Struct Code] from TSPL_Sales_Hierarchy_Structure left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code  "
                str = clsCommon.ShowSelectForm("SalesHierarchy", qry, "Code", "", txtcode.Value, "Code", isButtonClicked)
            txtcode.Value = str
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Dim objOT As clsSalesHierarchy
                objOT = clsSalesHierarchy.getdata(txtcode.Value, NavigatorType.Current)
                If Not objOT Is Nothing Then
                    getdata(txtcode.Value, NavigatorType.Current)
                End If
            Else
                resetdata()
            End If
            End If
    End Sub
     

    Private Sub fndLevelCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLevelCode._MYValidating
         Dim qry As String

        qry = " select TSPL_Sales_Hierarchy_Levels.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Levels.Description ,TSPL_Sales_Hierarchy_Levels.Seq_No as [Seq No] FROM TSPL_Sales_Hierarchy_Levels"
        fndLevelCode.Value = clsCommon.ShowSelectForm("Resig", qry, "Level Code", "", fndLevelCode.Value, "", isButtonClicked)

        If clsCommon.myLen(fndLevelCode.Value) > 0 Then
            Dim obj As ClsSaleLevelHierarchy = ClsSaleLevelHierarchy.GetData(fndLevelCode.Value, Nothing, NavigatorType.Current)
            txtlevelName.Text = obj.Description 'clsDBFuncationality.getSingleValue("select TSPL_Sales_Hierarchy_Levels.Description  FROM TSPL_Sales_Hierarchy_Levels  where TSPL_Sales_Hierarchy_Levels.Level_code='" + fndLevelCode.Value + "' ")
            txtType.Text = obj.Level_Type
            txtSubType.Text = obj.Sub_Type
        Else
            txtlevelName.Text = ""
            txtType.Text = ""
            txtSubType.Text = ""
        End If
    End Sub

    Private Sub fndParentStructCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndParentStructCode._MYValidating
        Dim qry As String = ""
        qryLevel = clsDBFuncationality.getSingleValue("select TSPL_Sales_Hierarchy_Levels.Seq_No  from TSPL_Sales_Hierarchy_Levels where Level_Code ='" + fndLevelCode.Value + "'")
        qry = "	select TSPL_Sales_Hierarchy_Structure.Struct_Code as [Code],TSPL_Sales_Hierarchy_Structure.Description ,TSPL_Sales_Hierarchy_Structure.Level_Code as [Level Code] ,TSPL_Sales_Hierarchy_Levels.Description as [Level Description],TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code as [Parent Struct Code],TSPL_Sales_Hierarchy_Structure_for_parent.description [Parent Struct Name],TSPL_Sales_Hierarchy_Structure_for_parent.Description as [Parent Struct Name],TSPL_Sales_Hierarchy_Levels.Seq_No  as [Seq No] from TSPL_Sales_Hierarchy_Structure left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code left outer join TSPL_Sales_Hierarchy_Structure as TSPL_Sales_Hierarchy_Structure_for_parent on TSPL_Sales_Hierarchy_Structure_for_parent.Struct_Code=TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code"
        fndParentStructCode.Value = clsCommon.ShowSelectForm("SalesHierarchy", qry, "Code", "TSPL_Sales_Hierarchy_Levels.Seq_No = " & qryLevel & "-1", fndParentStructCode.Value, "TSPL_Sales_Hierarchy_Structure.Struct_Code", isButtonClicked)
        If clsCommon.myLen(fndParentStructCode.Value) > 0 Then
            Dim objOT As clsTragetMasterHeadProductSale
            objOT = clsTragetMasterHeadProductSale.GetData(fndParentStructCode.Value, NavigatorType.Current)
            If Not objOT Is Nothing Then
                getdata(fndParentStructCode.Value, NavigatorType.Current)
            End If
        End If
        If clsCommon.myLen(fndParentStructCode.Value) > 0 Then
            lblParentStructName.Text = clsDBFuncationality.getSingleValue("select TSPL_Sales_Hierarchy_Structure.Description as [Parent Struct Name] from TSPL_Sales_Hierarchy_Structure left outer join TSPL_Sales_Hierarchy_Levels on TSPL_Sales_Hierarchy_Levels.Level_Code =TSPL_Sales_Hierarchy_Structure.Level_Code left outer join TSPL_Sales_Hierarchy_Structure as TSPL_Sales_Hierarchy_Structure_for_parent on TSPL_Sales_Hierarchy_Structure_for_parent.Parent_Struct_Code=TSPL_Sales_Hierarchy_Structure.Parent_Struct_Code where TSPL_Sales_Hierarchy_Structure.Struct_Code='" + fndParentStructCode.Value + "' ")

        Else
            lblParentStructName.Text = ""

        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        savedata()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        resetdata()
    End Sub

    Private Sub fndSource__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndSource._MYValidating
        FinderForSourceDoc(txtSubType.Text, isButtonClicked)
    End Sub
    Public Sub FinderForSourceDoc(ByVal SubType As String, isButtonClicked As Boolean)
        Dim qry As String = ""
        If clsCommon.CompairString(SubType, "EMP") = CompairStringResult.Equal Then           
            fndSource.Value = clsEmployeeMaster.getFinder("", fndSource.Value, isButtonClicked)
            txtcode.Value = fndSource.Value
            If clsCommon.myLen(txtcode.Value) > 0 Then
                lblSourceDocDesc.Text = clsEmployeeMaster.GetName(txtcode.Value, Nothing)
                txtDescription.Text = lblSourceDocDesc.Text
            Else
                lblSourceDocDesc.Text = ""
                txtDescription.Text = ""
            End If
        ElseIf clsCommon.CompairString(SubType, "COMP") = CompairStringResult.Equal Then
            fndSource.Value = clsCompanyMaster.getFinder("", fndSource.Value, isButtonClicked)
            txtcode.Value = fndSource.Value
            If clsCommon.myLen(txtcode.Value) > 0 Then
                lblSourceDocDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code='" & txtcode.Value & "'"))
                txtDescription.Text = lblSourceDocDesc.Text
            Else
                lblSourceDocDesc.Text = ""
                txtDescription.Text = ""
            End If
        ElseIf clsCommon.CompairString(SubType, "C") = CompairStringResult.Equal Then
            fndSource.Value = clsCountryMaster.getFinder("", fndSource.Value, isButtonClicked)
            txtcode.Value = fndSource.Value
            If clsCommon.myLen(txtcode.Value) > 0 Then
                lblSourceDocDesc.Text = clsCountryMaster.GetName(txtcode.Value, Nothing)
                txtDescription.Text = lblSourceDocDesc.Text
            Else
                lblSourceDocDesc.Text = ""
                txtDescription.Text = ""
            End If
        ElseIf clsCommon.CompairString(SubType, "ST") = CompairStringResult.Equal Then
            fndSource.Value = clsStateMaster.getFinder("", fndSource.Value, isButtonClicked)
            txtcode.Value = fndSource.Value
            If clsCommon.myLen(txtcode.Value) > 0 Then
                lblSourceDocDesc.Text = clsStateMaster.GetName(txtcode.Value)
                txtDescription.Text = lblSourceDocDesc.Text
            Else
                lblSourceDocDesc.Text = ""
                txtDescription.Text = ""
            End If
        ElseIf clsCommon.CompairString(SubType, "CT") = CompairStringResult.Equal Then
            fndSource.Value = clsCityMaster.getFinder("", fndSource.Value, isButtonClicked)
            txtcode.Value = fndSource.Value
            If clsCommon.myLen(txtcode.Value) > 0 Then
                lblSourceDocDesc.Text = clsCityMaster.GetName(txtcode.Value)
                txtDescription.Text = lblSourceDocDesc.Text
            Else
                lblSourceDocDesc.Text = ""
                txtDescription.Text = ""
            End If
        ElseIf clsCommon.CompairString(SubType, "Z") = CompairStringResult.Equal Then
            fndSource.Value = ClsZoneMaster.getFinder("", fndSource.Value, isButtonClicked)
            txtcode.Value = fndSource.Value
            If clsCommon.myLen(txtcode.Value) > 0 Then
                lblSourceDocDesc.Text = ClsZoneMaster.GetName(txtcode.Value)
                txtDescription.Text = lblSourceDocDesc.Text
            Else
                lblSourceDocDesc.Text = ""
                txtDescription.Text = ""
            End If
        ElseIf clsCommon.CompairString(SubType, "O") = CompairStringResult.Equal Then
        End If

    End Sub

End Class
