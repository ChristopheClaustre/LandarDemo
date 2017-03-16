# CODING RULES

## Script

All the script should be writed in `C#`

### Script naming

```
Notes
```
CamelCase

prefixe GUI, if it's intended to act on GUI

GUI script should not store game informations

### Format of `C#` files

`C#` files should follow this format :

```C#
/***************************************************
 *** INCLUDE                ************************
 ***************************************************/
using UnityEngine;
using System.Collections;

/***************************************************
 *** THE CLASS              ************************
 ***************************************************/

public class NewBehaviourScript :
	MonoBehaviour
{
	/***************************************************
	 ***  UNITY GUI PROPERTY    ************************
	 ***************************************************/
	
	/********  PUBLIC           ************************/
	
	/********  PROTECTED        ************************/
	
	/********  PRIVATE          ************************/
	
	/***************************************************
	 ***  SUB-CLASSES           ************************
	 ***************************************************/
	
	/********  PUBLIC           ************************/
	
	/********  PROTECTED        ************************/
	
	/********  PRIVATE          ************************/
	
	/***************************************************
	 ***  ATTRIBUTES            ************************
	 ***************************************************/
	
	/********  PUBLIC           ************************/
	
	/********  PROTECTED        ************************/
	
	/********  PRIVATE          ************************/
	
	/***************************************************
	 ***  METHODS               ************************
	 ***************************************************/
	
	/********  PUBLIC           ************************/
	
	// Use this for initialization
	public void Start ()
	{
		
	}
	
	// Update is called once per frame
	public void Update ()
	{
		
	}
	
	/********  PROTECTED        ************************/
	
	/********  PRIVATE          ************************/
	
}
```

### variable naming

Every variable must follow this naming's pattern : *[PREFIX]_clearlyComprehensibleAndUniqueCamelCasedName*

Here is a list of the existing prefixes, every variable should have at least one.

Prefixes | Meaning
-------- | --------
A | assessor on a variable (or not)
c | constant variable
g | global variable
l[num] | local variable (num is an optional digit representing the scope level)
m | member variable
p | parameter
s | static member variable

