using System;
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
            };
            var factory = new CommandBlockFactory();
            var blocks = factory.GetCommands(structure, new Vector3(10, 5, 10));

            foreach (var item in blocks)
            {
                System.Console.WriteLine(item.GetCommandSetBlock());
            }
        }
    }
}
