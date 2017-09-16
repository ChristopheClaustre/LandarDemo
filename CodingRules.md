# CODING RULES

## Empty directory

Empty directory must be filled with an empty file named ```no_media```. To make git able to version the empty directory.

## Commit messages

Every commit message must follow this naming’s pattern : 
```[PRE] Here is a new commit```

Here is a list of the existing prefixes, every commit message should have exactly one.

Prefixes | Meaning
-------- | --------
FCT | new functionnality
BUG | bug fixes
FIX | compilation fixes
GRP | new graphics
CDR | coding rules fixes
OTH | other commit

## Script

All the script should be writed in `C#`.

### Script naming

```
Notes
```
CamelCase

prefixe GUI, if it's intended to act on GUI

GUI script should not store game informations

### Format of `C#` files

`C#` files should follow this template : [this file](./template.cs)

Note: You can use this template as the default ```.cs``` template in Unity (see [here](http://answers.unity3d.com/questions/120957/change-the-default-script-template.html))

### Scope

```C#
/* if */
if ( condition )
{
	// stuff here
}

/* ternary */
( cond )? stuff1 : stuff2 ;

function( ( cond )? stuff1 : stuff2 )

/* switch */

// TODO

/* while */
while ( condition )
{
	// stuff here
}

/* for */
for ( int i = 0 ; cond ; ++i )
{
	// stuff here
}

// TODO 'for each' syntax

/* strictly forbidden */
( cond )? ( ( cond )? stuff11 : stuff12 ) : stuff2

```

### Variable naming

Every variable must follow this naming's pattern :
`prefix_clearlyComprehensibleAndUniqueCamelCasedName`

Here is a list of the existing prefixes, every variable should have at least one (excepts for iteration variable).

Prefixes | Meaning
-------- | --------
A | C#'s assessor
c | constant variable
g | global variable
l[num] | local variable (num is an optional digit representing the declaration's scope level) [1]
m | attribute
p | parameter
s | static member variable

[1]
```C#
class Something
{
	// SCOPE nothing
	void function()
	{
		// SCOPE 0
		if (cond)
		{
			// SCOPE 1
			while ()
			{
				// SCOPE 2
			}
			
			for ()
			{
				// SCOPE 2
			}
		}
	}
}
```
