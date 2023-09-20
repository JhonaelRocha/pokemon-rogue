using UnityEngine;
public static class Custom_Functions
{
    public static float Distance_Between(GameObject object_1, GameObject object_2){
        float distance = Vector2.Distance(object_1.transform.position, object_2.transform.position);
        return distance;
    }
    public static float Point_Direction(GameObject object_1, bool useMouse = false, GameObject object_2 = null, 
        int fragment = 1, bool is3D = false){
        Vector3 position_1 = object_1.transform.position;
        Vector3 position_2;

        if (useMouse){
            // Converte a posição do mouse de coordenadas de pixels da tela para um ponto no mundo do jogo
            position_2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }else{
        position_2 = object_2.transform.position;
        }
        float angle = 0;
        if (is3D){
            angle = Mathf.Atan2(position_2.z - position_1.z, position_2.x - position_1.x) * Mathf.Rad2Deg;
        }else{
            angle = Mathf.Atan2(position_2.y - position_1.y, position_2.x - position_1.x) * Mathf.Rad2Deg;
        }

        

        //Adjust angles to how they really are in math  // Ajusta os ângulos para como eles realmente são na matemática.

        //            90°

        //       180°      0° / 360°

        //            270°

        angle -= 180; 
        if (angle < 0) angle += 360;
        //---------------
        angle = Mathf.RoundToInt(angle / fragment); 
        return angle;
        
    }
    public static void Size(this Transform transform, float x = 1, float y = float.NaN){
        if(float.IsNaN(y)) y = x;
        transform.localScale = new Vector3( x , y , 1);
    }
}