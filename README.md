# git-tfs-authors

## Introduction
git-tfs-authors is a small utility to create an authors file, which you can use with the git-tfs clone command. It'll create a file with entries in the form of.

    DOMAIN\123123 = Doe, John <john.doe@company.com>
    DOMAIN\234234 = Doe, Jane <jane.doe@company.com>

## Synopsis

```
Command line arguments
	
-h, --help                 show this message and exit
-a, --authors=VALUE        the name of the output file
-k, --keep-empty-email     keep accounts without an e-mail address (default false)
-s, --sort                 sort the entries in the output file (default	false)
-t, --tfs-server-uri=VALUE the uri of the tfs server (e.g. http://tfs:8080/tfs)

Examples:
GitTfsAuthors --tfs-server-uri http://tfs:8080/tfs
GitTfsAuthors --tfs-server-uri http://tfs:8080/tfs --authors=output.txt
GitTfsAuthors --tfs-server-uri http://tfs:8080/tfs --authors=output.txt --sort
```

## Prerequisites
* You'll need Visual Studio 2017 or 2019 to build, because of the reference to Microsoft.TeamFoundation.Client.dll
    
## See also
* git-tfs-clone: https://github.com/git-tfs/git-tfs/blob/master/doc/commands/clone.md
