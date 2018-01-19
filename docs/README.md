# RJCP.Text

This assembly module contains an implementation of the C function sprintf(),
ported to C#.

## History

It was written originally for another project in SVN, then ported to GIT. The
contents of this repository attempts to recreate some of the history, although
it is not identical to the original project that had different build scripts,
dependencies and more content.

As such, the project files are not the original, but the source code is. Each
commit is taken, merged, tested that it runs against VS2015 with NUnit 2.6.4.
There is a reference to my NUnitExtensions project. To check out the correct
version, look at the date of the commit in this repository, and check out the
immediate previous commit by date. Only the last commit is managed by submodules
in the superproject which this repository is a part of.
