using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/ActivarCaminoMovimiento")]
public class AccionActivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemyMovimiento == null) { return; }
        controller.EnemyMovimiento.enabled = true;
    }
}
