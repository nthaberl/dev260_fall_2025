Benchmark Results (Best Times)

### N = 1000
|Data Structure | Time |
|---|---|
| List.Contains(N-1)   | 0.318  ms |
| HashSet.Contains     | 0.2438 ms |
| Dict.ContainsKey     | 0.0042 ms |
| List.Contains(-1)    | 0.0027 ms |
| HashSet.Contains(-1) | 0.0017 ms |
| Dict.ContainsKey(-1) | 0.0012 ms |

### N = 10000
|Data Structure | Time |
|---|---|
| List.Contains(N-1)   | 0.0009 ms |
| HashSet.Contains     | 0.0002 ms |
| Dict.ContainsKey     | 0.0002 ms |
| List.Contains(-1)    | 0.0009 ms |
| HashSet.Contains(-1) | 0.0002 ms |
| Dict.ContainsKey(-1) | 0.0001 ms |

### N = 100000
|Data Structure | Time |
|---|---|
| List.Contains(N-1)   | 0.0113  ms |
| HashSet.Contains     | 0.0003  ms |
| Dict.ContainsKey     | 0.0003  ms |
| List.Contains(-1)    | 0.007   ms |
| HashSet.Contains(-1) | 0.0002  ms |
| Dict.ContainsKey(-1) | 0.0002  ms |

### N = 250000
|Data Structure | Time |
|---|---|
| List.Contains(N-1)   | 0.0363  ms |
| HashSet.Contains     | 0.0002  ms |
| Dict.ContainsKey     | 0.0003  ms |
| List.Contains(-1)    | 0.0177  ms |
| HashSet.Contains(-1) | 0.0003  ms |
| Dict.ContainsKey(-1) | 0.0002  ms |

My predictions on time complexity seem to roughly match the results. Lists have to be iterated through so they take longer, whereas the HashSet and Dictionary are faster in every test of N. What I'm most surprised about is how much slower the operations are when N=1000. I would have guessed that smaller sample size means faster runtime but this was not the case and N=10000 seems to be the sweet spot. Also running the tests multiple times showed the greatest variability in speed with Lists. 
For large data sets I would choose between HashSet or Dictionary, depending on the use. If just doing membership checks, a HashSet, but it data needs to be stored with each value, then a Dictionary.