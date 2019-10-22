using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlipNormal : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var mesh = this.GetComponent<MeshFilter>().mesh;

        //mesh.uv = mesh.uv.Select(o => new Vector2(1 - o.x, o.y)).ToArray();

       mesh.uv = mesh.uv.Reverse().ToArray();

        mesh.triangles = mesh.triangles.Reverse().ToArray();

        //mesh.normals = mesh.normals.Select(o => -o).ToArray();




        //mesh.triangles = mesh.triangles.Reverse().ToArray();

        /*Vector3[] normals = mesh.normals;
        for(int i = 0; i < normals.Length; i++)
            normals[i] = -1 * normals[i];

        mesh.normals = normals;

        for(int i = 0; i < normals.Length; i++)
        {
            int[] triangles = mesh.GetTriangles(i);
            for(int j = 0; j < normals.Length; j++)
            {
                int temp = triangles[j];
                triangles[j] = triangles[j + 1];
                triangles[j + 1 ] = temp;
            }
            mesh.SetTriangles(triangles, i);
        }*/
    }
}
