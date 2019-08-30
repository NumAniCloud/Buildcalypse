using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BuildCalypse.CodeGen
{
    class CommandBlockFactory
    {
        private static readonly Matrix4x4 Rotate90 = new Matrix4x4(
            0,  0, -1,  0,
            0,  1,  0,  0,
            1,  0,  0,  0,
            0,  0,  0,  1);
        private static readonly int PlayerNum = 8;
        private static readonly int DirectionNum = 4;

        public IEnumerable<CommandBlock> GetCommands(Structure structure, Vector3 origin)
        {
            var mobName = structure.MobName;
            var structureId = structure.Id;
            yield return DetectorBlock(structure, origin);

            for (var i = 0; i < PlayerNum; ++i)
            {
                yield return PlayerQueryBlock(structure, i, origin);
                for (var j = 0; j < DirectionNum; ++j)
                {
                    yield return DirectionQueryBlock(structure, i, new Direction(j * 90), origin);
                }
                yield return ResetPlayerDetectorBlock(i, origin);
            }

            for (int j = 0; j < DirectionNum; j++)
            {
                var direction = new Direction(j * 90);
                yield return PlaceStructureBlock(structure, direction, origin);
                yield return PlaceRedstoneBlock(structure, direction, origin);
                yield return KillMobBlock(structure, direction, origin);
            }
        }

        private CommandBlock ResetPlayerDetectorBlock(int playerId, Vector3 origin)
        {
            var start = origin + new Vector3(playerId, 2, 0);
            var end = origin + new Vector3(playerId, 2, 3);
            var command = $"fill {start.X} {start.Y} {start.Z} {end.X} {end.Y} {end.Z} air";
            return new CommandBlock
            {
                Command = command,
                Position = end + new Vector3(0, 0, 2),
                Facing = Direction.South,
                Conditional = false,
                NeedsRedstone = true,
                Mode = CommandBlockMode.Impulse
            };
        }

        private CommandBlock KillMobBlock(Structure structure, Direction direction, Vector3 origin)
        {
            var mobSelector = $"@e[type={structure.MobName},nbt={{Tag=[\"{structure.Id}\",\"dir{direction.Degree}\"]}}]";
            var command = $"execute as {mobSelector} at @s tp @s ~ -100 ~";

            var position = origin + new Vector3(4, 0, 0);
            var facing = Direction.East;
            if (direction == Direction.North || direction == Direction.East)
            {
                position += new Vector3(1, 0, 0);
                facing = Direction.West;
            }

            return new CommandBlock
            {
                Command = command,
                Position = position,
                Facing = facing,
                Conditional = true,
                NeedsRedstone = false,
                Mode = CommandBlockMode.Chain,
            };
        }

        private CommandBlock PlaceRedstoneBlock(Structure structure, Direction direction, Vector3 origin)
        {
            var redstone = structure.RedstoneOffset;
            var mobSelector = $"@e[type={structure.MobName},nbt={{Tag=[\"{structure.Id}\",\"dir{direction.Degree}\"]}}]";
            var command = $"execute at {mobSelector} run setblock redstone_block {redstone.X} {redstone.Y} {redstone.Z}";

            var position = origin + new Vector3(3, 0, 0);
            var facing = Direction.East;
            if (direction == Direction.North || direction == Direction.East)
            {
                position += new Vector3(3, 0, 0);
                facing = Direction.West;
            }

            return new CommandBlock
            {
                Command = command,
                Position = position,
                Facing = facing,
                Conditional = true,
                NeedsRedstone = false,
                Mode = CommandBlockMode.Chain,
            };
        }

        private CommandBlock PlaceStructureBlock(Structure structure, Direction direction, Vector3 origin)
        {
            var matrix = Enumerable.Repeat(Rotate90, direction.Degree / 90).Aggregate(Matrix4x4.Identity, (x, y) => x * y);
            var offset = Vector3.Transform(structure.Offset, matrix);
            var rotation = direction.GetStructureBlockRotation();
            var mobSelector = $"@e[type={structure.MobName},nbt={{Tag=[\"{structure.Id}\",\"dir{direction.Degree}\"]}}]";
            var data = $"{{mode=LOAD,name={structure.FullName},posX={offset.X},posY={offset.Y},posZ={offset.Z},rotation={rotation}}}";
            var command = $"execute at {mobSelector} run setblock structure_block[]{data} ~ ~ ~";

            var position = origin + new Vector3(2, 0, 0);
            var facing = Direction.East;
            if (direction == Direction.North || direction == Direction.East)
            {
                position += new Vector3(5, 0, 0);
                facing = Direction.West;
            }

            return new CommandBlock
            {
                Command = command,
                Position = position,
                Facing = facing,
                Conditional = false,
                NeedsRedstone = false,
                Mode = CommandBlockMode.Repeat,
            };
        }

        private CommandBlock DetectorBlock(Structure structure, Vector3 origin)
        {
            var mobSelector = $"@e[type={structure.MobName},nbt={{Tag=[\"{structure.Id}\"]}}]";
            var command = $"execute if entity {mobSelector}";
            return new CommandBlock
            {
                Command = command,
                Position = origin,
                Facing = Direction.South,
                Conditional = false,
                NeedsRedstone = false,
                Mode = CommandBlockMode.Repeat,
            };
        }

        private CommandBlock PlayerQueryBlock(Structure structure, int playerId, Vector3 origin)
        {
            var rs = origin + new Vector3(playerId, 2, 0);
            var mobSelector = $"@e[type={structure.MobName},nbt={{Tag=[\"{structure.Id}\",\"player{playerId + 1}\"]}}]";
            var positionParams = $"{rs.X} {rs.Y} {rs.Z} {rs.X} {rs.Y} {rs.Z + 3}";
            var command = $"execute if entity {mobSelector} run fill redstone_block {positionParams}";
            return new CommandBlock
            {
                Command = command,
                Position = origin + new Vector3(playerId, 0, 4),
                Facing = Direction.South,
                Conditional = false,
                NeedsRedstone = true,
                Mode = CommandBlockMode.Impulse,
            };
        }

        private CommandBlock DirectionQueryBlock(Structure structure, int playerId, Direction direction, Vector3 origin)
        {
            var mobSelector = $"@e[type={structure.MobName},nbt={{Tag=[\"{structure.Id}\",\"player{playerId + 1}\"]}}]";
            var playerSelector = $"@p[scores={{eye_direction={direction.Degree},playerId={playerId + 1}}}]";
            var nbtToMerge = $"{{Tag=[\"dir{direction.Degree}\"]}}";
            var command = $"execute if entity {playerSelector} as {mobSelector} run data merge @s {nbtToMerge}";
            return new CommandBlock
            {
                Command = command,
                Position = origin + new Vector3(playerId, 3, direction.Degree / 90),
                Facing = Direction.South,
                Conditional = false,
                NeedsRedstone = true,
                Mode = CommandBlockMode.Impulse,
            };
        }
    }
}