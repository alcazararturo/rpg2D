using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA/Acciones/DesactivarCaminoMovimiento")]
public class AccionDesactivarCaminoMovimiento : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        if (controller.EnemyMovimiento == null) { return; }
        controller.EnemyMovimiento.enabled = false;
    }
}
