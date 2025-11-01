Compare your measured times to your Part 1 predictions. Where do the results clearly reflect Big-O expectations?

List.Contains grows lineraly with N. For the Last element, it is slower than checking a missing element, which matches my prediction.

Any surprises? (e.g., why List.Contains might be “fast enough” for small N; constant factors; JIT effects; your machine variability.)

For small N (e.g., 1,000), all membership checks appeared almost instantaneous. This is because the dataset is small, and operations complete within a fraction of a millisecond.

Using Stopwatch.ElapsedMilliseconds initally rounded these times to 0 ms, so I used Elapsed.TotalMilliseconds to actually see and compare. 

Given a large dataset and many membership checks, which structure would you choose and why?

For a large datasetand frequent membership checks, a HashSet or Dictionary is preferred because lookups are O(1) on average, making them scalable for Large N. Lists become increasingly slow as datasets grow making it inefficient. 