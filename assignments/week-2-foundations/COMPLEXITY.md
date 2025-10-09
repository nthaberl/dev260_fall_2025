# Predicting the Complexity

| Structure | Operation | Big-O (Avg) with rationale
|---|---|---|
| Array      | Access by index          | O(1) direct access to index |
| Array      | Search (unsorted)        | O(n) must loop through all indices |
| List       | Add at end               | O(1) direct access to end |
| List       | Insert at index          | O(n) rest of the elements must be shifted to add at index |
| Stack      | Push / Pop / Peek        | O(1) direct access to top |
| Queue      | Enqueue / Dequeue / Peek | O(1) direct access to front and back, no shifting elements |
| Dictionary | Add / Lookup / Remove    | O(1) key value pair allows for direct access, no looping needed|
| HashSet    | Add / Contains / Remove  | O(1) uniqueness allows for instant lookup |