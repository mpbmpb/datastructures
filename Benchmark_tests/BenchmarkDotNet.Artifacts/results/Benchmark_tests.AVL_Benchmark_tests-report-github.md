``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.1 (20C69) [Darwin 20.2.0]
Intel Core i5-7267U CPU 3.10GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=5.0.102
  [Host]     : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT
  DefaultJob : .NET Core 5.0.2 (CoreCLR 5.0.220.61120, CoreFX 5.0.220.61120), X64 RyuJIT


```
|        Method |       Mean |   Error |  StdDev | Rank | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |-----------:|--------:|--------:|-----:|------:|------:|------:|----------:|
| Tree1_inserts |   227.9 ns | 3.25 ns | 2.88 ns |    1 |     - |     - |     - |         - |
| Tree2_inserts |   400.0 ns | 6.78 ns | 6.01 ns |    2 |     - |     - |     - |         - |
| Tree3_inserts |   587.4 ns | 0.73 ns | 0.61 ns |    3 |     - |     - |     - |         - |
| Tree4_inserts |   764.6 ns | 3.17 ns | 2.65 ns |    4 |     - |     - |     - |         - |
| Tree5_inserts | 1,159.0 ns | 4.73 ns | 3.95 ns |    5 |     - |     - |     - |         - |
