﻿using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PRY2_Analisis_CCSS
{
    public class Tiquete
    {   
        public int prioridad = 0;
        public Especialidad especialidad;
        public int id_tiquete;
        public static int cantidadTiquetes = 0;

        public Tiquete(Especialidad especialidad)
        {
            cantidadTiquetes++;
            this.prioridad = cantidadTiquetes;
            this.especialidad = especialidad;
            this.id_tiquete = cantidadTiquetes;
        }

        public void setPrioridad(int prioridad)
        {
            this.prioridad = prioridad;
        }

        public Especialidad getEspecialidad() {
            return this.especialidad;
        }

        public int getPrioridad() {
            return this.prioridad;
        }
    }
}