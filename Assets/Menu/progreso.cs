using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class progreso : MonoBehaviour
{
    public Image barra_progreso;

    public float progreso_actual;

    public float progreso_maximo;

    void Update()
    {
        barra_progreso.fillAmount = progreso_actual/progreso_maximo;
    }
}
