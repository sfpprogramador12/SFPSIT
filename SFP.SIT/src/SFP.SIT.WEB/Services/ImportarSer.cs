using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using SFP.SIT.AFD.Core;
using SFP.Persistencia.Util;
using SFP.SIT.SERV.Util;
using SFP.SIT.SERV.Model.IMP;
using SFP.SIT.SERV.Model.DOC;

namespace SFP.SIT.WEB.Services
{
    public class ImportarSer 
    {
        private int _iUsuario;
        private int _iFlujoTrabajo;
        private int _iVersion;

        public const int COL_INDEX = 0;
        public const int COL_DESCRP = 1;
        public const string COL_DESCRP_MSJ = "Valor omitido";
        public const string SOLICITUD_FORMATO_FECHA_DDMMYYYY = "dd/MM/yyyy";
        public const string SOLICITUD_FORMATO_FECHA_DMMYYYY = "d/MM/yyyy";

        private Dictionary<string, SIT_DOC_EXTENSION> _dicTipoExt;
        private Dictionary<int, String> _dicEstados;
        private Dictionary<int, String> _dicModoEntrega;
        private Dictionary<int, String> _dicMunicipios;
        private Dictionary<int, String> _dicOcupaciones;
        private Dictionary<int, String> _dicPaises;
        private Dictionary<int, String> _dicTipoSolicitante;
        String _ClaveInstitucion;
        private Dictionary<Int64, char> _dicDiaNoLaboral;
        private Dictionary<int, String> _dicTipoRespuesta;
        private Dictionary<int, String> _dicRespTipoInfo;

        private Dictionary<long, object> dicRegistros;

        public ImportarSer(int iUsuario, int iFlujoTrabajo,
            Dictionary<string, SIT_DOC_EXTENSION> dicTipoExt, Dictionary<int, String> dicEstados, Dictionary<int, String> dicModoEntrega, Dictionary<int, String> dicMunicipios,
            Dictionary<int, String> dicOcupaciones, Dictionary<int, String> dicPaises, Dictionary<int, String> dicTipoSolicitante, String ClaveInstitucion,
            Dictionary<Int64, char> dicDiaNoLaboral, Dictionary<int, String> dicTipoRespuesta, Dictionary<int, String> dicRespTipoInfo) 
        {
            _iUsuario = iUsuario;
            _iFlujoTrabajo = iFlujoTrabajo;
            _dicTipoExt = dicTipoExt;
            _dicEstados = dicEstados;
            _dicModoEntrega = dicModoEntrega;
            _dicMunicipios = dicMunicipios;
            _dicOcupaciones = dicOcupaciones;
            _dicPaises = dicPaises;
            _dicTipoSolicitante = dicTipoSolicitante;
            _ClaveInstitucion = ClaveInstitucion;
            _dicDiaNoLaboral = dicDiaNoLaboral;
            _dicTipoRespuesta = dicTipoRespuesta;
            _dicRespTipoInfo = dicRespTipoInfo;
        }

