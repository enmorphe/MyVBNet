Namespace Encryption
    Public Class MyEncryptions
#Region "1"
        ''' <summary>
        ''' A simple obscuring of a string
        ''' Primarily used in my first serial generator
        ''' </summary>
        ''' <param name="strToEncode"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Encoder1(ByVal strToEncode As String)
            strToEncode = strToEncode.ToUpper 'always upper case
            Dim sNumberEquivalence As New Dictionary(Of String, String)
            Dim sLetterEquivalence As New Dictionary(Of String, String)
            With sNumberEquivalence
                .Add("0", "Q") '0
                .Add("1", "R") '1
                .Add("2", "S") '2
                .Add("3", "T") '3
                .Add("4", "U") '4
                .Add("5", "V") '5
                .Add("6", "W") '6
                .Add("7", "X") '7
                .Add("8", "Y") '8
                .Add("9", "Z") '9
            End With
            With sLetterEquivalence
                .Add("Q", "9") 'Q
                .Add("R", "8") 'R
                .Add("S", "7") 'S
                .Add("T", "6") 'T
                .Add("U", "5") 'U
                .Add("V", "4") 'V
                .Add("W", "3") 'W
                .Add("X", "2") 'X
                .Add("Y", "1") 'Y
                .Add("Z", "0") 'Z
                'Normal Letters
                .Add("A", "H") 'A
                .Add("B", "I") 'B
                .Add("C", "J") 'C
                .Add("D", "K") 'D
                .Add("E", "A") 'E
                .Add("F", "B") 'F
                .Add("G", "C") 'G
                .Add("H", "D") 'H
                .Add("I", "L") 'I
                .Add("J", "M") 'J
                .Add("K", "N") 'K
                .Add("L", "P") 'L
                .Add("M", "E") 'M
                .Add("N", "F") 'N
                .Add("O", "G") 'O
                .Add("P", "O") 'P
            End With
            Dim x As New System.Collections.Generic.List(Of Char)
            x = strToEncode.ToList()
            Dim encodedSerial As String = ""
            For Each a As Char In x
                If a = "-" Then
                    encodedSerial = encodedSerial & a
                Else
                    'process
                    If IsNumeric(a) Then
                        'based on the equivalent of numbers
                        encodedSerial = encodedSerial & sNumberEquivalence(a)
                    Else
                        'the dictionary may not contain the string
                        'check first
                        If sLetterEquivalence.ContainsKey(a) Then
                            encodedSerial = encodedSerial & sLetterEquivalence(a)
                        Else
                            'discard that
                        End If
                    End If
                End If
            Next
            Return encodedSerial
        End Function
        Public Shared Function Decoder1(ByVal encodedSerial As String) As String
            'REVERSE, the keys and values
            encodedSerial = encodedSerial.ToUpper 'to be safe
            Dim sNumberEquivalence As New Dictionary(Of String, String)
            Dim sLetterEquivalence As New Dictionary(Of String, String)
            With sNumberEquivalence
                .Add("Q", "0") '0
                .Add("R", "1") '1
                .Add("S", "2") '2
                .Add("T", "3") '3
                .Add("U", "4") '4
                .Add("V", "5") '5
                .Add("W", "6") '6
                .Add("X", "7") '7
                .Add("Y", "8") '8
                .Add("Z", "9") '9
            End With
            With sLetterEquivalence
                .Add("9", "Q") 'Q
                .Add("8", "R") 'R
                .Add("7", "S") 'S
                .Add("6", "T") 'T
                .Add("5", "U") 'U
                .Add("4", "V") 'V
                .Add("3", "W") 'W
                .Add("2", "X") 'X
                .Add("1", "Y") 'Y
                .Add("0", "Z") 'Z
                'Normal Letters
                .Add("H", "A") 'A
                .Add("I", "B") 'B
                .Add("J", "C") 'C
                .Add("K", "D") 'D
                .Add("A", "E") 'E
                .Add("B", "F") 'F
                .Add("C", "G") 'G
                .Add("D", "H") 'H
                .Add("L", "I") 'I
                .Add("M", "J") 'J
                .Add("N", "K") 'K
                .Add("P", "L") 'L
                .Add("E", "M") 'M
                .Add("F", "N") 'N
                .Add("G", "O") 'O
                .Add("O", "P") 'P
            End With
            Dim x As New System.Collections.Generic.List(Of Char)
            x = encodedSerial.ToList()
            Dim decodedSerial As String = ""
            Dim numericEquivString As String = "QRSTUVWXYZ"
            For Each a As Char In x
                If a = "-" Then
                    decodedSerial = decodedSerial & a
                Else
                    'process
                    If InStr(numericEquivString, a.ToString, CompareMethod.Binary) Then
                        'based on the equivalent of numbers
                        decodedSerial = decodedSerial & sNumberEquivalence(a)
                    Else
                        If sLetterEquivalence.ContainsKey(a) Then
                            decodedSerial = decodedSerial & sLetterEquivalence(a)
                        End If
                    End If
                End If
            Next
            Return decodedSerial
        End Function
#End Region
#Region "Random Strings"
        Public Shared Function GenerateRandomString(ByVal strLength As Integer) As String
            Dim xCharArray() As Char = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray
            Dim xNoArray() As Char = "0123456789".ToCharArray
            Dim xGenerator As System.Random = New System.Random()
            Dim xStr As String = String.Empty
            While xStr.Length < strLength
                If xGenerator.Next(0, 2) = 0 Then
                    xStr &= xCharArray(xGenerator.Next(0, xCharArray.Length))
                Else
                    xStr &= xNoArray(xGenerator.Next(0, xNoArray.Length))
                End If
            End While
            Return xStr
        End Function
#End Region
        
    End Class
End Namespace
