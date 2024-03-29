'' work done history related agaist ticket no. TEC/08/06/18-000280
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports CrystalDecisions.CrystalReports.Engine

Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports ZXing

Public Class clsERPFuncationalityOLD
    Public Shared Function AddToHistory(ByVal arrDocList As List(Of String), Form_Id As String, ByVal trans As SqlTransaction) As Boolean
        Dim isSaved As Boolean = True
        Dim obj = Nothing
        If clsCommon.CompairString(Form_Id, clsUserMgtCode.frmQualityCheck) = CompairStringResult.Equal Then

            Dim ChllanNo As String = ""
            Dim arrPaperSeal As List(Of clsQCPaperSealDetail) = Nothing
            Dim arrManualSeal As List(Of clsQCManualSealDetail) = Nothing
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsQualityCheck.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    If clsQualityCheck.saveData(obj, trans, True) Then
                        If clsCommon.CompairString(obj.Doc_Type, "BulkProc") <> CompairStringResult.Equal Then
                            arrPaperSeal = clsQCPaperSealDetail.getData(obj.Challan_No, trans)
                            arrManualSeal = clsQCManualSealDetail.getData(obj.Challan_No, trans)
                            If arrPaperSeal IsNot Nothing AndAlso arrPaperSeal.Count > 0 Then
                                isSaved = clsQCPaperSealDetail.SaveData(arrPaperSeal, trans, True)
                            End If
                            If arrManualSeal IsNot Nothing AndAlso arrManualSeal.Count > 0 Then
                                isSaved = clsQCManualSealDetail.SaveData(arrManualSeal, trans, True)
                            End If
                        End If
                    End If
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.frmWeighment) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsWeighment.getData(arrDocList.Item(i), NavigatorType.Current, False, trans)
                If obj IsNot Nothing Then
                    isSaved = clsWeighment.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.frmUnloading) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsUnloading.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsUnloading.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.frmGateEntry) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsGateEntry.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsGateEntry.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.frmCleaning) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsCleaning.getData(arrDocList.Item(i), NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsCleaning.saveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.FrmWeighmentEntry) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsWeighmentEntry.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsWeighmentEntry.SaveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.FrmLoadingTanker) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsLoadingTanker.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsLoadingTanker.SaveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.FrmQualityCheckBulkSale) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = ClsQualityCheckBulkSale.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = ClsQualityCheckBulkSale.SaveData(obj, trans, True)
                End If
            Next
        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.FrmGateEntrySale) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsGateEntrySale.GetData(arrDocList.Item(i), "", NavigatorType.Current, trans)
                If obj IsNot Nothing Then
                    isSaved = clsGateEntrySale.SaveDataHistory(obj, trans, True)
                End If
            Next

        ElseIf clsCommon.CompairString(Form_Id, clsUserMgtCode.frmBulkMilkSRN) = CompairStringResult.Equal Then
            For i As Integer = 0 To arrDocList.Count - 1
                obj = clsBulkMilkSRN.getData(arrDocList.Item(i), NavigatorType.Current, False, trans)
                If obj IsNot Nothing Then
                    isSaved = clsBulkMilkSRN.saveData(obj, trans, True)
                End If
            Next

        End If
        Return True
    End Function

    Public Shared Sub ShowAlert(ByVal message As String, Optional ByVal Caption As String = "A new message for you", Optional ByVal isAutoClose As Boolean = False, Optional ByVal alertPosition As AlertScreenPosition = AlertScreenPosition.BottomRight)
        Dim rAlert As RadDesktopAlert = New RadDesktopAlert()
        rAlert.AutoClose = isAutoClose
        rAlert.CaptionText = Caption
        rAlert.ContentText = message
        rAlert.CanMove = True
        rAlert.ScreenPosition = alertPosition
        rAlert.ShowCloseButton = True
        rAlert.ShowPinButton = True
        rAlert.Show()
    End Sub
    Public Shared Function GetTecxpertPaperSizeName(ByVal En As EnumTecxpertPaperSize) As String
        Dim str As String = ""
        Select Case En
            Case EnumTecxpertPaperSize.PaperSize10x12
                str = "Tecxpert 10x12"
            Case EnumTecxpertPaperSize.PaperSize10x6
                str = "Tecxpert 10x6"
            Case EnumTecxpertPaperSize.Guntur10x12
                str = "Guntur 10x12"
            Case EnumTecxpertPaperSize.HalfLegal85x7
                str = "Halflegal 8.5x7"
        End Select
        Return str
    End Function
    Public Shared Function SetCustomizedPaperSize(ByRef rpdoc As ReportDocument, ByVal ePaperSize As EnumTecxpertPaperSize)
        Try
            If ePaperSize <> EnumTecxpertPaperSize.NA Then
                Dim strPaperSize As String = GetTecxpertPaperSizeName(ePaperSize)
                Dim isFound As Boolean = False
                Dim i As Integer
                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                ''doctoprint.PrinterSettings.PrinterName = "Auto Xerox Phaser 3117 on SERVER"
                Dim rawKind As Integer
                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    If clsCommon.CompairString(doctoprint.PrinterSettings.PaperSizes(i).PaperName, strPaperSize) = CompairStringResult.Equal Then
                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                        isFound = True
                        Exit For
                    End If
                Next
                If Not isFound Then
                    Throw New Exception("Paper size " + strPaperSize + " not exist.Please Make it before Print.")
                End If
                rpdoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rpdoc
    End Function



    Public Shared Function exportCrystalToPDF(ByVal dt As DataTable, ByVal strReportPath As String, ByVal strSrcReportName As String, ByVal strTrgtReportName As String, ByVal strStartPath As String) As Boolean
        Try
            If dt.Rows.Count > 0 Then
                Dim rpdoc As New ReportDocument()
                Dim strReportFullPath = strReportPath & "\" & strSrcReportName & ".rpt"
                rpdoc.Load(strReportFullPath)
                rpdoc.SetDataSource(dt)
                If Not IO.Directory.Exists(strStartPath & "\pdfTemp") Then
                    IO.Directory.CreateDirectory(strStartPath & "\pdfTemp")
                End If
                rpdoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strStartPath & "\pdfTemp\" & strTrgtReportName & ".pdf")
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            Return False
        End Try

    End Function



    Public Shared Function GenerateMyQCCode(QCText As String) As Byte()

        Dim QCwriter = New BarcodeWriter()
        QCwriter.Format = BarcodeFormat.QR_CODE
        Dim result = QCwriter.Write(QCText)
        'Dim path As String = "D:/images/MyQRImage.jpg"
        Dim barcodeBitmap = New Bitmap(result)
        Dim bytes As Byte()
        Using memory As New MemoryStream()
            ' Using fs As New FileStream(path, FileMode.Create, FileAccess.ReadWrite)
            barcodeBitmap.Save(memory, ImageFormat.Jpeg)
            bytes = memory.ToArray()
            'fs.Write(bytes, 0, bytes.Length)
            'End Using
        End Using
        'PictureBox1.Load("D:/images/MyQRImage.jpg")


        ' imgageQRCode.Visible = True;
        'imgageQRCode.ImageUrl = "~/images/MyQRImage.jpg";
        'Return barcodeBitmap
        Return bytes
    End Function



    Public Shared Function ShowHistoryData(ByVal Code As String, ByVal PrimaryKeyValue As String, ByVal MasterTable As String, Optional Type As String = Nothing, Optional ByVal trans As SqlTransaction = Nothing, Optional ByVal SecondValue As String = Nothing, Optional ByVal Code2 As String = Nothing) As Boolean
        Dim dt As DataTable = Nothing
        Dim Mainqry As String = ""
        Try
            Dim qry As String = clsDBFuncationality.getSingleValue("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" & MasterTable + clsCommon.HistTablePostFix & "'")
            If clsCommon.myLen(qry) <= 0 Then
                clsCommon.MyMessageBoxShow("No History Table found")
                Return False
            End If
            Dim strMasterCodeColumn As String = ""
            Dim strMasterCodeColumnAS As String = ""
            Dim dtMasterCategory As DataTable = Nothing
            Dim FinalPrimaryKey As String = clsDBFuncationality.getSingleValue("select replace(upper(left('" + PrimaryKeyValue + "',1)) + upper(substring('" + PrimaryKeyValue + "',2,len('" + PrimaryKeyValue + "'))),'_',' ') as FinalName", trans)
            '' Sequence MasterTable column 
            Dim Masteryqry As String = ""
            Masteryqry = "  SELECT c.name as Name,replace(upper(left(c.name,1)) + upper(substring(c.name,2,len(c.name))),'_',' ') as FinalName "
            Masteryqry += " FROM " & objCommonVar.CurrDatabase & ".sys.tables t"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.all_columns c "
            Masteryqry += "  ON t.object_id = c.object_id"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.types ty "
            Masteryqry += "  ON c.system_type_id = ty.system_type_id and c.system_type_id = ty.user_type_id "
            Masteryqry += " WHERE t.name = '" & MasterTable & "'"
            Masteryqry += " order by c.name asc"
            dtMasterCategory = clsDBFuncationality.GetDataTable(Masteryqry, trans)

            If clsCommon.CompairString(MasterTable, "TSPL_CUSTOMER_MASTER") = CompairStringResult.Equal OrElse clsCommon.CompairString(MasterTable, "TSPL_VENDOR_MASTER") = CompairStringResult.Equal Then
                If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                        If ii <> 0 Then
                            strMasterCodeColumn += ","
                        End If
                        If clsCommon.CompairString(clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim(), "Status") = CompairStringResult.Equal Then
                            strMasterCodeColumn += "" + "(case when isnull(Status,'')='Y' then 'Inactive' else 'Active' end)" + " as [" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("FinalName")).Trim() + "]"
                        Else
                            strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + " as [" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("FinalName")).Trim() + "]"
                        End If
                    Next
                End If
            Else
                If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                    For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                        If ii <> 0 Then
                            strMasterCodeColumn += ","
                        End If
                        strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + " as [" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("FinalName")).Trim() + "]"
                    Next
                End If
            End If

            '' End
            '' =========Final Binding Main Qry=======
            Mainqry = "  select ROW_NUMBER() OVER(ORDER BY Hist_Version asc) AS Version,final.* from "
            Mainqry += " ( "
            Mainqry += " select " & clsCommon.HistTableColHistVersion & "," & clsCommon.HistTableColHistBy & "," & clsCommon.HistTableColHistOn & "," & strMasterCodeColumn & " from " & MasterTable + clsCommon.HistTablePostFix & ""
            Mainqry += " union all "
            Mainqry += " select (select count(Hist_Version)+1 as Hist_Version from  " & MasterTable + clsCommon.HistTablePostFix & "  where " & PrimaryKeyValue & "='" & Code & "' group by " & PrimaryKeyValue & " ) as Version,'Current' as [User By],convert(datetime,GETDATE(),103) as " & clsCommon.HistTableColHistOn & "," & strMasterCodeColumn & " from " & MasterTable & ""
            Mainqry += " )final "
            Mainqry += " where 2=2 and final.[" & FinalPrimaryKey & "]='" & Code & "'"
            'Mainqry += " where 2=2 and final." & PrimaryKeyValue & "='" & Code & "'"
            If clsCommon.myLen(SecondValue) > 0 Then
                Mainqry += " and final.[" & SecondValue & "]='" & Code2 & "'"
            End If
            ''==========End=========
            dt = clsDBFuncationality.GetDataTable(Mainqry)
            Dim frmHistory As New frmMasterHistory()
            frmHistory.Text = "History for (" & Code & ")"
            frmHistory.dt = dt
            frmHistory.ReportID = clsCommon.myCstr(MasterTable)
            frmHistory.ShowDialog()


        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function

    Public Shared Function ShowTransHistoryData(ByVal Code As String, ByVal PrimaryKeyValue As String, ByVal HeadTable As String, ByVal DetailTable As String, Optional ByVal trans As SqlTransaction = Nothing) As Boolean
        Dim dt As DataTable = Nothing
        Dim Mainqry As String = ""
        Try
            Dim qry As String = clsDBFuncationality.getSingleValue("select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME='" & HeadTable + clsCommon.HistTablePostFix & "'")
            If clsCommon.myLen(qry) <= 0 Then
                clsCommon.MyMessageBoxShow("No History Table found")
                Return False
            End If
            Dim strMasterCodeColumn As String = ""
            Dim dtMasterCategory As DataTable = Nothing

            '' Sequence MasterTable column 
            Dim Masteryqry As String = ""
            Masteryqry = "  SELECT  c.name + ' as [' +  REPLACE( c.name ,'_',' ' ) +']' as Name " ' REPLACE( c.name ,'_',' ' ) as Name
            Masteryqry += " FROM " & objCommonVar.CurrDatabase & ".sys.tables t"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.all_columns c "
            Masteryqry += "  ON t.object_id = c.object_id"
            Masteryqry += " INNER JOIN " & objCommonVar.CurrDatabase & ".sys.types ty "
            Masteryqry += "  ON c.system_type_id = ty.system_type_id and c.system_type_id = ty.user_type_id"
            Masteryqry += " WHERE t.name = '" & HeadTable & "'"
            Masteryqry += " order by c.name asc"
            dtMasterCategory = clsDBFuncationality.GetDataTable(Masteryqry, trans)

            Dim PrimaryKeyValueWithoutUnderScore As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  '['+ REPLACE( '" + PrimaryKeyValue + "' ,'_',' ' )+']' "))

            If dtMasterCategory IsNot Nothing AndAlso dtMasterCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtMasterCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strMasterCodeColumn += ","
                    End If
                    strMasterCodeColumn += "" + clsCommon.myCstr(dtMasterCategory.Rows(ii)("Name")).Trim() + ""
                Next
            End If
            '' End
            '' =========Final Binding Main Qry=======
            Mainqry = "  select " & clsCommon.HistTableColHistVersion & " AS Version,final.* from "
            Mainqry += " ( "
            Mainqry += " select " & clsCommon.HistTableColHistVersion & "," & clsCommon.HistTableColHistBy & "," & clsCommon.HistTableColHistOn & "," & strMasterCodeColumn & " from " & HeadTable + clsCommon.HistTablePostFix & ""
            'Mainqry += " union all "
            'Mainqry += " select '99999' as Version,'Current' as [User By],'' as " & clsCommon.HistTableColHistOn & "," & strMasterCodeColumn & " from " & HeadTable & ""
            Mainqry += " )final "
            Mainqry += " where 2=2 and final." & PrimaryKeyValueWithoutUnderScore & "='" & Code & "'   order by " & clsCommon.HistTableColHistVersion & "  asc "
            ''==========End=========
            dt = clsDBFuncationality.GetDataTable(Mainqry)
            Dim frmHistory As New frmTransactionHistory()
            frmHistory.Text = "History for (" & Code & ")"
            frmHistory.dt = dt
            frmHistory.PrimaryKeyValue = PrimaryKeyValue
            frmHistory.code = Code
            frmHistory.DetailTable = DetailTable
            frmHistory.HeadTable = HeadTable
            frmHistory.Show()

        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

        Return True
    End Function


