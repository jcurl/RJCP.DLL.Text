namespace RJCP.Core.Text
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using static StringUtilities;

    [TestFixture(Category = "String.SPrintF.Performance")]
    public class StringUtilitiesSPrintFPerformanceTest
    {
        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_Performance()
        {
            PerformanceTest("SPrintF Double %e", 13, 10, 100000, () => { SPrintF("%e", 123456.789); });
            PerformanceTest("SPrintF Single %e", 13, 10, 100000, () => { SPrintF("%e", (float)123456.789); });
            PerformanceTest("SPrintF Double %f", 13, 10, 100000, () => { SPrintF("%f", 123456.789); });
            PerformanceTest("SPrintF Single %f", 13, 10, 100000, () => { SPrintF("%f", (float)123456.789); });
            PerformanceTest("SPrintF Double %g", 13, 10, 100000, () => { SPrintF("%g", 123456.789); });
            PerformanceTest("SPrintF Single %g", 13, 10, 100000, () => { SPrintF("%g", (float)123456.789); });
            PerformanceTest("SPrintF Integer %d", 13, 10, 100000, () => { SPrintF("%d", 16384); });
            PerformanceTest("SPrintF Unsigned %u", 13, 10, 100000, () => { SPrintF("%u", 16384); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceInteger()
        {
            PerformanceTest("SPrintF Integer %d", 13, 10, 100000, () => { SPrintF("%d", 16384); });
            PerformanceTest(".NET Integer      ", 13, 10, 100000, () => { _ = string.Format("{0}", 16384); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceUnsigned()
        {
            PerformanceTest("SPrintF Unsigned %u", 13, 10, 100000, () => { SPrintF("%u", 16384); });
            PerformanceTest(".NET Integer      ", 13, 10, 100000, () => { _ = string.Format("{0}", 16384); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceExponentDouble()
        {
            PerformanceTest("SPrintF Double %e", 13, 10, 100000, () => { SPrintF("%e", 123456.789); });
            PerformanceTest(".NET Double {E}  ", 13, 10, 100000, () => { _ = string.Format("{0:E}", 123456.789); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceExponentSingle()
        {
            PerformanceTest("SPrintF Single %e", 13, 10, 100000, () => { SPrintF("%e", (float)123456.789); });
            PerformanceTest(".NET Single {E}  ", 13, 10, 100000, () => { _ = string.Format("{0:E}", (float)123456.789); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceFixedDouble()
        {
            PerformanceTest("SPrintF Double %f", 13, 10, 100000, () => { SPrintF("%f", 123456.789); });
            PerformanceTest(".NET Double {F}  ", 13, 10, 100000, () => { _ = string.Format("{0:F}", 123456.789); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceFixedSingle()
        {
            PerformanceTest("SPrintF Single %f", 13, 10, 100000, () => { SPrintF("%f", (float)123456.789); });
            PerformanceTest(".NET Single {F}  ", 13, 10, 100000, () => { _ = string.Format("{0:F}", (float)123456.789); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceGeneralDouble()
        {
            PerformanceTest("SPrintF Double %g", 13, 10, 100000, () => { SPrintF("%g", 123456.789); });
            PerformanceTest(".NET Double {G}  ", 13, 10, 100000, () => { _ = string.Format("{0:G}", 123456.789); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceGeneralSingle()
        {
            PerformanceTest("SPrintF Single %g", 13, 10, 100000, () => { SPrintF("%g", (float)123456.789); });
            PerformanceTest(".NET Single {G}  ", 13, 10, 100000, () => { _ = string.Format("{0:G}", (float)123456.789); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        [Test]
        [Category("Manual")]
        [Explicit("Performance Test")]
        public void SPrintF_PerformanceInteger_ForProfiling()
        {
            PerformanceTest("SPrintF Integer %d", 2, 0, 1000, () => { SPrintF("%d", 16384); });

            Assert.Pass();  // S2699: Performance test, user should check results.
        }

        private static void PerformanceTest(string test, int cycles, int validCycles, int count, Action action)
        {
            List<long> results = new List<long>();

            // Call the action once, to ensure the method is compiled.
            action();

            // Do the test multiple times.
            for (int j = 0; j < cycles; j++) {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                for (int i = 0; i < count; i++) {
                    action();
                }
                sw.Stop();
                results.Add(sw.ElapsedMilliseconds);
            }

            if (validCycles <= 0 || validCycles > count) validCycles = cycles;
            PerformanceTestResults(test, results, validCycles);
        }

        private static void PerformanceTestResults(string test, List<long> results, int validCycles)
        {
            Console.Write("{0}:  ", test);

            results.Sort();
            long sum = 0;

            for (int i = 0; i < validCycles; i++) {
                sum += results[i];
            }

            Console.WriteLine("n={0}: Average={1:F0}", validCycles, sum / validCycles);
        }
    }
}
