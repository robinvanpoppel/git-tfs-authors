# git-tfs-authors

## Introduction
git-tfs-authors is a small utility to create an authors file, which you can use with the git-tfs clone command. It'll create a file with entries in the form of.

    DOMAIN\123123 = Doe, John <john.doe@company.com>
    DOMAIN\234234 = Doe, Jane <jane.doe@company.com>

## Synopsis

	Usage: git-tfs-authors clone tfs-url authors-file
	  ex : git tfs clone http://my-tfs-server:8080/tfs authors.txt
    
## Dependencies
You'll need Visual Studio 2017 to build, because of the references to Microsoft.TeamFoundation.Client.dll
    
## See also
* git-tfs-clone: https://github.com/git-tfs/git-tfs/blob/master/doc/commands/clone.md
