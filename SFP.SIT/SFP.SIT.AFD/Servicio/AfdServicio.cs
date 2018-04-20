using SFP.Persistencia;
using SFP.SIT.AFD.Model;
using SFP.SIT.AFD.Servicio;
using SFP.SIT.SERV.Dao;
using SFP.SIT.SERV.Dao.RED;
using SFP.SIT.SERV.Model.DOC;
using SFP.SIT.SERV.Model.RED;
using SFP.SIT.SERV.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SFP.SIT.AFD.Core
{
    public class AfdServicio : BaseFunc
    {        
        public const Int32 OPE_INICIALIZAR = 1;
        //////public const Int32 OPE_IMPORTAR_SOLICITUD = 2;
        //////public const Int32 OPE_IMPORTAR_ACLARACION = 3;
        public const string OPE_ACCION = "Accion";
        public const string OPE_TURNAR = "Turnar";

        public const String PARAM_FLUJO_TRABAJO_ID = "FlujoTrabajoID";
        public const String PARAM_FRM_IMPORTAR_MDL = "FrmImportarMdl";

        //////public const String PARAM_AFD_EDO_DATA_MDL = "AfdEdoDataMdl";
        ////////public const String PARAM_FLUJO_TRABAJO_DIC = "FlujoTrabajoDic";
        public const String PARAM_OPERACION = "Operacion";


        public const Int32 RES_FLUJO_TRABAJO_NODOS= 1;
        public const Int32 RES_FLUJO_TRABAJO_ARISTAS = 2;
        //public Dictionary<int, Delegate> dicOperacion = new Dictionary<int, Delegate>();

        public AfdServicio(DbConnection connection, DbTransaction oraTransaction, String sDataADapter) :
            base(connection, oraTransaction, sDataADapter)
        {
            ////dicOperacion[OPE_INICIALIZAR] = new Func<Object, Object>(Inicializar);
            ////////////dicOperacion[OPE_IMPORTAR_ACLARACION] = new Func<Object, Object>(ImportarAclaracion);
            
            ////dicOperacion[OPE_ACCION] = new Func<Object, Object>(Accion);
            ////dicOperacion[OPE_TURNAR] = new Func<Object, Object>(Turnar);
        }


        ////////public object EjecutarOperacion(Object oDatos)
        ////////{
        ////////Dictionary<String, Object> dicParam = (Dictionary<String, Object>)oDatos;
           
        ////////return dicOperacion[(int)dicParam[PARAM_OPERACION]].DynamicInvoke(oDatos);
        ////////}


        public Object Inicializar(Object oDatos)
        {
            // Trae los datos del tipo de flujo 
            Dictionary <String, Object> dicParam = (Dictionary<String, Object>)oDatos;

            Int32 iFlujoTrabajoID = (Int32)dicParam[PARAM_FLUJO_TRABAJO_ID];

            // Inicializar los PARAMETRO SDE BUSQUEDA
            dicParam.Add( DButil.SIT_RED_AFDFLUJO_COL.AFDCLAVE, iFlujoTrabajoID);
            dicParam.Add( DButil.SIT_RED_AFDFLUJO_COL.AFFORIGEN, Constantes.NodoEstado.INAI_SOLICITUD); // SIEMPRE ES UNO Y ES CREAR SOLICITUD..


            SIT_RED_AFDFLUJODao SIT_RED_AFDFLUJODao = new SIT_RED_AFDFLUJODao(_cn, _transaction, _sDataAdapter);
            // FALTA METER TODOS LOS ESTADOS
            Dictionary<Int32, AfdNodoFlujo> dicAfdFlujoTrabajo = new Dictionary<Int32, AfdNodoFlujo>();
            
            List<SIT_RED_NODOESTADO> lstRedNodoEdoMdl = (List<SIT_RED_NODOESTADO>) new SIT_RED_NODOESTADODao(_cn, _transaction, _sDataAdapter).dmlSelectTabla();
            foreach (SIT_RED_NODOESTADO redNodoEdoMdl in lstRedNodoEdoMdl)
            {
                AfdNodoFlujo afdEdoPdo = new AfdNodoFlujo(nodclave: redNodoEdoMdl.nedclave, nodDescripcion: redNodoEdoMdl.neddescripcion, 
                        nedurl: redNodoEdoMdl.nedurl, nedtipo: redNodoEdoMdl.nedtipo);
                    
                // AQUI CREO LA CLASE
                String sClase = "SFP.SIT.AFD.WF" + iFlujoTrabajoID + "." + Constantes.NodoEstado.PREFIJO + redNodoEdoMdl.nedurl + iFlujoTrabajoID;
                Type type = Type.GetType(sClase);
                if (type == null)
                    afdEdoPdo.clase = "SFP.SIT.AFD.Servicio.AfdNodoBase";
                else
                    afdEdoPdo.clase = sClase;

                afdEdoPdo.DicAristaPlazo = new Dictionary<int, AfdEdoPdoMdl>();
                afdEdoPdo.dicAccionEstado = new Dictionary<int, int>();

                dicParam[DButil.SIT_RED_AFDFLUJO_COL.AFFORIGEN] = redNodoEdoMdl.nedclave;
                DataTable dtEdoPdo = (DataTable)SIT_RED_AFDFLUJODao.dmlSelectProdNodo(dicParam);

                for (Int32 iIdxProd = 0; iIdxProd < dtEdoPdo.Rows.Count; iIdxProd++)
                {
                    AfdEdoPdoMdl edoPdoMdl = new AfdEdoPdoMdl(  );
                    edoPdoMdl.id = Convert.ToInt32(dtEdoPdo.Rows[iIdxProd]["rtpclave"]);
                    edoPdoMdl.text = dtEdoPdo.Rows[iIdxProd]["rtpdescripcion"].ToString();
                    edoPdoMdl.estadoFinal = Convert.ToInt32(dtEdoPdo.Rows[iIdxProd]["affdestino"]);
                    edoPdoMdl.forma = dtEdoPdo.Rows[iIdxProd]["rtpforma"].ToString();
                    edoPdoMdl.plazo = Convert.ToInt32(dtEdoPdo.Rows[iIdxProd]["rtpPlazo"]);

                    if (dtEdoPdo.Rows[iIdxProd]["rtpTipo"].ToString() == "")
                        edoPdoMdl.nivel = 0;
                    else
                        edoPdoMdl.nivel = Convert.ToInt32(dtEdoPdo.Rows[iIdxProd]["rtpTipo"]);

                    edoPdoMdl.formato = dtEdoPdo.Rows[iIdxProd]["rtpformato"].ToString();

                    afdEdoPdo.DicAristaPlazo.Add(edoPdoMdl.id, edoPdoMdl);
                    afdEdoPdo.dicAccionEstado.Add(edoPdoMdl.id, edoPdoMdl.estadoFinal);
                }

                dicAfdFlujoTrabajo.Add(afdEdoPdo.nedclave, afdEdoPdo);
            }
            return dicAfdFlujoTrabajo;
        }

        public Object Accion(AfdEdoDataMdl afdDataMdl)
        {
            Type type = Type.GetType(afdDataMdl.dicAfdFlujo[afdDataMdl.ID_EstadoActual].clase);
            Object objNodo = Activator.CreateInstance(type, _cn, _transaction, _sDataAdapter);
            AfdNodoBase oNodoBase = (AfdNodoBase)objNodo;
            oNodoBase.AsignarID(afdDataMdl.ID_EstadoActual);

            return objNodo.GetType().GetMethod("Accion").Invoke(objNodo, new[] { afdDataMdl });
        }
    }
}