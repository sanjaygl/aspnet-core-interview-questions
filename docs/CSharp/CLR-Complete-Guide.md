# CLR Working Process — .NET Execution Model

How C# source code becomes running machine code, and the components involved along the way.

---

## High-Level Diagram

```
 ┌────────────────┐
 │  C# / VB.NET /  │   (or any .NET language)
 │  F# source code │
 └────────┬────────┘
          │ compiled by language compiler (csc.exe, etc.)
          ▼
 ┌────────────────────────────┐
 │ Assembly (.dll / .exe)     │
 │  - IL (Intermediate Lang)  │
 │  - Metadata                │
 │  - Manifest                │
 └────────┬────────────────────┘
          │ loaded at runtime
          ▼
 ┌───────────────────────────────────────────────────────────┐
 │                     CLR (Common Language Runtime)           │
 │                                                             │
 │   ┌───────────────┐   ┌────────────────┐   ┌─────────────┐ │
 │   │ Class Loader   │→│  JIT Compiler   │→│ Native Code  │ │
 │   │ (loads IL +    │  │ (IL → native    │  │ (executed   │ │
 │   │  metadata)     │  │  machine code)  │  │  by CPU)    │ │
 │   └───────────────┘   └────────────────┘   └─────────────┘ │
 │                                                             │
 │   ┌────────────────────────────────────────────────────┐   │
 │   │  Runtime Services (run alongside execution)         │   │
 │   │  - Garbage Collector (memory management)            │   │
 │   │  - Exception Handling                                │   │
 │   │  - Type Safety / Security                            │   │
 │   │  - Thread Management                                 │   │
 │   └────────────────────────────────────────────────────┘   │
 └───────────────────────────────────────────────────────────┘
          │
          ▼
 ┌────────────────────┐
 │  Operating System /  │
 │  CPU (native exec)   │
 └────────────────────┘
```

**Flow in one line:** Source code → compiled to IL (not native code) → CLR loads the assembly → JIT compiles IL to native code method-by-method, on first call → CPU executes native code → GC reclaims unused memory in the background throughout.

---

## Component Definitions

### CLR (Common Language Runtime)
The virtual machine/execution engine that runs .NET applications. It doesn't execute IL directly — it manages the whole lifecycle: loading assemblies, verifying type safety, invoking the JIT to produce native code, running the garbage collector, handling exceptions, and enforcing security. Every .NET language ultimately runs on top of the same CLR, which is what makes cross-language interop possible.

### IL (Intermediate Language)
Also called CIL (Common Intermediate Language) or MSIL. It's the CPU-independent, low-level instruction set that language compilers (csc for C#, vbc for VB.NET, fsc for F#) produce instead of native machine code. IL is stored inside the compiled assembly (.dll/.exe) along with metadata describing types, members, and references. IL is never executed directly by the CPU — the CLR's JIT compiler translates it to native code first.

### JIT (Just-In-Time Compiler)
The component inside the CLR that translates IL into native machine code specific to the CPU/OS the app is actually running on. "Just-in-time" means this happens on demand, method-by-method, the first time a method is called — not all at once when the app starts. Compiled native code is cached in memory for the process's lifetime, so subsequent calls to the same method skip recompilation. (Variants: pre-JIT/AOT compilation trades this startup cost for compiling ahead of time, e.g. ReadyToRun or Native AOT.)

### Native Language / Native Code
The machine code specific to a given CPU architecture and OS (e.g., x64 instructions on Windows) — the only form of code the physical processor can actually execute. IL and source code are both abstractions above this; native code is the final, concrete output the JIT produces, and it's not portable across architectures the way IL is.

### Garbage Collector (GC)
The CLR's automatic memory manager. It tracks object allocations on the managed heap and periodically reclaims memory for objects no longer reachable from any root (local variables, static fields, CPU registers, etc.), so developers don't manually free memory (no `delete`/`free`). It runs generationally (Gen 0, Gen 1, Gen 2) on the assumption that most objects die young — Gen 0 collections are frequent and cheap, Gen 2 (long-lived objects) collections are rarer and more expensive.

### CTS (Common Type System)
The specification that defines how types are declared, used, and managed across all .NET languages — the rules for what a class, struct, interface, enum, or delegate *is* at the runtime level, including value types vs. reference types. Because every .NET language maps its own types down to the same CTS, a C# `int` and a VB.NET `Integer` are the exact same runtime type (`System.Int32`), which is what makes cross-language type interoperability possible.

