﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFP.Persistencia.Model
{
    public class BasePagMdl
    {
        public int LimInf;
        public int LimSup;

        public int PagMaxReg { get; set; }
        public int PagAct { get; set; }

        public void CalcularLimites()
        {
            LimInf = ((PagAct - 1) * PagMaxReg) + 1;
            LimSup = PagAct * PagMaxReg;
        }

        public BasePagMdl()
        {
            LimInf = 0;
            LimSup = 0;
        }

        public BasePagMdl(int PagAct, int PagMaxReg)
        {
            this.PagAct = PagAct;
            this.PagMaxReg = PagMaxReg;
            CalcularLimites();
        }
    }
}