        public SERV.Model.DOC.FrmImportarMdl Importar(ZipArchive zipArchivo, String sRuta, String sNombre, Boolean bVistaPrevia)
        {
            Encoding tipoEncode;
            Stream unzippedEntryStream;         //Unzipped data from a file in the archive
            int iExtension = 0;
            FrmImportarMdl impMdl = new FrmImportarMdl();
            impMdl.dicArchivoDatos = new Dictionary<string, DocContenidoMdl>();
            impMdl.RutaServidor = sRuta;
            
            try
            {
                using (MD5 md5Hash = MD5.Create())
                {
                    foreach (ZipArchiveEntry entry in zipArchivo.Entries)
                    {
                        if (entry.Name != "")
                            impMdl.dicArchivoDatos.Add(entry.Name, null);

                        // buscar el tipo de extension...
                        string sExtension = entry.Name.Substring(entry.Name.LastIndexOf(".") + 1).ToUpper();
                        if (sExtension.Length > 0)
                        {
                            unzippedEntryStream = entry.Open();

                            using (MemoryStream ms = new MemoryStream())
                            {
                                unzippedEntryStream.CopyTo(ms);

                                if (sExtension == "TXT")
                                {
                                    tipoEncode = TextoUtil.detectTextEncoding(ms.ToArray());
                                    ms.Seek(0, SeekOrigin.Begin);
                                    using (StreamReader reader = new StreamReader(ms, tipoEncode, false))
                                    {
                                        procesarArchivo(reader, entry.Name);
                                    }
                                }
                                else
                                {
                                    if (bVistaPrevia==false)
                                    {

                                        if (_dicTipoExt.ContainsKey(sExtension))
                                        {
                                            SIT_DOC_EXTENSION docTipoExtMdl = _dicTipoExt[sExtension];
                                            iExtension = docTipoExtMdl.extclave;
                                        }
                                        else
                                        { 
                                            iExtension = 0;
                                        }

                                        String sMD5 = md5Hash.ComputeHash(unzippedEntryStream).ToString();

                                        DocContenidoMdl docDatosMDl = new DocContenidoMdl(
                                            0, DateTime.Now, "", entry.Name, ms.Length, sRuta,
                                            iExtension, DateTime.Now, "", sMD5 );

                                        docDatosMDl.doc_contenido = ms.ToArray();
                                        impMdl.dicArchivoDatos[entry.Name] =  docDatosMDl;
                                    }
                                }
                            }
                        }
                    }
                }
                impMdl.RutaCarpeta = sRuta;
                impMdl.ArchivoZip = sNombre;
                impMdl.iVersion = _iVersion;
                impMdl.dicRegistros = dicRegistros;                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return impMdl;
        }

        /**************************
            RUTINAS PARA IMPORTAR 
        **************************/
        private String[] ObtenerTotalCampos(StreamReader reader)
        {
            string sRegistro;
            sRegistro = reader.ReadLine();
            String[] sa = sRegistro.Split('|');
            return sa;
        }
        private void parseaArchivoResp(StreamReader reader, String[] asInicio)
        {
            Int64 iFolio;
            if (Convert.ToInt32(asInicio[1]) == Constantes.Respuesta.RECEPCION_INFO_ADICIONAL)
                validarRegInfoResp(ref asInicio);

            String line;
            while ((line = reader.ReadLine()) != null)
            {
                String[] asCampos = line.Split('\\', '|');

                iFolio = Convert.ToInt64(asCampos[0]);

                if (Convert.ToInt32(asCampos[1]) == Constantes.Respuesta.RECEPCION_INFO_ADICIONAL
                    && dicRegistros.ContainsKey(iFolio) == false)
                    validarRegInfoResp(ref asCampos);
            }
        }

        private void parseaArchivoSolV2(StreamReader reader, String[] asInicio)
        {
            Int64 iFolio;

            iFolio = Convert.ToInt64(asInicio[0]);
            validarRegistroInfomex2(ref asInicio, ref iFolio);

            while (!reader.EndOfStream)
            {
                String[] asCampos = reader.ReadLine().Split('\\', '|');
                iFolio = Convert.ToInt64(asCampos[0]);

                if (dicRegistros.ContainsKey(iFolio) == false)
                    validarRegistroInfomex2(ref asCampos, ref iFolio);
            }
        }

        public void procesarArchivo(StreamReader reader, String sNombre)
        {
            int iVersionInfomex;
            String[] asCampos;

            try
            {
                asCampos = ObtenerTotalCampos(reader);
                iVersionInfomex = asCampos.Length;

                dicRegistros = new Dictionary<long, object>();

                if (iVersionInfomex == Constantes.Infomex.ARCHIVO_CAMPOS_ACLARACION)
                {
                    _iVersion = Constantes.Infomex.ARCHIVO_ACLARACION;
                    parseaArchivoResp(reader, asCampos);
                }
                else if (iVersionInfomex == Constantes.Infomex.ARCHIVO_CAMPOS_SOLICITUD_VER2)
                {
                    _iVersion = Constantes.Infomex.ARCHIVO_SOLICITUD_VER2;
                    parseaArchivoSolV2(reader, asCampos);
                }
                else if (iVersionInfomex == Constantes.Infomex.ARCHIVO_CAMPOS_SOLICITUD_VER3)
                {
                    _iVersion = Constantes.Infomex.ARCHIVO_SOLICITUD_VER3;
                }
                else
                {
                    _iVersion = Constantes.Infomex.ARCHIVO_ERROR;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }

        /* *******************************
        **********   R U T I N A S
        **********************************   */
        private void parseaFolio(ref ImpInfomexArchivoSolMdl sRegistro, string sFolio)
        {
            if (sFolio == null)
            {
                sRegistro.solclave = -1;
                sRegistro.solfolio = "-1";
                sRegistro.Mensaje += "Error falta número de folio";
                sRegistro.Error = 1;
            }
            else if (sFolio.Length > 13)
            {
                sRegistro.solclave = -1;
                sRegistro.solfolio = sFolio;
                sRegistro.Mensaje += "Error en la longitud del número de folio";
                sRegistro.Error = 1;
            }
            else
            {
                sRegistro.solclave = Convert.ToInt64(sFolio);
                sRegistro.solfolio = sFolio;
            }
        }

        private void parseaUnidadEnlace(ref ImpInfomexArchivoSolMdl sRegistro, String sUenlace)
        {
            if (sUenlace == null)
            {
                sRegistro.kue_claenl = -1;
                sRegistro.UnidadEnlace = COL_DESCRP_MSJ;
                sRegistro.Mensaje += "Error en la unidad de enlace";
                sRegistro.Error = 1;
            }
            else
            {
                if ( Convert.ToInt32( _ClaveInstitucion) == Convert.ToInt32(sUenlace))
                    sRegistro.UnidadEnlace = sUenlace;
                else
                {
                    sRegistro.UnidadEnlace = "No existe la clave de unidad de enlace en los catálogos del sistema  \r\n";
                    sRegistro.Mensaje += "Error en la unidad de enlace";
                    sRegistro.Error = 1;
                }
            }
        }
        private void parseaEntidadFederativa(ref ImpInfomexArchivoSolMdl sRegistro, String estado)
        {
            if (estado == null)
            {
                sRegistro.edoclave = -1;
                sRegistro.EstidadFederativa = COL_DESCRP_MSJ;
                sRegistro.Mensaje += "Error en la unidad de enlace";
                sRegistro.Error = 1;
            }
            else
            {
                sRegistro.edoclave = Convert.ToInt32(estado);
                if (_dicEstados.ContainsKey(sRegistro.edoclave) == true)
                    sRegistro.EstidadFederativa = _dicEstados[sRegistro.edoclave];
                else
                {
                    sRegistro.EstidadFederativa = "No existe la clave de la Entidad Federativa en los catálogos del sistema \r\n";
                    sRegistro.Mensaje += "Error en la entidad federativa";
                    sRegistro.Error = 1;
                }
            }
        }
        private void parseaMunicipio(ref ImpInfomexArchivoSolMdl sRegistro, String municipio, String Estado)
        {
            if (municipio == null)
            {
                sRegistro.munclave = -1;
                sRegistro.Municipio = COL_DESCRP_MSJ;
                sRegistro.Mensaje += "Error en el municipio o delegacion";
                sRegistro.Error = 1;
            }
            else
            {
                sRegistro.munclave = Convert.ToInt32(Estado + municipio);

                if (_dicMunicipios.ContainsKey(sRegistro.munclave) == true)
                    sRegistro.Municipio = _dicMunicipios[sRegistro.munclave];
                else
                {
                    // SE hace una primera correcion para ver si el estado es el correcto
                    sRegistro.munclave = Convert.ToInt32(Estado + "000");
                    if (_dicMunicipios.ContainsKey(sRegistro.munclave) == true)
                        sRegistro.Municipio = _dicMunicipios[sRegistro.munclave];
                    else
                    {
                        sRegistro.Municipio = "No existe la clave del municipio  \r\n";
                        sRegistro.Mensaje += "Error en el municipio o delegacion";
                        sRegistro.Error = 1;
                    }
                }
            }
        }
        private void parseaTipoUsuario(ref ImpInfomexArchivoSolMdl sRegistro, String sTipoUsuario)
        {
            if (sTipoUsuario == null)
            {
                sRegistro.idtipo = -1;
                sRegistro.TipoUsuario = COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.idtipo = Convert.ToInt32(sTipoUsuario);

                if (_dicTipoSolicitante.ContainsKey(sRegistro.idtipo) == true)
                    sRegistro.TipoUsuario = _dicTipoSolicitante[sRegistro.idtipo];
                else
                {
                    sRegistro.TipoUsuario = COL_DESCRP_MSJ;
                    sRegistro.Mensaje += COL_DESCRP_MSJ;
                }
            }
        }
        private void parseaFechaSolicitud(ref ImpInfomexArchivoSolMdl sRegistro, string sFechaRecepcion)
        {
            if (sFechaRecepcion == null)
            {
                sRegistro.solfecsol = new DateTime();
                sRegistro.Mensaje += COL_DESCRP_MSJ;
            }
            else
            {
                if (sFechaRecepcion.Length == 10 )
                    sRegistro.solfecsol = getFecha(sFechaRecepcion, SOLICITUD_FORMATO_FECHA_DDMMYYYY);
                else
                    sRegistro.solfecsol = getFecha(sFechaRecepcion, SOLICITUD_FORMATO_FECHA_DMMYYYY);
            }
        }

        private void parseaFechaNacimiento(ref ImpInfomexArchivoSolMdl sRegistro, String fechaNacimiento)
        {
            if (fechaNacimiento == null)
            {
                sRegistro.sntfecnac = new DateTime();
                sRegistro.Mensaje += COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.sntfecnac = getFecha(fechaNacimiento, SOLICITUD_FORMATO_FECHA_DDMMYYYY);
                if (fechaNacimiento.Length == 10)
                    sRegistro.sntfecnac = getFecha(fechaNacimiento, SOLICITUD_FORMATO_FECHA_DDMMYYYY);
                else
                    sRegistro.sntfecnac = getFecha(fechaNacimiento, SOLICITUD_FORMATO_FECHA_DMMYYYY);
            }
        }
        private string validar(int longitud, string sDato)
        {
            if (sDato != null)
            {
                if (sDato != "NULL")
                    if (sDato.Length > longitud)
                        return sDato.Substring(0, longitud);
                    else
                        return sDato;
                else
                    return null;
            }
            else
                return sDato;
        }
        private void parseaPais(ref ImpInfomexArchivoSolMdl sRegistro, String pais)
        {
            if (pais == null)
            {
                sRegistro.paiclave = -1;
                sRegistro.Pais = COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.paiclave = Convert.ToInt32(pais);

                if (_dicPaises.ContainsKey(sRegistro.paiclave) == true)
                    sRegistro.Pais = _dicPaises[sRegistro.paiclave];
                else
                {
                    sRegistro.Pais = "No existe la clave de pais en los catálogos del sistema  \r\n";
                    sRegistro.Mensaje += COL_DESCRP_MSJ;
                    sRegistro.Error = 1;
                }
            }
        }
        private void parseaOcupacion(ref ImpInfomexArchivoSolMdl sRegistro, String ocupacion)
        {
            if (ocupacion == null)
            {
                sRegistro.ocuClave = -1;
                sRegistro.Ocupacion = COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.ocuClave = Convert.ToInt32(ocupacion);
                if (_dicOcupaciones.ContainsKey(sRegistro.ocuClave) == true)
                    sRegistro.Ocupacion = _dicOcupaciones[sRegistro.ocuClave];
                else
                {
                    sRegistro.Ocupacion = "No existe la clave de ocupacion en los catálogos del sistema  \r\n";
                    sRegistro.Mensaje += COL_DESCRP_MSJ;
                    sRegistro.Error = 1;
                }
            }
        }

        private void parseaModalidadEntrega(ref ImpInfomexArchivoSolMdl sRegistro, String modalidadEntrega)
        {
            if (modalidadEntrega == null)
            {
                sRegistro.km_clamod = -1;
                sRegistro.ModoEntrega = COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.km_clamod = Convert.ToInt32(modalidadEntrega);


                if (_dicModoEntrega.ContainsKey(sRegistro.km_clamod))
                    sRegistro.ModoEntrega = _dicModoEntrega[sRegistro.km_clamod];
                else
                {
                    sRegistro.ModoEntrega = "No existe la clave de modalidad de entrega en los catálogos del sistema  \r\n";
                    sRegistro.Mensaje += COL_DESCRP_MSJ;
                    sRegistro.Error = 1;
                }
            }
        }

        private void parseaRespuesta(ref ImpInfomexArchivoAclMdl sRegistro, String respuesta)
        {
            if (respuesta == null)
            {
                sRegistro.clarespuesta = -1;
                sRegistro.res_descripcion = COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.clarespuesta = Convert.ToInt32(respuesta);

                if (_dicTipoRespuesta.ContainsKey(sRegistro.clarespuesta))
                    sRegistro.res_descripcion = _dicTipoRespuesta[sRegistro.clarespuesta];
                else
                {
                    sRegistro.res_descripcion = COL_DESCRP_MSJ; ;
                    sRegistro.Mensaje = "No existe la clave de respuesta en el catálogo del sistema \r\n";
                    sRegistro.Error = 1;
                }
            }
        }

        private void parseaTipoInfo(ref ImpInfomexArchivoAclMdl sRegistro, String tipoInfo)
        {
            if (tipoInfo == null)
            {
                sRegistro.clatipo_info = 0;
                sRegistro.tpi_informacion = COL_DESCRP_MSJ;
            }
            else
            {
                sRegistro.clatipo_info = Convert.ToInt32(tipoInfo);

                if (_dicRespTipoInfo.ContainsKey(sRegistro.clatipo_info) == true)
                    sRegistro.tpi_informacion = _dicRespTipoInfo[sRegistro.clatipo_info];
                else
                {
                    sRegistro.Mensaje = sRegistro.Mensaje + " No existe la clave de tipo de información en el catálogo del sistema \r\n";
                    sRegistro.tpi_informacion = COL_DESCRP_MSJ;
                    sRegistro.Error = 1;
                }
            }
        }
        private DateTime getFecha(String sCampo, String sFormato)
        {
            DateTime parsedDate;

            if (DateTime.TryParseExact(sCampo, sFormato, null, DateTimeStyles.None, out parsedDate) == false)
                return new DateTime();

            return parsedDate;
        }
        private void validarRegInfoResp(ref String[] campos)
        {
            ImpInfomexArchivoAclMdl infoRespDto = new ImpInfomexArchivoAclMdl();
            infoRespDto.solclave = Convert.ToInt64(campos[0]);
            parseaRespuesta(ref infoRespDto, campos[1]);

            if (campos[2].Length == 10)
                infoRespDto.fecha_respuesta = getFecha(campos[2], SOLICITUD_FORMATO_FECHA_DDMMYYYY);
            else
                infoRespDto.fecha_respuesta = getFecha(campos[2], SOLICITUD_FORMATO_FECHA_DMMYYYY);

            parseaTipoInfo(ref infoRespDto, campos[3]);
            infoRespDto.descripcion = validar(4000, campos[4].Replace("\"","'"));
            infoRespDto.archivo_respuesta = validar(4000, campos[5]);

            dicRegistros.Add(infoRespDto.solclave, infoRespDto);
        }

        private void validarRegistroInfomex2(ref String[] campos, ref long iID)
        {
            ImpInfomexArchivoSolMdl solicitudImpDto = new ImpInfomexArchivoSolMdl();

            parseaFolio(ref solicitudImpDto, campos[0]);
            parseaUnidadEnlace(ref solicitudImpDto, campos[1]);
            parseaTipoUsuario(ref solicitudImpDto, campos[2]);
            parseaFechaSolicitud(ref solicitudImpDto, campos[3]);

            // ES CORECTO QUE ESTOS DATOS PUEDNE VENIR EN BLANCO
            solicitudImpDto.sntrepleg = validar(300, campos[4]);
            solicitudImpDto.sntrfc = validar(13, campos[5]);
            solicitudImpDto.sntapepat = validar(255, campos[6]);
            solicitudImpDto.sntapemat = validar(255, campos[7]);
            solicitudImpDto.sntnombre = validar(255, campos[8]);
            solicitudImpDto.sntcurp = validar(18, campos[9]);
            solicitudImpDto.sntcalle = validar(300, campos[10]);
            solicitudImpDto.sntnumext = validar(300, campos[11]);
            solicitudImpDto.sntnumint = validar(100, campos[12]);
            solicitudImpDto.sntcol = validar(255, campos[13]);
            parseaEntidadFederativa(ref solicitudImpDto, campos[14]);
            parseaMunicipio(ref solicitudImpDto, campos[15], campos[14]);
            solicitudImpDto.sntcodpos = validar(5, campos[16]);
            solicitudImpDto.snttel = validar(50, campos[17]);
            solicitudImpDto.sntcorele = validar(255, campos[18]);
            parseaPais(ref solicitudImpDto, campos[19]);
            solicitudImpDto.sntedoext = validar(255, campos[20]);
            solicitudImpDto.sntciudadext = validar(255, campos[21]);
            solicitudImpDto.sntsexo = validar(255, campos[22]);
            parseaFechaNacimiento(ref solicitudImpDto, campos[23]);
            parseaOcupacion(ref solicitudImpDto, campos[24]);
            parseaModalidadEntrega(ref solicitudImpDto, campos[25]);
            solicitudImpDto.solotromod = validar(255, campos[26].Replace("\"", "'"));
            solicitudImpDto.solarcdes = validar(4000, campos[27].Replace("\"", "'"));
            solicitudImpDto.soldes = validar(4000, campos[28].Replace("\"", "'"));
            solicitudImpDto.soldat = validar(4000, campos[29].Replace("\"", "'"));

            // El valor por omisión siempre es del tipo de solicitud debido que esta versión del archivo nose ucenta con un discriminante
            // En este caso es el mism ovalor que el proceso
            solicitudImpDto.idtiposol = Constantes.ProcesoTipo.SOLICITUD;
            solicitudImpDto.solfecrec = DateTime.Now;

            dicRegistros.Add(iID, solicitudImpDto);
        }

    }
}
