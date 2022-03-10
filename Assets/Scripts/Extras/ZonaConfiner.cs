using Cinemachine;
using UnityEngine;

public class ZonaConfiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;

     private void  OnTriggerEnter2D(Collider2D other)
     {
         if( other.CompareTag("Player"))
         {
             camera.gameObject.SetActive(true);
         }
     }
    private void  OnTriggerExit2D(Collider2D other)
     {
         if( other.CompareTag("Player"))
         {
             camera.gameObject.SetActive(false);
         }
     }

}
