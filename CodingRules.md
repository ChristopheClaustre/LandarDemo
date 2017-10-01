# CODING RULES

## Empty directory

Empty directory must be filled with an empty file named ```no_media```. To make git able to version the empty directory.

## Commit messages

Every commit message must follow this naming’s pattern : 
```[PRE] Here is a new commit```

Here is a list of the existing prefixes, every commit message should have exactly one.

Prefixes | Meaning
-------- | --------
FCT | new functionality
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

Prefix GUI, if it's intended to act on GUI. GUI script should not store game informations.
Prefix MONO, if it's intended to be instantiated only one time in a scene (singleton for example).

### Format of `C#` files

`C#` files should follow this template : [this file](./template.cs)

Note: You can use this template as the default ```.cs``` template in Unity (see [here](http://answers.unity3d.com/questions/120957/change-the-default-script-template.html))

### Variable naming

Every variable must follow this naming's pattern : `prefix_clearAndUniqueCamelCasedName`

Here is a list of the existing prefixes :
Prefixes | Meaning
-------- | --------
c | constant variable
g | global variable
l[num] | local variable (num is an optional digit representing the declaration's scope level) [1]
m | member variable
p | parameter
s | static member variable
e | element in a list (exemple: foreach)

There is two exceptions :
* the C#'s assessor that should follow this pattern : `ClearAndUniqueCamelCasedName`
* iteration variable that should be named by only one letter :
  * coordinate : x, y, z, w
  * array : i, j, k, l

### Formatting

```C#
/* if */
if ( condition )
{
	// stuff here
}

/* ternary */
( condition )? stuff1 : stuff2 ;

method( ( condition )? stuff1 : stuff2 )

/* switch */
switch ( value )
{
	case one:
		// stuff here
		break;
	case two:
		// stuff here
		break;
	//...
	default: break;
}

switch ( value )
{
	case one:
		// stuff here
		break;
	case two:
		// stuff here
		break;
	//...
	default:
		// stuff here
		break;
}

/* while */
while ( condition )
{
	// stuff here
}

/* for */
for ( int i = 0 ; condition ; ++i )
{
	// stuff here
}

/* method */
[public/private/protected] [return type] clearAndUniqueCamelCasedName([type] p_parameter1, [type] p_parameter2, ...)
{
	// code here
}

/* foreach */
for ( Type e_element in p_list )
{
	// stuff here
}
// TODO 'for each' syntax

/* strictly forbidden */
( condition )? ( ( condition )? stuff11 : stuff12 ) : stuff2

```

### Scope

No more than 3 level of scope.

## Reference

[1]
```C#
class Something
{
	// SCOPE nothing
	public void method()
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