### CLS (Common Language Specification)
A subset of the CTS — a stricter set of rules that, if a library follows them, guarantees the library can be consumed from *any* CLS-compliant .NET language. For example, CTS allows unsigned integers, but not all .NET languages support them, so CLS excludes public unsigned integers from its rules for publicly exposed members. Marking an assembly `[CLSCompliant(true)]` asks the compiler to flag any public API that isn't CLS-safe.

---

## Putting It Together

1. You write C# code; `csc` compiles it into an assembly containing **IL** + metadata (this is what CTS/CLS govern — the type shapes and rules baked into that metadata).
2. At runtime, the **CLR** loads the assembly and verifies it.
3. The **JIT** compiles each method's IL into **native code** the first time it's called.
4. The CPU executes that native code directly.
5. Throughout execution, the **Garbage Collector** runs in the background, reclaiming memory from objects that are no longer reachable.

This is also why .NET is described as "managed" — the CLR manages memory, type safety, and execution instead of leaving it to the developer or the OS directly.

---

## Cross Questions

**Q: If IL isn't native code, how does `ildasm`/`ILSpy` let you "decompile" a .NET DLL almost back to readable C#?**
A: IL retains far more structure than native machine code — type names, method signatures, and metadata are preserved (unless obfuscated), so IL is close enough to a direct instruction-by-instruction mapping from C# that decompilers can reconstruct near-original source. Native code strips that structure away, which is why decompiling native binaries is much harder.

**Q: Doesn't compiling method-by-method on first call make an app's first run slow? Why not just JIT everything at startup?**
A: It would waste time compiling methods that never get called (error paths, unused branches, rarely-hit code). JIT-per-method means you only pay the compilation cost for code paths actually exercised. This is also exactly the tradeoff Native AOT/ReadyToRun exist to avoid, for apps where startup latency matters more than peak flexibility.

**Q: If the JIT recompiles nothing after the first call, how does .NET have "tiered compilation"?**
A: Tiered JIT is a refinement of "compile once": tier 0 produces a fast-to-generate, unoptimized version of a method immediately so execution isn't blocked; if that method turns out to be called frequently (a hot path), the JIT recompiles it a second time at tier 1 with heavier optimizations, then swaps it in. So "compiled once" is really "compiled once per tier it qualifies for," not literally one compilation ever.

**Q: Since the GC frees memory automatically, why do memory leaks still happen in .NET apps?**
A: The GC only reclaims objects that are *unreachable*. If a long-lived object (a static field, a cached event subscription, a singleton's list) still holds a reference to something, the GC correctly treats it as "still in use" — even if no code will ever use it again. This is why unsubscribed event handlers and forgotten static collections are the classic source of "leaks" in a garbage-collected language.

**Q: If Gen 0 collections are cheap and frequent, why not just always collect at Gen 0 and skip Gen 1/2 entirely?**
A: Because objects that survive a Gen 0 collection get promoted, and re-scanning long-lived survivors on every single collection would be wasteful — most of them will still be alive next time too. Generations exist precisely so the GC doesn't have to re-examine objects it already has good reason to believe will survive.

**Q: CTS defines the type system — so why does C# let you write `uint`/`ulong` if CLS says unsigned types aren't safe to expose publicly?**
A: CTS and CLS operate at different scopes. CTS defines what the *runtime* supports, which does include unsigned integers — nothing stops you from using `uint` internally in a C# method body. CLS only restricts what you can put on a **publicly exposed** API surface, because a consuming language (historically, some VB.NET versions) might not support unsigned types at all. `[CLSCompliant(true)]` only flags public members, not private/internal usage.

**Q: If every .NET language compiles to the same IL and uses the same CTS, why can't you always mix languages freely — why do CLS violations even matter?**
A: Sharing the same IL/CTS guarantees the *runtime* can execute mixed-language code — but a consuming language's *compiler* still has to be able to express a call to your API in its own syntax. If your public method takes a `uint`, a language whose syntax has no unsigned integer literal can't call it directly, even though the CLR itself has no problem executing it. CLS compliance is a source-level interop guarantee, not a CLR execution guarantee — the CLR already treats all CTS types the same.

**Q: The CLR "verifies type safety" when loading an assembly — what's it actually protecting against, given the compiler already type-checked the code?**
A: The compiler that produced the IL might have already validated it, but the CLR can't assume the IL it's loading came from a trustworthy or bug-free compiler — the assembly could've been hand-crafted, generated by a third-party tool, or tampered with. Verification re-checks that operations are being performed on compatible types at the IL level (e.g., not treating an `object` reference as an `int` in memory), independent of what any compiler upstream promised.
 