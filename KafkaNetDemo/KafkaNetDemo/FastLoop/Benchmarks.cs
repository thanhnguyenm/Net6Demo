using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace FastLoop
{
    [MemoryDiagnoser(false)]
    public class Benchmarks
    {
        [Params(100, 100_000, 1_000_000)] public int Size { get; set; }
        private int[] _items;

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random(420);
            _items = Enumerable.Range(0, Size).Select(x=>random.Next()).ToArray();
        }

        [Benchmark]
        public int[] For()
        {
            for (var i = 0; i < _items.Length; i++)
            {
                var item = _items[i];
                DoSomething(item);
            }

            return _items;
        }

        [Benchmark]
        public int[] Foreach()
        {
            foreach (var item in _items)
            {
                DoSomething(item);
            }

            return _items;
        }

        [Benchmark]
        public int[] For_span()
        {
            Span<int> asSpan = _items;
            for (var i = 0; i < asSpan.Length; i++)
            {
                var item = asSpan[i];
                DoSomething(item);
            }

            return _items;
        }


        [Benchmark]
        public int[] Unsafe_For_Span_GetRef()
        {
            Span<int> asSpan = _items;
            ref var searchSpace = ref MemoryMarshal.GetReference(asSpan);
            for (var i = 0; i < asSpan.Length; i++)
            {
                var item = Unsafe.Add(ref searchSpace, i);
                DoSomething(item);
            }

            return _items;
        }

        public void DoSomething(int item)
        {
            
        }
    }

    public class IntWrap
    {
        public int Value { get; }

        public IntWrap(int value)
        {
            Value = value;
        }
    }
}
