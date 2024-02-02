Imports System.Security.Cryptography

Public Class clsAccess

    Public Function conBD(ByVal sistema)
        Dim servidor As String
        Dim usr As String
        Dim pass As String

        ' Local
        'servidor = "172.16.18.239"
        'usr = "sa"
        'pass = "12345#b"

        Select Case sistema
            Case "ProcAd"
                Return "Data Source=" + servidor + ";Initial Catalog=bd_ProcAd;Persist Security Info=True;User ID=" + usr + ";Password=" + pass
            Case "SiCEm"
                Return "Data Source=" + servidor + ";Initial Catalog=bd_Empleado;Persist Security Info=True;User ID=" + usr + ";Password=" + pass
            Case "SiTTi"
                Return "Data Source=" + servidor + ";Initial Catalog=bd_SiTTi;Persist Security Info=True;User ID=" + usr + ";Password=" + pass
            Case "SiLi"
                Return "Data Source=" + servidor + ";Initial Catalog=bd_SiLi;Persist Security Info=True;User ID=" + usr + ";Password=" + pass
            Case "NAV"
                Return "Data Source=40.76.105.1,5055;Initial Catalog=UnneProd;Persist Security Info=True;User ID=INT_SIS_Report;Password=2XAd91R?Fb71"
            Case "NOM"
                Return "Data Source=40.76.105.1,5055;Initial Catalog=NOM2001;Persist Security Info=True;User ID=INT_SIS_Report;Password=2XAd91R?Fb71"
            Case Else
                Return ""
        End Select
    End Function

    Public Function Encrypt(ByVal encryptText)

        ' Descomponer string en bytes
        Dim stringContent As Byte() = Encoding.UTF8.GetBytes(encryptText)

        ' Escriptamos el texto del string
        Dim stringEncrypt As Byte() = MachineKey.Protect(stringContent)

        Return HttpServerUtility.UrlTokenEncode(stringEncrypt)

    End Function

    Public Function Decrypt(ByVal decryptText)

        ' Descomponemos string en Bytes
        Dim stringContent As Byte() = HttpServerUtility.UrlTokenDecode(decryptText)

        ' Desencriptamos el texto del string
        Dim stringDecrypt As Byte() = MachineKey.Unprotect(stringContent)
        Dim pass = Encoding.UTF8.GetString(stringDecrypt)

        Return Encoding.UTF8.GetString(stringDecrypt)


    End Function

    Public Function EncryptSHA1(ByVal texto)

        Dim c_ As String = texto

        Dim sha1 As SHA1 = SHA1CryptoServiceProvider.Create()

        Dim textOriginal As Byte() = ASCIIEncoding.Default.GetBytes(c_)
        Dim hash As Byte() = sha1.ComputeHash(textOriginal)

        Dim cadena As StringBuilder = New StringBuilder()
        For Each i As Byte In hash
            cadena.AppendFormat("{0:x2}", i)
        Next
        Dim pass = cadena.ToString()
        Return cadena.ToString()

    End Function

    Public Function RandomString()
        Dim rdn As Random = New Random()
        Dim caracteres As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@"
        Dim longitud As Integer = caracteres.Length
        Dim letra As Char
        Dim longitudContrasenia As Integer = 12
        Dim cadenaAleatoria As String = String.Empty
        For i = 0 To longitudContrasenia
            letra = caracteres(rdn.Next(0, longitud))
            cadenaAleatoria += letra.ToString()
        Next
        Dim guid As String = System.Guid.NewGuid.ToString()
        Dim seed As String = guid.ToString().Substring(0, 3)
        cadenaAleatoria += seed
        Return cadenaAleatoria
    End Function

    Public Function verificarPermisos(ByVal usuario, ByVal perfil, ByVal pagina)

        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
        ConexionBD.ConnectionString = conBD("ProcAd")

        Dim SCMValoresPer As SqlCommand = New System.Data.SqlClient.SqlCommand
        SCMValoresPer.Connection = ConexionBD
        Dim cont As Integer


        SCMValoresPer.CommandText = ""
        SCMValoresPer.Parameters.Clear()
        SCMValoresPer.CommandText = "exec dbo.SP_C_dt_perfil @perfil, @pagina, @idUsuario "
        SCMValoresPer.Parameters.AddWithValue("@perfil", perfil)
        SCMValoresPer.Parameters.AddWithValue("@pagina", pagina)
        SCMValoresPer.Parameters.AddWithValue("@idUsuario", usuario)
        ConexionBD.Open()
        cont = Val(SCMValoresPer.ExecuteScalar)
        ConexionBD.Close()


        Return cont
    End Function

End Class
