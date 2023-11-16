Public Class RegCumplUN
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 1

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaMesEval As New SqlDataAdapter
                        Dim dsMesEval As New DataSet
                        .ddlMesEvalB.DataSource = dsMesEval
                        'Catálogo de Meses de Evaluación disponibles
                        sdaMesEval.SelectCommand = New SqlCommand("select distinct(cast(año_eval as varchar(4)) + '/' + cast(format(mes_eval, '00') as varchar(2)) + '/01') as periodo_date " +
                                                                  "     , case mes_eval " +
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
                                                                  "       end + ' ' + cast(año_eval as varchar(4)) as periodo_eval " +
                                                                  "from ms_cumpl_UN " +
                                                                  "where status = 'A' " +
                                                                  "union " +
                                                                  "select cast(datepart(year,cast(valor as date)) as varchar(4)) + '/' + cast(format(datepart(month,cast(valor as date)), '00') as varchar(2)) + '/01' as periodo_date " +
                                                                  "     , case datepart(month,cast(valor as date)) " +
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
                                                                  "       end + ' ' + cast(datepart(year,cast(valor as date)) as varchar(4)) as periodo_eval " +
                                                                  "from cg_parametros " +
                                                                  "where parametro = 'mes_eval' " +
                                                                  "order by periodo_date ", ConexionBD)
                        .ddlMesEvalB.DataTextField = "periodo_eval"
                        .ddlMesEvalB.DataValueField = "periodo_date"
                        ConexionBD.Open()
                        sdaMesEval.Fill(dsMesEval)
                        .ddlMesEvalB.DataBind()
                        ConexionBD.Close()
                        sdaMesEval.Dispose()
                        dsMesEval.Dispose()
                        .ddlMesEvalB.SelectedIndex = -1

                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select cast(valor as date) " +
                                                 "from cg_parametros " +
                                                 "where parametro = 'mes_eval' "
                        ConexionBD.Open()
                        .lblMesEvalD.Text = SCMValores.ExecuteScalar
                        ConexionBD.Close()

                        .ddlMesEvalB.SelectedValue = CDate(.lblMesEvalD.Text).ToString("yyyy/MM/dd")

                        limpiarPantalla()
                    Else
                        Server.Transfer("Login.aspx")
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub ddlMesEvalB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMesEvalB.SelectedIndexChanged
        limpiarPantalla()
    End Sub

#End Region

