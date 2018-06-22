using UnityEngine;

public class Parallax : MonoBehaviour
{
    public static float move;

    public float speed;
    private Transform[] layer;
    private Transform[,] childLayer;
    private float[] distance;

    private void Start()
    {
        layer = new Transform[transform.childCount];
        childLayer = new Transform[layer.Length, 3];
        distance = new float[layer.Length];

        for (int i = 0; i < layer.Length; i++)
            layer[i] = transform.GetChild(i);

        for (int i = 0; i < layer.Length; i++)
        {
            distance[i] = 8.6f;
            for (int j = 0; j < 3; j++)
            {
                childLayer[i, j] = layer[i].GetChild(j);
            }
        }
    }

   private void FixedUpdate()
   {
        move = Input.GetAxis("Horizontal");
        if ((layer[0].transform.position.x > 5 && move < 0) || GameController.isBattle)
            move = 0;

        //хмарки завжди рухаються
        layer[3].position -= new Vector3(1 * speed / 300 , 0, 0);
        layer[5].position -= new Vector3(1 * speed / 450 , 0, 0);
        
        if (move > 0.1f || move < -0.1f)
        {
            //небо
            layer[6].position -= new Vector3((move * speed) / 500, 0, 0);

            for (int i = 1; i < layer.Length;  i++)
                layer[i-1].position -= new Vector3((move * speed) / (i * 10), 0, 0);
        }

        for(int i = 0; i < layer.Length; i++)
        {
            if (-layer[i].position.x > distance[i])
            {
                distance[i] += 19.2f;

                childLayer[i,0].localPosition += new Vector3(57.6f, 0, 0);

                Transform tempTransform = childLayer[i, 0];
                childLayer[i, 0] = childLayer[i, 1];
                childLayer[i, 1] = childLayer[i, 2];
                childLayer[i, 2] = tempTransform;
            }
            else if(-layer[i].position.x < distance[i] - 19.2f)
            {
                distance[i] -= 19.2f;

                childLayer[i, 2].localPosition -= new Vector3(57.6f, 0, 0);

                Transform tempTransform = childLayer[i, 2];
                childLayer[i, 2] = childLayer[i, 1];
                childLayer[i, 1] = childLayer[i, 0];
                childLayer[i, 0] = tempTransform;
            }
        }
    }
}
