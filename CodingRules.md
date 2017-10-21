# CODING RULES

## A- Empty directory

Empty directory must be filled with an empty file named `no_media`. To make git able to version the empty directory.

## B- Commit messages

Every commit message must follow this naming’s pattern : 
```[PRE] Here is a new commit```

Here is a list of the existing prefixes, every commit message should have exactly one.

Prefixes | Meaning
-------- | --------
FCT | new functionality
WIP | work in progress (to save a state in the repository)
BUG | bug fixes
FIX | compilation fixes
GRP | new graphics
QA | coding rules fixes, quality of code or optimisations
OTH | other commit

## C- Script

All the script should be writed in `C#`.

Scripts should follow the template you can found here :
```./ScriptTemplates/81-C# Script-NewBehaviourScript.cs.txt```

Note: You can use this template as the default ```.cs``` template in Unity by replacing the file located in :
```%UNITY_INSTALL_DIR%/Editor/Data/Resources/ScriptTemplates/81-C# Script-NewBehaviourScript.cs.txt```

### 1. Script naming

```
Notes
```
CamelCase

Prefix GUI, if it's intended to act on GUI. GUI script should only concern the view (in MVC pattern).
Prefix ONE, if it's intended to be instantiated only one time in a scene (singleton for example).

### 2. Variable

Every variable must follow this naming's pattern : `prefix_clearAndUniqueCamelCasedName`

Here is a list of the existing prefixes :

Prefixes | Meaning
-------- | --------
c | constant or static readonly variable
g | global variable
m | member variable
p | method's or function's parameter
s | static member variable

There is three exceptions :
* local variable
* the C#'s property that should follow this pattern : `ClearAndUniquePascalCasedName`
* iteration variable that should be named by only one letter :
  * coordinate : x, y, z, w, u, v
  * array : i, j, k, l

#### a) Unity's inspector attributes

Should only be private or protected (with [SerializeField] flags).
Also, you can show in non-editable mode class's attributes with the [ReadOnly] flags.

#### b) Member variable

Member variable should only be private or protected.

### 3. Enumeration

Enum name should begin with the Enum prefix.
Each value of an enumeration should begin with the "e_" prefix.
Enumerations should have an additional value naming by the enumeration's name that will serve to count the number of value available in the enumeration.

### 4. Formatting

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
switch (value)
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

switch (value)
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
while (condition)
{
	// stuff here
}

/* for */
for (int i = 0; condition; ++i)
{
	// stuff here
}

/* method */
public void ClearAndUniquePascalCasedName(int p_parameter1, float p_parameter2, ...)
{
	// stuff here
}

/* foreach */
for (Type element in p_list)
{
	// stuff here
}

/* enumeration */
public enum EnumNumbers
{
	e_one,
	e_two,
	e_three,
	e_four,
	e_Numbers
}

/* strictly forbidden */
( condition )? ( ( condition )? stuff11 : stuff12 ) : stuff2

```

### 5. Scope

No more than 3 levels of scope.
