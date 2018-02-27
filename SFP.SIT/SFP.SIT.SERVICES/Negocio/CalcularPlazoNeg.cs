using SFP.SIT.SERVICES.Model.Sol;
using SFP.SIT.SERVICES.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.SIT.SERVICES.Negocio
{
    public class CalcularPlazoNeg
    {
        public const int DIAS_NATURALES = 0;
        public const int DIAS_LABORALES = 1;

        private Dictionary<Int64, char> _dicDiaNoLaboral;

        public CalcularPlazoNeg(Dictionary<Int64, char> dicDiaNoLaboral)
        {
            _dicDiaNoLaboral = dicDiaNoLaboral;
        }


        public SolSeguimientoMdl CalcularSeguimiento(int iTipoSolicitud, DateTime dtFechaFin, List<SolProcesoPlazosMdl> lstProcesoPlazo, SolSeguimientoMdl seguimientoMdl)
        {
            SolProcesoPlazosMdl prcPlzActual = null;
            DateTime FecInicio = new DateTime(seguimientoMdl.seg_fecini.Ticks);
            Int32 iVerde = 0;
            Int32 iAmarillo = 0;
            Int32 iTipoPlazo = 0;

            if (seguimientoMdl.seg_fecampliacion == DateTime.MinValue)
                iTipoPlazo = Constantes.ProcesoTiempo.NO_AMPLIACION;
            else
                iTipoPlazo = Constantes.ProcesoTiempo.AMPLIACION;

            foreach (SolProcesoPlazosMdl procesoPlazo in lstProcesoPlazo)
            {
                if (procesoPlazo.krp_claproceso == seguimientoMdl.krp_claproceso &&
                    procesoPlazo.tso_clatiposol == iTipoSolicitud &&
                    procesoPlazo.kpz_tipoplazo == iTipoPlazo)
                {
                    prcPlzActual = procesoPlazo;
                    break;
                }
            }

            // CALCULAR EL TIEMPO POR LEY
            int[] iDiasNatLab = obtenerDiasNaturalesLaborales(seguimientoMdl.seg_fecini, dtFechaFin);
            seguimientoMdl.seg_diasnolaborales = iDiasNatLab[DIAS_NATURALES] - iDiasNatLab[DIAS_LABORALES];
            seguimientoMdl.seg_diassemaforo = iDiasNatLab[DIAS_LABORALES];

            iVerde = prcPlzActual.kpz_verde;
            iAmarillo = prcPlzActual.kpz_amarillo;

            if (seguimientoMdl.seg_diassemaforo <= iVerde)
                seguimientoMdl.seg_colorsemaforo = Constantes.Semaforo.SOLICITUD_SEMAFORO_IVERDE;

            else if (seguimientoMdl.seg_diassemaforo > iVerde && seguimientoMdl.seg_diassemaforo <= iAmarillo)
                seguimientoMdl.seg_colorsemaforo = Constantes.Semaforo.SOLICITUD_SEMAFORO_IAMARILLO;

            else
                seguimientoMdl.seg_colorsemaforo = Constantes.Semaforo.SOLICITUD_SEMAFORO_IROJO;


            // Mover este calculo cuando se crea la solicitud. (+1 cuando comienza la soliciutd)
            // Mover este calculo cuando se crea la solicitud. (+1 cuando comienza la soliciutd)
            // Mover este calculo cuando se crea la solicitud. (+1 cuando comienza la soliciutd)
            // Mover este calculo cuando se crea la solicitud. (+1 cuando comienza la soliciutd)
            // Mover este calculo cuando se crea la solicitud. (+1 cuando comienza la soliciutd)
            // Mover este calculo cuando se crea la solicitud. (+1 cuando comienza la soliciutd)
            seguimientoMdl.seg_fecestimada = ObtenerFechaFinal(seguimientoMdl.seg_fecini, prcPlzActual.kpz_plazo);

            seguimientoMdl.seg_feccalculo = DateTime.Now;


            // AQUI DEBO DE ENVAIR EL CORREO A TODOS LOS INVOLUCRADOS...
            // AQUI DEBO DE ENVAIR EL CORREO A TODOS LOS INVOLUCRADOS...
            // AQUI DEBO DE ENVAIR EL CORREO A TODOS LOS INVOLUCRADOS...


            return seguimientoMdl;
        }

        public int[] obtenerDiasNaturalesLaborales(DateTime dtFechaIniOri, DateTime dtFechafinOri)
        {
            int[] iDias = new int[2];

            // CALCULAMOS LOS DIAS NATURALES   - TRUNCAR LAS HORAS para obtener dias completos.
            DateTime dtFechaIni = new DateTime(dtFechaIniOri.Year, dtFechaIniOri.Month, dtFechaIniOri.Day);
            DateTime dtFechafin = new DateTime(dtFechafinOri.Year, dtFechafinOri.Month, dtFechafinOri.Day);

            TimeSpan ts = dtFechafin - dtFechaIni;
            iDias[DIAS_NATURALES] = ts.Days;

            // CALCULAMOS LOS DIAS NO LABORALES
            int iSumaDiasNoLaborales = 0;
            DateTime calFechaIni = new DateTime(dtFechaIni.Ticks);

            while (calFechaIni.Ticks < dtFechafin.Ticks)
            {
                if (_dicDiaNoLaboral.ContainsKey(calFechaIni.Ticks))
                    iSumaDiasNoLaborales++;
                calFechaIni = calFechaIni.AddDays(1);
            }

            iDias[DIAS_LABORALES] = iDias[DIAS_NATURALES] - iSumaDiasNoLaborales;

            return iDias;
        }

        public DateTime ObtenerFechaFinal(DateTime dmFechaInicial, int iDias)
        {
            DateTime dmFechaIniCal = new DateTime(dmFechaInicial.Ticks);
            int iCuenta = 1;
            iDias++; // El tiempo de la soliciutd inicia un dia despues
            while (iCuenta < iDias)
            {
                if (_dicDiaNoLaboral.ContainsKey(dmFechaIniCal.Ticks) == false)
                    iCuenta++;

                dmFechaIniCal = dmFechaIniCal.AddDays(1);

            }

            return dmFechaIniCal;
        }
    }
}