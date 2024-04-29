using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DataAccess.Entities;

namespace DataAccess.Configuration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData
            (
                new Region
                {
                    Id = 1,
                    Name = "Shirak"
                },
                new Region
                {
                    Id = 2,
                    Name = "Kotayk",
                    TerrainType = TerrainType.Mountains
                },
                new Region
                {
                    Id = 3,
                    Name = "Kotayk",
                    TerrainType = TerrainType.Foothills
                },
                new Region
                {
                    Id = 4,
                    Name = "Gegharkunik",
                },
                new Region
                {
                    Id = 5,
                    Name = "Lori",
                },
                new Region
                {
                    Id = 6,
                    Name = "Tavush",
                },
                new Region
                {
                    Id = 7,
                    Name = "Aragatsotn",
                    TerrainType = TerrainType.Mountains
                },
                new Region
                {
                    Id = 8,
                    Name = "Aragatsotn",
                    TerrainType = TerrainType.Foothills
                },
                new Region
                {
                    Id = 9,
                    Name = "Ararat",
                },
                new Region
                {
                    Id = 10,
                    Name = "Armavir",
                },
                new Region
                {
                    Id = 11,
                    Name = "Vayots Dzor",
                    TerrainType = TerrainType.Mountains
                },
                new Region
                {
                    Id = 12,
                    Name = "Vayots Dzor",
                    TerrainType = TerrainType.Foothills
                },
                new Region
                {
                    Id = 13,
                    Name = "Syunik",
                    TerrainType = TerrainType.Valleys
                },
                new Region
                {
                    Id = 14,
                    Name = "Syunik",
                    TerrainType = TerrainType.Foothills
                }
            );
        }
    }
}
