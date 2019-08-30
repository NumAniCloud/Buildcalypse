using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BuildCalypse.CodeGen;

namespace codegen
{
    class Program
    {
        static void Main(string[] args)
        {
            var structure = new Structure()
            {
                Id = "floor",
                FullName = "bldclps:floor",
                MobName = "pig",
                Offset = new Vector3(-1, 0, 0),
                RedstoneOffset = new Vector3(0, 1, 0),
                Bp = 5,
                Name = "床",
                SpawnEggId = "pig_spawn_egg"
            };
            var structures = new []
            {
                structure
            };

            var codeGen = new CodeGen();
            var codes = codeGen.Generate(structures, new Vector3(10, 5, 10));

            // テスト用出力
            /*
            foreach (var item in placer)
            {
                System.Console.WriteLine(item.GetCommandSetBlock());
            }
            foreach (var item in triggers)
            {
                System.Console.WriteLine(item.GetInitializeCommand());
                System.Console.WriteLine(item.GetEnableCommand("@p"));
                System.Console.WriteLine(item.TriggerCommand(1));
            }
            for (int i = 0; i < 8; i++)
            {
                foreach (var item in shopContentsOfPlayer[i+1])
                {
                    System.Console.WriteLine(item.GetFunctionBody());
                }
                System.Console.WriteLine(shopBookOfPlayer[i+1].GetGiveCommand());
            }
            */
        }
    }
}
