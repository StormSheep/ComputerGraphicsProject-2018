using UnityEngine;
using System.Collections;
using System;

public class TransformMatrix {

    private Vector3 CAMERA_POS = new Vector3(0, 0, 5);
    private Vector3 CAMERA_LOOKAT = new Vector3(0, 0, 0);
    private Vector3 CAMERA_UP = new Vector3(0, 1, 0);
    private Matrix4x4 PROJECTION = Matrix4x4.Perspective(90, 1, 1, 1000);
    private Vector3 TRANSLATION = new Vector3(1, 0, 0);
    private Vector3 SCALE = new Vector3(15, 1, 1);

    public TransformMatrix() {}

    public Vector3[] transformMatrixRotating(Vector3[] cube, float angle)
    {
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, Vector3.up), Vector3.one);
        cube = MatrixTransform(cube, rotationMatrix);
        return cube;
    }

    public Matrix4x4 getViewingMatrix()
    {
        Vector3 forward = CAMERA_LOOKAT - CAMERA_POS;
        forward.Normalize();
        CAMERA_UP.Normalize();


        Quaternion lookRotation = Quaternion.LookRotation(forward, CAMERA_UP);
        Matrix4x4 viewingMatrix = Matrix4x4.TRS(-CAMERA_POS, lookRotation, Vector3.one);
        return viewingMatrix;
    }

    public Matrix4x4 getRotationMatrix(float angle, Vector3 axis)
    {
        Matrix4x4 rotationMatrix =
            Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(angle, axis), Vector3.one);
        return rotationMatrix;
    }

    public Matrix4x4 getTranslationMatrix()
    {
        Matrix4x4 translationMatrix = Matrix4x4.TRS(TRANSLATION, Quaternion.identity, Vector3.one);
        return translationMatrix;
    }

    public Matrix4x4 getScalingMatrix()
    {
        Matrix4x4 scalingMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, SCALE);
        return scalingMatrix;
    }

    public Matrix4x4 getProjectionMatrix()
    {
        return PROJECTION;
    }



    public Vector3[] transformMainMatrixRotation(float angle, Vector3 axis, Vector3[] cube)
    {
        Matrix4x4 mainMatrix = getProjectionMatrix() * getViewingMatrix() * getRotationMatrix(angle, axis) * getTranslationMatrix();

        Vector3[] newCube = MatrixTransform(cube, mainMatrix);
        return newCube;
    }

    public Vector3[] transformMatrixViewing(Vector3[] cube)
    {
        Vector3[] viewingCube =
            MatrixTransform(cube, getViewingMatrix());
        return viewingCube;
    }

    public Vector3[] transformMatrixProjection(Vector3[] cube)
    {
        Vector3[] projectionCube =
           MatrixTransform(cube, getProjectionMatrix());
        return projectionCube;
    }

    private Vector3[] MatrixTransform(
        Vector3[] meshVertices, 
        Matrix4x4 transformMatrix)
    {
        Vector3[] output = new Vector3[meshVertices.Length];
        for (int i = 0; i < meshVertices.Length; i++)
            output[i] = transformMatrix * 
                new Vector4( 
                meshVertices[i].x,
                meshVertices[i].y,
                meshVertices[i].z,
                    1);

        return output;
    }
}
