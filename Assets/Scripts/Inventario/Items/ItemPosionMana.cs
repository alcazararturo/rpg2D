using UnityEngine;

[CreateAssetMenu(menuName="Items/Posion Mana")]
public class ItemPosionMana : InventarioItem
{
     [Header("PosionInfo")]
    public float MPRestauracion;

    public override bool UsarItem()
    {
        if (Inventario.Instance.Personaje.personajeMana.SePuedeRestaurar)
        {
            Inventario.Instance.Personaje.personajeMana.RestaurarMana(MPRestauracion);
            return true;
        }
        return false;
    }
}