End Class

Public Class clsFatSnfRateCalculator
    Public fatR As Decimal = 0
    Public snfR As Decimal = 0
    Public FatAmt As Decimal = 0
    Public snfAmt As Decimal = 0

    Public Shared Function CalculateIn(Qty As Double, StdFatPer As Double, StdSnfPer As Double, FatPer As Double, SnfPer As Double, StdRate As Double, MilkRate As Double) As clsFatSnfRateCalculator

        Dim rValue As clsFatSnfRateCalculator = New clsFatSnfRateCalculator
        Try
            Dim Row As Integer = 1
            Dim Col As Integer = 2
            Dim Matrix(Row, Col) As Double
            Matrix(0, 0) = (Qty * FatPer / 100)
            Matrix(0, 1) = (Qty * SnfPer / 100)
            Matrix(0, 2) = MilkRate * Qty
            Matrix(1, 0) = (Qty * StdFatPer / 100)
            Matrix(1, 1) = (Qty * StdSnfPer / 100)
            Matrix(1, 2) = StdRate * Qty
            Dim ans() As Double = SolveEquations.SolveLinearEquation(Matrix)
            rValue.FatAmt = ans(0) * (Qty * FatPer / 100)
            rValue.snfAmt = ans(1) * (Qty * SnfPer / 100)
            rValue.fatR = ans(0)
            rValue.snfR = ans(1)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function

    Public Shared Function CalculateInonSamePercentage(Qty As Double, StdFatPer As Double, StdSnfPer As Double, FatRatio As Double, SnfRatio As Double, StdRate As Double) As clsFatSnfRateCalculator
        Dim rValue As clsFatSnfRateCalculator = New clsFatSnfRateCalculator
        Try
            rValue.fatR = (FatRatio * StdRate) / (StdFatPer * 100)
            rValue.snfR = (SnfRatio * StdRate) / (StdSnfPer * 100)
            rValue.FatAmt = ((FatRatio * StdRate) / (StdFatPer * 100)) * (StdFatPer * Qty)
            rValue.snfAmt = ((SnfRatio * StdRate) / (StdSnfPer * 100)) * (StdSnfPer * Qty)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return rValue
    End Function


    Public Shared Function CalculateStdFATSNFRate(QtyKG As Double, StdFatPer As Double, StdSNFPer As Double, StdFatWeightage As Double, StdSNFWeightage As Double, StdRate As Double, FatPer As Double, SNFPer As Double) As clsFatSnfRateCalculator
        Dim objReturn As clsFatSnfRateCalculator = New clsFatSnfRateCalculator
        Try
            objReturn.fatR = Math.Round(IIf(StdFatPer = 0, 0, StdRate * StdFatWeightage / StdFatPer), 3, MidpointRounding.AwayFromZero)
            objReturn.snfR = Math.Round(IIf(StdSNFPer = 0, 0, StdRate * StdSNFWeightage / StdSNFPer), 3, MidpointRounding.AwayFromZero)
            objReturn.FatAmt = Math.Round(objReturn.fatR * (QtyKG * FatPer / 100), 2, MidpointRounding.ToEven)
            objReturn.snfAmt = Math.Round(objReturn.snfR * (QtyKG * SNFPer / 100), 2, MidpointRounding.ToEven)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return objReturn
    End Function

    ''BHA/14/11/18-000682 by balwinder on 15/11/2018
    Public Shared Function CalculateFATSNFRatefromTransactionPer(ByVal QtyKG As Decimal, ByVal TransAmt As Decimal, ByVal TransFatPer As Decimal, ByVal TransSNFPer As Decimal, ByVal FatWeightage As Decimal, ByVal SNFWeightage As Decimal) As clsFatSnfRateCalculator
        Dim objReturn As clsFatSnfRateCalculator = New clsFatSnfRateCalculator
        Try
            If QtyKG <> 0 Then
                TransAmt = Math.Round(TransAmt, 2, MidpointRounding.ToEven)
                Dim Rate As Decimal = TransAmt / QtyKG
                objReturn.fatR = clsCommon.myCDivide((Rate * FatWeightage), TransFatPer)
                objReturn.snfR = clsCommon.myCDivide((Rate * SNFWeightage), TransSNFPer)
                objReturn.FatAmt = objReturn.fatR * (QtyKG * TransFatPer / 100)
                objReturn.snfAmt = objReturn.snfR * (QtyKG * TransSNFPer / 100)

                objReturn.fatR = Math.Round(objReturn.fatR, 3, MidpointRounding.AwayFromZero)
                objReturn.snfR = Math.Round(objReturn.snfR, 3, MidpointRounding.AwayFromZero)
                objReturn.FatAmt = Math.Round(objReturn.FatAmt, 2, MidpointRounding.ToEven)
                objReturn.snfAmt = Math.Round(objReturn.snfAmt, 2, MidpointRounding.ToEven)

                If Math.Abs(TransAmt - (objReturn.FatAmt + objReturn.snfAmt)) < 0.1 Then
                    objReturn.snfAmt = TransAmt - objReturn.FatAmt
                End If

            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return objReturn
    End Function


End Class