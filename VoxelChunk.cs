using Godot;
using System.Collections.Generic;

using CellID = System.UInt16;

public class VoxelChunk : Spatial
{
    public const int Width = 32, Height = 32, Depth = 32;
    private readonly CellID[,,] _cells = new CellID[Width, Height, Depth];

    private MeshInstance _meshInstance;

    public int GetCell(int x, int y, int z)
    {
        return _cells[x, y, z];
    }

    public void SetCell(int x, int y, int z, int value)
    {
        _cells[x, y, z] = (CellID)value;
    }

    public void GenerateMesh(VoxelWorld world)
    {
        MeshArrays meshArrays = new MeshArrays();

        List<Vector3> vertices = meshArrays.Vertices;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int z = 0; z < Depth; z++)
                {
                    int cell = GetCell(x, y, z);

                    if (cell == 0)
                    {
                        continue;
                    }

                    int cellTop = world.SampleCell(x, y + 1, z);

                    if (cellTop == 0)
                    {
                        vertices.Add(new Vector3(x, y + 1, z));
                        vertices.Add(new Vector3(x + 1, y + 1, z));
                        vertices.Add(new Vector3(x, y + 1, z + 1));
                        vertices.Add(new Vector3(x + 1, y + 1, z + 1));

                        meshArrays.CreateQuad(Vector3.Up);
                    }

                    int cellBottom = world.SampleCell(x, y - 1, z);

                    if (cellBottom == 0)
                    {
                        vertices.Add(new Vector3(x + 1, y, z));
                        vertices.Add(new Vector3(x, y, z));
                        vertices.Add(new Vector3(x + 1, y, z + 1));
                        vertices.Add(new Vector3(x, y, z + 1));

                        meshArrays.CreateQuad(Vector3.Down);
                    }

                    int cellLeft = world.SampleCell(x - 1, y, z);

                    if (cellLeft == 0)
                    {
                        vertices.Add(new Vector3(x, y + 1, z));
                        vertices.Add(new Vector3(x, y + 1, z + 1));
                        vertices.Add(new Vector3(x, y, z));
                        vertices.Add(new Vector3(x, y, z + 1));

                        meshArrays.CreateQuad(Vector3.Left);
                    }

                    int cellRight = world.SampleCell(x + 1, y, z);

                    if (cellRight == 0)
                    {
                        vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                        vertices.Add(new Vector3(x + 1, y + 1, z));
                        vertices.Add(new Vector3(x + 1, y, z + 1));
                        vertices.Add(new Vector3(x + 1, y, z));

                        meshArrays.CreateQuad(Vector3.Right);
                    }

                    int cellFront = world.SampleCell(x, y, z - 1);

                    if (cellFront == 0)
                    {
                        vertices.Add(new Vector3(x + 1, y + 1, z));
                        vertices.Add(new Vector3(x, y + 1, z));
                        vertices.Add(new Vector3(x + 1, y, z));
                        vertices.Add(new Vector3(x, y, z));

                        meshArrays.CreateQuad(Vector3.Forward);
                    }

                    int cellBack = world.SampleCell(x, y, z + 1);

                    if (cellBack == 0)
                    {
                        vertices.Add(new Vector3(x, y + 1, z + 1));
                        vertices.Add(new Vector3(x + 1, y + 1, z + 1));
                        vertices.Add(new Vector3(x, y, z + 1));
                        vertices.Add(new Vector3(x + 1, y, z + 1));

                        meshArrays.CreateQuad(Vector3.Back);
                    }
                }
            }
        }

        _meshInstance.Mesh = meshArrays.GenerateMesh();
    }

    public override void _Ready()
    {
        _meshInstance = GetNode<MeshInstance>("MeshInstance");
    }
}
