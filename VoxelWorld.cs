using Godot;

public class VoxelWorld : Spatial
{
    private VoxelChunk _chunk;
    
    public int SampleCell(int x, int y, int z)
    {
        if (x < 0 || x >= VoxelChunk.Width ||
            y < 0 || y >= VoxelChunk.Height ||
            z < 0 || z >= VoxelChunk.Depth)
        {
            return 0;
        }

        return _chunk.GetCell(x, y, z);
    }

    public void GenerateChunkMesh()
    {
        for (int x = 0; x < VoxelChunk.Width; x++)
        {
            for (int y = 0; y < VoxelChunk.Height; y++)
            {
                for (int z = 0; z < VoxelChunk.Depth; z++)
                {
                    _chunk.SetCell(x, y, z, (GD.Randi() % 2 == 0) ? 1 : 0);
                }
            }
        }

        _chunk.GenerateMesh(this);
    }

    public override void _Ready()
    {
        _chunk = GetNode<VoxelChunk>("VoxelChunk");
        GenerateChunkMesh();
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            GenerateChunkMesh();
        }
    }
}
