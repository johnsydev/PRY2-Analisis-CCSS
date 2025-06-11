using System;
using System.Collections.Generic;
using PRY2_Analisis_CCSS;

public class ColaDePrioridad
{
    private List<(int Prioridad, Tiquete tiquete)> tiquetes = new List<(int Prioridad, Tiquete tiquete)>();
    public void Agregar(Tiquete tiquete, int prioridad)
    {
        tiquetes.Add((prioridad, tiquete));
        tiquetes.Sort(CompararPorPrioridad);
    }

    private int CompararPorPrioridad
        ((int Prioridad, Tiquete tiquete) a,
        (int Prioridad, Tiquete tiquete) b)
        {
            return a.Prioridad.CompareTo(b.Prioridad);
        }

    public Tiquete Remover()
        {
            Tiquete tiquete = tiquetes[0].tiquete;
            tiquetes.RemoveAt(0);
            return tiquete;
        }

    public Tiquete PrimerElemento()
    {
        return tiquetes[0].tiquete;
    }

    public bool EstaVacia()
    {
        if (tiquetes.Count == 0)
            return true;
        else
            return false;
    }

    public int Cantidad()
    {
        return tiquetes.Count;
    }
}

