using System;
using System.Collections.Generic;
using PRY2_Analisis_CCSS;

// This class handles a priority queue of Tiquetes.
// Tiquetes are stored along with their priority and kept sorted
// so the one with the highest priority is always first.
public class ColaDePrioridad
{
    // List to store (priority, tiquete) pairs
    private List<(int Prioridad, Tiquete tiquete)> tiquetes = new List<(int Prioridad, Tiquete tiquete)>();

    // Adds a new Tiquete to the queue and sorts the list by priority (highest first)
    public void Agregar(Tiquete tiquete, int prioridad)
    {
        tiquetes.Add((prioridad, tiquete));
        tiquetes.Sort(CompararPorPrioridad);
    }

    // Helper method used to sort Tiquetes by priority in descending order
    private int CompararPorPrioridad((int Prioridad, Tiquete tiquete) a, (int Prioridad, Tiquete tiquete) b)
    {
        return b.Prioridad.CompareTo(a.Prioridad); // Highest priority comes first
    }

    // Returns the first Tiquete in the list (the one with the highest priority)
    public Tiquete PrimerElemento()
    {
        Tiquete tiquete = tiquetes[0].tiquete;
        return tiquete;
    }

    // Removes the first Tiquete in the list (the one with the highest priority)
    public void Remover()
    {
        tiquetes.RemoveAt(0);
    }

    // Checks if the queue is empty
    public bool EstaVacia()
    {
        if (tiquetes.Count == 0)
            return true;
        else
            return false;
    }

    // Returns how many Tiquetes are in the queue
    public int Cantidad()
    {
        return tiquetes.Count;
    }
}
