# Introduction
git-tfs-authors is a simple tool to create an authors file, which you can use with the git-tfs clone command.

## Synopsis

	Usage: git-tfs-authors clone tfs-url authors-file
	  ex : git tfs clone http://my-tfs-server:8080/tfs authors.txt
    
## Dependencies
You'll need Visual Studio 2017 to build, because of the references to Microsoft.TeamFoundation.Client.dll, Microsoft.VisualStudio.TeamFoundation.dll, Microsoft.VisualStudio.TeamFoundation.Client.dll, Microsoft.VisualStudio.TeamFoundation.VersionControl.dll
    
## See also
* git-tfs-clone: https://github.com/git-tfs/git-tfs/blob/master/doc/commands/clone.md
