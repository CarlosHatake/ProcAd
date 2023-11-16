Public Class PeriodoEval
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        'Datos del Periodo Actual y Nuevo
                        Dim sdaPeriodo As New SqlDataAdapter
                        Dim dsPeriodo As New DataSet
                        sdaPeriodo.SelectCommand = New SqlCommand("select case datepart(month,cast(valor as date)) " +
                                                                  "         when 1 then 'Enero' " +
                                                                  "         when 2 then 'Febrero' " +
                                                                  "         when 3 then 'Marzo' " +
                                                                  "         when 4 then 'Abril' " +
                                                                  "         when 5 then 'Mayo' " +
                                                                  "         when 6 then 'Junio' " +
                                                                  "         when 7 then 'Julio' " +
                                                                  "         when 8 then 'Agosto' " +
                                                                  "         when 9 then 'Septiembre' " +
                                                                  "         when 10 then 'Octubre' " +
                                                                  "         when 11 then 'Noviembre' " +
                                                                  "         when 12 then 'Diciembre' " +
                                                                  "         else '-' " +
                                                                  "       end + ' ' + cast(datepart(year,cast(valor as date)) as varchar(4)) as periodo_actual " +
                                                                  "     , case datepart(month,dateadd(mm, 1, cast(valor as date))) " +
                                                                  "         when 1 then 'Enero' " +
                                                                  "         when 2 then 'Febrero' " +
                                                                  "         when 3 then 'Marzo' " +
                                                                  "         when 4 then 'Abril' " +
                                                                  "         when 5 then 'Mayo' " +
                                                                  "         when 6 then 'Junio' " +
                                                                  "         when 7 then 'Julio' " +
                                                                  "         when 8 then 'Agosto' " +
                                                                  "         when 9 then 'Septiembre' " +
                                                                  "         when 10 then 'Octubre' " +
                                                                  "         when 11 then 'Noviembre' " +
                                                                  "         when 12 then 'Diciembre' " +
                                                                  "         else '-' " +
                                                                  "       end + ' ' + cast(datepart(year,dateadd(mm, 1, cast(valor as date))) as varchar(4)) as periodo_nuevo " +
                                                                  "     , dateadd(mm, 1, cast(valor as date)) as periodo_nuevo_date " +
                                                                  "from cg_parametros " +
                                                                  "where parametro = 'mes_eval' ", ConexionBD)
                        sdaPeriodo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        sdaPeriodo.Fill(dsPeriodo)
                        ConexionBD.Close()
                        .lblPeriodoAct.Text = dsPeriodo.Tables(0).Rows(0).Item("periodo_actual").ToString()
                        .lblPeriodoNuevo.Text = dsPeriodo.Tables(0).Rows(0).Item("periodo_nuevo").ToString()
                        .lblPeriodoNuevoD.Text = dsPeriodo.Tables(0).Rows(0).Item("periodo_nuevo_date").ToString()
                        sdaPeriodo.Dispose()
                        dsPeriodo.Dispose()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

#End Region

#Region "Abrir Periodo"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update cg_parametros " +
                                         "  set valor = @valor " +
                                         "where parametro = 'mes_eval' "
                SCMValores.Parameters.AddWithValue("@valor", CDate(.lblPeriodoNuevoD.Text))
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                .litError.Text = "Periodo Abierto"
                .btnAceptar.Enabled = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class