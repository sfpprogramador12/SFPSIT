using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
namespace SFP.SIT.SERV.Util
{
	 public class DButil
	 {
	 	 public static class SIT_ADM_AREA_COL
	 	 {
	 	 	 public const string ARAFECCREACION = "ARAFECCREACION";
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 }
	 	 public static class SIT_ADM_AREAGESTION_COL
	 	 {
	 	 	 public const string AGNDESCRIPCION = "AGNDESCRIPCION";
	 	 	 public const string AGNFECFIN = "AGNFECFIN";
	 	 	 public const string AGNFECINI = "AGNFECINI";
	 	 	 public const string AGNCLAVE = "AGNCLAVE";
	 	 }
	 	 public static class SIT_ADM_AREAHIST_COL
	 	 {
	 	 	 public const string ATPCLAVE = "ATPCLAVE";
	 	 	 public const string ANLCLAVE = "ANLCLAVE";
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 	 public const string ARHREPORTA = "ARHREPORTA";
	 	 	 public const string ARHSIGLAS = "ARHSIGLAS";
	 	 	 public const string ARHDESCRIPCION = "ARHDESCRIPCION";
	 	 	 public const string ARHCLAVEUA = "ARHCLAVEUA";
	 	 	 public const string ARHFECFIN = "ARHFECFIN";
	 	 	 public const string ARHFECINI = "ARHFECINI";
	 	 }
	 	 public static class SIT_ADM_AREANIVEL_COL
	 	 {
	 	 	 public const string ANLDESCRIPCION = "ANLDESCRIPCION";
	 	 	 public const string ANLCLAVE = "ANLCLAVE";
	 	 }
	 	 public static class SIT_ADM_AREAORG_COL
	 	 {
	 	 	 public const string AGNCLAVE = "AGNCLAVE";
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 	 public const string ORGORDEN = "ORGORDEN";
	 	 	 public const string ORGCLAVEREPORTA = "ORGCLAVEREPORTA";
	 	 }
	 	 public static class SIT_ADM_AREATIPO_COL
	 	 {
	 	 	 public const string ATPDESCRIPCION = "ATPDESCRIPCION";
	 	 	 public const string ATPCLAVE = "ATPCLAVE";
	 	 }
	 	 public static class SIT_ADM_CLASES_COL
	 	 {
	 	 	 public const string CLANOMBRE = "CLANOMBRE";
	 	 	 public const string CLADESCRIPCION = "CLADESCRIPCION";
	 	 	 public const string CLACLAVE = "CLACLAVE";
	 	 }
	 	 public static class SIT_ADM_CONFIGURACION_COL
	 	 {
	 	 	 public const string CFGFECBAJA = "CFGFECBAJA";
	 	 	 public const string CFGVALOR = "CFGVALOR";
	 	 	 public const string CFGSUBCLAVE = "CFGSUBCLAVE";
	 	 	 public const string CFGCLAVE = "CFGCLAVE";
	 	 }
	 	 public static class SIT_ADM_DIANOLABORAL_COL
	 	 {
	 	 	 public const string DIATIPO = "DIATIPO";
	 	 	 public const string DIACLAVE = "DIACLAVE";
	 	 }
	 	 public static class SIT_ADM_MODULO_COL
	 	 {
	 	 	 public const string MODMETODO = "MODMETODO";
	 	 	 public const string MODPADRE = "MODPADRE";
	 	 	 public const string MODFECBAJA = "MODFECBAJA";
	 	 	 public const string MODCONSECUTIVO = "MODCONSECUTIVO";
	 	 	 public const string MODCONTROL = "MODCONTROL";
	 	 	 public const string MODDESCRIPCION = "MODDESCRIPCION";
	 	 	 public const string MODCLAVE = "MODCLAVE";
	 	 }
	 	 public static class SIT_ADM_PERFIL_COL
	 	 {
	 	 	 public const string PERMULTIPLE = "PERMULTIPLE";
	 	 	 public const string PERSIGLA = "PERSIGLA";
	 	 	 public const string PERDESCRIPCION = "PERDESCRIPCION";
	 	 	 public const string PERFECBAJA = "PERFECBAJA";
	 	 	 public const string PERCLAVE = "PERCLAVE";
	 	 }
	 	 public static class SIT_ADM_PERFILCLASES_COL
	 	 {
	 	 	 public const string PERCLAVE = "PERCLAVE";
	 	 	 public const string CLACLAVE = "CLACLAVE";
	 	 }
	 	 public static class SIT_ADM_PERFILMOD_COL
	 	 {
	 	 	 public const string PERCLAVE = "PERCLAVE";
	 	 	 public const string MODCLAVE = "MODCLAVE";
	 	 }
	 	 public static class SIT_ADM_PERFILNODO_COL
	 	 {
	 	 	 public const string PERCLAVE = "PERCLAVE";
	 	 	 public const string NEDCLAVE = "NEDCLAVE";
	 	 }
	 	 public static class SIT_ADM_USRCOMPARTIR_COL
	 	 {
	 	 	 public const string COMUSR = "COMUSR";
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 }
	 	 public static class SIT_ADM_USRPERFIL_COL
	 	 {
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 	 public const string PERCLAVE = "PERCLAVE";
	 	 }
	 	 public static class SIT_ADM_USUARIO_COL
	 	 {
	 	 	 public const string USRACTIVO = "USRACTIVO";
	 	 	 public const string USRAUXCORREO = "USRAUXCORREO";
	 	 	 public const string USRFECMOD = "USRFECMOD";
	 	 	 public const string USRDESIGNACION = "USRDESIGNACION";
	 	 	 public const string USRTITULO = "USRTITULO";
	 	 	 public const string USRBLOQUEARFIN = "USRBLOQUEARFIN";
	 	 	 public const string USRINTENTOS = "USRINTENTOS";
	 	 	 public const string USREXTENSION = "USREXTENSION";
	 	 	 public const string USRCORREO = "USRCORREO";
	 	 	 public const string USRCONTRASEÑA = "USRCONTRASEÑA";
	 	 	 public const string USRFECBAJA = "USRFECBAJA";
	 	 	 public const string USRPUESTO = "USRPUESTO";
	 	 	 public const string USRMATERNO = "USRMATERNO";
	 	 	 public const string USRPATERNO = "USRPATERNO";
	 	 	 public const string USRNOMBRE = "USRNOMBRE";
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 }
	 	 public static class SIT_ADM_USUARIOAREA_COL
	 	 {
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 	 public const string UARORIGEN = "UARORIGEN";
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 }
	 	 public static class SIT_DOC_DOCUMENTO_COL
	 	 {
	 	 	 public const string DOCMD5 = "DOCMD5";
	 	 	 public const string DOCURL = "DOCURL";
	 	 	 public const string DOCFILESYSTEM = "DOCFILESYSTEM";
	 	 	 public const string DOCCLAVE = "DOCCLAVE";
	 	 	 public const string EXTCLAVE = "EXTCLAVE";
	 	 	 public const string DOCNOMBRE = "DOCNOMBRE";
	 	 	 public const string DOCFOLIO = "DOCFOLIO";
	 	 	 public const string DOCRUTA = "DOCRUTA";
	 	 	 public const string DOCTAMAÑO = "DOCTAMAÑO";
	 	 	 public const string DOCFECHA = "DOCFECHA";
	 	 }
	 	 public static class SIT_DOC_EXTENSION_COL
	 	 {
	 	 	 public const string EXTTERMINACION = "EXTTERMINACION";
	 	 	 public const string EXTMIMETYPE = "EXTMIMETYPE";
	 	 	 public const string EXTCLAVE = "EXTCLAVE";
	 	 }
	 	 public static class SIT_RED_AFD_COL
	 	 {
	 	 	 public const string AFDPREFIJO = "AFDPREFIJO";
	 	 	 public const string AFDFECBAJA = "AFDFECBAJA";
	 	 	 public const string AFDDESCRIPCION = "AFDDESCRIPCION";
	 	 	 public const string AFDCLAVE = "AFDCLAVE";
	 	 }
	 	 public static class SIT_RED_AFDFLUJO_COL
	 	 {
	 	 	 public const string RTPCLAVE = "RTPCLAVE";
	 	 	 public const string AFFORIGEN = "AFFORIGEN";
	 	 	 public const string AFDCLAVE = "AFDCLAVE";
	 	 	 public const string AFFDESTINO = "AFFDESTINO";
	 	 }
	 	 public static class SIT_RED_ARISTA_COL
	 	 {
	 	 	 public const string RTPCLAVE = "RTPCLAVE";
	 	 	 public const string PRCCLAVE = "PRCCLAVE";
	 	 	 public const string SOLCLAVE = "SOLCLAVE";
	 	 	 public const string ARIHITO = "ARIHITO";
	 	 	 public const string ARIDIASNAT = "ARIDIASNAT";
	 	 	 public const string ARIDIASLAB = "ARIDIASLAB";
	 	 	 public const string ARIFECENVIO = "ARIFECENVIO";
	 	 	 public const string ARICLAVE = "ARICLAVE";
	 	 	 public const string NODDESTINO = "NODDESTINO";
	 	 	 public const string NODORIGEN = "NODORIGEN";
	 	 }
	 	 public static class SIT_RED_NODO_COL
	 	 {
	 	 	 public const string PERCLAVE = "PERCLAVE";
	 	 	 public const string NODFECLECTURA = "NODFECLECTURA";
	 	 	 public const string NODUSRAUSENCIA = "NODUSRAUSENCIA";
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 	 public const string PRCCLAVE = "PRCCLAVE";
	 	 	 public const string SOLCLAVE = "SOLCLAVE";
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 	 public const string NODCAPA = "NODCAPA";
	 	 	 public const string NODATENDIDO = "NODATENDIDO";
	 	 	 public const string NEDCLAVE = "NEDCLAVE";
	 	 	 public const string NODFECCREACION = "NODFECCREACION";
	 	 	 public const string NODCLAVE = "NODCLAVE";
	 	 }
	 	 public static class SIT_RED_NODOESTADO_COL
	 	 {
	 	 	 public const string NEDTIPO = "NEDTIPO";
	 	 	 public const string NEDURL = "NEDURL";
	 	 	 public const string NEDDESCRIPCION = "NEDDESCRIPCION";
	 	 	 public const string NEDCLAVE = "NEDCLAVE";
	 	 }
	 	 public static class SIT_RED_NODORESP_COL
	 	 {
	 	 	 public const string SDOCLAVE = "SDOCLAVE";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 	 public const string NODCLAVE = "NODCLAVE";
	 	 }
	 	 public static class SIT_RESP_CLASINFO_COL
	 	 {
	 	 	 public const string NFODESCRIPCION = "NFODESCRIPCION";
	 	 	 public const string NFOCLAVE = "NFOCLAVE";
	 	 }
	 	 public static class SIT_RESP_COMITERUBRO_COL
	 	 {
	 	 	 public const string CORFECBAJA = "CORFECBAJA";
	 	 	 public const string CORDESCRIPCION = "CORDESCRIPCION";
	 	 	 public const string CORCLAVE = "CORCLAVE";
	 	 }
	 	 public static class SIT_RESP_DETALLE_COL
	 	 {
	 	 	 public const string DOCCLAVE = "DOCCLAVE";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 	 public const string DETDESCRIPCION = "DETDESCRIPCION";
	 	 	 public const string DETCLAVE = "DETCLAVE";
	 	 }
	 	 public static class SIT_RESP_ESTADO_COL
	 	 {
	 	 	 public const string SDODESCRIPCION = "SDODESCRIPCION";
	 	 	 public const string SDOCLAVE = "SDOCLAVE";
	 	 }
	 	 public static class SIT_RESP_GRAL_COL
	 	 {
	 	 	 public const string GRACONTENIDO = "GRACONTENIDO";
	 	 	 public const string RCCCLAVE = "RCCCLAVE";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 }
	 	 public static class SIT_RESP_INEXISTENCIA_COL
	 	 {
	 	 	 public const string INXCARGO = "INXCARGO";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 	 public const string INXRESPONSABLE = "INXRESPONSABLE";
	 	 }
	 	 public static class SIT_RESP_MOMENTO_COL
	 	 {
	 	 	 public const string MOMDESCRIPCION = "MOMDESCRIPCION";
	 	 	 public const string MOMCLAVE = "MOMCLAVE";
	 	 }
	 	 public static class SIT_RESP_REPRODUCCION_COL
	 	 {
	 	 	 public const string RCCDESCRIPCION = "RCCDESCRIPCION";
	 	 	 public const string RCCCLAVE = "RCCCLAVE";
	 	 }
	 	 public static class SIT_RESP_RESERVA_COL
	 	 {
	 	 	 public const string SDOCLAVE = "SDOCLAVE";
	 	 	 public const string RSVDIAS = "RSVDIAS";
	 	 	 public const string RSVMESES = "RSVMESES";
	 	 	 public const string RSVAÑOS = "RSVAÑOS";
	 	 	 public const string RSVEXPEDIENTE = "RSVEXPEDIENTE";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 	 public const string MOMCLAVE = "MOMCLAVE";
	 	 	 public const string TEMCLAVE = "TEMCLAVE";
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 	 public const string RSVTIPOCLASIF = "RSVTIPOCLASIF";
	 	 	 public const string RSVPLAZO = "RSVPLAZO";
	 	 	 public const string RSVFECFIN = "RSVFECFIN";
	 	 	 public const string RSVFECINI = "RSVFECINI";
	 	 	 public const string RSVTIPORESERVA = "RSVTIPORESERVA";
	 	 }
	 	 public static class SIT_RESP_RESPUESTA_COL
	 	 {
	 	 	 public const string REPCANTIDAD = "REPCANTIDAD";
	 	 	 public const string MEGCLAVE = "MEGCLAVE";
	 	 	 public const string DOCCLAVE = "DOCCLAVE";
	 	 	 public const string REPOFICIO = "REPOFICIO";
	 	 	 public const string REPEDOFEC = "REPEDOFEC";
	 	 	 public const string RTPCLAVE = "RTPCLAVE";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 }
	 	 public static class SIT_RESP_RREVISION_COL
	 	 {
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 }
	 	 public static class SIT_RESP_TEMA_COL
	 	 {
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 	 public const string TEMDESCRIPCION = "TEMDESCRIPCION";
	 	 	 public const string TEMCLAVE = "TEMCLAVE";
	 	 }
	 	 public static class SIT_RESP_TIPO_COL
	 	 {
	 	 	 public const string RTPPLAZO = "RTPPLAZO";
	 	 	 public const string RTPREPORTA = "RTPREPORTA";
	 	 	 public const string RTPTIPO = "RTPTIPO";
	 	 	 public const string RTPFORMATO = "RTPFORMATO";
	 	 	 public const string RTPFORMA = "RTPFORMA";
	 	 	 public const string RTPDESCRIPCION = "RTPDESCRIPCION";
	 	 	 public const string RTPCLAVE = "RTPCLAVE";
	 	 }
	 	 public static class SIT_RESP_TIPOINFO_COL
	 	 {
	 	 	 public const string NFOCLAVE = "NFOCLAVE";
	 	 	 public const string RTPCLAVE = "RTPCLAVE";
	 	 }
	 	 public static class SIT_RESP_TURNAR_COL
	 	 {
	 	 	 public const string ARACLAVE = "ARACLAVE";
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 	 public const string TURINSTRUCCION = "TURINSTRUCCION";
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 }
	 	 public static class SIT_SNT_ESTADO_COL
	 	 {
	 	 	 public const string PAICLAVE = "PAICLAVE";
	 	 	 public const string EDOFECBAJA = "EDOFECBAJA";
	 	 	 public const string EDODESCRIPCION = "EDODESCRIPCION";
	 	 	 public const string EDOCLAVE = "EDOCLAVE";
	 	 }
	 	 public static class SIT_SNT_MUNICIPIO_COL
	 	 {
	 	 	 public const string EDOCLAVE = "EDOCLAVE";
	 	 	 public const string PAICLAVE = "PAICLAVE";
	 	 	 public const string MUNFECBAJA = "MUNFECBAJA";
	 	 	 public const string MUNDESCRIPCION = "MUNDESCRIPCION";
	 	 	 public const string MUNCLAVE = "MUNCLAVE";
	 	 }
	 	 public static class SIT_SNT_OCUPACION_COL
	 	 {
	 	 	 public const string OCUFECBAJA = "OCUFECBAJA";
	 	 	 public const string OCUDESCRIPCION = "OCUDESCRIPCION";
	 	 	 public const string OCUCLAVE = "OCUCLAVE";
	 	 }
	 	 public static class SIT_SNT_PAIS_COL
	 	 {
	 	 	 public const string PAIFECBAJA = "PAIFECBAJA";
	 	 	 public const string PAIDESCRIPCION = "PAIDESCRIPCION";
	 	 	 public const string PAICLAVE = "PAICLAVE";
	 	 }
	 	 public static class SIT_SNT_SOLICITANTE_COL
	 	 {
	 	 	 public const string PAICLAVE = "PAICLAVE";
	 	 	 public const string OCUCLAVE = "OCUCLAVE";
	 	 	 public const string TSLCLAVE = "TSLCLAVE";
	 	 	 public const string SNTOTRONIVELEDU = "SNTOTRONIVELEDU";
	 	 	 public const string SNTOTRAOCUP = "SNTOTRAOCUP";
	 	 	 public const string SNTNIVEDUC = "SNTNIVEDUC";
	 	 	 public const string SNTREPLEG = "SNTREPLEG";
	 	 	 public const string MUNCLAVE = "MUNCLAVE";
	 	 	 public const string EDOCLAVE = "EDOCLAVE";
	 	 	 public const string SNTUSUARIO = "SNTUSUARIO";
	 	 	 public const string SNTFECNAC = "SNTFECNAC";
	 	 	 public const string SNTSEXO = "SNTSEXO";
	 	 	 public const string SNTCIUDADEXT = "SNTCIUDADEXT";
	 	 	 public const string SNTEDOEXT = "SNTEDOEXT";
	 	 	 public const string SNTCORELE = "SNTCORELE";
	 	 	 public const string SNTTEL = "SNTTEL";
	 	 	 public const string SNTCODPOS = "SNTCODPOS";
	 	 	 public const string SNTCOL = "SNTCOL";
	 	 	 public const string SNTNUMINT = "SNTNUMINT";
	 	 	 public const string SNTNUMEXT = "SNTNUMEXT";
	 	 	 public const string SNTCALLE = "SNTCALLE";
	 	 	 public const string SNTCURP = "SNTCURP";
	 	 	 public const string SNTNOMBRE = "SNTNOMBRE";
	 	 	 public const string SNTAPEMAT = "SNTAPEMAT";
	 	 	 public const string SNTAPEPAT = "SNTAPEPAT";
	 	 	 public const string SNTRFC = "SNTRFC";
	 	 	 public const string SNTCLAVE = "SNTCLAVE";
	 	 }
	 	 public static class SIT_SNT_SOLICITANTETIPO_COL
	 	 {
	 	 	 public const string TSLDESCRIPCION = "TSLDESCRIPCION";
	 	 	 public const string TSLCLAVE = "TSLCLAVE";
	 	 }
	 	 public static class SIT_SOL_DOC_COL
	 	 {
	 	 	 public const string DOCCLAVE = "DOCCLAVE";
	 	 	 public const string SOLCLAVE = "SOLCLAVE";
	 	 }
	 	 public static class SIT_SOL_MEDIOENTRADA_COL
	 	 {
	 	 	 public const string METFECBAJA = "METFECBAJA";
	 	 	 public const string METDESCRIPCION = "METDESCRIPCION";
	 	 	 public const string METCLAVE = "METCLAVE";
	 	 }
	 	 public static class SIT_SOL_MODOENTREGA_COL
	 	 {
	 	 	 public const string MEGMOSTRAR = "MEGMOSTRAR";
	 	 	 public const string MEGDESCRIPCION = "MEGDESCRIPCION";
	 	 	 public const string MEGCLAVE = "MEGCLAVE";
	 	 }
	 	 public static class SIT_SOL_PROCESO_COL
	 	 {
	 	 	 public const string PRCDESCRIPCION = "PRCDESCRIPCION";
	 	 	 public const string PRCCLAVE = "PRCCLAVE";
	 	 }
	 	 public static class SIT_SOL_PROCESOPLAZOS_COL
	 	 {
	 	 	 public const string PCZCLAVE = "PCZCLAVE";
	 	 	 public const string SOTCLAVE = "SOTCLAVE";
	 	 	 public const string PCZAMARILLO = "PCZAMARILLO";
	 	 	 public const string PCZVERDE = "PCZVERDE";
	 	 	 public const string PCZPLAZO = "PCZPLAZO";
	 	 	 public const string PRCCLAVE = "PRCCLAVE";
	 	 }
	 	 public static class SIT_SOL_SEGUIMIENTO_COL
	 	 {
	 	 	 public const string REPCLAVE = "REPCLAVE";
	 	 	 public const string USRCLAVE = "USRCLAVE";
	 	 	 public const string SEGFECESTIMADA = "SEGFECESTIMADA";
	 	 	 public const string SEGULTIMONODO = "SEGULTIMONODO";
	 	 	 public const string AFDCLAVE = "AFDCLAVE";
	 	 	 public const string SEGEDOPROCESO = "SEGEDOPROCESO";
	 	 	 public const string PRCCLAVE = "PRCCLAVE";
	 	 	 public const string SEGFECCALCULO = "SEGFECCALCULO";
	 	 	 public const string SEGDIASNOLAB = "SEGDIASNOLAB";
	 	 	 public const string SEGMULTIPLE = "SEGMULTIPLE";
	 	 	 public const string SEGFECFIN = "SEGFECFIN";
	 	 	 public const string SEGFECAMP = "SEGFECAMP";
	 	 	 public const string SEGSEMAFOROCOLOR = "SEGSEMAFOROCOLOR";
	 	 	 public const string SEGDIASSEMAFORO = "SEGDIASSEMAFORO";
	 	 	 public const string SEGFECINI = "SEGFECINI";
	 	 	 public const string SOLCLAVE = "SOLCLAVE";
	 	 }
	 	 public static class SIT_SOL_SOLICITUD_COL
	 	 {
	 	 	 public const string PRCCLAVE = "PRCCLAVE";
	 	 	 public const string SOLFECRECREV = "SOLFECRECREV";
	 	 	 public const string SOLFECACL = "SOLFECACL";
	 	 	 public const string MEGCLAVE = "MEGCLAVE";
	 	 	 public const string SNTCLAVE = "SNTCLAVE";
	 	 	 public const string SOTCLAVE = "SOTCLAVE";
	 	 	 public const string SOLNOTIFICADO = "SOLNOTIFICADO";
	 	 	 public const string SOLMOTDESECHA = "SOLMOTDESECHA";
	 	 	 public const string SOLRESPCLAVE = "SOLRESPCLAVE";
	 	 	 public const string METCLAVE = "METCLAVE";
	 	 	 public const string SOLOTRODERACC = "SOLOTRODERACC";
	 	 	 public const string SOLFECRESP = "SOLFECRESP";
	 	 	 public const string SOLFECENV = "SOLFECENV";
	 	 	 public const string SOLFECENT = "SOLFECENT";
	 	 	 public const string SOLFECSOL = "SOLFECSOL";
	 	 	 public const string SOLDAT = "SOLDAT";
	 	 	 public const string SOLDES = "SOLDES";
	 	 	 public const string SOLARCDES = "SOLARCDES";
	 	 	 public const string SOLOTROMOD = "SOLOTROMOD";
	 	 	 public const string SOLFECREC = "SOLFECREC";
	 	 	 public const string SOLCLAVE = "SOLCLAVE";
	 	 	 public const string SOLFOLIO = "SOLFOLIO";
	 	 }
	 	 public static class SIT_SOL_SOLICITUDTIPO_COL
	 	 {
	 	 	 public const string SOTDESCRIPCION = "SOTDESCRIPCION";
	 	 	 public const string SOTCLAVE = "SOTCLAVE";
	 	 }
 
	 }
}
