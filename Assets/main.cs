using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour {
    Vector3[] cube = new Vector3[8];
    
    outcodetest transformLines = new outcodetest();
    TransformMatrix transformMatrix = new TransformMatrix();
    Texture2D textureCube;
    Renderer cubeRenderer;
    List<Vector2> previousPixels = new List<Vector2>();
    
    const int TEXTURE_WIDTH = 512, TEXTURE_HEIGHT = 512;
    float angle = 1.0f;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;


    void Start () {
        /*MeshFilter filter = GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        Renderer renderer = GetComponent<MeshRenderer>();

        mesh.Clear();
        mesh.vertices = setVertices();
        mesh.triangles = setTriangles();
        mesh.uv = setUVs();
        mesh.RecalculateNormals();
        
        Texture2D textureDice = Resources.Load("DieTexture") as Texture2D;
        textureDice.width = textureWidth;
        textureDice.height = textureHeight;
        renderer.material.mainTexture = textureDice;
        */
        textureCube = new Texture2D(TEXTURE_WIDTH, TEXTURE_HEIGHT);
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.mainTexture = textureCube;

        
        
        cube[0] = new Vector3(1, 1, 1);    // run
        cube[1] = new Vector3(-1, 1, 1);   //  lun 
        cube[2] = new Vector3(-1, -1, 1);  // ldn 
        cube[3] = new Vector3(1, -1, 1);  //  rdn 
        cube[4] = new Vector3(1, 1, -1);   // ruf 
        cube[5] = new Vector3(-1, 1, -1);   // luf 
        cube[6] = new Vector3(-1, -1, -1);   // ldf 
        cube[7] = new Vector3(1, -1, -1);    // rdf 

        
    }

    /*Vector3[] setVertices()
    {
        vertices = new Vector3[]
        {
            //Front
            new Vector3(1, 1, 1),    // run 0
            new Vector3(-1, 1, 1),   //  lun 1
            new Vector3(-1, -1, 1),  // ldn 2
            new Vector3(1, -1, 1),  //  rdn 3

            //Up
            new Vector3(1, 1, -1),   // ruf 4
            new Vector3(-1, 1, -1),   // luf 5
            new Vector3(-1, 1, 1),   //  lun 1
            new Vector3(1, 1, 1),    // run 0

            //Left
            new Vector3(-1, 1, 1),   //  lun 1
            new Vector3(-1, 1, -1),   // luf 5
            new Vector3(-1, -1, -1),   // ldf 6
            new Vector3(-1, -1, 1),  // ldn 2

            //Right
            new Vector3(1, 1, -1),   // ruf 4
            new Vector3(1, 1, 1),    // run 0
            new Vector3(1, -1, 1),  //  rdn 3
            new Vector3(1, -1, -1),    // rdf 7

            //Down
            new Vector3(1, -1, 1),  //  rdn 3
            new Vector3(-1, -1, 1),  // ldn 2
            new Vector3(-1, -1, -1),   // ldf 6
            new Vector3(1, -1, -1),    // rdf 7

            //Back
            new Vector3(-1, 1, -1),   // luf 5
            new Vector3(1, 1, -1),   // ruf 4
            new Vector3(1, -1, -1),    // rdf 7
            new Vector3(-1, -1, -1),   // ldf 6
        };
        return vertices;
    }*/

    /*int[] setTriangles()
    {
        triangles = new int[]{
            //Front
            1, 2, 0, 
            0, 2, 3,

            //Up
            5, 6, 4,
            4, 6, 7,

            //Left
            9, 10, 8,
            8, 10, 11,

            //Right
            13, 14, 12,
            12, 14, 15,

            //Down
            17, 18, 16,
            16, 18, 19,

            //Back
            21, 22, 20,
            20, 22, 23
        };
        return triangles;
    }*/

    /*Vector2[] setUVs()
    {
        uvs = new Vector2[]{
            //Front
            new Vector2(0.33f, 1),      //0
            new Vector2(0, 1),          //1
            new Vector2(0,0.5f),        //2
            new Vector2(0.33f,0),       //3

            //Up
            new Vector2(0.66f, 1),      //4
            new Vector2(0.33f, 1),      //0
            new Vector2(0.33f,0),       //3
            new Vector2(0.66f, 0.5f),   //5

            //Left
            new Vector2(1, 1),          //6
            new Vector2(0.66f, 1),      //4
            new Vector2(0.66f, 0.5f),   //5
            new Vector2(1, 0.5f),       //7

            //Right
            new Vector2(0.33f,0),       //3
            new Vector2(0,0.5f),        //2
            new Vector2(0, 0),          //8
            new Vector2(0.33f, 0),      //9

            //Down
            new Vector2(0.66f, 0.5f),   //5
            new Vector2(0.33f,0),       //3
            new Vector2(0.33f, 0),      //9
            new Vector2(0.66f, 0),      //10

            //Back
            new Vector2(1, 0.5f),       //7
            new Vector2(0.66f, 0.5f),   //5
            new Vector2(0.66f, 0),      //10
            new Vector2(1, 0)           //11
        };
        return uvs;
    }*/




    //    0-1 1-2 2-3 3-0
    //    4-5 5-6 6-7 7-4 
    //    0-4   5-1    
    //    7-3    2-6

    void drawCubeLines(Vector3[]   imageOfCube, Color color)
    {
        //Near
        lineDraw(imageOfCube[0], imageOfCube[1], color);
        lineDraw(imageOfCube[1], imageOfCube[2], color);
        lineDraw(imageOfCube[2], imageOfCube[3], color);
        lineDraw(imageOfCube[3], imageOfCube[0], color);

        //Far
        lineDraw(imageOfCube[4], imageOfCube[5], color);
        lineDraw(imageOfCube[5], imageOfCube[6], color);
        lineDraw(imageOfCube[6], imageOfCube[7], color);
        lineDraw(imageOfCube[7], imageOfCube[4], color);

        //Up
        lineDraw(imageOfCube[0], imageOfCube[4], color);//
        lineDraw(imageOfCube[5], imageOfCube[1], color);

        //Down
        lineDraw(imageOfCube[7], imageOfCube[3], color);
        lineDraw(imageOfCube[2], imageOfCube[6], color);
    }
 

    private void lineDraw(Vector3 start, Vector3 end, Color color)
    {
        Vector2 start2 = new Vector2(start.x / start.z, start.y / start.z);
        Vector2 end2 = new Vector2(end.x / end.z, end.y / end.z);

        if (transformLines.Line_Clip( ref start2, ref end2))
        {
            Vector2 startConverted = transformLines.convertForScreen(start2, TEXTURE_WIDTH, TEXTURE_HEIGHT);
            Vector2 endConverted = transformLines.convertForScreen(end2, TEXTURE_WIDTH, TEXTURE_HEIGHT);

            List<Vector2> vectorsRasterised = transformLines.rasterise(startConverted, endConverted);
            foreach(Vector2 pixel in vectorsRasterised)
            {
                textureCube.SetPixel((int)pixel.x, (int)pixel.y, color);
                previousPixels.Add(pixel);
            }
            textureCube.Apply();
        }
    }
    

    private void clearPixels()
    {
        foreach (Vector2 pixel in previousPixels)
        {
            textureCube.SetPixel((int)pixel.x, (int)pixel.y, cubeRenderer.material.color);
        }
        previousPixels.Clear();
    }


    // Update is called once per frame
    void Update () {

       clearPixels();
       drawCubeLines(transformMatrix.transformMainMatrixRotation(angle++, (new Vector3(1,1,1)).normalized, cube), Color.blue);
       
    }
}