#Region "Funciones"

    Public Sub limpiarPantalla()
        With Me
            llenarGrid()
            .pnlGrid.Enabled = True
            .ibtnBaja.Enabled = False
            .ibtnBaja.ImageUrl = "images\Trash_i2.png"
            .ibtnModif.Enabled = False
            .ibtnModif.ImageUrl = "images\Edit_i2.png"
            .pnlGrid.Visible = True
            .pnlDatos.Visible = False
        End With
    End Sub

    Public Sub habilitarCampos(ByVal valor As Boolean)
        With Me
            .wpePorcentCumpl.Enabled = valor
        End With
    End Sub

    Public Sub bloqueoPantalla()
        With Me
            .pnlGrid.Visible = False
            .pnlDatos.Visible = True
        End With
    End Sub

    Public Sub llenarGrid()
        With Me
            Try
                .litError.Text = ""
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaPorcentCumplUN As New SqlDataAdapter
                Dim dsPorcentCumplUN As New DataSet
                .gvCumplUN.Columns(0).Visible = True
                .gvCumplUN.Columns(1).Visible = True
                .gvCumplUN.DataSource = dsPorcentCumplUN
                'Porcentajes de Cumplimientos de las Unidades de Negocio
                sdaPorcentCumplUN.SelectCommand = New SqlCommand("select isnull(id_ms_cumpl_UN, 0) as id_ms_cumpl_UN " +
                                                                 "     , cg_unidad_neg.id_unidad_neg " +
                                                                 "     , cg_unidad_neg.unidad_neg " +
                                                                 "     , porcent_cumpl " +
                                                                 "     , porcent_bono " +
                                                                 "from cg_unidad_neg " +
                                                                 "  left join ms_cumpl_UN on cg_unidad_neg.id_unidad_neg = ms_cumpl_UN.id_unidad_neg and ms_cumpl_UN.status = 'A' and año_eval = datepart(year,@mes_eval) and mes_eval = datepart(month,@mes_eval) " +
                                                                 "where cg_unidad_neg.status = 'A' " +
                                                                 "  and cg_unidad_neg.calculado = 'N' " +
                                                                 "order by cg_unidad_neg.indice ", ConexionBD)
                sdaPorcentCumplUN.SelectCommand.Parameters.AddWithValue("@mes_eval", CDate(.ddlMesEvalB.SelectedValue))
                ConexionBD.Open()
                sdaPorcentCumplUN.Fill(dsPorcentCumplUN)
                .gvCumplUN.DataBind()
                ConexionBD.Close()
                sdaPorcentCumplUN.Dispose()
                dsPorcentCumplUN.Dispose()
                .gvCumplUN.Columns(0).Visible = False
                .gvCumplUN.Columns(1).Visible = False
                .gvCumplUN.SelectedIndex = -1
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub localizar(ByVal idMsCumplUN, ByVal idCgUN)
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                If idMsCumplUN = 0 Then
                    'No existe registro
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select unidad_neg " +
                                             "from cg_unidad_neg " +
                                             "where id_unidad_neg = @id_unidad_neg "
                    SCMValores.Parameters.AddWithValue("@id_unidad_neg", idCgUN)
                    ConexionBD.Open()
                    .lblUnidadNeg.Text = SCMValores.ExecuteScalar
                    ConexionBD.Close()
                    .lblUnidadNegID.Text = idCgUN
                    .wpePorcentCumpl.Text = ""
                Else
                    'Ya se definió un valor previamente
                    Dim sdaCatalogo As New SqlDataAdapter
                    Dim dsCatalogo As New DataSet
                    sdaCatalogo.SelectCommand = New SqlCommand("select id_unidad_neg " +
                                                               "     , unidad_neg " +
                                                               "     , porcent_cumpl " +
                                                               "from ms_cumpl_UN " +
                                                               "where id_ms_cumpl_UN = @id_ms_cumpl_UN ", ConexionBD)
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_ms_cumpl_UN", idMsCumplUN)
                    ConexionBD.Open()
                    sdaCatalogo.Fill(dsCatalogo)
                    ConexionBD.Close()
                    .lblUnidadNeg.Text = dsCatalogo.Tables(0).Rows(0).Item("unidad_neg").ToString()
                    .lblUnidadNegID.Text = Val(dsCatalogo.Tables(0).Rows(0).Item("id_unidad_neg").ToString())
                    .wpePorcentCumpl.Value = Val(dsCatalogo.Tables(0).Rows(0).Item("porcent_cumpl").ToString())
                    sdaCatalogo.Dispose()
                    dsCatalogo.Dispose()
                End If
                .lblMesEval.Text = .ddlMesEvalB.SelectedItem.Text

                bloqueoPantalla()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Selección"

    Protected Sub gvCumplUN_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvCumplUN.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                If Val(.gvCumplUN.SelectedRow.Cells(0).Text) = 0 Or CDate(.ddlMesEvalB.SelectedValue) <> CDate(.lblMesEvalD.Text) Then
                    'No existe Registro Activo
                    .ibtnBaja.Enabled = False
                    .ibtnBaja.ImageUrl = "images\Trash_i2.png"
                Else
                    'Ya existe un Registro
                    .ibtnBaja.Enabled = True
                    .ibtnBaja.ImageUrl = "images\Trash.png"
                End If
                If CDate(.ddlMesEvalB.SelectedValue) <> CDate(.lblMesEvalD.Text) Then
                    .ibtnModif.Enabled = False
                    .ibtnModif.ImageUrl = "images\Edit_i2.png"
                Else
                    .ibtnModif.Enabled = True
                    .ibtnModif.ImageUrl = "images\Edit.png"
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Inicio"

    Protected Sub ibtnBaja_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnBaja.Click
        With Me
            Try
                ._txtTipoMov.Text = "B"
                .lblTipoMov.Text = "Baja"
                localizar(.gvCumplUN.SelectedRow.Cells(0).Text, .gvCumplUN.SelectedRow.Cells(1).Text)
                habilitarCampos(False)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub ibtnModif_Click(sender As Object, e As ImageClickEventArgs) Handles ibtnModif.Click
        With Me
            Try
                ._txtTipoMov.Text = "M"
                .lblTipoMov.Text = "Modificación"
                localizar(.gvCumplUN.SelectedRow.Cells(0).Text, .gvCumplUN.SelectedRow.Cells(1).Text)
                habilitarCampos(True)
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Botones Datos"

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                .litError.Text = ""
                Dim ban As Integer = 0
                If .wpePorcentCumpl.Text.Trim = "" Then
                    .litError.Text = "Favor de Ingresar el Porcentaje de Cumplimiento"
                Else
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    Dim SCMValoresTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValoresTemp.Connection = ConexionBD
                    SCMValoresTemp.CommandText = ""
                    SCMValoresTemp.Parameters.Clear()
                    'Tipo de Ajuste (alta, baja o modificación)
                    If ._txtTipoMov.Text = "B" Then
                        'Baja
                        SCMValores.CommandText = "UPDATE ms_cumpl_UN SET status = 'B' WHERE id_ms_cumpl_UN = @id_ms_cumpl_UN"
                        SCMValores.Parameters.AddWithValue("@id_ms_cumpl_UN", .gvCumplUN.SelectedRow.Cells(0).Text)
                    Else
                        'Alta / Modificación
                        If Val(.gvCumplUN.SelectedRow.Cells(0).Text) = 0 Then
                            'Alta
                            SCMValores.CommandText = "INSERT INTO ms_cumpl_UN (año_eval,  mes_eval,  id_unidad_neg,  unidad_neg,  porcent_cumpl,  porcent_bono,  status) values(@año_eval, @mes_eval, @id_unidad_neg, @unidad_neg, @porcent_cumpl, @porcent_bono, 'A')"
                        Else
                            'Modificación
                            SCMValores.CommandText = "UPDATE ms_cumpl_UN SET porcent_cumpl = @porcent_cumpl, porcent_bono = @porcent_bono WHERE id_ms_cumpl_UN = @id_ms_cumpl_UN"
                            SCMValores.Parameters.AddWithValue("@id_ms_cumpl_UN", .gvCumplUN.SelectedRow.Cells(0).Text)
                        End If
                    End If
                    SCMValores.Parameters.AddWithValue("@año_eval", CDate(.lblMesEvalD.Text).Year)
                    SCMValores.Parameters.AddWithValue("@mes_eval", CDate(.lblMesEvalD.Text).Month)
                    SCMValores.Parameters.AddWithValue("@id_unidad_neg", Val(.lblUnidadNegID.Text))
                    SCMValores.Parameters.AddWithValue("@unidad_neg", .lblUnidadNeg.Text)
                    SCMValores.Parameters.AddWithValue("@porcent_cumpl", .wpePorcentCumpl.Value)
                    'Obtener % para Pago de Bono
                    SCMValoresTemp.CommandText = "select top 1 porcent_bono " +
                                                 "from dt_porcent_bono " +
                                                 "where @porcentUN > porcent_min " +
                                                 "  and @porcentUN < = porcent_max "
                    SCMValoresTemp.Parameters.AddWithValue("@porcentUN", .wpePorcentCumpl.Value)
                    ConexionBD.Open()
                    SCMValores.Parameters.AddWithValue("@porcent_bono", SCMValoresTemp.ExecuteScalar())
                    ConexionBD.Close()
                    'Aplicar Cambio a ms_cumpl_UN
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    limpiarPantalla()
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        limpiarPantalla()
    End Sub

#End Region

End Class