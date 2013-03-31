This is a prototype of a RethinkDB client driver written in C# for the .NET platform.  This driver utilizes .NET 4.5 and C# 5.0.

Currently this driver is capable of the following things:
  
  * Connecting to a RethinkDB database.
  
  * A decent collection of RethinkDB queries:

    * DbList
    * DbCreate
    * DbDrop
    * TableList
    * TableCreate
    * TableDrop
    * Table
    * Get
    * Between
    * Filter
    * Update
    * Delete
    * Replace
    * Count
    * OrderBy
    * Skip
    * Limit
    * Slice
    * Nth
    * InnerJoin
    * OuterJoin
    * EqJoin
    * Zip
    * Distinct
    * Map
    * Reduce
    * GroupedMapReduce

  * Filter, Update, Inner/Outer/EqJoin, Map, Reduce, etc. can be built using C# expressions (with limitations) that are compile-time safe, and are automatically translated into RethinkDB's query language.  For example, `Query.Db("db").Table<ObjectDefinition>("objects").Update(o => new ObjectDefinition { Name = o.Name + " (new name!)" })`.

  * Converting data into objects and objects into data; non-primtiive objects are marked up using [DataContract] and [DataMember] attributes similar to WCF data contracts.

  * Anonymous types can be used in functions like Map and Reduce.  For example:
    ```
    Query
      .Db("db")
      .Table<ObjectDefinition>("objects")
      .Map(od => new { Sum = od.Value, Count = 1.0 })
      .Reduce((left, right) => new { Sum = left.Sum + right.Sum, Count = left.Count + right.Count })
    ```

  * Strong compile-time safety using generics.  Every query operation knows what type it returns, and whether it returns an object or an enumerable.

  * Performing absolutely everything asynchronously, with a synchronous API too if desired.

  * Reading streaming / chunked datasets using an async iterator.

  * Being 100% compatible with Mono (3.0+).


Currently this driver is lacking in the following areas:

  * Does not support schema-free / free-format objects.  Although the object conversion routines are interfaced out so that they can be replaced with something different as required, the first goal is to provide a client that works well in a native C# environment, and that implies type safety and structure.

  * Limited support for serialized object types with the provided DataContract-based datum converter classes.  No "int"s, for example; only doubles.

  * Documentation.  You're reaching the end of it, and it probably hasn't helped you at all.


I welcome pull requests.  This is just the start of a RethinkDB client for .NET.  It's nowhere near the end.
