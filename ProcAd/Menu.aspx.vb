Public Class Menu
    Inherits System.Web.UI.Page
    Dim accessDB As New clsAccess

#Region "Inicio"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            With Me
                Try
                    ._txtIdUsuario.Text = Session("id_usuario")
                    'Verificar que exista el usuario en la tabla cg_usuario
                    'Creación de Variables para la conexión y consulta de infromación a la base de datos
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    'Ocultar Páneles
                    '-- Catálogo de Usuarios
                    .pnlCaTitulo.Visible = False
                    .pnlCatAltaUsr.Visible = False
                    .pnlCatAltaUsrAC.Visible = False
                    .pnlCatConcepto.Visible = False
                    .pnlCatServicio.Visible = False
                    .pnlCatOrigenDest.Visible = False
                    .pnlCatConsAut.Visible = False
                    .pnlCatVoBo.Visible = False
                    .pnlCatAuditoria.Visible = False
                    .pnlConsSer.Visible = False
                    '-- Solicitar Recursos
                    .pnlSRTitulo.Visible = False
                    .pnlSRSolicitar.Visible = False
                    .pnlSRAutorizar.Visible = False
                    .pnlSRAutDir.Visible = False
                    .pnlSRVoBo.Visible = False
                    .pnlSRConsultar.Visible = False
                    .pnlSRConsultarAV.Visible = False
                    '-- Anticipos
                    .pnlATitulo.Visible = False
                    .pnlAGenerarAAE.Visible = False
                    .pnlAGenerarTransfer.Visible = False
                    .pnlAEntregarEfect.Visible = False
                    .pnlARegistrarAAE.Visible = False
                    .pnlAConsultar.Visible = False
                    .pnlAConsCuadreA.Visible = False
                    .pnlAConsAud.Visible = False
                    '-- Comprobaciones
                    .pnlCTitulo.Visible = False
                    .pnlCGenerarCompExt.Visible = False
                    .pnlCGenerarComp.Visible = False
                    .pnlCAutCompExt.Visible = False
                    .pnlCAutorizar.Visible = False
                    .pnlCValidar.Visible = False
                    .pnlCEntregarEfect.Visible = False
                    .pnlCConsultar.Visible = False
                    .pnlCConsCompConta.Visible = False
                    .pnlCConsCompT.Visible = False
                    .pnlCConsCompSV.Visible = False
                    .pnlCConsCompExp.Visible = False
                    .pnlCConsultarComp.Visible = False
                    '-- Facturas SAT
                    .pnlFSATTitulo.Visible = False
                    .pnlFSATCarga.Visible = False
                    .pnlFSATConsultar.Visible = False
                    .pnlFSATLiq.Visible = False

                    '-- FACTURAS CFDI
                    .pnlFCFDIConsulta.Visible = False

                    '-- Negociación de Servicio
                    .pnlNSTitulo.Visible = False
                    .pnlNSSolicitar.Visible = False
                    .pnlNSIngresarCot.Visible = False
                    .pnlNSAutorizarCot.Visible = False
                    .pnlNSAutorizarNeg.Visible = False
                    .pnlNSConsultaNeg.Visible = False

                    '-- Servicios Negociados
                    .pnlSNTitulo.Visible = False
                    .pnlSNSolicitar.Visible = False
                    .pnlSNValidar1.Visible = False
                    .pnlSNAutorizar.Visible = False
                    .pnlSNValPresup.Visible = False
                    .pnlSNSolAmplPre.Visible = False
                    .pnlSNIngresarF.Visible = False
                    .pnlSNValidar2.Visible = False
                    .pnlSNCorregirF.Visible = False

                    '-- Facturas de Gastos, Seg. y Asesorías
                    .pnlFTitulo.Visible = False
                    .pnlFIngresar.Visible = False
                    .pnlFCorregir.Visible = False
                    .pnlFAutorizar.Visible = False

                    '-- Versión 2 Inicio
                    .pnlIFTitulo.Visible = False
                    .pnlIFSolicitar.Visible = False
                    .pnlIFValidar.Visible = False
                    .pnlIFIngresarCot.Visible = False
                    .pnlIFCorregirSol.Visible = False
                    .pnlIFAutorizarSol.Visible = False
                    .pnlIFValidarPresup.Visible = False
                    .pnlIFSolAmplPresup.Visible = False
                    .pnlCConsFactExp.Visible = False

                    '-- Contratos NAV Inicio
                    .pnlIFCompContrato.Visible = False
                    .pnlIFAutContrato.Visible = False
                    .pnlIFAsigCContrato.Visible = False
                    .pnlIFRegContrato.Visible = False
                    '-- Contratos NAV Fin

                    .pnlIFIngresar.Visible = False
                    .pnlIFAutorizarFact.Visible = False
                    '-- Versión 2 Fin

                    .pnlFAsignar.Visible = False
                    .pnlFRegistrarNAV.Visible = False
                    .pnlFConsultar.Visible = False
                    .pnlFConsultarCot.Visible = False

                    '-- Presupuesto de Gastos de Viaje
                    .pnlPGVTitulo.Visible = False
                    .pnlPGVCargarPresup.Visible = False
                    .pnlPGVConsulta.Visible = False
                    .pnlPGVSolicitarAmpl.Visible = False
                    .pnlPGVAutorizarAmpl.Visible = False
                    .pnlPGVConsultarAmpl.Visible = False
                    .pnlPGVValidarAmpl.Visible = False

                    '-- Reservación de Vehículos
                    .pnlRVTitulo.Visible = False
                    .pnlRAdministar.Visible = False
                    .pnlRVConsultar.Visible = False
                    '-- Gasolina
                    .pnlGTitulo.Visible = False
                    .pnlGCargar.Visible = False
                    .pnlGDispersar.Visible = False
                    .pnlGComprobar.Visible = False
                    .pnlGConsultar.Visible = False
                    '-- Vehículos / Bloqueos por Rendimiento
                    .pnlVBRTitulo.Visible = False
                    .pnlVBRCatVehiculo.Visible = False
                    .pnlVBRBloqueo.Visible = False
                    .pnlVBRConsultar.Visible = False
                    'Consultas de Auditoría
                    .pnlConsAud.Visible = False
                    .pnlRVConsultaV.Visible = False

                    '-- Catálogos de Evaluaciones
                    .pnlCatEvaluacion.Visible = False
                    '-- Registro de Evaluaciones
                    .pnlEvaluacionTit.Visible = False
                    .pnlEvaluacion.Visible = False
                    .pnlEvaluacionAut.Visible = False
                    .pnlEvaluacionCorr.Visible = False
                    .pnlValidarEvalA.Visible = False
                    .pnlAutorizarEvalA.Visible = False
                    .pnlCorregirEvalA.Visible = False
                    .pnlConcentrarEval.Visible = False
                    .pnlProcesarEval1Q.Visible = False
                    .pnlProcesarEval2Q.Visible = False
                    '-- Jefatura de Información % Cumplimiento UN
                    .pnlJefInfo.Visible = False
                    '-- Consulta de Evaluaciones
                    .pnlConsEvaluacion.Visible = False
                    '-- Movimientos Internos --
                    pnlMovInt.Visible = False
                    'Reuniones
                    .pnlReunionTit.Visible = False
                    .pnlCatGrupo.Visible = False
                    .pnlAltaReunion.Visible = False
                    .pnlSegReunion.Visible = False
                    .pnlEvalReunion.Visible = False
                    .pnlConsReunion.Visible = False
                    .pnlAltaActividad.Visible = False
                    .pnlConsActividad.Visible = False

                    'Tablas
                    .gvRegistrosReun.Visible = False
                    .gvRegistrosEval.Visible = False
                    .gvRegistrosSR.Visible = False
                    .pnlFiltroA.Visible = False
                    .gvRegistrosA.Visible = False
                    .gvRegistrosC.Visible = False
                    .gvRegistrosNS.Visible = False
                    .pnlFiltroF.Visible = False
                    .gvRegistrosF.Visible = False
                    .gvRegistrosSAP.Visible = False
                    .gvRegistrosV.Visible = False
                    .gvRegistrosG.Visible = False
                    .gvRegistrosDG.Visible = False
                    'Permisos
                    .pnlCatPermisos.Visible = False
                    'Movimientos internos

                    .pnlCodificacionCont.Visible = False


                    'Ingreso Checador

                    .pnlChecador.Visible = False
                    'Imagen
                    .imgTrans.Visible = False
                    .imgTrans.Width = 260

                    If Val(._txtIdUsuario.Text) = 0 Then
                        '.imgUsuario.Visible = False
                        '.lblUsuario.Visible = False
                        '.btnSalir.Text = "Inicio"
                        '.imgMenu.ImageUrl = "images\Usuario.png"
                        Server.Transfer("Login.aspx")
                    Else
                        Dim conteo As Integer = 0
                        Dim contMi As Integer = 0
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) from cg_usuario where id_usuario=@idUsuario and status = 'A'"
                        SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                        ConexionBD.Open()
                        conteo = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        .litError.Text = ""
                        If conteo > 0 Then
                            'Nombre de empleado
                            SCMValores.CommandText = "select nombre + ' ' + ap_paterno + ' ' + ap_materno " +
                                                     "from cg_usuario " +
                                                     "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                     "where id_usuario = @idUsuario "
                            ConexionBD.Open()
                            .lblUsuario.Text = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            'No. de empleado
                            SCMValores.CommandText = "select no_empleado " +
                                                     "from cg_usuario " +
                                                     "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                                     "where id_usuario = @idUsuario "
                            ConexionBD.Open()
                            ._txtNoEmpleado.Text = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            .litError.Text = ""

                            SCMValores.CommandText = "select perfil from cg_usuario where id_usuario = @idUsuario"
                            ConexionBD.Open()
                            ._txtPerfil.Text = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            SCMValores.CommandText = "select transporte from cg_usuario where id_usuario = @idUsuario"
                            ConexionBD.Open()
                            ._txtTransporte.Text = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            SCMValores.CommandText = "select id_usuario from cg_usuario where id_usuario = @idUsuario"
                            ConexionBD.Open()
                            contMi = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If contMi = 122 Then
                                .pnlCodificacionCont.Visible = True
                            Else
                                .pnlCodificacionCont.Visible = False
                            End If

                            'ALlta de usuarios implementacion Dayra 

                            Dim banEf As Integer
                            SCMValores.Connection = ConexionBD
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select count(*) " +
                                                 "where @idUsuario in (select split.a.value('.', 'NVARCHAR(MAX)') data " +
                                                 "                     from (select cast('<X>' + replace((select valor from cg_parametros where parametro = 'catUsuario'), ',', '</X><X>') + '</X>' as xml) as string) as A " +
                                                 "                       cross apply string.nodes('/X') as split(a)) "
                            SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            banEf = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If banEf > 0 Then
                                .pnlCatAltaUsrAC.Visible = True
                            Else
                                .pnlCatAltaUsrAC.Visible = False

                            End If

                            Dim sdaCatalogo As New SqlDataAdapter
                            Dim dsCatalogo As New DataSet
                            sdaCatalogo.SelectCommand = New SqlCommand("select checador from cg_usuario where id_usuario = @id_usuario", ConexionBD)
                            sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", ._txtIdUsuario.Text)
                            ConexionBD.Open()
                            sdaCatalogo.Fill(dsCatalogo)
                            ConexionBD.Close()

                            If dsCatalogo.Tables(0).Rows(0).Item("checador").ToString() = "S" Then
                                .pnlChecador.Visible = True
                            End If

                            'Acceso a panel de facturas CFDI

                            Dim accCFDI As Integer
                            SCMValores.Connection = ConexionBD
                            SCMValores.CommandText = ""
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = " SELECT COUNT(*) " +
                                " WHERE @idUsuario in (SELECT split.a.value('.', 'NVARCHAR(MAX)') DATA " +
                                " FROM (SELECT cast('<X>' + replace((SELECT valor FROM cg_parametros WHERE parametro = 'facturas_CFDI'), ',', '</X><X>') + '</X>' as xml) AS string) AS A " +
                                " CROSS APPLY string.nodes('/X') AS split(a)) "
                            SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            accCFDI = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If accCFDI > 0 Then
                                .pnlFCFDIConsulta.Visible = True
                            Else
                                .pnlFCFDIConsulta.Visible = False
                            End If

                            .imgMenu.Visible = True
                            .imgMenu.Width = 695
                            Select Case ._txtPerfil.Text
                                Case "Adm"

                                    .pnlCatPermisos.Visible = True

                                    '-- Ingresos Checador
                                    '.pnlChecador.Visible = True
                                    '-- Catálogo de Usuarios
                                    .pnlCaTitulo.Visible = True
                                    .pnlCatAltaUsr.Visible = True
                                    .pnlCatOrigenDest.Visible = True
                                    .pnlCatConsAut.Visible = True
                                    .pnlCatVoBo.Visible = True
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRAutDir.Visible = True
                                    .pnlSRVoBo.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    .pnlSRConsultarAV.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAGenerarAAE.Visible = True
                                    .pnlAGenerarTransfer.Visible = True
                                    .pnlAEntregarEfect.Visible = True
                                    .pnlARegistrarAAE.Visible = True
                                    .pnlAConsultar.Visible = True
                                    .pnlAConsCuadreA.Visible = True
                                    .pnlAConsAud.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarCompExt.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutCompExt.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCValidar.Visible = True
                                    .pnlCEntregarEfect.Visible = True
                                    .pnlCConsultar.Visible = True
                                    .pnlCConsCompConta.Visible = True
                                    .pnlCConsCompSV.Visible = True
                                    .pnlCConsCompExp.Visible = True
                                    .pnlCConsultarComp.Visible = True
                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATCarga.Visible = True
                                    .pnlFSATConsultar.Visible = True
                                    .pnlFSATLiq.Visible = False

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSIngresarCot.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    .pnlFTitulo.Visible = False
                                    .pnlFIngresar.Visible = False
                                    .pnlFCorregir.Visible = False
                                    .pnlFAutorizar.Visible = False

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFValidar.Visible = True
                                    .pnlIFIngresarCot.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFAutorizarSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFAsigCContrato.Visible = True
                                    .pnlIFRegContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFAsignar.Visible = True
                                    .pnlFRegistrarNAV.Visible = True
                                    .pnlFConsultar.Visible = True
                                    .pnlFConsultarCot.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRAdministar.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGCargar.Visible = True
                                    .pnlGDispersar.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True
                                    '-- Vehículos / Bloqueos por Rendimiento
                                    .pnlVBRTitulo.Visible = True
                                    .pnlVBRCatVehiculo.Visible = True
                                    .pnlVBRBloqueo.Visible = True
                                    .pnlVBRConsultar.Visible = True
                                    .pnlRVConsultaV.Visible = True

                                    ''-- Catálogos de Evaluaciones
                                    '.pnlCatEvaluacion.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "AltUsr"
                                    '-- Catálogo de Usuarios
                                    .pnlCaTitulo.Visible = True
                                    .pnlCatAltaUsr.Visible = True

                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "Usr"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "UsrSL"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    '.pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    '.pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    '.pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    '.pnlSNTitulo.Visible = True
                                    '.pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    '.pnlIFSolicitar.Visible = True
                                    '.pnlIFCorregirSol.Visible = True
                                    '.pnlIFSolAmplPresup.Visible = True
                                    '.pnlIFCompContrato.Visible = True
                                    '.pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    '.pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "Liq"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATLiq.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "AdmViajes"
                                    '-- Catálogos
                                    .pnlCaTitulo.Visible = True
                                    .pnlCatVoBo.Visible = True
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRVoBo.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    .pnlSRConsultarAV.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAGenerarAAE.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "SegViajes"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True
                                    .pnlCConsCompSV.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFRegistrarNAV.Visible = True
                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "Compras"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSIngresarCot.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFIngresarCot.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFRegContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    .pnlFConsultarCot.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "JefCompras"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSIngresarCot.Visible = True
                                    .pnlNSAutorizarCot.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFIngresarCot.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFRegContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    .pnlFConsultarCot.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"

                                Case "AutAud"
                                    '-- Catálogos
                                    .pnlCaTitulo.Visible = True
                                    .pnlCatAuditoria.Visible = True

                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"

                                Case "Aut", "DirAdFi"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRAutDir.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"

                                Case "DesOrg"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    '-- Catálogos de Evaluaciones
                                    .pnlCatEvaluacion.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "JefInfo"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    '-- Jefatura de Información % Cumplimiento UN
                                    .pnlJefInfo.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "GerTesor"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutCompExt.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True
                                    .pnlSNAutorizar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFAutorizarSol.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "ValPresup"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True
                                    .pnlCConsCompSV.Visible = True
                                    .pnlCConsCompConta.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True
                                    .pnlSNValPresup.Visible = True
                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFValidarPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFRegistrarNAV.Visible = True
                                    .pnlFConsultar.Visible = True

                                    '-- Presupuesto de Gastos de Viaje
                                    .pnlPGVTitulo.Visible = True
                                    .pnlPGVCargarPresup.Visible = True
                                    .pnlPGVConsultarAmpl.Visible = True

                                    .pnlPGVValidarAmpl.Visible = True

                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    .pnlRVConsultaV.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "AdmonDCM"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True

                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True
                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATCarga.Visible = True
                                    .pnlFSATConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True
                                    .pnlSNAutorizar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFAutorizarSol.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFAsignar.Visible = True

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "GerConta"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True
                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATCarga.Visible = True
                                    .pnlFSATConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "Aud"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    .pnlAConsAud.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    .pnlFConsultarCot.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .pnlConsAud.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "AdmCat"
                                    '-- Catálogos
                                    .pnlCaTitulo.Visible = True
                                    .pnlCatAltaUsrAC.Visible = True
                                    .pnlCatConcepto.Visible = True
                                    .pnlCatServicio.Visible = True
                                    pnlConsSer.Visible = True
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True
                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATCarga.Visible = True
                                    .pnlFSATConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAsigCContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFAsignar.Visible = True
                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "AdmCatEst"
                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATCarga.Visible = True
                                    .pnlFSATConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "Vig"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True

                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRAdministar.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "CoPame", "CoDCM"
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAEntregarEfect.Visible = True
                                    .pnlAGenerarTransfer.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin
                                    .pnlFConsultar.Visible = True

                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCValidar.Visible = True
                                    .pnlCEntregarEfect.Visible = True
                                    .pnlCConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGCargar.Visible = True
                                    .pnlGDispersar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "Comp"
                                    '-- Facturas SAT
                                    .pnlFSATTitulo.Visible = True
                                    .pnlFSATConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlCGenerarCompExt.Visible = True
                                    .pnlAGenerarTransfer.Visible = True
                                    .pnlARegistrarAAE.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin
                                    .pnlFConsultar.Visible = True

                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCValidar.Visible = True
                                    .pnlCConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGCargar.Visible = True
                                    .pnlGDispersar.Visible = True
                                    .pnlGConsultar.Visible = True
                                    .pnlCConsultarComp.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "Caja"
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAEntregarEfect.Visible = True
                                    .pnlAConsultar.Visible = True
                                    .pnlAConsCuadreA.Visible = True
                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCEntregarEfect.Visible = True
                                    .pnlCConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Auxiliar.png"
                                Case "Conta"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True
                                    .pnlCConsCompConta.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFAsignar.Visible = True
                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "ContaF"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFAsignar.Visible = True
                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "CxP"
                                    .pnlCConsultarComp.Visible = True

                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFRegistrarNAV.Visible = True
                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Usuario.png"
                                Case "SopTec"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True
                                    '-- Vehículos / Bloqueos por Rendimiento
                                    .pnlVBRTitulo.Visible = True
                                    .pnlVBRBloqueo.Visible = True
                                    .pnlVBRConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case "GerSopTec"
                                    '-- Solicitar Recursos
                                    .pnlSRTitulo.Visible = True
                                    .pnlSRSolicitar.Visible = True
                                    .pnlSRAutorizar.Visible = True
                                    .pnlSRConsultar.Visible = True
                                    '-- Anticipos
                                    .pnlATitulo.Visible = True
                                    .pnlAConsultar.Visible = True
                                    '-- Comprobaciones
                                    .pnlCTitulo.Visible = True
                                    .pnlCGenerarComp.Visible = True
                                    .pnlCAutorizar.Visible = True
                                    .pnlCConsultar.Visible = True

                                    '-- Negociación de Servicio
                                    .pnlNSTitulo.Visible = True
                                    .pnlNSSolicitar.Visible = True
                                    .pnlNSConsultaNeg.Visible = True

                                    '-- Servicios Negociados
                                    .pnlSNTitulo.Visible = True
                                    .pnlSNSolicitar.Visible = True

                                    '-- Facturas de Gastos, Seg. y Asesorías
                                    '.pnlFTitulo.Visible = True
                                    '.pnlFIngresar.Visible = True
                                    '.pnlFCorregir.Visible = True
                                    '.pnlFAutorizar.Visible = True

                                    '-- Versión 2 Inicio
                                    .pnlIFTitulo.Visible = True
                                    .pnlIFSolicitar.Visible = True
                                    .pnlIFCorregirSol.Visible = True
                                    .pnlIFSolAmplPresup.Visible = True
                                    .pnlIFIngresar.Visible = True
                                    .pnlIFCompContrato.Visible = True
                                    .pnlIFAutContrato.Visible = True
                                    .pnlIFAutorizarFact.Visible = True
                                    '-- Versión 2 Fin

                                    .pnlFConsultar.Visible = True
                                    '-- Reservación de Vehículos
                                    .pnlRVTitulo.Visible = True
                                    .pnlRVConsultar.Visible = True
                                    '-- Gasolina
                                    .pnlGTitulo.Visible = True
                                    .pnlGComprobar.Visible = True
                                    .pnlGConsultar.Visible = True
                                    '-- Vehículos / Bloqueos por Rendimiento
                                    .pnlVBRTitulo.Visible = True
                                    .pnlVBRCatVehiculo.Visible = True
                                    .pnlVBRBloqueo.Visible = True
                                    .pnlVBRConsultar.Visible = True

                                    .imgMenu.ImageUrl = "images\Admon.png"
                                Case Else
                                    .imgMenu.ImageUrl = "images\Usuario.png"
                            End Select

                            Dim contConsExp As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "where @idUsuario in (select split.a.value('.', 'NVARCHAR(MAX)') data " +
                                                     "                     from (select cast('<X>' + replace((select valor from cg_parametros where parametro = 'consulta_expromat'), ',', '</X><X>') + '</X>' as xml) as string) as A " +
                                                     "                       cross apply string.nodes('/X') as split(a)) "
                            ConexionBD.Open()
                            contConsExp = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contConsExp > 0 Then 'Usuario de Expromat configurado para Consultas
                                .pnlCConsFactExp.Visible = True
                                .pnlCConsCompExp.Visible = True
                            End If

                            Dim contVal As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from (select distinct(id_usr_valida) as validador " +
                                                     "      from cg_tipo_servicio " +
                                                     "      where status = 'A' " +
                                                     "        and id_usr_valida > 0 " +
                                                     "      union " +
                                                     "      select distinct(id_usuario) as validador " +
                                                     "      from dt_autoriza_servicio " +
                                                     "      where valida_autoriza = 'V' " +
                                                     "      union " +
                                                     "      select distinct(id_usr_valida) " +
                                                     "      from ms_factura " +
                                                     "      where id_usr_valida is not null " +
                                                     "        and fecha_valida is null) validadores " +
                                                     "where @idUsuario = validador "
                            ConexionBD.Open()
                            contVal = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contVal > 0 Then
                                .pnlIFValidar.Visible = True
                            End If

                            'Verificar si existe evaluación pendiente por registrar
                            ''SCMValores.CommandText = "select count(*) as empInd " + _
                            ''                         "from dt_empleado " + _
                            ''                         "where case len(@no_empleado) when 4 then '000' + @no_empleado when 5 then '00' + @no_empleado else @no_empleado end = dt_empleado.no_empleado " + _
                            ''                         " and status = 'A'" + _
                            ''                         " and (select cast(valor as datetime) " + _
                            ''                         "      from cg_parametros " + _
                            ''                         "      where parametro = 'mes_eval') >= dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) "
                            Dim evalReg As Integer
                            SCMValores.CommandText = "select count(*) as empInd " +
                                                     "from dt_empleado " +
                                                     "  left join ms_evaluacion on dt_empleado.no_empleado = ms_evaluacion.no_empleado " +
                                                     "                         and mes_eval = month((select cast(valor as datetime) " +
                                                     "                                               from cg_parametros " +
                                                     "                                               where parametro = 'mes_eval')) " +
                                                     "                         and año_eval = year((select cast(valor as datetime) " +
                                                     "                                              from cg_parametros " +
                                                     "                                              where parametro = 'mes_eval')) " +
                                                     "where id_usr_evalua = @idUsuario " +
                                                     "  and (select count(*) from dt_empl_ind where dt_empl_ind.id_dt_empleado = dt_empleado.id_dt_empleado and status = 'A') > 0 " +
                                                     "  and dt_empleado.status = 'A' " +
                                                     "  and (select cast(valor as datetime) " +
                                                     "       from cg_parametros " +
                                                     "       where parametro = 'mes_eval') >= dateadd(mm, 2, cast(cast(datepart(year, fecha_ingreso) as varchar(4)) + '/' + cast(datepart(month, fecha_ingreso) as varchar(2)) + '/01' as date)) " +
                                                     "  and id_ms_evaluacion is null "
                            ConexionBD.Open()
                            evalReg = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalReg > 0 Then
                                .pnlEvaluacion.Visible = True
                            End If

                            'Verificar si existe al menos una evaluación por validar por el usuario
                            Dim evalAut As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacion " +
                                                     "where id_usr_valida = @idUsuario " +
                                                     "  and status = 'P' " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalAut = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalAut > 0 Then
                                .pnlEvaluacionAut.Visible = True
                            End If


                            'Verificar si tiene acceso a realizar un movimiento interno
                            Dim movInt As Integer
                            SCMValores.CommandText = "select count(*) from " +
                                "cg_usuario where movimientos_internos = 'S' and id_usuario = @idUsuario "
                            ConexionBD.Open()
                            movInt = SCMValores.ExecuteScalar
                            ConexionBD.Close()
                            If movInt > 0 Then
                                pnlMovInt.Visible = True
                                pnlAutMovInt.Visible = False
                                pnlSolMovInt.Visible = True
                            End If
                            'Verificar si existe algun autorizacion por aprobar en caso de se autorizador'
                            Dim autMovInt As Integer
                            SCMValores.CommandText = "select count(*) from " +
                                                     "ms_movimientos_internos " +
                                                     "left join ms_instancia On ms_instancia.id_ms_sol = ms_movimientos_internos.id_ms_movimientos_internos " +
                                                     "where id_usr_autoriza  = @idUsuario and id_actividad = 124"
                            ' SCMValores.Parameters.AddWithValue("@idUsuario", Val(_txtIdUsuario.Text))
                            ConexionBD.Open()
                            autMovInt = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If autMovInt > 0 Then
                                pnlMovInt.Visible = True
                                pnlAutMovInt.Visible = True
                                lblAutMovInt.Visible = True
                                lblAutMovInt.Text = autMovInt.ToString
                                lblAutMov.Visible = True
                                lblAutMov.Text = autMovInt.ToString
                            Else
                                lblAutMovInt.Visible = False
                                lblAutMov.Visible = False
                            End If



                            'Verificar si existe al menos una evaluación por corregir del usuario
                            Dim evalCorr As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacion " +
                                                     "where id_usr_registro = @idUsuario " +
                                                     "  and status = 'PC' " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalCorr = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalCorr > 0 Then
                                .pnlEvaluacionCorr.Visible = True
                            End If

                            'Validar si existe la menos un grupo de evaluaciones por autorizar 
                            Dim evalAAut As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacionA " +
                                                     "where status = 'PA' " +
                                                     "  and id_usr_director = @idUsuario " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalAAut = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalAAut > 0 Then
                                .pnlAutorizarEvalA.Visible = True
                            End If

                            'Validar si existe la menos un grupo de evaluaciones por corregir
                            Dim evalACorr As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacionA " +
                                                     "where status = 'PCA' " +
                                                     "  and ms_evaluacionA.id_usr_evalua = @idUsuario " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalACorr = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalACorr > 0 Then
                                .pnlCorregirEvalA.Visible = True
                            End If

                            'Validar si existe la menos un grupo de evaluaciones por validar
                            Dim evalAVal As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacionA " +
                                                     "where status = 'PVA' " +
                                                     "  and @idUsuario in (select top 1 ms_evaluacion.id_usr_valida " +
                                                     "                     from ms_evaluacionA msEvalAT " +
                                                     "                       left join ms_evaluacion on msEvalAT.id_dt_area = ms_evaluacion.id_dt_area and msEvalAT.año_eval = ms_evaluacion.año_eval and msEvalAT.mes_eval = ms_evaluacion.mes_eval " +
                                                     "                     where msEvalAT.id_ms_evaluacionA = ms_evaluacionA.id_ms_evaluacionA " +
                                                     "                       and ms_evaluacion.invalida = 'S' " +
                                                     "                       and id_usr_valida is not null) " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalAVal = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalAVal > 0 Then
                                .pnlValidarEvalA.Visible = True
                            End If

                            'Validar si existe la menos un grupo de evaluaciones por procesar (1ra Quincena)
                            Dim evalProc1Q As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacion " +
                                                     "where status in ('PCE', 'PPE', 'EP') " +
                                                     "  and @idUsuario = (select valor " +
                                                     "                    from cg_parametros " +
                                                     "                    where parametro = 'id_usr_nomina_eval') " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) " +
                                                     "  and fecha_nomina_1ra is null "
                            ConexionBD.Open()
                            evalProc1Q = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalProc1Q > 0 Then
                                .pnlProcesarEval1Q.Visible = True
                            End If

                            'Validar si existe la menos un grupo de evaluaciones por concentrar
                            Dim evalConc As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacion " +
                                                     "where status = 'PCE' " +
                                                     "  and @idUsuario = (select valor " +
                                                     "                    from cg_parametros " +
                                                     "                    where parametro = 'id_usr_valida_evalA') " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalConc = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalConc > 0 Then
                                .pnlConcentrarEval.Visible = True
                            End If

                            'Validar si existe la menos un grupo de evaluaciones por procesar (2da Quincena)
                            Dim evalProc2Q As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacion " +
                                                     "where status = 'PPE' " +
                                                     "  and @idUsuario = (select valor " +
                                                     "                    from cg_parametros " +
                                                     "                    where parametro = 'id_usr_nomina_eval') " +
                                                     "  and mes_eval = month((select cast(valor as datetime) " +
                                                     "                        from cg_parametros " +
                                                     "                        where parametro = 'mes_eval')) " +
                                                     "  and año_eval = year((select cast(valor as datetime) " +
                                                     "                       from cg_parametros " +
                                                     "                       where parametro = 'mes_eval')) "
                            ConexionBD.Open()
                            evalProc2Q = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If evalProc2Q > 0 Then
                                .pnlProcesarEval2Q.Visible = True
                            End If

                            'Validar si debe tener acceso a la consulta
                            Dim contPnl As Integer
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_evaluacion " +
                                                     "  left join cg_usuario cgUsrEval on ms_evaluacion.id_usr_registro = cgUsrEval.id_usuario " +
                                                     "  left join cg_usuario cgUsrVal on ms_evaluacion.id_usr_valida = cgUsrVal.id_usuario " +
                                                     "  left join cg_usuario cgUsrDir on ms_evaluacion.id_usr_director = cgUsrDir.id_usuario " +
                                                     "where cgUsrEval.id_usuario = @idUsuario " +
                                                     "   or cgUsrVal.id_usuario = @idUsuario " +
                                                     "   or cgUsrDir.id_usuario = @idUsuario " +
                                                     "   or ms_evaluacion.no_empleado = @noEmpleado "
                            SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                            SCMValores.Parameters.AddWithValue("@noEmpleado", Val(._txtNoEmpleado.Text))
                            ConexionBD.Open()
                            contPnl = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contPnl > 0 Or ._txtPerfil.Text = "DesOrg" Or ._txtPerfil.Text = "JefInfo" Then
                                .pnlConsEvaluacion.Visible = True
                            End If

                            'Validar si debe tener a Catálogo de Grupos
                            Dim contLid As Integer
                            SCMValores.Parameters.Clear()
                            SCMValores.CommandText = "select count(*) " +
                                                     "from cg_usuario " +
                                                     "where id_usuario = @idUsuario " +
                                                     "  and lider = 'S' "
                            SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                            ConexionBD.Open()
                            contLid = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contLid > 0 Then
                                .pnlCatGrupo.Visible = True
                            End If

                            'Validar si es Secretario de algún Grupo Activo
                            Dim contSec As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from cg_grupo " +
                                                     "where id_usr_secretario = @idUsuario " +
                                                     "  and status = 'A' "
                            ConexionBD.Open()
                            contSec = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contSec > 0 Then
                                .pnlAltaReunion.Visible = True
                            End If

                            Dim SCMTemp As SqlCommand = New System.Data.SqlClient.SqlCommand
                            SCMTemp.Connection = ConexionBD
                            SCMTemp.CommandText = ""
                            SCMTemp.Parameters.Clear()
                            SCMTemp.CommandText = "update dt_reunion set status = 'Z' where id_ms_reunion in (select id_ms_reunion from ms_reunion where status = 'P' and dateadd(hour, 1, fecha_reunion) < getdate()) "
                            ConexionBD.Open()
                            SCMTemp.ExecuteNonQuery()
                            ConexionBD.Close()
                            SCMTemp.CommandText = "update ms_reunion set status = 'Z' where status = 'P' and dateadd(hour, 1, fecha_reunion) < getdate() "
                            ConexionBD.Open()
                            SCMTemp.ExecuteNonQuery()
                            ConexionBD.Close()

                            'Validar si es Secretario de alguna Reunión Activa
                            Dim contSeg As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_reunion " +
                                                     "  left join cg_grupo on ms_reunion.id_grupo = cg_grupo.id_grupo " +
                                                     "where (ms_reunion.id_usr_secretario = @idUsuario " +
                                                     "    or cg_grupo.id_usr_lider = @idUsuario) " +
                                                     "  and ms_reunion.status in ('P', 'I') "
                            ConexionBD.Open()
                            contSeg = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contSeg > 0 Then
                                .pnlSegReunion.Visible = True
                            End If

                            'Validar si es Líder, Secretario o Responsable de al menos una actividad
                            Dim contIntReun As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from (select distinct(id_ms_actividad) " +
                                                     "      from ms_actividad " +
                                                     "        left join cg_grupo on ms_actividad.id_grupo = cg_grupo.id_grupo " +
                                                     "        left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo and dt_grupo.status = 'A' " +
                                                     "      where cg_grupo.status = 'A' " +
                                                     "        and (id_usr_secretario = @idUsuario " +
                                                     "            or id_usr_lider = @idUsuario) " +
                                                     "      union " +
                                                     "      select distinct(id_ms_actividad) " +
                                                     "      from ms_actividad " +
                                                     "      where id_usr_responsable = @idUsuario) ms_act "
                            ConexionBD.Open()
                            contIntReun = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contIntReun > 0 Then
                                .pnlConsActividad.Visible = True
                            End If

                            'Validar si es Integrante de al menos un Grupo
                            Dim contIntR As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from cg_grupo " +
                                                     "  left join dt_grupo on cg_grupo.id_grupo = dt_grupo.id_grupo and dt_grupo.status = 'A' " +
                                                     "where cg_grupo.status = 'A' " +
                                                     "  and dt_grupo.status = 'A' " +
                                                     "  and id_usr_part = @idUsuario "
                            ConexionBD.Open()
                            contIntR = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contIntR > 0 Then
                                .pnlAltaActividad.Visible = True
                            End If

                            'Validar si tiene pendiente al menos una evaluación por registrar
                            Dim contEvalReun As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_contribucion " +
                                                     "where id_usr_evaluador = @idUsuario " +
                                                     "  and ms_contribucion.status = 'P' "
                            ConexionBD.Open()
                            contEvalReun = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contEvalReun > 0 Then
                                .pnlEvalReunion.Visible = True
                            End If

                            'Validar si es Secretario o Líder de alguna Reunión
                            Dim contReu As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_reunion " +
                                                     "  left join cg_grupo on ms_reunion.id_grupo = cg_grupo.id_grupo " +
                                                     "where (ms_reunion.id_usr_secretario = @idUsuario " +
                                                     "    or cg_grupo.id_usr_lider = @idUsuario) "
                            ConexionBD.Open()
                            contReu = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contReu > 0 Then
                                .pnlConsReunion.Visible = True
                            End If

                            If .pnlCatGrupo.Visible = True Or .pnlAltaReunion.Visible = True Or .pnlSegReunion.Visible = True Or .pnlEvalReunion.Visible = True Or .pnlConsReunion.Visible = True Or .pnlAltaActividad.Visible = True Or .pnlConsActividad.Visible = True Then
                                .pnlReunionTit.Visible = True
                            End If

                            'Validar si tiene al menos una Negociación por Autorizar
                            Dim contNegPend As Integer
                            SCMValores.CommandText = "select count(*) aut_pend " +
                                                     "from ms_instancia " +
                                                     "  left join ms_negoc_servicio on ms_instancia.id_ms_sol = ms_negoc_servicio.id_ms_negoc_servicio and ms_instancia.tipo = 'NS' " +
                                                     "where id_actividad = 90 " +
                                                     "  and ms_negoc_servicio.id_usr_aut_negoc = @idUsuario "
                            ConexionBD.Open()
                            contNegPend = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contNegPend > 0 Then
                                .pnlNSAutorizarNeg.Visible = True
                            End If

                            'Validar si tiene al menos una Solicitud de Servicio Negociado por Validar
                            Dim contValSNPend As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_instancia " +
                                                     "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                     "where id_actividad = 94 " +
                                                     "  and id_usr_valida = @idUsuario "
                            ConexionBD.Open()
                            contValSNPend = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contValSNPend > 0 Then
                                .pnlSNValidar1.Visible = True
                            End If

                            'Validar si tiene al menos una Solicitud de Ampliación de Presupuesto
                            Dim contSolAmplSNPend As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_instancia " +
                                                     "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                     "where id_actividad = 99 " +
                                                     "  and id_usr_solicita = @idUsuario "
                            ConexionBD.Open()
                            contSolAmplSNPend = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contSolAmplSNPend > 0 Then
                                .pnlSNSolAmplPre.Visible = True
                            End If

                            'Validar si tiene al menos una Solicitud de Servicio Negociado por Validar Soportes
                            Dim contVal2SNPend As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_instancia " +
                                                     "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                     "where id_actividad = 102 " +
                                                     "  and id_usr_valida2 = @idUsuario "
                            ConexionBD.Open()
                            contVal2SNPend = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contVal2SNPend > 0 Then
                                .pnlSNValidar2.Visible = True
                            End If

                            'Validar si tiene al menos una Solicitud de Servicio Negociado por Ingresar Factura
                            Dim contIngFactSNPend As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_instancia " +
                                                     "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                     "where id_actividad = 101 " +
                                                     "  and id_usr_solicita = @idUsuario "
                            ConexionBD.Open()
                            contIngFactSNPend = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contIngFactSNPend > 0 Then
                                .pnlSNIngresarF.Visible = True
                            End If

                            'Validar si tiene al menos una Solicitud de Servicio Negociado por Corregir
                            Dim contCorregFactSNPend As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_instancia " +
                                                     "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                     "where id_actividad = 104 " +
                                                     "  and id_usr_solicita = @idUsuario "
                            ConexionBD.Open()
                            contCorregFactSNPend = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contCorregFactSNPend > 0 Then
                                .pnlSNCorregirF.Visible = True
                            End If

                            'Validar si tiene acceso a la consulta de Comprobaciones Detalle (ConsCompT)
                            If Val(._txtTransporte.Text) = 1 Or ._txtPerfil.Text = "GerConta" Or ._txtPerfil.Text = "Aud" Then
                                .pnlCConsCompT.Visible = True
                                'Consulta de Autorizadores
                                .pnlCaTitulo.Visible = True
                                .pnlCatConsAut.Visible = True
                            End If

                            'Validar si es responsable de al menos un Centro de Costo
                            Dim contRespCC As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_presupuesto " +
                                                     "  left join bd_Empleado.dbo.cg_cc on ms_presupuesto.id_cc = cg_cc.id_cc " +
                                                     "where cg_cc.id_usr_responsable = @idUsuario " +
                                                     "  and año >= year(getdate()) "
                            ConexionBD.Open()
                            contRespCC = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contRespCC > 0 Then
                                .pnlPGVConsulta.Visible = True
                                .pnlPGVSolicitarAmpl.Visible = True
                            End If

                            'Validar si es Director de al menos una solicitud de ampliación pendiente
                            Dim contAmpP As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_ampliacion_p " +
                                                     "where status = 'P' " +
                                                     "  and id_usr_autoriza = @idUsuario "
                            ConexionBD.Open()
                            contAmpP = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contAmpP > 0 Then
                                .pnlPGVAutorizarAmpl.Visible = True
                                .lblAutorizarAmplPGV.Text = contAmpP.ToString
                                .lblPGV.Text = contAmpP.ToString
                            Else
                                .lblAutorizarAmplPGV.Text = ""
                                .lblPGV.Text = ""
                            End If
                            'Comprobar si existen validaciones de presupuesto pendientes
                            Dim contValP As Integer
                            SCMValores.CommandText = " select count(*)" +
                                                     " from ms_instancia" +
                                                     " where tipo ='SAP'" +
                                                     " and id_actividad = 117 "
                            ConexionBD.Open()
                            contValP = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contValP > 0 And _txtPerfil.Text = "ValPresup" Then
                                .lblValAmplPGV.Text = contValP.ToString
                                .lblPGV.Text = contValP.ToString
                            Else
                                .lblValAmplPGV.Text = ""
                                .lblPGV.Text = ""
                            End If


                            'Acceso a la consulta
                            Dim contAmp As Integer
                            SCMValores.CommandText = "select count(*) " +
                                                     "from ms_ampliacion_p " +
                                                     "where id_usr_solicita = @idUsuario or id_usr_autoriza = @idUsuario "
                            ConexionBD.Open()
                            contAmp = SCMValores.ExecuteScalar()
                            ConexionBD.Close()
                            If contAmp > 0 Then
                                .pnlPGVConsultarAmpl.Visible = True
                            End If

                            If .pnlPGVSolicitarAmpl.Visible = True Or .pnlPGVAutorizarAmpl.Visible = True Or .pnlPGVConsultarAmpl.Visible = True Then
                                .pnlPGVTitulo.Visible = True
                            End If

                            If .pnlCatEvaluacion.Visible = True Or .pnlEvaluacion.Visible = True Or .pnlEvaluacionAut.Visible = True Or .pnlEvaluacionCorr.Visible = True Or .pnlValidarEvalA.Visible = True Or .pnlAutorizarEvalA.Visible = True Or .pnlCorregirEvalA.Visible = True Or .pnlProcesarEval1Q.Visible = True Or .pnlConcentrarEval.Visible = True Or .pnlProcesarEval2Q.Visible = True Or .pnlJefInfo.Visible = True Or .pnlConsEvaluacion.Visible = True Then
                                .pnlEvaluacionTit.Visible = True
                            End If

                            'Conteos
                            ' Negociación de Servicios
                            Dim contAutorizarNegNS As Integer
                            If .pnlNSAutorizarNeg.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_negoc_servicio on ms_instancia.id_ms_sol = ms_negoc_servicio.id_ms_negoc_servicio and ms_instancia.tipo = 'NS' " +
                                                         "where id_actividad = 90 " +
                                                         "  and ms_negoc_servicio.id_usr_aut_negoc = @idUsuario "
                                ConexionBD.Open()
                                contAutorizarNegNS = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutorizarNegNS > 0 Then
                                    .lblAutorizarNegNS.Text = contAutorizarNegNS.ToString
                                Else
                                    .lblAutorizarNegNS.Text = ""
                                End If
                            Else
                                .lblAutorizarNegNS.Text = ""
                            End If

                            If contAutorizarNegNS > 0 Then
                                .lblNS.Text = contAutorizarNegNS.ToString
                            Else
                                .lblNS.Text = ""
                            End If

                            ' Servicios Negociados
                            Dim contValidar1SN As Integer
                            If .pnlSNValidar1.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                         "where id_actividad = 94 " +
                                                         "  and id_usr_valida = @idUsuario "
                                ConexionBD.Open()
                                contValidar1SN = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contValidar1SN > 0 Then
                                    .lblValidar1SN.Text = contValidar1SN.ToString
                                Else
                                    .lblValidar1SN.Text = ""
                                End If
                            Else
                                .lblValidar1SN.Text = ""
                            End If

                            Dim contAutorizarSN As Integer
                            If .pnlSNAutorizar.Visible = True Then
                                Dim query As String
                                query = "select count(*) " +
                                        "from ms_instancia " +
                                        "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                        "where id_actividad = 96 "
                                If ._txtPerfil.Text = "AdmonDCM" Then
                                    query = query + "  and empresa in ('COPE', 'DICOMEX', 'DIBIESE') "
                                Else
                                    query = query + "  and empresa not in ('COPE', 'DICOMEX', 'DIBIESE') "
                                End If
                                SCMValores.CommandText = query
                                ConexionBD.Open()
                                contAutorizarSN = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutorizarSN > 0 Then
                                    .lblAutorizarSN.Text = contAutorizarSN.ToString
                                Else
                                    .lblAutorizarSN.Text = ""
                                End If
                            Else
                                .lblAutorizarSN.Text = ""
                            End If

                            Dim contValidar2SN As Integer
                            If .pnlSNValidar2.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                         "where id_actividad = 102 " +
                                                         "  and id_usr_valida2 = @idUsuario "
                                ConexionBD.Open()
                                contValidar2SN = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contValidar2SN > 0 Then
                                    .lblValidar2SN.Text = contValidar2SN.ToString
                                Else
                                    .lblValidar2SN.Text = ""
                                End If
                            Else
                                .lblValidar2SN.Text = ""
                            End If

                            If (contValidar1SN + contAutorizarSN + contValidar2SN) > 0 Then
                                .lblSN.Text = (contValidar1SN + contAutorizarSN + contValidar2SN).ToString
                            Else
                                .lblSN.Text = ""
                            End If

                            ' Ingreso de Facturas
                            Dim contValidarSol As Integer
                            If .pnlIFValidar.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                         "where id_actividad = 44 " +
                                                         "  and id_usr_valida = @idUsuario "
                                ConexionBD.Open()
                                contValidarSol = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contValidarSol > 0 Then
                                    .lblValidarSol.Text = contValidarSol.ToString
                                Else
                                    .lblValidarSol.Text = ""
                                End If
                            Else
                                .lblAutorizarSol.Text = ""
                            End If

                            Dim contAutorizarSol As Integer
                            If .pnlIFAutorizarSol.Visible = True Then
                                Dim query As String
                                query = "select count(*) " +
                                        "from ms_instancia " +
                                        "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                        "where id_actividad = 47 "
                                If ._txtPerfil.Text = "AdmonDCM" Then
                                    query = query + "  and empresa in ('COPE', 'DICOMEX', 'DIBIESE') "
                                Else
                                    query = query + "  and empresa not in ('COPE', 'DICOMEX', 'DIBIESE') "
                                End If
                                SCMValores.CommandText = query
                                ConexionBD.Open()
                                contAutorizarSol = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutorizarSol > 0 Then
                                    .lblAutorizarSol.Text = contAutorizarSol.ToString
                                Else
                                    .lblAutorizarSol.Text = ""
                                End If
                            Else
                                .lblAutorizarSol.Text = ""
                            End If

                            Dim contValidarPresup As Integer
                            If .pnlIFValidarPresup.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                         "where id_actividad = 84 "
                                ConexionBD.Open()
                                contValidarPresup = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contValidarPresup > 0 Then
                                    .lblValidarPresup.Text = contValidarPresup.ToString
                                Else
                                    .lblValidarPresup.Text = ""
                                End If
                            Else
                                .lblValidarPresup.Text = ""
                            End If

                            Dim contAutContrato As Integer
                            If .pnlIFAutContrato.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                         "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                                         "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " +
                                                         "where id_actividad = 55 " +
                                                         "  and ms_contrato.id_usr_autoriza = @idUsuario "
                                ConexionBD.Open()
                                contAutContrato = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutContrato > 0 Then
                                    .lblAutContrato.Text = contAutContrato.ToString
                                Else
                                    .lblAutContrato.Text = ""
                                End If
                            Else
                                .lblAutContrato.Text = ""
                            End If

                            Dim contAutorizarFact As Integer = 0
                            If .pnlIFAutorizarFact.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                                         "where id_actividad in (50, 52, 53) " +
                                                         "  and ((id_usr_autoriza = @idUsuario and fecha_autoriza is null) " +
                                                         "    or (id_usr_autoriza2 = @idUsuario and fecha_autoriza2 is null and fecha_autoriza is not null) " +
                                                         "    or (id_usr_autoriza3 = @idUsuario and fecha_autoriza3 is null and fecha_autoriza is not null and fecha_autoriza2 is not null)) "
                                ConexionBD.Open()
                                contAutorizarFact = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutorizarFact > 0 Then
                                    .lblAutorizarFact.Text = contAutorizarFact.ToString
                                Else
                                    .lblAutorizarFact.Text = ""
                                End If
                            Else
                                .lblAutorizarFact.Text = ""
                            End If

                            If (contValidarSol + contAutorizarSol + contValidarPresup + contAutContrato + contAutorizarFact) > 0 Then
                                .lblServGastoAseso.Text = (contValidarSol + contAutorizarSol + contValidarPresup + contAutContrato + contAutorizarFact).ToString
                            Else
                                .lblServGastoAseso.Text = ""
                            End If

                            ' Solicitud de Recursos
                            Dim contAutSolRec As Integer = 0
                            If .pnlSRAutorizar.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                                         "where id_actividad = 38 " +
                                                         "  and id_usr_autoriza = @idUsuario "
                                ConexionBD.Open()
                                contAutSolRec = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutSolRec > 0 Then
                                    .lblAutSolRec.Text = contAutSolRec.ToString
                                Else
                                    .lblAutSolRec.Text = ""
                                End If
                            Else
                                .lblAutSolRec.Text = ""
                            End If

                            ' Solicitud de Recursos
                            Dim contAutSolRecDir As Integer = 0
                            If .pnlSRAutDir.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                                         "where id_actividad = 116 " +
                                                         "  and id_usr_director = @idUsuario "
                                ConexionBD.Open()
                                contAutSolRecDir = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutSolRecDir > 0 Then
                                    .lblSRAutDir.Text = contAutSolRecDir.ToString
                                Else
                                    .lblSRAutDir.Text = ""
                                End If
                            Else
                                .lblSRAutDir.Text = ""
                            End If
                            Dim contVoBoSolRec As Integer
                            If .pnlSRVoBo.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                                         "where id_actividad = 59 "
                                ConexionBD.Open()
                                contVoBoSolRec = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contVoBoSolRec > 0 Then
                                    .lblVoBoSolRec.Text = contVoBoSolRec.ToString
                                Else
                                    .lblVoBoSolRec.Text = ""
                                End If
                            Else
                                .lblVoBoSolRec.Text = ""
                            End If

                            If (contAutSolRec + contVoBoSolRec + contAutSolRecDir) > 0 Then
                                .lblSolRec.Text = "  " + (contAutSolRec + contVoBoSolRec + contAutSolRecDir).ToString
                            Else
                                .lblSolRec.Text = ""
                            End If

                            'Pre Compro
                            Dim contValidador As Integer
                            Dim contPreComp As Integer
                            SCMValores.CommandText = "select count(*) from dt_autorizador where id_autorizador =@idUsuario and validador ='S' "
                            ConexionBD.Open()
                            contValidador = SCMValores.ExecuteScalar()
                            ConexionBD.Close()

                            If contValidador > 0 Then
                                pnlPreAtorizacion.Visible = True
                                SCMValores.CommandText = "select count(*) " +
                                                       "from ms_instancia " +
                                                       "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                                       "where id_actividad = 115 " +
                                                       "  and ((ms_comp.id_validador = @idUsuario and ms_comp.status = 'P')) "
                                ConexionBD.Open()
                                contPreComp = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contPreComp > 0 Then
                                    .lblPreAutorizacion.Text = contPreComp.ToString
                                Else
                                    .lblPreAutorizacion.Text = ""
                                End If
                            Else
                                pnlPreAtorizacion.Visible = False
                            End If


                            ' Comprobaciones
                            Dim contAutComp As Integer
                            If .pnlCAutorizar.Visible = True Then
                                SCMValores.CommandText = "select count(*) " +
                                                         "from ms_instancia " +
                                                         "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                                         "where id_actividad = 9 " +
                                                         "  and ((id_usr_autoriza = @idUsuario and ms_comp.status = 'P') or (id_usr_aut_dir = @idUsuario and ms_comp.status = 'A')) "
                                ConexionBD.Open()
                                contAutComp = SCMValores.ExecuteScalar()
                                ConexionBD.Close()
                                If contAutComp > 0 Then
                                    .lblAutComp.Text = contAutComp.ToString
                                Else
                                    .lblAutComp.Text = ""
                                End If
                            Else
                                .lblAutComp.Text = ""
                            End If

                            If contAutComp > 0 Then
                                .lblComp.Text = contAutComp.ToString
                            Else
                                .lblComp.Text = ""
                            End If


                            If Val(Session("id_actividadM")) > 0 Then
                                ._txtIdAct.Text = Val(Session("id_actividadM"))
                                Select Case Session("TipoM")
                                    Case "Eval"
                                        If (Val(._txtIdAct.Text) = 69 And .pnlEvaluacionAut.Visible = True) Or (Val(._txtIdAct.Text) = 71 And .pnlEvaluacionCorr.Visible = True) Or (Val(._txtIdAct.Text) = 72 And .pnlValidarEvalA.Visible = True) Or (Val(._txtIdAct.Text) = 73 And .pnlAutorizarEvalA.Visible = True) Or (Val(._txtIdAct.Text) = 81 And .pnlCorregirEvalA.Visible = True) Or (Val(._txtIdAct.Text) = 82 And .pnlValidarEvalA.Visible = True) Then
                                            llenarGridEval()
                                        End If
                                    Case "SR"
                                        llenarGridSR()
                                    Case "A"
                                        llenarGridA()
                                    Case "C"
                                        llenarGridC()
                                    Case "NS"
                                        llenarGridNS()
                                    Case "SN"
                                        llenarGridSN()
                                    Case "F"
                                        llenarGridF()
                                    Case "SAP"
                                        llenarGridSAP()
                                    Case "V"
                                        llenarGridV()
                                End Select
                            End If
                        Else
                            Session("id_usuario") = 0
                            Server.Transfer("Login.aspx")
                        End If
                    End If
                Catch ex As Exception
                    .litError.Text = ex.ToString
                End Try
            End With
        End If
    End Sub

    Protected Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        With Me
            Try
                .litError.Text = ""
                Session("id_usuario") = 0
                ' Server.Transfer("Login.aspx")
                Response.Redirect("Login.aspx")

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

#Region "Funciones"

    Public Sub envio(ByVal titulo, ByVal pagina)
        With Me
            Try
                'Session("id_usuario") = Val(Me._txtIdUsuario.Text)
                'Session("TituloAct") = titulo
                'Server.Transfer(pagina)
                Session("id_usuario") = Val(Me._txtIdUsuario.Text)
                Session("perfil") = Me._txtPerfil.Text.ToString().Trim
                Session("TituloAct") = titulo
                Server.Transfer(pagina)

            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridReun()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = True
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")

                'Cancelar reuniónes pasadas de acuerdo al la fecha_reunión
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "update dt_reunion set status = 'Z' where id_ms_reunion in (select id_ms_reunion from ms_reunion where status = 'P' and dateadd(hour, 1, fecha_reunion) < getdate()) "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()
                SCMValores.CommandText = "update ms_reunion set status = 'Z' where status = 'P' and dateadd(hour, 1, fecha_reunion) < getdate() "
                ConexionBD.Open()
                SCMValores.ExecuteNonQuery()
                ConexionBD.Close()

                'Reuniones Pendientes
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                Dim query As String = ""

                .gvRegistrosReun.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosReun.Columns(0).Visible = True
                Select Case Val(._txtIdAct.Text)
                    Case -1
                        'Seguimiento de Reunión
                        query = "select id_ms_reunion " +
                                "     , ms_reunion.grupo " +
                                "     , tema " +
                                "     , fecha_reunion " +
                                "from ms_reunion " +
                                "  left join cg_grupo on ms_reunion.id_grupo = cg_grupo.id_grupo " +
                                "where ms_reunion.status in ('P', 'I') " +
                                "  and (ms_reunion.id_usr_secretario = @idUsuario or cg_grupo.id_usr_lider = @idUsuario) " +
                                "order by ms_reunion.grupo, tema "
                    Case -2
                        'Evaluación de Participantes
                        query = "select id_ms_reunion " +
                                "     , ms_reunion.grupo " +
                                "     , tema " +
                                "     , fecha_reunion " +
                                "from ms_reunion " +
                                "where id_ms_reunion in (select distinct(id_ms_reunion) as idMsReunion " +
                                "                        from ms_contribucion " +
                                "                          left join dt_reunion on ms_contribucion.id_dt_reunion = dt_reunion.id_dt_reunion " +
                                "                        where id_usr_evaluador = @idUsuario " +
                                "                          and ms_contribucion.status = 'P') "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosReun.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosReun.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosReun.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridEval()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = True
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosEval.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosEval.Columns(0).Visible = True
                If Val(._txtIdAct.Text) = 77 Then
                    .gvRegistrosEval.Columns(4).Visible = True
                    .gvRegistrosEval.Columns(5).Visible = True
                    .gvRegistrosEval.Columns(6).Visible = True
                End If
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 69
                        'Autorizar Evaluación (Autorizador)
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacion " +
                                "     , direccion " +
                                "     , area " +
                                "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " +
                                "     , puesto " +
                                "     , porcent_cumpl " +
                                "     , fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacion on ms_instancia.id_ms_sol = ms_evaluacion.id_ms_evaluacion " +
                                "where tipo = 'Eval' " +
                                "  and id_actividad = @id_actividad " +
                                "  and id_usr_valida = @id_usuario " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                    Case 71
                        'Corregir Evaluación (Usuario)
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacion " +
                                "     , direccion " +
                                "     , area " +
                                "     , nombre + ' ' + ap_paterno + ' ' + ap_materno as empleado " +
                                "     , puesto " +
                                "     , porcent_cumpl " +
                                "     , fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacion on ms_instancia.id_ms_sol = ms_evaluacion.id_ms_evaluacion " +
                                "where tipo = 'Eval' " +
                                "  and id_actividad = @id_actividad " +
                                "  and id_usr_registro = @id_usuario " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                    Case 72
                        'Validar Evaluaciones del Área (Jefatura de Información)
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacionA as id_ms_evaluacion " +
                                "     , ms_evaluacionA.direccion " +
                                "     , ms_evaluacionA.area " +
                                "     , ms_evaluacionA.lider as empleado " +
                                "     , ms_evaluacionA.puesto_lider as puesto " +
                                "     , (select avg(porcent_cumpl) " +
                                "        from ms_evaluacion " +
                                "        where ms_evaluacionA.id_dt_area = ms_evaluacion.id_dt_area " +
                                "          and ms_evaluacionA.año_eval = ms_evaluacion.año_eval " +
                                "          and ms_evaluacionA.mes_eval = ms_evaluacion.mes_eval) as porcent_cumpl " +
                                "     , ms_evaluacionA.fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluacionA.id_ms_evaluacionA " +
                                "where tipo = 'EvalA' " +
                                "  and id_actividad = @id_actividad " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                    Case 73
                        'Autorizar Evaluaciones (Director)
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacionA as id_ms_evaluacion " +
                                "     , ms_evaluacionA.direccion " +
                                "     , ms_evaluacionA.area " +
                                "     , ms_evaluacionA.lider as empleado " +
                                "     , ms_evaluacionA.puesto_lider as puesto " +
                                "     , (select avg(porcent_cumpl) " +
                                "        from ms_evaluacion " +
                                "        where ms_evaluacionA.id_dt_area = ms_evaluacion.id_dt_area " +
                                "          and ms_evaluacionA.año_eval = ms_evaluacion.año_eval " +
                                "          and ms_evaluacionA.mes_eval = ms_evaluacion.mes_eval) as porcent_cumpl " +
                                "     , ms_evaluacionA.fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluacionA.id_ms_evaluacionA " +
                                "where tipo = 'EvalA' " +
                                "  and id_actividad = @id_actividad " +
                                "  and id_usr_director = @id_usuario " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                    Case 75
                        'Corregir Evaluaciones (Autorizador)
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacionA as id_ms_evaluacion " +
                                "     , ms_evaluacionA.direccion " +
                                "     , ms_evaluacionA.area " +
                                "     , ms_evaluacionA.lider as empleado " +
                                "     , ms_evaluacionA.puesto_lider as puesto " +
                                "     , (select avg(porcent_cumpl) " +
                                "        from ms_evaluacion " +
                                "        where ms_evaluacionA.id_dt_area = ms_evaluacion.id_dt_area " +
                                "          and ms_evaluacionA.año_eval = ms_evaluacion.año_eval " +
                                "          and ms_evaluacionA.mes_eval = ms_evaluacion.mes_eval) as porcent_cumpl " +
                                "     , ms_evaluacionA.fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluacionA.id_ms_evaluacionA " +
                                "where tipo = 'EvalA' " +
                                "  and id_actividad = @id_actividad " +
                                "  and @id_usuario in (select id_usr_aut " +
                                "                      from dt_area " +
                                "                      where dt_area.id_dt_area = ms_evaluacionA.id_dt_area) " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                    Case 81
                        'Corregir Evaluaciones de Área Inválidas
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacionA as id_ms_evaluacion " +
                                "     , ms_evaluacionA.direccion " +
                                "     , ms_evaluacionA.area " +
                                "     , ms_evaluacionA.lider as empleado " +
                                "     , ms_evaluacionA.puesto_lider as puesto " +
                                "     , (select avg(porcent_cumpl) " +
                                "        from ms_evaluacion " +
                                "        where ms_evaluacionA.id_dt_area = ms_evaluacion.id_dt_area " +
                                "          and ms_evaluacionA.año_eval = ms_evaluacion.año_eval " +
                                "          and ms_evaluacionA.mes_eval = ms_evaluacion.mes_eval) as porcent_cumpl " +
                                "     , ms_evaluacionA.fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluacionA.id_ms_evaluacionA " +
                                "where tipo = 'EvalA' " +
                                "  and id_actividad = @id_actividad " +
                                "  and ms_evaluacionA.id_usr_evalua = @id_usuario " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                    Case 82
                        'Validar Evaluaciones de Área Inválidas
                        query = "select id_ms_instancia " +
                                "     , id_ms_evaluacionA as id_ms_evaluacion " +
                                "     , ms_evaluacionA.direccion " +
                                "     , ms_evaluacionA.area " +
                                "     , ms_evaluacionA.lider as empleado " +
                                "     , ms_evaluacionA.puesto_lider as puesto " +
                                "     , (select avg(porcent_cumpl) " +
                                "        from ms_evaluacion " +
                                "        where ms_evaluacionA.id_dt_area = ms_evaluacion.id_dt_area " +
                                "          and ms_evaluacionA.año_eval = ms_evaluacion.año_eval " +
                                "          and ms_evaluacionA.mes_eval = ms_evaluacion.mes_eval) as porcent_cumpl " +
                                "     , ms_evaluacionA.fecha_registro " +
                                "from ms_instancia " +
                                "  left join ms_evaluacionA on ms_instancia.id_ms_sol = ms_evaluacionA.id_ms_evaluacionA " +
                                "where tipo = 'EvalA' " +
                                "  and id_actividad = @id_actividad " +
                                "  and @id_usuario in (select top 1 ms_evaluacion.id_usr_valida " +
                                "                      from ms_evaluacionA msEvalAT " +
                                "                        left join ms_evaluacion on msEvalAT.id_dt_area = ms_evaluacion.id_dt_area and msEvalAT.año_eval = ms_evaluacion.año_eval and msEvalAT.mes_eval = ms_evaluacion.mes_eval " +
                                "                      where msEvalAT.id_ms_evaluacionA = ms_evaluacionA.id_ms_evaluacionA " +
                                "                        and ms_evaluacion.invalida = 'S' " +
                                "                        and id_usr_valida is not null) " +
                                "  and mes_eval = month((select cast(valor as datetime) " +
                                "                        from cg_parametros " +
                                "                        where parametro = 'mes_eval')) " +
                                "  and año_eval = year((select cast(valor as datetime) " +
                                "                       from cg_parametros " +
                                "                       where parametro = 'mes_eval')) "
                        'Case 77
                        '    'Procesar Evaluaciones (Nómina)
                        '    query = "select id_ms_instancia " + _
                        '            "     , ms_evaluacionC.id_ms_evaluacionC as id_ms_evaluacion " + _
                        '            "     , (select SUBSTRING(STUFF((select distinct(direccion) + ', ' " + _
                        '            "                                from dt_evaluacionC " + _
                        '            "                                  left join ms_evaluacionA on dt_evaluacionC.id_ms_evaluacionA = ms_evaluacionA.id_ms_evaluacionA " + _
                        '            "                                where dt_evaluacionC.id_ms_evaluacionC = ms_evaluacionC.id_ms_evaluacionC FOR XML PATH ('')), 1, 0, ''), 1, LEN(STUFF((select distinct(direccion) + ', ' " + _
                        '            "                                                                                                                                                       from dt_evaluacionC " + _
                        '            "                                                                                                                                                         left join ms_evaluacionA on dt_evaluacionC.id_ms_evaluacionA = ms_evaluacionA.id_ms_evaluacionA " + _
                        '            "                                                                                                                                                       where dt_evaluacionC.id_ms_evaluacionC = ms_evaluacionC.id_ms_evaluacionC FOR XML PATH ('')), 1, 0, ''))-1)) as direccion " + _
                        '            "     , null as area " + _
                        '            "     , null as empleado " + _
                        '            "     , null as puesto " + _
                        '            "     , (select avg(porcent_cumpl) " + _
                        '            "        from ms_evaluacion " + _
                        '            "          left join ms_evaluacionA on ms_evaluacion.id_dt_area = ms_evaluacionA.id_dt_area and ms_evaluacion.año_eval = ms_evaluacionA.año_eval and ms_evaluacion.mes_eval = ms_evaluacionA.mes_eval " + _
                        '            "          left join dt_evaluacionC on ms_evaluacionA.id_ms_evaluacionA = dt_evaluacionC.id_ms_evaluacionA " + _
                        '            "        where dt_evaluacionC.id_ms_evaluacionC = ms_evaluacionC.id_ms_evaluacionC) as porcent_cumpl " + _
                        '            "     , ms_evaluacionC.fecha_registro " + _
                        '            "from ms_instancia " + _
                        '            "  left join ms_evaluacionC on ms_instancia.id_ms_sol = ms_evaluacionC.id_ms_evaluacionC " + _
                        '            "where tipo = 'EvalC' " + _
                        '            "  and id_actividad = @id_actividad " + _
                        '            "  and ms_evaluacionC.mes_eval = month((select cast(valor as datetime) " + _
                        '            "                                       from cg_parametros " + _
                        '            "                                       where parametro = 'mes_eval')) " + _
                        '            "  and ms_evaluacionC.año_eval = year((select cast(valor as datetime) " + _
                        '            "                                      from cg_parametros " + _
                        '            "                                      where parametro = 'mes_eval')) "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosEval.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosEval.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosEval.Columns(0).Visible = False
                If Val(._txtIdAct.Text) = 77 Then
                    .gvRegistrosEval.Columns(4).Visible = False
                    .gvRegistrosEval.Columns(5).Visible = False
                    .gvRegistrosEval.Columns(6).Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridSR()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = True
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosSR.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosSR.Columns(0).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 38
                        'Solicitudes de Recursos por Autorizar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_recursos.id_ms_recursos " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , periodo_ini, periodo_fin " +
                                "     , destino " +
                                "     , id_ms_anticipo " +
                                "     , id_ms_reserva " +
                                "     , id_ms_comb " +
                                "     , id_ms_avion " +
                                "from ms_instancia " +
                                "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_autoriza = @id_usuario " +
                                "order by ms_recursos.id_ms_recursos "
                    Case 59
                        'Vo.Bo. a Solicitudes de Recursos
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_recursos.id_ms_recursos " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , periodo_ini, periodo_fin " +
                                "     , destino " +
                                "     , id_ms_anticipo " +
                                "     , id_ms_reserva " +
                                "     , id_ms_comb " +
                                "     , id_ms_avion " +
                                "from ms_instancia " +
                                "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                "where id_actividad = @id_actividad " +
                                "order by ms_recursos.id_ms_recursos "
                    Case 116
                        'Solicitudes de Recursos por Autorizar Director
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_recursos.id_ms_recursos " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , periodo_ini, periodo_fin " +
                                "     , destino " +
                                "     , id_ms_anticipo " +
                                "     , id_ms_reserva " +
                                "     , id_ms_comb " +
                                "     , id_ms_avion " +
                                "from ms_instancia " +
                                "  left join ms_recursos on ms_instancia.id_ms_sol = ms_recursos.id_ms_recursos and ms_instancia.tipo = 'SR' " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_director = @id_usuario " +
                                "order by ms_recursos.id_ms_recursos "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosSR.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosSR.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosSR.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridMovInt()
        'Primero ocultar los otros grids'
        'Presentar Tabla de Rervas por procesar
        imgMenu.Visible = False
        imgTrans.Visible = False
        gvRegistrosReun.Visible = False
        gvRegistrosEval.Visible = False
        gvRegistrosSR.Visible = False
        pnlFiltroA.Visible = False
        gvRegistrosA.Visible = False
        gvRegistrosC.Visible = False
        gvRegistrosNS.Visible = False
        pnlFiltroF.Visible = False
        gvRegistrosF.Visible = False
        gvRegistrosSAP.Visible = False
        gvRegistrosV.Visible = True
        gvRegistrosG.Visible = False
        gvRegistrosDG.Visible = False

        'Inicia proceso'

        gvMovInt.Visible = True
        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
        Dim sdaCatalogo As New SqlDataAdapter
        Dim dsCatalogo As New DataSet
        gvMovInt.DataSource = dsCatalogo
        gvMovInt.Columns(0).Visible = True
        Dim query As String = ""
        Select Case Val(_txtIdAct.Text)
            Case 124
                'Solicitudes por Autorizar
                query = " select id_ms_instancia, " +
                        "id_ms_movimientos_internos, " +
                        "empresa, " +
                        "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CG.id_empleado) as solicita," +
                        "centro_costo, " +
                        "fecha_solicita " +
                        "From ms_instancia " +
                        "Left Join ms_movimientos_internos MV on ms_instancia.id_ms_sol = MV.id_ms_movimientos_internos " +
                        "Left Join cg_usuario CG on MV.id_usr_solicita = CG.id_usuario " +
                        "where id_actividad = @idActividad And MV.id_usr_autoriza = @idUsuario"
            Case 125
                'Actividad codificacion contable 
                query = " select id_ms_instancia, " +
                        "id_ms_movimientos_internos, " +
                        "empresa, " +
                        "(select nombre + ' ' + ap_paterno + ' ' + ap_materno from bd_Empleado.dbo.cg_empleado where id_empleado = CG.id_empleado) as solicita," +
                        "centro_costo, " +
                        "fecha_solicita " +
                        "From ms_instancia " +
                        "Left Join ms_movimientos_internos MV on ms_instancia.id_ms_sol = MV.id_ms_movimientos_internos " +
                        "Left Join cg_usuario CG on MV.id_usr_solicita = CG.id_usuario " +
                        "where id_actividad = @idActividad "

        End Select

        sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idActividad", Val(_txtIdAct.Text))
        sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(_txtIdUsuario.Text))
        ConexionBD.Open()
        sdaCatalogo.Fill(dsCatalogo)
        gvMovInt.DataBind()
        ConexionBD.Close()
        sdaCatalogo.Dispose()
        dsCatalogo.Dispose()
        gvMovInt.SelectedIndex = -1
        'Inhabilitar columnas para vista
        gvMovInt.Columns(0).Visible = False

    End Sub


    Public Sub llenarGridA()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = True
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosA.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosA.Columns(0).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 2
                        'Anticipos por Autorizar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_anticipo.id_ms_anticipo " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "	  , periodo_ini, periodo_fin " +
                                "	  , destino " +
                                "	  , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
                                "     , fecha_autoriza " +
                                "from ms_instancia " +
                                "  left join ms_anticipo on ms_instancia.id_ms_sol = ms_anticipo.id_ms_anticipo and ms_instancia.tipo = 'A' " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_autoriza = @id_usuario " +
                                "order by ms_anticipo.id_ms_anticipo "
                    Case 3, 66
                        .pnlFiltroA.Visible = True

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        Dim queryA As String = ""

                        queryA = "select 0 as id_empresa " +
                                 "     , '' as nombre " +
                                 "union " +
                                 "select id_empresa, nombre " +
                                 "from bd_empleado.dbo.cg_empresa Empresa " +
                                 "where status = 'A' " +
                                 "  and nombre in (select distinct(empresa) " +
                                 "                 from ms_instancia " +
                                 "                   left join ms_anticipo on ms_instancia.id_ms_sol = ms_anticipo.id_ms_anticipo and ms_instancia.tipo = 'A' " +
                                 "                 where id_actividad = @id_actividad) "
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                queryA = queryA + "  and nombre = 'PAME' "
                            Case "CoDCM"
                                queryA = queryA + "  and nombre = 'DICOMEX' "
                        End Select
                        queryA = queryA + "order by nombre "

                        sdaEmpresa.SelectCommand = New SqlCommand(queryA, ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                        .ddlEmpresaA.DataSource = dsEmpresa
                        .ddlEmpresaA.DataTextField = "nombre"
                        .ddlEmpresaA.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresaA.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()

                        'Transferencias por Gestionar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_anticipo.id_ms_anticipo " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "	  , periodo_ini, periodo_fin " +
                                "	  , destino " +
                                "	  , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
                                "     , fecha_autoriza " +
                                "from ms_instancia " +
                                "  left join ms_anticipo on ms_instancia.id_ms_sol = ms_anticipo.id_ms_anticipo and ms_instancia.tipo = 'A' " +
                                "where id_actividad = @id_actividad  and ms_anticipo.status ='A'"
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                query = query + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by ms_anticipo.id_ms_anticipo "
                    Case 5
                        'Efectivo por Entregar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_anticipo.id_ms_anticipo " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "	  , periodo_ini, periodo_fin " +
                                "	  , destino " +
                                "	  , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
                                "     , fecha_autoriza " +
                                "from ms_instancia " +
                                "  left join ms_anticipo on ms_instancia.id_ms_sol = ms_anticipo.id_ms_anticipo and ms_instancia.tipo = 'A' " +
                                "where id_actividad = @id_actividad "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                query = query + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by ms_anticipo.id_ms_anticipo "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                If Val(._txtIdAct.Text) = 2 Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                End If
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosA.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosA.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosA.Columns(0).Visible = False
                If Val(._txtIdAct.Text) = 2 Then
                    .gvRegistrosA.Columns(9).Visible = False
                Else
                    .gvRegistrosA.Columns(9).Visible = True
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridC()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Comprobaciones por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = True
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosC.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosC.Columns(0).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 9, 62, 115
                        'Comprobación por Autorizar
                        query = "select id_ms_instancia " +
                                "     , id_ms_comp " +
                                "     , empresa " +
                                "     , empleado " +
                                "     , periodo_ini " +
                                "     , periodo_fin " +
                                "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                                "        from dt_anticipo " +
                                "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                                "     , (select sum(monto_total) as monto_comp " +
                                "        from dt_comp " +
                                "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                                "     , importe_tot " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                "where id_actividad = @id_actividad " +
                                "  and ((id_usr_autoriza = @id_usuario and ms_comp.status = 'P') or (id_usr_aut_dir = @id_usuario and ms_comp.status = 'A') or (id_usr_aut_dir = @id_usuario and ms_comp.status = 'A') or (id_validador=@id_usuario and aut_val='S'))" +
                                "order by ms_comp.id_ms_comp "
                        .gvRegistrosC.Columns(4).HeaderText = "Comprobó"
                    Case 10
                        'Validar Comprobación
                        query = "select id_ms_instancia " +
                                "     , id_ms_comp " +
                                "     , empresa " +
                                "     , empleado " +
                                "     , periodo_ini " +
                                "     , periodo_fin " +
                                "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                                "        from dt_anticipo " +
                                "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                                "     , (select sum(monto_total) as monto_comp " +
                                "        from dt_comp " +
                                "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                                "     , importe_tot " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                "where id_actividad = @id_actividad "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                query = query + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by ms_comp.id_ms_comp "
                        .gvRegistrosC.Columns(4).HeaderText = "Comprobó"
                    Case 12
                        'Corregir Comprobación
                        query = "select id_ms_instancia " +
                                "     , id_ms_comp " +
                                "     , empresa " +
                                "     , autorizador as empleado " +
                                "     , periodo_ini " +
                                "     , periodo_fin " +
                                "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                                "        from dt_anticipo " +
                                "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                                "     , (select sum(monto_total) as monto_comp " +
                                "        from dt_comp " +
                                "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                                "     , importe_tot " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_solicita = @id_usuario " +
                                "order by ms_comp.id_ms_comp "
                        .gvRegistrosC.Columns(4).HeaderText = "Autorizador"
                    Case 31
                        'Entregar Efectivo
                        query = "select id_ms_instancia " +
                                "     , id_ms_comp " +
                                "     , empresa " +
                                "     , empleado " +
                                "     , periodo_ini " +
                                "     , periodo_fin " +
                                "     , (select sum(monto_hospedaje) + sum(monto_alimentos) + sum(monto_casetas) + sum(monto_otros) as monto_ant " +
                                "        from dt_anticipo " +
                                "          left join ms_anticipo on dt_anticipo.id_ms_anticipo = ms_anticipo.id_ms_anticipo " +
                                "        where dt_anticipo.id_ms_comp = ms_comp.id_ms_comp) as importe_ant " +
                                "     , (select sum(monto_total) as monto_comp " +
                                "        from dt_comp " +
                                "        where dt_comp.id_ms_comp = ms_comp.id_ms_comp) as importe_comp " +
                                "     , importe_tot " +
                                "from ms_instancia " +
                                "  left join ms_comp on ms_instancia.id_ms_sol = ms_comp.id_ms_comp and ms_instancia.tipo = 'C' " +
                                "where id_actividad = @id_actividad "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                query = query + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by ms_comp.id_ms_comp "
                        .gvRegistrosC.Columns(4).HeaderText = "Comprobó"
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                If Val(._txtIdAct.Text) = 9 Or Val(._txtIdAct.Text) = 12 Or Val(._txtIdAct.Text) = 62 Or Val(._txtIdAct.Text) = 115 Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                End If
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosC.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosC.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosC.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridNS()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = True
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosNS.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosNS.Columns(0).Visible = True
                .gvRegistrosNS.Columns(7).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 88
                        .gvRegistrosNS.Columns(8).HeaderText = "Fecha de Solicitud"
                        'Ingresar Cotizaciones
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_negoc_servicio.id_ms_negoc_servicio " +
                                "     , ms_negoc_servicio.empresa " +
                                "     , ms_negoc_servicio.empleado_solicita " +
                                "     , isnull(ms_negoc_servicio.division, ms_negoc_servicio.centro_costo) as cc_div " +
                                "     , ms_negoc_servicio.servicio " +
                                "     , null as proveedor " +
                                "     , ms_negoc_servicio.fecha_solicita as fecha " +
                                "from ms_instancia " +
                                "  left join ms_negoc_servicio on ms_instancia.id_ms_sol = ms_negoc_servicio.id_ms_negoc_servicio and ms_instancia.tipo = 'NS' " +
                                "where id_actividad = @id_actividad " +
                                "order by ms_negoc_servicio.id_ms_negoc_servicio "
                    Case 89
                        .gvRegistrosNS.Columns(8).HeaderText = "Fecha de Cotizaciones"
                        'Autorizar Cotización
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_negoc_servicio.id_ms_negoc_servicio " +
                                "     , ms_negoc_servicio.empresa " +
                                "     , ms_negoc_servicio.empleado_solicita " +
                                "     , isnull(ms_negoc_servicio.division, ms_negoc_servicio.centro_costo) as cc_div " +
                                "     , ms_negoc_servicio.servicio " +
                                "     , cg_proveedor.nombre as proveedor " +
                                "     , ms_negoc_servicio.fecha_cotiza as fecha " +
                                "from ms_instancia " +
                                "  left join ms_negoc_servicio on ms_instancia.id_ms_sol = ms_negoc_servicio.id_ms_negoc_servicio and ms_instancia.tipo = 'NS' " +
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_negoc_servicio.proveedor_selec = cg_proveedor.id_proveedor " +
                                "where id_actividad = @id_actividad " +
                                "order by ms_negoc_servicio.id_ms_negoc_servicio "
                    Case 90
                        .gvRegistrosNS.Columns(8).HeaderText = "Fecha de Aut. Cotizaciones"
                        'Autorizar Solicitud de Negociación
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_negoc_servicio.id_ms_negoc_servicio " +
                                "     , ms_negoc_servicio.empresa " +
                                "     , ms_negoc_servicio.empleado_solicita " +
                                "     , isnull(ms_negoc_servicio.division, ms_negoc_servicio.centro_costo) as cc_div " +
                                "     , ms_negoc_servicio.servicio " +
                                "     , cg_proveedor.nombre as proveedor " +
                                "     , ms_negoc_servicio.fecha_aut_cotiza as fecha " +
                                "from ms_instancia " +
                                "  left join ms_negoc_servicio on ms_instancia.id_ms_sol = ms_negoc_servicio.id_ms_negoc_servicio and ms_instancia.tipo = 'NS' " +
                                "  left join bd_SiSAC.dbo.cg_proveedor on ms_negoc_servicio.proveedor_selec = cg_proveedor.id_proveedor " +
                                "where id_actividad = @id_actividad " +
                                "  and ms_negoc_servicio.id_usr_aut_negoc = @id_usuario " +
                                "order by ms_negoc_servicio.id_ms_negoc_servicio "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                If Val(._txtIdAct.Text) = 90 Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                End If
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosNS.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosNS.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosNS.Columns(0).Visible = False
                If Val(._txtIdAct.Text) = 88 Then
                    .gvRegistrosNS.Columns(7).Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridSN()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = True
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosF.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosF.Columns(0).Visible = True
                .gvRegistrosF.Columns(7).HeaderText = "Fecha Emisión"
                .gvRegistrosF.Columns(6).Visible = True
                .gvRegistrosF.Columns(7).Visible = True
                .gvRegistrosF.Columns(8).Visible = True
                .gvRegistrosF.Columns(9).Visible = True
                .gvRegistrosF.Columns(10).Visible = True
                .gvRegistrosF.Columns(11).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 94
                        'Solicitud por Validar 1 
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_valida = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 102
                        'Solicitud por Validar 2
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_valida2 = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 96
                        'Autorizar Solicitud
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad "
                        If ._txtPerfil.Text = "AdmonDCM" Then
                            query = query + "  and empresa in ('COPE', 'DICOMEX', 'DIBIESE') "
                        Else
                            query = query + "  and empresa not in ('COPE', 'DICOMEX', 'DIBIESE') "
                        End If
                        query = query + "order by ms_factura.id_ms_factura "
                    Case 98
                        'Validar Presupuesto
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad "
                        query = query + "order by ms_factura.id_ms_factura "
                    Case 99, 101, 104
                        'Solicitar Ampliación de Presupuesto / Ingresar Factura / Corregir Factura
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_solicita = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                If Val(._txtIdAct.Text) = 94 Or Val(._txtIdAct.Text) = 102 Or Val(._txtIdAct.Text) = 96 Or Val(._txtIdAct.Text) = 101 Or Val(._txtIdAct.Text) = 104 Or Val(._txtIdAct.Text) = 99 Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                End If
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosF.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosF.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosF.Columns(0).Visible = False
                If Val(._txtIdAct.Text) = 94 Or Val(._txtIdAct.Text) = 102 Or Val(._txtIdAct.Text) = 96 Or Val(._txtIdAct.Text) = 98 Or Val(._txtIdAct.Text) = 99 Or Val(._txtIdAct.Text) = 101 Then
                    .gvRegistrosF.Columns(6).Visible = False
                    .gvRegistrosF.Columns(7).Visible = False
                    .gvRegistrosF.Columns(8).Visible = False
                Else
                    .gvRegistrosF.Columns(9).Visible = False
                    .gvRegistrosF.Columns(10).Visible = False
                    .gvRegistrosF.Columns(11).Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridF()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = True
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosF.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosF.Columns(0).Visible = True
                .gvRegistrosF.Columns(7).HeaderText = "Fecha Emisión"
                .gvRegistrosF.Columns(6).Visible = True
                .gvRegistrosF.Columns(7).Visible = True
                .gvRegistrosF.Columns(8).Visible = True
                .gvRegistrosF.Columns(9).Visible = True
                .gvRegistrosF.Columns(10).Visible = True
                .gvRegistrosF.Columns(11).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 14
                        'Facturas por Autorizar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_autoriza = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 15
                        .gvRegistrosF.Columns(7).HeaderText = "Fecha de Autorización"

                        .pnlFiltroF.Visible = True

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        Dim queryE As String = ""

                        queryE = "select 0 as id_empresa " +
                                 "     , '' as nombre " +
                                 "union " +
                                 "select id_empresa, nombre " +
                                 "from bd_empleado.dbo.cg_empresa empresa " +
                                 "where status = 'A' " +
                                 "  and nombre in (select distinct(empresa) " +
                                 "                 from ms_instancia " +
                                 "                   left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                 "                 where id_actividad = @id_actividad) "
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                queryE = queryE + "  and nombre = 'PAME' "
                            Case "CoDCM", "AdmonDCM"
                                queryE = queryE + "  and nombre in ('DICOMEX', 'COPE', 'DIBIESE', 'APYRHSA') "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        queryE = queryE + "order by nombre "

                        sdaEmpresa.SelectCommand = New SqlCommand(queryE, ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                        .ddlEmpresaF.DataSource = dsEmpresa
                        .ddlEmpresaF.DataTextField = "nombre"
                        .ddlEmpresaF.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresaF.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()

                        'Asignar Cuenta
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , case when ms_factura.fecha_autoriza3 is null then case when ms_factura.fecha_autoriza2 is null then ms_factura.fecha_autoriza else ms_factura.fecha_autoriza2 end else ms_factura.fecha_autoriza3 end as fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM", "AdmonDCM"
                                query = query + "  and empresa in ('DICOMEX', 'COPE', 'DIBIESE', 'APYRHSA') "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        'query = query + "order by ms_factura.fecha_autoriza "
                        query = query + "order by case when ms_factura.fecha_autoriza3 is null then case when ms_factura.fecha_autoriza2 is null then ms_factura.fecha_autoriza else ms_factura.fecha_autoriza2 end else ms_factura.fecha_autoriza3 end "
                    Case 17, 49, 51, 83, 85
                        'Corregir Factura
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_solicita = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 44
                        'Solicitud por Validar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_valida = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 46
                        'Ingresar Cotizaciones
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "order by ms_factura.id_ms_factura "
                    Case 47
                        'Autorizar Solicitud
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad "
                        If ._txtPerfil.Text = "AdmonDCM" Then
                            query = query + "  and empresa in ('COPE', 'DICOMEX', 'DIBIESE') "
                        Else
                            query = query + "  and empresa not in ('COPE', 'DICOMEX', 'DIBIESE') "
                        End If
                        query = query + "order by ms_factura.id_ms_factura "
                    Case 84
                        'Validar Presupuesto
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad "
                        query = query + "order by ms_factura.id_ms_factura "
                    Case 50
                        'Autorizar Facturas 1ra, 2da y 3ra
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , empresa " +
                                "     , empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , razon_emisor " +
                                "     , fecha_emision " +
                                "     , importe " +
                                "     , isnull(tipo_servicio, servicio) as tipo_servicio " +
                                "     , fecha_solicita " +
                                "     , case contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad in (50, 52, 53) " +
                                "  and ((id_usr_autoriza = @id_usuario and fecha_autoriza is null) " +
                                "    or (id_usr_autoriza2 = @id_usuario and fecha_autoriza2 is null and fecha_autoriza is not null) " +
                                "    or (id_usr_autoriza3 = @id_usuario and fecha_autoriza3 is null and fecha_autoriza is not null and fecha_autoriza2 is not null)) " +
                                "order by ms_factura.id_ms_factura "
                    Case 54
                        'Complementar Datos de Contrato
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , ms_factura.empresa " +
                                "     , ms_factura.empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , dt_factura.razon_emisor " +
                                "     , dt_factura.fecha_emision " +
                                "     , dt_factura.importe " +
                                "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                                "     , ms_factura.fecha_solicita " +
                                "     , case ms_factura.contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "where id_actividad = @id_actividad " +
                                "  and id_usr_solicita = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 55
                        .gvRegistrosF.Columns(7).HeaderText = "Fecha de Inicio"

                        'Autorizar Solicitud de Contrato
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , ms_factura.empresa " +
                                "     , ms_factura.empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , ms_contrato.proveedor as razon_emisor " +
                                "     , convert(varchar, ms_contrato.fecha_servicio_ini, 103) as fecha_emision " +
                                "     , ms_contrato.monto_contrato as importe " +
                                "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                                "     , ms_factura.fecha_solicita " +
                                "     , case ms_factura.contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " +
                                "where id_actividad = @id_actividad " +
                                "  and ms_contrato.id_usr_autoriza = @id_usuario " +
                                "order by ms_factura.id_ms_factura "
                    Case 57
                        .gvRegistrosF.Columns(7).HeaderText = "Fecha de Autorización"

                        .pnlFiltroF.Visible = True

                        'Lista de Empresas
                        Dim sdaEmpresa As New SqlDataAdapter
                        Dim dsEmpresa As New DataSet
                        Dim queryE As String = ""

                        queryE = "select 0 as id_empresa " +
                                 "     , '' as nombre " +
                                 "union " +
                                 "select id_empresa, nombre " +
                                 "from bd_empleado.dbo.cg_empresa Empresa " +
                                 "where status = 'A' " +
                                 "  and nombre in (select distinct(empresa) " +
                                 "                 from ms_instancia " +
                                 "                   left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                 "                 where id_actividad = @id_actividad) "
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                queryE = queryE + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                queryE = queryE + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        queryE = queryE + "order by nombre "

                        sdaEmpresa.SelectCommand = New SqlCommand(queryE, ConexionBD)
                        sdaEmpresa.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                        .ddlEmpresaF.DataSource = dsEmpresa
                        .ddlEmpresaF.DataTextField = "nombre"
                        .ddlEmpresaF.DataValueField = "id_empresa"
                        ConexionBD.Open()
                        sdaEmpresa.Fill(dsEmpresa)
                        .ddlEmpresaF.DataBind()
                        ConexionBD.Close()
                        sdaEmpresa.Dispose()
                        dsEmpresa.Dispose()

                        'Asignar Cuenta para el Contrato
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , ms_factura.empresa " +
                                "     , ms_factura.empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , ms_contrato.proveedor as razon_emisor " +
                                "     , ms_contrato.fecha_autoriza as fecha_emision " +
                                "     , ms_contrato.monto_contrato as importe " +
                                "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                                "     , ms_factura.fecha_solicita " +
                                "     , case ms_factura.contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " +
                                "where id_actividad = @id_actividad "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                query = query + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by ms_contrato.fecha_autoriza desc "
                    Case 58
                        .gvRegistrosF.Columns(7).HeaderText = "Fecha de Inicio"

                        'Registrar Contrato en NAV
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , ms_factura.empresa " +
                                "     , ms_factura.empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , ms_contrato.proveedor as razon_emisor " +
                                "     , convert(varchar, ms_contrato.fecha_servicio_ini, 103) as fecha_emision " +
                                "     , ms_contrato.monto_contrato as importe " +
                                "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                                "     , ms_factura.fecha_solicita " +
                                "     , case ms_factura.contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " +
                                "where id_actividad = @id_actividad " +
                                "order by ms_factura.id_ms_factura "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                If Val(._txtIdAct.Text) = 14 Or Val(._txtIdAct.Text) = 17 Or Val(._txtIdAct.Text) = 44 Or Val(._txtIdAct.Text) = 47 Or Val(._txtIdAct.Text) = 49 Or Val(._txtIdAct.Text) = 50 Or Val(._txtIdAct.Text) = 51 Or Val(._txtIdAct.Text) = 54 Or Val(._txtIdAct.Text) = 55 Or Val(._txtIdAct.Text) = 83 Or Val(._txtIdAct.Text) = 85 Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                End If
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosF.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosF.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosF.Columns(0).Visible = False
                If Val(._txtIdAct.Text) = 44 Or Val(._txtIdAct.Text) = 46 Or Val(._txtIdAct.Text) = 47 Or Val(._txtIdAct.Text) = 83 Or Val(._txtIdAct.Text) = 84 Or Val(._txtIdAct.Text) = 85 Or Val(._txtIdAct.Text) = 49 Then
                    .gvRegistrosF.Columns(6).Visible = False
                    .gvRegistrosF.Columns(7).Visible = False
                    .gvRegistrosF.Columns(8).Visible = False
                Else
                    .gvRegistrosF.Columns(9).Visible = False
                    .gvRegistrosF.Columns(10).Visible = False
                    .gvRegistrosF.Columns(11).Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridSAP()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Ampliaciones por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = True
                .gvRegistrosV.Visible = True
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosSAP.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosSAP.Columns(0).Visible = True
                Dim query As String = ""
                'Select Case Val(._txtIdAct.Text)
                '    Case 106
                '        'Solicitudes por Autorizar
                '        query = "select id_ms_instancia " +
                '                "     , id_ms_ampliacion_p " +
                '                "     , empresa " +
                '                "     , solicita " +
                '                "     , centro_costo " +
                '                "     , (select sum(monto_solicita) from dt_ampliacion_p where ms_ampliacion_p.id_ms_ampliacion_p = dt_ampliacion_p.id_ms_ampliacion_p) as monto_solicita " +
                '                "     , fecha_solicita " +
                '                "from ms_instancia " +
                '                "  left join ms_ampliacion_p on ms_instancia.id_ms_sol = ms_ampliacion_p.id_ms_ampliacion_p and tipo = 'SAP' " +
                '                "where id_actividad = @idActividad " +
                '                "  and id_usr_autoriza = @idUsuario "
                'End Select
                If ._txtIdAct.Text = "117" Then
                    query = "select id_ms_instancia " +
                               "     , id_ms_ampliacion_p " +
                               "     , empresa " +
                               "     , solicita " +
                               "     , centro_costo " +
                               "     , (select sum(monto_solicita) from dt_ampliacion_p where ms_ampliacion_p.id_ms_ampliacion_p = dt_ampliacion_p.id_ms_ampliacion_p) as monto_solicita " +
                               "     , fecha_solicita " +
                               " from ms_instancia " +
                               " left join ms_ampliacion_p on ms_instancia.id_ms_sol = ms_ampliacion_p.id_ms_ampliacion_p and tipo = 'SAP' " +
                               " where id_actividad = 117  "


                ElseIf ._txtIdAct.Text = "106" Then
                    query = "select id_ms_instancia " +
                                "     , id_ms_ampliacion_p " +
                                "     , empresa " +
                                "     , solicita " +
                                "     , centro_costo " +
                                "     , (select sum(monto_solicita_val) from dt_ampliacion_p where ms_ampliacion_p.id_ms_ampliacion_p = dt_ampliacion_p.id_ms_ampliacion_p and dt_ampliacion_p.impacto_pres_monto_val > 0) as monto_solicita " +
                                "     , fecha_solicita " +
                                " from ms_instancia " +
                                " left join ms_ampliacion_p on ms_instancia.id_ms_sol = ms_ampliacion_p.id_ms_ampliacion_p and tipo = 'SAP' " +
                                " where id_actividad = 106 and id_usr_autoriza = @idUsuario "

                End If
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                'sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idActividad", Val(._txtIdAct.Text))
                If ._txtIdAct.Text = "106" Then
                    sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                End If
                'sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idActividad", Val(._txtIdAct.Text))
                'sdaCatalogo.SelectCommand.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosSAP.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosSAP.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosSAP.Columns(0).Visible = False


                If .gvRegistrosSAP.Rows.Count() > 0 Then
                    .gvRegistrosSAP.Visible = True
                Else
                    .gvRegistrosSAP.Visible = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridV()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Rervas por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = True
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosV.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosV.Columns(0).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 19
                        'Solicitudes por Autorizar
                        query = "select id_ms_reserva " +
                                "     , empleado_nom + ' ' + empleado_appat as solicito " +
                                "	  , fecha_ini, fecha_fin " +
                                "	  , prioridad " +
                                "	  , no_eco, placas " +
                                "	  , destino " +
                                "from ms_reserva " +
                                "where status = 'P' " +
                                "  and id_usr_autorizo = @id_usuario " +
                                "  and id_ms_reserva not in (select id_ms_reserva " +
                                "                            from ms_recursos) " +
                                "order by id_ms_reserva "
                    Case 21
                        'Entregar Vehículo al Usuario
                        query = "select id_ms_reserva " +
                                "     , empleado_nom + ' ' + empleado_appat as solicito " +
                                "     , fecha_ini, fecha_fin " +
                                "     , prioridad " +
                                "     , no_eco, placas " +
                                "     , destino " +
                                "from ms_reserva " +
                                "where status = 'A' " +
                                "order by id_ms_reserva "
                    Case 22
                        'Recibir Vehículo del Usuario
                        query = "select id_ms_reserva " +
                                "     , empleado_nom + ' ' + empleado_appat as solicito " +
                                "     , fecha_ini, fecha_fin " +
                                "     , prioridad " +
                                "     , no_eco, placas " +
                                "     , destino " +
                                "from ms_reserva " +
                                "where status = 'T' " +
                                "order by id_ms_reserva "
                    Case 24
                        'Cambiar Vehículo
                        query = "select id_ms_reserva " +
                                "     , empleado_nom + ' ' + empleado_appat as solicito " +
                                "     , fecha_ini, fecha_fin " +
                                "     , prioridad " +
                                "     , no_eco, placas " +
                                "     , destino " +
                                "from ms_reserva " +
                                "where status not in ('Z', 'ZM', 'R') and fecha_ini < getdate() and fecha_fin > getdate() " +
                                "order by id_ms_reserva "
                    Case 25
                        'Cancelar Reservación
                        query = "select id_ms_reserva " +
                                "     , empleado_nom + ' ' + empleado_appat as solicito " +
                                "     , fecha_ini, fecha_fin " +
                                "     , prioridad " +
                                "     , no_eco, placas " +
                                "     , destino " +
                                "from ms_reserva " +
                                "where status not in ('Z', 'ZM', 'R') and fecha_ini < getdate() and fecha_fin > getdate() " +
                                "order by id_ms_reserva "
                    Case 26
                        'Cambio de Fecha de Regreso
                        query = "select id_ms_reserva " +
                                "     , empleado_nom + ' ' + empleado_appat as solicito " +
                                "     , fecha_ini, fecha_fin " +
                                "     , prioridad " +
                                "     , no_eco, placas " +
                                "     , destino " +
                                "from ms_reserva " +
                                "where status = 'T' " +
                                "order by id_ms_reserva "
                End Select

                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosV.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosV.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosV.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridG()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = True
                .gvRegistrosDG.Visible = False

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosG.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosG.Columns(0).Visible = True
                .gvRegistrosG.Columns(2).Visible = True
                .gvRegistrosG.Columns(3).Visible = True
                .gvRegistrosG.Columns(4).Visible = True
                .gvRegistrosG.Columns(5).Visible = True
                .gvRegistrosG.Columns(6).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 33
                        'Cargas por Comprobar
                        query = "select id_dt_carga_comb " +
                                "     , dt_carga_comb.fecha " +
                                "     , dt_carga_comb.identificador_vehiculo as unidad " +
                                "     , dt_carga_comb.placa " +
                                "     , dt_carga_comb.num_tarjeta " +
                                "     , dt_carga_comb.no_comprobante " +
                                "     , dt_carga_comb.razon_social_afiliado " +
                                "     , dt_carga_comb.cantidad_mercancia " +
                                "     , dt_carga_comb.importe_transaccion " +
                                "from dt_carga_comb " +
                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on dt_carga_comb.id_conductor = cgEmpl.id_conductor_edenred and cgEmpl.status = 'A' " +
                                "  left join cg_usuario on cgEmpl.id_empleado = cg_usuario.id_empleado and cg_usuario.status = 'A' " +
                                "where cg_usuario.id_usuario = @id_usuario " +
                                "  and dt_carga_comb.status = 'P' " +
                                "order by dt_carga_comb.fecha "
                    Case 35
                        'Cargas con Tarjeta Bancaria por Comprobar
                        query = "select id_dt_carga_comb_tar as id_dt_carga_comb " +
                                "     , dt_carga_comb_tar.fecha " +
                                "     , dt_carga_comb_tar.identificador_vehiculo as unidad " +
                                "     , dt_carga_comb_tar.placa " +
                                "     , dt_carga_comb_tar.num_tarjeta " +
                                "     , dt_carga_comb_tar.no_comprobante " +
                                "     , dt_carga_comb_tar.razon_social_afiliado " +
                                "     , dt_carga_comb_tar.cantidad_mercancia " +
                                "     , dt_carga_comb_tar.importe_transaccion " +
                                "from dt_carga_comb_tar " +
                                "  left join cg_usuario on dt_carga_comb_tar.id_usr_carga = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                "where cg_usuario.id_usuario = @id_usuario " +
                                "  and dt_carga_comb_tar.status = 'P' " +
                                "order by dt_carga_comb_tar.fecha "
                    Case 109
                        'Cargas por Comprobar TOKA
                        query = "select distinct(id_dt_carga_comb_toka) as id_dt_carga_comb " +
                                "     , dt_carga_comb_toka.fecha " +
                                "     , dt_carga_comb_toka.identificador_vehiculo as unidad " +
                                "     , dt_carga_comb_toka.placa " +
                                "     , dt_carga_comb_toka.num_tarjeta " +
                                "     , dt_carga_comb_toka.no_comprobante " +
                                "     , dt_carga_comb_toka.razon_social_afiliado " +
                                "     , dt_carga_comb_toka.cantidad_mercancia " +
                                "     , dt_carga_comb_toka.importe_transaccion " +
                                "from dt_carga_comb_toka " +
                                "  left join ms_comb on dt_carga_comb_toka.identificador_vehiculo = ms_comb.no_eco and (periodo_ini<= dt_carga_comb_toka.fecha and dateadd(hour, 23, dateadd(minute, 59, cast(periodo_fin as datetime))) >= dt_carga_comb_toka.fecha) and ms_comb.status not in ('Z', 'ZC') " +
                                "  left join cg_usuario on ms_comb.id_usr_solicita = cg_usuario.id_usuario and cg_usuario.status = 'A' " +
                                "  left join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado and cgEmpl.status = 'A' " +
                                "where cg_usuario.id_usuario = @id_usuario " +
                                "  and dt_carga_comb_toka.status = 'P' " +
                                "order by dt_carga_comb_toka.fecha "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosG.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosG.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosG.Columns(0).Visible = False

                If Val(._txtIdAct.Text) = 35 Then
                    .imgTrans.Visible = True
                    .gvRegistrosG.Columns(2).Visible = False
                    .gvRegistrosG.Columns(3).Visible = False
                    .gvRegistrosG.Columns(4).Visible = False
                    .gvRegistrosG.Columns(5).Visible = False
                    .gvRegistrosG.Columns(6).Visible = False
                    .gvRegistrosG.Width = 460
                Else
                    .imgTrans.Visible = False
                    .gvRegistrosG.Width = 900
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Sub llenarGridDG()
        With Me
            Try
                .litError.Text = ""
                'Presentar Tabla de Anticipos por procesar
                .imgMenu.Visible = False
                .imgTrans.Visible = False
                .gvRegistrosReun.Visible = False
                .gvRegistrosEval.Visible = False
                .gvRegistrosSR.Visible = False
                .pnlFiltroA.Visible = False
                .gvRegistrosA.Visible = False
                .gvRegistrosC.Visible = False
                .gvRegistrosNS.Visible = False
                .pnlFiltroF.Visible = False
                .gvRegistrosF.Visible = False
                .gvRegistrosSAP.Visible = False
                .gvRegistrosV.Visible = False
                .gvRegistrosG.Visible = False
                .gvRegistrosDG.Visible = True

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosDG.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosDG.Columns(0).Visible = True
                Dim query As String = ""
                Select Case Val(._txtIdAct.Text)
                    Case 40
                        'Cargas por Dispersar
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_comb.id_ms_comb " +
                                "     , empleado as solicito " +
                                "     , periodo_ini as desde " +
                                "     , periodo_fin as hasta " +
                                "     , no_eco " +
                                "     , format(ms_comb.litros_comb, 'N0', 'es-MX') as litros_comb " +
                                "     , format(ms_comb.importe_comb, 'C2', 'es-MX') as importe_comb " +
                                "     , fecha_autoriza " +
                                "from ms_instancia " +
                                "  left join ms_comb on ms_instancia.id_ms_sol = ms_comb.id_ms_comb and ms_instancia.tipo = 'Comb' " +
                                "where id_actividad = @id_actividad " +
                                "order by ms_comb.id_ms_comb "
                End Select
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosDG.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosDG.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosDG.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Public Function valNoProv()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                         "from cg_usuario " +
                                         "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                         "  inner join bd_Empleado.dbo.dt_empleado_prov dtNoProv on cgEmpl.id_empleado = dtNoProv.id_empleado " +
                                         "where id_usuario = @idUsuario " +
                                         "  and cg_usuario.status = 'A' " +
                                         "  and cgEmpl.status = 'A' " +
                                         "  and dtNoProv.status = 'A' "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If conteo > 0 Then
                    valNoProv = True
                Else
                    valNoProv = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                valNoProv = False
            End Try
        End With
    End Function

    Public Function valLic()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                         "from cg_usuario " +
                                         "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                         "where id_usuario = @idUsuario " +
                                         "  and cg_usuario.status = 'A' " +
                                         "  and cgEmpl.vig_licencia > getdate() "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If conteo > 0 Then
                    valLic = True
                Else
                    valLic = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                valLic = False
            End Try
        End With
    End Function

    Public Function valCurso()
        With Me
            Try
                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                SCMValores.Connection = ConexionBD
                Dim conteo As Integer = 0
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) " +
                                        "from cg_usuario " +
                                        "  inner join bd_Empleado.dbo.cg_empleado cgEmpl on cg_usuario.id_empleado = cgEmpl.id_empleado " +
                                        "where id_usuario = @idUsuario " +
                                        "  and cg_usuario.status = 'A' " +
                                        "  and (cgEmpl.curso_prog = 'N' or cgEmpl.fecha_prog > cast(getdate() as date) or cgEmpl.curso_aprob = 'S') "
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                conteo = SCMValores.ExecuteScalar()
                ConexionBD.Close()
                If conteo > 0 Then
                    valCurso = True
                Else
                    valCurso = False
                End If
            Catch ex As Exception
                .litError.Text = ex.ToString
                valCurso = False
            End Try
        End With
    End Function

    Public Sub enConstruccion()
        Me.imgMenu.ImageUrl = "images\enConst.png"
        Me.imgMenu.Visible = True
        Me.imgMenu.Width = 500
        Me.pnlFiltroA.Visible = False
        Me.gvRegistrosA.Visible = False
        Me.pnlFiltroF.Visible = False
        Me.gvRegistrosF.Visible = False
        Me.gvRegistrosSAP.Visible = False
        Me.gvRegistrosV.Visible = False
    End Sub

#End Region

#Region "Menú"

    'Catálogos
    Protected Sub btnCatUsuario_Click(sender As Object, e As EventArgs) Handles btnCatUsuario.Click
        envio("Catálogo de Usuarios", "CatUsuario.aspx")
        '' ''enConstruccion()
    End Sub

    Protected Sub btnCatUsuarioAC_Click(sender As Object, e As EventArgs) Handles btnCatUsuarioAC.Click
        envio("Catálogo de Usuarios", "CatUsuarioAC.aspx")
    End Sub

    Protected Sub btnCatCategoria_Click(sender As Object, e As EventArgs) Handles btnCatCategoria.Click
        envio("Catálogo de Categorías", "CatCConcepto.aspx")
    End Sub

    Protected Sub btnCatConceptoSF_Click(sender As Object, e As EventArgs) Handles btnCatConceptoSF.Click
        envio("Catálogo de Conceptos Sin Factura", "CatConceptoSF.aspx")
    End Sub

    Protected Sub btnCatConceptoCF_Click(sender As Object, e As EventArgs) Handles btnCatConceptoCF.Click
        envio("Catálogo de Conceptos Con Factura", "CatConceptoCF.aspx")
    End Sub

    Protected Sub btnCatServicio_Click(sender As Object, e As EventArgs) Handles btnCatServicio.Click
        envio("Catálogo de Servicios", "CatServicio.aspx")
    End Sub

    Protected Sub btnCatOrigenDest_Click(sender As Object, e As EventArgs) Handles btnCatLugar.Click
        envio("Catálogo de Lugares", "CatLugar.aspx")
    End Sub

    Protected Sub btnCatConsAut_Click(sender As Object, e As EventArgs) Handles btnCatConsAut.Click
        envio("Consulta de Autorizadores", "ConsAutorizador.aspx")
    End Sub

    Protected Sub btnCatListaNegra_Click(sender As Object, e As EventArgs) Handles btnCatListaNegra.Click
        envio("Catálogo Lista Negra", "CatListaNegra.aspx")
    End Sub
    Protected Sub btnConsServ_Click(sender As Object, e As EventArgs) Handles btnConsServ.Click
        envio("Consulta Conceptos y Servicios", "ConsConceptos.aspx")
    End Sub
    'Solicitud de Recursos
    Protected Sub btnSolRec_Click(sender As Object, e As EventArgs) Handles btnSolRec.Click
        envio("Solicitar Recursos", "37.aspx")
    End Sub

    Protected Sub btnAutSolRec_Click(sender As Object, e As EventArgs) Handles btnAutSolRec.Click
        Me._txtIdAct.Text = 38
        llenarGridSR()
    End Sub
    Protected Sub btnSRAutDir_Click(sender As Object, e As EventArgs) Handles btnSRAutDir.Click
        Me._txtIdAct.Text = 116
        llenarGridSR()
    End Sub

    Protected Sub btnCatTipoHospedaje_Click(sender As Object, e As EventArgs) Handles btnCatTipoHospedaje.Click
        envio("Catálogo de Tipos de Hospedaje", "CatTHospedaje.aspx")
    End Sub

    Protected Sub btnVoBoSolRec_Click(sender As Object, e As EventArgs) Handles btnVoBoSolRec.Click
        Me._txtIdAct.Text = 59
        llenarGridSR()
    End Sub

    Protected Sub btnConsReservCH_Click(sender As Object, e As EventArgs) Handles btnConsReservCH.Click
        envio("Consulta de Hospedaje en Casa de Huéspedes", "ConsReservCH.aspx")
    End Sub

    Protected Sub btnConsSR_Click(sender As Object, e As EventArgs) Handles btnConsSR.Click
        envio("Consulta de Solicitudes de Recursos", "ConsSolRec.aspx")
    End Sub

    Protected Sub btnConsSRAV_Click(sender As Object, e As EventArgs) Handles btnConsSRAV.Click
        envio("Consulta de Solicitudes de Recursos Detalle", "ConsSolRecDet.aspx")
    End Sub

    'Anticipos
    Protected Sub btnGenTransf_Click(sender As Object, e As EventArgs) Handles btnGenTransf.Click
        Me._txtIdAct.Text = 3
        llenarGridA()
    End Sub

    Protected Sub ddlEmpresaA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresaA.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosA.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosA.Columns(0).Visible = True
                Dim query As String = ""
                'Transferencias por Gestionar
                query = "select ms_instancia.id_ms_instancia " +
                        "     , ms_anticipo.id_ms_anticipo " +
                        "     , empresa " +
                        "     , empleado as solicito " +
                        "	  , periodo_ini, periodo_fin " +
                        "	  , destino " +
                        "	  , monto_hospedaje + monto_alimentos + monto_casetas + monto_otros as importe " +
                        "     , fecha_autoriza " +
                        "from ms_instancia " +
                        "  left join ms_anticipo on ms_instancia.id_ms_sol = ms_anticipo.id_ms_anticipo and ms_instancia.tipo = 'A' " +
                        "where id_actividad = @id_actividad " +
                        "  and empresa like '%' + @empresa + '%' "
                'Restricción por Empresas
                Select Case ._txtPerfil.Text
                    Case "CoPame"
                        query = query + "  and empresa = 'PAME' "
                    Case "CoDCM"
                        query = query + "  and empresa = 'DICOMEX' "
                        ''Case Else
                        ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                End Select
                query = query + "order by ms_anticipo.id_ms_anticipo "
                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresaA.SelectedItem.Text)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosA.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosA.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosA.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnEntEfectA_Click(sender As Object, e As EventArgs) Handles btnEntEfectA.Click
        Me._txtIdAct.Text = 5
        llenarGridA()
    End Sub

    Protected Sub btnConsAnt_Click(sender As Object, e As EventArgs) Handles btnConsAnt.Click
        envio("Consulta de Anticipos", "ConsAnt.aspx")
    End Sub

    Protected Sub btnConsAntCaja_Click(sender As Object, e As EventArgs) Handles btnConsAntCaja.Click
        envio("Consulta Cuadre de Anticipos", "ConsAntCaja.aspx")
    End Sub

    Protected Sub btnConsAntAud_Click(sender As Object, e As EventArgs) Handles btnConsAntAud.Click
        envio("Consulta de Anticipos Auditoria", "ContAntAud.aspx")
    End Sub

    'Comprobaciones
    Protected Sub btnGenCompExt_Click(sender As Object, e As EventArgs) Handles btnGenCompExt.Click
        envio("Generar Comprobación Extemporánea", "61.aspx")
    End Sub

    Protected Sub btnGenComp_Click(sender As Object, e As EventArgs) Handles btnGenComp.Click
        With Me
            Dim sin_comprobacion As String
            Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
            ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
            Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
            SCMValores.Connection = ConexionBD
            Dim cont As Integer

            SCMValores.CommandText = ""
            SCMValores.Parameters.Clear()
            SCMValores.CommandText = "SELECT anticipo_obl, edit_compro_datos  FROM cg_usuario WHERE id_usuario =@id_usuario"
            SCMValores.Parameters.AddWithValue("@id_usuario", Val(._txtIdUsuario.Text))
            ConexionBD.Open()
            sin_comprobacion = SCMValores.ExecuteScalar
            ConexionBD.Close()

            If sin_comprobacion = "S" Then
                envio("Generar Comprobación", "08.aspx")
            Else
                SCMValores.CommandText = ""
                SCMValores.Parameters.Clear()
                SCMValores.CommandText = "select count(*) from ms_anticipo" +
                                         "               where id_usr_solicita = @idUsuario " +
                                         "               and status in ('P', 'A', 'TR', 'EE', 'TRCP', 'EECP', 'TRCA', 'EECA') " +
                                         "               and tipo = 'A'"
                SCMValores.Parameters.AddWithValue("@idUsuario", Val(._txtIdUsuario.Text))
                ConexionBD.Open()
                cont = Val(SCMValores.ExecuteScalar)
                ConexionBD.Close()
                If cont >= 1 Then
                    envio("Generar Comprobación", "08.aspx")
                Else
                    .litError.Text = "No puede generar comprobaciones si no cuenta con un anticipo previo."
                End If
            End If
        End With
    End Sub

    Protected Sub btnCorrComp_Click(sender As Object, e As EventArgs) Handles btnCorrComp.Click
        Me._txtIdAct.Text = 12
        llenarGridC()
    End Sub

    Protected Sub btnAutCompExt_Click(sender As Object, e As EventArgs) Handles btnAutCompExt.Click
        Me._txtIdAct.Text = 62
        llenarGridC()
    End Sub

    Protected Sub btnPreAutorizacion_Click(sender As Object, e As EventArgs) Handles btnPreAutorizacion.Click
        Me._txtIdAct.Text = 115
        llenarGridC()
    End Sub

    Protected Sub btnAutComp_Click(sender As Object, e As EventArgs) Handles btnAutComp.Click
        Me._txtIdAct.Text = 9
        llenarGridC()
    End Sub

    Protected Sub btnValComp_Click(sender As Object, e As EventArgs) Handles btnValComp.Click
        Me._txtIdAct.Text = 10
        llenarGridC()
    End Sub

    Protected Sub btnEntEfectC_Click(sender As Object, e As EventArgs) Handles btnEntEfectC.Click
        Me._txtIdAct.Text = 31
        llenarGridC()
    End Sub

    Protected Sub btnConsComp_Click(sender As Object, e As EventArgs) Handles btnConsComp.Click
        envio("Consulta de Comprobaciones", "ConsComp.aspx")
    End Sub

    Protected Sub btnConsCompConta_Click(sender As Object, e As EventArgs) Handles btnConsCompConta.Click
        envio("Consulta de Comprobaciones Contabilidad", "ConsCompConta.aspx")
    End Sub

    Protected Sub btnConsCompT_Click(sender As Object, e As EventArgs) Handles btnConsCompT.Click
        envio("Consulta de Comprobaciones Detalle", "ConsCompT.aspx")
    End Sub

    Protected Sub btnConsCompSV_Click(sender As Object, e As EventArgs) Handles btnConsCompSV.Click
        envio("Consulta de Comprobaciones Seguimiento a Viajes", "ConsCompSV.aspx")
    End Sub

    Protected Sub btnConsCompExp_Click(sender As Object, e As EventArgs) Handles btnConsCompExp.Click
        envio("Consulta de Comprobaciones Expromat", "ConsCompExp.aspx")
    End Sub

    Protected Sub btnPlantComp_Click(sender As Object, e As EventArgs) Handles btnPlantComp.Click
        envio("Plantilla Comprobaciones", "ConsPlant.aspx")
    End Sub

    Protected Sub btnConsCompResum_Click(sender As Object, e As EventArgs) Handles btnConsCompResum.Click
        envio("Consulta de Comprobaciones Resumen", "ConsCompC.aspx")
    End Sub

    Protected Sub btnConsCompXConc_Click(sender As Object, e As EventArgs) Handles btnConsCompXConc.Click
        envio("Consulta de Comprobaciones por Concepto", "ConsCompXConcepto.aspx")
    End Sub

    'Facturas SAT
    Protected Sub btnCargaFact_Click(sender As Object, e As EventArgs) Handles btnCargaFact.Click
        envio("Carga de Facturas", "CargarArchivo.aspx")
    End Sub

    Protected Sub btnConsFactDet_Click(sender As Object, e As EventArgs) Handles btnConsFactDet.Click
        envio("Consulta de Facturas", "ConsFactDet.aspx")
    End Sub

    Protected Sub btnConsFactLiq_Click(sender As Object, e As EventArgs) Handles btnConsFactLiq.Click
        envio("Consultar Facturas", "ConsFactLiq.aspx")
    End Sub

    'Negociación de Servicios
    Protected Sub btnSolicitarNS_Click(sender As Object, e As EventArgs) Handles btnSolicitarNS.Click
        envio("Solicitar Negociación de Servicio", "87.aspx")
    End Sub

    Protected Sub btnIngresarCotNS_Click(sender As Object, e As EventArgs) Handles btnIngresarCotNS.Click
        Me._txtIdAct.Text = 88
        llenarGridNS()
    End Sub

    Protected Sub btnAutorizarCotNS_Click(sender As Object, e As EventArgs) Handles btnAutorizarCotNS.Click
        Me._txtIdAct.Text = 89
        llenarGridNS()
    End Sub

    Protected Sub btnAutorizarNegNS_Click(sender As Object, e As EventArgs) Handles btnAutorizarNegNS.Click
        Me._txtIdAct.Text = 90
        llenarGridNS()
    End Sub

    Protected Sub btnConsultaNeg_Click(sender As Object, e As EventArgs) Handles btnConsultaNeg.Click
        envio("Consulta de Negociaciones", "ConsNegoc.aspx")
    End Sub

    'Servicios Negociados
    Protected Sub btnSolicitarSN_Click(sender As Object, e As EventArgs) Handles btnSolicitarSN.Click
        envio("Solicitar Servicio Negociado", "93.aspx")
    End Sub

    Protected Sub btnValidar1SN_Click(sender As Object, e As EventArgs) Handles btnValidar1SN.Click
        Me._txtIdAct.Text = 94
        llenarGridSN()
    End Sub

    Protected Sub btnAutorizarSN_Click(sender As Object, e As EventArgs) Handles btnAutorizarSN.Click
        Me._txtIdAct.Text = 96
        llenarGridSN()
    End Sub

    Protected Sub btnValPresupSN_Click(sender As Object, e As EventArgs) Handles btnValPresupSN.Click
        Me._txtIdAct.Text = 98
        llenarGridSN()
    End Sub

    Protected Sub btnSolAmplPreSN_Click(sender As Object, e As EventArgs) Handles btnSolAmplPreSN.Click
        Me._txtIdAct.Text = 99
        llenarGridSN()
    End Sub

    Protected Sub btnIngresarFSN_Click(sender As Object, e As EventArgs) Handles btnIngresarFSN.Click
        Me._txtIdAct.Text = 101
        llenarGridSN()
    End Sub

    Protected Sub btnValidar2SN_Click(sender As Object, e As EventArgs) Handles btnValidar2SN.Click
        Me._txtIdAct.Text = 102
        llenarGridSN()
    End Sub

    Protected Sub btnCorregirFSN_Click(sender As Object, e As EventArgs) Handles btnCorregirFSN.Click
        Me._txtIdAct.Text = 104
        llenarGridSN()
    End Sub

    'Faturas de Gastos, Seguros y Asesorías
    Protected Sub btnSolFact_Click(sender As Object, e As EventArgs) Handles btnSolFact.Click
        envio("Ingresar Factura", "13.aspx")
    End Sub

    Protected Sub btnCorrFact_Click(sender As Object, e As EventArgs) Handles btnCorrFact.Click
        Me._txtIdAct.Text = 17
        llenarGridF()
    End Sub

    Protected Sub btnAutFact_Click(sender As Object, e As EventArgs) Handles btnAutFact.Click
        Me._txtIdAct.Text = 14
        llenarGridF()
    End Sub

    Protected Sub btnSolicitarGSA_Click(sender As Object, e As EventArgs) Handles btnSolicitarGSA.Click
        envio("Solicitar Gasto, Servicio o Asesoría", "43.aspx")
    End Sub

    Protected Sub btnValidarSol_Click(sender As Object, e As EventArgs) Handles btnValidarSol.Click
        Me._txtIdAct.Text = 44
        llenarGridF()
    End Sub

    Protected Sub btnIngresarCot_Click(sender As Object, e As EventArgs) Handles btnIngresarCot.Click
        Me._txtIdAct.Text = 46
        llenarGridF()
    End Sub

    Protected Sub btnCorregirSol_Click(sender As Object, e As EventArgs) Handles btnCorregirSol.Click
        Me._txtIdAct.Text = 83
        llenarGridF()
    End Sub

    Protected Sub btnAutorizarSol_Click(sender As Object, e As EventArgs) Handles btnAutorizarSol.Click
        Me._txtIdAct.Text = 47
        llenarGridF()
    End Sub

    Protected Sub btnValidarPresup_Click(sender As Object, e As EventArgs) Handles btnValidarPresup.Click
        Me._txtIdAct.Text = 84
        llenarGridF()
    End Sub

    Protected Sub btnSolAmplPresup_Click(sender As Object, e As EventArgs) Handles btnSolAmplPresup.Click
        Me._txtIdAct.Text = 85
        llenarGridF()
    End Sub

    Protected Sub btnConsFactExp_Click(sender As Object, e As EventArgs) Handles btnConsFactExp.Click
        envio("Consulta de Facturas Expromat", "ConsFactExp.aspx")
    End Sub

    'Contratos NAV
    Protected Sub btnCompContrato_Click(sender As Object, e As EventArgs) Handles btnCompContrato.Click
        Me._txtIdAct.Text = 54
        llenarGridF()
    End Sub

    Protected Sub btnAutContrato_Click(sender As Object, e As EventArgs) Handles btnAutContrato.Click
        Me._txtIdAct.Text = 55
        llenarGridF()
    End Sub

    Protected Sub btnAsigCContrato_Click(sender As Object, e As EventArgs) Handles btnAsigCContrato.Click
        Me._txtIdAct.Text = 57
        llenarGridF()
    End Sub

    Protected Sub btnRegContrato_Click(sender As Object, e As EventArgs) Handles btnRegContrato.Click
        Me._txtIdAct.Text = 58
        llenarGridF()
    End Sub
    'Contratos NAV

    Protected Sub btnIngresarFact_Click(sender As Object, e As EventArgs) Handles btnIngresarFact.Click
        Me._txtIdAct.Text = 49
        llenarGridF()
    End Sub

    Protected Sub btnAutorizarFact_Click(sender As Object, e As EventArgs) Handles btnAutorizarFact.Click
        Me._txtIdAct.Text = 50
        llenarGridF()
    End Sub

    Protected Sub btnCorrFactura_Click(sender As Object, e As EventArgs) Handles btnCorrFactura.Click
        Me._txtIdAct.Text = 51
        llenarGridF()
    End Sub

    Protected Sub btnAsigCuenta_Click(sender As Object, e As EventArgs) Handles btnAsigCuenta.Click
        Me._txtIdAct.Text = 15
        llenarGridF()
    End Sub

    Protected Sub ddlEmpresaF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmpresaF.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""

                Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                Dim sdaCatalogo As New SqlDataAdapter
                Dim dsCatalogo As New DataSet
                .gvRegistrosF.DataSource = dsCatalogo
                'Habilitar columnas para actualización
                .gvRegistrosF.Columns(0).Visible = True

                Dim query As String = ""

                Select Case Val(._txtIdAct.Text)
                    Case 57
                        'Asignar Cuenta para el Contrato
                        query = "select ms_instancia.id_ms_instancia " +
                                "     , ms_factura.id_ms_factura " +
                                "     , ms_factura.empresa " +
                                "     , ms_factura.empleado as solicito " +
                                "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                                "     , ms_contrato.proveedor as razon_emisor " +
                                "     , ms_contrato.fecha_autoriza as fecha_emision " +
                                "     , ms_contrato.monto_contrato as importe " +
                                "     , isnull(ms_factura.tipo_servicio, ms_factura.servicio) as tipo_servicio " +
                                "     , ms_factura.fecha_solicita " +
                                "     , case ms_factura.contrato_NAV_alta when 'S' then 'Sí' when 'N' then 'No' else '' end as alta_contrato " +
                                "from ms_instancia " +
                                "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                                "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                                "  left join ms_contrato on ms_factura.id_ms_factura = ms_contrato.id_ms_factura " +
                                "where id_actividad = @id_actividad " +
                                "  and ms_factura.empresa like '%' + @empresa + '%' "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM"
                                query = query + "  and empresa = 'DICOMEX' "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by ms_contrato.fecha_autoriza desc "

                    Case Else
                        'Asignar Cuenta
                        query = "select ms_instancia.id_ms_instancia " +
                        "     , ms_factura.id_ms_factura " +
                        "     , empresa " +
                        "     , empleado as solicito " +
                        "     , isnull(ms_factura.division, ms_factura.centro_costo) as centro_costo " +
                        "     , razon_emisor " +
                        "     , fecha_autoriza as fecha_emision " +
                        "     , importe " +
                        "from ms_instancia " +
                        "  left join ms_factura on ms_instancia.id_ms_sol = ms_factura.id_ms_factura and ms_instancia.tipo = 'F' " +
                        "  left join dt_factura on ms_factura.CFDI = dt_factura.uuid and dt_factura.movimiento in ('RECIBIDAS', 'RECIBIDA') " +
                        "where id_actividad = @id_actividad " +
                        "  and empresa like '%' + @empresa + '%' "
                        'Restricción por Empresas
                        Select Case ._txtPerfil.Text
                            Case "CoPame"
                                query = query + "  and empresa = 'PAME' "
                            Case "CoDCM", "AdmonDCM"
                                query = query + "  and empresa in ('DICOMEX', 'COPE', 'DIBIESE', 'APYRHSA') "
                                ''Case Else
                                ''    query = query + "  and empresa not in ('PAME', 'DICOMEX') "
                        End Select
                        query = query + "order by case when ms_factura.fecha_autoriza3 is null then case when ms_factura.fecha_autoriza2 is null then ms_factura.fecha_autoriza else ms_factura.fecha_autoriza2 end else ms_factura.fecha_autoriza3 end "
                End Select

                sdaCatalogo.SelectCommand = New SqlCommand(query, ConexionBD)
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@id_actividad", Val(._txtIdAct.Text))
                sdaCatalogo.SelectCommand.Parameters.AddWithValue("@empresa", .ddlEmpresaF.SelectedItem.Text)
                ConexionBD.Open()
                sdaCatalogo.Fill(dsCatalogo)
                .gvRegistrosF.DataBind()
                ConexionBD.Close()
                sdaCatalogo.Dispose()
                dsCatalogo.Dispose()
                .gvRegistrosF.SelectedIndex = -1
                'Inhabilitar columnas para vista
                .gvRegistrosF.Columns(0).Visible = False
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub btnConsFactI_Click(sender As Object, e As EventArgs) Handles btnConsFactI.Click
        envio("Consulta de Facturas Ingresadas", "ConsFactI.aspx")
    End Sub

    Protected Sub btnConsFact_Click(sender As Object, e As EventArgs) Handles btnConsFact.Click
        envio("Consulta de Facturas", "ConsFact.aspx")
    End Sub

    Protected Sub btnConsCotFact_Click(sender As Object, e As EventArgs) Handles btnConsCotFact.Click
        envio("Consulta de Cotizaciones de Facturas", "ConsCotFact.aspx")
    End Sub

    'Vehículos
    Protected Sub btnEntVeh_Click(sender As Object, e As EventArgs) Handles btnEntVeh.Click
        Me._txtIdAct.Text = 21
        llenarGridV()
    End Sub

    Protected Sub btnRecibVeh_Click(sender As Object, e As EventArgs) Handles btnRecibVeh.Click
        Me._txtIdAct.Text = 22
        llenarGridV()
    End Sub

    Protected Sub btnCambVeh_Click(sender As Object, e As EventArgs) Handles btnCambVeh.Click
        Me._txtIdAct.Text = 24
        llenarGridV()
    End Sub

    Protected Sub btnCancSol_Click(sender As Object, e As EventArgs) Handles btnCancSol.Click
        Me._txtIdAct.Text = 25
        llenarGridV()
    End Sub

    Protected Sub btnCambFecha_Click(sender As Object, e As EventArgs) Handles btnCambFecha.Click
        Me._txtIdAct.Text = 26
        llenarGridV()
    End Sub

    Protected Sub btnConsDisp_Click(sender As Object, e As EventArgs) Handles btnConsDisp.Click
        envio("Consulta de Disponibilidad", "ConsDisp.aspx")
    End Sub

    Protected Sub btnConsSol_Click(sender As Object, e As EventArgs) Handles btnConsSol.Click
        envio("Consulta de Solicitudes", "ConsSol.aspx")
    End Sub

    Protected Sub btnConsVeh_Click(sender As Object, e As EventArgs) Handles btnConsVeh.Click
        envio("Consulta de Vehículos", "ConsVehiculo.aspx")
    End Sub

    'Combustible (Gasolina)
    Protected Sub btnCargaEdenred_Click(sender As Object, e As EventArgs) Handles btnCargaEdenred.Click
        envio("Carga de Registros Edenred", "CargarArchEdenred.aspx")
    End Sub

    Protected Sub btnCargaToka_Click(sender As Object, e As EventArgs) Handles btnCargaToka.Click
        envio("Carga de Registros Toka", "CargarArchToka.aspx")
    End Sub

    Protected Sub btnDispersarG_Click(sender As Object, e As EventArgs) Handles btnDispersarG.Click
        Me._txtIdAct.Text = 40
        llenarGridDG()
    End Sub

    Protected Sub btnCargaSinID_Click(sender As Object, e As EventArgs) Handles btnCargaSinID.Click
        envio("Cargas Sin ID Conductor", "CargasSinID.aspx")
    End Sub

    Protected Sub btnConsUsrBloq_Click(sender As Object, e As EventArgs) Handles btnConsUsrBloq.Click
        envio("Usuarios Bloqueados", "ConsUsrBloq.aspx")
    End Sub

    Protected Sub btnCompComb_Click(sender As Object, e As EventArgs) Handles btnCompComb.Click
        Me._txtIdAct.Text = 33
        llenarGridG()
    End Sub

    Protected Sub btnCompCombTOKA_Click(sender As Object, e As EventArgs) Handles btnCompCombTOKA.Click
        Me._txtIdAct.Text = 109
        llenarGridG()
    End Sub

    Protected Sub btnCompCombTar_Click(sender As Object, e As EventArgs) Handles btnCompCombTar.Click
        Me._txtIdAct.Text = 35
        llenarGridG()
    End Sub

    Protected Sub btnConsComb_Click(sender As Object, e As EventArgs) Handles btnConsComb.Click
        envio("Consulta de Solicitudes de Combustible", "ConsComb.aspx")
    End Sub

    'Presupuesto de Gastos de Viaje
    Protected Sub btnCargaPresupGV_Click(sender As Object, e As EventArgs) Handles btnCargaPresupGV.Click
        envio("Carga de Presupuesto de Gastos de Viaje", "CargaPresupGV.aspx")
    End Sub

    Protected Sub btnConsPresupGV_Click(sender As Object, e As EventArgs) Handles btnConsPresupGV.Click
        envio("Consulta de Presupuesto de Gastos de Viaje", "ConsPresup.aspx")
    End Sub

    Protected Sub btnConsultaPGV_Click(sender As Object, e As EventArgs) Handles btnConsultaPGV.Click
        envio("Consulta de Presupuesto de Gastos de Viaje", "ConsPresupGV.aspx")
    End Sub

    Protected Sub btnSolicitarAmplPGV_Click(sender As Object, e As EventArgs) Handles btnSolicitarAmplPGV.Click
        envio("Solicitar Ampliación de Presupuesto de Gastos de Viaje", "105.aspx")
    End Sub

    Protected Sub btnAutorizarAmplPGV_Click(sender As Object, e As EventArgs) Handles btnAutorizarAmplPGV.Click
        Me._txtIdAct.Text = 106
        llenarGridSAP()
    End Sub

    Protected Sub btnValAmplPGV_Click(sender As Object, e As EventArgs) Handles btnValAmplPGV.Click
        Me._txtIdAct.Text = 117
        llenarGridSAP()
    End Sub

    Protected Sub btnConsultaAmplPGV_Click(sender As Object, e As EventArgs) Handles btnConsultaAmplPGV.Click
        envio("Consultar Ampliaciones de Presupuesto de Gastos de Viaje", "ConsAmpl.aspx")
    End Sub

    'Vehículos / Bloqueo x Rendimiento
    Protected Sub btnCatVehiculo_Click(sender As Object, e As EventArgs) Handles btnCatVehiculo.Click
        envio("Catálogo de Vehículos", "CatVehiculoST.aspx")
    End Sub

    Protected Sub btnConsUsrBloqR_Click(sender As Object, e As EventArgs) Handles btnConsUsrBloqR.Click
        envio("Bloqueos por Rendimiento", "ConsUsrBloq.aspx")
    End Sub

    Protected Sub btnBitacoraCargas_Click(sender As Object, e As EventArgs) Handles btnBitacoraCargas.Click
        envio("Bitácora de Cargas", "BitacoraCargas.aspx")
    End Sub

    Protected Sub btnConsBloqueos_Click(sender As Object, e As EventArgs) Handles btnConsBloqueos.Click
        envio("Consulta de Bloqueos", "ConsBloqueosST.aspx")
    End Sub

    'Consultas Auditoría
    Protected Sub btnConsAntCaAud_Click(sender As Object, e As EventArgs) Handles btnConsAntCaAud.Click
        envio("Consulta de Anticipos", "ConsAnt.aspx")
    End Sub

    Protected Sub btnConsAntCajaAud_Click(sender As Object, e As EventArgs) Handles btnConsAntCajaAud.Click
        envio("Consulta Cuadre de Anticipos", "ConsAntCaja.aspx")
    End Sub

    Protected Sub btnConsCompXConAud_Click(sender As Object, e As EventArgs) Handles btnConsCompXConAud.Click
        envio("Consulta de Comprobaciones por Concepto", "ConsCompXConceptoAud.aspx")
    End Sub

#End Region

#Region "Caja"

    ''Anticipos
    'Protected Sub btnEntEfect_Click(sender As Object, e As EventArgs) Handles btnEntEfect.Click
    '    Me._txtIdAct.Text = 5
    '    llenarGridA()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsAntCa_Click(sender As Object, e As EventArgs) Handles btnConsAntCa.Click
    '    envio("Consulta de Anticipos", "ConsAnt.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Faturas de Gastos, Seguros y Asesorías
    'Protected Sub btnConsFactCa_Click(sender As Object, e As EventArgs) Handles btnConsFactCa.Click
    '    envio("Consulta de Facturas", "ConsFactC.aspx")
    'End Sub

    ''Comprobaciones
    'Protected Sub btnEntEfectC_Click(sender As Object, e As EventArgs) Handles btnEntEfectC.Click
    '    Me._txtIdAct.Text = 31
    '    llenarGridC()
    'End Sub

    'Protected Sub btnConsCompCa_Click(sender As Object, e As EventArgs) Handles btnConsCompCa.Click
    '    envio("Consulta de Comprobaciones", "ConsComp.aspx")
    '    '' ''enConstruccion()
    'End Sub

#End Region

#Region "Comprobaciones / Caja PAME DCM"

    ''Anticipos
    'Protected Sub btnEntEfectACoPame_Click(sender As Object, e As EventArgs) Handles btnEntEfectACoPame.Click
    '    Me._txtIdAct.Text = 5
    '    llenarGridA()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnSolRecCoPAME_Click(sender As Object, e As EventArgs) Handles btnSolRecCoPAME.Click
    '    envio("Solicitar Recursos", "37.aspx")
    'End Sub

    'Protected Sub btnConsAntCoPame_Click(sender As Object, e As EventArgs) Handles btnConsAntCoPame.Click
    '    envio("Consulta de Anticipos", "ConsAnt.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Comprobaciones
    'Protected Sub btnValCompCoPame_Click(sender As Object, e As EventArgs) Handles btnValCompCoPame.Click
    '    Me._txtIdAct.Text = 10
    '    llenarGridC()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnGenCompCoPame_Click(sender As Object, e As EventArgs) Handles btnGenCompCoPame.Click
    '    envio("Generar Comprobación", "08.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnPlantCompCoPame_Click(sender As Object, e As EventArgs) Handles btnPlantCompCoPame.Click
    '    envio("Plantilla Comprobaciones", "ConsPlant.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsCompCoPame_Click(sender As Object, e As EventArgs) Handles btnConsCompCoPame.Click
    '    envio("Consulta de Comprobaciones", "ConsComp.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Gasolina
    'Protected Sub btnCargaEdenredDCM_Click(sender As Object, e As EventArgs) Handles btnCargaEdenredDCM.Click
    '    envio("Carga de Registros Edenred", "CargarArchEdenred.aspx")
    'End Sub

    'Protected Sub btnConsCombDCM_Click(sender As Object, e As EventArgs) Handles btnConsCombDCM.Click
    '    envio("Consulta de Solicitudes de Combustible", "ConsComb.aspx")
    'End Sub

    'Protected Sub btnConsUsrBloqDCM_Click(sender As Object, e As EventArgs) Handles btnConsUsrBloqDCM.Click
    '    envio("Usuarios Bloqueados", "ConsUsrBloq.aspx")
    'End Sub

    'Protected Sub btnDispersarGDCM_Click(sender As Object, e As EventArgs) Handles btnDispersarGDCM.Click
    '    Me._txtIdAct.Text = 40
    '    llenarGridDG()
    '    '' ''enConstruccion()
    'End Sub

#End Region

#Region "Contador"

    'Protected Sub btnAsigCuentaCon_Click(sender As Object, e As EventArgs) Handles btnAsigCuentaCon.Click
    '    Me._txtIdAct.Text = 15
    '    llenarGridF()
    '    '' ''enConstruccion()
    'End Sub

#End Region

#Region "Contador Funcionarios"

    'Protected Sub btnAutFactContaF_Click(sender As Object, e As EventArgs) Handles btnAutFactContaF.Click
    '    Me._txtIdAct.Text = 14
    '    llenarGridF()
    '    '' ''enConstruccion()
    'End Sub

#End Region

#Region "Cuentas por Pagar"

    'Protected Sub btnConsFactI_Click(sender As Object, e As EventArgs) Handles btnConsFactI.Click
    '    envio("Consulta de Facturas Ingresadas", "ConsFactI.aspx")
    'End Sub

#End Region

#Region "pnlUsuario"

    ''Solicitud de Recursos
    'Protected Sub btnSolRecU_Click(sender As Object, e As EventArgs) Handles btnSolRecU.Click
    '    'If valNoProv() Then
    '    '    If valLic() Then
    '    '        If valCurso() Then
    '    envio("Solicitar Recursos", "37.aspx")
    '    '        Else
    '    '            Me.litError.Text = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
    '    '        End If
    '    '    Else
    '    '        Me.litError.Text = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
    '    '    End If
    '    'Else
    '    '    Me.litError.Text = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
    '    'End If
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsSRU_Click(sender As Object, e As EventArgs) Handles btnConsSRU.Click
    '    envio("Consulta de Solicitudes de Recursos", "ConsSolRec.aspx")
    'End Sub

    ''Anticipos
    ' ''Protected Sub btnSolAntU_Click(sender As Object, e As EventArgs) Handles btnSolAntU.Click
    ' ''    If valNoProv() Then
    ' ''        envio("Solicitar Anticipos", "01.aspx")
    ' ''    Else
    ' ''        Me.litError.Text = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
    ' ''    End If
    ' ''    '' ''enConstruccion()
    ' ''End Sub

    'Protected Sub btnConsAntU_Click(sender As Object, e As EventArgs) Handles btnConsAntU.Click
    '    envio("Consulta de Anticipos", "ConsAnt.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Comprobaciones
    'Protected Sub btnGenCompU_Click(sender As Object, e As EventArgs) Handles btnGenCompU.Click
    '    envio("Generar Comprobación", "08.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnCorrCompU_Click(sender As Object, e As EventArgs) Handles btnCorrCompU.Click
    '    Me._txtIdAct.Text = 12
    '    llenarGridC()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsCompU_Click(sender As Object, e As EventArgs) Handles btnConsCompU.Click
    '    envio("Consulta de Comprobaciones", "ConsComp.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Faturas de Gastos, Seguros y Asesorías
    'Protected Sub btnSolFactU_Click(sender As Object, e As EventArgs) Handles btnSolFactU.Click
    '    envio("Ingresar Factura", "13.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnCorrFactU_Click(sender As Object, e As EventArgs) Handles btnCorrFactU.Click
    '    Me._txtIdAct.Text = 17
    '    llenarGridF()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsFactU_Click(sender As Object, e As EventArgs) Handles btnConsFactU.Click
    '    envio("Consulta de Facturas", "ConsFact.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Reservación de Vehículos
    ' ''Protected Sub btnSolReservU_Click(sender As Object, e As EventArgs) Handles btnSolReservU.Click
    ' ''    If valLic() Then
    ' ''        If valCurso() Then
    ' ''            envio("Solicitar Reservación", "18.aspx")
    ' ''        Else
    ' ''            Me.litError.Text = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
    ' ''        End If
    ' ''    Else
    ' ''        Me.litError.Text = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
    ' ''    End If
    ' ''End Sub

    'Protected Sub btnConsDispU_Click(sender As Object, e As EventArgs) Handles btnConsDispU.Click
    '    envio("Consulta de Disponibilidad", "ConsDisp.aspx")
    'End Sub

    'Protected Sub btnConsSolU_Click(sender As Object, e As EventArgs) Handles btnConsSolU.Click
    '    envio("Consulta de Solicitudes", "ConsSol.aspx")
    'End Sub

    ''Combustible (Gasolina)
    'Protected Sub btnConsCombU_Click(sender As Object, e As EventArgs) Handles btnConsCombU.Click
    '    envio("Consulta de Solicitudes de Combustible", "ConsComb.aspx")
    'End Sub

    'Protected Sub btnCompComb_Click(sender As Object, e As EventArgs) Handles btnCompComb.Click
    '    Me._txtIdAct.Text = 33
    '    llenarGridG()
    'End Sub

    'Protected Sub btnCompCombTar_Click(sender As Object, e As EventArgs) Handles btnCompCombTar.Click
    '    Me._txtIdAct.Text = 35
    '    llenarGridG()
    'End Sub

#End Region

#Region "pnlAutorizador"

    ''Solicitud de Recursos
    'Protected Sub btnSolRecAu_Click(sender As Object, e As EventArgs) Handles btnSolRecAu.Click
    '    'If valNoProv() Then
    '    '    If valLic() Then
    '    '        If valCurso() Then
    '    envio("Solicitar Recursos", "37.aspx")
    '    '        Else
    '    '            Me.litError.Text = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
    '    '        End If
    '    '    Else
    '    '        Me.litError.Text = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
    '    '    End If
    '    'Else
    '    '    Me.litError.Text = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
    '    'End If
    '    '' ''enConstruccion()
    'End Sub


    'Protected Sub btnConsSRAu_Click(sender As Object, e As EventArgs) Handles btnConsSRAu.Click
    '    envio("Consulta de Solicitudes de Recursos", "ConsSolRec.aspx")
    'End Sub

    ''Anticipos
    ' ''Protected Sub btnSolAntA_Click(sender As Object, e As EventArgs) Handles btnSolAntA.Click
    ' ''    If valNoProv() Then
    ' ''        envio("Solicitar Anticipos", "01.aspx")
    ' ''    Else
    ' ''        Me.litError.Text = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
    ' ''    End If
    ' ''    '' ''enConstruccion()
    ' ''End Sub

    'Protected Sub btnAutAntA_Click(sender As Object, e As EventArgs) Handles btnAutAntA.Click
    '    Me._txtIdAct.Text = 2
    '    llenarGridA()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsAntA_Click(sender As Object, e As EventArgs) Handles btnConsAntA.Click
    '    envio("Consulta de Anticipos", "ConsAnt.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Comprobaciones
    'Protected Sub btnGenCompAu_Click(sender As Object, e As EventArgs) Handles btnGenCompAu.Click
    '    envio("Generar Comprobación", "08.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnCorrCompAu_Click(sender As Object, e As EventArgs) Handles btnCorrCompAu.Click
    '    Me._txtIdAct.Text = 12
    '    llenarGridC()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnAutCompAu_Click(sender As Object, e As EventArgs) Handles btnAutCompAu.Click
    '    Me._txtIdAct.Text = 9
    '    llenarGridC()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsCompAu_Click(sender As Object, e As EventArgs) Handles btnConsCompAu.Click
    '    envio("Consulta de Comprobaciones", "ConsComp.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Faturas de Gastos, Seguros y Asesorías
    'Protected Sub btnSolFactA_Click(sender As Object, e As EventArgs) Handles btnSolFactA.Click
    '    envio("Ingresar Factura", "13.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnAutFactA_Click(sender As Object, e As EventArgs) Handles btnAutFactA.Click
    '    Me._txtIdAct.Text = 14
    '    llenarGridF()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnCorrFactA_Click(sender As Object, e As EventArgs) Handles btnCorrFactA.Click
    '    Me._txtIdAct.Text = 17
    '    llenarGridF()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsFactA_Click(sender As Object, e As EventArgs) Handles btnConsFactA.Click
    '    envio("Consulta de Facturas", "ConsFact.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Reservación de Vehículos
    ' ''Protected Sub btnSolReservAu_Click(sender As Object, e As EventArgs) Handles btnSolReservAu.Click
    ' ''    If valLic() Then
    ' ''        If valCurso() Then
    ' ''            envio("Solicitar Reservación", "18.aspx")
    ' ''        Else
    ' ''            Me.litError.Text = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
    ' ''        End If
    ' ''    Else
    ' ''        Me.litError.Text = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
    ' ''    End If
    ' ''End Sub

    'Protected Sub btnAutReservAu_Click(sender As Object, e As EventArgs) Handles btnAutReservAu.Click
    '    Me._txtIdAct.Text = 19
    '    llenarGridV()
    'End Sub

    'Protected Sub btnConsDispAu_Click(sender As Object, e As EventArgs) Handles btnConsDispAu.Click
    '    envio("Consulta de Disponibilidad", "ConsDisp.aspx")
    'End Sub

    'Protected Sub btnConsSolAu_Click(sender As Object, e As EventArgs) Handles btnConsSolAu.Click
    '    envio("Consulta de Solicitudes", "ConsSol.aspx")
    'End Sub

    ''Combustible (Gasolina)
    'Protected Sub btnConsCombAu_Click(sender As Object, e As EventArgs) Handles btnConsCombAu.Click
    '    envio("Consulta de Solicitudes de Combustible", "ConsComb.aspx")
    'End Sub

    'Protected Sub btnCompCombA_Click(sender As Object, e As EventArgs) Handles btnCompCombA.Click
    '    Me._txtIdAct.Text = 33
    '    llenarGridG()
    'End Sub

    'Protected Sub btnCompCombTarA_Click(sender As Object, e As EventArgs) Handles btnCompCombTarA.Click
    '    Me._txtIdAct.Text = 35
    '    llenarGridG()
    'End Sub

#End Region

#Region "pnlVigilante"

    ''Solicitud de Recursos
    'Protected Sub btnSolRecV_Click(sender As Object, e As EventArgs) Handles btnSolRecV.Click
    '    'If valNoProv() Then
    '    '    If valLic() Then
    '    '        If valCurso() Then
    '    envio("Solicitar Recursos", "37.aspx")
    '    '        Else
    '    '            Me.litError.Text = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
    '    '        End If
    '    '    Else
    '    '        Me.litError.Text = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
    '    '    End If
    '    'Else
    '    '    Me.litError.Text = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
    '    'End If
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsSRV_Click(sender As Object, e As EventArgs) Handles btnConsSRV.Click
    '    envio("Consulta de Solicitudes de Recursos", "ConsSolRec.aspx")
    'End Sub

    ''Anticipos
    ' ''Protected Sub btnSolAntV_Click(sender As Object, e As EventArgs) Handles btnSolAntV.Click
    ' ''    If valNoProv() Then
    ' ''        envio("Solicitar Anticipos", "01.aspx")
    ' ''    Else
    ' ''        Me.litError.Text = "Sin Código de Proveedor NAV, favor de validarlo con el Administrador del SiCEm y Nómina"
    ' ''    End If
    ' ''    '' ''enConstruccion()
    ' ''End Sub

    'Protected Sub btnConsAntV_Click(sender As Object, e As EventArgs) Handles btnConsAntV.Click
    '    envio("Consulta de Anticipos", "ConsAnt.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ''Comprobaciones
    'Protected Sub btnGenCompV_Click(sender As Object, e As EventArgs) Handles btnGenCompV.Click
    '    envio("Generar Comprobación", "08.aspx")
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnCorrCompV_Click(sender As Object, e As EventArgs) Handles btnCorrCompV.Click
    '    Me._txtIdAct.Text = 12
    '    llenarGridC()
    '    '' ''enConstruccion()
    'End Sub

    'Protected Sub btnConsCompV_Click(sender As Object, e As EventArgs) Handles btnConsCompV.Click
    '    envio("Consulta de Comprobaciones", "ConsComp.aspx")
    '    '' ''enConstruccion()
    'End Sub

    ' ''Protected Sub btnSolReservV_Click(sender As Object, e As EventArgs) Handles btnSolReservV.Click
    ' ''    If valLic() Then
    ' ''        If valCurso() Then
    ' ''            envio("Solicitar Reservación", "18.aspx")
    ' ''        Else
    ' ''            Me.litError.Text = "Curso de Manejo a la Defensiva no Aprobado, favor de validarlo con el Administrador del SiCEm"
    ' ''        End If
    ' ''    Else
    ' ''        Me.litError.Text = "Licencia Expirada, favor de validarlo con el Administrador del SiCEm"
    ' ''    End If
    ' ''End Sub

    'Protected Sub btnConsSolV_Click(sender As Object, e As EventArgs) Handles btnConsSolV.Click
    '    envio("Consulta de Solicitudes", "ConsSol.aspx")
    'End Sub

    ''Combustible (Gasolina)
    'Protected Sub btnConsCombV_Click(sender As Object, e As EventArgs) Handles btnConsCombV.Click
    '    envio("Consulta de Solicitudes de Combustible", "ConsComb.aspx")
    'End Sub

    'Protected Sub btnCompCombV_Click(sender As Object, e As EventArgs) Handles btnCompCombV.Click
    '    Me._txtIdAct.Text = 33
    '    llenarGridG()
    'End Sub

    'Protected Sub btnCompCombTarV_Click(sender As Object, e As EventArgs) Handles btnCompCombTarV.Click
    '    Me._txtIdAct.Text = 35
    '    llenarGridG()
    'End Sub

#End Region

#Region "Desarrollo Organizacional"

    Protected Sub btnCatUnidadN_Click(sender As Object, e As EventArgs) Handles btnCatUnidadN.Click
        envio("Catálogo de Unidades de Negocio", "CatUnidad.aspx")
    End Sub

    Protected Sub btnCatDireccion_Click(sender As Object, e As EventArgs) Handles btnCatDireccion.Click
        envio("Catálogo de Direcciones", "CatDireccion.aspx")
    End Sub

    Protected Sub btnCatArea_Click(sender As Object, e As EventArgs) Handles btnCatArea.Click
        envio("Catálogo de Áreas", "CatArea.aspx")
    End Sub

    Protected Sub btnPorcentBono_Click(sender As Object, e As EventArgs) Handles btnPorcentBono.Click
        envio("Porcentaje Bono", "PorcentBono.aspx")
    End Sub

    Protected Sub btnPeriodoEval_Click(sender As Object, e As EventArgs) Handles btnPeriodoEval.Click
        envio("Periodo de Evaluaciones", "PeriodoEval.aspx")
    End Sub

    Protected Sub btnCatEmplInd_Click(sender As Object, e As EventArgs) Handles btnCatEmplInd.Click
        envio("Catálogo de Empleados - Indicadores", "CatEmplInd.aspx")
    End Sub

#End Region

#Region "Jefatura de Información"

    Protected Sub btnRegCumplUN_Click(sender As Object, e As EventArgs) Handles btnRegCumplUN.Click
        envio("Registro de Porcentaje de Cumplimiento de Unidad de Negocio", "RegCumplUN.aspx")
    End Sub

#End Region

#Region "Evaluaciones"

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        envio("Registrar Evaluación", "68.aspx")
    End Sub

    Protected Sub btnValidarEval_Click(sender As Object, e As EventArgs) Handles btnValidarEval.Click
        Me._txtIdAct.Text = 69
        llenarGridEval()
    End Sub

    Protected Sub btnCorregirEval_Click(sender As Object, e As EventArgs) Handles btnCorregirEval.Click
        Me._txtIdAct.Text = 71
        llenarGridEval()
    End Sub

    Protected Sub btnAutorizarEvalA_Click(sender As Object, e As EventArgs) Handles btnAutorizarEvalA.Click
        Me._txtIdAct.Text = 73
        llenarGridEval()
    End Sub

    Protected Sub btnCorregirEvalA_Click(sender As Object, e As EventArgs) Handles btnCorregirEvalA.Click
        Me._txtIdAct.Text = 81
        llenarGridEval()
    End Sub

    Protected Sub btnValidarEvalA_Click(sender As Object, e As EventArgs) Handles btnValidarEvalA.Click
        Me._txtIdAct.Text = 82
        llenarGridEval()
    End Sub

    Protected Sub btnConcentrarEvalA_Click(sender As Object, e As EventArgs) Handles btnConcentrarEval.Click
        envio("Concentrar Evaluaciones", "76.aspx")
    End Sub

    Protected Sub btnProcesarEval2Q_Click(sender As Object, e As EventArgs) Handles btnProcesarEval2Q.Click
        envio("Procesar Evaluaciones 2da Quincena", "77.aspx")
    End Sub

    Protected Sub btnProcesarEval1Q_Click(sender As Object, e As EventArgs) Handles btnProcesarEval1Q.Click
        envio("Procesar Evaluaciones 1ra Quincena", "79.aspx")
    End Sub

    Protected Sub btnConsEvaluacion_Click(sender As Object, e As EventArgs) Handles btnConsEvaluacion.Click
        envio("Consultar Evaluaciones", "ConsEval.aspx")
    End Sub

#End Region

#Region "Reuniones"

    Protected Sub btnCatGrupo_Click(sender As Object, e As EventArgs) Handles btnCatGrupo.Click
        envio("Catálogo de Grupos", "CatGrupo.aspx")
    End Sub

    Protected Sub btnAltaReunion_Click(sender As Object, e As EventArgs) Handles btnAltaReunion.Click
        envio("Registrar Reunión", "AltReunion.aspx")
    End Sub

    Protected Sub btnSegReunion_Click(sender As Object, e As EventArgs) Handles btnSegReunion.Click
        Me._txtIdAct.Text = -1
        llenarGridReun()
    End Sub

    Protected Sub btnEvalReunion_Click(sender As Object, e As EventArgs) Handles btnEvalReunion.Click
        Me._txtIdAct.Text = -2
        llenarGridReun()
    End Sub

    Protected Sub btnConsReunion_Click(sender As Object, e As EventArgs) Handles btnConsReunion.Click
        envio("Consulta de Reuniones", "ConsReunion.aspx")
    End Sub

    Protected Sub btnAltaActividad_Click(sender As Object, e As EventArgs) Handles btnAltaActividad.Click
        envio("Alta de Actividad", "AltaActividad.aspx")
    End Sub

    Protected Sub btnConsActividad_Click(sender As Object, e As EventArgs) Handles btnConsActividad.Click
        envio("Consulta de Actividades", "ConsActividad.aspx")
    End Sub

#End Region

#Region "Movimientos Internos"
    Protected Sub btnAutMov_Click(sender As Object, e As EventArgs) Handles btnAutMov.Click
        With Me
            Try
                Me._txtIdAct.Text = 124
                llenarGridMovInt()
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub
    Protected Sub btnCodCont_Click(sender As Object, e As EventArgs) Handles btnCodCont.Click
        With Me
            Try
                Me._txtIdAct.Text = 125
                llenarGridMovInt()
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub

    Protected Sub btnSolMovInt_Click(sender As Object, e As EventArgs) Handles btnSolMovInt.Click
        With Me
            Try
                envio("Solicitar Movimientos Internos", "123.aspx")
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub
    Protected Sub btnConsFactIng_Click(sender As Object, e As EventArgs) Handles btnConsFactIng.Click
        With Me
            Try
                envio("Consulta Movimientos Internos", "ConsFactIng.aspx")
            Catch ex As Exception
                litError.Text = ex.Message
            End Try
        End With
    End Sub
#End Region

#Region "Grid"

    Protected Sub gvRegistrosReun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosReun.SelectedIndexChanged
        Me.litError.Text = ""
        Session("idMsInst") = Val(Me.gvRegistrosReun.SelectedRow.Cells(0).Text)
        Select Case Val(Me._txtIdAct.Text)
            Case -1
                envio("Seguimiento a Reunión", "SegReunion.aspx")
            Case -2
                envio("Evaluar Participantes de la Reunión", "EvalPartReunion.aspx")
        End Select
    End Sub

    Protected Sub gvRegistrosEval_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosEval.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosEval.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 69
                        envio("Autorizar Evaluación", "69.aspx")
                    Case 71
                        envio("Corregir Evaluación", "71.aspx")
                        'Case 72
                        '    envio("Validar Evaluaciones del Área", "72.aspx")
                    Case 73
                        envio("Autorizar Evaluaciones del Área", "73.aspx")
                        'Case 75
                        '    envio("Corregir Evaluaciones del Área", "75.aspx")
                        'Case 76
                        '    envio("Concentrar Evaluaciones", "76.aspx")
                    Case 81
                        envio("Corregir Evaluaciones de Área Inválidas", "81.aspx")
                    Case 82
                        envio("Validar Evaluaciones de Área Inválidas", "82.aspx")
                    Case 77
                        envio("Procesar Evaluaciones", "77.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosSR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosSR.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosSR.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 38
                        envio("Autorizar Solicitud de Recursos", "38.aspx")
                    Case 59
                        envio("Vo.Bo. de Solicitud de Recursos", "59.aspx")
                    Case 116
                        envio("Autorizar Solicitud de Recursos Exedente", "38a.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosA.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosA.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 2
                        envio("Autorizar Anticipo", "02.aspx")
                    Case 3
                        envio("Gestionar Transferencia", "03.aspx")
                    Case 5
                        envio("Entregar Efectivo", "05.aspx")
                    Case 66
                        envio("Registrar Anticipo American Express", "66.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosC.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosC.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 9
                        envio("Autorizar Comprobación", "09.aspx")
                    Case 10
                        envio("Validar Comprobación", "10.aspx")
                    Case 12
                        envio("Corregir Comprobación", "12.aspx")
                    Case 31
                        envio("Entregar Efectivo", "31.aspx")
                    Case 62
                        envio("Autorizar Comprobación Extemporánea", "62.aspx")
                    Case 115
                        envio("Pre-autorizar Comprobación", "09a.aspx")


                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosNS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosNS.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosNS.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 88
                        envio("Ingresar Cotizaciones de Negociación", "88.aspx")
                    Case 89
                        envio("Autorizar Cotización de Negociación", "89.aspx")
                    Case 90
                        envio("Autorizar Solicitud de Negociación", "90.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosF_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosF.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosF.SelectedRow.Cells(0).Text)
                If Val(._txtIdAct.Text) = 50 Then
                    Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                    ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                    Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                    SCMValores.Connection = ConexionBD
                    SCMValores.CommandText = ""
                    SCMValores.Parameters.Clear()
                    SCMValores.CommandText = "select id_actividad " +
                                             "from ms_instancia " +
                                             "where id_ms_instancia = @idMsInst "
                    SCMValores.Parameters.AddWithValue("@idMsInst", Val(.gvRegistrosF.SelectedRow.Cells(0).Text))
                    ConexionBD.Open()
                    ._txtIdAct.Text = SCMValores.ExecuteScalar()
                    ConexionBD.Close()
                End If
                Select Case Val(._txtIdAct.Text)
                    Case 14
                        envio("Autorizar Factura", "14.aspx")
                    Case 15
                        envio("Asignar Cuenta", "15.aspx")
                    Case 17
                        envio("Corregir Factura", "17.aspx")
                    Case 44
                        envio("Validar Solicitud", "44.aspx")
                    Case 46
                        envio("Ingresar Cotizaciones", "46.aspx")
                    Case 47
                        envio("Autorizar Solicitud", "47.aspx")
                    Case 49
                        envio("Ingresar Factura", "49.aspx")
                    Case 50
                        envio("Autorizar Factura", "50.aspx")
                    Case 51
                        envio("Corregir Factura", "51.aspx")
                    Case 52
                        envio("Autorizar Factura", "52.aspx")
                    Case 53
                        envio("Autorizar Factura", "53.aspx")
                    Case 54
                        envio("Complementar Datos de Contrato", "54.aspx")
                    Case 55
                        envio("Autorizar Solicitud de Contrato", "55.aspx")
                    Case 57
                        envio("Asignar Cuentas para Contrato", "57.aspx")
                    Case 58
                        envio("Registrar Contrato en NAV", "58.aspx")
                    Case 83
                        envio("Corregir Solicitud", "83.aspx")
                    Case 84
                        envio("Validar Presupuesto", "84.aspx")
                    Case 85
                        envio("Solicitar Ampliación de Presupuesto", "85.aspx")
                    Case 94
                        envio("Validar Aplicabilidad", "94.aspx")
                    Case 96
                        envio("Autorizar Servicio Negociado", "96.aspx")
                    Case 98
                        envio("Validar Presupuesto para Servicio Negociado", "98.aspx")
                    Case 99
                        envio("Solicitar Ampliación de Presupuesto para Servicio Negociado", "99.aspx")
                    Case 101
                        envio("Ingresar Factura de Servicio Negociado", "101.aspx")
                    Case 102
                        envio("Validar Soportes", "102.aspx")
                    Case 104
                        envio("Corregir Factura de Servicio Negociado", "104.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosSAP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosSAP.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosSAP.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 106
                        envio("Autorizar Ampliación de Presupuesto de Gastos de Viaje", "106.aspx")
                    Case 117
                        envio("Validar Ampliación de Presupuesto de Gastos de Viaje", "117.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosV.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsReserv") = Val(.gvRegistrosV.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 19
                        envio("Autorizar Reservación", "19.aspx")
                    Case 21
                        envio("Entregar Vehículo al Usuario", "21.aspx")
                    Case 22
                        envio("Recibir Vehículo del Usuario", "22.aspx")
                    Case 24
                        'Cambiar Vehículo
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        Dim conteo As Integer = 0
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "from ms_reserva " +
                                                 "where fecha_ini < getdate() and fecha_fin > getdate() and id_ms_reserva = @id_ms_reserva "
                        SCMValores.Parameters.AddWithValue("@id_ms_reserva", Val(.gvRegistrosV.SelectedRow.Cells(0).Text))
                        ConexionBD.Open()
                        conteo = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If conteo > 0 Then
                            envio("Cambiar Vehículo", "CambVeh.aspx")
                        Else
                            Session("idMsReserv") = 0
                            llenarGridV()
                        End If
                    Case 25
                        'Cancelar Reservación
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        Dim conteo As Integer = 0
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "from ms_reserva " +
                                                 "where fecha_ini < getdate() and fecha_fin > getdate() and id_ms_reserva = @id_ms_reserva "
                        SCMValores.Parameters.AddWithValue("@id_ms_reserva", Val(.gvRegistrosV.SelectedRow.Cells(0).Text))
                        ConexionBD.Open()
                        conteo = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If conteo > 0 Then
                            envio("Cancelar Reservación", "CancelSol.aspx")
                        Else
                            Session("idMsReserv") = 0
                            llenarGridV()
                        End If
                    Case 26
                        'Cambio de Fecha de Regreso
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        Dim conteo As Integer = 0
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "from ms_reserva " +
                                                 "where status = 'T' and id_ms_reserva = @id_ms_reserva "
                        SCMValores.Parameters.AddWithValue("@id_ms_reserva", Val(.gvRegistrosV.SelectedRow.Cells(0).Text))
                        ConexionBD.Open()
                        conteo = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If conteo > 0 Then
                            envio("Cambiar de Fecha de Regreso", "CambFecha.aspx")
                        Else
                            Session("idMsReserv") = 0
                            llenarGridV()
                        End If
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
    Protected Sub gvMovInt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvMovInt.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(gvMovInt.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 124
                        envio("Autorizar Movimientos Internos", "124.aspx")
                    Case 125
                        envio("Codificación contable", "125.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub
    Protected Sub gvRegistrosG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosG.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idDtCargaComb") = Val(.gvRegistrosG.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 33
                        envio("Comprobar Combustible", "33.aspx")
                    Case 35
                        envio("Comprobar Combustible con Tarjeta Bancaria", "35.aspx")
                    Case 109
                        'Validar que no exitan registros previos pendientes por comprobar de esa unidad
                        Dim ConexionBD As SqlConnection = New System.Data.SqlClient.SqlConnection
                        ConexionBD.ConnectionString = accessDB.conBD("ProcAd")
                        Dim SCMValores As SqlCommand = New System.Data.SqlClient.SqlCommand
                        Dim contPend As Integer
                        SCMValores.Connection = ConexionBD
                        SCMValores.CommandText = ""
                        SCMValores.Parameters.Clear()
                        SCMValores.CommandText = "select count(*) " +
                                                 "from dt_carga_comb_toka " +
                                                 "where identificador_vehiculo = @no_eco " +
                                                 "  and status = 'P' " +
                                                 "  and fecha <= (select fecha " +
                                                 "                from dt_carga_comb_toka " +
                                                 "                where id_dt_carga_comb_toka = @id_dt_carga_comb_toka) " +
                                                 "  and id_dt_carga_comb_toka <> @id_dt_carga_comb_toka "
                        SCMValores.Parameters.AddWithValue("@id_dt_carga_comb_toka", Val(.gvRegistrosG.SelectedRow.Cells(0).Text))
                        SCMValores.Parameters.AddWithValue("@no_eco", .gvRegistrosG.SelectedRow.Cells(3).Text)
                        ConexionBD.Open()
                        contPend = SCMValores.ExecuteScalar()
                        ConexionBD.Close()
                        If contPend = 0 Then
                            envio("Comprobar Combustible con Tarjeta TOKA", "109.aspx")
                        Else
                            'Existen Pendientes Previos
                            .litError.Text = "Existen registros previos pendientes de comprobar de esa unidad, hasta que no se finalicen, no se podrá comprobar esta carga"
                        End If
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

    Protected Sub gvRegistrosDG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvRegistrosDG.SelectedIndexChanged
        With Me
            Try
                .litError.Text = ""
                Session("idMsInst") = Val(.gvRegistrosDG.SelectedRow.Cells(0).Text)
                Select Case Val(._txtIdAct.Text)
                    Case 40
                        envio("Dispersar Gasolina", "40.aspx")
                End Select
            Catch ex As Exception
                .litError.Text = ex.ToString
            End Try
        End With
    End Sub

#End Region

    Protected Sub btnGenAAE_Click(sender As Object, e As EventArgs) Handles btnGenAAE.Click
        envio("Generar Anticipo American Express", "65.aspx")
    End Sub

    Protected Sub btnRegAAE_Click(sender As Object, e As EventArgs) Handles btnRegAAE.Click
        Me._txtIdAct.Text = 66
        llenarGridA()
    End Sub

    Protected Sub btnChecador_Click(sender As Object, e As EventArgs) Handles btnChecador.Click
        envio("Checador", "IngresoChecador.aspx")
    End Sub

    Protected Sub btnCatPermisos_Click(sender As Object, e As EventArgs) Handles btnCatPermisos.Click
        envio("Catálogo de Perfiles", "CatPermisos.aspx")
    End Sub


End Class