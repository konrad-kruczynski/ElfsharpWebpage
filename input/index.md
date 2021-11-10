# ELFSharp

This is *ELFSharp* by Konrad Kruczyński (and other [authors](authors.html)), a C# library for reading [ELF](http://en.wikipedia.org/wiki/Executable_and_Linkable_Format), UImage and [Mach-O](https://en.wikipedia.org/wiki/Mach-O) files. It is an open source software available on a permissive X11 license and hosted on GitHub. For obtaining the binary or getting the source code please visit [the download page](download.html). You can learn how to use this library by visiting the [examples section](examples.html)

****

# News

## 2021-11-10
ELFSharp 2.13.2 released. Fixed a bug when reading a NOBITS ELF section.

## 2021-11-02
ELFSharp 2.13.1 released. Fixed a bug when 64-bit big-endian Mach-O files were treated as 32-bit.

## 2021-07-20
ELFSharp 2.13 released. Added Mach-O file flags.

## 2021-06-21
ELFSharp 2.12.1 released. Fixes a `InvalidOperationException` when parsing intermediate object file.

## 2021-03-21
The ELFSharp webpage is now statically generated using [Statiq.Web](https://statiq.dev/web/), all subpages should be working fine now (previously only the news page was available).

## 2020-12-18
ELFSharp 2.12 released. Four new MachO commands are now available: `LC_ID_DYLIB`, `LC_LOAD_DYLIB`, `LC_LOAD_WEAK_DYLIB` and `LC_REEXPORT_DYLIB`. Also a `CHANGELOG` file was introduced, documenting all the changes using the format based on [Keep a changelog](https://keepachangelog.com/en/1.0.0/).

## 2020-05-31
ELFSharp 2.11.0 released. Fat Mach-O binaries are handled from now.

## 2020-05-17
ELFSharp 2.10.0 released. The library now provides symbol visibility in symbol table.

## 2020-05-02
ELFSharp 2.9.0 released. The library now supports big endian Mach-O files.

## 2020-04-19
ELFSharp 2.8.1 released. Offset parsing issue in MachO is now fixed, also data type of some properties was changed to a proper (unsigned) one.

## 2020-04-11
ELFSharp 2.8.0 released. NOTE segment is now handled by the library. This is a different animal than NOTE section, altough having the same internal structure.

## 2020-04-04
ELFSharp 2.7.0 released. Now stream overleads are available also for Mach-O and UImage.

## 2020-03-29
ELFSharp 2.6.1 released. This fixes a regression that happened during addition of stream reading feature. It resulted in corrupted contents of the sections and should now be fixed.

## 2020-03-21
ELFSharp 2.6.0 released. The library now targets .NET Standard 2.0 *only*.

## 2020-03-16
ELFSharp 2.5.0 released. Properties `Sections` and `Segments` are now of type `IReadOnlyList<T>` (instead of the previously used `IEnumerable<T>`), so that one can easily check their count or access them by a numeric index.

## 2020-02-15
ELFSharp 2.4.0 released. ELFReader now offers new overloads that let you read ELF directly from stream, instead from a file only. Therefore one is able to use e.g. MemoryStream directly. Note that the stream have to be seekable in order to be properly read.

## 2020-02-02
ELFSharp 2.3.0 released. Basic .dynamic section parsing was added.

## 2019-12-08
ELFSharp 2.2.1 released. This version is able to open files with a dynamic string table being a `NOBITS` section. I wasn't able to find a part of the ELF specification that allows that, but apparently such binaries exists in the wild.

## 2019-08-15
ELFSharp 2.2 released. This version is able to silently ignore non-conforming note section.

## 2018-10-27
ELFSharp 2.1.1 released. This release fixes getting memory contents of a segment when memory size is greater than file size.

## 2018-05-19
`ulong` instead of `long` is now used as a generic type for 64-bit ELF files, which resolves a bug. This is, however, an API breaking change so - according to semantic versioning - I have to bump the major version number. ELFSharp 2.0 is therefore released.

## 2018-03-18
ELFSharp 1.3 released. With this version, when getting segment contents, you should use one of the two new functions, to get either contents as it was written in the file, or as it should be loaded into memory. Old function is still there and works, but since its name is now misleading, it was turned obsolete.

## 2018-02-10
ELFSharp 1.2 is the first version that does not use MiscUtil internally.

## 2018-01-21
ELFSharp is now a .NET Standard 2.0 library. Also tests were updated and they are again available in the main repository. This is released on NuGet - version 1.1. Also - because of that - MiscUtil parts are directly embedded as it used to be some time ago (no MiscUtil for .NET Standard 2.0), but I plan to rewrite endianess related parts of the code, so they should soon be removed from the project.

## 2016-11-06
The offset of a segment is now public.

## 2016-06-06
Strongly named assembly is now available.

## 2016-04-30
New version of ELFSharp is available. From this version on I will not longer offer a binary to download. This is because I removed inlined MiscUtil library sources in favor of having the library as a NuGet dependency. I've also started using [different pattern of versioning](https://codingforsmarties.wordpress.com/2016/01/21/how-to-version-assemblies-destined-for-nuget/). Last but not least - basic Mach-O support is now there. Enjoy!

## 2015-03-13
New ELFSharp available, with some fixes regarding strings handling (we use UTF-8 now).

## 2014-12-05
New version of ELFSharp available, with a method to try getting section and some fixes. You can also get it from NuGet.

## 2013-12-03
New version of ELFSharp, with UImage support! 0.0.10 is already available for download, I'll soon put it on nuget gallery.

## 2013-08-29
New version of ELFSharp released, namely 0.0.9. Bug fixes, tests fixed and new API for testing whether file is an ELF (and of which class) file added.

## 2013-03-08
New release with [examples](examples.html) upgraded.

## 2012-05-23
ELFSharp is now available as a [package](http://nuget.org/packages/ELFSharp/) on nuget.org with the (not surprising) id `ELFSharp`.

## 2012-05-15
The new version of ELFSharp (0.0.5, precisely speaking) was just released. There was some cleanup in code, minor fixes, but, what is most important, two subnamespaces were introduced: Sections and Segments. Their purpose should be self describing ;)

## 2012-04-21
Some new stuff:
- releasing policy has been changed;
- API has been changed siginificantly, the 32/64 bit differences should be much easier to handle now (at least I hope so);
- bug fixes and refactorings, as usual.

## 2011-10-23
ELFSharp can be now used to read the NOTE section.

## 2011-10-09
Today's release contains some important bug fixes. Also missing fields for section and program header were added.

## 2011-09-25
Library is now able to explore 64-bit ELFs. Enjoy!

## 2011-09-14
Using the library, you can now explore program headers and get program images from file.

## 2011-09-07
The library should now work properly with non-native endianess (which usually means big endian).

## 2011-09-06
The library development has just started and an initial push was made to <a href="https://github.com/konrad-kruczynski/elfsharp">GitHub</a>.