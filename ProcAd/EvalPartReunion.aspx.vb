Public Class EvalPartReunion
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    'Session("id_usuario") = 5
                    'Session("idMsInst") = 20

                    If Session("litError") = "" And Val(Session("id_usuario")) > 0 Then
                        ._txtIdUsuario.Text = Session("id_usuario")
                        ._txtIdMsInst.Text = Session("idMsInst")

                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim sdaSol As New SqlDataAdapter
                        Dim dsSol As New DataSet
                        Dim query As String
                        query = "select tema " + _
                                "     , id_grupo " + _
                                "     , grupo " + _
                                "     , convert(varchar, fecha_reunion, 103) + replace(replace(replace(right(convert(varchar, fecha_reunion, 22), 12), 'AM', 'a. m.'), 'PM', 'p. m.'), '  ', ' 0') as fecha_reunion " + _
                                "     , status " + _
                                "from ms_reunion " + _
                                "where id_ms_reunion = @id_ms_reunion "
                        sdaSol.SelectCommand = New SqlCommand(query, ConexionBD)
                        sdaSol.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                        ConexionBD.Open()
                        sdaSol.Fill(dsSol)
                        ConexionBD.Close()
                        .lblFolio.Text = ._txtIdMsInst.Text
                        .lblTema.Text = dsSol.Tables(0).Rows(0).Item("tema").ToString()
                        ._txtIdGrupo.Text = dsSol.Tables(0).Rows(0).Item("id_grupo").ToString()
                        .lblGrupo.Text = dsSol.Tables(0).Rows(0).Item("grupo").ToString()
                        .lblFecha.Text = dsSol.Tables(0).Rows(0).Item("fecha_reunion").ToString()
                        sdaSol.Dispose()
                        dsSol.Dispose()

                        actualizarCalificacion()

                        'Panel
                        .pnlInicio.Visible = True
                        .pnlCalificacion.Visible = False
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

