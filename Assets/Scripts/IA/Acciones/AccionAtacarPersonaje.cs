using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="IA/Acciones/AtacarPersonaje")]
public class AccionAtacarPersonaje : IAAccion
{
    public override void Ejecutar(IAController controller)
    {
        Atacar(controller);
    }

    private void Atacar(IAController controller)
    {
        if (controller.PersonajeReferencia == null ) { return; }
        if (controller.EsTiempoDeAtacar() == false ) { return; }
        if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado))
        {
            if (controller.TipoAtaque == TiposDeAtaque.Embestida)
            {
                controller.AtaqueEmbestida(controller.Danio);
            }
            else
            {
                controller.AtaqueMelee(controller.Danio);    
            }
            
            controller.ActualizarTiempoEntreAtaques();
        }
    }
}
