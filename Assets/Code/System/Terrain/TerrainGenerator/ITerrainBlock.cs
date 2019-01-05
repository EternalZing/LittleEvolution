namespace Code.System.Terrain.TerrainGenerator {
    public interface ITerrainBlock {
        bool Wasted { get; set; }
        int BlockId { get; set; }
    }
}