#Region "Funciones"

    Public Sub actualizarCalificacion()
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaParticipantes As New SqlDataAdapter
                Dim dsParticipantes As New DataSet
                .gvParticipacion.Columns(0).Visible = True
                .gvParticipacion.Columns(1).Visible = True
                .gvParticipacion.DataSource = dsParticipantes
                'Catálogo de Participantes
                sdaParticipantes.SelectCommand = New SqlCommand("select id_ms_contribucion " + _
                                                                "     , cgEmpl.nombre + ' ' + cgEmpl.ap_paterno + ' ' + cgEmpl.ap_materno as integrante " + _
                                                                "     , calif " + _
                                                                "from ms_contribucion " + _
                                                                "  left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " + _
                                                                "  left join cg_usuario on dt_reunion.id_usr_part = cg_usuario.id_usuario " + _
                                                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " + _
                                                                "where id_ms_reunion = @id_ms_reunion " + _
                                                                "  and id_usr_evaluador = @idUsuario ", ConexionBD)
                sdaParticipantes.SelectCommand.Parameters.AddWithValue("@id_ms_reunion", Val(._txtIdMsInst.Text))
                sdaParticipantes.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaParticipantes.Fill(dsParticipantes)
                .gvParticipacion.DataBind()
                ConexionBD.Close()
                sdaParticipantes.Dispose()
                dsParticipantes.Dispose()
                .gvParticipacion.SelectedIndex = -1
                .gvParticipacion.Columns(0).Visible = False

                For i = 0 To .gvParticipacion.Rows.Count - 1
                    If .gvParticipacion.Rows(i).Cells(3).Text.Trim = "&nbsp;" Then
                        .gvParticipacion.Rows(i).Cells(1).Controls(0).Visible = True
                    Else
                        .gvParticipacion.Rows(i).Cells(1).Controls(0).Visible = False
                    End If
                Next
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Calificación"

    Protected Sub gvParticipacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvParticipacion.SelectedIndexChanged
        Me.pnlCalificacion.Visible = True
        Me.wneCalif.Value = 0
    End Sub

    Protected Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                'Actualizar Calificación y Estatus de Contribución
                SCMValores.CommandText = "update ms_contribucion " + _
                                         "  set calif = @calif, status = 'E', fecha_respuesta = getdate() " + _
                                         "where id_ms_contribucion = @id_ms_contribucion "
                SCMValores.Parameters.AddWithValue("@calif", .wneCalif.Value)
                SCMValores.Parameters.AddWithValue("@id_ms_contribucion", .gvParticipacion.SelectedRow.Cells(0).Text)
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                Dim contP As Integer
                SCMValores.CommandText = "select count(*) " + _
                                         "from ms_contribucion " + _
                                         "where status = 'P' " + _
                                         "  and id_dt_reunion in (select id_dt_reunion " + _
                                         "                        from ms_contribucion " + _
                                         "                        where id_ms_contribucion = @id_ms_contribucion) "
                ConexionBD.Open()
                contP = SCMValores.ExecuteScalar
                ConexionBD.Close()

                If contP = 0 Then
                    'Ya se registraron todas las evaluaciones, actualizar Estatus y Calificación de Participación
                    SCMValores.CommandText = "update dt_reunion " + _
                                             "  set status = 'E', particip_calif = (case when (select count(*) eval_cancel " + _
                                             "                                                 from ms_contribucion " + _
                                             "                                                   left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " + _
                                             "                                                 where id_ms_reunion in (select id_ms_reunion " + _
                                             "                                                                         from ms_contribucion " + _
                                             "                                                                           left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " + _
                                             "                                                                         where id_ms_contribucion = @id_ms_contribucion) " + _
                                             "                                                   and id_usr_evaluador in (select dtReunion.id_usr_part " + _
                                             "                                                                            from ms_contribucion " + _
                                             "                                                                              left join dt_reunion dtReunion on ms_contribucion.id_dt_reunion = dtReunion.id_dt_reunion " + _
                                             "                                                                            where id_ms_contribucion = @id_ms_contribucion) " + _
                                             "                                                   and ms_contribucion.status = 'Z') = 0 " + _
                                             "                                        then (select (select isnull(avg(calif), 10) * .5 " + _
                                             "                                              from ms_contribucion " + _
                                             "                                              where id_dt_reunion in (select id_dt_reunion " + _
                                             "                                                                      from ms_contribucion " + _
                                             "                                                                      where id_ms_contribucion = @id_ms_contribucion))) " + _
                                             "                                        else 0 " + _
                                             "                                      end " + _
                                             "                                  + " + _
                                             "                                     (select case when particip_pos >= min_part then 5 else 0 end " + _
                                             "                                      from dt_reunion " + _
                                             "                                        left join ms_reunion on dt_reunion.id_ms_reunion = ms_reunion.id_ms_reunion " + _
                                             "                                        left join cg_grupo on ms_reunion.id_grupo = cg_grupo.id_grupo " + _
                                             "                                      where id_dt_reunion in (select id_dt_reunion " + _
                                             "                                                              from ms_contribucion " + _
                                             "                                                              where id_ms_contribucion = @id_ms_contribucion))) " + _
                                             "where id_dt_reunion in (select id_dt_reunion " + _
                                             "                        from ms_contribucion " + _
                                             "                        where id_ms_contribucion = @id_ms_contribucion) "
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Actualizar Calificación General del Participante
                    SCMValores.CommandText = "update dt_reunion " + _
                                             "  set reunion_calif = (select case when activ_calif is null " + _
                                             "                                then llegada_calif * (select valor from cg_parametros where parametro = 'porcent_AsPa_As') + particip_calif * (select valor from cg_parametros where parametro = 'porcent_AsPa_Pa') " + _
                                             "                                else llegada_calif * (select valor from cg_parametros where parametro = 'porcent_AsPaAc_As') + particip_calif * (select valor from cg_parametros where parametro = 'porcent_AsPaAc_Pa') + activ_calif * (select valor from cg_parametros where parametro = 'porcent_AsPaAc_Ac') " + _
                                             "                              end as calif_gral " + _
                                             "                       from dt_reunion " + _
                                             "                       where id_dt_reunion in (select id_dt_reunion " + _
                                             "                                               from ms_contribucion " + _
                                             "                                               where id_ms_contribucion = @id_ms_contribucion)) " + _
                                             "where id_dt_reunion in (select id_dt_reunion " + _
                                             "                        from ms_contribucion " + _
                                             "                        where id_ms_contribucion = @id_ms_contribucion) "
                    ConexionBD.Open()
                    SCMValores.ExecuteNonQuery()
                    ConexionBD.Close()

                    'Validar si aún existen Evaluaciones pendientes
                    Dim contPP As Integer
                    SCMValores.CommandText = "select count(*) " + _
                                             "from dt_reunion " + _
                                             "where id_ms_reunion in (select id_ms_reunion " + _
                                             "                        from ms_contribucion " + _
                                             "                          left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " + _
                                             "                        where id_ms_contribucion = @id_ms_contribucion) " + _
                                             "  and status = 'P' "
                    ConexionBD.Open()
                    contPP = SCMValores.ExecuteScalar
                    ConexionBD.Close()

                    If contPP = 0 Then
                        'Actualizar Calificación General de la Reunión
                        SCMValores.CommandText = "update ms_reunion " + _
                                                 "  set calif_gral = (select avg(reunion_calif) as calif_gral " + _
                                                 "                    from dt_reunion " + _
                                                 "                    where id_ms_reunion in (select id_ms_reunion " + _
                                                 "                                            from ms_contribucion " + _
                                                 "                                              left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " + _
                                                 "                                            where id_ms_contribucion = @id_ms_contribucion)) " + _
                                                 "where id_ms_reunion in (select id_ms_reunion " + _
                                                 "                        from ms_contribucion " + _
                                                 "                          left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " + _
                                                 "                        where id_ms_contribucion = @id_ms_contribucion) "
                        ConexionBD.Open()
                        SCMValores.ExecuteNonQuery()
                        ConexionBD.Close()
                    End If
                End If

                .pnlCalificacion.Visible = False

                actualizarCalificacion()
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

End Class