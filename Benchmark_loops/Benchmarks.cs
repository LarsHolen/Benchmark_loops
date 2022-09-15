using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;

namespace Benchmark_loops;



[MemoryDiagnoser(false)]
public class Benchmarks
{
    private static readonly Random Rng = new Random(80085);
    [Params(100,100_000,1_000_000, 10_000_000)]
    public int Size { get; set; }

    private List<int> _items;

    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(1, Size).Select(i => Rng.Next()).ToList();
    }

    [Benchmark]
    public void For()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            int item = _items[i];
        }
    }


    [Benchmark]
    public void Foreach()
    {
        foreach (int item in _items)
        {

        }
    }

    [Benchmark]
    public void Foreach_linq()
    {
        _items.ForEach(item => { });
    }

    [Benchmark]
    public void Paralell_Foreach()
    {
        Parallel.ForEach(_items, i => { });
    }

    [Benchmark]
    public void Paralell_Foreach_linq()
    {
        _items.AsParallel().ForAll(i => { });
    }

    [Benchmark]
    public void Foreach_Span()
    {
        foreach (var item in CollectionsMarshal.AsSpan(_items))
        {

        }
    }

    [Benchmark]
    public void For_Span()
    {
        var asSpan = CollectionsMarshal.AsSpan(_items);
        for (int i = 0; i < asSpan.Length; i++)
        {
            var item = asSpan[i];
        }
        foreach (var item in CollectionsMarshal.AsSpan(_items))
        {

        }
    }

}
