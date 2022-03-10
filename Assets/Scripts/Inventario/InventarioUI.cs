using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("PanelInventarioDescripcion")] 
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;

    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor; 

    public InventarioSlot SlotSeleccionado { get; private set; }

    private List<InventarioSlot> slotsDisponible = new List<InventarioSlot>();

    public int  IndexSlotInicialPorMover { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
    }
    void Update() 
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (SlotSeleccionado != null )
            {
                IndexSlotInicialPorMover = SlotSeleccionado.Index;
            }
        }
    }

    // Update is called once per frame
    private void InicializarInventario()
    {
        for(int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
           InventarioSlot nuevoSlot =  Instantiate(slotPrefab, contenedor);
           nuevoSlot.Index = i;
           slotsDisponible.Add(nuevoSlot);
        }
    }

    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null ) { return; }
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null) 
        {
            SlotSeleccionado = slot;
        }

    }

    public void DibujarItemEnInventario(InventarioItem itemPorAnadir, int cantidad, int itemIndex)
    {
        InventarioSlot slot = slotsDisponible[itemIndex];
        if (itemPorAnadir != null)
        {
            slot.ActivarSlot(true);
            slot.ActualizarSlot(itemPorAnadir, cantidad);
        } else
        {
            slot.ActivarSlot(false);
        }
    }

    private void ActualizarInventarioDescripcion(int index)
    {
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            panelInventarioDescripcion.SetActive(false);
        }
    }

    public void UsarItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void EquiparItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotEquiparItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    public void RemoverItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotRemoverItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    
    #region Evento

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        if (tipo == TipoDeInteraccion.Click)
        {
            ActualizarInventarioDescripcion(index);
        }
    }

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion

}
