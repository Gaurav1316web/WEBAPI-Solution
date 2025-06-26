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
