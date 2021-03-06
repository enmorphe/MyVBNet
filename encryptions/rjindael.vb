﻿Imports System.Windows.Forms
Imports System
Imports System.IO
Imports System.Security.Cryptography

Namespace Encryption
    Public Class rjindael
        Public Shared Sub sample()
            Try
                Dim original As String = "Here is some data to encrypt!"
                ' Create a new instance of the Rijndael
                ' class.  This generates a new key and initialization 
                ' vector (IV).
                Using myRijndael = Rijndael.Create()

                    ' Encrypt the string to an array of bytes.
                    Dim encrypted As Byte() = EncryptStringToBytes(original)

                    ' Decrypt the bytes to a string.
                    Dim roundtrip As String = DecryptStringFromBytes(encrypted)

                    'Display the original data and the decrypted data.
                    Console.WriteLine("Original:   {0}", original)
                    Console.WriteLine("Round Trip: {0}", roundtrip)
                End Using
            Catch e As Exception
                Console.WriteLine("Error: {0}", e.Message)
            End Try
        End Sub 'Main
        Shared Function EncryptStringToBytes(ByVal plainText As String) As Byte()
            ' Check arguments.
            If plainText Is Nothing OrElse plainText.Length <= 0 Then
                Throw New ArgumentNullException("plainText")
            End If
            Dim encrypted() As Byte
            ' Create an Rijndael object
            ' with the specified key and IV.
            Using rijAlg = Rijndael.Create()

                rijAlg.Key = getKey()
                rijAlg.IV = getIV()

                ' Create a decrytor to perform the stream transform.
                Dim encryptor As ICryptoTransform = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV)
                ' Create the streams used for encryption.
                Using msEncrypt As New MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)

                            'Write all data to the stream.
                            swEncrypt.Write(plainText)
                        End Using
                        encrypted = msEncrypt.ToArray()
                    End Using
                End Using
            End Using

            ' Return the encrypted bytes from the memory stream.
            Return encrypted

        End Function 'EncryptStringToBytes
        Shared Function DecryptStringFromBytes(ByVal cipherText() As Byte) As String
            ' Check arguments.
            If cipherText Is Nothing OrElse cipherText.Length <= 0 Then
                Throw New ArgumentNullException("cipherText")
            End If
            ' Declare the string used to hold
            ' the decrypted text.
            Dim plaintext As String = Nothing

            ' Create an Rijndael object
            ' with the specified key and IV.
            Using rijAlg = Rijndael.Create()
                rijAlg.Key = getKey()
                rijAlg.IV = getIV()

                ' Create a decrytor to perform the stream transform.
                Dim decryptor As ICryptoTransform = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV)

                ' Create the streams used for decryption.
                Using msDecrypt As New MemoryStream(cipherText)

                    Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                        Using srDecrypt As New StreamReader(csDecrypt)


                            ' Read the decrypted bytes from the decrypting stream
                            ' and place them in a string.
                            plaintext = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using

            Return plaintext

        End Function 'DecryptStringFromBytes 
        Private Shared Function getKey() As Byte()
            Dim bytKey As Byte()
            Dim bytSalt As Byte() = System.Text.Encoding.ASCII.GetBytes("salt")
            'use the hash code from each computer so that it cannot be decipher if
            'data is moved from another computer
            Dim pdb As New PasswordDeriveBytes("GodIsGood", bytSalt)
            bytKey = pdb.GetBytes(32)
            Return bytKey
        End Function
        Private Shared Function getIV() As Byte()
            Dim bytKey As Byte()
            Dim bytSalt As Byte() = System.Text.Encoding.ASCII.GetBytes("salt")
            'use the hash code from each computer so that it cannot be decipher if
            'data is moved from another computer
            Dim pdb As New PasswordDeriveBytes("TheLordIsMyShepherd", bytSalt)
            bytKey = pdb.GetBytes(16)
            Return bytKey
        End Function
    End Class
End Namespace
