namespace RJCP.Core.Text
{
    using BenchmarkDotNet.Attributes;
    using static StringUtilities;

    public class SPrintFBenchmark
    {
        [Benchmark]
        public void FormatE_Double() => SPrintF("%e", 123456.789);

        [Benchmark]
        public void FormatE_Double_System() => _ = string.Format("{0:E}", 123456.789);

        [Benchmark]
        public void FormatE_Single() => SPrintF("%e", 123456.789f);

        [Benchmark]
        public void FormatE_Single_System() => _ = string.Format("{0:E}", 123456.789f);

        [Benchmark]
        public void FormatF_Double() => SPrintF("%f", 123456.789);

        [Benchmark]
        public void FormatF_Double_System() => _ = string.Format("{0:F}", 123456.789);

        [Benchmark]
        public void FormatF_Single() => SPrintF("%f", 123456.789f);

        [Benchmark]
        public void FormatF_Single_System() => _ = string.Format("{0:F}", 123456.789f);

        [Benchmark]
        public void FormatG_Double() => SPrintF("%g", 123456.789);

        [Benchmark]
        public void FormatG_Double_System() => _ = string.Format("{0:G}", 123456.789);

        [Benchmark]
        public void FormatG_Single() => SPrintF("%g", 123456.789f);

        [Benchmark]
        public void FormatG_Single_System() => _ = string.Format("{0:G}", 123456.789f);

        [Benchmark]
        public void FormatD_Integer() => SPrintF("%d", 16384);

        [Benchmark]
        public void FormatD_Integer_System() => _ = string.Format("{0}", 16384);

        [Benchmark]
        public void FormatU_Unsigned() => SPrintF("%u", 16384);

        [Benchmark]
        public void FormatU_Unsigned_System() => _ = string.Format("{0}", 16384);
    }
}