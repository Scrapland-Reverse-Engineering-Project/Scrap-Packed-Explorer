﻿using CommandLine;
using System;

namespace ch.romibi.Scrap.Packed.Explorer.Cli {
    internal abstract class BaseOptions {
        [Value(0, Required = true, MetaName = "Packed file", HelpText = "The .packed file to use as basis")]
        public String PackedFile { get; set; }
    }

    internal abstract class ModifyingOptions : BaseOptions {
        [Option('o', "outputPackedFile", Required = false, Default = "", HelpText = "Where to store the new .packed file. Modify input if not provided.")]
        public String OutputPackedFile { get; set; }

        [Option('k', "keepBackup", Required = false, Default = false, HelpText = "Keep the backup file that gets created during saving even after successful processing.")]
        public Boolean KeepBackup { get; set; }

        [Option("overwriteOldBackup", Required = false, Default = false, HelpText = "Allow overwriting existing .bak files")]
        public Boolean OverwriteOldBackup { get; set; }
    }

    [Verb("add", HelpText = "Add file to the container")]
    internal class AddOptions : ModifyingOptions {
        [Option('s', "sourcePath", Required = true, HelpText = "What file or folder to add to the .packed file")]
        public String SourcePath { get; set; }

        [Option('d', "packedPath", Required = false, Default = "", HelpText = "What path to put the source file(s) into")]
        public String PackedPath { get; set; }
    }

    [Verb("remove", HelpText = "Remove a file from the container")]
    internal class RemoveOptions : ModifyingOptions {
        [Option('d', "packedPath", Required = true, HelpText = "What path to remove from the container")]
        public String PackedPath { get; set; }
    }

    [Verb("rename", HelpText = "rename a file or folder inside the container")]
    internal class RenameOptions : ModifyingOptions {
        [Option('s', "oldPackedPath", Required = true, Default = "/", HelpText = "What path to rename inside the container")]
        public String OldPackedPath { get; set; }

        [Option('d', "newPackedPath", Required = true, HelpText = "The new path to use for the files to rename")]
        public String NewPackedPath { get; set; }
    }

    [Verb("extract", HelpText = "Extract/unpack a file from the container")]
    internal class ExtractOptions : BaseOptions {
        [Option('s', "packedPath", Required = false, Default = "", HelpText = "What path to extract from the container")]
        public String PackedPath { get; set; }

        [Option('d', "destinationPath", Required = true, HelpText = "The path to extract the files from the container to")]
        public String DestinationPath { get; set; }

        // todo add overwrite options
    }

    [Verb("list", HelpText = "list or search files and folders in the container")]
    internal class ListOptions : BaseOptions {
        [Option('l', "outputStyle", Required = false, Default = OutputStyles.List, HelpText = "Output list (default) or tree view")]
        public OutputStyles OutputStyle { get; set; }

        [Option('q', "searchString", Required = false, Default = "", HelpText = "A Search string to filter the output with")]
        public String SearchString { get; set; }

        [Option('r', "regex", Required = false, Default = false, HelpText = "Defines if the search string is a regular expression")]
        public Boolean IsRegex { get; set; }

        [Option('b', "matchBeginning", Required = false, Default = false, HelpText = "Apply search query only to beginnng of the files path. By default applies everywhere")]
        public Boolean MatchBeginning { get; set; }

        [Option('f', "matchFilename", Required = false, Default = false, HelpText = "Search only by files. By default search includes folders")]
        public Boolean MatchFilename { get; set; }

        [Option('s', "showFileSize", Required = false, Default = false, HelpText = "Show files sizes")]
        public Boolean ShowFileSize { get; set; }

        [Option('o', "showFileOffset", Required = false, Default = false, HelpText = "Show files offsets")]
        public Boolean ShowFileOffset { get; set; }
    }

    [Flags]
    public enum OutputStyles {
        List = 0x1,
        Tree = 0x2,
        Name = 0x4
    }
}
