```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4541/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


```
| Method       | modifierCount | Mean     | Error    | StdDev   |
|------------- |-------------- |---------:|---------:|---------:|
| AddModifiers | 1000          | 14.19 ms | 0.141 ms | 0.132 ms |
