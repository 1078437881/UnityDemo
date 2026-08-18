using UnityEngine;
using UnityEngine.UI;

namespace Scene
{
    public class Script_03_15_Scene : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            //画线
            Gizmos.DrawLine(transform.position,Vector3.one);
            Gizmos.DrawCube(Vector3.one, Vector3.one);
        }
        
#if UNITY_EDITOR
        private static Vector3[] fourCorners = new Vector3[4];
        private void OnDrawGizmos()
        {
            foreach (var graphic in GameObject.FindObjectsOfType<MaskableGraphic>())
            {
                if (graphic.raycastTarget)
                {
                    RectTransform rectTransform = graphic.transform as RectTransform;
                    rectTransform.GetWorldCorners(fourCorners);
                    Gizmos.color = Color.blue;
                    for (int i = 0; i < 4; i++)
                    {
                        Gizmos.DrawLine(fourCorners[i],fourCorners[(i+1)%4]);
                    }
                }
            }
            Gizmos.DrawSphere(transform.position,1);
        } 
#endif
    } 
    
   
    

}

