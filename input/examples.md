# Examples
## Listing an ELF's section headers
Let's say you have a typical hello world C program, compiled using GCC on Linux. To examine it:
```csharp
var elf = ELFReader.Load("hello");
foreach(var header in elf.Sections)
{
	Console.WriteLine(header);
}
```
		
Output:
```
: Null, load @0x0, 0 bytes long
.interp: ProgBits, load @0x400200, 28 bytes long
.note.ABI-tag: Note, Type=1
.note.gnu.build-id: Note, Type=3
.gnu.hash: 1879048182, load @0x400260, 28 bytes long
.dynsym: DynamicSymbolTable, load @0x400280, 96 bytes long
.dynstr: StringTable, load @0x4002E0, 61 bytes long
.gnu.version: 1879048191, load @0x40031E, 8 bytes long
.gnu.version_r: 1879048190, load @0x400328, 32 bytes long
.rela.dyn: RelocationAddends, load @0x400348, 24 bytes long
.rela.plt: RelocationAddends, load @0x400360, 72 bytes long
.init: ProgBits, load @0x4003A8, 26 bytes long
.plt: ProgBits, load @0x4003D0, 64 bytes long
.text: ProgBits, load @0x400410, 420 bytes long
.fini: ProgBits, load @0x4005B4, 9 bytes long
.rodata: ProgBits, load @0x4005C0, 13 bytes long
.eh_frame_hdr: ProgBits, load @0x4005D0, 44 bytes long
.eh_frame: ProgBits, load @0x400600, 172 bytes long
.init_array: 14, load @0x6006B0, 8 bytes long
.fini_array: 15, load @0x6006B8, 8 bytes long
.jcr: ProgBits, load @0x6006C0, 8 bytes long
.dynamic: Dynamic, load @0x6006C8, 464 bytes long
.got: ProgBits, load @0x600898, 8 bytes long
.got.plt: ProgBits, load @0x6008A0, 48 bytes long
.data: ProgBits, load @0x6008D0, 16 bytes long
.bss: NoBits, load @0x6008E0, 8 bytes long
.comment: ProgBits, load @0x0, 56 bytes long
.shstrtab: StringTable, load @0x0, 264 bytes long
.symtab: SymbolTable, load @0x0, 1536 bytes long
.strtab: StringTable, load @0x0, 566 bytes long
```

****
## Getting the names of function symbols
```csharp
var elf = ELFReader.Load("hello");
var functions = ((ISymbolTable)elf.GetSection(".symtab")).Entries.Where(x => x.Type == SymbolType.Function);
foreach(var f in functions)
{
    Console.WriteLine(f.Name);
}
```

Output:

```
deregister_tm_clones
register_tm_clones
__do_global_dtors_aux
frame_dummy
__libc_csu_fini
puts@@GLIBC_2.2.5
_fini
__libc_start_main@@GLIBC_2.2.5
__libc_csu_init
_start
main
_init
```

****
## Writing all the loadable `ProgBits` sections to an array

```csharp
var sectionsToLoad = elf.GetSections<ProgBitsSection<ulong>>()
    .Where(x => x.LoadAddress != 0);
foreach (var s in sectionsToLoad)
{
    s.GetContents().CopyTo(array, s.LoadAddress);
}
```

****
<a name="bit32"></a>
## Handling 32-bit and 64-bit ELFs

Every ELF file belongs to one of defined class. The class defines length of some values encountered in ELF file, symbol entry load address being an example. Currently, classes known to me (and ELFSharp as well) are 32bit and 64bit, respectively from the original ELF and ELF-64 standard. In the ELFSharp API, the ELF class dependent values are reflected in the generic parameter of the class (in the C# sense), such parameter defines type of those values. Consequently it will usually be int or uint for 32-bit and long or ulong for the 64-bit ones (but not necessarily!). Every such generic class implements also the non-generic interface, which you can use if you are interested only in class-independent values (because interfaces contain only those).

One with some awareness of the ELF standard will probably notice that the standard defines more than one type of class-dependent values. That's true and suggests approach with more than one generic parameter, i.e. each for corresponding type. For the simplicity and because of the IMO not important differences between those I decided to use only one. Please also note that the generic type is the one to which values are converted during read, so it is (for example) perfectly fine to use long for the 32-bit ELF files.

Some properties, like symbol name, are just strings, so they are independent of ELF class. If you are interested in these, you can limit yourself to interfaces only. This is how the examples above work. However, if you want to get the entry point for an ELF, you have to use generic class. For example:

```csharp
ELFReader.Load<uint>("filename");
```
or:
```csharp
ELFReader.Load<ulong>("filename");
```

The above functions return `ELF<uint>` or `ELF<ulong>` instead of the more general `IELF`. In fact, `ELF.Load` does that too, however the *declared* type is simply `IELF`. The question that arises here is: what actual types are returned by `ELF.Load`? These are `ELF<uint>` for 32-bit and `ELF<ulong>` for 64-bit ones.

If you are using the specialized generic class, some fields which are also available in the general ELF class return specialized counterparts. For example:
```csharp
var elf = ELFReader.Load("hello");
var elf32 = ELFReader.Load<uint>("hello");
// inferenced type for sections is IEnumerable<ISection>
var sections = elf.Sections;
// but for sections32 it is IEnumerable<Section<uint>>
var sections32 = elf32.Sections;
```

Like earlier, `Section<uint>` has some fields which `ISection` does not have, for example `LoadAddress`. So the general rule is that from specialized ELF object you get specialized objects, from these other specialized objects and so on. Therefore you usually have to care only once – or don't care at all if you want to explore class independent properties.

Getting a section using parametrized GetSection method needs a specialized parameter if you are interested in specialized fields. For example by calling:

```csharp
elf.GetSections<IProgBitsSection>()
```
you will of course get a collection of IProgBitsSection objects which are not specialized. If you are, for example, interested in a section's load address, you have to call:
```csharp
elf.GetSections<ProgBitsSection<TYPE>>()
```
with `TYPE` being the type matching one used in the `ELF.Load`.

****
## Extracting some properties from UImage

```csharp
var uImage = UImageReader.Load("uImage-panda");
Console.WriteLine(uImage.Size);
Console.WriteLine(uImage.EntryPoint);
Console.WriteLine(uImage.LoadAddress);
Console.WriteLine(uImage.CRC);
Console.WriteLine(uImage.Name);
```
Output:
```
3120712
2147516416
2147516416
1819783982
Linux-3.2.0
```
			
****
## Extracting some properties from UImage
```csharp
var uImage = UImageReader.Load("uImage-panda");
uImage.GetImageData().CopyTo(array, 0);
```

****