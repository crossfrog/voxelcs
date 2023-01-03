using Godot;
using System.Collections.Generic;

public class MeshArrays
{
    public readonly List<Vector3> Vertices = new List<Vector3>();
    public readonly List<int> Indices = new List<int>();
    public readonly List<Vector3> Normals = new List<Vector3>();
    public readonly List<Vector2> Uvs = new List<Vector2>();
    public readonly List<Color> Colors = new List<Color>();

    public void CreateQuad(Vector3 normal)
    {
        int baseIndex = Vertices.Count - 4;

        Indices.Add(baseIndex);
        Indices.Add(baseIndex + 1);
        Indices.Add(baseIndex + 2);

        Indices.Add(baseIndex + 1);
        Indices.Add(baseIndex + 3);
        Indices.Add(baseIndex + 2);

        Normals.Add(normal);
        Normals.Add(normal);
        Normals.Add(normal);
        Normals.Add(normal);
    }

    public Mesh GenerateMesh()
    {
        ArrayMesh mesh = new ArrayMesh();

        Godot.Collections.Array arrays = new Godot.Collections.Array();
        arrays.Resize((int)ArrayMesh.ArrayType.Max);

        arrays[(int)ArrayMesh.ArrayType.Vertex] = Vertices;
        arrays[(int)ArrayMesh.ArrayType.Index] = Indices;
        arrays[(int)ArrayMesh.ArrayType.Normal] = Normals;
        //arrays[(int)ArrayMesh.ArrayType.TexUv] = Uvs;
        //arrays[(int)ArrayMesh.ArrayType.Color] = Colors;

        mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles, arrays);

        return mesh;
    }
}
