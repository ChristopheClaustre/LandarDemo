//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.10
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


using System;
using System.Runtime.InteropServices;

namespace Noesis
{

[System.FlagsAttribute]
public enum ManipulationModes {
  None = 0,
  Rotate = 1,
  Scale = 2,
  TranslateX = 4,
  TranslateY = 8,
  Translate = TranslateX|TranslateY,
  All = Rotate|Scale|Translate
}

}

