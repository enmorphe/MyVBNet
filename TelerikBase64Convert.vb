Namespace Encryption
    Public Class TelerikBase64Convert
        Private Shared Function BaseConvert(ByVal number As String, ByVal fromBase As Integer, ByVal toBase As Integer) As String
            Dim digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            Dim length = number.Length
            Dim result = String.Empty

            Dim nibbles = number.[Select](Function(c) digits.IndexOf(c)).ToList()
            Dim newlen As Integer
            Do
                Dim value = 0
                newlen = 0

                For i As Double = 0 To length - 1
                    value = value * fromBase + nibbles(i)
                    If value >= toBase Then
                        If newlen = nibbles.Count Then
                            nibbles.Add(0)
                        End If
                        nibbles(System.Math.Max(System.Threading.Interlocked.Increment(newlen), newlen - 1)) = value / toBase
                        value = value Mod toBase
                    ElseIf newlen > 0 Then
                        If newlen = nibbles.Count Then
                            nibbles.Add(0)
                        End If
                        nibbles(System.Math.Max(System.Threading.Interlocked.Increment(newlen), newlen - 1)) = 0
                    End If
                Next
                length = newlen
                '
                result = digits(value) + result
            Loop While newlen <> 0

            Return result
        End Function
        '=======================================================
        'Service provided by Telerik (www.telerik.com)
        'Conversion powered by NRefactory.
        'Twitter: @telerik
        'Facebook: facebook.com/telerik
        '=======================================================
        Public Shared Function HashValueAndClean(ByVal value As String) As String
            If value.Trim = "" Then Return "" 'do not allow empty string
            value = value.ToUpper
            Dim hashed As String
            Dim WantedChars As String = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789" 'i did not include 0 and the letter O to prevent confusion :)
            hashed = BaseConvert(value, 16, 36) 'use this one because it always return the same code
            hashed = hashed.ToUpper() 'capitalize

            Dim clean_string As String = "", x As Integer
            Dim examine_str As String
            'may include unwanted characters, remove it
            For x = 1 To hashed.Length
                examine_str = Mid(hashed, x, 1)
                If Strings.InStr(WantedChars, examine_str, CompareMethod.Text) Then
                    'try its in there
                    clean_string = clean_string & examine_str
                End If
            Next

            Return clean_string

        End Function
    End Class
End Namespace


