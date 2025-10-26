using SinnohUndergroundRNGLibrary;
using SinnohUndergroundRNGLibrary.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DisplayTest
{
    public partial class FormDisplay : Form
    {
        public FormDisplay()
        {
            InitializeComponent();
        }

        private void FormDisplay_Load(object sender, EventArgs e)
        {
            DrawRandomWall();
        }

        private void DrawRandomWall()
        {
            var rng = new Random();
            var plates = new HashSet<MiningObjectType>();
            ushort tid = (ushort)rng.Next(ushort.MinValue, ushort.MaxValue);
            bool nationalDex = (rng.Next() & 1) == 0;

            ulong seed = ((ulong)rng.Next() << 32) | (uint)rng.Next();

            var generator = MiningWallGenerator.CreateInstance(tid, nationalDex, plates /*null*/);
            var result = generator.Generate(seed);

            Text = $"Seed: 0x{result.HeadSeed:X}";
            pbWall.Image = Visualizer.DrawWall(result);
            pbCover.Image = Visualizer.DrawCover(result.Cover);
        }
    }
}
