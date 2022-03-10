using UnityEngine;

[CreateAssetMenu(menuName="Items/Posion Vida")]
public class ItemPosionVida : InventarioItem
{
    [Header("PosionInfo")]
    public float HPRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.personajeVida.PuedeSerCurado)
        {
            Inventario.Instance.Personaje.personajeVida.RestaurarSalud(HPRestauracion);
            return true;
        }

        return false;
    }

}
